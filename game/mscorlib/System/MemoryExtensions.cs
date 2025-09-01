using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000159 RID: 345
	public static class MemoryExtensions
	{
		// Token: 0x06000D69 RID: 3433 RVA: 0x00033E93 File Offset: 0x00032093
		public static bool Contains(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			return span.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00033EA4 File Offset: 0x000320A4
		public static bool Equals(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionNone(span, other) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionIgnoreCase(span, other) == 0;
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.CompareOptionNone(span, other) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.CompareOptionIgnoreCase(span, other) == 0;
			case StringComparison.Ordinal:
				return span.EqualsOrdinal(other);
			case StringComparison.OrdinalIgnoreCase:
				return span.EqualsOrdinalIgnoreCase(other);
			default:
				return false;
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00033F32 File Offset: 0x00032132
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinal(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || span.SequenceEqual(value));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00033F58 File Offset: 0x00032158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || CompareInfo.CompareOrdinalIgnoreCase(span, value) == 0);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00033F84 File Offset: 0x00032184
		internal unsafe static bool Contains(this ReadOnlySpan<char> source, char value)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (*source[i] == (ushort)value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00033FB4 File Offset: 0x000321B4
		public static int CompareTo(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionNone(span, other);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.CompareOptionIgnoreCase(span, other);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.CompareOptionNone(span, other);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.CompareOptionIgnoreCase(span, other);
			case StringComparison.Ordinal:
				if (span.Length == 0 || other.Length == 0)
				{
					return span.Length - other.Length;
				}
				return string.CompareOrdinal(span, other);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.CompareOrdinalIgnoreCase(span, other);
			default:
				return 0;
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00034058 File Offset: 0x00032258
		public static int IndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (value.Length == 0)
			{
				return 0;
			}
			if (span.Length == 0)
			{
				return -1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.IndexOfCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.IndexOfCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.IndexOfCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.IndexOfCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return SpanHelpers.IndexOfOrdinalHelper(span, value, false);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.IndexOfOrdinalHelper(span, value, true);
			default:
				return -1;
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000340F4 File Offset: 0x000322F4
		public static int ToLower(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				culture.TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCase(source, destination, false);
			}
			return source.Length;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00034148 File Offset: 0x00032348
		public static int ToLowerInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				CultureInfo.InvariantCulture.TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				CultureInfo.InvariantCulture.TextInfo.ChangeCase(source, destination, false);
			}
			return source.Length;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003419C File Offset: 0x0003239C
		public static int ToUpper(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				culture.TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCase(source, destination, true);
			}
			return source.Length;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000341F0 File Offset: 0x000323F0
		public static int ToUpperInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				CultureInfo.InvariantCulture.TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				CultureInfo.InvariantCulture.TextInfo.ChangeCase(source, destination, true);
			}
			return source.Length;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00034244 File Offset: 0x00032444
		public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.EndsWithCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.EndsWithCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return span.EndsWith(value);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.EndsWithOrdinalIgnoreCaseHelper(span, value);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000342E0 File Offset: 0x000324E0
		public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return SpanHelpers.StartsWithCultureHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.CurrentCultureIgnoreCase:
				return SpanHelpers.StartsWithCultureIgnoreCaseHelper(span, value, CultureInfo.CurrentCulture.CompareInfo);
			case StringComparison.InvariantCulture:
				return SpanHelpers.StartsWithCultureHelper(span, value, CompareInfo.Invariant);
			case StringComparison.InvariantCultureIgnoreCase:
				return SpanHelpers.StartsWithCultureIgnoreCaseHelper(span, value, CompareInfo.Invariant);
			case StringComparison.Ordinal:
				return span.StartsWith(value);
			case StringComparison.OrdinalIgnoreCase:
				return SpanHelpers.StartsWithOrdinalIgnoreCaseHelper(span, value);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003437C File Offset: 0x0003257C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), start), array.Length - start);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000343F0 File Offset: 0x000325F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			int offset = startIndex.GetOffset(array.Length);
			if (offset > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), offset), array.Length - offset);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003447C File Offset: 0x0003267C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), item), item2);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00034524 File Offset: 0x00032724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(text.GetRawStringData(), text.Length);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00034550 File Offset: 0x00032750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), text.Length - start);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000345A0 File Offset: 0x000327A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), length);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000345F4 File Offset: 0x000327F4
		public static ReadOnlyMemory<char> AsMemory(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlyMemory<char>);
			}
			return new ReadOnlyMemory<char>(text, 0, text.Length);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003461C File Offset: 0x0003281C
		public static ReadOnlyMemory<char> AsMemory(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, text.Length - start);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00034660 File Offset: 0x00032860
		public static ReadOnlyMemory<char> AsMemory(this string text, Index startIndex)
		{
			if (text == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			int offset = startIndex.GetOffset(text.Length);
			if (offset > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlyMemory<char>(text, offset, text.Length - offset);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000346BC File Offset: 0x000328BC
		public static ReadOnlyMemory<char> AsMemory(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, length);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00034708 File Offset: 0x00032908
		public static ReadOnlyMemory<char> AsMemory(this string text, Range range)
		{
			if (text == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(text.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new ReadOnlyMemory<char>(text, item, item2);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00034778 File Offset: 0x00032978
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span)
		{
			return span.TrimStart().TrimEnd();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00034788 File Offset: 0x00032988
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000347C0 File Offset: 0x000329C0
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span)
		{
			int num = span.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000347FC File Offset: 0x000329FC
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, char trimChar)
		{
			return span.TrimStart(trimChar).TrimEnd(trimChar);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003480C File Offset: 0x00032A0C
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = 0;
			while (num < span.Length && *span[num] == (ushort)trimChar)
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00034840 File Offset: 0x00032A40
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = span.Length - 1;
			while (num >= 0 && *span[num] == (ushort)trimChar)
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00034878 File Offset: 0x00032A78
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			return span.TrimStart(trimChars).TrimEnd(trimChars);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00034888 File Offset: 0x00032A88
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimStart();
			}
			int i = 0;
			IL_40:
			while (i < span.Length)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i++;
						goto IL_40;
					}
				}
				break;
			}
			return span.Slice(i);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000348E8 File Offset: 0x00032AE8
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimEnd();
			}
			int i = span.Length - 1;
			IL_48:
			while (i >= 0)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i--;
						goto IL_48;
					}
				}
				break;
			}
			return span.Slice(0, i + 1);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003494C File Offset: 0x00032B4C
		public unsafe static bool IsWhiteSpace(this ReadOnlySpan<char> span)
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (!char.IsWhiteSpace((char)(*span[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00034980 File Offset: 0x00032B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this Span<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00034A18 File Offset: 0x00032C18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00034A8C File Offset: 0x00032C8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this Span<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00034B24 File Offset: 0x00032D24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00034B98 File Offset: 0x00032D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this Span<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (ulong)((long)length * (long)num));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00034C10 File Offset: 0x00032E10
		public static int SequenceCompareTo<T>(this Span<T> span, ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00034CC8 File Offset: 0x00032EC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00034D60 File Offset: 0x00032F60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00034DD4 File Offset: 0x00032FD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00034E6C File Offset: 0x0003306C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00034EE0 File Offset: 0x000330E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00034F44 File Offset: 0x00033144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00034FB4 File Offset: 0x000331B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00035028 File Offset: 0x00033228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003508C File Offset: 0x0003328C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000350FC File Offset: 0x000332FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00035170 File Offset: 0x00033370
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000351D4 File Offset: 0x000333D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00035244 File Offset: 0x00033444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000352B8 File Offset: 0x000334B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003531C File Offset: 0x0003351C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003538C File Offset: 0x0003358C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00035400 File Offset: 0x00033600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (ulong)((long)length * (long)num));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00035478 File Offset: 0x00033678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SequenceCompareTo<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00035530 File Offset: 0x00033730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length * (long)num));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000355A8 File Offset: 0x000337A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length * (long)num));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00035620 File Offset: 0x00033820
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length2 * (long)num));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000356A4 File Offset: 0x000338A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			ulong num;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out num))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (ulong)((long)length2 * (long)num));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00035728 File Offset: 0x00033928
		public unsafe static void Reverse<T>(this Span<T> span)
		{
			ref T reference = ref MemoryMarshal.GetReference<T>(span);
			int i = 0;
			int num = span.Length - 1;
			while (i < num)
			{
				T t = *Unsafe.Add<T>(ref reference, i);
				*Unsafe.Add<T>(ref reference, i) = *Unsafe.Add<T>(ref reference, num);
				*Unsafe.Add<T>(ref reference, num) = t;
				i++;
				num--;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00035788 File Offset: 0x00033988
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00035790 File Offset: 0x00033990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start, int length)
		{
			return new Span<T>(array, start, length);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003579A File Offset: 0x0003399A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000357B6 File Offset: 0x000339B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Span<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000357EC File Offset: 0x000339EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Index startIndex)
		{
			int offset = startIndex.GetOffset(segment.Count);
			return segment.AsSpan(offset);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003580F File Offset: 0x00033A0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Span<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00035850 File Offset: 0x00033A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Range range)
		{
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(segment.Count);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(segment.Array, segment.Offset + item, item2);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003588E File Offset: 0x00033A8E
		public static Memory<T> AsMemory<T>(this T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00035896 File Offset: 0x00033A96
		public static Memory<T> AsMemory<T>(this T[] array, int start)
		{
			return new Memory<T>(array, start);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000358A0 File Offset: 0x00033AA0
		public static Memory<T> AsMemory<T>(this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			int offset = startIndex.GetOffset(array.Length);
			return new Memory<T>(array, offset);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000358E0 File Offset: 0x00033AE0
		public static Memory<T> AsMemory<T>(this T[] array, int start, int length)
		{
			return new Memory<T>(array, start, length);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000358EC File Offset: 0x00033AEC
		public static Memory<T> AsMemory<T>(this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Memory<T>(array, item, item2);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00035958 File Offset: 0x00033B58
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00035974 File Offset: 0x00033B74
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000359A7 File Offset: 0x00033BA7
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000359E8 File Offset: 0x00033BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Span<T> destination)
		{
			new ReadOnlySpan<T>(source).CopyTo(destination);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00035A04 File Offset: 0x00033C04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Memory<T> destination)
		{
			source.CopyTo(destination.Span);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00035A13 File Offset: 0x00033C13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other)
		{
			return span.Overlaps(other);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00035A21 File Offset: 0x00033C21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			return span.Overlaps(other, out elementOffset);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00035A30 File Offset: 0x00033C30
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				return false;
			}
			IntPtr value = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				return (int)value < span.Length * Unsafe.SizeOf<T>() || (int)value > -(other.Length * Unsafe.SizeOf<T>());
			}
			return (long)value < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)value > -((long)other.Length * (long)Unsafe.SizeOf<T>());
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00035ACC File Offset: 0x00033CCC
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				elementOffset = 0;
				return false;
			}
			IntPtr value = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				if ((int)value < span.Length * Unsafe.SizeOf<T>() || (int)value > -(other.Length * Unsafe.SizeOf<T>()))
				{
					if ((int)value % Unsafe.SizeOf<T>() != 0)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)value / Unsafe.SizeOf<T>();
					return true;
				}
				elementOffset = 0;
				return false;
			}
			else
			{
				if ((long)value < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)value > -((long)other.Length * (long)Unsafe.SizeOf<T>()))
				{
					if ((long)value % (long)Unsafe.SizeOf<T>() != 0L)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)((long)value / (long)Unsafe.SizeOf<T>());
					return true;
				}
				elementOffset = 0;
				return false;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00035BB6 File Offset: 0x00033DB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this Span<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00035BBF File Offset: 0x00033DBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this Span<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00035BCD File Offset: 0x00033DCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this Span<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			return span.BinarySearch(value, comparer);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00035BDC File Offset: 0x00033DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this ReadOnlySpan<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00035BE5 File Offset: 0x00033DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00035BF0 File Offset: 0x00033DF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this ReadOnlySpan<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			if (comparer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparer);
			}
			SpanHelpers.ComparerComparable<T, TComparer> comparable = new SpanHelpers.ComparerComparable<T, TComparer>(value, comparer);
			return span.BinarySearch(comparable);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00035C1C File Offset: 0x00033E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsTypeComparableAsBytes<T>(out ulong size)
		{
			if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
			{
				size = 1UL;
				return true;
			}
			if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
			{
				size = 2UL;
				return true;
			}
			if (typeof(T) == typeof(int) || typeof(T) == typeof(uint))
			{
				size = 4UL;
				return true;
			}
			if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
			{
				size = 8UL;
				return true;
			}
			size = 0UL;
			return false;
		}
	}
}
