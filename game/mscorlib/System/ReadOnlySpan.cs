using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000176 RID: 374
	[NonVersionable]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public readonly ref struct ReadOnlySpan<T>
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003C65C File Offset: 0x0003A85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array)
		{
			if (array == null)
			{
				this = default(ReadOnlySpan<T>);
				return;
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()));
			this._length = array.Length;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003C688 File Offset: 0x0003A888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(ReadOnlySpan<T>);
				return;
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start));
			this._length = length;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003C6E1 File Offset: 0x0003A8E1
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ReadOnlySpan(void* pointer, int length)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(ref *(byte*)pointer));
			this._length = length;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003C71A File Offset: 0x0003A91A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ReadOnlySpan(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x1700010D RID: 269
		public T this[int index]
		{
			[NonVersionable]
			[Intrinsic]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return Unsafe.Add<T>(this._pointer.Value, index);
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003C760 File Offset: 0x0003A960
		public ref readonly T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			return this._pointer.Value;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003C78C File Offset: 0x0003A98C
		public void CopyTo(Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003C7D8 File Offset: 0x0003A9D8
		public bool TryCopyTo(Span<T> destination)
		{
			bool result = false;
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				result = true;
			}
			return result;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003C824 File Offset: 0x0003AA24
		public static bool operator ==(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003C864 File Offset: 0x0003AA64
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this._pointer.Value))
				{
					return new string(ptr, 0, this._length);
				}
			}
			return string.Format("System.ReadOnlySpan<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003C910 File Offset: 0x0003AB10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003C950 File Offset: 0x0003AB50
		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._length];
			Buffer.Memmove<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), this._pointer.Value, (ulong)((long)this._length));
			return array;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003C99B File Offset: 0x0003AB9B
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0003C9A3 File Offset: 0x0003ABA3
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003C9AE File Offset: 0x0003ABAE
		public static bool operator !=(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003C9BA File Offset: 0x0003ABBA
		[Obsolete("Equals() on ReadOnlySpan will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003C9C6 File Offset: 0x0003ABC6
		[Obsolete("GetHashCode() on ReadOnlySpan will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003C9D2 File Offset: 0x0003ABD2
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003C9DA File Offset: 0x0003ABDA
		public static implicit operator ReadOnlySpan<T>(ArraySegment<T> segment)
		{
			return new ReadOnlySpan<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003C9F8 File Offset: 0x0003ABF8
		public static ReadOnlySpan<T> Empty
		{
			get
			{
				return default(ReadOnlySpan<T>);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003CA0E File Offset: 0x0003AC0E
		public ReadOnlySpan<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySpan<T>.Enumerator(this);
		}

		// Token: 0x040012D2 RID: 4818
		internal readonly ByReference<T> _pointer;

		// Token: 0x040012D3 RID: 4819
		private readonly int _length;

		// Token: 0x02000177 RID: 375
		[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
		public ref struct Enumerator
		{
			// Token: 0x06000EDE RID: 3806 RVA: 0x0003CA1B File Offset: 0x0003AC1B
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(ReadOnlySpan<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x0003CA2C File Offset: 0x0003AC2C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0003CA5A File Offset: 0x0003AC5A
			public ref readonly T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x040012D4 RID: 4820
			private readonly ReadOnlySpan<T> _span;

			// Token: 0x040012D5 RID: 4821
			private int _index;
		}
	}
}
