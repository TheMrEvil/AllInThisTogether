using System;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a property. These attribute values are defined in corhdr.h.</summary>
	// Token: 0x020008BB RID: 2235
	[Flags]
	public enum PropertyAttributes
	{
		/// <summary>Specifies that no attributes are associated with a property.</summary>
		// Token: 0x04002F1E RID: 12062
		None = 0,
		/// <summary>Specifies that the property is special, with the name describing how the property is special.</summary>
		// Token: 0x04002F1F RID: 12063
		SpecialName = 512,
		/// <summary>Specifies that the metadata internal APIs check the name encoding.</summary>
		// Token: 0x04002F20 RID: 12064
		RTSpecialName = 1024,
		/// <summary>Specifies that the property has a default value.</summary>
		// Token: 0x04002F21 RID: 12065
		HasDefault = 4096,
		/// <summary>Reserved.</summary>
		// Token: 0x04002F22 RID: 12066
		Reserved2 = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04002F23 RID: 12067
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04002F24 RID: 12068
		Reserved4 = 32768,
		/// <summary>Specifies a flag reserved for runtime use only.</summary>
		// Token: 0x04002F25 RID: 12069
		ReservedMask = 62464
	}
}
