using System;

namespace System.ComponentModel
{
	/// <summary>Provides the <see langword="abstract" /> base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x020003C6 RID: 966
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001F46 RID: 8006
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x06001F47 RID: 8007
		public abstract void Dispose();

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.License" /> class.</summary>
		// Token: 0x06001F48 RID: 8008 RVA: 0x0000219B File Offset: 0x0000039B
		protected License()
		{
		}
	}
}
