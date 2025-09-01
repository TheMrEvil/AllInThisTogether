using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Writes records of transactional actions to a log.</summary>
	// Token: 0x02000070 RID: 112
	public sealed class Clerk
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">The name of the compensator.</param>
		/// <param name="description">The description of the compensator.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values.</param>
		// Token: 0x060001AF RID: 431 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public Clerk(string compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">A type that represents the compensator.</param>
		/// <param name="description">The description of the compensator.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values.</param>
		// Token: 0x060001B0 RID: 432 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public Clerk(Type compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of log records.</summary>
		/// <returns>The number of log records.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00002085 File Offset: 0x00000285
		public int LogRecordCount
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value representing the transaction unit of work (UOW).</summary>
		/// <returns>A GUID representing the UOW.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00002085 File Offset: 0x00000285
		public string TransactionUOW
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Frees the resources of the current Clerk before it is reclaimed by the garbage collector.</summary>
		// Token: 0x060001B3 RID: 435 RVA: 0x00002714 File Offset: 0x00000914
		[MonoTODO]
		~Clerk()
		{
			throw new NotImplementedException();
		}

		/// <summary>Forces all log records to disk.</summary>
		// Token: 0x060001B4 RID: 436 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ForceLog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Performs an immediate abort call on the transaction.</summary>
		// Token: 0x060001B5 RID: 437 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ForceTransactionToAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not deliver the last log record that was written by this instance of this interface.</summary>
		// Token: 0x060001B6 RID: 438 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void ForgetLogRecord()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes unstructured log records to the log.</summary>
		/// <param name="record">The log record to write to the log.</param>
		// Token: 0x060001B7 RID: 439 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void WriteLogRecord(object record)
		{
			throw new NotImplementedException();
		}
	}
}
