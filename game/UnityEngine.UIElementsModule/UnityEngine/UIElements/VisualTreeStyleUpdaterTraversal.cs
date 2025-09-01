using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FB RID: 251
	internal class VisualTreeStyleUpdaterTraversal : HierarchyTraversal
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001C856 File Offset: 0x0001AA56
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0001C85E File Offset: 0x0001AA5E
		private float currentPixelsPerPoint
		{
			[CompilerGenerated]
			get
			{
				return this.<currentPixelsPerPoint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<currentPixelsPerPoint>k__BackingField = value;
			}
		} = 1f;

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001C867 File Offset: 0x0001AA67
		public StyleMatchingContext styleMatchingContext
		{
			get
			{
				return this.m_StyleMatchingContext;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001C86F File Offset: 0x0001AA6F
		public void PrepareTraversal(float pixelsPerPoint)
		{
			this.currentPixelsPerPoint = pixelsPerPoint;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001C87C File Offset: 0x0001AA7C
		public void AddChangedElement(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_UpdateList.Add(ve);
			bool flag = (versionChangeType & VersionChangeType.StyleSheet) == VersionChangeType.StyleSheet;
			if (flag)
			{
				this.PropagateToChildren(ve);
			}
			this.PropagateToParents(ve);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001C8B3 File Offset: 0x0001AAB3
		public void Clear()
		{
			this.m_UpdateList.Clear();
			this.m_ParentList.Clear();
			this.m_TempMatchResults.Clear();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001C8DC File Offset: 0x0001AADC
		private void PropagateToChildren(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = this.m_UpdateList.Add(visualElement);
				bool flag2 = flag;
				if (flag2)
				{
					this.PropagateToChildren(visualElement);
				}
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001C93C File Offset: 0x0001AB3C
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(parent);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001C98A File Offset: 0x0001AB8A
		private static void OnProcessMatchResult(VisualElement current, MatchResultInfo info)
		{
			current.triggerPseudoMask |= info.triggerPseudoMask;
			current.dependencyPseudoMask |= info.dependencyPseudoMask;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public override void TraverseRecursive(VisualElement element, int depth)
		{
			bool flag = this.ShouldSkipElement(element);
			if (!flag)
			{
				bool flag2 = this.m_UpdateList.Contains(element);
				bool flag3 = flag2;
				if (flag3)
				{
					element.triggerPseudoMask = (PseudoStates)0;
					element.dependencyPseudoMask = (PseudoStates)0;
				}
				int styleSheetCount = this.m_StyleMatchingContext.styleSheetCount;
				bool flag4 = element.styleSheetList != null;
				if (flag4)
				{
					for (int i = 0; i < element.styleSheetList.Count; i++)
					{
						StyleSheet styleSheet = element.styleSheetList[i];
						bool flag5 = styleSheet.flattenedRecursiveImports != null;
						if (flag5)
						{
							for (int j = 0; j < styleSheet.flattenedRecursiveImports.Count; j++)
							{
								this.m_StyleMatchingContext.AddStyleSheet(styleSheet.flattenedRecursiveImports[j]);
							}
						}
						this.m_StyleMatchingContext.AddStyleSheet(styleSheet);
					}
				}
				StyleVariableContext variableContext = this.m_StyleMatchingContext.variableContext;
				int customPropertiesCount = element.computedStyle.customPropertiesCount;
				bool flag6 = flag2;
				if (flag6)
				{
					this.m_StyleMatchingContext.currentElement = element;
					StyleSelectorHelper.FindMatches(this.m_StyleMatchingContext, this.m_TempMatchResults, styleSheetCount - 1);
					ComputedStyle computedStyle = this.ProcessMatchedRules(element, this.m_TempMatchResults);
					computedStyle.Acquire();
					bool hasInlineStyle = element.hasInlineStyle;
					if (hasInlineStyle)
					{
						element.inlineStyleAccess.ApplyInlineStyles(ref computedStyle);
					}
					ComputedTransitionUtils.UpdateComputedTransitions(ref computedStyle);
					bool flag7 = element.hasRunningAnimations && !ComputedTransitionUtils.SameTransitionProperty(element.computedStyle, ref computedStyle);
					if (flag7)
					{
						this.CancelAnimationsWithNoTransitionProperty(element, ref computedStyle);
					}
					bool flag8 = computedStyle.hasTransition && element.styleInitialized;
					if (flag8)
					{
						this.ProcessTransitions(element, element.computedStyle, ref computedStyle);
						element.SetComputedStyle(ref computedStyle);
						this.ForceUpdateTransitions(element);
					}
					else
					{
						element.SetComputedStyle(ref computedStyle);
					}
					computedStyle.Release();
					element.styleInitialized = true;
					element.inheritedStylesHash = element.computedStyle.inheritedData.GetHashCode();
					this.m_StyleMatchingContext.currentElement = null;
					this.m_TempMatchResults.Clear();
				}
				else
				{
					this.m_StyleMatchingContext.variableContext = element.variableContext;
				}
				bool flag9 = flag2 && (customPropertiesCount > 0 || element.computedStyle.customPropertiesCount > 0);
				if (flag9)
				{
					using (CustomStyleResolvedEvent pooled = EventBase<CustomStyleResolvedEvent>.GetPooled())
					{
						pooled.target = element;
						element.SendEvent(pooled);
					}
				}
				base.Recurse(element, depth);
				this.m_StyleMatchingContext.variableContext = variableContext;
				bool flag10 = this.m_StyleMatchingContext.styleSheetCount > styleSheetCount;
				if (flag10)
				{
					this.m_StyleMatchingContext.RemoveStyleSheetRange(styleSheetCount, this.m_StyleMatchingContext.styleSheetCount - styleSheetCount);
				}
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001CC98 File Offset: 0x0001AE98
		private void ProcessTransitions(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle)
		{
			for (int i = newStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty computedTransitionProperty = newStyle.computedTransitions[i];
				bool flag = element.hasInlineStyle && element.inlineStyleAccess.IsValueSet(computedTransitionProperty.id);
				if (!flag)
				{
					ComputedStyle.StartAnimation(element, computedTransitionProperty.id, ref oldStyle, ref newStyle, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001CD14 File Offset: 0x0001AF14
		private void ForceUpdateTransitions(VisualElement element)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			bool flag = this.m_AnimatedProperties.Count > 0;
			if (flag)
			{
				foreach (StylePropertyId id in this.m_AnimatedProperties)
				{
					element.styleAnimation.UpdateAnimation(id);
				}
				this.m_AnimatedProperties.Clear();
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		internal void CancelAnimationsWithNoTransitionProperty(VisualElement element, ref ComputedStyle newStyle)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			foreach (StylePropertyId id in this.m_AnimatedProperties)
			{
				bool flag = !ref newStyle.HasTransitionProperty(id);
				if (flag)
				{
					element.styleAnimation.CancelAnimation(id);
				}
			}
			this.m_AnimatedProperties.Clear();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001CE30 File Offset: 0x0001B030
		protected bool ShouldSkipElement(VisualElement element)
		{
			return !this.m_ParentList.Contains(element) && !this.m_UpdateList.Contains(element);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001CE64 File Offset: 0x0001B064
		private ComputedStyle ProcessMatchedRules(VisualElement element, List<SelectorMatchRecord> matchingSelectors)
		{
			matchingSelectors.Sort((SelectorMatchRecord a, SelectorMatchRecord b) => SelectorMatchRecord.Compare(a, b));
			long num = (long)element.fullTypeName.GetHashCode();
			num = (num * 397L ^ (long)this.currentPixelsPerPoint.GetHashCode());
			int variableHash = this.m_StyleMatchingContext.variableContext.GetVariableHash();
			int num2 = 0;
			foreach (SelectorMatchRecord selectorMatchRecord in matchingSelectors)
			{
				num2 += selectorMatchRecord.complexSelector.rule.customPropertiesCount;
			}
			bool flag = num2 > 0;
			if (flag)
			{
				this.m_ProcessVarContext.AddInitialRange(this.m_StyleMatchingContext.variableContext);
			}
			foreach (SelectorMatchRecord selectorMatchRecord2 in matchingSelectors)
			{
				StyleSheet sheet = selectorMatchRecord2.sheet;
				StyleRule rule = selectorMatchRecord2.complexSelector.rule;
				int specificity = selectorMatchRecord2.complexSelector.specificity;
				num = (num * 397L ^ (long)sheet.contentHash);
				num = (num * 397L ^ (long)rule.GetHashCode());
				num = (num * 397L ^ (long)specificity);
				bool flag2 = rule.customPropertiesCount > 0;
				if (flag2)
				{
					this.ProcessMatchedVariables(selectorMatchRecord2.sheet, rule);
				}
			}
			VisualElement parent = element.hierarchy.parent;
			int num3 = (parent != null) ? parent.inheritedStylesHash : 0;
			num = (num * 397L ^ (long)num3);
			int num4 = variableHash;
			bool flag3 = num2 > 0;
			if (flag3)
			{
				num4 = this.m_ProcessVarContext.GetVariableHash();
			}
			num = (num * 397L ^ (long)num4);
			bool flag4 = variableHash != num4;
			if (flag4)
			{
				StyleVariableContext styleVariableContext;
				bool flag5 = !StyleCache.TryGetValue(num4, out styleVariableContext);
				if (flag5)
				{
					styleVariableContext = new StyleVariableContext(this.m_ProcessVarContext);
					StyleCache.SetValue(num4, styleVariableContext);
				}
				this.m_StyleMatchingContext.variableContext = styleVariableContext;
			}
			element.variableContext = this.m_StyleMatchingContext.variableContext;
			this.m_ProcessVarContext.Clear();
			ComputedStyle result;
			bool flag6 = !StyleCache.TryGetValue(num, out result);
			if (flag6)
			{
				ComputedStyle ptr;
				if (parent != null)
				{
					ref ComputedStyle computedStyle = ref parent.computedStyle;
					ptr = parent.computedStyle;
				}
				else
				{
					ptr = InitialStyle.Get();
				}
				ref ComputedStyle parentStyle = ref ptr;
				result = ComputedStyle.Create(ref parentStyle);
				result.matchingRulesHash = num;
				float scaledPixelsPerPoint = element.scaledPixelsPerPoint;
				foreach (SelectorMatchRecord selectorMatchRecord3 in matchingSelectors)
				{
					this.m_StylePropertyReader.SetContext(selectorMatchRecord3.sheet, selectorMatchRecord3.complexSelector, this.m_StyleMatchingContext.variableContext, scaledPixelsPerPoint);
					result.ApplyProperties(this.m_StylePropertyReader, ref parentStyle);
				}
				result.FinalizeApply(ref parentStyle);
				StyleCache.SetValue(num, ref result);
			}
			return result;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001D194 File Offset: 0x0001B394
		private void ProcessMatchedVariables(StyleSheet sheet, StyleRule rule)
		{
			foreach (StyleProperty styleProperty in rule.properties)
			{
				bool isCustomProperty = styleProperty.isCustomProperty;
				if (isCustomProperty)
				{
					StyleVariable sv = new StyleVariable(styleProperty.name, sheet, styleProperty.values);
					this.m_ProcessVarContext.Add(sv);
				}
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001D1EC File Offset: 0x0001B3EC
		public VisualTreeStyleUpdaterTraversal()
		{
		}

		// Token: 0x04000330 RID: 816
		private StyleVariableContext m_ProcessVarContext = new StyleVariableContext();

		// Token: 0x04000331 RID: 817
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04000332 RID: 818
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04000333 RID: 819
		private List<SelectorMatchRecord> m_TempMatchResults = new List<SelectorMatchRecord>();

		// Token: 0x04000334 RID: 820
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <currentPixelsPerPoint>k__BackingField;

		// Token: 0x04000335 RID: 821
		private StyleMatchingContext m_StyleMatchingContext = new StyleMatchingContext(new Action<VisualElement, MatchResultInfo>(VisualTreeStyleUpdaterTraversal.OnProcessMatchResult));

		// Token: 0x04000336 RID: 822
		private StylePropertyReader m_StylePropertyReader = new StylePropertyReader();

		// Token: 0x04000337 RID: 823
		private readonly List<StylePropertyId> m_AnimatedProperties = new List<StylePropertyId>();

		// Token: 0x020000FC RID: 252
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007D4 RID: 2004 RVA: 0x0001D264 File Offset: 0x0001B464
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x0001D270 File Offset: 0x0001B470
			internal int <ProcessMatchedRules>b__24_0(SelectorMatchRecord a, SelectorMatchRecord b)
			{
				return SelectorMatchRecord.Compare(a, b);
			}

			// Token: 0x04000338 RID: 824
			public static readonly VisualTreeStyleUpdaterTraversal.<>c <>9 = new VisualTreeStyleUpdaterTraversal.<>c();

			// Token: 0x04000339 RID: 825
			public static Comparison<SelectorMatchRecord> <>9__24_0;
		}
	}
}
