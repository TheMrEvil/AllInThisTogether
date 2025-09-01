using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020000EF RID: 239
	internal sealed class AsynchronousChannel<T> : IDisposable
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0001CB21 File Offset: 0x0001AD21
		internal AsynchronousChannel(int index, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent) : this(index, 512, chunkSize, cancellationToken, consumerEvent)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001CB34 File Offset: 0x0001AD34
		internal AsynchronousChannel(int index, int capacity, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent)
		{
			if (chunkSize == 0)
			{
				chunkSize = Scheduling.GetDefaultChunkSize<T>();
			}
			this._index = index;
			this._buffer = new T[capacity + 1][];
			this._producerBufferIndex = 0;
			this._consumerBufferIndex = 0;
			this._producerEvent = new ManualResetEventSlim();
			this._consumerEvent = consumerEvent;
			this._chunkSize = chunkSize;
			this._producerChunk = new T[chunkSize];
			this._producerChunkIndex = 0;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		internal bool IsFull
		{
			get
			{
				int producerBufferIndex = this._producerBufferIndex;
				int consumerBufferIndex = this._consumerBufferIndex;
				return producerBufferIndex == consumerBufferIndex - 1 || (consumerBufferIndex == 0 && producerBufferIndex == this._buffer.Length - 1);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001CBE9 File Offset: 0x0001ADE9
		internal bool IsChunkBufferEmpty
		{
			get
			{
				return this._producerBufferIndex == this._consumerBufferIndex;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001CBFD File Offset: 0x0001ADFD
		internal bool IsDone
		{
			get
			{
				return this._done;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001CC07 File Offset: 0x0001AE07
		internal void FlushBuffers()
		{
			this.FlushCachedChunk();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001CC10 File Offset: 0x0001AE10
		internal void SetDone()
		{
			this._done = true;
			lock (this)
			{
				if (this._consumerEvent != null)
				{
					this._consumerEvent.Set(this._index);
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001CC68 File Offset: 0x0001AE68
		internal void Enqueue(T item)
		{
			int producerChunkIndex = this._producerChunkIndex;
			this._producerChunk[producerChunkIndex] = item;
			if (producerChunkIndex == this._chunkSize - 1)
			{
				this.EnqueueChunk(this._producerChunk);
				this._producerChunk = new T[this._chunkSize];
			}
			this._producerChunkIndex = (producerChunkIndex + 1) % this._chunkSize;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		private void EnqueueChunk(T[] chunk)
		{
			if (this.IsFull)
			{
				this.WaitUntilNonFull();
			}
			int producerBufferIndex = this._producerBufferIndex;
			this._buffer[producerBufferIndex] = chunk;
			Interlocked.Exchange(ref this._producerBufferIndex, (producerBufferIndex + 1) % this._buffer.Length);
			if (this._consumerIsWaiting == 1 && !this.IsChunkBufferEmpty)
			{
				this._consumerIsWaiting = 0;
				this._consumerEvent.Set(this._index);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001CD38 File Offset: 0x0001AF38
		private void WaitUntilNonFull()
		{
			do
			{
				this._producerEvent.Reset();
				Interlocked.Exchange(ref this._producerIsWaiting, 1);
				if (this.IsFull)
				{
					this._producerEvent.Wait(this._cancellationToken);
				}
				else
				{
					this._producerIsWaiting = 0;
				}
			}
			while (this.IsFull);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		private void FlushCachedChunk()
		{
			if (this._producerChunk != null && this._producerChunkIndex != 0)
			{
				T[] array = new T[this._producerChunkIndex];
				Array.Copy(this._producerChunk, 0, array, 0, this._producerChunkIndex);
				this.EnqueueChunk(array);
				this._producerChunk = null;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		internal bool TryDequeue(ref T item)
		{
			if (this._consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this._consumerChunk))
				{
					return false;
				}
				this._consumerChunkIndex = 0;
			}
			item = this._consumerChunk[this._consumerChunkIndex];
			this._consumerChunkIndex++;
			if (this._consumerChunkIndex == this._consumerChunk.Length)
			{
				this._consumerChunk = null;
			}
			return true;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001CE41 File Offset: 0x0001B041
		private bool TryDequeueChunk(ref T[] chunk)
		{
			if (this.IsChunkBufferEmpty)
			{
				return false;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001CE58 File Offset: 0x0001B058
		internal bool TryDequeue(ref T item, ref bool isDone)
		{
			isDone = false;
			if (this._consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this._consumerChunk, ref isDone))
				{
					return false;
				}
				this._consumerChunkIndex = 0;
			}
			item = this._consumerChunk[this._consumerChunkIndex];
			this._consumerChunkIndex++;
			if (this._consumerChunkIndex == this._consumerChunk.Length)
			{
				this._consumerChunk = null;
			}
			return true;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001CEC8 File Offset: 0x0001B0C8
		private bool TryDequeueChunk(ref T[] chunk, ref bool isDone)
		{
			isDone = false;
			while (this.IsChunkBufferEmpty)
			{
				if (this.IsDone && this.IsChunkBufferEmpty)
				{
					isDone = true;
					return false;
				}
				Interlocked.Exchange(ref this._consumerIsWaiting, 1);
				if (this.IsChunkBufferEmpty && !this.IsDone)
				{
					return false;
				}
				this._consumerIsWaiting = 0;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001CF28 File Offset: 0x0001B128
		private T[] InternalDequeueChunk()
		{
			int consumerBufferIndex = this._consumerBufferIndex;
			T[] result = this._buffer[consumerBufferIndex];
			this._buffer[consumerBufferIndex] = null;
			Interlocked.Exchange(ref this._consumerBufferIndex, (consumerBufferIndex + 1) % this._buffer.Length);
			if (this._producerIsWaiting == 1 && !this.IsFull)
			{
				this._producerIsWaiting = 0;
				this._producerEvent.Set();
			}
			return result;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001CF8E File Offset: 0x0001B18E
		internal void DoneWithDequeueWait()
		{
			this._consumerIsWaiting = 0;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public void Dispose()
		{
			lock (this)
			{
				this._producerEvent.Dispose();
				this._producerEvent = null;
				this._consumerEvent = null;
			}
		}

		// Token: 0x040005C8 RID: 1480
		private T[][] _buffer;

		// Token: 0x040005C9 RID: 1481
		private readonly int _index;

		// Token: 0x040005CA RID: 1482
		private volatile int _producerBufferIndex;

		// Token: 0x040005CB RID: 1483
		private volatile int _consumerBufferIndex;

		// Token: 0x040005CC RID: 1484
		private volatile bool _done;

		// Token: 0x040005CD RID: 1485
		private T[] _producerChunk;

		// Token: 0x040005CE RID: 1486
		private int _producerChunkIndex;

		// Token: 0x040005CF RID: 1487
		private T[] _consumerChunk;

		// Token: 0x040005D0 RID: 1488
		private int _consumerChunkIndex;

		// Token: 0x040005D1 RID: 1489
		private int _chunkSize;

		// Token: 0x040005D2 RID: 1490
		private ManualResetEventSlim _producerEvent;

		// Token: 0x040005D3 RID: 1491
		private IntValueEvent _consumerEvent;

		// Token: 0x040005D4 RID: 1492
		private volatile int _producerIsWaiting;

		// Token: 0x040005D5 RID: 1493
		private volatile int _consumerIsWaiting;

		// Token: 0x040005D6 RID: 1494
		private CancellationToken _cancellationToken;
	}
}
