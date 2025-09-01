using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200001B RID: 27
	internal ref struct ValueStringBuilder
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00012922 File Offset: 0x00010B22
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00012939 File Offset: 0x00010B39
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00012941 File Offset: 0x00010B41
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0001294A File Offset: 0x00010B4A
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00012957 File Offset: 0x00010B57
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._chars.Length);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001297A File Offset: 0x00010B7A
		public unsafe ref char GetPinnableReference(bool terminate = false)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x17000027 RID: 39
		public char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000129B9 File Offset: 0x00010BB9
		public override string ToString()
		{
			string result = new string(this._chars.Slice(0, this._pos));
			this.Dispose();
			return result;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000129DD File Offset: 0x00010BDD
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000129E5 File Offset: 0x00010BE5
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00012A22 File Offset: 0x00010C22
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00012A3B File Offset: 0x00010C3B
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00012A56 File Offset: 0x00010C56
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00012A6C File Offset: 0x00010C6C
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00012AB0 File Offset: 0x00010CB0
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int length = this._pos - index;
			this._chars.Slice(index, length).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00012B2C File Offset: 0x00010D2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00012B70 File Offset: 0x00010D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00012BC0 File Offset: 0x00010DC0
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00012C24 File Offset: 0x00010E24
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00012C8C File Offset: 0x00010E8C
		public unsafe void Append(char* value, int length)
		{
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public void Append(ReadOnlySpan<char> value)
		{
			if (this._pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00012D5C File Offset: 0x00010F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00012D9D File Offset: 0x00010F9D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00012DB0 File Offset: 0x00010FB0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int requiredAdditionalCapacity)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + requiredAdditionalCapacity, this._chars.Length * 2));
			this._chars.CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00012E20 File Offset: 0x00011020
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x040000A6 RID: 166
		private char[] _arrayToReturnToPool;

		// Token: 0x040000A7 RID: 167
		private Span<char> _chars;

		// Token: 0x040000A8 RID: 168
		private int _pos;
	}
}
