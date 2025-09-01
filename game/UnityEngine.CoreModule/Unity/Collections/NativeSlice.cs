using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x0200009A RID: 154
	[NativeContainerSupportsMinMaxWriteRestriction]
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainer]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x000051DA File Offset: 0x000033DA
		public NativeSlice(NativeSlice<T> slice, int start)
		{
			this = new NativeSlice<T>(slice, start, slice.Length - start);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000051EF File Offset: 0x000033EF
		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000521A File Offset: 0x0000341A
		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000522D File Offset: 0x0000342D
		public NativeSlice(NativeArray<T> array, int start)
		{
			this = new NativeSlice<T>(array, start, array.Length - start);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00005244 File Offset: 0x00003444
		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000525C File Offset: 0x0000345C
		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* buffer = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = buffer;
			this.m_Length = length;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00005294 File Offset: 0x00003494
		public NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> result;
			result.m_Buffer = this.m_Buffer;
			result.m_Stride = num;
			result.m_Length = this.m_Length * this.m_Stride / num;
			return result;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000052D8 File Offset: 0x000034D8
		public NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> result;
			result.m_Buffer = this.m_Buffer + offset;
			result.m_Stride = this.m_Stride;
			result.m_Length = this.m_Length;
			return result;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00005314 File Offset: 0x00003514
		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return this.SliceWithStride<U>(0);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadIndex(int index)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteIndex(int index)
		{
		}

		// Token: 0x17000079 RID: 121
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000536B File Offset: 0x0000356B
		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000053A0 File Offset: 0x000035A0
		[WriteAccessRequired]
		public unsafe void CopyFrom(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr value = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, (void*)value, num, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000053F4 File Offset: 0x000035F4
		public void CopyTo(NativeArray<T> array)
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr<T>(), num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00005430 File Offset: 0x00003630
		public unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr value = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)value, num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005484 File Offset: 0x00003684
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000054AB File Offset: 0x000036AB
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000054B4 File Offset: 0x000036B4
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000054CC File Offset: 0x000036CC
		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000054E4 File Offset: 0x000036E4
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00005504 File Offset: 0x00003704
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00005524 File Offset: 0x00003724
		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00005564 File Offset: 0x00003764
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000559C File Offset: 0x0000379C
		public override int GetHashCode()
		{
			int num = this.m_Buffer;
			num = (num * 397 ^ this.m_Stride);
			return num * 397 ^ this.m_Length;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000055D8 File Offset: 0x000037D8
		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000055F4 File Offset: 0x000037F4
		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000235 RID: 565
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000236 RID: 566
		internal int m_Stride;

		// Token: 0x04000237 RID: 567
		internal int m_Length;

		// Token: 0x0200009B RID: 155
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x00005611 File Offset: 0x00003811
			public Enumerator(ref NativeSlice<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x00004563 File Offset: 0x00002763
			public void Dispose()
			{
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x00005628 File Offset: 0x00003828
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000565B File Offset: 0x0000385B
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x00005665 File Offset: 0x00003865
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x00005678 File Offset: 0x00003878
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000238 RID: 568
			private NativeSlice<T> m_Array;

			// Token: 0x04000239 RID: 569
			private int m_Index;
		}
	}
}
