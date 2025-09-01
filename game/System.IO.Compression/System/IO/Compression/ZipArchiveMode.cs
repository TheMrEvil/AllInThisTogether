using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values for interacting with zip archive entries.</summary>
	// Token: 0x0200002F RID: 47
	public enum ZipArchiveMode
	{
		/// <summary>Only reading archive entries is permitted.</summary>
		// Token: 0x040001AE RID: 430
		Read,
		/// <summary>Only creating new archive entries is permitted.</summary>
		// Token: 0x040001AF RID: 431
		Create,
		/// <summary>Both read and write operations are permitted for archive entries.</summary>
		// Token: 0x040001B0 RID: 432
		Update
	}
}
