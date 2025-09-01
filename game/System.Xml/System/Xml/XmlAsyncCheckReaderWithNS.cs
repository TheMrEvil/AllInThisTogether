using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000062 RID: 98
	internal class XmlAsyncCheckReaderWithNS : XmlAsyncCheckReader, IXmlNamespaceResolver
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00010D27 File Offset: 0x0000EF27
		public XmlAsyncCheckReaderWithNS(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00010D3C File Offset: 0x0000EF3C
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00010D4A File Offset: 0x0000EF4A
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010D58 File Offset: 0x0000EF58
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040006AF RID: 1711
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
