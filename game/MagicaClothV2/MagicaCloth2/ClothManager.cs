using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace MagicaCloth2
{
	// Token: 0x0200006B RID: 107
	public class ClothManager : IManager, IDisposable, IValid
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public void Dispose()
		{
			this.isValid = false;
			this.clothSet.Clear();
			this.boneClothSet.Clear();
			this.meshClothSet.Clear();
			MagicaManager.afterEarlyUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterEarlyUpdateDelegate, new MagicaManager.UpdateMethod(this.TransformRestoreUpdate));
			MagicaManager.afterLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterLateUpdateDelegate, new MagicaManager.UpdateMethod(this.StartClothUpdate));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000E571 File Offset: 0x0000C771
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000E57C File Offset: 0x0000C77C
		public void Initialize()
		{
			this.clothSet.Clear();
			this.boneClothSet.Clear();
			this.meshClothSet.Clear();
			MagicaManager.afterEarlyUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterEarlyUpdateDelegate, new MagicaManager.UpdateMethod(this.TransformRestoreUpdate));
			MagicaManager.afterLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterLateUpdateDelegate, new MagicaManager.UpdateMethod(this.StartClothUpdate));
			this.isValid = true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000E5F1 File Offset: 0x0000C7F1
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		private void ClearMasterJob()
		{
			this.masterJob = default(JobHandle);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000E607 File Offset: 0x0000C807
		private void CompleteMasterJob()
		{
			this.masterJob.Complete();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000E614 File Offset: 0x0000C814
		internal int AddCloth(ClothProcess cprocess, in ClothParameters clothParams)
		{
			if (!this.isValid)
			{
				return 0;
			}
			this.clothSet.Add(cprocess);
			if (cprocess.clothType == ClothProcess.ClothType.BoneCloth)
			{
				this.boneClothSet.Add(cprocess);
			}
			else
			{
				this.meshClothSet.Add(cprocess);
			}
			return MagicaManager.Team.AddTeam(cprocess, clothParams);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000E670 File Offset: 0x0000C870
		internal void RemoveCloth(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			MagicaManager.Team.RemoveTeam(cprocess.TeamId);
			this.clothSet.Remove(cprocess);
			this.boneClothSet.Remove(cprocess);
			this.meshClothSet.Remove(cprocess);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		private void TransformRestoreUpdate()
		{
			this.ClearMasterJob();
			this.masterJob = MagicaManager.Bone.RestoreTransform(this.masterJob);
			this.CompleteMasterJob();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		private void StartClothUpdate()
		{
			if (!MagicaManager.IsPlaying())
			{
				return;
			}
			TeamManager team = MagicaManager.Team;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			SimulationManager simulation = MagicaManager.Simulation;
			TransformManager bone = MagicaManager.Bone;
			this.ClearMasterJob();
			team.AlwaysTeamUpdate();
			int value = team.maxUpdateCount.Value;
			simulation.WorkBufferUpdate();
			this.masterJob = bone.ReadTransform(this.masterJob);
			this.masterJob = vmesh.PreProxyMeshUpdate(this.masterJob);
			this.masterJob = team.CalcCenterAndInertia(this.masterJob);
			this.masterJob = simulation.PreSimulationUpdate(this.masterJob);
			this.masterJob = MagicaManager.Collider.PreSimulationUpdate(this.masterJob);
			for (int i = 0; i < value; i++)
			{
				this.masterJob = simulation.SimulationStepUpdate(value, i, this.masterJob);
			}
			this.masterJob = simulation.CalcDisplayPosition(this.masterJob);
			this.masterJob = vmesh.PostProxyMeshUpdate(this.masterJob);
			if (team.MappingCount > 0)
			{
				this.masterJob = vmesh.PostMappingMeshUpdate(this.masterJob);
				foreach (ClothProcess clothProcess in this.meshClothSet)
				{
					if (clothProcess != null && clothProcess.IsValid() && clothProcess.IsEnable)
					{
						int count = clothProcess.renderMeshInfoList.Count;
						for (int j = 0; j < count; j++)
						{
							ClothProcess.RenderMeshInfo renderMeshInfo = clothProcess.renderMeshInfoList[j];
							RenderData rendererData = MagicaManager.Render.GetRendererData(renderMeshInfo.renderHandle);
							this.masterJob = rendererData.UpdatePositionNormal(renderMeshInfo.mappingChunk, this.masterJob);
							if (rendererData.ChangeCustomMesh)
							{
								this.masterJob = rendererData.UpdateBoneWeight(renderMeshInfo.mappingChunk, this.masterJob);
							}
						}
					}
				}
			}
			this.masterJob = bone.WriteTransform(this.masterJob);
			this.masterJob = MagicaManager.Collider.PostSimulationUpdate(this.masterJob);
			this.masterJob = team.PostTeamUpdate(this.masterJob);
			JobHandle.ScheduleBatchedJobs();
			Action onPreSimulation = MagicaManager.OnPreSimulation;
			if (onPreSimulation != null)
			{
				onPreSimulation();
			}
			this.CompleteMasterJob();
			Action onPostSimulation = MagicaManager.OnPostSimulation;
			if (onPostSimulation == null)
			{
				return;
			}
			onPostSimulation();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000E940 File Offset: 0x0000CB40
		public ClothManager()
		{
		}

		// Token: 0x040002C1 RID: 705
		internal HashSet<ClothProcess> clothSet = new HashSet<ClothProcess>();

		// Token: 0x040002C2 RID: 706
		internal HashSet<ClothProcess> boneClothSet = new HashSet<ClothProcess>();

		// Token: 0x040002C3 RID: 707
		internal HashSet<ClothProcess> meshClothSet = new HashSet<ClothProcess>();

		// Token: 0x040002C4 RID: 708
		private JobHandle masterJob;

		// Token: 0x040002C5 RID: 709
		private bool isValid;
	}
}
