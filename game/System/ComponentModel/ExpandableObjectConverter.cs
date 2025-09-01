using System;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert expandable objects to and from various other representations.</summary>
	// Token: 0x020003A9 RID: 937
	public class ExpandableObjectConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ExpandableObjectConverter" /> class.</summary>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x00018550 File Offset: 0x00016750
		public ExpandableObjectConverter()
		{
		}

		/// <summary>Gets a collection of properties for the type of object specified by the value parameter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of object to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06001EB9 RID: 7865 RVA: 0x0006C934 File Offset: 0x0006AB34
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns <see langword="false" />.</returns>
		// Token: 0x06001EBA RID: 7866 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
