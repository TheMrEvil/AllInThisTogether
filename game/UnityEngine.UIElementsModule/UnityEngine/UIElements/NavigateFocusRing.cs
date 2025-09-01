using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000246 RID: 582
	internal class NavigateFocusRing : IFocusRing
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004556D File Offset: 0x0004376D
		private FocusController focusController
		{
			get
			{
				return this.m_Root.focusController;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004557A File Offset: 0x0004377A
		public NavigateFocusRing(VisualElement root)
		{
			this.m_Root = root;
			this.m_Ring = new VisualElementFocusRing(root, VisualElementFocusRing.DefaultFocusOrder.ChildOrder);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00045598 File Offset: 0x00043798
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag)
			{
				Focusable target;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.target as Focusable, out target);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(target);
				}
			}
			bool flag2 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
			if (flag2)
			{
				switch (((NavigationMoveEvent)e).direction)
				{
				case NavigationMoveEvent.Direction.Left:
					return NavigateFocusRing.Left;
				case NavigationMoveEvent.Direction.Up:
					return NavigateFocusRing.Up;
				case NavigationMoveEvent.Direction.Right:
					return NavigateFocusRing.Right;
				case NavigationMoveEvent.Direction.Down:
					return NavigateFocusRing.Down;
				}
			}
			else
			{
				bool flag3 = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag3)
				{
					KeyDownEvent keyDownEvent = (KeyDownEvent)e;
					bool flag4 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
					if (flag4)
					{
						return keyDownEvent.shiftKey ? NavigateFocusRing.Previous : NavigateFocusRing.Next;
					}
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000456A4 File Offset: 0x000438A4
		public virtual Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
			bool flag = visualElementFocusChangeTarget != null;
			Focusable result;
			if (flag)
			{
				result = visualElementFocusChangeTarget.target;
			}
			else
			{
				bool flag2 = direction == NavigateFocusRing.Next || direction == NavigateFocusRing.Previous;
				if (flag2)
				{
					result = this.m_Ring.GetNextFocusable(currentFocusable, (direction == NavigateFocusRing.Next) ? VisualElementFocusChangeDirection.right : VisualElementFocusChangeDirection.left);
				}
				else
				{
					bool flag3 = direction == NavigateFocusRing.Up || direction == NavigateFocusRing.Down || direction == NavigateFocusRing.Right || direction == NavigateFocusRing.Left;
					if (flag3)
					{
						result = this.GetNextFocusable2D(currentFocusable, (NavigateFocusRing.ChangeDirection)direction);
					}
					else
					{
						result = currentFocusable;
					}
				}
			}
			return result;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00045748 File Offset: 0x00043948
		private Focusable GetNextFocusable2D(Focusable currentFocusable, NavigateFocusRing.ChangeDirection direction)
		{
			VisualElement visualElement = currentFocusable as VisualElement;
			bool flag = visualElement == null;
			if (flag)
			{
				visualElement = this.m_Root;
			}
			Rect worldBoundingBox = this.m_Root.worldBoundingBox;
			Rect rect = new Rect(worldBoundingBox.position - Vector2.one, worldBoundingBox.size + Vector2.one * 2f);
			Rect worldBound = visualElement.worldBound;
			Rect validRect = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
			bool flag2 = direction == NavigateFocusRing.Up;
			if (flag2)
			{
				validRect.yMin = rect.yMin;
			}
			else
			{
				bool flag3 = direction == NavigateFocusRing.Down;
				if (flag3)
				{
					validRect.yMax = rect.yMax;
				}
				else
				{
					bool flag4 = direction == NavigateFocusRing.Left;
					if (flag4)
					{
						validRect.xMin = rect.xMin;
					}
					else
					{
						bool flag5 = direction == NavigateFocusRing.Right;
						if (flag5)
						{
							validRect.xMax = rect.xMax;
						}
					}
				}
			}
			NavigateFocusRing.FocusableHierarchyTraversal focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
			focusableHierarchyTraversal.currentFocusable = visualElement;
			focusableHierarchyTraversal.direction = direction;
			focusableHierarchyTraversal.validRect = validRect;
			focusableHierarchyTraversal.firstPass = true;
			Focusable bestOverall = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
			bool flag6 = bestOverall != null;
			Focusable result;
			if (flag6)
			{
				result = bestOverall;
			}
			else
			{
				validRect = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
				bool flag7 = direction == NavigateFocusRing.Down;
				if (flag7)
				{
					validRect.yMin = rect.yMin;
				}
				else
				{
					bool flag8 = direction == NavigateFocusRing.Up;
					if (flag8)
					{
						validRect.yMax = rect.yMax;
					}
					else
					{
						bool flag9 = direction == NavigateFocusRing.Right;
						if (flag9)
						{
							validRect.xMin = rect.xMin;
						}
						else
						{
							bool flag10 = direction == NavigateFocusRing.Left;
							if (flag10)
							{
								validRect.xMax = rect.xMax;
							}
						}
					}
				}
				focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
				focusableHierarchyTraversal.currentFocusable = visualElement;
				focusableHierarchyTraversal.direction = direction;
				focusableHierarchyTraversal.validRect = validRect;
				focusableHierarchyTraversal.firstPass = false;
				bestOverall = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
				bool flag11 = bestOverall != null;
				if (flag11)
				{
					result = bestOverall;
				}
				else
				{
					result = currentFocusable;
				}
			}
			return result;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000459B0 File Offset: 0x00043BB0
		private static bool IsActive(VisualElement v)
		{
			return v.resolvedStyle.display != DisplayStyle.None && v.enabledInHierarchy;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000459DC File Offset: 0x00043BDC
		private static bool IsNavigable(Focusable focusable)
		{
			return focusable.canGrabFocus && focusable.tabIndex >= 0 && !focusable.delegatesFocus && !focusable.excludeFromFocusRing;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00045A14 File Offset: 0x00043C14
		// Note: this type is marked as 'beforefieldinit'.
		static NavigateFocusRing()
		{
		}

		// Token: 0x040007D5 RID: 2005
		public static readonly NavigateFocusRing.ChangeDirection Left = new NavigateFocusRing.ChangeDirection(1);

		// Token: 0x040007D6 RID: 2006
		public static readonly NavigateFocusRing.ChangeDirection Right = new NavigateFocusRing.ChangeDirection(2);

		// Token: 0x040007D7 RID: 2007
		public static readonly NavigateFocusRing.ChangeDirection Up = new NavigateFocusRing.ChangeDirection(3);

		// Token: 0x040007D8 RID: 2008
		public static readonly NavigateFocusRing.ChangeDirection Down = new NavigateFocusRing.ChangeDirection(4);

		// Token: 0x040007D9 RID: 2009
		public static readonly NavigateFocusRing.ChangeDirection Next = new NavigateFocusRing.ChangeDirection(5);

		// Token: 0x040007DA RID: 2010
		public static readonly NavigateFocusRing.ChangeDirection Previous = new NavigateFocusRing.ChangeDirection(6);

		// Token: 0x040007DB RID: 2011
		private readonly VisualElement m_Root;

		// Token: 0x040007DC RID: 2012
		private readonly VisualElementFocusRing m_Ring;

		// Token: 0x02000247 RID: 583
		public class ChangeDirection : FocusChangeDirection
		{
			// Token: 0x060011A4 RID: 4516 RVA: 0x0001AE63 File Offset: 0x00019063
			public ChangeDirection(int i) : base(i)
			{
			}
		}

		// Token: 0x02000248 RID: 584
		private struct FocusableHierarchyTraversal
		{
			// Token: 0x060011A5 RID: 4517 RVA: 0x00045A64 File Offset: 0x00043C64
			private bool ValidateHierarchyTraversal(VisualElement v)
			{
				return NavigateFocusRing.IsActive(v) && v.worldBoundingBox.Overlaps(this.validRect);
			}

			// Token: 0x060011A6 RID: 4518 RVA: 0x00045A98 File Offset: 0x00043C98
			private bool ValidateElement(VisualElement v)
			{
				return NavigateFocusRing.IsNavigable(v) && v.worldBound.Overlaps(this.validRect);
			}

			// Token: 0x060011A7 RID: 4519 RVA: 0x00045ACC File Offset: 0x00043CCC
			private int Order(VisualElement a, VisualElement b)
			{
				Rect worldBound = a.worldBound;
				Rect worldBound2 = b.worldBound;
				int num = this.StrictOrder(worldBound, worldBound2);
				return (num != 0) ? num : this.TieBreaker(worldBound, worldBound2);
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00045B04 File Offset: 0x00043D04
			private int StrictOrder(VisualElement a, VisualElement b)
			{
				return this.StrictOrder(a.worldBound, b.worldBound);
			}

			// Token: 0x060011A9 RID: 4521 RVA: 0x00045B28 File Offset: 0x00043D28
			private int StrictOrder(Rect ra, Rect rb)
			{
				float num = 0f;
				bool flag = this.direction == NavigateFocusRing.Up;
				if (flag)
				{
					num = rb.yMax - ra.yMax;
				}
				else
				{
					bool flag2 = this.direction == NavigateFocusRing.Down;
					if (flag2)
					{
						num = ra.yMin - rb.yMin;
					}
					else
					{
						bool flag3 = this.direction == NavigateFocusRing.Left;
						if (flag3)
						{
							num = rb.xMax - ra.xMax;
						}
						else
						{
							bool flag4 = this.direction == NavigateFocusRing.Right;
							if (flag4)
							{
								num = ra.xMin - rb.xMin;
							}
						}
					}
				}
				bool flag5 = !Mathf.Approximately(num, 0f);
				int result;
				if (flag5)
				{
					result = ((num > 0f) ? 1 : -1);
				}
				else
				{
					result = 0;
				}
				return result;
			}

			// Token: 0x060011AA RID: 4522 RVA: 0x00045BF4 File Offset: 0x00043DF4
			private int TieBreaker(Rect ra, Rect rb)
			{
				Rect worldBound = this.currentFocusable.worldBound;
				float num = (ra.min - worldBound.min).sqrMagnitude - (rb.min - worldBound.min).sqrMagnitude;
				bool flag = !Mathf.Approximately(num, 0f);
				int result;
				if (flag)
				{
					result = ((num > 0f) ? 1 : -1);
				}
				else
				{
					result = 0;
				}
				return result;
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x00045C70 File Offset: 0x00043E70
			public VisualElement GetBestOverall(VisualElement candidate, VisualElement bestSoFar = null)
			{
				bool flag = !this.ValidateHierarchyTraversal(candidate);
				VisualElement result;
				if (flag)
				{
					result = bestSoFar;
				}
				else
				{
					bool flag2 = this.ValidateElement(candidate);
					if (flag2)
					{
						bool flag3 = (!this.firstPass || this.StrictOrder(candidate, this.currentFocusable) > 0) && (bestSoFar == null || this.Order(bestSoFar, candidate) > 0);
						if (flag3)
						{
							bestSoFar = candidate;
						}
						result = bestSoFar;
					}
					else
					{
						int childCount = candidate.hierarchy.childCount;
						for (int i = 0; i < childCount; i++)
						{
							VisualElement candidate2 = candidate.hierarchy[i];
							bestSoFar = this.GetBestOverall(candidate2, bestSoFar);
						}
						result = bestSoFar;
					}
				}
				return result;
			}

			// Token: 0x040007DD RID: 2013
			public VisualElement currentFocusable;

			// Token: 0x040007DE RID: 2014
			public Rect validRect;

			// Token: 0x040007DF RID: 2015
			public bool firstPass;

			// Token: 0x040007E0 RID: 2016
			public NavigateFocusRing.ChangeDirection direction;
		}
	}
}
