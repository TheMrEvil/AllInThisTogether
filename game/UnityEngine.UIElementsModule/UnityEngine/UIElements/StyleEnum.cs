using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x02000286 RID: 646
	public struct StyleEnum<T> : IStyleValue<T>, IEquatable<StyleEnum<T>> where T : struct, IConvertible
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0005E37C File Offset: 0x0005C57C
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x0005E3A7 File Offset: 0x0005C5A7
		public T value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(T);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0005E3B8 File Offset: 0x0005C5B8
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0005E3D0 File Offset: 0x0005C5D0
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

		// Token: 0x06001523 RID: 5411 RVA: 0x0005E3DA File Offset: 0x0005C5DA
		public StyleEnum(T v)
		{
			this = new StyleEnum<T>(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0005E3E8 File Offset: 0x0005C5E8
		public StyleEnum(StyleKeyword keyword)
		{
			this = new StyleEnum<T>(default(T), keyword);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0005E407 File Offset: 0x0005C607
		internal StyleEnum(T v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005E418 File Offset: 0x0005C618
		public static bool operator ==(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && UnsafeUtility.EnumEquals<T>(lhs.m_Value, rhs.m_Value);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005E44C File Offset: 0x0005C64C
		public static bool operator !=(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0005E468 File Offset: 0x0005C668
		public static implicit operator StyleEnum<T>(StyleKeyword keyword)
		{
			return new StyleEnum<T>(keyword);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0005E480 File Offset: 0x0005C680
		public static implicit operator StyleEnum<T>(T v)
		{
			return new StyleEnum<T>(v);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0005E498 File Offset: 0x0005C698
		public bool Equals(StyleEnum<T> other)
		{
			return other == this;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005E4B8 File Offset: 0x0005C6B8
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is StyleEnum<T>)
			{
				StyleEnum<T> other = (StyleEnum<T>)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0005E4E4 File Offset: 0x0005C6E4
		public override int GetHashCode()
		{
			return UnsafeUtility.EnumToInt<T>(this.m_Value) * 397 ^ (int)this.m_Keyword;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0005E510 File Offset: 0x0005C710
		public override string ToString()
		{
			return this.DebugString<T>();
		}

		// Token: 0x0400094C RID: 2380
		private T m_Value;

		// Token: 0x0400094D RID: 2381
		private StyleKeyword m_Keyword;
	}
}
