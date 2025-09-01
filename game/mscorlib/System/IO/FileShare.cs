using System;

namespace System.IO
{
	/// <summary>Contains constants for controlling the kind of access other <see cref="T:System.IO.FileStream" /> objects can have to the same file.</summary>
	// Token: 0x02000B0A RID: 2826
	[Flags]
	public enum FileShare
	{
		/// <summary>Declines sharing of the current file. Any request to open the file (by this process or another process) will fail until the file is closed.</summary>
		// Token: 0x04003B45 RID: 15173
		None = 0,
		/// <summary>Allows subsequent opening of the file for reading. If this flag is not specified, any request to open the file for reading (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04003B46 RID: 15174
		Read = 1,
		/// <summary>Allows subsequent opening of the file for writing. If this flag is not specified, any request to open the file for writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04003B47 RID: 15175
		Write = 2,
		/// <summary>Allows subsequent opening of the file for reading or writing. If this flag is not specified, any request to open the file for reading or writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04003B48 RID: 15176
		ReadWrite = 3,
		/// <summary>Allows subsequent deleting of a file.</summary>
		// Token: 0x04003B49 RID: 15177
		Delete = 4,
		/// <summary>Makes the file handle inheritable by child processes. This is not directly supported by Win32.</summary>
		// Token: 0x04003B4A RID: 15178
		Inheritable = 16
	}
}
