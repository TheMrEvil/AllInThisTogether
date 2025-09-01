using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000058 RID: 88
	public static class ListBufferExtensions
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000EA22 File Offset: 0x0000CC22
		public unsafe static void QuickSort<[IsUnmanaged] T>(this ListBuffer<T> self) where T : struct, ValueType, IComparable<T>
		{
			CoreUnsafeUtils.QuickSort<int>(self.Count, (void*)self.BufferPtr);
		}
	}
}
