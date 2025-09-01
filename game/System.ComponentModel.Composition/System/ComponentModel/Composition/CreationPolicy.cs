using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies when and how a part will be instantiated.</summary>
	// Token: 0x02000032 RID: 50
	public enum CreationPolicy
	{
		/// <summary>Specifies that the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> will use the most appropriate <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the part given the current context. This is the default <see cref="T:System.ComponentModel.Composition.CreationPolicy" />. By default, <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> will use <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" />, unless the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> or importer requests <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" />.</summary>
		// Token: 0x040000AA RID: 170
		Any,
		/// <summary>Specifies that a single shared instance of the associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> will be created by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> and shared by all requestors.</summary>
		// Token: 0x040000AB RID: 171
		Shared,
		/// <summary>Specifies that a new non-shared instance of the associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> will be created by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> for every requestor.</summary>
		// Token: 0x040000AC RID: 172
		NonShared,
		// Token: 0x040000AD RID: 173
		NewScope
	}
}
