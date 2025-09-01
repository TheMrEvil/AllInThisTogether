using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm. </summary>
	// Token: 0x0200003E RID: 62
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithm : IEquatable<CngAlgorithm>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithm" /> class.</summary>
		/// <param name="algorithm">The name of the algorithm to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithm" /> parameter length is 0 (zero).</exception>
		// Token: 0x060000DB RID: 219 RVA: 0x00002EAC File Offset: 0x000010AC
		public CngAlgorithm(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (algorithm.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The algorithm name '{0}' is invalid.", new object[]
				{
					algorithm
				}), "algorithm");
			}
			this.m_algorithm = algorithm;
		}

		/// <summary>Gets the algorithm name that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002EFB File Offset: 0x000010FB
		public string Algorithm
		{
			get
			{
				return this.m_algorithm;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects specify the same algorithm name.</summary>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects specify the same algorithm name; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000DD RID: 221 RVA: 0x00002F03 File Offset: 0x00001103
		public static bool operator ==(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects do not specify the same algorithm.</summary>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects do not specify the same algorithm name; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000DE RID: 222 RVA: 0x00002F14 File Offset: 0x00001114
		public static bool operator !=(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> that specifies the same algorithm as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000DF RID: 223 RVA: 0x00002F28 File Offset: 0x00001128
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithm);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object. </summary>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="other" /> parameter specifies the same algorithm as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000E0 RID: 224 RVA: 0x00002F36 File Offset: 0x00001136
		public bool Equals(CngAlgorithm other)
		{
			return other != null && this.m_algorithm.Equals(other.Algorithm);
		}

		/// <summary>Generates a hash value for the algorithm name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm name.</returns>
		// Token: 0x060000E1 RID: 225 RVA: 0x00002F4E File Offset: 0x0000114E
		public override int GetHashCode()
		{
			return this.m_algorithm.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x060000E2 RID: 226 RVA: 0x00002EFB File Offset: 0x000010FB
		public override string ToString()
		{
			return this.m_algorithm;
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the RSA hash algorithm.</summary>
		/// <returns>An object that specifies the RSA algorithm.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002F5B File Offset: 0x0000115B
		public static CngAlgorithm Rsa
		{
			get
			{
				if (CngAlgorithm.s_rsa == null)
				{
					CngAlgorithm.s_rsa = new CngAlgorithm("RSA");
				}
				return CngAlgorithm.s_rsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm whose curve is described via a key property.</summary>
		/// <returns>An object that specifies an ECDH key exchange algorithm whose curve is described via a key property.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002F84 File Offset: 0x00001184
		public static CngAlgorithm ECDiffieHellman
		{
			get
			{
				if (CngAlgorithm.s_ecdh == null)
				{
					CngAlgorithm.s_ecdh = new CngAlgorithm("ECDH");
				}
				return CngAlgorithm.s_ecdh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-256 curve.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002FAD File Offset: 0x000011AD
		public static CngAlgorithm ECDiffieHellmanP256
		{
			get
			{
				if (CngAlgorithm.s_ecdhp256 == null)
				{
					CngAlgorithm.s_ecdhp256 = new CngAlgorithm("ECDH_P256");
				}
				return CngAlgorithm.s_ecdhp256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-384 curve.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002FD6 File Offset: 0x000011D6
		public static CngAlgorithm ECDiffieHellmanP384
		{
			get
			{
				if (CngAlgorithm.s_ecdhp384 == null)
				{
					CngAlgorithm.s_ecdhp384 = new CngAlgorithm("ECDH_P384");
				}
				return CngAlgorithm.s_ecdhp384;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-521 curve.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002FFF File Offset: 0x000011FF
		public static CngAlgorithm ECDiffieHellmanP521
		{
			get
			{
				if (CngAlgorithm.s_ecdhp521 == null)
				{
					CngAlgorithm.s_ecdhp521 = new CngAlgorithm("ECDH_P521");
				}
				return CngAlgorithm.s_ecdhp521;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) whose curve is described via a key property.</summary>
		/// <returns>An object that specifies an ECDSA whose curve is described via a key property.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003028 File Offset: 0x00001228
		public static CngAlgorithm ECDsa
		{
			get
			{
				if (CngAlgorithm.s_ecdsa == null)
				{
					CngAlgorithm.s_ecdsa = new CngAlgorithm("ECDSA");
				}
				return CngAlgorithm.s_ecdsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-256 curve.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003051 File Offset: 0x00001251
		public static CngAlgorithm ECDsaP256
		{
			get
			{
				if (CngAlgorithm.s_ecdsap256 == null)
				{
					CngAlgorithm.s_ecdsap256 = new CngAlgorithm("ECDSA_P256");
				}
				return CngAlgorithm.s_ecdsap256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-384 curve.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000307A File Offset: 0x0000127A
		public static CngAlgorithm ECDsaP384
		{
			get
			{
				if (CngAlgorithm.s_ecdsap384 == null)
				{
					CngAlgorithm.s_ecdsap384 = new CngAlgorithm("ECDSA_P384");
				}
				return CngAlgorithm.s_ecdsap384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-521 curve.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000030A3 File Offset: 0x000012A3
		public static CngAlgorithm ECDsaP521
		{
			get
			{
				if (CngAlgorithm.s_ecdsap521 == null)
				{
					CngAlgorithm.s_ecdsap521 = new CngAlgorithm("ECDSA_P521");
				}
				return CngAlgorithm.s_ecdsap521;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Message Digest 5 (MD5) hash algorithm.</summary>
		/// <returns>An object that specifies the MD5 algorithm.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000030CC File Offset: 0x000012CC
		public static CngAlgorithm MD5
		{
			get
			{
				if (CngAlgorithm.s_md5 == null)
				{
					CngAlgorithm.s_md5 = new CngAlgorithm("MD5");
				}
				return CngAlgorithm.s_md5;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 1 (SHA-1) algorithm.</summary>
		/// <returns>An object that specifies the SHA-1 algorithm.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000030F5 File Offset: 0x000012F5
		public static CngAlgorithm Sha1
		{
			get
			{
				if (CngAlgorithm.s_sha1 == null)
				{
					CngAlgorithm.s_sha1 = new CngAlgorithm("SHA1");
				}
				return CngAlgorithm.s_sha1;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 256 (SHA-256) algorithm.</summary>
		/// <returns>An object that specifies the SHA-256 algorithm.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000311E File Offset: 0x0000131E
		public static CngAlgorithm Sha256
		{
			get
			{
				if (CngAlgorithm.s_sha256 == null)
				{
					CngAlgorithm.s_sha256 = new CngAlgorithm("SHA256");
				}
				return CngAlgorithm.s_sha256;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 384 (SHA-384) algorithm.</summary>
		/// <returns>An object that specifies the SHA-384 algorithm.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003147 File Offset: 0x00001347
		public static CngAlgorithm Sha384
		{
			get
			{
				if (CngAlgorithm.s_sha384 == null)
				{
					CngAlgorithm.s_sha384 = new CngAlgorithm("SHA384");
				}
				return CngAlgorithm.s_sha384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 512 (SHA-512) algorithm.</summary>
		/// <returns>An object that specifies the SHA-512 algorithm.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003170 File Offset: 0x00001370
		public static CngAlgorithm Sha512
		{
			get
			{
				if (CngAlgorithm.s_sha512 == null)
				{
					CngAlgorithm.s_sha512 = new CngAlgorithm("SHA512");
				}
				return CngAlgorithm.s_sha512;
			}
		}

		// Token: 0x040002F5 RID: 757
		private static volatile CngAlgorithm s_ecdh;

		// Token: 0x040002F6 RID: 758
		private static volatile CngAlgorithm s_ecdhp256;

		// Token: 0x040002F7 RID: 759
		private static volatile CngAlgorithm s_ecdhp384;

		// Token: 0x040002F8 RID: 760
		private static volatile CngAlgorithm s_ecdhp521;

		// Token: 0x040002F9 RID: 761
		private static volatile CngAlgorithm s_ecdsa;

		// Token: 0x040002FA RID: 762
		private static volatile CngAlgorithm s_ecdsap256;

		// Token: 0x040002FB RID: 763
		private static volatile CngAlgorithm s_ecdsap384;

		// Token: 0x040002FC RID: 764
		private static volatile CngAlgorithm s_ecdsap521;

		// Token: 0x040002FD RID: 765
		private static volatile CngAlgorithm s_md5;

		// Token: 0x040002FE RID: 766
		private static volatile CngAlgorithm s_sha1;

		// Token: 0x040002FF RID: 767
		private static volatile CngAlgorithm s_sha256;

		// Token: 0x04000300 RID: 768
		private static volatile CngAlgorithm s_sha384;

		// Token: 0x04000301 RID: 769
		private static volatile CngAlgorithm s_sha512;

		// Token: 0x04000302 RID: 770
		private static volatile CngAlgorithm s_rsa;

		// Token: 0x04000303 RID: 771
		private string m_algorithm;
	}
}
