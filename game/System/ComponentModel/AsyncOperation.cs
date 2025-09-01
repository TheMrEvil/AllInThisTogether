using System;
using System.Threading;
using Unity;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x02000361 RID: 865
	public sealed class AsyncOperation
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x000679C9 File Offset: 0x00065BC9
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this._userSuppliedState = userSuppliedState;
			this._syncContext = syncContext;
			this._alreadyCompleted = false;
			this._syncContext.OperationStarted();
		}

		/// <summary>Finalizes the asynchronous operation.</summary>
		// Token: 0x06001CAE RID: 7342 RVA: 0x000679F4 File Offset: 0x00065BF4
		~AsyncOperation()
		{
			if (!this._alreadyCompleted && this._syncContext != null)
			{
				this._syncContext.OperationCompleted();
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00067A38 File Offset: 0x00065C38
		public object UserSuppliedState
		{
			get
			{
				return this._userSuppliedState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00067A40 File Offset: 0x00065C40
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this._syncContext;
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends.</param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00067A48 File Offset: 0x00065C48
		public void Post(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, false);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends.</param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x00067A53 File Offset: 0x00065C53
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, true);
			this.OperationCompletedCore();
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task.</exception>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00067A64 File Offset: 0x00065C64
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this._alreadyCompleted = true;
			this.OperationCompletedCore();
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00067A79 File Offset: 0x00065C79
		private void PostCore(SendOrPostCallback d, object arg, bool markCompleted)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			if (markCompleted)
			{
				this._alreadyCompleted = true;
			}
			this._syncContext.Post(d, arg);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00067AA0 File Offset: 0x00065CA0
		private void OperationCompletedCore()
		{
			try
			{
				this._syncContext.OperationCompleted();
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00067AD4 File Offset: 0x00065CD4
		private void VerifyNotCompleted()
		{
			if (this._alreadyCompleted)
			{
				throw new InvalidOperationException("This operation has already had OperationCompleted called on it and further calls are illegal.");
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00067AE9 File Offset: 0x00065CE9
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "A non-null SendOrPostCallback must be supplied.");
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00067AFE File Offset: 0x00065CFE
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal AsyncOperation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E91 RID: 3729
		private readonly SynchronizationContext _syncContext;

		// Token: 0x04000E92 RID: 3730
		private readonly object _userSuppliedState;

		// Token: 0x04000E93 RID: 3731
		private bool _alreadyCompleted;
	}
}
