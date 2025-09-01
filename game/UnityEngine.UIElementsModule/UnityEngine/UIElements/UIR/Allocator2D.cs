using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000300 RID: 768
	internal class Allocator2D
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00067C99 File Offset: 0x00065E99
		public Vector2Int minSize
		{
			get
			{
				return this.m_MinSize;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00067CA1 File Offset: 0x00065EA1
		public Vector2Int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00067CA9 File Offset: 0x00065EA9
		public Vector2Int maxAllocSize
		{
			get
			{
				return this.m_MaxAllocSize;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00067CB1 File Offset: 0x00065EB1
		public Allocator2D(int minSize, int maxSize, int rowHeightBias) : this(new Vector2Int(minSize, minSize), new Vector2Int(maxSize, maxSize), rowHeightBias)
		{
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00067CCC File Offset: 0x00065ECC
		public Allocator2D(Vector2Int minSize, Vector2Int maxSize, int rowHeightBias)
		{
			Debug.Assert(minSize.x > 0 && minSize.x <= maxSize.x && minSize.y > 0 && minSize.y <= maxSize.y);
			Debug.Assert(minSize.x == UIRUtility.GetNextPow2(minSize.x) && minSize.y == UIRUtility.GetNextPow2(minSize.y) && maxSize.x == UIRUtility.GetNextPow2(maxSize.x) && maxSize.y == UIRUtility.GetNextPow2(maxSize.y));
			Debug.Assert(rowHeightBias >= 0);
			this.m_MinSize = minSize;
			this.m_MaxSize = maxSize;
			this.m_RowHeightBias = rowHeightBias;
			Allocator2D.BuildAreas(this.m_Areas, minSize, maxSize);
			this.m_MaxAllocSize = Allocator2D.ComputeMaxAllocSize(this.m_Areas, rowHeightBias);
			this.m_Rows = Allocator2D.BuildRowArray(this.m_MaxAllocSize.y, rowHeightBias);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00067DE4 File Offset: 0x00065FE4
		public bool TryAllocate(int width, int height, out Allocator2D.Alloc2D alloc2D)
		{
			bool flag = width < 1 || width > this.m_MaxAllocSize.x || height < 1 || height > this.m_MaxAllocSize.y;
			bool result;
			if (flag)
			{
				alloc2D = default(Allocator2D.Alloc2D);
				result = false;
			}
			else
			{
				int nextPow2Exp = UIRUtility.GetNextPow2Exp(Mathf.Max(height - this.m_RowHeightBias, 1));
				for (Allocator2D.Row row = this.m_Rows[nextPow2Exp]; row != null; row = row.next)
				{
					bool flag2 = row.rect.width >= width;
					if (flag2)
					{
						Alloc alloc = row.allocator.Allocate((uint)width);
						bool flag3 = alloc.size > 0U;
						if (flag3)
						{
							alloc2D = new Allocator2D.Alloc2D(row, alloc, width, height);
							return true;
						}
					}
				}
				int num = (1 << nextPow2Exp) + this.m_RowHeightBias;
				Debug.Assert(num >= height);
				for (int i = 0; i < this.m_Areas.Count; i++)
				{
					Allocator2D.Area area = this.m_Areas[i];
					bool flag4 = area.rect.height >= num && area.rect.width >= width;
					if (flag4)
					{
						Alloc alloc2 = area.allocator.Allocate((uint)num);
						bool flag5 = alloc2.size > 0U;
						if (flag5)
						{
							Allocator2D.Row row = Allocator2D.Row.pool.Get();
							row.alloc = alloc2;
							row.allocator = new BestFitAllocator((uint)area.rect.width);
							row.area = area;
							row.next = this.m_Rows[nextPow2Exp];
							row.rect = new RectInt(area.rect.xMin, area.rect.yMin + (int)alloc2.start, area.rect.width, num);
							this.m_Rows[nextPow2Exp] = row;
							Alloc alloc3 = row.allocator.Allocate((uint)width);
							Debug.Assert(alloc3.size > 0U);
							alloc2D = new Allocator2D.Alloc2D(row, alloc3, width, height);
							return true;
						}
					}
				}
				alloc2D = default(Allocator2D.Alloc2D);
				result = false;
			}
			return result;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00068020 File Offset: 0x00066220
		public void Free(Allocator2D.Alloc2D alloc2D)
		{
			bool flag = alloc2D.alloc.size == 0U;
			if (!flag)
			{
				Allocator2D.Row row = alloc2D.row;
				row.allocator.Free(alloc2D.alloc);
				bool flag2 = row.allocator.highWatermark == 0U;
				if (flag2)
				{
					row.area.allocator.Free(row.alloc);
					int nextPow2Exp = UIRUtility.GetNextPow2Exp(row.rect.height - this.m_RowHeightBias);
					Allocator2D.Row row2 = this.m_Rows[nextPow2Exp];
					bool flag3 = row2 == row;
					if (flag3)
					{
						this.m_Rows[nextPow2Exp] = row.next;
					}
					else
					{
						Allocator2D.Row row3 = row2;
						while (row3.next != row)
						{
							row3 = row3.next;
						}
						row3.next = row.next;
					}
					Allocator2D.Row.pool.Return(row);
				}
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00068108 File Offset: 0x00066308
		private static void BuildAreas(List<Allocator2D.Area> areas, Vector2Int minSize, Vector2Int maxSize)
		{
			int num = Mathf.Min(minSize.x, minSize.y);
			int num2 = num;
			areas.Add(new Allocator2D.Area(new RectInt(0, 0, num, num2)));
			while (num < maxSize.x || num2 < maxSize.y)
			{
				bool flag = num < maxSize.x;
				if (flag)
				{
					areas.Add(new Allocator2D.Area(new RectInt(num, 0, num, num2)));
					num *= 2;
				}
				bool flag2 = num2 < maxSize.y;
				if (flag2)
				{
					areas.Add(new Allocator2D.Area(new RectInt(0, num2, num, num2)));
					num2 *= 2;
				}
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000681B4 File Offset: 0x000663B4
		private static Vector2Int ComputeMaxAllocSize(List<Allocator2D.Area> areas, int rowHeightBias)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < areas.Count; i++)
			{
				Allocator2D.Area area = areas[i];
				num = Mathf.Max(area.rect.width, num);
				num2 = Mathf.Max(area.rect.height, num2);
			}
			return new Vector2Int(num, UIRUtility.GetPrevPow2(num2 - rowHeightBias) + rowHeightBias);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00068224 File Offset: 0x00066424
		private static Allocator2D.Row[] BuildRowArray(int maxRowHeight, int rowHeightBias)
		{
			int num = UIRUtility.GetNextPow2Exp(maxRowHeight - rowHeightBias) + 1;
			return new Allocator2D.Row[num];
		}

		// Token: 0x04000AF5 RID: 2805
		private readonly Vector2Int m_MinSize;

		// Token: 0x04000AF6 RID: 2806
		private readonly Vector2Int m_MaxSize;

		// Token: 0x04000AF7 RID: 2807
		private readonly Vector2Int m_MaxAllocSize;

		// Token: 0x04000AF8 RID: 2808
		private readonly int m_RowHeightBias;

		// Token: 0x04000AF9 RID: 2809
		private readonly Allocator2D.Row[] m_Rows;

		// Token: 0x04000AFA RID: 2810
		private readonly List<Allocator2D.Area> m_Areas = new List<Allocator2D.Area>();

		// Token: 0x02000301 RID: 769
		public class Area
		{
			// Token: 0x0600197C RID: 6524 RVA: 0x00068247 File Offset: 0x00066447
			public Area(RectInt rect)
			{
				this.rect = rect;
				this.allocator = new BestFitAllocator((uint)rect.height);
			}

			// Token: 0x04000AFB RID: 2811
			public RectInt rect;

			// Token: 0x04000AFC RID: 2812
			public BestFitAllocator allocator;
		}

		// Token: 0x02000302 RID: 770
		public class Row : LinkedPoolItem<Allocator2D.Row>
		{
			// Token: 0x0600197D RID: 6525 RVA: 0x0006826A File Offset: 0x0006646A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static Allocator2D.Row Create()
			{
				return new Allocator2D.Row();
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x00068271 File Offset: 0x00066471
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Reset(Allocator2D.Row row)
			{
				row.rect = default(RectInt);
				row.area = null;
				row.allocator = null;
				row.alloc = default(Alloc);
				row.next = null;
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x000682A1 File Offset: 0x000664A1
			public Row()
			{
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x000682AA File Offset: 0x000664AA
			// Note: this type is marked as 'beforefieldinit'.
			static Row()
			{
			}

			// Token: 0x04000AFD RID: 2813
			public RectInt rect;

			// Token: 0x04000AFE RID: 2814
			public Allocator2D.Area area;

			// Token: 0x04000AFF RID: 2815
			public BestFitAllocator allocator;

			// Token: 0x04000B00 RID: 2816
			public Alloc alloc;

			// Token: 0x04000B01 RID: 2817
			public Allocator2D.Row next;

			// Token: 0x04000B02 RID: 2818
			public static readonly LinkedPool<Allocator2D.Row> pool = new LinkedPool<Allocator2D.Row>(new Func<Allocator2D.Row>(Allocator2D.Row.Create), new Action<Allocator2D.Row>(Allocator2D.Row.Reset), 256);
		}

		// Token: 0x02000303 RID: 771
		public struct Alloc2D
		{
			// Token: 0x06001981 RID: 6529 RVA: 0x000682D3 File Offset: 0x000664D3
			public Alloc2D(Allocator2D.Row row, Alloc alloc, int width, int height)
			{
				this.alloc = alloc;
				this.row = row;
				this.rect = new RectInt(row.rect.xMin + (int)alloc.start, row.rect.yMin, width, height);
			}

			// Token: 0x04000B03 RID: 2819
			public RectInt rect;

			// Token: 0x04000B04 RID: 2820
			public Allocator2D.Row row;

			// Token: 0x04000B05 RID: 2821
			public Alloc alloc;
		}
	}
}
