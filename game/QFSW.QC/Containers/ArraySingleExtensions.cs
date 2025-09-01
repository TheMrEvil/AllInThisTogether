using System;

namespace QFSW.QC.Containers
{
	// Token: 0x02000069 RID: 105
	public static class ArraySingleExtensions
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000A288 File Offset: 0x00008488
		public static ArraySingle<T> AsArraySingle<T>(this T data)
		{
			return new ArraySingle<T>(data);
		}
	}
}
