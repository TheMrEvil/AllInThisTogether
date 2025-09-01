using System;

namespace System.Xml.Schema
{
	// Token: 0x02000543 RID: 1347
	internal class Datatype_unsignedLong : Datatype_nonNegativeInteger
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x0012C210 File Offset: 0x0012A410
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedLong.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x0012C217 File Offset: 0x0012A417
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0012C21C File Offset: 0x0012A41C
		internal override int Compare(object value1, object value2)
		{
			return ((ulong)value1).CompareTo(value2);
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x0012C238 File Offset: 0x0012A438
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedLong.atomicValueType;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x0012C23F File Offset: 0x0012A43F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedLong.listValueType;
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0012C248 File Offset: 0x0012A448
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ulong num;
				ex = XmlConvert.TryToUInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0012C297 File Offset: 0x0012A497
		public Datatype_unsignedLong()
		{
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0012C29F File Offset: 0x0012A49F
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_unsignedLong()
		{
		}

		// Token: 0x040027B5 RID: 10165
		private static readonly Type atomicValueType = typeof(ulong);

		// Token: 0x040027B6 RID: 10166
		private static readonly Type listValueType = typeof(ulong[]);

		// Token: 0x040027B7 RID: 10167
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 18446744073709551615m);
	}
}
