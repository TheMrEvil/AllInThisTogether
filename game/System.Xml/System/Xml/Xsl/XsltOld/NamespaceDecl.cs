using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000391 RID: 913
	internal class NamespaceDecl
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000E1370 File Offset: 0x000DF570
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000E1378 File Offset: 0x000DF578
		internal string Uri
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x000E1380 File Offset: 0x000DF580
		internal string PrevDefaultNsUri
		{
			get
			{
				return this.prevDefaultNsUri;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x000E1388 File Offset: 0x000DF588
		internal NamespaceDecl Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000E1390 File Offset: 0x000DF590
		internal NamespaceDecl(string prefix, string nsUri, string prevDefaultNsUri, NamespaceDecl next)
		{
			this.Init(prefix, nsUri, prevDefaultNsUri, next);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000E13A3 File Offset: 0x000DF5A3
		internal void Init(string prefix, string nsUri, string prevDefaultNsUri, NamespaceDecl next)
		{
			this.prefix = prefix;
			this.nsUri = nsUri;
			this.prevDefaultNsUri = prevDefaultNsUri;
			this.next = next;
		}

		// Token: 0x04001D2D RID: 7469
		private string prefix;

		// Token: 0x04001D2E RID: 7470
		private string nsUri;

		// Token: 0x04001D2F RID: 7471
		private string prevDefaultNsUri;

		// Token: 0x04001D30 RID: 7472
		private NamespaceDecl next;
	}
}
