using System;

namespace System.Xml.Schema
{
	// Token: 0x0200051D RID: 1309
	internal class Datatype_dayTimeDuration : Datatype_duration
	{
		// Token: 0x06003530 RID: 13616 RVA: 0x0012B750 File Offset: 0x00129950
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
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.DayTimeDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.DayTimeDuration, out timeSpan);
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

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x0012B7C4 File Offset: 0x001299C4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DayTimeDuration;
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x0012B748 File Offset: 0x00129948
		public Datatype_dayTimeDuration()
		{
		}
	}
}
