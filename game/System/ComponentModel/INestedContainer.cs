using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for nested containers, which logically contain zero or more other components and are owned by a parent component.</summary>
	// Token: 0x020003B8 RID: 952
	public interface INestedContainer : IContainer, IDisposable
	{
		/// <summary>Gets the owning component for the nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns the nested container.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001F17 RID: 7959
		IComponent Owner { get; }
	}
}
