using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert components to and from various other representations.</summary>
	// Token: 0x0200040E RID: 1038
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ComponentConverter : ReferenceConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentConverter" /> class.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to associate with this component converter.</param>
		// Token: 0x0600218E RID: 8590 RVA: 0x00072ADB File Offset: 0x00070CDB
		public ComponentConverter(Type type) : base(type)
		{
		}

		/// <summary>Gets a collection of properties for the type of component specified by the value parameter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of component to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x0600218F RID: 8591 RVA: 0x0006C934 File Offset: 0x0006AB34
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns <see langword="false" />.</returns>
		// Token: 0x06002190 RID: 8592 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
