using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000297 RID: 663
	internal class SNIPacket : IDisposable, IEquatable<SNIPacket>
	{
		// Token: 0x06001E8D RID: 7821 RVA: 0x00090904 File Offset: 0x0008EB04
		public SNIPacket()
		{
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00090917 File Offset: 0x0008EB17
		public SNIPacket(int capacity)
		{
			this.Allocate(capacity);
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00090931 File Offset: 0x0008EB31
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x00090939 File Offset: 0x0008EB39
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00090942 File Offset: 0x0008EB42
		public int DataLeft
		{
			get
			{
				return this._length - this._offset;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x00090951 File Offset: 0x0008EB51
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00090959 File Offset: 0x0008EB59
		public bool IsInvalid
		{
			get
			{
				return this._data == null;
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00090964 File Offset: 0x0008EB64
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009096C File Offset: 0x0008EB6C
		public void SetCompletionCallback(SNIAsyncCallback completionCallback)
		{
			this._completionCallback = completionCallback;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00090975 File Offset: 0x0008EB75
		public void InvokeCompletionCallback(uint sniErrorCode)
		{
			this._completionCallback(this, sniErrorCode);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00090984 File Offset: 0x0008EB84
		public void Allocate(int capacity)
		{
			if (this._data != null && this._data.Length < capacity)
			{
				if (this._isBufferFromArrayPool)
				{
					this._arrayPool.Return(this._data, false);
				}
				this._data = null;
			}
			if (this._data == null)
			{
				this._data = this._arrayPool.Rent(capacity);
				this._isBufferFromArrayPool = true;
			}
			this._capacity = capacity;
			this._length = 0;
			this._offset = 0;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000909FC File Offset: 0x0008EBFC
		public SNIPacket Clone()
		{
			SNIPacket snipacket = new SNIPacket(this._capacity);
			Buffer.BlockCopy(this._data, 0, snipacket._data, 0, this._capacity);
			snipacket._length = this._length;
			snipacket._description = this._description;
			snipacket._completionCallback = this._completionCallback;
			return snipacket;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00090A53 File Offset: 0x0008EC53
		public void GetData(byte[] buffer, ref int dataSize)
		{
			Buffer.BlockCopy(this._data, 0, buffer, 0, this._length);
			dataSize = this._length;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00090A71 File Offset: 0x0008EC71
		public void SetData(byte[] data, int length)
		{
			this._data = data;
			this._length = length;
			this._capacity = data.Length;
			this._offset = 0;
			this._isBufferFromArrayPool = false;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00090A98 File Offset: 0x0008EC98
		public int TakeData(SNIPacket packet, int size)
		{
			int num = this.TakeData(packet._data, packet._length, size);
			packet._length += num;
			return num;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00090AC8 File Offset: 0x0008ECC8
		public void AppendData(byte[] data, int size)
		{
			Buffer.BlockCopy(data, 0, this._data, this._length, size);
			this._length += size;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00090AEC File Offset: 0x0008ECEC
		public void AppendPacket(SNIPacket packet)
		{
			Buffer.BlockCopy(packet._data, 0, this._data, this._length, packet._length);
			this._length += packet._length;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00090B20 File Offset: 0x0008ED20
		public int TakeData(byte[] buffer, int dataOffset, int size)
		{
			if (this._offset >= this._length)
			{
				return 0;
			}
			if (this._offset + size > this._length)
			{
				size = this._length - this._offset;
			}
			Buffer.BlockCopy(this._data, this._offset, buffer, dataOffset, size);
			this._offset += size;
			return size;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00090B7F File Offset: 0x0008ED7F
		public void Release()
		{
			if (this._data != null)
			{
				if (this._isBufferFromArrayPool)
				{
					this._arrayPool.Return(this._data, false);
				}
				this._data = null;
				this._capacity = 0;
			}
			this.Reset();
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00090BB7 File Offset: 0x0008EDB7
		public void Reset()
		{
			this._length = 0;
			this._offset = 0;
			this._description = null;
			this._completionCallback = null;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		public void ReadFromStreamAsync(Stream stream, SNIAsyncCallback callback)
		{
			bool error = false;
			stream.ReadAsync(this._data, 0, this._capacity, CancellationToken.None).ContinueWith(delegate(Task<int> t)
			{
				AggregateException exception = t.Exception;
				Exception ex = (exception != null) ? exception.InnerException : null;
				if (ex != null)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 35U, ex);
					error = true;
				}
				else
				{
					this._length = t.Result;
					if (this._length == 0)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 2U, string.Empty);
						error = true;
					}
				}
				if (error)
				{
					this.Release();
				}
				callback(this, error ? 1U : 0U);
			}, CancellationToken.None, TaskContinuationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00090C35 File Offset: 0x0008EE35
		public void ReadFromStream(Stream stream)
		{
			this._length = stream.Read(this._data, 0, this._capacity);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00090C50 File Offset: 0x0008EE50
		public void WriteToStream(Stream stream)
		{
			stream.Write(this._data, 0, this._length);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00090C68 File Offset: 0x0008EE68
		public void WriteToStreamAsync(Stream stream, SNIAsyncCallback callback, SNIProviders provider, bool disposeAfterWriteAsync = false)
		{
			SNIPacket.<WriteToStreamAsync>d__35 <WriteToStreamAsync>d__;
			<WriteToStreamAsync>d__.<>4__this = this;
			<WriteToStreamAsync>d__.stream = stream;
			<WriteToStreamAsync>d__.callback = callback;
			<WriteToStreamAsync>d__.provider = provider;
			<WriteToStreamAsync>d__.disposeAfterWriteAsync = disposeAfterWriteAsync;
			<WriteToStreamAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<WriteToStreamAsync>d__.<>1__state = -1;
			<WriteToStreamAsync>d__.<>t__builder.Start<SNIPacket.<WriteToStreamAsync>d__35>(ref <WriteToStreamAsync>d__);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0003EB4E File Offset: 0x0003CD4E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00090CC0 File Offset: 0x0008EEC0
		public override bool Equals(object obj)
		{
			SNIPacket snipacket = obj as SNIPacket;
			return snipacket != null && this.Equals(snipacket);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00090CE0 File Offset: 0x0008EEE0
		public bool Equals(SNIPacket packet)
		{
			return packet != null && packet == this;
		}

		// Token: 0x04001532 RID: 5426
		private byte[] _data;

		// Token: 0x04001533 RID: 5427
		private int _length;

		// Token: 0x04001534 RID: 5428
		private int _capacity;

		// Token: 0x04001535 RID: 5429
		private int _offset;

		// Token: 0x04001536 RID: 5430
		private string _description;

		// Token: 0x04001537 RID: 5431
		private SNIAsyncCallback _completionCallback;

		// Token: 0x04001538 RID: 5432
		private ArrayPool<byte> _arrayPool = ArrayPool<byte>.Shared;

		// Token: 0x04001539 RID: 5433
		private bool _isBufferFromArrayPool;

		// Token: 0x02000298 RID: 664
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x06001EA8 RID: 7848 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x06001EA9 RID: 7849 RVA: 0x00090CEC File Offset: 0x0008EEEC
			internal void <ReadFromStreamAsync>b__0(Task<int> t)
			{
				AggregateException exception = t.Exception;
				Exception ex = (exception != null) ? exception.InnerException : null;
				if (ex != null)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 35U, ex);
					this.error = true;
				}
				else
				{
					this.<>4__this._length = t.Result;
					if (this.<>4__this._length == 0)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 2U, string.Empty);
						this.error = true;
					}
				}
				if (this.error)
				{
					this.<>4__this.Release();
				}
				this.callback(this.<>4__this, this.error ? 1U : 0U);
			}

			// Token: 0x0400153A RID: 5434
			public bool error;

			// Token: 0x0400153B RID: 5435
			public SNIPacket <>4__this;

			// Token: 0x0400153C RID: 5436
			public SNIAsyncCallback callback;
		}

		// Token: 0x02000299 RID: 665
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteToStreamAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x06001EAA RID: 7850 RVA: 0x00090D98 File Offset: 0x0008EF98
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SNIPacket snipacket = this.<>4__this;
				try
				{
					if (num != 0)
					{
						this.<status>5__2 = 0U;
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.stream.WriteAsync(snipacket._data, 0, snipacket._length, CancellationToken.None).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, SNIPacket.<WriteToStreamAsync>d__35>(ref awaiter, ref this);
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
					}
					catch (Exception sniException)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(this.provider, 35U, sniException);
						this.<status>5__2 = 1U;
					}
					this.callback(snipacket, this.<status>5__2);
					if (this.disposeAfterWriteAsync)
					{
						snipacket.Dispose();
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

			// Token: 0x06001EAB RID: 7851 RVA: 0x00090ECC File Offset: 0x0008F0CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400153D RID: 5437
			public int <>1__state;

			// Token: 0x0400153E RID: 5438
			public AsyncVoidMethodBuilder <>t__builder;

			// Token: 0x0400153F RID: 5439
			public Stream stream;

			// Token: 0x04001540 RID: 5440
			public SNIPacket <>4__this;

			// Token: 0x04001541 RID: 5441
			public SNIProviders provider;

			// Token: 0x04001542 RID: 5442
			public SNIAsyncCallback callback;

			// Token: 0x04001543 RID: 5443
			public bool disposeAfterWriteAsync;

			// Token: 0x04001544 RID: 5444
			private uint <status>5__2;

			// Token: 0x04001545 RID: 5445
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
