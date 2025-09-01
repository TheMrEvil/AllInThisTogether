using System;

namespace System.Xml.Schema
{
	// Token: 0x02000524 RID: 1316
	internal class Datatype_time : Datatype_dateTimeBase
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x0012B93D File Offset: 0x00129B3D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x0012B934 File Offset: 0x00129B34
		internal Datatype_time() : base(XsdDateTimeFlags.Time)
		{
		}
	}
}
