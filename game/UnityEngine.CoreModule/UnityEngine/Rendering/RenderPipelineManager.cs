using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000408 RID: 1032
	public static class RenderPipelineManager
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0003B11A File Offset: 0x0003931A
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x0003B121 File Offset: 0x00039321
		public static RenderPipeline currentPipeline
		{
			get
			{
				return RenderPipelineManager.s_currentPipeline;
			}
			private set
			{
				RenderPipelineManager.s_currentPipelineType = ((value != null) ? value.GetType().ToString() : "Built-in Pipeline");
				RenderPipelineManager.s_currentPipeline = value;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002326 RID: 8998 RVA: 0x0003B144 File Offset: 0x00039344
		// (remove) Token: 0x06002327 RID: 8999 RVA: 0x0003B178 File Offset: 0x00039378
		public static event Action<ScriptableRenderContext, List<Camera>> beginContextRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.beginContextRendering;
				Action<ScriptableRenderContext, List<Camera>> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, List<Camera>> value2 = (Action<ScriptableRenderContext, List<Camera>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, List<Camera>>>(ref RenderPipelineManager.beginContextRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.beginContextRendering;
				Action<ScriptableRenderContext, List<Camera>> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, List<Camera>> value2 = (Action<ScriptableRenderContext, List<Camera>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, List<Camera>>>(ref RenderPipelineManager.beginContextRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002328 RID: 9000 RVA: 0x0003B1AC File Offset: 0x000393AC
		// (remove) Token: 0x06002329 RID: 9001 RVA: 0x0003B1E0 File Offset: 0x000393E0
		public static event Action<ScriptableRenderContext, List<Camera>> endContextRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.endContextRendering;
				Action<ScriptableRenderContext, List<Camera>> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, List<Camera>> value2 = (Action<ScriptableRenderContext, List<Camera>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, List<Camera>>>(ref RenderPipelineManager.endContextRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, List<Camera>> action = RenderPipelineManager.endContextRendering;
				Action<ScriptableRenderContext, List<Camera>> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, List<Camera>> value2 = (Action<ScriptableRenderContext, List<Camera>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, List<Camera>>>(ref RenderPipelineManager.endContextRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600232A RID: 9002 RVA: 0x0003B214 File Offset: 0x00039414
		// (remove) Token: 0x0600232B RID: 9003 RVA: 0x0003B248 File Offset: 0x00039448
		public static event Action<ScriptableRenderContext, Camera[]> beginFrameRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.beginFrameRendering;
				Action<ScriptableRenderContext, Camera[]> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera[]> value2 = (Action<ScriptableRenderContext, Camera[]>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera[]>>(ref RenderPipelineManager.beginFrameRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.beginFrameRendering;
				Action<ScriptableRenderContext, Camera[]> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera[]> value2 = (Action<ScriptableRenderContext, Camera[]>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera[]>>(ref RenderPipelineManager.beginFrameRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600232C RID: 9004 RVA: 0x0003B27C File Offset: 0x0003947C
		// (remove) Token: 0x0600232D RID: 9005 RVA: 0x0003B2B0 File Offset: 0x000394B0
		public static event Action<ScriptableRenderContext, Camera> beginCameraRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
				Action<ScriptableRenderContext, Camera> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera> value2 = (Action<ScriptableRenderContext, Camera>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera>>(ref RenderPipelineManager.beginCameraRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
				Action<ScriptableRenderContext, Camera> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera> value2 = (Action<ScriptableRenderContext, Camera>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera>>(ref RenderPipelineManager.beginCameraRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600232E RID: 9006 RVA: 0x0003B2E4 File Offset: 0x000394E4
		// (remove) Token: 0x0600232F RID: 9007 RVA: 0x0003B318 File Offset: 0x00039518
		public static event Action<ScriptableRenderContext, Camera[]> endFrameRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
				Action<ScriptableRenderContext, Camera[]> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera[]> value2 = (Action<ScriptableRenderContext, Camera[]>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera[]>>(ref RenderPipelineManager.endFrameRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
				Action<ScriptableRenderContext, Camera[]> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera[]> value2 = (Action<ScriptableRenderContext, Camera[]>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera[]>>(ref RenderPipelineManager.endFrameRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002330 RID: 9008 RVA: 0x0003B34C File Offset: 0x0003954C
		// (remove) Token: 0x06002331 RID: 9009 RVA: 0x0003B380 File Offset: 0x00039580
		public static event Action<ScriptableRenderContext, Camera> endCameraRendering
		{
			[CompilerGenerated]
			add
			{
				Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
				Action<ScriptableRenderContext, Camera> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera> value2 = (Action<ScriptableRenderContext, Camera>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera>>(ref RenderPipelineManager.endCameraRendering, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
				Action<ScriptableRenderContext, Camera> action2;
				do
				{
					action2 = action;
					Action<ScriptableRenderContext, Camera> value2 = (Action<ScriptableRenderContext, Camera>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScriptableRenderContext, Camera>>(ref RenderPipelineManager.endCameraRendering, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002332 RID: 9010 RVA: 0x0003B3B4 File Offset: 0x000395B4
		// (remove) Token: 0x06002333 RID: 9011 RVA: 0x0003B3E8 File Offset: 0x000395E8
		public static event Action activeRenderPipelineTypeChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = RenderPipelineManager.activeRenderPipelineTypeChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineTypeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = RenderPipelineManager.activeRenderPipelineTypeChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineTypeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x0003B41B File Offset: 0x0003961B
		public static bool pipelineSwitchCompleted
		{
			get
			{
				return RenderPipelineManager.s_CurrentPipelineAsset == GraphicsSettings.currentRenderPipeline && !RenderPipelineManager.IsPipelineRequireCreation();
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002335 RID: 9013 RVA: 0x0003B434 File Offset: 0x00039634
		// (remove) Token: 0x06002336 RID: 9014 RVA: 0x0003B468 File Offset: 0x00039668
		public static event Action activeRenderPipelineCreated
		{
			[CompilerGenerated]
			add
			{
				Action action = RenderPipelineManager.activeRenderPipelineCreated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineCreated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = RenderPipelineManager.activeRenderPipelineCreated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineCreated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002337 RID: 9015 RVA: 0x0003B49C File Offset: 0x0003969C
		// (remove) Token: 0x06002338 RID: 9016 RVA: 0x0003B4D0 File Offset: 0x000396D0
		public static event Action activeRenderPipelineDisposed
		{
			[CompilerGenerated]
			add
			{
				Action action = RenderPipelineManager.activeRenderPipelineDisposed;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineDisposed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = RenderPipelineManager.activeRenderPipelineDisposed;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref RenderPipelineManager.activeRenderPipelineDisposed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0003B503 File Offset: 0x00039703
		internal static void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.beginFrameRendering;
			if (action != null)
			{
				action(context, cameras.ToArray());
			}
			Action<ScriptableRenderContext, List<Camera>> action2 = RenderPipelineManager.beginContextRendering;
			if (action2 != null)
			{
				action2(context, cameras);
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0003B531 File Offset: 0x00039731
		internal static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.beginCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0003B547 File Offset: 0x00039747
		internal static void EndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			Action<ScriptableRenderContext, Camera[]> action = RenderPipelineManager.endFrameRendering;
			if (action != null)
			{
				action(context, cameras.ToArray());
			}
			Action<ScriptableRenderContext, List<Camera>> action2 = RenderPipelineManager.endContextRendering;
			if (action2 != null)
			{
				action2(context, cameras);
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0003B575 File Offset: 0x00039775
		internal static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			Action<ScriptableRenderContext, Camera> action = RenderPipelineManager.endCameraRendering;
			if (action != null)
			{
				action(context, camera);
			}
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0003B58B File Offset: 0x0003978B
		[RequiredByNativeCode]
		internal static void OnActiveRenderPipelineTypeChanged()
		{
			Action action = RenderPipelineManager.activeRenderPipelineTypeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0003B5A0 File Offset: 0x000397A0
		[RequiredByNativeCode]
		internal static void HandleRenderPipelineChange(RenderPipelineAsset pipelineAsset)
		{
			bool flag = RenderPipelineManager.s_CurrentPipelineAsset != pipelineAsset;
			bool flag2 = flag;
			if (flag2)
			{
				RenderPipelineManager.CleanupRenderPipeline();
				RenderPipelineManager.s_CurrentPipelineAsset = pipelineAsset;
			}
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0003B5D0 File Offset: 0x000397D0
		[RequiredByNativeCode]
		internal static void CleanupRenderPipeline()
		{
			bool flag = RenderPipelineManager.currentPipeline != null && !RenderPipelineManager.currentPipeline.disposed;
			if (flag)
			{
				Action action = RenderPipelineManager.activeRenderPipelineDisposed;
				if (action != null)
				{
					action();
				}
				RenderPipelineManager.currentPipeline.Dispose();
				RenderPipelineManager.s_CurrentPipelineAsset = null;
				RenderPipelineManager.currentPipeline = null;
				SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0003B630 File Offset: 0x00039830
		[RequiredByNativeCode]
		private static string GetCurrentPipelineAssetType()
		{
			return RenderPipelineManager.s_currentPipelineType;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0003B648 File Offset: 0x00039848
		[RequiredByNativeCode]
		private static void DoRenderLoop_Internal(RenderPipelineAsset pipe, IntPtr loopPtr, List<Camera.RenderRequest> renderRequests)
		{
			RenderPipelineManager.PrepareRenderPipeline(pipe);
			bool flag = RenderPipelineManager.currentPipeline == null;
			if (!flag)
			{
				ScriptableRenderContext context = new ScriptableRenderContext(loopPtr);
				RenderPipelineManager.s_Cameras.Clear();
				context.GetCameras(RenderPipelineManager.s_Cameras);
				bool flag2 = renderRequests == null;
				if (flag2)
				{
					RenderPipelineManager.currentPipeline.InternalRender(context, RenderPipelineManager.s_Cameras);
				}
				else
				{
					RenderPipelineManager.currentPipeline.InternalRenderWithRequests(context, RenderPipelineManager.s_Cameras, renderRequests);
				}
				RenderPipelineManager.s_Cameras.Clear();
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0003B6C4 File Offset: 0x000398C4
		internal static void PrepareRenderPipeline(RenderPipelineAsset pipelineAsset)
		{
			RenderPipelineManager.HandleRenderPipelineChange(pipelineAsset);
			bool flag = RenderPipelineManager.IsPipelineRequireCreation();
			if (flag)
			{
				RenderPipelineManager.currentPipeline = RenderPipelineManager.s_CurrentPipelineAsset.InternalCreatePipeline();
				Action action = RenderPipelineManager.activeRenderPipelineCreated;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0003B705 File Offset: 0x00039905
		private static bool IsPipelineRequireCreation()
		{
			return RenderPipelineManager.s_CurrentPipelineAsset != null && (RenderPipelineManager.currentPipeline == null || RenderPipelineManager.currentPipeline.disposed);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0003B72B File Offset: 0x0003992B
		// Note: this type is marked as 'beforefieldinit'.
		static RenderPipelineManager()
		{
		}

		// Token: 0x04000D0C RID: 3340
		internal static RenderPipelineAsset s_CurrentPipelineAsset;

		// Token: 0x04000D0D RID: 3341
		private static List<Camera> s_Cameras = new List<Camera>();

		// Token: 0x04000D0E RID: 3342
		private static string s_currentPipelineType = "Built-in Pipeline";

		// Token: 0x04000D0F RID: 3343
		private const string s_builtinPipelineName = "Built-in Pipeline";

		// Token: 0x04000D10 RID: 3344
		private static RenderPipeline s_currentPipeline = null;

		// Token: 0x04000D11 RID: 3345
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<ScriptableRenderContext, List<Camera>> beginContextRendering;

		// Token: 0x04000D12 RID: 3346
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<ScriptableRenderContext, List<Camera>> endContextRendering;

		// Token: 0x04000D13 RID: 3347
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<ScriptableRenderContext, Camera[]> beginFrameRendering;

		// Token: 0x04000D14 RID: 3348
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<ScriptableRenderContext, Camera> beginCameraRendering;

		// Token: 0x04000D15 RID: 3349
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ScriptableRenderContext, Camera[]> endFrameRendering;

		// Token: 0x04000D16 RID: 3350
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ScriptableRenderContext, Camera> endCameraRendering;

		// Token: 0x04000D17 RID: 3351
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action activeRenderPipelineTypeChanged;

		// Token: 0x04000D18 RID: 3352
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action activeRenderPipelineCreated;

		// Token: 0x04000D19 RID: 3353
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action activeRenderPipelineDisposed;
	}
}
