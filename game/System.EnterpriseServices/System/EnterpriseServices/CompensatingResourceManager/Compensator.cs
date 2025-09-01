using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Represents the base class for all Compensating Resource Manager (CRM) Compensators.</summary>
	// Token: 0x02000073 RID: 115
	public class Compensator : ServicedComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Compensator" /> class.</summary>
		// Token: 0x060001C7 RID: 455 RVA: 0x00002794 File Offset: 0x00000994
		[MonoTODO]
		public Compensator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value representing the Compensating Resource Manager (CRM) <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00002085 File Offset: 0x00000285
		public Clerk Clerk
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Delivers a log record to the Compensating Resource Manager (CRM) Compensator during the abort phase.</summary>
		/// <param name="rec">The log record to be delivered.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001C9 RID: 457 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual bool AbortRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the abort phase of the transaction completion, and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">
		///   <see langword="true" /> to begin abort phase; otherwise, <see langword="false" />.</param>
		// Token: 0x060001CA RID: 458 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual void BeginAbort(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the commit phase of the transaction completion and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">
		///   <see langword="true" /> to begin commit phase; otherwise, <see langword="false" />.</param>
		// Token: 0x060001CB RID: 459 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual void BeginCommit(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the prepare phase of the transaction completion and the upcoming delivery of records.</summary>
		// Token: 0x060001CC RID: 460 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual void BeginPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the commit phase.</summary>
		/// <param name="rec">The log record to forward.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001CD RID: 461 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual bool CommitRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has received all the log records available during the abort phase.</summary>
		// Token: 0x060001CE RID: 462 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual void EndAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has delivered all the log records available during the commit phase.</summary>
		// Token: 0x060001CF RID: 463 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual void EndCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has had all the log records available during the prepare phase.</summary>
		/// <returns>
		///   <see langword="true" /> if successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001D0 RID: 464 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual bool EndPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the prepare phase.</summary>
		/// <param name="rec">The log record to forward.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001D1 RID: 465 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public virtual bool PrepareRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}
	}
}
