using System;

namespace System.Security.Cryptography
{
	/// <summary>Represents a (X,Y) coordinate pair for elliptic curve cryptography (ECC) structures.</summary>
	// Token: 0x02000036 RID: 54
	public struct ECPoint
	{
		/// <summary>Represents the X coordinate.</summary>
		/// <returns>The X coordinate.</returns>
		// Token: 0x040002D3 RID: 723
		public byte[] X;

		/// <summary>Represents the Y coordinate.</summary>
		/// <returns>The Y coordinate.</returns>
		// Token: 0x040002D4 RID: 724
		public byte[] Y;
	}
}
