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
	// Token: 0x0200007B RID: 123
	public class ColliderManager : IManager, IDisposable, IValid
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0001080F File Offset: 0x0000EA0F
		public int DataCount
		{
			get
			{
				ExNativeArray<short> exNativeArray = this.teamIdArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00010822 File Offset: 0x0000EA22
		public int ColliderCount
		{
			get
			{
				return this.colliderSet.Count;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00010830 File Offset: 0x0000EA30
		public void Dispose()
		{
			this.isValid = false;
			ExNativeArray<short> exNativeArray = this.teamIdArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<ExBitFlag8> exNativeArray2 = this.flagArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<float3> exNativeArray3 = this.centerArray;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<float3> exNativeArray4 = this.sizeArray;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			ExNativeArray<float3> exNativeArray5 = this.framePositions;
			if (exNativeArray5 != null)
			{
				exNativeArray5.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray6 = this.frameRotations;
			if (exNativeArray6 != null)
			{
				exNativeArray6.Dispose();
			}
			ExNativeArray<float3> exNativeArray7 = this.frameScales;
			if (exNativeArray7 != null)
			{
				exNativeArray7.Dispose();
			}
			ExNativeArray<float3> exNativeArray8 = this.nowPositions;
			if (exNativeArray8 != null)
			{
				exNativeArray8.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray9 = this.nowRotations;
			if (exNativeArray9 != null)
			{
				exNativeArray9.Dispose();
			}
			ExNativeArray<float3> exNativeArray10 = this.oldFramePositions;
			if (exNativeArray10 != null)
			{
				exNativeArray10.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray11 = this.oldFrameRotations;
			if (exNativeArray11 != null)
			{
				exNativeArray11.Dispose();
			}
			ExNativeArray<float3> exNativeArray12 = this.oldPositions;
			if (exNativeArray12 != null)
			{
				exNativeArray12.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray13 = this.oldRotations;
			if (exNativeArray13 != null)
			{
				exNativeArray13.Dispose();
			}
			ExNativeArray<ColliderManager.WorkData> exNativeArray14 = this.workDataArray;
			if (exNativeArray14 != null)
			{
				exNativeArray14.Dispose();
			}
			this.teamIdArray = null;
			this.flagArray = null;
			this.sizeArray = null;
			this.framePositions = null;
			this.frameRotations = null;
			this.frameScales = null;
			this.nowPositions = null;
			this.nowRotations = null;
			this.oldFramePositions = null;
			this.oldFrameRotations = null;
			this.oldPositions = null;
			this.oldRotations = null;
			this.workDataArray = null;
			this.colliderSet.Clear();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00010998 File Offset: 0x0000EB98
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public void Initialize()
		{
			this.Dispose();
			this.teamIdArray = new ExNativeArray<short>(256, false);
			this.flagArray = new ExNativeArray<ExBitFlag8>(256, false);
			this.centerArray = new ExNativeArray<float3>(256, false);
			this.sizeArray = new ExNativeArray<float3>(256, false);
			this.framePositions = new ExNativeArray<float3>(256, false);
			this.frameRotations = new ExNativeArray<quaternion>(256, false);
			this.frameScales = new ExNativeArray<float3>(256, false);
			this.nowPositions = new ExNativeArray<float3>(256, false);
			this.nowRotations = new ExNativeArray<quaternion>(256, false);
			this.oldFramePositions = new ExNativeArray<float3>(256, false);
			this.oldFrameRotations = new ExNativeArray<quaternion>(256, false);
			this.oldPositions = new ExNativeArray<float3>(256, false);
			this.oldRotations = new ExNativeArray<quaternion>(256, false);
			this.workDataArray = new ExNativeArray<ColliderManager.WorkData>(256, false);
			this.isValid = true;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public void Register(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			int num = 32;
			teamData.colliderChunk = this.teamIdArray.AddRange(num, (short)teamId);
			this.flagArray.AddRange(num);
			this.centerArray.AddRange(num);
			this.sizeArray.AddRange(num);
			this.framePositions.AddRange(num);
			this.frameRotations.AddRange(num);
			this.frameScales.AddRange(num);
			this.nowPositions.AddRange(num);
			this.nowRotations.AddRange(num);
			this.oldFramePositions.AddRange(num);
			this.oldFrameRotations.AddRange(num);
			this.oldPositions.AddRange(num);
			this.oldRotations.AddRange(num);
			this.workDataArray.AddRange(num);
			teamData.colliderTransformChunk = MagicaManager.Bone.AddTransform(num);
			teamData.colliderCount = 0;
			MagicaManager.Team.SetTeamData(teamId, teamData);
			this.InitColliders(cprocess);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public void Exit(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			int num = 0;
			for (;;)
			{
				int num2 = num;
				ColliderComponent[] colliderArray = cprocess.colliderArray;
				int? num3 = (colliderArray != null) ? new int?(colliderArray.Length) : null;
				if (!(num2 < num3.GetValueOrDefault() & num3 != null))
				{
					break;
				}
				ColliderComponent colliderComponent = cprocess.colliderArray[num];
				if (colliderComponent)
				{
					colliderComponent.Exit(teamId);
					this.colliderSet.Remove(colliderComponent);
				}
				cprocess.colliderArray[num] = null;
				num++;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			DataChunk colliderChunk = teamData.colliderChunk;
			this.teamIdArray.RemoveAndFill(colliderChunk, 0);
			this.flagArray.RemoveAndFill(colliderChunk, default(ExBitFlag8));
			this.centerArray.Remove(colliderChunk);
			this.sizeArray.Remove(colliderChunk);
			this.framePositions.Remove(colliderChunk);
			this.frameRotations.Remove(colliderChunk);
			this.frameScales.Remove(colliderChunk);
			this.nowPositions.Remove(colliderChunk);
			this.nowRotations.Remove(colliderChunk);
			this.oldFramePositions.Remove(colliderChunk);
			this.oldFrameRotations.Remove(colliderChunk);
			this.oldPositions.Remove(colliderChunk);
			this.oldRotations.Remove(colliderChunk);
			this.workDataArray.Remove(colliderChunk);
			teamData.colliderChunk.Clear();
			teamData.colliderCount = 0;
			MagicaManager.Bone.RemoveTransform(teamData.colliderTransformChunk);
			teamData.colliderTransformChunk.Clear();
			MagicaManager.Team.SetTeamData(teamId, teamData);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00010D54 File Offset: 0x0000EF54
		internal void InitColliders(ClothProcess cprocess)
		{
			List<ColliderComponent> colliderList = cprocess.cloth.SerializeData.colliderCollisionConstraint.colliderList;
			if (colliderList.Count == 0)
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
			int num = 0;
			for (int i = 0; i < colliderList.Count; i++)
			{
				ColliderComponent colliderComponent = colliderList[i];
				if (colliderComponent)
				{
					if (num >= 32)
					{
						object obj = string.Format("No more colliders can be added. The maximum number of colliders is {0}.", 32);
						Develop.LogWarning(obj);
						break;
					}
					this.AddColliderInternal(cprocess, colliderComponent, num, teamData.colliderChunk.startIndex + num, teamData.colliderTransformChunk.startIndex + num);
					teamData.colliderCount++;
					num++;
				}
			}
			MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00010E1C File Offset: 0x0000F01C
		internal void UpdateColliders(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			List<ColliderComponent> colliderList = cprocess.cloth.SerializeData.colliderCollisionConstraint.colliderList;
			int i = 0;
			while (i < 32)
			{
				ColliderComponent colliderComponent = cprocess.colliderArray[i];
				if (colliderComponent && !colliderList.Contains(colliderComponent))
				{
					this.RemoveCollider(colliderComponent, cprocess.TeamId);
				}
				else
				{
					i++;
				}
			}
			foreach (ColliderComponent colliderComponent2 in colliderList)
			{
				if (colliderComponent2 && cprocess.GetColliderIndex(colliderComponent2) < 0)
				{
					this.AddCollider(cprocess, colliderComponent2);
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		private void AddCollider(ClothProcess cprocess, ColliderComponent col)
		{
			if (!this.isValid)
			{
				return;
			}
			if (col == null)
			{
				return;
			}
			if (cprocess.GetColliderIndex(col) >= 0)
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
			int colliderCount = teamData.colliderCount;
			if (colliderCount >= 32)
			{
				object obj = string.Format("No more colliders can be added. The maximum number of colliders is {0}.", 32);
				Develop.LogWarning(obj);
				return;
			}
			int arrayIndex = teamData.colliderChunk.startIndex + colliderCount;
			int transformIndex = teamData.colliderTransformChunk.startIndex + colliderCount;
			this.AddColliderInternal(cprocess, col, colliderCount, arrayIndex, transformIndex);
			teamData.colliderCount++;
			MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010F80 File Offset: 0x0000F180
		internal void RemoveCollider(ColliderComponent col, int teamId)
		{
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			int colliderCount = teamData.colliderCount;
			if (colliderCount == 0)
			{
				return;
			}
			ClothProcess clothProcess = MagicaManager.Team.GetClothProcess(teamId);
			int colliderIndex = clothProcess.GetColliderIndex(col);
			int num = teamData.colliderChunk.startIndex + colliderIndex;
			int num2 = teamData.colliderTransformChunk.startIndex + colliderIndex;
			int num3 = colliderCount - 1;
			int num4 = teamData.colliderChunk.startIndex + num3;
			int num5 = teamData.colliderTransformChunk.startIndex + num3;
			if (num < num4)
			{
				this.flagArray[num] = this.flagArray[num4];
				this.teamIdArray[num] = this.teamIdArray[num4];
				this.centerArray[num] = this.centerArray[num4];
				this.sizeArray[num] = this.sizeArray[num4];
				this.framePositions[num] = this.framePositions[num4];
				this.frameRotations[num] = this.frameRotations[num4];
				this.frameScales[num] = this.frameScales[num4];
				this.nowPositions[num] = this.nowPositions[num4];
				this.nowRotations[num] = this.nowRotations[num4];
				this.oldFramePositions[num] = this.oldFramePositions[num4];
				this.oldFrameRotations[num] = this.oldFrameRotations[num4];
				this.oldPositions[num] = this.oldPositions[num4];
				this.oldRotations[num] = this.oldRotations[num4];
				this.flagArray[num4] = default(ExBitFlag8);
				this.teamIdArray[num4] = 0;
				MagicaManager.Bone.CopyTransform(num5, num2);
				MagicaManager.Bone.SetTransform(null, default(ExBitFlag8), num5);
				clothProcess.colliderArray[colliderIndex] = clothProcess.colliderArray[num3];
				clothProcess.colliderArray[num3] = null;
			}
			else
			{
				this.flagArray[num] = default(ExBitFlag8);
				this.teamIdArray[num] = 0;
				MagicaManager.Bone.SetTransform(null, default(ExBitFlag8), num2);
				clothProcess.colliderArray[colliderIndex] = null;
			}
			teamData.colliderCount--;
			MagicaManager.Team.SetTeamData(teamId, teamData);
			this.colliderSet.Remove(col);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00011228 File Offset: 0x0000F428
		private void AddColliderInternal(ClothProcess cprocess, ColliderComponent col, int index, int arrayIndex, int transformIndex)
		{
			int teamId = cprocess.TeamId;
			this.teamIdArray[arrayIndex] = (short)teamId;
			ExBitFlag8 value = DataUtility.SetColliderType(default(ExBitFlag8), col.GetColliderType());
			value.SetFlag(16, true);
			value.SetFlag(32, col.isActiveAndEnabled);
			this.flagArray[arrayIndex] = value;
			this.centerArray[arrayIndex] = col.center;
			this.sizeArray[arrayIndex] = col.GetSize();
			Vector3 position = col.transform.position;
			Quaternion rotation = col.transform.rotation;
			Vector3 localScale = col.transform.localScale;
			this.framePositions[arrayIndex] = position;
			this.frameRotations[arrayIndex] = rotation;
			this.frameScales[arrayIndex] = localScale;
			this.nowPositions[arrayIndex] = position;
			this.nowRotations[arrayIndex] = rotation;
			this.oldFramePositions[arrayIndex] = position;
			this.oldFrameRotations[arrayIndex] = rotation;
			this.oldPositions[arrayIndex] = position;
			this.oldRotations[arrayIndex] = rotation;
			cprocess.colliderArray[index] = col;
			col.Register(teamId);
			bool sw = cprocess.IsEnable && value.IsSet(32);
			ExBitFlag8 flag = new ExBitFlag8(1);
			flag.SetFlag(16, sw);
			MagicaManager.Bone.SetTransform(col.transform, flag, transformIndex);
			this.colliderSet.Add(col);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000113E4 File Offset: 0x0000F5E4
		internal void EnableCollider(ColliderComponent col, int teamId, bool sw)
		{
			if (!this.IsValid())
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			if (!teamData.IsValid)
			{
				return;
			}
			ClothProcess clothProcess = MagicaManager.Team.GetClothProcess(teamId);
			int colliderIndex = clothProcess.GetColliderIndex(col);
			if (colliderIndex < 0)
			{
				return;
			}
			int index = teamData.colliderChunk.startIndex + colliderIndex;
			ExBitFlag8 value = this.flagArray[index];
			value.SetFlag(32, sw);
			this.flagArray[index] = value;
			int index2 = teamData.colliderTransformChunk.startIndex + colliderIndex;
			bool sw2 = clothProcess.IsEnable && value.IsSet(32);
			MagicaManager.Bone.EnableTransform(index2, sw2);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00011490 File Offset: 0x0000F690
		internal void EnableTeamCollider(int teamId, bool sw)
		{
			if (!this.IsValid())
			{
				return;
			}
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			if (!teamData.IsValid)
			{
				return;
			}
			if (teamData.ColliderCount == 0)
			{
				return;
			}
			bool isEnable = teamData.IsEnable;
			DataChunk colliderTransformChunk = teamData.colliderTransformChunk;
			for (int i = 0; i < colliderTransformChunk.dataLength; i++)
			{
				int index = teamData.colliderChunk.startIndex + i;
				int index2 = colliderTransformChunk.startIndex + i;
				ExBitFlag8 exBitFlag = this.flagArray[index];
				bool sw2 = isEnable && exBitFlag.IsSet(32);
				MagicaManager.Bone.EnableTransform(index2, sw2);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00011530 File Offset: 0x0000F730
		internal void UpdateParameters(ColliderComponent col, int teamId)
		{
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			if (!teamData.IsValid)
			{
				return;
			}
			int colliderIndex = MagicaManager.Team.GetClothProcess(teamId).GetColliderIndex(col);
			if (colliderIndex < 0)
			{
				return;
			}
			int index = teamData.colliderChunk.startIndex + colliderIndex;
			ExBitFlag8 exBitFlag = this.flagArray[index];
			exBitFlag = DataUtility.SetColliderType(exBitFlag, col.GetColliderType());
			this.flagArray[index] = exBitFlag;
			this.centerArray[index] = col.center;
			this.sizeArray[index] = math.max(col.GetSize(), 0.0001f);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000115DC File Offset: 0x0000F7DC
		internal JobHandle PreSimulationUpdate(JobHandle jobHandle)
		{
			if (this.DataCount == 0)
			{
				return jobHandle;
			}
			jobHandle = new ColliderManager.PreSimulationUpdateJob
			{
				teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				flagArray = this.flagArray.GetNativeArray(),
				centerArray = this.centerArray.GetNativeArray(),
				framePositions = this.framePositions.GetNativeArray(),
				frameRotations = this.frameRotations.GetNativeArray(),
				frameScales = this.frameScales.GetNativeArray(),
				oldFramePositions = this.oldFramePositions.GetNativeArray(),
				oldFrameRotations = this.oldFrameRotations.GetNativeArray(),
				nowPositions = this.nowPositions.GetNativeArray(),
				nowRotations = this.nowRotations.GetNativeArray(),
				oldPositions = this.oldPositions.GetNativeArray(),
				oldRotations = this.oldRotations.GetNativeArray(),
				transformPositionArray = MagicaManager.Bone.positionArray.GetNativeArray(),
				transformRotationArray = MagicaManager.Bone.rotationArray.GetNativeArray(),
				transformScaleArray = MagicaManager.Bone.scaleArray.GetNativeArray()
			}.Schedule(this.DataCount, 8, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001173C File Offset: 0x0000F93C
		internal JobHandle CreateUpdateColliderList(int updateIndex, JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			TeamManager team = MagicaManager.Team;
			jobHandle = new ColliderManager.CreateUpdatecolliderListJob
			{
				updateIndex = updateIndex,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				jobColliderCounter = simulation.processingStepCollider.Counter,
				jobColliderIndexList = simulation.processingStepCollider.Buffer
			}.Schedule(team.TeamCount, 1, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000117AC File Offset: 0x0000F9AC
		internal JobHandle StartSimulationStep(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			jobHandle = new ColliderManager.StartSimulationStepJob
			{
				jobColliderIndexList = simulation.processingStepCollider.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				centerDataArray = team.centerDataArray.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				flagArray = this.flagArray.GetNativeArray(),
				sizeArray = this.sizeArray.GetNativeArray(),
				framePositions = this.framePositions.GetNativeArray(),
				frameRotations = this.frameRotations.GetNativeArray(),
				frameScales = this.frameScales.GetNativeArray(),
				oldFramePositions = this.oldFramePositions.GetNativeArray(),
				oldFrameRotations = this.oldFrameRotations.GetNativeArray(),
				nowPositions = this.nowPositions.GetNativeArray(),
				nowRotations = this.nowRotations.GetNativeArray(),
				oldPositions = this.oldPositions.GetNativeArray(),
				oldRotations = this.oldRotations.GetNativeArray(),
				workDataArray = this.workDataArray.GetNativeArray()
			}.Schedule(simulation.processingStepCollider.GetJobSchedulePtr(), 8, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00011904 File Offset: 0x0000FB04
		internal JobHandle EndSimulationStep(JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			jobHandle = new ColliderManager.EndSimulationStepJob
			{
				jobColliderIndexList = simulation.processingStepCollider.Buffer,
				nowPositions = this.nowPositions.GetNativeArray(),
				nowRotations = this.nowRotations.GetNativeArray(),
				oldPositions = this.oldPositions.GetNativeArray(),
				oldRotations = this.oldRotations.GetNativeArray()
			}.Schedule(simulation.processingStepCollider.GetJobSchedulePtr(), 8, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00011990 File Offset: 0x0000FB90
		internal JobHandle PostSimulationUpdate(JobHandle jobHandle)
		{
			if (this.DataCount == 0)
			{
				return jobHandle;
			}
			jobHandle = new ColliderManager.PostSimulationUpdateJob
			{
				teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				framePositions = this.framePositions.GetNativeArray(),
				frameRotations = this.frameRotations.GetNativeArray(),
				oldFramePositions = this.oldFramePositions.GetNativeArray(),
				oldFrameRotations = this.oldFrameRotations.GetNativeArray()
			}.Schedule(this.DataCount, 8, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00011A30 File Offset: 0x0000FC30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Collider Manager. Collider:{0}", this.colliderSet.Count));
			ExNativeArray<short> exNativeArray = this.teamIdArray;
			int num = (exNativeArray != null) ? exNativeArray.Count : 0;
			for (int i = 0; i < num; i++)
			{
				ExBitFlag8 exBitFlag = this.flagArray[i];
				if (exBitFlag.IsSet(16))
				{
					ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(exBitFlag);
					stringBuilder.AppendLine(string.Format("  [{0}] tid:{1}, flag:0x{2:X}, type:{3}, size:{4}, cen:{5}", new object[]
					{
						i,
						this.teamIdArray[i],
						exBitFlag.Value,
						colliderType,
						this.sizeArray[i],
						this.centerArray[i]
					}));
				}
			}
			foreach (ColliderComponent colliderComponent in this.colliderSet)
			{
				string str = ((colliderComponent != null) ? colliderComponent.name : null) ?? "(null)";
				stringBuilder.AppendLine("  " + str);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011B90 File Offset: 0x0000FD90
		public ColliderManager()
		{
		}

		// Token: 0x04000329 RID: 809
		public ExNativeArray<short> teamIdArray;

		// Token: 0x0400032A RID: 810
		public const byte Flag_Valid = 16;

		// Token: 0x0400032B RID: 811
		public const byte Flag_Enable = 32;

		// Token: 0x0400032C RID: 812
		public ExNativeArray<ExBitFlag8> flagArray;

		// Token: 0x0400032D RID: 813
		public ExNativeArray<float3> centerArray;

		// Token: 0x0400032E RID: 814
		public ExNativeArray<float3> sizeArray;

		// Token: 0x0400032F RID: 815
		public ExNativeArray<float3> framePositions;

		// Token: 0x04000330 RID: 816
		public ExNativeArray<quaternion> frameRotations;

		// Token: 0x04000331 RID: 817
		public ExNativeArray<float3> frameScales;

		// Token: 0x04000332 RID: 818
		public ExNativeArray<float3> oldFramePositions;

		// Token: 0x04000333 RID: 819
		public ExNativeArray<quaternion> oldFrameRotations;

		// Token: 0x04000334 RID: 820
		public ExNativeArray<float3> nowPositions;

		// Token: 0x04000335 RID: 821
		public ExNativeArray<quaternion> nowRotations;

		// Token: 0x04000336 RID: 822
		public ExNativeArray<float3> oldPositions;

		// Token: 0x04000337 RID: 823
		public ExNativeArray<quaternion> oldRotations;

		// Token: 0x04000338 RID: 824
		public HashSet<ColliderComponent> colliderSet = new HashSet<ColliderComponent>();

		// Token: 0x04000339 RID: 825
		private bool isValid;

		// Token: 0x0400033A RID: 826
		internal ExNativeArray<ColliderManager.WorkData> workDataArray;

		// Token: 0x0200007C RID: 124
		public enum ColliderType : byte
		{
			// Token: 0x0400033C RID: 828
			None,
			// Token: 0x0400033D RID: 829
			Sphere,
			// Token: 0x0400033E RID: 830
			CapsuleX,
			// Token: 0x0400033F RID: 831
			CapsuleY,
			// Token: 0x04000340 RID: 832
			CapsuleZ,
			// Token: 0x04000341 RID: 833
			Plane,
			// Token: 0x04000342 RID: 834
			Box
		}

		// Token: 0x0200007D RID: 125
		internal struct WorkData
		{
			// Token: 0x04000343 RID: 835
			public AABB aabb;

			// Token: 0x04000344 RID: 836
			public float2 radius;

			// Token: 0x04000345 RID: 837
			public float3x2 oldPos;

			// Token: 0x04000346 RID: 838
			public float3x2 nextPos;

			// Token: 0x04000347 RID: 839
			public quaternion inverseOldRot;

			// Token: 0x04000348 RID: 840
			public quaternion rot;
		}

		// Token: 0x0200007E RID: 126
		[BurstCompile]
		private struct PreSimulationUpdateJob : IJobParallelFor
		{
			// Token: 0x060001E0 RID: 480 RVA: 0x00011BA4 File Offset: 0x0000FDA4
			public void Execute(int index)
			{
				ExBitFlag8 exBitFlag = this.flagArray[index];
				if (!exBitFlag.IsSet(16) || !exBitFlag.IsSet(32))
				{
					return;
				}
				int index2 = (int)this.teamIdArray[index];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (!teamData.IsProcess)
				{
					return;
				}
				float3 v = this.centerArray[index];
				int num = index - teamData.colliderChunk.startIndex;
				int index3 = teamData.colliderTransformChunk.startIndex + num;
				float3 @float = this.transformPositionArray[index3];
				quaternion quaternion = this.transformRotationArray[index3];
				float3 float2 = this.transformScaleArray[index3];
				@float += math.mul(quaternion, v) * float2;
				this.framePositions[index] = @float;
				this.frameRotations[index] = quaternion;
				this.frameScales[index] = float2;
				if (teamData.IsReset)
				{
					this.oldFramePositions[index] = @float;
					this.oldFrameRotations[index] = quaternion;
					this.nowPositions[index] = @float;
					this.nowRotations[index] = quaternion;
					this.oldPositions[index] = @float;
					this.oldRotations[index] = quaternion;
				}
			}

			// Token: 0x04000349 RID: 841
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400034A RID: 842
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x0400034B RID: 843
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagArray;

			// Token: 0x0400034C RID: 844
			[ReadOnly]
			public NativeArray<float3> centerArray;

			// Token: 0x0400034D RID: 845
			[WriteOnly]
			public NativeArray<float3> framePositions;

			// Token: 0x0400034E RID: 846
			[WriteOnly]
			public NativeArray<quaternion> frameRotations;

			// Token: 0x0400034F RID: 847
			[WriteOnly]
			public NativeArray<float3> frameScales;

			// Token: 0x04000350 RID: 848
			[WriteOnly]
			public NativeArray<float3> oldFramePositions;

			// Token: 0x04000351 RID: 849
			[WriteOnly]
			public NativeArray<quaternion> oldFrameRotations;

			// Token: 0x04000352 RID: 850
			[WriteOnly]
			public NativeArray<float3> nowPositions;

			// Token: 0x04000353 RID: 851
			[WriteOnly]
			public NativeArray<quaternion> nowRotations;

			// Token: 0x04000354 RID: 852
			[WriteOnly]
			public NativeArray<float3> oldPositions;

			// Token: 0x04000355 RID: 853
			[WriteOnly]
			public NativeArray<quaternion> oldRotations;

			// Token: 0x04000356 RID: 854
			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			// Token: 0x04000357 RID: 855
			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			// Token: 0x04000358 RID: 856
			[ReadOnly]
			public NativeArray<float3> transformScaleArray;
		}

		// Token: 0x0200007F RID: 127
		[BurstCompile]
		private struct CreateUpdatecolliderListJob : IJobParallelFor
		{
			// Token: 0x060001E1 RID: 481 RVA: 0x00011CF4 File Offset: 0x0000FEF4
			public void Execute(int teamId)
			{
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsProcess)
				{
					return;
				}
				if (this.updateIndex >= teamData.updateCount)
				{
					return;
				}
				if (teamData.ColliderCount > 0)
				{
					int num = ref this.jobColliderCounter.InterlockedStartIndex(teamData.ColliderCount);
					for (int i = 0; i < teamData.ColliderCount; i++)
					{
						int value = teamData.colliderChunk.startIndex + i;
						this.jobColliderIndexList[num + i] = value;
					}
				}
			}

			// Token: 0x04000359 RID: 857
			public int updateIndex;

			// Token: 0x0400035A RID: 858
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400035B RID: 859
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> jobColliderCounter;

			// Token: 0x0400035C RID: 860
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> jobColliderIndexList;
		}

		// Token: 0x02000080 RID: 128
		[BurstCompile]
		private struct StartSimulationStepJob : IJobParallelForDefer
		{
			// Token: 0x060001E2 RID: 482 RVA: 0x00011D74 File Offset: 0x0000FF74
			public void Execute(int index)
			{
				int index2 = this.jobColliderIndexList[index];
				ExBitFlag8 exBitFlag = this.flagArray[index2];
				if (!exBitFlag.IsSet(16) || !exBitFlag.IsSet(32))
				{
					return;
				}
				int index3 = (int)this.teamIdArray[index2];
				TeamManager.TeamData teamData = this.teamDataArray[index3];
				float3 @float = math.lerp(this.oldFramePositions[index2], this.framePositions[index2], teamData.frameInterpolation);
				quaternion quaternion = math.slerp(this.oldFrameRotations[index2], this.frameRotations[index2], teamData.frameInterpolation);
				quaternion = math.normalize(quaternion);
				this.nowPositions[index2] = @float;
				this.nowRotations[index2] = quaternion;
				float3 float2 = this.oldPositions[index2];
				quaternion quaternion2 = this.oldRotations[index2];
				InertiaConstraint.CenterData centerData = this.centerDataArray[index3];
				float2 = math.lerp(float2, @float, centerData.stepMoveInertiaRatio);
				quaternion2 = math.slerp(quaternion2, quaternion, centerData.stepRotationInertiaRatio);
				this.oldPositions[index2] = float2;
				this.oldRotations[index2] = math.normalize(quaternion2);
				ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(exBitFlag);
				ColliderManager.WorkData value = default(ColliderManager.WorkData);
				float3 float3 = this.sizeArray[index2];
				float3 float4 = this.frameScales[index2];
				value.inverseOldRot = math.inverse(quaternion2);
				value.rot = quaternion;
				if (colliderType == ColliderManager.ColliderType.Sphere)
				{
					float num = float3.x * math.abs(float4.x);
					value.radius = num;
					float3 float5 = math.min(float2, @float);
					float3 float6 = math.max(float2, @float);
					AABB aabb = new AABB(ref float5, ref float6);
					aabb.Expand(num);
					value.aabb = aabb;
					value.oldPos.c0 = float2;
					value.nextPos.c0 = @float;
				}
				else if (colliderType == ColliderManager.ColliderType.CapsuleX || colliderType == ColliderManager.ColliderType.CapsuleY || colliderType == ColliderManager.ColliderType.CapsuleZ)
				{
					float3 float7 = (colliderType == ColliderManager.ColliderType.CapsuleX) ? math.right() : ((colliderType == ColliderManager.ColliderType.CapsuleY) ? math.up() : math.forward());
					float rhs = math.dot(math.abs(float4), float7);
					float3 *= rhs;
					float x = float3.x;
					float y = float3.y;
					float z = float3.z;
					float num2 = z * 0.5f;
					float num3 = z * 0.5f;
					num2 = Mathf.Max(num2 - x, 0f);
					num3 = Mathf.Max(num3 - y, 0f);
					float3 float8 = float2 + math.mul(quaternion2, float7 * num2);
					float3 float9 = float2 - math.mul(quaternion2, float7 * num3);
					float3 float10 = @float + math.mul(quaternion, float7 * num2);
					float3 float11 = @float - math.mul(quaternion, float7 * num3);
					float3 float5 = math.min(float8, float10) - x;
					float3 float6 = math.max(float8, float10) + x;
					AABB aabb2 = new AABB(ref float5, ref float6);
					float5 = math.min(float9, float11) - y;
					float6 = math.max(float9, float11) + y;
					AABB aabb3 = new AABB(ref float5, ref float6);
					aabb2.Encapsulate(aabb3);
					value.aabb = aabb2;
					value.radius = new float2(x, y);
					value.oldPos = new float3x2(float8, float9);
					value.nextPos = new float3x2(float10, float11);
				}
				else if (colliderType == ColliderManager.ColliderType.Plane)
				{
					float3 c = math.mul(quaternion, math.up());
					value.oldPos.c0 = c;
					value.nextPos.c0 = @float;
				}
				this.workDataArray[index2] = value;
			}

			// Token: 0x0400035D RID: 861
			[ReadOnly]
			public NativeArray<int> jobColliderIndexList;

			// Token: 0x0400035E RID: 862
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400035F RID: 863
			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x04000360 RID: 864
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x04000361 RID: 865
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagArray;

			// Token: 0x04000362 RID: 866
			[ReadOnly]
			public NativeArray<float3> sizeArray;

			// Token: 0x04000363 RID: 867
			[ReadOnly]
			public NativeArray<float3> framePositions;

			// Token: 0x04000364 RID: 868
			[ReadOnly]
			public NativeArray<quaternion> frameRotations;

			// Token: 0x04000365 RID: 869
			[ReadOnly]
			public NativeArray<float3> frameScales;

			// Token: 0x04000366 RID: 870
			[ReadOnly]
			public NativeArray<float3> oldFramePositions;

			// Token: 0x04000367 RID: 871
			[ReadOnly]
			public NativeArray<quaternion> oldFrameRotations;

			// Token: 0x04000368 RID: 872
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> nowPositions;

			// Token: 0x04000369 RID: 873
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> nowRotations;

			// Token: 0x0400036A RID: 874
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> oldPositions;

			// Token: 0x0400036B RID: 875
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> oldRotations;

			// Token: 0x0400036C RID: 876
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<ColliderManager.WorkData> workDataArray;
		}

		// Token: 0x02000081 RID: 129
		[BurstCompile]
		private struct EndSimulationStepJob : IJobParallelForDefer
		{
			// Token: 0x060001E3 RID: 483 RVA: 0x00012148 File Offset: 0x00010348
			public void Execute(int index)
			{
				int index2 = this.jobColliderIndexList[index];
				this.oldPositions[index2] = this.nowPositions[index2];
				this.oldRotations[index2] = this.nowRotations[index2];
			}

			// Token: 0x0400036D RID: 877
			[ReadOnly]
			public NativeArray<int> jobColliderIndexList;

			// Token: 0x0400036E RID: 878
			[ReadOnly]
			public NativeArray<float3> nowPositions;

			// Token: 0x0400036F RID: 879
			[ReadOnly]
			public NativeArray<quaternion> nowRotations;

			// Token: 0x04000370 RID: 880
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> oldPositions;

			// Token: 0x04000371 RID: 881
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> oldRotations;
		}

		// Token: 0x02000082 RID: 130
		[BurstCompile]
		private struct PostSimulationUpdateJob : IJobParallelFor
		{
			// Token: 0x060001E4 RID: 484 RVA: 0x00012194 File Offset: 0x00010394
			public void Execute(int index)
			{
				int index2 = (int)this.teamIdArray[index];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (!teamData.IsProcess)
				{
					return;
				}
				if (teamData.IsRunning)
				{
					this.oldFramePositions[index] = this.framePositions[index];
					this.oldFrameRotations[index] = this.frameRotations[index];
				}
			}

			// Token: 0x04000372 RID: 882
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000373 RID: 883
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x04000374 RID: 884
			[ReadOnly]
			public NativeArray<float3> framePositions;

			// Token: 0x04000375 RID: 885
			[ReadOnly]
			public NativeArray<quaternion> frameRotations;

			// Token: 0x04000376 RID: 886
			[WriteOnly]
			public NativeArray<float3> oldFramePositions;

			// Token: 0x04000377 RID: 887
			[WriteOnly]
			public NativeArray<quaternion> oldFrameRotations;
		}
	}
}
