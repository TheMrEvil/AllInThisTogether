using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020003C1 RID: 961
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0006CD5D File Offset: 0x0006AF5D
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0006CD69 File Offset: 0x0006AF69
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0006CD77 File Offset: 0x0006AF77
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0006CD88 File Offset: 0x0006AF88
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int32Converter" /> class.</summary>
		// Token: 0x06001F35 RID: 7989 RVA: 0x00069981 File Offset: 0x00067B81
		public Int32Converter()
		{
		}
	}
}
