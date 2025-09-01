using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000173 RID: 371
	public readonly struct Range : IEquatable<Range>
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		public Index Start
		{
			[CompilerGenerated]
			get
			{
				return this.<Start>k__BackingField;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		public Index End
		{
			[CompilerGenerated]
			get
			{
				return this.<End>k__BackingField;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003BED8 File Offset: 0x0003A0D8
		public Range(Index start, Index end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003BEE8 File Offset: 0x0003A0E8
		public override bool Equals(object value)
		{
			if (value is Range)
			{
				Range range = (Range)value;
				return range.Start.Equals(this.Start) && range.End.Equals(this.End);
			}
			return false;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003BF34 File Offset: 0x0003A134
		public bool Equals(Range other)
		{
			return other.Start.Equals(this.Start) && other.End.Equals(this.End);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003BF70 File Offset: 0x0003A170
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Start.GetHashCode(), this.End.GetHashCode());
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		public unsafe override string ToString()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)48], 24);
			int num = 0;
			if (this.Start.IsFromEnd)
			{
				*span[0] = '^';
				num = 1;
			}
			int num2;
			((uint)this.Start.Value).TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			*span[num++] = '.';
			*span[num++] = '.';
			if (this.End.IsFromEnd)
			{
				*span[num++] = '^';
			}
			((uint)this.End.Value).TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			num += num2;
			return new string(span.Slice(0, num));
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003C097 File Offset: 0x0003A297
		public static Range StartAt(Index start)
		{
			return new Range(start, Index.End);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
		public static Range EndAt(Index end)
		{
			return new Range(Index.Start, end);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003C0B1 File Offset: 0x0003A2B1
		public static Range All
		{
			get
			{
				return new Range(Index.Start, Index.End);
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: TupleElementNames(new string[]
		{
			"Offset",
			"Length"
		})]
		public ValueTuple<int, int> GetOffsetAndLength(int length)
		{
			Index start = this.Start;
			int num;
			if (start.IsFromEnd)
			{
				num = length - start.Value;
			}
			else
			{
				num = start.Value;
			}
			Index end = this.End;
			int num2;
			if (end.IsFromEnd)
			{
				num2 = length - end.Value;
			}
			else
			{
				num2 = end.Value;
			}
			if (num2 > length || num > num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new ValueTuple<int, int>(num, num2 - num);
		}

		// Token: 0x040012CC RID: 4812
		[CompilerGenerated]
		private readonly Index <Start>k__BackingField;

		// Token: 0x040012CD RID: 4813
		[CompilerGenerated]
		private readonly Index <End>k__BackingField;
	}
}
