using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053F RID: 1343
	internal class Datatype_int : Datatype_long
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x0012BF99 File Offset: 0x0012A199
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_int.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x0012BFA0 File Offset: 0x0012A1A0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0012BFA4 File Offset: 0x0012A1A4
		internal override int Compare(object value1, object value2)
		{
			return ((int)value1).CompareTo(value2);
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x0012BFC0 File Offset: 0x0012A1C0
		public override Type ValueType
		{
			get
			{
				return Datatype_int.atomicValueType;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x0012BFC7 File Offset: 0x0012A1C7
		internal override Type ListValueType
		{
			get
			{
				return Datatype_int.listValueType;
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0012BFD0 File Offset: 0x0012A1D0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_int.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				int num;
				ex = XmlConvert.TryToInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_int.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0012C01A File Offset: 0x0012A21A
		public Datatype_int()
		{
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0012C022 File Offset: 0x0012A222
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_int()
		{
		}

		// Token: 0x040027AB RID: 10155
		private static readonly Type atomicValueType = typeof(int);

		// Token: 0x040027AC RID: 10156
		private static readonly Type listValueType = typeof(int[]);

		// Token: 0x040027AD RID: 10157
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-2147483648m, 2147483647m);
	}
}
