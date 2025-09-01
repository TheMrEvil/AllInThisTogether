using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029E RID: 670
	internal struct RareData : IStyleDataGroup<RareData>, IEquatable<RareData>
	{
		// Token: 0x06001707 RID: 5895 RVA: 0x00060AF0 File Offset: 0x0005ECF0
		public RareData Copy()
		{
			return this;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00060B08 File Offset: 0x0005ED08
		public void CopyFrom(ref RareData other)
		{
			this = other;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00060B18 File Offset: 0x0005ED18
		public static bool operator ==(RareData lhs, RareData rhs)
		{
			return lhs.cursor == rhs.cursor && lhs.textOverflow == rhs.textOverflow && lhs.unityBackgroundImageTintColor == rhs.unityBackgroundImageTintColor && lhs.unityBackgroundScaleMode == rhs.unityBackgroundScaleMode && lhs.unityOverflowClipBox == rhs.unityOverflowClipBox && lhs.unitySliceBottom == rhs.unitySliceBottom && lhs.unitySliceLeft == rhs.unitySliceLeft && lhs.unitySliceRight == rhs.unitySliceRight && lhs.unitySliceTop == rhs.unitySliceTop && lhs.unityTextOverflowPosition == rhs.unityTextOverflowPosition;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00060BC8 File Offset: 0x0005EDC8
		public static bool operator !=(RareData lhs, RareData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00060BE4 File Offset: 0x0005EDE4
		public bool Equals(RareData other)
		{
			return other == this;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00060C04 File Offset: 0x0005EE04
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RareData && this.Equals((RareData)obj);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00060C3C File Offset: 0x0005EE3C
		public override int GetHashCode()
		{
			int num = this.cursor.GetHashCode();
			num = (num * 397 ^ (int)this.textOverflow);
			num = (num * 397 ^ this.unityBackgroundImageTintColor.GetHashCode());
			num = (num * 397 ^ (int)this.unityBackgroundScaleMode);
			num = (num * 397 ^ (int)this.unityOverflowClipBox);
			num = (num * 397 ^ this.unitySliceBottom);
			num = (num * 397 ^ this.unitySliceLeft);
			num = (num * 397 ^ this.unitySliceRight);
			num = (num * 397 ^ this.unitySliceTop);
			return num * 397 ^ (int)this.unityTextOverflowPosition;
		}

		// Token: 0x040009A5 RID: 2469
		public Cursor cursor;

		// Token: 0x040009A6 RID: 2470
		public TextOverflow textOverflow;

		// Token: 0x040009A7 RID: 2471
		public Color unityBackgroundImageTintColor;

		// Token: 0x040009A8 RID: 2472
		public ScaleMode unityBackgroundScaleMode;

		// Token: 0x040009A9 RID: 2473
		public OverflowClipBox unityOverflowClipBox;

		// Token: 0x040009AA RID: 2474
		public int unitySliceBottom;

		// Token: 0x040009AB RID: 2475
		public int unitySliceLeft;

		// Token: 0x040009AC RID: 2476
		public int unitySliceRight;

		// Token: 0x040009AD RID: 2477
		public int unitySliceTop;

		// Token: 0x040009AE RID: 2478
		public TextOverflowPosition unityTextOverflowPosition;
	}
}
