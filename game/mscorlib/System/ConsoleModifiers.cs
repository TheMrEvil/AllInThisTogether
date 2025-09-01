using System;

namespace System
{
	/// <summary>Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard.</summary>
	// Token: 0x020001C1 RID: 449
	[Flags]
	public enum ConsoleModifiers
	{
		/// <summary>The left or right ALT modifier key.</summary>
		// Token: 0x0400143D RID: 5181
		Alt = 1,
		/// <summary>The left or right SHIFT modifier key.</summary>
		// Token: 0x0400143E RID: 5182
		Shift = 2,
		/// <summary>The left or right CTRL modifier key.</summary>
		// Token: 0x0400143F RID: 5183
		Control = 4
	}
}
