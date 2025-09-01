using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028F RID: 655
	public struct StyleTranslate : IStyleValue<Translate>, IEquatable<StyleTranslate>
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0005F44C File Offset: 0x0005D64C
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0005F477 File Offset: 0x0005D677
		public Translate value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Translate);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005F488 File Offset: 0x0005D688
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x0005F4A0 File Offset: 0x0005D6A0
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

		// Token: 0x060015B3 RID: 5555 RVA: 0x0005F4AA File Offset: 0x0005D6AA
		public StyleTranslate(Translate v)
		{
			this = new StyleTranslate(v, StyleKeyword.Undefined);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
		public StyleTranslate(StyleKeyword keyword)
		{
			this = new StyleTranslate(default(Translate), keyword);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0005F4D7 File Offset: 0x0005D6D7
		internal StyleTranslate(Translate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0005F4E8 File Offset: 0x0005D6E8
		public static bool operator ==(StyleTranslate lhs, StyleTranslate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0005F51C File Offset: 0x0005D71C
		public static bool operator !=(StyleTranslate lhs, StyleTranslate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0005F538 File Offset: 0x0005D738
		public static implicit operator StyleTranslate(StyleKeyword keyword)
		{
			return new StyleTranslate(keyword);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005F550 File Offset: 0x0005D750
		public static implicit operator StyleTranslate(Translate v)
		{
			return new StyleTranslate(v);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0005F568 File Offset: 0x0005D768
		public bool Equals(StyleTranslate other)
		{
			return other == this;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0005F588 File Offset: 0x0005D788
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleTranslate)
			{
				StyleTranslate other = (StyleTranslate)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		public override string ToString()
		{
			return this.DebugString<Translate>();
		}

		// Token: 0x0400095E RID: 2398
		private Translate m_Value;

		// Token: 0x0400095F RID: 2399
		private StyleKeyword m_Keyword;
	}
}
