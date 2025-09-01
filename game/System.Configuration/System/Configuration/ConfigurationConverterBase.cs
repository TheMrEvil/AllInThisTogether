using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>The base class for the configuration converter types.</summary>
	// Token: 0x02000018 RID: 24
	public abstract class ConfigurationConverterBase : TypeConverter
	{
		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000093 RID: 147 RVA: 0x000035AB File Offset: 0x000017AB
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertFrom(ctx, type);
		}

		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversion.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000094 RID: 148 RVA: 0x000035C9 File Offset: 0x000017C9
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertTo(ctx, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationConverterBase" /> class.</summary>
		// Token: 0x06000095 RID: 149 RVA: 0x000035E7 File Offset: 0x000017E7
		protected ConfigurationConverterBase()
		{
		}
	}
}
