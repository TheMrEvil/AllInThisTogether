using System;

namespace System.IO
{
	/// <summary>Provides attributes for files and directories.</summary>
	// Token: 0x02000B49 RID: 2889
	[Flags]
	public enum FileAttributes
	{
		/// <summary>The file is read-only.</summary>
		// Token: 0x04003CC1 RID: 15553
		ReadOnly = 1,
		/// <summary>The file is hidden, and thus is not included in an ordinary directory listing.</summary>
		// Token: 0x04003CC2 RID: 15554
		Hidden = 2,
		/// <summary>The file is a system file. That is, the file is part of the operating system or is used exclusively by the operating system.</summary>
		// Token: 0x04003CC3 RID: 15555
		System = 4,
		/// <summary>The file is a directory.</summary>
		// Token: 0x04003CC4 RID: 15556
		Directory = 16,
		/// <summary>The file is a candidate for backup or removal.</summary>
		// Token: 0x04003CC5 RID: 15557
		Archive = 32,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04003CC6 RID: 15558
		Device = 64,
		/// <summary>The file is a standard file that has no special attributes. This attribute is valid only if it is used alone.</summary>
		// Token: 0x04003CC7 RID: 15559
		Normal = 128,
		/// <summary>The file is temporary. A temporary file contains data that is needed while an application is executing but is not needed after the application is finished. File systems try to keep all the data in memory for quicker access rather than flushing the data back to mass storage. A temporary file should be deleted by the application as soon as it is no longer needed.</summary>
		// Token: 0x04003CC8 RID: 15560
		Temporary = 256,
		/// <summary>The file is a sparse file. Sparse files are typically large files whose data consists of mostly zeros.</summary>
		// Token: 0x04003CC9 RID: 15561
		SparseFile = 512,
		/// <summary>The file contains a reparse point, which is a block of user-defined data associated with a file or a directory.</summary>
		// Token: 0x04003CCA RID: 15562
		ReparsePoint = 1024,
		/// <summary>The file is compressed.</summary>
		// Token: 0x04003CCB RID: 15563
		Compressed = 2048,
		/// <summary>The file is offline. The data of the file is not immediately available.</summary>
		// Token: 0x04003CCC RID: 15564
		Offline = 4096,
		/// <summary>The file will not be indexed by the operating system's content indexing service.</summary>
		// Token: 0x04003CCD RID: 15565
		NotContentIndexed = 8192,
		/// <summary>The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and directories.</summary>
		// Token: 0x04003CCE RID: 15566
		Encrypted = 16384,
		/// <summary>The file or directory includes data integrity support. When this value is applied to a file, all data streams in the file have integrity support. When this value is applied to a directory, all new files and subdirectories within that directory, by default, include integrity support.</summary>
		// Token: 0x04003CCF RID: 15567
		IntegrityStream = 32768,
		/// <summary>The file or directory is excluded from the data integrity scan. When this value is applied to a directory, by default, all new files and subdirectories within that directory are excluded from data integrity.</summary>
		// Token: 0x04003CD0 RID: 15568
		NoScrubData = 131072
	}
}
