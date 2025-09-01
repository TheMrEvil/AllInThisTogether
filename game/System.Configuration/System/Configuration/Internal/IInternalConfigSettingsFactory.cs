using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the configuration system to set the <see cref="T:System.Configuration.ConfigurationSettings" /> class.</summary>
	// Token: 0x02000084 RID: 132
	[ComVisible(false)]
	public interface IInternalConfigSettingsFactory
	{
		/// <summary>Indicates that initialization of the configuration system has completed.</summary>
		// Token: 0x0600047C RID: 1148
		void CompleteInit();

		/// <summary>Provides hierarchical configuration settings and extensions specific to ASP.NET to the configuration system.</summary>
		/// <param name="internalConfigSystem">An <see cref="T:System.Configuration.Internal.IInternalConfigSystem" /> object used by the <see cref="T:System.Configuration.ConfigurationSettings" /> class.</param>
		/// <param name="initComplete">
		///   <see langword="true" /> if the initialization process of the configuration system is complete; otherwise, <see langword="false" />.</param>
		// Token: 0x0600047D RID: 1149
		void SetConfigurationSystem(IInternalConfigSystem internalConfigSystem, bool initComplete);
	}
}
