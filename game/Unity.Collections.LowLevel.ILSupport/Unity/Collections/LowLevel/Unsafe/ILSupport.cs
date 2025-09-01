using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000004 RID: 4
	internal static class ILSupport
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(in T thing) where T : struct
		{
			return (void*)(&thing);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002059 File Offset: 0x00000259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<T>(in T thing) where T : struct
		{
			return ref thing;
		}
	}
}
