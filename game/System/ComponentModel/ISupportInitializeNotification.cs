using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x020003BB RID: 955
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the component has completed initialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001F1A RID: 7962
		bool IsInitialized { get; }

		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001F1B RID: 7963
		// (remove) Token: 0x06001F1C RID: 7964
		event EventHandler Initialized;
	}
}
