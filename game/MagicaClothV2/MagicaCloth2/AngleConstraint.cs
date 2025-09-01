using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200001E RID: 30
	public class AngleConstraint : IDisposable
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002058 File Offset: 0x00000258
		public AngleConstraint()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005646 File Offset: 0x00003846
		public void Dispose()
		{
			ref this.lengthBuffer.DisposeSafe<float>();
			ref this.localPosBuffer.DisposeSafe<float3>();
			ref this.localRotBuffer.DisposeSafe<quaternion>();
			ref this.rotationBuffer.DisposeSafe<quaternion>();
			ref this.restorationVectorBuffer.DisposeSafe<float3>();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005680 File Offset: 0x00003880
		internal void WorkBufferUpdate()
		{
			int particleCount = MagicaManager.Simulation.ParticleCount;
			ref this.lengthBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ref this.localPosBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ref this.localRotBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ref this.rotationBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			ref this.restorationVectorBuffer.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000056E0 File Offset: 0x000038E0
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new AngleConstraint.AngleConstraintJob
			{
				stepBaseLineIndexArray = simulation.processingStepBaseLine.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				vertexDepths = vmesh.vertexDepths.GetNativeArray(),
				vertexParentIndices = vmesh.vertexParentIndices.GetNativeArray(),
				baseLineStartDataIndices = vmesh.baseLineStartDataIndices.GetNativeArray(),
				baseLineDataCounts = vmesh.baseLineDataCounts.GetNativeArray(),
				baseLineData = vmesh.baseLineData.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
				frictionArray = simulation.frictionArray.GetNativeArray(),
				stepBasicPositionBuffer = simulation.stepBasicPositionBuffer,
				stepBasicRotationBuffer = simulation.stepBasicRotationBuffer,
				lengthBufferArray = this.lengthBuffer,
				localPosBufferArray = this.localPosBuffer,
				localRotBufferArray = this.localRotBuffer,
				rotationBufferArray = this.rotationBuffer,
				restorationVectorBufferArray = this.restorationVectorBuffer
			}.Schedule(simulation.processingStepBaseLine.GetJobSchedulePtr(), 2, jobHandle);
			return jobHandle;
		}

		// Token: 0x040000AC RID: 172
		private NativeArray<float> lengthBuffer;

		// Token: 0x040000AD RID: 173
		private NativeArray<float3> localPosBuffer;

		// Token: 0x040000AE RID: 174
		private NativeArray<quaternion> localRotBuffer;

		// Token: 0x040000AF RID: 175
		private NativeArray<quaternion> rotationBuffer;

		// Token: 0x040000B0 RID: 176
		private NativeArray<float3> restorationVectorBuffer;

		// Token: 0x0200001F RID: 31
		[Serializable]
		public class RestorationSerializeData : IDataValidate
		{
			// Token: 0x06000081 RID: 129 RVA: 0x00005850 File Offset: 0x00003A50
			public RestorationSerializeData()
			{
				this.useAngleRestoration = true;
				this.stiffness = new CurveSerializeData(0.2f, 1f, 0.2f, true);
				this.velocityAttenuation = 0.8f;
				this.gravityFalloff = 0f;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00005890 File Offset: 0x00003A90
			public void DataValidate()
			{
				this.stiffness.DataValidate(0f, 1f);
				this.velocityAttenuation = Mathf.Clamp01(this.velocityAttenuation);
				this.gravityFalloff = Mathf.Clamp01(this.gravityFalloff);
			}

			// Token: 0x06000083 RID: 131 RVA: 0x000058C9 File Offset: 0x00003AC9
			public AngleConstraint.RestorationSerializeData Clone()
			{
				return new AngleConstraint.RestorationSerializeData
				{
					useAngleRestoration = this.useAngleRestoration,
					stiffness = this.stiffness.Clone(),
					velocityAttenuation = this.velocityAttenuation,
					gravityFalloff = this.gravityFalloff
				};
			}

			// Token: 0x040000B1 RID: 177
			public bool useAngleRestoration;

			// Token: 0x040000B2 RID: 178
			public CurveSerializeData stiffness;

			// Token: 0x040000B3 RID: 179
			[Range(0f, 1f)]
			public float velocityAttenuation;

			// Token: 0x040000B4 RID: 180
			[Range(0f, 1f)]
			public float gravityFalloff;
		}

		// Token: 0x02000020 RID: 32
		[Serializable]
		public class LimitSerializeData : IDataValidate
		{
			// Token: 0x06000084 RID: 132 RVA: 0x00005905 File Offset: 0x00003B05
			public LimitSerializeData()
			{
				this.useAngleLimit = false;
				this.limitAngle = new CurveSerializeData(60f, 0f, 1f, true);
				this.stiffness = 1f;
			}

			// Token: 0x06000085 RID: 133 RVA: 0x0000593A File Offset: 0x00003B3A
			public void DataValidate()
			{
				this.limitAngle.DataValidate(0f, 180f);
				this.stiffness = Mathf.Clamp01(this.stiffness);
			}

			// Token: 0x06000086 RID: 134 RVA: 0x00005962 File Offset: 0x00003B62
			public AngleConstraint.LimitSerializeData Clone()
			{
				return new AngleConstraint.LimitSerializeData
				{
					useAngleLimit = this.useAngleLimit,
					limitAngle = this.limitAngle.Clone(),
					stiffness = this.stiffness
				};
			}

			// Token: 0x040000B5 RID: 181
			public bool useAngleLimit;

			// Token: 0x040000B6 RID: 182
			public CurveSerializeData limitAngle;

			// Token: 0x040000B7 RID: 183
			[Range(0f, 1f)]
			public float stiffness;
		}

		// Token: 0x02000021 RID: 33
		public struct AngleConstraintParams
		{
			// Token: 0x06000087 RID: 135 RVA: 0x00005994 File Offset: 0x00003B94
			public void Convert(AngleConstraint.RestorationSerializeData restorationData, AngleConstraint.LimitSerializeData limitData)
			{
				this.useAngleRestoration = restorationData.useAngleRestoration;
				this.restorationStiffness = restorationData.stiffness.ConvertFloatArray() * 0.2f;
				this.restorationVelocityAttenuation = restorationData.velocityAttenuation;
				this.restorationGravityFalloff = restorationData.gravityFalloff;
				this.useAngleLimit = limitData.useAngleLimit;
				this.limitCurveData = limitData.limitAngle.ConvertFloatArray();
				this.limitstiffness = limitData.stiffness;
			}

			// Token: 0x040000B8 RID: 184
			public bool useAngleRestoration;

			// Token: 0x040000B9 RID: 185
			public float4x4 restorationStiffness;

			// Token: 0x040000BA RID: 186
			public float restorationVelocityAttenuation;

			// Token: 0x040000BB RID: 187
			public float restorationGravityFalloff;

			// Token: 0x040000BC RID: 188
			public bool useAngleLimit;

			// Token: 0x040000BD RID: 189
			public float4x4 limitCurveData;

			// Token: 0x040000BE RID: 190
			public float limitstiffness;
		}

		// Token: 0x02000022 RID: 34
		[BurstCompile]
		private struct AngleConstraintJob : IJobParallelForDefer
		{
			// Token: 0x06000088 RID: 136 RVA: 0x00005A0C File Offset: 0x00003C0C
			public void Execute(int index)
			{
				int pack = this.stepBaseLineIndexArray[index];
				int index2 = DataUtility.Unpack32Hi((uint)pack);
				int index3 = DataUtility.Unpack32Low((uint)pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				AngleConstraint.AngleConstraintParams angleConstraint = this.parameterArray[index2].angleConstraint;
				if (!angleConstraint.useAngleLimit && !angleConstraint.useAngleRestoration)
				{
					return;
				}
				int startIndex = teamData.baseLineDataChunk.startIndex;
				int startIndex2 = teamData.particleChunk.startIndex;
				int startIndex3 = teamData.proxyCommonChunk.startIndex;
				int num = (int)this.baseLineStartDataIndices[index3];
				int num2 = (int)this.baseLineDataCounts[index3];
				bool useAngleLimit = angleConstraint.useAngleLimit;
				bool useAngleRestoration = angleConstraint.useAngleRestoration;
				float limitstiffness = angleConstraint.limitstiffness;
				float restorationVelocityAttenuation = angleConstraint.restorationVelocityAttenuation;
				float num3 = math.lerp(1f - angleConstraint.restorationGravityFalloff, 1f, teamData.gravityDot);
				int num4 = num + startIndex;
				int i = 0;
				while (i < num2)
				{
					int num5 = (int)this.baseLineData[num4];
					int index4 = startIndex2 + num5;
					int index5 = startIndex3 + num5;
					float3 x = this.nextPosArray[index4];
					float3 lhs = this.stepBasicPositionBuffer[index4];
					quaternion quaternion = this.stepBasicRotationBuffer[index4];
					this.rotationBufferArray[index4] = quaternion;
					if (i > 0)
					{
						int index6 = this.vertexParentIndices[index5] + startIndex2;
						float3 y = this.nextPosArray[index6];
						float3 rhs = this.stepBasicPositionBuffer[index6];
						quaternion q = this.stepBasicRotationBuffer[index6];
						if (useAngleLimit)
						{
							float value = math.distance(x, y);
							float3 v = math.normalize(lhs - rhs);
							quaternion quaternion2 = math.inverse(q);
							float3 value2 = math.mul(quaternion2, v);
							quaternion value3 = math.mul(quaternion2, quaternion);
							this.lengthBufferArray[index4] = value;
							this.localPosBufferArray[index4] = value2;
							this.localRotBufferArray[index4] = value3;
						}
						if (useAngleRestoration)
						{
							float3 value4 = lhs - rhs;
							this.restorationVectorBufferArray[index4] = value4;
						}
					}
					i++;
					num4++;
				}
				for (int j = 0; j < 3; j++)
				{
					float s = (float)j / 2f;
					float num6 = 0.4f;
					float num7 = math.lerp(0.1f, 0.5f, s);
					num4 = num + startIndex;
					int k = 0;
					while (k < num2)
					{
						int num8 = (int)this.baseLineData[num4];
						int index7 = startIndex2 + num8;
						int index8 = startIndex3 + num8;
						float3 @float = this.nextPosArray[index7];
						float time = this.vertexDepths[index8];
						VertexAttribute vertexAttribute = this.attributes[index8];
						float rhs2 = MathUtility.CalcInverseMass(this.frictionArray[index7]);
						if (vertexAttribute.IsMove())
						{
							int index9 = this.vertexParentIndices[index8] + startIndex2;
							int index10 = this.vertexParentIndices[index8] + startIndex3;
							float3 float2 = this.nextPosArray[index9];
							VertexAttribute vertexAttribute2 = this.attributes[index10];
							float rhs3 = MathUtility.CalcInverseMass(this.frictionArray[index9]);
							if (useAngleLimit)
							{
								quaternion quaternion3 = this.rotationBufferArray[index9];
								float3 v2 = this.localPosBufferArray[index7];
								quaternion b = this.localRotBufferArray[index7];
								float3 float3 = @float - float2;
								float3 float4 = math.mul(quaternion3, v2);
								float num9 = math.length(float3);
								float y2 = this.lengthBufferArray[index7];
								num9 = math.lerp(num9, y2, 0.5f);
								float3 = math.normalize(float3) * num9;
								float num10 = math.radians(angleConstraint.limitCurveData.EvaluateCurve(time));
								float num11 = MathUtility.Angle(float3, float4);
								float3 lhs2 = float3;
								if (num11 > num10)
								{
									float maxAngle = math.lerp(num11, num10, limitstiffness);
									MathUtility.ClampAngle(float3, float4, maxAngle, out lhs2);
								}
								float3 lhs3 = float2 + float3 * num6;
								float3 lhs4 = lhs3 - lhs2 * num6;
								float3 lhs5 = lhs3 + lhs2 * (1f - num6);
								float3 float5 = lhs4 - float2;
								float3 float6 = lhs5 - @float;
								float6 *= rhs2;
								float5 *= rhs3;
								if (vertexAttribute.IsMove())
								{
									@float += float6;
									this.nextPosArray[index7] = @float;
									this.velocityPosArray[index7] = this.velocityPosArray[index7] + float6 * 0.9f;
								}
								if (vertexAttribute2.IsMove())
								{
									float2 += float5;
									this.nextPosArray[index9] = float2;
									this.velocityPosArray[index9] = this.velocityPosArray[index9] + float5 * 0.9f;
								}
								float3 = @float - float2;
								quaternion quaternion4 = math.mul(quaternion3, b);
								quaternion4 = math.mul(MathUtility.FromToRotation(float4, float3, 1f), quaternion4);
								this.rotationBufferArray[index7] = quaternion4;
							}
							if (useAngleRestoration)
							{
								float3 float7 = @float - float2;
								float3 float8 = this.restorationVectorBufferArray[index7];
								float num12 = angleConstraint.restorationStiffness.EvaluateCurveClamp01(time);
								num12 *= num3;
								float3 lhs6 = math.mul(MathUtility.FromToRotation(float7, float8, num12), float7);
								float3 lhs7 = float2 + float7 * num7;
								float3 lhs8 = lhs7 - lhs6 * num7;
								float3 lhs9 = lhs7 + lhs6 * (1f - num7);
								float3 float9 = lhs8 - float2;
								float3 float10 = lhs9 - @float;
								float9 *= rhs2;
								float10 *= rhs3;
								if (vertexAttribute.IsMove())
								{
									@float += float10;
									this.nextPosArray[index7] = @float;
									this.velocityPosArray[index7] = this.velocityPosArray[index7] + float10 * restorationVelocityAttenuation;
								}
								if (vertexAttribute2.IsMove())
								{
									float2 += float9;
									this.nextPosArray[index9] = float2;
									this.velocityPosArray[index9] = this.velocityPosArray[index9] + float9 * restorationVelocityAttenuation;
								}
							}
						}
						k++;
						num4++;
					}
				}
			}

			// Token: 0x040000BF RID: 191
			[ReadOnly]
			public NativeArray<int> stepBaseLineIndexArray;

			// Token: 0x040000C0 RID: 192
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040000C1 RID: 193
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040000C2 RID: 194
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040000C3 RID: 195
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040000C4 RID: 196
			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			// Token: 0x040000C5 RID: 197
			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			// Token: 0x040000C6 RID: 198
			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			// Token: 0x040000C7 RID: 199
			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			// Token: 0x040000C8 RID: 200
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040000C9 RID: 201
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040000CA RID: 202
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x040000CB RID: 203
			[ReadOnly]
			public NativeArray<float3> stepBasicPositionBuffer;

			// Token: 0x040000CC RID: 204
			[ReadOnly]
			public NativeArray<quaternion> stepBasicRotationBuffer;

			// Token: 0x040000CD RID: 205
			[NativeDisableParallelForRestriction]
			public NativeArray<float> lengthBufferArray;

			// Token: 0x040000CE RID: 206
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> localPosBufferArray;

			// Token: 0x040000CF RID: 207
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> localRotBufferArray;

			// Token: 0x040000D0 RID: 208
			[NativeDisableParallelForRestriction]
			public NativeArray<quaternion> rotationBufferArray;

			// Token: 0x040000D1 RID: 209
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> restorationVectorBufferArray;
		}
	}
}
