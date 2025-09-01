using System;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Represents an element that participates in composition.</summary>
	// Token: 0x02000096 RID: 150
	public interface ICompositionElement
	{
		/// <summary>Gets the display name of the composition element.</summary>
		/// <returns>The human-readable display name of the <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" />.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003F5 RID: 1013
		string DisplayName { get; }

		/// <summary>Gets the composition element from which the current composition element originated.</summary>
		/// <returns>The composition element from which the current <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" /> originated, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" /> is the root composition element.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003F6 RID: 1014
		ICompositionElement Origin { get; }
	}
}
