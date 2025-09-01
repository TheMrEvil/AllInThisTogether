using System;

namespace System.Xml.Schema
{
	// Token: 0x02000564 RID: 1380
	internal class NamespaceListV1Compat : NamespaceList
	{
		// Token: 0x060036DD RID: 14045 RVA: 0x00134059 File Offset: 0x00132259
		public NamespaceListV1Compat(string namespaces, string targetNamespace) : base(namespaces, targetNamespace)
		{
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x00134063 File Offset: 0x00132263
		public override bool Allows(string ns)
		{
			if (base.Type == NamespaceList.ListType.Other)
			{
				return ns != base.Excluded;
			}
			return base.Allows(ns);
		}
	}
}
