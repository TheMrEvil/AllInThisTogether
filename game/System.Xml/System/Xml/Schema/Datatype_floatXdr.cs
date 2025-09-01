using System;

namespace System.Xml.Schema
{
	// Token: 0x02000549 RID: 1353
	internal class Datatype_floatXdr : Datatype_float
	{
		// Token: 0x06003602 RID: 13826 RVA: 0x0012C5B0 File Offset: 0x0012A7B0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			float num;
			try
			{
				num = XmlConvert.ToSingle(s);
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[]
				{
					s
				}), innerException);
			}
			if (float.IsInfinity(num) || float.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0012C614 File Offset: 0x0012A814
		public Datatype_floatXdr()
		{
		}
	}
}
