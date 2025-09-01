using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a time span expressed in seconds.</summary>
	// Token: 0x02000070 RID: 112
	public class TimeSpanSecondsConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in seconds.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> cannot be parsed as an integer value.</exception>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000AADC File Offset: 0x00008CDC
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if (!(data is string))
			{
				throw new ArgumentException("data");
			}
			long num;
			if (!long.TryParse((string)data, out num))
			{
				throw new ArgumentException("data");
			}
			return TimeSpan.FromSeconds((double)num);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter in minutes.</returns>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000AB24 File Offset: 0x00008D24
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			return ((long)((TimeSpan)value).TotalSeconds).ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanSecondsConverter" /> class.</summary>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000258B File Offset: 0x0000078B
		public TimeSpanSecondsConverter()
		{
		}
	}
}
