using System;

namespace System.IO
{
	/// <summary>Specifies whether to search the current directory, or the current directory and all subdirectories.</summary>
	// Token: 0x02000B3F RID: 2879
	public enum SearchOption
	{
		/// <summary>Includes only the current directory in a search operation.</summary>
		// Token: 0x04003C81 RID: 15489
		TopDirectoryOnly,
		/// <summary>Includes the current directory and all its subdirectories in a search operation. This option includes reparse points such as mounted drives and symbolic links in the search.</summary>
		// Token: 0x04003C82 RID: 15490
		AllDirectories
	}
}
