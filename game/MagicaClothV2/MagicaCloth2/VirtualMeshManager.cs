using System;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200009E RID: 158
	public class VirtualMeshManager : IManager, IDisposable, IValid
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0001866F File Offset: 0x0001686F
		public int VertexCount
		{
			get
			{
				ExNativeArray<short> exNativeArray = this.teamIds;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00018682 File Offset: 0x00016882
		public int TriangleCount
		{
			get
			{
				ExNativeArray<int3> exNativeArray = this.triangles;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00018695 File Offset: 0x00016895
		public int EdgeCount
		{
			get
			{
				ExNativeArray<int2> exNativeArray = this.edges;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000186A8 File Offset: 0x000168A8
		public int BaseLineCount
		{
			get
			{
				ExNativeArray<ExBitFlag8> exNativeArray = this.baseLineFlags;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000186BB File Offset: 0x000168BB
		public int MeshClothVertexCount
		{
			get
			{
				ExNativeArray<float3> exNativeArray = this.localPositions;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000186CE File Offset: 0x000168CE
		public int MappingVertexCount
		{
			get
			{
				ExNativeArray<short> exNativeArray = this.mappingIdArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000186E4 File Offset: 0x000168E4
		public void Dispose()
		{
			this.isValid = false;
			ExNativeArray<short> exNativeArray = this.teamIds;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<VertexAttribute> exNativeArray2 = this.attributes;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<FixedList32Bytes<int>> exNativeArray3 = this.vertexToTriangles;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<float3> exNativeArray4 = this.vertexBindPosePositions;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray5 = this.vertexBindPoseRotations;
			if (exNativeArray5 != null)
			{
				exNativeArray5.Dispose();
			}
			ExNativeArray<float> exNativeArray6 = this.vertexDepths;
			if (exNativeArray6 != null)
			{
				exNativeArray6.Dispose();
			}
			ExNativeArray<int> exNativeArray7 = this.vertexRootIndices;
			if (exNativeArray7 != null)
			{
				exNativeArray7.Dispose();
			}
			ExNativeArray<float3> exNativeArray8 = this.vertexLocalPositions;
			if (exNativeArray8 != null)
			{
				exNativeArray8.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray9 = this.vertexLocalRotations;
			if (exNativeArray9 != null)
			{
				exNativeArray9.Dispose();
			}
			ExNativeArray<int> exNativeArray10 = this.vertexParentIndices;
			if (exNativeArray10 != null)
			{
				exNativeArray10.Dispose();
			}
			ExNativeArray<uint> exNativeArray11 = this.vertexChildIndexArray;
			if (exNativeArray11 != null)
			{
				exNativeArray11.Dispose();
			}
			ExNativeArray<ushort> exNativeArray12 = this.vertexChildDataArray;
			if (exNativeArray12 != null)
			{
				exNativeArray12.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray13 = this.normalAdjustmentRotations;
			if (exNativeArray13 != null)
			{
				exNativeArray13.Dispose();
			}
			ExNativeArray<float2> exNativeArray14 = this.uv;
			if (exNativeArray14 != null)
			{
				exNativeArray14.Dispose();
			}
			this.teamIds = null;
			this.attributes = null;
			this.vertexToTriangles = null;
			this.vertexBindPosePositions = null;
			this.vertexBindPoseRotations = null;
			this.vertexDepths = null;
			this.vertexRootIndices = null;
			this.vertexLocalPositions = null;
			this.vertexLocalRotations = null;
			this.vertexParentIndices = null;
			this.vertexChildIndexArray = null;
			this.vertexChildDataArray = null;
			this.normalAdjustmentRotations = null;
			this.uv = null;
			ExNativeArray<short> exNativeArray15 = this.triangleTeamIdArray;
			if (exNativeArray15 != null)
			{
				exNativeArray15.Dispose();
			}
			ExNativeArray<int3> exNativeArray16 = this.triangles;
			if (exNativeArray16 != null)
			{
				exNativeArray16.Dispose();
			}
			ExNativeArray<float3> exNativeArray17 = this.triangleNormals;
			if (exNativeArray17 != null)
			{
				exNativeArray17.Dispose();
			}
			ExNativeArray<float3> exNativeArray18 = this.triangleTangents;
			if (exNativeArray18 != null)
			{
				exNativeArray18.Dispose();
			}
			this.triangleTeamIdArray = null;
			this.triangles = null;
			this.triangleNormals = null;
			this.triangleTangents = null;
			ExNativeArray<short> exNativeArray19 = this.edgeTeamIdArray;
			if (exNativeArray19 != null)
			{
				exNativeArray19.Dispose();
			}
			ExNativeArray<int2> exNativeArray20 = this.edges;
			if (exNativeArray20 != null)
			{
				exNativeArray20.Dispose();
			}
			ExNativeArray<ExBitFlag8> exNativeArray21 = this.edgeFlags;
			if (exNativeArray21 != null)
			{
				exNativeArray21.Dispose();
			}
			this.edgeTeamIdArray = null;
			this.edges = null;
			this.edgeFlags = null;
			ExNativeArray<float3> exNativeArray22 = this.positions;
			if (exNativeArray22 != null)
			{
				exNativeArray22.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray23 = this.rotations;
			if (exNativeArray23 != null)
			{
				exNativeArray23.Dispose();
			}
			this.positions = null;
			this.rotations = null;
			ExNativeArray<ExBitFlag8> exNativeArray24 = this.baseLineFlags;
			if (exNativeArray24 != null)
			{
				exNativeArray24.Dispose();
			}
			ExNativeArray<short> exNativeArray25 = this.baseLineTeamIds;
			if (exNativeArray25 != null)
			{
				exNativeArray25.Dispose();
			}
			ExNativeArray<ushort> exNativeArray26 = this.baseLineStartDataIndices;
			if (exNativeArray26 != null)
			{
				exNativeArray26.Dispose();
			}
			ExNativeArray<ushort> exNativeArray27 = this.baseLineDataCounts;
			if (exNativeArray27 != null)
			{
				exNativeArray27.Dispose();
			}
			ExNativeArray<ushort> exNativeArray28 = this.baseLineData;
			if (exNativeArray28 != null)
			{
				exNativeArray28.Dispose();
			}
			this.baseLineFlags = null;
			this.baseLineTeamIds = null;
			this.baseLineStartDataIndices = null;
			this.baseLineDataCounts = null;
			this.baseLineData = null;
			ExNativeArray<float3> exNativeArray29 = this.localPositions;
			if (exNativeArray29 != null)
			{
				exNativeArray29.Dispose();
			}
			ExNativeArray<float3> exNativeArray30 = this.localNormals;
			if (exNativeArray30 != null)
			{
				exNativeArray30.Dispose();
			}
			ExNativeArray<float3> exNativeArray31 = this.localTangents;
			if (exNativeArray31 != null)
			{
				exNativeArray31.Dispose();
			}
			ExNativeArray<VirtualMeshBoneWeight> exNativeArray32 = this.boneWeights;
			if (exNativeArray32 != null)
			{
				exNativeArray32.Dispose();
			}
			ExNativeArray<int> exNativeArray33 = this.skinBoneTransformIndices;
			if (exNativeArray33 != null)
			{
				exNativeArray33.Dispose();
			}
			ExNativeArray<float4x4> exNativeArray34 = this.skinBoneBindPoses;
			if (exNativeArray34 != null)
			{
				exNativeArray34.Dispose();
			}
			this.localPositions = null;
			this.localNormals = null;
			this.localTangents = null;
			this.boneWeights = null;
			this.skinBoneTransformIndices = null;
			this.skinBoneBindPoses = null;
			ExNativeArray<quaternion> exNativeArray35 = this.vertexToTransformRotations;
			if (exNativeArray35 != null)
			{
				exNativeArray35.Dispose();
			}
			this.vertexToTransformRotations = null;
			ExNativeArray<short> exNativeArray36 = this.mappingIdArray;
			if (exNativeArray36 != null)
			{
				exNativeArray36.Dispose();
			}
			ExNativeArray<int> exNativeArray37 = this.mappingReferenceIndices;
			if (exNativeArray37 != null)
			{
				exNativeArray37.Dispose();
			}
			ExNativeArray<VertexAttribute> exNativeArray38 = this.mappingAttributes;
			if (exNativeArray38 != null)
			{
				exNativeArray38.Dispose();
			}
			ExNativeArray<float3> exNativeArray39 = this.mappingLocalPositins;
			if (exNativeArray39 != null)
			{
				exNativeArray39.Dispose();
			}
			ExNativeArray<float3> exNativeArray40 = this.mappingLocalNormals;
			if (exNativeArray40 != null)
			{
				exNativeArray40.Dispose();
			}
			ExNativeArray<float3> exNativeArray41 = this.mappingLocalTangents;
			if (exNativeArray41 != null)
			{
				exNativeArray41.Dispose();
			}
			ExNativeArray<VirtualMeshBoneWeight> exNativeArray42 = this.mappingBoneWeights;
			if (exNativeArray42 != null)
			{
				exNativeArray42.Dispose();
			}
			ExNativeArray<float3> exNativeArray43 = this.mappingPositions;
			if (exNativeArray43 != null)
			{
				exNativeArray43.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray44 = this.mappingRotations;
			if (exNativeArray44 != null)
			{
				exNativeArray44.Dispose();
			}
			this.mappingIdArray = null;
			this.mappingReferenceIndices = null;
			this.mappingAttributes = null;
			this.mappingLocalPositins = null;
			this.mappingLocalNormals = null;
			this.mappingLocalTangents = null;
			this.mappingBoneWeights = null;
			this.mappingPositions = null;
			this.mappingRotations = null;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00018B18 File Offset: 0x00016D18
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00018B20 File Offset: 0x00016D20
		public void Initialize()
		{
			this.Dispose();
			this.teamIds = new ExNativeArray<short>(0, true);
			this.attributes = new ExNativeArray<VertexAttribute>(0, true);
			this.vertexToTriangles = new ExNativeArray<FixedList32Bytes<int>>(0, true);
			this.vertexBindPosePositions = new ExNativeArray<float3>(0, true);
			this.vertexBindPoseRotations = new ExNativeArray<quaternion>(0, true);
			this.vertexDepths = new ExNativeArray<float>(0, true);
			this.vertexRootIndices = new ExNativeArray<int>(0, true);
			this.vertexLocalPositions = new ExNativeArray<float3>(0, true);
			this.vertexLocalRotations = new ExNativeArray<quaternion>(0, true);
			this.vertexParentIndices = new ExNativeArray<int>(0, true);
			this.vertexChildIndexArray = new ExNativeArray<uint>(0, true);
			this.vertexChildDataArray = new ExNativeArray<ushort>(0, true);
			this.normalAdjustmentRotations = new ExNativeArray<quaternion>(0, true);
			this.uv = new ExNativeArray<float2>(0, true);
			this.triangleTeamIdArray = new ExNativeArray<short>(0, true);
			this.triangles = new ExNativeArray<int3>(0, true);
			this.triangleNormals = new ExNativeArray<float3>(0, true);
			this.triangleTangents = new ExNativeArray<float3>(0, true);
			this.edgeTeamIdArray = new ExNativeArray<short>(0, true);
			this.edges = new ExNativeArray<int2>(0, true);
			this.edgeFlags = new ExNativeArray<ExBitFlag8>(0, true);
			this.positions = new ExNativeArray<float3>(0, true);
			this.rotations = new ExNativeArray<quaternion>(0, true);
			this.baseLineFlags = new ExNativeArray<ExBitFlag8>(0, true);
			this.baseLineTeamIds = new ExNativeArray<short>(0, true);
			this.baseLineStartDataIndices = new ExNativeArray<ushort>(0, true);
			this.baseLineDataCounts = new ExNativeArray<ushort>(0, true);
			this.baseLineData = new ExNativeArray<ushort>(0, true);
			this.localPositions = new ExNativeArray<float3>(0, true);
			this.localNormals = new ExNativeArray<float3>(0, true);
			this.localTangents = new ExNativeArray<float3>(0, true);
			this.boneWeights = new ExNativeArray<VirtualMeshBoneWeight>(0, true);
			this.skinBoneTransformIndices = new ExNativeArray<int>(0, true);
			this.skinBoneBindPoses = new ExNativeArray<float4x4>(0, true);
			this.vertexToTransformRotations = new ExNativeArray<quaternion>(0, true);
			this.mappingIdArray = new ExNativeArray<short>(0, true);
			this.mappingReferenceIndices = new ExNativeArray<int>(0, true);
			this.mappingAttributes = new ExNativeArray<VertexAttribute>(0, true);
			this.mappingLocalPositins = new ExNativeArray<float3>(0, true);
			this.mappingLocalNormals = new ExNativeArray<float3>(0, true);
			this.mappingLocalTangents = new ExNativeArray<float3>(0, true);
			this.mappingBoneWeights = new ExNativeArray<VirtualMeshBoneWeight>(0, true);
			this.mappingPositions = new ExNativeArray<float3>(0, true);
			this.mappingRotations = new ExNativeArray<quaternion>(0, true);
			this.isValid = true;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00018D76 File Offset: 0x00016F76
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00018D80 File Offset: 0x00016F80
		public void RegisterProxyMesh(int teamId, VirtualMesh proxyMesh)
		{
			if (!this.isValid)
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			teamData.proxyMeshType = proxyMesh.meshType;
			teamData.proxyTransformChunk = MagicaManager.Bone.AddTransform(proxyMesh.transformData);
			teamData.centerTransformIndex = proxyMesh.centerTransformIndex + teamData.proxyTransformChunk.startIndex;
			int vertexCount = proxyMesh.VertexCount;
			teamData.proxyCommonChunk = this.teamIds.AddRange(vertexCount, (short)teamId);
			this.attributes.AddRange(proxyMesh.attributes);
			this.vertexToTriangles.AddRange<FixedList32Bytes<int>>(proxyMesh.vertexToTriangles);
			this.vertexBindPosePositions.AddRange<float3>(proxyMesh.vertexBindPosePositions);
			this.vertexBindPoseRotations.AddRange<quaternion>(proxyMesh.vertexBindPoseRotations);
			this.vertexDepths.AddRange<float>(proxyMesh.vertexDepths);
			this.vertexRootIndices.AddRange<int>(proxyMesh.vertexRootIndices);
			this.vertexLocalPositions.AddRange<float3>(proxyMesh.vertexLocalPositions);
			this.vertexLocalRotations.AddRange<quaternion>(proxyMesh.vertexLocalRotations);
			this.vertexParentIndices.AddRange<int>(proxyMesh.vertexParentIndices);
			this.vertexChildIndexArray.AddRange<uint>(proxyMesh.vertexChildIndexArray);
			this.normalAdjustmentRotations.AddRange<quaternion>(proxyMesh.normalAdjustmentRotations);
			this.uv.AddRange(proxyMesh.uv);
			this.positions.AddRange(vertexCount);
			this.rotations.AddRange(vertexCount);
			teamData.proxyVertexChildDataChunk = this.vertexChildDataArray.AddRange<ushort>(proxyMesh.vertexChildDataArray);
			if (proxyMesh.TriangleCount > 0)
			{
				teamData.proxyTriangleChunk = this.triangleTeamIdArray.AddRange(proxyMesh.TriangleCount, (short)teamId);
				this.triangles.AddRange(proxyMesh.triangles);
				this.triangleNormals.AddRange(proxyMesh.TriangleCount);
				this.triangleTangents.AddRange(proxyMesh.TriangleCount);
			}
			if (proxyMesh.EdgeCount > 0)
			{
				teamData.proxyEdgeChunk = this.edgeTeamIdArray.AddRange(proxyMesh.EdgeCount, (short)teamId);
				this.edges.AddRange<int2>(proxyMesh.edges);
				this.edgeFlags.AddRange<ExBitFlag8>(proxyMesh.edgeFlags);
			}
			if (proxyMesh.BaseLineCount > 0)
			{
				teamData.baseLineChunk = this.baseLineFlags.AddRange<ExBitFlag8>(proxyMesh.baseLineFlags);
				this.baseLineStartDataIndices.AddRange<ushort>(proxyMesh.baseLineStartDataIndices);
				this.baseLineDataCounts.AddRange<ushort>(proxyMesh.baseLineDataCounts);
				this.baseLineTeamIds.AddRange(proxyMesh.BaseLineCount, (short)teamId);
				teamData.baseLineDataChunk = this.baseLineData.AddRange<ushort>(proxyMesh.baseLineData);
			}
			teamData.proxyMeshChunk = this.localPositions.AddRange(proxyMesh.localPositions);
			this.localNormals.AddRange(proxyMesh.localNormals);
			this.localTangents.AddRange(proxyMesh.localTangents);
			this.boneWeights.AddRange(proxyMesh.boneWeights);
			teamData.proxySkinBoneChunk = this.skinBoneTransformIndices.AddRange(proxyMesh.skinBoneTransformIndices);
			this.skinBoneBindPoses.AddRange(proxyMesh.skinBoneBindPoses);
			if (proxyMesh.meshType == VirtualMesh.MeshType.ProxyBoneMesh)
			{
				teamData.proxyBoneChunk = this.vertexToTransformRotations.AddRange<quaternion>(proxyMesh.vertexToTransformRotations);
			}
			MagicaManager.Team.SetTeamData(teamId, teamData);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000190BC File Offset: 0x000172BC
		public void ExitProxyMesh(int teamId)
		{
			if (!this.isValid)
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			MagicaManager.Bone.RemoveTransform(teamData.proxyTransformChunk);
			teamData.proxyTransformChunk.Clear();
			this.teamIds.RemoveAndFill(teamData.proxyCommonChunk, 0);
			this.attributes.RemoveAndFill(teamData.proxyCommonChunk, default(VertexAttribute));
			this.vertexToTriangles.Remove(teamData.proxyCommonChunk);
			this.vertexBindPosePositions.Remove(teamData.proxyCommonChunk);
			this.vertexBindPoseRotations.Remove(teamData.proxyCommonChunk);
			this.vertexDepths.Remove(teamData.proxyCommonChunk);
			this.vertexRootIndices.Remove(teamData.proxyCommonChunk);
			this.vertexLocalPositions.Remove(teamData.proxyCommonChunk);
			this.vertexLocalRotations.Remove(teamData.proxyCommonChunk);
			this.vertexParentIndices.Remove(teamData.proxyCommonChunk);
			this.vertexChildIndexArray.Remove(teamData.proxyCommonChunk);
			this.normalAdjustmentRotations.Remove(teamData.proxyCommonChunk);
			this.uv.Remove(teamData.proxyCommonChunk);
			teamData.proxyCommonChunk.Clear();
			this.vertexChildDataArray.Remove(teamData.proxyVertexChildDataChunk);
			teamData.proxyVertexChildDataChunk.Clear();
			this.triangleTeamIdArray.RemoveAndFill(teamData.proxyTriangleChunk, 0);
			this.triangles.Remove(teamData.proxyTriangleChunk);
			this.triangleNormals.Remove(teamData.proxyTriangleChunk);
			this.triangleTangents.Remove(teamData.proxyTriangleChunk);
			teamData.proxyTriangleChunk.Clear();
			this.edgeTeamIdArray.RemoveAndFill(teamData.proxyEdgeChunk, 0);
			this.edges.Remove(teamData.proxyEdgeChunk);
			this.edgeFlags.Remove(teamData.proxyEdgeChunk);
			this.baseLineFlags.RemoveAndFill(teamData.baseLineChunk, default(ExBitFlag8));
			this.baseLineTeamIds.Remove(teamData.baseLineChunk);
			this.baseLineStartDataIndices.Remove(teamData.baseLineChunk);
			this.baseLineDataCounts.Remove(teamData.baseLineChunk);
			teamData.baseLineChunk.Clear();
			this.baseLineData.Remove(teamData.baseLineDataChunk);
			teamData.baseLineDataChunk.Clear();
			this.localPositions.Remove(teamData.proxyMeshChunk);
			this.localNormals.Remove(teamData.proxyMeshChunk);
			this.localTangents.Remove(teamData.proxyMeshChunk);
			this.boneWeights.Remove(teamData.proxyMeshChunk);
			teamData.proxyMeshChunk.Clear();
			this.skinBoneTransformIndices.Remove(teamData.proxySkinBoneChunk);
			this.skinBoneBindPoses.Remove(teamData.proxySkinBoneChunk);
			teamData.proxySkinBoneChunk.Clear();
			this.vertexToTransformRotations.Remove(teamData.proxyBoneChunk);
			teamData.proxyBoneChunk.Clear();
			MagicaManager.Team.SetTeamData(teamId, teamData);
			short[] array = teamData.mappingDataIndexSet.ToArray();
			int length = teamData.mappingDataIndexSet.Length;
			for (int i = 0; i < length; i++)
			{
				int mappingIndex = (int)array[i];
				this.ExitMappingMesh(teamId, mappingIndex);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000193E8 File Offset: 0x000175E8
		public DataChunk RegisterMappingMesh(int teamId, VirtualMesh mappingMesh)
		{
			if (!this.isValid)
			{
				return DataChunk.Empty;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			TeamManager.MappingData mappingData = default(TeamManager.MappingData);
			mappingData.teamId = teamId;
			Transform centerTransform = mappingMesh.GetCenterTransform();
			DataChunk dataChunk = MagicaManager.Bone.AddTransform(centerTransform, new ExBitFlag8(17));
			mappingData.centerTransformIndex = dataChunk.startIndex;
			mappingData.toProxyMatrix = mappingMesh.toProxyMatrix;
			mappingData.toProxyRotation = mappingMesh.toProxyRotation;
			dataChunk = MagicaManager.Team.mappingDataArray.Add(mappingData);
			int startIndex = dataChunk.startIndex;
			int vertexCount = mappingMesh.VertexCount;
			mappingData.mappingCommonChunk = this.mappingIdArray.AddRange(vertexCount, (short)(startIndex + 1));
			this.mappingReferenceIndices.AddRange(mappingMesh.referenceIndices);
			this.mappingAttributes.AddRange(mappingMesh.attributes);
			this.mappingLocalPositins.AddRange(mappingMesh.localPositions);
			this.mappingLocalNormals.AddRange(mappingMesh.localNormals);
			this.mappingLocalTangents.AddRange(mappingMesh.localTangents);
			this.mappingBoneWeights.AddRange(mappingMesh.boneWeights);
			this.mappingPositions.AddRange(vertexCount);
			this.mappingRotations.AddRange(vertexCount);
			MagicaManager.Team.mappingDataArray[startIndex] = mappingData;
			ref teamData.mappingDataIndexSet.Set((short)startIndex);
			MagicaManager.Team.SetTeamData(teamId, teamData);
			mappingMesh.mappingId = startIndex;
			return mappingData.mappingCommonChunk;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00019560 File Offset: 0x00017760
		public void ExitMappingMesh(int teamId, int mappingIndex)
		{
			if (!this.isValid)
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			TeamManager.MappingData mappingData = MagicaManager.Team.mappingDataArray[mappingIndex];
			MagicaManager.Bone.RemoveTransform(new DataChunk(mappingData.centerTransformIndex, 1));
			this.mappingIdArray.RemoveAndFill(mappingData.mappingCommonChunk, 0);
			this.mappingReferenceIndices.Remove(mappingData.mappingCommonChunk);
			this.mappingAttributes.Remove(mappingData.mappingCommonChunk);
			this.mappingLocalPositins.Remove(mappingData.mappingCommonChunk);
			this.mappingLocalNormals.Remove(mappingData.mappingCommonChunk);
			this.mappingLocalTangents.Remove(mappingData.mappingCommonChunk);
			this.mappingBoneWeights.Remove(mappingData.mappingCommonChunk);
			this.mappingPositions.Remove(mappingData.mappingCommonChunk);
			this.mappingRotations.Remove(mappingData.mappingCommonChunk);
			ref teamData.mappingDataIndexSet.RemoveItemAtSwapBack((short)mappingIndex);
			MagicaManager.Team.mappingDataArray.RemoveAndFill(new DataChunk(mappingIndex, 1), default(TeamManager.MappingData));
			MagicaManager.Team.SetTeamData(teamId, teamData);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001967C File Offset: 0x0001787C
		internal JobHandle PreProxyMeshUpdate(JobHandle jobHandle)
		{
			if (this.VertexCount == 0)
			{
				return jobHandle;
			}
			SimulationManager simulation = MagicaManager.Simulation;
			TeamManager team = MagicaManager.Team;
			TransformManager bone = MagicaManager.Bone;
			int capacity = math.max(this.VertexCount, this.TriangleCount);
			simulation.processingStepParticle.UpdateBuffer(capacity);
			simulation.processingStepTriangleBending.UpdateBuffer(capacity);
			simulation.processingStepEdgeCollision.UpdateBuffer(this.BaseLineCount);
			jobHandle = new VirtualMeshManager.ClearProxyMeshUpdateBufferJob
			{
				processingCounter0 = simulation.processingStepParticle.Counter,
				processingCounter1 = simulation.processingStepTriangleBending.Counter,
				processingCounter2 = simulation.processingStepEdgeCollision.Counter
			}.Schedule(jobHandle);
			jobHandle = new VirtualMeshManager.CreateProxyMeshUpdateVertexList
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				processingCounter1 = simulation.processingStepTriangleBending.Counter,
				processingList1 = simulation.processingStepTriangleBending.Buffer
			}.Schedule(team.TeamCount, 1, jobHandle);
			jobHandle = new VirtualMeshManager.CalcTransformOnlySkinningJob
			{
				jobVertexIndexList = simulation.processingStepTriangleBending.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				teamIds = this.teamIds.GetNativeArray(),
				attributes = this.attributes.GetNativeArray(),
				localPositions = this.localPositions.GetNativeArray(),
				localNormals = this.localNormals.GetNativeArray(),
				localTangents = this.localTangents.GetNativeArray(),
				boneWeights = this.boneWeights.GetNativeArray(),
				skinBoneTransformIndices = this.skinBoneTransformIndices.GetNativeArray(),
				skinBoneBindPoses = this.skinBoneBindPoses.GetNativeArray(),
				positions = this.positions.GetNativeArray(),
				rotations = this.rotations.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				transformScaleArray = bone.scaleArray.GetNativeArray()
			}.Schedule(simulation.processingStepTriangleBending.GetJobSchedulePtr(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000198A8 File Offset: 0x00017AA8
		internal JobHandle PostProxyMeshUpdate(JobHandle jobHandle)
		{
			if (this.VertexCount == 0)
			{
				return jobHandle;
			}
			SimulationManager simulation = MagicaManager.Simulation;
			TeamManager team = MagicaManager.Team;
			TransformManager bone = MagicaManager.Bone;
			jobHandle = new VirtualMeshManager.ClearProxyMeshUpdateBufferJob
			{
				processingCounter0 = simulation.processingStepParticle.Counter,
				processingCounter1 = simulation.processingStepTriangleBending.Counter,
				processingCounter2 = simulation.processingStepEdgeCollision.Counter
			}.Schedule(jobHandle);
			jobHandle = new VirtualMeshManager.CreatePostProxyMeshUpdateListJob
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				processingCounter0 = simulation.processingStepParticle.Counter,
				processingList0 = simulation.processingStepParticle.Buffer,
				processingCounter1 = simulation.processingStepTriangleBending.Counter,
				processingList1 = simulation.processingStepTriangleBending.Buffer,
				processingCounter2 = simulation.processingStepEdgeCollision.Counter,
				processingList2 = simulation.processingStepEdgeCollision.Buffer
			}.Schedule(team.TeamCount, 1, jobHandle);
			if (this.BaseLineCount > 0)
			{
				jobHandle = new VirtualMeshManager.CalcBaseLineNormalTangentJob
				{
					jobBaseLineList = simulation.processingStepEdgeCollision.Buffer,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = this.attributes.GetNativeArray(),
					positions = this.positions.GetNativeArray(),
					rotations = this.rotations.GetNativeArray(),
					vertexLocalPositions = this.vertexLocalPositions.GetNativeArray(),
					vertexLocalRotations = this.vertexLocalRotations.GetNativeArray(),
					parentIndices = this.vertexParentIndices.GetNativeArray(),
					childIndexArray = this.vertexChildIndexArray.GetNativeArray(),
					childDataArray = this.vertexChildDataArray.GetNativeArray(),
					baseLineFlags = this.baseLineFlags.GetNativeArray(),
					baseLineTeamIds = this.baseLineTeamIds.GetNativeArray(),
					baseLineStartIndices = this.baseLineStartDataIndices.GetNativeArray(),
					baseLineCounts = this.baseLineDataCounts.GetNativeArray(),
					baseLineIndices = this.baseLineData.GetNativeArray()
				}.Schedule(simulation.processingStepEdgeCollision.GetJobSchedulePtr(), 8, jobHandle);
			}
			if (this.TriangleCount > 0)
			{
				jobHandle = new VirtualMeshManager.CalcTriangleNormalTangentJob
				{
					teamDataArray = team.teamDataArray.GetNativeArray(),
					triangleTeamIdArray = this.triangleTeamIdArray.GetNativeArray(),
					triangles = this.triangles.GetNativeArray(),
					outTriangleNormals = this.triangleNormals.GetNativeArray(),
					outTriangleTangents = this.triangleTangents.GetNativeArray(),
					positions = this.positions.GetNativeArray(),
					uv = this.uv.GetNativeArray()
				}.Schedule(this.TriangleCount, 16, jobHandle);
				jobHandle = new VirtualMeshManager.CalcVertexNormalTangentFromTriangleJob
				{
					jobVertexIndexList = simulation.processingStepParticle.Buffer,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					teamIds = this.teamIds.GetNativeArray(),
					triangleNormals = this.triangleNormals.GetNativeArray(),
					triangleTangents = this.triangleTangents.GetNativeArray(),
					vertexToTriangles = this.vertexToTriangles.GetNativeArray(),
					normalAdjustmentRotations = this.normalAdjustmentRotations.GetNativeArray(),
					outRotations = this.rotations.GetNativeArray()
				}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			}
			jobHandle = new VirtualMeshManager.WriteTransformDataJob
			{
				jobVertexIndexList = simulation.processingStepTriangleBending.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				teamIds = this.teamIds.GetNativeArray(),
				positions = this.positions.GetNativeArray(),
				rotations = this.rotations.GetNativeArray(),
				vertexToTransformRotations = this.vertexToTransformRotations.GetNativeArray()
			}.Schedule(simulation.processingStepTriangleBending.GetJobSchedulePtr(), 32, jobHandle);
			jobHandle = new VirtualMeshManager.WriteTransformLocalDataJob
			{
				jobVertexIndexList = simulation.processingStepTriangleBending.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				teamIds = this.teamIds.GetNativeArray(),
				attributes = this.attributes.GetNativeArray(),
				vertexParentIndices = this.vertexParentIndices.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				transformScaleArray = bone.scaleArray.GetNativeArray(),
				transformLocalPositionArray = bone.localPositionArray.GetNativeArray(),
				transformLocalRotationArray = bone.localRotationArray.GetNativeArray()
			}.Schedule(simulation.processingStepTriangleBending.GetJobSchedulePtr(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00019DD0 File Offset: 0x00017FD0
		internal JobHandle PostMappingMeshUpdate(JobHandle jobHandle)
		{
			if (MagicaManager.Team.MappingCount == 0)
			{
				return jobHandle;
			}
			TeamManager team = MagicaManager.Team;
			TransformManager bone = MagicaManager.Bone;
			jobHandle = new VirtualMeshManager.CalcMeshConvertMatrixJob
			{
				mappingDataArray = team.mappingDataArray.GetNativeArray(),
				teamDataArray = team.teamDataArray.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				transformScaleArray = bone.scaleArray.GetNativeArray(),
				transformInverseRotationArray = bone.inverseRotationArray.GetNativeArray()
			}.Schedule(team.MappingCount, 1, jobHandle);
			jobHandle = new VirtualMeshManager.CalcProxySkinningJob
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				mappingDataArray = team.mappingDataArray.GetNativeArray(),
				mappingIdArray = this.mappingIdArray.GetNativeArray(),
				mappingAttributes = this.mappingAttributes.GetNativeArray(),
				mappingLocalPositions = this.mappingLocalPositins.GetNativeArray(),
				mappingLocalNormals = this.mappingLocalNormals.GetNativeArray(),
				mappingLocalTangents = this.mappingLocalTangents.GetNativeArray(),
				mappingBoneWeights = this.mappingBoneWeights.GetNativeArray(),
				mappingPositions = this.mappingPositions.GetNativeArray(),
				mappingRotations = this.mappingRotations.GetNativeArray(),
				proxyPositions = this.positions.GetNativeArray(),
				proxyRotations = this.rotations.GetNativeArray(),
				proxyVertexBindPosePositions = this.vertexBindPosePositions.GetNativeArray(),
				proxyVertexBindPoseRotations = this.vertexBindPoseRotations.GetNativeArray()
			}.Schedule(this.MappingVertexCount, 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00019F94 File Offset: 0x00018194
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("VirtualMesh Manager.");
			stringBuilder.AppendLine(string.Format("  -VertexCount:{0}", this.VertexCount));
			stringBuilder.AppendLine(string.Format("  -EdgeCount:{0}", this.EdgeCount));
			stringBuilder.AppendLine(string.Format("  -TriangleCount:{0}", this.TriangleCount));
			stringBuilder.AppendLine(string.Format("  -BaseLineCount:{0}", this.BaseLineCount));
			stringBuilder.AppendLine(string.Format("  -MeshClothVertexCount:{0}", this.MeshClothVertexCount));
			stringBuilder.AppendLine(string.Format("  -MappingVertexCount:{0}", this.MappingVertexCount));
			return stringBuilder.ToString();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00002058 File Offset: 0x00000258
		public VirtualMeshManager()
		{
		}

		// Token: 0x040004D6 RID: 1238
		public ExNativeArray<short> teamIds;

		// Token: 0x040004D7 RID: 1239
		public ExNativeArray<VertexAttribute> attributes;

		// Token: 0x040004D8 RID: 1240
		public ExNativeArray<FixedList32Bytes<int>> vertexToTriangles;

		// Token: 0x040004D9 RID: 1241
		public ExNativeArray<float3> vertexBindPosePositions;

		// Token: 0x040004DA RID: 1242
		public ExNativeArray<quaternion> vertexBindPoseRotations;

		// Token: 0x040004DB RID: 1243
		public ExNativeArray<float> vertexDepths;

		// Token: 0x040004DC RID: 1244
		public ExNativeArray<int> vertexRootIndices;

		// Token: 0x040004DD RID: 1245
		public ExNativeArray<float3> vertexLocalPositions;

		// Token: 0x040004DE RID: 1246
		public ExNativeArray<quaternion> vertexLocalRotations;

		// Token: 0x040004DF RID: 1247
		public ExNativeArray<int> vertexParentIndices;

		// Token: 0x040004E0 RID: 1248
		public ExNativeArray<uint> vertexChildIndexArray;

		// Token: 0x040004E1 RID: 1249
		public ExNativeArray<ushort> vertexChildDataArray;

		// Token: 0x040004E2 RID: 1250
		public ExNativeArray<quaternion> normalAdjustmentRotations;

		// Token: 0x040004E3 RID: 1251
		public ExNativeArray<float2> uv;

		// Token: 0x040004E4 RID: 1252
		public ExNativeArray<short> triangleTeamIdArray;

		// Token: 0x040004E5 RID: 1253
		public ExNativeArray<int3> triangles;

		// Token: 0x040004E6 RID: 1254
		public ExNativeArray<float3> triangleNormals;

		// Token: 0x040004E7 RID: 1255
		public ExNativeArray<float3> triangleTangents;

		// Token: 0x040004E8 RID: 1256
		public ExNativeArray<short> edgeTeamIdArray;

		// Token: 0x040004E9 RID: 1257
		public ExNativeArray<int2> edges;

		// Token: 0x040004EA RID: 1258
		public ExNativeArray<ExBitFlag8> edgeFlags;

		// Token: 0x040004EB RID: 1259
		public ExNativeArray<ExBitFlag8> baseLineFlags;

		// Token: 0x040004EC RID: 1260
		public ExNativeArray<short> baseLineTeamIds;

		// Token: 0x040004ED RID: 1261
		public ExNativeArray<ushort> baseLineStartDataIndices;

		// Token: 0x040004EE RID: 1262
		public ExNativeArray<ushort> baseLineDataCounts;

		// Token: 0x040004EF RID: 1263
		public ExNativeArray<ushort> baseLineData;

		// Token: 0x040004F0 RID: 1264
		public ExNativeArray<float3> localPositions;

		// Token: 0x040004F1 RID: 1265
		public ExNativeArray<float3> localNormals;

		// Token: 0x040004F2 RID: 1266
		public ExNativeArray<float3> localTangents;

		// Token: 0x040004F3 RID: 1267
		public ExNativeArray<VirtualMeshBoneWeight> boneWeights;

		// Token: 0x040004F4 RID: 1268
		public ExNativeArray<int> skinBoneTransformIndices;

		// Token: 0x040004F5 RID: 1269
		public ExNativeArray<float4x4> skinBoneBindPoses;

		// Token: 0x040004F6 RID: 1270
		public ExNativeArray<quaternion> vertexToTransformRotations;

		// Token: 0x040004F7 RID: 1271
		public ExNativeArray<float3> positions;

		// Token: 0x040004F8 RID: 1272
		public ExNativeArray<quaternion> rotations;

		// Token: 0x040004F9 RID: 1273
		public ExNativeArray<short> mappingIdArray;

		// Token: 0x040004FA RID: 1274
		public ExNativeArray<int> mappingReferenceIndices;

		// Token: 0x040004FB RID: 1275
		public ExNativeArray<VertexAttribute> mappingAttributes;

		// Token: 0x040004FC RID: 1276
		public ExNativeArray<float3> mappingLocalPositins;

		// Token: 0x040004FD RID: 1277
		public ExNativeArray<float3> mappingLocalNormals;

		// Token: 0x040004FE RID: 1278
		public ExNativeArray<float3> mappingLocalTangents;

		// Token: 0x040004FF RID: 1279
		public ExNativeArray<VirtualMeshBoneWeight> mappingBoneWeights;

		// Token: 0x04000500 RID: 1280
		public ExNativeArray<float3> mappingPositions;

		// Token: 0x04000501 RID: 1281
		public ExNativeArray<quaternion> mappingRotations;

		// Token: 0x04000502 RID: 1282
		private bool isValid;

		// Token: 0x0200009F RID: 159
		[BurstCompile]
		private struct ClearProxyMeshUpdateBufferJob : IJob
		{
			// Token: 0x06000280 RID: 640 RVA: 0x0001A05F File Offset: 0x0001825F
			public void Execute()
			{
				this.processingCounter0.Value = 0;
				this.processingCounter1.Value = 0;
				this.processingCounter2.Value = 0;
			}

			// Token: 0x04000503 RID: 1283
			public NativeReference<int> processingCounter0;

			// Token: 0x04000504 RID: 1284
			public NativeReference<int> processingCounter1;

			// Token: 0x04000505 RID: 1285
			public NativeReference<int> processingCounter2;
		}

		// Token: 0x020000A0 RID: 160
		[BurstCompile]
		private struct CreateProxyMeshUpdateVertexList : IJobParallelFor
		{
			// Token: 0x06000281 RID: 641 RVA: 0x0001A088 File Offset: 0x00018288
			public void Execute(int teamId)
			{
				if (teamId == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsEnable)
				{
					return;
				}
				DataChunk proxyCommonChunk = teamData.proxyCommonChunk;
				if (proxyCommonChunk.dataLength == 0)
				{
					return;
				}
				int num = ref this.processingCounter1.InterlockedStartIndex(proxyCommonChunk.dataLength);
				for (int i = 0; i < proxyCommonChunk.dataLength; i++)
				{
					int value = proxyCommonChunk.startIndex + i;
					this.processingList1[num + i] = value;
				}
			}

			// Token: 0x04000506 RID: 1286
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000507 RID: 1287
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> processingCounter1;

			// Token: 0x04000508 RID: 1288
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> processingList1;
		}

		// Token: 0x020000A1 RID: 161
		[BurstCompile]
		private struct CalcTransformOnlySkinningJob : IJobParallelForDefer
		{
			// Token: 0x06000282 RID: 642 RVA: 0x0001A100 File Offset: 0x00018300
			public void Execute(int index)
			{
				int num = this.jobVertexIndexList[index];
				int index2 = (int)this.teamIds[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				int num2 = num - teamData.proxyCommonChunk.startIndex;
				int index3 = teamData.proxyMeshChunk.startIndex + num2;
				int startIndex = teamData.proxySkinBoneChunk.startIndex;
				int startIndex2 = teamData.proxyTransformChunk.startIndex;
				VirtualMeshBoneWeight virtualMeshBoneWeight = this.boneWeights[index3];
				int count = virtualMeshBoneWeight.Count;
				float3 @float = 0;
				float3 float2 = 0;
				float3 float3 = 0;
				for (int i = 0; i < count; i++)
				{
					float rhs = virtualMeshBoneWeight.weights[i];
					int num3 = virtualMeshBoneWeight.boneIndices[i];
					float4x4 a = this.skinBoneBindPoses[startIndex + num3];
					float4 b = new float4(this.localPositions[index3], 1f);
					float4 b2 = new float4(this.localNormals[index3], 0f);
					float4 b3 = new float4(this.localTangents[index3], 0f);
					float3 xyz = math.mul(a, b).xyz;
					float3 xyz2 = math.mul(a, b2).xyz;
					float3 xyz3 = math.mul(a, b3).xyz;
					int index4 = this.skinBoneTransformIndices[startIndex + num3] + startIndex2;
					float3 float4 = this.transformPositionArray[index4];
					quaternion quaternion = this.transformRotationArray[index4];
					float3 float5 = this.transformScaleArray[index4];
					MathUtility.TransformPositionNormalTangent(float4, quaternion, float5, ref xyz, ref xyz2, ref xyz3);
					@float += xyz * rhs;
					float2 += xyz2 * rhs;
					float3 += xyz3 * rhs;
				}
				float2 = math.normalize(float2);
				float3 = math.normalize(float3);
				quaternion value = MathUtility.ToRotation(float2, float3);
				this.positions[num] = @float;
				this.rotations[num] = value;
			}

			// Token: 0x04000509 RID: 1289
			[ReadOnly]
			public NativeArray<int> jobVertexIndexList;

			// Token: 0x0400050A RID: 1290
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400050B RID: 1291
			[ReadOnly]
			public NativeArray<short> teamIds;

			// Token: 0x0400050C RID: 1292
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400050D RID: 1293
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x0400050E RID: 1294
			[ReadOnly]
			public NativeArray<float3> localNormals;

			// Token: 0x0400050F RID: 1295
			[ReadOnly]
			public NativeArray<float3> localTangents;

			// Token: 0x04000510 RID: 1296
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			// Token: 0x04000511 RID: 1297
			[ReadOnly]
			public NativeArray<int> skinBoneTransformIndices;

			// Token: 0x04000512 RID: 1298
			[ReadOnly]
			public NativeArray<float4x4> skinBoneBindPoses;

			// Token: 0x04000513 RID: 1299
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000514 RID: 1300
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x04000515 RID: 1301
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000516 RID: 1302
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000517 RID: 1303
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;
		}

		// Token: 0x020000A2 RID: 162
		[BurstCompile]
		private struct CreatePostProxyMeshUpdateListJob : IJobParallelFor
		{
			// Token: 0x06000283 RID: 643 RVA: 0x0001A328 File Offset: 0x00018528
			public void Execute(int teamId)
			{
				if (teamId == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsEnable)
				{
					return;
				}
				if (teamData.TriangleCount > 0 && teamData.proxyCommonChunk.IsValid)
				{
					int num = ref this.processingCounter0.InterlockedStartIndex(teamData.proxyCommonChunk.dataLength);
					for (int i = 0; i < teamData.proxyCommonChunk.dataLength; i++)
					{
						int value = teamData.proxyCommonChunk.startIndex + i;
						this.processingList0[num + i] = value;
					}
				}
				if (teamData.proxyMeshType == VirtualMesh.MeshType.ProxyBoneMesh)
				{
					int num2 = ref this.processingCounter1.InterlockedStartIndex(teamData.proxyCommonChunk.dataLength);
					for (int j = 0; j < teamData.proxyCommonChunk.dataLength; j++)
					{
						int value2 = teamData.proxyCommonChunk.startIndex + j;
						this.processingList1[num2 + j] = value2;
					}
				}
				if (teamData.baseLineChunk.IsValid)
				{
					int num3 = ref this.processingCounter2.InterlockedStartIndex(teamData.baseLineChunk.dataLength);
					for (int k = 0; k < teamData.baseLineChunk.dataLength; k++)
					{
						int value3 = teamData.baseLineChunk.startIndex + k;
						this.processingList2[num3 + k] = value3;
					}
				}
			}

			// Token: 0x04000518 RID: 1304
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000519 RID: 1305
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> processingCounter0;

			// Token: 0x0400051A RID: 1306
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> processingList0;

			// Token: 0x0400051B RID: 1307
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> processingCounter1;

			// Token: 0x0400051C RID: 1308
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> processingList1;

			// Token: 0x0400051D RID: 1309
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> processingCounter2;

			// Token: 0x0400051E RID: 1310
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> processingList2;
		}

		// Token: 0x020000A3 RID: 163
		[BurstCompile]
		private struct CalcBaseLineNormalTangentJob : IJobParallelForDefer
		{
			// Token: 0x06000284 RID: 644 RVA: 0x0001A474 File Offset: 0x00018674
			public void Execute(int index)
			{
				int index2 = this.jobBaseLineList[index];
				if (!this.baseLineFlags[index2].IsSet(1))
				{
					return;
				}
				int num = (int)this.baseLineTeamIds[index2];
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				if (!teamData.IsEnable)
				{
					return;
				}
				ClothParameters clothParameters = this.parameterArray[num];
				float rotationalInterpolation = clothParameters.rotationalInterpolation;
				float rootRotation = clothParameters.rootRotation;
				int startIndex = teamData.proxyCommonChunk.startIndex;
				int startIndex2 = teamData.baseLineDataChunk.startIndex;
				int startIndex3 = teamData.proxyVertexChildDataChunk.startIndex;
				int num2 = (int)this.baseLineStartIndices[index2] + startIndex2;
				int num3 = (int)this.baseLineCounts[index2];
				int i = 0;
				while (i < num3)
				{
					int index3 = (int)this.baseLineIndices[num2] + startIndex;
					float3 rhs = this.positions[index3];
					quaternion quaternion = this.rotations[index3];
					VertexAttribute vertexAttribute = this.attributes[index3];
					uint pack = this.childIndexArray[index3];
					int num4 = DataUtility.Unpack10_22Low(pack);
					int num5 = DataUtility.Unpack10_22Hi(pack);
					int num6 = 0;
					if (num5 > 0)
					{
						float3 lhs = 0;
						float3 lhs2 = 0;
						for (int j = 0; j < num5; j++)
						{
							int index4 = (int)this.childDataArray[startIndex3 + num4 + j] + startIndex;
							VertexAttribute vertexAttribute2 = this.attributes[index4];
							float3 lhs3 = this.positions[index4];
							float3 rhs2 = math.mul(quaternion, this.vertexLocalPositions[index4]);
							lhs += rhs2;
							if (vertexAttribute2.IsMove())
							{
								float3 rhs3 = lhs3 - rhs;
								lhs2 += rhs3;
								quaternion a = MathUtility.FromToRotation(rhs2, rhs3, 1f);
								quaternion quaternion2 = math.mul(quaternion, this.vertexLocalRotations[index4]);
								quaternion2 = math.mul(a, quaternion2);
								this.rotations[index4] = quaternion2;
								num6++;
							}
							else
							{
								lhs2 += rhs2;
							}
						}
						if (num6 != 0)
						{
							float t = vertexAttribute.IsMove() ? rotationalInterpolation : rootRotation;
							quaternion = math.mul(MathUtility.FromToRotation(lhs, lhs2, t), quaternion);
							this.rotations[index3] = quaternion;
						}
					}
					i++;
					num2++;
				}
			}

			// Token: 0x0400051F RID: 1311
			[ReadOnly]
			public NativeArray<int> jobBaseLineList;

			// Token: 0x04000520 RID: 1312
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000521 RID: 1313
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x04000522 RID: 1314
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000523 RID: 1315
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000524 RID: 1316
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> rotations;

			// Token: 0x04000525 RID: 1317
			[ReadOnly]
			public NativeArray<float3> vertexLocalPositions;

			// Token: 0x04000526 RID: 1318
			[ReadOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			// Token: 0x04000527 RID: 1319
			[ReadOnly]
			public NativeArray<int> parentIndices;

			// Token: 0x04000528 RID: 1320
			[ReadOnly]
			public NativeArray<uint> childIndexArray;

			// Token: 0x04000529 RID: 1321
			[ReadOnly]
			public NativeArray<ushort> childDataArray;

			// Token: 0x0400052A RID: 1322
			[ReadOnly]
			public NativeArray<ExBitFlag8> baseLineFlags;

			// Token: 0x0400052B RID: 1323
			[ReadOnly]
			public NativeArray<short> baseLineTeamIds;

			// Token: 0x0400052C RID: 1324
			[ReadOnly]
			public NativeArray<ushort> baseLineStartIndices;

			// Token: 0x0400052D RID: 1325
			[ReadOnly]
			public NativeArray<ushort> baseLineCounts;

			// Token: 0x0400052E RID: 1326
			[ReadOnly]
			public NativeArray<ushort> baseLineIndices;
		}

		// Token: 0x020000A4 RID: 164
		[BurstCompile]
		private struct CalcTriangleNormalTangentJob : IJobParallelFor
		{
			// Token: 0x06000285 RID: 645 RVA: 0x0001A6D8 File Offset: 0x000188D8
			public void Execute(int tindex)
			{
				int num = (int)this.triangleTeamIdArray[tindex];
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				if (!teamData.IsEnable)
				{
					return;
				}
				int3 @int = this.triangles[tindex];
				int startIndex = teamData.proxyCommonChunk.startIndex;
				float3 @float = this.positions[startIndex + @int.x];
				float3 float2 = this.positions[startIndex + @int.y];
				float3 float3 = this.positions[startIndex + @int.z];
				float3 value = MathUtility.TriangleNormal(@float, float2, float3);
				this.outTriangleNormals[tindex] = value;
				float2 float4 = this.uv[startIndex + @int.x];
				float2 float5 = this.uv[startIndex + @int.y];
				float2 float6 = this.uv[startIndex + @int.z];
				float3 value2 = MathUtility.TriangleTangent(@float, float2, float3, float4, float5, float6);
				this.outTriangleTangents[tindex] = value2;
			}

			// Token: 0x0400052F RID: 1327
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000530 RID: 1328
			[ReadOnly]
			public NativeArray<short> triangleTeamIdArray;

			// Token: 0x04000531 RID: 1329
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x04000532 RID: 1330
			[WriteOnly]
			public NativeArray<float3> outTriangleNormals;

			// Token: 0x04000533 RID: 1331
			[WriteOnly]
			public NativeArray<float3> outTriangleTangents;

			// Token: 0x04000534 RID: 1332
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000535 RID: 1333
			[ReadOnly]
			public NativeArray<float2> uv;
		}

		// Token: 0x020000A5 RID: 165
		[BurstCompile]
		private struct CalcVertexNormalTangentFromTriangleJob : IJobParallelForDefer
		{
			// Token: 0x06000286 RID: 646 RVA: 0x0001A7E0 File Offset: 0x000189E0
			public void Execute(int index)
			{
				int index2 = this.jobVertexIndexList[index];
				int num = (int)this.teamIds[index2];
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				FixedList32Bytes<int> fixedList32Bytes = this.vertexToTriangles[index2];
				if (fixedList32Bytes.Length > 0)
				{
					float3 @float = 0;
					float3 float2 = 0;
					for (int i = 0; i < fixedList32Bytes.Length; i++)
					{
						int num2 = fixedList32Bytes[i];
						float rhs = math.sign((float)num2);
						num2 = math.abs(num2) - 1;
						num2 += teamData.proxyTriangleChunk.startIndex;
						@float += this.triangleNormals[num2] * rhs;
						float2 += this.triangleTangents[num2];
					}
					if (math.lengthsq(@float) > 1E-06f)
					{
						@float = math.normalize(@float);
						float2 = math.normalize(float2);
						quaternion quaternion = MathUtility.ToRotation(@float, float2);
						quaternion = math.mul(quaternion, this.normalAdjustmentRotations[index2]);
						this.outRotations[index2] = quaternion;
					}
				}
			}

			// Token: 0x04000536 RID: 1334
			[ReadOnly]
			public NativeArray<int> jobVertexIndexList;

			// Token: 0x04000537 RID: 1335
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000538 RID: 1336
			[ReadOnly]
			public NativeArray<short> teamIds;

			// Token: 0x04000539 RID: 1337
			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			// Token: 0x0400053A RID: 1338
			[ReadOnly]
			public NativeArray<float3> triangleTangents;

			// Token: 0x0400053B RID: 1339
			[ReadOnly]
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

			// Token: 0x0400053C RID: 1340
			[ReadOnly]
			public NativeArray<quaternion> normalAdjustmentRotations;

			// Token: 0x0400053D RID: 1341
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> outRotations;
		}

		// Token: 0x020000A6 RID: 166
		[BurstCompile]
		private struct WriteTransformDataJob : IJobParallelForDefer
		{
			// Token: 0x06000287 RID: 647 RVA: 0x0001A90C File Offset: 0x00018B0C
			public void Execute(int index)
			{
				int num = this.jobVertexIndexList[index];
				int num2 = (int)this.teamIds[num];
				if (num2 == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num2];
				int num3 = num - teamData.proxyCommonChunk.startIndex;
				float3 value = this.positions[num];
				quaternion quaternion = this.rotations[num];
				int index2 = teamData.proxyBoneChunk.startIndex + num3;
				quaternion = math.mul(quaternion, this.vertexToTransformRotations[index2]);
				int index3 = teamData.proxyTransformChunk.startIndex + num3;
				this.transformPositionArray[index3] = value;
				this.transformRotationArray[index3] = quaternion;
			}

			// Token: 0x0400053E RID: 1342
			[ReadOnly]
			public NativeArray<int> jobVertexIndexList;

			// Token: 0x0400053F RID: 1343
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000540 RID: 1344
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000541 RID: 1345
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000542 RID: 1346
			[ReadOnly]
			public NativeArray<short> teamIds;

			// Token: 0x04000543 RID: 1347
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x04000544 RID: 1348
			[ReadOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x04000545 RID: 1349
			[ReadOnly]
			public NativeArray<quaternion> vertexToTransformRotations;
		}

		// Token: 0x020000A7 RID: 167
		[BurstCompile]
		private struct WriteTransformLocalDataJob : IJobParallelForDefer
		{
			// Token: 0x06000288 RID: 648 RVA: 0x0001A9C0 File Offset: 0x00018BC0
			public void Execute(int index)
			{
				int num = this.jobVertexIndexList[index];
				int num2 = (int)this.teamIds[num];
				if (num2 == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num2];
				int num3 = num - teamData.proxyCommonChunk.startIndex;
				int num4 = this.vertexParentIndices[num];
				if (num4 < 0)
				{
					return;
				}
				if (!this.attributes[num].IsMove())
				{
					return;
				}
				int index2 = teamData.proxyTransformChunk.startIndex + num3;
				int index3 = teamData.proxyTransformChunk.startIndex + num4;
				float3 rhs = this.transformPositionArray[index3];
				quaternion q = this.transformRotationArray[index3];
				float3 rhs2 = this.transformScaleArray[index3];
				float3 lhs = this.transformPositionArray[index2];
				quaternion b = this.transformRotationArray[index2];
				quaternion quaternion = math.inverse(q);
				float3 v = lhs - rhs;
				float3 @float = math.mul(quaternion, v);
				@float /= rhs2;
				quaternion value = math.mul(quaternion, b);
				this.transformLocalPositionArray[index2] = @float;
				this.transformLocalRotationArray[index2] = value;
			}

			// Token: 0x04000546 RID: 1350
			[ReadOnly]
			public NativeArray<int> jobVertexIndexList;

			// Token: 0x04000547 RID: 1351
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000548 RID: 1352
			[ReadOnly]
			public NativeArray<short> teamIds;

			// Token: 0x04000549 RID: 1353
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400054A RID: 1354
			[NativeDisableParallelForRestriction]
			public NativeArray<int> vertexParentIndices;

			// Token: 0x0400054B RID: 1355
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x0400054C RID: 1356
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x0400054D RID: 1357
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			// Token: 0x0400054E RID: 1358
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> transformLocalPositionArray;

			// Token: 0x0400054F RID: 1359
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> transformLocalRotationArray;
		}

		// Token: 0x020000A8 RID: 168
		[BurstCompile]
		private struct CalcMeshConvertMatrixJob : IJobParallelFor
		{
			// Token: 0x06000289 RID: 649 RVA: 0x0001AAE8 File Offset: 0x00018CE8
			public void Execute(int index)
			{
				TeamManager.MappingData mappingData = this.mappingDataArray[index];
				if (!mappingData.IsValid())
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[mappingData.teamId];
				if (!teamData.IsEnable)
				{
					return;
				}
				float3 @float = this.transformPositionArray[mappingData.centerTransformIndex];
				quaternion quaternion = this.transformRotationArray[mappingData.centerTransformIndex];
				float3 float2 = this.transformScaleArray[mappingData.centerTransformIndex];
				quaternion toMappingRotation = this.transformInverseRotationArray[mappingData.centerTransformIndex];
				float3 float3 = this.transformPositionArray[teamData.centerTransformIndex];
				quaternion quaternion2 = this.transformRotationArray[teamData.centerTransformIndex];
				float3 float4 = this.transformScaleArray[teamData.centerTransformIndex];
				bool sameSpace = MathUtility.CompareTransform(@float, quaternion, float2, float3, quaternion2, float4);
				mappingData.sameSpace = sameSpace;
				mappingData.toMappingMatrix = math.inverse(MathUtility.LocalToWorldMatrix(@float, quaternion, float2));
				mappingData.toMappingRotation = toMappingRotation;
				this.mappingDataArray[index] = mappingData;
			}

			// Token: 0x04000550 RID: 1360
			public NativeArray<TeamManager.MappingData> mappingDataArray;

			// Token: 0x04000551 RID: 1361
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000552 RID: 1362
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000553 RID: 1363
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000554 RID: 1364
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			// Token: 0x04000555 RID: 1365
			[ReadOnly]
			public NativeArray<quaternion> transformInverseRotationArray;
		}

		// Token: 0x020000A9 RID: 169
		[BurstCompile]
		private struct CalcProxySkinningJob : IJobParallelFor
		{
			// Token: 0x0600028A RID: 650 RVA: 0x0001ABF4 File Offset: 0x00018DF4
			public void Execute(int mvindex)
			{
				int num = (int)this.mappingIdArray[mvindex];
				if (num == 0)
				{
					return;
				}
				num--;
				TeamManager.MappingData mappingData = this.mappingDataArray[num];
				if (!mappingData.IsValid())
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[mappingData.teamId];
				if (!teamData.IsEnable)
				{
					return;
				}
				VertexAttribute vertexAttribute = this.mappingAttributes[mvindex];
				if (vertexAttribute.IsInvalid())
				{
					return;
				}
				if (vertexAttribute.IsFixed())
				{
					return;
				}
				float3 @float = this.mappingLocalPositions[mvindex];
				float3 v = this.mappingLocalNormals[mvindex];
				float3 v2 = this.mappingLocalTangents[mvindex];
				if (!mappingData.sameSpace)
				{
					@float = math.transform(mappingData.toProxyMatrix, @float);
					v = math.mul(mappingData.toProxyRotation, v);
					v2 = math.mul(mappingData.toProxyRotation, v2);
				}
				VirtualMeshBoneWeight virtualMeshBoneWeight = this.mappingBoneWeights[mvindex];
				int count = virtualMeshBoneWeight.Count;
				float3 float2 = 0;
				float3 float3 = 0;
				float3 float4 = 0;
				float3 initScale = teamData.initScale;
				for (int i = 0; i < count; i++)
				{
					float rhs = virtualMeshBoneWeight.weights[i];
					int index = virtualMeshBoneWeight.boneIndices[i] + teamData.proxyCommonChunk.startIndex;
					float3 rhs2 = this.proxyVertexBindPosePositions[index];
					quaternion q = this.proxyVertexBindPoseRotations[index];
					float3 lhs = math.mul(q, @float + rhs2);
					float3 v3 = math.mul(q, v);
					float3 v4 = math.mul(q, v2);
					float3 rhs3 = this.proxyPositions[index];
					quaternion q2 = this.proxyRotations[index];
					lhs = math.mul(q2, lhs * initScale) + rhs3;
					v3 = math.mul(q2, v3);
					v4 = math.mul(q2, v4);
					float2 += lhs.xyz * rhs;
					float3 += v3.xyz * rhs;
					float4 += v4.xyz * rhs;
				}
				float3 = math.normalize(float3);
				float4 = math.normalize(float4);
				quaternion quaternion = MathUtility.ToRotation(float3, float4);
				float2 = math.transform(mappingData.toMappingMatrix, float2);
				quaternion = math.mul(mappingData.toMappingRotation, quaternion);
				this.mappingPositions[mvindex] = float2;
				this.mappingRotations[mvindex] = quaternion;
			}

			// Token: 0x04000556 RID: 1366
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000557 RID: 1367
			[ReadOnly]
			public NativeArray<TeamManager.MappingData> mappingDataArray;

			// Token: 0x04000558 RID: 1368
			[ReadOnly]
			public NativeArray<short> mappingIdArray;

			// Token: 0x04000559 RID: 1369
			[ReadOnly]
			public NativeArray<VertexAttribute> mappingAttributes;

			// Token: 0x0400055A RID: 1370
			[ReadOnly]
			public NativeArray<float3> mappingLocalPositions;

			// Token: 0x0400055B RID: 1371
			[ReadOnly]
			public NativeArray<float3> mappingLocalNormals;

			// Token: 0x0400055C RID: 1372
			[ReadOnly]
			public NativeArray<float3> mappingLocalTangents;

			// Token: 0x0400055D RID: 1373
			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> mappingBoneWeights;

			// Token: 0x0400055E RID: 1374
			[WriteOnly]
			public NativeArray<float3> mappingPositions;

			// Token: 0x0400055F RID: 1375
			[WriteOnly]
			public NativeArray<quaternion> mappingRotations;

			// Token: 0x04000560 RID: 1376
			[ReadOnly]
			public NativeArray<float3> proxyPositions;

			// Token: 0x04000561 RID: 1377
			[ReadOnly]
			public NativeArray<quaternion> proxyRotations;

			// Token: 0x04000562 RID: 1378
			[ReadOnly]
			public NativeArray<float3> proxyVertexBindPosePositions;

			// Token: 0x04000563 RID: 1379
			[ReadOnly]
			public NativeArray<quaternion> proxyVertexBindPoseRotations;
		}
	}
}
