using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000057 RID: 87
	public struct ListBuffer<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000E87E File Offset: 0x0000CA7E
		internal unsafe T* BufferPtr
		{
			get
			{
				return this.m_BufferPtr;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000E886 File Offset: 0x0000CA86
		public unsafe int Count
		{
			get
			{
				return *this.m_CountPtr;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000E88F File Offset: 0x0000CA8F
		public int Capacity
		{
			get
			{
				return this.m_Capacity;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E897 File Offset: 0x0000CA97
		public unsafe ListBuffer(T* bufferPtr, int* countPtr, int capacity)
		{
			this.m_BufferPtr = bufferPtr;
			this.m_Capacity = capacity;
			this.m_CountPtr = countPtr;
		}

		// Token: 0x1700004C RID: 76
		public unsafe T this[in int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException(string.Format("Expected a value between 0 and {0}, but received {1}.", this.Count, index));
				}
				return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E8FF File Offset: 0x0000CAFF
		public unsafe ref T GetUnchecked(in int index)
		{
			return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E912 File Offset: 0x0000CB12
		public unsafe bool TryAdd(in T value)
		{
			if (this.Count >= this.m_Capacity)
			{
				return false;
			}
			this.m_BufferPtr[(IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			(*this.m_CountPtr)++;
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E950 File Offset: 0x0000CB50
		public unsafe void CopyTo(T* dstBuffer, int startDstIndex, int copyCount)
		{
			UnsafeUtility.MemCpy((void*)(dstBuffer + (IntPtr)startDstIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * copyCount));
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E970 File Offset: 0x0000CB70
		public unsafe bool TryCopyTo(ListBuffer<T> other)
		{
			if (other.Count + this.Count >= other.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(other.m_BufferPtr + (IntPtr)other.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * this.Count));
			*other.m_CountPtr += this.Count;
			return true;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public unsafe bool TryCopyFrom(T* srcPtr, int count)
		{
			if (count + this.Count > this.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(this.m_BufferPtr + (IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)srcPtr, (long)(UnsafeUtility.SizeOf<T>() * count));
			*this.m_CountPtr += count;
			return true;
		}

		// Token: 0x040001F7 RID: 503
		private unsafe T* m_BufferPtr;

		// Token: 0x040001F8 RID: 504
		private int m_Capacity;

		// Token: 0x040001F9 RID: 505
		private unsafe int* m_CountPtr;
	}
}
