using System;

namespace System.Xml.Schema
{
	// Token: 0x0200054D RID: 1357
	internal class Datatype_fixed : Datatype_decimal
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x0012C7A4 File Offset: 0x0012A9A4
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			Exception ex;
			try
			{
				Numeric10FacetsChecker numeric10FacetsChecker = this.FacetsChecker as Numeric10FacetsChecker;
				decimal num = XmlConvert.ToDecimal(s);
				ex = numeric10FacetsChecker.CheckTotalAndFractionDigits(num, 18, 4, true, true);
				if (ex == null)
				{
					return num;
				}
			}
			catch (XmlSchemaException ex2)
			{
				throw ex2;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[]
				{
					s
				}), innerException);
			}
			throw ex;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0012C81C File Offset: 0x0012AA1C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			decimal num;
			Exception ex = XmlConvert.TryToDecimal(s, out num);
			if (ex == null)
			{
				ex = (this.FacetsChecker as Numeric10FacetsChecker).CheckTotalAndFractionDigits(num, 18, 4, true, true);
				if (ex == null)
				{
					typedValue = num;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0012BE60 File Offset: 0x0012A060
		public Datatype_fixed()
		{
		}
	}
}
