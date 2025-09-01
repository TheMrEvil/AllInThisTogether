using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values that indicate whether a compression operation emphasizes speed or compression size.</summary>
	// Token: 0x02000543 RID: 1347
	public enum CompressionLevel
	{
		/// <summary>The compression operation should be optimally compressed, even if the operation takes a longer time to complete.</summary>
		// Token: 0x040017A5 RID: 6053
		Optimal,
		/// <summary>The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed.</summary>
		// Token: 0x040017A6 RID: 6054
		Fastest,
		/// <summary>No compression should be performed on the file.</summary>
		// Token: 0x040017A7 RID: 6055
		NoCompression
	}
}
