using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020003F8 RID: 1016
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x00071FED File Offset: 0x000701ED
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00071FF9 File Offset: 0x000701F9
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00072007 File Offset: 0x00070207
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00072018 File Offset: 0x00070218
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt32Converter" /> class.</summary>
		// Token: 0x06002126 RID: 8486 RVA: 0x00069981 File Offset: 0x00067B81
		public UInt32Converter()
		{
		}
	}
}
