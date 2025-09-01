using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200004E RID: 78
	public class TetherConstraint : IDisposable
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00005305 File Offset: 0x00003505
		public void Dispose()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000BD64 File Offset: 0x00009F64
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new TetherConstraint.TethreConstraintJob
			{
				stepParticleIndexArray = simulation.processingStepParticle.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				centerDataArray = team.centerDataArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				vertexDepths = vmesh.vertexDepths.GetNativeArray(),
				vertexRootIndices = vmesh.vertexRootIndices.GetNativeArray(),
				teamIdArray = simulation.teamIdArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
				frictionArray = simulation.frictionArray.GetNativeArray(),
				stepBasicPositionBuffer = simulation.stepBasicPositionBuffer
			}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002058 File Offset: 0x00000258
		public TetherConstraint()
		{
		}

		// Token: 0x0200004F RID: 79
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x0000BE75 File Offset: 0x0000A075
			public SerializeData()
			{
				this.distanceCompression = 0.9f;
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x0000BE88 File Offset: 0x0000A088
			public void DataValidate()
			{
				this.distanceCompression = Mathf.Clamp(this.distanceCompression, 0f, 1f);
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x0000BEA5 File Offset: 0x0000A0A5
			public TetherConstraint.SerializeData Clone()
			{
				return new TetherConstraint.SerializeData
				{
					distanceCompression = this.distanceCompression
				};
			}

			// Token: 0x040001E8 RID: 488
			[Range(0f, 1f)]
			public float distanceCompression;
		}

		// Token: 0x02000050 RID: 80
		public struct TetherConstraintParams
		{
			// Token: 0x060000F9 RID: 249 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
			public void Convert(TetherConstraint.SerializeData sdata)
			{
				this.compressionLimit = sdata.distanceCompression;
				this.stretchLimit = 0.03f;
			}

			// Token: 0x040001E9 RID: 489
			public float compressionLimit;

			// Token: 0x040001EA RID: 490
			public float stretchLimit;
		}

		// Token: 0x02000051 RID: 81
		[BurstCompile]
		private struct TethreConstraintJob : IJobParallelForDefer
		{
			// Token: 0x060000FA RID: 250 RVA: 0x0000BED4 File Offset: 0x0000A0D4
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				TetherConstraint.TetherConstraintParams tetherConstraint = this.parameterArray[index2].tetherConstraint;
				int startIndex = teamData.particleChunk.startIndex;
				int num2 = num - startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				if (!this.attributes[index3].IsMove())
				{
					return;
				}
				int num3 = this.vertexRootIndices[index3];
				if (num3 < 0)
				{
					return;
				}
				float3 @float = this.nextPosArray[num];
				float3 lhs = this.nextPosArray[num3 + startIndex];
				float num4 = this.vertexDepths[index3];
				float num5 = this.frictionArray[num];
				float3 float2 = lhs - @float;
				float num6 = math.length(float2);
				if (num6 < 1E-08f)
				{
					return;
				}
				float3 x = this.stepBasicPositionBuffer[num];
				float3 y = this.stepBasicPositionBuffer[num3 + startIndex];
				float num7 = math.distance(x, y);
				float num8 = num6 / num7;
				float num9 = 1f - tetherConstraint.compressionLimit;
				float num10 = 1f + tetherConstraint.stretchLimit;
				float num11;
				float num13;
				float rhs;
				if (num8 < num9)
				{
					num11 = num6 - num9 * num7;
					float num12 = math.saturate((num9 - num8) / 0.3f);
					num13 = 1f * num12;
					rhs = 0.7f;
				}
				else
				{
					if (num8 <= num10)
					{
						return;
					}
					num11 = num6 - num10 * num7;
					float num14 = math.saturate((num8 - num10) / 0.3f);
					num13 = 1f * num14;
					rhs = 0.7f;
				}
				float3 float3 = float2 / num6 * (num11 * num13);
				@float += float3;
				this.nextPosArray[num] = @float;
				this.velocityPosArray[num] = this.velocityPosArray[num] + float3 * rhs;
			}

			// Token: 0x040001EB RID: 491
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x040001EC RID: 492
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040001ED RID: 493
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040001EE RID: 494
			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			// Token: 0x040001EF RID: 495
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040001F0 RID: 496
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040001F1 RID: 497
			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			// Token: 0x040001F2 RID: 498
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x040001F3 RID: 499
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040001F4 RID: 500
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040001F5 RID: 501
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x040001F6 RID: 502
			[ReadOnly]
			public NativeArray<float3> stepBasicPositionBuffer;
		}
	}
}
