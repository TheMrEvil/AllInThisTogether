using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.Interop
{
	// Token: 0x0200003C RID: 60
	[SecurityCritical]
	internal sealed class SafeEventLogWriteHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00008A9B File Offset: 0x00006C9B
		[SecurityCritical]
		private SafeEventLogWriteHandle() : base(true)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008AA4 File Offset: 0x00006CA4
		[SecurityCritical]
		public static SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName)
		{
			SafeEventLogWriteHandle safeEventLogWriteHandle = UnsafeNativeMethods.RegisterEventSource(uncServerName, sourceName);
			Marshal.GetLastWin32Error();
			bool isInvalid = safeEventLogWriteHandle.IsInvalid;
			return safeEventLogWriteHandle;
		}

		// Token: 0x0600020C RID: 524
		[DllImport("advapi32", SetLastError = true)]
		private static extern bool DeregisterEventSource(IntPtr hEventLog);

		// Token: 0x0600020D RID: 525 RVA: 0x00008ABA File Offset: 0x00006CBA
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeEventLogWriteHandle.DeregisterEventSource(this.handle);
		}
	}
}
