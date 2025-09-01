using System;

namespace System.Xml.Schema
{
	// Token: 0x020005EF RID: 1519
	internal abstract class XmlValueConverter
	{
		// Token: 0x06003CFA RID: 15610
		public abstract bool ToBoolean(bool value);

		// Token: 0x06003CFB RID: 15611
		public abstract bool ToBoolean(long value);

		// Token: 0x06003CFC RID: 15612
		public abstract bool ToBoolean(int value);

		// Token: 0x06003CFD RID: 15613
		public abstract bool ToBoolean(decimal value);

		// Token: 0x06003CFE RID: 15614
		public abstract bool ToBoolean(float value);

		// Token: 0x06003CFF RID: 15615
		public abstract bool ToBoolean(double value);

		// Token: 0x06003D00 RID: 15616
		public abstract bool ToBoolean(DateTime value);

		// Token: 0x06003D01 RID: 15617
		public abstract bool ToBoolean(DateTimeOffset value);

		// Token: 0x06003D02 RID: 15618
		public abstract bool ToBoolean(string value);

		// Token: 0x06003D03 RID: 15619
		public abstract bool ToBoolean(object value);

		// Token: 0x06003D04 RID: 15620
		public abstract int ToInt32(bool value);

		// Token: 0x06003D05 RID: 15621
		public abstract int ToInt32(int value);

		// Token: 0x06003D06 RID: 15622
		public abstract int ToInt32(long value);

		// Token: 0x06003D07 RID: 15623
		public abstract int ToInt32(decimal value);

		// Token: 0x06003D08 RID: 15624
		public abstract int ToInt32(float value);

		// Token: 0x06003D09 RID: 15625
		public abstract int ToInt32(double value);

		// Token: 0x06003D0A RID: 15626
		public abstract int ToInt32(DateTime value);

		// Token: 0x06003D0B RID: 15627
		public abstract int ToInt32(DateTimeOffset value);

		// Token: 0x06003D0C RID: 15628
		public abstract int ToInt32(string value);

		// Token: 0x06003D0D RID: 15629
		public abstract int ToInt32(object value);

		// Token: 0x06003D0E RID: 15630
		public abstract long ToInt64(bool value);

		// Token: 0x06003D0F RID: 15631
		public abstract long ToInt64(int value);

		// Token: 0x06003D10 RID: 15632
		public abstract long ToInt64(long value);

		// Token: 0x06003D11 RID: 15633
		public abstract long ToInt64(decimal value);

		// Token: 0x06003D12 RID: 15634
		public abstract long ToInt64(float value);

		// Token: 0x06003D13 RID: 15635
		public abstract long ToInt64(double value);

		// Token: 0x06003D14 RID: 15636
		public abstract long ToInt64(DateTime value);

		// Token: 0x06003D15 RID: 15637
		public abstract long ToInt64(DateTimeOffset value);

		// Token: 0x06003D16 RID: 15638
		public abstract long ToInt64(string value);

		// Token: 0x06003D17 RID: 15639
		public abstract long ToInt64(object value);

		// Token: 0x06003D18 RID: 15640
		public abstract decimal ToDecimal(bool value);

		// Token: 0x06003D19 RID: 15641
		public abstract decimal ToDecimal(int value);

		// Token: 0x06003D1A RID: 15642
		public abstract decimal ToDecimal(long value);

		// Token: 0x06003D1B RID: 15643
		public abstract decimal ToDecimal(decimal value);

		// Token: 0x06003D1C RID: 15644
		public abstract decimal ToDecimal(float value);

		// Token: 0x06003D1D RID: 15645
		public abstract decimal ToDecimal(double value);

		// Token: 0x06003D1E RID: 15646
		public abstract decimal ToDecimal(DateTime value);

		// Token: 0x06003D1F RID: 15647
		public abstract decimal ToDecimal(DateTimeOffset value);

		// Token: 0x06003D20 RID: 15648
		public abstract decimal ToDecimal(string value);

		// Token: 0x06003D21 RID: 15649
		public abstract decimal ToDecimal(object value);

		// Token: 0x06003D22 RID: 15650
		public abstract double ToDouble(bool value);

		// Token: 0x06003D23 RID: 15651
		public abstract double ToDouble(int value);

		// Token: 0x06003D24 RID: 15652
		public abstract double ToDouble(long value);

		// Token: 0x06003D25 RID: 15653
		public abstract double ToDouble(decimal value);

		// Token: 0x06003D26 RID: 15654
		public abstract double ToDouble(float value);

		// Token: 0x06003D27 RID: 15655
		public abstract double ToDouble(double value);

		// Token: 0x06003D28 RID: 15656
		public abstract double ToDouble(DateTime value);

		// Token: 0x06003D29 RID: 15657
		public abstract double ToDouble(DateTimeOffset value);

		// Token: 0x06003D2A RID: 15658
		public abstract double ToDouble(string value);

		// Token: 0x06003D2B RID: 15659
		public abstract double ToDouble(object value);

		// Token: 0x06003D2C RID: 15660
		public abstract float ToSingle(bool value);

		// Token: 0x06003D2D RID: 15661
		public abstract float ToSingle(int value);

		// Token: 0x06003D2E RID: 15662
		public abstract float ToSingle(long value);

		// Token: 0x06003D2F RID: 15663
		public abstract float ToSingle(decimal value);

		// Token: 0x06003D30 RID: 15664
		public abstract float ToSingle(float value);

		// Token: 0x06003D31 RID: 15665
		public abstract float ToSingle(double value);

		// Token: 0x06003D32 RID: 15666
		public abstract float ToSingle(DateTime value);

		// Token: 0x06003D33 RID: 15667
		public abstract float ToSingle(DateTimeOffset value);

		// Token: 0x06003D34 RID: 15668
		public abstract float ToSingle(string value);

		// Token: 0x06003D35 RID: 15669
		public abstract float ToSingle(object value);

		// Token: 0x06003D36 RID: 15670
		public abstract DateTime ToDateTime(bool value);

		// Token: 0x06003D37 RID: 15671
		public abstract DateTime ToDateTime(int value);

		// Token: 0x06003D38 RID: 15672
		public abstract DateTime ToDateTime(long value);

		// Token: 0x06003D39 RID: 15673
		public abstract DateTime ToDateTime(decimal value);

		// Token: 0x06003D3A RID: 15674
		public abstract DateTime ToDateTime(float value);

		// Token: 0x06003D3B RID: 15675
		public abstract DateTime ToDateTime(double value);

		// Token: 0x06003D3C RID: 15676
		public abstract DateTime ToDateTime(DateTime value);

		// Token: 0x06003D3D RID: 15677
		public abstract DateTime ToDateTime(DateTimeOffset value);

		// Token: 0x06003D3E RID: 15678
		public abstract DateTime ToDateTime(string value);

		// Token: 0x06003D3F RID: 15679
		public abstract DateTime ToDateTime(object value);

		// Token: 0x06003D40 RID: 15680
		public abstract DateTimeOffset ToDateTimeOffset(bool value);

		// Token: 0x06003D41 RID: 15681
		public abstract DateTimeOffset ToDateTimeOffset(int value);

		// Token: 0x06003D42 RID: 15682
		public abstract DateTimeOffset ToDateTimeOffset(long value);

		// Token: 0x06003D43 RID: 15683
		public abstract DateTimeOffset ToDateTimeOffset(decimal value);

		// Token: 0x06003D44 RID: 15684
		public abstract DateTimeOffset ToDateTimeOffset(float value);

		// Token: 0x06003D45 RID: 15685
		public abstract DateTimeOffset ToDateTimeOffset(double value);

		// Token: 0x06003D46 RID: 15686
		public abstract DateTimeOffset ToDateTimeOffset(DateTime value);

		// Token: 0x06003D47 RID: 15687
		public abstract DateTimeOffset ToDateTimeOffset(DateTimeOffset value);

		// Token: 0x06003D48 RID: 15688
		public abstract DateTimeOffset ToDateTimeOffset(string value);

		// Token: 0x06003D49 RID: 15689
		public abstract DateTimeOffset ToDateTimeOffset(object value);

		// Token: 0x06003D4A RID: 15690
		public abstract string ToString(bool value);

		// Token: 0x06003D4B RID: 15691
		public abstract string ToString(int value);

		// Token: 0x06003D4C RID: 15692
		public abstract string ToString(long value);

		// Token: 0x06003D4D RID: 15693
		public abstract string ToString(decimal value);

		// Token: 0x06003D4E RID: 15694
		public abstract string ToString(float value);

		// Token: 0x06003D4F RID: 15695
		public abstract string ToString(double value);

		// Token: 0x06003D50 RID: 15696
		public abstract string ToString(DateTime value);

		// Token: 0x06003D51 RID: 15697
		public abstract string ToString(DateTimeOffset value);

		// Token: 0x06003D52 RID: 15698
		public abstract string ToString(string value);

		// Token: 0x06003D53 RID: 15699
		public abstract string ToString(string value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06003D54 RID: 15700
		public abstract string ToString(object value);

		// Token: 0x06003D55 RID: 15701
		public abstract string ToString(object value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06003D56 RID: 15702
		public abstract object ChangeType(bool value, Type destinationType);

		// Token: 0x06003D57 RID: 15703
		public abstract object ChangeType(int value, Type destinationType);

		// Token: 0x06003D58 RID: 15704
		public abstract object ChangeType(long value, Type destinationType);

		// Token: 0x06003D59 RID: 15705
		public abstract object ChangeType(decimal value, Type destinationType);

		// Token: 0x06003D5A RID: 15706
		public abstract object ChangeType(float value, Type destinationType);

		// Token: 0x06003D5B RID: 15707
		public abstract object ChangeType(double value, Type destinationType);

		// Token: 0x06003D5C RID: 15708
		public abstract object ChangeType(DateTime value, Type destinationType);

		// Token: 0x06003D5D RID: 15709
		public abstract object ChangeType(DateTimeOffset value, Type destinationType);

		// Token: 0x06003D5E RID: 15710
		public abstract object ChangeType(string value, Type destinationType);

		// Token: 0x06003D5F RID: 15711
		public abstract object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver);

		// Token: 0x06003D60 RID: 15712
		public abstract object ChangeType(object value, Type destinationType);

		// Token: 0x06003D61 RID: 15713
		public abstract object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver);

		// Token: 0x06003D62 RID: 15714 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlValueConverter()
		{
		}
	}
}
