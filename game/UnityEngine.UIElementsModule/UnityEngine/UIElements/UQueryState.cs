using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D1 RID: 209
	public struct UQueryState<T> : IEnumerable<T>, IEnumerable, IEquatable<UQueryState<T>> where T : VisualElement
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x000193CC File Offset: 0x000175CC
		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000193E0 File Offset: 0x000175E0
		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00019400 File Offset: 0x00017600
		private T Single(UQuery.SingleQueryMatcher matcher)
		{
			bool flag = matcher.IsInUse();
			if (flag)
			{
				matcher = matcher.CreateNew();
			}
			matcher.Run(this.m_Element, this.m_Matchers);
			T result = matcher.match as T;
			matcher.match = null;
			return result;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00019453 File Offset: 0x00017653
		public T First()
		{
			return this.Single(UQuery.FirstQueryMatcher.Instance);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00019460 File Offset: 0x00017660
		public T Last()
		{
			return this.Single(UQuery.LastQueryMatcher.Instance);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001946D File Offset: 0x0001766D
		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000194A0 File Offset: 0x000176A0
		public List<T> ToList()
		{
			List<T> list = new List<T>();
			this.ToList(list);
			return list;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000194C4 File Offset: 0x000176C4
		public T AtIndex(int index)
		{
			UQuery.IndexQueryMatcher instance = UQuery.IndexQueryMatcher.Instance;
			instance.matchIndex = index;
			return this.Single(instance);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000194EC File Offset: 0x000176EC
		public void ForEach(Action<T> funcCall)
		{
			UQueryState<T>.ActionQueryMatcher actionQueryMatcher = UQueryState<T>.s_Action;
			bool flag = actionQueryMatcher.callBack != null;
			if (flag)
			{
				actionQueryMatcher = new UQueryState<T>.ActionQueryMatcher();
			}
			try
			{
				actionQueryMatcher.callBack = funcCall;
				actionQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				actionQueryMatcher.callBack = null;
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00019550 File Offset: 0x00017750
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			UQueryState<T>.DelegateQueryMatcher<T2> delegateQueryMatcher = UQueryState<T>.DelegateQueryMatcher<T2>.s_Instance;
			bool flag = delegateQueryMatcher.callBack != null;
			if (flag)
			{
				delegateQueryMatcher = new UQueryState<T>.DelegateQueryMatcher<T2>();
			}
			try
			{
				delegateQueryMatcher.callBack = funcCall;
				delegateQueryMatcher.result = result;
				delegateQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				delegateQueryMatcher.callBack = null;
				delegateQueryMatcher.result = null;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000195C4 File Offset: 0x000177C4
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			List<T2> result = new List<T2>();
			this.ForEach<T2>(result, funcCall);
			return result;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000195E6 File Offset: 0x000177E6
		public UQueryState<T>.Enumerator GetEnumerator()
		{
			return new UQueryState<T>.Enumerator(this);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000195F3 File Offset: 0x000177F3
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000195F3 File Offset: 0x000177F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00019600 File Offset: 0x00017800
		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001963C File Offset: 0x0001783C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00019670 File Offset: 0x00017870
		public override int GetHashCode()
		{
			int num = 488160421;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000196BC File Offset: 0x000178BC
		public static bool operator ==(UQueryState<T> state1, UQueryState<T> state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000196D8 File Offset: 0x000178D8
		public static bool operator !=(UQueryState<T> state1, UQueryState<T> state2)
		{
			return !(state1 == state2);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000196F4 File Offset: 0x000178F4
		// Note: this type is marked as 'beforefieldinit'.
		static UQueryState()
		{
		}

		// Token: 0x040002AB RID: 683
		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		// Token: 0x040002AC RID: 684
		private readonly VisualElement m_Element;

		// Token: 0x040002AD RID: 685
		internal readonly List<RuleMatcher> m_Matchers;

		// Token: 0x040002AE RID: 686
		private static readonly UQueryState<T>.ListQueryMatcher<T> s_List = new UQueryState<T>.ListQueryMatcher<T>();

		// Token: 0x040002AF RID: 687
		private static readonly UQueryState<T>.ListQueryMatcher<VisualElement> s_EnumerationList = new UQueryState<T>.ListQueryMatcher<VisualElement>();

		// Token: 0x020000D2 RID: 210
		private class ListQueryMatcher<TElement> : UQuery.UQueryMatcher where TElement : VisualElement
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00019714 File Offset: 0x00017914
			// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001971C File Offset: 0x0001791C
			public List<TElement> matches
			{
				[CompilerGenerated]
				get
				{
					return this.<matches>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<matches>k__BackingField = value;
				}
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x00019728 File Offset: 0x00017928
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as TElement);
				return false;
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x00019752 File Offset: 0x00017952
			public void Reset()
			{
				this.matches = null;
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x000192A3 File Offset: 0x000174A3
			public ListQueryMatcher()
			{
			}

			// Token: 0x040002B0 RID: 688
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private List<TElement> <matches>k__BackingField;
		}

		// Token: 0x020000D3 RID: 211
		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001975D File Offset: 0x0001795D
			// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00019765 File Offset: 0x00017965
			internal Action<T> callBack
			{
				[CompilerGenerated]
				get
				{
					return this.<callBack>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<callBack>k__BackingField = value;
				}
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x00019770 File Offset: 0x00017970
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.callBack(t);
				}
				return false;
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x000192A3 File Offset: 0x000174A3
			public ActionQueryMatcher()
			{
			}

			// Token: 0x040002B1 RID: 689
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private Action<T> <callBack>k__BackingField;
		}

		// Token: 0x020000D4 RID: 212
		private class DelegateQueryMatcher<TReturnType> : UQuery.UQueryMatcher
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060006EB RID: 1771 RVA: 0x000197AB File Offset: 0x000179AB
			// (set) Token: 0x060006EC RID: 1772 RVA: 0x000197B3 File Offset: 0x000179B3
			public Func<T, TReturnType> callBack
			{
				[CompilerGenerated]
				get
				{
					return this.<callBack>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<callBack>k__BackingField = value;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060006ED RID: 1773 RVA: 0x000197BC File Offset: 0x000179BC
			// (set) Token: 0x060006EE RID: 1774 RVA: 0x000197C4 File Offset: 0x000179C4
			public List<TReturnType> result
			{
				[CompilerGenerated]
				get
				{
					return this.<result>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<result>k__BackingField = value;
				}
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x000197D0 File Offset: 0x000179D0
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.result.Add(this.callBack(t));
				}
				return false;
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x000192A3 File Offset: 0x000174A3
			public DelegateQueryMatcher()
			{
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00019816 File Offset: 0x00017A16
			// Note: this type is marked as 'beforefieldinit'.
			static DelegateQueryMatcher()
			{
			}

			// Token: 0x040002B2 RID: 690
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private Func<T, TReturnType> <callBack>k__BackingField;

			// Token: 0x040002B3 RID: 691
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<TReturnType> <result>k__BackingField;

			// Token: 0x040002B4 RID: 692
			public static UQueryState<T>.DelegateQueryMatcher<TReturnType> s_Instance = new UQueryState<T>.DelegateQueryMatcher<TReturnType>();
		}

		// Token: 0x020000D5 RID: 213
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060006F2 RID: 1778 RVA: 0x00019824 File Offset: 0x00017A24
			internal Enumerator(UQueryState<T> queryState)
			{
				this.iterationList = VisualElementListPool.Get(0);
				UQueryState<T>.s_EnumerationList.matches = this.iterationList;
				UQueryState<T>.s_EnumerationList.Run(queryState.m_Element, queryState.m_Matchers);
				UQueryState<T>.s_EnumerationList.Reset();
				this.currentIndex = -1;
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00019878 File Offset: 0x00017A78
			public T Current
			{
				get
				{
					return (T)((object)this.iterationList[this.currentIndex]);
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00019890 File Offset: 0x00017A90
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x000198A0 File Offset: 0x00017AA0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.iterationList.Count;
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x000198D0 File Offset: 0x00017AD0
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x000198DA File Offset: 0x00017ADA
			public void Dispose()
			{
				VisualElementListPool.Release(this.iterationList);
				this.iterationList = null;
			}

			// Token: 0x040002B5 RID: 693
			private List<VisualElement> iterationList;

			// Token: 0x040002B6 RID: 694
			private int currentIndex;
		}
	}
}
