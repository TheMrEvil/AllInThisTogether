using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000287 RID: 647
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0005E534 File Offset: 0x0005C734
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0005E55B File Offset: 0x0005C75B
		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0005E56C File Offset: 0x0005C76C
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0005E584 File Offset: 0x0005C784
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

		// Token: 0x06001532 RID: 5426 RVA: 0x0005E58E File Offset: 0x0005C78E
		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005E59A File Offset: 0x0005C79A
		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0005E5AA File Offset: 0x0005C7AA
		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005E5BC File Offset: 0x0005C7BC
		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0005E5F0 File Offset: 0x0005C7F0
		public static bool operator !=(StyleFloat lhs, StyleFloat rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0005E60C File Offset: 0x0005C80C
		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005E624 File Offset: 0x0005C824
		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005E63C File Offset: 0x0005C83C
		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005E65C File Offset: 0x0005C85C
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleFloat)
			{
				StyleFloat other = (StyleFloat)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005E688 File Offset: 0x0005C888
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005E6B4 File Offset: 0x0005C8B4
		public override string ToString()
		{
			return this.DebugString<float>();
		}

		// Token: 0x0400094E RID: 2382
		private float m_Value;

		// Token: 0x0400094F RID: 2383
		private StyleKeyword m_Keyword;
	}
}
