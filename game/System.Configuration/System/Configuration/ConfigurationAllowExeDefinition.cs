using System;

namespace System.Configuration
{
	/// <summary>Specifies the locations within the configuration-file hierarchy that can set or override the properties contained within a <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
	// Token: 0x02000016 RID: 22
	public enum ConfigurationAllowExeDefinition
	{
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined only in the Machine.config file.</summary>
		// Token: 0x04000054 RID: 84
		MachineOnly,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined either in the Machine.config file or in the Exe.config file in the client application directory. This is the default value.</summary>
		// Token: 0x04000055 RID: 85
		MachineToApplication = 100,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in the Machine.config file, in the Exe.config file in the client application directory, in the User.config file in the roaming user directory, or in the User.config file in the local user directory.</summary>
		// Token: 0x04000056 RID: 86
		MachineToLocalUser = 300,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in the Machine.config file, in the Exe.config file in the client application directory, or in the User.config file in the roaming user directory.</summary>
		// Token: 0x04000057 RID: 87
		MachineToRoamingUser = 200
	}
}
