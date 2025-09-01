using System;

namespace System.Linq.Parallel
{
	// Token: 0x020000FE RID: 254
	internal class JaggedArray<TElement>
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public static TElement[][] Allocate(int size1, int size2)
		{
			TElement[][] array = new TElement[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TElement[size2];
			}
			return array;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00002162 File Offset: 0x00000362
		public JaggedArray()
		{
		}
	}
}
