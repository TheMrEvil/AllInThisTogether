using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020005F5 RID: 1525
	internal class XmlMiscConverter : XmlBaseConverter
	{
		// Token: 0x06003E5B RID: 15963 RVA: 0x00156AE0 File Offset: 0x00154CE0
		protected XmlMiscConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x0015888A File Offset: 0x00156A8A
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlMiscConverter(schemaType);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00156DDB File Offset: 0x00154FDB
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00158894 File Offset: 0x00156A94
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.HexBinary)
				{
					return XmlConvert.ToBinHexString((byte[])value);
				}
				if (typeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.Base64BinaryToString((byte[])value);
				}
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.DurationToString((TimeSpan)value);
				}
				if (typeCode == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.YearMonthDurationToString((TimeSpan)value);
				}
				if (typeCode == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.DayTimeDurationToString((TimeSpan)value);
				}
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.QName)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
				if (typeCode == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x001589B4 File Offset: 0x00156BB4
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.HexBinary)
				{
					return XmlBaseConverter.StringToHexBinary(value);
				}
				if (typeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.StringToBase64Binary(value);
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.QName)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
				if (typeCode == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode typeCode = base.TypeCode;
				if (typeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.StringToDuration(value);
				}
				if (typeCode == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.StringToYearMonthDuration(value);
				}
				if (typeCode == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.StringToDayTimeDuration(value);
				}
			}
			if (destinationType == XmlBaseConverter.UriType && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value, nsResolver);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x00158AEC File Offset: 0x00156CEC
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.HexBinary)
					{
						return (byte[])value;
					}
					if (typeCode == XmlTypeCode.Base64Binary)
					{
						return (byte[])value;
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.HexBinary)
					{
						return XmlBaseConverter.StringToHexBinary((string)value);
					}
					if (typeCode == XmlTypeCode.Base64Binary)
					{
						return XmlBaseConverter.StringToBase64Binary((string)value);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.QName)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
					if (typeCode == XmlTypeCode.Notation)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.QName)
					{
						return (XmlQualifiedName)value;
					}
					if (typeCode == XmlTypeCode.Notation)
					{
						return (XmlQualifiedName)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.Duration)
					{
						return XmlBaseConverter.StringToDuration((string)value);
					}
					if (typeCode == XmlTypeCode.YearMonthDuration)
					{
						return XmlBaseConverter.StringToYearMonthDuration((string)value);
					}
					if (typeCode == XmlTypeCode.DayTimeDuration)
					{
						return XmlBaseConverter.StringToDayTimeDuration((string)value);
					}
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.Duration)
					{
						return (TimeSpan)value;
					}
					if (typeCode == XmlTypeCode.YearMonthDuration)
					{
						return (TimeSpan)value;
					}
					if (typeCode == XmlTypeCode.DayTimeDuration)
					{
						return (TimeSpan)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				if (type == XmlBaseConverter.StringType && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return XmlConvert.ToUri((string)value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return (Uri)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.HexBinary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode == XmlTypeCode.Base64Binary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value, nsResolver);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.Duration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode == XmlTypeCode.YearMonthDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (typeCode == XmlTypeCode.DayTimeDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode typeCode = base.TypeCode;
					if (typeCode == XmlTypeCode.QName)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
					if (typeCode == XmlTypeCode.Notation)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (XmlAtomicValue)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x00158EB1 File Offset: 0x001570B1
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00158EDC File Offset: 0x001570DC
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
