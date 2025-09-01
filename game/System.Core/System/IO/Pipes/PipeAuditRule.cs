using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an audit rule for a pipe.</summary>
	// Token: 0x02000349 RID: 841
	public sealed class PipeAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a user account specified in a <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x0600197A RID: 6522 RVA: 0x000556C1 File Offset: 0x000538C1
		public PipeAuditRule(IdentityReference identity, PipeAccessRights rights, AuditFlags flags) : this(identity, PipeAuditRule.AccessMaskFromRights(rights), false, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a named user account.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x0600197B RID: 6523 RVA: 0x000556D2 File Offset: 0x000538D2
		public PipeAuditRule(string identity, PipeAccessRights rights, AuditFlags flags) : this(new NTAccount(identity), PipeAuditRule.AccessMaskFromRights(rights), false, flags)
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000556E8 File Offset: 0x000538E8
		internal PipeAuditRule(IdentityReference identity, int accessMask, bool isInherited, AuditFlags flags) : base(identity, accessMask, isInherited, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000556F7 File Offset: 0x000538F7
		private static int AccessMaskFromRights(PipeAccessRights rights)
		{
			if (rights < (PipeAccessRights)0 || rights > (PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity))
			{
				throw new ArgumentOutOfRangeException("rights", "Invalid PipeAccessRights value.");
			}
			return (int)rights;
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAuditRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values. </returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00055666 File Offset: 0x00053866
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return PipeAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}
	}
}
