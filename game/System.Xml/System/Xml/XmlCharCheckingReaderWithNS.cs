using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000070 RID: 112
	internal class XmlCharCheckingReaderWithNS : XmlCharCheckingReader, IXmlNamespaceResolver
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00013CF2 File Offset: 0x00011EF2
		internal XmlCharCheckingReaderWithNS(XmlReader reader, IXmlNamespaceResolver readerAsNSResolver, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing) : base(reader, checkCharacters, ignoreWhitespace, ignoreComments, ignorePis, dtdProcessing)
		{
			this.readerAsNSResolver = readerAsNSResolver;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013D0B File Offset: 0x00011F0B
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013D19 File Offset: 0x00011F19
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013D27 File Offset: 0x00011F27
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040006F0 RID: 1776
		internal IXmlNamespaceResolver readerAsNSResolver;
	}
}
