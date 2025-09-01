using System;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x0200003A RID: 58
	internal sealed class XHashtable<TValue>
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000B07E File Offset: 0x0000927E
		public XHashtable(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
		{
			this._state = new XHashtable<TValue>.XHashtableState(extractKey, capacity);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B093 File Offset: 0x00009293
		public bool TryGetValue(string key, int index, int count, out TValue value)
		{
			return this._state.TryGetValue(key, index, count, out value);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public TValue Add(TValue value)
		{
			TValue result;
			while (!this._state.TryAdd(value, out result))
			{
				lock (this)
				{
					XHashtable<TValue>.XHashtableState state = this._state.Resize();
					Thread.MemoryBarrier();
					this._state = state;
				}
			}
			return result;
		}

		// Token: 0x0400013C RID: 316
		private XHashtable<TValue>.XHashtableState _state;

		// Token: 0x0400013D RID: 317
		private const int StartingHash = 352654597;

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x06000247 RID: 583
		public delegate string ExtractKeyDelegate(TValue value);

		// Token: 0x0200003C RID: 60
		private sealed class XHashtableState
		{
			// Token: 0x0600024A RID: 586 RVA: 0x0000B108 File Offset: 0x00009308
			public XHashtableState(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
			{
				this._buckets = new int[capacity];
				this._entries = new XHashtable<TValue>.XHashtableState.Entry[capacity];
				this._extractKey = extractKey;
			}

			// Token: 0x0600024B RID: 587 RVA: 0x0000B130 File Offset: 0x00009330
			public XHashtable<TValue>.XHashtableState Resize()
			{
				if (this._numEntries < this._buckets.Length)
				{
					return this;
				}
				int num = 0;
				for (int i = 0; i < this._buckets.Length; i++)
				{
					int j = this._buckets[i];
					if (j == 0)
					{
						j = Interlocked.CompareExchange(ref this._buckets[i], -1, 0);
					}
					while (j > 0)
					{
						if (this._extractKey(this._entries[j].Value) != null)
						{
							num++;
						}
						if (this._entries[j].Next == 0)
						{
							j = Interlocked.CompareExchange(ref this._entries[j].Next, -1, 0);
						}
						else
						{
							j = this._entries[j].Next;
						}
					}
				}
				if (num < this._buckets.Length / 2)
				{
					num = this._buckets.Length;
				}
				else
				{
					num = this._buckets.Length * 2;
					if (num < 0)
					{
						throw new OverflowException();
					}
				}
				XHashtable<TValue>.XHashtableState xhashtableState = new XHashtable<TValue>.XHashtableState(this._extractKey, num);
				for (int k = 0; k < this._buckets.Length; k++)
				{
					for (int l = this._buckets[k]; l > 0; l = this._entries[l].Next)
					{
						TValue tvalue;
						xhashtableState.TryAdd(this._entries[l].Value, out tvalue);
					}
				}
				return xhashtableState;
			}

			// Token: 0x0600024C RID: 588 RVA: 0x0000B288 File Offset: 0x00009488
			public bool TryGetValue(string key, int index, int count, out TValue value)
			{
				int hashCode = XHashtable<TValue>.XHashtableState.ComputeHashCode(key, index, count);
				int num = 0;
				if (this.FindEntry(hashCode, key, index, count, ref num))
				{
					value = this._entries[num].Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x0600024D RID: 589 RVA: 0x0000B2D4 File Offset: 0x000094D4
			public bool TryAdd(TValue value, out TValue newValue)
			{
				newValue = value;
				string text = this._extractKey(value);
				if (text == null)
				{
					return true;
				}
				int num = XHashtable<TValue>.XHashtableState.ComputeHashCode(text, 0, text.Length);
				int num2 = Interlocked.Increment(ref this._numEntries);
				if (num2 < 0 || num2 >= this._buckets.Length)
				{
					return false;
				}
				this._entries[num2].Value = value;
				this._entries[num2].HashCode = num;
				Thread.MemoryBarrier();
				int num3 = 0;
				while (!this.FindEntry(num, text, 0, text.Length, ref num3))
				{
					if (num3 == 0)
					{
						num3 = Interlocked.CompareExchange(ref this._buckets[num & this._buckets.Length - 1], num2, 0);
					}
					else
					{
						num3 = Interlocked.CompareExchange(ref this._entries[num3].Next, num2, 0);
					}
					if (num3 <= 0)
					{
						return num3 == 0;
					}
				}
				newValue = this._entries[num3].Value;
				return true;
			}

			// Token: 0x0600024E RID: 590 RVA: 0x0000B3C4 File Offset: 0x000095C4
			private bool FindEntry(int hashCode, string key, int index, int count, ref int entryIndex)
			{
				int num = entryIndex;
				int i;
				if (num == 0)
				{
					i = this._buckets[hashCode & this._buckets.Length - 1];
				}
				else
				{
					i = num;
				}
				while (i > 0)
				{
					if (this._entries[i].HashCode == hashCode)
					{
						string text = this._extractKey(this._entries[i].Value);
						if (text == null)
						{
							if (this._entries[i].Next > 0)
							{
								this._entries[i].Value = default(TValue);
								i = this._entries[i].Next;
								if (num == 0)
								{
									this._buckets[hashCode & this._buckets.Length - 1] = i;
									continue;
								}
								this._entries[num].Next = i;
								continue;
							}
						}
						else if (count == text.Length && string.CompareOrdinal(key, index, text, 0, count) == 0)
						{
							entryIndex = i;
							return true;
						}
					}
					num = i;
					i = this._entries[i].Next;
				}
				entryIndex = num;
				return false;
			}

			// Token: 0x0600024F RID: 591 RVA: 0x0000B4D8 File Offset: 0x000096D8
			private static int ComputeHashCode(string key, int index, int count)
			{
				int num = 352654597;
				int num2 = index + count;
				for (int i = index; i < num2; i++)
				{
					num += (num << 7 ^ (int)key[i]);
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				return num & int.MaxValue;
			}

			// Token: 0x0400013E RID: 318
			private int[] _buckets;

			// Token: 0x0400013F RID: 319
			private XHashtable<TValue>.XHashtableState.Entry[] _entries;

			// Token: 0x04000140 RID: 320
			private int _numEntries;

			// Token: 0x04000141 RID: 321
			private XHashtable<TValue>.ExtractKeyDelegate _extractKey;

			// Token: 0x04000142 RID: 322
			private const int EndOfList = 0;

			// Token: 0x04000143 RID: 323
			private const int FullList = -1;

			// Token: 0x0200003D RID: 61
			private struct Entry
			{
				// Token: 0x04000144 RID: 324
				public TValue Value;

				// Token: 0x04000145 RID: 325
				public int HashCode;

				// Token: 0x04000146 RID: 326
				public int Next;
			}
		}
	}
}
