using System;

namespace System.Xml.Schema
{
	// Token: 0x02000528 RID: 1320
	internal class Datatype_monthDay : Datatype_dateTimeBase
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x0012B969 File Offset: 0x00129B69
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x0012B96D File Offset: 0x00129B6D
		internal Datatype_monthDay() : base(XsdDateTimeFlags.GMonthDay)
		{
		}
	}
}
