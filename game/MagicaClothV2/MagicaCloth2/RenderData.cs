using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000072 RID: 114
	public class RenderData : IDisposable, ITransform
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		public int ReferenceCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ReferenceCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ReferenceCount>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		internal string Name
		{
			get
			{
				RenderSetupData renderSetupData = this.setupData;
				return ((renderSetupData != null) ? renderSetupData.name : null) ?? "(empty)";
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		internal bool IsSkinning
		{
			get
			{
				RenderSetupData renderSetupData = this.setupData;
				return renderSetupData != null && renderSetupData.isSkinning;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000EFF6 File Offset: 0x0000D1F6
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000EFFE File Offset: 0x0000D1FE
		public bool UseCustomMesh
		{
			[CompilerGenerated]
			get
			{
				return this.<UseCustomMesh>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UseCustomMesh>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000F007 File Offset: 0x0000D207
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000F00F File Offset: 0x0000D20F
		public bool ChangeCustomMesh
		{
			[CompilerGenerated]
			get
			{
				return this.<ChangeCustomMesh>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChangeCustomMesh>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000F018 File Offset: 0x0000D218
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000F020 File Offset: 0x0000D220
		public bool ChangePositionNormal
		{
			[CompilerGenerated]
			get
			{
				return this.<ChangePositionNormal>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChangePositionNormal>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000F029 File Offset: 0x0000D229
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000F031 File Offset: 0x0000D231
		public bool ChangeBoneWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<ChangeBoneWeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChangeBoneWeight>k__BackingField = value;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F03C File Offset: 0x0000D23C
		public void Dispose()
		{
			this.SwapOriginalMesh();
			RenderSetupData renderSetupData = this.setupData;
			if (renderSetupData != null)
			{
				renderSetupData.Dispose();
			}
			if (this.localPositions.IsCreated)
			{
				this.localPositions.Dispose();
			}
			if (this.localNormals.IsCreated)
			{
				this.localNormals.Dispose();
			}
			if (this.boneWeights.IsCreated)
			{
				this.boneWeights.Dispose();
			}
			if (this.customMesh)
			{
				UnityEngine.Object.Destroy(this.customMesh);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			RenderSetupData renderSetupData = this.setupData;
			if (renderSetupData == null)
			{
				return;
			}
			renderSetupData.GetUsedTransform(transformSet);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F0D3 File Offset: 0x0000D2D3
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			RenderSetupData renderSetupData = this.setupData;
			if (renderSetupData == null)
			{
				return;
			}
			renderSetupData.ReplaceTransform(replaceDict);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		internal void Initialize(Renderer ren)
		{
			this.setupData = new RenderSetupData(ren);
			this.centerBoneWeight = default(BoneWeight);
			this.centerBoneWeight.boneIndex0 = this.setupData.renderTransformIndex;
			this.centerBoneWeight.weight0 = 1f;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000F126 File Offset: 0x0000D326
		internal ResultCode Result
		{
			get
			{
				RenderSetupData renderSetupData = this.setupData;
				if (renderSetupData == null)
				{
					return ResultCode.None;
				}
				return renderSetupData.result;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F140 File Offset: 0x0000D340
		internal int AddReferenceCount()
		{
			int referenceCount = this.ReferenceCount;
			this.ReferenceCount = referenceCount + 1;
			return this.ReferenceCount;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000F164 File Offset: 0x0000D364
		internal int RemoveReferenceCount()
		{
			int referenceCount = this.ReferenceCount;
			this.ReferenceCount = referenceCount - 1;
			return this.ReferenceCount;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F188 File Offset: 0x0000D388
		private void SwapCustomMesh()
		{
			if (this.setupData.IsFaild())
			{
				return;
			}
			if (this.setupData.originalMesh == null)
			{
				return;
			}
			if (this.customMesh == null)
			{
				this.customMesh = UnityEngine.Object.Instantiate<Mesh>(this.setupData.originalMesh);
				this.customMesh.MarkDynamic();
				int vertexCount = this.setupData.vertexCount;
				this.localPositions = new NativeArray<Vector3>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.localNormals = new NativeArray<Vector3>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.boneWeights = new NativeArray<BoneWeight>(vertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				int transformCount = this.setupData.TransformCount;
				List<Matrix4x4> list = new List<Matrix4x4>(transformCount);
				list.AddRange(this.setupData.bindPoseList);
				while (list.Count < transformCount)
				{
					list.Add(Matrix4x4.identity);
				}
				this.customMesh.bindposes = list.ToArray();
			}
			this.ResetCustomMeshWorkData();
			this.SetMesh(this.customMesh);
			if (this.IsSkinning)
			{
				this.setupData.skinRenderer.bones = this.setupData.transformList.ToArray();
			}
			this.UseCustomMesh = true;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		private void ResetCustomMeshWorkData()
		{
			Mesh.MeshData meshData = this.setupData.meshDataArray[0];
			meshData.GetVertices(this.localPositions);
			meshData.GetNormals(this.localNormals);
			if (this.IsSkinning)
			{
				this.setupData.GetBoneWeightsRun(this.boneWeights);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F300 File Offset: 0x0000D500
		private void SwapOriginalMesh()
		{
			if (this.UseCustomMesh && this.setupData != null)
			{
				this.SetMesh(this.setupData.originalMesh);
				if (this.setupData.skinRenderer != null)
				{
					this.setupData.skinRenderer.bones = this.setupData.transformList.ToArray();
				}
			}
			this.UseCustomMesh = false;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F368 File Offset: 0x0000D568
		private void SetMesh(Mesh mesh)
		{
			if (mesh == null)
			{
				return;
			}
			if (this.setupData != null)
			{
				if (this.setupData.meshFilter != null)
				{
					this.setupData.meshFilter.mesh = mesh;
					return;
				}
				if (this.setupData.skinRenderer != null)
				{
					this.setupData.skinRenderer.sharedMesh = mesh;
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public void StartUse(ClothProcess cprocess)
		{
			this.useProcessSet.Add(cprocess);
			if (this.useProcessSet.Count == 1)
			{
				this.SwapCustomMesh();
			}
			this.ChangeCustomMesh = true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F3FA File Offset: 0x0000D5FA
		public void EndUse(ClothProcess cprocess)
		{
			this.useProcessSet.Remove(cprocess);
			if (this.useProcessSet.Count == 0)
			{
				this.SwapOriginalMesh();
			}
			else
			{
				this.ResetCustomMeshWorkData();
			}
			this.ChangeCustomMesh = true;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F42C File Offset: 0x0000D62C
		internal void WriteMesh()
		{
			if (!this.UseCustomMesh || this.useProcessSet.Count == 0)
			{
				return;
			}
			if (this.ChangePositionNormal)
			{
				this.customMesh.SetVertices<Vector3>(this.localPositions);
				this.customMesh.SetNormals<Vector3>(this.localNormals);
			}
			if (this.ChangeBoneWeight && this.IsSkinning)
			{
				this.customMesh.boneWeights = this.boneWeights.ToArray();
			}
			this.ChangeCustomMesh = false;
			this.ChangePositionNormal = false;
			this.ChangeBoneWeight = false;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		internal JobHandle UpdatePositionNormal(DataChunk mappingChunk, JobHandle jobHandle = default(JobHandle))
		{
			if (!this.UseCustomMesh)
			{
				return jobHandle;
			}
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new RenderData.UpdatePositionNormalJob2
			{
				startIndex = mappingChunk.startIndex,
				meshLocalPositions = this.localPositions.Reinterpret<float3>(),
				meshLocalNormals = this.localNormals.Reinterpret<float3>(),
				mappingReferenceIndices = vmesh.mappingReferenceIndices.GetNativeArray(),
				mappingAttributes = vmesh.mappingAttributes.GetNativeArray(),
				mappingPositions = vmesh.mappingPositions.GetNativeArray(),
				mappingRotations = vmesh.mappingRotations.GetNativeArray()
			}.Schedule(mappingChunk.dataLength, 32, jobHandle);
			this.ChangePositionNormal = true;
			return jobHandle;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F56C File Offset: 0x0000D76C
		internal JobHandle UpdateBoneWeight(DataChunk mappingChunk, JobHandle jobHandle = default(JobHandle))
		{
			if (!this.UseCustomMesh)
			{
				return jobHandle;
			}
			if (this.IsSkinning)
			{
				VirtualMeshManager vmesh = MagicaManager.VMesh;
				jobHandle = new RenderData.UpdateBoneWeightJob2
				{
					startIndex = mappingChunk.startIndex,
					centerBoneWeight = this.centerBoneWeight,
					meshBoneWeights = this.boneWeights,
					mappingReferenceIndices = vmesh.mappingReferenceIndices.GetNativeArray(),
					mappingAttributes = vmesh.mappingAttributes.GetNativeArray()
				}.Schedule(mappingChunk.dataLength, 32, jobHandle);
				this.ChangeBoneWeight = true;
			}
			return jobHandle;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F600 File Offset: 0x0000D800
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format(">>> [{0}] ref({1}), use:{2}", this.Name, this.ReferenceCount, this.useProcessSet.Count));
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F650 File Offset: 0x0000D850
		public RenderData()
		{
		}

		// Token: 0x040002DD RID: 733
		[CompilerGenerated]
		private int <ReferenceCount>k__BackingField;

		// Token: 0x040002DE RID: 734
		private HashSet<ClothProcess> useProcessSet = new HashSet<ClothProcess>();

		// Token: 0x040002DF RID: 735
		internal RenderSetupData setupData;

		// Token: 0x040002E0 RID: 736
		private Mesh customMesh;

		// Token: 0x040002E1 RID: 737
		private NativeArray<Vector3> localPositions;

		// Token: 0x040002E2 RID: 738
		private NativeArray<Vector3> localNormals;

		// Token: 0x040002E3 RID: 739
		private NativeArray<BoneWeight> boneWeights;

		// Token: 0x040002E4 RID: 740
		private BoneWeight centerBoneWeight;

		// Token: 0x040002E5 RID: 741
		[CompilerGenerated]
		private bool <UseCustomMesh>k__BackingField;

		// Token: 0x040002E6 RID: 742
		[CompilerGenerated]
		private bool <ChangeCustomMesh>k__BackingField;

		// Token: 0x040002E7 RID: 743
		[CompilerGenerated]
		private bool <ChangePositionNormal>k__BackingField;

		// Token: 0x040002E8 RID: 744
		[CompilerGenerated]
		private bool <ChangeBoneWeight>k__BackingField;

		// Token: 0x02000073 RID: 115
		[BurstCompile]
		private struct UpdatePositionNormalJob2 : IJobParallelFor
		{
			// Token: 0x060001A8 RID: 424 RVA: 0x0000F664 File Offset: 0x0000D864
			public void Execute(int index)
			{
				int index2 = index + this.startIndex;
				VertexAttribute vertexAttribute = this.mappingAttributes[index2];
				if (vertexAttribute.IsInvalid())
				{
					return;
				}
				if (vertexAttribute.IsFixed())
				{
					return;
				}
				int num = this.mappingReferenceIndices[index2];
				this.meshLocalPositions[num] = this.mappingPositions[index2];
				int index3 = num;
				quaternion quaternion = this.mappingRotations[index2];
				this.meshLocalNormals[index3] = MathUtility.ToNormal(quaternion);
			}

			// Token: 0x040002E9 RID: 745
			public int startIndex;

			// Token: 0x040002EA RID: 746
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> meshLocalPositions;

			// Token: 0x040002EB RID: 747
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> meshLocalNormals;

			// Token: 0x040002EC RID: 748
			[ReadOnly]
			public NativeArray<int> mappingReferenceIndices;

			// Token: 0x040002ED RID: 749
			[ReadOnly]
			public NativeArray<VertexAttribute> mappingAttributes;

			// Token: 0x040002EE RID: 750
			[ReadOnly]
			public NativeArray<float3> mappingPositions;

			// Token: 0x040002EF RID: 751
			[ReadOnly]
			public NativeArray<quaternion> mappingRotations;
		}

		// Token: 0x02000074 RID: 116
		[BurstCompile]
		private struct UpdateBoneWeightJob2 : IJobParallelFor
		{
			// Token: 0x060001A9 RID: 425 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
			public void Execute(int index)
			{
				int index2 = index + this.startIndex;
				VertexAttribute vertexAttribute = this.mappingAttributes[index2];
				if (vertexAttribute.IsInvalid())
				{
					return;
				}
				if (vertexAttribute.IsFixed())
				{
					return;
				}
				int index3 = this.mappingReferenceIndices[index2];
				this.meshBoneWeights[index3] = this.centerBoneWeight;
			}

			// Token: 0x040002F0 RID: 752
			public int startIndex;

			// Token: 0x040002F1 RID: 753
			public BoneWeight centerBoneWeight;

			// Token: 0x040002F2 RID: 754
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<BoneWeight> meshBoneWeights;

			// Token: 0x040002F3 RID: 755
			[ReadOnly]
			public NativeArray<int> mappingReferenceIndices;

			// Token: 0x040002F4 RID: 756
			[ReadOnly]
			public NativeArray<VertexAttribute> mappingAttributes;
		}
	}
}
