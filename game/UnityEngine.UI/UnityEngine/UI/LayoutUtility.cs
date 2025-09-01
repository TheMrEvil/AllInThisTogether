using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000027 RID: 39
	public static class LayoutUtility
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000EFF5 File Offset: 0x0000D1F5
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetMinHeight(rect);
			}
			return LayoutUtility.GetMinWidth(rect);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F007 File Offset: 0x0000D207
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetPreferredHeight(rect);
			}
			return LayoutUtility.GetPreferredWidth(rect);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F019 File Offset: 0x0000D219
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetFlexibleHeight(rect);
			}
			return LayoutUtility.GetFlexibleWidth(rect);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F02B File Offset: 0x0000D22B
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F058 File Offset: 0x0000D258
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000F0EA File Offset: 0x0000D2EA
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F118 File Offset: 0x0000D318
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F17E File Offset: 0x0000D37E
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((Behaviour)layoutElement).isActiveAndEnabled)
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
			return num;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006DA RID: 1754 RVA: 0x0001C0BF File Offset: 0x0001A2BF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x0001C0CB File Offset: 0x0001A2CB
			public <>c()
			{
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x0001C0D3 File Offset: 0x0001A2D3
			internal float <GetMinWidth>b__3_0(ILayoutElement e)
			{
				return e.minWidth;
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x0001C0DB File Offset: 0x0001A2DB
			internal float <GetPreferredWidth>b__4_0(ILayoutElement e)
			{
				return e.minWidth;
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x0001C0E3 File Offset: 0x0001A2E3
			internal float <GetPreferredWidth>b__4_1(ILayoutElement e)
			{
				return e.preferredWidth;
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x0001C0EB File Offset: 0x0001A2EB
			internal float <GetFlexibleWidth>b__5_0(ILayoutElement e)
			{
				return e.flexibleWidth;
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
			internal float <GetMinHeight>b__6_0(ILayoutElement e)
			{
				return e.minHeight;
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x0001C0FB File Offset: 0x0001A2FB
			internal float <GetPreferredHeight>b__7_0(ILayoutElement e)
			{
				return e.minHeight;
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x0001C103 File Offset: 0x0001A303
			internal float <GetPreferredHeight>b__7_1(ILayoutElement e)
			{
				return e.preferredHeight;
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x0001C10B File Offset: 0x0001A30B
			internal float <GetFlexibleHeight>b__8_0(ILayoutElement e)
			{
				return e.flexibleHeight;
			}

			// Token: 0x040002D1 RID: 721
			public static readonly LayoutUtility.<>c <>9 = new LayoutUtility.<>c();

			// Token: 0x040002D2 RID: 722
			public static Func<ILayoutElement, float> <>9__3_0;

			// Token: 0x040002D3 RID: 723
			public static Func<ILayoutElement, float> <>9__4_0;

			// Token: 0x040002D4 RID: 724
			public static Func<ILayoutElement, float> <>9__4_1;

			// Token: 0x040002D5 RID: 725
			public static Func<ILayoutElement, float> <>9__5_0;

			// Token: 0x040002D6 RID: 726
			public static Func<ILayoutElement, float> <>9__6_0;

			// Token: 0x040002D7 RID: 727
			public static Func<ILayoutElement, float> <>9__7_0;

			// Token: 0x040002D8 RID: 728
			public static Func<ILayoutElement, float> <>9__7_1;

			// Token: 0x040002D9 RID: 729
			public static Func<ILayoutElement, float> <>9__8_0;
		}
	}
}
