using System;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	// Token: 0x02000098 RID: 152
	public class TransformManager : IManager, IDisposable, IValid
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0001773F File Offset: 0x0001593F
		internal int Count
		{
			get
			{
				ExNativeArray<ExBitFlag8> exNativeArray = this.flagArray;
				if (exNativeArray == null)
				{
					return 0;
				}
				return exNativeArray.Count;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00017754 File Offset: 0x00015954
		public void Dispose()
		{
			this.isValid = false;
			ExNativeArray<ExBitFlag8> exNativeArray = this.flagArray;
			if (exNativeArray != null)
			{
				exNativeArray.Dispose();
			}
			ExNativeArray<float3> exNativeArray2 = this.initLocalPositionArray;
			if (exNativeArray2 != null)
			{
				exNativeArray2.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray3 = this.initLocalRotationArray;
			if (exNativeArray3 != null)
			{
				exNativeArray3.Dispose();
			}
			ExNativeArray<float3> exNativeArray4 = this.positionArray;
			if (exNativeArray4 != null)
			{
				exNativeArray4.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray5 = this.rotationArray;
			if (exNativeArray5 != null)
			{
				exNativeArray5.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray6 = this.inverseRotationArray;
			if (exNativeArray6 != null)
			{
				exNativeArray6.Dispose();
			}
			ExNativeArray<float3> exNativeArray7 = this.scaleArray;
			if (exNativeArray7 != null)
			{
				exNativeArray7.Dispose();
			}
			ExNativeArray<float3> exNativeArray8 = this.localPositionArray;
			if (exNativeArray8 != null)
			{
				exNativeArray8.Dispose();
			}
			ExNativeArray<quaternion> exNativeArray9 = this.localRotationArray;
			if (exNativeArray9 != null)
			{
				exNativeArray9.Dispose();
			}
			this.flagArray = null;
			this.initLocalPositionArray = null;
			this.initLocalRotationArray = null;
			this.positionArray = null;
			this.rotationArray = null;
			this.inverseRotationArray = null;
			this.scaleArray = null;
			this.localPositionArray = null;
			this.localRotationArray = null;
			if (this.transformAccessArray.isCreated)
			{
				this.transformAccessArray.Dispose();
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00017858 File Offset: 0x00015A58
		public void EnterdEditMode()
		{
			this.Dispose();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00017860 File Offset: 0x00015A60
		public void Initialize()
		{
			this.Dispose();
			this.flagArray = new ExNativeArray<ExBitFlag8>(256, false);
			this.initLocalPositionArray = new ExNativeArray<float3>(256, false);
			this.initLocalRotationArray = new ExNativeArray<quaternion>(256, false);
			this.positionArray = new ExNativeArray<float3>(256, false);
			this.rotationArray = new ExNativeArray<quaternion>(256, false);
			this.inverseRotationArray = new ExNativeArray<quaternion>(256, false);
			this.scaleArray = new ExNativeArray<float3>(256, false);
			this.localPositionArray = new ExNativeArray<float3>(256, false);
			this.localRotationArray = new ExNativeArray<quaternion>(256, false);
			this.transformAccessArray = new TransformAccessArray(256, -1);
			this.isValid = true;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00017924 File Offset: 0x00015B24
		public bool IsValid()
		{
			return this.isValid;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001792C File Offset: 0x00015B2C
		internal DataChunk AddTransform(TransformData tdata)
		{
			if (!this.isValid)
			{
				return default(DataChunk);
			}
			int count = tdata.Count;
			DataChunk dataChunk = this.flagArray.AddRange(tdata.flagArray);
			this.initLocalPositionArray.AddRange(tdata.initLocalPositionArray);
			this.initLocalRotationArray.AddRange(tdata.initLocalRotationArray);
			this.positionArray.AddRange(count);
			this.rotationArray.AddRange(count);
			this.inverseRotationArray.AddRange(count);
			this.scaleArray.AddRange(count);
			this.localPositionArray.AddRange(count);
			this.localRotationArray.AddRange(count);
			int i = this.transformAccessArray.length;
			int startIndex = dataChunk.startIndex;
			while (i < startIndex)
			{
				this.transformAccessArray.Add(null);
				i++;
			}
			for (int j = 0; j < count; j++)
			{
				Transform transform = tdata.transformList[j];
				int num = dataChunk.startIndex + j;
				if (num < i)
				{
					this.transformAccessArray[num] = transform;
				}
				else
				{
					this.transformAccessArray.Add(transform);
				}
			}
			return dataChunk;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00017A50 File Offset: 0x00015C50
		internal DataChunk AddTransform(int count)
		{
			if (!this.isValid)
			{
				return default(DataChunk);
			}
			DataChunk dataChunk = this.flagArray.AddRange(count);
			this.initLocalPositionArray.AddRange(count);
			this.initLocalRotationArray.AddRange(count);
			this.positionArray.AddRange(count);
			this.rotationArray.AddRange(count);
			this.inverseRotationArray.AddRange(count);
			this.scaleArray.AddRange(count);
			this.localPositionArray.AddRange(count);
			this.localRotationArray.AddRange(count);
			int i = this.transformAccessArray.length;
			int startIndex = dataChunk.startIndex;
			while (i < startIndex)
			{
				this.transformAccessArray.Add(null);
				i++;
			}
			for (int j = 0; j < count; j++)
			{
				Transform transform = null;
				int num = dataChunk.startIndex + j;
				if (num < i)
				{
					this.transformAccessArray[num] = transform;
				}
				else
				{
					this.transformAccessArray.Add(transform);
				}
			}
			return dataChunk;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00017B50 File Offset: 0x00015D50
		internal DataChunk AddTransform(Transform t, ExBitFlag8 flag)
		{
			if (!this.isValid)
			{
				return default(DataChunk);
			}
			DataChunk dataChunk = this.flagArray.Add(flag);
			this.initLocalPositionArray.Add(t.localPosition);
			this.initLocalRotationArray.Add(t.localRotation);
			this.positionArray.Add(t.position);
			this.rotationArray.Add(t.rotation);
			this.inverseRotationArray.Add(math.inverse(t.rotation));
			this.scaleArray.Add(t.lossyScale);
			this.localPositionArray.Add(t.localPosition);
			this.localRotationArray.Add(t.localRotation);
			int length = this.transformAccessArray.length;
			int startIndex = dataChunk.startIndex;
			if (startIndex < length)
			{
				this.transformAccessArray[startIndex] = t;
				return dataChunk;
			}
			this.transformAccessArray.Add(t);
			return dataChunk;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00017C6C File Offset: 0x00015E6C
		internal void SetTransform(Transform t, ExBitFlag8 flag, int index)
		{
			if (!this.isValid)
			{
				return;
			}
			if (t != null)
			{
				this.flagArray[index] = flag;
				this.initLocalPositionArray[index] = t.localPosition;
				this.initLocalRotationArray[index] = t.localRotation;
				this.positionArray[index] = t.position;
				this.rotationArray[index] = t.rotation;
				this.inverseRotationArray[index] = math.inverse(t.rotation);
				this.scaleArray[index] = t.lossyScale;
				this.localPositionArray[index] = t.localPosition;
				this.localRotationArray[index] = t.localRotation;
				this.transformAccessArray[index] = t;
				return;
			}
			this.flagArray[index] = default(ExBitFlag8);
			this.transformAccessArray[index] = null;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00017D88 File Offset: 0x00015F88
		internal void CopyTransform(int fromIndex, int toIndex)
		{
			if (!this.isValid)
			{
				return;
			}
			this.flagArray[toIndex] = this.flagArray[fromIndex];
			this.initLocalPositionArray[toIndex] = this.initLocalPositionArray[fromIndex];
			this.initLocalRotationArray[toIndex] = this.initLocalRotationArray[fromIndex];
			this.positionArray[toIndex] = this.positionArray[fromIndex];
			this.rotationArray[toIndex] = this.rotationArray[fromIndex];
			this.inverseRotationArray[toIndex] = this.inverseRotationArray[fromIndex];
			this.scaleArray[toIndex] = this.scaleArray[fromIndex];
			this.localPositionArray[toIndex] = this.localPositionArray[fromIndex];
			this.localRotationArray[toIndex] = this.localRotationArray[fromIndex];
			this.transformAccessArray[toIndex] = this.transformAccessArray[fromIndex];
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00017E90 File Offset: 0x00016090
		internal void RemoveTransform(DataChunk c)
		{
			if (!this.isValid)
			{
				return;
			}
			if (!c.IsValid)
			{
				return;
			}
			this.flagArray.RemoveAndFill(c, default(ExBitFlag8));
			this.initLocalPositionArray.Remove(c);
			this.initLocalRotationArray.Remove(c);
			this.positionArray.Remove(c);
			this.rotationArray.Remove(c);
			this.inverseRotationArray.Remove(c);
			this.scaleArray.Remove(c);
			this.localPositionArray.Remove(c);
			this.localRotationArray.Remove(c);
			for (int i = 0; i < c.dataLength; i++)
			{
				int index = c.startIndex + i;
				this.transformAccessArray[index] = null;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00017F4C File Offset: 0x0001614C
		internal void EnableTransform(DataChunk c, bool sw)
		{
			if (!this.isValid)
			{
				return;
			}
			if (!c.IsValid)
			{
				return;
			}
			new TransformManager.EnableTransformJob
			{
				chunk = c,
				sw = sw,
				flagList = this.flagArray.GetNativeArray()
			}.Run<TransformManager.EnableTransformJob>();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00017F9C File Offset: 0x0001619C
		internal void EnableTransform(int index, bool sw)
		{
			if (!this.isValid)
			{
				return;
			}
			if (index < 0)
			{
				return;
			}
			ExBitFlag8 exBitFlag = this.flagArray[index];
			if (exBitFlag.Value == 0)
			{
				return;
			}
			exBitFlag.SetFlag(16, sw);
			this.flagArray[index] = exBitFlag;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00017FE4 File Offset: 0x000161E4
		public JobHandle RestoreTransform(JobHandle jobHandle)
		{
			if (this.Count > 0)
			{
				jobHandle = new TransformManager.RestoreTransformJob
				{
					flagList = this.flagArray.GetNativeArray(),
					localPositionArray = this.initLocalPositionArray.GetNativeArray(),
					localRotationArray = this.initLocalRotationArray.GetNativeArray()
				}.Schedule(this.transformAccessArray, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00018048 File Offset: 0x00016248
		public JobHandle ReadTransform(JobHandle jobHandle)
		{
			if (this.Count > 0)
			{
				jobHandle = new TransformManager.ReadTransformJob
				{
					flagList = this.flagArray.GetNativeArray(),
					positionArray = this.positionArray.GetNativeArray(),
					rotationArray = this.rotationArray.GetNativeArray(),
					scaleList = this.scaleArray.GetNativeArray(),
					localPositionArray = this.localPositionArray.GetNativeArray(),
					localRotationArray = this.localRotationArray.GetNativeArray(),
					inverseRotationArray = this.inverseRotationArray.GetNativeArray()
				}.ScheduleReadOnly(this.transformAccessArray, 8, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000180F8 File Offset: 0x000162F8
		public JobHandle WriteTransform(JobHandle jobHandle)
		{
			jobHandle = new TransformManager.WriteTransformJob
			{
				flagList = this.flagArray.GetNativeArray(),
				worldPositions = this.positionArray.GetNativeArray(),
				worldRotations = this.rotationArray.GetNativeArray(),
				localPositions = this.localPositionArray.GetNativeArray(),
				localRotations = this.localRotationArray.GetNativeArray()
			}.Schedule(this.transformAccessArray, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00018178 File Offset: 0x00016378
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Transform Manager. :{0}", this.Count));
			if (this.transformAccessArray.isCreated)
			{
				int length = this.transformAccessArray.length;
				for (int i = 0; i < length; i++)
				{
					Transform transform = this.transformAccessArray[i];
					ExBitFlag8 exBitFlag = this.flagArray[i];
					stringBuilder.Append(string.Format("  [{0}] (", i));
					stringBuilder.Append(exBitFlag.IsSet(16) ? "E" : "");
					stringBuilder.Append(exBitFlag.IsSet(8) ? "R" : "");
					stringBuilder.Append(exBitFlag.IsSet(1) ? "r" : "");
					stringBuilder.Append(exBitFlag.IsSet(2) ? "W" : "");
					stringBuilder.Append(exBitFlag.IsSet(4) ? "w" : "");
					stringBuilder.AppendLine(") " + (((transform != null) ? transform.name : null) ?? "(null)"));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00002058 File Offset: 0x00000258
		public TransformManager()
		{
		}

		// Token: 0x040004AA RID: 1194
		internal const byte Flag_Read = 1;

		// Token: 0x040004AB RID: 1195
		internal const byte Flag_WorldRotWrite = 2;

		// Token: 0x040004AC RID: 1196
		internal const byte Flag_LocalPosRotWrite = 4;

		// Token: 0x040004AD RID: 1197
		internal const byte Flag_Restore = 8;

		// Token: 0x040004AE RID: 1198
		internal const byte Flag_Enable = 16;

		// Token: 0x040004AF RID: 1199
		internal ExNativeArray<ExBitFlag8> flagArray;

		// Token: 0x040004B0 RID: 1200
		internal ExNativeArray<float3> initLocalPositionArray;

		// Token: 0x040004B1 RID: 1201
		internal ExNativeArray<quaternion> initLocalRotationArray;

		// Token: 0x040004B2 RID: 1202
		internal ExNativeArray<float3> positionArray;

		// Token: 0x040004B3 RID: 1203
		internal ExNativeArray<quaternion> rotationArray;

		// Token: 0x040004B4 RID: 1204
		internal ExNativeArray<quaternion> inverseRotationArray;

		// Token: 0x040004B5 RID: 1205
		internal ExNativeArray<float3> scaleArray;

		// Token: 0x040004B6 RID: 1206
		internal ExNativeArray<float3> localPositionArray;

		// Token: 0x040004B7 RID: 1207
		internal ExNativeArray<quaternion> localRotationArray;

		// Token: 0x040004B8 RID: 1208
		internal TransformAccessArray transformAccessArray;

		// Token: 0x040004B9 RID: 1209
		private bool isValid;

		// Token: 0x02000099 RID: 153
		[BurstCompile]
		private struct EnableTransformJob : IJob
		{
			// Token: 0x06000264 RID: 612 RVA: 0x000182C8 File Offset: 0x000164C8
			public void Execute()
			{
				for (int i = 0; i < this.chunk.dataLength; i++)
				{
					int index = this.chunk.startIndex + i;
					ExBitFlag8 exBitFlag = this.flagList[index];
					if (exBitFlag.Value != 0)
					{
						exBitFlag.SetFlag(16, this.sw);
						this.flagList[index] = exBitFlag;
					}
				}
			}

			// Token: 0x040004BA RID: 1210
			public DataChunk chunk;

			// Token: 0x040004BB RID: 1211
			public bool sw;

			// Token: 0x040004BC RID: 1212
			public NativeArray<ExBitFlag8> flagList;
		}

		// Token: 0x0200009A RID: 154
		[BurstCompile]
		private struct RestoreTransformJob : IJobParallelForTransform
		{
			// Token: 0x06000265 RID: 613 RVA: 0x0001832C File Offset: 0x0001652C
			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				if (!this.flagList[index].IsSet(8))
				{
					return;
				}
				transform.localPosition = this.localPositionArray[index];
				transform.localRotation = this.localRotationArray[index];
			}

			// Token: 0x040004BD RID: 1213
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x040004BE RID: 1214
			[ReadOnly]
			public NativeArray<float3> localPositionArray;

			// Token: 0x040004BF RID: 1215
			[ReadOnly]
			public NativeArray<quaternion> localRotationArray;
		}

		// Token: 0x0200009B RID: 155
		[BurstCompile]
		private struct ReadTransformJob : IJobParallelForTransform
		{
			// Token: 0x06000266 RID: 614 RVA: 0x0001838C File Offset: 0x0001658C
			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = this.flagList[index];
				if (!exBitFlag.IsSet(16))
				{
					return;
				}
				if (!exBitFlag.IsSet(1))
				{
					return;
				}
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				float4x4 b = transform.localToWorldMatrix;
				this.positionArray[index] = position;
				this.rotationArray[index] = rotation;
				this.localPositionArray[index] = transform.localPosition;
				this.localRotationArray[index] = transform.localRotation;
				float4x4 float4x = math.mul(new float4x4(math.inverse(rotation), float3.zero), b);
				float3 value = new float3(float4x.c0.x, float4x.c1.y, float4x.c2.z);
				this.scaleList[index] = value;
				this.inverseRotationArray[index] = math.inverse(rotation);
			}

			// Token: 0x040004C0 RID: 1216
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x040004C1 RID: 1217
			[WriteOnly]
			public NativeArray<float3> positionArray;

			// Token: 0x040004C2 RID: 1218
			[WriteOnly]
			public NativeArray<quaternion> rotationArray;

			// Token: 0x040004C3 RID: 1219
			[WriteOnly]
			public NativeArray<float3> scaleList;

			// Token: 0x040004C4 RID: 1220
			[WriteOnly]
			public NativeArray<float3> localPositionArray;

			// Token: 0x040004C5 RID: 1221
			[WriteOnly]
			public NativeArray<quaternion> localRotationArray;

			// Token: 0x040004C6 RID: 1222
			[WriteOnly]
			public NativeArray<quaternion> inverseRotationArray;
		}

		// Token: 0x0200009C RID: 156
		[BurstCompile]
		private struct WriteTransformJob : IJobParallelForTransform
		{
			// Token: 0x06000267 RID: 615 RVA: 0x000184A8 File Offset: 0x000166A8
			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = this.flagList[index];
				if (!exBitFlag.IsSet(16))
				{
					return;
				}
				if (exBitFlag.IsSet(2))
				{
					transform.rotation = this.worldRotations[index];
					return;
				}
				if (exBitFlag.IsSet(4))
				{
					transform.localPosition = this.localPositions[index];
					transform.localRotation = this.localRotations[index];
				}
			}

			// Token: 0x040004C7 RID: 1223
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x040004C8 RID: 1224
			[ReadOnly]
			public NativeArray<float3> worldPositions;

			// Token: 0x040004C9 RID: 1225
			[ReadOnly]
			public NativeArray<quaternion> worldRotations;

			// Token: 0x040004CA RID: 1226
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040004CB RID: 1227
			[ReadOnly]
			public NativeArray<quaternion> localRotations;
		}
	}
}
