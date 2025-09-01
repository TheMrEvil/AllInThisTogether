using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for the machine configuration file.</summary>
	// Token: 0x02000021 RID: 33
	public class ConfigurationFileMap : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class.</summary>
		// Token: 0x06000124 RID: 292 RVA: 0x00005775 File Offset: 0x00003975
		public ConfigurationFileMap()
		{
			this.machineConfigFilename = RuntimeEnvironment.SystemConfigurationFile;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class based on the supplied parameter.</summary>
		/// <param name="machineConfigFilename">The name of the machine configuration file.</param>
		// Token: 0x06000125 RID: 293 RVA: 0x00005788 File Offset: 0x00003988
		public ConfigurationFileMap(string machineConfigFilename)
		{
			this.machineConfigFilename = machineConfigFilename;
		}

		/// <summary>Gets or sets the name of the machine configuration file name.</summary>
		/// <returns>The machine configuration file name.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005797 File Offset: 0x00003997
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000579F File Offset: 0x0000399F
		public string MachineConfigFilename
		{
			get
			{
				return this.machineConfigFilename;
			}
			set
			{
				this.machineConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</returns>
		// Token: 0x06000128 RID: 296 RVA: 0x000057A8 File Offset: 0x000039A8
		public virtual object Clone()
		{
			return new ConfigurationFileMap(this.machineConfigFilename);
		}

		// Token: 0x04000088 RID: 136
		private string machineConfigFilename;
	}
}
