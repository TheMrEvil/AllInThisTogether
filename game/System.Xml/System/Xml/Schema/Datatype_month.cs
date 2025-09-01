using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052A RID: 1322
	internal class Datatype_month : Datatype_dateTimeBase
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x0012B981 File Offset: 0x00129B81
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0012B985 File Offset: 0x00129B85
		internal Datatype_month() : base(XsdDateTimeFlags.GMonth)
		{
		}
	}
}
