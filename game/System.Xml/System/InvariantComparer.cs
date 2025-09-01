using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000217A File Offset: 0x0000037A
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002194 File Offset: 0x00000394
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021CF File Offset: 0x000003CF
		// Note: this type is marked as 'beforefieldinit'.
		static InvariantComparer()
		{
		}

		// Token: 0x040004BB RID: 1211
		private CompareInfo m_compareInfo;

		// Token: 0x040004BC RID: 1212
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
