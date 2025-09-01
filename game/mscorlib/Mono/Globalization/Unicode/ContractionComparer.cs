using System;
using System.Collections.Generic;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200006E RID: 110
	internal class ContractionComparer : IComparer<Contraction>
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00005D0C File Offset: 0x00003F0C
		public int Compare(Contraction c1, Contraction c2)
		{
			char[] source = c1.Source;
			char[] source2 = c2.Source;
			int num = (source.Length > source2.Length) ? source2.Length : source.Length;
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			if (source.Length != source2.Length)
			{
				return source.Length - source2.Length;
			}
			return c1.Index - c2.Index;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000259F File Offset: 0x0000079F
		public ContractionComparer()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005D71 File Offset: 0x00003F71
		// Note: this type is marked as 'beforefieldinit'.
		static ContractionComparer()
		{
		}

		// Token: 0x04000E39 RID: 3641
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}
