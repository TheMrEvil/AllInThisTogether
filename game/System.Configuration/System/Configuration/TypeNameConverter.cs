using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts between type and string values. This class cannot be inherited.</summary>
	// Token: 0x02000074 RID: 116
	public sealed class TypeNameConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> object to a <see cref="T:System.Type" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.Type" /> that represents the <paramref name="data" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Type" /> value cannot be resolved.</exception>
		// Token: 0x060003DA RID: 986 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return Type.GetType((string)data);
		}

		/// <summary>Converts a <see cref="T:System.Type" /> object to a <see cref="T:System.String" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter.</returns>
		// Token: 0x060003DB RID: 987 RVA: 0x0000ADF2 File Offset: 0x00008FF2
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return null;
			}
			if (!(value is Type))
			{
				throw new ArgumentException("value");
			}
			return ((Type)value).AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TypeNameConverter" /> class.</summary>
		// Token: 0x060003DC RID: 988 RVA: 0x0000258B File Offset: 0x0000078B
		public TypeNameConverter()
		{
		}
	}
}
