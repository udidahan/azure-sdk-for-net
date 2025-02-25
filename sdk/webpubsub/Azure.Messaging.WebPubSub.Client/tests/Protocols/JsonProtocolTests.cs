﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.WebPubSub.Clients;
using Xunit;
using Xunit.Sdk;

namespace Azure.Messaging.WebPubSub.Client.Tests.Protocols
{
    public class JsonProtocolTests
    {
        private class JsonData
        {
            public string Value { get; set; }
        }

        public static IEnumerable<object[]> GetParsingTestData()
        {
            static object[] GetData(object jsonPayload, Action<WebPubSubMessage> assert)
            {
                var converter = JsonSerializer.Serialize(jsonPayload);
                return new object[] { Encoding.UTF8.GetBytes(converter), assert };
            }

            yield return GetData(new { type="ack", ackId = 123, success=true }, message =>
            {
                Assert.True(message is AckMessage);
                var ackMessage = message as AckMessage;
                Assert.Equal(123u, ackMessage.AckId);
                Assert.True(ackMessage.Success);
                Assert.Null(ackMessage.Error);
            });
            yield return GetData(new { type = "ack", ackId = 123, success = false, error = new { name = "Forbidden", message = "message"} }, message =>
            {
                Assert.True(message is AckMessage);
                var ackMessage = message as AckMessage;
                Assert.Equal(123u, ackMessage.AckId);
                Assert.False(ackMessage.Success);
                Assert.Equal("Forbidden", ackMessage.Error.Name);
                Assert.Equal("message", ackMessage.Error.Message);
            });
            yield return GetData(new { sequenceId = 738476327894u, type = "message", from = "group", group = "groupname", dataType = "text", data = "xyz", fromUserId = "user" }, message =>
            {
                Assert.True(message is GroupDataMessage);
                var groupDataMessage = message as GroupDataMessage;
                Assert.Equal("groupname", groupDataMessage.Group);
                Assert.Equal(738476327894u, groupDataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Text, groupDataMessage.DataType);
                Assert.Equal("user", groupDataMessage.FromUserId);
                Assert.Equal("xyz", groupDataMessage.Data.ToString());
            });
            yield return GetData(new { type = "message", from = "group", group = "groupname", dataType = "json", data = new JsonData { Value = "xyz" } }, message =>
            {
                Assert.True(message is GroupDataMessage);
                var groupDataMessage = message as GroupDataMessage;
                Assert.Equal("groupname", groupDataMessage.Group);
                Assert.Null(groupDataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Json, groupDataMessage.DataType);
                var obj = groupDataMessage.Data.ToObjectFromJson<JsonData>();
                Assert.Equal("xyz", obj.Value);
            });
            yield return GetData(new { type = "message", from = "group", group = "groupname", dataType = "binary", data = "eHl6" }, message =>
            {
                Assert.True(message is GroupDataMessage);
                var groupDataMessage = message as GroupDataMessage;
                Assert.Equal("groupname", groupDataMessage.Group);
                Assert.Null(groupDataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Binary, groupDataMessage.DataType);
                Assert.Equal("eHl6", Convert.ToBase64String(groupDataMessage.Data.ToArray()));
            });
            yield return GetData(new { sequenceId = 738476327894u, type = "message", from = "server", dataType = "text", data = "xyz" }, message =>
            {
                Assert.True(message is ServerDataMessage);
                var dataMessage = message as ServerDataMessage;
                Assert.Equal(738476327894u, dataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Text, dataMessage.DataType);
                Assert.Equal("xyz", dataMessage.Data.ToString());
            });
            yield return GetData(new { type = "message", from = "server", dataType = "json", data = new JsonData { Value = "xyz" } }, message =>
            {
                Assert.True(message is ServerDataMessage);
                var dataMessage = message as ServerDataMessage;;
                Assert.Null(dataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Json, dataMessage.DataType);
                var obj = dataMessage.Data.ToObjectFromJson<JsonData>();
                Assert.Equal("xyz", obj.Value);
            });
            yield return GetData(new { type = "message", from = "server", dataType = "binary", data = "eHl6" }, message =>
            {
                Assert.True(message is ServerDataMessage);
                var dataMessage = message as ServerDataMessage;
                Assert.Null(dataMessage.SequenceId);
                Assert.Equal(WebPubSubDataType.Binary, dataMessage.DataType);
                Assert.Equal("eHl6", Convert.ToBase64String(dataMessage.Data.ToArray()));
            });
            yield return GetData(new { type = "system", @event = "connected", userId = "user", connectionId = "connection" }, message =>
            {
                Assert.True(message is ConnectedMessage);
                var connectedMessage = message as ConnectedMessage;
                Assert.Equal("user", connectedMessage.UserId);
                Assert.Equal("connection", connectedMessage.ConnectionId);
                Assert.Null(connectedMessage.ReconnectionToken);
            });
            yield return GetData(new { type = "system", @event = "connected", userId = "user", connectionId = "connection", reconnectionToken = "rec" }, message =>
            {
                Assert.True(message is ConnectedMessage);
                var connectedMessage = message as ConnectedMessage;
                Assert.Equal("user", connectedMessage.UserId);
                Assert.Equal("connection", connectedMessage.ConnectionId);
                Assert.Equal("rec", connectedMessage.ReconnectionToken);
            });
            yield return GetData(new { type = "system", @event = "disconnected", message = "msg" }, message =>
            {
                Assert.True(message is DisconnectedMessage);
                var disconnectedMessage = message as DisconnectedMessage;
                Assert.Equal("msg", disconnectedMessage.Reason);
            });
        }

        public static IEnumerable<object[]> GetSerializingTestData()
        {
            static object[] GetData(WebPubSubMessage message, object json)
            {
                return new object[] { message, JsonSerializer.Serialize(json)};
            }

            yield return GetData(new JoinGroupMessage("group", null), new { type = "joinGroup", group = "group" });
            yield return GetData(new JoinGroupMessage("group", 738476327894u), new { type = "joinGroup", group = "group", ackId = 738476327894u });
            yield return GetData(new LeaveGroupMessage("group", null), new { type = "leaveGroup", group = "group" });
            yield return GetData(new LeaveGroupMessage("group", 738476327894u), new { type = "leaveGroup", group = "group", ackId = 738476327894u });
            yield return GetData(new SendToGroupMessage("group", BinaryData.FromString("xzy"), WebPubSubDataType.Text, null, false), new { type = "sendToGroup", group = "group", noEcho = false, dataType = "Text", data = "xzy" });
            yield return GetData(new SendToGroupMessage("group", BinaryData.FromObjectAsJson(new JsonData { Value = "xyz"}), WebPubSubDataType.Json, 738476327894u, true), new { type = "sendToGroup", group = "group", ackId = 738476327894u, noEcho = true, dataType = "Json", data = new { Value = "xyz" } });
            yield return GetData(new SendToGroupMessage("group", BinaryData.FromBytes(Convert.FromBase64String("eHl6")), WebPubSubDataType.Binary, 738476327894u, true), new { type = "sendToGroup", group = "group", ackId = 738476327894u, noEcho = true, dataType = "Binary", data = "eHl6" });
            yield return GetData(new SendToGroupMessage("group", BinaryData.FromBytes(Convert.FromBase64String("eHl6")), WebPubSubDataType.Protobuf, 738476327894u, true), new { type = "sendToGroup", group = "group", ackId = 738476327894u, noEcho = true, dataType = "Protobuf", data = "eHl6" });
            yield return GetData(new SendEventMessage("event", BinaryData.FromString("xzy"), WebPubSubDataType.Text, null), new { type = "event", @event = "event", dataType = "Text", data = "xzy" });
            yield return GetData(new SendEventMessage("event", BinaryData.FromObjectAsJson(new JsonData { Value = "xyz" }), WebPubSubDataType.Json, 738476327894u), new { type = "event", @event = "event", ackId = 738476327894u,dataType = "Json", data = new { Value = "xyz" } });
            yield return GetData(new SendEventMessage("event", BinaryData.FromBytes(Convert.FromBase64String("eHl6")), WebPubSubDataType.Binary, 738476327894u), new { type = "event", @event = "event", ackId = 738476327894u, dataType = "Binary", data = "eHl6" });
            yield return GetData(new SendEventMessage("event", BinaryData.FromBytes(Convert.FromBase64String("eHl6")), WebPubSubDataType.Protobuf, 738476327894u), new { type = "event", @event = "event", ackId = 738476327894u, dataType = "Protobuf", data = "eHl6" });
            yield return GetData(new SequenceAckMessage(123), new { type = "sequenceAck", sequenceId = 123 });
            yield return GetData(new SequenceAckMessage(738476327894u), new { type = "sequenceAck", sequenceId = 738476327894u });
        }

        [MemberData(nameof(GetParsingTestData))]
        [Theory]
        public void ParseMessageTest(byte[] payload, Action<WebPubSubMessage> messageAssert)
        {
            var protocol = new WebPubSubJsonProtocol();
            var resolvedMessage = protocol.ParseMessage(new ReadOnlySequence<byte>(payload));
            messageAssert(resolvedMessage);
        }

        [MemberData(nameof(GetSerializingTestData))]
        [Theory]
        public void SerializeMessageTest(WebPubSubMessage message, string serializedPayload)
        {
            var protocol = new WebPubSubJsonProtocol();
            Assert.Equal(serializedPayload, Encoding.UTF8.GetString(protocol.GetMessageBytes(message).ToArray()));
        }

        [Fact]
        public void ProtocolPropertyTest()
        {
            var jsonProtocol = new WebPubSubJsonProtocol();
            Assert.False(jsonProtocol.IsReliable);
            Assert.Equal("json.webpubsub.azure.v1", jsonProtocol.Name);

            var jsonReliableProtocol = new WebPubSubJsonReliableProtocol();
            Assert.True(jsonReliableProtocol.IsReliable);
            Assert.Equal("json.reliable.webpubsub.azure.v1", jsonReliableProtocol.Name);
        }
    }
}
#endif
