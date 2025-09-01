using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000225 RID: 549
	internal sealed class SqlSequentialStream : Stream
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x0007A3E8 File Offset: 0x000785E8
		internal SqlSequentialStream(SqlDataReader reader, int columnIndex)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
			if (reader.Command != null && reader.Command.CommandTimeout != 0)
			{
				this._readTimeout = (int)Math.Min((long)reader.Command.CommandTimeout * 1000L, 2147483647L);
				return;
			}
			this._readTimeout = -1;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0007A45D File Offset: 0x0007865D
		public override bool CanRead
		{
			get
			{
				return this._reader != null && !this._reader.IsClosed;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00007EED File Offset: 0x000060ED
		public override void Flush()
		{
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00008E4B File Offset: 0x0000704B
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0007A477 File Offset: 0x00078677
		// (set) Token: 0x06001A8F RID: 6799 RVA: 0x0007A47F File Offset: 0x0007867F
		public override int ReadTimeout
		{
			get
			{
				return this._readTimeout;
			}
			set
			{
				if (value > 0 || value == -1)
				{
					this._readTimeout = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("value");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0007A49B File Offset: 0x0007869B
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0007A4A4 File Offset: 0x000786A4
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int bytesInternalSequential;
			try
			{
				bytesInternalSequential = this._reader.GetBytesInternalSequential(this._columnIndex, buffer, offset, count, new long?((long)this._readTimeout));
			}
			catch (SqlException internalException)
			{
				throw ADP.ErrorReadingFromStream(internalException);
			}
			return bytesInternalSequential;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0007A514 File Offset: 0x00078714
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (!this.CanRead)
			{
				completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
			}
			else
			{
				try
				{
					if (Interlocked.CompareExchange<Task>(ref this._currentTask, completion.Task, null) != null)
					{
						completion.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					}
					else
					{
						CancellationTokenSource combinedTokenSource;
						if (!cancellationToken.CanBeCanceled)
						{
							combinedTokenSource = this._disposalTokenSource;
						}
						else
						{
							combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this._disposalTokenSource.Token);
						}
						int result = 0;
						Task<int> task = null;
						SqlDataReader reader = this._reader;
						if (reader != null && !cancellationToken.IsCancellationRequested && !this._disposalTokenSource.Token.IsCancellationRequested)
						{
							task = reader.GetBytesAsync(this._columnIndex, buffer, offset, count, this._readTimeout, combinedTokenSource.Token, out result);
						}
						if (task == null)
						{
							this._currentTask = null;
							if (cancellationToken.IsCancellationRequested)
							{
								completion.SetCanceled();
							}
							else if (!this.CanRead)
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
							else
							{
								completion.SetResult(result);
							}
							if (combinedTokenSource != this._disposalTokenSource)
							{
								combinedTokenSource.Dispose();
							}
						}
						else
						{
							task.ContinueWith(delegate(Task<int> t)
							{
								this._currentTask = null;
								if (t.Status == TaskStatus.RanToCompletion && this.CanRead)
								{
									completion.SetResult(t.Result);
								}
								else if (t.Status == TaskStatus.Faulted)
								{
									if (t.Exception.InnerException is SqlException)
									{
										completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
									}
									else
									{
										completion.SetException(t.Exception.InnerException);
									}
								}
								else if (!this.CanRead)
								{
									completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
								}
								else
								{
									completion.SetCanceled();
								}
								if (combinedTokenSource != this._disposalTokenSource)
								{
									combinedTokenSource.Dispose();
								}
							}, TaskScheduler.Default);
						}
					}
				}
				catch (Exception exception)
				{
					completion.TrySetException(exception);
					Interlocked.CompareExchange<Task>(ref this._currentTask, null, completion.Task);
					throw;
				}
			}
			return completion.Task;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0007A6EC File Offset: 0x000788EC
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0007A705 File Offset: 0x00078905
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00008E4B File Offset: 0x0000704B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00008E4B File Offset: 0x0000704B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0007A710 File Offset: 0x00078910
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0007A745 File Offset: 0x00078945
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0007A758 File Offset: 0x00078958
		internal static void ValidateReadParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw ADP.ArgumentOutOfRange("offset");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(offset + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x040010F9 RID: 4345
		private SqlDataReader _reader;

		// Token: 0x040010FA RID: 4346
		private int _columnIndex;

		// Token: 0x040010FB RID: 4347
		private Task _currentTask;

		// Token: 0x040010FC RID: 4348
		private int _readTimeout;

		// Token: 0x040010FD RID: 4349
		private CancellationTokenSource _disposalTokenSource;

		// Token: 0x02000226 RID: 550
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x06001A9B RID: 6811 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x0007A7BC File Offset: 0x000789BC
			internal void <ReadAsync>b__0(Task<int> t)
			{
				this.<>4__this._currentTask = null;
				if (t.Status == TaskStatus.RanToCompletion && this.<>4__this.CanRead)
				{
					this.completion.SetResult(t.Result);
				}
				else if (t.Status == TaskStatus.Faulted)
				{
					if (t.Exception.InnerException is SqlException)
					{
						this.completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
					}
					else
					{
						this.completion.SetException(t.Exception.InnerException);
					}
				}
				else if (!this.<>4__this.CanRead)
				{
					this.completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this.<>4__this)));
				}
				else
				{
					this.completion.SetCanceled();
				}
				if (this.combinedTokenSource != this.<>4__this._disposalTokenSource)
				{
					this.combinedTokenSource.Dispose();
				}
			}

			// Token: 0x040010FE RID: 4350
			public SqlSequentialStream <>4__this;

			// Token: 0x040010FF RID: 4351
			public TaskCompletionSource<int> completion;

			// Token: 0x04001100 RID: 4352
			public CancellationTokenSource combinedTokenSource;
		}
	}
}
