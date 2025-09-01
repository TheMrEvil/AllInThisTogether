using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000407 RID: 1031
	internal class Sort : XslNode
	{
		// Token: 0x060028C4 RID: 10436 RVA: 0x000F5174 File Offset: 0x000F3374
		public Sort(string select, string lang, string dataType, string order, string caseOrder, XslVersion xslVer) : base(XslNodeType.Sort, null, select, xslVer)
		{
			this.Lang = lang;
			this.DataType = dataType;
			this.Order = order;
			this.CaseOrder = caseOrder;
		}

		// Token: 0x0400205A RID: 8282
		public readonly string Lang;

		// Token: 0x0400205B RID: 8283
		public readonly string DataType;

		// Token: 0x0400205C RID: 8284
		public readonly string Order;

		// Token: 0x0400205D RID: 8285
		public readonly string CaseOrder;
	}
}
