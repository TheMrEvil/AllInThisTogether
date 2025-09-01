using System;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Provides notifications when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> changes.</summary>
	// Token: 0x020000E8 RID: 232
	public interface INotifyComposablePartCatalogChanged
	{
		/// <summary>Occurs when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> has changed.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000628 RID: 1576
		// (remove) Token: 0x06000629 RID: 1577
		event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		/// <summary>Occurs when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> is changing.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600062A RID: 1578
		// (remove) Token: 0x0600062B RID: 1579
		event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;
	}
}
