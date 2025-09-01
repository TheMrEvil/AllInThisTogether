using System;
using System.Diagnostics;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000155 RID: 341
	[BurstCompatible]
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	public struct UnsafeText : INativeDisposable, IDisposable, IUTF8Bytes, INativeList<byte>, IIndexable<byte>
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x00024080 File Offset: 0x00022280
		public unsafe UnsafeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_UntypedListData = default(UntypedUnsafeList);
			*ref this.AsUnsafeListOfBytes() = new UnsafeList<byte>(capacity + 1, allocator, NativeArrayOptions.UninitializedMemory);
			this.Length = 0;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000240AA File Offset: 0x000222AA
		public bool IsCreated
		{
			get
			{
				return ref this.AsUnsafeListOfBytes().IsCreated;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000240B7 File Offset: 0x000222B7
		public void Dispose()
		{
			ref this.AsUnsafeListOfBytes().Dispose();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000240C4 File Offset: 0x000222C4
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return ref this.AsUnsafeListOfBytes().Dispose(inputDeps);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x000240D2 File Offset: 0x000222D2
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x1700015E RID: 350
		public byte this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<byte>(this.m_UntypedListData.Ptr, index);
			}
			set
			{
				UnsafeUtility.WriteArrayElement<byte>(this.m_UntypedListData.Ptr, index, value);
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002410E File Offset: 0x0002230E
		public ref byte ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<byte>(this.m_UntypedListData.Ptr, index);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00024121 File Offset: 0x00022321
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002412A File Offset: 0x0002232A
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)this.m_UntypedListData.Ptr;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00024137 File Offset: 0x00022337
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			ref this.AsUnsafeListOfBytes().Resize(newLength + 1, clearOptions);
			ref this.AsUnsafeListOfBytes()[newLength] = 0;
			return true;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00024156 File Offset: 0x00022356
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00024165 File Offset: 0x00022365
		public int Capacity
		{
			get
			{
				return ref this.AsUnsafeListOfBytes().Capacity - 1;
			}
			set
			{
				ref this.AsUnsafeListOfBytes().SetCapacity(value + 1);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00024175 File Offset: 0x00022375
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00024184 File Offset: 0x00022384
		public int Length
		{
			get
			{
				return ref this.AsUnsafeListOfBytes().Length - 1;
			}
			set
			{
				ref this.AsUnsafeListOfBytes().Resize(value + 1, NativeArrayOptions.UninitializedMemory);
				ref this.AsUnsafeListOfBytes()[value] = 0;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000241A2 File Offset: 0x000223A2
		[NotBurstCompatible]
		public override string ToString()
		{
			if (!this.IsCreated)
			{
				return "";
			}
			return ref this.ConvertToString<UnsafeText>();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000241B8 File Offset: 0x000223B8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in UnsafeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00024209 File Offset: 0x00022409
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("UnsafeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00024221 File Offset: 0x00022421
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCapacityInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value < length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x040003FB RID: 1019
		internal UntypedUnsafeList m_UntypedListData;
	}
}
