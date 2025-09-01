using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AE1 RID: 2785
	public static class BuffersExtensions
	{
		// Token: 0x0600630F RID: 25359 RVA: 0x0014B444 File Offset: 0x00149644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SequencePosition? PositionOf<T>(this ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			if (!source.IsSingleSegment)
			{
				return BuffersExtensions.PositionOfMultiSegment<T>(source, value);
			}
			int num = source.First.Span.IndexOf(value);
			if (num != -1)
			{
				return new SequencePosition?(source.GetPosition((long)num));
			}
			return null;
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x0014B494 File Offset: 0x00149694
		private static SequencePosition? PositionOfMultiSegment<T>(in ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			SequencePosition start = source.Start;
			SequencePosition origin = start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (source.TryGet(ref start, out readOnlyMemory, true))
			{
				int num = readOnlyMemory.Span.IndexOf(value);
				if (num != -1)
				{
					return new SequencePosition?(source.GetPosition((long)num, origin));
				}
				if (start.GetObject() == null)
				{
					break;
				}
				origin = start;
			}
			return null;
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0014B4F0 File Offset: 0x001496F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this ReadOnlySequence<T> source, Span<T> destination)
		{
			if (source.Length > (long)destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.destination);
			}
			if (source.IsSingleSegment)
			{
				source.First.Span.CopyTo(destination);
				return;
			}
			BuffersExtensions.CopyToMultiSegment<T>(source, destination);
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x0014B53C File Offset: 0x0014973C
		private static void CopyToMultiSegment<T>(in ReadOnlySequence<T> sequence, Span<T> destination)
		{
			SequencePosition start = sequence.Start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (sequence.TryGet(ref start, out readOnlyMemory, true))
			{
				ReadOnlySpan<T> span = readOnlyMemory.Span;
				span.CopyTo(destination);
				if (start.GetObject() == null)
				{
					break;
				}
				destination = destination.Slice(span.Length);
			}
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x0014B588 File Offset: 0x00149788
		public static T[] ToArray<T>(this ReadOnlySequence<T> sequence)
		{
			T[] array = new T[sequence.Length];
			sequence.CopyTo(array);
			return array;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x0014B5B0 File Offset: 0x001497B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(this IBufferWriter<T> writer, ReadOnlySpan<T> value)
		{
			Span<T> span = writer.GetSpan(0);
			if (value.Length <= span.Length)
			{
				value.CopyTo(span);
				writer.Advance(value.Length);
				return;
			}
			BuffersExtensions.WriteMultiSegment<T>(writer, value, span);
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x0014B5F4 File Offset: 0x001497F4
		private static void WriteMultiSegment<T>(IBufferWriter<T> writer, in ReadOnlySpan<T> source, Span<T> destination)
		{
			ReadOnlySpan<T> readOnlySpan = source;
			for (;;)
			{
				int num = Math.Min(destination.Length, readOnlySpan.Length);
				readOnlySpan.Slice(0, num).CopyTo(destination);
				writer.Advance(num);
				readOnlySpan = readOnlySpan.Slice(num);
				if (readOnlySpan.Length <= 0)
				{
					break;
				}
				destination = writer.GetSpan(readOnlySpan.Length);
			}
		}
	}
}
