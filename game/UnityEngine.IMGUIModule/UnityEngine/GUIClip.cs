using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	[NativeHeader("Modules/IMGUI/GUIClip.h")]
	internal sealed class GUIClip
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014E RID: 334
		internal static extern bool enabled { [FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetEnabled")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000076F4 File Offset: 0x000058F4
		internal static Rect visibleRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
			get
			{
				Rect result;
				GUIClip.get_visibleRect_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000770C File Offset: 0x0000590C
		internal static Rect topmostRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopMostPhysicalRect")]
			get
			{
				Rect result;
				GUIClip.get_topmostRect_Injected(out result);
				return result;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007721 File Offset: 0x00005921
		internal static void Internal_Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push_Injected(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Pop();

		// Token: 0x06000153 RID: 339
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetCount();

		// Token: 0x06000154 RID: 340 RVA: 0x00007730 File Offset: 0x00005930
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopRect")]
		internal static Rect GetTopRect()
		{
			Rect result;
			GUIClip.GetTopRect_Injected(out result);
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007748 File Offset: 0x00005948
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Vector2 Unclip_Vector2(Vector2 pos)
		{
			Vector2 result;
			GUIClip.Unclip_Vector2_Injected(ref pos, out result);
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007760 File Offset: 0x00005960
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Rect Unclip_Rect(Rect rect)
		{
			Rect result;
			GUIClip.Unclip_Rect_Injected(ref rect, out result);
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007778 File Offset: 0x00005978
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Vector2 Clip_Vector2(Vector2 absolutePos)
		{
			Vector2 result;
			GUIClip.Clip_Vector2_Injected(ref absolutePos, out result);
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007790 File Offset: 0x00005990
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Rect Internal_Clip_Rect(Rect absoluteRect)
		{
			Rect result;
			GUIClip.Internal_Clip_Rect_Injected(ref absoluteRect, out result);
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000077A8 File Offset: 0x000059A8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Vector2 UnclipToWindow_Vector2(Vector2 pos)
		{
			Vector2 result;
			GUIClip.UnclipToWindow_Vector2_Injected(ref pos, out result);
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000077C0 File Offset: 0x000059C0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Rect UnclipToWindow_Rect(Rect rect)
		{
			Rect result;
			GUIClip.UnclipToWindow_Rect_Injected(ref rect, out result);
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000077D8 File Offset: 0x000059D8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Vector2 ClipToWindow_Vector2(Vector2 absolutePos)
		{
			Vector2 result;
			GUIClip.ClipToWindow_Vector2_Injected(ref absolutePos, out result);
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000077F0 File Offset: 0x000059F0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Rect ClipToWindow_Rect(Rect absoluteRect)
		{
			Rect result;
			GUIClip.ClipToWindow_Rect_Injected(ref absoluteRect, out result);
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007808 File Offset: 0x00005A08
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetAbsoluteMousePosition")]
		private static Vector2 Internal_GetAbsoluteMousePosition()
		{
			Vector2 result;
			GUIClip.Internal_GetAbsoluteMousePosition_Injected(out result);
			return result;
		}

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reapply();

		// Token: 0x0600015F RID: 351 RVA: 0x00007820 File Offset: 0x00005A20
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 result;
			GUIClip.GetMatrix_Injected(out result);
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007835 File Offset: 0x00005A35
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.SetMatrix_Injected(ref m);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007840 File Offset: 0x00005A40
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetParentTransform")]
		internal static Matrix4x4 GetParentMatrix()
		{
			Matrix4x4 result;
			GUIClip.GetParentMatrix_Injected(out result);
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007855 File Offset: 0x00005A55
		internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip(objectTransform, objectTransform, clipRect);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007861 File Offset: 0x00005A61
		internal static void Internal_PushParentClip(Matrix4x4 renderTransform, Matrix4x4 inputTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip_Injected(ref renderTransform, ref inputTransform, ref clipRect);
		}

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopParentClip();

		// Token: 0x06000165 RID: 357 RVA: 0x0000786E File Offset: 0x00005A6E
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push(screenRect, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000787B File Offset: 0x00005A7B
		internal static void Pop()
		{
			GUIClip.Internal_Pop();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007884 File Offset: 0x00005A84
		public static Vector2 Unclip(Vector2 pos)
		{
			return GUIClip.Unclip_Vector2(pos);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000789C File Offset: 0x00005A9C
		public static Rect Unclip(Rect rect)
		{
			return GUIClip.Unclip_Rect(rect);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static Vector2 Clip(Vector2 absolutePos)
		{
			return GUIClip.Clip_Vector2(absolutePos);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000078CC File Offset: 0x00005ACC
		public static Rect Clip(Rect absoluteRect)
		{
			return GUIClip.Internal_Clip_Rect(absoluteRect);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000078E4 File Offset: 0x00005AE4
		public static Vector2 UnclipToWindow(Vector2 pos)
		{
			return GUIClip.UnclipToWindow_Vector2(pos);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000078FC File Offset: 0x00005AFC
		public static Rect UnclipToWindow(Rect rect)
		{
			return GUIClip.UnclipToWindow_Rect(rect);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007914 File Offset: 0x00005B14
		public static Vector2 ClipToWindow(Vector2 absolutePos)
		{
			return GUIClip.ClipToWindow_Vector2(absolutePos);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000792C File Offset: 0x00005B2C
		public static Rect ClipToWindow(Rect absoluteRect)
		{
			return GUIClip.ClipToWindow_Rect(absoluteRect);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007944 File Offset: 0x00005B44
		public static Vector2 GetAbsoluteMousePosition()
		{
			return GUIClip.Internal_GetAbsoluteMousePosition();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUIClip()
		{
		}

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_visibleRect_Injected(out Rect ret);

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_topmostRect_Injected(out Rect ret);

		// Token: 0x06000173 RID: 371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Push_Injected(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x06000174 RID: 372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTopRect_Injected(out Rect ret);

		// Token: 0x06000175 RID: 373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Rect_Injected(ref Rect rect, out Rect ret);

		// Token: 0x06000177 RID: 375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clip_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x06000178 RID: 376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Clip_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		// Token: 0x06000179 RID: 377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		// Token: 0x0600017A RID: 378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Rect_Injected(ref Rect rect, out Rect ret);

		// Token: 0x0600017B RID: 379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x0600017C RID: 380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		// Token: 0x0600017D RID: 381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAbsoluteMousePosition_Injected(out Vector2 ret);

		// Token: 0x0600017E RID: 382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x0600017F RID: 383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x06000180 RID: 384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParentMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_PushParentClip_Injected(ref Matrix4x4 renderTransform, ref Matrix4x4 inputTransform, ref Rect clipRect);

		// Token: 0x02000015 RID: 21
		internal struct ParentClipScope : IDisposable
		{
			// Token: 0x06000182 RID: 386 RVA: 0x0000795B File Offset: 0x00005B5B
			public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
			{
				this.m_Disposed = false;
				GUIClip.Internal_PushParentClip(objectTransform, clipRect);
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00007970 File Offset: 0x00005B70
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUIClip.Internal_PopParentClip();
				}
			}

			// Token: 0x0400006C RID: 108
			private bool m_Disposed;
		}
	}
}
