using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200026C RID: 620
	internal sealed class SNIHandle : SafeHandle
	{
		// Token: 0x06001CF4 RID: 7412 RVA: 0x000899F4 File Offset: 0x00087BF4
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, byte[] spnBuffer, bool ignoreSniOpenTimeout, int timeout, out byte[] instanceName, bool flushCache, bool fSync, bool fParallel) : base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._fSync = fSync;
				instanceName = new byte[256];
				if (ignoreSniOpenTimeout)
				{
					timeout = -1;
				}
				this._status = SNINativeMethodWrapper.SNIOpenSyncEx(myInfo, serverName, ref this.handle, spnBuffer, instanceName, flushCache, fSync, timeout, fParallel);
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00089A64 File Offset: 0x00087C64
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, SNIHandle parent) : base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._status = SNINativeMethodWrapper.SNIOpenMarsSession(myInfo, parent, ref this.handle, parent._fSync);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00089910 File Offset: 0x00087B10
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00089AB0 File Offset: 0x00087CB0
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return !(IntPtr.Zero != handle) || SNINativeMethodWrapper.SNIClose(handle) == 0U;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00089AE7 File Offset: 0x00087CE7
		internal uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x04001425 RID: 5157
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001426 RID: 5158
		private readonly bool _fSync;
	}
}
