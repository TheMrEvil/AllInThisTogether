using System;

namespace System.Xml.Schema
{
	// Token: 0x0200054A RID: 1354
	internal class Datatype_QNameXdr : Datatype_anySimpleType
	{
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0012C61C File Offset: 0x0012A81C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (s == null || s.Length == 0)
			{
				throw new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			if (nsmgr == null)
			{
				throw new ArgumentNullException("nsmgr");
			}
			object result;
			try
			{
				string text;
				result = XmlQualifiedName.Parse(s.Trim(), nsmgr, out text);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[]
				{
					s
				}), innerException);
			}
			return result;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x0012C6A0 File Offset: 0x0012A8A0
		public override Type ValueType
		{
			get
			{
				return Datatype_QNameXdr.atomicValueType;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x0012C6A7 File Offset: 0x0012A8A7
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QNameXdr.listValueType;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_QNameXdr()
		{
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0012C6AE File Offset: 0x0012A8AE
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_QNameXdr()
		{
		}

		// Token: 0x040027C2 RID: 10178
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x040027C3 RID: 10179
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
