using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000033 RID: 51
	internal static class GroupBoxUtility
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00006779 File Offset: 0x00004979
		public static void RegisterGroupBoxOptionCallbacks<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			option.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(GroupBoxUtility.OnOptionAttachToPanel), TrickleDown.NoTrickleDown);
			option.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnOptionDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000067B0 File Offset: 0x000049B0
		public static void OnOptionSelected<T>(this T selectedOption) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(selectedOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[selectedOption].OnOptionSelectionChanged(selectedOption);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000067F4 File Offset: 0x000049F4
		public static IGroupBoxOption GetSelectedOption(this IGroupBox groupBox)
		{
			return (!GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox)) ? null : GroupBoxUtility.s_GroupManagers[groupBox].GetSelectedOption();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006828 File Offset: 0x00004A28
		public static IGroupManager GetGroupManager(this IGroupBox groupBox)
		{
			return GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox) ? GroupBoxUtility.s_GroupManagers[groupBox] : null;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006858 File Offset: 0x00004A58
		private static void OnOptionAttachToPanel(AttachToPanelEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			IGroupBoxOption groupBoxOption = evt.currentTarget as IGroupBoxOption;
			IGroupManager groupManager = null;
			for (VisualElement parent = visualElement.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				IGroupBox groupBox = parent as IGroupBox;
				bool flag = groupBox != null;
				if (flag)
				{
					groupManager = GroupBoxUtility.FindOrCreateGroupManager(groupBox);
					break;
				}
			}
			bool flag2 = groupManager == null;
			if (flag2)
			{
				groupManager = GroupBoxUtility.FindOrCreateGroupManager(visualElement.elementPanel);
			}
			groupManager.RegisterOption(groupBoxOption);
			GroupBoxUtility.s_GroupOptionManagerCache[groupBoxOption] = groupManager;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000068FC File Offset: 0x00004AFC
		private static void OnOptionDetachFromPanel(DetachFromPanelEvent evt)
		{
			IGroupBoxOption groupBoxOption = evt.currentTarget as IGroupBoxOption;
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(groupBoxOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[groupBoxOption].UnregisterOption(groupBoxOption);
				GroupBoxUtility.s_GroupOptionManagerCache.Remove(groupBoxOption);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006948 File Offset: 0x00004B48
		private static IGroupManager FindOrCreateGroupManager(IGroupBox groupBox)
		{
			bool flag = GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox);
			IGroupManager result;
			if (flag)
			{
				result = GroupBoxUtility.s_GroupManagers[groupBox];
			}
			else
			{
				Type type = null;
				foreach (Type type2 in groupBox.GetType().GetInterfaces())
				{
					bool flag2 = type2.IsGenericType && GroupBoxUtility.k_GenericGroupBoxType.IsAssignableFrom(type2.GetGenericTypeDefinition());
					if (flag2)
					{
						type = type2.GetGenericArguments()[0];
						break;
					}
				}
				IGroupManager groupManager2;
				if (type == null)
				{
					IGroupManager groupManager = new DefaultGroupManager();
					groupManager2 = groupManager;
				}
				else
				{
					groupManager2 = (IGroupManager)Activator.CreateInstance(type);
				}
				IGroupManager groupManager3 = groupManager2;
				BaseVisualElementPanel baseVisualElementPanel = groupBox as BaseVisualElementPanel;
				bool flag3 = baseVisualElementPanel != null;
				if (flag3)
				{
					baseVisualElementPanel.panelDisposed += GroupBoxUtility.OnPanelDestroyed;
				}
				else
				{
					VisualElement visualElement = groupBox as VisualElement;
					bool flag4 = visualElement != null;
					if (flag4)
					{
						visualElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnGroupBoxDetachedFromPanel), TrickleDown.NoTrickleDown);
					}
				}
				GroupBoxUtility.s_GroupManagers[groupBox] = groupManager3;
				result = groupManager3;
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006A54 File Offset: 0x00004C54
		private static void OnGroupBoxDetachedFromPanel(DetachFromPanelEvent evt)
		{
			GroupBoxUtility.s_GroupManagers.Remove(evt.currentTarget as IGroupBox);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006A6D File Offset: 0x00004C6D
		private static void OnPanelDestroyed(BaseVisualElementPanel panel)
		{
			GroupBoxUtility.s_GroupManagers.Remove(panel);
			panel.panelDisposed -= GroupBoxUtility.OnPanelDestroyed;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006A8F File Offset: 0x00004C8F
		// Note: this type is marked as 'beforefieldinit'.
		static GroupBoxUtility()
		{
		}

		// Token: 0x04000090 RID: 144
		private static Dictionary<IGroupBox, IGroupManager> s_GroupManagers = new Dictionary<IGroupBox, IGroupManager>();

		// Token: 0x04000091 RID: 145
		private static Dictionary<IGroupBoxOption, IGroupManager> s_GroupOptionManagerCache = new Dictionary<IGroupBoxOption, IGroupManager>();

		// Token: 0x04000092 RID: 146
		private static readonly Type k_GenericGroupBoxType = typeof(IGroupBox<>);
	}
}
