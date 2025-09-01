using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000023 RID: 35
	internal abstract class InternalBufferManager
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00005338 File Offset: 0x00003538
		protected InternalBufferManager()
		{
		}

		// Token: 0x06000107 RID: 263
		public abstract byte[] TakeBuffer(int bufferSize);

		// Token: 0x06000108 RID: 264
		public abstract void ReturnBuffer(byte[] buffer);

		// Token: 0x06000109 RID: 265
		public abstract void Clear();

		// Token: 0x0600010A RID: 266 RVA: 0x00005340 File Offset: 0x00003540
		public static InternalBufferManager Create(long maxBufferPoolSize, int maxBufferSize)
		{
			if (maxBufferPoolSize == 0L)
			{
				return InternalBufferManager.GCBufferManager.Value;
			}
			return new InternalBufferManager.PooledBufferManager(maxBufferPoolSize, maxBufferSize);
		}

		// Token: 0x0200007D RID: 125
		private class PooledBufferManager : InternalBufferManager
		{
			// Token: 0x060003CE RID: 974 RVA: 0x00012178 File Offset: 0x00010378
			public PooledBufferManager(long maxMemoryToPool, int maxBufferSize)
			{
				this.tuningLock = new object();
				this.memoryLimit = maxMemoryToPool;
				this.remainingMemory = maxMemoryToPool;
				List<InternalBufferManager.PooledBufferManager.BufferPool> list = new List<InternalBufferManager.PooledBufferManager.BufferPool>();
				int num = 128;
				for (;;)
				{
					long num2 = this.remainingMemory / (long)num;
					int num3 = (num2 > 2147483647L) ? int.MaxValue : ((int)num2);
					if (num3 > 1)
					{
						num3 = 1;
					}
					list.Add(InternalBufferManager.PooledBufferManager.BufferPool.CreatePool(num, num3));
					this.remainingMemory -= (long)num3 * (long)num;
					if (num >= maxBufferSize)
					{
						break;
					}
					long num4 = (long)num * 2L;
					if (num4 > (long)maxBufferSize)
					{
						num = maxBufferSize;
					}
					else
					{
						num = (int)num4;
					}
				}
				this.bufferPools = list.ToArray();
				this.bufferSizes = new int[this.bufferPools.Length];
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					this.bufferSizes[i] = this.bufferPools[i].BufferSize;
				}
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0001225C File Offset: 0x0001045C
			public override void Clear()
			{
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					this.bufferPools[i].Clear();
				}
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0001228C File Offset: 0x0001048C
			private void ChangeQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool, int delta)
			{
				if (TraceCore.BufferPoolChangeQuotaIsEnabled(Fx.Trace))
				{
					TraceCore.BufferPoolChangeQuota(Fx.Trace, bufferPool.BufferSize, delta);
				}
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool2 = bufferPool;
				int num = bufferPool2.Limit + delta;
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool3 = InternalBufferManager.PooledBufferManager.BufferPool.CreatePool(bufferPool2.BufferSize, num);
				for (int i = 0; i < num; i++)
				{
					byte[] array = bufferPool2.Take();
					if (array == null)
					{
						break;
					}
					bufferPool3.Return(array);
					bufferPool3.IncrementCount();
				}
				this.remainingMemory -= (long)(bufferPool2.BufferSize * delta);
				bufferPool = bufferPool3;
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x00012310 File Offset: 0x00010510
			private void DecreaseQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool)
			{
				this.ChangeQuota(ref bufferPool, -1);
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x0001231C File Offset: 0x0001051C
			private int FindMostExcessivePool()
			{
				long num = 0L;
				int result = -1;
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[i];
					if (bufferPool.Peak < bufferPool.Limit)
					{
						long num2 = (long)(bufferPool.Limit - bufferPool.Peak) * (long)bufferPool.BufferSize;
						if (num2 > num)
						{
							result = i;
							num = num2;
						}
					}
				}
				return result;
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x0001237C File Offset: 0x0001057C
			private int FindMostStarvedPool()
			{
				long num = 0L;
				int result = -1;
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[i];
					if (bufferPool.Peak == bufferPool.Limit)
					{
						long num2 = (long)bufferPool.Misses * (long)bufferPool.BufferSize;
						if (num2 > num)
						{
							result = i;
							num = num2;
						}
					}
				}
				return result;
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x000123D4 File Offset: 0x000105D4
			private InternalBufferManager.PooledBufferManager.BufferPool FindPool(int desiredBufferSize)
			{
				for (int i = 0; i < this.bufferSizes.Length; i++)
				{
					if (desiredBufferSize <= this.bufferSizes[i])
					{
						return this.bufferPools[i];
					}
				}
				return null;
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x00012409 File Offset: 0x00010609
			private void IncreaseQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool)
			{
				this.ChangeQuota(ref bufferPool, 1);
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x00012414 File Offset: 0x00010614
			public override void ReturnBuffer(byte[] buffer)
			{
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.FindPool(buffer.Length);
				if (bufferPool != null)
				{
					if (buffer.Length != bufferPool.BufferSize)
					{
						throw Fx.Exception.Argument("buffer", "Buffer Is Not Right Size For Buffer Manager");
					}
					if (bufferPool.Return(buffer))
					{
						bufferPool.IncrementCount();
					}
				}
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x00012460 File Offset: 0x00010660
			public override byte[] TakeBuffer(int bufferSize)
			{
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.FindPool(bufferSize);
				byte[] result;
				if (bufferPool != null)
				{
					byte[] array = bufferPool.Take();
					if (array != null)
					{
						bufferPool.DecrementCount();
						result = array;
					}
					else
					{
						if (bufferPool.Peak == bufferPool.Limit)
						{
							InternalBufferManager.PooledBufferManager.BufferPool bufferPool2 = bufferPool;
							int num = bufferPool2.Misses;
							bufferPool2.Misses = num + 1;
							num = this.totalMisses + 1;
							this.totalMisses = num;
							if (num >= 8)
							{
								this.TuneQuotas();
							}
						}
						if (TraceCore.BufferPoolAllocationIsEnabled(Fx.Trace))
						{
							TraceCore.BufferPoolAllocation(Fx.Trace, bufferPool.BufferSize);
						}
						result = Fx.AllocateByteArray(bufferPool.BufferSize);
					}
				}
				else
				{
					if (TraceCore.BufferPoolAllocationIsEnabled(Fx.Trace))
					{
						TraceCore.BufferPoolAllocation(Fx.Trace, bufferSize);
					}
					result = Fx.AllocateByteArray(bufferSize);
				}
				return result;
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x00012510 File Offset: 0x00010710
			private void TuneQuotas()
			{
				if (this.areQuotasBeingTuned)
				{
					return;
				}
				bool flag = false;
				try
				{
					Monitor.TryEnter(this.tuningLock, ref flag);
					if (!flag || this.areQuotasBeingTuned)
					{
						return;
					}
					this.areQuotasBeingTuned = true;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.tuningLock);
					}
				}
				int num = this.FindMostStarvedPool();
				if (num >= 0)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[num];
					if (this.remainingMemory < (long)bufferPool.BufferSize)
					{
						int num2 = this.FindMostExcessivePool();
						if (num2 >= 0)
						{
							this.DecreaseQuota(ref this.bufferPools[num2]);
						}
					}
					if (this.remainingMemory >= (long)bufferPool.BufferSize)
					{
						this.IncreaseQuota(ref this.bufferPools[num]);
					}
				}
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					this.bufferPools[i].Misses = 0;
				}
				this.totalMisses = 0;
				this.areQuotasBeingTuned = false;
			}

			// Token: 0x040002AA RID: 682
			private const int minBufferSize = 128;

			// Token: 0x040002AB RID: 683
			private const int maxMissesBeforeTuning = 8;

			// Token: 0x040002AC RID: 684
			private const int initialBufferCount = 1;

			// Token: 0x040002AD RID: 685
			private readonly object tuningLock;

			// Token: 0x040002AE RID: 686
			private int[] bufferSizes;

			// Token: 0x040002AF RID: 687
			private InternalBufferManager.PooledBufferManager.BufferPool[] bufferPools;

			// Token: 0x040002B0 RID: 688
			private long memoryLimit;

			// Token: 0x040002B1 RID: 689
			private long remainingMemory;

			// Token: 0x040002B2 RID: 690
			private bool areQuotasBeingTuned;

			// Token: 0x040002B3 RID: 691
			private int totalMisses;

			// Token: 0x020000B4 RID: 180
			private abstract class BufferPool
			{
				// Token: 0x06000493 RID: 1171 RVA: 0x00013B14 File Offset: 0x00011D14
				public BufferPool(int bufferSize, int limit)
				{
					this.bufferSize = bufferSize;
					this.limit = limit;
				}

				// Token: 0x170000D1 RID: 209
				// (get) Token: 0x06000494 RID: 1172 RVA: 0x00013B2A File Offset: 0x00011D2A
				public int BufferSize
				{
					get
					{
						return this.bufferSize;
					}
				}

				// Token: 0x170000D2 RID: 210
				// (get) Token: 0x06000495 RID: 1173 RVA: 0x00013B32 File Offset: 0x00011D32
				public int Limit
				{
					get
					{
						return this.limit;
					}
				}

				// Token: 0x170000D3 RID: 211
				// (get) Token: 0x06000496 RID: 1174 RVA: 0x00013B3A File Offset: 0x00011D3A
				// (set) Token: 0x06000497 RID: 1175 RVA: 0x00013B42 File Offset: 0x00011D42
				public int Misses
				{
					get
					{
						return this.misses;
					}
					set
					{
						this.misses = value;
					}
				}

				// Token: 0x170000D4 RID: 212
				// (get) Token: 0x06000498 RID: 1176 RVA: 0x00013B4B File Offset: 0x00011D4B
				public int Peak
				{
					get
					{
						return this.peak;
					}
				}

				// Token: 0x06000499 RID: 1177 RVA: 0x00013B53 File Offset: 0x00011D53
				public void Clear()
				{
					this.OnClear();
					this.count = 0;
				}

				// Token: 0x0600049A RID: 1178 RVA: 0x00013B64 File Offset: 0x00011D64
				public void DecrementCount()
				{
					int num = this.count - 1;
					if (num >= 0)
					{
						this.count = num;
					}
				}

				// Token: 0x0600049B RID: 1179 RVA: 0x00013B88 File Offset: 0x00011D88
				public void IncrementCount()
				{
					int num = this.count + 1;
					if (num <= this.limit)
					{
						this.count = num;
						if (num > this.peak)
						{
							this.peak = num;
						}
					}
				}

				// Token: 0x0600049C RID: 1180
				internal abstract byte[] Take();

				// Token: 0x0600049D RID: 1181
				internal abstract bool Return(byte[] buffer);

				// Token: 0x0600049E RID: 1182
				internal abstract void OnClear();

				// Token: 0x0600049F RID: 1183 RVA: 0x00013BBE File Offset: 0x00011DBE
				internal static InternalBufferManager.PooledBufferManager.BufferPool CreatePool(int bufferSize, int limit)
				{
					if (bufferSize < 85000)
					{
						return new InternalBufferManager.PooledBufferManager.BufferPool.SynchronizedBufferPool(bufferSize, limit);
					}
					return new InternalBufferManager.PooledBufferManager.BufferPool.LargeBufferPool(bufferSize, limit);
				}

				// Token: 0x0400035F RID: 863
				private int bufferSize;

				// Token: 0x04000360 RID: 864
				private int count;

				// Token: 0x04000361 RID: 865
				private int limit;

				// Token: 0x04000362 RID: 866
				private int misses;

				// Token: 0x04000363 RID: 867
				private int peak;

				// Token: 0x020000B8 RID: 184
				private class SynchronizedBufferPool : InternalBufferManager.PooledBufferManager.BufferPool
				{
					// Token: 0x060004B0 RID: 1200 RVA: 0x00013ECC File Offset: 0x000120CC
					internal SynchronizedBufferPool(int bufferSize, int limit) : base(bufferSize, limit)
					{
						this.innerPool = new SynchronizedPool<byte[]>(limit);
					}

					// Token: 0x060004B1 RID: 1201 RVA: 0x00013EE2 File Offset: 0x000120E2
					internal override void OnClear()
					{
						this.innerPool.Clear();
					}

					// Token: 0x060004B2 RID: 1202 RVA: 0x00013EEF File Offset: 0x000120EF
					internal override byte[] Take()
					{
						return this.innerPool.Take();
					}

					// Token: 0x060004B3 RID: 1203 RVA: 0x00013EFC File Offset: 0x000120FC
					internal override bool Return(byte[] buffer)
					{
						return this.innerPool.Return(buffer);
					}

					// Token: 0x0400036C RID: 876
					private SynchronizedPool<byte[]> innerPool;
				}

				// Token: 0x020000B9 RID: 185
				private class LargeBufferPool : InternalBufferManager.PooledBufferManager.BufferPool
				{
					// Token: 0x060004B4 RID: 1204 RVA: 0x00013F0A File Offset: 0x0001210A
					internal LargeBufferPool(int bufferSize, int limit) : base(bufferSize, limit)
					{
						this.items = new Stack<byte[]>(limit);
					}

					// Token: 0x170000D9 RID: 217
					// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00013F20 File Offset: 0x00012120
					private object ThisLock
					{
						get
						{
							return this.items;
						}
					}

					// Token: 0x060004B6 RID: 1206 RVA: 0x00013F28 File Offset: 0x00012128
					internal override void OnClear()
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							this.items.Clear();
						}
					}

					// Token: 0x060004B7 RID: 1207 RVA: 0x00013F70 File Offset: 0x00012170
					internal override byte[] Take()
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.items.Count > 0)
							{
								return this.items.Pop();
							}
						}
						return null;
					}

					// Token: 0x060004B8 RID: 1208 RVA: 0x00013FCC File Offset: 0x000121CC
					internal override bool Return(byte[] buffer)
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.items.Count < base.Limit)
							{
								this.items.Push(buffer);
								return true;
							}
						}
						return false;
					}

					// Token: 0x0400036D RID: 877
					private Stack<byte[]> items;
				}
			}
		}

		// Token: 0x0200007E RID: 126
		private class GCBufferManager : InternalBufferManager
		{
			// Token: 0x060003D9 RID: 985 RVA: 0x00012604 File Offset: 0x00010804
			private GCBufferManager()
			{
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0001260C File Offset: 0x0001080C
			public static InternalBufferManager.GCBufferManager Value
			{
				get
				{
					return InternalBufferManager.GCBufferManager.value;
				}
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00012613 File Offset: 0x00010813
			public override void Clear()
			{
			}

			// Token: 0x060003DC RID: 988 RVA: 0x00012615 File Offset: 0x00010815
			public override byte[] TakeBuffer(int bufferSize)
			{
				return Fx.AllocateByteArray(bufferSize);
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0001261D File Offset: 0x0001081D
			public override void ReturnBuffer(byte[] buffer)
			{
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0001261F File Offset: 0x0001081F
			// Note: this type is marked as 'beforefieldinit'.
			static GCBufferManager()
			{
			}

			// Token: 0x040002B4 RID: 692
			private static InternalBufferManager.GCBufferManager value = new InternalBufferManager.GCBufferManager();
		}
	}
}
