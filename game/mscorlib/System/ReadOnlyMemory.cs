using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000175 RID: 373
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	public readonly struct ReadOnlyMemory<T> : IEquatable<ReadOnlyMemory<T>>
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003C172 File Offset: 0x0003A372
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlyMemory<T>);
				return;
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003C196 File Offset: 0x0003A396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlyMemory<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003C1D6 File Offset: 0x0003A3D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlyMemory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003C1ED File Offset: 0x0003A3ED
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003C1F5 File Offset: 0x0003A3F5
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003C214 File Offset: 0x0003A414
		public static ReadOnlyMemory<T> Empty
		{
			get
			{
				return default(ReadOnlyMemory<T>);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003C22A File Offset: 0x0003A42A
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0003C238 File Offset: 0x0003A438
		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003C24C File Offset: 0x0003A44C
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.ReadOnlyMemory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003C2DC File Offset: 0x0003A4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003C31C File Offset: 0x0003A51C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = this._length & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0003C36C File Offset: 0x0003A56C
		public ReadOnlySpan<T> Span
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._index < 0)
				{
					return ((MemoryManager<T>)this._object).GetSpan().Slice(this._index & int.MaxValue, this._length);
				}
				if (typeof(T) == typeof(char))
				{
					string text = this._object as string;
					if (text != null)
					{
						return new ReadOnlySpan<T>(Unsafe.As<char, T>(text.GetRawStringData()), text.Length).Slice(this._index, this._length);
					}
				}
				if (this._object != null)
				{
					return new ReadOnlySpan<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003C43C File Offset: 0x0003A63C
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003C460 File Offset: 0x0003A660
		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003C484 File Offset: 0x0003A684
		public MemoryHandle Pin()
		{
			if (this._index < 0)
			{
				return ((MemoryManager<T>)this._object).Pin(this._index & int.MaxValue);
			}
			if (typeof(T) == typeof(char))
			{
				string text = this._object as string;
				if (text != null)
				{
					GCHandle handle = GCHandle.Alloc(text, GCHandleType.Pinned);
					return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<char>(text.GetRawStringData()), this._index), handle, null);
				}
			}
			T[] array = this._object as T[];
			if (array == null)
			{
				return default(MemoryHandle);
			}
			if (this._length < 0)
			{
				return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<byte>(array.GetRawSzArrayData()), this._index), default(GCHandle), null);
			}
			GCHandle handle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			return new MemoryHandle(Unsafe.Add<T>(Unsafe.AsPointer<byte>(array.GetRawSzArrayData()), this._index), handle2, null);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003C578 File Offset: 0x0003A778
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003C594 File Offset: 0x0003A794
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				ReadOnlyMemory<T> other = (ReadOnlyMemory<T>)obj;
				return this.Equals(other);
			}
			if (obj is Memory<T>)
			{
				Memory<T> memory = (Memory<T>)obj;
				return this.Equals(memory);
			}
			return false;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003C5D5 File Offset: 0x0003A7D5
		public bool Equals(ReadOnlyMemory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003C603 File Offset: 0x0003A803
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return ReadOnlyMemory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00033E4B File Offset: 0x0003204B
		private static int CombineHashCodes(int left, int right)
		{
			return (left << 5) + left ^ right;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003C635 File Offset: 0x0003A835
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return ReadOnlyMemory<T>.CombineHashCodes(ReadOnlyMemory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003C644 File Offset: 0x0003A844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetObjectStartLength(out int start, out int length)
		{
			start = this._index;
			length = this._length;
			return this._object;
		}

		// Token: 0x040012CE RID: 4814
		private readonly object _object;

		// Token: 0x040012CF RID: 4815
		private readonly int _index;

		// Token: 0x040012D0 RID: 4816
		private readonly int _length;

		// Token: 0x040012D1 RID: 4817
		internal const int RemoveFlagsBitMask = 2147483647;
	}
}
