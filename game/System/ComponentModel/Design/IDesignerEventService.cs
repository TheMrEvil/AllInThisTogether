using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides event notifications when root designers are added and removed, when a selected component changes, and when the current root designer changes.</summary>
	// Token: 0x0200045A RID: 1114
	public interface IDesignerEventService
	{
		/// <summary>Gets the root designer for the currently active document.</summary>
		/// <returns>The currently active document, or <see langword="null" /> if there is no active document.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002413 RID: 9235
		IDesignerHost ActiveDesigner { get; }

		/// <summary>Gets a collection of root designers for design documents that are currently active in the development environment.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerCollection" /> containing the root designers that have been created and not yet disposed.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002414 RID: 9236
		DesignerCollection Designers { get; }

		/// <summary>Occurs when the current root designer changes.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06002415 RID: 9237
		// (remove) Token: 0x06002416 RID: 9238
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		/// <summary>Occurs when a root designer is created.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06002417 RID: 9239
		// (remove) Token: 0x06002418 RID: 9240
		event DesignerEventHandler DesignerCreated;

		/// <summary>Occurs when a root designer for a document is disposed.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06002419 RID: 9241
		// (remove) Token: 0x0600241A RID: 9242
		event DesignerEventHandler DesignerDisposed;

		/// <summary>Occurs when the current design-view selection changes.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600241B RID: 9243
		// (remove) Token: 0x0600241C RID: 9244
		event EventHandler SelectionChanged;
	}
}
