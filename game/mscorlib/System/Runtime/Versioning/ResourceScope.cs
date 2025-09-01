using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the scope of a sharable resource.</summary>
	// Token: 0x02000641 RID: 1601
	[Flags]
	public enum ResourceScope
	{
		/// <summary>There is no shared state.</summary>
		// Token: 0x040026FA RID: 9978
		None = 0,
		/// <summary>The state is shared by objects within the machine.</summary>
		// Token: 0x040026FB RID: 9979
		Machine = 1,
		/// <summary>The state is shared within a process.</summary>
		// Token: 0x040026FC RID: 9980
		Process = 2,
		/// <summary>The state is shared by objects within an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x040026FD RID: 9981
		AppDomain = 4,
		/// <summary>The state is shared by objects within a library.</summary>
		// Token: 0x040026FE RID: 9982
		Library = 8,
		/// <summary>The resource is visible to only the type.</summary>
		// Token: 0x040026FF RID: 9983
		Private = 16,
		/// <summary>The resource is visible at an assembly scope.</summary>
		// Token: 0x04002700 RID: 9984
		Assembly = 32
	}
}
