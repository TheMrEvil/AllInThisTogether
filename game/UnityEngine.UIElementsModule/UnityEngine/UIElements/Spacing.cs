using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006D RID: 109
	internal struct Spacing
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B320 File Offset: 0x00009520
		public float horizontal
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B340 File Offset: 0x00009540
		public float vertical
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B35F File Offset: 0x0000955F
		public Spacing(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B380 File Offset: 0x00009580
		public static Rect operator +(Rect r, Spacing a)
		{
			r.x -= a.left;
			r.y -= a.top;
			r.width += a.horizontal;
			r.height += a.vertical;
			return r;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B3EC File Offset: 0x000095EC
		public static Rect operator -(Rect r, Spacing a)
		{
			r.x += a.left;
			r.y += a.top;
			r.width = Mathf.Max(0f, r.width - a.horizontal);
			r.height = Mathf.Max(0f, r.height - a.vertical);
			return r;
		}

		// Token: 0x04000163 RID: 355
		public float left;

		// Token: 0x04000164 RID: 356
		public float top;

		// Token: 0x04000165 RID: 357
		public float right;

		// Token: 0x04000166 RID: 358
		public float bottom;
	}
}
