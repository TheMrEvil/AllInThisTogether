using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		[RequiredByNativeCode]
		public GUIScrollGroup()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000EE04 File Offset: 0x0000D004
		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			bool flag = this.allowHorizontalScroll;
			if (flag)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			bool flag2 = this.allowHorizontalScroll;
			if (flag2)
			{
				bool flag3 = this.minWidth > 32f;
				if (flag3)
				{
					this.minWidth = 32f;
				}
				bool flag4 = minWidth != 0f;
				if (flag4)
				{
					this.minWidth = minWidth;
				}
				bool flag5 = maxWidth != 0f;
				if (flag5)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public override void SetHorizontal(float x, float width)
		{
			float num = this.needsVerticalScrollbar ? (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left) : width;
			bool flag = this.allowHorizontalScroll && num < this.calcMinWidth;
			if (flag)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				bool flag2 = this.allowHorizontalScroll;
				if (flag2)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			bool flag = this.allowVerticalScroll;
			if (flag)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			bool flag2 = this.needsHorizontalScrollbar;
			if (flag2)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			bool flag3 = this.allowVerticalScroll;
			if (flag3)
			{
				bool flag4 = this.minHeight > 32f;
				if (flag4)
				{
					this.minHeight = 32f;
				}
				bool flag5 = minHeight != 0f;
				if (flag5)
				{
					this.minHeight = minHeight;
				}
				bool flag6 = maxHeight != 0f;
				if (flag6)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public override void SetVertical(float y, float height)
		{
			float num = height;
			bool flag = this.needsHorizontalScrollbar;
			if (flag)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			bool flag2 = this.allowVerticalScroll && num < this.calcMinHeight;
			if (flag2)
			{
				bool flag3 = !this.needsHorizontalScrollbar && !this.needsVerticalScrollbar;
				if (flag3)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					bool flag4 = this.clientWidth < this.calcMinWidth;
					if (flag4)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				bool flag5 = this.allowVerticalScroll;
				if (flag5)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}

		// Token: 0x04000119 RID: 281
		public float calcMinWidth;

		// Token: 0x0400011A RID: 282
		public float calcMaxWidth;

		// Token: 0x0400011B RID: 283
		public float calcMinHeight;

		// Token: 0x0400011C RID: 284
		public float calcMaxHeight;

		// Token: 0x0400011D RID: 285
		public float clientWidth;

		// Token: 0x0400011E RID: 286
		public float clientHeight;

		// Token: 0x0400011F RID: 287
		public bool allowHorizontalScroll = true;

		// Token: 0x04000120 RID: 288
		public bool allowVerticalScroll = true;

		// Token: 0x04000121 RID: 289
		public bool needsHorizontalScrollbar;

		// Token: 0x04000122 RID: 290
		public bool needsVerticalScrollbar;

		// Token: 0x04000123 RID: 291
		public GUIStyle horizontalScrollbar;

		// Token: 0x04000124 RID: 292
		public GUIStyle verticalScrollbar;
	}
}
