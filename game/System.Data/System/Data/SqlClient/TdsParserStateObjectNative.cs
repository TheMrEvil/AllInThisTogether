using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x0200027A RID: 634
	internal class TdsParserStateObjectNative : TdsParserStateObject
	{
		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008D41B File Offset: 0x0008B61B
		public TdsParserStateObjectNative(TdsParser parser) : base(parser)
		{
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0008D43A File Offset: 0x0008B63A
		internal TdsParserStateObjectNative(TdsParser parser, TdsParserStateObject physicalConnection, bool async) : base(parser, physicalConnection, async)
		{
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0008D45B File Offset: 0x0008B65B
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0008D463 File Offset: 0x0008B663
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

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0008D45B File Offset: 0x0008B65B
		internal override object SessionHandle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0008D47A File Offset: 0x0008B67A
		protected override object EmptyReadPacket
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0008D488 File Offset: 0x0008B688
		protected override void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async)
		{
			TdsParserStateObjectNative tdsParserStateObjectNative = physicalConnection as TdsParserStateObjectNative;
			SNINativeMethodWrapper.ConsumerInfo myInfo = this.CreateConsumerInfo(async);
			this._sessionHandle = new SNIHandle(myInfo, tdsParserStateObjectNative.Handle);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0008D4B8 File Offset: 0x0008B6B8
		private SNINativeMethodWrapper.ConsumerInfo CreateConsumerInfo(bool async)
		{
			SNINativeMethodWrapper.ConsumerInfo result = default(SNINativeMethodWrapper.ConsumerInfo);
			result.defaultBufferSize = this._outBuff.Length;
			if (async)
			{
				result.readDelegate = SNILoadHandle.SingletonInstance.ReadAsyncCallbackDispatcher;
				result.writeDelegate = SNILoadHandle.SingletonInstance.WriteAsyncCallbackDispatcher;
				this._gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
				result.key = (IntPtr)this._gcHandle;
			}
			return result;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0008D524 File Offset: 0x0008B724
		internal override void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool fParallel, bool isIntegratedSecurity)
		{
			spnBuffer = null;
			if (isIntegratedSecurity)
			{
				spnBuffer = new byte[SNINativeMethodWrapper.SniMaxComposedSpnLength];
			}
			SNINativeMethodWrapper.ConsumerInfo myInfo = this.CreateConsumerInfo(async);
			long num;
			if (9223372036854775807L == timerExpire)
			{
				num = 2147483647L;
			}
			else
			{
				num = ADP.TimerRemainingMilliseconds(timerExpire);
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				else if (0L > num)
				{
					num = 0L;
				}
			}
			this._sessionHandle = new SNIHandle(myInfo, serverName, spnBuffer, ignoreSniOpenTimeout, checked((int)num), ref instanceName, flushCache, !async, fParallel);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0008D5A3 File Offset: 0x0008B7A3
		protected override uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize)
		{
			return SNINativeMethodWrapper.SNIPacketGetData((IntPtr)packet, _inBuff, ref dataSize);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0008D5B4 File Offset: 0x0008B7B4
		protected override bool CheckPacket(object packet, TaskCompletionSource<object> source)
		{
			IntPtr value = (IntPtr)packet;
			return IntPtr.Zero == value || (IntPtr.Zero != value && source != null);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0008D5EA File Offset: 0x0008B7EA
		public void ReadAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			this.ReadAsyncCallback(key, packet, error);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0008D5F5 File Offset: 0x0008B7F5
		public void WriteAsyncCallback(IntPtr key, IntPtr packet, uint sniError)
		{
			this.WriteAsyncCallback(key, packet, sniError);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0008D600 File Offset: 0x0008B800
		protected override void RemovePacketFromPendingList(object ptr)
		{
			IntPtr key = (IntPtr)ptr;
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				SNIPacket packet;
				if (this._pendingWritePackets.TryGetValue(key, out packet))
				{
					this._pendingWritePackets.Remove(key);
					this._writePacketCache.Add(packet);
				}
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0008D66C File Offset: 0x0008B86C
		internal override void Dispose()
		{
			SafeHandle sniPacket = this._sniPacket;
			SafeHandle sessionHandle = this._sessionHandle;
			SafeHandle sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
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

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0008D6D8 File Offset: 0x0008B8D8
		protected override void FreeGcHandle(int remaining, bool release)
		{
			if ((remaining == 0 || release) && this._gcHandle.IsAllocated)
			{
				this._gcHandle.Free();
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0008D6FA File Offset: 0x0008B8FA
		internal override bool IsFailedHandle()
		{
			return this._sessionHandle.Status > 0U;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0008D70C File Offset: 0x0008B90C
		internal override object ReadSyncOverAsync(int timeoutRemaining, out uint error)
		{
			SNIHandle handle = this.Handle;
			if (handle == null)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			error = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, base.GetTimeoutRemaining());
			return zero;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0008D745 File Offset: 0x0008B945
		internal override bool IsPacketEmpty(object readPacket)
		{
			return IntPtr.Zero == (IntPtr)readPacket;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0008D757 File Offset: 0x0008B957
		internal override void ReleasePacket(object syncReadPacket)
		{
			SNINativeMethodWrapper.SNIPacketRelease((IntPtr)syncReadPacket);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0008D764 File Offset: 0x0008B964
		internal override uint CheckConnection()
		{
			SNIHandle handle = this.Handle;
			if (handle != null)
			{
				return SNINativeMethodWrapper.SNICheckConnection(handle);
			}
			return 0U;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0008D784 File Offset: 0x0008B984
		internal override object ReadAsync(out uint error, ref object handle)
		{
			IntPtr zero = IntPtr.Zero;
			error = SNINativeMethodWrapper.SNIReadAsync((SNIHandle)handle, ref zero);
			return zero;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0008D7B0 File Offset: 0x0008B9B0
		internal override object CreateAndSetAttentionPacket()
		{
			SNIPacket snipacket = new SNIPacket(this.Handle);
			this._sniAsyncAttnPacket = snipacket;
			this.SetPacketData(snipacket, SQL.AttentionHeader, 8);
			return snipacket;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0008D7DE File Offset: 0x0008B9DE
		internal override uint WritePacket(object packet, bool sync)
		{
			return SNINativeMethodWrapper.SNIWritePacket(this.Handle, (SNIPacket)packet, sync);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0008D7F4 File Offset: 0x0008B9F4
		internal override object AddPacketToPendingList(object packetToAdd)
		{
			SNIPacket snipacket = (SNIPacket)packetToAdd;
			this._sniPacket = null;
			IntPtr intPtr = snipacket.DangerousGetHandle();
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._pendingWritePackets.Add(intPtr, snipacket);
			}
			return intPtr;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0008D858 File Offset: 0x0008BA58
		internal override bool IsValidPacket(object packetPointer)
		{
			return (IntPtr)packetPointer != IntPtr.Zero;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0008D86C File Offset: 0x0008BA6C
		internal override object GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				SNINativeMethodWrapper.SNIPacketReset(this.Handle, SNINativeMethodWrapper.IOType.WRITE, this._sniPacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
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

		// Token: 0x06001DCC RID: 7628 RVA: 0x0008D8E0 File Offset: 0x0008BAE0
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

		// Token: 0x06001DCD RID: 7629 RVA: 0x0008D940 File Offset: 0x0008BB40
		internal override void SetPacketData(object packet, byte[] buffer, int bytesUsed)
		{
			SNINativeMethodWrapper.SNIPacketSetData((SNIPacket)packet, buffer, bytesUsed);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0008D94F File Offset: 0x0008BB4F
		internal override uint SniGetConnectionId(ref Guid clientConnectionId)
		{
			return SNINativeMethodWrapper.SniGetConnectionId(this.Handle, ref clientConnectionId);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0008D95D File Offset: 0x0008BB5D
		internal override uint DisabeSsl()
		{
			return SNINativeMethodWrapper.SNIRemoveProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0008D96B File Offset: 0x0008BB6B
		internal override uint EnableMars(ref uint info)
		{
			return SNINativeMethodWrapper.SNIAddProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SMUX_PROV, ref info);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0008D97A File Offset: 0x0008BB7A
		internal override uint EnableSsl(ref uint info)
		{
			return SNINativeMethodWrapper.SNIAddProvider(this.Handle, SNINativeMethodWrapper.ProviderEnum.SSL_PROV, ref info);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0008D989 File Offset: 0x0008BB89
		internal override uint SetConnectionBufferSize(ref uint unsignedPacketSize)
		{
			return SNINativeMethodWrapper.SNISetInfo(this.Handle, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_BUFSIZE, ref unsignedPacketSize);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0008D998 File Offset: 0x0008BB98
		internal override uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer)
		{
			return SNINativeMethodWrapper.SNISecGenClientContext(this.Handle, receivedBuff, receivedLength, sendBuff, ref sendLength, _sniSpnBuffer);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0008D9AD File Offset: 0x0008BBAD
		internal override uint WaitForSSLHandShakeToComplete()
		{
			return SNINativeMethodWrapper.SNIWaitForSSLHandshakeToComplete(this.Handle, base.GetTimeoutRemaining());
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0008D9C0 File Offset: 0x0008BBC0
		internal override void DisposePacketCache()
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Dispose();
			}
		}

		// Token: 0x04001498 RID: 5272
		private SNIHandle _sessionHandle;

		// Token: 0x04001499 RID: 5273
		private SNIPacket _sniPacket;

		// Token: 0x0400149A RID: 5274
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x0400149B RID: 5275
		private readonly TdsParserStateObjectNative.WritePacketCache _writePacketCache = new TdsParserStateObjectNative.WritePacketCache();

		// Token: 0x0400149C RID: 5276
		private GCHandle _gcHandle;

		// Token: 0x0400149D RID: 5277
		private Dictionary<IntPtr, SNIPacket> _pendingWritePackets = new Dictionary<IntPtr, SNIPacket>();

		// Token: 0x0200027B RID: 635
		internal sealed class WritePacketCache : IDisposable
		{
			// Token: 0x06001DD6 RID: 7638 RVA: 0x0008DA08 File Offset: 0x0008BC08
			public WritePacketCache()
			{
				this._disposed = false;
				this._packets = new Stack<SNIPacket>();
			}

			// Token: 0x06001DD7 RID: 7639 RVA: 0x0008DA24 File Offset: 0x0008BC24
			public SNIPacket Take(SNIHandle sniHandle)
			{
				SNIPacket snipacket;
				if (this._packets.Count > 0)
				{
					snipacket = this._packets.Pop();
					SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
				}
				else
				{
					snipacket = new SNIPacket(sniHandle);
				}
				return snipacket;
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x0008DA5E File Offset: 0x0008BC5E
			public void Add(SNIPacket packet)
			{
				if (!this._disposed)
				{
					this._packets.Push(packet);
					return;
				}
				packet.Dispose();
			}

			// Token: 0x06001DD9 RID: 7641 RVA: 0x0008DA7B File Offset: 0x0008BC7B
			public void Clear()
			{
				while (this._packets.Count > 0)
				{
					this._packets.Pop().Dispose();
				}
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x0008DA9D File Offset: 0x0008BC9D
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this.Clear();
				}
			}

			// Token: 0x0400149E RID: 5278
			private bool _disposed;

			// Token: 0x0400149F RID: 5279
			private Stack<SNIPacket> _packets;
		}
	}
}
