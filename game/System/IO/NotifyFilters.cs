using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	// Token: 0x02000523 RID: 1315
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x040016E6 RID: 5862
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x040016E7 RID: 5863
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x040016E8 RID: 5864
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x040016E9 RID: 5865
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x040016EA RID: 5866
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x040016EB RID: 5867
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x040016EC RID: 5868
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x040016ED RID: 5869
		Size = 8
	}
}
