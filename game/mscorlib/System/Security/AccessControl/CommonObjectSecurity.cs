using System;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to objects without direct manipulation of access control lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class.</summary>
	// Token: 0x02000516 RID: 1302
	public abstract class CommonObjectSecurity : ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> class.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new object is a container object.</param>
		// Token: 0x060033A0 RID: 13216 RVA: 0x000BD83A File Offset: 0x000BBA3A
		protected CommonObjectSecurity(bool isContainer) : base(isContainer, false)
		{
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000BD844 File Offset: 0x000BBA44
		internal CommonObjectSecurity(CommonSecurityDescriptor securityDescriptor) : base(securityDescriptor)
		{
		}

		/// <summary>Gets a collection of the access rules associated with the specified security identifier.</summary>
		/// <param name="includeExplicit">
		///   <see langword="true" /> to include access rules explicitly set for the object.</param>
		/// <param name="includeInherited">
		///   <see langword="true" /> to include inherited access rules.</param>
		/// <param name="targetType">Specifies whether the security identifier for which to retrieve access rules is of type T:System.Security.Principal.SecurityIdentifier or type T:System.Security.Principal.NTAccount. The value of this parameter must be a type that can be translated to  the <see cref="T:System.Security.Principal.SecurityIdentifier" /> type.</param>
		/// <returns>The collection of access rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x060033A2 RID: 13218 RVA: 0x000BD84D File Offset: 0x000BBA4D
		public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAccessRules(includeExplicit, includeInherited, targetType);
		}

		/// <summary>Gets a collection of the audit rules associated with the specified security identifier.</summary>
		/// <param name="includeExplicit">
		///   <see langword="true" /> to include audit rules explicitly set for the object.</param>
		/// <param name="includeInherited">
		///   <see langword="true" /> to include inherited audit rules.</param>
		/// <param name="targetType">The security identifier for which to retrieve audit rules. This must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <returns>The collection of audit rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x060033A3 RID: 13219 RVA: 0x000BD858 File Offset: 0x000BBA58
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAuditRules(includeExplicit, includeInherited, targetType);
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x060033A4 RID: 13220 RVA: 0x000BD864 File Offset: 0x000BBA64
		protected void AddAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Add, rule, out flag);
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the access rule was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033A5 RID: 13221 RVA: 0x000BD87C File Offset: 0x000BBA7C
		protected bool RemoveAccessRule(AccessRule rule)
		{
			bool flag;
			return this.ModifyAccess(AccessControlModification.Remove, rule, out flag);
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060033A6 RID: 13222 RVA: 0x000BD894 File Offset: 0x000BBA94
		protected void RemoveAccessRuleAll(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveAll, rule, out flag);
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060033A7 RID: 13223 RVA: 0x000BD8AC File Offset: 0x000BBAAC
		protected void RemoveAccessRuleSpecific(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x060033A8 RID: 13224 RVA: 0x000BD8C4 File Offset: 0x000BBAC4
		protected void ResetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Reset, rule, out flag);
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x060033A9 RID: 13225 RVA: 0x000BD8DC File Offset: 0x000BBADC
		protected void SetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Set, rule, out flag);
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033AA RID: 13226 RVA: 0x000BD8F4 File Offset: 0x000BBAF4
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					break;
				case AccessControlModification.Set:
					this.descriptor.DiscretionaryAcl.SetAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_13D;
				case AccessControlModification.Reset:
					this.PurgeAccessRules(rule.IdentityReference);
					break;
				case AccessControlModification.Remove:
					modified = this.descriptor.DiscretionaryAcl.RemoveAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_13D;
				case AccessControlModification.RemoveAll:
					this.PurgeAccessRules(rule.IdentityReference);
					goto IL_13D;
				case AccessControlModification.RemoveSpecific:
					this.descriptor.DiscretionaryAcl.RemoveAccessSpecific(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_13D;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				this.descriptor.DiscretionaryAcl.AddAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
				IL_13D:
				if (modified)
				{
					base.AccessRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x060033AB RID: 13227 RVA: 0x000BDA70 File Offset: 0x000BBC70
		protected void AddAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Add, rule, out flag);
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the audit rule was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033AC RID: 13228 RVA: 0x000BDA88 File Offset: 0x000BBC88
		protected bool RemoveAuditRule(AuditRule rule)
		{
			bool flag;
			return this.ModifyAudit(AccessControlModification.Remove, rule, out flag);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060033AD RID: 13229 RVA: 0x000BDAA0 File Offset: 0x000BBCA0
		protected void RemoveAuditRuleAll(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveAll, rule, out flag);
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060033AE RID: 13230 RVA: 0x000BDAB8 File Offset: 0x000BBCB8
		protected void RemoveAuditRuleSpecific(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x060033AF RID: 13231 RVA: 0x000BDAD0 File Offset: 0x000BBCD0
		protected void SetAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Set, rule, out flag);
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033B0 RID: 13232 RVA: 0x000BDAE8 File Offset: 0x000BBCE8
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.AddAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Set:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.SetAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Reset:
					break;
				case AccessControlModification.Remove:
					if (this.descriptor.SystemAcl == null)
					{
						modified = false;
					}
					else
					{
						modified = this.descriptor.SystemAcl.RemoveAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				case AccessControlModification.RemoveAll:
					this.PurgeAuditRules(rule.IdentityReference);
					break;
				case AccessControlModification.RemoveSpecific:
					if (this.descriptor.SystemAcl != null)
					{
						this.descriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				if (modified)
				{
					base.AuditRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}
	}
}
