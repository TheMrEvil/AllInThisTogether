using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a registry key. This class cannot be inherited.</summary>
	// Token: 0x020004FB RID: 1275
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		// Token: 0x06003315 RID: 13077 RVA: 0x000BC4E0 File Offset: 0x000BA6E0
		private static Exception _HandleErrorCodeCore(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						result = new ArgumentException(SR.Format("Registry key name must start with a valid base key name.", "name"));
					}
				}
				else
				{
					result = new ArgumentException("The supplied handle is invalid. This can happen when trying to set an ACL on an anonymous kernel object.");
				}
			}
			else
			{
				result = new IOException(SR.Format("The specified registry key does not exist.", errorCode));
			}
			return result;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class with default values.</summary>
		// Token: 0x06003316 RID: 13078 RVA: 0x000BC539 File Offset: 0x000BA739
		public RegistrySecurity() : base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000BC543 File Offset: 0x000BA743
		internal RegistrySecurity(SafeRegistryHandle hKey, string name, AccessControlSections includeSections) : base(true, ResourceType.RegistryKey, hKey, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(RegistrySecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000BC55C File Offset: 0x000BA75C
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			return RegistrySecurity._HandleErrorCodeCore(errorCode, name, handle, context);
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003319 RID: 13081 RVA: 0x000BC567 File Offset: 0x000BA767
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, the inheritance and propagation of the rule, and the outcome that triggers the rule.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit successful access, failed access, or both.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> object representing the specified audit rule for the specified user, with the specified flags. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600331A RID: 13082 RVA: 0x000BC577 File Offset: 0x000BA777
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000BC588 File Offset: 0x000BA788
		internal AccessControlSections GetAccessControlSectionsFromChanges()
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

		// Token: 0x0600331C RID: 13084 RVA: 0x000BC5C8 File Offset: 0x000BA7C8
		internal void Persist(SafeRegistryHandle hKey, string keyName)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					this.Persist(hKey, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Searches for a matching access control with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x0600331D RID: 13085 RVA: 0x000BC62C File Offset: 0x000BA82C
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x0600331E RID: 13086 RVA: 0x000BC635 File Offset: 0x000BA835
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		// Token: 0x0600331F RID: 13087 RVA: 0x000BC63E File Offset: 0x000BA83E
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified access rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003320 RID: 13088 RVA: 0x000BC647 File Offset: 0x000BA847
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003321 RID: 13089 RVA: 0x000BC650 File Offset: 0x000BA850
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003322 RID: 13090 RVA: 0x000BC659 File Offset: 0x000BA859
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x06003323 RID: 13091 RVA: 0x000BC662 File Offset: 0x000BA862
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003324 RID: 13092 RVA: 0x000BC66B File Offset: 0x000BA86B
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003325 RID: 13093 RVA: 0x000BC674 File Offset: 0x000BA874
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003326 RID: 13094 RVA: 0x000BC67D File Offset: 0x000BA87D
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06003327 RID: 13095 RVA: 0x000BC686 File Offset: 0x000BA886
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Gets the enumeration type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryRights" /> enumeration.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000BC68F File Offset: 0x000BA88F
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000BC69B File Offset: 0x000BA89B
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> class.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000BC6A7 File Offset: 0x000BA8A7
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}
	}
}
