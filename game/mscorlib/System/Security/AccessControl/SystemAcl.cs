using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a System Access Control List (SACL).</summary>
	// Token: 0x0200054B RID: 1355
	public sealed class SystemAcl : CommonAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.SystemAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x06003594 RID: 13716 RVA: 0x000BE74C File Offset: 0x000BC94C
		public SystemAcl(bool isContainer, bool isDS, int capacity) : base(isContainer, isDS, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values from the specified <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Specify <see langword="null" /> to create an empty ACL.</param>
		// Token: 0x06003595 RID: 13717 RVA: 0x000BE757 File Offset: 0x000BC957
		public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl) : base(isContainer, isDS, rawAcl)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="revision">The revision level of the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.SystemAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x06003596 RID: 13718 RVA: 0x000BE762 File Offset: 0x000BC962
		public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity) : base(isContainer, isDS, revision, capacity)
		{
		}

		/// <summary>Adds an audit rule to the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <param name="auditFlags">The type of audit rule to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		// Token: 0x06003597 RID: 13719 RVA: 0x000C1D4A File Offset: 0x000BFF4A
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.AddAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Adds an audit rule with the specified settings to the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the new audit rule.</summary>
		/// <param name="auditFlags">The type of audit rule to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new audit rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new audit rule.</param>
		// Token: 0x06003598 RID: 13720 RVA: 0x000C1D5C File Offset: 0x000BFF5C
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.AddAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Adds an audit rule to the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an audit rule.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> for the new audit rule.</param>
		// Token: 0x06003599 RID: 13721 RVA: 0x000C1D80 File Offset: 0x000BFF80
		public void AddAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <returns>
		///   <see langword="true" /> if this method successfully removes the specified audit rule; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600359A RID: 13722 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed audit control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed audit rule.</param>
		/// <returns>
		///   <see langword="true" /> if this method successfully removes the specified audit rule; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600359B RID: 13723 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> for which to remove an audit rule.</param>
		/// <returns>
		///   <see langword="true" /> if this method successfully removes the specified audit rule; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600359C RID: 13724 RVA: 0x000C1DC0 File Offset: 0x000BFFC0
		public bool RemoveAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			return this.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		// Token: 0x0600359D RID: 13725 RVA: 0x000C1DFE File Offset: 0x000BFFFE
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.RemoveAceSpecific(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed audit control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed audit rule.</param>
		// Token: 0x0600359E RID: 13726 RVA: 0x000C1E10 File Offset: 0x000C0010
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.RemoveAceSpecific(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> for the rule to be removed.</param>
		// Token: 0x0600359F RID: 13727 RVA: 0x000C1E34 File Offset: 0x000C0034
		public void RemoveAuditSpecific(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Sets the specified audit rule for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="auditFlags">The audit condition to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		// Token: 0x060035A0 RID: 13728 RVA: 0x000C1E72 File Offset: 0x000C0072
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.SetAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Sets the specified audit rule for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="auditFlags">The audit condition to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new audit rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new audit rule.</param>
		// Token: 0x060035A1 RID: 13729 RVA: 0x000C1E84 File Offset: 0x000C0084
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.SetAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Sets the specified audit rule for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an audit rule.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> for which to set an audit rule.</param>
		// Token: 0x060035A2 RID: 13730 RVA: 0x000C1EA8 File Offset: 0x000C00A8
		public void SetAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000C1EE8 File Offset: 0x000C00E8
		internal override void ApplyCanonicalSortToExplicitAces()
		{
			int canonicalExplicitAceCount = base.GetCanonicalExplicitAceCount();
			base.ApplyCanonicalSortToExplicitAces(0, canonicalExplicitAceCount);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override int GetAceInsertPosition(AceQualifier aceQualifier)
		{
			return 0;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000C1F04 File Offset: 0x000C0104
		internal override bool IsAceMeaningless(GenericAce ace)
		{
			if (base.IsAceMeaningless(ace))
			{
				return true;
			}
			if (!SystemAcl.IsValidAuditFlags(ace.AuditFlags))
			{
				return true;
			}
			QualifiedAce qualifiedAce = ace as QualifiedAce;
			return null != qualifiedAce && AceQualifier.SystemAudit != qualifiedAce.AceQualifier && AceQualifier.SystemAlarm != qualifiedAce.AceQualifier;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000C1F50 File Offset: 0x000C0150
		private static bool IsValidAuditFlags(AuditFlags auditFlags)
		{
			return auditFlags != AuditFlags.None && auditFlags == ((AuditFlags.Success | AuditFlags.Failure) & auditFlags);
		}
	}
}
