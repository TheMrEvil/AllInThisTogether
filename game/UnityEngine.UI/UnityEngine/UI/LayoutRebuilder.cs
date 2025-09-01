using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000026 RID: 38
	public class LayoutRebuilder : ICanvasElement
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000EAC1 File Offset: 0x0000CCC1
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000EAD6 File Offset: 0x0000CCD6
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EB3A File Offset: 0x0000CD3A
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000EB42 File Offset: 0x0000CD42
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000EB4A File Offset: 0x0000CD4A
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000EB58 File Offset: 0x0000CD58
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000EB80 File Offset: 0x0000CD80
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(layoutRoot);
			layoutRebuilder.Rebuild(CanvasUpdate.Layout);
			LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000EC74 File Offset: 0x0000CE74
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						Component component = list[j];
						if (component && component is ScrollRect)
						{
							if (((ScrollRect)component).content != rect)
							{
								action(list[j]);
							}
						}
						else
						{
							action(list[j]);
						}
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ED78 File Offset: 0x0000CF78
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0 || rect.GetComponent(typeof(ILayoutGroup)))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EE14 File Offset: 0x0000D014
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			bool flag = true;
			RectTransform rectTransform = rect;
			RectTransform rectTransform2 = rectTransform.parent as RectTransform;
			while (flag && !(rectTransform2 == null) && !(rectTransform2.gameObject == null))
			{
				flag = false;
				rectTransform2.GetComponents(typeof(ILayoutGroup), list);
				for (int i = 0; i < list.Count; i++)
				{
					Component component = list[i];
					if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
					{
						flag = true;
						rectTransform = rectTransform2;
						break;
					}
				}
				rectTransform2 = (rectTransform2.parent as RectTransform);
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform, list))
			{
				CollectionPool<List<Component>, Component>.Release(list);
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				Component component = comps[i];
				if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000EF6C File Offset: 0x0000D16C
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(layoutRebuilder))
			{
				LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public void LayoutComplete()
		{
			LayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000EFB7 File Offset: 0x0000D1B7
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EFBF File Offset: 0x0000D1BF
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000EFCF File Offset: 0x0000D1CF
		public override string ToString()
		{
			string str = "(Layout Rebuilder for) ";
			RectTransform toRebuild = this.m_ToRebuild;
			return str + ((toRebuild != null) ? toRebuild.ToString() : null);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EFED File Offset: 0x0000D1ED
		public LayoutRebuilder()
		{
		}

		// Token: 0x040000F3 RID: 243
		private RectTransform m_ToRebuild;

		// Token: 0x040000F4 RID: 244
		private int m_CachedHashFromTransform;

		// Token: 0x040000F5 RID: 245
		private static ObjectPool<LayoutRebuilder> s_Rebuilders = new ObjectPool<LayoutRebuilder>(() => new LayoutRebuilder(), null, delegate(LayoutRebuilder x)
		{
			x.Clear();
		}, null, true, 10, 10000);

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006D1 RID: 1745 RVA: 0x0001C04E File Offset: 0x0001A24E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0001C05A File Offset: 0x0001A25A
			public <>c()
			{
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x0001C062 File Offset: 0x0001A262
			internal LayoutRebuilder <.cctor>b__5_0()
			{
				return new LayoutRebuilder();
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x0001C069 File Offset: 0x0001A269
			internal void <.cctor>b__5_1(LayoutRebuilder x)
			{
				x.Clear();
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x0001C071 File Offset: 0x0001A271
			internal bool <StripDisabledBehavioursFromList>b__10_0(Component e)
			{
				return e is Behaviour && !((Behaviour)e).isActiveAndEnabled;
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x0001C08B File Offset: 0x0001A28B
			internal void <Rebuild>b__12_0(Component e)
			{
				(e as ILayoutElement).CalculateLayoutInputHorizontal();
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x0001C098 File Offset: 0x0001A298
			internal void <Rebuild>b__12_1(Component e)
			{
				(e as ILayoutController).SetLayoutHorizontal();
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001C0A5 File Offset: 0x0001A2A5
			internal void <Rebuild>b__12_2(Component e)
			{
				(e as ILayoutElement).CalculateLayoutInputVertical();
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x0001C0B2 File Offset: 0x0001A2B2
			internal void <Rebuild>b__12_3(Component e)
			{
				(e as ILayoutController).SetLayoutVertical();
			}

			// Token: 0x040002CB RID: 715
			public static readonly LayoutRebuilder.<>c <>9 = new LayoutRebuilder.<>c();

			// Token: 0x040002CC RID: 716
			public static Predicate<Component> <>9__10_0;

			// Token: 0x040002CD RID: 717
			public static UnityAction<Component> <>9__12_0;

			// Token: 0x040002CE RID: 718
			public static UnityAction<Component> <>9__12_1;

			// Token: 0x040002CF RID: 719
			public static UnityAction<Component> <>9__12_2;

			// Token: 0x040002D0 RID: 720
			public static UnityAction<Component> <>9__12_3;
		}
	}
}
