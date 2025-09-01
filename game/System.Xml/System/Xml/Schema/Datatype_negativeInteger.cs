using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053D RID: 1341
	internal class Datatype_negativeInteger : Datatype_nonPositiveInteger
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x0012BE96 File Offset: 0x0012A096
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_negativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x0012BE9D File Offset: 0x0012A09D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0012BEA1 File Offset: 0x0012A0A1
		public Datatype_negativeInteger()
		{
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0012BEA9 File Offset: 0x0012A0A9
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_negativeInteger()
		{
		}

		// Token: 0x040027A7 RID: 10151
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, -1m);
	}
}
