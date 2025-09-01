using System;

namespace System.Xml.Schema
{
	// Token: 0x02000529 RID: 1321
	internal class Datatype_day : Datatype_dateTimeBase
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x0012B047 File Offset: 0x00129247
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x0012B977 File Offset: 0x00129B77
		internal Datatype_day() : base(XsdDateTimeFlags.GDay)
		{
		}
	}
}
