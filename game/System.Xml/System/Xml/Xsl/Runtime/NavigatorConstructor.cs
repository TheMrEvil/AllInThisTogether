using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000443 RID: 1091
	internal sealed class NavigatorConstructor
	{
		// Token: 0x06002B1C RID: 11036 RVA: 0x001031EC File Offset: 0x001013EC
		public XPathNavigator GetNavigator(XmlEventCache events, XmlNameTable nameTable)
		{
			if (this.cache == null)
			{
				XPathDocument xpathDocument = new XPathDocument(nameTable);
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames | (events.HasRootNode ? XPathDocument.LoadFlags.None : XPathDocument.LoadFlags.Fragment), events.BaseUri);
				events.EventsToWriter(xmlRawWriter);
				xmlRawWriter.Close();
				this.cache = xpathDocument;
			}
			return ((XPathDocument)this.cache).CreateNavigator();
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x00103248 File Offset: 0x00101448
		public XPathNavigator GetNavigator(string text, string baseUri, XmlNameTable nameTable)
		{
			if (this.cache == null)
			{
				XPathDocument xpathDocument = new XPathDocument(nameTable);
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, baseUri);
				xmlRawWriter.WriteString(text);
				xmlRawWriter.Close();
				this.cache = xpathDocument;
			}
			return ((XPathDocument)this.cache).CreateNavigator();
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0000216B File Offset: 0x0000036B
		public NavigatorConstructor()
		{
		}

		// Token: 0x0400220F RID: 8719
		private object cache;
	}
}
