using System;
using System.Configuration;

namespace System.Transactions.Configuration
{
	/// <summary>Represents a configuration section that encapsulates and allows traversal of all the transaction configuration XML elements and attributes that are within this configuration section. This class cannot be inherited.</summary>
	// Token: 0x0200002F RID: 47
	public class TransactionsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Provides static access to a <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" />.</summary>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> representing the configuration settings that apply to a particular computer, application, or resource.</param>
		/// <returns>A <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" /> object.</returns>
		// Token: 0x060000E5 RID: 229 RVA: 0x00003420 File Offset: 0x00001620
		public static TransactionsSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.transactions") as TransactionsSectionGroup;
		}

		/// <summary>Gets the default settings used to initialize the elements and attributes in a transactions section.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that represents the default settings. The default is a <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that is populated with default values.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003440 File Offset: 0x00001640
		[ConfigurationProperty("defaultSettings")]
		public DefaultSettingsSection DefaultSettings
		{
			get
			{
				return (DefaultSettingsSection)base.Sections["defaultSettings"];
			}
		}

		/// <summary>Gets the configuration settings set at the machine level.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that represents the configuration settings at the machine level. The default is a <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that is populated with default values.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003457 File Offset: 0x00001657
		[ConfigurationProperty("machineSettings")]
		public MachineSettingsSection MachineSettings
		{
			get
			{
				return (MachineSettingsSection)base.Sections["machineSettings"];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" /> class.</summary>
		// Token: 0x060000E8 RID: 232 RVA: 0x0000346E File Offset: 0x0000166E
		public TransactionsSectionGroup()
		{
		}
	}
}
