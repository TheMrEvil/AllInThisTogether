using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies whether a mapping is read, write, or both.</summary>
	// Token: 0x020002D5 RID: 725
	[Flags]
	public enum XmlMappingAccess
	{
		/// <summary>Both read and write methods are generated.</summary>
		// Token: 0x040019F6 RID: 6646
		None = 0,
		/// <summary>Read methods are generated.</summary>
		// Token: 0x040019F7 RID: 6647
		Read = 1,
		/// <summary>Write methods are generated.</summary>
		// Token: 0x040019F8 RID: 6648
		Write = 2
	}
}
