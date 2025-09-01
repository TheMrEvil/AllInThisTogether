using System;

namespace System.Xml.Schema
{
	// Token: 0x02000547 RID: 1351
	internal class Datatype_positiveInteger : Datatype_nonNegativeInteger
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x0012C51B File Offset: 0x0012A71B
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_positiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x0012C522 File Offset: 0x0012A722
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x0012C297 File Offset: 0x0012A497
		public Datatype_positiveInteger()
		{
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0012C526 File Offset: 0x0012A726
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_positiveInteger()
		{
		}

		// Token: 0x040027C1 RID: 10177
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(1m, decimal.MaxValue);
	}
}
