using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines a set of flags used when registering assemblies.</summary>
	// Token: 0x02000719 RID: 1817
	[Flags]
	[ComVisible(true)]
	public enum AssemblyRegistrationFlags
	{
		/// <summary>Indicates no special settings.</summary>
		// Token: 0x04002B00 RID: 11008
		None = 0,
		/// <summary>Indicates that the code base key for the assembly should be set in the registry.</summary>
		// Token: 0x04002B01 RID: 11009
		SetCodeBase = 1
	}
}
