using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020003A4 RID: 932
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0006C14D File Offset: 0x0006A34D
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0006C159 File Offset: 0x0006A359
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006C16B File Offset: 0x0006A36B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0006C180 File Offset: 0x0006A380
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoubleConverter" /> class.</summary>
		// Token: 0x06001E79 RID: 7801 RVA: 0x00069981 File Offset: 0x00067B81
		public DoubleConverter()
		{
		}
	}
}
