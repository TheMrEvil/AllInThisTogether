using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A1 RID: 673
	internal struct VisualData : IStyleDataGroup<VisualData>, IEquatable<VisualData>
	{
		// Token: 0x0600171C RID: 5916 RVA: 0x000610CC File Offset: 0x0005F2CC
		public VisualData Copy()
		{
			return this;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000610E4 File Offset: 0x0005F2E4
		public void CopyFrom(ref VisualData other)
		{
			this = other;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000610F4 File Offset: 0x0005F2F4
		public static bool operator ==(VisualData lhs, VisualData rhs)
		{
			return lhs.backgroundColor == rhs.backgroundColor && lhs.backgroundImage == rhs.backgroundImage && lhs.borderBottomColor == rhs.borderBottomColor && lhs.borderBottomLeftRadius == rhs.borderBottomLeftRadius && lhs.borderBottomRightRadius == rhs.borderBottomRightRadius && lhs.borderLeftColor == rhs.borderLeftColor && lhs.borderRightColor == rhs.borderRightColor && lhs.borderTopColor == rhs.borderTopColor && lhs.borderTopLeftRadius == rhs.borderTopLeftRadius && lhs.borderTopRightRadius == rhs.borderTopRightRadius && lhs.opacity == rhs.opacity && lhs.overflow == rhs.overflow;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000611F0 File Offset: 0x0005F3F0
		public static bool operator !=(VisualData lhs, VisualData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0006120C File Offset: 0x0005F40C
		public bool Equals(VisualData other)
		{
			return other == this;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0006122C File Offset: 0x0005F42C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualData && this.Equals((VisualData)obj);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00061264 File Offset: 0x0005F464
		public override int GetHashCode()
		{
			int num = this.backgroundColor.GetHashCode();
			num = (num * 397 ^ this.backgroundImage.GetHashCode());
			num = (num * 397 ^ this.borderBottomColor.GetHashCode());
			num = (num * 397 ^ this.borderBottomLeftRadius.GetHashCode());
			num = (num * 397 ^ this.borderBottomRightRadius.GetHashCode());
			num = (num * 397 ^ this.borderLeftColor.GetHashCode());
			num = (num * 397 ^ this.borderRightColor.GetHashCode());
			num = (num * 397 ^ this.borderTopColor.GetHashCode());
			num = (num * 397 ^ this.borderTopLeftRadius.GetHashCode());
			num = (num * 397 ^ this.borderTopRightRadius.GetHashCode());
			num = (num * 397 ^ this.opacity.GetHashCode());
			return num * 397 ^ (int)this.overflow;
		}

		// Token: 0x040009B7 RID: 2487
		public Color backgroundColor;

		// Token: 0x040009B8 RID: 2488
		public Background backgroundImage;

		// Token: 0x040009B9 RID: 2489
		public Color borderBottomColor;

		// Token: 0x040009BA RID: 2490
		public Length borderBottomLeftRadius;

		// Token: 0x040009BB RID: 2491
		public Length borderBottomRightRadius;

		// Token: 0x040009BC RID: 2492
		public Color borderLeftColor;

		// Token: 0x040009BD RID: 2493
		public Color borderRightColor;

		// Token: 0x040009BE RID: 2494
		public Color borderTopColor;

		// Token: 0x040009BF RID: 2495
		public Length borderTopLeftRadius;

		// Token: 0x040009C0 RID: 2496
		public Length borderTopRightRadius;

		// Token: 0x040009C1 RID: 2497
		public float opacity;

		// Token: 0x040009C2 RID: 2498
		public OverflowInternal overflow;
	}
}
