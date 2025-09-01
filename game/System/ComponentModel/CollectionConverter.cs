using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert collection objects to and from various other representations.</summary>
	// Token: 0x0200040B RID: 1035
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionConverter : TypeConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The culture to which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. This parameter must inherit from <see cref="T:System.Collections.ICollection" />.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06002178 RID: 8568 RVA: 0x00072860 File Offset: 0x00070A60
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is ICollection)
			{
				return SR.GetString("(Collection)");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties. This method always returns <see langword="null" />.</returns>
		// Token: 0x06002179 RID: 8569 RVA: 0x00002F6A File Offset: 0x0000116A
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="false" /> because <see cref="M:System.ComponentModel.CollectionConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should not be called to find the properties of this object. This method never returns <see langword="true" />.</returns>
		// Token: 0x0600217A RID: 8570 RVA: 0x00003062 File Offset: 0x00001262
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionConverter" /> class.</summary>
		// Token: 0x0600217B RID: 8571 RVA: 0x00018550 File Offset: 0x00016750
		public CollectionConverter()
		{
		}
	}
}
