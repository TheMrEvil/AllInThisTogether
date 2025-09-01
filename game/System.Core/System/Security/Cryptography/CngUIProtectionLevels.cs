using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the protection level for the key in user interface (UI) prompting scenarios.</summary>
	// Token: 0x02000053 RID: 83
	[Flags]
	public enum CngUIProtectionLevels
	{
		/// <summary>No UI prompt is displayed when the key is accessed.</summary>
		// Token: 0x0400034A RID: 842
		None = 0,
		/// <summary>A UI prompt is displayed the first time the key is accessed in a process.</summary>
		// Token: 0x0400034B RID: 843
		ProtectKey = 1,
		/// <summary>A UI prompt is displayed every time the key is accessed.</summary>
		// Token: 0x0400034C RID: 844
		ForceHighProtection = 2
	}
}
