using System;

namespace System.Xml.Schema
{
	// Token: 0x02000545 RID: 1349
	internal class Datatype_unsignedShort : Datatype_unsignedInt
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x0012C394 File Offset: 0x0012A594
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedShort.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x0012C39B File Offset: 0x0012A59B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0012C3A0 File Offset: 0x0012A5A0
		internal override int Compare(object value1, object value2)
		{
			return ((ushort)value1).CompareTo(value2);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x0012C3BC File Offset: 0x0012A5BC
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedShort.atomicValueType;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x0012C3C3 File Offset: 0x0012A5C3
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedShort.listValueType;
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0012C3CC File Offset: 0x0012A5CC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ushort num;
				ex = XmlConvert.TryToUInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckValueFacets((int)num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0012C416 File Offset: 0x0012A616
		public Datatype_unsignedShort()
		{
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0012C41E File Offset: 0x0012A61E
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_unsignedShort()
		{
		}

		// Token: 0x040027BB RID: 10171
		private static readonly Type atomicValueType = typeof(ushort);

		// Token: 0x040027BC RID: 10172
		private static readonly Type listValueType = typeof(ushort[]);

		// Token: 0x040027BD RID: 10173
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 65535m);
	}
}
