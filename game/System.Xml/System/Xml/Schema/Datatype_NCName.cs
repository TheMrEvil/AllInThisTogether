using System;

namespace System.Xml.Schema
{
	// Token: 0x02000536 RID: 1334
	internal class Datatype_NCName : Datatype_Name
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x0012BC70 File Offset: 0x00129E70
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x0012BC74 File Offset: 0x00129E74
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					nameTable.Add(s);
					typedValue = s;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x0012BCB5 File Offset: 0x00129EB5
		public Datatype_NCName()
		{
		}
	}
}
