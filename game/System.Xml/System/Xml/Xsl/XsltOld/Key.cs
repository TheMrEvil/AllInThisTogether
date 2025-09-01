using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A6 RID: 934
	internal class Key
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x000E57D9 File Offset: 0x000E39D9
		public Key(XmlQualifiedName name, int matchkey, int usekey)
		{
			this.name = name;
			this.matchKey = matchkey;
			this.useKey = usekey;
			this.keyNodes = null;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000E57FD File Offset: 0x000E39FD
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000E5805 File Offset: 0x000E3A05
		public int MatchKey
		{
			get
			{
				return this.matchKey;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000E580D File Offset: 0x000E3A0D
		public int UseKey
		{
			get
			{
				return this.useKey;
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000E5815 File Offset: 0x000E3A15
		public void AddKey(XPathNavigator root, Hashtable table)
		{
			if (this.keyNodes == null)
			{
				this.keyNodes = new ArrayList();
			}
			this.keyNodes.Add(new DocumentKeyList(root, table));
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000E5844 File Offset: 0x000E3A44
		public Hashtable GetKeys(XPathNavigator root)
		{
			if (this.keyNodes != null)
			{
				for (int i = 0; i < this.keyNodes.Count; i++)
				{
					if (((DocumentKeyList)this.keyNodes[i]).RootNav.IsSamePosition(root))
					{
						return ((DocumentKeyList)this.keyNodes[i]).KeyTable;
					}
				}
			}
			return null;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000E58AB File Offset: 0x000E3AAB
		public Key Clone()
		{
			return new Key(this.name, this.matchKey, this.useKey);
		}

		// Token: 0x04001DD5 RID: 7637
		private XmlQualifiedName name;

		// Token: 0x04001DD6 RID: 7638
		private int matchKey;

		// Token: 0x04001DD7 RID: 7639
		private int useKey;

		// Token: 0x04001DD8 RID: 7640
		private ArrayList keyNodes;
	}
}
