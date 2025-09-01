using System;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000AC RID: 172
	internal abstract class SortedTable<T> : Table<T> where T : SortedTable<T>.IRecord
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0001E781 File Offset: 0x0001C981
		internal SortedTable<T>.Enumerable Filter(int token)
		{
			return new SortedTable<T>.Enumerable(this, token);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001E78C File Offset: 0x0001C98C
		protected void Sort()
		{
			ulong[] array = new ulong[this.rowCount];
			uint num = 0U;
			while ((ulong)num < (ulong)((long)array.Length))
			{
				array[(int)num] = (ulong)((long)this.records[(int)num].SortKey << 32 | (long)((ulong)num));
				num += 1U;
			}
			Array.Sort<ulong>(array);
			T[] array2 = new T[this.rowCount];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.records[(int)array[i]];
			}
			this.records = array2;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001E815 File Offset: 0x0001CA15
		protected SortedTable()
		{
		}

		// Token: 0x02000341 RID: 833
		internal interface IRecord
		{
			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x06002614 RID: 9748
			int SortKey { get; }

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x06002615 RID: 9749
			int FilterKey { get; }
		}

		// Token: 0x02000342 RID: 834
		internal struct Enumerable
		{
			// Token: 0x06002616 RID: 9750 RVA: 0x000B5457 File Offset: 0x000B3657
			internal Enumerable(SortedTable<T> table, int token)
			{
				this.table = table;
				this.token = token;
			}

			// Token: 0x06002617 RID: 9751 RVA: 0x000B5468 File Offset: 0x000B3668
			public SortedTable<T>.Enumerator GetEnumerator()
			{
				T[] records = this.table.records;
				if (!this.table.Sorted)
				{
					return new SortedTable<T>.Enumerator(records, this.table.RowCount - 1, -1, this.token);
				}
				int num = SortedTable<T>.Enumerable.BinarySearch(records, this.table.RowCount, this.token & 16777215);
				if (num < 0)
				{
					return new SortedTable<T>.Enumerator(null, 0, 1, -1);
				}
				int num2 = num;
				while (num2 > 0 && (records[num2 - 1].FilterKey & 16777215) == (this.token & 16777215))
				{
					num2--;
				}
				int num3 = num;
				int num4 = this.table.RowCount - 1;
				while (num3 < num4 && (records[num3 + 1].FilterKey & 16777215) == (this.token & 16777215))
				{
					num3++;
				}
				return new SortedTable<T>.Enumerator(records, num3, num2 - 1, this.token);
			}

			// Token: 0x06002618 RID: 9752 RVA: 0x000B5564 File Offset: 0x000B3764
			private static int BinarySearch(T[] records, int length, int maskedToken)
			{
				int i = 0;
				int num = length - 1;
				while (i <= num)
				{
					int num2 = i + (num - i) / 2;
					int num3 = records[num2].FilterKey & 16777215;
					if (maskedToken == num3)
					{
						return num2;
					}
					if (maskedToken < num3)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return -1;
			}

			// Token: 0x04000E8F RID: 3727
			private readonly SortedTable<T> table;

			// Token: 0x04000E90 RID: 3728
			private readonly int token;
		}

		// Token: 0x02000343 RID: 835
		internal struct Enumerator
		{
			// Token: 0x06002619 RID: 9753 RVA: 0x000B55B5 File Offset: 0x000B37B5
			internal Enumerator(T[] records, int max, int index, int token)
			{
				this.records = records;
				this.token = token;
				this.max = max;
				this.index = index;
			}

			// Token: 0x170008A5 RID: 2213
			// (get) Token: 0x0600261A RID: 9754 RVA: 0x000B55D4 File Offset: 0x000B37D4
			public int Current
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x0600261B RID: 9755 RVA: 0x000B55DC File Offset: 0x000B37DC
			public bool MoveNext()
			{
				while (this.index < this.max)
				{
					this.index++;
					if (this.records[this.index].FilterKey == this.token)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04000E91 RID: 3729
			private readonly T[] records;

			// Token: 0x04000E92 RID: 3730
			private readonly int token;

			// Token: 0x04000E93 RID: 3731
			private readonly int max;

			// Token: 0x04000E94 RID: 3732
			private int index;
		}
	}
}
