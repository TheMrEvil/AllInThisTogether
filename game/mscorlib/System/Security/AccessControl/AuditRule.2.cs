using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity and an access mask.</summary>
	/// <typeparam name="T">The type of the audit rule.</typeparam>
	// Token: 0x0200050B RID: 1291
	public class AuditRule<T> : AuditRule where T : struct
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule`1" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="flags">The properties of the audit rule.</param>
		// Token: 0x0600334B RID: 13131 RVA: 0x000BC916 File Offset: 0x000BAB16
		public AuditRule(string identity, T rights, AuditFlags flags) : this(new NTAccount(identity), rights, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule`1" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which this audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="flags">The conditions for which the rule is audited.</param>
		// Token: 0x0600334C RID: 13132 RVA: 0x000BC926 File Offset: 0x000BAB26
		public AuditRule(IdentityReference identity, T rights, AuditFlags flags) : this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule`1" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Whether inherited audit rules are automatically propagated.</param>
		/// <param name="flags">The conditions for which the rule is audited.</param>
		// Token: 0x0600334D RID: 13133 RVA: 0x000BC933 File Offset: 0x000BAB33
		public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new NTAccount(identity), rights, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule`1" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Whether inherited audit rules are automatically propagated.</param>
		/// <param name="flags">The conditions for which the rule is audited.</param>
		// Token: 0x0600334E RID: 13134 RVA: 0x000BC947 File Offset: 0x000BAB47
		public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000BC737 File Offset: 0x000BA937
		internal AuditRule(IdentityReference identity, int rights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, rights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Gets the rights of the audit rule.</summary>
		/// <returns>The rights of the audit rule.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000BC860 File Offset: 0x000BAA60
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
