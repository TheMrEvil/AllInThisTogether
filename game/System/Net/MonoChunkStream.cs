using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006A5 RID: 1701
	internal class MonoChunkStream : WebReadStream
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000BF25D File Offset: 0x000BD45D
		protected WebHeaderCollection Headers
		{
			[CompilerGenerated]
			get
			{
				return this.<Headers>k__BackingField;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000BF265 File Offset: 0x000BD465
		protected MonoChunkParser Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000BF26D File Offset: 0x000BD46D
		public MonoChunkStream(WebOperation operation, Stream innerStream, WebHeaderCollection headers) : base(operation, innerStream)
		{
			this.Headers = headers;
			this.Decoder = new MonoChunkParser(headers);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000BF28C File Offset: 0x000BD48C
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			MonoChunkStream.<ProcessReadAsync>d__7 <ProcessReadAsync>d__;
			<ProcessReadAsync>d__.<>4__this = this;
			<ProcessReadAsync>d__.buffer = buffer;
			<ProcessReadAsync>d__.offset = offset;
			<ProcessReadAsync>d__.size = size;
			<ProcessReadAsync>d__.cancellationToken = cancellationToken;
			<ProcessReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ProcessReadAsync>d__.<>1__state = -1;
			<ProcessReadAsync>d__.<>t__builder.Start<MonoChunkStream.<ProcessReadAsync>d__7>(ref <ProcessReadAsync>d__);
			return <ProcessReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000BF2F0 File Offset: 0x000BD4F0
		internal override Task FinishReading(CancellationToken cancellationToken)
		{
			MonoChunkStream.<FinishReading>d__8 <FinishReading>d__;
			<FinishReading>d__.<>4__this = this;
			<FinishReading>d__.cancellationToken = cancellationToken;
			<FinishReading>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReading>d__.<>1__state = -1;
			<FinishReading>d__.<>t__builder.Start<MonoChunkStream.<FinishReading>d__8>(ref <FinishReading>d__);
			return <FinishReading>d__.<>t__builder.Task;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000BF33B File Offset: 0x000BD53B
		private static void ThrowExpectingChunkTrailer()
		{
			throw new WebException("Expecting chunk trailer.", null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000BF34B File Offset: 0x000BD54B
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__0(CancellationToken cancellationToken)
		{
			return base.FinishReading(cancellationToken);
		}

		// Token: 0x04001FBE RID: 8126
		[CompilerGenerated]
		private readonly WebHeaderCollection <Headers>k__BackingField;

		// Token: 0x04001FBF RID: 8127
		[CompilerGenerated]
		private readonly MonoChunkParser <Decoder>k__BackingField;

		// Token: 0x020006A6 RID: 1702
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessReadAsync>d__7 : IAsyncStateMachine
		{
			// Token: 0x0600367B RID: 13947 RVA: 0x000BF354 File Offset: 0x000BD554
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MonoChunkStream monoChunkStream = this.<>4__this;
				int result;
				try
				{
					int num2;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						if (monoChunkStream.Decoder.DataAvailable)
						{
							result = monoChunkStream.Decoder.Read(this.buffer, this.offset, this.size);
							goto IL_196;
						}
						num2 = 0;
						this.<moreBytes>5__2 = null;
						goto IL_15F;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_11E:
					num2 = awaiter.GetResult();
					if (num2 <= 0)
					{
						result = num2;
						goto IL_196;
					}
					monoChunkStream.Decoder.Write(this.<moreBytes>5__2, 0, num2);
					num2 = monoChunkStream.Decoder.Read(this.buffer, this.offset, this.size);
					IL_15F:
					if (num2 != 0 || !monoChunkStream.Decoder.WantMore)
					{
						result = num2;
					}
					else
					{
						int num3 = monoChunkStream.Decoder.ChunkLeft;
						if (num3 <= 0)
						{
							num3 = 1024;
						}
						else if (num3 > 16384)
						{
							num3 = 16384;
						}
						if (this.<moreBytes>5__2 == null || this.<moreBytes>5__2.Length < num3)
						{
							this.<moreBytes>5__2 = new byte[num3];
						}
						awaiter = monoChunkStream.InnerStream.ReadAsync(this.<moreBytes>5__2, 0, num3, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, MonoChunkStream.<ProcessReadAsync>d__7>(ref awaiter, ref this);
							return;
						}
						goto IL_11E;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<moreBytes>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_196:
				this.<>1__state = -2;
				this.<moreBytes>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600367C RID: 13948 RVA: 0x000BF530 File Offset: 0x000BD730
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FC0 RID: 8128
			public int <>1__state;

			// Token: 0x04001FC1 RID: 8129
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001FC2 RID: 8130
			public CancellationToken cancellationToken;

			// Token: 0x04001FC3 RID: 8131
			public MonoChunkStream <>4__this;

			// Token: 0x04001FC4 RID: 8132
			public byte[] buffer;

			// Token: 0x04001FC5 RID: 8133
			public int offset;

			// Token: 0x04001FC6 RID: 8134
			public int size;

			// Token: 0x04001FC7 RID: 8135
			private byte[] <moreBytes>5__2;

			// Token: 0x04001FC8 RID: 8136
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006A7 RID: 1703
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishReading>d__8 : IAsyncStateMachine
		{
			// Token: 0x0600367D RID: 13949 RVA: 0x000BF540 File Offset: 0x000BD740
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MonoChunkStream monoChunkStream = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_132;
						}
						awaiter2 = monoChunkStream.<>n__0(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MonoChunkStream.<FinishReading>d__8>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					this.cancellationToken.ThrowIfCancellationRequested();
					if (monoChunkStream.Decoder.DataAvailable)
					{
						MonoChunkStream.ThrowExpectingChunkTrailer();
						goto IL_17E;
					}
					goto IL_17E;
					IL_132:
					int num2 = awaiter.GetResult();
					if (num2 <= 0)
					{
						MonoChunkStream.ThrowExpectingChunkTrailer();
					}
					monoChunkStream.Decoder.Write(this.<buffer>5__2, 0, num2);
					num2 = monoChunkStream.Decoder.Read(this.<buffer>5__2, 0, 1);
					if (num2 != 0)
					{
						MonoChunkStream.ThrowExpectingChunkTrailer();
					}
					this.<buffer>5__2 = null;
					IL_17E:
					if (monoChunkStream.Decoder.WantMore)
					{
						this.<buffer>5__2 = new byte[256];
						awaiter = monoChunkStream.InnerStream.ReadAsync(this.<buffer>5__2, 0, this.<buffer>5__2.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, MonoChunkStream.<FinishReading>d__8>(ref awaiter, ref this);
							return;
						}
						goto IL_132;
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

			// Token: 0x0600367E RID: 13950 RVA: 0x000BF728 File Offset: 0x000BD928
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FC9 RID: 8137
			public int <>1__state;

			// Token: 0x04001FCA RID: 8138
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001FCB RID: 8139
			public MonoChunkStream <>4__this;

			// Token: 0x04001FCC RID: 8140
			public CancellationToken cancellationToken;

			// Token: 0x04001FCD RID: 8141
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001FCE RID: 8142
			private byte[] <buffer>5__2;

			// Token: 0x04001FCF RID: 8143
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
