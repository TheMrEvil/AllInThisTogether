using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000288 RID: 648
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0005E6D8 File Offset: 0x0005C8D8
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x0005E6FB File Offset: 0x0005C8FB
		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0005E70C File Offset: 0x0005C90C
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x0005E724 File Offset: 0x0005C924
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

		// Token: 0x06001541 RID: 5441 RVA: 0x0005E72E File Offset: 0x0005C92E
		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005E73A File Offset: 0x0005C93A
		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005E746 File Offset: 0x0005C946
		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005E758 File Offset: 0x0005C958
		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0005E78C File Offset: 0x0005C98C
		public static bool operator !=(StyleFont lhs, StyleFont rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0005E7C0 File Offset: 0x0005C9C0
		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005E7D8 File Offset: 0x0005C9D8
		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005E7F8 File Offset: 0x0005C9F8
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleFont)
			{
				StyleFont other = (StyleFont)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005E824 File Offset: 0x0005CA24
		public override int GetHashCode()
		{
			return ((this.m_Value != null) ? this.m_Value.GetHashCode() : 0) * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005E860 File Offset: 0x0005CA60
		public override string ToString()
		{
			return this.DebugString<Font>();
		}

		// Token: 0x04000950 RID: 2384
		private Font m_Value;

		// Token: 0x04000951 RID: 2385
		private StyleKeyword m_Keyword;
	}
}
