using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface that can list extender providers.</summary>
	// Token: 0x02000461 RID: 1121
	public interface IExtenderListService
	{
		/// <summary>Gets the set of extender providers for the component.</summary>
		/// <returns>An array of type <see cref="T:System.ComponentModel.IExtenderProvider" /> that lists the active extender providers. If there are no providers, an empty array is returned.</returns>
		// Token: 0x0600244D RID: 9293
		IExtenderProvider[] GetExtenderProviders();
	}
}
