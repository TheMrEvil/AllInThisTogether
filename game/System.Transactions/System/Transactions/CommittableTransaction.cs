using System;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Transactions
{
	/// <summary>Describes a committable transaction.</summary>
	// Token: 0x0200000A RID: 10
	[Serializable]
	public sealed class CommittableTransaction : Transaction, ISerializable, IDisposable, IAsyncResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		// Token: 0x0600000C RID: 12 RVA: 0x0000208C File Offset: 0x0000028C
		public CommittableTransaction() : this(default(TransactionOptions))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified <paramref name="timeout" /> value.</summary>
		/// <param name="timeout">The maximum amount of time the transaction can exist, before it is aborted.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		// Token: 0x0600000D RID: 13 RVA: 0x000020A8 File Offset: 0x000002A8
		public CommittableTransaction(TimeSpan timeout) : base(IsolationLevel.Serializable)
		{
			this.options = default(TransactionOptions);
			this.options.Timeout = timeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified transaction options.</summary>
		/// <param name="options">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use for the new transaction.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is invalid.</exception>
		// Token: 0x0600000E RID: 14 RVA: 0x000020C9 File Offset: 0x000002C9
		public CommittableTransaction(TransactionOptions options) : base(options.IsolationLevel)
		{
			this.options = options;
		}

		/// <summary>Begins an attempt to commit the transaction asynchronously.</summary>
		/// <param name="asyncCallback">The <see cref="T:System.AsyncCallback" /> delegate that is invoked when the transaction completes. This parameter can be <see langword="null" />, in which case the application is not notified of the transaction's completion. Instead, the application must use the <see cref="T:System.IAsyncResult" /> interface to check for completion and wait accordingly, or call <see cref="M:System.Transactions.CommittableTransaction.EndCommit(System.IAsyncResult)" /> to wait for completion.</param>
		/// <param name="asyncState">An object, which might contain arbitrary state information, associated with the asynchronous commitment. This object is passed to the callback, and is not interpreted by <see cref="N:System.Transactions" />. A null reference is permitted.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> interface that can be used by the caller to check the status of the asynchronous operation, or to wait for the operation to complete.</returns>
		// Token: 0x0600000F RID: 15 RVA: 0x000020E0 File Offset: 0x000002E0
		public IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState)
		{
			this.callback = asyncCallback;
			this.user_defined_state = asyncState;
			AsyncCallback asyncCallback2 = null;
			if (asyncCallback != null)
			{
				asyncCallback2 = new AsyncCallback(this.CommitCallback);
			}
			this.asyncResult = base.BeginCommitInternal(asyncCallback2);
			return this;
		}

		/// <summary>Ends an attempt to commit the transaction asynchronously.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object associated with the asynchronous commitment.</param>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> is called and the transaction rolls back for the first time.</exception>
		// Token: 0x06000010 RID: 16 RVA: 0x0000211B File Offset: 0x0000031B
		public void EndCommit(IAsyncResult asyncResult)
		{
			if (asyncResult != this)
			{
				throw new ArgumentException("The IAsyncResult parameter must be the same parameter as returned by BeginCommit.", "asyncResult");
			}
			base.EndCommitInternal(this.asyncResult);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000213D File Offset: 0x0000033D
		private void CommitCallback(IAsyncResult ar)
		{
			if (this.asyncResult == null && ar.CompletedSynchronously)
			{
				this.asyncResult = ar;
			}
			this.callback(this);
		}

		/// <summary>Attempts to commit the transaction.</summary>
		/// <exception cref="T:System.Transactions.TransactionInDoubtException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called on a transaction and the transaction becomes <see cref="F:System.Transactions.TransactionStatus.InDoubt" />.</exception>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called and the transaction rolls back for the first time.</exception>
		// Token: 0x06000012 RID: 18 RVA: 0x00002162 File Offset: 0x00000362
		public void Commit()
		{
			base.CommitInternal();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</summary>
		/// <returns>The object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002171 File Offset: 0x00000371
		object IAsyncResult.AsyncState
		{
			get
			{
				return this.user_defined_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002179 File Offset: 0x00000379
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return this.asyncResult.AsyncWaitHandle;
			}
		}

		/// <summary>Gets an indication of whether the asynchronous commit operation completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the asynchronous commit operation completed synchronously; otherwise, <see langword="false" />. This property always returns <see langword="false" /> even if the operation completed synchronously.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002186 File Offset: 0x00000386
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return this.asyncResult.CompletedSynchronously;
			}
		}

		/// <summary>Gets an indication whether the asynchronous commit operation has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation is complete; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002193 File Offset: 0x00000393
		bool IAsyncResult.IsCompleted
		{
			get
			{
				return this.asyncResult.IsCompleted;
			}
		}

		// Token: 0x0400002B RID: 43
		private TransactionOptions options;

		// Token: 0x0400002C RID: 44
		private AsyncCallback callback;

		// Token: 0x0400002D RID: 45
		private object user_defined_state;

		// Token: 0x0400002E RID: 46
		private IAsyncResult asyncResult;
	}
}
