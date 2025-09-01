using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/IMGUI/GUIDebugger.bindings.h")]
	internal class GUIDebugger
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00007D5F File Offset: 0x00005F5F
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style)
		{
			GUIDebugger.LogLayoutEntry_Injected(ref rect, left, right, top, bottom, style);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007D6F File Offset: 0x00005F6F
		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutGroupEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical)
		{
			GUIDebugger.LogLayoutGroupEntry_Injected(ref rect, left, right, top, bottom, style, isVertical);
		}

		// Token: 0x0600019F RID: 415
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[NativeMethod("LogEndGroup")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogLayoutEndGroup();

		// Token: 0x060001A0 RID: 416 RVA: 0x00007D81 File Offset: 0x00005F81
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		public static void LogBeginProperty(string targetTypeAssemblyQualifiedName, string path, Rect position)
		{
			GUIDebugger.LogBeginProperty_Injected(targetTypeAssemblyQualifiedName, path, ref position);
		}

		// Token: 0x060001A1 RID: 417
		[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogEndProperty();

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A2 RID: 418
		[NativeConditional("UNITY_EDITOR")]
		public static extern bool active { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060001A3 RID: 419 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUIDebugger()
		{
		}

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutGroupEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogBeginProperty_Injected(string targetTypeAssemblyQualifiedName, string path, ref Rect position);
	}
}
