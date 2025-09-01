using System;

namespace System.IO.IsolatedStorage
{
	/// <summary>Enumerates the levels of isolated storage scope that are supported by <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" />.</summary>
	// Token: 0x02000B70 RID: 2928
	[Flags]
	public enum IsolatedStorageScope
	{
		/// <summary>No isolated storage usage.</summary>
		// Token: 0x04003D8E RID: 15758
		None = 0,
		/// <summary>Isolated storage scoped by user identity.</summary>
		// Token: 0x04003D8F RID: 15759
		User = 1,
		/// <summary>Isolated storage scoped to the application domain identity.</summary>
		// Token: 0x04003D90 RID: 15760
		Domain = 2,
		/// <summary>Isolated storage scoped to the identity of the assembly.</summary>
		// Token: 0x04003D91 RID: 15761
		Assembly = 4,
		/// <summary>The isolated store can be placed in a location on the file system that might roam (if roaming user data is enabled on the underlying operating system).</summary>
		// Token: 0x04003D92 RID: 15762
		Roaming = 8,
		/// <summary>Isolated storage scoped to the machine.</summary>
		// Token: 0x04003D93 RID: 15763
		Machine = 16,
		/// <summary>Isolated storage scoped to the application.</summary>
		// Token: 0x04003D94 RID: 15764
		Application = 32
	}
}
