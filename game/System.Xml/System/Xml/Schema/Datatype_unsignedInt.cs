using System;

namespace System.Xml.Schema
{
	// Token: 0x02000544 RID: 1348
	internal class Datatype_unsignedInt : Datatype_unsignedLong
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x0012C2D5 File Offset: 0x0012A4D5
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedInt.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x0012C2DC File Offset: 0x0012A4DC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0012C2E0 File Offset: 0x0012A4E0
		internal override int Compare(object value1, object value2)
		{
			return ((uint)value1).CompareTo(value2);
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x0012C2FC File Offset: 0x0012A4FC
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedInt.atomicValueType;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060035E8 RID: 13800 RVA: 0x0012C303 File Offset: 0x0012A503
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedInt.listValueType;
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0012C30C File Offset: 0x0012A50C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				uint num;
				ex = XmlConvert.TryToUInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckValueFacets((long)((ulong)num), this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x0012C357 File Offset: 0x0012A557
		public Datatype_unsignedInt()
		{
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0012C35F File Offset: 0x0012A55F
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_unsignedInt()
		{
		}

		// Token: 0x040027B8 RID: 10168
		private static readonly Type atomicValueType = typeof(uint);

		// Token: 0x040027B9 RID: 10169
		private static readonly Type listValueType = typeof(uint[]);

		// Token: 0x040027BA RID: 10170
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 4294967295m);
	}
}
