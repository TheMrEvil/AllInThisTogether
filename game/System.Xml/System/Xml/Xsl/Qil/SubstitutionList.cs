using System;
using System.Collections;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004DE RID: 1246
	internal sealed class SubstitutionList
	{
		// Token: 0x06003340 RID: 13120 RVA: 0x00124ABE File Offset: 0x00122CBE
		public SubstitutionList()
		{
			this._s = new ArrayList(4);
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x00124AD2 File Offset: 0x00122CD2
		public void AddSubstitutionPair(QilNode find, QilNode replace)
		{
			this._s.Add(find);
			this._s.Add(replace);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x00124AEE File Offset: 0x00122CEE
		public void RemoveLastSubstitutionPair()
		{
			this._s.RemoveRange(this._s.Count - 2, 2);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x00124B0C File Offset: 0x00122D0C
		public QilNode FindReplacement(QilNode n)
		{
			for (int i = this._s.Count - 2; i >= 0; i -= 2)
			{
				if (this._s[i] == n)
				{
					return (QilNode)this._s[i + 1];
				}
			}
			return null;
		}

		// Token: 0x0400266C RID: 9836
		private ArrayList _s;
	}
}
