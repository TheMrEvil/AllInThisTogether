using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its software publisher's Authenticode X.509v3 certificate. This class cannot be inherited.</summary>
	// Token: 0x020003FD RID: 1021
	[Serializable]
	public sealed class PublisherMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> class with the Authenticode X.509v3 certificate that determines membership.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060029BA RID: 10682 RVA: 0x0000259F File Offset: 0x0000079F
		public PublisherMembershipCondition(X509Certificate certificate)
		{
		}

		/// <summary>Gets or sets the Authenticode X.509v3 certificate for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x00097E23 File Offset: 0x00096023
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x00097E2B File Offset: 0x0009602B
		public X509Certificate Certificate
		{
			[CompilerGenerated]
			get
			{
				return this.<Certificate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Certificate>k__BackingField = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029BD RID: 10685 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Check(Evidence evidence)
		{
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029BE RID: 10686 RVA: 0x0000270D File Offset: 0x0000090D
		public IMembershipCondition Copy()
		{
			return this;
		}

		/// <summary>Determines whether the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029BF RID: 10687 RVA: 0x00097E12 File Offset: 0x00096012
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x060029C0 RID: 10688 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void FromXml(SecurityElement e)
		{
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x060029C1 RID: 10689 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029C2 RID: 10690 RVA: 0x000930D0 File Offset: 0x000912D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029C3 RID: 10691 RVA: 0x00097E1B File Offset: 0x0009601B
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029C4 RID: 10692 RVA: 0x0000AF5E File Offset: 0x0000915E
		public SecurityElement ToXml()
		{
			return null;
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x060029C5 RID: 10693 RVA: 0x0000AF5E File Offset: 0x0000915E
		public SecurityElement ToXml(PolicyLevel level)
		{
			return null;
		}

		// Token: 0x04001F51 RID: 8017
		[CompilerGenerated]
		private X509Certificate <Certificate>k__BackingField;
	}
}
