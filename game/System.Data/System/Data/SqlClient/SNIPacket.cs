using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200026D RID: 621
	internal sealed class SNIPacket : SafeHandle
	{
		// Token: 0x06001CF9 RID: 7417 RVA: 0x00089AEF File Offset: 0x00087CEF
		internal SNIPacket(SafeHandle sniHandle) : base(IntPtr.Zero, true)
		{
			SNINativeMethodWrapper.SNIPacketAllocate(sniHandle, SNINativeMethodWrapper.IOType.WRITE, ref this.handle);
			if (IntPtr.Zero == this.handle)
			{
				throw SQL.SNIPacketAllocationFailure();
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00089910 File Offset: 0x00087B10
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00089B24 File Offset: 0x00087D24
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SNINativeMethodWrapper.SNIPacketRelease(handle);
			}
			return true;
		}
	}
}
