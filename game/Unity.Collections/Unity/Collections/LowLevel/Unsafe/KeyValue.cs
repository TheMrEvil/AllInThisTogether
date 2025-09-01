using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000134 RID: 308
	[DebuggerDisplay("Key = {Key}, Value = {Value}")]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct KeyValue<TKey, TValue> where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00021978 File Offset: 0x0001FB78
		public static KeyValue<TKey, TValue> Null
		{
			get
			{
				return new KeyValue<TKey, TValue>
				{
					m_Index = -1
				};
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00021998 File Offset: 0x0001FB98
		public unsafe TKey Key
		{
			get
			{
				if (this.m_Index != -1)
				{
					return UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
				}
				return default(TKey);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000219CE File Offset: 0x0001FBCE
		public unsafe ref TValue Value
		{
			get
			{
				return UnsafeUtility.AsRef<TValue>((void*)(this.m_Buffer->values + UnsafeUtility.SizeOf<TValue>() * this.m_Index));
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public unsafe bool GetKeyValue(out TKey key, out TValue value)
		{
			if (this.m_Index != -1)
			{
				key = UnsafeUtility.ReadArrayElement<TKey>((void*)this.m_Buffer->keys, this.m_Index);
				value = UnsafeUtility.ReadArrayElement<TValue>((void*)this.m_Buffer->values, this.m_Index);
				return true;
			}
			key = default(TKey);
			value = default(TValue);
			return false;
		}

		// Token: 0x040003AA RID: 938
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003AB RID: 939
		internal int m_Index;

		// Token: 0x040003AC RID: 940
		internal int m_Next;
	}
}
