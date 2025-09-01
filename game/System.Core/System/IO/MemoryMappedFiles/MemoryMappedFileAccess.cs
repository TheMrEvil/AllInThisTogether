using System;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Specifies access capabilities and restrictions for a memory-mapped file or view. </summary>
	// Token: 0x02000334 RID: 820
	[Serializable]
	public enum MemoryMappedFileAccess
	{
		/// <summary>Read and write access to the file.</summary>
		// Token: 0x04000BEF RID: 3055
		ReadWrite,
		/// <summary>Read-only access to the file.</summary>
		// Token: 0x04000BF0 RID: 3056
		Read,
		/// <summary>Write-only access to file.</summary>
		// Token: 0x04000BF1 RID: 3057
		Write,
		/// <summary>Read and write access to the file, with the restriction that any write operations will not be seen by other processes. </summary>
		// Token: 0x04000BF2 RID: 3058
		CopyOnWrite,
		/// <summary>Read access to the file that can store and run executable code.</summary>
		// Token: 0x04000BF3 RID: 3059
		ReadExecute,
		/// <summary>Read and write access to the file that can can store and run executable code.</summary>
		// Token: 0x04000BF4 RID: 3060
		ReadWriteExecute
	}
}
