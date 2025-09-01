using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000331 RID: 817
	internal struct BitmapAllocator32
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x00072ED0 File Offset: 0x000710D0
		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00072F1C File Offset: 0x0007111C
		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00072F94 File Offset: 0x00071194
		public BMPAlloc Allocate(BaseShaderInfoStorage storage)
		{
			int count = this.m_Pages.Count;
			for (int i = 0; i < count; i++)
			{
				BitmapAllocator32.Page page = this.m_Pages[i];
				bool flag = page.freeSlots == 0;
				if (!flag)
				{
					int j = i * this.m_PageHeight;
					int num = j + this.m_PageHeight;
					while (j < num)
					{
						uint num2 = this.m_AllocMap[j];
						bool flag2 = num2 == 0U;
						if (!flag2)
						{
							byte b = BitmapAllocator32.CountTrailingZeroes(num2);
							this.m_AllocMap[j] = (num2 & ~(1U << (int)b));
							page.freeSlots--;
							this.m_Pages[i] = page;
							return new BMPAlloc
							{
								page = i,
								pageLine = (ushort)(j - i * this.m_PageHeight),
								bitIndex = b,
								ownedState = OwnedState.Owned
							};
						}
						j++;
					}
				}
			}
			RectInt rectInt;
			bool flag3 = storage == null || !storage.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out rectInt);
			if (flag3)
			{
				return BMPAlloc.Invalid;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294U);
			for (int k = 1; k < this.m_PageHeight; k++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)rectInt.xMin,
				y = (ushort)rectInt.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			return new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000731A8 File Offset: 0x000713A8
		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int index = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[index] = (this.m_AllocMap[index] | 1U << (int)alloc.bitIndex);
			BitmapAllocator32.Page value = this.m_Pages[alloc.page];
			value.freeSlots++;
			this.m_Pages[alloc.page] = value;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00073230 File Offset: 0x00071430
		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00073248 File Offset: 0x00071448
		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00073260 File Offset: 0x00071460
		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page page2 = this.m_Pages[page];
			x = page2.x;
			y = page2.y;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0007328C File Offset: 0x0007148C
		private static byte CountTrailingZeroes(uint val)
		{
			byte b = 0;
			bool flag = (val & 65535U) == 0U;
			if (flag)
			{
				val >>= 16;
				b = 16;
			}
			bool flag2 = (val & 255U) == 0U;
			if (flag2)
			{
				val >>= 8;
				b += 8;
			}
			bool flag3 = (val & 15U) == 0U;
			if (flag3)
			{
				val >>= 4;
				b += 4;
			}
			bool flag4 = (val & 3U) == 0U;
			if (flag4)
			{
				val >>= 2;
				b += 2;
			}
			bool flag5 = (val & 1U) == 0U;
			if (flag5)
			{
				b += 1;
			}
			return b;
		}

		// Token: 0x04000C39 RID: 3129
		public const int kPageWidth = 32;

		// Token: 0x04000C3A RID: 3130
		private int m_PageHeight;

		// Token: 0x04000C3B RID: 3131
		private List<BitmapAllocator32.Page> m_Pages;

		// Token: 0x04000C3C RID: 3132
		private List<uint> m_AllocMap;

		// Token: 0x04000C3D RID: 3133
		private int m_EntryWidth;

		// Token: 0x04000C3E RID: 3134
		private int m_EntryHeight;

		// Token: 0x02000332 RID: 818
		private struct Page
		{
			// Token: 0x04000C3F RID: 3135
			public ushort x;

			// Token: 0x04000C40 RID: 3136
			public ushort y;

			// Token: 0x04000C41 RID: 3137
			public int freeSlots;
		}
	}
}
