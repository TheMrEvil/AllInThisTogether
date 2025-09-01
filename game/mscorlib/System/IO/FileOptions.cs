using System;

namespace System.IO
{
	/// <summary>Represents advanced options for creating a <see cref="T:System.IO.FileStream" /> object.</summary>
	// Token: 0x02000B09 RID: 2825
	[Flags]
	public enum FileOptions
	{
		/// <summary>Indicates that no additional options should be used when creating a <see cref="T:System.IO.FileStream" /> object.</summary>
		// Token: 0x04003B3D RID: 15165
		None = 0,
		/// <summary>Indicates that the system should write through any intermediate cache and go directly to disk.</summary>
		// Token: 0x04003B3E RID: 15166
		WriteThrough = -2147483648,
		/// <summary>Indicates that a file can be used for asynchronous reading and writing.</summary>
		// Token: 0x04003B3F RID: 15167
		Asynchronous = 1073741824,
		/// <summary>Indicates that the file is accessed randomly. The system can use this as a hint to optimize file caching.</summary>
		// Token: 0x04003B40 RID: 15168
		RandomAccess = 268435456,
		/// <summary>Indicates that a file is automatically deleted when it is no longer in use.</summary>
		// Token: 0x04003B41 RID: 15169
		DeleteOnClose = 67108864,
		/// <summary>Indicates that the file is to be accessed sequentially from beginning to end. The system can use this as a hint to optimize file caching. If an application moves the file pointer for random access, optimum caching may not occur; however, correct operation is still guaranteed.</summary>
		// Token: 0x04003B42 RID: 15170
		SequentialScan = 134217728,
		/// <summary>Indicates that a file is encrypted and can be decrypted only by using the same user account used for encryption.</summary>
		// Token: 0x04003B43 RID: 15171
		Encrypted = 16384
	}
}
