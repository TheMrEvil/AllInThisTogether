using System;

namespace System.Xml.Schema
{
	// Token: 0x0200054B RID: 1355
	internal class Datatype_ENUMERATION : Datatype_NMTOKEN
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x0006807E File Offset: 0x0006627E
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENUMERATION;
			}
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x0012C6CE File Offset: 0x0012A8CE
		public Datatype_ENUMERATION()
		{
		}
	}
}
