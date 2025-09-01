using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000324 RID: 804
	internal struct TextCoreSettings : IEquatable<TextCoreSettings>
	{
		// Token: 0x06001A44 RID: 6724 RVA: 0x00071CD8 File Offset: 0x0006FED8
		public override bool Equals(object obj)
		{
			return obj is TextCoreSettings && this.Equals((TextCoreSettings)obj);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00071D04 File Offset: 0x0006FF04
		public bool Equals(TextCoreSettings other)
		{
			return other.faceColor == this.faceColor && other.outlineColor == this.outlineColor && other.outlineWidth == this.outlineWidth && other.underlayColor == this.underlayColor && other.underlayOffset == this.underlayOffset && other.underlaySoftness == this.underlaySoftness;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00071D84 File Offset: 0x0006FF84
		public override int GetHashCode()
		{
			int num = 75905159;
			num = num * -1521134295 + this.faceColor.GetHashCode();
			num = num * -1521134295 + this.outlineColor.GetHashCode();
			num = num * -1521134295 + this.outlineWidth.GetHashCode();
			num = num * -1521134295 + this.underlayColor.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.x.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.y.GetHashCode();
			return num * -1521134295 + this.underlaySoftness.GetHashCode();
		}

		// Token: 0x04000BFA RID: 3066
		public Color faceColor;

		// Token: 0x04000BFB RID: 3067
		public Color outlineColor;

		// Token: 0x04000BFC RID: 3068
		public float outlineWidth;

		// Token: 0x04000BFD RID: 3069
		public Color underlayColor;

		// Token: 0x04000BFE RID: 3070
		public Vector2 underlayOffset;

		// Token: 0x04000BFF RID: 3071
		public float underlaySoftness;
	}
}
