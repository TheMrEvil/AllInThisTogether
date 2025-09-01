using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x020002A6 RID: 678
	internal class TdsParserStateObjectManaged : TdsParserStateObject
	{
		// Token: 0x06001F17 RID: 7959 RVA: 0x00093009 File Offset: 0x00091209
		public TdsParserStateObjectManaged(TdsParser parser) : base(parser)
		{
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00093033 File Offset: 0x00091233
		internal TdsParserStateObjectManaged(TdsParser parser, TdsParserStateObject physicalConnection, bool async) : base(parser, physicalConnection, async)
		{
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0009305F File Offset: 0x0009125F
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00093067 File Offset: 0x00091267
		internal override uint Status
		{
			get
			{
				if (this._sessionHandle == null)
				{
					return uint.MaxValue;
				}
				return this._sessionHandle.Status;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0009305F File Offset: 0x0009125F
		internal override object SessionHandle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00003E32 File Offset: 0x00002032
		protected override object EmptyReadPacket
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00093080 File Offset: 0x00091280
		protected override bool CheckPacket(object packet, TaskCompletionSource<object> source)
		{
			SNIPacket snipacket = packet as SNIPacket;
			return snipacket.IsInvalid || (!snipacket.IsInvalid && source != null);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000930AC File Offset: 0x000912AC
		protected override void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async)
		{
			TdsParserStateObjectManaged tdsParserStateObjectManaged = physicalConnection as TdsParserStateObjectManaged;
			this._sessionHandle = tdsParserStateObjectManaged.CreateMarsSession(this, async);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000930CE File Offset: 0x000912CE
		internal SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			return this._marsConnection.CreateMarsSession(callbackObject, async);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000930DD File Offset: 0x000912DD
		protected override uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize)
		{
			return SNIProxy.Singleton.PacketGetData(packet as SNIPacket, _inBuff, ref dataSize);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000930F4 File Offset: 0x000912F4
		internal override void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			this._sessionHandle = SNIProxy.Singleton.CreateConnectionHandle(this, serverName, ignoreSniOpenTimeout, timerExpire, out instanceName, ref spnBuffer, flushCache, async, parallel, isIntegratedSecurity);
			if (this._sessionHandle == null)
			{
				this._parser.ProcessSNIError(this);
				return;
			}
			if (async)
			{
				SNIAsyncCallback receiveCallback = new SNIAsyncCallback(this.ReadAsyncCallback);
				SNIAsyncCallback sendCallback = new SNIAsyncCallback(this.WriteAsyncCallback);
				this._sessionHandle.SetAsyncCallbacks(receiveCallback, sendCallback);
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00093162 File Offset: 0x00091362
		internal void ReadAsyncCallback(SNIPacket packet, uint error)
		{
			base.ReadAsyncCallback<SNIPacket>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00093171 File Offset: 0x00091371
		internal void WriteAsyncCallback(SNIPacket packet, uint sniError)
		{
			base.WriteAsyncCallback<SNIPacket>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00007EED File Offset: 0x000060ED
		protected override void RemovePacketFromPendingList(object packet)
		{
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00093180 File Offset: 0x00091380
		internal override void Dispose()
		{
			SNIPacket sniPacket = this._sniPacket;
			SNIHandle sessionHandle = this._sessionHandle;
			SNIPacket sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			this._marsConnection = null;
			base.DisposeCounters();
			if (sessionHandle != null || sniPacket != null)
			{
				if (sniPacket != null)
				{
					sniPacket.Dispose();
				}
				if (sniAsyncAttnPacket != null)
				{
					sniAsyncAttnPacket.Dispose();
				}
				if (sessionHandle != null)
				{
					sessionHandle.Dispose();
					base.DecrementPendingCallbacks(true);
				}
			}
			this.DisposePacketCache();
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000931F4 File Offset: 0x000913F4
		internal override void DisposePacketCache()
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Dispose();
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00007EED File Offset: 0x000060ED
		protected override void FreeGcHandle(int remaining, bool release)
		{
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0009323C File Offset: 0x0009143C
		internal override bool IsFailedHandle()
		{
			return this._sessionHandle.Status > 0U;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0009324C File Offset: 0x0009144C
		internal override object ReadSyncOverAsync(int timeoutRemaining, out uint error)
		{
			SNIHandle handle = this.Handle;
			if (handle == null)
			{
				throw ADP.ClosedConnectionError();
			}
			SNIPacket result = null;
			error = SNIProxy.Singleton.ReadSyncOverAsync(handle, out result, timeoutRemaining);
			return result;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0009327C File Offset: 0x0009147C
		internal override bool IsPacketEmpty(object packet)
		{
			return packet == null;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00093282 File Offset: 0x00091482
		internal override void ReleasePacket(object syncReadPacket)
		{
			((SNIPacket)syncReadPacket).Dispose();
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00093290 File Offset: 0x00091490
		internal override uint CheckConnection()
		{
			SNIHandle handle = this.Handle;
			if (handle != null)
			{
				return SNIProxy.Singleton.CheckConnection(handle);
			}
			return 0U;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000932B4 File Offset: 0x000914B4
		internal override object ReadAsync(out uint error, ref object handle)
		{
			SNIPacket result;
			error = SNIProxy.Singleton.ReadAsync((SNIHandle)handle, out result);
			return result;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000932D8 File Offset: 0x000914D8
		internal override object CreateAndSetAttentionPacket()
		{
			if (this._sniAsyncAttnPacket == null)
			{
				SNIPacket snipacket = new SNIPacket();
				this.SetPacketData(snipacket, SQL.AttentionHeader, 8);
				this._sniAsyncAttnPacket = snipacket;
			}
			return this._sniAsyncAttnPacket;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0009330D File Offset: 0x0009150D
		internal override uint WritePacket(object packet, bool sync)
		{
			return SNIProxy.Singleton.WritePacket(this.Handle, (SNIPacket)packet, sync);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000056BA File Offset: 0x000038BA
		internal override object AddPacketToPendingList(object packet)
		{
			return packet;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00093326 File Offset: 0x00091526
		internal override bool IsValidPacket(object packetPointer)
		{
			return (SNIPacket)packetPointer != null && !((SNIPacket)packetPointer).IsInvalid;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00093340 File Offset: 0x00091540
		internal override object GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Reset();
			}
			else
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					this._sniPacket = this._writePacketCache.Take(this.Handle);
				}
			}
			return this._sniPacket;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000933AC File Offset: 0x000915AC
		internal override void ClearAllWritePackets()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Dispose();
				this._sniPacket = null;
			}
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Clear();
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0009340C File Offset: 0x0009160C
		internal override void SetPacketData(object packet, byte[] buffer, int bytesUsed)
		{
			SNIProxy.Singleton.PacketSetData((SNIPacket)packet, buffer, bytesUsed);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00093420 File Offset: 0x00091620
		internal override uint SniGetConnectionId(ref Guid clientConnectionId)
		{
			return SNIProxy.Singleton.GetConnectionId(this.Handle, ref clientConnectionId);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00093433 File Offset: 0x00091633
		internal override uint DisabeSsl()
		{
			return SNIProxy.Singleton.DisableSsl(this.Handle);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00093445 File Offset: 0x00091645
		internal override uint EnableMars(ref uint info)
		{
			this._marsConnection = new SNIMarsConnection(this.Handle);
			if (this._marsConnection.StartReceive() == 997U)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0009346D File Offset: 0x0009166D
		internal override uint EnableSsl(ref uint info)
		{
			return SNIProxy.Singleton.EnableSsl(this.Handle, info);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00093481 File Offset: 0x00091681
		internal override uint SetConnectionBufferSize(ref uint unsignedPacketSize)
		{
			return SNIProxy.Singleton.SetConnectionBufferSize(this.Handle, unsignedPacketSize);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00093495 File Offset: 0x00091695
		internal override uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer)
		{
			SNIProxy.Singleton.GenSspiClientContext(this.sspiClientContextStatus, receivedBuff, ref sendBuff, _sniSpnBuffer);
			sendLength = (uint)((sendBuff != null) ? sendBuff.Length : 0);
			return 0U;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override uint WaitForSSLHandShakeToComplete()
		{
			return 0U;
		}

		// Token: 0x0400159E RID: 5534
		private SNIMarsConnection _marsConnection;

		// Token: 0x0400159F RID: 5535
		private SNIHandle _sessionHandle;

		// Token: 0x040015A0 RID: 5536
		private SNIPacket _sniPacket;

		// Token: 0x040015A1 RID: 5537
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x040015A2 RID: 5538
		private readonly Dictionary<SNIPacket, SNIPacket> _pendingWritePackets = new Dictionary<SNIPacket, SNIPacket>();

		// Token: 0x040015A3 RID: 5539
		private readonly TdsParserStateObjectManaged.WritePacketCache _writePacketCache = new TdsParserStateObjectManaged.WritePacketCache();

		// Token: 0x040015A4 RID: 5540
		internal SspiClientContextStatus sspiClientContextStatus = new SspiClientContextStatus();

		// Token: 0x020002A7 RID: 679
		internal sealed class WritePacketCache : IDisposable
		{
			// Token: 0x06001F3C RID: 7996 RVA: 0x000934BA File Offset: 0x000916BA
			public WritePacketCache()
			{
				this._disposed = false;
				this._packets = new Stack<SNIPacket>();
			}

			// Token: 0x06001F3D RID: 7997 RVA: 0x000934D4 File Offset: 0x000916D4
			public SNIPacket Take(SNIHandle sniHandle)
			{
				SNIPacket snipacket;
				if (this._packets.Count > 0)
				{
					snipacket = this._packets.Pop();
					snipacket.Reset();
				}
				else
				{
					snipacket = new SNIPacket();
				}
				return snipacket;
			}

			// Token: 0x06001F3E RID: 7998 RVA: 0x0009350A File Offset: 0x0009170A
			public void Add(SNIPacket packet)
			{
				if (!this._disposed)
				{
					this._packets.Push(packet);
					return;
				}
				packet.Dispose();
			}

			// Token: 0x06001F3F RID: 7999 RVA: 0x00093527 File Offset: 0x00091727
			public void Clear()
			{
				while (this._packets.Count > 0)
				{
					this._packets.Pop().Dispose();
				}
			}

			// Token: 0x06001F40 RID: 8000 RVA: 0x00093549 File Offset: 0x00091749
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this.Clear();
				}
			}

			// Token: 0x040015A5 RID: 5541
			private bool _disposed;

			// Token: 0x040015A6 RID: 5542
			private Stack<SNIPacket> _packets;
		}
	}
}
