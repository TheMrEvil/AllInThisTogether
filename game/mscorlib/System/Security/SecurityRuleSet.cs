using System;

namespace System.Security
{
	/// <summary>Identifies the set of security rules the common language runtime should enforce for an assembly.</summary>
	// Token: 0x020003D7 RID: 983
	public enum SecurityRuleSet : byte
	{
		/// <summary>Unsupported. Using this value results in a <see cref="T:System.IO.FileLoadException" /> being thrown.</summary>
		// Token: 0x04001EA3 RID: 7843
		None,
		/// <summary>Indicates that the runtime will enforce level 1 (.NET Framework version 2.0) transparency rules.</summary>
		// Token: 0x04001EA4 RID: 7844
		Level1,
		/// <summary>Indicates that the runtime will enforce level 2 transparency rules.</summary>
		// Token: 0x04001EA5 RID: 7845
		Level2
	}
}
