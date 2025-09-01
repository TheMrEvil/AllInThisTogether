using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200002A RID: 42
	public class DistanceConstraint : IDisposable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000734F File Offset: 0x0000554F
		public int DataCount
		{
			get
			{
				ExNativeArray<uint> exNativeArray = this.indexArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007362 File Offset: 0x00005562
		public DistanceConstraint()
		{
			this.indexArray = new ExNativeArray<uint>(0, true);
			this.dataArray = new ExNativeArray<ushort>(0, true);
			this.distanceArray = new ExNativeArray<float>(0, true);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007391 File Offset: 0x00005591
		public void Dispose()
		{
			ExNativeArray<uint> exNativeArray = this.indexArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<ushort> exNativeArray2 = this.dataArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<float> exNativeArray3 = this.distanceArray;
			if (exNativeArray3 == null)
			{
				return;
			}
			exNativeArray3.Dispose();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000073C8 File Offset: 0x000055C8
		internal static DistanceConstraint.ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			DistanceConstraint.ConstraintData constraintData = new DistanceConstraint.ConstraintData();
			NativeParallelMultiHashMap<int, ushort> nativeParallelMultiHashMap = default(NativeParallelMultiHashMap<int, ushort>);
			try
			{
				int vertexCount = proxyMesh.VertexCount;
				nativeParallelMultiHashMap = JobUtility.ToNativeMultiHashMap(proxyMesh.vertexToVertexIndexArray, proxyMesh.vertexToVertexDataArray);
				HashSet<uint> hashSet = new HashSet<uint>();
				using (MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount * 2))
				{
					using (MultiDataBuilder<ushort> multiDataBuilder2 = new MultiDataBuilder<ushort>(vertexCount, vertexCount * 2))
					{
						for (int i = 0; i < vertexCount; i++)
						{
							if (nativeParallelMultiHashMap.ContainsKey(i))
							{
								VertexAttribute vertexAttribute = proxyMesh.attributes[i];
								int num = proxyMesh.vertexParentIndices[i];
								foreach (ushort num2 in nativeParallelMultiHashMap.GetValuesForKey(i))
								{
									int num3 = (int)num2;
									VertexAttribute vertexAttribute2 = proxyMesh.attributes[num3];
									int num4 = proxyMesh.vertexParentIndices[num3];
									if (vertexAttribute.IsMove() || vertexAttribute2.IsMove())
									{
										if (num3 == num || i == num4)
										{
											multiDataBuilder.Add(i, num2);
										}
										else
										{
											multiDataBuilder2.Add(i, num2);
										}
										uint item = DataUtility.Pack32Sort(i, num3);
										hashSet.Add(item);
									}
								}
							}
						}
						if (proxyMesh.edgeToTriangles.IsCreated)
						{
							int edgeCount = proxyMesh.EdgeCount;
							for (int j = 0; j < edgeCount; j++)
							{
								int2 @int = proxyMesh.edges[j];
								FixedList128Bytes<ushort> fixedList128Bytes = ref proxyMesh.edgeToTriangles.ToFixedList128Bytes(@int);
								int length = fixedList128Bytes.Length;
								if (length >= 2)
								{
									float3 lhs = proxyMesh.localPositions[@int.x];
									float3 rhs = proxyMesh.localPositions[@int.y];
									float num5 = math.length(lhs - rhs);
									if (num5 >= 1E-08f)
									{
										math.normalize(lhs - rhs);
										(lhs + rhs) * 0.5f;
										for (int k = 0; k < length - 1; k++)
										{
											int unuseTriangleIndex = MathUtility.GetUnuseTriangleIndex(proxyMesh.triangles[(int)fixedList128Bytes[k]], @int);
											float3 lhs2 = proxyMesh.localPositions[unuseTriangleIndex];
											VertexAttribute vertexAttribute3 = proxyMesh.attributes[unuseTriangleIndex];
											float3 x = MathUtility.TriangleNormal(lhs, rhs, lhs2);
											for (int l = k + 1; l < length; l++)
											{
												int unuseTriangleIndex2 = MathUtility.GetUnuseTriangleIndex(proxyMesh.triangles[(int)fixedList128Bytes[l]], @int);
												float3 rhs2 = proxyMesh.localPositions[unuseTriangleIndex2];
												VertexAttribute vertexAttribute4 = proxyMesh.attributes[unuseTriangleIndex2];
												float3 y = MathUtility.TriangleNormal(lhs, rhs, rhs2);
												if ((vertexAttribute3.IsMove() || vertexAttribute4.IsMove()) && math.abs(math.dot(x, y)) >= 0.9396926f && math.abs(math.length(lhs2 - rhs2) / num5 - 1f) <= 0.3f)
												{
													uint item2 = DataUtility.Pack32Sort(unuseTriangleIndex, unuseTriangleIndex2);
													if (!hashSet.Contains(item2))
													{
														hashSet.Add(item2);
														multiDataBuilder2.Add(unuseTriangleIndex, (ushort)unuseTriangleIndex2);
														multiDataBuilder2.Add(unuseTriangleIndex2, (ushort)unuseTriangleIndex);
													}
												}
											}
										}
									}
								}
							}
						}
						ValueTuple<ushort[], uint[]> valueTuple = multiDataBuilder.ToArray();
						ushort[] item3 = valueTuple.Item1;
						uint[] item4 = valueTuple.Item2;
						ValueTuple<ushort[], uint[]> valueTuple2 = multiDataBuilder2.ToArray();
						ushort[] item5 = valueTuple2.Item1;
						uint[] item6 = valueTuple2.Item2;
						int num6 = ((item3 != null) ? item3.Length : 0) + ((item5 != null) ? item5.Length : 0);
						if (num6 > 0)
						{
							List<uint> list = new List<uint>(vertexCount);
							List<ushort> list2 = new List<ushort>(num6);
							List<float> list3 = new List<float>(num6);
							for (int m = 0; m < vertexCount; m++)
							{
								int count = list2.Count;
								int num7 = 0;
								float3 x2 = proxyMesh.localPositions[m];
								for (int n = 0; n < 2; n++)
								{
									int num8;
									int num9;
									DataUtility.Unpack10_22((n == 0) ? item4[m] : item6[m], out num8, out num9);
									for (int num10 = 0; num10 < num8; num10++)
									{
										ushort num11 = (n == 0) ? item3[num9 + num10] : item5[num9 + num10];
										float3 y2 = proxyMesh.localPositions[(int)num11];
										float num12 = math.distance(x2, y2);
										if (num12 >= 1E-06f)
										{
											list2.Add(num11);
											list3.Add((n == 0) ? num12 : (-num12));
											num7++;
										}
									}
								}
								uint item7 = DataUtility.Pack10_22(num7, count);
								list.Add(item7);
							}
							constraintData.indexArray = list.ToArray();
							constraintData.dataArray = list2.ToArray();
							constraintData.distanceArray = list3.ToArray();
						}
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateDistanceException);
			}
			finally
			{
				if (nativeParallelMultiHashMap.IsCreated)
				{
					nativeParallelMultiHashMap.Dispose();
				}
			}
			return constraintData;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007938 File Offset: 0x00005B38
		internal void Register(ClothProcess cprocess)
		{
			bool? flag;
			if (cprocess == null)
			{
				flag = null;
			}
			else
			{
				DistanceConstraint.ConstraintData distanceConstraintData = cprocess.distanceConstraintData;
				flag = ((distanceConstraintData != null) ? new bool?(distanceConstraintData.IsValid()) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
				teamData.distanceStartChunk = this.indexArray.AddRange(cprocess.distanceConstraintData.indexArray);
				teamData.distanceDataChunk = this.dataArray.AddRange(cprocess.distanceConstraintData.dataArray);
				this.distanceArray.AddRange(cprocess.distanceConstraintData.distanceArray);
				MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000079F0 File Offset: 0x00005BF0
		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
				this.indexArray.Remove(teamData.distanceStartChunk);
				this.dataArray.Remove(teamData.distanceDataChunk);
				this.distanceArray.Remove(teamData.distanceDataChunk);
				teamData.distanceStartChunk.Clear();
				teamData.distanceDataChunk.Clear();
				MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007A78 File Offset: 0x00005C78
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			jobHandle = new DistanceConstraint.DistanceConstraintJob
			{
				stepParticleIndexArray = simulation.processingStepParticle.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				depthArray = vmesh.vertexDepths.GetNativeArray(),
				teamIdArray = simulation.teamIdArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				basePosArray = simulation.basePosArray.GetNativeArray(),
				velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
				frictionArray = simulation.frictionArray.GetNativeArray(),
				indexArray = this.indexArray.GetNativeArray(),
				dataArray = this.dataArray.GetNativeArray(),
				distanceArray = this.distanceArray.GetNativeArray()
			}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			return jobHandle;
		}

		// Token: 0x04000104 RID: 260
		public const int TypeCount = 2;

		// Token: 0x04000105 RID: 261
		public ExNativeArray<uint> indexArray;

		// Token: 0x04000106 RID: 262
		public ExNativeArray<ushort> dataArray;

		// Token: 0x04000107 RID: 263
		public ExNativeArray<float> distanceArray;

		// Token: 0x0200002B RID: 43
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x00007BA0 File Offset: 0x00005DA0
			public SerializeData()
			{
				this.stiffness = new CurveSerializeData(1f, 1f, 0.5f, false);
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00007BC3 File Offset: 0x00005DC3
			public void DataValidate()
			{
				this.stiffness.DataValidate(0f, 1f);
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x00007BDA File Offset: 0x00005DDA
			public DistanceConstraint.SerializeData Clone()
			{
				return new DistanceConstraint.SerializeData
				{
					stiffness = this.stiffness.Clone()
				};
			}

			// Token: 0x04000108 RID: 264
			public CurveSerializeData stiffness;
		}

		// Token: 0x0200002C RID: 44
		public struct DistanceConstraintParams
		{
			// Token: 0x060000A4 RID: 164 RVA: 0x00007BF2 File Offset: 0x00005DF2
			public void Convert(DistanceConstraint.SerializeData sdata)
			{
				this.restorationStiffness = sdata.stiffness.ConvertFloatArray();
				this.velocityAttenuation = 0.3f;
			}

			// Token: 0x04000109 RID: 265
			public float4x4 restorationStiffness;

			// Token: 0x0400010A RID: 266
			public float velocityAttenuation;
		}

		// Token: 0x0200002D RID: 45
		internal class ConstraintData : IValid
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x00007C10 File Offset: 0x00005E10
			public bool IsValid()
			{
				return this.indexArray != null && this.indexArray.Length != 0;
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00002058 File Offset: 0x00000258
			public ConstraintData()
			{
			}

			// Token: 0x0400010B RID: 267
			internal ResultCode result;

			// Token: 0x0400010C RID: 268
			public uint[] indexArray;

			// Token: 0x0400010D RID: 269
			public ushort[] dataArray;

			// Token: 0x0400010E RID: 270
			public float[] distanceArray;
		}

		// Token: 0x0200002E RID: 46
		[BurstCompile]
		private struct DistanceConstraintJob : IJobParallelForDefer
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x00007C28 File Offset: 0x00005E28
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				ClothParameters clothParameters = this.parameterArray[index2];
				float animationPoseRatio = teamData.animationPoseRatio;
				float num2 = teamData.InitScale * teamData.scaleRatio;
				int startIndex = teamData.particleChunk.startIndex;
				int num3 = num - startIndex;
				ref DataChunk distanceStartChunk = teamData.distanceStartChunk;
				DataChunk distanceDataChunk = teamData.distanceDataChunk;
				int startIndex2 = distanceStartChunk.startIndex;
				int startIndex3 = distanceDataChunk.startIndex;
				int startIndex4 = teamData.proxyCommonChunk.startIndex;
				int index3 = startIndex4 + num3;
				float3 @float = this.nextPosArray[num];
				VertexAttribute vertexAttribute = this.attributes[index3];
				float num4 = this.depthArray[index3];
				float friction = this.frictionArray[num];
				if (vertexAttribute.IsDontMove())
				{
					return;
				}
				float num5 = MathUtility.CalcInverseMass(friction, num4, vertexAttribute.IsDontMove());
				float num6 = clothParameters.distanceConstraint.restorationStiffness.EvaluateCurveClamp01(num4);
				int num7;
				int num8;
				DataUtility.Unpack10_22(this.indexArray[startIndex2 + num3], out num7, out num8);
				if (num7 > 0)
				{
					float3 x = this.basePosArray[num];
					float3 float2 = 0;
					int num9 = 0;
					int num10 = startIndex3 + num8;
					for (int i = 0; i < num7; i++)
					{
						int num11 = (int)this.dataArray[num10 + i];
						float num12 = this.distanceArray[num10 + i];
						float lhs = (num12 >= 0f) ? num6 : (num6 * 0.5f);
						int index4 = startIndex + num11;
						int index5 = startIndex4 + num11;
						float3 lhs2 = this.nextPosArray[index4];
						float3 y = this.basePosArray[index4];
						float depth = this.depthArray[index5];
						float num13 = MathUtility.CalcInverseMass(this.frictionArray[index4], depth, this.attributes[index5].IsDontMove());
						float num14 = math.lerp(math.abs(num12) * num2, math.distance(x, y), animationPoseRatio);
						float3 x2 = lhs2 - @float;
						float num15 = math.length(x2);
						if (num15 >= 1E-08f)
						{
							float3 rhs = math.normalize(x2);
							float3 rhs2 = lhs * rhs * (num15 - num14) / (num5 + num13);
							float3 rhs3 = num5 * rhs2;
							float2 += rhs3;
							num9++;
						}
					}
					if (num9 > 0)
					{
						float2 /= (float)num9;
						@float += float2;
						this.nextPosArray[num] = @float;
						float velocityAttenuation = clothParameters.distanceConstraint.velocityAttenuation;
						this.velocityPosArray[num] = this.velocityPosArray[num] + float2 * velocityAttenuation;
					}
				}
			}

			// Token: 0x0400010F RID: 271
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x04000110 RID: 272
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x04000111 RID: 273
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x04000112 RID: 274
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x04000113 RID: 275
			[ReadOnly]
			public NativeArray<float> depthArray;

			// Token: 0x04000114 RID: 276
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x04000115 RID: 277
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x04000116 RID: 278
			[ReadOnly]
			public NativeArray<float3> basePosArray;

			// Token: 0x04000117 RID: 279
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x04000118 RID: 280
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x04000119 RID: 281
			[ReadOnly]
			public NativeArray<uint> indexArray;

			// Token: 0x0400011A RID: 282
			[ReadOnly]
			public NativeArray<ushort> dataArray;

			// Token: 0x0400011B RID: 283
			[ReadOnly]
			public NativeArray<float> distanceArray;
		}
	}
}
