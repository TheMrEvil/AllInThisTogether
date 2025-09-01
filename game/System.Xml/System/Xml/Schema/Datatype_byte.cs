using System;

namespace System.Xml.Schema
{
	// Token: 0x02000541 RID: 1345
	internal class Datatype_byte : Datatype_short
	{
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x0012C128 File Offset: 0x0012A328
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_byte.numeric10FacetsChecker;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x0012C12F File Offset: 0x0012A32F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x0012C134 File Offset: 0x0012A334
		internal override int Compare(object value1, object value2)
		{
			return ((sbyte)value1).CompareTo(value2);
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x0012C150 File Offset: 0x0012A350
		public override Type ValueType
		{
			get
			{
				return Datatype_byte.atomicValueType;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x0012C157 File Offset: 0x0012A357
		internal override Type ListValueType
		{
			get
			{
				return Datatype_byte.listValueType;
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x0012C160 File Offset: 0x0012A360
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_byte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				sbyte b;
				ex = XmlConvert.TryToSByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_byte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x0012C1AA File Offset: 0x0012A3AA
		public Datatype_byte()
		{
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x0012C1B2 File Offset: 0x0012A3B2
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_byte()
		{
		}

		// Token: 0x040027B1 RID: 10161
		private static readonly Type atomicValueType = typeof(sbyte);

		// Token: 0x040027B2 RID: 10162
		private static readonly Type listValueType = typeof(sbyte[]);

		// Token: 0x040027B3 RID: 10163
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-128m, 127m);
	}
}
