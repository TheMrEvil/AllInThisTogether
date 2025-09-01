using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EC RID: 1004
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class BatchRendererGroup : IDisposable
	{
		// Token: 0x060021E0 RID: 8672 RVA: 0x00037FC7 File Offset: 0x000361C7
		public BatchRendererGroup(BatchRendererGroup.OnPerformCulling cullingCallback)
		{
			this.m_PerformCulling = cullingCallback;
			this.m_GroupHandle = BatchRendererGroup.Create(this);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00037FEF File Offset: 0x000361EF
		public void Dispose()
		{
			BatchRendererGroup.Destroy(this.m_GroupHandle);
			this.m_GroupHandle = IntPtr.Zero;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0003800C File Offset: 0x0003620C
		public int AddBatch(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask = 9223372036854775808UL, uint renderingLayerMask = 4294967295U)
		{
			return this.AddBatch_Injected(mesh, subMeshIndex, material, layer, castShadows, receiveShadows, invertCulling, ref bounds, instanceCount, customProps, associatedSceneObject, sceneCullingMask, renderingLayerMask);
		}

		// Token: 0x060021E3 RID: 8675
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBatchFlags(int batchIndex, ulong flags);

		// Token: 0x060021E4 RID: 8676 RVA: 0x00038036 File Offset: 0x00036236
		public void SetBatchPropertyMetadata(int batchIndex, NativeArray<int> cbufferLengths, NativeArray<int> cbufferMetadata)
		{
			this.InternalSetBatchPropertyMetadata(batchIndex, (IntPtr)cbufferLengths.GetUnsafeReadOnlyPtr<int>(), cbufferLengths.Length, (IntPtr)cbufferMetadata.GetUnsafeReadOnlyPtr<int>(), cbufferMetadata.Length);
		}

		// Token: 0x060021E5 RID: 8677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetBatchPropertyMetadata(int batchIndex, IntPtr cbufferLengths, int cbufferLengthsCount, IntPtr cbufferMetadata, int cbufferMetadataCount);

		// Token: 0x060021E6 RID: 8678
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInstancingData(int batchIndex, int instanceCount, MaterialPropertyBlock customProps);

		// Token: 0x060021E7 RID: 8679 RVA: 0x00038068 File Offset: 0x00036268
		public unsafe NativeArray<Matrix4x4> GetBatchMatrices(int batchIndex)
		{
			int length = 0;
			void* batchMatrices = this.GetBatchMatrices(batchIndex, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrices, length, Allocator.Invalid);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00038090 File Offset: 0x00036290
		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray, length, Allocator.Invalid);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000380BC File Offset: 0x000362BC
		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchScalarArray = this.GetBatchScalarArray(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray, length, Allocator.Invalid);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000380E8 File Offset: 0x000362E8
		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray, length, Allocator.Invalid);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00038114 File Offset: 0x00036314
		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchVectorArray = this.GetBatchVectorArray(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray, length, Allocator.Invalid);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00038140 File Offset: 0x00036340
		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, string propertyName)
		{
			int length = 0;
			void* batchMatrixArray = this.GetBatchMatrixArray(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray, length, Allocator.Invalid);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0003816C File Offset: 0x0003636C
		public unsafe NativeArray<int> GetBatchScalarArrayInt(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchScalarArray_Internal, length, Allocator.Invalid);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00038198 File Offset: 0x00036398
		public unsafe NativeArray<float> GetBatchScalarArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchScalarArray_Internal = this.GetBatchScalarArray_Internal(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(batchScalarArray_Internal, length, Allocator.Invalid);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000381C4 File Offset: 0x000363C4
		public unsafe NativeArray<int> GetBatchVectorArrayInt(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(batchVectorArray_Internal, length, Allocator.Invalid);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000381F0 File Offset: 0x000363F0
		public unsafe NativeArray<Vector4> GetBatchVectorArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchVectorArray_Internal = this.GetBatchVectorArray_Internal(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector4>(batchVectorArray_Internal, length, Allocator.Invalid);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0003821C File Offset: 0x0003641C
		public unsafe NativeArray<Matrix4x4> GetBatchMatrixArray(int batchIndex, int propertyName)
		{
			int length = 0;
			void* batchMatrixArray_Internal = this.GetBatchMatrixArray_Internal(batchIndex, propertyName, out length);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(batchMatrixArray_Internal, length, Allocator.Invalid);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00038245 File Offset: 0x00036445
		public void SetBatchBounds(int batchIndex, Bounds bounds)
		{
			this.SetBatchBounds_Injected(batchIndex, ref bounds);
		}

		// Token: 0x060021F3 RID: 8691
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetNumBatches();

		// Token: 0x060021F4 RID: 8692
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveBatch(int index);

		// Token: 0x060021F5 RID: 8693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrices(int batchIndex, out int matrixCount);

		// Token: 0x060021F6 RID: 8694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchScalarArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x060021F7 RID: 8695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchVectorArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x060021F8 RID: 8696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrixArray(int batchIndex, string propertyName, out int elementCount);

		// Token: 0x060021F9 RID: 8697
		[NativeName("GetBatchScalarArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchScalarArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x060021FA RID: 8698
		[NativeName("GetBatchVectorArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchVectorArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x060021FB RID: 8699
		[NativeName("GetBatchMatrixArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void* GetBatchMatrixArray_Internal(int batchIndex, int propertyName, out int elementCount);

		// Token: 0x060021FC RID: 8700
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableVisibleIndicesYArray(bool enabled);

		// Token: 0x060021FD RID: 8701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(BatchRendererGroup group);

		// Token: 0x060021FE RID: 8702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr groupHandle);

		// Token: 0x060021FF RID: 8703 RVA: 0x00038250 File Offset: 0x00036450
		[RequiredByNativeCode]
		private unsafe static void InvokeOnPerformCulling(BatchRendererGroup group, ref BatchRendererCullingOutput context, ref LODParameters lodParameters)
		{
			NativeArray<Plane> inCullingPlanes = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Plane>((void*)context.cullingPlanes, context.cullingPlanesCount, Allocator.Invalid);
			NativeArray<BatchVisibility> inOutBatchVisibility = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BatchVisibility>((void*)context.batchVisibility, context.batchVisibilityCount, Allocator.Invalid);
			NativeArray<int> outVisibleIndices = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)context.visibleIndices, context.visibleIndicesCount, Allocator.Invalid);
			NativeArray<int> outVisibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>((void*)context.visibleIndicesY, context.visibleIndicesCount, Allocator.Invalid);
			try
			{
				context.cullingJobsFence = group.m_PerformCulling(group, new BatchCullingContext(inCullingPlanes, inOutBatchVisibility, outVisibleIndices, outVisibleIndicesY, lodParameters, context.cullingMatrix, context.nearPlane));
			}
			finally
			{
				JobHandle.ScheduleBatchedJobs();
			}
		}

		// Token: 0x06002200 RID: 8704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddBatch_Injected(Mesh mesh, int subMeshIndex, Material material, int layer, ShadowCastingMode castShadows, bool receiveShadows, bool invertCulling, ref Bounds bounds, int instanceCount, MaterialPropertyBlock customProps, GameObject associatedSceneObject, ulong sceneCullingMask = 9223372036854775808UL, uint renderingLayerMask = 4294967295U);

		// Token: 0x06002201 RID: 8705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBatchBounds_Injected(int batchIndex, ref Bounds bounds);

		// Token: 0x04000C65 RID: 3173
		private IntPtr m_GroupHandle = IntPtr.Zero;

		// Token: 0x04000C66 RID: 3174
		private BatchRendererGroup.OnPerformCulling m_PerformCulling;

		// Token: 0x020003ED RID: 1005
		// (Invoke) Token: 0x06002203 RID: 8707
		public delegate JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cullingContext);
	}
}
