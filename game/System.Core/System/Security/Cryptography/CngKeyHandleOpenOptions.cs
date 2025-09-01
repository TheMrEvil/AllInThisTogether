using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options for opening key handles.</summary>
	// Token: 0x02000040 RID: 64
	[Flags]
	public enum CngKeyHandleOpenOptions
	{
		/// <summary>The key handle being opened does not specify an ephemeral key.</summary>
		// Token: 0x0400030B RID: 779
		None = 0,
		/// <summary>The key handle being opened specifies an ephemeral key.</summary>
		// Token: 0x0400030C RID: 780
		EphemeralKey = 1
	}
}
