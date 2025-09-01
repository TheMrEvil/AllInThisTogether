using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BD RID: 189
	public struct TextShadow : IEquatable<TextShadow>
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x000177C4 File Offset: 0x000159C4
		public override bool Equals(object obj)
		{
			return obj is TextShadow && this.Equals((TextShadow)obj);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000177F0 File Offset: 0x000159F0
		public bool Equals(TextShadow other)
		{
			return other.offset == this.offset && other.blurRadius == this.blurRadius && other.color == this.color;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00017838 File Offset: 0x00015A38
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.offset.GetHashCode();
			num = num * -1521134295 + this.blurRadius.GetHashCode();
			return num * -1521134295 + this.color.GetHashCode();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001789C File Offset: 0x00015A9C
		public static bool operator ==(TextShadow style1, TextShadow style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000178B8 File Offset: 0x00015AB8
		public static bool operator !=(TextShadow style1, TextShadow style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000178D4 File Offset: 0x00015AD4
		public override string ToString()
		{
			return string.Format("offset={0}, blurRadius={1}, color={2}", this.offset, this.blurRadius, this.color);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00017914 File Offset: 0x00015B14
		internal static TextShadow LerpUnclamped(TextShadow a, TextShadow b, float t)
		{
			return new TextShadow
			{
				offset = Vector2.LerpUnclamped(a.offset, b.offset, t),
				blurRadius = Mathf.LerpUnclamped(a.blurRadius, b.blurRadius, t),
				color = Color.LerpUnclamped(a.color, b.color, t)
			};
		}

		// Token: 0x0400027E RID: 638
		public Vector2 offset;

		// Token: 0x0400027F RID: 639
		public float blurRadius;

		// Token: 0x04000280 RID: 640
		public Color color;
	}
}
