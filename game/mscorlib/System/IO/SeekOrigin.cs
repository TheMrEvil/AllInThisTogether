using System;

namespace System.IO
{
	/// <summary>Specifies the position in a stream to use for seeking.</summary>
	// Token: 0x02000B10 RID: 2832
	public enum SeekOrigin
	{
		/// <summary>Specifies the beginning of a stream.</summary>
		// Token: 0x04003B6A RID: 15210
		Begin,
		/// <summary>Specifies the current position within a stream.</summary>
		// Token: 0x04003B6B RID: 15211
		Current,
		/// <summary>Specifies the end of a stream.</summary>
		// Token: 0x04003B6C RID: 15212
		End
	}
}
