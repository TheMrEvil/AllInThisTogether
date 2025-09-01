using System;

namespace System.Xml.Schema
{
	// Token: 0x02000525 RID: 1317
	internal class Datatype_date : Datatype_dateTimeBase
	{
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x0012B941 File Offset: 0x00129B41
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x0012B945 File Offset: 0x00129B45
		internal Datatype_date() : base(XsdDateTimeFlags.Date)
		{
		}
	}
}
