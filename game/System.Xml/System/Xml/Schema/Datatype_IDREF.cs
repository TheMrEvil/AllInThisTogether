using System;

namespace System.Xml.Schema
{
	// Token: 0x02000538 RID: 1336
	internal class Datatype_IDREF : Datatype_NCName
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x0012BCC9 File Offset: 0x00129EC9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x00066748 File Offset: 0x00064948
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0012BCC1 File Offset: 0x00129EC1
		public Datatype_IDREF()
		{
		}
	}
}
