using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class.</summary>
		// Token: 0x06000BD8 RID: 3032 RVA: 0x00031A39 File Offset: 0x0002FC39
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class.</summary>
		/// <param name="attributes">A collection of key/value pairs that are related to configuration settings.</param>
		// Token: 0x06000BD9 RID: 3033 RVA: 0x00031A41 File Offset: 0x0002FC41
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes) : base(attributes)
		{
		}
	}
}
