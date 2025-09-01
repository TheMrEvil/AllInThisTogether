using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a time span expressed in minutes.</summary>
	// Token: 0x0200006E RID: 110
	public class TimeSpanMinutesConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in minutes.</returns>
		// Token: 0x060003BF RID: 959 RVA: 0x0000A9FD File Offset: 0x00008BFD
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return TimeSpan.FromMinutes((double)long.Parse((string)data));
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> representing the <paramref name="value" /> parameter in minutes.</returns>
		// Token: 0x060003C0 RID: 960 RVA: 0x0000AA18 File Offset: 0x00008C18
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			return ((long)((TimeSpan)value).TotalMinutes).ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanMinutesConverter" /> class.</summary>
		// Token: 0x060003C1 RID: 961 RVA: 0x0000258B File Offset: 0x0000078B
		public TimeSpanMinutesConverter()
		{
		}
	}
}
