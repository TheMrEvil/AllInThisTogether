using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/UIElementsNative/UIRendererUtility.h")]
	[VisibleToOtherModules(new string[]
	{
		"Unity.UIElements"
	})]
	internal class Utility
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00003F20 File Offset: 0x00002120
		public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
		{
			int count = vector4s.Length * vector4s.Stride / 16;
			Utility.SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr<T>()), count);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000176 RID: 374 RVA: 0x00003F58 File Offset: 0x00002158
		// (remove) Token: 0x06000177 RID: 375 RVA: 0x00003F8C File Offset: 0x0000218C
		public static event Action<bool> GraphicsResourcesRecreate
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = Utility.GraphicsResourcesRecreate;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref Utility.GraphicsResourcesRecreate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = Utility.GraphicsResourcesRecreate;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref Utility.GraphicsResourcesRecreate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000178 RID: 376 RVA: 0x00003FC0 File Offset: 0x000021C0
		// (remove) Token: 0x06000179 RID: 377 RVA: 0x00003FF4 File Offset: 0x000021F4
		public static event Action EngineUpdate
		{
			[CompilerGenerated]
			add
			{
				Action action = Utility.EngineUpdate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Utility.EngineUpdate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = Utility.EngineUpdate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Utility.EngineUpdate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600017A RID: 378 RVA: 0x00004028 File Offset: 0x00002228
		// (remove) Token: 0x0600017B RID: 379 RVA: 0x0000405C File Offset: 0x0000225C
		public static event Action FlushPendingResources
		{
			[CompilerGenerated]
			add
			{
				Action action = Utility.FlushPendingResources;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Utility.FlushPendingResources, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = Utility.FlushPendingResources;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref Utility.FlushPendingResources, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600017C RID: 380 RVA: 0x00004090 File Offset: 0x00002290
		// (remove) Token: 0x0600017D RID: 381 RVA: 0x000040C4 File Offset: 0x000022C4
		public static event Action<Camera> RegisterIntermediateRenderers
		{
			[CompilerGenerated]
			add
			{
				Action<Camera> action = Utility.RegisterIntermediateRenderers;
				Action<Camera> action2;
				do
				{
					action2 = action;
					Action<Camera> value2 = (Action<Camera>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Camera>>(ref Utility.RegisterIntermediateRenderers, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Camera> action = Utility.RegisterIntermediateRenderers;
				Action<Camera> action2;
				do
				{
					action2 = action;
					Action<Camera> value2 = (Action<Camera>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Camera>>(ref Utility.RegisterIntermediateRenderers, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600017E RID: 382 RVA: 0x000040F8 File Offset: 0x000022F8
		// (remove) Token: 0x0600017F RID: 383 RVA: 0x0000412C File Offset: 0x0000232C
		public static event Action<IntPtr> RenderNodeAdd
		{
			[CompilerGenerated]
			add
			{
				Action<IntPtr> action = Utility.RenderNodeAdd;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeAdd, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IntPtr> action = Utility.RenderNodeAdd;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeAdd, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000180 RID: 384 RVA: 0x00004160 File Offset: 0x00002360
		// (remove) Token: 0x06000181 RID: 385 RVA: 0x00004194 File Offset: 0x00002394
		public static event Action<IntPtr> RenderNodeExecute
		{
			[CompilerGenerated]
			add
			{
				Action<IntPtr> action = Utility.RenderNodeExecute;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeExecute, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IntPtr> action = Utility.RenderNodeExecute;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeExecute, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000182 RID: 386 RVA: 0x000041C8 File Offset: 0x000023C8
		// (remove) Token: 0x06000183 RID: 387 RVA: 0x000041FC File Offset: 0x000023FC
		public static event Action<IntPtr> RenderNodeCleanup
		{
			[CompilerGenerated]
			add
			{
				Action<IntPtr> action = Utility.RenderNodeCleanup;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeCleanup, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IntPtr> action = Utility.RenderNodeCleanup;
				Action<IntPtr> action2;
				do
				{
					action2 = action;
					Action<IntPtr> value2 = (Action<IntPtr>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IntPtr>>(ref Utility.RenderNodeCleanup, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000422F File Offset: 0x0000242F
		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> graphicsResourcesRecreate = Utility.GraphicsResourcesRecreate;
			if (graphicsResourcesRecreate != null)
			{
				graphicsResourcesRecreate(recreate);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004244 File Offset: 0x00002444
		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.s_MarkerRaiseEngineUpdate.Begin();
				Utility.EngineUpdate();
				Utility.s_MarkerRaiseEngineUpdate.End();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004281 File Offset: 0x00002481
		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action flushPendingResources = Utility.FlushPendingResources;
			if (flushPendingResources != null)
			{
				flushPendingResources();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004295 File Offset: 0x00002495
		[RequiredByNativeCode]
		internal static void RaiseRegisterIntermediateRenderers(Camera camera)
		{
			Action<Camera> registerIntermediateRenderers = Utility.RegisterIntermediateRenderers;
			if (registerIntermediateRenderers != null)
			{
				registerIntermediateRenderers(camera);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000042AA File Offset: 0x000024AA
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeAdd(IntPtr userData)
		{
			Action<IntPtr> renderNodeAdd = Utility.RenderNodeAdd;
			if (renderNodeAdd != null)
			{
				renderNodeAdd(userData);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000042BF File Offset: 0x000024BF
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeExecute(IntPtr userData)
		{
			Action<IntPtr> renderNodeExecute = Utility.RenderNodeExecute;
			if (renderNodeExecute != null)
			{
				renderNodeExecute(userData);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000042D4 File Offset: 0x000024D4
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeCleanup(IntPtr userData)
		{
			Action<IntPtr> renderNodeCleanup = Utility.RenderNodeCleanup;
			if (renderNodeCleanup != null)
			{
				renderNodeCleanup(userData);
			}
		}

		// Token: 0x0600018B RID: 395
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		// Token: 0x0600018C RID: 396
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeBuffer(IntPtr buffer);

		// Token: 0x0600018D RID: 397
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count);

		// Token: 0x0600018F RID: 399
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes);

		// Token: 0x06000190 RID: 400 RVA: 0x000042EC File Offset: 0x000024EC
		public static void RegisterIntermediateRenderer(Camera camera, Material material, Matrix4x4 transform, Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize)
		{
			Utility.RegisterIntermediateRenderer_Injected(camera, material, ref transform, ref aabb, renderLayer, shadowCasting, receiveShadows, sameDistanceSortPriority, sceneCullingMask, rendererCallbackFlags, userData, userDataSize);
		}

		// Token: 0x06000191 RID: 401
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		// Token: 0x06000192 RID: 402
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPropertyBlock(MaterialPropertyBlock props);

		// Token: 0x06000193 RID: 403 RVA: 0x00004314 File Offset: 0x00002514
		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		// Token: 0x06000194 RID: 404
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableScissor();

		// Token: 0x06000195 RID: 405
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsScissorEnabled();

		// Token: 0x06000196 RID: 406 RVA: 0x0000431D File Offset: 0x0000251D
		[ThreadSafe]
		public static IntPtr CreateStencilState(StencilState stencilState)
		{
			return Utility.CreateStencilState_Injected(ref stencilState);
		}

		// Token: 0x06000197 RID: 407
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStencilState(IntPtr stencilState, int stencilRef);

		// Token: 0x06000198 RID: 408
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasMappedBufferRange();

		// Token: 0x06000199 RID: 409
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint InsertCPUFence();

		// Token: 0x0600019A RID: 410
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CPUFencePassed(uint fence);

		// Token: 0x0600019B RID: 411
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForCPUFencePassed(uint fence);

		// Token: 0x0600019C RID: 412
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncRenderThread();

		// Token: 0x0600019D RID: 413 RVA: 0x00004328 File Offset: 0x00002528
		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt result;
			Utility.GetActiveViewport_Injected(out result);
			return result;
		}

		// Token: 0x0600019E RID: 414
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainBegin();

		// Token: 0x0600019F RID: 415
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainEnd();

		// Token: 0x060001A0 RID: 416
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		// Token: 0x060001A1 RID: 417 RVA: 0x00004340 File Offset: 0x00002540
		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 result;
			Utility.GetUnityProjectionMatrix_Injected(out result);
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004358 File Offset: 0x00002558
		[ThreadSafe]
		public static Matrix4x4 GetDeviceProjectionMatrix()
		{
			Matrix4x4 result;
			Utility.GetDeviceProjectionMatrix_Injected(out result);
			return result;
		}

		// Token: 0x060001A3 RID: 419
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DebugIsMainThread();

		// Token: 0x060001A4 RID: 420 RVA: 0x0000207B File Offset: 0x0000027B
		public Utility()
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000436D File Offset: 0x0000256D
		// Note: this type is marked as 'beforefieldinit'.
		static Utility()
		{
		}

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterIntermediateRenderer_Injected(Camera camera, Material material, ref Matrix4x4 transform, ref Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScissorRect_Injected(ref RectInt scissorRect);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateStencilState_Injected(ref StencilState stencilState);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<bool> GraphicsResourcesRecreate;

		// Token: 0x04000075 RID: 117
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action EngineUpdate;

		// Token: 0x04000076 RID: 118
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action FlushPendingResources;

		// Token: 0x04000077 RID: 119
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<Camera> RegisterIntermediateRenderers;

		// Token: 0x04000078 RID: 120
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<IntPtr> RenderNodeAdd;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<IntPtr> RenderNodeExecute;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<IntPtr> RenderNodeCleanup;

		// Token: 0x0400007B RID: 123
		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		// Token: 0x02000029 RID: 41
		[Flags]
		internal enum RendererCallbacks
		{
			// Token: 0x0400007D RID: 125
			RendererCallback_Init = 1,
			// Token: 0x0400007E RID: 126
			RendererCallback_Exec = 2,
			// Token: 0x0400007F RID: 127
			RendererCallback_Cleanup = 4
		}

		// Token: 0x0200002A RID: 42
		internal enum GPUBufferType
		{
			// Token: 0x04000081 RID: 129
			Vertex,
			// Token: 0x04000082 RID: 130
			Index
		}

		// Token: 0x0200002B RID: 43
		public class GPUBuffer<T> : IDisposable where T : struct
		{
			// Token: 0x060001AC RID: 428 RVA: 0x0000437E File Offset: 0x0000257E
			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			// Token: 0x060001AD RID: 429 RVA: 0x000043B0 File Offset: 0x000025B0
			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			// Token: 0x060001AE RID: 430 RVA: 0x000043BF File Offset: 0x000025BF
			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001AF RID: 431 RVA: 0x000043E4 File Offset: 0x000025E4
			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x000043FC File Offset: 0x000025FC
			public int Count
			{
				get
				{
					return this.elemCount;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004414 File Offset: 0x00002614
			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x04000083 RID: 131
			private IntPtr buffer;

			// Token: 0x04000084 RID: 132
			private int elemCount;

			// Token: 0x04000085 RID: 133
			private int elemStride;
		}
	}
}
