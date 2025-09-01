using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000157 RID: 343
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	public readonly struct Memory<T> : IEquatable<Memory<T>>
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00033854 File Offset: 0x00031A54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000338B0 File Offset: 0x00031AB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = array.Length - start;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00033920 File Offset: 0x00031B20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
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
			this._object = array;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00033997 File Offset: 0x00031B97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int length)
		{
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = int.MinValue;
			this._length = length;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000339BB File Offset: 0x00031BBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(MemoryManager<T> manager, int start, int length)
		{
			if (length < 0 || start < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = (start | int.MinValue);
			this._length = length;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000339E5 File Offset: 0x00031BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000339FC File Offset: 0x00031BFC
		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00033A04 File Offset: 0x00031C04
		public static implicit operator Memory<T>(ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00033A20 File Offset: 0x00031C20
		public unsafe static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00033A30 File Offset: 0x00031C30
		public static Memory<T> Empty
		{
			get
			{
				return default(Memory<T>);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00033A46 File Offset: 0x00031C46
		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00033A54 File Offset: 0x00031C54
		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00033A68 File Offset: 0x00031C68
		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format("System.Memory<{0}>[{1}]", typeof(T).Name, this._length & int.MaxValue);
			}
			string text = this._object as string;
			if (text == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00033AF8 File Offset: 0x00031CF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, length - start);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00033B38 File Offset: 0x00031D38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Memory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = length2 & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00033B80 File Offset: 0x00031D80
		public Span<T> Span
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
						return new Span<T>(Unsafe.As<char, T>(text.GetRawStringData()), text.Length).Slice(this._index, this._length);
					}
				}
				if (this._object != null)
				{
					return new Span<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(Span<T>);
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00033C4C File Offset: 0x00031E4C
		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00033C70 File Offset: 0x00031E70
		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00033C94 File Offset: 0x00031E94
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

		// Token: 0x06000D60 RID: 3424 RVA: 0x00033D88 File Offset: 0x00031F88
		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00033DA4 File Offset: 0x00031FA4
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> other = (Memory<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00033DEB File Offset: 0x00031FEB
		public bool Equals(Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00033E19 File Offset: 0x00032019
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return Memory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00033E4B File Offset: 0x0003204B
		private static int CombineHashCodes(int left, int right)
		{
			return (left << 5) + left ^ right;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00033E54 File Offset: 0x00032054
		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x0400127C RID: 4732
		private readonly object _object;

		// Token: 0x0400127D RID: 4733
		private readonly int _index;

		// Token: 0x0400127E RID: 4734
		private readonly int _length;

		// Token: 0x0400127F RID: 4735
		private const int RemoveFlagsBitMask = 2147483647;
	}
}
