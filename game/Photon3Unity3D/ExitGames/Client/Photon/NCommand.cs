using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000014 RID: 20
	internal class NCommand : IComparable<NCommand>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000079E0 File Offset: 0x00005BE0
		protected internal int SizeOfPayload
		{
			get
			{
				return (this.Payload != null) ? this.Payload.Length : 0;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007A08 File Offset: 0x00005C08
		protected internal bool IsFlaggedUnsequenced
		{
			get
			{
				return (this.commandFlags & 2) > 0;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00007A28 File Offset: 0x00005C28
		protected internal bool IsFlaggedReliable
		{
			get
			{
				return (this.commandFlags & 1) > 0;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007A48 File Offset: 0x00005C48
		internal static void CreateAck(byte[] buffer, int offset, NCommand commandToAck, int sentTime)
		{
			buffer[offset++] = (commandToAck.IsFlaggedUnsequenced ? 16 : 1);
			buffer[offset++] = commandToAck.commandChannelID;
			buffer[offset++] = 0;
			buffer[offset++] = 4;
			Protocol.Serialize(20, buffer, ref offset);
			Protocol.Serialize(0, buffer, ref offset);
			Protocol.Serialize(commandToAck.reliableSequenceNumber, buffer, ref offset);
			Protocol.Serialize(sentTime, buffer, ref offset);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007AB9 File Offset: 0x00005CB9
		internal NCommand(EnetPeer peer, byte commandType, StreamBuffer payload, byte channel)
		{
			this.Initialize(peer, commandType, payload, channel);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007AD8 File Offset: 0x00005CD8
		internal void Initialize(EnetPeer peer, byte commandType, StreamBuffer payload, byte channel)
		{
			this.commandType = commandType;
			this.commandFlags = 1;
			this.commandChannelID = channel;
			this.Payload = payload;
			this.Size = 12;
			switch (this.commandType)
			{
			case 2:
			{
				this.Size = 44;
				byte[] array = new byte[32];
				array[0] = 0;
				array[1] = 0;
				int num = 2;
				Protocol.Serialize((short)peer.mtu, array, ref num);
				array[4] = 0;
				array[5] = 0;
				array[6] = 128;
				array[7] = 0;
				array[11] = peer.ChannelCount;
				array[15] = 0;
				array[19] = 0;
				array[22] = 19;
				array[23] = 136;
				array[27] = 2;
				array[31] = 2;
				this.Payload = new StreamBuffer(array);
				break;
			}
			case 4:
			{
				this.Size = 12;
				bool flag = peer.peerConnectionState != ConnectionStateValue.Connected;
				if (flag)
				{
					this.commandFlags = 2;
					this.reservedByte = ((peer.peerConnectionState == ConnectionStateValue.Zombie) ? 2 : 4);
				}
				break;
			}
			case 6:
				this.Size = 12 + payload.Length;
				break;
			case 7:
				this.Size = 16 + payload.Length;
				this.commandFlags = 0;
				break;
			case 8:
				this.Size = 32 + payload.Length;
				break;
			case 11:
				this.Size = 16 + payload.Length;
				this.commandFlags = 2;
				break;
			case 14:
				this.Size = 12 + payload.Length;
				this.commandFlags = 3;
				break;
			case 15:
				this.Size = 32 + payload.Length;
				this.commandFlags = 3;
				break;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007C92 File Offset: 0x00005E92
		internal NCommand(EnetPeer peer, byte[] inBuff, ref int readingOffset)
		{
			this.Initialize(peer, inBuff, ref readingOffset);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007CB0 File Offset: 0x00005EB0
		internal void Initialize(EnetPeer peer, byte[] inBuff, ref int readingOffset)
		{
			int num = readingOffset;
			readingOffset = num + 1;
			this.commandType = inBuff[num];
			num = readingOffset;
			readingOffset = num + 1;
			this.commandChannelID = inBuff[num];
			num = readingOffset;
			readingOffset = num + 1;
			this.commandFlags = inBuff[num];
			num = readingOffset;
			readingOffset = num + 1;
			this.reservedByte = inBuff[num];
			Protocol.Deserialize(out this.Size, inBuff, ref readingOffset);
			Protocol.Deserialize(out this.reliableSequenceNumber, inBuff, ref readingOffset);
			peer.bytesIn += (long)this.Size;
			int num2 = 0;
			switch (this.commandType)
			{
			case 1:
			case 16:
				Protocol.Deserialize(out this.ackReceivedReliableSequenceNumber, inBuff, ref readingOffset);
				Protocol.Deserialize(out this.ackReceivedSentTime, inBuff, ref readingOffset);
				goto IL_1DF;
			case 3:
			{
				short peerID;
				Protocol.Deserialize(out peerID, inBuff, ref readingOffset);
				readingOffset += 30;
				bool flag = peer.peerID == -1 || peer.peerID == -2;
				if (flag)
				{
					peer.peerID = peerID;
				}
				goto IL_1DF;
			}
			case 6:
			case 14:
				num2 = this.Size - 12;
				goto IL_1DF;
			case 7:
				Protocol.Deserialize(out this.unreliableSequenceNumber, inBuff, ref readingOffset);
				num2 = this.Size - 16;
				goto IL_1DF;
			case 8:
			case 15:
				Protocol.Deserialize(out this.startSequenceNumber, inBuff, ref readingOffset);
				Protocol.Deserialize(out this.fragmentCount, inBuff, ref readingOffset);
				Protocol.Deserialize(out this.fragmentNumber, inBuff, ref readingOffset);
				Protocol.Deserialize(out this.totalLength, inBuff, ref readingOffset);
				Protocol.Deserialize(out this.fragmentOffset, inBuff, ref readingOffset);
				num2 = this.Size - 32;
				this.fragmentsRemaining = this.fragmentCount;
				goto IL_1DF;
			case 11:
				Protocol.Deserialize(out this.unsequencedGroupNumber, inBuff, ref readingOffset);
				num2 = this.Size - 16;
				goto IL_1DF;
			}
			readingOffset += this.Size - 12;
			IL_1DF:
			bool flag2 = num2 != 0;
			if (flag2)
			{
				StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
				streamBuffer.Write(inBuff, readingOffset, num2);
				this.Payload = streamBuffer;
				this.Payload.Position = 0;
				readingOffset += num2;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007ED8 File Offset: 0x000060D8
		public void Reset()
		{
			this.commandFlags = 0;
			this.commandType = 0;
			this.commandChannelID = 0;
			this.reliableSequenceNumber = 0;
			this.unreliableSequenceNumber = 0;
			this.unsequencedGroupNumber = 0;
			this.reservedByte = 4;
			this.startSequenceNumber = 0;
			this.fragmentCount = 0;
			this.fragmentNumber = 0;
			this.totalLength = 0;
			this.fragmentOffset = 0;
			this.fragmentsRemaining = 0;
			this.commandSentTime = 0;
			this.commandSentCount = 0;
			this.roundTripTimeout = 0;
			this.timeoutTime = 0;
			this.ackReceivedReliableSequenceNumber = 0;
			this.ackReceivedSentTime = 0;
			this.Size = 0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007F74 File Offset: 0x00006174
		internal void SerializeHeader(byte[] buffer, ref int bufferIndex)
		{
			int num = bufferIndex;
			bufferIndex = num + 1;
			buffer[num] = this.commandType;
			num = bufferIndex;
			bufferIndex = num + 1;
			buffer[num] = this.commandChannelID;
			num = bufferIndex;
			bufferIndex = num + 1;
			buffer[num] = this.commandFlags;
			num = bufferIndex;
			bufferIndex = num + 1;
			buffer[num] = this.reservedByte;
			Protocol.Serialize(this.Size, buffer, ref bufferIndex);
			Protocol.Serialize(this.reliableSequenceNumber, buffer, ref bufferIndex);
			bool flag = this.commandType == 7;
			if (flag)
			{
				Protocol.Serialize(this.unreliableSequenceNumber, buffer, ref bufferIndex);
			}
			else
			{
				bool flag2 = this.commandType == 11;
				if (flag2)
				{
					Protocol.Serialize(this.unsequencedGroupNumber, buffer, ref bufferIndex);
				}
				else
				{
					bool flag3 = this.commandType == 8 || this.commandType == 15;
					if (flag3)
					{
						Protocol.Serialize(this.startSequenceNumber, buffer, ref bufferIndex);
						Protocol.Serialize(this.fragmentCount, buffer, ref bufferIndex);
						Protocol.Serialize(this.fragmentNumber, buffer, ref bufferIndex);
						Protocol.Serialize(this.totalLength, buffer, ref bufferIndex);
						Protocol.Serialize(this.fragmentOffset, buffer, ref bufferIndex);
					}
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008088 File Offset: 0x00006288
		internal byte[] Serialize()
		{
			return this.Payload.GetBuffer();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000080A8 File Offset: 0x000062A8
		public void FreePayload()
		{
			bool flag = this.Payload != null;
			if (flag)
			{
				PeerBase.MessageBufferPoolPut(this.Payload);
			}
			this.Payload = null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000080D8 File Offset: 0x000062D8
		public void Release()
		{
			this.returnPool.Release(this);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000080E8 File Offset: 0x000062E8
		public int CompareTo(NCommand other)
		{
			bool flag = other == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				int num = this.reliableSequenceNumber - other.reliableSequenceNumber;
				bool flag2 = this.IsFlaggedReliable || num != 0;
				if (flag2)
				{
					result = num;
				}
				else
				{
					result = this.unreliableSequenceNumber - other.unreliableSequenceNumber;
				}
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000813C File Offset: 0x0000633C
		public override string ToString()
		{
			bool flag = this.commandType == 1 || this.commandType == 16;
			string result;
			if (flag)
			{
				result = string.Format("CMD({1} ack for ch#/sq#/time: {0}/{2}/{3})", new object[]
				{
					this.commandChannelID,
					this.commandType,
					this.ackReceivedReliableSequenceNumber,
					this.ackReceivedSentTime
				});
			}
			else
			{
				result = string.Format("CMD({1} ch#/sq#/usq#: {0}/{2}/{3} r#/st/tt/rt:{5}/{4}/{6}/{7})", new object[]
				{
					this.commandChannelID,
					this.commandType,
					this.reliableSequenceNumber,
					this.unreliableSequenceNumber,
					this.commandSentTime,
					this.commandSentCount,
					this.timeoutTime,
					this.roundTripTimeout
				});
			}
			return result;
		}

		// Token: 0x0400007A RID: 122
		internal const byte FV_UNRELIABLE = 0;

		// Token: 0x0400007B RID: 123
		internal const byte FV_RELIABLE = 1;

		// Token: 0x0400007C RID: 124
		internal const byte FV_UNRELIABLE_UNSEQUENCED = 2;

		// Token: 0x0400007D RID: 125
		internal const byte FV_RELIBALE_UNSEQUENCED = 3;

		// Token: 0x0400007E RID: 126
		internal const byte CT_NONE = 0;

		// Token: 0x0400007F RID: 127
		internal const byte CT_ACK = 1;

		// Token: 0x04000080 RID: 128
		internal const byte CT_CONNECT = 2;

		// Token: 0x04000081 RID: 129
		internal const byte CT_VERIFYCONNECT = 3;

		// Token: 0x04000082 RID: 130
		internal const byte CT_DISCONNECT = 4;

		// Token: 0x04000083 RID: 131
		internal const byte CT_PING = 5;

		// Token: 0x04000084 RID: 132
		internal const byte CT_SENDRELIABLE = 6;

		// Token: 0x04000085 RID: 133
		internal const byte CT_SENDUNRELIABLE = 7;

		// Token: 0x04000086 RID: 134
		internal const byte CT_SENDFRAGMENT = 8;

		// Token: 0x04000087 RID: 135
		internal const byte CT_SENDUNSEQUENCED = 11;

		// Token: 0x04000088 RID: 136
		internal const byte CT_EG_SERVERTIME = 12;

		// Token: 0x04000089 RID: 137
		internal const byte CT_EG_SEND_UNRELIABLE_PROCESSED = 13;

		// Token: 0x0400008A RID: 138
		internal const byte CT_EG_SEND_RELIABLE_UNSEQUENCED = 14;

		// Token: 0x0400008B RID: 139
		internal const byte CT_EG_SEND_FRAGMENT_UNSEQUENCED = 15;

		// Token: 0x0400008C RID: 140
		internal const byte CT_EG_ACK_UNSEQUENCED = 16;

		// Token: 0x0400008D RID: 141
		internal const int HEADER_UDP_PACK_LENGTH = 12;

		// Token: 0x0400008E RID: 142
		internal const int CmdSizeMinimum = 12;

		// Token: 0x0400008F RID: 143
		internal const int CmdSizeAck = 20;

		// Token: 0x04000090 RID: 144
		internal const int CmdSizeConnect = 44;

		// Token: 0x04000091 RID: 145
		internal const int CmdSizeVerifyConnect = 44;

		// Token: 0x04000092 RID: 146
		internal const int CmdSizeDisconnect = 12;

		// Token: 0x04000093 RID: 147
		internal const int CmdSizePing = 12;

		// Token: 0x04000094 RID: 148
		internal const int CmdSizeReliableHeader = 12;

		// Token: 0x04000095 RID: 149
		internal const int CmdSizeUnreliableHeader = 16;

		// Token: 0x04000096 RID: 150
		internal const int CmdSizeUnsequensedHeader = 16;

		// Token: 0x04000097 RID: 151
		internal const int CmdSizeFragmentHeader = 32;

		// Token: 0x04000098 RID: 152
		internal const int CmdSizeMaxHeader = 36;

		// Token: 0x04000099 RID: 153
		internal byte commandFlags;

		// Token: 0x0400009A RID: 154
		internal byte commandType;

		// Token: 0x0400009B RID: 155
		internal byte commandChannelID;

		// Token: 0x0400009C RID: 156
		internal int reliableSequenceNumber;

		// Token: 0x0400009D RID: 157
		internal int unreliableSequenceNumber;

		// Token: 0x0400009E RID: 158
		internal int unsequencedGroupNumber;

		// Token: 0x0400009F RID: 159
		internal byte reservedByte = 4;

		// Token: 0x040000A0 RID: 160
		internal int startSequenceNumber;

		// Token: 0x040000A1 RID: 161
		internal int fragmentCount;

		// Token: 0x040000A2 RID: 162
		internal int fragmentNumber;

		// Token: 0x040000A3 RID: 163
		internal int totalLength;

		// Token: 0x040000A4 RID: 164
		internal int fragmentOffset;

		// Token: 0x040000A5 RID: 165
		internal int fragmentsRemaining;

		// Token: 0x040000A6 RID: 166
		internal int commandSentTime;

		// Token: 0x040000A7 RID: 167
		internal byte commandSentCount;

		// Token: 0x040000A8 RID: 168
		internal int roundTripTimeout;

		// Token: 0x040000A9 RID: 169
		internal int timeoutTime;

		// Token: 0x040000AA RID: 170
		internal int ackReceivedReliableSequenceNumber;

		// Token: 0x040000AB RID: 171
		internal int ackReceivedSentTime;

		// Token: 0x040000AC RID: 172
		internal int Size;

		// Token: 0x040000AD RID: 173
		internal StreamBuffer Payload;

		// Token: 0x040000AE RID: 174
		internal NCommandPool returnPool;
	}
}
