using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000290 RID: 656
	public struct StyleTextShadow : IStyleValue<TextShadow>, IEquatable<StyleTextShadow>
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005F60C File Offset: 0x0005D80C
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x0005F637 File Offset: 0x0005D837
		public TextShadow value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TextShadow);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0005F648 File Offset: 0x0005D848
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x0005F660 File Offset: 0x0005D860
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

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005F66A File Offset: 0x0005D86A
		public StyleTextShadow(TextShadow v)
		{
			this = new StyleTextShadow(v, StyleKeyword.Undefined);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0005F678 File Offset: 0x0005D878
		public StyleTextShadow(StyleKeyword keyword)
		{
			this = new StyleTextShadow(default(TextShadow), keyword);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005F697 File Offset: 0x0005D897
		internal StyleTextShadow(TextShadow v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0005F6A8 File Offset: 0x0005D8A8
		public static bool operator ==(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0005F6DC File Offset: 0x0005D8DC
		public static bool operator !=(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		public static implicit operator StyleTextShadow(StyleKeyword keyword)
		{
			return new StyleTextShadow(keyword);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0005F710 File Offset: 0x0005D910
		public static implicit operator StyleTextShadow(TextShadow v)
		{
			return new StyleTextShadow(v);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0005F728 File Offset: 0x0005D928
		public bool Equals(StyleTextShadow other)
		{
			return other == this;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0005F748 File Offset: 0x0005D948
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleTextShadow);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				StyleTextShadow lhs = (StyleTextShadow)obj;
				result = (lhs == this);
			}
			return result;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0005F784 File Offset: 0x0005D984
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0005F7D4 File Offset: 0x0005D9D4
		public override string ToString()
		{
			return this.DebugString<TextShadow>();
		}

		// Token: 0x04000960 RID: 2400
		private StyleKeyword m_Keyword;

		// Token: 0x04000961 RID: 2401
		private TextShadow m_Value;
	}
}
