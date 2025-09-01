using System;

namespace System.Xml.Schema
{
	// Token: 0x02000540 RID: 1344
	internal class Datatype_short : Datatype_int
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x0012C060 File Offset: 0x0012A260
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_short.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x0012C067 File Offset: 0x0012A267
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x0012C06C File Offset: 0x0012A26C
		internal override int Compare(object value1, object value2)
		{
			return ((short)value1).CompareTo(value2);
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x0012C088 File Offset: 0x0012A288
		public override Type ValueType
		{
			get
			{
				return Datatype_short.atomicValueType;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x0012C08F File Offset: 0x0012A28F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_short.listValueType;
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0012C098 File Offset: 0x0012A298
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_short.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				short num;
				ex = XmlConvert.TryToInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_short.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0012C0E2 File Offset: 0x0012A2E2
		public Datatype_short()
		{
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0012C0EA File Offset: 0x0012A2EA
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_short()
		{
		}

		// Token: 0x040027AE RID: 10158
		private static readonly Type atomicValueType = typeof(short);

		// Token: 0x040027AF RID: 10159
		private static readonly Type listValueType = typeof(short[]);

		// Token: 0x040027B0 RID: 10160
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-32768m, 32767m);
	}
}
