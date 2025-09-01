using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm group. </summary>
	// Token: 0x0200003F RID: 63
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithmGroup : IEquatable<CngAlgorithmGroup>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> class.</summary>
		/// <param name="algorithmGroup">The name of the algorithm group to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmGroup" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithmGroup" /> parameter length is 0 (zero).</exception>
		// Token: 0x060000F1 RID: 241 RVA: 0x0000319C File Offset: 0x0000139C
		public CngAlgorithmGroup(string algorithmGroup)
		{
			if (algorithmGroup == null)
			{
				throw new ArgumentNullException("algorithmGroup");
			}
			if (algorithmGroup.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The algorithm group '{0}' is invalid.", new object[]
				{
					algorithmGroup
				}), "algorithmGroup");
			}
			this.m_algorithmGroup = algorithmGroup;
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000031EB File Offset: 0x000013EB
		public string AlgorithmGroup
		{
			get
			{
				return this.m_algorithmGroup;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects specify the same algorithm group.</summary>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects specify the same algorithm group; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000F3 RID: 243 RVA: 0x000031F3 File Offset: 0x000013F3
		public static bool operator ==(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects do not specify the same algorithm group.</summary>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		/// <returns>
		///     <see langword="true" /> if the two objects do not specify the same algorithm group; otherwise, <see langword="false" />. </returns>
		// Token: 0x060000F4 RID: 244 RVA: 0x00003204 File Offset: 0x00001404
		public static bool operator !=(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> that specifies the same algorithm group as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000F5 RID: 245 RVA: 0x00003218 File Offset: 0x00001418
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithmGroup);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="other" /> parameter specifies the same algorithm group as the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000F6 RID: 246 RVA: 0x00003226 File Offset: 0x00001426
		public bool Equals(CngAlgorithmGroup other)
		{
			return other != null && this.m_algorithmGroup.Equals(other.AlgorithmGroup);
		}

		/// <summary>Generates a hash value for the algorithm group name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm group name.</returns>
		// Token: 0x060000F7 RID: 247 RVA: 0x0000323E File Offset: 0x0000143E
		public override int GetHashCode()
		{
			return this.m_algorithmGroup.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x060000F8 RID: 248 RVA: 0x000031EB File Offset: 0x000013EB
		public override string ToString()
		{
			return this.m_algorithmGroup;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Diffie-Hellman family of algorithms.</summary>
		/// <returns>An object that specifies the Diffie-Hellman family of algorithms.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000324B File Offset: 0x0000144B
		public static CngAlgorithmGroup DiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_dh == null)
				{
					CngAlgorithmGroup.s_dh = new CngAlgorithmGroup("DH");
				}
				return CngAlgorithmGroup.s_dh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Digital Signature Algorithm (DSA) family of algorithms.</summary>
		/// <returns>An object that specifies the DSA family of algorithms.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003274 File Offset: 0x00001474
		public static CngAlgorithmGroup Dsa
		{
			get
			{
				if (CngAlgorithmGroup.s_dsa == null)
				{
					CngAlgorithmGroup.s_dsa = new CngAlgorithmGroup("DSA");
				}
				return CngAlgorithmGroup.s_dsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Diffie-Hellman (ECDH) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDH family of algorithms.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000329D File Offset: 0x0000149D
		public static CngAlgorithmGroup ECDiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdh == null)
				{
					CngAlgorithmGroup.s_ecdh = new CngAlgorithmGroup("ECDH");
				}
				return CngAlgorithmGroup.s_ecdh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Digital Signature Algorithm (ECDSA) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDSA family of algorithms.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000032C6 File Offset: 0x000014C6
		public static CngAlgorithmGroup ECDsa
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdsa == null)
				{
					CngAlgorithmGroup.s_ecdsa = new CngAlgorithmGroup("ECDSA");
				}
				return CngAlgorithmGroup.s_ecdsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Rivest-Shamir-Adleman (RSA) family of algorithms.</summary>
		/// <returns>An object that specifies the RSA family of algorithms.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000032EF File Offset: 0x000014EF
		public static CngAlgorithmGroup Rsa
		{
			get
			{
				if (CngAlgorithmGroup.s_rsa == null)
				{
					CngAlgorithmGroup.s_rsa = new CngAlgorithmGroup("RSA");
				}
				return CngAlgorithmGroup.s_rsa;
			}
		}

		// Token: 0x04000304 RID: 772
		private static volatile CngAlgorithmGroup s_dh;

		// Token: 0x04000305 RID: 773
		private static volatile CngAlgorithmGroup s_dsa;

		// Token: 0x04000306 RID: 774
		private static volatile CngAlgorithmGroup s_ecdh;

		// Token: 0x04000307 RID: 775
		private static volatile CngAlgorithmGroup s_ecdsa;

		// Token: 0x04000308 RID: 776
		private static volatile CngAlgorithmGroup s_rsa;

		// Token: 0x04000309 RID: 777
		private string m_algorithmGroup;
	}
}
