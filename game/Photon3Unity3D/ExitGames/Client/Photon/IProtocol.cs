using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000011 RID: 17
	public abstract class IProtocol
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A1 RID: 161
		public abstract string ProtocolType { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A2 RID: 162
		public abstract byte[] VersionBytes { get; }

		// Token: 0x060000A3 RID: 163
		public abstract void Serialize(StreamBuffer dout, object serObject, bool setType);

		// Token: 0x060000A4 RID: 164
		public abstract void SerializeShort(StreamBuffer dout, short serObject, bool setType);

		// Token: 0x060000A5 RID: 165
		public abstract void SerializeString(StreamBuffer dout, string serObject, bool setType);

		// Token: 0x060000A6 RID: 166
		public abstract void SerializeEventData(StreamBuffer stream, EventData serObject, bool setType);

		// Token: 0x060000A7 RID: 167
		[Obsolete("Use ParameterDictionary instead.")]
		public abstract void SerializeOperationRequest(StreamBuffer stream, byte operationCode, Dictionary<byte, object> parameters, bool setType);

		// Token: 0x060000A8 RID: 168
		public abstract void SerializeOperationRequest(StreamBuffer stream, byte operationCode, ParameterDictionary parameters, bool setType);

		// Token: 0x060000A9 RID: 169
		public abstract void SerializeOperationResponse(StreamBuffer stream, OperationResponse serObject, bool setType);

		// Token: 0x060000AA RID: 170
		public abstract object Deserialize(StreamBuffer din, byte type, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None);

		// Token: 0x060000AB RID: 171
		public abstract short DeserializeShort(StreamBuffer din);

		// Token: 0x060000AC RID: 172
		public abstract byte DeserializeByte(StreamBuffer din);

		// Token: 0x060000AD RID: 173
		public abstract EventData DeserializeEventData(StreamBuffer din, EventData target = null, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None);

		// Token: 0x060000AE RID: 174
		public abstract OperationRequest DeserializeOperationRequest(StreamBuffer din, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None);

		// Token: 0x060000AF RID: 175
		public abstract OperationResponse DeserializeOperationResponse(StreamBuffer stream, IProtocol.DeserializationFlags flags = IProtocol.DeserializationFlags.None);

		// Token: 0x060000B0 RID: 176
		public abstract DisconnectMessage DeserializeDisconnectMessage(StreamBuffer stream);

		// Token: 0x060000B1 RID: 177 RVA: 0x000077B4 File Offset: 0x000059B4
		public byte[] Serialize(object obj)
		{
			StreamBuffer streamBuffer = new StreamBuffer(64);
			this.Serialize(streamBuffer, obj, true);
			return streamBuffer.ToArray();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000077E0 File Offset: 0x000059E0
		public object Deserialize(StreamBuffer stream)
		{
			return this.Deserialize(stream, stream.ReadByte(), IProtocol.DeserializationFlags.None);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007800 File Offset: 0x00005A00
		public object Deserialize(byte[] serializedData)
		{
			StreamBuffer streamBuffer = new StreamBuffer(serializedData);
			return this.Deserialize(streamBuffer, streamBuffer.ReadByte(), IProtocol.DeserializationFlags.None);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007828 File Offset: 0x00005A28
		public object DeserializeMessage(StreamBuffer stream)
		{
			return this.Deserialize(stream, stream.ReadByte(), IProtocol.DeserializationFlags.None);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007848 File Offset: 0x00005A48
		internal void SerializeMessage(StreamBuffer ms, object msg)
		{
			this.Serialize(ms, msg, true);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007855 File Offset: 0x00005A55
		protected IProtocol()
		{
		}

		// Token: 0x04000078 RID: 120
		public readonly ByteArraySlicePool ByteArraySlicePool = new ByteArraySlicePool();

		// Token: 0x02000053 RID: 83
		public enum DeserializationFlags
		{
			// Token: 0x04000226 RID: 550
			None,
			// Token: 0x04000227 RID: 551
			AllowPooledByteArray,
			// Token: 0x04000228 RID: 552
			WrapIncomingStructs
		}
	}
}
