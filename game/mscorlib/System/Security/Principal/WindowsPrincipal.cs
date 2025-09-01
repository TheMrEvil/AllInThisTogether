using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
using Mono;
using Unity;

namespace System.Security.Principal
{
	/// <summary>Enables code to check the Windows group membership of a Windows user.</summary>
	// Token: 0x020004EE RID: 1262
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsPrincipal" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="ntIdentity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsPrincipal" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ntIdentity" /> is <see langword="null" />.</exception>
		// Token: 0x06003278 RID: 12920 RVA: 0x000B997F File Offset: 0x000B7B7F
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.WindowsIdentity" /> object of the current principal.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000B999C File Offset: 0x000B7B9C
		public override IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified relative identifier (RID).</summary>
		/// <param name="rid">The RID of the Windows user group in which to check for the principal's membership status.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified Windows user group, that is, in a particular role; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600327A RID: 12922 RVA: 0x000B99A4 File Offset: 0x000B7BA4
		public virtual bool IsInRole(int rid)
		{
			if (Environment.IsUnix)
			{
				return WindowsPrincipal.IsMemberOfGroupId(this.Token, (IntPtr)rid);
			}
			string role;
			switch (rid)
			{
			case 544:
				role = "BUILTIN\\Administrators";
				break;
			case 545:
				role = "BUILTIN\\Users";
				break;
			case 546:
				role = "BUILTIN\\Guests";
				break;
			case 547:
				role = "BUILTIN\\Power Users";
				break;
			case 548:
				role = "BUILTIN\\Account Operators";
				break;
			case 549:
				role = "BUILTIN\\System Operators";
				break;
			case 550:
				role = "BUILTIN\\Print Operators";
				break;
			case 551:
				role = "BUILTIN\\Backup Operators";
				break;
			case 552:
				role = "BUILTIN\\Replicator";
				break;
			default:
				return false;
			}
			return this.IsInRole(role);
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified name.</summary>
		/// <param name="role">The name of the Windows user group for which to check membership.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified Windows user group; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600327B RID: 12923 RVA: 0x000B9A50 File Offset: 0x000B7C50
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public override bool IsInRole(string role)
		{
			if (role == null)
			{
				return false;
			}
			if (Environment.IsUnix)
			{
				using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(role))
				{
					return WindowsPrincipal.IsMemberOfGroupName(this.Token, safeStringMarshal.Value);
				}
			}
			if (this.m_roles == null)
			{
				this.m_roles = WindowsIdentity._GetRoles(this.Token);
			}
			role = role.ToUpperInvariant();
			foreach (string text in this.m_roles)
			{
				if (text != null && role == text.ToUpperInvariant())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.</summary>
		/// <param name="role">One of the <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified Windows user group; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> value.</exception>
		// Token: 0x0600327C RID: 12924 RVA: 0x000B9AF8 File Offset: 0x000B7CF8
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (!Environment.IsUnix)
			{
				return this.IsInRole((int)role);
			}
			if (role == WindowsBuiltInRole.Administrator)
			{
				string role2 = "root";
				return this.IsInRole(role2);
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified security identifier (SID).</summary>
		/// <param name="sid">A <see cref="T:System.Security.Principal.SecurityIdentifier" /> that uniquely identifies a Windows user group.</param>
		/// <returns>
		///   <see langword="true" /> if the current principal is a member of the specified Windows user group; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sid" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">Windows returned a Win32 error.</exception>
		// Token: 0x0600327D RID: 12925 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000B9B2F File Offset: 0x000B7D2F
		private IntPtr Token
		{
			get
			{
				return this._identity.Token;
			}
		}

		// Token: 0x0600327F RID: 12927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupId(IntPtr user, IntPtr group);

		// Token: 0x06003280 RID: 12928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupName(IntPtr user, IntPtr group);

		/// <summary>Gets all Windows device claims from this principal.</summary>
		/// <returns>A collection of all Windows device claims from this principal.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000B98E7 File Offset: 0x000B7AE7
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets all Windows user claims from this principal.</summary>
		/// <returns>A collection of all Windows user claims from this principal.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000B98E7 File Offset: 0x000B7AE7
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x04002346 RID: 9030
		private WindowsIdentity _identity;

		// Token: 0x04002347 RID: 9031
		private string[] m_roles;
	}
}
