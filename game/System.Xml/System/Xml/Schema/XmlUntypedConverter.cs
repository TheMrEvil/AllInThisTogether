using System;

namespace System.Xml.Schema
{
	// Token: 0x020005F7 RID: 1527
	internal class XmlUntypedConverter : XmlListConverter
	{
		// Token: 0x06003E69 RID: 15977 RVA: 0x001590EE File Offset: 0x001572EE
		protected XmlUntypedConverter() : base(DatatypeImplementation.UntypedAtomicType)
		{
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x001590FB File Offset: 0x001572FB
		protected XmlUntypedConverter(XmlUntypedConverter atomicConverter, bool allowListToList) : base(atomicConverter, allowListToList ? XmlBaseConverter.StringArrayType : XmlBaseConverter.StringType)
		{
			this.allowListToList = allowListToList;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x001584E4 File Offset: 0x001566E4
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0015911A File Offset: 0x0015731A
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			return (bool)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x0015915A File Offset: 0x0015735A
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTime(value);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00159170 File Offset: 0x00157370
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			return (DateTime)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x001591B0 File Offset: 0x001573B0
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x001591C6 File Offset: 0x001573C6
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			return (DateTimeOffset)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00159206 File Offset: 0x00157406
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0015921C File Offset: 0x0015741C
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0015925C File Offset: 0x0015745C
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00159272 File Offset: 0x00157472
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			return (double)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x001592B2 File Offset: 0x001574B2
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x001592C8 File Offset: 0x001574C8
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00159308 File Offset: 0x00157508
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0015931E File Offset: 0x0015751E
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0015935E File Offset: 0x0015755E
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToSingle(value);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00159374 File Offset: 0x00157574
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			return (float)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00158576 File Offset: 0x00156776
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x001593B4 File Offset: 0x001575B4
		public override string ToString(DateTime value)
		{
			return XmlBaseConverter.DateTimeToString(value);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x001593BC File Offset: 0x001575BC
		public override string ToString(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToString(value);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x001593C4 File Offset: 0x001575C4
		public override string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x001593CC File Offset: 0x001575CC
		public override string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00156DCB File Offset: 0x00154FCB
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x00156DD3 File Offset: 0x00154FD3
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x001593D5 File Offset: 0x001575D5
		public override string ToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00156DDB File Offset: 0x00154FDB
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x001593E0 File Offset: 0x001575E0
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToString((bool)value);
			}
			if (type == XmlBaseConverter.ByteType)
			{
				return XmlConvert.ToString((byte)value);
			}
			if (type == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.Base64BinaryToString((byte[])value);
			}
			if (type == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.DateTimeToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.DateTimeOffsetToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToString((decimal)value);
			}
			if (type == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToString((double)value);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return XmlConvert.ToString((short)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToString((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToString((long)value);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return XmlConvert.ToString((sbyte)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToString((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.DurationToString((TimeSpan)value);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return XmlConvert.ToString((ushort)value);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return XmlConvert.ToString((uint)value);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return XmlConvert.ToString((ulong)value);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType))
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (string)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.StringType, nsResolver);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00159610 File Offset: 0x00157810
		public override object ChangeType(bool value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00159668 File Offset: 0x00157868
		public override object ChangeType(DateTime value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x001596C0 File Offset: 0x001578C0
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00159718 File Offset: 0x00157918
		public override object ChangeType(decimal value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00159770 File Offset: 0x00157970
		public override object ChangeType(double value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x001597CC File Offset: 0x001579CC
		public override object ChangeType(int value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00159824 File Offset: 0x00157A24
		public override object ChangeType(long value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x0015987C File Offset: 0x00157A7C
		public override object ChangeType(float value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x001598D8 File Offset: 0x00157AD8
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.StringToBase64Binary(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.StringToDuration(value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal(value));
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				return XmlBaseConverter.StringToQName(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x00159B3C File Offset: 0x00157D3C
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
			if (destinationType == XmlBaseConverter.BooleanType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			if (destinationType == XmlBaseConverter.ByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToBase64Binary((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			if (destinationType == XmlBaseConverter.DecimalType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			if (destinationType == XmlBaseConverter.DoubleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			if (destinationType == XmlBaseConverter.Int16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.Int32Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			if (destinationType == XmlBaseConverter.Int64Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			if (destinationType == XmlBaseConverter.SByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.SingleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToDuration((string)value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal((string)value));
			}
			if (destinationType == XmlBaseConverter.UriType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToUri((string)value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToQName((string)value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x00158EB1 File Offset: 0x001570B1
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x00159F9C File Offset: 0x0015819C
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00159FF4 File Offset: 0x001581F4
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (this.atomicConverter != null && (this.allowListToList || !(type != XmlBaseConverter.StringType) || !(destinationType != XmlBaseConverter.StringType)))
			{
				return base.ChangeListType(value, destinationType, nsResolver);
			}
			if (this.SupportsType(type))
			{
				throw new InvalidCastException(Res.GetString("Xml type '{0}' cannot convert from Clr type '{1}' unless the destination type is String or XmlAtomicValue.", new object[]
				{
					base.XmlTypeName,
					type.Name
				}));
			}
			if (this.SupportsType(destinationType))
			{
				throw new InvalidCastException(Res.GetString("Xml type '{0}' cannot convert to Clr type '{1}' unless the source value is a String or an XmlAtomicValue.", new object[]
				{
					base.XmlTypeName,
					destinationType.Name
				}));
			}
			throw base.CreateInvalidClrMappingException(type, destinationType);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x0015A0AC File Offset: 0x001582AC
		private bool SupportsType(Type clrType)
		{
			return clrType == XmlBaseConverter.BooleanType || clrType == XmlBaseConverter.ByteType || clrType == XmlBaseConverter.ByteArrayType || clrType == XmlBaseConverter.DateTimeType || clrType == XmlBaseConverter.DateTimeOffsetType || clrType == XmlBaseConverter.DecimalType || clrType == XmlBaseConverter.DoubleType || clrType == XmlBaseConverter.Int16Type || clrType == XmlBaseConverter.Int32Type || clrType == XmlBaseConverter.Int64Type || clrType == XmlBaseConverter.SByteType || clrType == XmlBaseConverter.SingleType || clrType == XmlBaseConverter.TimeSpanType || clrType == XmlBaseConverter.UInt16Type || clrType == XmlBaseConverter.UInt32Type || clrType == XmlBaseConverter.UInt64Type || clrType == XmlBaseConverter.UriType || clrType == XmlBaseConverter.XmlQualifiedNameType;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0015A1C8 File Offset: 0x001583C8
		// Note: this type is marked as 'beforefieldinit'.
		static XmlUntypedConverter()
		{
		}

		// Token: 0x04002C8D RID: 11405
		private bool allowListToList;

		// Token: 0x04002C8E RID: 11406
		public static readonly XmlValueConverter Untyped = new XmlUntypedConverter(new XmlUntypedConverter(), false);

		// Token: 0x04002C8F RID: 11407
		public static readonly XmlValueConverter UntypedList = new XmlUntypedConverter(new XmlUntypedConverter(), true);
	}
}
