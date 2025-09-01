using System;

namespace System.ComponentModel
{
	/// <summary>Provides contextual information about a component, such as its container and property descriptor.</summary>
	// Token: 0x020003BC RID: 956
	public interface ITypeDescriptorContext : IServiceProvider
	{
		/// <summary>Gets the container representing this <see cref="T:System.ComponentModel.TypeDescriptor" /> request.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IContainer" /> with the set of objects for this <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, <see langword="null" /> if there is no container or if the <see cref="T:System.ComponentModel.TypeDescriptor" /> does not use outside objects.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001F1D RID: 7965
		IContainer Container { get; }

		/// <summary>Gets the object that is connected with this type descriptor request.</summary>
		/// <returns>The object that invokes the method on the <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, <see langword="null" /> if there is no object responsible for the call.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001F1E RID: 7966
		object Instance { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with the given context item.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the given context item; otherwise, <see langword="null" /> if there is no <see cref="T:System.ComponentModel.PropertyDescriptor" /> responsible for the call.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001F1F RID: 7967
		PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
		/// <returns>
		///   <see langword="true" /> if this object can be changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F20 RID: 7968
		bool OnComponentChanging();

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
		// Token: 0x06001F21 RID: 7969
		void OnComponentChanged();
	}
}
