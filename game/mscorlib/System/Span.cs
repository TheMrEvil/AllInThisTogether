using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200017D RID: 381
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[NonVersionable]
	public readonly ref struct Span<T>
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0003D298 File Offset: 0x0003B498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._pointer = new ByReference<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()));
			this._length = array.Length;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._pointer = new ByReference<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start));
			this._length = length;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003D381 File Offset: 0x0003B581
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Span(void* pointer, int length)
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

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003D3BA File Offset: 0x0003B5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Span(ref T ptr, int length)
		{
			this._pointer = new ByReference<T>(ref ptr);
			this._length = length;
		}

		// Token: 0x17000114 RID: 276
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

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003D400 File Offset: 0x0003B600
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			return this._pointer.Value;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003D42C File Offset: 0x0003B62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				SpanHelpers.ClearWithReferences(Unsafe.As<T, IntPtr>(this._pointer.Value), (ulong)((long)this._length * (long)(Unsafe.SizeOf<T>() / IntPtr.Size)));
				return;
			}
			SpanHelpers.ClearWithoutReferences(Unsafe.As<T, byte>(this._pointer.Value), (ulong)((long)this._length * (long)Unsafe.SizeOf<T>()));
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003D494 File Offset: 0x0003B694
		public unsafe void Fill(T value)
		{
			if (Unsafe.SizeOf<T>() == 1)
			{
				uint length = (uint)this._length;
				if (length == 0U)
				{
					return;
				}
				T t = value;
				Unsafe.InitBlockUnaligned(Unsafe.As<T, byte>(this._pointer.Value), *Unsafe.As<T, byte>(ref t), length);
				return;
			}
			else
			{
				ulong num = (ulong)this._length;
				if (num == 0UL)
				{
					return;
				}
				ref T value2 = ref this._pointer.Value;
				ulong num2 = (ulong)Unsafe.SizeOf<T>();
				ulong num3;
				for (num3 = 0UL; num3 < (num & 18446744073709551608UL); num3 += 8UL)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 4UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 5UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 6UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 7UL) * num2) = value;
				}
				if (num3 < (num & 18446744073709551612UL))
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 1UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 2UL) * num2) = value;
					*Unsafe.AddByteOffset<T>(ref value2, (num3 + 3UL) * num2) = value;
					num3 += 4UL;
				}
				while (num3 < num)
				{
					*Unsafe.AddByteOffset<T>(ref value2, num3 * num2) = value;
					num3 += 1UL;
				}
				return;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003D640 File Offset: 0x0003B840
		public void CopyTo(Span<T> destination)
		{
			if (this._length <= destination.Length)
			{
				Buffer.Memmove<T>(destination._pointer.Value, this._pointer.Value, (ulong)((long)this._length));
				return;
			}
			ThrowHelper.ThrowArgumentException_DestinationTooShort();
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003D68C File Offset: 0x0003B88C
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003D6D8 File Offset: 0x0003B8D8
		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left._pointer.Value, right._pointer.Value);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003D718 File Offset: 0x0003B918
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pointer.Value, span._length);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003D740 File Offset: 0x0003B940
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this._pointer.Value))
				{
					return new string(ptr, 0, this._length);
				}
			}
			return string.Format("System.Span<{0}>[{1}]", typeof(T).Name, this._length);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), this._length - start);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003D7EC File Offset: 0x0003B9EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(this._pointer.Value, start), length);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003D82C File Offset: 0x0003BA2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003D877 File Offset: 0x0003BA77
		public int Length
		{
			[NonVersionable]
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003D87F File Offset: 0x0003BA7F
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003D88A File Offset: 0x0003BA8A
		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003C9BA File Offset: 0x0003ABBA
		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.");
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003C9C6 File Offset: 0x0003ABC6
		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		public override int GetHashCode()
		{
			throw new NotSupportedException("GetHashCode() on Span and ReadOnlySpan is not supported.");
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003D896 File Offset: 0x0003BA96
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003D89E File Offset: 0x0003BA9E
		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003D8BC File Offset: 0x0003BABC
		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003D8D2 File Offset: 0x0003BAD2
		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		// Token: 0x040012E3 RID: 4835
		internal readonly ByReference<T> _pointer;

		// Token: 0x040012E4 RID: 4836
		private readonly int _length;

		// Token: 0x0200017E RID: 382
		[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
		public ref struct Enumerator
		{
			// Token: 0x06000F5B RID: 3931 RVA: 0x0003D8DF File Offset: 0x0003BADF
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			// Token: 0x06000F5C RID: 3932 RVA: 0x0003D8F0 File Offset: 0x0003BAF0
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

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003D91E File Offset: 0x0003BB1E
			public ref T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._span[this._index];
				}
			}

			// Token: 0x040012E5 RID: 4837
			private readonly Span<T> _span;

			// Token: 0x040012E6 RID: 4838
			private int _index;
		}
	}
}
