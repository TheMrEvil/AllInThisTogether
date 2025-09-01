using System;

namespace System.Xml.Schema
{
	// Token: 0x02000542 RID: 1346
	internal class Datatype_nonNegativeInteger : Datatype_integer
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x0012C1EA File Offset: 0x0012A3EA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonNegativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x0012C1F1 File Offset: 0x0012A3F1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0012BE73 File Offset: 0x0012A073
		public Datatype_nonNegativeInteger()
		{
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0012C1F5 File Offset: 0x0012A3F5
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_nonNegativeInteger()
		{
		}

		// Token: 0x040027B4 RID: 10164
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, decimal.MaxValue);
	}
}
