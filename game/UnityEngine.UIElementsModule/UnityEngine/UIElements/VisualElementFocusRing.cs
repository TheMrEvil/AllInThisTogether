using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EB RID: 235
	public class VisualElementFocusRing : IFocusRing
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x0001AF16 File Offset: 0x00019116
		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001AF3A File Offset: 0x0001913A
		private FocusController focusController
		{
			get
			{
				return this.root.focusController;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001AF47 File Offset: 0x00019147
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x0001AF4F File Offset: 0x0001914F
		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultFocusOrder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<defaultFocusOrder>k__BackingField = value;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001AF58 File Offset: 0x00019158
		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			int result;
			switch (this.defaultFocusOrder)
			{
			default:
				result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement visualElement = a.m_Focusable as VisualElement;
				VisualElement visualElement2 = b.m_Focusable as VisualElement;
				bool flag = visualElement != null && visualElement2 != null;
				if (flag)
				{
					bool flag2 = visualElement.layout.position.x < visualElement2.layout.position.x;
					if (flag2)
					{
						result = -1;
						break;
					}
					bool flag3 = visualElement.layout.position.x > visualElement2.layout.position.x;
					if (flag3)
					{
						result = 1;
						break;
					}
					bool flag4 = visualElement.layout.position.y < visualElement2.layout.position.y;
					if (flag4)
					{
						result = -1;
						break;
					}
					bool flag5 = visualElement.layout.position.y > visualElement2.layout.position.y;
					if (flag5)
					{
						result = 1;
						break;
					}
				}
				result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement visualElement3 = a.m_Focusable as VisualElement;
				VisualElement visualElement4 = b.m_Focusable as VisualElement;
				bool flag6 = visualElement3 != null && visualElement4 != null;
				if (flag6)
				{
					bool flag7 = visualElement3.layout.position.y < visualElement4.layout.position.y;
					if (flag7)
					{
						result = -1;
						break;
					}
					bool flag8 = visualElement3.layout.position.y > visualElement4.layout.position.y;
					if (flag8)
					{
						result = 1;
						break;
					}
					bool flag9 = visualElement3.layout.position.x < visualElement4.layout.position.x;
					if (flag9)
					{
						result = -1;
						break;
					}
					bool flag10 = visualElement3.layout.position.x > visualElement4.layout.position.x;
					if (flag10)
					{
						result = 1;
						break;
					}
				}
				result = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B204 File Offset: 0x00019404
		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int result;
			if (flag)
			{
				result = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						int num = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = num == 0;
						if (flag4)
						{
							num = this.FocusRingAutoIndexSort(a, b);
						}
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B2B0 File Offset: 0x000194B0
		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> list = new List<VisualElementFocusRing.FocusRingRecord>();
				int num = 0;
				this.BuildRingForScopeRecursive(this.root, ref num, list);
				this.SortAndFlattenScopeLists(list);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001B2FC File Offset: 0x000194FC
		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.parent != null && visualElement == visualElement.parent.contentContainer;
				bool flag2 = visualElement.isCompositeRoot || flag;
				if (flag2)
				{
					VisualElementFocusRing.FocusRingRecord focusRingRecord = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					focusRingRecord.m_AutoIndex = num;
					focusRingRecord.m_Focusable = visualElement;
					focusRingRecord.m_IsSlot = flag;
					focusRingRecord.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord focusRingRecord2 = focusRingRecord;
					scopeList.Add(focusRingRecord2);
					int num2 = 0;
					this.BuildRingForScopeRecursive(visualElement, ref num2, focusRingRecord2.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag3 = visualElement.canGrabFocus && visualElement.isHierarchyDisplayed && visualElement.tabIndex >= 0;
					if (flag3)
					{
						VisualElementFocusRing.FocusRingRecord focusRingRecord3 = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						focusRingRecord3.m_AutoIndex = num;
						focusRingRecord3.m_Focusable = visualElement;
						focusRingRecord3.m_IsSlot = false;
						focusRingRecord3.m_ScopeNavigationOrder = null;
						scopeList.Add(focusRingRecord3);
					}
					this.BuildRingForScopeRecursive(visualElement, ref scopeIndex, scopeList);
				}
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001B428 File Offset: 0x00019628
		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord focusRingRecord in rootScopeList)
				{
					bool flag2 = focusRingRecord.m_Focusable.canGrabFocus && focusRingRecord.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !focusRingRecord.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(focusRingRecord);
						}
						this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = focusRingRecord.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
						}
					}
					focusRingRecord.m_ScopeNavigationOrder = null;
				}
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001B518 File Offset: 0x00019718
		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001B570 File Offset: 0x00019770
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag2)
			{
				Focusable target;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.target as Focusable, out target);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(target);
				}
			}
			bool flag3 = currentFocusable is IMGUIContainer && e.imguiEvent != null;
			FocusChangeDirection result;
			if (flag3)
			{
				result = FocusChangeDirection.none;
			}
			else
			{
				result = VisualElementFocusRing.GetKeyDownFocusChangeDirection(e);
			}
			return result;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001B5FC File Offset: 0x000197FC
		internal static FocusChangeDirection GetKeyDownFocusChangeDirection(EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			if (flag)
			{
				KeyDownEvent keyDownEvent = e as KeyDownEvent;
				bool flag2 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
				if (flag2)
				{
					bool flag3 = keyDownEvent.modifiers == EventModifiers.Shift;
					if (flag3)
					{
						return VisualElementFocusChangeDirection.left;
					}
					bool flag4 = keyDownEvent.modifiers == EventModifiers.None;
					if (flag4)
					{
						return VisualElementFocusChangeDirection.right;
					}
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001B67C File Offset: 0x0001987C
		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable result;
			if (flag)
			{
				result = currentFocusable;
			}
			else
			{
				VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
				bool flag2 = visualElementFocusChangeTarget != null;
				if (flag2)
				{
					result = visualElementFocusChangeTarget.target;
				}
				else
				{
					this.DoUpdate();
					bool flag3 = this.m_FocusRing.Count == 0;
					if (flag3)
					{
						result = null;
					}
					else
					{
						int num = 0;
						bool flag4 = direction == VisualElementFocusChangeDirection.right;
						if (flag4)
						{
							num = this.GetFocusableInternalIndex(currentFocusable) + 1;
							bool flag5 = currentFocusable != null && num == 0;
							if (flag5)
							{
								return VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
							}
							bool flag6 = num == this.m_FocusRing.Count;
							if (flag6)
							{
								num = 0;
							}
							while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
							{
								num++;
								bool flag7 = num == this.m_FocusRing.Count;
								if (flag7)
								{
									return null;
								}
							}
						}
						else
						{
							bool flag8 = direction == VisualElementFocusChangeDirection.left;
							if (flag8)
							{
								num = this.GetFocusableInternalIndex(currentFocusable) - 1;
								bool flag9 = currentFocusable != null && num == -2;
								if (flag9)
								{
									return VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
								}
								bool flag10 = num < 0;
								if (flag10)
								{
									num = this.m_FocusRing.Count - 1;
								}
								while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
								{
									num--;
									bool flag11 = num == -1;
									if (flag11)
									{
										return null;
									}
								}
							}
						}
						result = this.m_FocusRing[num].m_Focusable;
					}
				}
			}
			return result;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001B824 File Offset: 0x00019A24
		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VisualElement nextElementDepthFirst = currentFocusable.GetNextElementDepthFirst();
				while (!nextElementDepthFirst.canGrabFocus || nextElementDepthFirst.tabIndex < 0 || nextElementDepthFirst.excludeFromFocusRing)
				{
					nextElementDepthFirst = nextElementDepthFirst.GetNextElementDepthFirst();
					bool flag2 = nextElementDepthFirst == null;
					if (flag2)
					{
						nextElementDepthFirst = currentFocusable.GetRoot();
					}
					bool flag3 = nextElementDepthFirst == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				result = nextElementDepthFirst;
			}
			return result;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001B898 File Offset: 0x00019A98
		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetPreviousElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetPreviousElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
						while (visualElement.childCount > 0)
						{
							visualElement = visualElement.hierarchy.ElementAt(visualElement.childCount - 1);
						}
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				result = visualElement;
			}
			return result;
		}

		// Token: 0x040002FC RID: 764
		private readonly VisualElement root;

		// Token: 0x040002FD RID: 765
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElementFocusRing.DefaultFocusOrder <defaultFocusOrder>k__BackingField;

		// Token: 0x040002FE RID: 766
		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		// Token: 0x020000EC RID: 236
		public enum DefaultFocusOrder
		{
			// Token: 0x04000300 RID: 768
			ChildOrder,
			// Token: 0x04000301 RID: 769
			PositionXY,
			// Token: 0x04000302 RID: 770
			PositionYX
		}

		// Token: 0x020000ED RID: 237
		private class FocusRingRecord
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x000020C2 File Offset: 0x000002C2
			public FocusRingRecord()
			{
			}

			// Token: 0x04000303 RID: 771
			public int m_AutoIndex;

			// Token: 0x04000304 RID: 772
			public Focusable m_Focusable;

			// Token: 0x04000305 RID: 773
			public bool m_IsSlot;

			// Token: 0x04000306 RID: 774
			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}
	}
}
