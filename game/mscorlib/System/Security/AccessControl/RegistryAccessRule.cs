using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights allowed or denied for a user or group. This class cannot be inherited.</summary>
	// Token: 0x020004FC RID: 1276
	public sealed class RegistryAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class, specifying the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values indicating the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values indicating whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="registryRights" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600332B RID: 13099 RVA: 0x000BC6B3 File Offset: 0x000BA8B3
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type) : this(identity, (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class, specifying the name of the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The name of the user or group the rule applies to.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values indicating the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values indicating whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="registryRights" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="registryRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="identity" /> is a zero-length string.  
		/// -or-  
		/// <paramref name="identity" /> is longer than 512 characters.</exception>
		// Token: 0x0600332C RID: 13100 RVA: 0x000BC6C1 File Offset: 0x000BA8C1
		public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class, specifying the user or group the rule applies to, the access rights, the inheritance flags, the propagation flags, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the rights allowed or denied.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> flags specifying how access rights are inherited from other objects.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> flags specifying how access rights are propagated to other objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="registryRights" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="type" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="inheritanceFlags" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="propagationFlags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="registryRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600332D RID: 13101 RVA: 0x000BC6D4 File Offset: 0x000BA8D4
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class, specifying the name of the user or group the rule applies to, the access rights, the inheritance flags, the propagation flags, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The name of the user or group the rule applies to.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values indicating the rights allowed or denied.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> flags specifying how access rights are inherited from other objects.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> flags specifying how access rights are propagated to other objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="registryRights" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="type" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="inheritanceFlags" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="propagationFlags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="identity" /> is a zero-length string.  
		/// -or-  
		/// <paramref name="identity" /> is longer than 512 characters.</exception>
		// Token: 0x0600332E RID: 13102 RVA: 0x000BC6E4 File Offset: 0x000BA8E4
		public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000BC6F9 File Offset: 0x000BA8F9
		internal RegistryAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Gets the rights allowed or denied by the access rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values indicating the rights allowed or denied by the access rule.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000BC70A File Offset: 0x000BA90A
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
