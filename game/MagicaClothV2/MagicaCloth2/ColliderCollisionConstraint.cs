using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000023 RID: 35
	public class ColliderCollisionConstraint : IDisposable
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00002058 File Offset: 0x00000258
		public ColliderCollisionConstraint()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006084 File Offset: 0x00004284
		public void Dispose()
		{
			ref this.tempFrictionArray.DisposeSafe<int>();
			ref this.tempNormalArray.DisposeSafe<int>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000609C File Offset: 0x0000429C
		internal void WorkBufferUpdate()
		{
			if (MagicaManager.Team.edgeColliderCollisionCount == 0)
			{
				return;
			}
			int particleCount = MagicaManager.Simulation.ParticleCount;
			ref this.tempFrictionArray.Resize(particleCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			ref this.tempNormalArray.Resize(particleCount * 3, Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000060E0 File Offset: 0x000042E0
		internal JobHandle SolverConstraint(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			SimulationManager simulation = MagicaManager.Simulation;
			VirtualMeshManager vmesh = MagicaManager.VMesh;
			ColliderManager collider = MagicaManager.Collider;
			jobHandle = new ColliderCollisionConstraint.PointColliderCollisionConstraintJob
			{
				stepParticleIndexArray = simulation.processingStepParticle.Buffer,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				parameterArray = team.parameterArray.GetNativeArray(),
				attributes = vmesh.attributes.GetNativeArray(),
				vertexDepths = vmesh.vertexDepths.GetNativeArray(),
				teamIdArray = simulation.teamIdArray.GetNativeArray(),
				nextPosArray = simulation.nextPosArray.GetNativeArray(),
				frictionArray = simulation.frictionArray.GetNativeArray(),
				collisionNormalArray = simulation.collisionNormalArray.GetNativeArray(),
				velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
				colliderFlagArray = collider.flagArray.GetNativeArray(),
				colliderWorkDataArray = collider.workDataArray.GetNativeArray()
			}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			if (team.edgeColliderCollisionCount > 0)
			{
				jobHandle = new ColliderCollisionConstraint.EdgeColliderCollisionConstraintJob
				{
					stepEdgeCollisionIndexArray = simulation.processingStepEdgeCollision.Buffer,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vmesh.attributes.GetNativeArray(),
					vertexDepths = vmesh.vertexDepths.GetNativeArray(),
					edgeTeamIdArray = vmesh.edgeTeamIdArray.GetNativeArray(),
					edges = vmesh.edges.GetNativeArray(),
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					collisionNormalArray = simulation.collisionNormalArray.GetNativeArray(),
					velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
					colliderFlagArray = collider.flagArray.GetNativeArray(),
					colliderWorkDataArray = collider.workDataArray.GetNativeArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray,
					tempFrictionArray = this.tempFrictionArray,
					tempNormalArray = this.tempNormalArray
				}.Schedule(simulation.processingStepEdgeCollision.GetJobSchedulePtr(), 32, jobHandle);
				jobHandle = new ColliderCollisionConstraint.SolveEdgeBufferAndClearJob
				{
					jobParticleIndexList = simulation.processingStepParticle.Buffer,
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					frictionArray = simulation.frictionArray.GetNativeArray(),
					velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
					collisionNormalArray = simulation.collisionNormalArray.GetNativeArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray,
					tempFrictionArray = this.tempFrictionArray,
					tempNormalArray = this.tempNormalArray
				}.Schedule(simulation.processingStepParticle.GetJobSchedulePtr(), 32, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x040000D2 RID: 210
		private NativeArray<int> tempFrictionArray;

		// Token: 0x040000D3 RID: 211
		private NativeArray<int> tempNormalArray;

		// Token: 0x02000024 RID: 36
		public enum Mode
		{
			// Token: 0x040000D5 RID: 213
			None,
			// Token: 0x040000D6 RID: 214
			Point,
			// Token: 0x040000D7 RID: 215
			Edge
		}

		// Token: 0x02000025 RID: 37
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x0600008D RID: 141 RVA: 0x000063F3 File Offset: 0x000045F3
			public SerializeData()
			{
				this.mode = ColliderCollisionConstraint.Mode.Point;
				this.friction = 0.05f;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00006418 File Offset: 0x00004618
			public void DataValidate()
			{
				this.friction = Mathf.Clamp(this.friction, 0f, 0.3f);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00006435 File Offset: 0x00004635
			public ColliderCollisionConstraint.SerializeData Clone()
			{
				return new ColliderCollisionConstraint.SerializeData
				{
					mode = this.mode,
					friction = this.friction,
					colliderList = new List<ColliderComponent>(this.colliderList)
				};
			}

			// Token: 0x040000D8 RID: 216
			public ColliderCollisionConstraint.Mode mode;

			// Token: 0x040000D9 RID: 217
			[Range(0f, 0.3f)]
			public float friction;

			// Token: 0x040000DA RID: 218
			public List<ColliderComponent> colliderList = new List<ColliderComponent>();
		}

		// Token: 0x02000026 RID: 38
		public struct ColliderCollisionConstraintParams
		{
			// Token: 0x06000090 RID: 144 RVA: 0x00006465 File Offset: 0x00004665
			public void Convert(ColliderCollisionConstraint.SerializeData sdata)
			{
				this.mode = sdata.mode;
				this.dynamicFriction = sdata.friction * 1f;
				this.staticFriction = sdata.friction * 1f;
			}

			// Token: 0x040000DB RID: 219
			public ColliderCollisionConstraint.Mode mode;

			// Token: 0x040000DC RID: 220
			public float dynamicFriction;

			// Token: 0x040000DD RID: 221
			public float staticFriction;
		}

		// Token: 0x02000027 RID: 39
		[BurstCompile]
		private struct PointColliderCollisionConstraintJob : IJobParallelForDefer
		{
			// Token: 0x06000091 RID: 145 RVA: 0x00006498 File Offset: 0x00004698
			public void Execute(int index)
			{
				int num = this.stepParticleIndexArray[index];
				int index2 = (int)this.teamIdArray[num];
				TeamManager.TeamData teamData = this.teamDataArray[index2];
				if (teamData.colliderCount == 0)
				{
					return;
				}
				ClothParameters clothParameters = this.parameterArray[index2];
				if (clothParameters.colliderCollisionConstraint.mode != ColliderCollisionConstraint.Mode.Point)
				{
					return;
				}
				float3 @float = this.nextPosArray[num];
				int num2 = num - teamData.particleChunk.startIndex;
				int index3 = teamData.proxyCommonChunk.startIndex + num2;
				if (!this.attributes[index3].IsMove())
				{
					return;
				}
				float time = this.vertexDepths[index3];
				float num3 = math.max(clothParameters.radiusCurveData.EvaluateCurve(time), 0.0001f);
				num3 *= teamData.scaleRatio;
				float num4 = float.MaxValue;
				int num5 = -1;
				float3 float2 = 0;
				float3 rhs = 0;
				float3 lhs = 0;
				int num6 = 0;
				float3 float3 = 0;
				float num7 = num3 * 1f;
				float3 float4 = @float - num3;
				float3 float5 = @float + num3;
				AABB aabb = new AABB(ref float4, ref float5);
				aabb.Expand(num7);
				int num8 = teamData.colliderChunk.startIndex;
				int colliderCount = teamData.colliderCount;
				int i = 0;
				while (i < colliderCount)
				{
					ExBitFlag8 exBitFlag = this.colliderFlagArray[num8];
					if (exBitFlag.IsSet(16) && exBitFlag.IsSet(32))
					{
						ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(exBitFlag);
						ColliderManager.WorkData workData = this.colliderWorkDataArray[num8];
						float num9 = 100f;
						float3 lhs2 = @float;
						switch (colliderType)
						{
						case ColliderManager.ColliderType.Sphere:
							num9 = this.PointSphereColliderDetection(ref lhs2, num3, aabb, workData, out rhs);
							break;
						case ColliderManager.ColliderType.CapsuleX:
						case ColliderManager.ColliderType.CapsuleY:
						case ColliderManager.ColliderType.CapsuleZ:
							num9 = this.PointCapsuleColliderDetection(ref lhs2, num3, aabb, workData, out rhs);
							break;
						case ColliderManager.ColliderType.Plane:
							num9 = this.PointPlaneColliderDetction(ref lhs2, num3, workData, out rhs);
							break;
						default:
							Debug.LogError(string.Format("unknown collider type:{0}", colliderType));
							break;
						}
						if (num9 <= 0f)
						{
							lhs += lhs2 - @float;
							float3 += rhs;
							num6++;
						}
						if (num9 <= num7)
						{
							num5 = num8;
							float2 += rhs;
							num4 = math.min(num4, num9);
						}
					}
					i++;
					num8++;
				}
				if (num6 > 0)
				{
					float3 /= (float)num6;
					float num10 = math.length(float3);
					if (num10 < 1E-08f)
					{
						lhs = 0;
					}
					else
					{
						float rhs2 = math.min(num10, 1f);
						lhs /= (float)num6;
						@float += lhs * rhs2;
					}
				}
				if (num5 >= 0)
				{
					float x = 1f - math.saturate(num4 / num7);
					this.frictionArray[num] = math.max(x, this.frictionArray[num]);
					float2 = math.normalize(float2);
				}
				this.collisionNormalArray[num] = float2;
				this.nextPosArray[num] = @float;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x000067BC File Offset: 0x000049BC
			private float PointSphereColliderDetection(ref float3 nextpos, float radius, in AABB aabb, in ColliderManager.WorkData cwork, out float3 normal)
			{
				normal = 0;
				AABB aabb2 = aabb;
				if (!aabb2.Overlaps(cwork.aabb))
				{
					return float.MaxValue;
				}
				float3 c = cwork.oldPos.c0;
				float3 c2 = cwork.nextPos.c0;
				float x = cwork.radius.x;
				float3 @float = math.normalize(nextpos - c);
				float3 float2 = c2 + @float * (x + radius);
				normal = @float;
				return MathUtility.IntersectPointPlaneDist(float2, @float, nextpos, out nextpos);
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00006850 File Offset: 0x00004A50
			private float PointPlaneColliderDetction(ref float3 nextpos, float radius, in ColliderManager.WorkData cwork, out float3 normal)
			{
				float3 c = cwork.nextPos.c0;
				float3 c2 = cwork.oldPos.c0;
				normal = c2;
				float3 @float = c + c2 * radius;
				return MathUtility.IntersectPointPlaneDist(@float, c2, nextpos, out nextpos);
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00006894 File Offset: 0x00004A94
			private float PointCapsuleColliderDetection(ref float3 nextpos, float radius, in AABB aabb, in ColliderManager.WorkData cwork, out float3 normal)
			{
				normal = 0;
				AABB aabb2 = aabb;
				if (!aabb2.Overlaps(cwork.aabb))
				{
					return float.MaxValue;
				}
				float3 c = cwork.oldPos.c0;
				float3 c2 = cwork.oldPos.c1;
				float3 c3 = cwork.nextPos.c0;
				float3 c4 = cwork.nextPos.c1;
				float x = cwork.radius.x;
				float y = cwork.radius.y;
				float s = MathUtility.ClosestPtPointSegmentRatio(nextpos, c, c2);
				float num = math.lerp(x, y, s);
				float3 @float = math.lerp(c, c2, s);
				float3 float2 = nextpos - @float;
				float3 v = math.mul(cwork.inverseOldRot, float2);
				@float = math.lerp(c3, c4, s);
				float2 = math.mul(cwork.rot, v);
				float3 float3 = math.normalize(float2);
				float3 float4 = @float + float3 * (num + radius);
				normal = float3;
				return MathUtility.IntersectPointPlaneDist(float4, float3, nextpos, out nextpos);
			}

			// Token: 0x040000DE RID: 222
			[ReadOnly]
			public NativeArray<int> stepParticleIndexArray;

			// Token: 0x040000DF RID: 223
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040000E0 RID: 224
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040000E1 RID: 225
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040000E2 RID: 226
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040000E3 RID: 227
			[ReadOnly]
			public NativeArray<short> teamIdArray;

			// Token: 0x040000E4 RID: 228
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040000E5 RID: 229
			[NativeDisableParallelForRestriction]
			public NativeArray<float> frictionArray;

			// Token: 0x040000E6 RID: 230
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> collisionNormalArray;

			// Token: 0x040000E7 RID: 231
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040000E8 RID: 232
			[ReadOnly]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			// Token: 0x040000E9 RID: 233
			[ReadOnly]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;
		}

		// Token: 0x02000028 RID: 40
		[BurstCompile]
		private struct EdgeColliderCollisionConstraintJob : IJobParallelForDefer
		{
			// Token: 0x06000095 RID: 149 RVA: 0x000069A4 File Offset: 0x00004BA4
			public unsafe void Execute(int index)
			{
				int index2 = this.stepEdgeCollisionIndexArray[index];
				int index3 = (int)this.edgeTeamIdArray[index2];
				TeamManager.TeamData teamData = this.teamDataArray[index3];
				if (teamData.colliderCount == 0)
				{
					return;
				}
				ClothParameters clothParameters = this.parameterArray[index3];
				if (clothParameters.colliderCollisionConstraint.mode != ColliderCollisionConstraint.Mode.Edge)
				{
					return;
				}
				int startIndex = teamData.proxyCommonChunk.startIndex;
				int2 lhs = this.edges[index2];
				int2 @int = lhs + startIndex;
				VertexAttribute vertexAttribute = this.attributes[@int.x];
				VertexAttribute vertexAttribute2 = this.attributes[@int.y];
				if (!vertexAttribute.IsMove() && !vertexAttribute2.IsMove())
				{
					return;
				}
				int startIndex2 = teamData.particleChunk.startIndex;
				int2 int2 = lhs + startIndex2;
				float3x2 float3x = new float3x2(this.nextPosArray[int2.x], this.nextPosArray[int2.y]);
				float2 @float = new float2(this.vertexDepths[@int.x], this.vertexDepths[@int.y]);
				float2 float2 = new float2(clothParameters.radiusCurveData.EvaluateCurve(@float.x), clothParameters.radiusCurveData.EvaluateCurve(@float.y));
				float2 *= teamData.scaleRatio;
				float num = (float2.x + float2.y) * 0.5f * 1f;
				float num2 = float.MaxValue;
				int num3 = -1;
				float3 float3 = 0;
				float3 rhs = 0;
				int* unsafePtr = (int*)this.countArray.GetUnsafePtr<int>();
				int* unsafePtr2 = (int*)this.sumArray.GetUnsafePtr<int>();
				int* unsafePtr3 = (int*)this.tempFrictionArray.GetUnsafePtr<int>();
				int* unsafePtr4 = (int*)this.tempNormalArray.GetUnsafePtr<int>();
				float3 float4 = float3x.c0 - float2.x;
				float3 float5 = float3x.c0 + float2.x;
				AABB aabb = new AABB(ref float4, ref float5);
				float4 = float3x.c1 - float2.y;
				float5 = float3x.c1 + float2.y;
				AABB aabb2 = new AABB(ref float4, ref float5);
				aabb.Encapsulate(aabb2);
				aabb.Expand(num);
				float3x2 float3x2 = 0;
				int num4 = 0;
				float3 float6 = 0;
				int num5 = teamData.colliderChunk.startIndex;
				int colliderCount = teamData.colliderCount;
				int i = 0;
				while (i < colliderCount)
				{
					ExBitFlag8 exBitFlag = this.colliderFlagArray[num5];
					if (exBitFlag.IsSet(16) && exBitFlag.IsSet(32))
					{
						ColliderManager.ColliderType colliderType = DataUtility.GetColliderType(exBitFlag);
						ColliderManager.WorkData workData = this.colliderWorkDataArray[num5];
						float num6 = 100f;
						float3x2 lhs2 = float3x;
						switch (colliderType)
						{
						case ColliderManager.ColliderType.Sphere:
							num6 = this.EdgeSphereColliderDetection(ref lhs2, float2, aabb, num, workData, out rhs);
							break;
						case ColliderManager.ColliderType.CapsuleX:
						case ColliderManager.ColliderType.CapsuleY:
						case ColliderManager.ColliderType.CapsuleZ:
							num6 = this.EdgeCapsuleColliderDetection(ref lhs2, float2, aabb, num, workData, out rhs);
							break;
						case ColliderManager.ColliderType.Plane:
							num6 = this.EdgePlaneColliderDetection(ref lhs2, float2, workData, out rhs);
							break;
						default:
							Debug.LogError(string.Format("Unknown collider type:{0}", colliderType));
							break;
						}
						if (num6 <= 0f)
						{
							float3x2 += lhs2 - float3x;
							float6 += rhs;
							num4++;
						}
						if (num6 <= num)
						{
							num3 = num5;
							float3 += rhs;
							num2 = math.min(num2, num6);
						}
					}
					i++;
					num5++;
				}
				if (num4 > 0)
				{
					float6 /= (float)num4;
					float num7 = math.length(float6);
					if (num7 > 1E-08f)
					{
						float rhs2 = math.min(num7, 1f);
						float3x2 /= (float)num4;
						float3x2 *= rhs2;
						InterlockUtility.AddFloat3(int2.x, float3x2.c0, unsafePtr, unsafePtr2);
						InterlockUtility.AddFloat3(int2.y, float3x2.c1, unsafePtr, unsafePtr2);
					}
				}
				if (num3 >= 0)
				{
					float value = 1f - math.saturate(num2 / num);
					InterlockUtility.Max(int2.x, value, unsafePtr3);
					InterlockUtility.Max(int2.y, value, unsafePtr3);
					float3 = math.normalize(float3);
					InterlockUtility.AddFloat3(int2.x, float3, unsafePtr4);
					InterlockUtility.AddFloat3(int2.y, float3, unsafePtr4);
				}
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00006E18 File Offset: 0x00005018
			private float EdgeSphereColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in AABB aabbE, float cfr, in ColliderManager.WorkData cwork, out float3 normal)
			{
				normal = 0;
				AABB aabb = aabbE;
				if (!aabb.Overlaps(cwork.aabb))
				{
					return float.MaxValue;
				}
				float3 c = cwork.oldPos.c0;
				float3 c2 = cwork.nextPos.c0;
				float x = cwork.radius.x;
				float num = MathUtility.ClosestPtPointSegmentRatio(c, nextPosE.c0, nextPosE.c1);
				float3 lhs = math.lerp(nextPosE.c0, nextPosE.c1, num);
				float3 @float = lhs - c;
				float num2 = math.length(@float);
				if (num2 < 1E-09f)
				{
					return float.MaxValue;
				}
				float3 float2 = @float / num2;
				normal = float2;
				float3 y = c2 - c;
				float num3 = math.dot(float2, y);
				float num4 = num2 - num3;
				float num5 = math.lerp(radiusE.x, radiusE.y, num);
				float num6 = x;
				float num7 = num5 + num6;
				if (num4 > num7 + cfr)
				{
					return float.MaxValue;
				}
				@float = lhs - c2;
				num4 = math.dot(float2, @float);
				if (num4 > num7)
				{
					return num4 - num7;
				}
				float num8 = num7 - num4;
				float2 float3 = new float2(1f - num, num);
				float3x2 lhs2 = new float3x2(float2 * float3.x, float2 * float3.y);
				float num9 = math.dot(float3, float3);
				if (num9 == 0f)
				{
					return float.MaxValue;
				}
				num9 = num8 / num9;
				float3x2 rhs = lhs2 * num9;
				nextPosE += rhs;
				return -num8;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00006FB8 File Offset: 0x000051B8
			private float EdgeCapsuleColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in AABB aabbE, float cfr, in ColliderManager.WorkData cwork, out float3 normal)
			{
				normal = 0;
				AABB aabb = aabbE;
				if (!aabb.Overlaps(cwork.aabb))
				{
					return float.MaxValue;
				}
				float3 c = cwork.oldPos.c0;
				float3 c2 = cwork.oldPos.c1;
				float3 c3 = cwork.nextPos.c0;
				float3 c4 = cwork.nextPos.c1;
				float x = cwork.radius.x;
				float y = cwork.radius.y;
				float num2;
				float s;
				float3 lhs;
				float3 rhs;
				float num = math.sqrt(MathUtility.ClosestPtSegmentSegment(nextPosE.c0, nextPosE.c1, c, c2, out num2, out s, out lhs, out rhs));
				if (num < 1E-09f)
				{
					return float.MaxValue;
				}
				float3 @float = lhs - rhs;
				float3 float2 = math.normalize(@float);
				normal = float2;
				float3 x2 = c3 - c;
				float3 y2 = c4 - c2;
				float3 y3 = math.lerp(x2, y2, s);
				float num3 = math.dot(float2, y3);
				float num4 = num - num3;
				float num5 = math.lerp(radiusE.x, radiusE.y, num2);
				float num6 = math.lerp(x, y, s);
				float num7 = num5 + num6;
				if (num4 > num7 + cfr)
				{
					return float.MaxValue;
				}
				float3 rhs2 = math.lerp(c3, c4, s);
				@float = lhs - rhs2;
				num4 = math.dot(float2, @float);
				if (num4 > num7)
				{
					return num4 - num7;
				}
				float num8 = num7 - num4;
				float2 float3 = new float2(1f - num2, num2);
				float3x2 lhs2 = new float3x2(float2 * float3.x, float2 * float3.y);
				float num9 = math.dot(float3, float3);
				if (num9 == 0f)
				{
					return float.MaxValue;
				}
				num9 = num8 / num9;
				float3x2 rhs3 = lhs2 * num9;
				nextPosE += rhs3;
				return -num8;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x0000719C File Offset: 0x0000539C
			private float EdgePlaneColliderDetection(ref float3x2 nextPosE, in float2 radiusE, in ColliderManager.WorkData cwork, out float3 normal)
			{
				float3 c = cwork.nextPos.c0;
				float3 c2 = cwork.oldPos.c0;
				normal = c2;
				float3 @float = c + c2 * radiusE.x;
				float x = MathUtility.IntersectPointPlaneDist(@float, c2, nextPosE.c0, out nextPosE.c0);
				@float = c + c2 * radiusE.y;
				float y = MathUtility.IntersectPointPlaneDist(@float, c2, nextPosE.c1, out nextPosE.c1);
				return math.min(x, y);
			}

			// Token: 0x040000EA RID: 234
			[ReadOnly]
			public NativeArray<int> stepEdgeCollisionIndexArray;

			// Token: 0x040000EB RID: 235
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			// Token: 0x040000EC RID: 236
			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			// Token: 0x040000ED RID: 237
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			// Token: 0x040000EE RID: 238
			[ReadOnly]
			public NativeArray<float> vertexDepths;

			// Token: 0x040000EF RID: 239
			[ReadOnly]
			public NativeArray<short> edgeTeamIdArray;

			// Token: 0x040000F0 RID: 240
			[ReadOnly]
			public NativeArray<int2> edges;

			// Token: 0x040000F1 RID: 241
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040000F2 RID: 242
			[NativeDisableParallelForRestriction]
			public NativeArray<float> frictionArray;

			// Token: 0x040000F3 RID: 243
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> collisionNormalArray;

			// Token: 0x040000F4 RID: 244
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040000F5 RID: 245
			[ReadOnly]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			// Token: 0x040000F6 RID: 246
			[ReadOnly]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;

			// Token: 0x040000F7 RID: 247
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040000F8 RID: 248
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;

			// Token: 0x040000F9 RID: 249
			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempFrictionArray;

			// Token: 0x040000FA RID: 250
			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempNormalArray;
		}

		// Token: 0x02000029 RID: 41
		[BurstCompile]
		private struct SolveEdgeBufferAndClearJob : IJobParallelForDefer
		{
			// Token: 0x06000099 RID: 153 RVA: 0x00007220 File Offset: 0x00005420
			public void Execute(int index)
			{
				int num = this.jobParticleIndexList[index];
				int num2 = this.countArray[num];
				int num3 = num * 3;
				if (num2 > 0)
				{
					float3 rhs = InterlockUtility.ReadAverageFloat3(num, this.countArray, this.sumArray);
					this.nextPosArray[num] = this.nextPosArray[num] + rhs;
					this.countArray[num] = 0;
					this.sumArray[num3] = 0;
					this.sumArray[num3 + 1] = 0;
					this.sumArray[num3 + 2] = 0;
				}
				float num4 = InterlockUtility.ReadFloat(num, this.tempFrictionArray);
				if (num4 > 0f && num4 > this.frictionArray[num])
				{
					this.frictionArray[num] = num4;
					this.tempFrictionArray[num] = 0;
				}
				float3 @float = InterlockUtility.ReadFloat3(num, this.tempNormalArray);
				if (math.lengthsq(@float) > 0f)
				{
					@float = math.normalize(@float);
					this.collisionNormalArray[num] = @float;
					this.tempNormalArray[num3] = 0;
					this.tempNormalArray[num3 + 1] = 0;
					this.tempNormalArray[num3 + 2] = 0;
				}
			}

			// Token: 0x040000FB RID: 251
			[ReadOnly]
			public NativeArray<int> jobParticleIndexList;

			// Token: 0x040000FC RID: 252
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040000FD RID: 253
			[NativeDisableParallelForRestriction]
			public NativeArray<float> frictionArray;

			// Token: 0x040000FE RID: 254
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> collisionNormalArray;

			// Token: 0x040000FF RID: 255
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x04000100 RID: 256
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x04000101 RID: 257
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;

			// Token: 0x04000102 RID: 258
			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempFrictionArray;

			// Token: 0x04000103 RID: 259
			[NativeDisableParallelForRestriction]
			public NativeArray<int> tempNormalArray;
		}
	}
}
