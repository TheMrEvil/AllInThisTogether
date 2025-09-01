using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	// Token: 0x0200042F RID: 1071
	[NativeConditional("ENABLE_PROFILER")]
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	[StaticAccessor("FileAccessor", StaticAccessorType.DoubleColon)]
	internal static class File
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x0003F114 File Offset: 0x0003D314
		internal static ulong totalOpenCalls
		{
			get
			{
				return File.GetTotalOpenCalls();
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x0003F12C File Offset: 0x0003D32C
		internal static ulong totalCloseCalls
		{
			get
			{
				return File.GetTotalCloseCalls();
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x0003F144 File Offset: 0x0003D344
		internal static ulong totalReadCalls
		{
			get
			{
				return File.GetTotalReadCalls();
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x0003F15C File Offset: 0x0003D35C
		internal static ulong totalWriteCalls
		{
			get
			{
				return File.GetTotalWriteCalls();
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0003F174 File Offset: 0x0003D374
		internal static ulong totalSeekCalls
		{
			get
			{
				return File.GetTotalSeekCalls();
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x0003F18C File Offset: 0x0003D38C
		internal static ulong totalZeroSeekCalls
		{
			get
			{
				return File.GetTotalZeroSeekCalls();
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		internal static ulong totalFilesOpened
		{
			get
			{
				return File.GetTotalFilesOpened();
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0003F1BC File Offset: 0x0003D3BC
		internal static ulong totalFilesClosed
		{
			get
			{
				return File.GetTotalFilesClosed();
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
		internal static ulong totalBytesRead
		{
			get
			{
				return File.GetTotalBytesRead();
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x0003F1EC File Offset: 0x0003D3EC
		internal static ulong totalBytesWritten
		{
			get
			{
				return File.GetTotalBytesWritten();
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0003F210 File Offset: 0x0003D410
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x0003F203 File Offset: 0x0003D403
		internal static bool recordZeroSeeks
		{
			get
			{
				return File.GetRecordZeroSeeks();
			}
			set
			{
				File.SetRecordZeroSeeks(value);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x0003F228 File Offset: 0x0003D428
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x0003F23F File Offset: 0x0003D43F
		internal static ThreadIORestrictionMode MainThreadIORestrictionMode
		{
			get
			{
				return File.GetMainThreadFileIORestriction();
			}
			set
			{
				File.SetMainThreadFileIORestriction(value);
			}
		}

		// Token: 0x06002552 RID: 9554
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetRecordZeroSeeks(bool enable);

		// Token: 0x06002553 RID: 9555
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetRecordZeroSeeks();

		// Token: 0x06002554 RID: 9556
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalOpenCalls();

		// Token: 0x06002555 RID: 9557
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalCloseCalls();

		// Token: 0x06002556 RID: 9558
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalReadCalls();

		// Token: 0x06002557 RID: 9559
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalWriteCalls();

		// Token: 0x06002558 RID: 9560
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalSeekCalls();

		// Token: 0x06002559 RID: 9561
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalZeroSeekCalls();

		// Token: 0x0600255A RID: 9562
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesOpened();

		// Token: 0x0600255B RID: 9563
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesClosed();

		// Token: 0x0600255C RID: 9564
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesRead();

		// Token: 0x0600255D RID: 9565
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesWritten();

		// Token: 0x0600255E RID: 9566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMainThreadFileIORestriction(ThreadIORestrictionMode mode);

		// Token: 0x0600255F RID: 9567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadIORestrictionMode GetMainThreadFileIORestriction();
	}
}
