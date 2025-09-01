using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029D RID: 669
	internal struct LayoutData : IStyleDataGroup<LayoutData>, IEquatable<LayoutData>
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x000604D8 File Offset: 0x0005E6D8
		public LayoutData Copy()
		{
			return this;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000604F0 File Offset: 0x0005E6F0
		public void CopyFrom(ref LayoutData other)
		{
			this = other;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00060500 File Offset: 0x0005E700
		public static bool operator ==(LayoutData lhs, LayoutData rhs)
		{
			return lhs.alignContent == rhs.alignContent && lhs.alignItems == rhs.alignItems && lhs.alignSelf == rhs.alignSelf && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.display == rhs.display && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection == rhs.flexDirection && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap == rhs.flexWrap && lhs.height == rhs.height && lhs.justifyContent == rhs.justifyContent && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position == rhs.position && lhs.right == rhs.right && lhs.top == rhs.top && lhs.width == rhs.width;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00060790 File Offset: 0x0005E990
		public static bool operator !=(LayoutData lhs, LayoutData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000607AC File Offset: 0x0005E9AC
		public bool Equals(LayoutData other)
		{
			return other == this;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000607CC File Offset: 0x0005E9CC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LayoutData && this.Equals((LayoutData)obj);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00060804 File Offset: 0x0005EA04
		public override int GetHashCode()
		{
			int num = (int)this.alignContent;
			num = (num * 397 ^ (int)this.alignItems);
			num = (num * 397 ^ (int)this.alignSelf);
			num = (num * 397 ^ this.borderBottomWidth.GetHashCode());
			num = (num * 397 ^ this.borderLeftWidth.GetHashCode());
			num = (num * 397 ^ this.borderRightWidth.GetHashCode());
			num = (num * 397 ^ this.borderTopWidth.GetHashCode());
			num = (num * 397 ^ this.bottom.GetHashCode());
			num = (num * 397 ^ (int)this.display);
			num = (num * 397 ^ this.flexBasis.GetHashCode());
			num = (num * 397 ^ (int)this.flexDirection);
			num = (num * 397 ^ this.flexGrow.GetHashCode());
			num = (num * 397 ^ this.flexShrink.GetHashCode());
			num = (num * 397 ^ (int)this.flexWrap);
			num = (num * 397 ^ this.height.GetHashCode());
			num = (num * 397 ^ (int)this.justifyContent);
			num = (num * 397 ^ this.left.GetHashCode());
			num = (num * 397 ^ this.marginBottom.GetHashCode());
			num = (num * 397 ^ this.marginLeft.GetHashCode());
			num = (num * 397 ^ this.marginRight.GetHashCode());
			num = (num * 397 ^ this.marginTop.GetHashCode());
			num = (num * 397 ^ this.maxHeight.GetHashCode());
			num = (num * 397 ^ this.maxWidth.GetHashCode());
			num = (num * 397 ^ this.minHeight.GetHashCode());
			num = (num * 397 ^ this.minWidth.GetHashCode());
			num = (num * 397 ^ this.paddingBottom.GetHashCode());
			num = (num * 397 ^ this.paddingLeft.GetHashCode());
			num = (num * 397 ^ this.paddingRight.GetHashCode());
			num = (num * 397 ^ this.paddingTop.GetHashCode());
			num = (num * 397 ^ (int)this.position);
			num = (num * 397 ^ this.right.GetHashCode());
			num = (num * 397 ^ this.top.GetHashCode());
			return num * 397 ^ this.width.GetHashCode();
		}

		// Token: 0x04000984 RID: 2436
		public Align alignContent;

		// Token: 0x04000985 RID: 2437
		public Align alignItems;

		// Token: 0x04000986 RID: 2438
		public Align alignSelf;

		// Token: 0x04000987 RID: 2439
		public float borderBottomWidth;

		// Token: 0x04000988 RID: 2440
		public float borderLeftWidth;

		// Token: 0x04000989 RID: 2441
		public float borderRightWidth;

		// Token: 0x0400098A RID: 2442
		public float borderTopWidth;

		// Token: 0x0400098B RID: 2443
		public Length bottom;

		// Token: 0x0400098C RID: 2444
		public DisplayStyle display;

		// Token: 0x0400098D RID: 2445
		public Length flexBasis;

		// Token: 0x0400098E RID: 2446
		public FlexDirection flexDirection;

		// Token: 0x0400098F RID: 2447
		public float flexGrow;

		// Token: 0x04000990 RID: 2448
		public float flexShrink;

		// Token: 0x04000991 RID: 2449
		public Wrap flexWrap;

		// Token: 0x04000992 RID: 2450
		public Length height;

		// Token: 0x04000993 RID: 2451
		public Justify justifyContent;

		// Token: 0x04000994 RID: 2452
		public Length left;

		// Token: 0x04000995 RID: 2453
		public Length marginBottom;

		// Token: 0x04000996 RID: 2454
		public Length marginLeft;

		// Token: 0x04000997 RID: 2455
		public Length marginRight;

		// Token: 0x04000998 RID: 2456
		public Length marginTop;

		// Token: 0x04000999 RID: 2457
		public Length maxHeight;

		// Token: 0x0400099A RID: 2458
		public Length maxWidth;

		// Token: 0x0400099B RID: 2459
		public Length minHeight;

		// Token: 0x0400099C RID: 2460
		public Length minWidth;

		// Token: 0x0400099D RID: 2461
		public Length paddingBottom;

		// Token: 0x0400099E RID: 2462
		public Length paddingLeft;

		// Token: 0x0400099F RID: 2463
		public Length paddingRight;

		// Token: 0x040009A0 RID: 2464
		public Length paddingTop;

		// Token: 0x040009A1 RID: 2465
		public Position position;

		// Token: 0x040009A2 RID: 2466
		public Length right;

		// Token: 0x040009A3 RID: 2467
		public Length top;

		// Token: 0x040009A4 RID: 2468
		public Length width;
	}
}
