using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions.Configuration;

namespace System.Transactions
{
	/// <summary>Contains methods used for transaction management. This class cannot be inherited.</summary>
	// Token: 0x02000025 RID: 37
	public static class TransactionManager
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00002CEC File Offset: 0x00000EEC
		static TransactionManager()
		{
			TransactionManager.defaultSettings = (ConfigurationManager.GetSection("system.transactions/defaultSettings") as DefaultSettingsSection);
			TransactionManager.machineSettings = (ConfigurationManager.GetSection("system.transactions/machineSettings") as MachineSettingsSection);
		}

		/// <summary>Gets the default timeout interval for new transactions.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout interval for new transactions.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static TimeSpan DefaultTimeout
		{
			get
			{
				if (TransactionManager.defaultSettings != null)
				{
					return TransactionManager.defaultSettings.Timeout;
				}
				return TransactionManager.defaultTimeout;
			}
		}

		/// <summary>Gets or sets a custom transaction factory.</summary>
		/// <returns>A <see cref="T:System.Transactions.HostCurrentTransactionCallback" /> that contains a custom transaction factory.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		public static HostCurrentTransactionCallback HostCurrentCallback
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the default maximum timeout interval for new transactions.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum timeout interval that is allowed when creating new transactions.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002D55 File Offset: 0x00000F55
		public static TimeSpan MaximumTimeout
		{
			get
			{
				if (TransactionManager.machineSettings != null)
				{
					return TransactionManager.machineSettings.MaxTimeout;
				}
				return TransactionManager.maxTimeout;
			}
		}

		/// <summary>Notifies the transaction manager that a resource manager recovering from failure has finished reenlisting in all unresolved transactions.</summary>
		/// <param name="resourceManagerIdentifier">A <see cref="T:System.Guid" /> that uniquely identifies the resource to be recovered from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="resourceManagerIdentifier" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000AF RID: 175 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		public static void RecoveryComplete(Guid resourceManagerIdentifier)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reenlists a durable participant in a transaction.</summary>
		/// <param name="resourceManagerIdentifier">A <see cref="T:System.Guid" /> that uniquely identifies the resource manager.</param>
		/// <param name="recoveryInformation">Contains additional information of recovery information.</param>
		/// <param name="enlistmentNotification">A resource object that implements <see cref="T:System.Transactions.IEnlistmentNotification" /> to receive notifications.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> that describes the enlistment.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="recoveryInformation" /> is invalid.  
		/// -or-  
		/// Transaction Manager information in <paramref name="recoveryInformation" /> does not match the configured transaction manager.  
		/// -or-  
		/// <paramref name="RecoveryInformation" /> is not recognized by <see cref="N:System.Transactions" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Transactions.TransactionManager.RecoveryComplete(System.Guid)" /> has already been called for the specified <paramref name="resourceManagerIdentifier" />. The reenlistment is rejected.</exception>
		/// <exception cref="T:System.Transactions.TransactionException">The <paramref name="resourceManagerIdentifier" /> does not match the content of the specified recovery information in <paramref name="recoveryInformation" />.</exception>
		// Token: 0x060000B0 RID: 176 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		public static Enlistment Reenlist(Guid resourceManagerIdentifier, byte[] recoveryInformation, IEnlistmentNotification enlistmentNotification)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates that a distributed transaction has started.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000B1 RID: 177 RVA: 0x00002D70 File Offset: 0x00000F70
		// (remove) Token: 0x060000B2 RID: 178 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static event TransactionStartedEventHandler DistributedTransactionStarted
		{
			[CompilerGenerated]
			add
			{
				TransactionStartedEventHandler transactionStartedEventHandler = TransactionManager.DistributedTransactionStarted;
				TransactionStartedEventHandler transactionStartedEventHandler2;
				do
				{
					transactionStartedEventHandler2 = transactionStartedEventHandler;
					TransactionStartedEventHandler value2 = (TransactionStartedEventHandler)Delegate.Combine(transactionStartedEventHandler2, value);
					transactionStartedEventHandler = Interlocked.CompareExchange<TransactionStartedEventHandler>(ref TransactionManager.DistributedTransactionStarted, value2, transactionStartedEventHandler2);
				}
				while (transactionStartedEventHandler != transactionStartedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				TransactionStartedEventHandler transactionStartedEventHandler = TransactionManager.DistributedTransactionStarted;
				TransactionStartedEventHandler transactionStartedEventHandler2;
				do
				{
					transactionStartedEventHandler2 = transactionStartedEventHandler;
					TransactionStartedEventHandler value2 = (TransactionStartedEventHandler)Delegate.Remove(transactionStartedEventHandler2, value);
					transactionStartedEventHandler = Interlocked.CompareExchange<TransactionStartedEventHandler>(ref TransactionManager.DistributedTransactionStarted, value2, transactionStartedEventHandler2);
				}
				while (transactionStartedEventHandler != transactionStartedEventHandler2);
			}
		}

		// Token: 0x04000060 RID: 96
		private static DefaultSettingsSection defaultSettings;

		// Token: 0x04000061 RID: 97
		private static MachineSettingsSection machineSettings;

		// Token: 0x04000062 RID: 98
		private static TimeSpan defaultTimeout = new TimeSpan(0, 1, 0);

		// Token: 0x04000063 RID: 99
		private static TimeSpan maxTimeout = new TimeSpan(0, 10, 0);

		// Token: 0x04000064 RID: 100
		[CompilerGenerated]
		private static TransactionStartedEventHandler DistributedTransactionStarted;
	}
}
