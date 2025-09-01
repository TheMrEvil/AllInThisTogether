using System;
using System.Threading;

namespace System.Transactions
{
	/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class TransactionScope : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class.</summary>
		// Token: 0x060000C4 RID: 196 RVA: 0x00002E86 File Offset: 0x00001086
		public TransactionScope() : this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified asynchronous flow option.</summary>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000C5 RID: 197 RVA: 0x00002E94 File Offset: 0x00001094
		public TransactionScope(TransactionScopeAsyncFlowOption asyncFlowOption) : this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout, asyncFlowOption)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		// Token: 0x060000C6 RID: 198 RVA: 0x00002EA3 File Offset: 0x000010A3
		public TransactionScope(Transaction transactionToUse) : this(transactionToUse, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00002EB1 File Offset: 0x000010B1
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout) : this(transactionToUse, scopeTimeout, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x060000C8 RID: 200 RVA: 0x00002EBC File Offset: 0x000010BC
		[MonoTODO("EnterpriseServicesInteropOption not supported.")]
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
		{
			this.Initialize(TransactionScopeOption.Required, transactionToUse, TransactionScope.defaultOptions, interopOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		// Token: 0x060000C9 RID: 201 RVA: 0x00002ED4 File Offset: 0x000010D4
		public TransactionScope(TransactionScopeOption scopeOption) : this(scopeOption, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00002EE2 File Offset: 0x000010E2
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout) : this(scopeOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000CB RID: 203 RVA: 0x00002EED File Offset: 0x000010ED
		public TransactionScope(TransactionScopeOption option, TransactionScopeAsyncFlowOption asyncFlow) : this(option, TransactionManager.DefaultTimeout, asyncFlow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, requirements, and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000CC RID: 204 RVA: 0x00002EFC File Offset: 0x000010FC
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			this.Initialize(scopeOption, null, TransactionScope.defaultOptions, EnterpriseServicesInteropOption.None, scopeTimeout, asyncFlow);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		// Token: 0x060000CD RID: 205 RVA: 0x00002F14 File Offset: 0x00001114
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions) : this(scopeOption, transactionOptions, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified scope and COM+ interoperability requirements, and transaction options.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x00002F1F File Offset: 0x0000111F
		[MonoTODO("EnterpriseServicesInteropOption not supported")]
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
		{
			this.Initialize(scopeOption, null, transactionOptions, interopOption, transactionOptions.Timeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000CF RID: 207 RVA: 0x00002F39 File Offset: 0x00001139
		public TransactionScope(Transaction transactionToUse, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000D0 RID: 208 RVA: 0x00002F39 File Offset: 0x00001139
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		// Token: 0x060000D1 RID: 209 RVA: 0x00002F39 File Offset: 0x00001139
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002F48 File Offset: 0x00001148
		private void Initialize(TransactionScopeOption scopeOption, Transaction tx, TransactionOptions options, EnterpriseServicesInteropOption interop, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			this.completed = false;
			this.isRoot = false;
			this.nested = 0;
			this.asyncFlowEnabled = (asyncFlow == TransactionScopeAsyncFlowOption.Enabled);
			if (scopeTimeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("scopeTimeout");
			}
			this.timeout = scopeTimeout;
			this.oldTransaction = Transaction.CurrentInternal;
			Transaction.CurrentInternal = (this.transaction = this.InitTransaction(tx, scopeOption, options));
			if (this.transaction != null)
			{
				this.transaction.InitScope(this);
			}
			if (this.parentScope != null)
			{
				this.parentScope.nested++;
			}
			if (this.timeout != TimeSpan.Zero)
			{
				this.scopeTimer = new Timer(new TimerCallback(TransactionScope.TimerCallback), this, scopeTimeout, TimeSpan.Zero);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003020 File Offset: 0x00001220
		private static void TimerCallback(object state)
		{
			TransactionScope transactionScope = state as TransactionScope;
			if (transactionScope == null)
			{
				throw new TransactionException("TransactionScopeTimerObjectInvalid", null);
			}
			transactionScope.TimeoutScope();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000304C File Offset: 0x0000124C
		private void TimeoutScope()
		{
			if (!this.completed && this.transaction != null)
			{
				try
				{
					this.transaction.Rollback();
					this.aborted = true;
				}
				catch (ObjectDisposedException)
				{
				}
				catch (TransactionException)
				{
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000030A8 File Offset: 0x000012A8
		private Transaction InitTransaction(Transaction tx, TransactionScopeOption scopeOption, TransactionOptions options)
		{
			if (tx != null)
			{
				return tx;
			}
			if (scopeOption == TransactionScopeOption.Suppress)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				return null;
			}
			if (scopeOption != TransactionScopeOption.Required)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				this.isRoot = true;
				return new Transaction(options.IsolationLevel);
			}
			if (Transaction.CurrentInternal == null)
			{
				this.isRoot = true;
				return new Transaction(options.IsolationLevel);
			}
			this.parentScope = Transaction.CurrentInternal.Scope;
			return Transaction.CurrentInternal;
		}

		/// <summary>Indicates that all operations within the scope are completed successfully.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method has already been called once.</exception>
		// Token: 0x060000D6 RID: 214 RVA: 0x0000314D File Offset: 0x0000134D
		public void Complete()
		{
			if (this.completed)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete. You should dispose the TransactionScope.");
			}
			this.completed = true;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003169 File Offset: 0x00001369
		internal bool IsAborted
		{
			get
			{
				return this.aborted;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003171 File Offset: 0x00001371
		internal bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003179 File Offset: 0x00001379
		internal bool IsComplete
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003181 File Offset: 0x00001381
		internal TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		/// <summary>Ends the transaction scope.</summary>
		// Token: 0x060000DB RID: 219 RVA: 0x0000318C File Offset: 0x0000138C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.parentScope != null)
			{
				this.parentScope.nested--;
			}
			if (this.nested > 0)
			{
				this.transaction.Rollback();
				throw new InvalidOperationException("TransactionScope nested incorrectly");
			}
			if (Transaction.CurrentInternal != this.transaction && !this.asyncFlowEnabled)
			{
				if (this.transaction != null)
				{
					this.transaction.Rollback();
				}
				if (Transaction.CurrentInternal != null)
				{
					Transaction.CurrentInternal.Rollback();
				}
				throw new InvalidOperationException("Transaction.Current has changed inside of the TransactionScope");
			}
			if (this.scopeTimer != null)
			{
				this.scopeTimer.Dispose();
			}
			if (this.asyncFlowEnabled)
			{
				if (this.oldTransaction != null)
				{
					this.oldTransaction.Scope = this.parentScope;
				}
				Transaction currentInternal = Transaction.CurrentInternal;
				if (this.transaction == null && currentInternal == null)
				{
					return;
				}
				currentInternal.Scope = this.parentScope;
				Transaction.CurrentInternal = this.oldTransaction;
				this.transaction.Scope = null;
				if (this.IsAborted)
				{
					throw new TransactionAbortedException("Transaction has aborted");
				}
				if (!this.IsComplete)
				{
					this.transaction.Rollback();
					currentInternal.Rollback();
					return;
				}
				if (!this.isRoot)
				{
					return;
				}
				currentInternal.CommitInternal();
				this.transaction.CommitInternal();
				return;
			}
			else
			{
				if (Transaction.CurrentInternal == this.oldTransaction && this.oldTransaction != null)
				{
					this.oldTransaction.Scope = this.parentScope;
				}
				Transaction.CurrentInternal = this.oldTransaction;
				if (this.transaction == null)
				{
					return;
				}
				if (this.IsAborted)
				{
					this.transaction.Scope = null;
					throw new TransactionAbortedException("Transaction has aborted");
				}
				if (!this.IsComplete)
				{
					this.transaction.Rollback();
					return;
				}
				if (!this.isRoot)
				{
					return;
				}
				this.transaction.CommitInternal();
				this.transaction.Scope = null;
				return;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000339C File Offset: 0x0000159C
		// Note: this type is marked as 'beforefieldinit'.
		static TransactionScope()
		{
		}

		// Token: 0x04000067 RID: 103
		private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

		// Token: 0x04000068 RID: 104
		private Timer scopeTimer;

		// Token: 0x04000069 RID: 105
		private Transaction transaction;

		// Token: 0x0400006A RID: 106
		private Transaction oldTransaction;

		// Token: 0x0400006B RID: 107
		private TransactionScope parentScope;

		// Token: 0x0400006C RID: 108
		private TimeSpan timeout;

		// Token: 0x0400006D RID: 109
		private int nested;

		// Token: 0x0400006E RID: 110
		private bool disposed;

		// Token: 0x0400006F RID: 111
		private bool completed;

		// Token: 0x04000070 RID: 112
		private bool aborted;

		// Token: 0x04000071 RID: 113
		private bool isRoot;

		// Token: 0x04000072 RID: 114
		private bool asyncFlowEnabled;
	}
}
