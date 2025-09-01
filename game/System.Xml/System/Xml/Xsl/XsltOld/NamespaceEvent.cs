using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000392 RID: 914
	internal class NamespaceEvent : Event
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000E13C2 File Offset: 0x000DF5C2
		public NamespaceEvent(NavigatorInput input)
		{
			this.namespaceUri = input.Value;
			this.name = input.LocalName;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000E13E4 File Offset: 0x000DF5E4
		public override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.namespaceUri.Length != 0)
			{
				NamespaceInfo namespaceInfo = compiler.FindNamespaceAlias(this.namespaceUri);
				if (namespaceInfo != null)
				{
					this.namespaceUri = namespaceInfo.nameSpace;
					if (namespaceInfo.prefix != null)
					{
						this.name = namespaceInfo.prefix;
					}
				}
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000E142E File Offset: 0x000DF62E
		public override bool Output(Processor processor, ActionFrame frame)
		{
			processor.BeginEvent(XPathNodeType.Namespace, null, this.name, this.namespaceUri, false);
			processor.EndEvent(XPathNodeType.Namespace);
			return true;
		}

		// Token: 0x04001D31 RID: 7473
		private string namespaceUri;

		// Token: 0x04001D32 RID: 7474
		private string name;
	}
}
