using System;

namespace System.EnterpriseServices
{
	/// <summary>Describes the chain of callers leading up to the current method call.</summary>
	// Token: 0x02000042 RID: 66
	public sealed class SecurityCallContext
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000021E0 File Offset: 0x000003E0
		internal SecurityCallContext()
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000021E0 File Offset: 0x000003E0
		internal SecurityCallContext(ISecurityCallContext context)
		{
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no security context.</exception>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002085 File Offset: 0x00000285
		public SecurityCallers Callers
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002085 File Offset: 0x00000285
		public static SecurityCallContext CurrentCall
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object that describes the direct caller of this method.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> value.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002085 File Offset: 0x00000285
		public SecurityIdentity DirectCaller
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines whether security checks are enabled in the current context.</summary>
		/// <returns>
		///   <see langword="true" /> if security checks are enabled in the current context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002085 File Offset: 0x00000285
		public bool IsSecurityEnabled
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see langword="MinAuthenticationLevel" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</summary>
		/// <returns>The <see langword="MinAuthenticationLevel" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002085 File Offset: 0x00000285
		public int MinAuthenticationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see langword="NumCallers" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</summary>
		/// <returns>The <see langword="NumCallers" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002085 File Offset: 0x00000285
		public int NumCallers
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> that describes the original caller.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SecurityIdentity" /> values.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002085 File Offset: 0x00000285
		public SecurityIdentity OriginalCaller
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Verifies that the direct caller is a member of the specified role.</summary>
		/// <param name="role">The specified role.</param>
		/// <returns>
		///   <see langword="true" /> if the direct caller is a member of the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000EC RID: 236 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies that the specified user is in the specified role.</summary>
		/// <param name="user">The specified user.</param>
		/// <param name="role">The specified role.</param>
		/// <returns>
		///   <see langword="true" /> if the specified user is a member of the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000ED RID: 237 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool IsUserInRole(string user, string role)
		{
			throw new NotImplementedException();
		}
	}
}
