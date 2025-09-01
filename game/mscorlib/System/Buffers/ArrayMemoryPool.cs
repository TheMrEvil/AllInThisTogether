using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000ADF RID: 2783
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06006308 RID: 25352 RVA: 0x0008406A File Offset: 0x0008226A
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x0014B3B6 File Offset: 0x001495B6
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x0014B3E2 File Offset: 0x001495E2
		public ArrayMemoryPool()
		{
		}

		// Token: 0x04003A59 RID: 14937
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x02000AE0 RID: 2784
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x0600630C RID: 25356 RVA: 0x0014B3EA File Offset: 0x001495EA
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x17001185 RID: 4485
			// (get) Token: 0x0600630D RID: 25357 RVA: 0x0014B403 File Offset: 0x00149603
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x0600630E RID: 25358 RVA: 0x0014B418 File Offset: 0x00149618
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04003A5A RID: 14938
			private T[] _array;
		}
	}
}
