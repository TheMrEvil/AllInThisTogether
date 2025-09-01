using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x02000363 RID: 867
	[DefaultEvent("DoWork")]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00067B34 File Offset: 0x00065D34
		public BackgroundWorker()
		{
			this._operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this._progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00067B60 File Offset: 0x00065D60
		private void AsyncOperationCompleted(object arg)
		{
			this._isRunning = false;
			this._cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>
		///   <see langword="true" /> if the application has requested cancellation of a background operation; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00067B7C File Offset: 0x00065D7C
		public bool CancellationPending
		{
			get
			{
				return this._cancellationPending;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is <see langword="false" />.</exception>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x00067B84 File Offset: 0x00065D84
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation.");
			}
			this._cancellationPending = true;
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001CC1 RID: 7361 RVA: 0x00067BA0 File Offset: 0x00065DA0
		// (remove) Token: 0x06001CC2 RID: 7362 RVA: 0x00067BD8 File Offset: 0x00065DD8
		public event DoWorkEventHandler DoWork
		{
			[CompilerGenerated]
			add
			{
				DoWorkEventHandler doWorkEventHandler = this.DoWork;
				DoWorkEventHandler doWorkEventHandler2;
				do
				{
					doWorkEventHandler2 = doWorkEventHandler;
					DoWorkEventHandler value2 = (DoWorkEventHandler)Delegate.Combine(doWorkEventHandler2, value);
					doWorkEventHandler = Interlocked.CompareExchange<DoWorkEventHandler>(ref this.DoWork, value2, doWorkEventHandler2);
				}
				while (doWorkEventHandler != doWorkEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DoWorkEventHandler doWorkEventHandler = this.DoWork;
				DoWorkEventHandler doWorkEventHandler2;
				do
				{
					doWorkEventHandler2 = doWorkEventHandler;
					DoWorkEventHandler value2 = (DoWorkEventHandler)Delegate.Remove(doWorkEventHandler2, value);
					doWorkEventHandler = Interlocked.CompareExchange<DoWorkEventHandler>(ref this.DoWork, value2, doWorkEventHandler2);
				}
				while (doWorkEventHandler != doWorkEventHandler2);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>
		///   <see langword="true" />, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x00067C0D File Offset: 0x00065E0D
		public bool IsBusy
		{
			get
			{
				return this._isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CC4 RID: 7364 RVA: 0x00067C18 File Offset: 0x00065E18
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			if (doWork != null)
			{
				doWork(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00067C38 File Offset: 0x00065E38
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			if (runWorkerCompleted != null)
			{
				runWorkerCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x00067C58 File Offset: 0x00065E58
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			if (progressChanged != null)
			{
				progressChanged(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001CC7 RID: 7367 RVA: 0x00067C78 File Offset: 0x00065E78
		// (remove) Token: 0x06001CC8 RID: 7368 RVA: 0x00067CB0 File Offset: 0x00065EB0
		public event ProgressChangedEventHandler ProgressChanged
		{
			[CompilerGenerated]
			add
			{
				ProgressChangedEventHandler progressChangedEventHandler = this.ProgressChanged;
				ProgressChangedEventHandler progressChangedEventHandler2;
				do
				{
					progressChangedEventHandler2 = progressChangedEventHandler;
					ProgressChangedEventHandler value2 = (ProgressChangedEventHandler)Delegate.Combine(progressChangedEventHandler2, value);
					progressChangedEventHandler = Interlocked.CompareExchange<ProgressChangedEventHandler>(ref this.ProgressChanged, value2, progressChangedEventHandler2);
				}
				while (progressChangedEventHandler != progressChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ProgressChangedEventHandler progressChangedEventHandler = this.ProgressChanged;
				ProgressChangedEventHandler progressChangedEventHandler2;
				do
				{
					progressChangedEventHandler2 = progressChangedEventHandler;
					ProgressChangedEventHandler value2 = (ProgressChangedEventHandler)Delegate.Remove(progressChangedEventHandler2, value);
					progressChangedEventHandler = Interlocked.CompareExchange<ProgressChangedEventHandler>(ref this.ProgressChanged, value2, progressChangedEventHandler2);
				}
				while (progressChangedEventHandler != progressChangedEventHandler2);
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00067CE5 File Offset: 0x00065EE5
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to <see langword="false" />.</exception>
		// Token: 0x06001CCA RID: 7370 RVA: 0x00067CF3 File Offset: 0x00065EF3
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to <see langword="false" />.</exception>
		// Token: 0x06001CCB RID: 7371 RVA: 0x00067D00 File Offset: 0x00065F00
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.");
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this._asyncOperation != null)
			{
				this._asyncOperation.Post(this._progressReporter, progressChangedEventArgs);
				return;
			}
			this._progressReporter(progressChangedEventArgs);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is <see langword="true" />.</exception>
		// Token: 0x06001CCC RID: 7372 RVA: 0x00067D4F File Offset: 0x00065F4F
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is <see langword="true" />.</exception>
		// Token: 0x06001CCD RID: 7373 RVA: 0x00067D58 File Offset: 0x00065F58
		public void RunWorkerAsync(object argument)
		{
			if (this._isRunning)
			{
				throw new InvalidOperationException("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.");
			}
			this._isRunning = true;
			this._cancellationPending = false;
			this._asyncOperation = AsyncOperationManager.CreateOperation(null);
			Task.Factory.StartNew(delegate(object arg)
			{
				this.WorkerThreadStart(arg);
			}, argument, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001CCE RID: 7374 RVA: 0x00067DB8 File Offset: 0x00065FB8
		// (remove) Token: 0x06001CCF RID: 7375 RVA: 0x00067DF0 File Offset: 0x00065FF0
		public event RunWorkerCompletedEventHandler RunWorkerCompleted
		{
			[CompilerGenerated]
			add
			{
				RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = this.RunWorkerCompleted;
				RunWorkerCompletedEventHandler runWorkerCompletedEventHandler2;
				do
				{
					runWorkerCompletedEventHandler2 = runWorkerCompletedEventHandler;
					RunWorkerCompletedEventHandler value2 = (RunWorkerCompletedEventHandler)Delegate.Combine(runWorkerCompletedEventHandler2, value);
					runWorkerCompletedEventHandler = Interlocked.CompareExchange<RunWorkerCompletedEventHandler>(ref this.RunWorkerCompleted, value2, runWorkerCompletedEventHandler2);
				}
				while (runWorkerCompletedEventHandler != runWorkerCompletedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = this.RunWorkerCompleted;
				RunWorkerCompletedEventHandler runWorkerCompletedEventHandler2;
				do
				{
					runWorkerCompletedEventHandler2 = runWorkerCompletedEventHandler;
					RunWorkerCompletedEventHandler value2 = (RunWorkerCompletedEventHandler)Delegate.Remove(runWorkerCompletedEventHandler2, value);
					runWorkerCompletedEventHandler = Interlocked.CompareExchange<RunWorkerCompletedEventHandler>(ref this.RunWorkerCompleted, value2, runWorkerCompletedEventHandler2);
				}
				while (runWorkerCompletedEventHandler != runWorkerCompletedEventHandler2);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00067E25 File Offset: 0x00066025
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00067E2D File Offset: 0x0006602D
		public bool WorkerReportsProgress
		{
			get
			{
				return this._workerReportsProgress;
			}
			set
			{
				this._workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00067E36 File Offset: 0x00066036
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x00067E3E File Offset: 0x0006603E
		public bool WorkerSupportsCancellation
		{
			get
			{
				return this._canCancelWorker;
			}
			set
			{
				this._canCancelWorker = value;
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00067E48 File Offset: 0x00066048
		private void WorkerThreadStart(object argument)
		{
			object result = null;
			Exception error = null;
			bool cancelled = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					cancelled = true;
				}
				else
				{
					result = doWorkEventArgs.Result;
				}
			}
			catch (Exception error)
			{
			}
			RunWorkerCompletedEventArgs arg = new RunWorkerCompletedEventArgs(result, error, cancelled);
			this._asyncOperation.PostOperationCompleted(this._operationCompleted, arg);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00067EB0 File Offset: 0x000660B0
		[CompilerGenerated]
		private void <RunWorkerAsync>b__27_0(object arg)
		{
			this.WorkerThreadStart(arg);
		}

		// Token: 0x04000E94 RID: 3732
		private bool _canCancelWorker;

		// Token: 0x04000E95 RID: 3733
		private bool _workerReportsProgress;

		// Token: 0x04000E96 RID: 3734
		private bool _cancellationPending;

		// Token: 0x04000E97 RID: 3735
		private bool _isRunning;

		// Token: 0x04000E98 RID: 3736
		private AsyncOperation _asyncOperation;

		// Token: 0x04000E99 RID: 3737
		private readonly SendOrPostCallback _operationCompleted;

		// Token: 0x04000E9A RID: 3738
		private readonly SendOrPostCallback _progressReporter;

		// Token: 0x04000E9B RID: 3739
		[CompilerGenerated]
		private DoWorkEventHandler DoWork;

		// Token: 0x04000E9C RID: 3740
		[CompilerGenerated]
		private ProgressChangedEventHandler ProgressChanged;

		// Token: 0x04000E9D RID: 3741
		[CompilerGenerated]
		private RunWorkerCompletedEventHandler RunWorkerCompleted;
	}
}
