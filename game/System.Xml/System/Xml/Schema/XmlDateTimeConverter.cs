using System;

namespace System.Xml.Schema
{
	// Token: 0x020005F3 RID: 1523
	internal class XmlDateTimeConverter : XmlBaseConverter
	{
		// Token: 0x06003E3E RID: 15934 RVA: 0x00156AE0 File Offset: 0x00154CE0
		protected XmlDateTimeConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00157D24 File Offset: 0x00155F24
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlDateTimeConverter(schemaType);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0000206B File Offset: 0x0000026B
		public override DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00157D2C File Offset: 0x00155F2C
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToDateTime(value);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x00157D34 File Offset: 0x00155F34
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTime(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDate(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonth(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYear(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDay(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDay(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonth(value);
			default:
				return XmlBaseConverter.StringToDateTime(value);
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00157DB4 File Offset: 0x00155FB4
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return (DateTime)value;
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTime((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTime((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTime)this.ChangeListType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x00157E49 File Offset: 0x00156049
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return new DateTimeOffset(value);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0000206B File Offset: 0x0000026B
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x00157E54 File Offset: 0x00156054
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTimeOffset(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDateOffset(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonthOffset(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYearOffset(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDayOffset(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDayOffset(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonthOffset(value);
			default:
				return XmlBaseConverter.StringToDateTimeOffset(value);
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00157ED4 File Offset: 0x001560D4
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTimeOffset((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return (DateTimeOffset)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTimeOffset((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTimeOffset)this.ChangeListType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00157F70 File Offset: 0x00156170
		public override string ToString(DateTime value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthToString(value);
			default:
				return XmlBaseConverter.DateTimeToString(value);
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00157FE4 File Offset: 0x001561E4
		public override string ToString(DateTimeOffset value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeOffsetToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateOffsetToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthOffsetToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearOffsetToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayOffsetToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayOffsetToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthOffsetToString(value);
			default:
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00156DDB File Offset: 0x00154FDB
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00158058 File Offset: 0x00156258
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeListType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001580F0 File Offset: 0x001562F0
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
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x001581AC File Offset: 0x001563AC
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
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00158274 File Offset: 0x00156474
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
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00158338 File Offset: 0x00156538
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
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
