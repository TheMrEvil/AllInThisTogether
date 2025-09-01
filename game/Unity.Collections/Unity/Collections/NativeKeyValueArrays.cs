using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B7 RID: 183
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeKeyValueArrays<TKey, TValue> : INativeDisposable, IDisposable where TKey : struct where TValue : struct
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000160B0 File Offset: 0x000142B0
		public int Length
		{
			get
			{
				return this.Keys.Length;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000160BD File Offset: 0x000142BD
		public NativeKeyValueArrays(int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options)
		{
			this.Keys = CollectionHelper.CreateNativeArray<TKey>(length, allocator, options);
			this.Values = CollectionHelper.CreateNativeArray<TValue>(length, allocator, options);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000160DB File Offset: 0x000142DB
		public void Dispose()
		{
			this.Keys.Dispose();
			this.Values.Dispose();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000160F3 File Offset: 0x000142F3
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.Keys.Dispose(this.Values.Dispose(inputDeps));
		}

		// Token: 0x04000285 RID: 645
		public NativeArray<TKey> Keys;

		// Token: 0x04000286 RID: 646
		public NativeArray<TValue> Values;
	}
}
