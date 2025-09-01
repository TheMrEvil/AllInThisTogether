using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000052 RID: 82
	public class TriangleBendingConstraint : IDisposable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		public int DataCount
		{
			get
			{
				ExNativeArray<ulong> exNativeArray = this.trianglePairArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public TriangleBendingConstraint()
		{
			this.trianglePairArray = new ExNativeArray<ulong>(0, true);
			this.restAngleOrVolumeArray = new ExNativeArray<float>(0, true);
			this.signOrVolumeArray = new ExNativeArray<sbyte>(0, true);
			this.writeDataArray = new ExNativeArray<uint>(0, true);
			this.writeIndexArray = new ExNativeArray<uint>(0, true);
			this.writeBuffer = new ExNativeArray<float3>(0, true);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000C144 File Offset: 0x0000A344
		public void Dispose()
		{
			ExNativeArray<ulong> exNativeArray = this.trianglePairArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<float> exNativeArray2 = this.restAngleOrVolumeArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<sbyte> exNativeArray3 = this.signOrVolumeArray;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<uint> exNativeArray4 = this.writeDataArray;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			ExNativeArray<uint> exNativeArray5 = this.writeIndexArray;
			if (exNativeArray5 != null)
			{
				exNativeArray5.Dispose();
			}
			ExNativeArray<float3> exNativeArray6 = this.writeBuffer;
			if (exNativeArray6 != null)
			{
				exNativeArray6.Dispose();
			}
			this.trianglePairArray = null;
			this.restAngleOrVolumeArray = null;
			this.signOrVolumeArray = null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		internal static TriangleBendingConstraint.ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			TriangleBendingConstraint.ConstraintData constraintData = new TriangleBendingConstraint.ConstraintData();
			try
			{
				if (proxyMesh.TriangleCount == 0)
				{
					return null;
				}
				int edgeCount = proxyMesh.EdgeCount;
				if (edgeCount == 0)
				{
					return null;
				}
				List<ulong> list = new List<ulong>(edgeCount * 2);
				List<float> list2 = new List<float>(edgeCount * 2);
				List<sbyte> list3 = new List<sbyte>(edgeCount * 2);
				List<uint> list4 = new List<uint>(edgeCount * 2);
				HashSet<int4> hashSet = new HashSet<int4>();
				int num = 0;
				int num2 = 0;
				int vertexCount = proxyMesh.VertexCount;
				using (MultiDataBuilder<byte> multiDataBuilder = new MultiDataBuilder<byte>(vertexCount, vertexCount * 2))
				{
					for (int i = 0; i < edgeCount; i++)
					{
						int2 @int = proxyMesh.edges[i];
						if (proxyMesh.edgeToTriangles.ContainsKey(@int))
						{
							FixedList128Bytes<ushort> fixedList128Bytes = ref proxyMesh.edgeToTriangles.ToFixedList128Bytes(@int);
							int length = fixedList128Bytes.Length;
							for (int j = 0; j < length - 1; j++)
							{
								int index = (int)fixedList128Bytes[j];
								int3 tri = proxyMesh.triangles[index];
								for (int k = j + 1; k < length; k++)
								{
									int index2 = (int)fixedList128Bytes[k];
									int3 tri2 = proxyMesh.triangles[index2];
									int2 restTriangleVertex = MathUtility.GetRestTriangleVertex(tri, tri2, @int);
									int4 int2 = new int4(restTriangleVertex.x, restTriangleVertex.y, @int.x, @int.y);
									VertexAttribute vertexAttribute = proxyMesh.attributes[int2.x];
									VertexAttribute vertexAttribute2 = proxyMesh.attributes[int2.y];
									VertexAttribute vertexAttribute3 = proxyMesh.attributes[int2.z];
									VertexAttribute vertexAttribute4 = proxyMesh.attributes[int2.w];
									if ((!vertexAttribute.IsDontMove() || !vertexAttribute2.IsDontMove() || !vertexAttribute3.IsDontMove() || !vertexAttribute4.IsDontMove()) && !vertexAttribute.IsInvalid() && !vertexAttribute2.IsInvalid() && !vertexAttribute3.IsInvalid() && !vertexAttribute4.IsInvalid())
									{
										ulong item = DataUtility.Pack64(int2);
										float num3;
										sbyte item2;
										TriangleBendingConstraint.InitDihedralAngle(proxyMesh, int2.x, int2.y, int2.z, int2.w, out num3, out item2);
										float num4 = math.abs(math.degrees(num3));
										if (num4 < 120f)
										{
											list.Add(item);
											list2.Add(num3);
											list3.Add(item2);
											uint item3 = DataUtility.Pack32(multiDataBuilder.CountValuesForKey(int2.x), multiDataBuilder.CountValuesForKey(int2.y), multiDataBuilder.CountValuesForKey(int2.z), multiDataBuilder.CountValuesForKey(int2.w));
											list4.Add(item3);
											multiDataBuilder.Add(int2.x, 0);
											multiDataBuilder.Add(int2.y, 0);
											multiDataBuilder.Add(int2.z, 0);
											multiDataBuilder.Add(int2.w, 0);
											num++;
										}
										if (num4 >= 90f && num4 <= 179f)
										{
											int4 item4 = DataUtility.PackInt4(int2);
											if (!hashSet.Contains(item4))
											{
												TriangleBendingConstraint.InitVolume(proxyMesh, int2.x, int2.y, int2.z, int2.w, out num3, out item2);
												list.Add(item);
												list2.Add(num3);
												list3.Add(item2);
												hashSet.Add(item4);
												uint item5 = DataUtility.Pack32(multiDataBuilder.CountValuesForKey(int2.x), multiDataBuilder.CountValuesForKey(int2.y), multiDataBuilder.CountValuesForKey(int2.z), multiDataBuilder.CountValuesForKey(int2.w));
												list4.Add(item5);
												multiDataBuilder.Add(int2.x, 0);
												multiDataBuilder.Add(int2.y, 0);
												multiDataBuilder.Add(int2.z, 0);
												multiDataBuilder.Add(int2.w, 0);
												num2++;
											}
										}
									}
								}
							}
						}
					}
					constraintData.trianglePairArray = ((list.Count > 0) ? list.ToArray() : null);
					constraintData.restAngleOrVolumeArray = ((list2.Count > 0) ? list2.ToArray() : null);
					constraintData.signOrVolumeArray = ((list3.Count > 0) ? list3.ToArray() : null);
					constraintData.writeDataArray = ((list4.Count > 0) ? list4.ToArray() : null);
					constraintData.writeBufferCount = multiDataBuilder.Count();
					constraintData.writeIndexArray = multiDataBuilder.ToIndexArray();
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateTriangleBendingException);
			}
			return constraintData;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000C69C File Offset: 0x0000A89C
		private static void InitVolume(VirtualMesh proxyMesh, int v0, int v1, int v2, int v3, out float volumeRest, out sbyte signFlag)
		{
			float3 rhs = proxyMesh.localPositions[v0];
			float3 lhs = proxyMesh.localPositions[v1];
			float3 lhs2 = proxyMesh.localPositions[v2];
			float3 lhs3 = proxyMesh.localPositions[v3];
			volumeRest = 0.16666667f * math.dot(math.cross(lhs - rhs, lhs2 - rhs), lhs3 - rhs);
			signFlag = 100;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000C70C File Offset: 0x0000A90C
		private static void InitDihedralAngle(VirtualMesh proxyMesh, int v0, int v1, int v2, int v3, out float restAngle, out sbyte signFlag)
		{
			float3 rhs = proxyMesh.localPositions[v0];
			float3 rhs2 = proxyMesh.localPositions[v1];
			float3 @float = proxyMesh.localPositions[v2];
			float3 lhs = proxyMesh.localPositions[v3];
			float3 x = math.cross(@float - rhs, lhs - rhs);
			float3 float2 = math.cross(lhs - rhs2, @float - rhs2);
			float3 x2 = math.normalize(x);
			float2 = math.normalize(float2);
			float num = math.dot(x2, float2);
			num = MathUtility.Clamp1(num);
			restAngle = math.acos(num);
			float3 y = lhs - @float;
			float num2 = math.sign(math.dot(math.cross(x2, float2), y));
			signFlag = ((num2 < 0f) ? -1 : 1);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		internal void Register(ClothProcess cprocess)
		{
			bool? flag;
			if (cprocess == null)
			{
				flag = null;
			}
			else
			{
				TriangleBendingConstraint.ConstraintData bendingConstraintData = cprocess.bendingConstraintData;
				flag = ((bendingConstraintData != null) ? new bool?(bendingConstraintData.IsValid()) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
				TriangleBendingConstraint.ConstraintData bendingConstraintData2 = cprocess.bendingConstraintData;
				teamData.bendingPairChunk = this.trianglePairArray.AddRange(bendingConstraintData2.trianglePairArray);
				this.restAngleOrVolumeArray.AddRange(bendingConstraintData2.restAngleOrVolumeArray);
				this.signOrVolumeArray.AddRange(bendingConstraintData2.signOrVolumeArray);
				this.writeDataArray.AddRange(bendingConstraintData2.writeDataArray);
				teamData.bendingWriteIndexChunk = this.writeIndexArray.AddRange(bendingConstraintData2.writeIndexArray);
				teamData.bendingBufferChunk = this.writeBuffer.AddRange(bendingConstraintData2.writeBufferCount);
				MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
				this.trianglePairArray.Remove(teamData.bendingPairChunk);
				this.restAngleOrVolumeArray.Remove(teamData.bendingPairChunk);
				this.signOrVolumeArray.Remove(teamData.bendingPairChunk);
				this.writeDataArray.Remove(teamData.bendingPairChunk);
				this.writeIndexArray.Remove(teamData.bendingWriteIndexChunk);
				this.writeBuffer.Remove(teamData.bendingBufferChunk);
				teamData.bendingPairChunk.Clear();
				teamData.bendingWriteIndexChunk.Clear();
				teamData.bendingBufferChunk.Clear();
				MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000C98C File Offset: 0x0000AB8C
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			if (this.DataCount > 0)
			{
				jobHandle = new TriangleBendingConstraint.TriangleBendingJob
				{
					stepTriangleBendIndexArray = simulation.processingStepTriangleBending.Buffer,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					depthArray = vmesh.vertexDepths.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					trianglePairArray = this.trianglePairArray.GetNativeArray(),
					restAngleOrVolumeArray = this.restAngleOrVolumeArray.GetNativeArray(),
					signOrVolumeArray = this.signOrVolumeArray.GetNativeArray(),
					writeDataArray = this.writeDataArray.GetNativeArray(),
					writeIndexArray = this.writeIndexArray.GetNativeArray(),
					writeBuffer = this.writeBuffer.GetNativeArray()
				}.Schedule(simulation.processingStepTriangleBending.GetJobSchedulePtr(), 16, jobHandle);
				jobHandle = new TriangleBendingConstraint.SolveAggregateBufferJob
				{
					stepParticleIndexArray = simulation.processingStepParticle.Buffer,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					teamIdArray = simulation.teamIdArray.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					writeIndexArray = this.writeIndexArray.GetNativeArray(),
					writeBuffer = this.writeBuffer.GetNativeArray()
				}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 16, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x040001F7 RID: 503
		public ExNativeArray<ulong> trianglePairArray;

		// Token: 0x040001F8 RID: 504
		public ExNativeArray<float> restAngleOrVolumeArray;

		// Token: 0x040001F9 RID: 505
		public ExNativeArray<sbyte> signOrVolumeArray;

		// Token: 0x040001FA RID: 506
		public ExNativeArray<uint> writeDataArray;

		// Token: 0x040001FB RID: 507
		public ExNativeArray<uint> writeIndexArray;

		// Token: 0x040001FC RID: 508
		public ExNativeArray<float3> writeBuffer;

		// Token: 0x02000053 RID: 83
		public enum Method
		{
			// Token: 0x040001FE RID: 510
			None,
			// Token: 0x040001FF RID: 511
			DihedralAngle,
			// Token: 0x04000200 RID: 512
			DirectionDihedralAngle
		}

		// Token: 0x02000054 RID: 84
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x06000104 RID: 260 RVA: 0x0000CB5D File Offset: 0x0000AD5D
			public SerializeData()
			{
				this.stiffness = 1f;
			}

			// Token: 0x06000105 RID: 261 RVA: 0x0000CB70 File Offset: 0x0000AD70
			public void DataValidate()
			{
				this.stiffness = Mathf.Clamp01(this.stiffness);
			}

			// Token: 0x06000106 RID: 262 RVA: 0x0000CB83 File Offset: 0x0000AD83
			public TriangleBendingConstraint.SerializeData Clone()
			{
				return new TriangleBendingConstraint.SerializeData
				{
					stiffness = this.stiffness
				};
			}

			// Token: 0x04000201 RID: 513
			[Range(0f, 1f)]
			public float stiffness;
		}

		// Token: 0x02000055 RID: 85
		public struct TriangleBendingConstraintParams
		{
			// Token: 0x06000107 RID: 263 RVA: 0x0000CB96 File Offset: 0x0000AD96
			public void Convert(TriangleBendingConstraint.SerializeData sdata)
			{
				this.method = ((sdata.stiffness > 1E-08f) ? TriangleBendingConstraint.Method.DirectionDihedralAngle : TriangleBendingConstraint.Method.None);
				this.stiffness = sdata.stiffness;
			}

			// Token: 0x04000202 RID: 514
			public TriangleBendingConstraint.Method method;

			// Token: 0x04000203 RID: 515
			public float stiffness;
		}

		// Token: 0x02000056 RID: 86
		internal class ConstraintData : IValid
		{
			// Token: 0x06000108 RID: 264 RVA: 0x0000CBBB File Offset: 0x0000ADBB
			public bool IsValid()
			{
				return this.trianglePairArray != null && this.trianglePairArray.Length != 0;
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00002058 File Offset: 0x00000258
			public ConstraintData()
			{
			}

			// Token: 0x04000204 RID: 516
			public ResultCode result;

			// Token: 0x04000205 RID: 517
			public ulong[] trianglePairArray;

			// Token: 0x04000206 RID: 518
			public float[] restAngleOrVolumeArray;

			// Token: 0x04000207 RID: 519
			public sbyte[] signOrVolumeArray;

			// Token: 0x04000208 RID: 520
			public int writeBufferCount;

			// Token: 0x04000209 RID: 521
			public uint[] writeDataArray;

			// Token: 0x0400020A RID: 522
			public uint[] writeIndexArray;
		}

		// Token: 0x02000057 RID: 87
		[BurstCompile]
		private struct TriangleBendingJob : IJobParallelForDefer
		{
			// Token: 0x0600010A RID: 266 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
			public unsafe void Execute(int index)
			{
				int pack = this.stepTriangleBendIndexArray[index];
				int num = DataUtility.Unpack10_22Low((uint)pack);
				int index2 = DataUtility.Unpack10_22Hi((uint)pack);
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				TriangleBendingConstraint.TriangleBendingConstraintParams triangleBendingConstraint = this.parameterArray[index2].triangleBendingConstraint;
				if (triangleBendingConstraint.method == TriangleBendingConstraint.Method.None)
				{
					return;
				}
				float stiffness = triangleBendingConstraint.stiffness;
				if (stiffness < 1E-06f)
				{
					return;
				}
				int startIndex = teamData.particleChunk.startIndex;
				int startIndex2 = teamData.proxyCommonChunk.startIndex;
				ulong num2 = this.trianglePairArray[num];
				int4 @int = DataUtility.Unpack64(num2);
				float3x4 float3x = 0;
				float3x4 float3x2 = 0;
				float4 @float = 1;
				for (int i = 0; i < 4; i++)
				{
					int index3 = startIndex + @int[i];
					int index4 = startIndex2 + @int[i];
					*float3x[i] = this.nextPosArray[index3];
					float friction = this.frictionArray[index3];
					float depth = this.depthArray[index4];
					bool flag = this.attributes[index4].IsDontMove();
					@float[i] = (flag ? 0.01f : MathUtility.CalcInverseMass(friction, depth));
				}
				int num3 = num - teamData.bendingPairChunk.startIndex;
				int index5 = teamData.bendingPairChunk.startIndex + num3;
				float num4 = this.restAngleOrVolumeArray[index5];
				sbyte b = this.signOrVolumeArray[index5];
				bool flag2 = false;
				if (b == 100)
				{
					flag2 = this.Volume(float3x, @float, num4, stiffness, ref float3x2);
				}
				else
				{
					float num5 = (float)((b < 0) ? -1 : 1);
					if (triangleBendingConstraint.method == TriangleBendingConstraint.Method.DihedralAngle)
					{
						flag2 = this.DihedralAngle(0f, float3x, @float, num4, stiffness, ref float3x2);
					}
					else if (triangleBendingConstraint.method == TriangleBendingConstraint.Method.DirectionDihedralAngle)
					{
						num4 *= num5;
						flag2 = this.DihedralAngle(num5, float3x, @float, num4, stiffness, ref float3x2);
					}
				}
				if (flag2)
				{
					uint num6 = this.writeDataArray[index5];
					int4 int2 = DataUtility.Unpack32(num6);
					int startIndex3 = teamData.bendingWriteIndexChunk.startIndex;
					int startIndex4 = teamData.bendingBufferChunk.startIndex;
					for (int j = 0; j < 4; j++)
					{
						int num7 = @int[j];
						int num8 = DataUtility.Unpack10_22Low(this.writeIndexArray[startIndex3 + num7]);
						int index6 = startIndex4 + num8 + int2[j];
						this.writeBuffer[index6] = *float3x2[j];
					}
				}
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000CE60 File Offset: 0x0000B060
			private unsafe bool Volume(in float3x4 nextPosBuffer, in float4 invMassBuffer, float volumeRest, float stiffness, ref float3x4 addPosBuffer)
			{
				float3x4 float3x = nextPosBuffer;
				float3 @float = *float3x[0];
				float3x = nextPosBuffer;
				float3 float2 = *float3x[1];
				float3x = nextPosBuffer;
				float3 float3 = *float3x[2];
				float3x = nextPosBuffer;
				float3 lhs = *float3x[3];
				float4 float4 = invMassBuffer;
				float num = float4[0];
				float4 = invMassBuffer;
				float num2 = float4[1];
				float4 = invMassBuffer;
				float num3 = float4[2];
				float4 = invMassBuffer;
				float num4 = float4[3];
				float num5 = 0.16666667f * math.dot(math.cross(float2 - @float, float3 - @float), lhs - @float);
				float3 float5 = math.cross(float2 - float3, lhs - float3);
				float3 float6 = math.cross(float3 - @float, lhs - @float);
				float3 float7 = math.cross(@float - float2, lhs - float2);
				float3 float8 = math.cross(float2 - @float, float3 - @float);
				float num6 = num * math.lengthsq(float5) + num2 * math.lengthsq(float6) + num3 * math.lengthsq(float7) + num4 * math.lengthsq(float8);
				if (math.abs(num6) < 1E-06f)
				{
					return false;
				}
				num6 = stiffness * (num5 - volumeRest) / num6;
				*addPosBuffer[0] = -num6 * num * float5;
				*addPosBuffer[1] = -num6 * num2 * float6;
				*addPosBuffer[2] = -num6 * num3 * float7;
				*addPosBuffer[3] = -num6 * num4 * float8;
				return true;
			}

			// Token: 0x0600010C RID: 268 RVA: 0x0000D03C File Offset: 0x0000B23C
			private unsafe bool DihedralAngle(float sign, in float3x4 nextPosBuffer, in float4 invMassBuffer, float restAngle, float stiffness, ref float3x4 addPosBuffer)
			{
				float3x4 float3x = nextPosBuffer;
				float3 @float = *float3x[0];
				float3x = nextPosBuffer;
				float3 float2 = *float3x[1];
				float3x = nextPosBuffer;
				float3 float3 = *float3x[2];
				float3x = nextPosBuffer;
				float3 float4 = *float3x[3];
				float4 float5 = invMassBuffer;
				float num = float5[0];
				float5 = invMassBuffer;
				float num2 = float5[1];
				float5 = invMassBuffer;
				float num3 = float5[2];
				float5 = invMassBuffer;
				float num4 = float5[3];
				float3 float6 = float4 - float3;
				float num5 = math.length(float6);
				if (num5 < 1E-08f)
				{
					return false;
				}
				float num6 = 1f / num5;
				float3 float7 = math.cross(float3 - @float, float4 - @float);
				float3 float8 = math.cross(float4 - float2, float3 - float2);
				float7 /= math.lengthsq(float7);
				float8 /= math.lengthsq(float8);
				float3 float9 = num5 * float7;
				float3 float10 = num5 * float8;
				float3 float11 = math.dot(@float - float4, float6) * num6 * float7 + math.dot(float2 - float4, float6) * num6 * float8;
				float3 float12 = math.dot(float3 - @float, float6) * num6 * float7 + math.dot(float3 - float2, float6) * num6 * float8;
				float7 = math.normalize(float7);
				float8 = math.normalize(float8);
				float num7 = math.acos(MathUtility.Clamp1(math.dot(float7, float8)));
				float num8 = math.dot(math.cross(float7, float8), float6);
				if (sign != 0f)
				{
					num7 *= math.sign(num8);
					num8 = 1f;
				}
				float num9 = num * math.lengthsq(float9) + num2 * math.lengthsq(float10) + num3 * math.lengthsq(float11) + num4 * math.lengthsq(float12);
				if (num9 == 0f)
				{
					return false;
				}
				num9 = (num7 - restAngle) / num9 * stiffness;
				if (num8 > 0f)
				{
					num9 = -num9;
				}
				float3 float13 = -num * num9 * float9;
				float3 float14 = -num2 * num9 * float10;
				float3 float15 = -num3 * num9 * float11;
				float3 float16 = -num4 * num9 * float12;
				*addPosBuffer[0] = float13;
				*addPosBuffer[1] = float14;
				*addPosBuffer[2] = float15;
				*addPosBuffer[3] = float16;
				return true;
			}

			// Token: 0x0400020B RID: 523
			[ReadOnly]
			public NativeArray<int> stepTriangleBendIndexArray;

			// Token: 0x0400020C RID: 524
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400020D RID: 525
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x0400020E RID: 526
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400020F RID: 527
			[ReadOnly]
			public NativeArray<float> depthArray;

			// Token: 0x04000210 RID: 528
			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			// Token: 0x04000211 RID: 529
			[ReadOnly]
			public NativeArray<float> frictionArray;

			// Token: 0x04000212 RID: 530
			[ReadOnly]
			public NativeArray<ulong> trianglePairArray;

			// Token: 0x04000213 RID: 531
			[ReadOnly]
			public NativeArray<float> restAngleOrVolumeArray;

			// Token: 0x04000214 RID: 532
			[ReadOnly]
			public NativeArray<sbyte> signOrVolumeArray;

			// Token: 0x04000215 RID: 533
			[ReadOnly]
			public NativeArray<uint> writeDataArray;

			// Token: 0x04000216 RID: 534
			[ReadOnly]
			public NativeArray<uint> writeIndexArray;

			// Token: 0x04000217 RID: 535
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> writeBuffer;
		}

		// Token: 0x02000058 RID: 88
		[BurstCompile]
		private struct SolveAggregateBufferJob : IJobParallelForDefer
		{
			// Token: 0x0600010D RID: 269 RVA: 0x0000D300 File Offset: 0x0000B500
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (!teamData.bendingPairChunk.IsValid)
				{
					return;
				}
				int num2 = num - teamData.particleChunk.startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				if (this.attributes[index3].IsDontMove())
				{
					return;
				}
				uint pack = this.writeIndexArray[teamData.bendingWriteIndexChunk.startIndex + num2];
				int num3 = DataUtility.Unpack10_22Hi(pack);
				int num4 = DataUtility.Unpack10_22Low(pack);
				int num5 = teamData.bendingBufferChunk.startIndex + num4;
				float3 @float = 0;
				for (int i = 0; i < num3; i++)
				{
					@float += this.writeBuffer[num5 + i];
				}
				if (num3 > 0)
				{
					@float /= (float)num3;
					this.nextPosArray[num] = this.nextPosArray[num] + @float;
				}
			}

			// Token: 0x04000218 RID: 536
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x04000219 RID: 537
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x0400021A RID: 538
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x0400021B RID: 539
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x0400021C RID: 540
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x0400021D RID: 541
			[ReadOnly]
			public NativeArray<uint> writeIndexArray;

			// Token: 0x0400021E RID: 542
			[ReadOnly]
			public NativeArray<float3> writeBuffer;
		}
	}
}
