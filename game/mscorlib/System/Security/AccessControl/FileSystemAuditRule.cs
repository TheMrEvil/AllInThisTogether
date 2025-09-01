using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an audit rule for a file or directory. This class cannot be inherited.</summary>
	// Token: 0x02000529 RID: 1321
	public sealed class FileSystemAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using a reference to a user account, a value that specifies the type of operation associated with the audit rule, and a value that specifies when to perform auditing.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.  
		///  -or-  
		///  The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x0600343F RID: 13375 RVA: 0x000BEA9E File Offset: 0x000BCC9E
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using a user account name, a value that specifies the type of operation associated with the audit rule, and a value that specifies when to perform auditing.</summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.  
		///  -or-  
		///  The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x06003440 RID: 13376 RVA: 0x000BEAAB File Offset: 0x000BCCAB
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(new NTAccount(identity), fileSystemRights, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using the name of a reference to a user account, a value that specifies the type of operation associated with the audit rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies when to perform auditing.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identity" /> parameter is not an <see cref="T:System.Security.Principal.IdentityReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An incorrect enumeration was passed to the <paramref name="flags" /> parameter.  
		///  -or-  
		///  The <see cref="F:System.Security.AccessControl.AuditFlags.None" /> value was passed to the <paramref name="flags" /> parameter.</exception>
		// Token: 0x06003441 RID: 13377 RVA: 0x000BEABB File Offset: 0x000BCCBB
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(identity, fileSystemRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000BC737 File Offset: 0x000BA937
		internal FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, (int)fileSystemRights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class using the name of a user account, a value that specifies the type of operation associated with the audit rule, a value that determines how rights are inherited, a value that determines how rights are propagated, and a value that specifies when to perform auditing.</summary>
		/// <param name="identity">The name of a user account.</param>
		/// <param name="fileSystemRights">One of the <see cref="T:System.Security.AccessControl.FileSystemRights" /> values that specifies the type of operation associated with the audit rule.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how access masks are propagated to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how Access Control Entries (ACEs) are propagated to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x06003443 RID: 13379 RVA: 0x000BEACB File Offset: 0x000BCCCB
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new NTAccount(identity), fileSystemRights, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.FileSystemRights" /> flags associated with the current <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object.</returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000BC70A File Offset: 0x000BA90A
		public FileSystemRights FileSystemRights
		{
			get
			{
				return (FileSystemRights)base.AccessMask;
			}
		}
	}
}
