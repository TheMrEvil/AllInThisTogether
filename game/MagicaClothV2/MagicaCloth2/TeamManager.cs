using System;
using System.Collections.Generic;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200008D RID: 141
	public class TeamManager : IManager, IDisposable, IValid
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000145BE File Offset: 0x000127BE
		public int TeamCount
		{
			get
			{
				ExNativeArray<TeamManager.TeamData> exNativeArray = this.teamDataArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000145D1 File Offset: 0x000127D1
		public int MappingCount
		{
			get
			{
				ExNativeArray<TeamManager.MappingData> exNativeArray = this.mappingDataArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000145E4 File Offset: 0x000127E4
		public void Dispose()
		{
			this.isValid = false;
			ExNativeArray<TeamManager.TeamData> exNativeArray = this.teamDataArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<TeamManager.MappingData> exNativeArray2 = this.mappingDataArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<ClothParameters> exNativeArray3 = this.parameterArray;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<InertiaConstraint.CenterData> exNativeArray4 = this.centerDataArray;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			this.teamDataArray = null;
			this.mappingDataArray = null;
			this.parameterArray = null;
			this.centerDataArray = null;
			if (this.maxUpdateCount.IsCreated)
			{
				this.maxUpdateCount.Dispose();
			}
			this.enableTeamSet.Clear();
			this.clothProcessDict.Clear();
			this.globalTimeScale = 1f;
			this.fixedUpdateCount = 0;
			MagicaManager.afterFixedUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterFixedUpdateDelegate, new MagicaManager.UpdateMethod(this.AfterFixedUpdate));
			MagicaManager.afterRenderingDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterRenderingDelegate, new MagicaManager.UpdateMethod(this.AfterRenderring));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000146D8 File Offset: 0x000128D8
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000146E0 File Offset: 0x000128E0
		public void Initialize()
		{
			this.Dispose();
			this.teamDataArray = new ExNativeArray<TeamManager.TeamData>(32, false);
			this.mappingDataArray = new ExNativeArray<TeamManager.MappingData>(32, false);
			this.parameterArray = new ExNativeArray<ClothParameters>(32, false);
			this.centerDataArray = new ExNativeArray<InertiaConstraint.CenterData>(32, false);
			TeamManager.TeamData data = default(TeamManager.TeamData);
			this.teamDataArray.Add(data);
			this.parameterArray.Add(default(ClothParameters));
			this.centerDataArray.Add(default(InertiaConstraint.CenterData));
			this.maxUpdateCount = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.globalTimeScale = 1f;
			this.fixedUpdateCount = 0;
			MagicaManager.afterFixedUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterFixedUpdateDelegate, new MagicaManager.UpdateMethod(this.AfterFixedUpdate));
			MagicaManager.afterRenderingDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterRenderingDelegate, new MagicaManager.UpdateMethod(this.AfterRenderring));
			this.isValid = true;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000147D5 File Offset: 0x000129D5
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000147DD File Offset: 0x000129DD
		private void AfterFixedUpdate()
		{
			this.fixedUpdateCount++;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000147ED File Offset: 0x000129ED
		private void AfterRenderring()
		{
			this.fixedUpdateCount = 0;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000147F8 File Offset: 0x000129F8
		internal int AddTeam(ClothProcess cprocess, ClothParameters clothParams)
		{
			if (!this.isValid)
			{
				return 0;
			}
			TeamManager.TeamData data = default(TeamManager.TeamData);
			data.flag.SetBits(0, true);
			data.flag.SetBits(2, true);
			data.flag.SetBits(3, true);
			data.flag.SetBits(6, cprocess.cloth.SerializeData.customSkinningSetting.enable);
			data.flag.SetBits(9, cprocess.cloth.SerializeData.normalAlignmentSetting.alignmentMode > NormalAlignmentSettings.AlignmentMode.None);
			data.updateMode = cprocess.cloth.SerializeData.updateMode;
			data.frequency = clothParams.solverFrequency;
			data.timeScale = 1f;
			data.initScale = cprocess.clothTransformRecord.scale;
			data.scaleRatio = 1f;
			data.centerWorldPosition = cprocess.clothTransformRecord.position;
			data.animationPoseRatio = cprocess.cloth.SerializeData.animationPoseRatio;
			int startIndex = this.teamDataArray.Add(data).startIndex;
			this.parameterArray.Add(clothParams);
			InertiaConstraint.CenterData centerData = default(InertiaConstraint.CenterData);
			centerData.initLocalCenterPosition = cprocess.ProxyMesh.localCenterPosition.Value;
			centerData.frameLocalPosition = centerData.initLocalCenterPosition;
			this.centerDataArray.Add(centerData);
			this.clothProcessDict.Add(startIndex, cprocess);
			return startIndex;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00014970 File Offset: 0x00012B70
		internal void RemoveTeam(int teamId)
		{
			if (!this.isValid || teamId == 0)
			{
				return;
			}
			TeamManager.TeamData teamData = this.GetTeamData(teamId);
			if (teamData.syncTeamId > 0 && this.ContainsTeamData(teamData.syncTeamId))
			{
				TeamManager.TeamData teamData2 = this.GetTeamData(teamData.syncTeamId);
				this.RemoveSyncParent(ref teamData2, teamId);
				this.SetTeamData(teamData.syncTeamId, teamData2);
			}
			DataChunk chunk = new DataChunk(teamId, 1);
			this.teamDataArray.RemoveAndFill(chunk, default(TeamManager.TeamData));
			this.parameterArray.Remove(chunk);
			this.centerDataArray.Remove(chunk);
			this.clothProcessDict.Remove(teamId);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00014A10 File Offset: 0x00012C10
		public void SetEnable(int teamId, bool sw)
		{
			if (!this.isValid || teamId == 0)
			{
				return;
			}
			TeamManager.TeamData teamData = this.teamDataArray[teamId];
			teamData.flag.SetBits(1, sw);
			this.teamDataArray[teamId] = teamData;
			if (sw)
			{
				this.enableTeamSet.Add(teamId);
			}
			else
			{
				this.enableTeamSet.Remove(teamId);
			}
			MagicaManager.Collider.EnableTeamCollider(teamId, sw);
			MagicaManager.Bone.EnableTransform(teamData.centerTransformIndex, sw);
			MagicaManager.Bone.EnableTransform(teamData.proxyTransformChunk, sw);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00014A9E File Offset: 0x00012C9E
		public bool IsEnable(int teamId)
		{
			return this.enableTeamSet.Contains(teamId);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00014AAC File Offset: 0x00012CAC
		public bool ContainsTeamData(int teamId)
		{
			return teamId >= 0 && this.clothProcessDict.ContainsKey(teamId);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public TeamManager.TeamData GetTeamData(int teamId)
		{
			if (!this.isValid || !this.ContainsTeamData(teamId))
			{
				return default(TeamManager.TeamData);
			}
			return this.teamDataArray[teamId];
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00014AF4 File Offset: 0x00012CF4
		public void SetTeamData(int teamId, TeamManager.TeamData tdata)
		{
			if (!this.isValid)
			{
				return;
			}
			this.teamDataArray[teamId] = tdata;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00014B0C File Offset: 0x00012D0C
		public ClothParameters GetParameters(int teamId)
		{
			if (!this.isValid)
			{
				return default(ClothParameters);
			}
			return this.parameterArray[teamId];
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00014B37 File Offset: 0x00012D37
		public void SetParameters(int teamId, ClothParameters parameters)
		{
			if (!this.isValid)
			{
				return;
			}
			this.parameterArray[teamId] = parameters;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00014B50 File Offset: 0x00012D50
		internal InertiaConstraint.CenterData GetCenterData(int teamId)
		{
			if (!this.isValid)
			{
				return default(InertiaConstraint.CenterData);
			}
			return this.centerDataArray[teamId];
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00014B7B File Offset: 0x00012D7B
		public ClothProcess GetClothProcess(int teamId)
		{
			if (this.clothProcessDict.ContainsKey(teamId))
			{
				return this.clothProcessDict[teamId];
			}
			return null;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00014B9C File Offset: 0x00012D9C
		internal void AlwaysTeamUpdate()
		{
			this.edgeColliderCollisionCount = 0;
			foreach (ClothProcess clothProcess in MagicaManager.Cloth.clothSet)
			{
				int teamId = clothProcess.TeamId;
				TeamManager.TeamData teamData = this.GetTeamData(teamId);
				MagicaCloth cloth = clothProcess.cloth;
				if (teamData.flag.IsSet(1))
				{
					bool value = true;
					if (clothProcess.GetSuspendCounter() == 0)
					{
						value = false;
					}
					if (cloth.SyncCloth != null)
					{
						int teamId2 = cloth.SyncCloth.Process.TeamId;
						if (teamId2 > 0 && this.GetTeamData(teamId2).flag.IsSet(1) && cloth.SyncCloth.Process.GetSuspendCounter() == 0)
						{
							value = false;
						}
					}
					teamData.flag.SetBits(4, value);
					this.SetTeamData(teamId, teamData);
					if (teamData.IsProcess)
					{
						bool flag = false;
						if (clothProcess.IsState(2) && clothProcess.IsEnable)
						{
							MagicaManager.Collider.UpdateColliders(clothProcess);
							teamData = this.GetTeamData(teamId);
							clothProcess.SyncParameters();
							this.SetParameters(teamId, clothProcess.parameters);
							teamData.updateMode = cloth.SerializeData.updateMode;
							teamData.animationPoseRatio = cloth.SerializeData.animationPoseRatio;
							flag = true;
							clothProcess.SetState(2, false);
						}
						int syncTeamId = teamData.syncTeamId;
						MagicaCloth magicaCloth = cloth.SyncCloth;
						if (magicaCloth != null)
						{
							MagicaCloth magicaCloth2 = magicaCloth;
							while (magicaCloth2)
							{
								if (magicaCloth2 == cloth)
								{
									magicaCloth = null;
									magicaCloth2 = null;
								}
								else
								{
									magicaCloth2 = magicaCloth2.SyncCloth;
								}
							}
						}
						teamData.syncTeamId = ((magicaCloth != null) ? magicaCloth.Process.TeamId : 0);
						teamData.flag.SetBits(7, teamData.syncTeamId != 0);
						if (syncTeamId != teamData.syncTeamId)
						{
							if (syncTeamId > 0)
							{
								TeamManager.TeamData teamData2 = this.GetTeamData(syncTeamId);
								this.RemoveSyncParent(ref teamData2, teamId);
								this.SetTeamData(syncTeamId, teamData2);
							}
							if (magicaCloth != null)
							{
								TeamManager.TeamData teamData3 = this.GetTeamData(magicaCloth.Process.TeamId);
								this.AddSyncParent(ref teamData3, teamId);
								this.SetTeamData(magicaCloth.Process.TeamId, teamData3);
								teamData.flag.SetBits(3, false);
							}
							else
							{
								cloth.SerializeData.selfCollisionConstraint.syncPartner = null;
								teamData.frequency = clothProcess.parameters.solverFrequency;
							}
							flag = true;
						}
						if (magicaCloth && teamData.syncTeamId > 0)
						{
							TeamManager.TeamData teamData4 = this.GetTeamData(magicaCloth.Process.TeamId);
							if (teamData4.IsValid)
							{
								teamData.updateMode = teamData4.updateMode;
								teamData.frequency = teamData4.frequency;
								teamData.time = teamData4.time;
								teamData.oldTime = teamData4.oldTime;
								teamData.nowUpdateTime = teamData4.nowUpdateTime;
								teamData.oldUpdateTime = teamData4.oldUpdateTime;
								teamData.frameUpdateTime = teamData4.frameUpdateTime;
								teamData.frameOldTime = teamData4.frameOldTime;
								teamData.timeScale = teamData4.timeScale;
								teamData.updateCount = teamData4.updateCount;
								teamData.frameInterpolation = teamData4.frameInterpolation;
							}
						}
						if (this.GetParameters(teamId).colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
						{
							this.edgeColliderCollisionCount += teamData.EdgeCount;
						}
						this.SetTeamData(teamId, teamData);
						if (flag)
						{
							MagicaManager.Simulation.selfCollisionConstraint.UpdateTeam(teamId);
						}
					}
				}
			}
			float deltaTime = Time.deltaTime;
			float frameFixedDeltaTime = (float)this.fixedUpdateCount * Time.fixedDeltaTime;
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			new TeamManager.AlwaysTeamUpdateJob
			{
				teamCount = this.TeamCount,
				frameDeltaTime = deltaTime,
				frameFixedDeltaTime = frameFixedDeltaTime,
				frameUnscaledDeltaTime = unscaledDeltaTime,
				globalTimeScale = this.globalTimeScale,
				maxUpdateCount = this.maxUpdateCount,
				teamDataArray = this.teamDataArray.GetNativeArray(),
				parameterArray = this.parameterArray.GetNativeArray()
			}.Run<TeamManager.AlwaysTeamUpdateJob>();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00015000 File Offset: 0x00013200
		private bool AddSyncParent(ref TeamManager.TeamData tdata, int parentTeamId)
		{
			if (tdata.syncParentTeamId.Length == tdata.syncParentTeamId.Capacity)
			{
				object obj = "Synchronous team number limit!";
				Develop.LogWarning(obj);
				return false;
			}
			tdata.syncParentTeamId.Add(parentTeamId);
			return true;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00015042 File Offset: 0x00013242
		private void RemoveSyncParent(ref TeamManager.TeamData tdata, int parentTeamId)
		{
			ref tdata.syncParentTeamId.RemoveItemAtSwapBack(parentTeamId);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00015050 File Offset: 0x00013250
		internal JobHandle CalcCenterAndInertia(JobHandle jobHandle)
		{
			TransformManager bone = MagicaManager.Bone;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new TeamManager.CalcCenterAndInertiaJob
			{
				teamDataArray = this.teamDataArray.GetNativeArray(),
				centerDataArray = MagicaManager.Team.centerDataArray.GetNativeArray(),
				positions = vmesh.positions.GetNativeArray(),
				rotations = vmesh.rotations.GetNativeArray(),
				vertexBindPoseRotations = vmesh.vertexBindPoseRotations.GetNativeArray(),
				fixedArray = MagicaManager.Simulation.inertiaConstraint.fixedArray.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				transformScaleArray = bone.scaleArray.GetNativeArray()
			}.Schedule(this.TeamCount, 1, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00015134 File Offset: 0x00013334
		internal JobHandle SimulationStepTeamUpdate(int updateIndex, JobHandle jobHandle)
		{
			jobHandle = new TeamManager.SimulationStepTeamUpdateJob
			{
				updateIndex = updateIndex,
				teamDataArray = this.teamDataArray.GetNativeArray(),
				parameterArray = this.parameterArray.GetNativeArray(),
				centerDataArray = this.centerDataArray.GetNativeArray()
			}.Schedule(this.TeamCount, 1, jobHandle);
			return jobHandle;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00015198 File Offset: 0x00013398
		internal JobHandle PostTeamUpdate(JobHandle jobHandle)
		{
			jobHandle = new TeamManager.PostTeamUpdateJob
			{
				teamDataArray = this.teamDataArray.GetNativeArray(),
				centerDataArray = this.centerDataArray.GetNativeArray()
			}.Schedule(this.teamDataArray.Length, 1, jobHandle);
			return jobHandle;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000151E8 File Offset: 0x000133E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Team Manager. Team:{0}, Mapping:{1}", this.TeamCount, this.MappingCount));
			for (int i = 1; i < this.TeamCount; i++)
			{
				TeamManager.TeamData teamData = this.teamDataArray[i];
				if (teamData.IsValid)
				{
					ClothProcess clothProcess = this.GetClothProcess(i);
					MagicaCloth cloth = clothProcess.cloth;
					stringBuilder.AppendLine(string.Format("ID:{0} [{1}] state:0x{2:X}, Flag:0x{3:X}, Particle:{4}, Collider:{5} Proxy:{6}, Mapping:{7}", new object[]
					{
						i,
						clothProcess.Name,
						clothProcess.GetStateFlag().Value,
						teamData.flag.Value,
						teamData.ParticleCount,
						clothProcess.ColliderCount,
						teamData.proxyMeshType,
						teamData.MappingCount
					}));
					stringBuilder.AppendLine(string.Format("  Sync:{0}, SyncParentCount:{1}", cloth.SyncCloth, teamData.syncParentTeamId.Length));
					stringBuilder.AppendLine(string.Format("  -ProxyTransformChunk {0}", teamData.proxyTransformChunk));
					stringBuilder.AppendLine(string.Format("  -ProxyCommonChunk {0}", teamData.proxyCommonChunk));
					stringBuilder.AppendLine(string.Format("  -ProxyMeshChunk {0}", teamData.proxyMeshChunk));
					stringBuilder.AppendLine(string.Format("  -ProxyBoneChunk {0}", teamData.proxyBoneChunk));
					stringBuilder.AppendLine(string.Format("  -ProxySkinBoneChunk {0}", teamData.proxySkinBoneChunk));
					stringBuilder.AppendLine(string.Format("  -ProxyTriangleChunk {0}", teamData.proxyTriangleChunk));
					stringBuilder.AppendLine(string.Format("  -ProxyEdgeChunk {0}", teamData.proxyEdgeChunk));
					stringBuilder.AppendLine(string.Format("  -BaseLineChunk {0}", teamData.baseLineChunk));
					stringBuilder.AppendLine(string.Format("  -BaseLineDataChunk {0}", teamData.baseLineDataChunk));
					stringBuilder.AppendLine(string.Format("  -ParticleChunk {0}", teamData.particleChunk));
					stringBuilder.AppendLine(string.Format("  -ColliderChunk {0}", teamData.colliderChunk));
					stringBuilder.AppendLine(string.Format("  -ColliderTrnasformChunk {0}", teamData.colliderTransformChunk));
					if (teamData.MappingCount > 0)
					{
						for (int j = 0; j < teamData.MappingCount; j++)
						{
							int num = (int)teamData.mappingDataIndexSet[j];
							TeamManager.MappingData mappingData = this.mappingDataArray[num];
							stringBuilder.AppendLine(string.Format("  *Mapping [{0}] Vertex:{1}", num, mappingData.VertexCount));
						}
					}
					stringBuilder.AppendLine(string.Format("  +DistanceStartChunk {0}", teamData.distanceStartChunk));
					stringBuilder.AppendLine(string.Format("  +DistanceDataChunk {0}", teamData.distanceDataChunk));
					stringBuilder.AppendLine(string.Format("  +BendingPairChunk {0}", teamData.bendingPairChunk));
					stringBuilder.AppendLine(string.Format("  +selfPointChunk {0}", teamData.selfPointChunk));
					stringBuilder.AppendLine(string.Format("  +selfEdgeChunk {0}", teamData.selfEdgeChunk));
					stringBuilder.AppendLine(string.Format("  +selfTriangleChunk {0}", teamData.selfTriangleChunk));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00015575 File Offset: 0x00013775
		public TeamManager()
		{
		}

		// Token: 0x04000412 RID: 1042
		public const int Flag_Valid = 0;

		// Token: 0x04000413 RID: 1043
		public const int Flag_Enable = 1;

		// Token: 0x04000414 RID: 1044
		public const int Flag_Reset = 2;

		// Token: 0x04000415 RID: 1045
		public const int Flag_TimeReset = 3;

		// Token: 0x04000416 RID: 1046
		public const int Flag_Suspend = 4;

		// Token: 0x04000417 RID: 1047
		public const int Flag_Running = 5;

		// Token: 0x04000418 RID: 1048
		public const int Flag_CustomSkinning = 6;

		// Token: 0x04000419 RID: 1049
		public const int Flag_Synchronization = 7;

		// Token: 0x0400041A RID: 1050
		public const int Flag_StepRunning = 8;

		// Token: 0x0400041B RID: 1051
		public const int Flag_NormalAdjustment = 9;

		// Token: 0x0400041C RID: 1052
		public const int Flag_Exit = 10;

		// Token: 0x0400041D RID: 1053
		public const int Flag_Self_PointPrimitive = 13;

		// Token: 0x0400041E RID: 1054
		public const int Flag_Self_EdgePrimitive = 14;

		// Token: 0x0400041F RID: 1055
		public const int Flag_Self_TrianglePrimitive = 15;

		// Token: 0x04000420 RID: 1056
		public const int Flag_Self_EdgeEdge = 16;

		// Token: 0x04000421 RID: 1057
		public const int Flag_Sync_EdgeEdge = 17;

		// Token: 0x04000422 RID: 1058
		public const int Flag_PSync_EdgeEdge = 18;

		// Token: 0x04000423 RID: 1059
		public const int Flag_Self_PointTriangle = 19;

		// Token: 0x04000424 RID: 1060
		public const int Flag_Sync_PointTriangle = 20;

		// Token: 0x04000425 RID: 1061
		public const int Flag_PSync_PointTriangle = 21;

		// Token: 0x04000426 RID: 1062
		public const int Flag_Self_TrianglePoint = 22;

		// Token: 0x04000427 RID: 1063
		public const int Flag_Sync_TrianglePoint = 23;

		// Token: 0x04000428 RID: 1064
		public const int Flag_PSync_TrianglePoint = 24;

		// Token: 0x04000429 RID: 1065
		public const int Flag_Self_EdgeTriangleIntersect = 25;

		// Token: 0x0400042A RID: 1066
		public const int Flag_Sync_EdgeTriangleIntersect = 26;

		// Token: 0x0400042B RID: 1067
		public const int Flag_PSync_EdgeTriangleIntersect = 27;

		// Token: 0x0400042C RID: 1068
		public const int Flag_Self_TriangleEdgeIntersect = 28;

		// Token: 0x0400042D RID: 1069
		public const int Flag_Sync_TriangleEdgeIntersect = 29;

		// Token: 0x0400042E RID: 1070
		public const int Flag_PSync_TriangleEdgeIntersect = 30;

		// Token: 0x0400042F RID: 1071
		public ExNativeArray<TeamManager.TeamData> teamDataArray;

		// Token: 0x04000430 RID: 1072
		public ExNativeArray<TeamManager.MappingData> mappingDataArray;

		// Token: 0x04000431 RID: 1073
		public NativeReference<int> maxUpdateCount;

		// Token: 0x04000432 RID: 1074
		public ExNativeArray<ClothParameters> parameterArray;

		// Token: 0x04000433 RID: 1075
		public ExNativeArray<InertiaConstraint.CenterData> centerDataArray;

		// Token: 0x04000434 RID: 1076
		private HashSet<int> enableTeamSet = new HashSet<int>();

		// Token: 0x04000435 RID: 1077
		private Dictionary<int, ClothProcess> clothProcessDict = new Dictionary<int, ClothProcess>();

		// Token: 0x04000436 RID: 1078
		internal float globalTimeScale = 1f;

		// Token: 0x04000437 RID: 1079
		private int fixedUpdateCount;

		// Token: 0x04000438 RID: 1080
		private bool isValid;

		// Token: 0x04000439 RID: 1081
		internal int edgeColliderCollisionCount;

		// Token: 0x0200008E RID: 142
		public struct TeamData
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0001559E File Offset: 0x0001379E
			public bool IsFixedUpdate
			{
				get
				{
					return this.updateMode == ClothUpdateMode.UnityPhysics;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600021E RID: 542 RVA: 0x000155A9 File Offset: 0x000137A9
			public bool IsUnscaled
			{
				get
				{
					return this.updateMode == ClothUpdateMode.Unscaled;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600021F RID: 543 RVA: 0x000155B4 File Offset: 0x000137B4
			public float SimulationDeltaTime
			{
				get
				{
					return 1f / (float)this.frequency;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000220 RID: 544 RVA: 0x000155C3 File Offset: 0x000137C3
			public bool IsValid
			{
				get
				{
					return this.flag.IsSet(0);
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000221 RID: 545 RVA: 0x000155D1 File Offset: 0x000137D1
			public bool IsEnable
			{
				get
				{
					return this.flag.IsSet(1);
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000222 RID: 546 RVA: 0x000155DF File Offset: 0x000137DF
			public bool IsProcess
			{
				get
				{
					return this.flag.IsSet(1) && !this.flag.IsSet(4);
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000223 RID: 547 RVA: 0x00015600 File Offset: 0x00013800
			public bool IsReset
			{
				get
				{
					return this.flag.IsSet(2);
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000224 RID: 548 RVA: 0x0001560E File Offset: 0x0001380E
			public bool IsRunning
			{
				get
				{
					return this.flag.IsSet(5);
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000225 RID: 549 RVA: 0x0001561C File Offset: 0x0001381C
			public bool IsStepRunning
			{
				get
				{
					return this.flag.IsSet(8);
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000226 RID: 550 RVA: 0x0001562A File Offset: 0x0001382A
			public int ParticleCount
			{
				get
				{
					return this.particleChunk.dataLength;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000227 RID: 551 RVA: 0x00015637 File Offset: 0x00013837
			public int ColliderCount
			{
				get
				{
					return this.colliderCount;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000228 RID: 552 RVA: 0x0001563F File Offset: 0x0001383F
			public int BaseLineCount
			{
				get
				{
					return this.baseLineChunk.dataLength;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000229 RID: 553 RVA: 0x0001564C File Offset: 0x0001384C
			public int TriangleCount
			{
				get
				{
					return this.proxyTriangleChunk.dataLength;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600022A RID: 554 RVA: 0x00015659 File Offset: 0x00013859
			public int EdgeCount
			{
				get
				{
					return this.proxyEdgeChunk.dataLength;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600022B RID: 555 RVA: 0x00015666 File Offset: 0x00013866
			public int MappingCount
			{
				get
				{
					return this.mappingDataIndexSet.Length;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600022C RID: 556 RVA: 0x00015673 File Offset: 0x00013873
			public float InitScale
			{
				get
				{
					return this.initScale.x;
				}
			}

			// Token: 0x0400043A RID: 1082
			public BitField32 flag;

			// Token: 0x0400043B RID: 1083
			public ClothUpdateMode updateMode;

			// Token: 0x0400043C RID: 1084
			public int frequency;

			// Token: 0x0400043D RID: 1085
			public float time;

			// Token: 0x0400043E RID: 1086
			public float oldTime;

			// Token: 0x0400043F RID: 1087
			public float nowUpdateTime;

			// Token: 0x04000440 RID: 1088
			public float oldUpdateTime;

			// Token: 0x04000441 RID: 1089
			public float frameUpdateTime;

			// Token: 0x04000442 RID: 1090
			public float frameOldTime;

			// Token: 0x04000443 RID: 1091
			public float timeScale;

			// Token: 0x04000444 RID: 1092
			public int updateCount;

			// Token: 0x04000445 RID: 1093
			public float frameInterpolation;

			// Token: 0x04000446 RID: 1094
			public float gravityRatio;

			// Token: 0x04000447 RID: 1095
			public float gravityDot;

			// Token: 0x04000448 RID: 1096
			public int centerTransformIndex;

			// Token: 0x04000449 RID: 1097
			public float3 centerWorldPosition;

			// Token: 0x0400044A RID: 1098
			public float3 initScale;

			// Token: 0x0400044B RID: 1099
			public float scaleRatio;

			// Token: 0x0400044C RID: 1100
			public int syncTeamId;

			// Token: 0x0400044D RID: 1101
			public FixedList32Bytes<int> syncParentTeamId;

			// Token: 0x0400044E RID: 1102
			public float animationPoseRatio;

			// Token: 0x0400044F RID: 1103
			public float velocityWeight;

			// Token: 0x04000450 RID: 1104
			public float blendWeight;

			// Token: 0x04000451 RID: 1105
			public VirtualMesh.MeshType proxyMeshType;

			// Token: 0x04000452 RID: 1106
			public DataChunk proxyTransformChunk;

			// Token: 0x04000453 RID: 1107
			public DataChunk proxyCommonChunk;

			// Token: 0x04000454 RID: 1108
			public DataChunk proxyVertexChildDataChunk;

			// Token: 0x04000455 RID: 1109
			public DataChunk proxyTriangleChunk;

			// Token: 0x04000456 RID: 1110
			public DataChunk proxyEdgeChunk;

			// Token: 0x04000457 RID: 1111
			public DataChunk proxyMeshChunk;

			// Token: 0x04000458 RID: 1112
			public DataChunk proxyBoneChunk;

			// Token: 0x04000459 RID: 1113
			public DataChunk proxySkinBoneChunk;

			// Token: 0x0400045A RID: 1114
			public DataChunk baseLineChunk;

			// Token: 0x0400045B RID: 1115
			public DataChunk baseLineDataChunk;

			// Token: 0x0400045C RID: 1116
			public DataChunk fixedDataChunk;

			// Token: 0x0400045D RID: 1117
			public FixedList32Bytes<short> mappingDataIndexSet;

			// Token: 0x0400045E RID: 1118
			public DataChunk particleChunk;

			// Token: 0x0400045F RID: 1119
			public DataChunk colliderChunk;

			// Token: 0x04000460 RID: 1120
			public DataChunk colliderTransformChunk;

			// Token: 0x04000461 RID: 1121
			public int colliderCount;

			// Token: 0x04000462 RID: 1122
			public DataChunk distanceStartChunk;

			// Token: 0x04000463 RID: 1123
			public DataChunk distanceDataChunk;

			// Token: 0x04000464 RID: 1124
			public DataChunk bendingPairChunk;

			// Token: 0x04000465 RID: 1125
			public DataChunk bendingWriteIndexChunk;

			// Token: 0x04000466 RID: 1126
			public DataChunk bendingBufferChunk;

			// Token: 0x04000467 RID: 1127
			public DataChunk selfPointChunk;

			// Token: 0x04000468 RID: 1128
			public DataChunk selfEdgeChunk;

			// Token: 0x04000469 RID: 1129
			public DataChunk selfTriangleChunk;
		}

		// Token: 0x0200008F RID: 143
		public struct MappingData : IValid
		{
			// Token: 0x0600022D RID: 557 RVA: 0x00015680 File Offset: 0x00013880
			public bool IsValid()
			{
				return this.teamId > 0;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0001568B File Offset: 0x0001388B
			public int VertexCount
			{
				get
				{
					return this.mappingCommonChunk.dataLength;
				}
			}

			// Token: 0x0400046A RID: 1130
			public int teamId;

			// Token: 0x0400046B RID: 1131
			public int centerTransformIndex;

			// Token: 0x0400046C RID: 1132
			public DataChunk mappingCommonChunk;

			// Token: 0x0400046D RID: 1133
			public float4x4 toProxyMatrix;

			// Token: 0x0400046E RID: 1134
			public quaternion toProxyRotation;

			// Token: 0x0400046F RID: 1135
			public bool sameSpace;

			// Token: 0x04000470 RID: 1136
			public float4x4 toMappingMatrix;

			// Token: 0x04000471 RID: 1137
			public quaternion toMappingRotation;
		}

		// Token: 0x02000090 RID: 144
		[BurstCompile]
		private struct AlwaysTeamUpdateJob : IJob
		{
			// Token: 0x0600022F RID: 559 RVA: 0x00015698 File Offset: 0x00013898
			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < this.teamCount; i++)
				{
					if (i != 0)
					{
						TeamManager.TeamData teamData = this.teamDataArray[i];
						if (teamData.IsProcess)
						{
							if (teamData.flag.IsSet(3))
							{
								ClothParameters clothParameters = this.parameterArray[i];
								teamData.time = 0f;
								teamData.oldTime = 0f;
								teamData.nowUpdateTime = 0f;
								teamData.oldUpdateTime = 0f;
								teamData.frameUpdateTime = 0f;
								teamData.frameOldTime = 0f;
								teamData.velocityWeight = ((clothParameters.stablizationTimeAfterReset > 1E-06f) ? 0f : 1f);
								teamData.blendWeight = teamData.velocityWeight;
							}
							float num2 = math.min(teamData.IsFixedUpdate ? this.frameFixedDeltaTime : (teamData.IsUnscaled ? this.frameUnscaledDeltaTime : this.frameDeltaTime), teamData.SimulationDeltaTime * 3f);
							float num3 = teamData.timeScale * (teamData.IsUnscaled ? 1f : this.globalTimeScale);
							num3 = (teamData.flag.IsSet(4) ? 0f : num3);
							float num4 = num2 * num3;
							float num5 = teamData.time + num4;
							float num6 = num5 - teamData.nowUpdateTime;
							teamData.updateCount = (int)(num6 / teamData.SimulationDeltaTime);
							if (teamData.updateCount > 0 && num4 == 0f)
							{
								teamData.updateCount = 0;
								teamData.nowUpdateTime = num5 - teamData.SimulationDeltaTime + 0.0001f;
							}
							if (teamData.updateCount > 0)
							{
								teamData.frameOldTime = teamData.frameUpdateTime;
								teamData.frameUpdateTime = num5;
								teamData.oldUpdateTime = teamData.nowUpdateTime;
							}
							teamData.oldTime = teamData.time;
							teamData.time = num5;
							teamData.flag.SetBits(5, teamData.updateCount > 0);
							this.teamDataArray[i] = teamData;
							num = math.max(num, teamData.updateCount);
						}
					}
				}
				this.maxUpdateCount.Value = num;
			}

			// Token: 0x04000472 RID: 1138
			public int teamCount;

			// Token: 0x04000473 RID: 1139
			public float frameDeltaTime;

			// Token: 0x04000474 RID: 1140
			public float frameFixedDeltaTime;

			// Token: 0x04000475 RID: 1141
			public float frameUnscaledDeltaTime;

			// Token: 0x04000476 RID: 1142
			public float globalTimeScale;

			// Token: 0x04000477 RID: 1143
			public NativeReference<int> maxUpdateCount;

			// Token: 0x04000478 RID: 1144
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000479 RID: 1145
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;
		}

		// Token: 0x02000091 RID: 145
		[BurstCompile]
		private struct CalcCenterAndInertiaJob : IJobParallelFor
		{
			// Token: 0x06000230 RID: 560 RVA: 0x000158C0 File Offset: 0x00013AC0
			public void Execute(int teamId)
			{
				if (teamId == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsProcess)
				{
					return;
				}
				InertiaConstraint.CenterData centerData = this.centerDataArray[teamId];
				float3 @float = this.transformPositionArray[centerData.centerTransformIndex];
				quaternion quaternion = this.transformRotationArray[centerData.centerTransformIndex];
				float3 float2 = this.transformScaleArray[centerData.centerTransformIndex];
				float4x4 float4x = MathUtility.WorldToLocalMatrix(@float, quaternion, float2);
				if (teamData.fixedDataChunk.IsValid)
				{
					float3 lhs = 0;
					float3 float3 = 0;
					float3 float4 = 0;
					int num = 0;
					int startIndex = teamData.proxyCommonChunk.startIndex;
					int dataLength = teamData.fixedDataChunk.dataLength;
					int startIndex2 = teamData.fixedDataChunk.startIndex;
					for (int i = 0; i < dataLength; i++)
					{
						int index = (int)this.fixedArray[startIndex2 + i] + startIndex;
						lhs += this.positions[index];
						num++;
						quaternion a = this.rotations[index];
						a = math.mul(a, this.vertexBindPoseRotations[index]);
						float3 += MathUtility.ToNormal(a);
						float4 += MathUtility.ToTangent(a);
					}
					@float = lhs / (float)num;
					float3 float5 = math.normalize(float3);
					float3 float6 = math.normalize(float4);
					quaternion = MathUtility.ToRotation(float5, float6);
				}
				if (teamData.IsReset)
				{
					centerData.frameWorldPosition = @float;
					centerData.frameWorldRotation = quaternion;
					centerData.frameWorldScale = float2;
					centerData.oldFrameWorldPosition = @float;
					centerData.oldFrameWorldRotation = quaternion;
					centerData.oldFrameWorldScale = float2;
					centerData.nowWorldPosition = @float;
					centerData.nowWorldRotation = quaternion;
					centerData.nowWorldScale = float2;
					centerData.oldWorldPosition = @float;
					centerData.oldWorldRotation = quaternion;
					centerData.frameLocalPosition = centerData.initLocalCenterPosition;
					teamData.centerWorldPosition = @float;
				}
				else
				{
					centerData.frameWorldPosition = @float;
					centerData.frameWorldRotation = quaternion;
					centerData.frameWorldScale = float2;
				}
				float3 frameLocalPosition = MathUtility.InverseTransformPoint(@float, float4x);
				centerData.frameLocalPosition = frameLocalPosition;
				float3 frameVector = 0;
				quaternion frameRotation = quaternion.identity;
				if (teamData.IsRunning)
				{
					frameVector = centerData.frameWorldPosition - centerData.oldFrameWorldPosition;
					frameRotation = MathUtility.FromToRotation(centerData.oldFrameWorldRotation, centerData.frameWorldRotation);
				}
				centerData.frameVector = frameVector;
				centerData.frameRotation = frameRotation;
				this.centerDataArray[teamId] = centerData;
				this.teamDataArray[teamId] = teamData;
			}

			// Token: 0x0400047A RID: 1146
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400047B RID: 1147
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x0400047C RID: 1148
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x0400047D RID: 1149
			[ReadOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x0400047E RID: 1150
			[ReadOnly]
			public NativeArray<quaternion> vertexBindPoseRotations;

			// Token: 0x0400047F RID: 1151
			[ReadOnly]
			public NativeArray<ushort> fixedArray;

			// Token: 0x04000480 RID: 1152
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000481 RID: 1153
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000482 RID: 1154
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;
		}

		// Token: 0x02000092 RID: 146
		[BurstCompile]
		private struct SimulationStepTeamUpdateJob : IJobParallelFor
		{
			// Token: 0x06000231 RID: 561 RVA: 0x00015B54 File Offset: 0x00013D54
			public void Execute(int teamId)
			{
				if (teamId == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsProcess)
				{
					return;
				}
				bool value = this.updateIndex < teamData.updateCount;
				teamData.flag.SetBits(8, value);
				if (this.updateIndex >= teamData.updateCount)
				{
					this.teamDataArray[teamId] = teamData;
					return;
				}
				ClothParameters clothParameters = this.parameterArray[teamId];
				teamData.nowUpdateTime += teamData.SimulationDeltaTime;
				teamData.frameInterpolation = (teamData.nowUpdateTime - teamData.frameOldTime) / (teamData.time - teamData.frameOldTime);
				InertiaConstraint.CenterData centerData = this.centerDataArray[teamId];
				centerData.oldWorldPosition = centerData.nowWorldPosition;
				centerData.oldWorldRotation = centerData.nowWorldRotation;
				centerData.nowWorldPosition = math.lerp(centerData.oldFrameWorldPosition, centerData.frameWorldPosition, teamData.frameInterpolation);
				centerData.nowWorldRotation = math.slerp(centerData.oldFrameWorldRotation, centerData.frameWorldRotation, teamData.frameInterpolation);
				centerData.nowWorldRotation = math.normalize(centerData.nowWorldRotation);
				float3 @float = math.lerp(centerData.oldFrameWorldScale, centerData.frameWorldScale, teamData.frameInterpolation);
				centerData.nowWorldScale = @float;
				teamData.centerWorldPosition = centerData.nowWorldPosition;
				centerData.stepVector = centerData.nowWorldPosition - centerData.oldWorldPosition;
				centerData.stepRotation = MathUtility.FromToRotation(centerData.oldWorldRotation, centerData.nowWorldRotation);
				float num = MathUtility.Angle(centerData.oldWorldRotation, centerData.nowWorldRotation);
				float num2 = 0f;
				float num3 = 0f;
				float num4 = math.length(centerData.stepVector) / teamData.SimulationDeltaTime;
				float num5 = math.degrees(num) / teamData.SimulationDeltaTime;
				if (num4 > clothParameters.inertiaConstraint.movementSpeedLimit && clothParameters.inertiaConstraint.movementSpeedLimit >= 0f)
				{
					num2 = math.saturate(math.max(num4 - clothParameters.inertiaConstraint.movementSpeedLimit, 0f) / num4);
				}
				if (num5 > clothParameters.inertiaConstraint.rotationSpeedLimit && clothParameters.inertiaConstraint.rotationSpeedLimit >= 0f)
				{
					num3 = math.saturate(math.max(num5 - clothParameters.inertiaConstraint.rotationSpeedLimit, 0f) / num5);
				}
				num2 = math.lerp(num2, 1f, 1f - clothParameters.inertiaConstraint.movementInertia);
				num3 = math.lerp(num3, 1f, 1f - clothParameters.inertiaConstraint.rotationInertia);
				centerData.stepMoveInertiaRatio = num2;
				centerData.stepRotationInertiaRatio = num3;
				centerData.inertiaVector = math.lerp(float3.zero, centerData.stepVector, num2);
				centerData.inertiaRotation = math.slerp(quaternion.identity, centerData.stepRotation, num3);
				centerData.angularVelocity = num / teamData.SimulationDeltaTime;
				if (centerData.angularVelocity > 1E-08f)
				{
					float num6;
					MathUtility.ToAngleAxis(centerData.stepRotation, out num6, out centerData.rotationAxis);
				}
				else
				{
					centerData.rotationAxis = 0;
				}
				teamData.scaleRatio = math.length(@float) / math.length(teamData.initScale);
				float num7 = 1f;
				if (math.lengthsq(clothParameters.gravityDirection) > 1E-08f)
				{
					num7 = math.dot(math.mul(centerData.nowWorldRotation, centerData.initLocalGravityDirection), clothParameters.gravityDirection);
					num7 = math.saturate(num7 * 0.5f + 0.5f);
				}
				teamData.gravityDot = num7;
				float gravityRatio = 1f;
				if (clothParameters.gravity > 1E-06f && clothParameters.gravityFalloff > 1E-06f)
				{
					gravityRatio = math.lerp(math.saturate(1f - clothParameters.gravityFalloff), 1f, math.saturate(1f - num7));
				}
				teamData.gravityRatio = gravityRatio;
				if (teamData.velocityWeight < 1f)
				{
					float num8 = (clothParameters.stablizationTimeAfterReset > 1E-06f) ? (teamData.SimulationDeltaTime / clothParameters.stablizationTimeAfterReset) : 1f;
					teamData.velocityWeight = math.saturate(teamData.velocityWeight + num8);
				}
				teamData.blendWeight = math.saturate(teamData.velocityWeight * clothParameters.blendWeight);
				this.teamDataArray[teamId] = teamData;
				this.centerDataArray[teamId] = centerData;
			}

			// Token: 0x04000483 RID: 1155
			public int updateIndex;

			// Token: 0x04000484 RID: 1156
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000485 RID: 1157
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x04000486 RID: 1158
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;
		}

		// Token: 0x02000093 RID: 147
		[BurstCompile]
		private struct PostTeamUpdateJob : IJobParallelFor
		{
			// Token: 0x06000232 RID: 562 RVA: 0x00015F9C File Offset: 0x0001419C
			public void Execute(int teamId)
			{
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsProcess)
				{
					return;
				}
				if (teamData.IsRunning)
				{
					InertiaConstraint.CenterData centerData = this.centerDataArray[teamId];
					centerData.oldFrameWorldPosition = centerData.frameWorldPosition;
					centerData.oldFrameWorldRotation = centerData.frameWorldRotation;
					centerData.oldFrameWorldScale = centerData.frameWorldScale;
					this.centerDataArray[teamId] = centerData;
				}
				teamData.flag.SetBits(2, false);
				teamData.flag.SetBits(3, false);
				teamData.flag.SetBits(5, false);
				teamData.flag.SetBits(8, false);
				if (teamData.time > 60f)
				{
					teamData.time -= 30f;
					teamData.oldTime -= 30f;
					teamData.nowUpdateTime -= 30f;
					teamData.oldUpdateTime -= 30f;
					teamData.frameUpdateTime -= 30f;
					teamData.frameOldTime -= 30f;
				}
				this.teamDataArray[teamId] = teamData;
			}

			// Token: 0x04000487 RID: 1159
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000488 RID: 1160
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;
		}
	}
}
