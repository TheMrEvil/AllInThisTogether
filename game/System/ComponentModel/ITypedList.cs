using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to discover the schema for a bindable list, where the properties available for binding differ from the public properties of the object to bind to.</summary>
	// Token: 0x020003BD RID: 957
	public interface ITypedList
	{
		/// <summary>Returns the name of the list.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be <see langword="null" />.</param>
		/// <returns>The name of the list.</returns>
		// Token: 0x06001F22 RID: 7970
		string GetListName(PropertyDescriptor[] listAccessors);

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the collection as bindable. This can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		// Token: 0x06001F23 RID: 7971
		PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
	}
}
