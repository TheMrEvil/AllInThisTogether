using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access rule for a cryptographic key. An access rule represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An access rule object also contains information about the how the rule is inherited by child objects and how that inheritance is propagated.</summary>
	// Token: 0x0200051B RID: 1307
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x060033D4 RID: 13268 RVA: 0x000BE054 File Offset: 0x000BC254
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : base(identity, (int)cryptoKeyRights, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x060033D5 RID: 13269 RVA: 0x000BE062 File Offset: 0x000BC262
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(new NTAccount(identity), cryptoKeyRights, type)
		{
		}

		/// <summary>Gets the cryptographic key operation to which this access rule controls access.</summary>
		/// <returns>The cryptographic key operation to which this access rule controls access.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x000BC70A File Offset: 0x000BA90A
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return (CryptoKeyRights)base.AccessMask;
			}
		}
	}
}
