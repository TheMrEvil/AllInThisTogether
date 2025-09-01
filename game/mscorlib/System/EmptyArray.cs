using System;

namespace System
{
	// Token: 0x02000236 RID: 566
	internal static class EmptyArray<T>
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x0005FCDB File Offset: 0x0005DEDB
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyArray()
		{
		}

		// Token: 0x04001717 RID: 5911
		public static readonly T[] Value = new T[0];
	}
}
