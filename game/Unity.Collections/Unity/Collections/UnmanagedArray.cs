using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000E5 RID: 229
	internal struct UnmanagedArray<[IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x060008BC RID: 2236 RVA: 0x00019E42 File Offset: 0x00018042
		public unsafe UnmanagedArray(int length, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_pointer = (IntPtr)((void*)Memory.Unmanaged.Array.Allocate<T>((long)length, allocator));
			this.m_length = length;
			this.m_allocator = allocator;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00019E65 File Offset: 0x00018065
		public unsafe void Dispose()
		{
			Memory.Unmanaged.Free<T>((T*)((void*)this.m_pointer), Allocator.Persistent);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00019E7D File Offset: 0x0001807D
		public unsafe T* GetUnsafePointer()
		{
			return (T*)((void*)this.m_pointer);
		}

		// Token: 0x170000F2 RID: 242
		public unsafe T this[int index]
		{
			get
			{
				return ref *(T*)((byte*)((void*)this.m_pointer) + (IntPtr)index * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x040002D9 RID: 729
		private IntPtr m_pointer;

		// Token: 0x040002DA RID: 730
		private int m_length;

		// Token: 0x040002DB RID: 731
		private AllocatorManager.AllocatorHandle m_allocator;
	}
}
