using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	// Token: 0x02000076 RID: 118
	public class RenderSetupData : IDisposable, ITransform
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000FABB File Offset: 0x0000DCBB
		public bool IsSuccess()
		{
			return this.result.IsSuccess();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public bool IsFaild()
		{
			return this.result.IsFaild();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000FAD5 File Offset: 0x0000DCD5
		public int TransformCount
		{
			get
			{
				List<Transform> list = this.transformList;
				if (list == null)
				{
					return 0;
				}
				return list.Count;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public RenderSetupData(Renderer ren)
		{
			this.result.Clear();
			this.setupType = RenderSetupData.SetupType.Mesh;
			if (ren == null)
			{
				object obj = "Renderer is null!";
				Develop.LogWarning(obj);
				this.result.SetError(Define.Result.RenderSetup_InvalidSource);
				return;
			}
			this.name = ren.name;
			SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
			this.isSkinning = skinnedMeshRenderer;
			this.skinRenderer = skinnedMeshRenderer;
			Transform transform = ren.transform;
			if (this.isSkinning)
			{
				Transform[] bones = skinnedMeshRenderer.bones;
				this.skinBoneCount = bones.Length;
				this.transformList = new List<Transform>(this.skinBoneCount + 2);
				this.transformList.AddRange(bones);
				Transform item = skinnedMeshRenderer.rootBone ? skinnedMeshRenderer.rootBone : transform;
				this.skinRootBoneIndex = this.transformList.Count;
				this.transformList.Add(item);
				this.renderTransformIndex = this.transformList.Count;
				this.transformList.Add(transform);
			}
			else
			{
				this.skinBoneCount = 1;
				this.transformList = new List<Transform>(this.skinBoneCount);
				this.transformList.Add(transform);
				this.skinRootBoneIndex = 0;
				this.renderTransformIndex = 0;
			}
			this.ReadTransformInformation(false);
			Mesh sharedMesh;
			if (this.isSkinning)
			{
				sharedMesh = skinnedMeshRenderer.sharedMesh;
				if (sharedMesh == null)
				{
					object obj = "SkinnedMeshRenderer.sharedMesh is null!";
					Develop.LogWarning(obj);
					this.result.SetError(Define.Result.RenderSetup_NoMeshOnRenderer);
					return;
				}
				this.bindPoseList = new List<Matrix4x4>(sharedMesh.bindposes);
				NativeArray<BoneWeight1> allBoneWeights = sharedMesh.GetAllBoneWeights();
				NativeArray<byte> bonesPerVertex = sharedMesh.GetBonesPerVertex();
				this.boneWeightArray = new NativeArray<BoneWeight1>(allBoneWeights, Allocator.Persistent);
				this.bonesPerVertexArray = new NativeArray<byte>(bonesPerVertex, Allocator.Persistent);
			}
			else
			{
				MeshFilter component = ren.GetComponent<MeshFilter>();
				if (component == null)
				{
					this.result.SetError(Define.Result.RenderSetup_InvalidSource);
					return;
				}
				sharedMesh = component.sharedMesh;
				if (sharedMesh == null)
				{
					object obj = "MeshFilter.sharedMesh is null!";
					Develop.LogWarning(obj);
					this.result.SetError(Define.Result.RenderSetup_NoMeshOnRenderer);
					return;
				}
				this.bindPoseList = new List<Matrix4x4>(1);
				this.bindPoseList.Add(Matrix4x4.identity);
				this.meshFilter = component;
			}
			if (!sharedMesh.isReadable)
			{
				this.result.SetError(Define.Result.RenderSetup_Unreadable);
				return;
			}
			if (sharedMesh.vertexCount > 65535)
			{
				this.result.SetError(Define.Result.RenderSetup_Over65535vertices);
				return;
			}
			this.meshDataArray = Mesh.AcquireReadOnlyMeshData(sharedMesh);
			this.renderer = ren;
			this.originalMesh = sharedMesh;
			this.vertexCount = sharedMesh.vertexCount;
			this.result.SetSuccess();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public RenderSetupData(Transform renderTransform, List<Transform> rootTransforms, RenderSetupData.BoneConnectionMode connectionMode = RenderSetupData.BoneConnectionMode.Line, string name = "(no name)")
		{
			this.result.Clear();
			try
			{
				this.setupType = RenderSetupData.SetupType.Bone;
				this.boneConnectionMode = connectionMode;
				if (renderTransform == null)
				{
					this.result.SetError(Define.Result.RenderSetup_InvalidSource);
				}
				else if (rootTransforms == null || rootTransforms.Count == 0)
				{
					this.result.SetError(Define.Result.RenderSetup_InvalidSource);
				}
				else
				{
					this.name = name;
					Dictionary<Transform, int> dictionary = new Dictionary<Transform, int>(256);
					this.transformList = new List<Transform>(1024);
					Stack<Transform> stack = new Stack<Transform>(1024);
					using (List<Transform>.Enumerator enumerator = rootTransforms.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Transform item = enumerator.Current;
							stack.Push(item);
						}
						goto IL_126;
					}
					IL_C7:
					Transform transform = stack.Pop();
					if (!dictionary.ContainsKey(transform))
					{
						int count = this.transformList.Count;
						this.transformList.Add(transform);
						dictionary.Add(transform, count);
						int childCount = transform.childCount;
						for (int i = 0; i < childCount; i++)
						{
							stack.Push(transform.GetChild(i));
						}
					}
					IL_126:
					if (stack.Count > 0)
					{
						goto IL_C7;
					}
					this.rootTransformIdList = new List<int>(rootTransforms.Count);
					foreach (Transform transform2 in rootTransforms)
					{
						this.rootTransformIdList.Add(transform2.GetInstanceID());
					}
					this.skinBoneCount = this.transformList.Count;
					this.renderTransformIndex = this.transformList.Count;
					this.transformList.Add(renderTransform);
					this.ReadTransformInformation(true);
					this.result.SetSuccess();
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!this.result.IsError())
				{
					this.result.SetError(Define.Result.RenderSetup_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				this.result.SetError(Define.Result.RenderSetup_Exception);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		private void ReadTransformInformation(bool includeChilds)
		{
			int count = this.transformList.Count;
			using (TransformAccessArray transforms = new TransformAccessArray(this.transformList.ToArray(), -1))
			{
				this.transformPositions = new NativeArray<float3>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.transformRotations = new NativeArray<quaternion>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.transformLocalPositins = new NativeArray<float3>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.transformLocalRotations = new NativeArray<quaternion>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.transformScales = new NativeArray<float3>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.transformInverseRotations = new NativeArray<quaternion>(count, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				new RenderSetupData.ReadTransformJob
				{
					positions = this.transformPositions,
					rotations = this.transformRotations,
					scales = this.transformScales,
					localPositions = this.transformLocalPositins,
					localRotations = this.transformLocalRotations,
					inverseRotations = this.transformInverseRotations
				}.RunReadOnly(transforms);
				this.initRenderLocalToWorld = this.GetRendeerLocalToWorldMatrix();
				this.initRenderWorldtoLocal = math.inverse(this.initRenderLocalToWorld);
				this.initRenderRotation = this.transformRotations[this.renderTransformIndex];
				this.initRenderScale = this.transformScales[this.renderTransformIndex];
				this.transformIdList = new List<int>(count);
				this.transformParentIdList = new List<int>(count);
				for (int i = 0; i < count; i++)
				{
					Transform transform = this.transformList[i];
					this.transformIdList.Add((transform != null) ? transform.GetInstanceID() : 0);
					List<int> list = this.transformParentIdList;
					int? num;
					if (transform == null)
					{
						num = null;
					}
					else
					{
						Transform parent = transform.parent;
						num = ((parent != null) ? new int?(parent.GetInstanceID()) : null);
					}
					int? num2 = num;
					list.Add(num2.GetValueOrDefault());
				}
				if (includeChilds)
				{
					this.transformChildIdList = new List<FixedList512Bytes<int>>(count);
					for (int j = 0; j < count; j++)
					{
						Transform transform2 = this.transformList[j];
						FixedList512Bytes<int> item = default(FixedList512Bytes<int>);
						if (transform2 != null && transform2.childCount > 0)
						{
							for (int k = 0; k < transform2.childCount; k++)
							{
								Transform child = transform2.GetChild(k);
								int instanceID = child.GetInstanceID();
								item.Add(instanceID);
							}
						}
						this.transformChildIdList.Add(item);
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00010264 File Offset: 0x0000E464
		public void Dispose()
		{
			if (this.bonesPerVertexArray.IsCreated)
			{
				this.bonesPerVertexArray.Dispose();
			}
			if (this.boneWeightArray.IsCreated)
			{
				this.boneWeightArray.Dispose();
			}
			if (this.transformPositions.IsCreated)
			{
				this.transformPositions.Dispose();
			}
			if (this.transformRotations.IsCreated)
			{
				this.transformRotations.Dispose();
			}
			if (this.transformLocalPositins.IsCreated)
			{
				this.transformLocalPositins.Dispose();
			}
			if (this.transformLocalRotations.IsCreated)
			{
				this.transformLocalRotations.Dispose();
			}
			if (this.transformScales.IsCreated)
			{
				this.transformScales.Dispose();
			}
			if (this.transformInverseRotations.IsCreated)
			{
				this.transformInverseRotations.Dispose();
			}
			if (this.setupType == RenderSetupData.SetupType.Mesh && this.meshDataArray.Length > 0)
			{
				this.meshDataArray.Dispose();
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00010354 File Offset: 0x0000E554
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			foreach (Transform item in this.transformList)
			{
				transformSet.Add(item);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (this.rootTransformIdList != null)
			{
				for (int i = 0; i < this.rootTransformIdList.Count; i++)
				{
					int key = this.rootTransformIdList[i];
					if (replaceDict.ContainsKey(key))
					{
						this.rootTransformIdList[i] = replaceDict[key].GetInstanceID();
					}
				}
			}
			for (int j = 0; j < this.TransformCount; j++)
			{
				int key2 = this.transformIdList[j];
				if (replaceDict.ContainsKey(key2))
				{
					Transform transform = replaceDict[key2];
					this.transformIdList[j] = transform.GetInstanceID();
					this.transformList[j] = transform;
				}
				int key3 = this.transformParentIdList[j];
				if (replaceDict.ContainsKey(key3))
				{
					Transform transform2 = replaceDict[key3];
					this.transformParentIdList[j] = transform2.GetInstanceID();
				}
				if (this.transformChildIdList != null)
				{
					FixedList512Bytes<int> value = this.transformChildIdList[j];
					for (int k = 0; k < value.Length; k++)
					{
						int key4 = value[k];
						if (replaceDict.ContainsKey(key4))
						{
							Transform transform3 = replaceDict[key4];
							value[k] = transform3.GetInstanceID();
						}
					}
					this.transformChildIdList[j] = value;
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000104F6 File Offset: 0x0000E6F6
		public Transform GetRendeerTransform()
		{
			return this.transformList[this.renderTransformIndex];
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010509 File Offset: 0x0000E709
		public int GetRenderTransformId()
		{
			return this.transformIdList[this.renderTransformIndex];
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001051C File Offset: 0x0000E71C
		public float4x4 GetRendeerLocalToWorldMatrix()
		{
			float3 translation = this.transformPositions[this.renderTransformIndex];
			quaternion rotation = this.transformRotations[this.renderTransformIndex];
			float3 scale = this.transformScales[this.renderTransformIndex];
			return float4x4.TRS(translation, rotation, scale);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00010565 File Offset: 0x0000E765
		public Transform GetSkinRootTransform()
		{
			return this.transformList[this.skinRootBoneIndex];
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010578 File Offset: 0x0000E778
		public int GetSkinRootTransformId()
		{
			return this.transformIdList[this.skinRootBoneIndex];
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001058B File Offset: 0x0000E78B
		public int GetTransformIndexFromId(int id)
		{
			return this.transformIdList.IndexOf(id);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001059C File Offset: 0x0000E79C
		public int GetParentTransformIndex(int index, bool centerExcluded)
		{
			int item = this.transformParentIdList[index];
			int num = this.transformIdList.IndexOf(item);
			if (centerExcluded && num == this.renderTransformIndex)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public void GetBoneWeightsRun(NativeArray<BoneWeight> weights)
		{
			new RenderSetupData.GetBoneWeightJos
			{
				vcnt = this.vertexCount,
				bonesPerVertexArray = this.bonesPerVertexArray,
				boneWeightArray = this.boneWeightArray,
				boneWeights = weights
			}.Run<RenderSetupData.GetBoneWeightJos>();
		}

		// Token: 0x040002F7 RID: 759
		public ResultCode result;

		// Token: 0x040002F8 RID: 760
		public string name = string.Empty;

		// Token: 0x040002F9 RID: 761
		public RenderSetupData.SetupType setupType;

		// Token: 0x040002FA RID: 762
		public Renderer renderer;

		// Token: 0x040002FB RID: 763
		public SkinnedMeshRenderer skinRenderer;

		// Token: 0x040002FC RID: 764
		public MeshFilter meshFilter;

		// Token: 0x040002FD RID: 765
		public Mesh originalMesh;

		// Token: 0x040002FE RID: 766
		public int vertexCount;

		// Token: 0x040002FF RID: 767
		public bool isSkinning;

		// Token: 0x04000300 RID: 768
		public Mesh.MeshDataArray meshDataArray;

		// Token: 0x04000301 RID: 769
		public int skinRootBoneIndex;

		// Token: 0x04000302 RID: 770
		public int skinBoneCount;

		// Token: 0x04000303 RID: 771
		public List<Matrix4x4> bindPoseList;

		// Token: 0x04000304 RID: 772
		public NativeArray<byte> bonesPerVertexArray;

		// Token: 0x04000305 RID: 773
		public NativeArray<BoneWeight1> boneWeightArray;

		// Token: 0x04000306 RID: 774
		public List<int> rootTransformIdList;

		// Token: 0x04000307 RID: 775
		public RenderSetupData.BoneConnectionMode boneConnectionMode;

		// Token: 0x04000308 RID: 776
		public List<Transform> transformList;

		// Token: 0x04000309 RID: 777
		public List<int> transformIdList;

		// Token: 0x0400030A RID: 778
		public List<int> transformParentIdList;

		// Token: 0x0400030B RID: 779
		public List<FixedList512Bytes<int>> transformChildIdList;

		// Token: 0x0400030C RID: 780
		public NativeArray<float3> transformPositions;

		// Token: 0x0400030D RID: 781
		public NativeArray<quaternion> transformRotations;

		// Token: 0x0400030E RID: 782
		public NativeArray<float3> transformLocalPositins;

		// Token: 0x0400030F RID: 783
		public NativeArray<quaternion> transformLocalRotations;

		// Token: 0x04000310 RID: 784
		public NativeArray<float3> transformScales;

		// Token: 0x04000311 RID: 785
		public NativeArray<quaternion> transformInverseRotations;

		// Token: 0x04000312 RID: 786
		public int renderTransformIndex;

		// Token: 0x04000313 RID: 787
		public float4x4 initRenderLocalToWorld;

		// Token: 0x04000314 RID: 788
		public float4x4 initRenderWorldtoLocal;

		// Token: 0x04000315 RID: 789
		public quaternion initRenderRotation;

		// Token: 0x04000316 RID: 790
		public float3 initRenderScale;

		// Token: 0x02000077 RID: 119
		public enum SetupType
		{
			// Token: 0x04000318 RID: 792
			Mesh,
			// Token: 0x04000319 RID: 793
			Bone
		}

		// Token: 0x02000078 RID: 120
		public enum BoneConnectionMode
		{
			// Token: 0x0400031B RID: 795
			Line,
			// Token: 0x0400031C RID: 796
			AutomaticMesh,
			// Token: 0x0400031D RID: 797
			SequentialLoopMesh,
			// Token: 0x0400031E RID: 798
			SequentialNonLoopMesh
		}

		// Token: 0x02000079 RID: 121
		[BurstCompile]
		private struct ReadTransformJob : IJobParallelForTransform
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x00010620 File Offset: 0x0000E820
			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				float4x4 b = transform.localToWorldMatrix;
				this.positions[index] = position;
				this.rotations[index] = rotation;
				this.localPositions[index] = transform.localPosition;
				this.localRotations[index] = transform.localRotation;
				quaternion quaternion = math.inverse(rotation);
				float4x4 float4x = math.mul(new float4x4(quaternion, float3.zero), b);
				float3 value = new float3(float4x.c0.x, float4x.c1.y, float4x.c2.z);
				this.scales[index] = value;
				this.inverseRotations[index] = quaternion;
			}

			// Token: 0x0400031F RID: 799
			[WriteOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000320 RID: 800
			[WriteOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x04000321 RID: 801
			[WriteOnly]
			public NativeArray<float3> scales;

			// Token: 0x04000322 RID: 802
			[WriteOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x04000323 RID: 803
			[WriteOnly]
			public NativeArray<quaternion> localRotations;

			// Token: 0x04000324 RID: 804
			[WriteOnly]
			public NativeArray<quaternion> inverseRotations;
		}

		// Token: 0x0200007A RID: 122
		[BurstCompile]
		private struct GetBoneWeightJos : IJob
		{
			// Token: 0x060001C8 RID: 456 RVA: 0x00010710 File Offset: 0x0000E910
			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < this.vcnt; i++)
				{
					BoneWeight value = default(BoneWeight);
					byte b = this.bonesPerVertexArray[i];
					for (int j = 0; j < (int)b; j++)
					{
						BoneWeight1 boneWeight = this.boneWeightArray[num];
						num++;
						switch (j)
						{
						case 0:
							value.weight0 = boneWeight.weight;
							value.boneIndex0 = boneWeight.boneIndex;
							break;
						case 1:
							value.weight1 = boneWeight.weight;
							value.boneIndex1 = boneWeight.boneIndex;
							break;
						case 2:
							value.weight2 = boneWeight.weight;
							value.boneIndex2 = boneWeight.boneIndex;
							break;
						case 3:
							value.weight3 = boneWeight.weight;
							value.boneIndex3 = boneWeight.boneIndex;
							break;
						}
					}
					this.boneWeights[i] = value;
				}
			}

			// Token: 0x04000325 RID: 805
			public int vcnt;

			// Token: 0x04000326 RID: 806
			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			// Token: 0x04000327 RID: 807
			[ReadOnly]
			public NativeArray<BoneWeight1> boneWeightArray;

			// Token: 0x04000328 RID: 808
			[WriteOnly]
			public NativeArray<BoneWeight> boneWeights;
		}
	}
}
