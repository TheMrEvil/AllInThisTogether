using System;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000083 RID: 131
	public class SimulationManager : IManager, IDisposable, IValid
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000121FE File Offset: 0x000103FE
		public int ParticleCount
		{
			get
			{
				ExNativeArray<float3> exNativeArray = this.nextPosArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00012211 File Offset: 0x00010411
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00012219 File Offset: 0x00010419
		internal int SimulationStepCount
		{
			[CompilerGenerated]
			get
			{
				return this.<SimulationStepCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SimulationStepCount>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00012222 File Offset: 0x00010422
		internal int WorkerCount
		{
			get
			{
				return JobsUtility.JobWorkerCount;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001222C File Offset: 0x0001042C
		public void Dispose()
		{
			this.isValid = false;
			ExNativeArray<short> exNativeArray = this.teamIdArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<float3> exNativeArray2 = this.nextPosArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<float3> exNativeArray3 = this.oldPosArray;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray4 = this.oldRotArray;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			ExNativeArray<float3> exNativeArray5 = this.basePosArray;
			if (exNativeArray5 != null)
			{
				exNativeArray5.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray6 = this.baseRotArray;
			if (exNativeArray6 != null)
			{
				exNativeArray6.Dispose();
			}
			ExNativeArray<float3> exNativeArray7 = this.oldPositionArray;
			if (exNativeArray7 != null)
			{
				exNativeArray7.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray8 = this.oldRotationArray;
			if (exNativeArray8 != null)
			{
				exNativeArray8.Dispose();
			}
			ExNativeArray<float3> exNativeArray9 = this.velocityPosArray;
			if (exNativeArray9 != null)
			{
				exNativeArray9.Dispose();
			}
			ExNativeArray<float3> exNativeArray10 = this.dispPosArray;
			if (exNativeArray10 != null)
			{
				exNativeArray10.Dispose();
			}
			ExNativeArray<float3> exNativeArray11 = this.velocityArray;
			if (exNativeArray11 != null)
			{
				exNativeArray11.Dispose();
			}
			ExNativeArray<float3> exNativeArray12 = this.realVelocityArray;
			if (exNativeArray12 != null)
			{
				exNativeArray12.Dispose();
			}
			ExNativeArray<float> exNativeArray13 = this.frictionArray;
			if (exNativeArray13 != null)
			{
				exNativeArray13.Dispose();
			}
			ExNativeArray<float> exNativeArray14 = this.staticFrictionArray;
			if (exNativeArray14 != null)
			{
				exNativeArray14.Dispose();
			}
			ExNativeArray<float3> exNativeArray15 = this.collisionNormalArray;
			if (exNativeArray15 != null)
			{
				exNativeArray15.Dispose();
			}
			this.teamIdArray = null;
			this.nextPosArray = null;
			this.oldPosArray = null;
			this.oldRotArray = null;
			this.basePosArray = null;
			this.baseRotArray = null;
			this.oldPositionArray = null;
			this.oldRotationArray = null;
			this.velocityPosArray = null;
			this.dispPosArray = null;
			this.velocityArray = null;
			this.realVelocityArray = null;
			this.frictionArray = null;
			this.staticFrictionArray = null;
			this.collisionNormalArray = null;
			ExProcessingList<int> exProcessingList = this.processingStepParticle;
			if (exProcessingList != null)
			{
				exProcessingList.Dispose();
			}
			ExProcessingList<int> exProcessingList2 = this.processingStepTriangleBending;
			if (exProcessingList2 != null)
			{
				exProcessingList2.Dispose();
			}
			ExProcessingList<int> exProcessingList3 = this.processingStepEdgeCollision;
			if (exProcessingList3 != null)
			{
				exProcessingList3.Dispose();
			}
			ExProcessingList<int> exProcessingList4 = this.processingStepCollider;
			if (exProcessingList4 != null)
			{
				exProcessingList4.Dispose();
			}
			ExProcessingList<int> exProcessingList5 = this.processingStepBaseLine;
			if (exProcessingList5 != null)
			{
				exProcessingList5.Dispose();
			}
			ExProcessingList<int> exProcessingList6 = this.processingStepMotionParticle;
			if (exProcessingList6 != null)
			{
				exProcessingList6.Dispose();
			}
			ExProcessingList<int> exProcessingList7 = this.processingSelfParticle;
			if (exProcessingList7 != null)
			{
				exProcessingList7.Dispose();
			}
			ExProcessingList<uint> exProcessingList8 = this.processingSelfPointTriangle;
			if (exProcessingList8 != null)
			{
				exProcessingList8.Dispose();
			}
			ExProcessingList<uint> exProcessingList9 = this.processingSelfEdgeEdge;
			if (exProcessingList9 != null)
			{
				exProcessingList9.Dispose();
			}
			ExProcessingList<uint> exProcessingList10 = this.processingSelfTrianglePoint;
			if (exProcessingList10 != null)
			{
				exProcessingList10.Dispose();
			}
			if (this.tempFloat3Buffer.IsCreated)
			{
				this.tempFloat3Buffer.Dispose();
			}
			if (this.countArray.IsCreated)
			{
				this.countArray.Dispose();
			}
			if (this.sumArray.IsCreated)
			{
				this.sumArray.Dispose();
			}
			if (this.stepBasicPositionBuffer.IsCreated)
			{
				this.stepBasicPositionBuffer.Dispose();
			}
			if (this.stepBasicRotationBuffer.IsCreated)
			{
				this.stepBasicRotationBuffer.Dispose();
			}
			DistanceConstraint distanceConstraint = this.distanceConstraint;
			if (distanceConstraint != null)
			{
				distanceConstraint.Dispose();
			}
			TriangleBendingConstraint triangleBendingConstraint = this.bendingConstraint;
			if (triangleBendingConstraint != null)
			{
				triangleBendingConstraint.Dispose();
			}
			TetherConstraint tetherConstraint = this.tetherConstraint;
			if (tetherConstraint != null)
			{
				tetherConstraint.Dispose();
			}
			AngleConstraint angleConstraint = this.angleConstraint;
			if (angleConstraint != null)
			{
				angleConstraint.Dispose();
			}
			InertiaConstraint inertiaConstraint = this.inertiaConstraint;
			if (inertiaConstraint != null)
			{
				inertiaConstraint.Dispose();
			}
			ColliderCollisionConstraint colliderCollisionConstraint = this.colliderCollisionConstraint;
			if (colliderCollisionConstraint != null)
			{
				colliderCollisionConstraint.Dispose();
			}
			MotionConstraint motionConstraint = this.motionConstraint;
			if (motionConstraint != null)
			{
				motionConstraint.Dispose();
			}
			SelfCollisionConstraint selfCollisionConstraint = this.selfCollisionConstraint;
			if (selfCollisionConstraint != null)
			{
				selfCollisionConstraint.Dispose();
			}
			this.distanceConstraint = null;
			this.bendingConstraint = null;
			this.tetherConstraint = null;
			this.angleConstraint = null;
			this.inertiaConstraint = null;
			this.colliderCollisionConstraint = null;
			this.motionConstraint = null;
			this.selfCollisionConstraint = null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0001258A File Offset: 0x0001078A
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00012594 File Offset: 0x00010794
		public void Initialize()
		{
			this.Dispose();
			this.teamIdArray = new ExNativeArray<short>(0, false);
			this.nextPosArray = new ExNativeArray<float3>(0, false);
			this.oldPosArray = new ExNativeArray<float3>(0, false);
			this.oldRotArray = new ExNativeArray<quaternion>(0, false);
			this.basePosArray = new ExNativeArray<float3>(0, false);
			this.baseRotArray = new ExNativeArray<quaternion>(0, false);
			this.oldPositionArray = new ExNativeArray<float3>(0, false);
			this.oldRotationArray = new ExNativeArray<quaternion>(0, false);
			this.velocityPosArray = new ExNativeArray<float3>(0, false);
			this.dispPosArray = new ExNativeArray<float3>(0, false);
			this.velocityArray = new ExNativeArray<float3>(0, false);
			this.realVelocityArray = new ExNativeArray<float3>(0, false);
			this.frictionArray = new ExNativeArray<float>(0, false);
			this.staticFrictionArray = new ExNativeArray<float>(0, false);
			this.collisionNormalArray = new ExNativeArray<float3>(0, false);
			this.processingStepParticle = new ExProcessingList<int>();
			this.processingStepTriangleBending = new ExProcessingList<int>();
			this.processingStepEdgeCollision = new ExProcessingList<int>();
			this.processingStepCollider = new ExProcessingList<int>();
			this.processingStepBaseLine = new ExProcessingList<int>();
			this.processingStepMotionParticle = new ExProcessingList<int>();
			this.processingSelfParticle = new ExProcessingList<int>();
			this.processingSelfPointTriangle = new ExProcessingList<uint>();
			this.processingSelfEdgeEdge = new ExProcessingList<uint>();
			this.processingSelfTrianglePoint = new ExProcessingList<uint>();
			this.tempFloat3Buffer = new NativeArray<float3>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.distanceConstraint = new DistanceConstraint();
			this.bendingConstraint = new TriangleBendingConstraint();
			this.tetherConstraint = new TetherConstraint();
			this.angleConstraint = new AngleConstraint();
			this.inertiaConstraint = new InertiaConstraint();
			this.colliderCollisionConstraint = new ColliderCollisionConstraint();
			this.motionConstraint = new MotionConstraint();
			this.selfCollisionConstraint = new SelfCollisionConstraint();
			this.SimulationStepCount = 0;
			this.isValid = true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001274C File Offset: 0x0001094C
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00012754 File Offset: 0x00010954
		internal void RegisterProxyMesh(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			VirtualMesh proxyMesh = cprocess.ProxyMesh;
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			int vertexCount = proxyMesh.VertexCount;
			teamData.particleChunk = this.teamIdArray.AddRange(vertexCount, (short)teamId);
			this.nextPosArray.AddRange(vertexCount);
			this.oldPosArray.AddRange(vertexCount);
			this.oldRotArray.AddRange(vertexCount);
			this.basePosArray.AddRange(vertexCount);
			this.baseRotArray.AddRange(vertexCount);
			this.oldPositionArray.AddRange(vertexCount);
			this.oldRotationArray.AddRange(vertexCount);
			this.velocityPosArray.AddRange(vertexCount);
			this.dispPosArray.AddRange(vertexCount);
			this.velocityArray.AddRange(vertexCount);
			this.realVelocityArray.AddRange(vertexCount);
			this.frictionArray.AddRange(vertexCount);
			this.staticFrictionArray.AddRange(vertexCount);
			this.collisionNormalArray.AddRange(vertexCount);
			MagicaManager.Team.SetTeamData(teamId, teamData);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00012860 File Offset: 0x00010A60
		internal void RegisterConstraint(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			MagicaManager.Team.centerDataArray[teamId] = cprocess.inertiaConstraintData.centerData;
			this.distanceConstraint.Register(cprocess);
			this.bendingConstraint.Register(cprocess);
			this.inertiaConstraint.Register(cprocess);
			this.selfCollisionConstraint.Register(cprocess);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000128C8 File Offset: 0x00010AC8
		internal void ExitProxyMesh(ClothProcess cprocess)
		{
			if (!this.isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(teamId);
			teamData.flag.SetBits(10, true);
			DataChunk particleChunk = teamData.particleChunk;
			this.teamIdArray.RemoveAndFill(particleChunk, 0);
			this.nextPosArray.Remove(particleChunk);
			this.oldPosArray.Remove(particleChunk);
			this.oldRotArray.Remove(particleChunk);
			this.basePosArray.Remove(particleChunk);
			this.baseRotArray.Remove(particleChunk);
			this.oldPositionArray.Remove(particleChunk);
			this.oldRotationArray.Remove(particleChunk);
			this.velocityPosArray.Remove(particleChunk);
			this.dispPosArray.Remove(particleChunk);
			this.velocityArray.Remove(particleChunk);
			this.realVelocityArray.Remove(particleChunk);
			this.frictionArray.Remove(particleChunk);
			this.staticFrictionArray.Remove(particleChunk);
			this.collisionNormalArray.Remove(particleChunk);
			teamData.particleChunk.Clear();
			MagicaManager.Team.SetTeamData(teamId, teamData);
			this.distanceConstraint.Exit(cprocess);
			this.bendingConstraint.Exit(cprocess);
			this.inertiaConstraint.Exit(cprocess);
			this.selfCollisionConstraint.Exit(cprocess);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00012A04 File Offset: 0x00010C04
		internal void WorkBufferUpdate()
		{
			int particleCount = this.ParticleCount;
			int baseLineCount = MagicaManager.VMesh.BaseLineCount;
			int dataCount = MagicaManager.Collider.DataCount;
			int dataCount2 = this.bendingConstraint.DataCount;
			this.processingStepParticle.UpdateBuffer(particleCount);
			this.processingStepTriangleBending.UpdateBuffer(dataCount2);
			int edgeColliderCollisionCount = MagicaManager.Team.edgeColliderCollisionCount;
			this.processingStepEdgeCollision.UpdateBuffer(edgeColliderCollisionCount);
			this.processingStepCollider.UpdateBuffer(dataCount);
			this.processingStepBaseLine.UpdateBuffer(baseLineCount);
			this.processingStepMotionParticle.UpdateBuffer(particleCount);
			this.processingSelfParticle.UpdateBuffer(particleCount);
			this.processingSelfPointTriangle.UpdateBuffer(this.selfCollisionConstraint.PointPrimitiveCount);
			this.processingSelfEdgeEdge.UpdateBuffer(this.selfCollisionConstraint.EdgePrimitiveCount);
			this.processingSelfTrianglePoint.UpdateBuffer(this.selfCollisionConstraint.TrianglePrimitiveCount);
			ref this.tempFloat3Buffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ref this.stepBasicPositionBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ref this.stepBasicRotationBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ref this.countArray.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ref this.sumArray.Resize(particleCount * 3, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.angleConstraint.WorkBufferUpdate();
			this.colliderCollisionConstraint.WorkBufferUpdate();
			this.selfCollisionConstraint.WorkBufferUpdate();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00012B48 File Offset: 0x00010D48
		internal JobHandle PreSimulationUpdate(JobHandle jobHandle)
		{
			jobHandle = new SimulationManager.PreSimulationUpdateJob
			{
				teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray(),
				parameterArray = MagicaManager.Team.parameterArray.GetNativeArray(),
				centerDataArray = MagicaManager.Team.centerDataArray.GetNativeArray(),
				positions = MagicaManager.VMesh.positions.GetNativeArray(),
				rotations = MagicaManager.VMesh.rotations.GetNativeArray(),
				vertexDepths = MagicaManager.VMesh.vertexDepths.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				nextPosArray = this.nextPosArray.GetNativeArray(),
				oldPosArray = this.oldPosArray.GetNativeArray(),
				oldRotArray = this.oldRotArray.GetNativeArray(),
				basePosArray = this.basePosArray.GetNativeArray(),
				baseRotArray = this.baseRotArray.GetNativeArray(),
				oldPositionArray = this.oldPositionArray.GetNativeArray(),
				oldRotationArray = this.oldRotationArray.GetNativeArray(),
				velocityPosArray = this.velocityPosArray.GetNativeArray(),
				dispPosArray = this.dispPosArray.GetNativeArray(),
				velocityArray = this.velocityArray.GetNativeArray(),
				realVelocityArray = this.realVelocityArray.GetNativeArray(),
				frictionArray = this.frictionArray.GetNativeArray(),
				staticFrictionArray = this.staticFrictionArray.GetNativeArray(),
				collisionNormalArray = this.collisionNormalArray.GetNativeArray()
			}.Schedule(this.ParticleCount, 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00012D04 File Offset: 0x00010F04
		internal JobHandle SimulationStepUpdate(int updateCount, int updateIndex, JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			int simulationStepCount = this.SimulationStepCount;
			this.SimulationStepCount = simulationStepCount + 1;
			jobHandle = team.SimulationStepTeamUpdate(updateIndex, jobHandle);
			jobHandle = new SimulationManager.ClearStepCounter
			{
				processingStepParticle = this.processingStepParticle.Counter,
				processingStepTriangleBending = this.processingStepTriangleBending.Counter,
				processingStepEdgeCollision = this.processingStepEdgeCollision.Counter,
				processingStepCollider = this.processingStepCollider.Counter,
				processingStepBaseLine = this.processingStepBaseLine.Counter,
				processingStepMotionParticle = this.processingStepMotionParticle.Counter,
				processingSelfParticle = this.processingSelfParticle.Counter,
				processingSelfPointTriangle = this.processingSelfPointTriangle.Counter,
				processingSelfEdgeEdge = this.processingSelfEdgeEdge.Counter,
				processingSelfTrianglePoint = this.processingSelfTrianglePoint.Counter
			}.Schedule(jobHandle);
			jobHandle = new SimulationManager.CreateUpdateParticleList
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				stepParticleIndexCounter = this.processingStepParticle.Counter,
				stepParticleIndexArray = this.processingStepParticle.Buffer,
				stepBaseLineIndexCounter = this.processingStepBaseLine.Counter,
				stepBaseLineIndexArray = this.processingStepBaseLine.Buffer,
				stepTriangleBendIndexCounter = this.processingStepTriangleBending.Counter,
				stepTriangleBendIndexArray = this.processingStepTriangleBending.Buffer,
				stepEdgeCollisionIndexCounter = this.processingStepEdgeCollision.Counter,
				stepEdgeCollisionIndexArray = this.processingStepEdgeCollision.Buffer,
				motionParticleIndexCounter = this.processingStepMotionParticle.Counter,
				motionParticleIndexArray = this.processingStepMotionParticle.Buffer,
				selfParticleCounter = this.processingSelfParticle.Counter,
				selfParticleIndexArray = this.processingSelfParticle.Buffer,
				selfPointTriangleCounter = this.processingSelfPointTriangle.Counter,
				selfPointTriangleIndexArray = this.processingSelfPointTriangle.Buffer,
				selfEdgeEdgeCounter = this.processingSelfEdgeEdge.Counter,
				selfEdgeEdgeIndexArray = this.processingSelfEdgeEdge.Buffer,
				selfTrianglePointCounter = this.processingSelfTrianglePoint.Counter,
				selfTrianglePointIndexArray = this.processingSelfTrianglePoint.Buffer
			}.Schedule(team.TeamCount, 1, jobHandle);
			jobHandle = MagicaManager.Collider.CreateUpdateColliderList(updateIndex, jobHandle);
			jobHandle = MagicaManager.Collider.StartSimulationStep(jobHandle);
			jobHandle = new SimulationManager.StartSimulationStepJob
			{
				stepParticleIndexArray = this.processingStepParticle.Buffer,
				attributes = vmesh.attributes.GetNativeArray(),
				depthArray = vmesh.vertexDepths.GetNativeArray(),
				positions = vmesh.positions.GetNativeArray(),
				rotations = vmesh.rotations.GetNativeArray(),
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				centerDataArray = team.centerDataArray.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				oldPosArray = this.oldPosArray.GetNativeArray(),
				velocityArray = this.velocityArray.GetNativeArray(),
				nextPosArray = this.nextPosArray.GetNativeArray(),
				basePosArray = this.basePosArray.GetNativeArray(),
				baseRotArray = this.baseRotArray.GetNativeArray(),
				oldPositionArray = this.oldPositionArray.GetNativeArray(),
				oldRotationArray = this.oldRotationArray.GetNativeArray(),
				velocityPosArray = this.velocityPosArray.GetNativeArray(),
				stepBasicPositionArray = this.stepBasicPositionBuffer,
				stepBasicRotationArray = this.stepBasicRotationBuffer
			}.Schedule(this.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			jobHandle = new SimulationManager.UpdateStepBasicPotureJob
			{
				stepBaseLineIndexArray = this.processingStepBaseLine.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				attributes = MagicaManager.VMesh.attributes.GetNativeArray(),
				vertexParentIndices = vmesh.vertexParentIndices.GetNativeArray(),
				vertexLocalPositions = vmesh.vertexLocalPositions.GetNativeArray(),
				vertexLocalRotations = vmesh.vertexLocalRotations.GetNativeArray(),
				baseLineStartDataIndices = vmesh.baseLineStartDataIndices.GetNativeArray(),
				baseLineDataCounts = vmesh.baseLineDataCounts.GetNativeArray(),
				baseLineData = vmesh.baseLineData.GetNativeArray(),
				basePosArray = this.basePosArray.GetNativeArray(),
				baseRotArray = this.baseRotArray.GetNativeArray(),
				stepBasicPositionArray = this.stepBasicPositionBuffer,
				stepBasicRotationArray = this.stepBasicRotationBuffer
			}.Schedule(this.processingStepBaseLine.GetJobSchedulePtr(), 2, jobHandle);
			jobHandle = this.tetherConstraint.SolverConstraint(jobHandle);
			jobHandle = this.distanceConstraint.SolverConstraint(jobHandle);
			jobHandle = this.angleConstraint.SolverConstraint(jobHandle);
			jobHandle = this.bendingConstraint.SolverConstraint(jobHandle);
			jobHandle = this.colliderCollisionConstraint.SolverConstraint(jobHandle);
			jobHandle = this.distanceConstraint.SolverConstraint(jobHandle);
			jobHandle = this.motionConstraint.SolverConstraint(jobHandle);
			jobHandle = this.selfCollisionConstraint.SolverConstraint(updateIndex, jobHandle);
			jobHandle = new SimulationManager.EndSimulationStepJob
			{
				stepParticleIndexArray = this.processingStepParticle.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				centerDataArray = team.centerDataArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				vertexDepths = vmesh.vertexDepths.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				nextPosArray = this.nextPosArray.GetNativeArray(),
				oldPosArray = this.oldPosArray.GetNativeArray(),
				velocityArray = this.velocityArray.GetNativeArray(),
				realVelocityArray = this.realVelocityArray.GetNativeArray(),
				velocityPosArray = this.velocityPosArray.GetNativeArray(),
				frictionArray = this.frictionArray.GetNativeArray(),
				staticFrictionArray = this.staticFrictionArray.GetNativeArray(),
				collisionNormalArray = this.collisionNormalArray.GetNativeArray()
			}.Schedule(this.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			jobHandle = MagicaManager.Collider.EndSimulationStep(jobHandle);
			return jobHandle;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000133B4 File Offset: 0x000115B4
		internal JobHandle CalcDisplayPosition(JobHandle jobHandle)
		{
			jobHandle = new SimulationManager.CalcDisplayPositionJob
			{
				teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray(),
				teamIdArray = this.teamIdArray.GetNativeArray(),
				oldPosArray = this.oldPosArray.GetNativeArray(),
				realVelocityArray = this.realVelocityArray.GetNativeArray(),
				oldPositionArray = this.oldPositionArray.GetNativeArray(),
				oldRotationArray = this.oldRotationArray.GetNativeArray(),
				dispPosArray = this.dispPosArray.GetNativeArray(),
				attributes = MagicaManager.VMesh.attributes.GetNativeArray(),
				positions = MagicaManager.VMesh.positions.GetNativeArray(),
				rotations = MagicaManager.VMesh.rotations.GetNativeArray()
			}.Schedule(this.ParticleCount, 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000134A0 File Offset: 0x000116A0
		internal JobHandle FeedbackTempFloat3Buffer(in NativeList<int> particleList, JobHandle jobHandle)
		{
			jobHandle = new SimulationManager.FeedbackTempPosJob
			{
				jobParticleIndexList = particleList,
				tempFloat3Buffer = this.tempFloat3Buffer,
				nextPosArray = this.nextPosArray.GetNativeArray()
			}.Schedule(particleList, 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000134F3 File Offset: 0x000116F3
		internal JobHandle FeedbackTempFloat3Buffer(in ExProcessingList<int> processingList, JobHandle jobHandle)
		{
			return this.FeedbackTempFloat3Buffer(processingList.Buffer, processingList.Counter, jobHandle);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001350C File Offset: 0x0001170C
		internal unsafe JobHandle FeedbackTempFloat3Buffer(in NativeArray<int> particleArray, in NativeReference<int> counter, JobHandle jobHandle)
		{
			jobHandle = new SimulationManager.FeedbackTempPosJob2
			{
				particleIndexArray = particleArray,
				tempFloat3Buffer = this.tempFloat3Buffer,
				nextPosArray = this.nextPosArray.GetNativeArray()
			}.Schedule((int*)counter.GetUnsafePtrWithoutChecks<int>(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00013564 File Offset: 0x00011764
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Simulation Manager. Particle:{0}", this.ParticleCount));
			string format = "  -Distance:{0}";
			DistanceConstraint distanceConstraint = this.distanceConstraint;
			stringBuilder.AppendLine(string.Format(format, (distanceConstraint != null) ? distanceConstraint.DataCount : 0));
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00002058 File Offset: 0x00000258
		public SimulationManager()
		{
		}

		// Token: 0x04000378 RID: 888
		public ExNativeArray<short> teamIdArray;

		// Token: 0x04000379 RID: 889
		public ExNativeArray<float3> nextPosArray;

		// Token: 0x0400037A RID: 890
		public ExNativeArray<float3> oldPosArray;

		// Token: 0x0400037B RID: 891
		public ExNativeArray<quaternion> oldRotArray;

		// Token: 0x0400037C RID: 892
		public ExNativeArray<float3> basePosArray;

		// Token: 0x0400037D RID: 893
		public ExNativeArray<quaternion> baseRotArray;

		// Token: 0x0400037E RID: 894
		public ExNativeArray<float3> oldPositionArray;

		// Token: 0x0400037F RID: 895
		public ExNativeArray<quaternion> oldRotationArray;

		// Token: 0x04000380 RID: 896
		public ExNativeArray<float3> velocityPosArray;

		// Token: 0x04000381 RID: 897
		public ExNativeArray<float3> dispPosArray;

		// Token: 0x04000382 RID: 898
		public ExNativeArray<float3> velocityArray;

		// Token: 0x04000383 RID: 899
		public ExNativeArray<float3> realVelocityArray;

		// Token: 0x04000384 RID: 900
		public ExNativeArray<float> frictionArray;

		// Token: 0x04000385 RID: 901
		public ExNativeArray<float> staticFrictionArray;

		// Token: 0x04000386 RID: 902
		public ExNativeArray<float3> collisionNormalArray;

		// Token: 0x04000387 RID: 903
		public DistanceConstraint distanceConstraint;

		// Token: 0x04000388 RID: 904
		public TriangleBendingConstraint bendingConstraint;

		// Token: 0x04000389 RID: 905
		public TetherConstraint tetherConstraint;

		// Token: 0x0400038A RID: 906
		public AngleConstraint angleConstraint;

		// Token: 0x0400038B RID: 907
		public InertiaConstraint inertiaConstraint;

		// Token: 0x0400038C RID: 908
		public ColliderCollisionConstraint colliderCollisionConstraint;

		// Token: 0x0400038D RID: 909
		public MotionConstraint motionConstraint;

		// Token: 0x0400038E RID: 910
		public SelfCollisionConstraint selfCollisionConstraint;

		// Token: 0x0400038F RID: 911
		internal ExProcessingList<int> processingStepParticle;

		// Token: 0x04000390 RID: 912
		internal ExProcessingList<int> processingStepTriangleBending;

		// Token: 0x04000391 RID: 913
		internal ExProcessingList<int> processingStepEdgeCollision;

		// Token: 0x04000392 RID: 914
		internal ExProcessingList<int> processingStepCollider;

		// Token: 0x04000393 RID: 915
		internal ExProcessingList<int> processingStepBaseLine;

		// Token: 0x04000394 RID: 916
		internal ExProcessingList<int> processingStepMotionParticle;

		// Token: 0x04000395 RID: 917
		internal ExProcessingList<int> processingSelfParticle;

		// Token: 0x04000396 RID: 918
		internal ExProcessingList<uint> processingSelfPointTriangle;

		// Token: 0x04000397 RID: 919
		internal ExProcessingList<uint> processingSelfEdgeEdge;

		// Token: 0x04000398 RID: 920
		internal ExProcessingList<uint> processingSelfTrianglePoint;

		// Token: 0x04000399 RID: 921
		internal NativeArray<float3> tempFloat3Buffer;

		// Token: 0x0400039A RID: 922
		internal NativeArray<int> countArray;

		// Token: 0x0400039B RID: 923
		internal NativeArray<int> sumArray;

		// Token: 0x0400039C RID: 924
		public NativeArray<float3> stepBasicPositionBuffer;

		// Token: 0x0400039D RID: 925
		public NativeArray<quaternion> stepBasicRotationBuffer;

		// Token: 0x0400039E RID: 926
		[CompilerGenerated]
		private int <SimulationStepCount>k__BackingField;

		// Token: 0x0400039F RID: 927
		private bool isValid;

		// Token: 0x02000084 RID: 132
		[BurstCompile]
		private struct PreSimulationUpdateJob : IJobParallelFor
		{
			// Token: 0x060001F9 RID: 505 RVA: 0x000135C8 File Offset: 0x000117C8
			public void Execute(int pindex)
			{
				int num = (int)this.teamIdArray[pindex];
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				if (!teamData.IsProcess)
				{
					return;
				}
				int num2 = pindex - teamData.particleChunk.startIndex;
				int index = teamData.proxyCommonChunk.startIndex + num2;
				if (teamData.IsReset)
				{
					float3 value = this.positions[index];
					quaternion value2 = this.rotations[index];
					this.nextPosArray[pindex] = value;
					this.oldPosArray[pindex] = value;
					this.oldRotArray[pindex] = value2;
					this.basePosArray[pindex] = value;
					this.baseRotArray[pindex] = value2;
					this.oldPositionArray[pindex] = value;
					this.oldRotationArray[pindex] = value2;
					this.velocityPosArray[pindex] = value;
					this.dispPosArray[pindex] = value;
					this.velocityArray[pindex] = 0;
					this.realVelocityArray[pindex] = 0;
					this.frictionArray[pindex] = 0f;
					this.staticFrictionArray[pindex] = 0f;
					this.collisionNormalArray[pindex] = 0;
				}
			}

			// Token: 0x040003A0 RID: 928
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040003A1 RID: 929
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040003A2 RID: 930
			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x040003A3 RID: 931
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x040003A4 RID: 932
			[ReadOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x040003A5 RID: 933
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040003A6 RID: 934
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x040003A7 RID: 935
			[WriteOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040003A8 RID: 936
			public NativeArray<float3> oldPosArray;

			// Token: 0x040003A9 RID: 937
			[WriteOnly]
			public NativeArray<quaternion> oldRotArray;

			// Token: 0x040003AA RID: 938
			[WriteOnly]
			public NativeArray<float3> basePosArray;

			// Token: 0x040003AB RID: 939
			[WriteOnly]
			public NativeArray<quaternion> baseRotArray;

			// Token: 0x040003AC RID: 940
			public NativeArray<float3> oldPositionArray;

			// Token: 0x040003AD RID: 941
			public NativeArray<quaternion> oldRotationArray;

			// Token: 0x040003AE RID: 942
			[WriteOnly]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040003AF RID: 943
			public NativeArray<float3> dispPosArray;

			// Token: 0x040003B0 RID: 944
			public NativeArray<float3> velocityArray;

			// Token: 0x040003B1 RID: 945
			[WriteOnly]
			public NativeArray<float3> realVelocityArray;

			// Token: 0x040003B2 RID: 946
			[WriteOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x040003B3 RID: 947
			[WriteOnly]
			public NativeArray<float> staticFrictionArray;

			// Token: 0x040003B4 RID: 948
			[WriteOnly]
			public NativeArray<float3> collisionNormalArray;
		}

		// Token: 0x02000085 RID: 133
		[BurstCompile]
		private struct ClearStepCounter : IJob
		{
			// Token: 0x060001FA RID: 506 RVA: 0x00013718 File Offset: 0x00011918
			public void Execute()
			{
				this.processingStepParticle.Value = 0;
				this.processingStepTriangleBending.Value = 0;
				this.processingStepEdgeCollision.Value = 0;
				this.processingStepCollider.Value = 0;
				this.processingStepBaseLine.Value = 0;
				this.processingStepMotionParticle.Value = 0;
				this.processingSelfParticle.Value = 0;
				this.processingSelfPointTriangle.Value = 0;
				this.processingSelfEdgeEdge.Value = 0;
				this.processingSelfTrianglePoint.Value = 0;
			}

			// Token: 0x040003B5 RID: 949
			[WriteOnly]
			public NativeReference<int> processingStepParticle;

			// Token: 0x040003B6 RID: 950
			[WriteOnly]
			public NativeReference<int> processingStepTriangleBending;

			// Token: 0x040003B7 RID: 951
			[WriteOnly]
			public NativeReference<int> processingStepEdgeCollision;

			// Token: 0x040003B8 RID: 952
			[WriteOnly]
			public NativeReference<int> processingStepCollider;

			// Token: 0x040003B9 RID: 953
			[WriteOnly]
			public NativeReference<int> processingStepBaseLine;

			// Token: 0x040003BA RID: 954
			[WriteOnly]
			public NativeReference<int> processingStepMotionParticle;

			// Token: 0x040003BB RID: 955
			[WriteOnly]
			public NativeReference<int> processingSelfParticle;

			// Token: 0x040003BC RID: 956
			[WriteOnly]
			public NativeReference<int> processingSelfPointTriangle;

			// Token: 0x040003BD RID: 957
			[WriteOnly]
			public NativeReference<int> processingSelfEdgeEdge;

			// Token: 0x040003BE RID: 958
			[WriteOnly]
			public NativeReference<int> processingSelfTrianglePoint;
		}

		// Token: 0x02000086 RID: 134
		[BurstCompile]
		private struct CreateUpdateParticleList : IJobParallelFor
		{
			// Token: 0x060001FB RID: 507 RVA: 0x000137A0 File Offset: 0x000119A0
			public void Execute(int teamId)
			{
				if (teamId == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[teamId];
				if (!teamData.IsProcess || !teamData.IsRunning)
				{
					return;
				}
				if (!teamData.IsStepRunning)
				{
					return;
				}
				ClothParameters clothParameters = this.parameterArray[teamId];
				int dataLength = teamData.particleChunk.dataLength;
				int startIndex = teamData.particleChunk.startIndex;
				int num = ref this.stepParticleIndexCounter.InterlockedStartIndex(dataLength);
				for (int i = 0; i < dataLength; i++)
				{
					this.stepParticleIndexArray[num + i] = startIndex + i;
				}
				int baseLineCount = teamData.BaseLineCount;
				int startIndex2 = teamData.baseLineChunk.startIndex;
				num = ref this.stepBaseLineIndexCounter.InterlockedStartIndex(baseLineCount);
				for (int j = 0; j < baseLineCount; j++)
				{
					uint value = DataUtility.Pack32(teamId, startIndex2 + j);
					this.stepBaseLineIndexArray[num + j] = (int)value;
				}
				if (clothParameters.triangleBendingConstraint.method != TriangleBendingConstraint.Method.None)
				{
					int dataLength2 = teamData.bendingPairChunk.dataLength;
					int num2 = teamData.bendingPairChunk.startIndex;
					num = ref this.stepTriangleBendIndexCounter.InterlockedStartIndex(dataLength2);
					int k = 0;
					while (k < dataLength2)
					{
						uint value2 = DataUtility.Pack10_22(teamId, num2);
						this.stepTriangleBendIndexArray[num + k] = (int)value2;
						k++;
						num2++;
					}
				}
				int colliderCount = teamData.ColliderCount;
				if (clothParameters.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge && teamData.proxyEdgeChunk.IsValid && colliderCount > 0)
				{
					int dataLength3 = teamData.proxyEdgeChunk.dataLength;
					int startIndex3 = teamData.proxyEdgeChunk.startIndex;
					num = ref this.stepEdgeCollisionIndexCounter.InterlockedStartIndex(dataLength3);
					for (int l = 0; l < dataLength3; l++)
					{
						this.stepEdgeCollisionIndexArray[num + l] = startIndex3 + l;
					}
				}
				if (clothParameters.motionConstraint.useMaxDistance || clothParameters.motionConstraint.useBackstop)
				{
					num = ref this.motionParticleIndexCounter.InterlockedStartIndex(dataLength);
					for (int m = 0; m < dataLength; m++)
					{
						this.motionParticleIndexArray[num + m] = startIndex + m;
					}
				}
				bool flag = teamData.flag.TestAny(16, 3);
				bool flag2 = teamData.flag.TestAny(19, 3);
				bool flag3 = teamData.flag.TestAny(22, 3);
				if (flag)
				{
					int edgeCount = teamData.EdgeCount;
					num = ref this.selfEdgeEdgeCounter.InterlockedStartIndex(edgeCount);
					for (int n = 0; n < edgeCount; n++)
					{
						uint value3 = DataUtility.Pack32(teamId, n);
						this.selfEdgeEdgeIndexArray[num + n] = value3;
					}
				}
				if (flag2)
				{
					num = ref this.selfPointTriangleCounter.InterlockedStartIndex(dataLength);
					for (int num3 = 0; num3 < dataLength; num3++)
					{
						uint value4 = DataUtility.Pack32(teamId, num3);
						this.selfPointTriangleIndexArray[num + num3] = value4;
					}
				}
				if (flag3)
				{
					int triangleCount = teamData.TriangleCount;
					num = ref this.selfTrianglePointCounter.InterlockedStartIndex(triangleCount);
					for (int num4 = 0; num4 < triangleCount; num4++)
					{
						uint value5 = DataUtility.Pack32(teamId, num4);
						this.selfTrianglePointIndexArray[num + num4] = value5;
					}
				}
				if (flag || flag2 || flag3)
				{
					num = ref this.selfParticleCounter.InterlockedStartIndex(dataLength);
					for (int num5 = 0; num5 < dataLength; num5++)
					{
						this.selfParticleIndexArray[num + num5] = startIndex + num5;
					}
				}
			}

			// Token: 0x040003BF RID: 959
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040003C0 RID: 960
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040003C1 RID: 961
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> stepParticleIndexCounter;

			// Token: 0x040003C2 RID: 962
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x040003C3 RID: 963
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> stepBaseLineIndexCounter;

			// Token: 0x040003C4 RID: 964
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> stepBaseLineIndexArray;

			// Token: 0x040003C5 RID: 965
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> stepTriangleBendIndexCounter;

			// Token: 0x040003C6 RID: 966
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> stepTriangleBendIndexArray;

			// Token: 0x040003C7 RID: 967
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> stepEdgeCollisionIndexCounter;

			// Token: 0x040003C8 RID: 968
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> stepEdgeCollisionIndexArray;

			// Token: 0x040003C9 RID: 969
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> motionParticleIndexCounter;

			// Token: 0x040003CA RID: 970
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> motionParticleIndexArray;

			// Token: 0x040003CB RID: 971
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> selfParticleCounter;

			// Token: 0x040003CC RID: 972
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> selfParticleIndexArray;

			// Token: 0x040003CD RID: 973
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> selfPointTriangleCounter;

			// Token: 0x040003CE RID: 974
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<uint> selfPointTriangleIndexArray;

			// Token: 0x040003CF RID: 975
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> selfEdgeEdgeCounter;

			// Token: 0x040003D0 RID: 976
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<uint> selfEdgeEdgeIndexArray;

			// Token: 0x040003D1 RID: 977
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeReference<int> selfTrianglePointCounter;

			// Token: 0x040003D2 RID: 978
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<uint> selfTrianglePointIndexArray;
		}

		// Token: 0x02000087 RID: 135
		[BurstCompile]
		private struct StartSimulationStepJob : IJobParallelForDefer
		{
			// Token: 0x060001FC RID: 508 RVA: 0x00013AF4 File Offset: 0x00011CF4
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				int num2 = num - teamData.particleChunk.startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				ClothParameters clothParameters = this.parameterArray[index2];
				VertexAttribute vertexAttribute = this.attributes[index3];
				float num3 = this.depthArray[index3];
				float3 @float = this.oldPosArray[num];
				float3 float2 = @float;
				float3 float3 = @float;
				float3 x = this.oldPositionArray[num];
				quaternion q = this.oldRotationArray[num];
				float3 y = this.positions[index3];
				quaternion q2 = this.rotations[index3];
				float3 float4 = math.lerp(x, y, teamData.frameInterpolation);
				quaternion quaternion = math.slerp(q, q2, teamData.frameInterpolation);
				quaternion = math.normalize(quaternion);
				this.basePosArray[num] = float4;
				this.baseRotArray[num] = quaternion;
				this.stepBasicPositionArray[num] = float4;
				this.stepBasicRotationArray[num] = quaternion;
				if (vertexAttribute.IsMove())
				{
					float3 float5 = this.velocityArray[num];
					InertiaConstraint.CenterData centerData = this.centerDataArray[index2];
					float3 float6 = centerData.inertiaVector;
					quaternion inertiaRotation = centerData.inertiaRotation;
					float num4 = clothParameters.inertiaConstraint.depthInertia * (1f - num3 * num3);
					float6 = math.lerp(float6, centerData.stepVector, num4);
					quaternion q3 = math.slerp(inertiaRotation, centerData.stepRotation, num4);
					float3 float7 = @float - centerData.oldWorldPosition;
					float7 = math.mul(q3, float7);
					float7 += float6;
					float3 float8 = centerData.oldWorldPosition + float7;
					float3 rhs = float8 - float2;
					float2 = float8;
					float3 += rhs;
					float5 = math.mul(q3, float5);
					float5 *= teamData.velocityWeight;
					float num5 = clothParameters.dampingCurveData.EvaluateCurveClamp01(num3);
					float5 *= 1f - num5;
					float3 lhs = 0;
					float3 rhs2 = clothParameters.gravityDirection * (clothParameters.gravity * teamData.gravityRatio);
					lhs += rhs2;
					lhs *= teamData.scaleRatio;
					float5 += lhs * teamData.SimulationDeltaTime;
					float2 += float5 * teamData.SimulationDeltaTime;
				}
				else
				{
					float2 = float4;
					float3 = float4;
				}
				this.velocityPosArray[num] = float3;
				this.nextPosArray[num] = float2;
			}

			// Token: 0x040003D3 RID: 979
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x040003D4 RID: 980
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040003D5 RID: 981
			[ReadOnly]
			public NativeArray<float> depthArray;

			// Token: 0x040003D6 RID: 982
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x040003D7 RID: 983
			[ReadOnly]
			public NativeArray<quaternion> rotations;

			// Token: 0x040003D8 RID: 984
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040003D9 RID: 985
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040003DA RID: 986
			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x040003DB RID: 987
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x040003DC RID: 988
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040003DD RID: 989
			[ReadOnly]
			public NativeArray<float3> velocityArray;

			// Token: 0x040003DE RID: 990
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040003DF RID: 991
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> basePosArray;

			// Token: 0x040003E0 RID: 992
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> baseRotArray;

			// Token: 0x040003E1 RID: 993
			[ReadOnly]
			public NativeArray<float3> oldPositionArray;

			// Token: 0x040003E2 RID: 994
			[ReadOnly]
			public NativeArray<quaternion> oldRotationArray;

			// Token: 0x040003E3 RID: 995
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040003E4 RID: 996
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> stepBasicPositionArray;

			// Token: 0x040003E5 RID: 997
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> stepBasicRotationArray;
		}

		// Token: 0x02000088 RID: 136
		[BurstCompile]
		private struct UpdateStepBasicPotureJob : IJobParallelForDefer
		{
			// Token: 0x060001FD RID: 509 RVA: 0x00013DA8 File Offset: 0x00011FA8
			public void Execute(int index)
			{
				int pack = this.stepBaseLineIndexArray[index];
				int index2 = DataUtility.Unpack32Hi((uint)pack);
				int index3 = DataUtility.Unpack32Low((uint)pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				float animationPoseRatio = teamData.animationPoseRatio;
				if (animationPoseRatio > 0.99f)
				{
					return;
				}
				int startIndex = teamData.baseLineDataChunk.startIndex;
				int startIndex2 = teamData.particleChunk.startIndex;
				int startIndex3 = teamData.proxyCommonChunk.startIndex;
				float3 rhs = teamData.initScale * teamData.scaleRatio;
				int num = (int)this.baseLineStartDataIndices[index3];
				int num2 = (int)this.baseLineDataCounts[index3];
				int num3 = num + startIndex;
				int i = 0;
				while (i < num2)
				{
					int num4 = (int)this.baseLineData[num3];
					int index4 = startIndex2 + num4;
					int index5 = startIndex3 + num4;
					int num5 = this.vertexParentIndices[index5];
					int index6 = num5 + startIndex2;
					if (this.attributes[index5].IsMove() && num5 >= 0)
					{
						float3 lhs = this.vertexLocalPositions[index5];
						quaternion b = this.vertexLocalRotations[index5];
						float3 rhs2 = this.stepBasicPositionArray[index6];
						quaternion quaternion = this.stepBasicRotationArray[index6];
						this.stepBasicPositionArray[index4] = math.mul(quaternion, lhs * rhs) + rhs2;
						this.stepBasicRotationArray[index4] = math.mul(quaternion, b);
					}
					i++;
					num3++;
				}
				if (animationPoseRatio > 1E-08f)
				{
					num3 = num + startIndex;
					int j = 0;
					while (j < num2)
					{
						int num6 = (int)this.baseLineData[num3];
						int index7 = startIndex2 + num6;
						float3 y = this.basePosArray[index7];
						quaternion q = this.baseRotArray[index7];
						this.stepBasicPositionArray[index7] = math.lerp(this.stepBasicPositionArray[index7], y, animationPoseRatio);
						this.stepBasicRotationArray[index7] = math.slerp(this.stepBasicRotationArray[index7], q, animationPoseRatio);
						j++;
						num3++;
					}
				}
			}

			// Token: 0x040003E6 RID: 998
			[ReadOnly]
			public NativeArray<int> stepBaseLineIndexArray;

			// Token: 0x040003E7 RID: 999
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040003E8 RID: 1000
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040003E9 RID: 1001
			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			// Token: 0x040003EA RID: 1002
			[ReadOnly]
			public NativeArray<float3> vertexLocalPositions;

			// Token: 0x040003EB RID: 1003
			[ReadOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			// Token: 0x040003EC RID: 1004
			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			// Token: 0x040003ED RID: 1005
			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			// Token: 0x040003EE RID: 1006
			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			// Token: 0x040003EF RID: 1007
			[ReadOnly]
			public NativeArray<float3> basePosArray;

			// Token: 0x040003F0 RID: 1008
			[ReadOnly]
			public NativeArray<quaternion> baseRotArray;

			// Token: 0x040003F1 RID: 1009
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> stepBasicPositionArray;

			// Token: 0x040003F2 RID: 1010
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> stepBasicRotationArray;
		}

		// Token: 0x02000089 RID: 137
		[BurstCompile]
		private struct EndSimulationStepJob : IJobParallelForDefer
		{
			// Token: 0x060001FE RID: 510 RVA: 0x00013FD4 File Offset: 0x000121D4
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				InertiaConstraint.CenterData centerData = this.centerDataArray[index2];
				ClothParameters clothParameters = this.parameterArray[index2];
				int startIndex = teamData.particleChunk.startIndex;
				int num2 = num - startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				VertexAttribute vertexAttribute = this.attributes[index3];
				float num3 = this.vertexDepths[index3];
				float3 @float = this.nextPosArray[num];
				float3 rhs = this.oldPosArray[num];
				if (vertexAttribute.IsMove())
				{
					float3 float2 = this.velocityPosArray[num];
					float num4 = this.frictionArray[num];
					float3 x = this.collisionNormalArray[num];
					bool flag = math.lengthsq(x) > 1E-08f;
					float num5 = clothParameters.colliderCollisionConstraint.staticFriction * teamData.scaleRatio;
					float dynamicFriction = clothParameters.colliderCollisionConstraint.dynamicFriction;
					float num6 = this.staticFrictionArray[num];
					if (flag && num4 > 0f && num5 > 0f)
					{
						float3 lhs = @float - rhs;
						float3 float3 = lhs - MathUtility.Project(lhs, x);
						float num7 = math.length(float3) / teamData.SimulationDeltaTime;
						if (num7 < num5)
						{
							num6 = math.saturate(num6 + 0.04f);
						}
						else
						{
							float num8 = math.max((num7 - num5) / 0.2f, 0.05f);
							num6 = math.saturate(num6 - num8);
						}
						float3 *= num6;
						@float -= float3;
						float2 -= float3;
					}
					else
					{
						num6 = math.saturate(num6 - 0.05f);
					}
					this.staticFrictionArray[num] = num6;
					float3 float4 = (@float - float2) / teamData.SimulationDeltaTime;
					float num9 = math.lengthsq(float4);
					float3 float5 = (num9 > 1E-08f) ? math.normalize(float4) : 0;
					if (num4 > 1E-08f && flag && dynamicFriction > 0f && num9 >= 1E-08f)
					{
						float num10 = math.dot(x, float5);
						num10 = 0.5f + 0.5f * num10;
						num10 *= num10;
						num10 = 1f - num10;
						float4 -= float4 * (num10 * math.saturate(num4 * dynamicFriction));
					}
					num4 *= 0.6f;
					this.frictionArray[num] = num4;
					if (clothParameters.inertiaConstraint.particleSpeedLimit >= 0f)
					{
						float4 = MathUtility.ClampVector(float4, clothParameters.inertiaConstraint.particleSpeedLimit * teamData.scaleRatio);
					}
					if (centerData.angularVelocity > 1E-08f && clothParameters.inertiaConstraint.centrifualAcceleration > 1E-08f && num9 >= 1E-08f)
					{
						float3 float6 = @float - centerData.nowWorldPosition;
						float3 float7 = MathUtility.ProjectOnPlane(float6, centerData.rotationAxis);
						float num11 = math.length(float7);
						if (num11 > 1E-08f)
						{
							float3 float8 = float7 / num11;
							float angularVelocity = centerData.angularVelocity;
							float num12 = (1f + (1f - num3)) * angularVelocity * angularVelocity * num11;
							float3 y = math.normalize(math.cross(centerData.rotationAxis, float8));
							num12 *= math.saturate(math.dot(float5, y));
							float4 += float8 * (num12 * clothParameters.inertiaConstraint.centrifualAcceleration * 0.02f);
						}
					}
					float4 *= teamData.velocityWeight;
					this.velocityArray[num] = float4;
				}
				float3 value = (@float - rhs) / teamData.SimulationDeltaTime;
				this.realVelocityArray[num] = value;
				this.oldPosArray[num] = @float;
			}

			// Token: 0x040003F3 RID: 1011
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x040003F4 RID: 1012
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040003F5 RID: 1013
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040003F6 RID: 1014
			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x040003F7 RID: 1015
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040003F8 RID: 1016
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040003F9 RID: 1017
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x040003FA RID: 1018
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040003FB RID: 1019
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> oldPosArray;

			// Token: 0x040003FC RID: 1020
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> velocityArray;

			// Token: 0x040003FD RID: 1021
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> realVelocityArray;

			// Token: 0x040003FE RID: 1022
			[ReadOnly]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040003FF RID: 1023
			[NativeDisableParallelForRestriction]
			public NativeArray<float> frictionArray;

			// Token: 0x04000400 RID: 1024
			[NativeDisableParallelForRestriction]
			public NativeArray<float> staticFrictionArray;

			// Token: 0x04000401 RID: 1025
			[ReadOnly]
			public NativeArray<float3> collisionNormalArray;
		}

		// Token: 0x0200008A RID: 138
		[BurstCompile]
		private struct CalcDisplayPositionJob : IJobParallelFor
		{
			// Token: 0x060001FF RID: 511 RVA: 0x000143D4 File Offset: 0x000125D4
			public void Execute(int pindex)
			{
				int num = (int)this.teamIdArray[pindex];
				if (num == 0)
				{
					return;
				}
				TeamManager.TeamData teamData = this.teamDataArray[num];
				if (!teamData.IsProcess)
				{
					return;
				}
				int num2 = pindex - teamData.particleChunk.startIndex;
				int index = teamData.proxyCommonChunk.startIndex + num2;
				VertexAttribute vertexAttribute = this.attributes[index];
				float3 value = this.positions[index];
				quaternion value2 = this.rotations[index];
				if (vertexAttribute.IsMove())
				{
					float3 lhs = this.oldPosArray[pindex];
					float3 rhs = this.realVelocityArray[pindex] * teamData.SimulationDeltaTime;
					float3 @float = lhs + rhs;
					float num3 = teamData.nowUpdateTime + teamData.SimulationDeltaTime - teamData.oldTime;
					float s = (num3 > 0f) ? ((teamData.time - teamData.oldTime) / num3) : 0f;
					@float = math.lerp(this.dispPosArray[pindex], @float, s);
					float3 float2 = @float;
					this.dispPosArray[pindex] = float2;
					float3 value3 = math.lerp(this.positions[index], float2, teamData.blendWeight);
					this.positions[index] = value3;
				}
				else
				{
					float3 value4 = this.positions[index];
					this.dispPosArray[pindex] = value4;
				}
				if (teamData.IsRunning)
				{
					this.oldPositionArray[pindex] = value;
					this.oldRotationArray[pindex] = value2;
				}
			}

			// Token: 0x04000402 RID: 1026
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000403 RID: 1027
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x04000404 RID: 1028
			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			// Token: 0x04000405 RID: 1029
			[ReadOnly]
			public NativeArray<float3> realVelocityArray;

			// Token: 0x04000406 RID: 1030
			[WriteOnly]
			public NativeArray<float3> oldPositionArray;

			// Token: 0x04000407 RID: 1031
			[WriteOnly]
			public NativeArray<quaternion> oldRotationArray;

			// Token: 0x04000408 RID: 1032
			public NativeArray<float3> dispPosArray;

			// Token: 0x04000409 RID: 1033
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400040A RID: 1034
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> positions;

			// Token: 0x0400040B RID: 1035
			[ReadOnly]
			public NativeArray<quaternion> rotations;
		}

		// Token: 0x0200008B RID: 139
		[BurstCompile]
		private struct FeedbackTempPosJob : IJobParallelForDefer
		{
			// Token: 0x06000200 RID: 512 RVA: 0x00014558 File Offset: 0x00012758
			public void Execute(int index)
			{
				int index2 = this.jobParticleIndexList[index];
				this.nextPosArray[index2] = this.tempFloat3Buffer[index2];
			}

			// Token: 0x0400040C RID: 1036
			[ReadOnly]
			public NativeList<int> jobParticleIndexList;

			// Token: 0x0400040D RID: 1037
			[ReadOnly]
			public NativeArray<float3> tempFloat3Buffer;

			// Token: 0x0400040E RID: 1038
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;
		}

		// Token: 0x0200008C RID: 140
		[BurstCompile]
		private struct FeedbackTempPosJob2 : IJobParallelForDefer
		{
			// Token: 0x06000201 RID: 513 RVA: 0x0001458C File Offset: 0x0001278C
			public void Execute(int index)
			{
				int index2 = this.particleIndexArray[index];
				this.nextPosArray[index2] = this.tempFloat3Buffer[index2];
			}

			// Token: 0x0400040F RID: 1039
			[ReadOnly]
			public NativeArray<int> particleIndexArray;

			// Token: 0x04000410 RID: 1040
			[ReadOnly]
			public NativeArray<float3> tempFloat3Buffer;

			// Token: 0x04000411 RID: 1041
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;
		}
	}
}
