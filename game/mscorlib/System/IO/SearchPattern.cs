using System;

namespace System.IO
{
	// Token: 0x02000B69 RID: 2921
	internal class SearchPattern
	{
		// Token: 0x06006A57 RID: 27223 RVA: 0x0000259F File Offset: 0x0000079F
		public SearchPattern()
		{
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x0016C13B File Offset: 0x0016A33B
		// Note: this type is marked as 'beforefieldinit'.
		static SearchPattern()
		{
		}

		// Token: 0x04003D88 RID: 15752
		internal static readonly char[] WildcardChars = new char[]
		{
			'*',
			'?'
		};
	}
}
