using System;

namespace System.Xml.Schema
{
	// Token: 0x02000535 RID: 1333
	internal class Datatype_Name : Datatype_token
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x0012BC6C File Offset: 0x00129E6C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x0012BC64 File Offset: 0x00129E64
		public Datatype_Name()
		{
		}
	}
}
