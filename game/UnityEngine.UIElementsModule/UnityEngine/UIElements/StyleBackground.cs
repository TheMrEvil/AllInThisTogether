using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000280 RID: 640
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0005A6AC File Offset: 0x000588AC
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0005A6E8 File Offset: 0x000588E8
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x0005A700 File Offset: 0x00058900
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005A70A File Offset: 0x0005890A
		public StyleBackground(Background v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0005A716 File Offset: 0x00058916
		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005A722 File Offset: 0x00058922
		public StyleBackground(Sprite v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005A72E File Offset: 0x0005892E
		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005A73C File Offset: 0x0005893C
		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005A75B File Offset: 0x0005895B
		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005A76C File Offset: 0x0005896C
		internal StyleBackground(Sprite v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromSprite(v), keyword);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005A77D File Offset: 0x0005897D
		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005A78E File Offset: 0x0005898E
		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005A7A0 File Offset: 0x000589A0
		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0005A7D4 File Offset: 0x000589D4
		public static bool operator !=(StyleBackground lhs, StyleBackground rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005A7F0 File Offset: 0x000589F0
		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0005A808 File Offset: 0x00058A08
		public static implicit operator StyleBackground(Background v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0005A820 File Offset: 0x00058A20
		public static implicit operator StyleBackground(Texture2D v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0005A838 File Offset: 0x00058A38
		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0005A858 File Offset: 0x00058A58
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleBackground)
			{
				StyleBackground other = (StyleBackground)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0005A884 File Offset: 0x00058A84
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0005A8B8 File Offset: 0x00058AB8
		public override string ToString()
		{
			return this.DebugString<Background>();
		}

		// Token: 0x0400093D RID: 2365
		private Background m_Value;

		// Token: 0x0400093E RID: 2366
		private StyleKeyword m_Keyword;
	}
}
