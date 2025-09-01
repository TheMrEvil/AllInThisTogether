using System;

namespace System.Xml.Schema
{
	// Token: 0x02000537 RID: 1335
	internal class Datatype_ID : Datatype_NCName
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x0012BCBD File Offset: 0x00129EBD
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x0001222F File Offset: 0x0001042F
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0012BCC1 File Offset: 0x00129EC1
		public Datatype_ID()
		{
		}
	}
}
