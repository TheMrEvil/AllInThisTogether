using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000282 RID: 642
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0005AAC8 File Offset: 0x00058CC8
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0005AAF3 File Offset: 0x00058CF3
		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0005AB04 File Offset: 0x00058D04
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0005AB1C File Offset: 0x00058D1C
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

		// Token: 0x060014EC RID: 5356 RVA: 0x0005AB26 File Offset: 0x00058D26
		public StyleCursor(Cursor v)
		{
			this = new StyleCursor(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0005AB34 File Offset: 0x00058D34
		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0005AB53 File Offset: 0x00058D53
		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005AB64 File Offset: 0x00058D64
		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0005AB98 File Offset: 0x00058D98
		public static bool operator !=(StyleCursor lhs, StyleCursor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0005ABCC File Offset: 0x00058DCC
		public static implicit operator StyleCursor(Cursor v)
		{
			return new StyleCursor(v);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005ABE4 File Offset: 0x00058DE4
		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005AC04 File Offset: 0x00058E04
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleCursor)
			{
				StyleCursor other = (StyleCursor)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005AC30 File Offset: 0x00058E30
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0005AC64 File Offset: 0x00058E64
		public override string ToString()
		{
			return this.DebugString<Cursor>();
		}

		// Token: 0x04000941 RID: 2369
		private Cursor m_Value;

		// Token: 0x04000942 RID: 2370
		private StyleKeyword m_Keyword;
	}
}
