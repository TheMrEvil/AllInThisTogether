using System;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	/// <summary>Extends the <see cref="T:System.Xml.Linq.XDocument" /> class by providing a method for navigating and editing an XML node.</summary>
	// Token: 0x0200000A RID: 10
	public static class XDocumentExtensions
	{
		/// <summary>Returns an accessor that allows you to navigate and edit the specified <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <param name="node">The XML node to navigate.</param>
		/// <returns>An interface that provides an accessor to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</returns>
		// Token: 0x06000056 RID: 86 RVA: 0x000034F5 File Offset: 0x000016F5
		public static IXPathNavigable ToXPathNavigable(this XNode node)
		{
			return new XDocumentExtensions.XDocumentNavigable(node);
		}

		// Token: 0x0200000B RID: 11
		private class XDocumentNavigable : IXPathNavigable
		{
			// Token: 0x06000057 RID: 87 RVA: 0x000034FD File Offset: 0x000016FD
			public XDocumentNavigable(XNode n)
			{
				this._node = n;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x0000350C File Offset: 0x0000170C
			public XPathNavigator CreateNavigator()
			{
				return this._node.CreateNavigator();
			}

			// Token: 0x04000033 RID: 51
			private XNode _node;
		}
	}
}
