using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x0200007E RID: 126
	internal class CFRunLoop : CFObject
	{
		// Token: 0x060001D8 RID: 472
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopAddSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060001D9 RID: 473
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopRemoveSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060001DA RID: 474
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFRunLoopRunInMode(IntPtr mode, double seconds, bool returnAfterSourceHandled);

		// Token: 0x060001DB RID: 475
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetCurrent();

		// Token: 0x060001DC RID: 476
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopStop(IntPtr rl);

		// Token: 0x060001DD RID: 477 RVA: 0x00002474 File Offset: 0x00000674
		public CFRunLoop(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005630 File Offset: 0x00003830
		public static CFRunLoop CurrentRunLoop
		{
			get
			{
				return new CFRunLoop(CFRunLoop.CFRunLoopGetCurrent(), false);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000563D File Offset: 0x0000383D
		public void AddSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopAddSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005651 File Offset: 0x00003851
		public void RemoveSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopRemoveSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005665 File Offset: 0x00003865
		public int RunInMode(CFString mode, double seconds, bool returnAfterSourceHandled)
		{
			return CFRunLoop.CFRunLoopRunInMode(mode.Handle, seconds, returnAfterSourceHandled);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005674 File Offset: 0x00003874
		public void Stop()
		{
			CFRunLoop.CFRunLoopStop(base.Handle);
		}
	}
}
