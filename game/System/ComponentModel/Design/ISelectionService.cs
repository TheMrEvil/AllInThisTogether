using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for a designer to select components.</summary>
	// Token: 0x02000469 RID: 1129
	public interface ISelectionService
	{
		/// <summary>Gets the object that is currently the primary selected object.</summary>
		/// <returns>The object that is currently the primary selected object.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600246A RID: 9322
		object PrimarySelection { get; }

		/// <summary>Gets the count of selected objects.</summary>
		/// <returns>The number of selected objects.</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600246B RID: 9323
		int SelectionCount { get; }

		/// <summary>Occurs when the current selection changes.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600246C RID: 9324
		// (remove) Token: 0x0600246D RID: 9325
		event EventHandler SelectionChanged;

		/// <summary>Occurs when the current selection is about to change.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600246E RID: 9326
		// (remove) Token: 0x0600246F RID: 9327
		event EventHandler SelectionChanging;

		/// <summary>Gets a value indicating whether the specified component is currently selected.</summary>
		/// <param name="component">The component to test.</param>
		/// <returns>
		///   <see langword="true" /> if the component is part of the user's current selection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002470 RID: 9328
		bool GetComponentSelected(object component);

		/// <summary>Gets a collection of components that are currently selected.</summary>
		/// <returns>A collection that represents the current set of components that are selected.</returns>
		// Token: 0x06002471 RID: 9329
		ICollection GetSelectedComponents();

		/// <summary>Selects the specified collection of components.</summary>
		/// <param name="components">The collection of components to select.</param>
		// Token: 0x06002472 RID: 9330
		void SetSelectedComponents(ICollection components);

		/// <summary>Selects the components from within the specified collection of components that match the specified selection type.</summary>
		/// <param name="components">The collection of components to select.</param>
		/// <param name="selectionType">A value from the <see cref="T:System.ComponentModel.Design.SelectionTypes" /> enumeration. The default is <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />.</param>
		// Token: 0x06002473 RID: 9331
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);
	}
}
