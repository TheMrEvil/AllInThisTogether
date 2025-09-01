using System;

namespace System.Configuration
{
	/// <summary>Determines which properties are written out to a configuration file.</summary>
	// Token: 0x0200002F RID: 47
	public enum ConfigurationSaveMode
	{
		/// <summary>Causes only properties that differ from inherited values to be written to the configuration file.</summary>
		// Token: 0x040000B9 RID: 185
		Minimal = 1,
		/// <summary>Causes all properties to be written to the configuration file. This is useful mostly for creating information configuration files or moving configuration values from one machine to another.</summary>
		// Token: 0x040000BA RID: 186
		Full,
		/// <summary>Causes only modified properties to be written to the configuration file, even when the value is the same as the inherited value.</summary>
		// Token: 0x040000BB RID: 187
		Modified = 0
	}
}
