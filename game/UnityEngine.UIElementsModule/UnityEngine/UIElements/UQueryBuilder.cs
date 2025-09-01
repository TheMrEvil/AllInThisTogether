using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D6 RID: 214
	public struct UQueryBuilder<T> : IEquatable<UQueryBuilder<T>> where T : VisualElement
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000198F0 File Offset: 0x00017AF0
		private List<StyleSelector> styleSelectors
		{
			get
			{
				List<StyleSelector> result;
				if ((result = this.m_StyleSelectors) == null)
				{
					result = (this.m_StyleSelectors = new List<StyleSelector>());
				}
				return result;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001991C File Offset: 0x00017B1C
		private List<StyleSelectorPart> parts
		{
			get
			{
				List<StyleSelectorPart> result;
				if ((result = this.m_Parts) == null)
				{
					result = (this.m_Parts = new List<StyleSelectorPart>());
				}
				return result;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00019948 File Offset: 0x00017B48
		public UQueryBuilder(VisualElement visualElement)
		{
			this = default(UQueryBuilder<T>);
			this.m_Element = visualElement;
			this.m_Parts = null;
			this.m_StyleSelectors = null;
			this.m_Relationship = StyleSelectorRelationship.None;
			this.m_Matchers = new List<RuleMatcher>();
			this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00019994 File Offset: 0x00017B94
		public UQueryBuilder<T> Class(string classname)
		{
			this.AddClass(classname);
			return this;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000199B4 File Offset: 0x00017BB4
		public UQueryBuilder<T> Name(string id)
		{
			this.AddName(id);
			return this;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000199D4 File Offset: 0x00017BD4
		public UQueryBuilder<T2> Descendents<T2>(string name = null, params string[] classNames) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classNames);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00019A0C File Offset: 0x00017C0C
		public UQueryBuilder<T2> Descendents<T2>(string name = null, string classname = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(classname);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00019A44 File Offset: 0x00017C44
		public UQueryBuilder<T2> Children<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00019A7C File Offset: 0x00017C7C
		public UQueryBuilder<T2> Children<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public UQueryBuilder<T2> OfType<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00019AE4 File Offset: 0x00017CE4
		public UQueryBuilder<T2> OfType<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00019B14 File Offset: 0x00017D14
		internal UQueryBuilder<T> SingleBaseType()
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance));
			return this;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00019B44 File Offset: 0x00017D44
		public UQueryBuilder<T> Where(Func<T, bool> selectorPredicate)
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(new UQuery.PredicateWrapper<T>(selectorPredicate)));
			return this;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00019B74 File Offset: 0x00017D74
		private void AddClass(string c)
		{
			bool flag = c != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateClass(c));
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00019B9C File Offset: 0x00017D9C
		private void AddClasses(params string[] classes)
		{
			bool flag = classes != null;
			if (flag)
			{
				for (int i = 0; i < classes.Length; i++)
				{
					this.AddClass(classes[i]);
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00019BD0 File Offset: 0x00017DD0
		private void AddName(string id)
		{
			bool flag = id != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateId(id));
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00019BF8 File Offset: 0x00017DF8
		private void AddType<T2>() where T2 : VisualElement
		{
			bool flag = typeof(T2) != typeof(VisualElement);
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T2>.s_Instance));
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00019C3C File Offset: 0x00017E3C
		private UQueryBuilder<T> AddPseudoState(PseudoStates s)
		{
			this.pseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00019C64 File Offset: 0x00017E64
		private UQueryBuilder<T> AddNegativePseudoState(PseudoStates s)
		{
			this.negatedPseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00019C8C File Offset: 0x00017E8C
		public UQueryBuilder<T> Active()
		{
			return this.AddPseudoState(PseudoStates.Active);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00019CA8 File Offset: 0x00017EA8
		public UQueryBuilder<T> NotActive()
		{
			return this.AddNegativePseudoState(PseudoStates.Active);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00019CC4 File Offset: 0x00017EC4
		public UQueryBuilder<T> Visible()
		{
			return from e in this
			where e.visible
			select e;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00019CFC File Offset: 0x00017EFC
		public UQueryBuilder<T> NotVisible()
		{
			return from e in this
			where !e.visible
			select e;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00019D34 File Offset: 0x00017F34
		public UQueryBuilder<T> Hovered()
		{
			return this.AddPseudoState(PseudoStates.Hover);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00019D50 File Offset: 0x00017F50
		public UQueryBuilder<T> NotHovered()
		{
			return this.AddNegativePseudoState(PseudoStates.Hover);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00019D6C File Offset: 0x00017F6C
		public UQueryBuilder<T> Checked()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00019D88 File Offset: 0x00017F88
		public UQueryBuilder<T> NotChecked()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00019DA4 File Offset: 0x00017FA4
		[Obsolete("Use Checked() instead")]
		public UQueryBuilder<T> Selected()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00019DC0 File Offset: 0x00017FC0
		[Obsolete("Use NotChecked() instead")]
		public UQueryBuilder<T> NotSelected()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00019DDC File Offset: 0x00017FDC
		public UQueryBuilder<T> Enabled()
		{
			return this.AddNegativePseudoState(PseudoStates.Disabled);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public UQueryBuilder<T> NotEnabled()
		{
			return this.AddPseudoState(PseudoStates.Disabled);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00019E14 File Offset: 0x00018014
		public UQueryBuilder<T> Focused()
		{
			return this.AddPseudoState(PseudoStates.Focus);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00019E30 File Offset: 0x00018030
		public UQueryBuilder<T> NotFocused()
		{
			return this.AddNegativePseudoState(PseudoStates.Focus);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00019E4C File Offset: 0x0001804C
		private UQueryBuilder<T2> AddRelationship<T2>(StyleSelectorRelationship relationship) where T2 : VisualElement
		{
			return new UQueryBuilder<T2>(this.m_Element)
			{
				m_Matchers = this.m_Matchers,
				m_Parts = this.m_Parts,
				m_StyleSelectors = this.m_StyleSelectors,
				m_Relationship = ((relationship == StyleSelectorRelationship.None) ? this.m_Relationship : relationship),
				pseudoStatesMask = this.pseudoStatesMask,
				negatedPseudoStatesMask = this.negatedPseudoStatesMask
			};
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00019EC0 File Offset: 0x000180C0
		private void AddPseudoStatesRuleIfNecessasy()
		{
			bool flag = this.pseudoStatesMask != 0 || this.negatedPseudoStatesMask != 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.PseudoClass
				});
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00019F08 File Offset: 0x00018108
		private void FinishSelector()
		{
			this.FinishCurrentSelector();
			bool flag = this.styleSelectors.Count > 0;
			if (flag)
			{
				StyleComplexSelector styleComplexSelector = new StyleComplexSelector();
				styleComplexSelector.selectors = this.styleSelectors.ToArray();
				this.styleSelectors.Clear();
				this.m_Matchers.Add(new RuleMatcher
				{
					complexSelector = styleComplexSelector
				});
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00019F74 File Offset: 0x00018174
		private bool CurrentSelectorEmpty()
		{
			return this.parts.Count == 0 && this.m_Relationship == StyleSelectorRelationship.None && this.pseudoStatesMask == 0 && this.negatedPseudoStatesMask == 0;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00019FB0 File Offset: 0x000181B0
		private void FinishCurrentSelector()
		{
			bool flag = !this.CurrentSelectorEmpty();
			if (flag)
			{
				StyleSelector styleSelector = new StyleSelector();
				styleSelector.previousRelationship = this.m_Relationship;
				this.AddPseudoStatesRuleIfNecessasy();
				styleSelector.parts = this.m_Parts.ToArray();
				styleSelector.pseudoStateMask = this.pseudoStatesMask;
				styleSelector.negatedPseudoStateMask = this.negatedPseudoStatesMask;
				this.styleSelectors.Add(styleSelector);
				this.m_Parts.Clear();
				this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001A03C File Offset: 0x0001823C
		public UQueryState<T> Build()
		{
			this.FinishSelector();
			bool flag = this.m_Matchers.Count == 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.Wildcard
				});
				this.FinishSelector();
			}
			return new UQueryState<T>(this.m_Element, this.m_Matchers);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001A0A0 File Offset: 0x000182A0
		public static implicit operator T(UQueryBuilder<T> s)
		{
			return s.First();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001A0BC File Offset: 0x000182BC
		public static bool operator ==(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return builder1.Equals(builder2);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001A0D8 File Offset: 0x000182D8
		public static bool operator !=(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return !(builder1 == builder2);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001A0F4 File Offset: 0x000182F4
		public T First()
		{
			return this.Build().First();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001A114 File Offset: 0x00018314
		public T Last()
		{
			return this.Build().Last();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001A134 File Offset: 0x00018334
		public List<T> ToList()
		{
			return this.Build().ToList();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001A154 File Offset: 0x00018354
		public void ToList(List<T> results)
		{
			this.Build().ToList(results);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001A174 File Offset: 0x00018374
		public T AtIndex(int index)
		{
			return this.Build().AtIndex(index);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001A198 File Offset: 0x00018398
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			this.Build().ForEach<T2>(result, funcCall);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001A1B8 File Offset: 0x000183B8
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			return this.Build().ForEach<T2>(funcCall);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001A1DC File Offset: 0x000183DC
		public void ForEach(Action<T> funcCall)
		{
			this.Build().ForEach(funcCall);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001A1FC File Offset: 0x000183FC
		public bool Equals(UQueryBuilder<T> other)
		{
			return EqualityComparer<List<StyleSelector>>.Default.Equals(this.m_StyleSelectors, other.m_StyleSelectors) && EqualityComparer<List<StyleSelector>>.Default.Equals(this.styleSelectors, other.styleSelectors) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.m_Parts, other.m_Parts) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.parts, other.parts) && this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers) && this.m_Relationship == other.m_Relationship && this.pseudoStatesMask == other.pseudoStatesMask && this.negatedPseudoStatesMask == other.negatedPseudoStatesMask;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001A2CC File Offset: 0x000184CC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryBuilder<T>);
			return !flag && this.Equals((UQueryBuilder<T>)obj);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001A300 File Offset: 0x00018500
		public override int GetHashCode()
		{
			int num = -949812380;
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.m_StyleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.styleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.m_Parts);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.parts);
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			num = num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
			num = num * -1521134295 + this.m_Relationship.GetHashCode();
			num = num * -1521134295 + this.pseudoStatesMask.GetHashCode();
			return num * -1521134295 + this.negatedPseudoStatesMask.GetHashCode();
		}

		// Token: 0x040002B7 RID: 695
		private List<StyleSelector> m_StyleSelectors;

		// Token: 0x040002B8 RID: 696
		private List<StyleSelectorPart> m_Parts;

		// Token: 0x040002B9 RID: 697
		private VisualElement m_Element;

		// Token: 0x040002BA RID: 698
		private List<RuleMatcher> m_Matchers;

		// Token: 0x040002BB RID: 699
		private StyleSelectorRelationship m_Relationship;

		// Token: 0x040002BC RID: 700
		private int pseudoStatesMask;

		// Token: 0x040002BD RID: 701
		private int negatedPseudoStatesMask;

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600072D RID: 1837 RVA: 0x0001A3F1 File Offset: 0x000185F1
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x0001A3FD File Offset: 0x000185FD
			internal bool <Visible>b__30_0(T e)
			{
				return e.visible;
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x0001A40A File Offset: 0x0001860A
			internal bool <NotVisible>b__31_0(T e)
			{
				return !e.visible;
			}

			// Token: 0x040002BE RID: 702
			public static readonly UQueryBuilder<T>.<>c <>9 = new UQueryBuilder<T>.<>c();

			// Token: 0x040002BF RID: 703
			public static Func<T, bool> <>9__30_0;

			// Token: 0x040002C0 RID: 704
			public static Func<T, bool> <>9__31_0;
		}
	}
}
