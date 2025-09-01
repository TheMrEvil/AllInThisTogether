using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053B RID: 1339
	internal class Datatype_integer : Datatype_decimal
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x0012B37F File Offset: 0x0012957F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0012BE14 File Offset: 0x0012A014
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = this.FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToInteger(s, out num);
				if (ex == null)
				{
					ex = this.FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0012BE60 File Offset: 0x0012A060
		public Datatype_integer()
		{
		}
	}
}
