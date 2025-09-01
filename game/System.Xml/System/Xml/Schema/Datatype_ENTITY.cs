using System;

namespace System.Xml.Schema
{
	// Token: 0x02000539 RID: 1337
	internal class Datatype_ENTITY : Datatype_NCName
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x0012BCCD File Offset: 0x00129ECD
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x00067362 File Offset: 0x00065562
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0012BCC1 File Offset: 0x00129EC1
		public Datatype_ENTITY()
		{
		}
	}
}
