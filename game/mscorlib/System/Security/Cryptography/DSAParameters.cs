using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains the typical parameters for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
	// Token: 0x0200048A RID: 1162
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		/// <summary>Specifies the <see langword="P" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04002159 RID: 8537
		public byte[] P;

		/// <summary>Specifies the <see langword="Q" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215A RID: 8538
		public byte[] Q;

		/// <summary>Specifies the <see langword="G" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215B RID: 8539
		public byte[] G;

		/// <summary>Specifies the <see langword="Y" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215C RID: 8540
		public byte[] Y;

		/// <summary>Specifies the <see langword="J" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215D RID: 8541
		public byte[] J;

		/// <summary>Specifies the <see langword="X" /> parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215E RID: 8542
		[NonSerialized]
		public byte[] X;

		/// <summary>Specifies the seed for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400215F RID: 8543
		public byte[] Seed;

		/// <summary>Specifies the counter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04002160 RID: 8544
		public int Counter;
	}
}
