using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040F RID: 1039
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		// Token: 0x0600237E RID: 9086
		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		// Token: 0x0600237F RID: 9087
		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly, bool isStencilReadOnly);

		// Token: 0x06002380 RID: 9088
		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndSubPass_Internal(IntPtr self);

		// Token: 0x06002381 RID: 9089
		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		// Token: 0x06002382 RID: 9090 RVA: 0x0003C024 File Offset: 0x0003A224
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		// Token: 0x06002383 RID: 9091
		[FreeFunction("InitializeSortSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings);

		// Token: 0x06002384 RID: 9092 RVA: 0x0003C02F File Offset: 0x0003A22F
		private void Submit_Internal()
		{
			ScriptableRenderContext.Submit_Internal_Injected(ref this);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0003C037 File Offset: 0x0003A237
		private bool SubmitForRenderPassValidation_Internal()
		{
			return ScriptableRenderContext.SubmitForRenderPassValidation_Internal_Injected(ref this);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0003C03F File Offset: 0x0003A23F
		private void GetCameras_Internal(Type listType, object resultList)
		{
			ScriptableRenderContext.GetCameras_Internal_Injected(ref this, listType, resultList);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0003C04C File Offset: 0x0003A24C
		private void DrawRenderers_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			ScriptableRenderContext.DrawRenderers_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0003C06C File Offset: 0x0003A26C
		private void DrawShadows_Internal(IntPtr shadowDrawingSettings)
		{
			ScriptableRenderContext.DrawShadows_Internal_Injected(ref this, shadowDrawingSettings);
		}

		// Token: 0x06002389 RID: 9097
		[FreeFunction("PlayerEmitCanvasGeometryForCamera")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EmitGeometryForCamera(Camera camera);

		// Token: 0x0600238A RID: 9098 RVA: 0x0003C075 File Offset: 0x0003A275
		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, commandBuffer);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0003C07E File Offset: 0x0003A27E
		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, commandBuffer, queueType);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0003C088 File Offset: 0x0003A288
		private void SetupCameraProperties_Internal([NotNull("NullExceptionObject")] Camera camera, bool stereoSetup, int eye)
		{
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, camera, stereoSetup, eye);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0003C093 File Offset: 0x0003A293
		private void StereoEndRender_Internal([NotNull("NullExceptionObject")] Camera camera, int eye, bool isFinalPass)
		{
			ScriptableRenderContext.StereoEndRender_Internal_Injected(ref this, camera, eye, isFinalPass);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0003C09E File Offset: 0x0003A29E
		private void StartMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera, int eye)
		{
			ScriptableRenderContext.StartMultiEye_Internal_Injected(ref this, camera, eye);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0003C0A8 File Offset: 0x0003A2A8
		private void StopMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.StopMultiEye_Internal_Injected(ref this, camera);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0003C0B1 File Offset: 0x0003A2B1
		private void DrawSkybox_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawSkybox_Internal_Injected(ref this, camera);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0003C0BA File Offset: 0x0003A2BA
		private void InvokeOnRenderObjectCallback_Internal()
		{
			ScriptableRenderContext.InvokeOnRenderObjectCallback_Internal_Injected(ref this);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0003C0C2 File Offset: 0x0003A2C2
		private void DrawGizmos_Internal([NotNull("NullExceptionObject")] Camera camera, GizmoSubset gizmoSubset)
		{
			ScriptableRenderContext.DrawGizmos_Internal_Injected(ref this, camera, gizmoSubset);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		private void DrawWireOverlay_Impl([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawWireOverlay_Impl_Injected(ref this, camera);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0003C0D5 File Offset: 0x0003A2D5
		private void DrawUIOverlay_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawUIOverlay_Internal_Injected(ref this, camera);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0003C0E0 File Offset: 0x0003A2E0
		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0003C0F8 File Offset: 0x0003A2F8
		private RendererList CreateRendererList_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			RendererList result;
			ScriptableRenderContext.CreateRendererList_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount, out result);
			return result;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0003C11B File Offset: 0x0003A31B
		private void PrepareRendererListsAsync_Internal(object rendererLists)
		{
			ScriptableRenderContext.PrepareRendererListsAsync_Internal_Injected(ref this, rendererLists);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0003C124 File Offset: 0x0003A324
		private RendererListStatus QueryRendererListStatus_Internal(RendererList handle)
		{
			return ScriptableRenderContext.QueryRendererListStatus_Internal_Injected(ref this, ref handle);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0003C12E File Offset: 0x0003A32E
		internal ScriptableRenderContext(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0003C138 File Offset: 0x0003A338
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0003C160 File Offset: 0x0003A360
		public ScopedRenderPass BeginScopedRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.BeginRenderPass(width, height, samples, attachments, depthAttachmentIndex);
			return new ScopedRenderPass(this);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0003C18B File Offset: 0x0003A38B
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0003C1C1 File Offset: 0x0003A3C1
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0003C1F6 File Offset: 0x0003A3F6
		public void BeginSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0003C21F File Offset: 0x0003A41F
		public void BeginSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0003C248 File Offset: 0x0003A448
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, inputs, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0003C274 File Offset: 0x0003A474
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, inputs, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0003C29C File Offset: 0x0003A49C
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0003C2C4 File Offset: 0x0003A4C4
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0003C2EA File Offset: 0x0003A4EA
		public void EndSubPass()
		{
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0003C2F9 File Offset: 0x0003A4F9
		public void EndRenderPass()
		{
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0003C308 File Offset: 0x0003A508
		public void Submit()
		{
			this.Submit_Internal();
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0003C314 File Offset: 0x0003A514
		public bool SubmitForRenderPassValidation()
		{
			return this.SubmitForRenderPassValidation_Internal();
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0003C32C File Offset: 0x0003A52C
		internal void GetCameras(List<Camera> results)
		{
			this.GetCameras_Internal(typeof(Camera), results);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0003C344 File Offset: 0x0003A544
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings)
		{
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0003C374 File Offset: 0x0003A574
		public unsafe void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock stateBlock)
		{
			ShaderTagId shaderTagId = default(ShaderTagId);
			fixed (RenderStateBlock* ptr = &stateBlock)
			{
				RenderStateBlock* value = ptr;
				this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)value), 1);
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0003C3BC File Offset: 0x0003A5BC
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, NativeArray<ShaderTagId> renderTypes, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = renderTypes.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
				{
					"renderTypes",
					"stateBlocks",
					"renderTypes",
					renderTypes.Length,
					"stateBlocks",
					stateBlocks.Length
				}));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ScriptableRenderContext.kRenderTypeTag, false, (IntPtr)renderTypes.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), renderTypes.Length);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0003C46C File Offset: 0x0003A66C
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, NativeArray<ShaderTagId> tagValues, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = tagValues.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
				{
					"tagValues",
					"stateBlocks",
					"tagValues",
					tagValues.Length,
					"stateBlocks",
					stateBlocks.Length
				}));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, tagName, isPassTagName, (IntPtr)tagValues.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), tagValues.Length);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0003C518 File Offset: 0x0003A718
		public unsafe void DrawShadows(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* value = ptr;
				this.DrawShadows_Internal((IntPtr)((void*)value));
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0003C540 File Offset: 0x0003A740
		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0003C588 File Offset: 0x0003A788
		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x0003C5D1 File Offset: 0x0003A7D1
		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0003C5DE File Offset: 0x0003A7DE
		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0003C5EB File Offset: 0x0003A7EB
		public void StereoEndRender(Camera camera)
		{
			this.StereoEndRender(camera, 0, true);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0003C5F8 File Offset: 0x0003A7F8
		public void StereoEndRender(Camera camera, int eye)
		{
			this.StereoEndRender(camera, eye, true);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0003C605 File Offset: 0x0003A805
		public void StereoEndRender(Camera camera, int eye, bool isFinalPass)
		{
			this.StereoEndRender_Internal(camera, eye, isFinalPass);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0003C612 File Offset: 0x0003A812
		public void StartMultiEye(Camera camera)
		{
			this.StartMultiEye(camera, 0);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0003C61E File Offset: 0x0003A81E
		public void StartMultiEye(Camera camera, int eye)
		{
			this.StartMultiEye_Internal(camera, eye);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0003C62A File Offset: 0x0003A82A
		public void StopMultiEye(Camera camera)
		{
			this.StopMultiEye_Internal(camera);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0003C635 File Offset: 0x0003A835
		public void DrawSkybox(Camera camera)
		{
			this.DrawSkybox_Internal(camera);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0003C640 File Offset: 0x0003A840
		public void InvokeOnRenderObjectCallback()
		{
			this.InvokeOnRenderObjectCallback_Internal();
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0003C64A File Offset: 0x0003A84A
		public void DrawGizmos(Camera camera, GizmoSubset gizmoSubset)
		{
			this.DrawGizmos_Internal(camera, gizmoSubset);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0003C656 File Offset: 0x0003A856
		public void DrawWireOverlay(Camera camera)
		{
			this.DrawWireOverlay_Impl(camera);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0003C661 File Offset: 0x0003A861
		public void DrawUIOverlay(Camera camera)
		{
			this.DrawUIOverlay_Internal(camera);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0003C66C File Offset: 0x0003A86C
		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults result = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&result)));
			return result;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0003C69C File Offset: 0x0003A89C
		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0003C6C4 File Offset: 0x0003A8C4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x0003C71C File Offset: 0x0003A91C
		public static bool operator ==(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0003C738 File Offset: 0x0003A938
		public static bool operator !=(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0003C758 File Offset: 0x0003A958
		public unsafe RendererList CreateRendererList(RendererListDesc desc)
		{
			RendererListParams rendererListParams = RendererListParams.Create(desc);
			bool flag = rendererListParams.stateBlock == null;
			RendererList result;
			if (flag)
			{
				result = this.CreateRendererList_Internal(rendererListParams.cullingResult.ptr, ref rendererListParams.drawSettings, ref rendererListParams.filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
			}
			else
			{
				ShaderTagId shaderTagId = default(ShaderTagId);
				RenderStateBlock value = rendererListParams.stateBlock.Value;
				RenderStateBlock* value2 = &value;
				result = this.CreateRendererList_Internal(rendererListParams.cullingResult.ptr, ref rendererListParams.drawSettings, ref rendererListParams.filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)value2), 1);
			}
			return result;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0003C80A File Offset: 0x0003AA0A
		public void PrepareRendererListsAsync(List<RendererList> rendererLists)
		{
			this.PrepareRendererListsAsync_Internal(rendererLists);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0003C818 File Offset: 0x0003AA18
		public RendererListStatus QueryRendererListStatus(RendererList rendererList)
		{
			return this.QueryRendererListStatus_Internal(rendererList);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0003C831 File Offset: 0x0003AA31
		// Note: this type is marked as 'beforefieldinit'.
		static ScriptableRenderContext()
		{
		}

		// Token: 0x060023C8 RID: 9160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, ref ScriptableRenderContext renderLoop, IntPtr results);

		// Token: 0x060023C9 RID: 9161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Submit_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023CA RID: 9162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SubmitForRenderPassValidation_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023CB RID: 9163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCameras_Internal_Injected(ref ScriptableRenderContext _unity_self, Type listType, object resultList);

		// Token: 0x060023CC RID: 9164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawRenderers_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount);

		// Token: 0x060023CD RID: 9165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawShadows_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawingSettings);

		// Token: 0x060023CE RID: 9166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer);

		// Token: 0x060023CF RID: 9167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer, ComputeQueueType queueType);

		// Token: 0x060023D0 RID: 9168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, bool stereoSetup, int eye);

		// Token: 0x060023D1 RID: 9169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StereoEndRender_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye, bool isFinalPass);

		// Token: 0x060023D2 RID: 9170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye);

		// Token: 0x060023D3 RID: 9171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023D4 RID: 9172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawSkybox_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023D5 RID: 9173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeOnRenderObjectCallback_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023D6 RID: 9174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawGizmos_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, GizmoSubset gizmoSubset);

		// Token: 0x060023D7 RID: 9175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireOverlay_Impl_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023D8 RID: 9176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawUIOverlay_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023D9 RID: 9177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount, out RendererList ret);

		// Token: 0x060023DA RID: 9178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareRendererListsAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, object rendererLists);

		// Token: 0x060023DB RID: 9179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RendererListStatus QueryRendererListStatus_Internal_Injected(ref ScriptableRenderContext _unity_self, ref RendererList handle);

		// Token: 0x04000D37 RID: 3383
		private static readonly ShaderTagId kRenderTypeTag = new ShaderTagId("RenderType");

		// Token: 0x04000D38 RID: 3384
		private IntPtr m_Ptr;
	}
}
