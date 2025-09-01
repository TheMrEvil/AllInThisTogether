using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000015 RID: 21
	internal abstract class Base64Encoder
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002DC7 File Offset: 0x00000FC7
		internal Base64Encoder()
		{
			this.charsLine = new char[76];
		}

		// Token: 0x0600003E RID: 62
		internal abstract void WriteChars(char[] chars, int index, int count);

		// Token: 0x0600003F RID: 63 RVA: 0x00002DDC File Offset: 0x00000FDC
		internal void Encode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.leftOverBytesCount > 0)
			{
				int num = this.leftOverBytesCount;
				while (num < 3 && count > 0)
				{
					this.leftOverBytes[num++] = buffer[index++];
					count--;
				}
				if (count == 0 && num < 3)
				{
					this.leftOverBytesCount = num;
					return;
				}
				int count2 = Convert.ToBase64CharArray(this.leftOverBytes, 0, 3, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, count2);
			}
			this.leftOverBytesCount = count % 3;
			if (this.leftOverBytesCount > 0)
			{
				count -= this.leftOverBytesCount;
				if (this.leftOverBytes == null)
				{
					this.leftOverBytes = new byte[3];
				}
				for (int i = 0; i < this.leftOverBytesCount; i++)
				{
					this.leftOverBytes[i] = buffer[index + count + i];
				}
			}
			int num2 = index + count;
			int num3 = 57;
			while (index < num2)
			{
				if (index + num3 > num2)
				{
					num3 = num2 - index;
				}
				int count3 = Convert.ToBase64CharArray(buffer, index, num3, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, count3);
				index += num3;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F20 File Offset: 0x00001120
		internal void Flush()
		{
			if (this.leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(this.leftOverBytes, 0, this.leftOverBytesCount, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, count);
				this.leftOverBytesCount = 0;
			}
		}

		// Token: 0x06000041 RID: 65
		internal abstract Task WriteCharsAsync(char[] chars, int index, int count);

		// Token: 0x06000042 RID: 66 RVA: 0x00002F68 File Offset: 0x00001168
		internal Task EncodeAsync(byte[] buffer, int index, int count)
		{
			Base64Encoder.<EncodeAsync>d__10 <EncodeAsync>d__;
			<EncodeAsync>d__.<>4__this = this;
			<EncodeAsync>d__.buffer = buffer;
			<EncodeAsync>d__.index = index;
			<EncodeAsync>d__.count = count;
			<EncodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EncodeAsync>d__.<>1__state = -1;
			<EncodeAsync>d__.<>t__builder.Start<Base64Encoder.<EncodeAsync>d__10>(ref <EncodeAsync>d__);
			return <EncodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FC4 File Offset: 0x000011C4
		internal Task FlushAsync()
		{
			Base64Encoder.<FlushAsync>d__11 <FlushAsync>d__;
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<Base64Encoder.<FlushAsync>d__11>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040004EB RID: 1259
		private byte[] leftOverBytes;

		// Token: 0x040004EC RID: 1260
		private int leftOverBytesCount;

		// Token: 0x040004ED RID: 1261
		private char[] charsLine;

		// Token: 0x040004EE RID: 1262
		internal const int Base64LineSize = 76;

		// Token: 0x040004EF RID: 1263
		internal const int LineSizeInBytes = 57;

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EncodeAsync>d__10 : IAsyncStateMachine
		{
			// Token: 0x06000044 RID: 68 RVA: 0x00003008 File Offset: 0x00001208
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				Base64Encoder base64Encoder = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_2B2;
						}
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.count > this.buffer.Length - this.index)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (base64Encoder.leftOverBytesCount <= 0)
						{
							goto IL_170;
						}
						int leftOverBytesCount = base64Encoder.leftOverBytesCount;
						while (leftOverBytesCount < 3 && this.count > 0)
						{
							byte[] leftOverBytes = base64Encoder.leftOverBytes;
							int num2 = leftOverBytesCount++;
							byte[] array = this.buffer;
							int num3 = this.index;
							this.index = num3 + 1;
							leftOverBytes[num2] = array[num3];
							num3 = this.count;
							this.count = num3 - 1;
						}
						if (this.count == 0 && leftOverBytesCount < 3)
						{
							base64Encoder.leftOverBytesCount = leftOverBytesCount;
							goto IL_2F8;
						}
						int num4 = Convert.ToBase64CharArray(base64Encoder.leftOverBytes, 0, 3, base64Encoder.charsLine, 0);
						awaiter = base64Encoder.WriteCharsAsync(base64Encoder.charsLine, 0, num4).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Base64Encoder.<EncodeAsync>d__10>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					IL_170:
					base64Encoder.leftOverBytesCount = this.count % 3;
					if (base64Encoder.leftOverBytesCount > 0)
					{
						this.count -= base64Encoder.leftOverBytesCount;
						if (base64Encoder.leftOverBytes == null)
						{
							base64Encoder.leftOverBytes = new byte[3];
						}
						for (int i = 0; i < base64Encoder.leftOverBytesCount; i++)
						{
							base64Encoder.leftOverBytes[i] = this.buffer[this.index + this.count + i];
						}
					}
					this.<endIndex>5__2 = this.index + this.count;
					this.<chunkSize>5__3 = 57;
					goto IL_2CC;
					IL_2B2:
					awaiter.GetResult();
					this.index += this.<chunkSize>5__3;
					IL_2CC:
					if (this.index < this.<endIndex>5__2)
					{
						if (this.index + this.<chunkSize>5__3 > this.<endIndex>5__2)
						{
							this.<chunkSize>5__3 = this.<endIndex>5__2 - this.index;
						}
						int num5 = Convert.ToBase64CharArray(this.buffer, this.index, this.<chunkSize>5__3, base64Encoder.charsLine, 0);
						awaiter = base64Encoder.WriteCharsAsync(base64Encoder.charsLine, 0, num5).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Base64Encoder.<EncodeAsync>d__10>(ref awaiter, ref this);
							return;
						}
						goto IL_2B2;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2F8:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000045 RID: 69 RVA: 0x0000333C File Offset: 0x0000153C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004F0 RID: 1264
			public int <>1__state;

			// Token: 0x040004F1 RID: 1265
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004F2 RID: 1266
			public byte[] buffer;

			// Token: 0x040004F3 RID: 1267
			public int index;

			// Token: 0x040004F4 RID: 1268
			public int count;

			// Token: 0x040004F5 RID: 1269
			public Base64Encoder <>4__this;

			// Token: 0x040004F6 RID: 1270
			private int <endIndex>5__2;

			// Token: 0x040004F7 RID: 1271
			private int <chunkSize>5__3;

			// Token: 0x040004F8 RID: 1272
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsync>d__11 : IAsyncStateMachine
		{
			// Token: 0x06000046 RID: 70 RVA: 0x0000334C File Offset: 0x0000154C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				Base64Encoder base64Encoder = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (base64Encoder.leftOverBytesCount <= 0)
						{
							goto IL_A8;
						}
						int count = Convert.ToBase64CharArray(base64Encoder.leftOverBytes, 0, base64Encoder.leftOverBytesCount, base64Encoder.charsLine, 0);
						awaiter = base64Encoder.WriteCharsAsync(base64Encoder.charsLine, 0, count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Base64Encoder.<FlushAsync>d__11>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					base64Encoder.leftOverBytesCount = 0;
					IL_A8:;
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

			// Token: 0x06000047 RID: 71 RVA: 0x00003440 File Offset: 0x00001640
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004F9 RID: 1273
			public int <>1__state;

			// Token: 0x040004FA RID: 1274
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004FB RID: 1275
			public Base64Encoder <>4__this;

			// Token: 0x040004FC RID: 1276
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
