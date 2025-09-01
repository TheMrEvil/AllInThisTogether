using System;
using System.Transactions;

namespace System.EnterpriseServices
{
	/// <summary>Obtains information about the COM+ object context. This class cannot be inherited.</summary>
	// Token: 0x02000019 RID: 25
	public sealed class ContextUtil
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000021E0 File Offset: 0x000003E0
		internal ContextUtil()
		{
		}

		/// <summary>Gets a GUID representing the activity containing the component.</summary>
		/// <returns>The GUID for an activity if the current context is part of an activity; otherwise, <see langword="GUID_NULL" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid ActivityId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application.</summary>
		/// <returns>The GUID for the current application.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid ApplicationId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application instance.</summary>
		/// <returns>The GUID for the current application instance.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid ApplicationInstanceId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current context.</summary>
		/// <returns>The GUID for the current context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid ContextId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the <see langword="done" /> bit in the COM+ context.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is to be deactivated when the method returns; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000228A File Offset: 0x0000048A
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002291 File Offset: 0x00000491
		public static bool DeactivateOnReturn
		{
			get
			{
				return ContextUtil.deactivateOnReturn;
			}
			set
			{
				ContextUtil.deactivateOnReturn = value;
			}
		}

		/// <summary>Gets a value that indicates whether the current context is transactional.</summary>
		/// <returns>
		///   <see langword="true" /> if the current context is transactional; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002085 File Offset: 0x00000285
		public static bool IsInTransaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether role-based security is active in the current context.</summary>
		/// <returns>
		///   <see langword="true" /> if the current context has security enabled; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002085 File Offset: 0x00000285
		public static bool IsSecurityEnabled
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the <see langword="consistent" /> bit in the COM+ context.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionVote" /> values, either <see langword="Commit" /> or <see langword="Abort" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002299 File Offset: 0x00000499
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000022A0 File Offset: 0x000004A0
		[MonoTODO]
		public static TransactionVote MyTransactionVote
		{
			get
			{
				return ContextUtil.myTransactionVote;
			}
			set
			{
				ContextUtil.myTransactionVote = value;
			}
		}

		/// <summary>Gets a GUID for the current partition.</summary>
		/// <returns>The GUID for the current partition.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid PartitionId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object describing the current COM+ DTC transaction.</summary>
		/// <returns>An object that represents the current transaction.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002085 File Offset: 0x00000285
		public static object Transaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current transaction context.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that represents the current transaction context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002085 File Offset: 0x00000285
		public static Transaction SystemTransaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the GUID of the current COM+ DTC transaction.</summary>
		/// <returns>A GUID representing the current COM+ DTC transaction, if one exists.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002085 File Offset: 0x00000285
		public static Guid TransactionId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets both the <see langword="consistent" /> bit and the <see langword="done" /> bit to <see langword="false" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available.</exception>
		// Token: 0x06000056 RID: 86 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void DisableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit to <see langword="true" /> and the <see langword="done" /> bit to <see langword="false" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available.</exception>
		// Token: 0x06000057 RID: 87 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void EnableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a named property from the COM+ context.</summary>
		/// <param name="name">The name of the requested property.</param>
		/// <returns>The named property for the context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x06000058 RID: 88 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static object GetNamedProperty(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the caller is in the specified role.</summary>
		/// <param name="role">The name of the role to check.</param>
		/// <returns>
		///   <see langword="true" /> if the caller is in the specified role; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		// Token: 0x06000059 RID: 89 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the serviced component is activated in the default context. Serviced components that do not have COM+ catalog information are activated in the default context.</summary>
		/// <returns>
		///   <see langword="true" /> if the serviced component is activated in the default context; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600005A RID: 90 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static bool IsDefaultContext()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit to <see langword="false" /> and the <see langword="done" /> bit to <see langword="true" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		// Token: 0x0600005B RID: 91 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void SetAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit and the <see langword="done" /> bit to <see langword="true" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		// Token: 0x0600005C RID: 92 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void SetComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the named property for the COM+ context.</summary>
		/// <param name="name">The name of the property to set.</param>
		/// <param name="value">Object that represents the property value to set.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		// Token: 0x0600005D RID: 93 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void SetNamedProperty(string name, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400004E RID: 78
		private static bool deactivateOnReturn;

		// Token: 0x0400004F RID: 79
		private static TransactionVote myTransactionVote;
	}
}
