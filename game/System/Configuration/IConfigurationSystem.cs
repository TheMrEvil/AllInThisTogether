using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	// Token: 0x020001B2 RID: 434
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <param name="configKey">The configuration key.</param>
		/// <returns>The object representing the configuration.</returns>
		// Token: 0x06000B7E RID: 2942
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		// Token: 0x06000B7F RID: 2943
		void Init();
	}
}
