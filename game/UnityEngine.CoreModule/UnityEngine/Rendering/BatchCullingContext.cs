using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EA RID: 1002
	[UsedByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct BatchCullingContext
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x00037F00 File Offset: 0x00036100
		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = Matrix4x4.identity;
			this.nearPlane = 0f;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00037F50 File Offset: 0x00036150
		[Obsolete("For internal BatchRendererGroup use only")]
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(null, 0, Allocator.Invalid);
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00037F8F File Offset: 0x0003618F
		internal BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, NativeArray<int> outVisibleIndicesY, LODParameters inLodParameters, Matrix4x4 inCullingMatrix, float inNearPlane)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.visibleIndicesY = outVisibleIndicesY;
			this.lodParameters = inLodParameters;
			this.cullingMatrix = inCullingMatrix;
			this.nearPlane = inNearPlane;
		}

		// Token: 0x04000C54 RID: 3156
		public readonly NativeArray<Plane> cullingPlanes;

		// Token: 0x04000C55 RID: 3157
		public NativeArray<BatchVisibility> batchVisibility;

		// Token: 0x04000C56 RID: 3158
		public NativeArray<int> visibleIndices;

		// Token: 0x04000C57 RID: 3159
		public NativeArray<int> visibleIndicesY;

		// Token: 0x04000C58 RID: 3160
		public readonly LODParameters lodParameters;

		// Token: 0x04000C59 RID: 3161
		public readonly Matrix4x4 cullingMatrix;

		// Token: 0x04000C5A RID: 3162
		public readonly float nearPlane;
	}
}
