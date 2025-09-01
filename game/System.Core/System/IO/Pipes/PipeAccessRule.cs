using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an access rule for a pipe.</summary>
	// Token: 0x02000348 RID: 840
	public sealed class PipeAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x06001974 RID: 6516 RVA: 0x0005562E File Offset: 0x0005382E
		public PipeAccessRule(string identity, PipeAccessRights rights, AccessControlType type) : this(new NTAccount(identity), PipeAccessRule.AccessMaskFromRights(rights, type), false, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x06001975 RID: 6517 RVA: 0x00055645 File Offset: 0x00053845
		public PipeAccessRule(IdentityReference identity, PipeAccessRights rights, AccessControlType type) : this(identity, PipeAccessRule.AccessMaskFromRights(rights, type), false, type)
		{
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00055657 File Offset: 0x00053857
		internal PipeAccessRule(IdentityReference identity, int accessMask, bool isInherited, AccessControlType type) : base(identity, accessMask, isInherited, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAccessRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00055666 File Offset: 0x00053866
		public PipeAccessRights PipeAccessRights
		{
			get
			{
				return PipeAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00055674 File Offset: 0x00053874
		internal static int AccessMaskFromRights(PipeAccessRights rights, AccessControlType controlType)
		{
			if (rights < (PipeAccessRights)0 || rights > (PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity))
			{
				throw new ArgumentOutOfRangeException("rights", "Invalid PipeAccessRights value.");
			}
			if (controlType == AccessControlType.Allow)
			{
				rights |= PipeAccessRights.Synchronize;
			}
			else if (controlType == AccessControlType.Deny && rights != PipeAccessRights.FullControl)
			{
				rights &= ~PipeAccessRights.Synchronize;
			}
			return (int)rights;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000022A7 File Offset: 0x000004A7
		internal static PipeAccessRights RightsFromAccessMask(int accessMask)
		{
			return (PipeAccessRights)accessMask;
		}
	}
}
