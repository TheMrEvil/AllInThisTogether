using System;

namespace System.Buffers
{
	// Token: 0x02000AEE RID: 2798
	public sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x0600637E RID: 25470 RVA: 0x0014CCCB File Offset: 0x0014AECB
		public ArrayBufferWriter()
		{
			this._buffer = Array.Empty<T>();
			this._index = 0;
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x0014CCE5 File Offset: 0x0014AEE5
		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException("initialCapacity");
			}
			this._buffer = new T[initialCapacity];
			this._index = 0;
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06006380 RID: 25472 RVA: 0x0014CD0F File Offset: 0x0014AF0F
		public ReadOnlyMemory<T> WrittenMemory
		{
			get
			{
				return this._buffer.AsMemory(0, this._index);
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x0014CD28 File Offset: 0x0014AF28
		public ReadOnlySpan<T> WrittenSpan
		{
			get
			{
				return this._buffer.AsSpan(0, this._index);
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06006382 RID: 25474 RVA: 0x0014CD41 File Offset: 0x0014AF41
		public int WrittenCount
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06006383 RID: 25475 RVA: 0x0014CD49 File Offset: 0x0014AF49
		public int Capacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x0014CD53 File Offset: 0x0014AF53
		public int FreeCapacity
		{
			get
			{
				return this._buffer.Length - this._index;
			}
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0014CD64 File Offset: 0x0014AF64
		public void Clear()
		{
			this._buffer.AsSpan(0, this._index).Clear();
			this._index = 0;
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0014CD92 File Offset: 0x0014AF92
		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count");
			}
			if (this._index > this._buffer.Length - count)
			{
				ArrayBufferWriter<T>.ThrowInvalidOperationException_AdvancedTooFar(this._buffer.Length);
			}
			this._index += count;
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x0014CDD0 File Offset: 0x0014AFD0
		public Memory<T> GetMemory(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsMemory(this._index);
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0014CDEA File Offset: 0x0014AFEA
		public Span<T> GetSpan(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsSpan(this._index);
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0014CE04 File Offset: 0x0014B004
		private void CheckAndResizeBuffer(int sizeHint)
		{
			if (sizeHint < 0)
			{
				throw new ArgumentException("sizeHint");
			}
			if (sizeHint == 0)
			{
				sizeHint = 1;
			}
			if (sizeHint > this.FreeCapacity)
			{
				int num = Math.Max(sizeHint, this._buffer.Length);
				if (this._buffer.Length == 0)
				{
					num = Math.Max(num, 256);
				}
				int newSize = checked(this._buffer.Length + num);
				Array.Resize<T>(ref this._buffer, newSize);
			}
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x0014CE6A File Offset: 0x0014B06A
		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException(SR.Format("Cannot advance past the end of the buffer, which has a size of {0}.", capacity));
		}

		// Token: 0x04003A86 RID: 14982
		private T[] _buffer;

		// Token: 0x04003A87 RID: 14983
		private int _index;

		// Token: 0x04003A88 RID: 14984
		private const int DefaultInitialBufferSize = 256;
	}
}
