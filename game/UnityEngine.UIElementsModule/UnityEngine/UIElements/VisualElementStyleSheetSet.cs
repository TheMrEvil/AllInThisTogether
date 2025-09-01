using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F2 RID: 242
	public struct VisualElementStyleSheetSet : IEquatable<VisualElementStyleSheetSet>
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		internal VisualElementStyleSheetSet(VisualElement element)
		{
			this.m_Element = element;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BB00 File Offset: 0x00019D00
		public void Add(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList == null;
			if (flag2)
			{
				this.m_Element.styleSheetList = new List<StyleSheet>();
			}
			else
			{
				bool flag3 = this.m_Element.styleSheetList.Contains(styleSheet);
				if (flag3)
				{
					return;
				}
			}
			this.m_Element.styleSheetList.Add(styleSheet);
			this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001BB84 File Offset: 0x00019D84
		public void Clear()
		{
			bool flag = this.m_Element.styleSheetList == null;
			if (!flag)
			{
				this.m_Element.styleSheetList = null;
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001BBC0 File Offset: 0x00019DC0
		public bool Remove(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null && this.m_Element.styleSheetList.Remove(styleSheet);
			bool result;
			if (flag2)
			{
				bool flag3 = this.m_Element.styleSheetList.Count == 0;
				if (flag3)
				{
					this.m_Element.styleSheetList = null;
				}
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001BC48 File Offset: 0x00019E48
		internal void Swap(StyleSheet old, StyleSheet @new)
		{
			bool flag = old == null;
			if (flag)
			{
				throw new ArgumentNullException("old");
			}
			bool flag2 = @new == null;
			if (flag2)
			{
				throw new ArgumentNullException("new");
			}
			bool flag3 = this.m_Element.styleSheetList == null;
			if (!flag3)
			{
				int num = this.m_Element.styleSheetList.IndexOf(old);
				bool flag4 = num >= 0;
				if (flag4)
				{
					this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
					this.m_Element.styleSheetList[num] = @new;
				}
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001BCDC File Offset: 0x00019EDC
		public bool Contains(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null;
			return flag2 && this.m_Element.styleSheetList.Contains(styleSheet);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001BD2C File Offset: 0x00019F2C
		public int count
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_Element.styleSheetList.Count;
				}
				return result;
			}
		}

		// Token: 0x1700018A RID: 394
		public StyleSheet this[int index]
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Element.styleSheetList[index];
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public bool Equals(VisualElementStyleSheetSet other)
		{
			return object.Equals(this.m_Element, other.m_Element);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001BDC8 File Offset: 0x00019FC8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualElementStyleSheetSet && this.Equals((VisualElementStyleSheetSet)obj);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001BE00 File Offset: 0x0001A000
		public override int GetHashCode()
		{
			return (this.m_Element != null) ? this.m_Element.GetHashCode() : 0;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001BE28 File Offset: 0x0001A028
		public static bool operator ==(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001BE44 File Offset: 0x0001A044
		public static bool operator !=(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400030A RID: 778
		private readonly VisualElement m_Element;
	}
}
