using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
	/// <summary>Encapsulates and propagates all security-related data for execution contexts transferred across threads. This class cannot be inherited.</summary>
	// Token: 0x020003DC RID: 988
	public sealed class SecurityContext : IDisposable
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x0000259F File Offset: 0x0000079F
		private SecurityContext()
		{
		}

		/// <summary>Creates a copy of the current security context.</summary>
		/// <returns>The security context for the current thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current security context has been previously used, was marshaled across application domains, or was not acquired through the <see cref="M:System.Security.SecurityContext.Capture" /> method.</exception>
		// Token: 0x06002882 RID: 10370 RVA: 0x0000270D File Offset: 0x0000090D
		public SecurityContext CreateCopy()
		{
			return this;
		}

		/// <summary>Captures the security context for the current thread.</summary>
		/// <returns>The security context for the current thread.</returns>
		// Token: 0x06002883 RID: 10371 RVA: 0x00093050 File Offset: 0x00091250
		public static SecurityContext Capture()
		{
			return new SecurityContext();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.SecurityContext" /> class.</summary>
		// Token: 0x06002884 RID: 10372 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dispose()
		{
		}

		/// <summary>Determines whether the flow of the security context has been suppressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the flow has been suppressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002885 RID: 10373 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public static bool IsFlowSuppressed()
		{
			return false;
		}

		/// <summary>Determines whether the flow of the Windows identity portion of the current security context has been suppressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the flow has been suppressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002886 RID: 10374 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return false;
		}

		/// <summary>Restores the flow of the security context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The security context is <see langword="null" /> or an empty string.</exception>
		// Token: 0x06002887 RID: 10375 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void RestoreFlow()
		{
		}

		/// <summary>Runs the specified method in the specified security context on the current thread.</summary>
		/// <param name="securityContext">The security context to set.</param>
		/// <param name="callback">The delegate that represents the method to run in the specified security context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="securityContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="securityContext" /> was not acquired through a capture operation.  
		/// -or-  
		/// <paramref name="securityContext" /> has already been used as the argument to a <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> method call.</exception>
		// Token: 0x06002888 RID: 10376 RVA: 0x00093057 File Offset: 0x00091257
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			callback(state);
		}

		/// <summary>Suppresses the flow of the security context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		// Token: 0x06002889 RID: 10377 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public static AsyncFlowControl SuppressFlow()
		{
			throw new NotSupportedException();
		}

		/// <summary>Suppresses the flow of the Windows identity portion of the current security context across asynchronous threads.</summary>
		/// <returns>A structure for restoring the flow.</returns>
		// Token: 0x0600288A RID: 10378 RVA: 0x000472C8 File Offset: 0x000454C8
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			throw new NotSupportedException();
		}
	}
}
