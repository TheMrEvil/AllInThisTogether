using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TMPro
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class KerningTable
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0001C6C3 File Offset: 0x0001A8C3
		public KerningTable()
		{
			this.kerningPairs = new List<KerningPair>();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001C6D8 File Offset: 0x0001A8D8
		public void AddKerningPair()
		{
			if (this.kerningPairs.Count == 0)
			{
				this.kerningPairs.Add(new KerningPair(0U, 0U, 0f));
				return;
			}
			uint firstGlyph = this.kerningPairs.Last<KerningPair>().firstGlyph;
			uint secondGlyph = this.kerningPairs.Last<KerningPair>().secondGlyph;
			float xOffset = this.kerningPairs.Last<KerningPair>().xOffset;
			this.kerningPairs.Add(new KerningPair(firstGlyph, secondGlyph, xOffset));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001C750 File Offset: 0x0001A950
		public int AddKerningPair(uint first, uint second, float offset)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, second, offset));
				return 0;
			}
			return -1;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public int AddGlyphPairAdjustmentRecord(uint first, GlyphValueRecord_Legacy firstAdjustments, uint second, GlyphValueRecord_Legacy secondAdjustments)
		{
			if (this.kerningPairs.FindIndex((KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				this.kerningPairs.Add(new KerningPair(first, firstAdjustments, second, secondAdjustments));
				return 0;
			}
			return -1;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0001C80C File Offset: 0x0001AA0C
		public void RemoveKerningPair(int left, int right)
		{
			int num = this.kerningPairs.FindIndex((KerningPair item) => (ulong)item.firstGlyph == (ulong)((long)left) && (ulong)item.secondGlyph == (ulong)((long)right));
			if (num != -1)
			{
				this.kerningPairs.RemoveAt(num);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001C855 File Offset: 0x0001AA55
		public void RemoveKerningPair(int index)
		{
			this.kerningPairs.RemoveAt(index);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0001C864 File Offset: 0x0001AA64
		public void SortKerningPairs()
		{
			if (this.kerningPairs.Count > 0)
			{
				this.kerningPairs = (from s in this.kerningPairs
				orderby s.firstGlyph, s.secondGlyph
				select s).ToList<KerningPair>();
			}
		}

		// Token: 0x040001E9 RID: 489
		public List<KerningPair> kerningPairs;

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000608 RID: 1544 RVA: 0x000386EE File Offset: 0x000368EE
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x000386F6 File Offset: 0x000368F6
			internal bool <AddKerningPair>b__0(KerningPair item)
			{
				return item.firstGlyph == this.first && item.secondGlyph == this.second;
			}

			// Token: 0x040005A2 RID: 1442
			public uint first;

			// Token: 0x040005A3 RID: 1443
			public uint second;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600060A RID: 1546 RVA: 0x00038716 File Offset: 0x00036916
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0003871E File Offset: 0x0003691E
			internal bool <AddGlyphPairAdjustmentRecord>b__0(KerningPair item)
			{
				return item.firstGlyph == this.first && item.secondGlyph == this.second;
			}

			// Token: 0x040005A4 RID: 1444
			public uint first;

			// Token: 0x040005A5 RID: 1445
			public uint second;
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600060C RID: 1548 RVA: 0x0003873E File Offset: 0x0003693E
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x00038746 File Offset: 0x00036946
			internal bool <RemoveKerningPair>b__0(KerningPair item)
			{
				return (ulong)item.firstGlyph == (ulong)((long)this.left) && (ulong)item.secondGlyph == (ulong)((long)this.right);
			}

			// Token: 0x040005A6 RID: 1446
			public int left;

			// Token: 0x040005A7 RID: 1447
			public int right;
		}

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600060E RID: 1550 RVA: 0x0003876A File Offset: 0x0003696A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x00038776 File Offset: 0x00036976
			public <>c()
			{
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0003877E File Offset: 0x0003697E
			internal uint <SortKerningPairs>b__7_0(KerningPair s)
			{
				return s.firstGlyph;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00038786 File Offset: 0x00036986
			internal uint <SortKerningPairs>b__7_1(KerningPair s)
			{
				return s.secondGlyph;
			}

			// Token: 0x040005A8 RID: 1448
			public static readonly KerningTable.<>c <>9 = new KerningTable.<>c();

			// Token: 0x040005A9 RID: 1449
			public static Func<KerningPair, uint> <>9__7_0;

			// Token: 0x040005AA RID: 1450
			public static Func<KerningPair, uint> <>9__7_1;
		}
	}
}
