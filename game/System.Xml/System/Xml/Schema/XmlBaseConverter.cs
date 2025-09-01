using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020005F0 RID: 1520
	internal abstract class XmlBaseConverter : XmlValueConverter
	{
		// Token: 0x06003D63 RID: 15715 RVA: 0x001555C8 File Offset: 0x001537C8
		protected XmlBaseConverter(XmlSchemaType schemaType)
		{
			XmlSchemaDatatype datatype = schemaType.Datatype;
			while (schemaType != null && !(schemaType is XmlSchemaSimpleType))
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			if (schemaType == null)
			{
				schemaType = XmlSchemaType.GetBuiltInSimpleType(datatype.TypeCode);
			}
			this.schemaType = schemaType;
			this.typeCode = schemaType.TypeCode;
			this.clrTypeDefault = schemaType.Datatype.ValueType;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0015562C File Offset: 0x0015382C
		protected XmlBaseConverter(XmlTypeCode typeCode)
		{
			if (typeCode != XmlTypeCode.Item)
			{
				if (typeCode != XmlTypeCode.Node)
				{
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						this.clrTypeDefault = XmlBaseConverter.XmlAtomicValueType;
					}
				}
				else
				{
					this.clrTypeDefault = XmlBaseConverter.XPathNavigatorType;
				}
			}
			else
			{
				this.clrTypeDefault = XmlBaseConverter.XPathItemType;
			}
			this.typeCode = typeCode;
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0015567A File Offset: 0x0015387A
		protected XmlBaseConverter(XmlBaseConverter converterAtomic)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = Array.CreateInstance(converterAtomic.DefaultClrType, 0).GetType();
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x001556B1 File Offset: 0x001538B1
		protected XmlBaseConverter(XmlBaseConverter converterAtomic, Type clrTypeDefault)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = clrTypeDefault;
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x001556D8 File Offset: 0x001538D8
		public override bool ToBoolean(bool value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x001556F1 File Offset: 0x001538F1
		public override bool ToBoolean(DateTime value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x0015570A File Offset: 0x0015390A
		public override bool ToBoolean(DateTimeOffset value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x00155723 File Offset: 0x00153923
		public override bool ToBoolean(decimal value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0015573C File Offset: 0x0015393C
		public override bool ToBoolean(double value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x00155755 File Offset: 0x00153955
		public override bool ToBoolean(int value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x0015576E File Offset: 0x0015396E
		public override bool ToBoolean(long value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00155787 File Offset: 0x00153987
		public override bool ToBoolean(float value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x001557A0 File Offset: 0x001539A0
		public override bool ToBoolean(string value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x001557A0 File Offset: 0x001539A0
		public override bool ToBoolean(object value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x001557B4 File Offset: 0x001539B4
		public override DateTime ToDateTime(bool value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x001557CD File Offset: 0x001539CD
		public override DateTime ToDateTime(DateTime value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x001557E6 File Offset: 0x001539E6
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x001557FF File Offset: 0x001539FF
		public override DateTime ToDateTime(decimal value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00155818 File Offset: 0x00153A18
		public override DateTime ToDateTime(double value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00155831 File Offset: 0x00153A31
		public override DateTime ToDateTime(int value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x0015584A File Offset: 0x00153A4A
		public override DateTime ToDateTime(long value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x00155863 File Offset: 0x00153A63
		public override DateTime ToDateTime(float value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x0015587C File Offset: 0x00153A7C
		public override DateTime ToDateTime(string value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x0015587C File Offset: 0x00153A7C
		public override DateTime ToDateTime(object value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x00155890 File Offset: 0x00153A90
		public override DateTimeOffset ToDateTimeOffset(bool value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x001558A9 File Offset: 0x00153AA9
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x001558C2 File Offset: 0x00153AC2
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x001558DB File Offset: 0x00153ADB
		public override DateTimeOffset ToDateTimeOffset(decimal value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x001558F4 File Offset: 0x00153AF4
		public override DateTimeOffset ToDateTimeOffset(double value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x0015590D File Offset: 0x00153B0D
		public override DateTimeOffset ToDateTimeOffset(int value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x00155926 File Offset: 0x00153B26
		public override DateTimeOffset ToDateTimeOffset(long value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x0015593F File Offset: 0x00153B3F
		public override DateTimeOffset ToDateTimeOffset(float value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x00155958 File Offset: 0x00153B58
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x00155958 File Offset: 0x00153B58
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x0015596C File Offset: 0x00153B6C
		public override decimal ToDecimal(bool value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x00155985 File Offset: 0x00153B85
		public override decimal ToDecimal(DateTime value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0015599E File Offset: 0x00153B9E
		public override decimal ToDecimal(DateTimeOffset value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x001559B7 File Offset: 0x00153BB7
		public override decimal ToDecimal(decimal value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x001559D0 File Offset: 0x00153BD0
		public override decimal ToDecimal(double value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x001559E9 File Offset: 0x00153BE9
		public override decimal ToDecimal(int value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x00155A02 File Offset: 0x00153C02
		public override decimal ToDecimal(long value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x00155A1B File Offset: 0x00153C1B
		public override decimal ToDecimal(float value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x00155A34 File Offset: 0x00153C34
		public override decimal ToDecimal(string value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x00155A34 File Offset: 0x00153C34
		public override decimal ToDecimal(object value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00155A48 File Offset: 0x00153C48
		public override double ToDouble(bool value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x00155A61 File Offset: 0x00153C61
		public override double ToDouble(DateTime value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x00155A7A File Offset: 0x00153C7A
		public override double ToDouble(DateTimeOffset value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x00155A93 File Offset: 0x00153C93
		public override double ToDouble(decimal value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x00155AAC File Offset: 0x00153CAC
		public override double ToDouble(double value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x00155AC5 File Offset: 0x00153CC5
		public override double ToDouble(int value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x00155ADE File Offset: 0x00153CDE
		public override double ToDouble(long value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00155AF7 File Offset: 0x00153CF7
		public override double ToDouble(float value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00155B10 File Offset: 0x00153D10
		public override double ToDouble(string value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00155B10 File Offset: 0x00153D10
		public override double ToDouble(object value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x00155B24 File Offset: 0x00153D24
		public override int ToInt32(bool value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x00155B3D File Offset: 0x00153D3D
		public override int ToInt32(DateTime value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00155B56 File Offset: 0x00153D56
		public override int ToInt32(DateTimeOffset value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00155B6F File Offset: 0x00153D6F
		public override int ToInt32(decimal value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00155B88 File Offset: 0x00153D88
		public override int ToInt32(double value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00155BA1 File Offset: 0x00153DA1
		public override int ToInt32(int value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00155BBA File Offset: 0x00153DBA
		public override int ToInt32(long value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x00155BD3 File Offset: 0x00153DD3
		public override int ToInt32(float value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00155BEC File Offset: 0x00153DEC
		public override int ToInt32(string value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x00155BEC File Offset: 0x00153DEC
		public override int ToInt32(object value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00155C00 File Offset: 0x00153E00
		public override long ToInt64(bool value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00155C19 File Offset: 0x00153E19
		public override long ToInt64(DateTime value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00155C32 File Offset: 0x00153E32
		public override long ToInt64(DateTimeOffset value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x00155C4B File Offset: 0x00153E4B
		public override long ToInt64(decimal value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00155C64 File Offset: 0x00153E64
		public override long ToInt64(double value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00155C7D File Offset: 0x00153E7D
		public override long ToInt64(int value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00155C96 File Offset: 0x00153E96
		public override long ToInt64(long value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00155CAF File Offset: 0x00153EAF
		public override long ToInt64(float value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00155CC8 File Offset: 0x00153EC8
		public override long ToInt64(string value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00155CC8 File Offset: 0x00153EC8
		public override long ToInt64(object value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00155CDC File Offset: 0x00153EDC
		public override float ToSingle(bool value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00155CF5 File Offset: 0x00153EF5
		public override float ToSingle(DateTime value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x00155D0E File Offset: 0x00153F0E
		public override float ToSingle(DateTimeOffset value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x00155D27 File Offset: 0x00153F27
		public override float ToSingle(decimal value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x00155D40 File Offset: 0x00153F40
		public override float ToSingle(double value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00155D59 File Offset: 0x00153F59
		public override float ToSingle(int value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x00155D72 File Offset: 0x00153F72
		public override float ToSingle(long value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00155D8B File Offset: 0x00153F8B
		public override float ToSingle(float value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x00155DA4 File Offset: 0x00153FA4
		public override float ToSingle(string value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x00155DA4 File Offset: 0x00153FA4
		public override float ToSingle(object value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x00155DB8 File Offset: 0x00153FB8
		public override string ToString(bool value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x00155DD1 File Offset: 0x00153FD1
		public override string ToString(DateTime value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x00155DEA File Offset: 0x00153FEA
		public override string ToString(DateTimeOffset value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x00155E03 File Offset: 0x00154003
		public override string ToString(decimal value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00155E1C File Offset: 0x0015401C
		public override string ToString(double value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00155E35 File Offset: 0x00154035
		public override string ToString(int value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00155E4E File Offset: 0x0015404E
		public override string ToString(long value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x00155E67 File Offset: 0x00154067
		public override string ToString(float value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00155E80 File Offset: 0x00154080
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00155E80 File Offset: 0x00154080
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00155E94 File Offset: 0x00154094
		public override string ToString(string value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00155E9E File Offset: 0x0015409E
		public override string ToString(object value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00155EA8 File Offset: 0x001540A8
		public override object ChangeType(bool value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x00155EB8 File Offset: 0x001540B8
		public override object ChangeType(DateTime value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00155EC8 File Offset: 0x001540C8
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x00155ED8 File Offset: 0x001540D8
		public override object ChangeType(decimal value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x00155EE8 File Offset: 0x001540E8
		public override object ChangeType(double value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x00155EF8 File Offset: 0x001540F8
		public override object ChangeType(int value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x00155F08 File Offset: 0x00154108
		public override object ChangeType(long value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x00155F18 File Offset: 0x00154118
		public override object ChangeType(float value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00155F28 File Offset: 0x00154128
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			return this.ChangeType(value, destinationType, nsResolver);
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x00155F33 File Offset: 0x00154133
		public override object ChangeType(string value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00155F3E File Offset: 0x0015413E
		public override object ChangeType(object value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x00155F49 File Offset: 0x00154149
		protected XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x00155F51 File Offset: 0x00154151
		protected XmlTypeCode TypeCode
		{
			get
			{
				return this.typeCode;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x00155F5C File Offset: 0x0015415C
		protected string XmlTypeName
		{
			get
			{
				XmlSchemaType baseXmlSchemaType = this.schemaType;
				if (baseXmlSchemaType != null)
				{
					while (baseXmlSchemaType.QualifiedName.IsEmpty)
					{
						baseXmlSchemaType = baseXmlSchemaType.BaseXmlSchemaType;
					}
					return XmlBaseConverter.QNameToString(baseXmlSchemaType.QualifiedName);
				}
				if (this.typeCode == XmlTypeCode.Node)
				{
					return "node";
				}
				if (this.typeCode == XmlTypeCode.AnyAtomicType)
				{
					return "xdt:anyAtomicType";
				}
				return "item";
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x00155FB9 File Offset: 0x001541B9
		protected Type DefaultClrType
		{
			get
			{
				return this.clrTypeDefault;
			}
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00155FC1 File Offset: 0x001541C1
		protected static bool IsDerivedFrom(Type derivedType, Type baseType)
		{
			while (derivedType != null)
			{
				if (derivedType == baseType)
				{
					return true;
				}
				derivedType = derivedType.BaseType;
			}
			return false;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00155FE4 File Offset: 0x001541E4
		protected Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type '{0}' does not support Clr type '{1}'.", new object[]
				{
					this.XmlTypeName,
					sourceType.Name
				}));
			}
			return new InvalidCastException(Res.GetString("Xml type '{0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[]
			{
				this.XmlTypeName,
				sourceType.Name,
				destinationType.Name
			}));
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x00156054 File Offset: 0x00154254
		protected static string QNameToString(XmlQualifiedName name)
		{
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x001560D6 File Offset: 0x001542D6
		protected virtual object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			throw this.CreateInvalidClrMappingException(value.GetType(), destinationType);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x001560E5 File Offset: 0x001542E5
		protected static byte[] StringToBase64Binary(string value)
		{
			return Convert.FromBase64String(XmlConvert.TrimString(value));
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x001560F2 File Offset: 0x001542F2
		protected static DateTime StringToDate(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x00156100 File Offset: 0x00154300
		protected static DateTime StringToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00156110 File Offset: 0x00154310
		protected static TimeSpan StringToDayTimeDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToTimeSpan(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x00156130 File Offset: 0x00154330
		protected static TimeSpan StringToDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x0015614D File Offset: 0x0015434D
		protected static DateTime StringToGDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0015615C File Offset: 0x0015435C
		protected static DateTime StringToGMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0015616E File Offset: 0x0015436E
		protected static DateTime StringToGMonthDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0015617D File Offset: 0x0015437D
		protected static DateTime StringToGYear(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x0015618C File Offset: 0x0015438C
		protected static DateTime StringToGYearMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0015619A File Offset: 0x0015439A
		protected static DateTimeOffset StringToDateOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x001561A8 File Offset: 0x001543A8
		protected static DateTimeOffset StringToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x001561B6 File Offset: 0x001543B6
		protected static DateTimeOffset StringToGDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x001561C5 File Offset: 0x001543C5
		protected static DateTimeOffset StringToGMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x001561D7 File Offset: 0x001543D7
		protected static DateTimeOffset StringToGMonthDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x001561E6 File Offset: 0x001543E6
		protected static DateTimeOffset StringToGYearOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x001561F5 File Offset: 0x001543F5
		protected static DateTimeOffset StringToGYearMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00156204 File Offset: 0x00154404
		protected static byte[] StringToHexBinary(string value)
		{
			byte[] result;
			try
			{
				result = XmlConvert.FromBinHexString(XmlConvert.TrimString(value), false);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			return result;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0015623C File Offset: 0x0015443C
		protected static XmlQualifiedName StringToQName(string value, IXmlNamespaceResolver nsResolver)
		{
			value = value.Trim();
			string text;
			string name;
			try
			{
				ValidateNames.ParseQNameThrow(value, out text, out name);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			if (nsResolver == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[]
				{
					value,
					text
				}));
			}
			string text2 = nsResolver.LookupNamespace(text);
			if (text2 == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[]
				{
					value,
					text
				}));
			}
			return new XmlQualifiedName(name, text2);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x001562CC File Offset: 0x001544CC
		protected static DateTime StringToTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x001562DA File Offset: 0x001544DA
		protected static DateTimeOffset StringToTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x001562E8 File Offset: 0x001544E8
		protected static TimeSpan StringToYearMonthDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToTimeSpan(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00156305 File Offset: 0x00154505
		protected static string AnyUriToString(Uri value)
		{
			return value.OriginalString;
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x0015630D File Offset: 0x0015450D
		protected static string Base64BinaryToString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00156318 File Offset: 0x00154518
		protected static string DateToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0015633C File Offset: 0x0015453C
		protected static string DateTimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00156360 File Offset: 0x00154560
		protected static string DayTimeDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToString(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00156380 File Offset: 0x00154580
		protected static string DurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x001563A0 File Offset: 0x001545A0
		protected static string GDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x001563C4 File Offset: 0x001545C4
		protected static string GMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x001563EC File Offset: 0x001545EC
		protected static string GMonthDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x00156410 File Offset: 0x00154610
		protected static string GYearToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00156434 File Offset: 0x00154634
		protected static string GYearMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00156458 File Offset: 0x00154658
		protected static string DateOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x0015647C File Offset: 0x0015467C
		protected static string DateTimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x001564A0 File Offset: 0x001546A0
		protected static string GDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x001564C4 File Offset: 0x001546C4
		protected static string GMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x001564EC File Offset: 0x001546EC
		protected static string GMonthDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00156510 File Offset: 0x00154710
		protected static string GYearOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00156534 File Offset: 0x00154734
		protected static string GYearMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00156558 File Offset: 0x00154758
		protected static string QNameToString(XmlQualifiedName qname, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				return "{" + qname.Namespace + "}" + qname.Name;
			}
			string text = nsResolver.LookupPrefix(qname.Namespace);
			if (text == null)
			{
				throw new InvalidCastException(Res.GetString("The QName '{0}' cannot be represented as a String.  A prefix for namespace '{1}' cannot be found.", new object[]
				{
					qname.ToString(),
					qname.Namespace
				}));
			}
			if (text.Length == 0)
			{
				return qname.Name;
			}
			return text + ":" + qname.Name;
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x001565DC File Offset: 0x001547DC
		protected static string TimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00156600 File Offset: 0x00154800
		protected static string TimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00156624 File Offset: 0x00154824
		protected static string YearMonthDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToString(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00156641 File Offset: 0x00154841
		internal static DateTime DateTimeOffsetToDateTime(DateTimeOffset value)
		{
			return value.LocalDateTime;
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0015664C File Offset: 0x0015484C
		internal static int DecimalToInt32(decimal value)
		{
			if (value < -2147483648m || value > 2147483647m)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (int)value;
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x001566AC File Offset: 0x001548AC
		protected static long DecimalToInt64(decimal value)
		{
			if (value < -9223372036854775808m || value > 9223372036854775807m)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int64"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (long)value;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00156714 File Offset: 0x00154914
		protected static ulong DecimalToUInt64(decimal value)
		{
			if (value < 0m || value > 18446744073709551615m)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt64"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (ulong)value;
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x0015676C File Offset: 0x0015496C
		protected static byte Int32ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Byte"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (byte)value;
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x001567B0 File Offset: 0x001549B0
		protected static short Int32ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int16"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (short)value;
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x001567F8 File Offset: 0x001549F8
		protected static sbyte Int32ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"SByte"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (sbyte)value;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x0015683C File Offset: 0x00154A3C
		protected static ushort Int32ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt16"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (ushort)value;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00156880 File Offset: 0x00154A80
		protected static int Int64ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (int)value;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x001568CC File Offset: 0x00154ACC
		protected static uint Int64ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)-1))
			{
				string name = "Value '{0}' was either too large or too small for {1}.";
				object[] args = new string[]
				{
					XmlConvert.ToString(value),
					"UInt32"
				};
				throw new OverflowException(Res.GetString(name, args));
			}
			return (uint)value;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x0010A577 File Offset: 0x00108777
		protected static DateTime UntypedAtomicToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0015690E File Offset: 0x00154B0E
		protected static DateTimeOffset UntypedAtomicToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00156920 File Offset: 0x00154B20
		// Note: this type is marked as 'beforefieldinit'.
		static XmlBaseConverter()
		{
		}

		// Token: 0x04002C6D RID: 11373
		private XmlSchemaType schemaType;

		// Token: 0x04002C6E RID: 11374
		private XmlTypeCode typeCode;

		// Token: 0x04002C6F RID: 11375
		private Type clrTypeDefault;

		// Token: 0x04002C70 RID: 11376
		protected static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x04002C71 RID: 11377
		protected static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x04002C72 RID: 11378
		protected static readonly Type IListType = typeof(IList);

		// Token: 0x04002C73 RID: 11379
		protected static readonly Type ObjectArrayType = typeof(object[]);

		// Token: 0x04002C74 RID: 11380
		protected static readonly Type StringArrayType = typeof(string[]);

		// Token: 0x04002C75 RID: 11381
		protected static readonly Type XmlAtomicValueArrayType = typeof(XmlAtomicValue[]);

		// Token: 0x04002C76 RID: 11382
		protected static readonly Type DecimalType = typeof(decimal);

		// Token: 0x04002C77 RID: 11383
		protected static readonly Type Int32Type = typeof(int);

		// Token: 0x04002C78 RID: 11384
		protected static readonly Type Int64Type = typeof(long);

		// Token: 0x04002C79 RID: 11385
		protected static readonly Type StringType = typeof(string);

		// Token: 0x04002C7A RID: 11386
		protected static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x04002C7B RID: 11387
		protected static readonly Type ObjectType = typeof(object);

		// Token: 0x04002C7C RID: 11388
		protected static readonly Type ByteType = typeof(byte);

		// Token: 0x04002C7D RID: 11389
		protected static readonly Type Int16Type = typeof(short);

		// Token: 0x04002C7E RID: 11390
		protected static readonly Type SByteType = typeof(sbyte);

		// Token: 0x04002C7F RID: 11391
		protected static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x04002C80 RID: 11392
		protected static readonly Type UInt32Type = typeof(uint);

		// Token: 0x04002C81 RID: 11393
		protected static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x04002C82 RID: 11394
		protected static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x04002C83 RID: 11395
		protected static readonly Type DoubleType = typeof(double);

		// Token: 0x04002C84 RID: 11396
		protected static readonly Type SingleType = typeof(float);

		// Token: 0x04002C85 RID: 11397
		protected static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x04002C86 RID: 11398
		protected static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

		// Token: 0x04002C87 RID: 11399
		protected static readonly Type BooleanType = typeof(bool);

		// Token: 0x04002C88 RID: 11400
		protected static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x04002C89 RID: 11401
		protected static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x04002C8A RID: 11402
		protected static readonly Type UriType = typeof(Uri);

		// Token: 0x04002C8B RID: 11403
		protected static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x04002C8C RID: 11404
		protected static readonly Type XPathNavigatorType = typeof(XPathNavigator);
	}
}
