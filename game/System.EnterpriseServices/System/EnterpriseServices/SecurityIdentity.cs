using System;

namespace System.EnterpriseServices
{
	/// <summary>Contains information that regards an identity in a COM+ call chain.</summary>
	// Token: 0x02000044 RID: 68
	public sealed class SecurityIdentity
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000021E0 File Offset: 0x000003E0
		[MonoTODO]
		internal SecurityIdentity()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000021E0 File Offset: 0x000003E0
		[MonoTODO]
		internal SecurityIdentity(ISecurityIdentityColl collection)
		{
		}

		/// <summary>Gets the name of the user described by this identity.</summary>
		/// <returns>The name of the user described by this identity.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002085 File Offset: 0x00000285
		public string AccountName
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication level of the user described by this identity.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002085 File Offset: 0x00000285
		public AuthenticationOption AuthenticationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication service described by this identity.</summary>
		/// <returns>The authentication service described by this identity.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002085 File Offset: 0x00000285
		public int AuthenticationService
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the impersonation level of the user described by this identity.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> value.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002085 File Offset: 0x00000285
		public ImpersonationLevelOption ImpersonationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
