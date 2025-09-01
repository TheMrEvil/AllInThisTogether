using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000AD4 RID: 2772
	internal sealed class ConfigurableArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x060062DB RID: 25307 RVA: 0x0014A800 File Offset: 0x00148A00
		internal ConfigurableArrayPool() : this(1048576, 50)
		{
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x0014A810 File Offset: 0x00148A10
		internal ConfigurableArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			ConfigurableArrayPool<T>.Bucket[] array = new ConfigurableArrayPool<T>.Bucket[Utilities.SelectBucketIndex(maxArrayLength) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ConfigurableArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x0014A895 File Offset: 0x00148A95
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x0014A8A0 File Offset: 0x00148AA0
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				return Array.Empty<T>();
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_86;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_86:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				int bucketId = -1;
				log.BufferRented(hashCode, array.Length, this.Id, bucketId);
				log.BufferAllocated(hashCode, array.Length, this.Id, bucketId, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x0014A998 File Offset: 0x00148B98
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04003A3D RID: 14909
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x04003A3E RID: 14910
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04003A3F RID: 14911
		private readonly ConfigurableArrayPool<T>.Bucket[] _buckets;

		// Token: 0x02000AD5 RID: 2773
		private sealed class Bucket
		{
			// Token: 0x060062E0 RID: 25312 RVA: 0x0014AA0A File Offset: 0x00148C0A
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x1700117F RID: 4479
			// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0014A895 File Offset: 0x00148A95
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x060062E2 RID: 25314 RVA: 0x0014AA3C File Offset: 0x00148C3C
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = (array == null);
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x060062E3 RID: 25315 RVA: 0x0014AAF8 File Offset: 0x00148CF8
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException("The buffer is not associated with this pool and may not be returned to it.", "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04003A40 RID: 14912
			internal readonly int _bufferLength;

			// Token: 0x04003A41 RID: 14913
			private readonly T[][] _buffers;

			// Token: 0x04003A42 RID: 14914
			private readonly int _poolId;

			// Token: 0x04003A43 RID: 14915
			private SpinLock _lock;

			// Token: 0x04003A44 RID: 14916
			private int _index;
		}
	}
}
