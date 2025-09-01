using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000DA RID: 218
	internal class XmlDataNode : DataNode<object>
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x00032F2E File Offset: 0x0003112E
		internal XmlDataNode()
		{
			this.dataType = Globals.TypeOfXmlDataNode;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00032F41 File Offset: 0x00031141
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00032F49 File Offset: 0x00031149
		internal IList<XmlAttribute> XmlAttributes
		{
			get
			{
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00032F52 File Offset: 0x00031152
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00032F5A File Offset: 0x0003115A
		internal IList<XmlNode> XmlChildNodes
		{
			get
			{
				return this.xmlChildNodes;
			}
			set
			{
				this.xmlChildNodes = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00032F63 File Offset: 0x00031163
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00032F6B File Offset: 0x0003116B
		internal XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDocument;
			}
			set
			{
				this.ownerDocument = value;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00032F74 File Offset: 0x00031174
		public override void Clear()
		{
			base.Clear();
			this.xmlAttributes = null;
			this.xmlChildNodes = null;
			this.ownerDocument = null;
		}

		// Token: 0x0400052B RID: 1323
		private IList<XmlAttribute> xmlAttributes;

		// Token: 0x0400052C RID: 1324
		private IList<XmlNode> xmlChildNodes;

		// Token: 0x0400052D RID: 1325
		private XmlDocument ownerDocument;
	}
}
