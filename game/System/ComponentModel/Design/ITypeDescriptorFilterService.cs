using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to modify the set of member descriptors for a component in design mode.</summary>
	// Token: 0x0200046C RID: 1132
	public interface ITypeDescriptorFilterService
	{
		/// <summary>Filters the attributes that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="component">The component to filter the attributes of.</param>
		/// <param name="attributes">A dictionary of attributes that can be modified.</param>
		/// <returns>
		///   <see langword="true" /> if the set of filtered attributes is to be cached; <see langword="false" /> if the filter service must query again.</returns>
		// Token: 0x0600247C RID: 9340
		bool FilterAttributes(IComponent component, IDictionary attributes);

		/// <summary>Filters the events that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="component">The component to filter events for.</param>
		/// <param name="events">A dictionary of events that can be modified.</param>
		/// <returns>
		///   <see langword="true" /> if the set of filtered events is to be cached; <see langword="false" /> if the filter service must query again.</returns>
		// Token: 0x0600247D RID: 9341
		bool FilterEvents(IComponent component, IDictionary events);

		/// <summary>Filters the properties that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="component">The component to filter properties for.</param>
		/// <param name="properties">A dictionary of properties that can be modified.</param>
		/// <returns>
		///   <see langword="true" /> if the set of filtered properties is to be cached; <see langword="false" /> if the filter service must query again.</returns>
		// Token: 0x0600247E RID: 9342
		bool FilterProperties(IComponent component, IDictionary properties);
	}
}
