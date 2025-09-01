using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000038 RID: 56
	public class SelfCollisionConstraint : IDisposable
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000889D File Offset: 0x00006A9D
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000088A5 File Offset: 0x00006AA5
		public int PointPrimitiveCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PointPrimitiveCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PointPrimitiveCount>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000088AE File Offset: 0x00006AAE
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000088B6 File Offset: 0x00006AB6
		public int EdgePrimitiveCount
		{
			[CompilerGenerated]
			get
			{
				return this.<EdgePrimitiveCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EdgePrimitiveCount>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000088BF File Offset: 0x00006ABF
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000088C7 File Offset: 0x00006AC7
		public int TrianglePrimitiveCount
		{
			[CompilerGenerated]
			get
			{
				return this.<TrianglePrimitiveCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TrianglePrimitiveCount>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000088D0 File Offset: 0x00006AD0
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000088D8 File Offset: 0x00006AD8
		public int IntersectCount
		{
			[CompilerGenerated]
			get
			{
				return this.<IntersectCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IntersectCount>k__BackingField = value;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000088E4 File Offset: 0x00006AE4
		public SelfCollisionConstraint()
		{
			this.primitiveArray = new ExNativeArray<SelfCollisionConstraint.Primitive>(0, true);
			this.sortAndSweepArray = new ExNativeArray<SelfCollisionConstraint.SortData>(0, true);
			this.edgeEdgeContactQueue = new NativeQueue<SelfCollisionConstraint.EdgeEdgeContact>(Allocator.Persistent);
			this.pointTriangleContactQueue = new NativeQueue<SelfCollisionConstraint.PointTriangleContact>(Allocator.Persistent);
			this.edgeEdgeContactList = new NativeList<SelfCollisionConstraint.EdgeEdgeContact>(Allocator.Persistent);
			this.pointTriangleContactList = new NativeList<SelfCollisionConstraint.PointTriangleContact>(Allocator.Persistent);
			this.intersectFlagArray = new NativeArray<byte>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00008964 File Offset: 0x00006B64
		public void Dispose()
		{
			ExNativeArray<SelfCollisionConstraint.Primitive> exNativeArray = this.primitiveArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			this.primitiveArray = null;
			ExNativeArray<SelfCollisionConstraint.SortData> exNativeArray2 = this.sortAndSweepArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			this.sortAndSweepArray = null;
			this.PointPrimitiveCount = 0;
			this.EdgePrimitiveCount = 0;
			this.TrianglePrimitiveCount = 0;
			this.edgeEdgeContactQueue.Dispose();
			this.pointTriangleContactQueue.Dispose();
			this.edgeEdgeContactList.Dispose();
			this.pointTriangleContactList.Dispose();
			ref this.intersectFlagArray.DisposeSafe<byte>();
			this.IntersectCount = 0;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000089F4 File Offset: 0x00006BF4
		public bool HasPrimitive()
		{
			return this.PointPrimitiveCount + this.EdgePrimitiveCount + this.TrianglePrimitiveCount > 0;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008A0D File Offset: 0x00006C0D
		internal void Register(ClothProcess cprocess)
		{
			this.UpdateTeam(cprocess.TeamId);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008A1B File Offset: 0x00006C1B
		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				this.UpdateTeam(cprocess.TeamId);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008A38 File Offset: 0x00006C38
		internal void UpdateTeam(int teamId)
		{
			TeamManager team = MagicaManager.Team;
			if (!team.ContainsTeamData(teamId))
			{
				return;
			}
			TeamManager.TeamData teamData = team.GetTeamData(teamId);
			BitField32 flag = teamData.flag;
			bool flag2 = teamData.flag.IsSet(10);
			ClothParameters parameters = team.GetParameters(teamId);
			int num = (int)(flag2 ? SelfCollisionConstraint.SelfCollisionMode.None : parameters.selfCollisionConstraint.selfMode);
			SelfCollisionConstraint.SelfCollisionMode selfCollisionMode = flag2 ? SelfCollisionConstraint.SelfCollisionMode.None : parameters.selfCollisionConstraint.syncMode;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool value = false;
			bool value2 = false;
			bool value3 = false;
			bool value4 = false;
			bool value5 = false;
			bool value6 = false;
			bool value7 = false;
			bool value8 = false;
			bool value9 = false;
			bool value10 = false;
			bool value11 = false;
			bool value12 = false;
			bool value13 = false;
			bool value14 = false;
			bool value15 = false;
			if (num == 2)
			{
				if (teamData.EdgeCount > 0)
				{
					value = true;
					flag4 = true;
				}
				if (teamData.TriangleCount > 0)
				{
					value2 = true;
					value3 = true;
					flag3 = true;
					flag5 = true;
				}
				if (teamData.EdgeCount > 0 && teamData.TriangleCount > 0)
				{
					value4 = true;
					value5 = true;
				}
			}
			if (selfCollisionMode != SelfCollisionConstraint.SelfCollisionMode.None && team.ContainsTeamData(teamData.syncTeamId))
			{
				TeamManager.TeamData teamData2 = team.GetTeamData(teamData.syncTeamId);
				if (selfCollisionMode == SelfCollisionConstraint.SelfCollisionMode.FullMesh)
				{
					if (teamData.EdgeCount > 0 && teamData2.EdgeCount > 0)
					{
						value6 = true;
						flag4 = true;
					}
					if (teamData.TriangleCount > 0)
					{
						value8 = true;
						flag5 = true;
					}
					if (teamData2.TriangleCount > 0)
					{
						value7 = true;
						flag3 = true;
					}
					if (teamData.EdgeCount > 0 && teamData2.TriangleCount > 0)
					{
						value9 = true;
					}
					if (teamData.TriangleCount > 0 && teamData2.EdgeCount > 0)
					{
						value10 = true;
					}
				}
			}
			if (teamData.syncParentTeamId.Length > 0 && !flag2)
			{
				for (int i = 0; i < teamData.syncParentTeamId.Length; i++)
				{
					int teamId2 = teamData.syncParentTeamId[i];
					TeamManager.TeamData teamData3 = team.GetTeamData(teamId2);
					if (teamData3.IsValid && team.GetParameters(teamId2).selfCollisionConstraint.syncMode == SelfCollisionConstraint.SelfCollisionMode.FullMesh)
					{
						if (teamData3.EdgeCount > 0 && teamData.EdgeCount > 0)
						{
							value11 = true;
							flag4 = true;
						}
						if (teamData3.TriangleCount > 0)
						{
							value12 = true;
							flag3 = true;
						}
						if (teamData.TriangleCount > 0)
						{
							value13 = true;
							flag5 = true;
						}
						if (teamData.EdgeCount > 0 && teamData3.TriangleCount > 0)
						{
							value14 = true;
						}
						if (teamData.TriangleCount > 0 && teamData3.EdgeCount > 0)
						{
							value15 = true;
						}
					}
				}
			}
			teamData.flag.SetBits(13, flag3);
			teamData.flag.SetBits(14, flag4);
			teamData.flag.SetBits(15, flag5);
			teamData.flag.SetBits(16, value);
			teamData.flag.SetBits(19, value2);
			teamData.flag.SetBits(22, value3);
			teamData.flag.SetBits(25, value4);
			teamData.flag.SetBits(28, value5);
			teamData.flag.SetBits(17, value6);
			teamData.flag.SetBits(20, value7);
			teamData.flag.SetBits(23, value8);
			teamData.flag.SetBits(26, value9);
			teamData.flag.SetBits(29, value10);
			teamData.flag.SetBits(18, value11);
			teamData.flag.SetBits(21, value12);
			teamData.flag.SetBits(24, value13);
			teamData.flag.SetBits(27, value14);
			teamData.flag.SetBits(30, value15);
			if (flag3 && !teamData.selfPointChunk.IsValid)
			{
				int particleCount = teamData.ParticleCount;
				teamData.selfPointChunk = this.primitiveArray.AddRange(particleCount);
				this.sortAndSweepArray.AddRange(particleCount);
				int startIndex = teamData.selfPointChunk.startIndex;
				this.InitPrimitive(teamId, teamData, 0U, startIndex, startIndex, particleCount);
				this.PointPrimitiveCount += particleCount;
			}
			else if (!flag3 && teamData.selfPointChunk.IsValid)
			{
				this.primitiveArray.Remove(teamData.selfPointChunk);
				this.sortAndSweepArray.Remove(teamData.selfPointChunk);
				this.PointPrimitiveCount -= teamData.selfPointChunk.dataLength;
				teamData.selfPointChunk.Clear();
			}
			if (flag4 && !teamData.selfEdgeChunk.IsValid)
			{
				int edgeCount = teamData.EdgeCount;
				teamData.selfEdgeChunk = this.primitiveArray.AddRange(edgeCount);
				this.sortAndSweepArray.AddRange(edgeCount);
				int startIndex2 = teamData.selfEdgeChunk.startIndex;
				this.InitPrimitive(teamId, teamData, 1U, startIndex2, startIndex2, edgeCount);
				this.EdgePrimitiveCount += edgeCount;
			}
			else if (!flag4 && teamData.selfEdgeChunk.IsValid)
			{
				this.primitiveArray.Remove(teamData.selfEdgeChunk);
				this.sortAndSweepArray.Remove(teamData.selfEdgeChunk);
				this.EdgePrimitiveCount -= teamData.selfEdgeChunk.dataLength;
				teamData.selfEdgeChunk.Clear();
			}
			if (flag5 && !teamData.selfTriangleChunk.IsValid)
			{
				int triangleCount = teamData.TriangleCount;
				teamData.selfTriangleChunk = this.primitiveArray.AddRange(triangleCount);
				this.sortAndSweepArray.AddRange(triangleCount);
				int startIndex3 = teamData.selfTriangleChunk.startIndex;
				this.InitPrimitive(teamId, teamData, 2U, startIndex3, startIndex3, triangleCount);
				this.TrianglePrimitiveCount += triangleCount;
			}
			else if (!flag5 && teamData.selfTriangleChunk.IsValid)
			{
				this.primitiveArray.Remove(teamData.selfTriangleChunk);
				this.sortAndSweepArray.Remove(teamData.selfTriangleChunk);
				this.TrianglePrimitiveCount -= teamData.selfTriangleChunk.dataLength;
				teamData.selfTriangleChunk.Clear();
			}
			bool flag6 = teamData.flag.TestAny(25, 6);
			bool flag7 = flag.TestAny(25, 6);
			if (flag6 && !flag7)
			{
				int intersectCount = this.IntersectCount;
				this.IntersectCount = intersectCount + 1;
			}
			else if (!flag6 && flag7)
			{
				int intersectCount = this.IntersectCount;
				this.IntersectCount = intersectCount - 1;
			}
			team.SetTeamData(teamId, teamData);
			if (selfCollisionMode != SelfCollisionConstraint.SelfCollisionMode.None && team.ContainsTeamData(teamData.syncTeamId))
			{
				this.UpdateTeam(teamData.syncTeamId);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009068 File Offset: 0x00007268
		private void InitPrimitive(int teamId, TeamManager.TeamData tdata, uint kind, int startPrimitive, int startSort, int length)
		{
			new SelfCollisionConstraint.InitPrimitiveJob
			{
				teamId = teamId,
				tdata = tdata,
				kind = kind,
				startPrimitive = startPrimitive,
				startSort = startSort,
				edges = MagicaManager.VMesh.edges.GetNativeArray(),
				triangles = MagicaManager.VMesh.triangles.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				sortArray = this.sortAndSweepArray.GetNativeArray()
			}.Run(length);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009100 File Offset: 0x00007300
		internal void WorkBufferUpdate()
		{
			if (this.IntersectCount > 0)
			{
				int particleCount = MagicaManager.Simulation.ParticleCount;
				ref this.intersectFlagArray.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009130 File Offset: 0x00007330
		private unsafe static int BinarySearchSortAndlSweep(ref NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray, in SelfCollisionConstraint.SortData sd, in DataChunk chunk)
		{
			SelfCollisionConstraint.SortData* ptr = (SelfCollisionConstraint.SortData*)sortAndSweepArray.GetUnsafeReadOnlyPtr<SelfCollisionConstraint.SortData>();
			ptr += chunk.startIndex;
			int num = NativeSortExtension.BinarySearch<SelfCollisionConstraint.SortData>(ptr, chunk.dataLength, sd);
			if (num < 0)
			{
				num = math.max(-num - 1, 0);
			}
			return num + chunk.startIndex;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009186 File Offset: 0x00007386
		internal JobHandle SolverConstraint(int updateIndex, JobHandle jobHandle)
		{
			jobHandle = this.SolverRuntimeSelfCollision(updateIndex, jobHandle);
			jobHandle = this.SolveIntersect(jobHandle);
			return jobHandle;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000919C File Offset: 0x0000739C
		private JobHandle SolverRuntimeSelfCollision(int updateIndex, JobHandle jobHandle)
		{
			if (!this.HasPrimitive())
			{
				return jobHandle;
			}
			if (updateIndex == 0)
			{
				jobHandle = this.SolverBroadPhase(jobHandle);
			}
			else
			{
				jobHandle = this.UpdateBroadPhase(jobHandle);
			}
			SimulationManager simulation = MagicaManager.Simulation;
			for (int i = 0; i < 4; i++)
			{
				jobHandle = new SelfCollisionConstraint.SolverEdgeEdgeJob
				{
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					edgeEdgeContactArray = this.edgeEdgeContactList.AsDeferredJobArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule(this.edgeEdgeContactList, 16, jobHandle);
				jobHandle = new SelfCollisionConstraint.SolverPointTriangleJob
				{
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					pointTriangleContactArray = this.pointTriangleContactList.AsDeferredJobArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule(this.pointTriangleContactList, 16, jobHandle);
				jobHandle = InterlockUtility.SolveAggregateBufferAndClear(simulation.processingSelfParticle, 0f, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000092A4 File Offset: 0x000074A4
		private JobHandle SolverBroadPhase(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new SelfCollisionConstraint.ClearBufferJob
			{
				edgeEdgeContactQueue = this.edgeEdgeContactQueue,
				pointTriangleContactQueue = this.pointTriangleContactQueue
			}.Schedule(jobHandle);
			if (this.PointPrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.UpdatePrimitiveJob
				{
					kind = 0U,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					depthArray = vmesh.vertexDepths.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					oldPosArray = simulation.oldPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingArray = simulation.processingSelfPointTriangle.Buffer
				}.Schedule(simulation.processingSelfPointTriangle.GetJobSchedulePtr(), 16, jobHandle);
			}
			if (this.EdgePrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.UpdatePrimitiveJob
				{
					kind = 1U,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					depthArray = vmesh.vertexDepths.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					oldPosArray = simulation.oldPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingArray = simulation.processingSelfEdgeEdge.Buffer
				}.Schedule(simulation.processingSelfEdgeEdge.GetJobSchedulePtr(), 16, jobHandle);
			}
			if (this.TrianglePrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.UpdatePrimitiveJob
				{
					kind = 2U,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					depthArray = vmesh.vertexDepths.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					oldPosArray = simulation.oldPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingArray = simulation.processingSelfTrianglePoint.Buffer
				}.Schedule(simulation.processingSelfTrianglePoint.GetJobSchedulePtr(), 16, jobHandle);
			}
			jobHandle = new SelfCollisionConstraint.SortJob
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				sortAndSweepArray = this.sortAndSweepArray.GetNativeArray()
			}.Schedule(team.TeamCount * 3, 1, jobHandle);
			if (this.EdgePrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.EdgeEdgeBroadPhaseJob
				{
					teamDataArray = team.teamDataArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingEdgeEdgeArray = simulation.processingSelfEdgeEdge.Buffer,
					edgeEdgeContactQueue = this.edgeEdgeContactQueue.AsParallelWriter(),
					intersectFlagArray = this.intersectFlagArray
				}.Schedule(simulation.processingSelfEdgeEdge.GetJobSchedulePtr(), 16, jobHandle);
			}
			if (this.PointPrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.PointTriangleBroadPhaseJob
				{
					mainKind = 0U,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					triangles = vmesh.triangles.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					oldPosArray = simulation.oldPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingPointTriangleArray = simulation.processingSelfPointTriangle.Buffer,
					pointTriangleContactQueue = this.pointTriangleContactQueue.AsParallelWriter(),
					intersectFlagArray = this.intersectFlagArray
				}.Schedule(simulation.processingSelfPointTriangle.GetJobSchedulePtr(), 16, jobHandle);
			}
			if (this.TrianglePrimitiveCount > 0)
			{
				jobHandle = new SelfCollisionConstraint.PointTriangleBroadPhaseJob
				{
					mainKind = 2U,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					triangles = vmesh.triangles.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					oldPosArray = simulation.oldPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					primitiveArray = this.primitiveArray.GetNativeArray(),
					sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
					processingPointTriangleArray = simulation.processingSelfTrianglePoint.Buffer,
					pointTriangleContactQueue = this.pointTriangleContactQueue.AsParallelWriter(),
					intersectFlagArray = this.intersectFlagArray
				}.Schedule(simulation.processingSelfTrianglePoint.GetJobSchedulePtr(), 16, jobHandle);
			}
			JobHandle job = new SelfCollisionConstraint.EdgeEdgeToListJob
			{
				edgeEdgeContactQueue = this.edgeEdgeContactQueue,
				edgeEdgeContactList = this.edgeEdgeContactList
			}.Schedule(jobHandle);
			JobHandle job2 = new SelfCollisionConstraint.PointTriangleToListJob
			{
				pointTriangleContactQueue = this.pointTriangleContactQueue,
				pointTriangleContactList = this.pointTriangleContactList
			}.Schedule(jobHandle);
			jobHandle = JobHandle.CombineDependencies(job, job2);
			return jobHandle;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000098D0 File Offset: 0x00007AD0
		private JobHandle UpdateBroadPhase(JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			jobHandle = new SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseJob
			{
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				oldPosArray = simulation.oldPosArray.GetNativeArray(),
				edgeEdgeContactList = this.edgeEdgeContactList
			}.Schedule(this.edgeEdgeContactList, 16, jobHandle);
			jobHandle = new SelfCollisionConstraint.UpdatePointTriangleBroadPhaseJob
			{
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				oldPosArray = simulation.oldPosArray.GetNativeArray(),
				pointTriangleContactList = this.pointTriangleContactList
			}.Schedule(this.pointTriangleContactList, 16, jobHandle);
			return jobHandle;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009978 File Offset: 0x00007B78
		private JobHandle SolveIntersect(JobHandle jobHandle)
		{
			if (this.IntersectCount == 0)
			{
				return jobHandle;
			}
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			jobHandle = JobUtility.Fill(this.intersectFlagArray, this.intersectFlagArray.Length, 0, jobHandle);
			jobHandle = new SelfCollisionConstraint.IntersectUpdatePrimitiveJob
			{
				kind = 1U,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				processingArray = simulation.processingSelfEdgeEdge.Buffer
			}.Schedule(simulation.processingSelfEdgeEdge.GetJobSchedulePtr(), 16, jobHandle);
			jobHandle = new SelfCollisionConstraint.IntersectUpdatePrimitiveJob
			{
				kind = 2U,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				processingArray = simulation.processingSelfTrianglePoint.Buffer
			}.Schedule(simulation.processingSelfTrianglePoint.GetJobSchedulePtr(), 16, jobHandle);
			int execNumber = simulation.SimulationStepCount % 8;
			jobHandle = new SelfCollisionConstraint.IntersectEdgeTriangleJob
			{
				mainKind = 1U,
				execNumber = execNumber,
				div = 8,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
				processingEdgeEdgeArray = simulation.processingSelfEdgeEdge.Buffer,
				intersectFlagArray = this.intersectFlagArray
			}.Schedule(simulation.processingSelfEdgeEdge.GetJobSchedulePtr(), 16, jobHandle);
			jobHandle = new SelfCollisionConstraint.IntersectEdgeTriangleJob
			{
				mainKind = 2U,
				execNumber = execNumber,
				div = 8,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				primitiveArray = this.primitiveArray.GetNativeArray(),
				sortAndSweepArray = this.sortAndSweepArray.GetNativeArray(),
				processingEdgeEdgeArray = simulation.processingSelfTrianglePoint.Buffer,
				intersectFlagArray = this.intersectFlagArray
			}.Schedule(simulation.processingSelfTrianglePoint.GetJobSchedulePtr(), 16, jobHandle);
			return jobHandle;
		}

		// Token: 0x0400015F RID: 351
		public const uint KindPoint = 0U;

		// Token: 0x04000160 RID: 352
		public const uint KindEdge = 1U;

		// Token: 0x04000161 RID: 353
		public const uint KindTriangle = 2U;

		// Token: 0x04000162 RID: 354
		public const uint Flag_KindMask = 50331648U;

		// Token: 0x04000163 RID: 355
		public const uint Flag_Fix0 = 67108864U;

		// Token: 0x04000164 RID: 356
		public const uint Flag_Fix1 = 134217728U;

		// Token: 0x04000165 RID: 357
		public const uint Flag_Fix2 = 268435456U;

		// Token: 0x04000166 RID: 358
		public const uint Flag_AllFix = 536870912U;

		// Token: 0x04000167 RID: 359
		public const uint Flag_Ignore = 1073741824U;

		// Token: 0x04000168 RID: 360
		public const uint Flag_Enable = 2147483648U;

		// Token: 0x04000169 RID: 361
		private ExNativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

		// Token: 0x0400016A RID: 362
		private ExNativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;

		// Token: 0x0400016B RID: 363
		[CompilerGenerated]
		private int <PointPrimitiveCount>k__BackingField;

		// Token: 0x0400016C RID: 364
		[CompilerGenerated]
		private int <EdgePrimitiveCount>k__BackingField;

		// Token: 0x0400016D RID: 365
		[CompilerGenerated]
		private int <TrianglePrimitiveCount>k__BackingField;

		// Token: 0x0400016E RID: 366
		private NativeQueue<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactQueue;

		// Token: 0x0400016F RID: 367
		private NativeList<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactList;

		// Token: 0x04000170 RID: 368
		private NativeQueue<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactQueue;

		// Token: 0x04000171 RID: 369
		private NativeList<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactList;

		// Token: 0x04000172 RID: 370
		private NativeArray<byte> intersectFlagArray;

		// Token: 0x04000173 RID: 371
		[CompilerGenerated]
		private int <IntersectCount>k__BackingField;

		// Token: 0x02000039 RID: 57
		public enum SelfCollisionMode
		{
			// Token: 0x04000175 RID: 373
			None,
			// Token: 0x04000176 RID: 374
			FullMesh = 2
		}

		// Token: 0x0200003A RID: 58
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00009BB3 File Offset: 0x00007DB3
			public SerializeData()
			{
				this.selfMode = SelfCollisionConstraint.SelfCollisionMode.None;
				this.syncMode = SelfCollisionConstraint.SelfCollisionMode.None;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00009BE4 File Offset: 0x00007DE4
			public void DataValidate()
			{
				this.surfaceThickness.DataValidate(0.001f, 0.05f);
				this.clothMass = Mathf.Clamp01(this.clothMass);
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00009C0C File Offset: 0x00007E0C
			public SelfCollisionConstraint.SerializeData Clone()
			{
				return new SelfCollisionConstraint.SerializeData
				{
					selfMode = this.selfMode,
					surfaceThickness = this.surfaceThickness.Clone(),
					syncMode = this.syncMode,
					syncPartner = this.syncPartner,
					clothMass = this.clothMass
				};
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00009C5F File Offset: 0x00007E5F
			public MagicaCloth GetSyncPartner()
			{
				if (this.syncMode == SelfCollisionConstraint.SelfCollisionMode.None)
				{
					return null;
				}
				return this.syncPartner;
			}

			// Token: 0x04000177 RID: 375
			public SelfCollisionConstraint.SelfCollisionMode selfMode;

			// Token: 0x04000178 RID: 376
			public CurveSerializeData surfaceThickness = new CurveSerializeData(0.005f, 0.5f, 1f, false);

			// Token: 0x04000179 RID: 377
			public SelfCollisionConstraint.SelfCollisionMode syncMode;

			// Token: 0x0400017A RID: 378
			public MagicaCloth syncPartner;

			// Token: 0x0400017B RID: 379
			[Range(0f, 1f)]
			public float clothMass;
		}

		// Token: 0x0200003B RID: 59
		public struct SelfCollisionConstraintParams
		{
			// Token: 0x060000D4 RID: 212 RVA: 0x00009C71 File Offset: 0x00007E71
			public void Convert(SelfCollisionConstraint.SerializeData sdata)
			{
				this.selfMode = sdata.selfMode;
				this.surfaceThicknessCurveData = sdata.surfaceThickness.ConvertFloatArray();
				this.syncMode = sdata.syncMode;
				this.clothMass = sdata.clothMass;
			}

			// Token: 0x0400017C RID: 380
			public SelfCollisionConstraint.SelfCollisionMode selfMode;

			// Token: 0x0400017D RID: 381
			public float4x4 surfaceThicknessCurveData;

			// Token: 0x0400017E RID: 382
			public SelfCollisionConstraint.SelfCollisionMode syncMode;

			// Token: 0x0400017F RID: 383
			public float clothMass;
		}

		// Token: 0x0200003C RID: 60
		private struct Primitive
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x00009CA8 File Offset: 0x00007EA8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool IsIgnore()
			{
				return (this.flagAndTeamId & 1073741824U) > 0U;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00009CB9 File Offset: 0x00007EB9
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool HasParticle(int p)
			{
				return p >= 0 && !math.all(this.particleIndices - p);
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00009CD5 File Offset: 0x00007ED5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public uint GetKind()
			{
				return (this.flagAndTeamId & 50331648U) >> 24;
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x00009CE6 File Offset: 0x00007EE6
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int GetTeamId()
			{
				return (int)(this.flagAndTeamId & 16777215U);
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00009CF4 File Offset: 0x00007EF4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float GetSolveThickness(in SelfCollisionConstraint.Primitive pri)
			{
				return this.thickness + pri.thickness;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00009D04 File Offset: 0x00007F04
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool AnyParticle(in SelfCollisionConstraint.Primitive pri)
			{
				for (int i = 0; i < 3; i++)
				{
					int num = this.particleIndices[i];
					if (num >= 0 && !math.all(pri.particleIndices - num))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04000180 RID: 384
			public uint flagAndTeamId;

			// Token: 0x04000181 RID: 385
			public int sortIndex;

			// Token: 0x04000182 RID: 386
			public int3 particleIndices;

			// Token: 0x04000183 RID: 387
			public float3x3 nextPos;

			// Token: 0x04000184 RID: 388
			public float3x3 oldPos;

			// Token: 0x04000185 RID: 389
			public float3 invMass;

			// Token: 0x04000186 RID: 390
			public float thickness;
		}

		// Token: 0x0200003D RID: 61
		private struct SortData : IComparable<SelfCollisionConstraint.SortData>
		{
			// Token: 0x060000DB RID: 219 RVA: 0x00009D44 File Offset: 0x00007F44
			public int CompareTo(SelfCollisionConstraint.SortData other)
			{
				return (int)math.sign(this.firstMinMax.x - other.firstMinMax.x);
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00009D63 File Offset: 0x00007F63
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public uint GetKind()
			{
				return (this.flagAndTeamId & 50331648U) >> 24;
			}

			// Token: 0x04000187 RID: 391
			public uint flagAndTeamId;

			// Token: 0x04000188 RID: 392
			public int primitiveIndex;

			// Token: 0x04000189 RID: 393
			public float2 firstMinMax;

			// Token: 0x0400018A RID: 394
			public float2 secondMinMax;

			// Token: 0x0400018B RID: 395
			public float2 thirdMinMax;
		}

		// Token: 0x0200003E RID: 62
		internal struct EdgeEdgeContact
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00009D74 File Offset: 0x00007F74
			public override string ToString()
			{
				return string.Format("EdgeEdge f0:{0:X}, f1:{1:X}, p0:{2}, p1:{3}, inv0:{4}, inv1:{5}", new object[]
				{
					this.flagAndTeamId0,
					this.flagAndTeamId1,
					this.edgeParticleIndex0,
					this.edgeParticleIndex1,
					this.edgeInvMass0,
					this.edgeInvMass1
				});
			}

			// Token: 0x0400018C RID: 396
			public uint flagAndTeamId0;

			// Token: 0x0400018D RID: 397
			public uint flagAndTeamId1;

			// Token: 0x0400018E RID: 398
			public half thickness;

			// Token: 0x0400018F RID: 399
			public half s;

			// Token: 0x04000190 RID: 400
			public half t;

			// Token: 0x04000191 RID: 401
			public half3 n;

			// Token: 0x04000192 RID: 402
			public half2 edgeInvMass0;

			// Token: 0x04000193 RID: 403
			public half2 edgeInvMass1;

			// Token: 0x04000194 RID: 404
			public int2 edgeParticleIndex0;

			// Token: 0x04000195 RID: 405
			public int2 edgeParticleIndex1;
		}

		// Token: 0x0200003F RID: 63
		internal struct PointTriangleContact
		{
			// Token: 0x060000DE RID: 222 RVA: 0x00009DE8 File Offset: 0x00007FE8
			public override string ToString()
			{
				return string.Format("PointTriangle f0:{0:X}, f1:{1:X}, pp:{2}, pt:{3}, pinv:{4}, tinv:{5}", new object[]
				{
					this.flagAndTeamId0,
					this.flagAndTeamId1,
					this.pointParticleIndex,
					this.triangleParticleIndex,
					this.pointInvMass,
					this.triangleInvMass
				});
			}

			// Token: 0x04000196 RID: 406
			public uint flagAndTeamId0;

			// Token: 0x04000197 RID: 407
			public uint flagAndTeamId1;

			// Token: 0x04000198 RID: 408
			public half thickness;

			// Token: 0x04000199 RID: 409
			public half sign;

			// Token: 0x0400019A RID: 410
			public int pointParticleIndex;

			// Token: 0x0400019B RID: 411
			public int3 triangleParticleIndex;

			// Token: 0x0400019C RID: 412
			public half pointInvMass;

			// Token: 0x0400019D RID: 413
			public half3 triangleInvMass;
		}

		// Token: 0x02000040 RID: 64
		[BurstCompile]
		private struct InitPrimitiveJob : IJobParallelFor
		{
			// Token: 0x060000DF RID: 223 RVA: 0x00009E5C File Offset: 0x0000805C
			public void Execute(int index)
			{
				int num = this.startPrimitive + index;
				int num2 = this.startSort + index;
				SelfCollisionConstraint.Primitive value = this.primitiveArray[num];
				SelfCollisionConstraint.SortData value2 = this.sortArray[num2];
				int3 particleIndices = -1;
				int startIndex = this.tdata.particleChunk.startIndex;
				if (this.kind == 0U)
				{
					particleIndices[0] = startIndex + index;
				}
				else if (this.kind == 1U)
				{
					int startIndex2 = this.tdata.proxyEdgeChunk.startIndex;
					particleIndices.xy = this.edges[startIndex2 + index] + startIndex;
				}
				else if (this.kind == 2U)
				{
					int startIndex3 = this.tdata.proxyTriangleChunk.startIndex;
					particleIndices.xyz = this.triangles[startIndex3 + index] + startIndex;
				}
				value.flagAndTeamId = (uint)(this.teamId | (int)((int)this.kind << 24));
				value.sortIndex = num2;
				value.particleIndices = particleIndices;
				value2.primitiveIndex = num;
				value2.flagAndTeamId = (uint)this.teamId;
				this.primitiveArray[num] = value;
				this.sortArray[num2] = value2;
			}

			// Token: 0x0400019E RID: 414
			public int teamId;

			// Token: 0x0400019F RID: 415
			public TeamManager.TeamData tdata;

			// Token: 0x040001A0 RID: 416
			public uint kind;

			// Token: 0x040001A1 RID: 417
			public int startPrimitive;

			// Token: 0x040001A2 RID: 418
			public int startSort;

			// Token: 0x040001A3 RID: 419
			[ReadOnly]
			public NativeArray<int2> edges;

			// Token: 0x040001A4 RID: 420
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x040001A5 RID: 421
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001A6 RID: 422
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.SortData> sortArray;
		}

		// Token: 0x02000041 RID: 65
		[BurstCompile]
		private struct ClearBufferJob : IJob
		{
			// Token: 0x060000E0 RID: 224 RVA: 0x00009F8E File Offset: 0x0000818E
			public void Execute()
			{
				this.edgeEdgeContactQueue.Clear();
				this.pointTriangleContactQueue.Clear();
			}

			// Token: 0x040001A7 RID: 423
			[WriteOnly]
			public NativeQueue<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactQueue;

			// Token: 0x040001A8 RID: 424
			[WriteOnly]
			public NativeQueue<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactQueue;
		}

		// Token: 0x02000042 RID: 66
		[BurstCompile]
		private struct UpdatePrimitiveJob : IJobParallelForDefer
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x00009FA8 File Offset: 0x000081A8
			public unsafe void Execute(int index)
			{
				uint pack = this.processingArray[index];
				int index2 = DataUtility.Unpack32Hi(pack);
				int num = DataUtility.Unpack32Low(pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				int startIndex = teamData.particleChunk.startIndex;
				SelfCollisionConstraint.SelfCollisionConstraintParams selfCollisionConstraint = this.parameterArray[index2].selfCollisionConstraint;
				int index3 = 0;
				switch (this.kind)
				{
				case 0U:
					index3 = teamData.selfPointChunk.startIndex + num;
					break;
				case 1U:
					index3 = teamData.selfEdgeChunk.startIndex + num;
					break;
				case 2U:
					index3 = teamData.selfTriangleChunk.startIndex + num;
					break;
				}
				SelfCollisionConstraint.Primitive primitive = this.primitiveArray[index3];
				uint num2 = primitive.flagAndTeamId;
				int num3 = (int)(this.kind + 1U);
				uint num4 = 67108864U;
				bool flag = false;
				int num5 = 0;
				float num6 = 0f;
				for (int i = 0; i < num3; i++)
				{
					int num7 = primitive.particleIndices[i];
					*primitive.nextPos[i] = this.nextPosArray[num7];
					*primitive.oldPos[i] = this.oldPosArray[num7];
					int index4 = teamData.proxyCommonChunk.startIndex + num7 - startIndex;
					VertexAttribute vertexAttribute = this.attributes[index4];
					if (vertexAttribute.IsMove())
					{
						num2 &= ~num4;
					}
					else
					{
						num2 |= num4;
						num5++;
					}
					num4 <<= 1;
					if (vertexAttribute.IsInvalid())
					{
						flag = true;
					}
					primitive.invMass[i] = MathUtility.CalcSelfCollisionInverseMass(this.frictionArray[num7], vertexAttribute.IsDontMove(), selfCollisionConstraint.clothMass);
					num6 += this.depthArray[index4];
				}
				if (num5 == num3)
				{
					num2 |= 536870912U;
				}
				else
				{
					num2 &= 3758096383U;
				}
				if (flag)
				{
					num2 |= 1073741824U;
				}
				primitive.flagAndTeamId = num2;
				num6 /= (float)num3;
				float num8 = selfCollisionConstraint.surfaceThicknessCurveData.EvaluateCurve(num6);
				primitive.thickness = num8;
				this.primitiveArray[index3] = primitive;
				float3 @float = math.min(*primitive.nextPos[0], *primitive.oldPos[0]);
				float3 float2 = math.max(*primitive.nextPos[0], *primitive.oldPos[0]);
				AABB aabb = new AABB(ref @float, ref float2);
				for (int j = 1; j < num3; j++)
				{
					aabb.Encapsulate(primitive.nextPos[j]);
					aabb.Encapsulate(primitive.oldPos[j]);
				}
				aabb.Expand(num8);
				int sortIndex = primitive.sortIndex;
				SelfCollisionConstraint.SortData value = this.sortAndSweepArray[sortIndex];
				value.flagAndTeamId = primitive.flagAndTeamId;
				value.firstMinMax = new float2(aabb.Min.y, aabb.Max.y);
				value.secondMinMax = new float2(aabb.Min.x, aabb.Max.x);
				value.thirdMinMax = new float2(aabb.Min.z, aabb.Max.z);
				this.sortAndSweepArray[sortIndex] = value;
			}

			// Token: 0x040001A9 RID: 425
			public uint kind;

			// Token: 0x040001AA RID: 426
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001AB RID: 427
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040001AC RID: 428
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040001AD RID: 429
			[ReadOnly]
			public NativeArray<float> depthArray;

			// Token: 0x040001AE RID: 430
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001AF RID: 431
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040001B0 RID: 432
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x040001B1 RID: 433
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001B2 RID: 434
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;

			// Token: 0x040001B3 RID: 435
			[ReadOnly]
			public NativeArray<uint> processingArray;
		}

		// Token: 0x02000043 RID: 67
		[BurstCompile]
		private struct SortJob : IJobParallelFor
		{
			// Token: 0x060000E2 RID: 226 RVA: 0x0000A324 File Offset: 0x00008524
			public unsafe void Execute(int index)
			{
				int num = index / 3;
				int num2 = index % 3;
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				if (!teamData.IsProcess)
				{
					return;
				}
				DataChunk dataChunk = default(DataChunk);
				switch (num2)
				{
				case 0:
					dataChunk = teamData.selfPointChunk;
					break;
				case 1:
					dataChunk = teamData.selfEdgeChunk;
					break;
				case 2:
					dataChunk = teamData.selfTriangleChunk;
					break;
				}
				if (!dataChunk.IsValid)
				{
					return;
				}
				SelfCollisionConstraint.SortData* ptr = (SelfCollisionConstraint.SortData*)this.sortAndSweepArray.GetUnsafePtr<SelfCollisionConstraint.SortData>();
				ptr += dataChunk.startIndex;
				NativeSortExtension.Sort<SelfCollisionConstraint.SortData>(ptr, dataChunk.dataLength);
				for (int i = 0; i < dataChunk.dataLength; i++)
				{
					int num3 = dataChunk.startIndex + i;
					SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[num3];
					SelfCollisionConstraint.Primitive value = this.primitiveArray[sortData.primitiveIndex];
					value.sortIndex = num3;
					this.primitiveArray[sortData.primitiveIndex] = value;
				}
			}

			// Token: 0x040001B4 RID: 436
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001B5 RID: 437
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001B6 RID: 438
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;
		}

		// Token: 0x02000044 RID: 68
		[BurstCompile]
		private struct PointTriangleBroadPhaseJob : IJobParallelForDefer
		{
			// Token: 0x060000E3 RID: 227 RVA: 0x0000A420 File Offset: 0x00008620
			public void Execute(int index)
			{
				uint pack = this.processingPointTriangleArray[index];
				int index2 = DataUtility.Unpack32Hi(pack);
				int num = DataUtility.Unpack32Low(pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				bool flag = this.mainKind == 0U;
				ref DataChunk ptr = flag ? teamData.selfPointChunk : teamData.selfTriangleChunk;
				DataChunk dataChunk = flag ? teamData.selfTriangleChunk : teamData.selfPointChunk;
				int index3 = ptr.startIndex + num;
				SelfCollisionConstraint.Primitive primitive = this.primitiveArray[index3];
				SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[primitive.sortIndex];
				if (primitive.IsIgnore())
				{
					return;
				}
				if (teamData.flag.TestAny(25, 6))
				{
					int num2 = flag ? 1 : 3;
					for (int i = 0; i < num2; i++)
					{
						if (this.intersectFlagArray[primitive.particleIndices[i]] > 0)
						{
							return;
						}
					}
				}
				if (teamData.flag.IsSet(flag ? 19 : 22))
				{
					this.SweepTest(-1, ref primitive, sortData, dataChunk, true);
				}
				if (teamData.flag.IsSet(flag ? 20 : 23) && teamData.syncTeamId > 0)
				{
					TeamManager.TeamData teamData2 = this.teamDataArray[teamData.syncTeamId];
					int sortIndex = -1;
					DataChunk dataChunk2 = flag ? teamData2.selfTriangleChunk : teamData2.selfPointChunk;
					this.SweepTest(sortIndex, ref primitive, sortData, dataChunk2, false);
				}
				if (teamData.flag.IsSet(flag ? 21 : 24))
				{
					int length = teamData.syncParentTeamId.Length;
					for (int j = 0; j < length; j++)
					{
						int index4 = teamData.syncParentTeamId[j];
						TeamManager.TeamData teamData3 = this.teamDataArray[index4];
						if (teamData3.flag.IsSet(flag ? 23 : 20))
						{
							int sortIndex2 = -1;
							DataChunk dataChunk2 = flag ? teamData3.selfTriangleChunk : teamData3.selfPointChunk;
							this.SweepTest(sortIndex2, ref primitive, sortData, dataChunk2, false);
						}
					}
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x0000A60C File Offset: 0x0000880C
			private void SweepTest(int sortIndex, ref SelfCollisionConstraint.Primitive primitive0, in SelfCollisionConstraint.SortData sd0, in DataChunk subChunk, bool connectionCheck)
			{
				if (sortIndex < 0)
				{
					sortIndex = SelfCollisionConstraint.BinarySearchSortAndlSweep(ref this.sortAndSweepArray, sd0, subChunk);
				}
				float y = sd0.firstMinMax.y;
				int num = subChunk.startIndex + subChunk.dataLength;
				while (sortIndex < num)
				{
					SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[sortIndex];
					sortIndex++;
					if (sortData.firstMinMax.x > y)
					{
						break;
					}
					if (sortData.secondMinMax.y >= sd0.secondMinMax.x && sortData.secondMinMax.x <= sd0.secondMinMax.y && sortData.thirdMinMax.y >= sd0.thirdMinMax.x && sortData.thirdMinMax.x <= sd0.thirdMinMax.y)
					{
						SelfCollisionConstraint.Primitive primitive = this.primitiveArray[sortData.primitiveIndex];
						if (!primitive.IsIgnore() && (!connectionCheck || !primitive0.AnyParticle(primitive)) && ((primitive0.flagAndTeamId & 536870912U) == 0U || (primitive.flagAndTeamId & 536870912U) == 0U))
						{
							float solveThickness = primitive0.GetSolveThickness(primitive);
							float scr = solveThickness * 2f;
							if (solveThickness >= 0.0001f)
							{
								if (this.mainKind == 0U)
								{
									this.BroadPointTriangle(ref primitive0, ref primitive, solveThickness, scr, Define.System.SelfCollisionPointTriangleAngleCos);
								}
								else
								{
									this.BroadPointTriangle(ref primitive, ref primitive0, solveThickness, scr, Define.System.SelfCollisionPointTriangleAngleCos);
								}
							}
						}
					}
				}
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x0000A774 File Offset: 0x00008974
			private void BroadPointTriangle(ref SelfCollisionConstraint.Primitive p_pri, ref SelfCollisionConstraint.Primitive t_pri, float thickness, float scr, float ang)
			{
				float3 y = p_pri.nextPos.c0 - p_pri.oldPos.c0;
				float3 lhs = t_pri.nextPos.c0 - t_pri.oldPos.c0;
				float3 lhs2 = t_pri.nextPos.c1 - t_pri.oldPos.c1;
				float3 lhs3 = t_pri.nextPos.c2 - t_pri.oldPos.c2;
				float3 float2;
				float3 @float = MathUtility.ClosestPtPointTriangle(p_pri.oldPos.c0, t_pri.oldPos.c0, t_pri.oldPos.c1, t_pri.oldPos.c2, out float2);
				float3 y2 = lhs * float2.x + lhs2 * float2.y + lhs3 * float2.z;
				float3 float3 = @float - p_pri.oldPos.c0;
				float num = math.length(float3);
				if (num > 1E-08f)
				{
					float3 float4 = float3 / num;
					float num2 = math.dot(float4, y);
					float num3 = math.dot(float4, y2);
					if (num - num2 + num3 < thickness + scr)
					{
						float3 x = MathUtility.TriangleNormal(t_pri.oldPos.c0, t_pri.oldPos.c1, t_pri.oldPos.c2);
						float4 = math.normalize(p_pri.oldPos.c0 - @float);
						float x2 = math.dot(x, float4);
						if (math.abs(x2) < ang)
						{
							return;
						}
						float v = math.sign(x2);
						this.pointTriangleContactQueue.Enqueue(new SelfCollisionConstraint.PointTriangleContact
						{
							flagAndTeamId0 = (p_pri.flagAndTeamId | 2147483648U),
							flagAndTeamId1 = t_pri.flagAndTeamId,
							thickness = (half)thickness,
							sign = (half)v,
							pointParticleIndex = p_pri.particleIndices.x,
							triangleParticleIndex = t_pri.particleIndices,
							pointInvMass = (half)p_pri.invMass.x,
							triangleInvMass = (half3)t_pri.invMass
						});
					}
				}
			}

			// Token: 0x040001B7 RID: 439
			public uint mainKind;

			// Token: 0x040001B8 RID: 440
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001B9 RID: 441
			[ReadOnly]
			public NativeArray<int3> triangles;

			// Token: 0x040001BA RID: 442
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040001BB RID: 443
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001BC RID: 444
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040001BD RID: 445
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x040001BE RID: 446
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001BF RID: 447
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;

			// Token: 0x040001C0 RID: 448
			[ReadOnly]
			public NativeArray<uint> processingPointTriangleArray;

			// Token: 0x040001C1 RID: 449
			[WriteOnly]
			public NativeQueue<SelfCollisionConstraint.PointTriangleContact>.ParallelWriter pointTriangleContactQueue;

			// Token: 0x040001C2 RID: 450
			[ReadOnly]
			public NativeArray<byte> intersectFlagArray;
		}

		// Token: 0x02000045 RID: 69
		[BurstCompile]
		private struct EdgeEdgeBroadPhaseJob : IJobParallelForDefer
		{
			// Token: 0x060000E6 RID: 230 RVA: 0x0000A9AC File Offset: 0x00008BAC
			public void Execute(int index)
			{
				uint pack = this.processingEdgeEdgeArray[index];
				int index2 = DataUtility.Unpack32Hi(pack);
				int num = DataUtility.Unpack32Low(pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				int index3 = teamData.selfEdgeChunk.startIndex + num;
				SelfCollisionConstraint.Primitive primitive = this.primitiveArray[index3];
				if (primitive.IsIgnore())
				{
					return;
				}
				if (teamData.flag.TestAny(25, 6))
				{
					if (this.intersectFlagArray[primitive.particleIndices.x] > 0)
					{
						return;
					}
					if (this.intersectFlagArray[primitive.particleIndices.y] > 0)
					{
						return;
					}
				}
				SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[primitive.sortIndex];
				if (teamData.flag.IsSet(16))
				{
					this.SweepTest(primitive.sortIndex + 1, ref primitive, sortData, teamData.selfEdgeChunk, true);
				}
				if (teamData.flag.IsSet(17) && teamData.syncTeamId > 0)
				{
					this.SweepTest(-1, ref primitive, sortData, this.teamDataArray[teamData.syncTeamId].selfEdgeChunk, false);
				}
				if (teamData.flag.IsSet(18))
				{
					int length = teamData.syncParentTeamId.Length;
					for (int i = 0; i < length; i++)
					{
						int index4 = teamData.syncParentTeamId[i];
						TeamManager.TeamData teamData2 = this.teamDataArray[index4];
						if (teamData2.flag.IsSet(17))
						{
							this.SweepTest(-1, ref primitive, sortData, teamData2.selfEdgeChunk, false);
						}
					}
				}
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000AB3C File Offset: 0x00008D3C
			private void SweepTest(int sortIndex, ref SelfCollisionConstraint.Primitive primitive0, in SelfCollisionConstraint.SortData sd0, in DataChunk subChunk, bool connectionCheck)
			{
				if (sortIndex < 0)
				{
					sortIndex = SelfCollisionConstraint.BinarySearchSortAndlSweep(ref this.sortAndSweepArray, sd0, subChunk);
				}
				float y = sd0.firstMinMax.y;
				int num = subChunk.startIndex + subChunk.dataLength;
				while (sortIndex < num)
				{
					SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[sortIndex];
					sortIndex++;
					if (sortData.firstMinMax.x > y)
					{
						break;
					}
					if (sortData.secondMinMax.y >= sd0.secondMinMax.x && sortData.secondMinMax.x <= sd0.secondMinMax.y && sortData.thirdMinMax.y >= sd0.thirdMinMax.x && sortData.thirdMinMax.x <= sd0.thirdMinMax.y)
					{
						SelfCollisionConstraint.Primitive primitive = this.primitiveArray[sortData.primitiveIndex];
						if (!primitive.IsIgnore() && (!connectionCheck || !primitive0.AnyParticle(primitive)) && ((primitive0.flagAndTeamId & 536870912U) == 0U || (primitive.flagAndTeamId & 536870912U) == 0U))
						{
							float solveThickness = primitive0.GetSolveThickness(primitive);
							float scr = solveThickness * 2f;
							if (solveThickness >= 0.0001f)
							{
								this.BroadEdgeEdge(ref primitive0, ref primitive, solveThickness, scr);
							}
						}
					}
				}
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x0000AC80 File Offset: 0x00008E80
			private unsafe void BroadEdgeEdge(ref SelfCollisionConstraint.Primitive pri0, ref SelfCollisionConstraint.Primitive pri1, float thickness, float scr)
			{
				float num2;
				float num3;
				float3 lhs;
				float3 rhs;
				float num = math.sqrt(MathUtility.ClosestPtSegmentSegment(pri0.oldPos[0], pri0.oldPos[1], pri1.oldPos[0], pri1.oldPos[1], out num2, out num3, out lhs, out rhs));
				if (num < 1E-09f)
				{
					return;
				}
				float3 @float = (lhs - rhs) / num;
				float3 x = *pri0.nextPos[0] - *pri0.oldPos[0];
				float3 y = *pri0.nextPos[1] - *pri0.oldPos[1];
				float3 x2 = *pri1.nextPos[0] - *pri1.oldPos[0];
				float3 y2 = *pri1.nextPos[1] - *pri1.oldPos[1];
				float3 y3 = math.lerp(x, y, num2);
				float3 y4 = math.lerp(x2, y2, num3);
				float num4 = math.dot(@float, y3);
				float num5 = math.dot(@float, y4);
				if (num + num4 - num5 > thickness + scr)
				{
					return;
				}
				this.edgeEdgeContactQueue.Enqueue(new SelfCollisionConstraint.EdgeEdgeContact
				{
					flagAndTeamId0 = (pri0.flagAndTeamId | 2147483648U),
					flagAndTeamId1 = pri1.flagAndTeamId,
					thickness = (half)thickness,
					s = (half)num2,
					t = (half)num3,
					n = (half3)@float,
					edgeInvMass0 = (half2)pri0.invMass.xy,
					edgeInvMass1 = (half2)pri1.invMass.xy,
					edgeParticleIndex0 = pri0.particleIndices.xy,
					edgeParticleIndex1 = pri1.particleIndices.xy
				});
			}

			// Token: 0x040001C3 RID: 451
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001C4 RID: 452
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001C5 RID: 453
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;

			// Token: 0x040001C6 RID: 454
			[ReadOnly]
			public NativeArray<uint> processingEdgeEdgeArray;

			// Token: 0x040001C7 RID: 455
			[WriteOnly]
			public NativeQueue<SelfCollisionConstraint.EdgeEdgeContact>.ParallelWriter edgeEdgeContactQueue;

			// Token: 0x040001C8 RID: 456
			[ReadOnly]
			public NativeArray<byte> intersectFlagArray;
		}

		// Token: 0x02000046 RID: 70
		[BurstCompile]
		private struct EdgeEdgeToListJob : IJob
		{
			// Token: 0x060000E9 RID: 233 RVA: 0x0000AE84 File Offset: 0x00009084
			public void Execute()
			{
				this.edgeEdgeContactList.Clear();
				if (this.edgeEdgeContactQueue.Count > 0)
				{
					this.edgeEdgeContactList.AddRange(this.edgeEdgeContactQueue.ToArray(Allocator.Temp));
				}
			}

			// Token: 0x040001C9 RID: 457
			[ReadOnly]
			public NativeQueue<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactQueue;

			// Token: 0x040001CA RID: 458
			[NativeDisableParallelForRestriction]
			public NativeList<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactList;
		}

		// Token: 0x02000047 RID: 71
		[BurstCompile]
		private struct PointTriangleToListJob : IJob
		{
			// Token: 0x060000EA RID: 234 RVA: 0x0000AEBB File Offset: 0x000090BB
			public void Execute()
			{
				this.pointTriangleContactList.Clear();
				if (this.pointTriangleContactQueue.Count > 0)
				{
					this.pointTriangleContactList.AddRange(this.pointTriangleContactQueue.ToArray(Allocator.Temp));
				}
			}

			// Token: 0x040001CB RID: 459
			[ReadOnly]
			public NativeQueue<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactQueue;

			// Token: 0x040001CC RID: 460
			[NativeDisableParallelForRestriction]
			public NativeList<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactList;
		}

		// Token: 0x02000048 RID: 72
		[BurstCompile]
		private struct UpdateEdgeEdgeBroadPhaseJob : IJobParallelForDefer
		{
			// Token: 0x060000EB RID: 235 RVA: 0x0000AEF4 File Offset: 0x000090F4
			public void Execute(int index)
			{
				SelfCollisionConstraint.EdgeEdgeContact edgeEdgeContact = this.edgeEdgeContactList[index];
				float3 rhs = this.oldPosArray[edgeEdgeContact.edgeParticleIndex0.x];
				float3 rhs2 = this.oldPosArray[edgeEdgeContact.edgeParticleIndex0.y];
				float3 rhs3 = this.oldPosArray[edgeEdgeContact.edgeParticleIndex1.x];
				float3 rhs4 = this.oldPosArray[edgeEdgeContact.edgeParticleIndex1.y];
				float3 lhs = this.nextPosArray[edgeEdgeContact.edgeParticleIndex0.x];
				float3 lhs2 = this.nextPosArray[edgeEdgeContact.edgeParticleIndex0.y];
				float3 lhs3 = this.nextPosArray[edgeEdgeContact.edgeParticleIndex1.x];
				float3 lhs4 = this.nextPosArray[edgeEdgeContact.edgeParticleIndex1.y];
				float num2;
				float num3;
				float3 lhs5;
				float3 rhs5;
				float num = math.sqrt(MathUtility.ClosestPtSegmentSegment(rhs, rhs2, rhs3, rhs4, out num2, out num3, out lhs5, out rhs5));
				if (num < 1E-09f)
				{
					return;
				}
				float3 @float = (lhs5 - rhs5) / num;
				float3 x = lhs - rhs;
				float3 y = lhs2 - rhs2;
				float3 x2 = lhs3 - rhs3;
				float3 y2 = lhs4 - rhs4;
				float3 y3 = math.lerp(x, y, num2);
				float3 y4 = math.lerp(x2, y2, num3);
				float num4 = math.dot(@float, y3);
				float num5 = math.dot(@float, y4);
				float num6 = num + num4 - num5;
				float num7 = edgeEdgeContact.thickness;
				if (num6 > edgeEdgeContact.thickness + num7)
				{
					edgeEdgeContact.flagAndTeamId0 &= 2147483647U;
				}
				else
				{
					edgeEdgeContact.flagAndTeamId0 |= 2147483648U;
					edgeEdgeContact.s = (half)num2;
					edgeEdgeContact.t = (half)num3;
					edgeEdgeContact.n = (half3)@float;
				}
				this.edgeEdgeContactList[index] = edgeEdgeContact;
			}

			// Token: 0x040001CD RID: 461
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001CE RID: 462
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040001CF RID: 463
			[NativeDisableParallelForRestriction]
			public NativeList<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactList;
		}

		// Token: 0x02000049 RID: 73
		[BurstCompile]
		private struct UpdatePointTriangleBroadPhaseJob : IJobParallelForDefer
		{
			// Token: 0x060000EC RID: 236 RVA: 0x0000B0D8 File Offset: 0x000092D8
			public void Execute(int index)
			{
				SelfCollisionConstraint.PointTriangleContact pointTriangleContact = this.pointTriangleContactList[index];
				bool flag = false;
				float3 rhs = this.oldPosArray[pointTriangleContact.pointParticleIndex];
				float3 rhs2 = this.oldPosArray[pointTriangleContact.triangleParticleIndex.x];
				float3 rhs3 = this.oldPosArray[pointTriangleContact.triangleParticleIndex.y];
				float3 rhs4 = this.oldPosArray[pointTriangleContact.triangleParticleIndex.z];
				float3 lhs = this.nextPosArray[pointTriangleContact.pointParticleIndex];
				float3 lhs2 = this.nextPosArray[pointTriangleContact.triangleParticleIndex.x];
				float3 lhs3 = this.nextPosArray[pointTriangleContact.triangleParticleIndex.y];
				float3 lhs4 = this.nextPosArray[pointTriangleContact.triangleParticleIndex.z];
				float3 y = lhs - rhs;
				float3 lhs5 = lhs2 - rhs2;
				float3 lhs6 = lhs3 - rhs3;
				float3 lhs7 = lhs4 - rhs4;
				float3 @float;
				float3 lhs8 = MathUtility.ClosestPtPointTriangle(rhs, rhs2, rhs3, rhs4, out @float);
				float3 y2 = lhs5 * @float.x + lhs6 * @float.y + lhs7 * @float.z;
				float3 float2 = lhs8 - rhs;
				float num = math.length(float2);
				if (num > 1E-08f)
				{
					float3 x = float2 / num;
					float num2 = math.dot(x, y);
					float num3 = math.dot(x, y2);
					float num4 = num - num2 + num3;
					float num5 = pointTriangleContact.thickness;
					if (num4 < pointTriangleContact.thickness + num5)
					{
						flag = true;
					}
				}
				if (flag)
				{
					pointTriangleContact.flagAndTeamId0 |= 2147483648U;
				}
				else
				{
					pointTriangleContact.flagAndTeamId0 &= 2147483647U;
				}
				this.pointTriangleContactList[index] = pointTriangleContact;
			}

			// Token: 0x040001D0 RID: 464
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001D1 RID: 465
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040001D2 RID: 466
			[NativeDisableParallelForRestriction]
			public NativeList<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactList;
		}

		// Token: 0x0200004A RID: 74
		[BurstCompile]
		private struct SolverEdgeEdgeJob : IJobParallelForDefer
		{
			// Token: 0x060000ED RID: 237 RVA: 0x0000B2A4 File Offset: 0x000094A4
			public unsafe void Execute(int index)
			{
				SelfCollisionConstraint.EdgeEdgeContact edgeEdgeContact = this.edgeEdgeContactArray[index];
				if ((edgeEdgeContact.flagAndTeamId0 & 2147483648U) == 0U)
				{
					return;
				}
				float3 x = this.nextPosArray[edgeEdgeContact.edgeParticleIndex0.x];
				float3 y = this.nextPosArray[edgeEdgeContact.edgeParticleIndex0.y];
				float3 x2 = this.nextPosArray[edgeEdgeContact.edgeParticleIndex1.x];
				float3 y2 = this.nextPosArray[edgeEdgeContact.edgeParticleIndex1.y];
				float num = edgeEdgeContact.s;
				float num2 = edgeEdgeContact.t;
				float3 @float = edgeEdgeContact.n;
				float num3 = edgeEdgeContact.thickness;
				float3 lhs = math.lerp(x, y, num);
				float3 rhs = math.lerp(x2, y2, num2);
				float3 y3 = lhs - rhs;
				float num4 = math.dot(@float, y3);
				if (num4 > num3)
				{
					return;
				}
				float num5 = edgeEdgeContact.edgeInvMass0.x;
				float num6 = edgeEdgeContact.edgeInvMass0.y;
				float num7 = edgeEdgeContact.edgeInvMass1.x;
				float num8 = edgeEdgeContact.edgeInvMass1.y;
				float num9 = num3 - num4;
				float num10 = 1f - num;
				float num11 = num;
				float num12 = 1f - num2;
				float num13 = num2;
				float3 rhs2 = @float * num10;
				float3 rhs3 = @float * num11;
				float3 rhs4 = -@float * num12;
				float3 rhs5 = -@float * num13;
				float num14 = num5 * num10 * num10 + num6 * num11 * num11 + num7 * num12 * num12 + num8 * num13 * num13;
				if (num14 == 0f)
				{
					return;
				}
				num14 = num9 / num14;
				float3 add = num14 * num5 * rhs2;
				float3 add2 = num14 * num6 * rhs3;
				float3 add3 = num14 * num7 * rhs4;
				float3 add4 = num14 * num8 * rhs5;
				int* unsafePtr = (int*)this.countArray.GetUnsafePtr<int>();
				int* unsafePtr2 = (int*)this.sumArray.GetUnsafePtr<int>();
				if ((edgeEdgeContact.flagAndTeamId0 & 67108864U) == 0U)
				{
					InterlockUtility.AddFloat3(edgeEdgeContact.edgeParticleIndex0.x, add, unsafePtr, unsafePtr2);
				}
				if ((edgeEdgeContact.flagAndTeamId0 & 134217728U) == 0U)
				{
					InterlockUtility.AddFloat3(edgeEdgeContact.edgeParticleIndex0.y, add2, unsafePtr, unsafePtr2);
				}
				if ((edgeEdgeContact.flagAndTeamId1 & 67108864U) == 0U)
				{
					InterlockUtility.AddFloat3(edgeEdgeContact.edgeParticleIndex1.x, add3, unsafePtr, unsafePtr2);
				}
				if ((edgeEdgeContact.flagAndTeamId1 & 134217728U) == 0U)
				{
					InterlockUtility.AddFloat3(edgeEdgeContact.edgeParticleIndex1.y, add4, unsafePtr, unsafePtr2);
				}
			}

			// Token: 0x040001D3 RID: 467
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001D4 RID: 468
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.EdgeEdgeContact> edgeEdgeContactArray;

			// Token: 0x040001D5 RID: 469
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040001D6 RID: 470
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}

		// Token: 0x0200004B RID: 75
		[BurstCompile]
		private struct SolverPointTriangleJob : IJobParallelForDefer
		{
			// Token: 0x060000EE RID: 238 RVA: 0x0000B548 File Offset: 0x00009748
			public unsafe void Execute(int index)
			{
				SelfCollisionConstraint.PointTriangleContact pointTriangleContact = this.pointTriangleContactArray[index];
				if ((pointTriangleContact.flagAndTeamId0 & 2147483648U) == 0U)
				{
					return;
				}
				float num = pointTriangleContact.thickness;
				int3 triangleParticleIndex = pointTriangleContact.triangleParticleIndex;
				float3 rhs = this.nextPosArray[triangleParticleIndex.x];
				float3 @float = this.nextPosArray[triangleParticleIndex.y];
				float3 float2 = this.nextPosArray[triangleParticleIndex.z];
				float num2 = pointTriangleContact.triangleInvMass.x;
				float num3 = pointTriangleContact.triangleInvMass.y;
				float num4 = pointTriangleContact.triangleInvMass.z;
				float3 lhs = MathUtility.TriangleNormal(rhs, @float, float2);
				int pointParticleIndex = pointTriangleContact.pointParticleIndex;
				float3 lhs2 = this.nextPosArray[pointParticleIndex];
				float num5 = pointTriangleContact.pointInvMass;
				float3 float3;
				MathUtility.ClosestPtPointTriangle(lhs2, rhs, @float, float2, out float3);
				float rhs2 = pointTriangleContact.sign;
				float3 float4 = lhs * rhs2;
				float num6 = math.dot(float4, lhs2 - rhs);
				if (num6 >= num)
				{
					return;
				}
				float num7 = num;
				float num8 = num6 - num7;
				float3 rhs3 = float4;
				float3 rhs4 = -float4 * float3[0];
				float3 rhs5 = -float4 * float3[1];
				float3 rhs6 = -float4 * float3[2];
				float num9 = num5 + num2 * float3.x * float3.x + num3 * float3.y * float3.y + num4 * float3.z * float3.z;
				if (num9 == 0f)
				{
					return;
				}
				num9 = num8 / num9;
				float3 add = -num9 * num5 * rhs3;
				float3 add2 = -num9 * num2 * rhs4;
				float3 add3 = -num9 * num3 * rhs5;
				float3 add4 = -num9 * num4 * rhs6;
				int* unsafePtr = (int*)this.countArray.GetUnsafePtr<int>();
				int* unsafePtr2 = (int*)this.sumArray.GetUnsafePtr<int>();
				if ((pointTriangleContact.flagAndTeamId0 & 67108864U) == 0U)
				{
					InterlockUtility.AddFloat3(pointParticleIndex, add, unsafePtr, unsafePtr2);
				}
				if ((pointTriangleContact.flagAndTeamId1 & 67108864U) == 0U)
				{
					InterlockUtility.AddFloat3(triangleParticleIndex.x, add2, unsafePtr, unsafePtr2);
				}
				if ((pointTriangleContact.flagAndTeamId1 & 134217728U) == 0U)
				{
					InterlockUtility.AddFloat3(triangleParticleIndex.y, add3, unsafePtr, unsafePtr2);
				}
				if ((pointTriangleContact.flagAndTeamId1 & 268435456U) == 0U)
				{
					InterlockUtility.AddFloat3(triangleParticleIndex.z, add4, unsafePtr, unsafePtr2);
				}
			}

			// Token: 0x040001D7 RID: 471
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001D8 RID: 472
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.PointTriangleContact> pointTriangleContactArray;

			// Token: 0x040001D9 RID: 473
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040001DA RID: 474
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}

		// Token: 0x0200004C RID: 76
		[BurstCompile]
		private struct IntersectUpdatePrimitiveJob : IJobParallelForDefer
		{
			// Token: 0x060000EF RID: 239 RVA: 0x0000B7CC File Offset: 0x000099CC
			public unsafe void Execute(int index)
			{
				uint pack = this.processingArray[index];
				int index2 = DataUtility.Unpack32Hi(pack);
				int num = DataUtility.Unpack32Low(pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (this.kind == 1U && !teamData.flag.TestAny(25, 3))
				{
					return;
				}
				if (this.kind == 2U && !teamData.flag.TestAny(28, 3))
				{
					return;
				}
				int index3 = 0;
				switch (this.kind)
				{
				case 0U:
					index3 = teamData.selfPointChunk.startIndex + num;
					break;
				case 1U:
					index3 = teamData.selfEdgeChunk.startIndex + num;
					break;
				case 2U:
					index3 = teamData.selfTriangleChunk.startIndex + num;
					break;
				}
				SelfCollisionConstraint.Primitive value = this.primitiveArray[index3];
				int num2 = (int)(this.kind + 1U);
				for (int i = 0; i < num2; i++)
				{
					int index4 = value.particleIndices[i];
					*value.nextPos[i] = this.nextPosArray[index4];
				}
				this.primitiveArray[index3] = value;
			}

			// Token: 0x040001DB RID: 475
			public uint kind;

			// Token: 0x040001DC RID: 476
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001DD RID: 477
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001DE RID: 478
			[NativeDisableParallelForRestriction]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001DF RID: 479
			[ReadOnly]
			public NativeArray<uint> processingArray;
		}

		// Token: 0x0200004D RID: 77
		[BurstCompile]
		private struct IntersectEdgeTriangleJob : IJobParallelForDefer
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x0000B8EC File Offset: 0x00009AEC
			public void Execute(int index)
			{
				if (index % this.div != this.execNumber)
				{
					return;
				}
				uint pack = this.processingEdgeEdgeArray[index];
				int index2 = DataUtility.Unpack32Hi(pack);
				int num = DataUtility.Unpack32Low(pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (this.mainKind == 1U && !teamData.flag.TestAny(25, 3))
				{
					return;
				}
				if (this.mainKind == 2U && !teamData.flag.TestAny(28, 3))
				{
					return;
				}
				bool flag = this.mainKind == 1U;
				ref DataChunk ptr = flag ? teamData.selfEdgeChunk : teamData.selfTriangleChunk;
				DataChunk dataChunk = flag ? teamData.selfTriangleChunk : teamData.selfEdgeChunk;
				int index3 = ptr.startIndex + num;
				SelfCollisionConstraint.Primitive primitive = this.primitiveArray[index3];
				SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[primitive.sortIndex];
				if (teamData.flag.IsSet(flag ? 25 : 28))
				{
					this.SweepTest(ref primitive, sortData, dataChunk, true);
				}
				if (teamData.flag.IsSet(flag ? 26 : 29))
				{
					TeamManager.TeamData teamData2 = this.teamDataArray[teamData.syncTeamId];
					DataChunk dataChunk2 = flag ? teamData2.selfTriangleChunk : teamData2.selfEdgeChunk;
					this.SweepTest(ref primitive, sortData, dataChunk2, false);
				}
				if (teamData.flag.IsSet(flag ? 27 : 30))
				{
					int length = teamData.syncParentTeamId.Length;
					for (int i = 0; i < length; i++)
					{
						int index4 = teamData.syncParentTeamId[i];
						TeamManager.TeamData teamData3 = this.teamDataArray[index4];
						if (teamData3.flag.IsSet(flag ? 29 : 26))
						{
							DataChunk dataChunk2 = flag ? teamData3.selfTriangleChunk : teamData3.selfEdgeChunk;
							this.SweepTest(ref primitive, sortData, dataChunk2, false);
						}
					}
				}
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x0000BAC0 File Offset: 0x00009CC0
			private void SweepTest(ref SelfCollisionConstraint.Primitive primitive0, in SelfCollisionConstraint.SortData sd0, in DataChunk subChunk, bool connectionCheck)
			{
				int i = SelfCollisionConstraint.BinarySearchSortAndlSweep(ref this.sortAndSweepArray, sd0, subChunk);
				float y = sd0.firstMinMax.y;
				int num = subChunk.startIndex + subChunk.dataLength;
				while (i < num)
				{
					SelfCollisionConstraint.SortData sortData = this.sortAndSweepArray[i];
					i++;
					if (sortData.firstMinMax.x > y)
					{
						break;
					}
					if (sortData.secondMinMax.y >= sd0.secondMinMax.x && sortData.secondMinMax.x <= sd0.secondMinMax.y && sortData.thirdMinMax.y >= sd0.thirdMinMax.x && sortData.thirdMinMax.x <= sd0.thirdMinMax.y)
					{
						SelfCollisionConstraint.Primitive primitive = this.primitiveArray[sortData.primitiveIndex];
						if ((!connectionCheck || !primitive0.AnyParticle(primitive)) && ((primitive0.flagAndTeamId & 536870912U) == 0U || (primitive.flagAndTeamId & 536870912U) == 0U))
						{
							if (this.mainKind == 1U)
							{
								this.IntersectTest(ref primitive0, ref primitive);
							}
							else
							{
								this.IntersectTest(ref primitive, ref primitive0);
							}
						}
					}
				}
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000BBE4 File Offset: 0x00009DE4
			private void IntersectTest(ref SelfCollisionConstraint.Primitive epri, ref SelfCollisionConstraint.Primitive tpri)
			{
				float3 lhs = epri.nextPos.c0;
				float3 c = epri.nextPos.c1;
				float3 @float = lhs - c;
				float3 c2 = tpri.nextPos.c0;
				float3 c3 = tpri.nextPos.c1;
				float3 float2 = tpri.nextPos.c2 - c2;
				float3 x = c3 - c2;
				float3 y = math.cross(x, float2);
				float num = math.dot(@float, y);
				if (math.abs(num) < 1E-08f)
				{
					return;
				}
				if (num < 0f)
				{
					lhs = epri.nextPos.c1;
					@float = -@float;
					num = -num;
				}
				float3 float3 = lhs - c2;
				float num2 = math.dot(float3, y);
				if (num2 < 0f)
				{
					return;
				}
				if (num2 > num)
				{
					return;
				}
				float3 y2 = math.cross(@float, float3);
				float num3 = math.dot(float2, y2);
				if (num3 < 0f || num3 > num)
				{
					return;
				}
				float num4 = -math.dot(x, y2);
				if (num4 < 0f || num3 + num4 > num)
				{
					return;
				}
				this.intersectFlagArray[epri.particleIndices.x] = 1;
				this.intersectFlagArray[epri.particleIndices.y] = 1;
				this.intersectFlagArray[tpri.particleIndices.x] = 1;
				this.intersectFlagArray[tpri.particleIndices.y] = 1;
				this.intersectFlagArray[tpri.particleIndices.z] = 1;
			}

			// Token: 0x040001E0 RID: 480
			public uint mainKind;

			// Token: 0x040001E1 RID: 481
			public int execNumber;

			// Token: 0x040001E2 RID: 482
			public int div;

			// Token: 0x040001E3 RID: 483
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001E4 RID: 484
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.Primitive> primitiveArray;

			// Token: 0x040001E5 RID: 485
			[ReadOnly]
			public NativeArray<SelfCollisionConstraint.SortData> sortAndSweepArray;

			// Token: 0x040001E6 RID: 486
			[ReadOnly]
			public NativeArray<uint> processingEdgeEdgeArray;

			// Token: 0x040001E7 RID: 487
			[NativeDisableParallelForRestriction]
			public NativeArray<byte> intersectFlagArray;
		}
	}
}
