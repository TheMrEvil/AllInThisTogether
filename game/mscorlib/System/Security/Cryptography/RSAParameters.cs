using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	// Token: 0x020004A9 RID: 1193
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		/// <summary>Represents the <see langword="Exponent" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021B8 RID: 8632
		public byte[] Exponent;

		/// <summary>Represents the <see langword="Modulus" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021B9 RID: 8633
		public byte[] Modulus;

		/// <summary>Represents the <see langword="P" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BA RID: 8634
		[NonSerialized]
		public byte[] P;

		/// <summary>Represents the <see langword="Q" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BB RID: 8635
		[NonSerialized]
		public byte[] Q;

		/// <summary>Represents the <see langword="DP" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BC RID: 8636
		[NonSerialized]
		public byte[] DP;

		/// <summary>Represents the <see langword="DQ" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BD RID: 8637
		[NonSerialized]
		public byte[] DQ;

		/// <summary>Represents the <see langword="InverseQ" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BE RID: 8638
		[NonSerialized]
		public byte[] InverseQ;

		/// <summary>Represents the <see langword="D" /> parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x040021BF RID: 8639
		[NonSerialized]
		public byte[] D;
	}
}
