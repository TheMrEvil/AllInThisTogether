using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a string to its canonical format.</summary>
	// Token: 0x02000076 RID: 118
	public sealed class WhiteSpaceTrimStringConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>An object representing the converted value.</returns>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000AE17 File Offset: 0x00009017
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).Trim();
		}

		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>An object representing the converted value.</returns>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000AE24 File Offset: 0x00009024
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return "";
			}
			if (!(value is string))
			{
				throw new ArgumentException("value");
			}
			return ((string)value).Trim();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.WhiteSpaceTrimStringConverter" /> class.</summary>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000258B File Offset: 0x0000078B
		public WhiteSpaceTrimStringConverter()
		{
		}
	}
}
