using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000291 RID: 657
	public struct StyleTransformOrigin : IStyleValue<TransformOrigin>, IEquatable<StyleTransformOrigin>
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005F7F8 File Offset: 0x0005D9F8
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0005F823 File Offset: 0x0005DA23
		public TransformOrigin value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TransformOrigin);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0005F834 File Offset: 0x0005DA34
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0005F84C File Offset: 0x0005DA4C
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

		// Token: 0x060015D1 RID: 5585 RVA: 0x0005F856 File Offset: 0x0005DA56
		public StyleTransformOrigin(TransformOrigin v)
		{
			this = new StyleTransformOrigin(v, StyleKeyword.Undefined);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0005F864 File Offset: 0x0005DA64
		public StyleTransformOrigin(StyleKeyword keyword)
		{
			this = new StyleTransformOrigin(default(TransformOrigin), keyword);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0005F883 File Offset: 0x0005DA83
		internal StyleTransformOrigin(TransformOrigin v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0005F894 File Offset: 0x0005DA94
		public static bool operator ==(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0005F8C8 File Offset: 0x0005DAC8
		public static bool operator !=(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		public static implicit operator StyleTransformOrigin(StyleKeyword keyword)
		{
			return new StyleTransformOrigin(keyword);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0005F8FC File Offset: 0x0005DAFC
		public static implicit operator StyleTransformOrigin(TransformOrigin v)
		{
			return new StyleTransformOrigin(v);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0005F914 File Offset: 0x0005DB14
		public bool Equals(StyleTransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0005F934 File Offset: 0x0005DB34
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleTransformOrigin)
			{
				StyleTransformOrigin other = (StyleTransformOrigin)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0005F960 File Offset: 0x0005DB60
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0005F994 File Offset: 0x0005DB94
		public override string ToString()
		{
			return this.DebugString<TransformOrigin>();
		}

		// Token: 0x04000962 RID: 2402
		private TransformOrigin m_Value;

		// Token: 0x04000963 RID: 2403
		private StyleKeyword m_Keyword;
	}
}
