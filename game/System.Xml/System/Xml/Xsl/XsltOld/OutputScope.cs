using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200039A RID: 922
	internal class OutputScope : DocumentScope
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x000E28CA File Offset: 0x000E0ACA
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000E28D2 File Offset: 0x000E0AD2
		internal string Namespace
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x000E28DA File Offset: 0x000E0ADA
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x000E28E2 File Offset: 0x000E0AE2
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000E28EB File Offset: 0x000E0AEB
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x000E28F3 File Offset: 0x000E0AF3
		internal XmlSpace Space
		{
			get
			{
				return this.space;
			}
			set
			{
				this.space = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000E28FC File Offset: 0x000E0AFC
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x000E2904 File Offset: 0x000E0B04
		internal string Lang
		{
			get
			{
				return this.lang;
			}
			set
			{
				this.lang = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000E290D File Offset: 0x000E0B0D
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x000E2915 File Offset: 0x000E0B15
		internal bool Mixed
		{
			get
			{
				return this.mixed;
			}
			set
			{
				this.mixed = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000E291E File Offset: 0x000E0B1E
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x000E2926 File Offset: 0x000E0B26
		internal bool ToCData
		{
			get
			{
				return this.toCData;
			}
			set
			{
				this.toCData = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000E292F File Offset: 0x000E0B2F
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x000E2937 File Offset: 0x000E0B37
		internal HtmlElementProps HtmlElementProps
		{
			get
			{
				return this.htmlElementProps;
			}
			set
			{
				this.htmlElementProps = value;
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000E2940 File Offset: 0x000E0B40
		internal OutputScope()
		{
			this.Init(string.Empty, string.Empty, string.Empty, XmlSpace.None, string.Empty, false);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000E2964 File Offset: 0x000E0B64
		internal void Init(string name, string nspace, string prefix, XmlSpace space, string lang, bool mixed)
		{
			this.scopes = null;
			this.name = name;
			this.nsUri = nspace;
			this.prefix = prefix;
			this.space = space;
			this.lang = lang;
			this.mixed = mixed;
			this.toCData = false;
			this.htmlElementProps = null;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000E29B4 File Offset: 0x000E0BB4
		internal bool FindPrefix(string urn, out string prefix)
		{
			for (NamespaceDecl namespaceDecl = this.scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				if (Ref.Equal(namespaceDecl.Uri, urn) && namespaceDecl.Prefix != null && namespaceDecl.Prefix.Length > 0)
				{
					prefix = namespaceDecl.Prefix;
					return true;
				}
			}
			prefix = string.Empty;
			return false;
		}

		// Token: 0x04001D68 RID: 7528
		private string name;

		// Token: 0x04001D69 RID: 7529
		private string nsUri;

		// Token: 0x04001D6A RID: 7530
		private string prefix;

		// Token: 0x04001D6B RID: 7531
		private XmlSpace space;

		// Token: 0x04001D6C RID: 7532
		private string lang;

		// Token: 0x04001D6D RID: 7533
		private bool mixed;

		// Token: 0x04001D6E RID: 7534
		private bool toCData;

		// Token: 0x04001D6F RID: 7535
		private HtmlElementProps htmlElementProps;
	}
}
