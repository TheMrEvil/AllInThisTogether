using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the type of padding to apply when the message data block is shorter than the full number of bytes needed for a cryptographic operation.</summary>
	// Token: 0x02000481 RID: 1153
	[ComVisible(true)]
	[Serializable]
	public enum PaddingMode
	{
		/// <summary>No padding is done.</summary>
		// Token: 0x0400213A RID: 8506
		None = 1,
		/// <summary>The PKCS #7 padding string consists of a sequence of bytes, each of which is equal to the total number of padding bytes added.</summary>
		// Token: 0x0400213B RID: 8507
		PKCS7,
		/// <summary>The padding string consists of bytes set to zero.</summary>
		// Token: 0x0400213C RID: 8508
		Zeros,
		/// <summary>The ANSIX923 padding string consists of a sequence of bytes filled with zeros before the length.</summary>
		// Token: 0x0400213D RID: 8509
		ANSIX923,
		/// <summary>The ISO10126 padding string consists of random data before the length.</summary>
		// Token: 0x0400213E RID: 8510
		ISO10126
	}
}
