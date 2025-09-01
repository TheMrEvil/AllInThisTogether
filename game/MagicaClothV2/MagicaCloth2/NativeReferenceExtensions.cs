using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicaCloth2
{
	// Token: 0x020000F3 RID: 243
	internal static class NativeReferenceExtensions
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x0002278C File Offset: 0x0002098C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int InterlockedStartIndex(this NativeReference<int> counter, int dataCount)
		{
			int* unsafePtr = (int*)counter.GetUnsafePtr<int>();
			return Interlocked.Add(ref *unsafePtr, dataCount) - dataCount;
		}
	}
}
