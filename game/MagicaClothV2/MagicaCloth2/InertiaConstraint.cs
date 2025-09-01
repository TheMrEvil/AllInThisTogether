using System;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200002F RID: 47
	public class InertiaConstraint : IDisposable
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00007F01 File Offset: 0x00006101
		public InertiaConstraint()
		{
			this.fixedArray = new ExNativeArray<ushort>(0, true);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007F16 File Offset: 0x00006116
		public void Dispose()
		{
			ExNativeArray<ushort> exNativeArray = this.fixedArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			this.fixedArray = null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007F30 File Offset: 0x00006130
		internal static InertiaConstraint.ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			InertiaConstraint.ConstraintData constraintData = new InertiaConstraint.ConstraintData();
			try
			{
				constraintData.centerData = new InertiaConstraint.CenterData
				{
					centerTransformIndex = proxyMesh.centerTransformIndex
				};
				float3 @float = 0;
				float3 float2 = 0;
				int centerFixedPointCount = proxyMesh.CenterFixedPointCount;
				if (centerFixedPointCount > 0)
				{
					for (int i = 0; i < centerFixedPointCount; i++)
					{
						int index = (int)proxyMesh.centerFixedList[i];
						float3 float3 = proxyMesh.localNormals[index];
						float3 float4 = proxyMesh.localTangents[index];
						quaternion a = MathUtility.ToRotation(float3, float4);
						quaternion b = proxyMesh.vertexBindPoseRotations[index];
						quaternion quaternion = math.mul(a, b);
						@float += MathUtility.ToNormal(quaternion);
						float2 += MathUtility.ToTangent(quaternion);
					}
				}
				float3 initLocalGravityDirection = new float3(0f, -1f, 0f);
				if (centerFixedPointCount > 0)
				{
					float3 float5 = math.normalize(@float);
					float3 float6 = math.normalize(float2);
					initLocalGravityDirection = math.mul(math.inverse(MathUtility.ToRotation(float5, float6)), parameters.gravityDirection);
				}
				constraintData.initLocalGravityDirection = initLocalGravityDirection;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateInertiaException);
			}
			return constraintData;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000806C File Offset: 0x0000626C
		internal void Register(ClothProcess cprocess)
		{
			TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
			InertiaConstraint.CenterData value = MagicaManager.Team.centerDataArray[cprocess.TeamId];
			value.centerTransformIndex = teamData.centerTransformIndex;
			value.initLocalGravityDirection = cprocess.inertiaConstraintData.initLocalGravityDirection;
			DataChunk fixedDataChunk = default(DataChunk);
			if (cprocess.ProxyMesh.CenterFixedPointCount > 0)
			{
				fixedDataChunk = this.fixedArray.AddRange(cprocess.ProxyMesh.centerFixedList);
			}
			teamData.fixedDataChunk = fixedDataChunk;
			MagicaManager.Team.centerDataArray[cprocess.TeamId] = value;
			MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000811C File Offset: 0x0000631C
		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				TeamManager.TeamData teamData = MagicaManager.Team.GetTeamData(cprocess.TeamId);
				this.fixedArray.Remove(teamData.fixedDataChunk);
				teamData.fixedDataChunk.Clear();
				MagicaManager.Team.SetTeamData(cprocess.TeamId, teamData);
			}
		}

		// Token: 0x0400011C RID: 284
		internal ExNativeArray<ushort> fixedArray;

		// Token: 0x02000030 RID: 48
		[Serializable]
		public class SerializeData : IDataValidate
		{
			// Token: 0x060000AD RID: 173 RVA: 0x00008174 File Offset: 0x00006374
			public SerializeData()
			{
				this.movementInertia = 1f;
				this.rotationInertia = 1f;
				this.depthInertia = 0f;
				this.centrifualAcceleration = 0f;
				this.movementSpeedLimit = new CheckSliderSerializeData(true, 5f);
				this.rotationSpeedLimit = new CheckSliderSerializeData(true, 720f);
				this.particleSpeedLimit = new CheckSliderSerializeData(true, 4f);
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000081E8 File Offset: 0x000063E8
			public InertiaConstraint.SerializeData Clone()
			{
				return new InertiaConstraint.SerializeData
				{
					movementInertia = this.movementInertia,
					rotationInertia = this.rotationInertia,
					depthInertia = this.depthInertia,
					centrifualAcceleration = this.centrifualAcceleration,
					movementSpeedLimit = this.movementSpeedLimit.Clone(),
					rotationSpeedLimit = this.rotationSpeedLimit.Clone()
				};
			}

			// Token: 0x060000AF RID: 175 RVA: 0x0000824C File Offset: 0x0000644C
			public void DataValidate()
			{
				this.movementInertia = Mathf.Clamp01(this.movementInertia);
				this.rotationInertia = Mathf.Clamp01(this.rotationInertia);
				this.centrifualAcceleration = Mathf.Clamp01(this.centrifualAcceleration);
				this.depthInertia = Mathf.Clamp01(this.depthInertia);
				this.movementSpeedLimit.DataValidate(0f, 10f);
				this.rotationSpeedLimit.DataValidate(0f, 1440f);
				this.particleSpeedLimit.DataValidate(0f, 10f);
			}

			// Token: 0x0400011D RID: 285
			[Range(0f, 1f)]
			public float movementInertia;

			// Token: 0x0400011E RID: 286
			[Range(0f, 1f)]
			public float rotationInertia;

			// Token: 0x0400011F RID: 287
			[Range(0f, 1f)]
			public float depthInertia;

			// Token: 0x04000120 RID: 288
			[Range(0f, 1f)]
			public float centrifualAcceleration;

			// Token: 0x04000121 RID: 289
			public CheckSliderSerializeData movementSpeedLimit;

			// Token: 0x04000122 RID: 290
			public CheckSliderSerializeData rotationSpeedLimit;

			// Token: 0x04000123 RID: 291
			public CheckSliderSerializeData particleSpeedLimit;
		}

		// Token: 0x02000031 RID: 49
		public struct InertiaConstraintParams
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x000082DC File Offset: 0x000064DC
			public void Convert(InertiaConstraint.SerializeData sdata)
			{
				this.movementInertia = sdata.movementInertia;
				this.rotationInertia = sdata.rotationInertia;
				this.depthInertia = sdata.depthInertia;
				this.centrifualAcceleration = sdata.centrifualAcceleration;
				this.movementSpeedLimit = sdata.movementSpeedLimit.GetValue(-1f);
				this.rotationSpeedLimit = sdata.rotationSpeedLimit.GetValue(-1f);
				this.particleSpeedLimit = sdata.particleSpeedLimit.GetValue(-1f);
			}

			// Token: 0x04000124 RID: 292
			public float movementInertia;

			// Token: 0x04000125 RID: 293
			public float rotationInertia;

			// Token: 0x04000126 RID: 294
			public float depthInertia;

			// Token: 0x04000127 RID: 295
			public float centrifualAcceleration;

			// Token: 0x04000128 RID: 296
			public float movementSpeedLimit;

			// Token: 0x04000129 RID: 297
			public float rotationSpeedLimit;

			// Token: 0x0400012A RID: 298
			public float particleSpeedLimit;
		}

		// Token: 0x02000032 RID: 50
		public struct CenterData
		{
			// Token: 0x0400012B RID: 299
			public int centerTransformIndex;

			// Token: 0x0400012C RID: 300
			public float3 frameWorldPosition;

			// Token: 0x0400012D RID: 301
			public quaternion frameWorldRotation;

			// Token: 0x0400012E RID: 302
			public float3 frameWorldScale;

			// Token: 0x0400012F RID: 303
			public float3 frameLocalPosition;

			// Token: 0x04000130 RID: 304
			public float3 oldFrameWorldPosition;

			// Token: 0x04000131 RID: 305
			public quaternion oldFrameWorldRotation;

			// Token: 0x04000132 RID: 306
			public float3 oldFrameWorldScale;

			// Token: 0x04000133 RID: 307
			public float3 nowWorldPosition;

			// Token: 0x04000134 RID: 308
			public quaternion nowWorldRotation;

			// Token: 0x04000135 RID: 309
			public float3 nowWorldScale;

			// Token: 0x04000136 RID: 310
			public float3 oldWorldPosition;

			// Token: 0x04000137 RID: 311
			public quaternion oldWorldRotation;

			// Token: 0x04000138 RID: 312
			public float3 frameVector;

			// Token: 0x04000139 RID: 313
			public quaternion frameRotation;

			// Token: 0x0400013A RID: 314
			public float stepMoveInertiaRatio;

			// Token: 0x0400013B RID: 315
			public float stepRotationInertiaRatio;

			// Token: 0x0400013C RID: 316
			public float3 stepVector;

			// Token: 0x0400013D RID: 317
			public quaternion stepRotation;

			// Token: 0x0400013E RID: 318
			public float3 inertiaVector;

			// Token: 0x0400013F RID: 319
			public quaternion inertiaRotation;

			// Token: 0x04000140 RID: 320
			public float angularVelocity;

			// Token: 0x04000141 RID: 321
			public float3 rotationAxis;

			// Token: 0x04000142 RID: 322
			public float3 initLocalGravityDirection;

			// Token: 0x04000143 RID: 323
			public float3 initLocalCenterPosition;
		}

		// Token: 0x02000033 RID: 51
		public class ConstraintData
		{
			// Token: 0x060000B1 RID: 177 RVA: 0x00002058 File Offset: 0x00000258
			public ConstraintData()
			{
			}

			// Token: 0x04000144 RID: 324
			public ResultCode result;

			// Token: 0x04000145 RID: 325
			public InertiaConstraint.CenterData centerData;

			// Token: 0x04000146 RID: 326
			public float3 initLocalGravityDirection;
		}
	}
}
