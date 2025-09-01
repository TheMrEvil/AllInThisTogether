using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Contains information describing an active Compensating Resource Manager (CRM) Clerk object.</summary>
	// Token: 0x02000071 RID: 113
	public sealed class ClerkInfo
	{
		/// <summary>Frees the resources of the current <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> before it is reclaimed by the garbage collector.</summary>
		// Token: 0x060001B8 RID: 440 RVA: 0x00002740 File Offset: 0x00000940
		[MonoTODO]
		~ClerkInfo()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000021E0 File Offset: 0x000003E0
		internal ClerkInfo()
		{
		}

		/// <summary>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</summary>
		/// <returns>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string ActivityId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</summary>
		/// <returns>
		///   <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public Clerk Clerk
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the ProgId of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk.</summary>
		/// <returns>The ProgId of the CRM Compensator for the current CRM Clerk.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string Compensator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk. The description string is the string that was provided by the <see langword="ICrmLogControl::RegisterCompensator" /> method.</summary>
		/// <returns>The description of the CRM Compensator for the current CRM Clerk.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the instance class ID (CLSID) of the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The instance CLSID of the current CRM Clerk.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string InstanceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the unit of work (UOW) of the transaction for the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The UOW of the transaction for the current CRM Clerk.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string TransactionUOW
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
