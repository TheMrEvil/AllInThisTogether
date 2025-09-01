using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F9 RID: 1017
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
	public struct CullingResults : IEquatable<CullingResults>
	{
		// Token: 0x0600227E RID: 8830
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetLightIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x0600227F RID: 8831
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetReflectionProbeIndexCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexCount(IntPtr cullingResultsPtr);

		// Token: 0x06002280 RID: 8832
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndices(IntPtr cullingResultsPtr, ComputeBuffer computeBuffer);

		// Token: 0x06002281 RID: 8833
		[FreeFunction("FillLightAndReflectionProbeIndices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightAndReflectionProbeIndicesGraphicsBuffer(IntPtr cullingResultsPtr, GraphicsBuffer buffer);

		// Token: 0x06002282 RID: 8834
		[FreeFunction("GetLightIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLightIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06002283 RID: 8835
		[FreeFunction("GetReflectionProbeIndexMapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetReflectionProbeIndexMapSize(IntPtr cullingResultsPtr);

		// Token: 0x06002284 RID: 8836
		[FreeFunction("FillLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06002285 RID: 8837
		[FreeFunction("FillReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06002286 RID: 8838
		[FreeFunction("SetLightIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLightIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06002287 RID: 8839
		[FreeFunction("SetReflectionProbeIndexMapScriptable")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReflectionProbeIndexMap(IntPtr cullingResultsPtr, IntPtr indexMapPtr, int indexMapSize);

		// Token: 0x06002288 RID: 8840
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetShadowCasterBounds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetShadowCasterBounds(IntPtr cullingResultsPtr, int lightIndex, out Bounds bounds);

		// Token: 0x06002289 RID: 8841
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeSpotShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeSpotShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x0600228A RID: 8842
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputePointShadowMatricesAndCullingPrimitives")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputePointShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x0600228B RID: 8843 RVA: 0x00039EFC File Offset: 0x000380FC
		[FreeFunction("ScriptableRenderPipeline_Bindings::ComputeDirectionalShadowMatricesAndCullingPrimitives")]
		private static bool ComputeDirectionalShadowMatricesAndCullingPrimitives(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(cullingResultsPtr, activeLightIndex, splitIndex, splitCount, ref splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x00039F1E File Offset: 0x0003811E
		public unsafe NativeArray<VisibleLight> visibleLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleLightsPtr, this.m_AllocationInfo->visibleLightCount);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x00039F3C File Offset: 0x0003813C
		public unsafe NativeArray<VisibleLight> visibleOffscreenVertexLights
		{
			get
			{
				return this.GetNativeArray<VisibleLight>((void*)this.m_AllocationInfo->visibleOffscreenVertexLightsPtr, this.m_AllocationInfo->visibleOffscreenVertexLightCount);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x00039F5A File Offset: 0x0003815A
		public unsafe NativeArray<VisibleReflectionProbe> visibleReflectionProbes
		{
			get
			{
				return this.GetNativeArray<VisibleReflectionProbe>((void*)this.m_AllocationInfo->visibleReflectionProbesPtr, this.m_AllocationInfo->visibleReflectionProbeCount);
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00039F78 File Offset: 0x00038178
		private unsafe NativeArray<T> GetNativeArray<T>(void* dataPointer, int length) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataPointer, length, Allocator.Invalid);
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x00039F94 File Offset: 0x00038194
		public int lightIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x00039FB4 File Offset: 0x000381B4
		public int reflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x00039FD4 File Offset: 0x000381D4
		public int lightAndReflectionProbeIndexCount
		{
			get
			{
				return CullingResults.GetLightIndexCount(this.ptr) + CullingResults.GetReflectionProbeIndexCount(this.ptr);
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00039FFD File Offset: 0x000381FD
		public void FillLightAndReflectionProbeIndices(ComputeBuffer computeBuffer)
		{
			CullingResults.FillLightAndReflectionProbeIndices(this.ptr, computeBuffer);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0003A00D File Offset: 0x0003820D
		public void FillLightAndReflectionProbeIndices(GraphicsBuffer buffer)
		{
			CullingResults.FillLightAndReflectionProbeIndicesGraphicsBuffer(this.ptr, buffer);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0003A020 File Offset: 0x00038220
		public NativeArray<int> GetLightIndexMap(Allocator allocator)
		{
			int lightIndexMapSize = CullingResults.GetLightIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(lightIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillLightIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), lightIndexMapSize);
			return nativeArray;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0003A061 File Offset: 0x00038261
		public void SetLightIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetLightIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0003A084 File Offset: 0x00038284
		public NativeArray<int> GetReflectionProbeIndexMap(Allocator allocator)
		{
			int reflectionProbeIndexMapSize = CullingResults.GetReflectionProbeIndexMapSize(this.ptr);
			NativeArray<int> nativeArray = new NativeArray<int>(reflectionProbeIndexMapSize, allocator, NativeArrayOptions.UninitializedMemory);
			CullingResults.FillReflectionProbeIndexMap(this.ptr, (IntPtr)nativeArray.GetUnsafePtr<int>(), reflectionProbeIndexMapSize);
			return nativeArray;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0003A0C5 File Offset: 0x000382C5
		public void SetReflectionProbeIndexMap(NativeArray<int> lightIndexMap)
		{
			CullingResults.SetReflectionProbeIndexMap(this.ptr, (IntPtr)lightIndexMap.GetUnsafeReadOnlyPtr<int>(), lightIndexMap.Length);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0003A0E8 File Offset: 0x000382E8
		public bool GetShadowCasterBounds(int lightIndex, out Bounds outBounds)
		{
			return CullingResults.GetShadowCasterBounds(this.ptr, lightIndex, out outBounds);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0003A108 File Offset: 0x00038308
		public bool ComputeSpotShadowMatricesAndCullingPrimitives(int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeSpotShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0003A12C File Offset: 0x0003832C
		public bool ComputePointShadowMatricesAndCullingPrimitives(int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputePointShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0003A154 File Offset: 0x00038354
		public bool ComputeDirectionalShadowMatricesAndCullingPrimitives(int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return CullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(this.ptr, activeLightIndex, splitIndex, splitCount, splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0003A180 File Offset: 0x00038380
		public bool Equals(CullingResults other)
		{
			return this.ptr.Equals(other.ptr) && this.m_AllocationInfo == other.m_AllocationInfo;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0003A1BC File Offset: 0x000383BC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CullingResults && this.Equals((CullingResults)obj);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0003A1F4 File Offset: 0x000383F4
		public override int GetHashCode()
		{
			int hashCode = this.ptr.GetHashCode();
			return hashCode * 397 ^ this.m_AllocationInfo;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0003A228 File Offset: 0x00038428
		public static bool operator ==(CullingResults left, CullingResults right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0003A244 File Offset: 0x00038444
		public static bool operator !=(CullingResults left, CullingResults right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060022A3 RID: 8867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(IntPtr cullingResultsPtr, int activeLightIndex, int splitIndex, int splitCount, ref Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		// Token: 0x04000CCA RID: 3274
		internal IntPtr ptr;

		// Token: 0x04000CCB RID: 3275
		private unsafe CullingAllocationInfo* m_AllocationInfo;
	}
}
