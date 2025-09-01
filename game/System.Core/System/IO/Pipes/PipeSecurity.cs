using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Represents the access control and audit security for a pipe.</summary>
	// Token: 0x0200034E RID: 846
	public class PipeSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeSecurity" /> class.</summary>
		// Token: 0x0600198D RID: 6541 RVA: 0x0005595E File Offset: 0x00053B5E
		public PipeSecurity() : base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00055968 File Offset: 0x00053B68
		internal PipeSecurity(SafePipeHandle safeHandle, AccessControlSections includeSections) : base(false, ResourceType.KernelObject, safeHandle, includeSections)
		{
		}

		/// <summary>Adds an access rule to the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600198F RID: 6543 RVA: 0x00055974 File Offset: 0x00053B74
		public void AddAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.AddAccessRule(rule);
		}

		/// <summary>Sets an access rule in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001990 RID: 6544 RVA: 0x0005598B File Offset: 0x00053B8B
		public void SetAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.SetAccessRule(rule);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001991 RID: 6545 RVA: 0x000559A2 File Offset: 0x00053BA2
		public void ResetAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes an access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001992 RID: 6546 RVA: 0x000559BC File Offset: 0x00053BBC
		public bool RemoveAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				PipeAccessRule pipeAccessRule = accessRules[i] as PipeAccessRule;
				if (pipeAccessRule != null && pipeAccessRule.PipeAccessRights == rule.PipeAccessRights && pipeAccessRule.IdentityReference == rule.IdentityReference && pipeAccessRule.AccessControlType == rule.AccessControlType)
				{
					return base.RemoveAccessRule(rule);
				}
			}
			if (rule.PipeAccessRights != PipeAccessRights.FullControl)
			{
				return base.RemoveAccessRule(new PipeAccessRule(rule.IdentityReference, PipeAccessRule.AccessMaskFromRights(rule.PipeAccessRights, AccessControlType.Deny), false, rule.AccessControlType));
			}
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes the specified access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001993 RID: 6547 RVA: 0x00055A7C File Offset: 0x00053C7C
		public void RemoveAccessRuleSpecific(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				PipeAccessRule pipeAccessRule = accessRules[i] as PipeAccessRule;
				if (pipeAccessRule != null && pipeAccessRule.PipeAccessRights == rule.PipeAccessRights && pipeAccessRule.IdentityReference == rule.IdentityReference && pipeAccessRule.AccessControlType == rule.AccessControlType)
				{
					base.RemoveAccessRuleSpecific(rule);
					return;
				}
			}
			if (rule.PipeAccessRights != PipeAccessRights.FullControl)
			{
				base.RemoveAccessRuleSpecific(new PipeAccessRule(rule.IdentityReference, PipeAccessRule.AccessMaskFromRights(rule.PipeAccessRights, AccessControlType.Deny), false, rule.AccessControlType));
				return;
			}
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Adds an audit rule to the System Access Control List (SACL)that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001994 RID: 6548 RVA: 0x00055B3C File Offset: 0x00053D3C
		public void AddAuditRule(PipeAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Sets an audit rule in the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001995 RID: 6549 RVA: 0x00055B45 File Offset: 0x00053D45
		public void SetAuditRule(PipeAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Removes an audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <returns>
		///     <see langword="true" /> if the audit rule was removed; otherwise, <see langword="false" /></returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001996 RID: 6550 RVA: 0x00055B4E File Offset: 0x00053D4E
		public bool RemoveAuditRule(PipeAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001997 RID: 6551 RVA: 0x00055B57 File Offset: 0x00053D57
		public void RemoveAuditRuleAll(PipeAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001998 RID: 6552 RVA: 0x00055B60 File Offset: 0x00053D60
		public void RemoveAuditRuleSpecific(PipeAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">
		///       <see langword="true" /> if this rule is inherited from a parent container; otherwise false.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="identityReference" /> is <see langword="null" />. -or-
		///         <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001999 RID: 6553 RVA: 0x00055B69 File Offset: 0x00053D69
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			if (inheritanceFlags != InheritanceFlags.None)
			{
				throw new ArgumentException("This flag may not be set on a pipe.", "inheritanceFlags");
			}
			if (propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("This flag may not be set on a pipe.", "propagationFlags");
			}
			return new PipeAccessRule(identityReference, accessMask, isInherited, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">
		///       <see langword="true" /> if this rule is inherited from a parent container; otherwise, false..</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the valid access control type.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is <see langword="null" />. -or-The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600199A RID: 6554 RVA: 0x00055B9D File Offset: 0x00053D9D
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			if (inheritanceFlags != InheritanceFlags.None)
			{
				throw new ArgumentException("This flag may not be set on a pipe.", "inheritanceFlags");
			}
			if (propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("This flag may not be set on a pipe.", "propagationFlags");
			}
			return new PipeAuditRule(identityReference, accessMask, isInherited, flags);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00055BD4 File Offset: 0x00053DD4
		private AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections accessControlSections = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				accessControlSections = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				accessControlSections |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				accessControlSections |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				accessControlSections |= AccessControlSections.Group;
			}
			return accessControlSections;
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="handle">The handle of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x0600199C RID: 6556 RVA: 0x00055C14 File Offset: 0x00053E14
		protected internal void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(handle, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="name">The name of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x0600199D RID: 6557 RVA: 0x00055C74 File Offset: 0x00053E74
		protected internal void Persist(string name)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(name, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00055CD4 File Offset: 0x00053ED4
		public override Type AccessRightType
		{
			get
			{
				return typeof(PipeAccessRights);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00055CE0 File Offset: 0x00053EE0
		public override Type AccessRuleType
		{
			get
			{
				return typeof(PipeAccessRule);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00055CEC File Offset: 0x00053EEC
		public override Type AuditRuleType
		{
			get
			{
				return typeof(PipeAuditRule);
			}
		}
	}
}
