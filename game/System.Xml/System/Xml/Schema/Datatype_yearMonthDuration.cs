using System;

namespace System.Xml.Schema
{
	// Token: 0x0200051C RID: 1308
	internal class Datatype_yearMonthDuration : Datatype_duration
	{
		// Token: 0x0600352D RID: 13613 RVA: 0x0012B6D0 File Offset: 0x001298D0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDuration xsdDuration;
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.YearMonthDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.YearMonthDuration, out timeSpan);
					if (ex == null)
					{
						ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
						if (ex == null)
						{
							typedValue = timeSpan;
							return null;
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x0012B744 File Offset: 0x00129944
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x0012B748 File Offset: 0x00129948
		public Datatype_yearMonthDuration()
		{
		}
	}
}
