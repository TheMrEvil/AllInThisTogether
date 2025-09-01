using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053E RID: 1342
	internal class Datatype_long : Datatype_integer
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x0012BEC4 File Offset: 0x0012A0C4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_long.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x0012BECB File Offset: 0x0012A0CB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0012BED0 File Offset: 0x0012A0D0
		internal override int Compare(object value1, object value2)
		{
			return ((long)value1).CompareTo(value2);
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x0012BEEC File Offset: 0x0012A0EC
		public override Type ValueType
		{
			get
			{
				return Datatype_long.atomicValueType;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x0012BEF3 File Offset: 0x0012A0F3
		internal override Type ListValueType
		{
			get
			{
				return Datatype_long.listValueType;
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0012BEFC File Offset: 0x0012A0FC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_long.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				long num;
				ex = XmlConvert.TryToInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_long.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0012BE73 File Offset: 0x0012A073
		public Datatype_long()
		{
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x0012BF48 File Offset: 0x0012A148
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_long()
		{
		}

		// Token: 0x040027A8 RID: 10152
		private static readonly Type atomicValueType = typeof(long);

		// Token: 0x040027A9 RID: 10153
		private static readonly Type listValueType = typeof(long[]);

		// Token: 0x040027AA RID: 10154
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-9223372036854775808m, 9223372036854775807m);
	}
}
