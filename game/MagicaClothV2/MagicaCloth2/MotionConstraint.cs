using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000034 RID: 52
	public class MotionConstraint : IDisposable
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00005305 File Offset: 0x00003505
		public void Dispose()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000835C File Offset: 0x0000655C
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new MotionConstraint.MotionConstraintJob
			{
				stepParticleIndexArray = simulation.processingStepMotionParticle.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				vertexDepths = vmesh.vertexDepths.GetNativeArray(),
				teamIdArray = simulation.teamIdArray.GetNativeArray(),
				basePosArray = simulation.basePosArray.GetNativeArray(),
				baseRotArray = simulation.baseRotArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
				frictionArray = simulation.frictionArray.GetNativeArray(),
				collisionNormalArray = simulation.collisionNormalArray.GetNativeArray()
			}.Schedule(simulation.processingStepMotionParticle.GetJobSchedulePtr(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002058 File Offset: 0x00000258
		public MotionConstraint()
		{
		}

		// Token: 0x02000035 RID: 53
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x060000B5 RID: 181 RVA: 0x00008474 File Offset: 0x00006674
			public SerializeData()
			{
				this.useMaxDistance = false;
				this.maxDistance = new CurveSerializeData(0.3f);
				this.useBackstop = false;
				this.backstopRadius = 10f;
				this.backstopDistance = new CurveSerializeData(0f);
				this.stiffness = 1f;
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x000084CC File Offset: 0x000066CC
			public void DataValidate()
			{
				this.maxDistance.DataValidate(0f, 5f);
				this.backstopRadius = Mathf.Clamp(this.backstopRadius, 0f, 10f);
				this.backstopDistance.DataValidate(0f, 1f);
				this.stiffness = Mathf.Clamp01(this.stiffness);
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00008530 File Offset: 0x00006730
			public MotionConstraint.SerializeData Clone()
			{
				return new MotionConstraint.SerializeData
				{
					useMaxDistance = this.useMaxDistance,
					maxDistance = this.maxDistance.Clone(),
					useBackstop = this.useBackstop,
					backstopRadius = this.backstopRadius,
					backstopDistance = this.backstopDistance.Clone(),
					stiffness = this.stiffness
				};
			}

			// Token: 0x04000147 RID: 327
			public bool useMaxDistance;

			// Token: 0x04000148 RID: 328
			public CurveSerializeData maxDistance;

			// Token: 0x04000149 RID: 329
			public bool useBackstop;

			// Token: 0x0400014A RID: 330
			[Range(0.1f, 10f)]
			public float backstopRadius;

			// Token: 0x0400014B RID: 331
			public CurveSerializeData backstopDistance;

			// Token: 0x0400014C RID: 332
			[Range(0f, 1f)]
			public float stiffness;
		}

		// Token: 0x02000036 RID: 54
		public struct MotionConstraintParams
		{
			// Token: 0x060000B8 RID: 184 RVA: 0x00008594 File Offset: 0x00006794
			public void Convert(MotionConstraint.SerializeData sdata)
			{
				this.useMaxDistance = sdata.useMaxDistance;
				this.maxDistanceCurveData = sdata.maxDistance.ConvertFloatArray();
				this.useBackstop = sdata.useBackstop;
				this.backstopRadius = sdata.backstopRadius;
				this.backstopDistanceCurveData = sdata.backstopDistance.ConvertFloatArray();
				this.stiffness = sdata.stiffness;
			}

			// Token: 0x0400014D RID: 333
			public bool useMaxDistance;

			// Token: 0x0400014E RID: 334
			public float4x4 maxDistanceCurveData;

			// Token: 0x0400014F RID: 335
			public bool useBackstop;

			// Token: 0x04000150 RID: 336
			public float backstopRadius;

			// Token: 0x04000151 RID: 337
			public float4x4 backstopDistanceCurveData;

			// Token: 0x04000152 RID: 338
			public float stiffness;
		}

		// Token: 0x02000037 RID: 55
		[BurstCompile]
		private struct MotionConstraintJob : IJobParallelForDefer
		{
			// Token: 0x060000B9 RID: 185 RVA: 0x000085F4 File Offset: 0x000067F4
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				ClothParameters clothParameters = this.parameterArray[index2];
				MotionConstraint.MotionConstraintParams motionConstraint = clothParameters.motionConstraint;
				ClothNormalAxis normalAxis = clothParameters.normalAxis;
				if (!motionConstraint.useMaxDistance && !motionConstraint.useBackstop)
				{
					return;
				}
				int startIndex = teamData.particleChunk.startIndex;
				int num2 = num - startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				VertexAttribute vertexAttribute = this.attributes[index3];
				if (!vertexAttribute.IsMove())
				{
					return;
				}
				float3 @float = this.nextPosArray[num];
				float3 float2 = this.basePosArray[num];
				float num3 = this.vertexDepths[index3];
				float stiffness = motionConstraint.stiffness;
				if (vertexAttribute.IsMotion())
				{
					float3 float3 = @float;
					float num4 = math.max(clothParameters.radiusCurveData.EvaluateCurve(num3), 0.0001f) * 1f;
					num3 *= num3;
					quaternion q = this.baseRotArray[num];
					float3 float4 = math.up();
					switch (normalAxis)
					{
					case ClothNormalAxis.Right:
						float4 = math.right();
						break;
					case ClothNormalAxis.Up:
						float4 = math.up();
						break;
					case ClothNormalAxis.Forward:
						float4 = math.forward();
						break;
					case ClothNormalAxis.InverseRight:
						float4 = -math.right();
						break;
					case ClothNormalAxis.InverseUp:
						float4 = -math.up();
						break;
					case ClothNormalAxis.InverseForward:
						float4 = -math.forward();
						break;
					}
					float4 = math.mul(q, float4);
					if (motionConstraint.useMaxDistance)
					{
						float maxlength = motionConstraint.maxDistanceCurveData.EvaluateCurve(num3);
						float3 float5 = float2;
						float3 rhs = MathUtility.ClampVector(@float - float5, maxlength);
						@float = float5 + rhs;
					}
					if (motionConstraint.useBackstop)
					{
						float backstopRadius = motionConstraint.backstopRadius;
						float num5 = motionConstraint.backstopDistanceCurveData.EvaluateCurve(num3);
						if (backstopRadius > 0f)
						{
							float3 float6 = float2 + -float4 * (num5 + backstopRadius);
							float3 float7 = @float - float6;
							float num6 = math.length(float7);
							if (num6 > 1E-08f && num6 < backstopRadius + num4)
							{
								float3 lhs = float7 / num6;
								if (num6 < backstopRadius)
								{
									@float = float6 + lhs * backstopRadius;
								}
							}
						}
					}
					@float = math.lerp(float3, @float, stiffness);
					this.nextPosArray[num] = @float;
					float3 lhs2 = @float - float3;
					this.velocityPosArray[num] = this.velocityPosArray[num] + lhs2 * 0.95f;
				}
			}

			// Token: 0x04000153 RID: 339
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x04000154 RID: 340
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000155 RID: 341
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x04000156 RID: 342
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000157 RID: 343
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x04000158 RID: 344
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x04000159 RID: 345
			[ReadOnly]
			public NativeArray<float3> basePosArray;

			// Token: 0x0400015A RID: 346
			[ReadOnly]
			public NativeArray<quaternion> baseRotArray;

			// Token: 0x0400015B RID: 347
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x0400015C RID: 348
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x0400015D RID: 349
			[NativeDisableParallelForRestriction]
			public NativeArray<float> frictionArray;

			// Token: 0x0400015E RID: 350
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> collisionNormalArray;
		}
	}
}
