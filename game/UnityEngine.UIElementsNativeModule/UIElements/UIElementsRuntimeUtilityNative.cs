using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x02000025 RID: 37
	[VisibleToOtherModules(new string[]
	{
		"Unity.UIElements"
	})]
	[NativeHeader("Modules/UIElementsNative/UIElementsRuntimeUtilityNative.h")]
	internal static class UIElementsRuntimeUtilityNative
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00003EE1 File Offset: 0x000020E1
		[RequiredByNativeCode]
		public static void RepaintOverlayPanels()
		{
			Action repaintOverlayPanelsCallback = UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback;
			if (repaintOverlayPanelsCallback != null)
			{
				repaintOverlayPanelsCallback();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003EF5 File Offset: 0x000020F5
		[RequiredByNativeCode]
		public static void UpdateRuntimePanels()
		{
			Action updateRuntimePanelsCallback = UIElementsRuntimeUtilityNative.UpdateRuntimePanelsCallback;
			if (updateRuntimePanelsCallback != null)
			{
				updateRuntimePanelsCallback();
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003F09 File Offset: 0x00002109
		[RequiredByNativeCode]
		public static void RepaintOffscreenPanels()
		{
			Action repaintOffscreenPanelsCallback = UIElementsRuntimeUtilityNative.RepaintOffscreenPanelsCallback;
			if (repaintOffscreenPanelsCallback != null)
			{
				repaintOffscreenPanelsCallback();
			}
		}

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterPlayerloopCallback();

		// Token: 0x06000173 RID: 371
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterPlayerloopCallback();

		// Token: 0x06000174 RID: 372
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VisualElementCreation();

		// Token: 0x0400006A RID: 106
		internal static Action RepaintOverlayPanelsCallback;

		// Token: 0x0400006B RID: 107
		internal static Action UpdateRuntimePanelsCallback;

		// Token: 0x0400006C RID: 108
		internal static Action RepaintOffscreenPanelsCallback;
	}
}
