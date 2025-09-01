using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x02000462 RID: 1122
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add.</param>
		// Token: 0x0600244E RID: 9294
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove.</param>
		// Token: 0x0600244F RID: 9295
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
