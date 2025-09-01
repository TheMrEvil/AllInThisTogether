using System;

namespace System.IO
{
	/// <summary>Specifies whether the underlying handle is inheritable by child processes.</summary>
	// Token: 0x02000333 RID: 819
	[Serializable]
	public enum HandleInheritability
	{
		/// <summary>Specifies that the handle is not inheritable by child processes.</summary>
		// Token: 0x04000BEC RID: 3052
		None,
		/// <summary>Specifies that the handle is inheritable by child processes.</summary>
		// Token: 0x04000BED RID: 3053
		Inheritable
	}
}
