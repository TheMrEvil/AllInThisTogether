using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020003EA RID: 1002
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000717E0 File Offset: 0x0006F9E0
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000717EC File Offset: 0x0006F9EC
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000717FA File Offset: 0x0006F9FA
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0007180C File Offset: 0x0006FA0C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SByteConverter" /> class.</summary>
		// Token: 0x060020DB RID: 8411 RVA: 0x00069981 File Offset: 0x00067B81
		public SByteConverter()
		{
		}
	}
}
