using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029C RID: 668
	internal struct InheritedData : IStyleDataGroup<InheritedData>, IEquatable<InheritedData>
	{
		// Token: 0x060016F9 RID: 5881 RVA: 0x000601D4 File Offset: 0x0005E3D4
		public InheritedData Copy()
		{
			return this;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000601EC File Offset: 0x0005E3EC
		public void CopyFrom(ref InheritedData other)
		{
			this = other;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000601FC File Offset: 0x0005E3FC
		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.letterSpacing == rhs.letterSpacing && lhs.textShadow == rhs.textShadow && lhs.unityFont == rhs.unityFont && lhs.unityFontDefinition == rhs.unityFontDefinition && lhs.unityFontStyleAndWeight == rhs.unityFontStyleAndWeight && lhs.unityParagraphSpacing == rhs.unityParagraphSpacing && lhs.unityTextAlign == rhs.unityTextAlign && lhs.unityTextOutlineColor == rhs.unityTextOutlineColor && lhs.unityTextOutlineWidth == rhs.unityTextOutlineWidth && lhs.visibility == rhs.visibility && lhs.whiteSpace == rhs.whiteSpace && lhs.wordSpacing == rhs.wordSpacing;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00060310 File Offset: 0x0005E510
		public static bool operator !=(InheritedData lhs, InheritedData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0006032C File Offset: 0x0005E52C
		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0006034C File Offset: 0x0005E54C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00060384 File Offset: 0x0005E584
		public override int GetHashCode()
		{
			int num = this.color.GetHashCode();
			num = (num * 397 ^ this.fontSize.GetHashCode());
			num = (num * 397 ^ this.letterSpacing.GetHashCode());
			num = (num * 397 ^ this.textShadow.GetHashCode());
			num = (num * 397 ^ ((this.unityFont == null) ? 0 : this.unityFont.GetHashCode()));
			num = (num * 397 ^ this.unityFontDefinition.GetHashCode());
			num = (num * 397 ^ (int)this.unityFontStyleAndWeight);
			num = (num * 397 ^ this.unityParagraphSpacing.GetHashCode());
			num = (num * 397 ^ (int)this.unityTextAlign);
			num = (num * 397 ^ this.unityTextOutlineColor.GetHashCode());
			num = (num * 397 ^ this.unityTextOutlineWidth.GetHashCode());
			num = (num * 397 ^ (int)this.visibility);
			num = (num * 397 ^ (int)this.whiteSpace);
			return num * 397 ^ this.wordSpacing.GetHashCode();
		}

		// Token: 0x04000976 RID: 2422
		public Color color;

		// Token: 0x04000977 RID: 2423
		public Length fontSize;

		// Token: 0x04000978 RID: 2424
		public Length letterSpacing;

		// Token: 0x04000979 RID: 2425
		public TextShadow textShadow;

		// Token: 0x0400097A RID: 2426
		public Font unityFont;

		// Token: 0x0400097B RID: 2427
		public FontDefinition unityFontDefinition;

		// Token: 0x0400097C RID: 2428
		public FontStyle unityFontStyleAndWeight;

		// Token: 0x0400097D RID: 2429
		public Length unityParagraphSpacing;

		// Token: 0x0400097E RID: 2430
		public TextAnchor unityTextAlign;

		// Token: 0x0400097F RID: 2431
		public Color unityTextOutlineColor;

		// Token: 0x04000980 RID: 2432
		public float unityTextOutlineWidth;

		// Token: 0x04000981 RID: 2433
		public Visibility visibility;

		// Token: 0x04000982 RID: 2434
		public WhiteSpace whiteSpace;

		// Token: 0x04000983 RID: 2435
		public Length wordSpacing;
	}
}
