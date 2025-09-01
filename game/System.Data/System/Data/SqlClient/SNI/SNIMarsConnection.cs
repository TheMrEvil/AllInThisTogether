using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000293 RID: 659
	internal class SNIMarsConnection
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0008F4A5 File Offset: 0x0008D6A5
		public Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0008F4B0 File Offset: 0x0008D6B0
		public SNIMarsConnection(SNIHandle lowerHandle)
		{
			this._lowerHandle = lowerHandle;
			this._lowerHandle.SetAsyncCallbacks(new SNIAsyncCallback(this.HandleReceiveComplete), new SNIAsyncCallback(this.HandleSendComplete));
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0008F510 File Offset: 0x0008D710
		public SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			SNIMarsHandle result;
			lock (this)
			{
				ushort nextSessionId = this._nextSessionId;
				this._nextSessionId = nextSessionId + 1;
				ushort num = nextSessionId;
				SNIMarsHandle snimarsHandle = new SNIMarsHandle(this, num, callbackObject, async);
				this._sessions.Add((int)num, snimarsHandle);
				result = snimarsHandle;
			}
			return result;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0008F578 File Offset: 0x0008D778
		public uint StartReceive()
		{
			SNIPacket snipacket = null;
			if (this.ReceiveAsync(ref snipacket) == 997U)
			{
				return 997U;
			}
			return SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 0U, 19U, string.Empty);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0008F5AC File Offset: 0x0008D7AC
		public uint Send(SNIPacket packet)
		{
			uint result;
			lock (this)
			{
				result = this._lowerHandle.Send(packet);
			}
			return result;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0008F5F0 File Offset: 0x0008D7F0
		public uint SendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint result;
			lock (this)
			{
				result = this._lowerHandle.SendAsync(packet, false, callback);
			}
			return result;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0008F638 File Offset: 0x0008D838
		public uint ReceiveAsync(ref SNIPacket packet)
		{
			uint result;
			lock (this)
			{
				result = this._lowerHandle.ReceiveAsync(ref packet);
			}
			return result;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0008F67C File Offset: 0x0008D87C
		public uint CheckConnection()
		{
			uint result;
			lock (this)
			{
				result = this._lowerHandle.CheckConnection();
			}
			return result;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
		public void HandleReceiveError(SNIPacket packet)
		{
			foreach (SNIMarsHandle snimarsHandle in this._sessions.Values)
			{
				snimarsHandle.HandleReceiveError(packet);
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0008F718 File Offset: 0x0008D918
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			packet.InvokeCompletionCallback(sniErrorCode);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0008F724 File Offset: 0x0008D924
		public void HandleReceiveComplete(SNIPacket packet, uint sniErrorCode)
		{
			SNISMUXHeader snismuxheader = null;
			SNIPacket packet2 = null;
			SNIMarsHandle snimarsHandle = null;
			if (sniErrorCode != 0U)
			{
				SNIMarsConnection obj = this;
				lock (obj)
				{
					this.HandleReceiveError(packet);
					return;
				}
			}
			for (;;)
			{
				SNIMarsConnection obj = this;
				lock (obj)
				{
					if (this._currentHeaderByteCount != 16)
					{
						snismuxheader = null;
						packet2 = null;
						snimarsHandle = null;
						while (this._currentHeaderByteCount != 16)
						{
							int num = packet.TakeData(this._headerBytes, this._currentHeaderByteCount, 16 - this._currentHeaderByteCount);
							this._currentHeaderByteCount += num;
							if (num == 0)
							{
								sniErrorCode = this.ReceiveAsync(ref packet);
								if (sniErrorCode == 997U)
								{
									return;
								}
								this.HandleReceiveError(packet);
								return;
							}
						}
						this._currentHeader = new SNISMUXHeader
						{
							SMID = this._headerBytes[0],
							flags = this._headerBytes[1],
							sessionId = BitConverter.ToUInt16(this._headerBytes, 2),
							length = BitConverter.ToUInt32(this._headerBytes, 4) - 16U,
							sequenceNumber = BitConverter.ToUInt32(this._headerBytes, 8),
							highwater = BitConverter.ToUInt32(this._headerBytes, 12)
						};
						this._dataBytesLeft = (int)this._currentHeader.length;
						this._currentPacket = new SNIPacket((int)this._currentHeader.length);
					}
					snismuxheader = this._currentHeader;
					packet2 = this._currentPacket;
					if (this._currentHeader.flags == 8 && this._dataBytesLeft > 0)
					{
						int num2 = packet.TakeData(this._currentPacket, this._dataBytesLeft);
						this._dataBytesLeft -= num2;
						if (this._dataBytesLeft > 0)
						{
							sniErrorCode = this.ReceiveAsync(ref packet);
							if (sniErrorCode == 997U)
							{
								break;
							}
							this.HandleReceiveError(packet);
							break;
						}
					}
					this._currentHeaderByteCount = 0;
					if (!this._sessions.ContainsKey((int)this._currentHeader.sessionId))
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 5U, string.Empty);
						this.HandleReceiveError(packet);
						this._lowerHandle.Dispose();
						this._lowerHandle = null;
						break;
					}
					if (this._currentHeader.flags == 4)
					{
						this._sessions.Remove((int)this._currentHeader.sessionId);
					}
					else
					{
						snimarsHandle = this._sessions[(int)this._currentHeader.sessionId];
					}
				}
				if (snismuxheader.flags == 8)
				{
					snimarsHandle.HandleReceiveComplete(packet2, snismuxheader);
				}
				if (this._currentHeader.flags == 2)
				{
					try
					{
						snimarsHandle.HandleAck(snismuxheader.highwater);
					}
					catch (Exception sniException)
					{
						SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, sniException);
					}
				}
				obj = this;
				lock (obj)
				{
					if (packet.DataLeft != 0)
					{
						continue;
					}
					sniErrorCode = this.ReceiveAsync(ref packet);
					if (sniErrorCode != 997U)
					{
						this.HandleReceiveError(packet);
					}
				}
				break;
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0008FA6C File Offset: 0x0008DC6C
		public uint EnableSsl(uint options)
		{
			return this._lowerHandle.EnableSsl(options);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0008FA7A File Offset: 0x0008DC7A
		public void DisableSsl()
		{
			this._lowerHandle.DisableSsl();
		}

		// Token: 0x04001508 RID: 5384
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x04001509 RID: 5385
		private readonly Dictionary<int, SNIMarsHandle> _sessions = new Dictionary<int, SNIMarsHandle>();

		// Token: 0x0400150A RID: 5386
		private readonly byte[] _headerBytes = new byte[16];

		// Token: 0x0400150B RID: 5387
		private SNIHandle _lowerHandle;

		// Token: 0x0400150C RID: 5388
		private ushort _nextSessionId;

		// Token: 0x0400150D RID: 5389
		private int _currentHeaderByteCount;

		// Token: 0x0400150E RID: 5390
		private int _dataBytesLeft;

		// Token: 0x0400150F RID: 5391
		private SNISMUXHeader _currentHeader;

		// Token: 0x04001510 RID: 5392
		private SNIPacket _currentPacket;
	}
}
