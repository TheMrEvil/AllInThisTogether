using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200066C RID: 1644
	internal class BufferedReadStream : WebReadStream
	{
		// Token: 0x060033DF RID: 13279 RVA: 0x000B4A7E File Offset: 0x000B2C7E
		public BufferedReadStream(WebOperation operation, Stream innerStream, BufferOffsetSize readBuffer) : base(operation, innerStream)
		{
			this.readBuffer = readBuffer;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000B4A90 File Offset: 0x000B2C90
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			BufferedReadStream.<ProcessReadAsync>d__2 <ProcessReadAsync>d__;
			<ProcessReadAsync>d__.<>4__this = this;
			<ProcessReadAsync>d__.buffer = buffer;
			<ProcessReadAsync>d__.offset = offset;
			<ProcessReadAsync>d__.size = size;
			<ProcessReadAsync>d__.cancellationToken = cancellationToken;
			<ProcessReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ProcessReadAsync>d__.<>1__state = -1;
			<ProcessReadAsync>d__.<>t__builder.Start<BufferedReadStream.<ProcessReadAsync>d__2>(ref <ProcessReadAsync>d__);
			return <ProcessReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000B4AF4 File Offset: 0x000B2CF4
		internal bool TryReadFromBuffer(byte[] buffer, int offset, int size, out int result)
		{
			BufferOffsetSize bufferOffsetSize = this.readBuffer;
			int num = (bufferOffsetSize != null) ? bufferOffsetSize.Size : 0;
			if (num <= 0)
			{
				result = 0;
				return base.InnerStream == null;
			}
			int num2 = (num > size) ? size : num;
			Buffer.BlockCopy(this.readBuffer.Buffer, this.readBuffer.Offset, buffer, offset, num2);
			this.readBuffer.Offset += num2;
			this.readBuffer.Size -= num2;
			offset += num2;
			size -= num2;
			result = num2;
			return true;
		}

		// Token: 0x04001E5D RID: 7773
		private readonly BufferOffsetSize readBuffer;

		// Token: 0x0200066D RID: 1645
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ProcessReadAsync>d__2 : IAsyncStateMachine
		{
			// Token: 0x060033E2 RID: 13282 RVA: 0x000B4B84 File Offset: 0x000B2D84
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				BufferedReadStream bufferedReadStream = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						BufferOffsetSize readBuffer = bufferedReadStream.readBuffer;
						int num2 = (readBuffer != null) ? readBuffer.Size : 0;
						if (num2 > 0)
						{
							int num3 = (num2 > this.size) ? this.size : num2;
							Buffer.BlockCopy(bufferedReadStream.readBuffer.Buffer, bufferedReadStream.readBuffer.Offset, this.buffer, this.offset, num3);
							bufferedReadStream.readBuffer.Offset += num3;
							bufferedReadStream.readBuffer.Size -= num3;
							this.offset += num3;
							this.size -= num3;
							result = num3;
							goto IL_171;
						}
						if (bufferedReadStream.InnerStream == null)
						{
							result = 0;
							goto IL_171;
						}
						awaiter = bufferedReadStream.InnerStream.ReadAsync(this.buffer, this.offset, this.size, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, BufferedReadStream.<ProcessReadAsync>d__2>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_171:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060033E3 RID: 13283 RVA: 0x000B4D34 File Offset: 0x000B2F34
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001E5E RID: 7774
			public int <>1__state;

			// Token: 0x04001E5F RID: 7775
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001E60 RID: 7776
			public CancellationToken cancellationToken;

			// Token: 0x04001E61 RID: 7777
			public BufferedReadStream <>4__this;

			// Token: 0x04001E62 RID: 7778
			public int size;

			// Token: 0x04001E63 RID: 7779
			public byte[] buffer;

			// Token: 0x04001E64 RID: 7780
			public int offset;

			// Token: 0x04001E65 RID: 7781
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
