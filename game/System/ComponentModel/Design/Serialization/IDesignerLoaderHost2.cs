using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that extends <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> to specify whether errors are tolerated while loading a design document.</summary>
	// Token: 0x02000486 RID: 1158
	public interface IDesignerLoaderHost2 : IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider
	{
		/// <summary>Gets or sets a value indicating whether errors should be ignored when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerLoaderHost.Reload" /> is called.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer loader will ignore errors when it reloads; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002517 RID: 9495
		// (set) Token: 0x06002518 RID: 9496
		bool IgnoreErrorsDuringReload { get; set; }

		/// <summary>Gets or sets a value indicating whether it is possible to reload with errors.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer loader can reload the design document when errors are detected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002519 RID: 9497
		// (set) Token: 0x0600251A RID: 9498
		bool CanReloadWithErrors { get; set; }
	}
}
