using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an audit rule for a cryptographic key. An audit rule represents a combination of a user's identity and an access mask. An audit rule also contains information about the how the rule is inherited by child objects, how that inheritance is propagated, and for what conditions it is audited.</summary>
	// Token: 0x0200051C RID: 1308
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x060033D7 RID: 13271 RVA: 0x000BE072 File Offset: 0x000BC272
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : base(identity, (int)cryptoKeyRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x060033D8 RID: 13272 RVA: 0x000BE080 File Offset: 0x000BC280
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : this(new NTAccount(identity), cryptoKeyRights, flags)
		{
		}

		/// <summary>Gets the cryptographic key operation for which this audit rule generates audits.</summary>
		/// <returns>The cryptographic key operation for which this audit rule generates audits.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000BC70A File Offset: 0x000BA90A
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return (CryptoKeyRights)base.AccessMask;
			}
		}
	}
}
