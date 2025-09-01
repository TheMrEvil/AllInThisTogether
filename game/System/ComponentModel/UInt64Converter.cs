using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020003F9 RID: 1017
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00072039 File Offset: 0x00070239
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00072045 File Offset: 0x00070245
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00072053 File Offset: 0x00070253
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00072064 File Offset: 0x00070264
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt64Converter" /> class.</summary>
		// Token: 0x0600212B RID: 8491 RVA: 0x00069981 File Offset: 0x00067B81
		public UInt64Converter()
		{
		}
	}
}
