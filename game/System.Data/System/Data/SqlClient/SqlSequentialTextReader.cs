using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000227 RID: 551
	internal sealed class SqlSequentialTextReader : TextReader
	{
		// Token: 0x06001A9D RID: 6813 RVA: 0x0007A8AC File Offset: 0x00078AAC
		internal SqlSequentialTextReader(SqlDataReader reader, int columnIndex, Encoding encoding)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			this._leftOverBytes = null;
			this._peekedChar = -1;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0007A900 File Offset: 0x00078B00
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0007A908 File Offset: 0x00078B08
		public override int Peek()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0007A944 File Offset: 0x00078B44
		public override int Read()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int result = -1;
			if (this.HasPeekedChar)
			{
				result = this._peekedChar;
				this._peekedChar = -1;
			}
			else
			{
				char[] array = new char[1];
				if (this.InternalRead(array, 0, 1) == 1)
				{
					result = (int)array[0];
				}
			}
			return result;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0007A9A0 File Offset: 0x00078BA0
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int num = 0;
			int num2 = count;
			if (num2 > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				num2--;
				this._peekedChar = -1;
			}
			return num + this.InternalRead(buffer, index + num, num2);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0007AA0C File Offset: 0x00078C0C
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (this.IsClosed)
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
						bool flag = true;
						int charsRead = 0;
						int adjustedIndex = index;
						int charsNeeded = count;
						if (this.HasPeekedChar && charsNeeded > 0)
						{
							int peekedChar = this._peekedChar;
							if (peekedChar >= 0)
							{
								buffer[adjustedIndex] = (char)peekedChar;
								int num = adjustedIndex;
								adjustedIndex = num + 1;
								num = charsRead;
								charsRead = num + 1;
								num = charsNeeded;
								charsNeeded = num - 1;
								this._peekedChar = -1;
							}
						}
						int byteBufferUsed;
						byte[] byteBuffer = this.PrepareByteBuffer(charsNeeded, out byteBufferUsed);
						if (byteBufferUsed < byteBuffer.Length || byteBuffer.Length == 0)
						{
							SqlDataReader reader = this._reader;
							if (reader != null)
							{
								int num2;
								Task<int> bytesAsync = reader.GetBytesAsync(this._columnIndex, byteBuffer, byteBufferUsed, byteBuffer.Length - byteBufferUsed, -1, this._disposalTokenSource.Token, out num2);
								if (bytesAsync == null)
								{
									byteBufferUsed += num2;
								}
								else
								{
									flag = false;
									bytesAsync.ContinueWith(delegate(Task<int> t)
									{
										this._currentTask = null;
										if (t.Status == TaskStatus.RanToCompletion && !this.IsClosed)
										{
											try
											{
												int result = t.Result;
												byteBufferUsed += result;
												if (byteBufferUsed > 0)
												{
													charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
												}
												completion.SetResult(charsRead);
												return;
											}
											catch (Exception exception2)
											{
												completion.SetException(exception2);
												return;
											}
										}
										if (this.IsClosed)
										{
											completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
											return;
										}
										if (t.Status == TaskStatus.Faulted)
										{
											if (t.Exception.InnerException is SqlException)
											{
												completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
												return;
											}
											completion.SetException(t.Exception.InnerException);
											return;
										}
										else
										{
											completion.SetCanceled();
										}
									}, TaskScheduler.Default);
								}
								if (flag && byteBufferUsed > 0)
								{
									charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
								}
							}
							else
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
						}
						if (flag)
						{
							this._currentTask = null;
							if (this.IsClosed)
							{
								completion.SetCanceled();
							}
							else
							{
								completion.SetResult(charsRead);
							}
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

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0007AC98 File Offset: 0x00078E98
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0007ACAC File Offset: 0x00078EAC
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			this._peekedChar = -1;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0007ACE8 File Offset: 0x00078EE8
		private int InternalRead(char[] buffer, int index, int count)
		{
			int result;
			try
			{
				int num;
				byte[] array = this.PrepareByteBuffer(count, out num);
				num += this._reader.GetBytesInternalSequential(this._columnIndex, array, num, array.Length - num, null);
				if (num > 0)
				{
					result = this.DecodeBytesToChars(array, num, buffer, index, count);
				}
				else
				{
					result = 0;
				}
			}
			catch (SqlException internalException)
			{
				throw ADP.ErrorReadingFromStream(internalException);
			}
			return result;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0007AD50 File Offset: 0x00078F50
		private byte[] PrepareByteBuffer(int numberOfChars, out int byteBufferUsed)
		{
			byte[] array;
			if (numberOfChars == 0)
			{
				array = Array.Empty<byte>();
				byteBufferUsed = 0;
			}
			else
			{
				int maxByteCount = this._encoding.GetMaxByteCount(numberOfChars);
				if (this._leftOverBytes != null)
				{
					if (this._leftOverBytes.Length > maxByteCount)
					{
						array = this._leftOverBytes;
						byteBufferUsed = array.Length;
					}
					else
					{
						array = new byte[maxByteCount];
						Buffer.BlockCopy(this._leftOverBytes, 0, array, 0, this._leftOverBytes.Length);
						byteBufferUsed = this._leftOverBytes.Length;
					}
				}
				else
				{
					array = new byte[maxByteCount];
					byteBufferUsed = 0;
				}
			}
			return array;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0007ADD0 File Offset: 0x00078FD0
		private int DecodeBytesToChars(byte[] inBuffer, int inBufferCount, char[] outBuffer, int outBufferOffset, int outBufferCount)
		{
			int num;
			int result;
			bool flag;
			this._decoder.Convert(inBuffer, 0, inBufferCount, outBuffer, outBufferOffset, outBufferCount, false, out num, out result, out flag);
			if (!flag && num < inBufferCount)
			{
				this._leftOverBytes = new byte[inBufferCount - num];
				Buffer.BlockCopy(inBuffer, num, this._leftOverBytes, 0, this._leftOverBytes.Length);
			}
			else
			{
				this._leftOverBytes = null;
			}
			return result;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0007AE2C File Offset: 0x0007902C
		private bool IsClosed
		{
			get
			{
				return this._reader == null;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0007AE37 File Offset: 0x00079037
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0007AE48 File Offset: 0x00079048
		internal static void ValidateReadParameters(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw ADP.ArgumentOutOfRange("index");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(index + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x04001101 RID: 4353
		private SqlDataReader _reader;

		// Token: 0x04001102 RID: 4354
		private int _columnIndex;

		// Token: 0x04001103 RID: 4355
		private Encoding _encoding;

		// Token: 0x04001104 RID: 4356
		private Decoder _decoder;

		// Token: 0x04001105 RID: 4357
		private byte[] _leftOverBytes;

		// Token: 0x04001106 RID: 4358
		private int _peekedChar;

		// Token: 0x04001107 RID: 4359
		private Task _currentTask;

		// Token: 0x04001108 RID: 4360
		private CancellationTokenSource _disposalTokenSource;

		// Token: 0x02000228 RID: 552
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06001AAB RID: 6827 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06001AAC RID: 6828 RVA: 0x0007AEAC File Offset: 0x000790AC
			internal void <ReadAsync>b__0(Task<int> t)
			{
				this.<>4__this._currentTask = null;
				if (t.Status == TaskStatus.RanToCompletion && !this.<>4__this.IsClosed)
				{
					try
					{
						int result = t.Result;
						this.byteBufferUsed += result;
						if (this.byteBufferUsed > 0)
						{
							this.charsRead += this.<>4__this.DecodeBytesToChars(this.byteBuffer, this.byteBufferUsed, this.buffer, this.adjustedIndex, this.charsNeeded);
						}
						this.completion.SetResult(this.charsRead);
						return;
					}
					catch (Exception exception)
					{
						this.completion.SetException(exception);
						return;
					}
				}
				if (this.<>4__this.IsClosed)
				{
					this.completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this.<>4__this)));
					return;
				}
				if (t.Status == TaskStatus.Faulted)
				{
					if (t.Exception.InnerException is SqlException)
					{
						this.completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
						return;
					}
					this.completion.SetException(t.Exception.InnerException);
					return;
				}
				else
				{
					this.completion.SetCanceled();
				}
			}

			// Token: 0x04001109 RID: 4361
			public SqlSequentialTextReader <>4__this;

			// Token: 0x0400110A RID: 4362
			public char[] buffer;

			// Token: 0x0400110B RID: 4363
			public TaskCompletionSource<int> completion;

			// Token: 0x0400110C RID: 4364
			public int byteBufferUsed;

			// Token: 0x0400110D RID: 4365
			public int charsRead;

			// Token: 0x0400110E RID: 4366
			public byte[] byteBuffer;

			// Token: 0x0400110F RID: 4367
			public int adjustedIndex;

			// Token: 0x04001110 RID: 4368
			public int charsNeeded;
		}
	}
}
