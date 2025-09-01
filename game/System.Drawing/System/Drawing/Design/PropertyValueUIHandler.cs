using System;
using System.Collections;
using System.ComponentModel;

namespace System.Drawing.Design
{
	/// <summary>Represents the method that adds a delegate to an implementation of <see cref="T:System.Drawing.Design.IPropertyValueUIService" />.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to obtain context information.</param>
	/// <param name="propDesc">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property being queried.</param>
	/// <param name="valueUIItemList">An <see cref="T:System.Collections.ArrayList" /> of <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects containing the UI items associated with the property.</param>
	// Token: 0x02000126 RID: 294
	// (Invoke) Token: 0x06000D9F RID: 3487
	public delegate void PropertyValueUIHandler(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList);
}
