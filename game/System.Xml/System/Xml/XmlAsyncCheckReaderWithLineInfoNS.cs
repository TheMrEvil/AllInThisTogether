using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000064 RID: 100
	internal class XmlAsyncCheckReaderWithLineInfoNS : XmlAsyncCheckReaderWithLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public XmlAsyncCheckReaderWithLineInfoNS(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010DB7 File Offset: 0x0000EFB7
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010DC5 File Offset: 0x0000EFC5
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010DD3 File Offset: 0x0000EFD3
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040006B1 RID: 1713
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
