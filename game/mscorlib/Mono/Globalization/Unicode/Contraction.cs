using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200006D RID: 109
	internal class Contraction
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005CE5 File Offset: 0x00003EE5
		public Contraction(int index, char[] source, string replacement, byte[] sortkey)
		{
			this.Index = index;
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x04000E35 RID: 3637
		public int Index;

		// Token: 0x04000E36 RID: 3638
		public readonly char[] Source;

		// Token: 0x04000E37 RID: 3639
		public readonly string Replacement;

		// Token: 0x04000E38 RID: 3640
		public readonly byte[] SortKey;
	}
}
