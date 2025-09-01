using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D8 RID: 216
	public static class UQueryExtensions
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001A41C File Offset: 0x0001861C
		public static T Q<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			return e.Query(name, classes).Build().First();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001A448 File Offset: 0x00018648
		public static VisualElement Q(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes).Build().First();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001A474 File Offset: 0x00018674
		public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = typeof(T) == typeof(VisualElement);
			T result;
			if (flag)
			{
				result = (e.Q(name, className) as T);
			}
			else
			{
				bool flag2 = name == null;
				if (flag2)
				{
					bool flag3 = className == null;
					if (flag3)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						result = (uqueryState.First() as T);
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
						result = (uqueryState.First() as T);
					}
				}
				else
				{
					bool flag4 = className == null;
					if (flag4)
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						result = (uqueryState.First() as T);
					}
					else
					{
						UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementTypeAndNameAndClassQuery.RebuildOn(e);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[2] = StyleSelectorPart.CreateClass(className);
						result = (uqueryState.First() as T);
					}
				}
			}
			return result;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001A6BC File Offset: 0x000188BC
		internal static T MandatoryQ<T>(this VisualElement e, string name, string className = null) where T : VisualElement
		{
			T t = e.Q(name, className);
			bool flag = t == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return t;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public static VisualElement Q(this VisualElement e, string name = null, string className = null)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = name == null;
			VisualElement result;
			if (flag2)
			{
				bool flag3 = className == null;
				if (flag3)
				{
					result = UQueryExtensions.SingleElementEmptyQuery.RebuildOn(e).First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateClass(className);
					result = uqueryState.First();
				}
			}
			else
			{
				bool flag4 = className == null;
				if (flag4)
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					result = uqueryState.First();
				}
				else
				{
					UQueryState<VisualElement> uqueryState = UQueryExtensions.SingleElementNameAndClassQuery.RebuildOn(e);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					uqueryState.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
					result = uqueryState.First();
				}
			}
			return result;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001A840 File Offset: 0x00018A40
		internal static VisualElement MandatoryQ(this VisualElement e, string name, string className = null)
		{
			VisualElement visualElement = e.Q(name, className);
			bool flag = visualElement == null;
			if (flag)
			{
				throw new UQueryExtensions.MissingVisualElementException("Element not found: " + name);
			}
			return visualElement;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001A878 File Offset: 0x00018A78
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, params string[] classes)
		{
			return e.Query(name, classes);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001A894 File Offset: 0x00018A94
		public static UQueryBuilder<VisualElement> Query(this VisualElement e, string name = null, string className = null)
		{
			return e.Query(name, className);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, classes);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, className);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001A920 File Offset: 0x00018B20
		public static UQueryBuilder<VisualElement> Query(this VisualElement e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001A94C File Offset: 0x00018B4C
		// Note: this type is marked as 'beforefieldinit'.
		static UQueryExtensions()
		{
		}

		// Token: 0x040002C1 RID: 705
		private static UQueryState<VisualElement> SingleElementEmptyQuery = new UQueryBuilder<VisualElement>(null).Build();

		// Token: 0x040002C2 RID: 706
		private static UQueryState<VisualElement> SingleElementNameQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Build();

		// Token: 0x040002C3 RID: 707
		private static UQueryState<VisualElement> SingleElementClassQuery = new UQueryBuilder<VisualElement>(null).Class(string.Empty).Build();

		// Token: 0x040002C4 RID: 708
		private static UQueryState<VisualElement> SingleElementNameAndClassQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Class(string.Empty).Build();

		// Token: 0x040002C5 RID: 709
		private static UQueryState<VisualElement> SingleElementTypeQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Build();

		// Token: 0x040002C6 RID: 710
		private static UQueryState<VisualElement> SingleElementTypeAndNameQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Build();

		// Token: 0x040002C7 RID: 711
		private static UQueryState<VisualElement> SingleElementTypeAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Class(string.Empty).Build();

		// Token: 0x040002C8 RID: 712
		private static UQueryState<VisualElement> SingleElementTypeAndNameAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Class(string.Empty).Build();

		// Token: 0x020000D9 RID: 217
		private class MissingVisualElementException : Exception
		{
			// Token: 0x0600073D RID: 1853 RVA: 0x0001AA79 File Offset: 0x00018C79
			public MissingVisualElementException()
			{
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0001AA83 File Offset: 0x00018C83
			public MissingVisualElementException(string message) : base(message)
			{
			}
		}
	}
}
