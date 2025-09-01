using System;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000AB RID: 171
	public class ReductionWorkData : IDisposable
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0001AF48 File Offset: 0x00019148
		public ReductionWorkData(VirtualMesh vmesh)
		{
			this.vmesh = vmesh;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001AF58 File Offset: 0x00019158
		public void Dispose()
		{
			if (this.vertexJoinIndices.IsCreated)
			{
				this.vertexJoinIndices.Dispose();
			}
			if (this.vertexToVertexMap.IsCreated)
			{
				this.vertexToVertexMap.Dispose();
			}
			if (this.vertexRemapIndices.IsCreated)
			{
				this.vertexRemapIndices.Dispose();
			}
			if (this.useSkinBoneMap.IsCreated)
			{
				this.useSkinBoneMap.Dispose();
			}
			if (this.newVertexToVertexMap.IsCreated)
			{
				this.newVertexToVertexMap.Dispose();
			}
			if (this.edgeSet.IsCreated)
			{
				this.edgeSet.Dispose();
			}
			if (this.triangleSet.IsCreated)
			{
				this.triangleSet.Dispose();
			}
			ExSimpleNativeArray<VertexAttribute> exSimpleNativeArray = this.newAttributes;
			if (exSimpleNativeArray != null)
			{
				exSimpleNativeArray.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray2 = this.newLocalPositions;
			if (exSimpleNativeArray2 != null)
			{
				exSimpleNativeArray2.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray3 = this.newLocalNormals;
			if (exSimpleNativeArray3 != null)
			{
				exSimpleNativeArray3.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray4 = this.newLocalTangents;
			if (exSimpleNativeArray4 != null)
			{
				exSimpleNativeArray4.Dispose();
			}
			ExSimpleNativeArray<float2> exSimpleNativeArray5 = this.newUv;
			if (exSimpleNativeArray5 != null)
			{
				exSimpleNativeArray5.Dispose();
			}
			ExSimpleNativeArray<VirtualMeshBoneWeight> exSimpleNativeArray6 = this.newBoneWeights;
			if (exSimpleNativeArray6 != null)
			{
				exSimpleNativeArray6.Dispose();
			}
			if (this.newSkinBoneCount.IsCreated)
			{
				this.newSkinBoneCount.Dispose();
			}
			if (this.newSkinBoneTransformIndices.IsCreated)
			{
				this.newSkinBoneTransformIndices.Dispose();
			}
			if (this.newSkinBoneBindPoseList.IsCreated)
			{
				this.newSkinBoneBindPoseList.Dispose();
			}
			if (this.newLineList.IsCreated)
			{
				this.newLineList.Dispose();
			}
			if (this.newTriangleList.IsCreated)
			{
				this.newTriangleList.Dispose();
			}
		}

		// Token: 0x04000566 RID: 1382
		public VirtualMesh vmesh;

		// Token: 0x04000567 RID: 1383
		public NativeArray<int> vertexJoinIndices;

		// Token: 0x04000568 RID: 1384
		public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

		// Token: 0x04000569 RID: 1385
		public NativeArray<int> vertexRemapIndices;

		// Token: 0x0400056A RID: 1386
		public NativeParallelHashMap<int, int> useSkinBoneMap;

		// Token: 0x0400056B RID: 1387
		public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

		// Token: 0x0400056C RID: 1388
		public NativeParallelHashSet<int2> edgeSet;

		// Token: 0x0400056D RID: 1389
		public NativeParallelHashSet<int3> triangleSet;

		// Token: 0x0400056E RID: 1390
		public int oldVertexCount;

		// Token: 0x0400056F RID: 1391
		public int newVertexCount;

		// Token: 0x04000570 RID: 1392
		public int removeVertexCount;

		// Token: 0x04000571 RID: 1393
		public ExSimpleNativeArray<VertexAttribute> newAttributes;

		// Token: 0x04000572 RID: 1394
		public ExSimpleNativeArray<float3> newLocalPositions;

		// Token: 0x04000573 RID: 1395
		public ExSimpleNativeArray<float3> newLocalNormals;

		// Token: 0x04000574 RID: 1396
		public ExSimpleNativeArray<float3> newLocalTangents;

		// Token: 0x04000575 RID: 1397
		public ExSimpleNativeArray<float2> newUv;

		// Token: 0x04000576 RID: 1398
		public ExSimpleNativeArray<VirtualMeshBoneWeight> newBoneWeights;

		// Token: 0x04000577 RID: 1399
		public NativeReference<int> newSkinBoneCount;

		// Token: 0x04000578 RID: 1400
		public NativeList<int> newSkinBoneTransformIndices;

		// Token: 0x04000579 RID: 1401
		public NativeList<float4x4> newSkinBoneBindPoseList;

		// Token: 0x0400057A RID: 1402
		public NativeList<int2> newLineList;

		// Token: 0x0400057B RID: 1403
		public NativeList<int3> newTriangleList;
	}
}
