using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001B9 RID: 441
	internal class XmlChildNodes : XmlNodeList
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x00067833 File Offset: 0x00065A33
		public XmlChildNodes(XmlNode container)
		{
			this.container = container;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00067844 File Offset: 0x00065A44
		public override XmlNode Item(int i)
		{
			if (i < 0)
			{
				return null;
			}
			XmlNode xmlNode = this.container.FirstChild;
			while (xmlNode != null)
			{
				if (i == 0)
				{
					return xmlNode;
				}
				xmlNode = xmlNode.NextSibling;
				i--;
			}
			return null;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0006787C File Offset: 0x00065A7C
		public override int Count
		{
			get
			{
				int num = 0;
				for (XmlNode xmlNode = this.container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000678A8 File Offset: 0x00065AA8
		public override IEnumerator GetEnumerator()
		{
			if (this.container.FirstChild == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return new XmlChildEnumerator(this.container);
		}

		// Token: 0x04001040 RID: 4160
		private XmlNode container;
	}
}
