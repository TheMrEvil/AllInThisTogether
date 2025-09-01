using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents the access control and audit security for a file or directory.</summary>
	// Token: 0x0200052B RID: 1323
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x06003445 RID: 13381 RVA: 0x000BEADF File Offset: 0x000BCCDF
		internal FileSystemSecurity(bool isContainer) : base(isContainer, ResourceType.FileObject)
		{
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000BEAE9 File Offset: 0x000BCCE9
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections) : base(isContainer, ResourceType.FileObject, name, includeSections)
		{
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000BEAF5 File Offset: 0x000BCCF5
		internal FileSystemSecurity(bool isContainer, SafeHandle handle, AccessControlSections includeSections) : base(isContainer, ResourceType.FileObject, handle, includeSections)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemRights" /> enumeration.</returns>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000BEB01 File Offset: 0x000BCD01
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class.</returns>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000BEB0D File Offset: 0x000BCD0D
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000BEB19 File Offset: 0x000BCD19
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if the access rule is inherited; otherwise, <see langword="false" />.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether access is allowed or denied.</param>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> parameters specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="accessMask" /> parameter is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> parameter is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600344B RID: 13387 RVA: 0x000BEB25 File Offset: 0x000BCD25
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600344C RID: 13388 RVA: 0x000BC62C File Offset: 0x000BA82C
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes all matching allow or deny access control list (ACL) permissions from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to remove from a file or directory.</param>
		/// <returns>
		///   <see langword="true" /> if the access rule was removed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600344D RID: 13389 RVA: 0x000BC647 File Offset: 0x000BA847
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes all access control list (ACL) permissions for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600344E RID: 13390 RVA: 0x000BC650 File Offset: 0x000BA850
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny access control list (ACL) permission from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600344F RID: 13391 RVA: 0x000BC659 File Offset: 0x000BA859
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory and removes all matching ACL permissions.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003450 RID: 13392 RVA: 0x000BC63E File Offset: 0x000BA83E
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Sets the specified access control list (ACL) permission for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003451 RID: 13393 RVA: 0x000BC635 File Offset: 0x000BA835
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class representing the specified audit rule for the specified user.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if the access rule is inherited; otherwise, <see langword="false" />.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the type of auditing to perform.</param>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object representing the specified audit rule for the specified user.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003452 RID: 13394 RVA: 0x000BEB35 File Offset: 0x000BCD35
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Adds the specified audit rule to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003453 RID: 13395 RVA: 0x000BC662 File Offset: 0x000BA862
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes all matching allow or deny audit rules from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to remove from a file or directory.</param>
		/// <returns>
		///   <see langword="true" /> if the audit rule was removed; otherwise, <see langword="false" /></returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003454 RID: 13396 RVA: 0x000BC674 File Offset: 0x000BA874
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that specifies a user whose audit rules should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003455 RID: 13397 RVA: 0x000BC67D File Offset: 0x000BA87D
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny audit rule from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003456 RID: 13398 RVA: 0x000BC686 File Offset: 0x000BA886
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Sets the specified audit rule for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003457 RID: 13399 RVA: 0x000BC66B File Offset: 0x000BA86B
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000173AD File Offset: 0x000155AD
		internal FileSystemSecurity()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
