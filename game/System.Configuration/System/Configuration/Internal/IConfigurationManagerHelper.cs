using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support configuration management.</summary>
	// Token: 0x0200007D RID: 125
	[ComVisible(false)]
	public interface IConfigurationManagerHelper
	{
		/// <summary>Ensures that the networking configuration is loaded.</summary>
		// Token: 0x0600042C RID: 1068
		void EnsureNetConfigLoaded();
	}
}
