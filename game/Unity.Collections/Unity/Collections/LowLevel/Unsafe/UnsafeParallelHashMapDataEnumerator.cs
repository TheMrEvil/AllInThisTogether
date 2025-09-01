using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000135 RID: 309
	internal struct UnsafeParallelHashMapDataEnumerator
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x00021A4F File Offset: 0x0001FC4F
		internal unsafe UnsafeParallelHashMapDataEnumerator(UnsafeParallelHashMapData* data)
		{
			this.m_Buffer = data;
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00021A6D File Offset: 0x0001FC6D
		internal bool MoveNext()
		{
			return UnsafeParallelHashMapData.MoveNext(this.m_Buffer, ref this.m_BucketIndex, ref this.m_NextIndex, out this.m_Index);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00021A8C File Offset: 0x0001FC8C
		internal void Reset()
		{
			this.m_Index = -1;
			this.m_BucketIndex = 0;
			this.m_NextIndex = -1;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		internal KeyValue<TKey, TValue> GetCurrent<TKey, TValue>() where TKey : struct, IEquatable<TKey> where TValue : struct
		{
			return new KeyValue<TKey, TValue>
			{
				m_Buffer = this.m_Buffer,
				m_Index = this.m_Index
			};
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00021AD4 File Offset: 0x0001FCD4
		internal unsafe TKey GetCurrentKey<TKey>() where TKey : struct, IEquatable<TKey>
		{
			if (this.m_Index != -1)
			{
				return UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
			}
			return default(TKey);
		}

		// Token: 0x040003AD RID: 941
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003AE RID: 942
		internal int m_Index;

		// Token: 0x040003AF RID: 943
		internal int m_BucketIndex;

		// Token: 0x040003B0 RID: 944
		internal int m_NextIndex;
	}
}
