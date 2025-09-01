using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000294 RID: 660
	internal class SNIMarsHandle : SNIHandle
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0008FA87 File Offset: 0x0008DC87
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0008FA8F File Offset: 0x0008DC8F
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0008FA98 File Offset: 0x0008DC98
		public override void Dispose()
		{
			try
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_FIN);
			}
			catch (Exception sniException)
			{
				SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, sniException);
				throw;
			}
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0008FACC File Offset: 0x0008DCCC
		public SNIMarsHandle(SNIMarsConnection connection, ushort sessionId, object callbackObject, bool async)
		{
			this._sessionId = sessionId;
			this._connection = connection;
			this._callbackObject = callbackObject;
			this.SendControlPacket(SNISMUXFlags.SMUX_SYN);
			this._status = 0U;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0008FB64 File Offset: 0x0008DD64
		private void SendControlPacket(SNISMUXFlags flags)
		{
			byte[] data = null;
			lock (this)
			{
				this.GetSMUXHeaderBytes(0, (byte)flags, ref data);
			}
			SNIPacket snipacket = new SNIPacket();
			snipacket.SetData(data, 16);
			this._connection.Send(snipacket);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0008FBC4 File Offset: 0x0008DDC4
		private void GetSMUXHeaderBytes(int length, byte flags, ref byte[] headerBytes)
		{
			headerBytes = new byte[16];
			this._currentHeader.SMID = 83;
			this._currentHeader.flags = flags;
			this._currentHeader.sessionId = this._sessionId;
			this._currentHeader.length = (uint)(16 + length);
			SNISMUXHeader currentHeader = this._currentHeader;
			uint sequenceNumber2;
			if (flags != 4 && flags != 2)
			{
				uint sequenceNumber = this._sequenceNumber;
				this._sequenceNumber = sequenceNumber + 1U;
				sequenceNumber2 = sequenceNumber;
			}
			else
			{
				sequenceNumber2 = this._sequenceNumber - 1U;
			}
			currentHeader.sequenceNumber = sequenceNumber2;
			this._currentHeader.highwater = this._receiveHighwater;
			this._receiveHighwaterLastAck = this._currentHeader.highwater;
			BitConverter.GetBytes((short)this._currentHeader.SMID).CopyTo(headerBytes, 0);
			BitConverter.GetBytes((short)this._currentHeader.flags).CopyTo(headerBytes, 1);
			BitConverter.GetBytes(this._currentHeader.sessionId).CopyTo(headerBytes, 2);
			BitConverter.GetBytes(this._currentHeader.length).CopyTo(headerBytes, 4);
			BitConverter.GetBytes(this._currentHeader.sequenceNumber).CopyTo(headerBytes, 8);
			BitConverter.GetBytes(this._currentHeader.highwater).CopyTo(headerBytes, 12);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0008FCF4 File Offset: 0x0008DEF4
		private SNIPacket GetSMUXEncapsulatedPacket(SNIPacket packet)
		{
			uint sequenceNumber = this._sequenceNumber;
			byte[] data = null;
			this.GetSMUXHeaderBytes(packet.Length, 8, ref data);
			SNIPacket snipacket = new SNIPacket(16 + packet.Length);
			snipacket.Description = string.Format("({0}) SMUX packet {1}", (packet.Description == null) ? "" : packet.Description, sequenceNumber);
			snipacket.AppendData(data, 16);
			snipacket.AppendPacket(packet);
			return snipacket;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0008FD64 File Offset: 0x0008DF64
		public override uint Send(SNIPacket packet)
		{
			for (;;)
			{
				SNIMarsHandle obj = this;
				lock (obj)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						break;
					}
				}
				this._ackEvent.Wait();
				obj = this;
				lock (obj)
				{
					this._ackEvent.Reset();
					continue;
				}
				break;
			}
			return this._connection.Send(this.GetSMUXEncapsulatedPacket(packet));
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0008FDF8 File Offset: 0x0008DFF8
		private uint InternalSendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint result;
			lock (this)
			{
				if (this._sequenceNumber >= this._sendHighwater)
				{
					result = 1048576U;
				}
				else
				{
					SNIPacket smuxencapsulatedPacket = this.GetSMUXEncapsulatedPacket(packet);
					if (callback != null)
					{
						smuxencapsulatedPacket.SetCompletionCallback(callback);
					}
					else
					{
						smuxencapsulatedPacket.SetCompletionCallback(new SNIAsyncCallback(this.HandleSendComplete));
					}
					result = this._connection.SendAsync(smuxencapsulatedPacket, callback);
				}
			}
			return result;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0008FE7C File Offset: 0x0008E07C
		private uint SendPendingPackets()
		{
			for (;;)
			{
				lock (this)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						if (this._sendPacketQueue.Count != 0)
						{
							SNIMarsQueuedPacket snimarsQueuedPacket = this._sendPacketQueue.Peek();
							uint num = this.InternalSendAsync(snimarsQueuedPacket.Packet, snimarsQueuedPacket.Callback);
							if (num != 0U && num != 997U)
							{
								return num;
							}
							this._sendPacketQueue.Dequeue();
							continue;
						}
						else
						{
							this._ackEvent.Set();
						}
					}
				}
				break;
			}
			return 0U;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0008FF1C File Offset: 0x0008E11C
		public override uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null)
		{
			lock (this)
			{
				this._sendPacketQueue.Enqueue(new SNIMarsQueuedPacket(packet, (callback != null) ? callback : new SNIAsyncCallback(this.HandleSendComplete)));
			}
			this.SendPendingPackets();
			return 997U;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0008FF80 File Offset: 0x0008E180
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				int count = this._receivedPacketQueue.Count;
				if (this._connectionError != null)
				{
					return SNICommon.ReportSNIError(this._connectionError);
				}
				if (count == 0)
				{
					this._asyncReceives++;
					return 997U;
				}
				packet = this._receivedPacketQueue.Dequeue();
				if (count == 1)
				{
					this._packetEvent.Reset();
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return 0U;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00090050 File Offset: 0x0008E250
		public void HandleReceiveError(SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				this._connectionError = SNILoadHandle.SingletonInstance.LastError;
				this._packetEvent.Set();
			}
			((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 1U);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000900B8 File Offset: 0x0008E2B8
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			lock (this)
			{
				((TdsParserStateObject)this._callbackObject).WriteAsyncCallback<SNIPacket>(packet, sniErrorCode);
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00090100 File Offset: 0x0008E300
		public void HandleAck(uint highwater)
		{
			lock (this)
			{
				if (this._sendHighwater != highwater)
				{
					this._sendHighwater = highwater;
					this.SendPendingPackets();
				}
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0009014C File Offset: 0x0008E34C
		public void HandleReceiveComplete(SNIPacket packet, SNISMUXHeader header)
		{
			SNIMarsHandle obj = this;
			lock (obj)
			{
				if (this._sendHighwater != header.highwater)
				{
					this.HandleAck(header.highwater);
				}
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._asyncReceives == 0)
					{
						this._receivedPacketQueue.Enqueue(packet);
						this._packetEvent.Set();
						return;
					}
					this._asyncReceives--;
					((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 0U);
				}
			}
			obj = this;
			lock (obj)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0009023C File Offset: 0x0008E43C
		private void SendAckIfNecessary()
		{
			uint receiveHighwater;
			uint receiveHighwaterLastAck;
			lock (this)
			{
				receiveHighwater = this._receiveHighwater;
				receiveHighwaterLastAck = this._receiveHighwaterLastAck;
			}
			if (receiveHighwater - receiveHighwaterLastAck > 2U)
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_ACK);
			}
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0009028C File Offset: 0x0008E48C
		public override uint Receive(out SNIPacket packet, int timeoutInMilliseconds)
		{
			packet = null;
			uint num = 997U;
			for (;;)
			{
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._connectionError != null)
					{
						return SNICommon.ReportSNIError(this._connectionError);
					}
					int count = this._receivedPacketQueue.Count;
					if (count > 0)
					{
						packet = this._receivedPacketQueue.Dequeue();
						if (count == 1)
						{
							this._packetEvent.Reset();
						}
						num = 0U;
					}
				}
				if (num == 0U)
				{
					break;
				}
				if (!this._packetEvent.Wait(timeoutInMilliseconds))
				{
					goto Block_4;
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return num;
			Block_4:
			SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 11U, string.Empty);
			return 258U;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00090388 File Offset: 0x0008E588
		public override uint CheckConnection()
		{
			return this._connection.CheckConnection();
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00007EED File Offset: 0x000060ED
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00007EED File Offset: 0x000060ED
		public override void SetBufferSize(int bufferSize)
		{
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00090395 File Offset: 0x0008E595
		public override uint EnableSsl(uint options)
		{
			return this._connection.EnableSsl(options);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000903A3 File Offset: 0x0008E5A3
		public override void DisableSsl()
		{
			this._connection.DisableSsl();
		}

		// Token: 0x04001511 RID: 5393
		private const uint ACK_THRESHOLD = 2U;

		// Token: 0x04001512 RID: 5394
		private readonly SNIMarsConnection _connection;

		// Token: 0x04001513 RID: 5395
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001514 RID: 5396
		private readonly Queue<SNIPacket> _receivedPacketQueue = new Queue<SNIPacket>();

		// Token: 0x04001515 RID: 5397
		private readonly Queue<SNIMarsQueuedPacket> _sendPacketQueue = new Queue<SNIMarsQueuedPacket>();

		// Token: 0x04001516 RID: 5398
		private readonly object _callbackObject;

		// Token: 0x04001517 RID: 5399
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x04001518 RID: 5400
		private readonly ushort _sessionId;

		// Token: 0x04001519 RID: 5401
		private readonly ManualResetEventSlim _packetEvent = new ManualResetEventSlim(false);

		// Token: 0x0400151A RID: 5402
		private readonly ManualResetEventSlim _ackEvent = new ManualResetEventSlim(false);

		// Token: 0x0400151B RID: 5403
		private readonly SNISMUXHeader _currentHeader = new SNISMUXHeader();

		// Token: 0x0400151C RID: 5404
		private uint _sendHighwater = 4U;

		// Token: 0x0400151D RID: 5405
		private int _asyncReceives;

		// Token: 0x0400151E RID: 5406
		private uint _receiveHighwater = 4U;

		// Token: 0x0400151F RID: 5407
		private uint _receiveHighwaterLastAck = 4U;

		// Token: 0x04001520 RID: 5408
		private uint _sequenceNumber;

		// Token: 0x04001521 RID: 5409
		private SNIError _connectionError;
	}
}
