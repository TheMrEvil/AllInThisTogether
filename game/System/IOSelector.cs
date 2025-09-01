using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000172 RID: 370
	internal static class IOSelector
	{
		// Token: 0x060009D8 RID: 2520
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Add(IntPtr handle, IOSelectorJob job);

		// Token: 0x060009D9 RID: 2521
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Remove(IntPtr handle);
	}
}
