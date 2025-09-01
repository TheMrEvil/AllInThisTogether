using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000145 RID: 325
	public readonly struct Index : IEquatable<Index>
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x00031D9E File Offset: 0x0002FF9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Index(int value, bool fromEnd = false)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			if (fromEnd)
			{
				this._value = ~value;
				return;
			}
			this._value = value;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00031DBC File Offset: 0x0002FFBC
		private Index(int value)
		{
			this._value = value;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00031DC5 File Offset: 0x0002FFC5
		public static Index Start
		{
			get
			{
				return new Index(0);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00031DCD File Offset: 0x0002FFCD
		public static Index End
		{
			get
			{
				return new Index(-1);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00031DD5 File Offset: 0x0002FFD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromStart(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(value);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00031DE6 File Offset: 0x0002FFE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromEnd(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(~value);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00031DF8 File Offset: 0x0002FFF8
		public int Value
		{
			get
			{
				if (this._value < 0)
				{
					return ~this._value;
				}
				return this._value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00031E11 File Offset: 0x00030011
		public bool IsFromEnd
		{
			get
			{
				return this._value < 0;
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00031E1C File Offset: 0x0003001C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetOffset(int length)
		{
			int result;
			if (this.IsFromEnd)
			{
				result = length - ~this._value;
			}
			else
			{
				result = this._value;
			}
			return result;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00031E45 File Offset: 0x00030045
		public override bool Equals(object value)
		{
			return value is Index && this._value == ((Index)value)._value;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00031E64 File Offset: 0x00030064
		public bool Equals(Index other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00031E74 File Offset: 0x00030074
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00031E7C File Offset: 0x0003007C
		public static implicit operator Index(int value)
		{
			return Index.FromStart(value);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00031E84 File Offset: 0x00030084
		public override string ToString()
		{
			if (this.IsFromEnd)
			{
				return this.ToStringFromEnd();
			}
			return ((uint)this.Value).ToString();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00031EB0 File Offset: 0x000300B0
		private unsafe string ToStringFromEnd()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)22], 11);
			int num;
			((uint)this.Value).TryFormat(span.Slice(1), out num, default(ReadOnlySpan<char>), null);
			*span[0] = '^';
			return new string(span.Slice(0, num + 1));
		}

		// Token: 0x04001251 RID: 4689
		private readonly int _value;
	}
}
