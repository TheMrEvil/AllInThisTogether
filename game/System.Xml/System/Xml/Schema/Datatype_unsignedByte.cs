using System;

namespace System.Xml.Schema
{
	// Token: 0x02000546 RID: 1350
	internal class Datatype_unsignedByte : Datatype_unsignedShort
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x0012C457 File Offset: 0x0012A657
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedByte.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x0012C45E File Offset: 0x0012A65E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0012C464 File Offset: 0x0012A664
		internal override int Compare(object value1, object value2)
		{
			return ((byte)value1).CompareTo(value2);
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x0012C480 File Offset: 0x0012A680
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedByte.atomicValueType;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x0012C487 File Offset: 0x0012A687
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedByte.listValueType;
			}
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x0012C490 File Offset: 0x0012A690
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte b;
				ex = XmlConvert.TryToByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x0012C4DA File Offset: 0x0012A6DA
		public Datatype_unsignedByte()
		{
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x0012C4E2 File Offset: 0x0012A6E2
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_unsignedByte()
		{
		}

		// Token: 0x040027BE RID: 10174
		private static readonly Type atomicValueType = typeof(byte);

		// Token: 0x040027BF RID: 10175
		private static readonly Type listValueType = typeof(byte[]);

		// Token: 0x040027C0 RID: 10176
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 255m);
	}
}
