using System;

namespace System.Xml.Schema
{
	// Token: 0x0200050B RID: 1291
	internal class XsdSimpleValue
	{
		// Token: 0x06003484 RID: 13444 RVA: 0x0012959C File Offset: 0x0012779C
		public XsdSimpleValue(XmlSchemaSimpleType st, object value)
		{
			this.xmlType = st;
			this.typedValue = value;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x001295B2 File Offset: 0x001277B2
		public XmlSchemaSimpleType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x001295BA File Offset: 0x001277BA
		public object TypedValue
		{
			get
			{
				return this.typedValue;
			}
		}

		// Token: 0x04002707 RID: 9991
		private XmlSchemaSimpleType xmlType;

		// Token: 0x04002708 RID: 9992
		private object typedValue;
	}
}
