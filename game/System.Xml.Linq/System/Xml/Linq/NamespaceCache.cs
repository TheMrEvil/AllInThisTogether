using System;

namespace System.Xml.Linq
{
	// Token: 0x02000040 RID: 64
	internal struct NamespaceCache
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000B972 File Offset: 0x00009B72
		public XNamespace Get(string namespaceName)
		{
			if (namespaceName == this._namespaceName)
			{
				return this._ns;
			}
			this._namespaceName = namespaceName;
			this._ns = XNamespace.Get(namespaceName);
			return this._ns;
		}

		// Token: 0x0400014A RID: 330
		private XNamespace _ns;

		// Token: 0x0400014B RID: 331
		private string _namespaceName;
	}
}
