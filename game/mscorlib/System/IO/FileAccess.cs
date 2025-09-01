using System;

namespace System.IO
{
	/// <summary>Defines constants for read, write, or read/write access to a file.</summary>
	// Token: 0x02000B05 RID: 2821
	[Flags]
	public enum FileAccess
	{
		/// <summary>Read access to the file. Data can be read from the file. Combine with <see langword="Write" /> for read/write access.</summary>
		// Token: 0x04003B2E RID: 15150
		Read = 1,
		/// <summary>Write access to the file. Data can be written to the file. Combine with <see langword="Read" /> for read/write access.</summary>
		// Token: 0x04003B2F RID: 15151
		Write = 2,
		/// <summary>Read and write access to the file. Data can be written to and read from the file.</summary>
		// Token: 0x04003B30 RID: 15152
		ReadWrite = 3
	}
}
