using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028E RID: 654
	public struct StyleScale : IStyleValue<Scale>, IEquatable<StyleScale>
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0005F28C File Offset: 0x0005D48C
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x0005F2B7 File Offset: 0x0005D4B7
		public Scale value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Scale);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0005F2C8 File Offset: 0x0005D4C8
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
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

		// Token: 0x060015A4 RID: 5540 RVA: 0x0005F2EA File Offset: 0x0005D4EA
		public StyleScale(Scale v)
		{
			this = new StyleScale(v, StyleKeyword.Undefined);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
		public StyleScale(StyleKeyword keyword)
		{
			this = new StyleScale(default(Scale), keyword);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0005F317 File Offset: 0x0005D517
		internal StyleScale(Scale v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005F328 File Offset: 0x0005D528
		public static bool operator ==(StyleScale lhs, StyleScale rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005F35C File Offset: 0x0005D55C
		public static bool operator !=(StyleScale lhs, StyleScale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0005F378 File Offset: 0x0005D578
		public static implicit operator StyleScale(StyleKeyword keyword)
		{
			return new StyleScale(keyword);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0005F390 File Offset: 0x0005D590
		public static implicit operator StyleScale(Scale v)
		{
			return new StyleScale(v);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0005F3A8 File Offset: 0x0005D5A8
		public bool Equals(StyleScale other)
		{
			return other == this;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleScale)
			{
				StyleScale other = (StyleScale)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0005F3F4 File Offset: 0x0005D5F4
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0005F428 File Offset: 0x0005D628
		public override string ToString()
		{
			return this.DebugString<Scale>();
		}

		// Token: 0x0400095C RID: 2396
		private Scale m_Value;

		// Token: 0x0400095D RID: 2397
		private StyleKeyword m_Keyword;
	}
}
