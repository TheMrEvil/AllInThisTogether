using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020003EC RID: 1004
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0007189F File Offset: 0x0006FA9F
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000718AB File Offset: 0x0006FAAB
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000718BD File Offset: 0x0006FABD
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000718D0 File Offset: 0x0006FAD0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SingleConverter" /> class.</summary>
		// Token: 0x060020E6 RID: 8422 RVA: 0x00069981 File Offset: 0x00067B81
		public SingleConverter()
		{
		}
	}
}
