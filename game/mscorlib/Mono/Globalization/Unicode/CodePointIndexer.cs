using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200006A RID: 106
	internal class CodePointIndexer
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005A48 File Offset: 0x00003C48
		public static Array CompressArray(Array source, Type type, CodePointIndexer indexer)
		{
			int num = 0;
			for (int i = 0; i < indexer.ranges.Length; i++)
			{
				num += indexer.ranges[i].Count;
			}
			Array array = Array.CreateInstance(type, num);
			for (int j = 0; j < indexer.ranges.Length; j++)
			{
				Array.Copy(source, indexer.ranges[j].Start, array, indexer.ranges[j].IndexStart, indexer.ranges[j].Count);
			}
			return array;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i == 0) ? 0 : (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count));
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005B8C File Offset: 0x00003D8C
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005C08 File Offset: 0x00003E08
		public int ToCodePoint(int i)
		{
			for (int j = 0; j < this.ranges.Length; j++)
			{
				if (i < this.ranges[j].IndexStart)
				{
					return this.defaultCP;
				}
				if (i < this.ranges[j].IndexEnd)
				{
					return i - this.ranges[j].IndexStart + this.ranges[j].Start;
				}
			}
			return this.defaultCP;
		}

		// Token: 0x04000E28 RID: 3624
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x04000E29 RID: 3625
		public readonly int TotalCount;

		// Token: 0x04000E2A RID: 3626
		private int defaultIndex;

		// Token: 0x04000E2B RID: 3627
		private int defaultCP;

		// Token: 0x0200006B RID: 107
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x06000186 RID: 390 RVA: 0x00005C83 File Offset: 0x00003E83
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x04000E2C RID: 3628
			public readonly int Start;

			// Token: 0x04000E2D RID: 3629
			public readonly int End;

			// Token: 0x04000E2E RID: 3630
			public readonly int Count;

			// Token: 0x04000E2F RID: 3631
			public readonly int IndexStart;

			// Token: 0x04000E30 RID: 3632
			public readonly int IndexEnd;
		}
	}
}
