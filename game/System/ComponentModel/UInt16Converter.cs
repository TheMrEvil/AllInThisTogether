using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020003F7 RID: 1015
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00071FA1 File Offset: 0x000701A1
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00071FAD File Offset: 0x000701AD
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00071FBB File Offset: 0x000701BB
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00071FCC File Offset: 0x000701CC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt16Converter" /> class.</summary>
		// Token: 0x06002121 RID: 8481 RVA: 0x00069981 File Offset: 0x00067B81
		public UInt16Converter()
		{
		}
	}
}
