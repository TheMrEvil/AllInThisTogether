using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a comma-delimited string value to and from a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000010 RID: 16
	public sealed class CommaDelimitedStringCollectionConverter : ConfigurationConverterBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CommaDelimitedStringCollectionConverter" /> class.</summary>
		// Token: 0x06000036 RID: 54 RVA: 0x0000258B File Offset: 0x0000078B
		public CommaDelimitedStringCollectionConverter()
		{
		}

		/// <summary>Converts a <see cref="T:System.String" /> object to a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> used during conversion.</param>
		/// <param name="data">The comma-separated <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> containing the converted value.</returns>
		// Token: 0x06000037 RID: 55 RVA: 0x00002594 File Offset: 0x00000794
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			foreach (string text in ((string)data).Split(',', StringSplitOptions.None))
			{
				commaDelimitedStringCollection.Add(text.Trim());
			}
			commaDelimitedStringCollection.UpdateStringHash();
			return commaDelimitedStringCollection;
		}

		/// <summary>Converts a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object to a <see cref="T:System.String" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> used during conversion.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="type">The conversion type.</param>
		/// <returns>The <see cref="T:System.String" /> representing the converted <paramref name="value" /> parameter, which is a <see cref="T:System.Configuration.CommaDelimitedStringCollection" />.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x000025DB File Offset: 0x000007DB
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return null;
			}
			if (!typeof(StringCollection).IsAssignableFrom(value.GetType()))
			{
				throw new ArgumentException();
			}
			return value.ToString();
		}
	}
}
