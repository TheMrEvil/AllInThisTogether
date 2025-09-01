using System;

namespace System.Xml.Schema
{
	// Token: 0x02000527 RID: 1319
	internal class Datatype_year : Datatype_dateTimeBase
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x0012B95B File Offset: 0x00129B5B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0012B95F File Offset: 0x00129B5F
		internal Datatype_year() : base(XsdDateTimeFlags.GYear)
		{
		}
	}
}
