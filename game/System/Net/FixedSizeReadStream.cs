using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000683 RID: 1667
	internal class FixedSizeReadStream : WebReadStream
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000B798B File Offset: 0x000B5B8B
		public long ContentLength
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentLength>k__BackingField;
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000B7993 File Offset: 0x000B5B93
		public FixedSizeReadStream(WebOperation operation, Stream innerStream, long contentLength) : base(operation, innerStream)
		{
			this.ContentLength = contentLength;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000B79A4 File Offset: 0x000B5BA4
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			FixedSizeReadStream.<ProcessReadAsync>d__5 <ProcessReadAsync>d__;
			<ProcessReadAsync>d__.<>4__this = this;
			<ProcessReadAsync>d__.buffer = buffer;
			<ProcessReadAsync>d__.offset = offset;
			<ProcessReadAsync>d__.size = size;
			<ProcessReadAsync>d__.cancellationToken = cancellationToken;
			<ProcessReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ProcessReadAsync>d__.<>1__state = -1;
			<ProcessReadAsync>d__.<>t__builder.Start<FixedSizeReadStream.<ProcessReadAsync>d__5>(ref <ProcessReadAsync>d__);
			return <ProcessReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04001EA0 RID: 7840
		[CompilerGenerated]
		private readonly long <ContentLength>k__BackingField;

		// Token: 0x04001EA1 RID: 7841
		private long position;

		// Token: 0x02000684 RID: 1668
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessReadAsync>d__5 : IAsyncStateMachine
		{
			// Token: 0x06003490 RID: 13456 RVA: 0x000B7A08 File Offset: 0x000B5C08
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				FixedSizeReadStream fixedSizeReadStream = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						long num2 = fixedSizeReadStream.ContentLength - fixedSizeReadStream.position;
						if (num2 == 0L)
						{
							result = 0;
							goto IL_FF;
						}
						int count = (int)Math.Min(num2, (long)this.size);
						awaiter = fixedSizeReadStream.InnerStream.ReadAsync(this.buffer, this.offset, count, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, FixedSizeReadStream.<ProcessReadAsync>d__5>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter.GetResult();
					if (result2 <= 0)
					{
						result = result2;
					}
					else
					{
						fixedSizeReadStream.position += (long)result2;
						result = result2;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_FF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06003491 RID: 13457 RVA: 0x000B7B38 File Offset: 0x000B5D38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001EA2 RID: 7842
			public int <>1__state;

			// Token: 0x04001EA3 RID: 7843
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001EA4 RID: 7844
			public CancellationToken cancellationToken;

			// Token: 0x04001EA5 RID: 7845
			public FixedSizeReadStream <>4__this;

			// Token: 0x04001EA6 RID: 7846
			public int size;

			// Token: 0x04001EA7 RID: 7847
			public byte[] buffer;

			// Token: 0x04001EA8 RID: 7848
			public int offset;

			// Token: 0x04001EA9 RID: 7849
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
