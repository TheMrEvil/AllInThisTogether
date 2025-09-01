using System;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Provides memory allocation options for memory-mapped files.</summary>
	// Token: 0x02000335 RID: 821
	[Flags]
	[Serializable]
	public enum MemoryMappedFileOptions
	{
		/// <summary>No memory allocation options are applied.</summary>
		// Token: 0x04000BF6 RID: 3062
		None = 0,
		/// <summary>Memory allocation is delayed until a view is created with either the <see cref="M:System.IO.MemoryMappedFiles.MemoryMappedFile.CreateViewAccessor" /> or <see cref="M:System.IO.MemoryMappedFiles.MemoryMappedFile.CreateViewStream" /> method.</summary>
		// Token: 0x04000BF7 RID: 3063
		DelayAllocatePages = 67108864
	}
}
