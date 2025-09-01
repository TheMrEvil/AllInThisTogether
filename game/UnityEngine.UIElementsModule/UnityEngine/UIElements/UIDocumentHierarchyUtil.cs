using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000253 RID: 595
	internal static class UIDocumentHierarchyUtil
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x000474EC File Offset: 0x000456EC
		internal static int FindHierarchicalSortedIndex(SortedDictionary<UIDocumentHierarchicalIndex, UIDocument> children, UIDocument child)
		{
			int num = 0;
			foreach (UIDocument uidocument in children.Values)
			{
				bool flag = uidocument == child;
				if (flag)
				{
					return num;
				}
				bool flag2 = uidocument.rootVisualElement != null && uidocument.rootVisualElement.parent != null;
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004757C File Offset: 0x0004577C
		internal static void SetHierarchicalIndex(Transform childTransform, Transform directParentTransform, Transform mainParentTransform, out UIDocumentHierarchicalIndex hierarchicalIndex)
		{
			bool flag = mainParentTransform == null || childTransform == null;
			if (flag)
			{
				hierarchicalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == mainParentTransform;
				if (flag2)
				{
					hierarchicalIndex.pathToParent = new int[]
					{
						childTransform.GetSiblingIndex()
					};
				}
				else
				{
					List<int> list = new List<int>();
					while (mainParentTransform != childTransform && childTransform != null)
					{
						list.Add(childTransform.GetSiblingIndex());
						childTransform = childTransform.parent;
					}
					list.Reverse();
					hierarchicalIndex.pathToParent = list.ToArray();
				}
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004761C File Offset: 0x0004581C
		internal static void SetGlobalIndex(Transform objectTransform, Transform directParentTransform, out UIDocumentHierarchicalIndex globalIndex)
		{
			bool flag = objectTransform == null;
			if (flag)
			{
				globalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == null;
				if (flag2)
				{
					globalIndex.pathToParent = new int[]
					{
						objectTransform.GetSiblingIndex()
					};
				}
				else
				{
					List<int> list = new List<int>
					{
						objectTransform.GetSiblingIndex()
					};
					while (directParentTransform != null)
					{
						list.Add(directParentTransform.GetSiblingIndex());
						directParentTransform = directParentTransform.parent;
					}
					list.Reverse();
					globalIndex.pathToParent = list.ToArray();
				}
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000476AF File Offset: 0x000458AF
		// Note: this type is marked as 'beforefieldinit'.
		static UIDocumentHierarchyUtil()
		{
		}

		// Token: 0x0400081B RID: 2075
		internal static UIDocumentHierarchicalIndexComparer indexComparer = new UIDocumentHierarchicalIndexComparer();
	}
}
