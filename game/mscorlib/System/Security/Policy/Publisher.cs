using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	/// <summary>Provides the Authenticode X.509v3 digital signature of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x020003FC RID: 1020
	[Serializable]
	public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Publisher" /> class with the Authenticode X.509v3 certificate containing the publisher's public key.</summary>
		/// <param name="cert">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060029B3 RID: 10675 RVA: 0x00097E0A File Offset: 0x0009600A
		public Publisher(X509Certificate cert)
		{
		}

		/// <summary>Gets the publisher's Authenticode X.509v3 certificate.</summary>
		/// <returns>The publisher's <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x0000AF5E File Offset: 0x0000915E
		public X509Certificate Certificate
		{
			get
			{
				return null;
			}
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x060029B5 RID: 10677 RVA: 0x0000AF5E File Offset: 0x0000915E
		public object Copy()
		{
			return null;
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Publisher" /> class.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x060029B6 RID: 10678 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return null;
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Publisher" /> to the specified object for equivalence.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.Publisher" /> to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances of the <see cref="T:System.Security.Policy.Publisher" /> class are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Publisher" /> object.</exception>
		// Token: 0x060029B7 RID: 10679 RVA: 0x00097E12 File Offset: 0x00096012
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Gets the hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</summary>
		/// <returns>The hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</returns>
		// Token: 0x060029B8 RID: 10680 RVA: 0x000930D0 File Offset: 0x000912D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x060029B9 RID: 10681 RVA: 0x00097E1B File Offset: 0x0009601B
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
