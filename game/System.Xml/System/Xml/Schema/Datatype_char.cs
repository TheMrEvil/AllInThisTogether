using System;

namespace System.Xml.Schema
{
	// Token: 0x0200054C RID: 1356
	internal class Datatype_char : Datatype_anySimpleType
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x0012C6D6 File Offset: 0x0012A8D6
		public override Type ValueType
		{
			get
			{
				return Datatype_char.atomicValueType;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x0012C6DD File Offset: 0x0012A8DD
		internal override Type ListValueType
		{
			get
			{
				return Datatype_char.listValueType;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x0012C6E4 File Offset: 0x0012A8E4
		internal override int Compare(object value1, object value2)
		{
			return ((char)value1).CompareTo(value2);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0012C700 File Offset: 0x0012A900
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object result;
			try
			{
				result = XmlConvert.ToChar(s);
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

		// Token: 0x06003611 RID: 13841 RVA: 0x0012C758 File Offset: 0x0012A958
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			char c;
			Exception ex = XmlConvert.TryToChar(s, out c);
			if (ex == null)
			{
				typedValue = c;
				return null;
			}
			return ex;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_char()
		{
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0012C781 File Offset: 0x0012A981
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_char()
		{
		}

		// Token: 0x040027C4 RID: 10180
		private static readonly Type atomicValueType = typeof(char);

		// Token: 0x040027C5 RID: 10181
		private static readonly Type listValueType = typeof(char[]);
	}
}
