using System;

namespace System.Xml.Schema
{
	// Token: 0x02000548 RID: 1352
	internal class Datatype_doubleXdr : Datatype_double
	{
		// Token: 0x06003600 RID: 13824 RVA: 0x0012C544 File Offset: 0x0012A744
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			double num;
			try
			{
				num = XmlConvert.ToDouble(s);
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[]
				{
					s
				}), innerException);
			}
			if (double.IsInfinity(num) || double.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0012C5A8 File Offset: 0x0012A7A8
		public Datatype_doubleXdr()
		{
		}
	}
}
