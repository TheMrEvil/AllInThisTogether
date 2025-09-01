using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x020003C2 RID: 962
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006CDA9 File Offset: 0x0006AFA9
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0006CDB5 File Offset: 0x0006AFB5
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0006CDC3 File Offset: 0x0006AFC3
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0006CDD4 File Offset: 0x0006AFD4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int64Converter" /> class.</summary>
		// Token: 0x06001F3A RID: 7994 RVA: 0x00069981 File Offset: 0x00067B81
		public Int64Converter()
		{
		}
	}
}
