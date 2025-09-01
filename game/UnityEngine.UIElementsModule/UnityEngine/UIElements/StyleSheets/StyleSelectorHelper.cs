using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000367 RID: 871
	internal static class StyleSelectorHelper
	{
		// Token: 0x06001C2D RID: 7213 RVA: 0x00084CE4 File Offset: 0x00082EE4
		public static MatchResultInfo MatchesSelector(VisualElement element, StyleSelector selector)
		{
			bool flag = true;
			StyleSelectorPart[] parts = selector.parts;
			int num = parts.Length;
			int num2 = 0;
			while (num2 < num && flag)
			{
				switch (parts[num2].type)
				{
				case StyleSelectorType.Wildcard:
					break;
				case StyleSelectorType.Type:
					flag = (element.typeName == parts[num2].value);
					break;
				case StyleSelectorType.Class:
					flag = element.ClassListContains(parts[num2].value);
					break;
				case StyleSelectorType.PseudoClass:
					break;
				case StyleSelectorType.RecursivePseudoClass:
					goto IL_C7;
				case StyleSelectorType.ID:
					flag = (element.name == parts[num2].value);
					break;
				case StyleSelectorType.Predicate:
				{
					UQuery.IVisualPredicateWrapper visualPredicateWrapper = parts[num2].tempData as UQuery.IVisualPredicateWrapper;
					flag = (visualPredicateWrapper != null && visualPredicateWrapper.Predicate(element));
					break;
				}
				default:
					goto IL_C7;
				}
				IL_CB:
				num2++;
				continue;
				IL_C7:
				flag = false;
				goto IL_CB;
			}
			int num3 = 0;
			int num4 = 0;
			bool flag2 = flag;
			bool flag3 = flag2 && selector.pseudoStateMask != 0;
			if (flag3)
			{
				flag = ((selector.pseudoStateMask & (int)element.pseudoStates) == selector.pseudoStateMask);
				bool flag4 = flag;
				if (flag4)
				{
					num4 = selector.pseudoStateMask;
				}
				else
				{
					num3 = selector.pseudoStateMask;
				}
			}
			bool flag5 = flag2 && selector.negatedPseudoStateMask != 0;
			if (flag5)
			{
				flag &= ((selector.negatedPseudoStateMask & (int)(~(int)element.pseudoStates)) == selector.negatedPseudoStateMask);
				bool flag6 = flag;
				if (flag6)
				{
					num3 |= selector.negatedPseudoStateMask;
				}
				else
				{
					num4 |= selector.negatedPseudoStateMask;
				}
			}
			return new MatchResultInfo(flag, (PseudoStates)num3, (PseudoStates)num4);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00084E88 File Offset: 0x00083088
		public static bool MatchRightToLeft(VisualElement element, StyleComplexSelector complexSelector, Action<VisualElement, MatchResultInfo> processResult)
		{
			VisualElement visualElement = element;
			int i = complexSelector.selectors.Length - 1;
			VisualElement visualElement2 = null;
			int num = -1;
			while (i >= 0)
			{
				bool flag = visualElement == null;
				if (flag)
				{
					break;
				}
				MatchResultInfo matchResultInfo = StyleSelectorHelper.MatchesSelector(visualElement, complexSelector.selectors[i]);
				processResult(visualElement, matchResultInfo);
				bool flag2 = !matchResultInfo.success;
				if (flag2)
				{
					bool flag3 = i < complexSelector.selectors.Length - 1 && complexSelector.selectors[i + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag3)
					{
						visualElement = visualElement.parent;
					}
					else
					{
						bool flag4 = visualElement2 != null;
						if (!flag4)
						{
							break;
						}
						visualElement = visualElement2;
						i = num;
					}
				}
				else
				{
					bool flag5 = i < complexSelector.selectors.Length - 1 && complexSelector.selectors[i + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag5)
					{
						visualElement2 = visualElement.parent;
						num = i;
					}
					bool flag6 = --i < 0;
					if (flag6)
					{
						return true;
					}
					visualElement = visualElement.parent;
				}
			}
			return false;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00084F94 File Offset: 0x00083194
		private static void FastLookup(IDictionary<string, StyleComplexSelector> table, List<SelectorMatchRecord> matchedSelectors, StyleMatchingContext context, string input, ref SelectorMatchRecord record)
		{
			StyleComplexSelector nextInTable;
			bool flag = table.TryGetValue(input, out nextInTable);
			if (flag)
			{
				while (nextInTable != null)
				{
					bool flag2 = StyleSelectorHelper.MatchRightToLeft(context.currentElement, nextInTable, context.processResult);
					if (flag2)
					{
						record.complexSelector = nextInTable;
						matchedSelectors.Add(record);
					}
					nextInTable = nextInTable.nextInTable;
				}
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00084FF4 File Offset: 0x000831F4
		public static void FindMatches(StyleMatchingContext context, List<SelectorMatchRecord> matchedSelectors)
		{
			VisualElement currentElement = context.currentElement;
			int num = context.styleSheetCount - 1;
			bool flag = currentElement.styleSheetList != null;
			if (flag)
			{
				int num2 = currentElement.styleSheetList.Count;
				for (int i = 0; i < currentElement.styleSheetList.Count; i++)
				{
					StyleSheet styleSheet = currentElement.styleSheetList[i];
					bool flag2 = styleSheet.flattenedRecursiveImports != null;
					if (flag2)
					{
						num2 += styleSheet.flattenedRecursiveImports.Count;
					}
				}
				num -= num2;
			}
			StyleSelectorHelper.FindMatches(context, matchedSelectors, num);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0008508C File Offset: 0x0008328C
		public static void FindMatches(StyleMatchingContext context, List<SelectorMatchRecord> matchedSelectors, int parentSheetIndex)
		{
			Debug.Assert(matchedSelectors.Count == 0);
			Debug.Assert(context.currentElement != null, "context.currentElement != null");
			VisualElement currentElement = context.currentElement;
			bool flag = false;
			for (int i = 0; i < context.styleSheetCount; i++)
			{
				bool flag2 = !flag && i > parentSheetIndex;
				if (flag2)
				{
					currentElement.pseudoStates |= PseudoStates.Root;
					flag = true;
				}
				StyleSheet styleSheetAt = context.GetStyleSheetAt(i);
				SelectorMatchRecord selectorMatchRecord = new SelectorMatchRecord(styleSheetAt, i);
				StyleSelectorHelper.FastLookup(styleSheetAt.orderedTypeSelectors, matchedSelectors, context, currentElement.typeName, ref selectorMatchRecord);
				StyleSelectorHelper.FastLookup(styleSheetAt.orderedTypeSelectors, matchedSelectors, context, "*", ref selectorMatchRecord);
				bool flag3 = !string.IsNullOrEmpty(currentElement.name);
				if (flag3)
				{
					StyleSelectorHelper.FastLookup(styleSheetAt.orderedNameSelectors, matchedSelectors, context, currentElement.name, ref selectorMatchRecord);
				}
				foreach (string input in currentElement.GetClassesForIteration())
				{
					StyleSelectorHelper.FastLookup(styleSheetAt.orderedClassSelectors, matchedSelectors, context, input, ref selectorMatchRecord);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				currentElement.pseudoStates &= ~PseudoStates.Root;
			}
		}
	}
}
