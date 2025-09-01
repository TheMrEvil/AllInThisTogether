using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000247 RID: 583
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004E230 File Offset: 0x0004C430
		internal ProcessWaitHandle(SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!NativeMethods.DuplicateHandle(new HandleRef(this, NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				throw new SystemException("Unknown error in DuplicateHandle");
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
