using System;

namespace System.Xml.Schema
{
	// Token: 0x02000534 RID: 1332
	internal class Datatype_NMTOKEN : Datatype_token
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x0001E51A File Offset: 0x0001C71A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x0006AAC4 File Offset: 0x00068CC4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x0012BC64 File Offset: 0x00129E64
		public Datatype_NMTOKEN()
		{
		}
	}
}
