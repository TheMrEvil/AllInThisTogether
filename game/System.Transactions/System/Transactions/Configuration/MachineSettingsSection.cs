using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	/// <summary>Represents an XML section in a configuration file encapsulating all settings that can be modified only at the machine level. This class cannot be inherited.</summary>
	// Token: 0x0200002E RID: 46
	public class MachineSettingsSection : ConfigurationSection
	{
		/// <summary>Gets a maximum amount of time allowed before a transaction times out.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the maximum allowable time. The default value is 00:10:00.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt to set this property to negative values.</exception>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000033FB File Offset: 0x000015FB
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000340D File Offset: 0x0000160D
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("maxTimeout", DefaultValue = "00:10:00")]
		public TimeSpan MaxTimeout
		{
			get
			{
				return (TimeSpan)base["maxTimeout"];
			}
			set
			{
				base["maxTimeout"] = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> class.</summary>
		// Token: 0x060000E4 RID: 228 RVA: 0x000033F3 File Offset: 0x000015F3
		public MachineSettingsSection()
		{
		}
	}
}
