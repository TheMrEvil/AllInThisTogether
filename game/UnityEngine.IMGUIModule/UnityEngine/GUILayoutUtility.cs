using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
	public class GUILayoutUtility
	{
		// Token: 0x0600024C RID: 588 RVA: 0x000095D4 File Offset: 0x000077D4
		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect result;
			GUILayoutUtility.Internal_GetWindowRect_Injected(windowID, out result);
			return result;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000095EA File Offset: 0x000077EA
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.Internal_MoveWindow_Injected(windowID, ref r);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000095F4 File Offset: 0x000077F4
		internal static Rect GetWindowsBounds()
		{
			Rect result;
			GUILayoutUtility.GetWindowsBounds_Injected(out result);
			return result;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00009609 File Offset: 0x00007809
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00009610 File Offset: 0x00007810
		internal static int unbalancedgroupscount
		{
			[CompilerGenerated]
			get
			{
				return GUILayoutUtility.<unbalancedgroupscount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				GUILayoutUtility.<unbalancedgroupscount>k__BackingField = value;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009618 File Offset: 0x00007818
		internal static void CleanupRoots()
		{
			GUILayoutUtility.s_SpaceStyle = null;
			GUILayoutUtility.s_StoredLayouts.Clear();
			GUILayoutUtility.s_StoredWindows.Clear();
			GUILayoutUtility.current = new GUILayoutUtility.LayoutCache(-1);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009644 File Offset: 0x00007844
		internal static GUILayoutUtility.LayoutCache GetLayoutCache(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts;
			GUILayoutUtility.LayoutCache result;
			dictionary.TryGetValue(instanceID, out result);
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009674 File Offset: 0x00007874
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts;
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.GetLayoutCache(instanceID, isWindow);
			bool flag = layoutCache == null;
			if (flag)
			{
				layoutCache = new GUILayoutUtility.LayoutCache(instanceID);
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000096EC File Offset: 0x000078EC
		internal static void RemoveSelectedIdList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts;
			bool flag = dictionary.ContainsKey(instanceID);
			if (flag)
			{
				dictionary.Remove(instanceID);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009720 File Offset: 0x00007920
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000097D8 File Offset: 0x000079D8
		internal static void BeginContainer(GUILayoutUtility.LayoutCache cache)
		{
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				cache.topLevel = new GUILayoutGroup();
				cache.layoutGroups.Clear();
				cache.layoutGroups.Push(cache.topLevel);
				cache.windows = new GUILayoutGroup();
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009860 File Offset: 0x00007A60
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				bool flag2 = options != null;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000220D File Offset: 0x0000040D
		[Obsolete("BeginGroup has no effect and will be removed", false)]
		public static void BeginGroup(string GroupName)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000220D File Offset: 0x0000040D
		[Obsolete("EndGroup has no effect and will be removed", false)]
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009954 File Offset: 0x00007B54
		internal static void Layout()
		{
			bool flag = GUILayoutUtility.current.topLevel.windowID == -1;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009A3C File Offset: 0x00007C3C
		internal static void LayoutFromEditorWindow()
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009AE0 File Offset: 0x00007CE0
		internal static void LayoutFromContainer(float w, float h)
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, w);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, h);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009B6C File Offset: 0x00007D6C
		internal static float LayoutFromInspector(float width)
		{
			bool flag = GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1;
			float result;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				result = minHeight;
			}
			else
			{
				bool flag2 = GUILayoutUtility.current.topLevel != null;
				if (flag2)
				{
					GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				}
				result = 0f;
			}
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009C5C File Offset: 0x00007E5C
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup i = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(i);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009CC0 File Offset: 0x00007EC0
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			bool flag = !i.isWindow;
			if (flag)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009DC4 File Offset: 0x00007FC4
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			bool flag = !typeof(GUILayoutGroup).IsAssignableFrom(LayoutType);
			if (flag)
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup", "LayoutType");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009E08 File Offset: 0x00008008
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
		{
			GUILayoutUtility.unbalancedgroupscount++;
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup guilayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				guilayoutGroup = (GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup);
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				bool flag2 = options != null;
				if (flag2)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009EE4 File Offset: 0x000080E4
		internal static void EndLayoutGroup()
		{
			GUILayoutUtility.unbalancedgroupscount--;
			bool flag = GUILayoutUtility.current.layoutGroups.Count == 0;
			if (flag)
			{
				Debug.LogError("EndLayoutGroup: BeginLayoutGroup must be called first.");
			}
			else
			{
				GUILayoutUtility.current.layoutGroups.Pop();
				bool flag2 = 0 < GUILayoutUtility.current.layoutGroups.Count;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				}
				else
				{
					GUILayoutUtility.current.topLevel = new GUILayoutGroup();
				}
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009F78 File Offset: 0x00008178
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup guilayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				guilayoutGroup = (GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup);
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A034 File Offset: 0x00008234
		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, layoutType);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A04D File Offset: 0x0000824D
		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A05C File Offset: 0x0000825C
		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A078 File Offset: 0x00008278
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A094 File Offset: 0x00008294
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					GUILayoutEntry next = GUILayoutUtility.current.topLevel.GetNext();
					rect = next.rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				bool isHeightDependantOnWidth = style.isHeightDependantOnWidth;
				if (isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 constraints = new Vector2(0f, 0f);
					bool flag = options != null;
					if (flag)
					{
						foreach (GUILayoutOption guilayoutOption in options)
						{
							GUILayoutOption.Type type2 = guilayoutOption.type;
							GUILayoutOption.Type type3 = type2;
							if (type3 != GUILayoutOption.Type.maxWidth)
							{
								if (type3 == GUILayoutOption.Type.maxHeight)
								{
									constraints.y = (float)guilayoutOption.value;
								}
							}
							else
							{
								constraints.x = (float)guilayoutOption.value;
							}
						}
					}
					Vector2 vector = style.CalcSizeWithConstraints(content, constraints);
					vector.x = Mathf.Ceil(vector.x);
					vector.y = Mathf.Ceil(vector.y);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A208 File Offset: 0x00008408
		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A22C File Offset: 0x0000842C
		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A24C File Offset: 0x0000844C
		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A270 File Offset: 0x00008470
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A290 File Offset: 0x00008490
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A318 File Offset: 0x00008518
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A388 File Offset: 0x00008588
		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect last;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				last = GUILayoutUtility.current.topLevel.GetLast();
			}
			else
			{
				last = GUILayoutUtility.kDummyRect;
			}
			return last;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A3CC File Offset: 0x000085CC
		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A3E8 File Offset: 0x000085E8
		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A404 File Offset: 0x00008604
		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A420 File Offset: 0x00008620
		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A43C File Offset: 0x0000863C
		private static Rect DoGetAspectRect(float aspect, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A4A4 File Offset: 0x000086A4
		internal static GUIStyle spaceStyle
		{
			get
			{
				bool flag = GUILayoutUtility.s_SpaceStyle == null;
				if (flag)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUILayoutUtility()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A4DD File Offset: 0x000086DD
		// Note: this type is marked as 'beforefieldinit'.
		static GUILayoutUtility()
		{
		}

		// Token: 0x0600027B RID: 635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

		// Token: 0x0600027C RID: 636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MoveWindow_Injected(int windowID, ref Rect r);

		// Token: 0x0600027D RID: 637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWindowsBounds_Injected(out Rect ret);

		// Token: 0x04000093 RID: 147
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000094 RID: 148
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000095 RID: 149
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache(-1);

		// Token: 0x04000096 RID: 150
		internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000097 RID: 151
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static int <unbalancedgroupscount>k__BackingField;

		// Token: 0x04000098 RID: 152
		private static GUIStyle s_SpaceStyle;

		// Token: 0x02000025 RID: 37
		internal readonly struct LayoutCacheState
		{
			// Token: 0x0600027E RID: 638 RVA: 0x0000A51C File Offset: 0x0000871C
			public LayoutCacheState(GUILayoutUtility.LayoutCache cache)
			{
				this.id = cache.id;
				this.topLevel = cache.topLevel;
				this.layoutGroups = cache.layoutGroups;
				this.windows = cache.windows;
			}

			// Token: 0x04000099 RID: 153
			public readonly int id;

			// Token: 0x0400009A RID: 154
			public readonly GUILayoutGroup topLevel;

			// Token: 0x0400009B RID: 155
			public readonly GenericStack layoutGroups;

			// Token: 0x0400009C RID: 156
			public readonly GUILayoutGroup windows;
		}

		// Token: 0x02000026 RID: 38
		[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
		internal sealed class LayoutCache
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A54F File Offset: 0x0000874F
			// (set) Token: 0x06000280 RID: 640 RVA: 0x0000A557 File Offset: 0x00008757
			internal int id
			{
				[CompilerGenerated]
				get
				{
					return this.<id>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<id>k__BackingField = value;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A560 File Offset: 0x00008760
			public GUILayoutUtility.LayoutCacheState State
			{
				get
				{
					return new GUILayoutUtility.LayoutCacheState(this);
				}
			}

			// Token: 0x06000282 RID: 642 RVA: 0x0000A568 File Offset: 0x00008768
			internal LayoutCache(int instanceID = -1)
			{
				this.id = instanceID;
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x06000283 RID: 643 RVA: 0x0000A5B8 File Offset: 0x000087B8
			internal void CopyState(GUILayoutUtility.LayoutCacheState other)
			{
				this.id = other.id;
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			// Token: 0x06000284 RID: 644 RVA: 0x0000A5EC File Offset: 0x000087EC
			public void ResetCursor()
			{
				this.windows.ResetCursor();
				this.topLevel.ResetCursor();
				foreach (object obj in this.layoutGroups)
				{
					((GUILayoutGroup)obj).ResetCursor();
				}
			}

			// Token: 0x0400009D RID: 157
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private int <id>k__BackingField;

			// Token: 0x0400009E RID: 158
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x0400009F RID: 159
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x040000A0 RID: 160
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
