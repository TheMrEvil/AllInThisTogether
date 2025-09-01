using System;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the elliptic curve cryptography (ECC) algorithm.</summary>
	// Token: 0x02000035 RID: 53
	public struct ECParameters
	{
		/// <summary>Validates the current object.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key or curve parameters are not valid for the current curve type.</exception>
		// Token: 0x060000B9 RID: 185 RVA: 0x00002868 File Offset: 0x00000A68
		public void Validate()
		{
			bool flag = false;
			if (this.Q.X == null || this.Q.Y == null || this.Q.X.Length != this.Q.Y.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.Curve.IsExplicit)
				{
					flag = (this.D != null && this.D.Length != this.Curve.Order.Length);
				}
				else if (this.Curve.IsNamed)
				{
					flag = (this.D != null && this.D.Length != this.Q.X.Length);
				}
			}
			if (flag)
			{
				throw new CryptographicException("The specified key parameters are not valid. Q.X and Q.Y are required fields. Q.X, Q.Y must be the same length. If D is specified it must be the same length as Q.X and Q.Y for named curves or the same length as Order for explicit curves.");
			}
			this.Curve.Validate();
		}

		/// <summary>Represents the public key <see langword="Q" /> for the elliptic curve cryptography (ECC) algorithm.</summary>
		/// <returns>The <see langword="Q" /> parameter for the elliptic curve cryptography (ECC) algorithm.</returns>
		// Token: 0x040002D0 RID: 720
		public ECPoint Q;

		/// <summary>Represents the private key <see langword="D" /> for the elliptic curve cryptography (ECC) algorithm, stored in big-endian format.</summary>
		/// <returns>The <see langword="D" /> parameter for the elliptic curve cryptography (ECC) algorithm.</returns>
		// Token: 0x040002D1 RID: 721
		public byte[] D;

		/// <summary>Represents the curve associated with the public key (<see cref="F:System.Security.Cryptography.ECParameters.Q" />) and the optional private key (<see cref="F:System.Security.Cryptography.ECParameters.D" />).</summary>
		/// <returns>The curve.</returns>
		// Token: 0x040002D2 RID: 722
		public ECCurve Curve;
	}
}
