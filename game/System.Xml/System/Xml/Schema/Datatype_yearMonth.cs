using System;

namespace System.Xml.Schema
{
	// Token: 0x02000526 RID: 1318
	internal class Datatype_yearMonth : Datatype_dateTimeBase
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x0012B94E File Offset: 0x00129B4E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x0012B952 File Offset: 0x00129B52
		internal Datatype_yearMonth() : base(XsdDateTimeFlags.GYearMonth)
		{
		}
	}
}
