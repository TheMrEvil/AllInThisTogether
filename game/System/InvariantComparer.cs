using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00020AC5 File Offset: 0x0001ECC5
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00020AE0 File Offset: 0x0001ECE0
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

		// Token: 0x060008DB RID: 2267 RVA: 0x00020B1B File Offset: 0x0001ED1B
		// Note: this type is marked as 'beforefieldinit'.
		static InvariantComparer()
		{
		}

		// Token: 0x0400057E RID: 1406
		private CompareInfo m_compareInfo;

		// Token: 0x0400057F RID: 1407
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
