using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200046B RID: 1131
	internal class XmlExtensionFunctionTable
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x00105699 File Offset: 0x00103899
		public XmlExtensionFunctionTable()
		{
			this.table = new Dictionary<XmlExtensionFunction, XmlExtensionFunction>();
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x001056AC File Offset: 0x001038AC
		public XmlExtensionFunction Bind(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			if (this.funcCached == null)
			{
				this.funcCached = new XmlExtensionFunction();
			}
			this.funcCached.Init(name, namespaceUri, numArgs, objectType, flags);
			XmlExtensionFunction xmlExtensionFunction;
			if (!this.table.TryGetValue(this.funcCached, out xmlExtensionFunction))
			{
				xmlExtensionFunction = this.funcCached;
				this.funcCached = null;
				xmlExtensionFunction.Bind();
				this.table.Add(xmlExtensionFunction, xmlExtensionFunction);
			}
			return xmlExtensionFunction;
		}

		// Token: 0x040022BB RID: 8891
		private Dictionary<XmlExtensionFunction, XmlExtensionFunction> table;

		// Token: 0x040022BC RID: 8892
		private XmlExtensionFunction funcCached;
	}
}
