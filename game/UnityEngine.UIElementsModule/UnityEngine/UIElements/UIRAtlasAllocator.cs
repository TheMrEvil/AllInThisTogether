using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000256 RID: 598
	internal class UIRAtlasAllocator : IDisposable
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004784A File Offset: 0x00045A4A
		public int maxAtlasSize
		{
			[CompilerGenerated]
			get
			{
				return this.<maxAtlasSize>k__BackingField;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00047852 File Offset: 0x00045A52
		public int maxImageWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageWidth>k__BackingField;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004785A File Offset: 0x00045A5A
		public int maxImageHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<maxImageHeight>k__BackingField;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00047862 File Offset: 0x00045A62
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0004786A File Offset: 0x00045A6A
		public int virtualWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<virtualWidth>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<virtualWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00047873 File Offset: 0x00045A73
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0004787B File Offset: 0x00045A7B
		public int virtualHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<virtualHeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<virtualHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00047884 File Offset: 0x00045A84
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x0004788C File Offset: 0x00045A8C
		public int physicalWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<physicalWidth>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<physicalWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00047895 File Offset: 0x00045A95
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0004789D File Offset: 0x00045A9D
		public int physicalHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<physicalHeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<physicalHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x000478A6 File Offset: 0x00045AA6
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x000478AE File Offset: 0x00045AAE
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000478B7 File Offset: 0x00045AB7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000478CC File Offset: 0x00045ACC
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode next;
					for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = next)
					{
						next = areaNode.next;
						areaNode.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00047964 File Offset: 0x00045B64
		private static int GetLog2OfNextPower(int n)
		{
			float f = (float)Mathf.NextPowerOfTwo(n);
			float f2 = Mathf.Log(f, 2f);
			return Mathf.RoundToInt(f2);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00047990 File Offset: 0x00045B90
		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.maxAtlasSize = maxAtlasSize;
			this.maxImageWidth = maxAtlasSize;
			this.maxImageHeight = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int num = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[num];
			RectInt rect = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(rect);
			this.BuildAreas();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00047A58 File Offset: 0x00045C58
		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool result;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					result = false;
				}
				else
				{
					bool flag = width < 1 || height < 1;
					if (flag)
					{
						result = false;
					}
					else
					{
						bool flag2 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag2)
						{
							result = false;
						}
						else
						{
							int log2OfNextPower = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int rowHeight = (1 << log2OfNextPower) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[log2OfNextPower];
							bool flag3 = row != null && row.width - row.Cursor < width;
							if (flag3)
							{
								row = null;
							}
							bool flag4 = row == null;
							if (flag4)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag5 = this.TryPartitionArea(areaNode, log2OfNextPower, rowHeight, width);
									if (flag5)
									{
										row = this.m_OpenRows[log2OfNextPower];
										break;
									}
								}
								bool flag6 = row == null;
								if (flag6)
								{
									return false;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00047C20 File Offset: 0x00045E20
		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt rect = areaNode.rect;
			bool flag = rect.height < rowHeight || rect.width < minWidth;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag2 = row != null;
				if (flag2)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(rect.x, rect.y, rect.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				rect.y += rowHeight;
				rect.height -= rowHeight;
				bool flag3 = rect.height == 0;
				if (flag3)
				{
					bool flag4 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag4)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = rect;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00047D00 File Offset: 0x00045F00
		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode previous = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt rect;
				if (flag)
				{
					rect = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					rect = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.Acquire(rect);
				areaNode.AddAfter(previous);
				previous = areaNode;
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00047DBC File Offset: 0x00045FBC
		// Note: this type is marked as 'beforefieldinit'.
		static UIRAtlasAllocator()
		{
		}

		// Token: 0x0400081D RID: 2077
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <maxAtlasSize>k__BackingField;

		// Token: 0x0400081E RID: 2078
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <maxImageWidth>k__BackingField;

		// Token: 0x0400081F RID: 2079
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <maxImageHeight>k__BackingField;

		// Token: 0x04000820 RID: 2080
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <virtualWidth>k__BackingField;

		// Token: 0x04000821 RID: 2081
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <virtualHeight>k__BackingField;

		// Token: 0x04000822 RID: 2082
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <physicalWidth>k__BackingField;

		// Token: 0x04000823 RID: 2083
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <physicalHeight>k__BackingField;

		// Token: 0x04000824 RID: 2084
		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		// Token: 0x04000825 RID: 2085
		private UIRAtlasAllocator.Row[] m_OpenRows;

		// Token: 0x04000826 RID: 2086
		private int m_1SidePadding;

		// Token: 0x04000827 RID: 2087
		private int m_2SidePadding;

		// Token: 0x04000828 RID: 2088
		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		// Token: 0x04000829 RID: 2089
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;

		// Token: 0x02000257 RID: 599
		private class Row
		{
			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06001231 RID: 4657 RVA: 0x00047DCD File Offset: 0x00045FCD
			// (set) Token: 0x06001232 RID: 4658 RVA: 0x00047DD5 File Offset: 0x00045FD5
			public int offsetX
			{
				[CompilerGenerated]
				get
				{
					return this.<offsetX>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<offsetX>k__BackingField = value;
				}
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06001233 RID: 4659 RVA: 0x00047DDE File Offset: 0x00045FDE
			// (set) Token: 0x06001234 RID: 4660 RVA: 0x00047DE6 File Offset: 0x00045FE6
			public int offsetY
			{
				[CompilerGenerated]
				get
				{
					return this.<offsetY>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<offsetY>k__BackingField = value;
				}
			}

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001235 RID: 4661 RVA: 0x00047DEF File Offset: 0x00045FEF
			// (set) Token: 0x06001236 RID: 4662 RVA: 0x00047DF7 File Offset: 0x00045FF7
			public int width
			{
				[CompilerGenerated]
				get
				{
					return this.<width>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<width>k__BackingField = value;
				}
			}

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x00047E00 File Offset: 0x00046000
			// (set) Token: 0x06001238 RID: 4664 RVA: 0x00047E08 File Offset: 0x00046008
			public int height
			{
				[CompilerGenerated]
				get
				{
					return this.<height>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<height>k__BackingField = value;
				}
			}

			// Token: 0x06001239 RID: 4665 RVA: 0x00047E14 File Offset: 0x00046014
			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x00047E59 File Offset: 0x00046059
			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}

			// Token: 0x0600123B RID: 4667 RVA: 0x000020C2 File Offset: 0x000002C2
			public Row()
			{
			}

			// Token: 0x0600123C RID: 4668 RVA: 0x00047E8F File Offset: 0x0004608F
			// Note: this type is marked as 'beforefieldinit'.
			static Row()
			{
			}

			// Token: 0x0400082A RID: 2090
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(100);

			// Token: 0x0400082B RID: 2091
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private int <offsetX>k__BackingField;

			// Token: 0x0400082C RID: 2092
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private int <offsetY>k__BackingField;

			// Token: 0x0400082D RID: 2093
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private int <width>k__BackingField;

			// Token: 0x0400082E RID: 2094
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int <height>k__BackingField;

			// Token: 0x0400082F RID: 2095
			public int Cursor;
		}

		// Token: 0x02000258 RID: 600
		private class AreaNode
		{
			// Token: 0x0600123D RID: 4669 RVA: 0x00047EA0 File Offset: 0x000460A0
			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				areaNode.rect = rect;
				areaNode.previous = null;
				areaNode.next = null;
				return areaNode;
			}

			// Token: 0x0600123E RID: 4670 RVA: 0x00047ED3 File Offset: 0x000460D3
			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			// Token: 0x0600123F RID: 4671 RVA: 0x00047EE4 File Offset: 0x000460E4
			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			// Token: 0x06001240 RID: 4672 RVA: 0x00047F3C File Offset: 0x0004613C
			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}

			// Token: 0x06001241 RID: 4673 RVA: 0x000020C2 File Offset: 0x000002C2
			public AreaNode()
			{
			}

			// Token: 0x06001242 RID: 4674 RVA: 0x00047F9F File Offset: 0x0004619F
			// Note: this type is marked as 'beforefieldinit'.
			static AreaNode()
			{
			}

			// Token: 0x04000830 RID: 2096
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(100);

			// Token: 0x04000831 RID: 2097
			public RectInt rect;

			// Token: 0x04000832 RID: 2098
			public UIRAtlasAllocator.AreaNode previous;

			// Token: 0x04000833 RID: 2099
			public UIRAtlasAllocator.AreaNode next;
		}
	}
}
