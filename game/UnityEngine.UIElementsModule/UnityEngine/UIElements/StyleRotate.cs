using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028D RID: 653
	public struct StyleRotate : IStyleValue<Rotate>, IEquatable<StyleRotate>
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0005F0CC File Offset: 0x0005D2CC
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x0005F0F7 File Offset: 0x0005D2F7
		public Rotate value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Rotate);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0005F108 File Offset: 0x0005D308
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x0005F120 File Offset: 0x0005D320
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

		// Token: 0x06001595 RID: 5525 RVA: 0x0005F12A File Offset: 0x0005D32A
		public StyleRotate(Rotate v)
		{
			this = new StyleRotate(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0005F138 File Offset: 0x0005D338
		public StyleRotate(StyleKeyword keyword)
		{
			this = new StyleRotate(default(Rotate), keyword);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0005F157 File Offset: 0x0005D357
		internal StyleRotate(Rotate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0005F168 File Offset: 0x0005D368
		public static bool operator ==(StyleRotate lhs, StyleRotate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0005F19C File Offset: 0x0005D39C
		public static bool operator !=(StyleRotate lhs, StyleRotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0005F1B8 File Offset: 0x0005D3B8
		public static implicit operator StyleRotate(StyleKeyword keyword)
		{
			return new StyleRotate(keyword);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0005F1D0 File Offset: 0x0005D3D0
		public static implicit operator StyleRotate(Rotate v)
		{
			return new StyleRotate(v);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005F1E8 File Offset: 0x0005D3E8
		public bool Equals(StyleRotate other)
		{
			return other == this;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0005F208 File Offset: 0x0005D408
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleRotate)
			{
				StyleRotate other = (StyleRotate)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005F234 File Offset: 0x0005D434
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005F268 File Offset: 0x0005D468
		public override string ToString()
		{
			return this.DebugString<Rotate>();
		}

		// Token: 0x0400095A RID: 2394
		private Rotate m_Value;

		// Token: 0x0400095B RID: 2395
		private StyleKeyword m_Keyword;
	}
}
