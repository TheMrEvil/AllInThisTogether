using System;

namespace System.Xml.Schema
{
	// Token: 0x0200054E RID: 1358
	internal class Datatype_uuid : Datatype_anySimpleType
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x0012C85F File Offset: 0x0012AA5F
		public override Type ValueType
		{
			get
			{
				return Datatype_uuid.atomicValueType;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x0012C866 File Offset: 0x0012AA66
		internal override Type ListValueType
		{
			get
			{
				return Datatype_uuid.listValueType;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0012C870 File Offset: 0x0012AA70
		internal override int Compare(object value1, object value2)
		{
			if (!((Guid)value1).Equals(value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0012C898 File Offset: 0x0012AA98
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object result;
			try
			{
				result = XmlConvert.ToGuid(s);
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

		// Token: 0x0600361C RID: 13852 RVA: 0x0012C8F0 File Offset: 0x0012AAF0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Guid guid;
			Exception ex = XmlConvert.TryToGuid(s, out guid);
			if (ex == null)
			{
				typedValue = guid;
				return null;
			}
			return ex;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_uuid()
		{
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0012C919 File Offset: 0x0012AB19
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_uuid()
		{
		}

		// Token: 0x040027C6 RID: 10182
		private static readonly Type atomicValueType = typeof(Guid);

		// Token: 0x040027C7 RID: 10183
		private static readonly Type listValueType = typeof(Guid[]);
	}
}
