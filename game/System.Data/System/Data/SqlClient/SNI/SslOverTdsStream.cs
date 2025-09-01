using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x020002A2 RID: 674
	internal sealed class SslOverTdsStream : Stream
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x0009284C File Offset: 0x00090A4C
		public SslOverTdsStream(Stream stream)
		{
			this._stream = stream;
			this._encapsulate = true;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00092862 File Offset: 0x00090A62
		public void FinishHandshake()
		{
			this._encapsulate = false;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0009286C File Offset: 0x00090A6C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.ReadInternal(buffer, offset, count, CancellationToken.None, false).GetAwaiter().GetResult();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00092895 File Offset: 0x00090A95
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteInternal(buffer, offset, count, CancellationToken.None, false).Wait();
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000928AB File Offset: 0x00090AAB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken token)
		{
			return this.WriteInternal(buffer, offset, count, token, true);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000928B9 File Offset: 0x00090AB9
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken token)
		{
			return this.ReadInternal(buffer, offset, count, token, true);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000928C8 File Offset: 0x00090AC8
		private Task<int> ReadInternal(byte[] buffer, int offset, int count, CancellationToken token, bool async)
		{
			SslOverTdsStream.<ReadInternal>d__11 <ReadInternal>d__;
			<ReadInternal>d__.<>4__this = this;
			<ReadInternal>d__.buffer = buffer;
			<ReadInternal>d__.offset = offset;
			<ReadInternal>d__.count = count;
			<ReadInternal>d__.token = token;
			<ReadInternal>d__.async = async;
			<ReadInternal>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadInternal>d__.<>1__state = -1;
			<ReadInternal>d__.<>t__builder.Start<SslOverTdsStream.<ReadInternal>d__11>(ref <ReadInternal>d__);
			return <ReadInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00092938 File Offset: 0x00090B38
		private Task WriteInternal(byte[] buffer, int offset, int count, CancellationToken token, bool async)
		{
			SslOverTdsStream.<WriteInternal>d__12 <WriteInternal>d__;
			<WriteInternal>d__.<>4__this = this;
			<WriteInternal>d__.buffer = buffer;
			<WriteInternal>d__.offset = offset;
			<WriteInternal>d__.count = count;
			<WriteInternal>d__.token = token;
			<WriteInternal>d__.async = async;
			<WriteInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteInternal>d__.<>1__state = -1;
			<WriteInternal>d__.<>t__builder.Start<SslOverTdsStream.<WriteInternal>d__12>(ref <WriteInternal>d__);
			return <WriteInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00087F51 File Offset: 0x00086151
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000929A5 File Offset: 0x00090BA5
		public override void Flush()
		{
			if (!(this._stream is PipeStream))
			{
				this._stream.Flush();
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00087F51 File Offset: 0x00086151
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x00087F51 File Offset: 0x00086151
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00087F51 File Offset: 0x00086151
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000929BF File Offset: 0x00090BBF
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000929CC File Offset: 0x00090BCC
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00087F51 File Offset: 0x00086151
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001580 RID: 5504
		private readonly Stream _stream;

		// Token: 0x04001581 RID: 5505
		private int _packetBytes;

		// Token: 0x04001582 RID: 5506
		private bool _encapsulate;

		// Token: 0x04001583 RID: 5507
		private const int PACKET_SIZE_WITHOUT_HEADER = 4088;

		// Token: 0x04001584 RID: 5508
		private const int PRELOGIN_PACKET_TYPE = 18;

		// Token: 0x020002A3 RID: 675
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadInternal>d__11 : IAsyncStateMachine
		{
			// Token: 0x06001F0C RID: 7948 RVA: 0x000929DC File Offset: 0x00090BDC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SslOverTdsStream sslOverTdsStream = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					int num2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1D5;
						}
						num2 = 0;
						this.<packetData>5__2 = new byte[(this.count < 8) ? 8 : this.count];
						if (!sslOverTdsStream._encapsulate)
						{
							goto IL_151;
						}
						if (sslOverTdsStream._packetBytes == 0)
						{
							goto IL_109;
						}
						goto IL_137;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_DD:
					int num3 = awaiter.GetResult();
					IL_FF:
					num2 = this.<>7__wrap2 + num3;
					IL_109:
					if (num2 >= 8)
					{
						sslOverTdsStream._packetBytes = ((int)this.<packetData>5__2[2] << 8 | (int)this.<packetData>5__2[3]);
						sslOverTdsStream._packetBytes -= 8;
					}
					else
					{
						this.<>7__wrap2 = num2;
						if (!this.async)
						{
							num3 = sslOverTdsStream._stream.Read(this.<packetData>5__2, num2, 8 - num2);
							goto IL_FF;
						}
						awaiter = sslOverTdsStream._stream.ReadAsync(this.<packetData>5__2, num2, 8 - num2, this.token).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SslOverTdsStream.<ReadInternal>d__11>(ref awaiter, ref this);
							return;
						}
						goto IL_DD;
					}
					IL_137:
					if (this.count > sslOverTdsStream._packetBytes)
					{
						this.count = sslOverTdsStream._packetBytes;
					}
					IL_151:
					if (!this.async)
					{
						num3 = sslOverTdsStream._stream.Read(this.<packetData>5__2, 0, this.count);
						goto IL_1FA;
					}
					awaiter = sslOverTdsStream._stream.ReadAsync(this.<packetData>5__2, 0, this.count, this.token).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, SslOverTdsStream.<ReadInternal>d__11>(ref awaiter, ref this);
						return;
					}
					IL_1D5:
					num3 = awaiter.GetResult();
					IL_1FA:
					num2 = num3;
					if (sslOverTdsStream._encapsulate)
					{
						sslOverTdsStream._packetBytes -= num2;
					}
					Buffer.BlockCopy(this.<packetData>5__2, 0, this.buffer, this.offset, num2);
					result = num2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<packetData>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<packetData>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001F0D RID: 7949 RVA: 0x00092C70 File Offset: 0x00090E70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001585 RID: 5509
			public int <>1__state;

			// Token: 0x04001586 RID: 5510
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001587 RID: 5511
			public int count;

			// Token: 0x04001588 RID: 5512
			public SslOverTdsStream <>4__this;

			// Token: 0x04001589 RID: 5513
			public bool async;

			// Token: 0x0400158A RID: 5514
			public CancellationToken token;

			// Token: 0x0400158B RID: 5515
			public byte[] buffer;

			// Token: 0x0400158C RID: 5516
			public int offset;

			// Token: 0x0400158D RID: 5517
			private byte[] <packetData>5__2;

			// Token: 0x0400158E RID: 5518
			private int <>7__wrap2;

			// Token: 0x0400158F RID: 5519
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020002A4 RID: 676
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteInternal>d__12 : IAsyncStateMachine
		{
			// Token: 0x06001F0E RID: 7950 RVA: 0x00092C80 File Offset: 0x00090E80
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SslOverTdsStream sslOverTdsStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_22C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2BD;
					default:
						this.<currentCount>5__2 = 0;
						this.<currentOffset>5__3 = this.offset;
						goto IL_2E4;
					}
					IL_16F:
					awaiter.GetResult();
					goto IL_252;
					IL_22C:
					awaiter.GetResult();
					IL_252:
					if (!this.async)
					{
						sslOverTdsStream._stream.Flush();
						goto IL_2D1;
					}
					awaiter = sslOverTdsStream._stream.FlushAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SslOverTdsStream.<WriteInternal>d__12>(ref awaiter, ref this);
						return;
					}
					IL_2BD:
					awaiter.GetResult();
					IL_2D1:
					this.<currentOffset>5__3 += this.<currentCount>5__2;
					IL_2E4:
					if (this.count > 0)
					{
						if (sslOverTdsStream._encapsulate)
						{
							if (this.count > 4088)
							{
								this.<currentCount>5__2 = 4088;
							}
							else
							{
								this.<currentCount>5__2 = this.count;
							}
							this.count -= this.<currentCount>5__2;
							byte[] array = new byte[8 + this.<currentCount>5__2];
							array[0] = 18;
							array[1] = ((this.count > 0) ? 0 : 1);
							array[2] = (byte)((this.<currentCount>5__2 + 8) / 256);
							array[3] = (byte)((this.<currentCount>5__2 + 8) % 256);
							array[4] = 0;
							array[5] = 0;
							array[6] = 0;
							array[7] = 0;
							for (int i = 8; i < array.Length; i++)
							{
								array[i] = this.buffer[this.<currentOffset>5__3 + (i - 8)];
							}
							if (!this.async)
							{
								sslOverTdsStream._stream.Write(array, 0, array.Length);
								goto IL_252;
							}
							awaiter = sslOverTdsStream._stream.WriteAsync(array, 0, array.Length, this.token).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SslOverTdsStream.<WriteInternal>d__12>(ref awaiter, ref this);
								return;
							}
							goto IL_16F;
						}
						else
						{
							this.<currentCount>5__2 = this.count;
							this.count = 0;
							if (!this.async)
							{
								sslOverTdsStream._stream.Write(this.buffer, this.<currentOffset>5__3, this.<currentCount>5__2);
								goto IL_252;
							}
							awaiter = sslOverTdsStream._stream.WriteAsync(this.buffer, this.<currentOffset>5__3, this.<currentCount>5__2, this.token).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SslOverTdsStream.<WriteInternal>d__12>(ref awaiter, ref this);
								return;
							}
							goto IL_22C;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06001F0F RID: 7951 RVA: 0x00092FC8 File Offset: 0x000911C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001590 RID: 5520
			public int <>1__state;

			// Token: 0x04001591 RID: 5521
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001592 RID: 5522
			public int offset;

			// Token: 0x04001593 RID: 5523
			public SslOverTdsStream <>4__this;

			// Token: 0x04001594 RID: 5524
			public int count;

			// Token: 0x04001595 RID: 5525
			public byte[] buffer;

			// Token: 0x04001596 RID: 5526
			public bool async;

			// Token: 0x04001597 RID: 5527
			public CancellationToken token;

			// Token: 0x04001598 RID: 5528
			private int <currentCount>5__2;

			// Token: 0x04001599 RID: 5529
			private int <currentOffset>5__3;

			// Token: 0x0400159A RID: 5530
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
