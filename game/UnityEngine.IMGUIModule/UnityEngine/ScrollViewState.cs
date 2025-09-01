using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	internal class ScrollViewState
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000F2EF File Offset: 0x0000D4EF
		[RequiredByNativeCode]
		public ScrollViewState()
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
		public void ScrollTo(Rect pos)
		{
			this.ScrollTowards(pos, float.PositiveInfinity);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F30C File Offset: 0x0000D50C
		public bool ScrollTowards(Rect pos, float maxDelta)
		{
			Vector2 b = this.ScrollNeeded(pos);
			bool flag = b.sqrMagnitude < 0.0001f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = maxDelta == 0f;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = b.magnitude > maxDelta;
					if (flag3)
					{
						b = b.normalized * maxDelta;
					}
					this.scrollPosition += b;
					this.apply = true;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F384 File Offset: 0x0000D584
		private Vector2 ScrollNeeded(Rect pos)
		{
			Rect rect = this.visibleRect;
			rect.x += this.scrollPosition.x;
			rect.y += this.scrollPosition.y;
			float num = pos.width - this.visibleRect.width;
			bool flag = num > 0f;
			if (flag)
			{
				pos.width -= num;
				pos.x += num * 0.5f;
			}
			num = pos.height - this.visibleRect.height;
			bool flag2 = num > 0f;
			if (flag2)
			{
				pos.height -= num;
				pos.y += num * 0.5f;
			}
			Vector2 zero = Vector2.zero;
			bool flag3 = pos.xMax > rect.xMax;
			if (flag3)
			{
				zero.x += pos.xMax - rect.xMax;
			}
			else
			{
				bool flag4 = pos.xMin < rect.xMin;
				if (flag4)
				{
					zero.x -= rect.xMin - pos.xMin;
				}
			}
			bool flag5 = pos.yMax > rect.yMax;
			if (flag5)
			{
				zero.y += pos.yMax - rect.yMax;
			}
			else
			{
				bool flag6 = pos.yMin < rect.yMin;
				if (flag6)
				{
					zero.y -= rect.yMin - pos.yMin;
				}
			}
			Rect rect2 = this.viewRect;
			rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
			rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
			zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
			zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
			return zero;
		}

		// Token: 0x04000126 RID: 294
		public Rect position;

		// Token: 0x04000127 RID: 295
		public Rect visibleRect;

		// Token: 0x04000128 RID: 296
		public Rect viewRect;

		// Token: 0x04000129 RID: 297
		public Vector2 scrollPosition;

		// Token: 0x0400012A RID: 298
		public bool apply;

		// Token: 0x0400012B RID: 299
		public bool isDuringTouchScroll;

		// Token: 0x0400012C RID: 300
		public Vector2 touchScrollStartMousePosition;

		// Token: 0x0400012D RID: 301
		public Vector2 touchScrollStartPosition;

		// Token: 0x0400012E RID: 302
		public Vector2 velocity;

		// Token: 0x0400012F RID: 303
		public float previousTimeSinceStartup;
	}
}
