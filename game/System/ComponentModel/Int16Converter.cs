using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020003C0 RID: 960
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0006CD13 File Offset: 0x0006AF13
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0006CD1F File Offset: 0x0006AF1F
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0006CD2D File Offset: 0x0006AF2D
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0006CD3C File Offset: 0x0006AF3C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int16Converter" /> class.</summary>
		// Token: 0x06001F30 RID: 7984 RVA: 0x00069981 File Offset: 0x00067B81
		public Int16Converter()
		{
		}
	}
}
