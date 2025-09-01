using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FTail
{
	// Token: 0x02000065 RID: 101
	public static class FTail_Skinning
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001F270 File Offset: 0x0001D470
		public static FTail_SkinningVertexData[] CalculateVertexWeightingData(Mesh baseMesh, Transform[] bonesCoords, Vector3 spreadOffset, int weightBoneLimit = 2, float spreadValue = 0.8f, float spreadPower = 0.185f)
		{
			Vector3[] array = new Vector3[bonesCoords.Length];
			Quaternion[] array2 = new Quaternion[bonesCoords.Length];
			for (int i = 0; i < bonesCoords.Length; i++)
			{
				array[i] = bonesCoords[0].parent.InverseTransformPoint(bonesCoords[i].position);
				array2[i] = bonesCoords[0].parent.rotation.QToLocal(bonesCoords[i].rotation);
			}
			return FTail_Skinning.CalculateVertexWeightingData(baseMesh, array, array2, spreadOffset, weightBoneLimit, spreadValue, spreadPower);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		public static FTail_SkinningVertexData[] CalculateVertexWeightingData(Mesh baseMesh, Vector3[] bonesPos, Quaternion[] bonesRot, Vector3 spreadOffset, int weightBoneLimit = 2, float spreadValue = 0.8f, float spreadPower = 0.185f)
		{
			if (weightBoneLimit < 1)
			{
				weightBoneLimit = 1;
			}
			if (weightBoneLimit > 2)
			{
				weightBoneLimit = 2;
			}
			int vertexCount = baseMesh.vertexCount;
			FTail_SkinningVertexData[] array = new FTail_SkinningVertexData[vertexCount];
			Vector3[] array2 = new Vector3[bonesPos.Length];
			for (int i = 0; i < bonesPos.Length - 1; i++)
			{
				array2[i] = bonesPos[i + 1] - bonesPos[i];
			}
			if (array2.Length > 1)
			{
				array2[array2.Length - 1] = array2[array2.Length - 2];
			}
			for (int j = 0; j < vertexCount; j++)
			{
				array[j] = new FTail_SkinningVertexData(baseMesh.vertices[j]);
				array[j].CalculateVertexParameters(bonesPos, bonesRot, array2, weightBoneLimit, spreadValue, spreadOffset, spreadPower);
			}
			return array;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
		public static SkinnedMeshRenderer SkinMesh(Mesh baseMesh, Transform skinParent, Transform[] bonesStructure, FTail_SkinningVertexData[] vertData)
		{
			Vector3[] array = new Vector3[bonesStructure.Length];
			Quaternion[] array2 = new Quaternion[bonesStructure.Length];
			for (int i = 0; i < bonesStructure.Length; i++)
			{
				array[i] = skinParent.InverseTransformPoint(bonesStructure[i].position);
				array2[i] = skinParent.rotation.QToLocal(bonesStructure[i].rotation);
			}
			return FTail_Skinning.SkinMesh(baseMesh, array, array2, vertData);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001F408 File Offset: 0x0001D608
		public static SkinnedMeshRenderer SkinMesh(Mesh baseMesh, Vector3[] bonesPositions, Quaternion[] bonesRotations, FTail_SkinningVertexData[] vertData)
		{
			if (bonesPositions == null)
			{
				return null;
			}
			if (bonesRotations == null)
			{
				return null;
			}
			if (baseMesh == null)
			{
				return null;
			}
			if (vertData == null)
			{
				return null;
			}
			Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(baseMesh);
			mesh.name = baseMesh.name + " [FSKINNED]";
			Transform transform = new GameObject(baseMesh.name + " [FSKINNED]").transform;
			SkinnedMeshRenderer skinnedMeshRenderer = transform.gameObject.AddComponent<SkinnedMeshRenderer>();
			Transform[] array = new Transform[bonesPositions.Length];
			Matrix4x4[] array2 = new Matrix4x4[bonesPositions.Length];
			string text;
			if (baseMesh.name.Length < 6)
			{
				text = baseMesh.name;
			}
			else
			{
				text = baseMesh.name.Substring(0, 5);
			}
			for (int i = 0; i < bonesPositions.Length; i++)
			{
				array[i] = new GameObject(string.Concat(new string[]
				{
					"BoneF-",
					text,
					"[",
					i.ToString(),
					"]"
				})).transform;
				if (i == 0)
				{
					array[i].SetParent(transform, true);
				}
				else
				{
					array[i].SetParent(array[i - 1], true);
				}
				array[i].transform.position = bonesPositions[i];
				array[i].transform.rotation = bonesRotations[i];
				array2[i] = array[i].worldToLocalMatrix * transform.localToWorldMatrix;
			}
			BoneWeight[] array3 = new BoneWeight[mesh.vertexCount];
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] = default(BoneWeight);
			}
			for (int k = 0; k < vertData.Length; k++)
			{
				for (int l = 0; l < vertData[k].weights.Length; l++)
				{
					array3[k] = FTail_Skinning.SetWeightIndex(array3[k], l, vertData[k].bonesIndexes[l]);
					array3[k] = FTail_Skinning.SetWeightToBone(array3[k], l, vertData[k].weights[l]);
				}
			}
			mesh.bindposes = array2;
			mesh.boneWeights = array3;
			List<Vector3> normals = new List<Vector3>();
			List<Vector4> tangents = new List<Vector4>();
			baseMesh.GetNormals(normals);
			baseMesh.GetTangents(tangents);
			mesh.SetNormals(normals);
			mesh.SetTangents(tangents);
			mesh.bounds = baseMesh.bounds;
			skinnedMeshRenderer.sharedMesh = mesh;
			skinnedMeshRenderer.rootBone = array[0];
			skinnedMeshRenderer.bones = array;
			return skinnedMeshRenderer;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001F676 File Offset: 0x0001D876
		public static BoneWeight SetWeightIndex(BoneWeight weight, int bone = 0, int index = 0)
		{
			switch (bone)
			{
			case 1:
				weight.boneIndex1 = index;
				break;
			case 2:
				weight.boneIndex2 = index;
				break;
			case 3:
				weight.boneIndex3 = index;
				break;
			default:
				weight.boneIndex0 = index;
				break;
			}
			return weight;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001F6B5 File Offset: 0x0001D8B5
		public static BoneWeight SetWeightToBone(BoneWeight weight, int bone = 0, float value = 1f)
		{
			switch (bone)
			{
			case 1:
				weight.weight1 = value;
				break;
			case 2:
				weight.weight2 = value;
				break;
			case 3:
				weight.weight3 = value;
				break;
			default:
				weight.weight0 = value;
				break;
			}
			return weight;
		}
	}
}
