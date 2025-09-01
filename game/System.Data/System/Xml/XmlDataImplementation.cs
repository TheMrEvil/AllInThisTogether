using System;

namespace System.Xml
{
	// Token: 0x02000080 RID: 128
	internal sealed class XmlDataImplementation : XmlImplementation
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x00017F24 File Offset: 0x00016124
		public XmlDataImplementation()
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00017F2C File Offset: 0x0001612C
		public override XmlDocument CreateDocument()
		{
			return new XmlDataDocument(this);
		}
	}
}
