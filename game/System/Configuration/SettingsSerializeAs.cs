using System;

namespace System.Configuration
{
	/// <summary>Determines the serialization scheme used to store application settings.</summary>
	// Token: 0x020001D7 RID: 471
	public enum SettingsSerializeAs
	{
		/// <summary>The settings property is serialized as plain text.</summary>
		// Token: 0x040007B4 RID: 1972
		String,
		/// <summary>The settings property is serialized as XML using XML serialization.</summary>
		// Token: 0x040007B5 RID: 1973
		Xml,
		/// <summary>The settings property is serialized using binary object serialization.</summary>
		// Token: 0x040007B6 RID: 1974
		Binary,
		/// <summary>The settings provider has implicit knowledge of the property or its type and picks an appropriate serialization mechanism. Often used for custom serialization.</summary>
		// Token: 0x040007B7 RID: 1975
		ProviderSpecific
	}
}
