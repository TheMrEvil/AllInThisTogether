using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C8 RID: 1736
	public static class MemoryMarshal
	{
		// Token: 0x06003FE0 RID: 16352 RVA: 0x000DFE94 File Offset: 0x000DE094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new Span<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000DFEC9 File Offset: 0x000DE0C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new ReadOnlySpan<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000DFEFE File Offset: 0x000DE0FE
		public unsafe static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000DFF0C File Offset: 0x000DE10C
		public static ref T GetReference<T>(Span<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000DFF28 File Offset: 0x000DE128
		public static ref T GetReference<T>(ReadOnlySpan<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000DFF44 File Offset: 0x000DE144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(Span<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000DFF70 File Offset: 0x000DE170
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000DFF9C File Offset: 0x000DE19C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			int length2;
			if (num == num2)
			{
				length2 = (int)length;
			}
			else if (num == 1U)
			{
				length2 = (int)(length / num2);
			}
			else
			{
				length2 = checked((int)(unchecked((ulong)length * (ulong)num / (ulong)num2)));
			}
			return new Span<TTo>(Unsafe.As<TFrom, TTo>(span._pointer.Value), length2);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000E0020 File Offset: 0x000DE220
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			int length2;
			if (num == num2)
			{
				length2 = (int)length;
			}
			else if (num == 1U)
			{
				length2 = (int)(length / num2);
			}
			else
			{
				length2 = checked((int)(unchecked((ulong)length * (ulong)num / (ulong)num2)));
			}
			return new ReadOnlySpan<TTo>(Unsafe.As<TFrom, TTo>(MemoryMarshal.GetReference<TFrom>(span)), length2);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000E009A File Offset: 0x000DE29A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> CreateSpan<T>(ref T reference, int length)
		{
			return new Span<T>(ref reference, length);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000E00A3 File Offset: 0x000DE2A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
		{
			return new ReadOnlySpan<T>(ref reference, length);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000E00AC File Offset: 0x000DE2AC
		public static bool TryGetArray<T>(ReadOnlyMemory<T> memory, out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			if (num < 0)
			{
				ArraySegment<T> arraySegment;
				if (((MemoryManager<T>)objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + (num & int.MaxValue), num2);
					return true;
				}
			}
			else
			{
				T[] array = objectStartLength as T[];
				if (array != null)
				{
					segment = new ArraySegment<T>(array, num, num2 & int.MaxValue);
					return true;
				}
			}
			if ((num2 & 2147483647) == 0)
			{
				segment = ArraySegment<T>.Empty;
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000E0140 File Offset: 0x000DE340
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager) where TManager : MemoryManager<T>
		{
			int num;
			int num2;
			manager = (memory.GetObjectStartLength(out num, out num2) as TManager);
			return manager != null;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000E0178 File Offset: 0x000DE378
		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager, out int start, out int length) where TManager : MemoryManager<T>
		{
			manager = (memory.GetObjectStartLength(out start, out length) as TManager);
			start &= int.MaxValue;
			if (manager == null)
			{
				start = 0;
				length = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000E01C0 File Offset: 0x000DE3C0
		public unsafe static IEnumerable<T> ToEnumerable<T>(ReadOnlyMemory<T> memory)
		{
			int num;
			for (int i = 0; i < memory.Length; i = num + 1)
			{
				yield return *memory.Span[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000E01D0 File Offset: 0x000DE3D0
		public static bool TryGetString(ReadOnlyMemory<char> memory, out string text, out int start, out int length)
		{
			int num;
			int num2;
			string text2 = memory.GetObjectStartLength(out num, out num2) as string;
			if (text2 != null)
			{
				text = text2;
				start = num;
				length = num2;
				return true;
			}
			text = null;
			start = 0;
			length = 0;
			return false;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000E0206 File Offset: 0x000DE406
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000E0240 File Offset: 0x000DE440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryRead<T>(ReadOnlySpan<byte> source, out T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)source.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
			return true;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000E028E File Offset: 0x000DE48E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000E02CC File Offset: 0x000DE4CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWrite<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)destination.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000E0308 File Offset: 0x000DE508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Memory<T> CreateFromPinnedArray<T>(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Memory<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(array, start, length | int.MinValue);
		}

		// Token: 0x020006C9 RID: 1737
		[CompilerGenerated]
		private sealed class <ToEnumerable>d__14<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06003FF5 RID: 16373 RVA: 0x000E037A File Offset: 0x000DE57A
			[DebuggerHidden]
			public <ToEnumerable>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06003FF6 RID: 16374 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06003FF7 RID: 16375 RVA: 0x000E0394 File Offset: 0x000DE594
			unsafe bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= memory.Length)
				{
					return false;
				}
				this.<>2__current = *memory.Span[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000E0419 File Offset: 0x000DE619
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003FF9 RID: 16377 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000E0421 File Offset: 0x000DE621
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003FFB RID: 16379 RVA: 0x000E0430 File Offset: 0x000DE630
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				MemoryMarshal.<ToEnumerable>d__14<T> <ToEnumerable>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<ToEnumerable>d__ = this;
				}
				else
				{
					<ToEnumerable>d__ = new MemoryMarshal.<ToEnumerable>d__14<T>(0);
				}
				<ToEnumerable>d__.memory = memory;
				return <ToEnumerable>d__;
			}

			// Token: 0x06003FFC RID: 16380 RVA: 0x000E0473 File Offset: 0x000DE673
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x040029F9 RID: 10745
			private int <>1__state;

			// Token: 0x040029FA RID: 10746
			private T <>2__current;

			// Token: 0x040029FB RID: 10747
			private int <>l__initialThreadId;

			// Token: 0x040029FC RID: 10748
			private ReadOnlyMemory<T> memory;

			// Token: 0x040029FD RID: 10749
			public ReadOnlyMemory<T> <>3__memory;

			// Token: 0x040029FE RID: 10750
			private int <i>5__2;
		}
	}
}
