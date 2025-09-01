using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000385 RID: 901
	internal class DocumentScope
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000DFEDF File Offset: 0x000DE0DF
		internal NamespaceDecl Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000DFEE7 File Offset: 0x000DE0E7
		internal NamespaceDecl AddNamespace(string prefix, string uri, string prevDefaultNsUri)
		{
			this.scopes = new NamespaceDecl(prefix, uri, prevDefaultNsUri, this.scopes);
			return this.scopes;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000DFF04 File Offset: 0x000DE104
		internal string ResolveAtom(string prefix)
		{
			for (NamespaceDecl next = this.scopes; next != null; next = next.Next)
			{
				if (Ref.Equal(next.Prefix, prefix))
				{
					return next.Uri;
				}
			}
			return null;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000DFF3C File Offset: 0x000DE13C
		internal string ResolveNonAtom(string prefix)
		{
			for (NamespaceDecl next = this.scopes; next != null; next = next.Next)
			{
				if (next.Prefix == prefix)
				{
					return next.Uri;
				}
			}
			return null;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0000216B File Offset: 0x0000036B
		public DocumentScope()
		{
		}

		// Token: 0x04001D01 RID: 7425
		protected NamespaceDecl scopes;
	}
}
