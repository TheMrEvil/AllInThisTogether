using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053C RID: 1340
	internal class Datatype_nonPositiveInteger : Datatype_integer
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x0012BE68 File Offset: 0x0012A068
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonPositiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x0012BE6F File Offset: 0x0012A06F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0012BE73 File Offset: 0x0012A073
		public Datatype_nonPositiveInteger()
		{
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0012BE7B File Offset: 0x0012A07B
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_nonPositiveInteger()
		{
		}

		// Token: 0x040027A6 RID: 10150
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, 0m);
	}
}
