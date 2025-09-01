using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000033 RID: 51
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(AllocatorManager.AllocatorHandle)
	})]
	public struct AllocatorHelper<[IsUnmanaged] T> : IDisposable where T : struct, ValueType, AllocatorManager.IAllocator
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000385A File Offset: 0x00001A5A
		public unsafe ref T Allocator
		{
			get
			{
				return UnsafeUtility.AsRef<T>((void*)this.m_allocator);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003868 File Offset: 0x00001A68
		[NotBurstCompatible]
		public unsafe AllocatorHelper(AllocatorManager.AllocatorHandle backingAllocator)
		{
			ref T output = ref AllocatorManager.CreateAllocator<T>(backingAllocator);
			this.m_allocator = (T*)UnsafeUtility.AddressOf<T>(ref output);
			this.m_backingAllocator = backingAllocator;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000388F File Offset: 0x00001A8F
		[NotBurstCompatible]
		public unsafe void Dispose()
		{
			UnsafeUtility.AsRef<T>((void*)this.m_allocator).DestroyAllocator(this.m_backingAllocator);
		}

		// Token: 0x0400006C RID: 108
		private unsafe readonly T* m_allocator;

		// Token: 0x0400006D RID: 109
		private AllocatorManager.AllocatorHandle m_backingAllocator;
	}
}
