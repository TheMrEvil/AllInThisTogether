using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts between a string and an enumeration type.</summary>
	// Token: 0x02000042 RID: 66
	public sealed class GenericEnumConverter : ConfigurationConverterBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.GenericEnumConverter" /> class.</summary>
		/// <param name="typeEnum">The enumeration type to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeEnum" /> is <see langword="null" />.</exception>
		// Token: 0x06000240 RID: 576 RVA: 0x000079FD File Offset: 0x00005BFD
		public GenericEnumConverter(Type typeEnum)
		{
			if (typeEnum == null)
			{
				throw new ArgumentNullException("typeEnum");
			}
			this.typeEnum = typeEnum;
		}

		/// <summary>Converts a <see cref="T:System.String" /> to an <see cref="T:System.Enum" /> type.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.Enum" /> type that represents the <paramref name="data" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is null or an empty string ("").  
		/// -or-
		///  <paramref name="data" /> starts with a numeric character.  
		/// -or-
		///  <paramref name="data" /> includes white space.</exception>
		// Token: 0x06000241 RID: 577 RVA: 0x00007A20 File Offset: 0x00005C20
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if (data == null)
			{
				throw new ArgumentException();
			}
			return Enum.Parse(this.typeEnum, (string)data);
		}

		/// <summary>Converts an <see cref="T:System.Enum" /> type to a <see cref="T:System.String" /> value.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter.</returns>
		// Token: 0x06000242 RID: 578 RVA: 0x00007A3C File Offset: 0x00005C3C
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			return value.ToString();
		}

		// Token: 0x040000EA RID: 234
		private Type typeEnum;
	}
}
