using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Provides contextual information that the provider can use when persisting settings.</summary>
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class SettingsContext : Hashtable
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00031DEB File Offset: 0x0002FFEB
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00031DF3 File Offset: 0x0002FFF3
		internal ApplicationSettingsBase CurrentSettings
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsContext" /> class.</summary>
		// Token: 0x06000BEA RID: 3050 RVA: 0x00031A39 File Offset: 0x0002FC39
		public SettingsContext()
		{
		}

		// Token: 0x04000795 RID: 1941
		[NonSerialized]
		private ApplicationSettingsBase current;
	}
}
