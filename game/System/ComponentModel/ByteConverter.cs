using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x02000387 RID: 903
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00069936 File Offset: 0x00067B36
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00069942 File Offset: 0x00067B42
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00069950 File Offset: 0x00067B50
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00069960 File Offset: 0x00067B60
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ByteConverter" /> class.</summary>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x00069981 File Offset: 0x00067B81
		public ByteConverter()
		{
		}
	}
}
