using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	// Token: 0x02000094 RID: 148
	public class TransformData : IDisposable
	{
		// Token: 0x06000233 RID: 563 RVA: 0x000160BC File Offset: 0x000142BC
		public TransformData()
		{
			this.Init(100);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000160CC File Offset: 0x000142CC
		public TransformData(int capacity)
		{
			this.Init(capacity);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000160DC File Offset: 0x000142DC
		public void Init(int capacity)
		{
			this.transformList = new List<Transform>(capacity);
			this.idArray = new ExSimpleNativeArray<int>(capacity, true);
			this.parentIdArray = new ExSimpleNativeArray<int>(capacity, true);
			this.flagArray = new ExSimpleNativeArray<ExBitFlag8>(capacity, true);
			this.initLocalPositionArray = new ExSimpleNativeArray<float3>(capacity, true);
			this.initLocalRotationArray = new ExSimpleNativeArray<quaternion>(capacity, true);
			this.positionArray = new ExSimpleNativeArray<float3>(capacity, true);
			this.rotationArray = new ExSimpleNativeArray<quaternion>(capacity, true);
			this.scaleArray = new ExSimpleNativeArray<float3>(capacity, true);
			this.localPositionArray = new ExSimpleNativeArray<float3>(capacity, true);
			this.localRotationArray = new ExSimpleNativeArray<quaternion>(capacity, true);
			this.inverseRotationArray = new ExSimpleNativeArray<quaternion>(capacity, true);
			this.emptyStack = new Queue<int>(capacity);
			this.isDirty = true;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00016198 File Offset: 0x00014398
		public void Dispose()
		{
			this.transformList.Clear();
			ExSimpleNativeArray<int> exSimpleNativeArray = this.idArray;
			if (exSimpleNativeArray != null)
			{
				exSimpleNativeArray.Dispose();
			}
			ExSimpleNativeArray<int> exSimpleNativeArray2 = this.parentIdArray;
			if (exSimpleNativeArray2 != null)
			{
				exSimpleNativeArray2.Dispose();
			}
			ExSimpleNativeArray<ExBitFlag8> exSimpleNativeArray3 = this.flagArray;
			if (exSimpleNativeArray3 != null)
			{
				exSimpleNativeArray3.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray4 = this.initLocalPositionArray;
			if (exSimpleNativeArray4 != null)
			{
				exSimpleNativeArray4.Dispose();
			}
			ExSimpleNativeArray<quaternion> exSimpleNativeArray5 = this.initLocalRotationArray;
			if (exSimpleNativeArray5 != null)
			{
				exSimpleNativeArray5.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray6 = this.positionArray;
			if (exSimpleNativeArray6 != null)
			{
				exSimpleNativeArray6.Dispose();
			}
			ExSimpleNativeArray<quaternion> exSimpleNativeArray7 = this.rotationArray;
			if (exSimpleNativeArray7 != null)
			{
				exSimpleNativeArray7.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray8 = this.scaleArray;
			if (exSimpleNativeArray8 != null)
			{
				exSimpleNativeArray8.Dispose();
			}
			ExSimpleNativeArray<float3> exSimpleNativeArray9 = this.localPositionArray;
			if (exSimpleNativeArray9 != null)
			{
				exSimpleNativeArray9.Dispose();
			}
			ExSimpleNativeArray<quaternion> exSimpleNativeArray10 = this.localRotationArray;
			if (exSimpleNativeArray10 != null)
			{
				exSimpleNativeArray10.Dispose();
			}
			ExSimpleNativeArray<quaternion> exSimpleNativeArray11 = this.inverseRotationArray;
			if (exSimpleNativeArray11 != null)
			{
				exSimpleNativeArray11.Dispose();
			}
			this.emptyStack.Clear();
			if (this.transformAccessArray.isCreated)
			{
				this.transformAccessArray.Dispose();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0001628E File Offset: 0x0001448E
		public int Count
		{
			get
			{
				return this.transformList.Count;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001629B File Offset: 0x0001449B
		public int RootCount
		{
			get
			{
				List<int> list = this.rootIdList;
				if (list == null)
				{
					return 0;
				}
				return list.Count;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000162AE File Offset: 0x000144AE
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000162B8 File Offset: 0x000144B8
		public int AddTransform(Transform t, int tid = 0, int pid = 0, byte flag = 1, bool checkDuplicate = true)
		{
			int num;
			if (checkDuplicate)
			{
				num = this.ReferenceIndexOf<Transform>(this.transformList, t);
				if (num >= 0)
				{
					return num;
				}
			}
			if (this.emptyStack.Count > 0)
			{
				num = this.emptyStack.Dequeue();
				this.transformList[num] = t;
				if (tid == 0)
				{
					this.idArray[num] = t.GetInstanceID();
					ExSimpleNativeArray<int> exSimpleNativeArray = this.parentIdArray;
					int index = num;
					Transform parent = t.parent;
					exSimpleNativeArray[index] = ((parent != null) ? parent.GetInstanceID() : 0);
					this.initLocalPositionArray[num] = t.localPosition;
					this.initLocalRotationArray[num] = t.localRotation;
					this.positionArray[num] = t.position;
					this.rotationArray[num] = t.rotation;
					this.scaleArray[num] = t.lossyScale;
					this.localPositionArray[num] = t.localPosition;
					this.localRotationArray[num] = t.localRotation;
					this.inverseRotationArray[num] = Quaternion.Inverse(t.rotation);
					this.flagArray[num] = new ExBitFlag8(flag);
				}
				else
				{
					this.idArray[num] = tid;
					this.parentIdArray[num] = pid;
					this.initLocalPositionArray[num] = 0;
					this.initLocalRotationArray[num] = quaternion.identity;
					this.positionArray[num] = 0;
					this.rotationArray[num] = quaternion.identity;
					this.scaleArray[num] = 1;
					this.localPositionArray[num] = 0;
					this.localRotationArray[num] = quaternion.identity;
					this.inverseRotationArray[num] = quaternion.identity;
					this.flagArray[num] = new ExBitFlag8(flag);
				}
			}
			else
			{
				num = this.Count;
				this.transformList.Add(t);
				if (tid == 0)
				{
					this.idArray.Add(t.GetInstanceID());
					ExSimpleNativeArray<int> exSimpleNativeArray2 = this.parentIdArray;
					Transform parent2 = t.parent;
					exSimpleNativeArray2.Add((parent2 != null) ? parent2.GetInstanceID() : 0);
					this.initLocalPositionArray.Add(t.localPosition);
					this.initLocalRotationArray.Add(t.localRotation);
					this.positionArray.Add(t.position);
					this.rotationArray.Add(t.rotation);
					this.scaleArray.Add(t.lossyScale);
					this.localPositionArray.Add(t.localPosition);
					this.localRotationArray.Add(t.localRotation);
					this.inverseRotationArray.Add(Quaternion.Inverse(t.rotation));
					this.flagArray.Add(new ExBitFlag8(flag));
				}
				else
				{
					this.idArray.Add(tid);
					this.parentIdArray.Add(pid);
					this.initLocalPositionArray.Add(0);
					this.initLocalRotationArray.Add(quaternion.identity);
					this.positionArray.Add(0);
					this.rotationArray.Add(quaternion.identity);
					this.scaleArray.Add(1);
					this.localPositionArray.Add(0);
					this.localRotationArray.Add(quaternion.identity);
					this.inverseRotationArray.Add(quaternion.identity);
					this.flagArray.Add(new ExBitFlag8(flag));
				}
			}
			this.isDirty = true;
			return num;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0001669C File Offset: 0x0001489C
		public int AddTransform(TransformRecord record, int pid = 0, byte flag = 1, bool checkDuplicate = true)
		{
			int num;
			if (checkDuplicate)
			{
				num = this.ReferenceIndexOf<Transform>(this.transformList, record.transform);
				if (num >= 0)
				{
					return num;
				}
			}
			if (this.emptyStack.Count > 0)
			{
				num = this.emptyStack.Dequeue();
				this.transformList[num] = record.transform;
				this.idArray[num] = record.id;
				this.parentIdArray[num] = pid;
				this.initLocalPositionArray[num] = record.localPosition;
				this.initLocalRotationArray[num] = record.localRotation;
				this.positionArray[num] = record.position;
				this.rotationArray[num] = record.rotation;
				this.scaleArray[num] = record.scale;
				this.localPositionArray[num] = record.localPosition;
				this.localRotationArray[num] = record.localRotation;
				this.inverseRotationArray[num] = Quaternion.Inverse(record.rotation);
				this.flagArray[num] = new ExBitFlag8(flag);
			}
			else
			{
				num = this.Count;
				this.transformList.Add(record.transform);
				this.idArray.Add(record.id);
				this.parentIdArray.Add(pid);
				this.initLocalPositionArray.Add(record.localPosition);
				this.initLocalRotationArray.Add(record.localRotation);
				this.positionArray.Add(record.position);
				this.rotationArray.Add(record.rotation);
				this.scaleArray.Add(record.scale);
				this.localPositionArray.Add(record.localPosition);
				this.localRotationArray.Add(record.localRotation);
				this.inverseRotationArray.Add(Quaternion.Inverse(record.rotation));
				this.flagArray.Add(new ExBitFlag8(flag));
			}
			this.isDirty = true;
			return num;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000168EC File Offset: 0x00014AEC
		public int AddTransform(TransformData srcData, int srcIndex, bool checkDuplicate = true)
		{
			Transform transform = srcData.transformList[srcIndex];
			int num;
			if (checkDuplicate)
			{
				num = this.ReferenceIndexOf<Transform>(this.transformList, transform);
				if (num >= 0)
				{
					return num;
				}
			}
			int num2 = srcData.idArray[srcIndex];
			int num3 = srcData.parentIdArray[srcIndex];
			float3 @float = srcData.initLocalPositionArray[srcIndex];
			quaternion quaternion = srcData.initLocalRotationArray[srcIndex];
			float3 float2 = srcData.positionArray[srcIndex];
			quaternion quaternion2 = srcData.rotationArray[srcIndex];
			float3 float3 = srcData.scaleArray[srcIndex];
			float3 float4 = srcData.localPositionArray[srcIndex];
			quaternion quaternion3 = srcData.localRotationArray[srcIndex];
			quaternion quaternion4 = srcData.inverseRotationArray[srcIndex];
			ExBitFlag8 exBitFlag = srcData.flagArray[srcIndex];
			if (this.emptyStack.Count > 0)
			{
				num = this.emptyStack.Dequeue();
				this.transformList[num] = transform;
				this.idArray[num] = num2;
				this.parentIdArray[num] = num3;
				this.initLocalPositionArray[num] = @float;
				this.initLocalRotationArray[num] = quaternion;
				this.positionArray[num] = float2;
				this.rotationArray[num] = quaternion2;
				this.scaleArray[num] = float3;
				this.localPositionArray[num] = float4;
				this.localRotationArray[num] = quaternion3;
				this.inverseRotationArray[num] = quaternion4;
				this.flagArray[num] = exBitFlag;
			}
			else
			{
				num = this.Count;
				this.transformList.Add(transform);
				this.idArray.Add(num2);
				this.parentIdArray.Add(num3);
				this.initLocalPositionArray.Add(@float);
				this.initLocalRotationArray.Add(quaternion);
				this.positionArray.Add(float2);
				this.rotationArray.Add(quaternion2);
				this.scaleArray.Add(float3);
				this.localPositionArray.Add(float4);
				this.localRotationArray.Add(quaternion3);
				this.inverseRotationArray.Add(quaternion4);
				this.flagArray.Add(exBitFlag);
			}
			this.isDirty = true;
			return num;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00016B24 File Offset: 0x00014D24
		public int[] AddTransformRange(List<Transform> tlist, List<int> idList, List<int> pidList, int copyCount = 0)
		{
			int num = (copyCount > 0) ? copyCount : tlist.Count;
			int count = this.Count;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				this.transformList.Add(tlist[i]);
				this.idArray.Add(idList[i]);
				this.parentIdArray.Add(pidList[i]);
				array[i] = count + i;
			}
			this.flagArray.AddRange(num, new ExBitFlag8(1));
			this.initLocalPositionArray.AddRange(num);
			this.initLocalRotationArray.AddRange(num);
			this.positionArray.AddRange(num);
			this.rotationArray.AddRange(num);
			this.scaleArray.AddRange(num);
			this.localPositionArray.AddRange(num);
			this.localRotationArray.AddRange(num);
			this.inverseRotationArray.AddRange(num);
			this.isDirty = true;
			return array;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00016C11 File Offset: 0x00014E11
		public int[] AddTransformRange(TransformData stdata, int copyCount = 0)
		{
			return this.AddTransformRange(stdata.transformList, new List<int>(stdata.idArray.ToArray()), new List<int>(stdata.parentIdArray.ToArray()), copyCount);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00016C40 File Offset: 0x00014E40
		public int[] AddTransformRange(List<Transform> tlist, List<int> idList, List<int> pidList, List<int> rootIds, NativeArray<float3> localPositions, NativeArray<quaternion> localRotations, NativeArray<float3> positions, NativeArray<quaternion> rotations, NativeArray<float3> scales, NativeArray<quaternion> inverseRotations)
		{
			int count = tlist.Count;
			int count2 = this.Count;
			int[] array = new int[count];
			this.transformList.AddRange(tlist);
			for (int i = 0; i < count; i++)
			{
				this.idArray.Add(idList[i]);
				this.parentIdArray.Add(pidList[i]);
				array[i] = count2 + i;
			}
			if (rootIds != null && rootIds.Count > 0)
			{
				if (this.rootIdList == null)
				{
					this.rootIdList = new List<int>(rootIds);
				}
				else
				{
					this.rootIdList.AddRange(rootIds);
				}
			}
			this.flagArray.AddRange(count, new ExBitFlag8(1));
			this.initLocalPositionArray.AddRange(localPositions);
			this.initLocalRotationArray.AddRange(localRotations);
			this.positionArray.AddRange(positions);
			this.rotationArray.AddRange(rotations);
			this.scaleArray.AddRange(scales);
			this.localPositionArray.AddRange(localPositions);
			this.localRotationArray.AddRange(localRotations);
			this.inverseRotationArray.AddRange(inverseRotations);
			this.isDirty = true;
			return array;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00016D58 File Offset: 0x00014F58
		public void RemoveTransformIndex(int index)
		{
			this.transformList[index] = null;
			this.flagArray[index] = default(ExBitFlag8);
			this.emptyStack.Enqueue(index);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00016D94 File Offset: 0x00014F94
		public int ReplaceTransform(int index, Transform t, int tid = 0, int pid = 0, byte flag = 1)
		{
			this.transformList[index] = t;
			this.flagArray[index] = new ExBitFlag8(flag);
			if (tid == 0)
			{
				this.idArray[index] = t.GetInstanceID();
				ExSimpleNativeArray<int> exSimpleNativeArray = this.parentIdArray;
				Transform parent = t.parent;
				exSimpleNativeArray[index] = ((parent != null) ? parent.GetInstanceID() : 0);
				this.initLocalPositionArray[index] = t.localPosition;
				this.initLocalRotationArray[index] = t.localRotation;
				this.positionArray[index] = t.position;
				this.rotationArray[index] = t.rotation;
				this.scaleArray[index] = t.lossyScale;
				this.localPositionArray[index] = t.localPosition;
				this.localRotationArray[index] = t.localRotation;
			}
			else
			{
				this.idArray[index] = tid;
				this.parentIdArray[index] = pid;
				this.initLocalPositionArray[index] = 0;
				this.initLocalRotationArray[index] = quaternion.identity;
				this.positionArray[index] = 0;
				this.rotationArray[index] = quaternion.identity;
				this.scaleArray[index] = 1;
				this.localPositionArray[index] = 0;
				this.localRotationArray[index] = quaternion.identity;
			}
			this.isDirty = true;
			return index;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00016F3C File Offset: 0x0001513C
		private int ReferenceIndexOf<T>(List<T> list, T item) where T : class
		{
			if (list == null)
			{
				return -1;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00016F76 File Offset: 0x00015176
		public void UpdateWorkData()
		{
			if (this.isDirty)
			{
				if (this.transformAccessArray.isCreated)
				{
					this.transformAccessArray.Dispose();
				}
				this.transformAccessArray = new TransformAccessArray(this.transformList.ToArray(), -1);
				this.isDirty = false;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00016FB8 File Offset: 0x000151B8
		public JobHandle RestoreTransform(int count, JobHandle jobHandle = default(JobHandle))
		{
			this.UpdateWorkData();
			jobHandle = new TransformData.RestoreTransformJob
			{
				count = count,
				flagList = this.flagArray.GetNativeArray(),
				localPositionArray = this.initLocalPositionArray.GetNativeArray(),
				localRotationArray = this.initLocalRotationArray.GetNativeArray()
			}.Schedule(this.transformAccessArray, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00017024 File Offset: 0x00015224
		public JobHandle ReadTransform(JobHandle jobHandle = default(JobHandle))
		{
			this.UpdateWorkData();
			jobHandle = new TransformData.ReadTransformJob
			{
				flagList = this.flagArray.GetNativeArray(),
				positionArray = this.positionArray.GetNativeArray(),
				rotationArray = this.rotationArray.GetNativeArray(),
				scaleList = this.scaleArray.GetNativeArray(),
				localPositionArray = this.localPositionArray.GetNativeArray(),
				localRotationArray = this.localRotationArray.GetNativeArray(),
				inverseRotationArray = this.inverseRotationArray.GetNativeArray()
			}.ScheduleReadOnly(this.transformAccessArray, 16, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000170D0 File Offset: 0x000152D0
		public void ReadTransformRun()
		{
			this.UpdateWorkData();
			new TransformData.ReadTransformJob
			{
				flagList = this.flagArray.GetNativeArray(),
				positionArray = this.positionArray.GetNativeArray(),
				rotationArray = this.rotationArray.GetNativeArray(),
				scaleList = this.scaleArray.GetNativeArray(),
				localPositionArray = this.localPositionArray.GetNativeArray(),
				localRotationArray = this.localRotationArray.GetNativeArray(),
				inverseRotationArray = this.inverseRotationArray.GetNativeArray()
			}.RunReadOnly(this.transformAccessArray);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00017178 File Offset: 0x00015378
		public JobHandle WriteTransform(int count, JobHandle jobHandle = default(JobHandle))
		{
			jobHandle = new TransformData.WriteTransformJob
			{
				count = count,
				flagList = this.flagArray.GetNativeArray(),
				worldPositions = this.positionArray.GetNativeArray(),
				worldRotations = this.rotationArray.GetNativeArray(),
				localPositions = this.localPositionArray.GetNativeArray(),
				localRotations = this.localRotationArray.GetNativeArray()
			}.Schedule(this.transformAccessArray, jobHandle);
			return jobHandle;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00017200 File Offset: 0x00015400
		public void OrganizeReductionTransform(VirtualMesh vmesh, ReductionWorkData workData)
		{
			List<int> list = new List<int>(workData.newSkinBoneCount.Value + 2);
			foreach (KeyValue<int, int> keyValue in workData.useSkinBoneMap)
			{
				list.Add(vmesh.skinBoneTransformIndices[keyValue.Key]);
			}
			int count = list.Count;
			list.Add(vmesh.skinRootIndex);
			int count2 = list.Count;
			list.Add(vmesh.centerTransformIndex);
			int count3 = list.Count;
			List<Transform> list2 = new List<Transform>(count3);
			ExSimpleNativeArray<int> exSimpleNativeArray = new ExSimpleNativeArray<int>(count3, false);
			ExSimpleNativeArray<int> exSimpleNativeArray2 = new ExSimpleNativeArray<int>(count3, false);
			ExSimpleNativeArray<ExBitFlag8> exSimpleNativeArray3 = new ExSimpleNativeArray<ExBitFlag8>(count3, false);
			ExSimpleNativeArray<float3> exSimpleNativeArray4 = new ExSimpleNativeArray<float3>(count3, false);
			ExSimpleNativeArray<quaternion> exSimpleNativeArray5 = new ExSimpleNativeArray<quaternion>(count3, false);
			ExSimpleNativeArray<float3> exSimpleNativeArray6 = new ExSimpleNativeArray<float3>(count3, false);
			ExSimpleNativeArray<quaternion> exSimpleNativeArray7 = new ExSimpleNativeArray<quaternion>(count3, false);
			ExSimpleNativeArray<float3> exSimpleNativeArray8 = new ExSimpleNativeArray<float3>(count3, false);
			for (int i = 0; i < count3; i++)
			{
				int index = list[i];
				list2.Add(this.transformList[index]);
				exSimpleNativeArray[i] = this.idArray[index];
				exSimpleNativeArray2[i] = this.parentIdArray[index];
				exSimpleNativeArray3[i] = this.flagArray[index];
				exSimpleNativeArray4[i] = this.initLocalPositionArray[index];
				exSimpleNativeArray5[i] = this.initLocalRotationArray[index];
				exSimpleNativeArray6[i] = this.positionArray[index];
				exSimpleNativeArray7[i] = this.rotationArray[index];
				exSimpleNativeArray8[i] = this.scaleArray[index];
			}
			this.transformList.Clear();
			this.idArray.Dispose();
			this.parentIdArray.Dispose();
			this.flagArray.Dispose();
			this.initLocalPositionArray.Dispose();
			this.initLocalRotationArray.Dispose();
			this.positionArray.Dispose();
			this.rotationArray.Dispose();
			this.scaleArray.Dispose();
			this.transformList = list2;
			this.idArray = exSimpleNativeArray;
			this.parentIdArray = exSimpleNativeArray2;
			this.flagArray = exSimpleNativeArray3;
			this.initLocalPositionArray = exSimpleNativeArray4;
			this.initLocalRotationArray = exSimpleNativeArray5;
			this.positionArray = exSimpleNativeArray6;
			this.rotationArray = exSimpleNativeArray7;
			this.scaleArray = exSimpleNativeArray8;
			this.emptyStack.Clear();
			vmesh.centerTransformIndex = count2;
			vmesh.skinRootIndex = count;
			this.isDirty = true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000174A8 File Offset: 0x000156A8
		public Transform GetTransformFromIndex(int index)
		{
			return this.transformList[index];
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000174B8 File Offset: 0x000156B8
		public int GetTransformIndexFormId(int id)
		{
			NativeArray<int> nativeArray = this.idArray.GetNativeArray();
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (nativeArray[i] == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000174F2 File Offset: 0x000156F2
		public int GetTransformIdFromIndex(int index)
		{
			return this.idArray[index];
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00017500 File Offset: 0x00015700
		public int GetParentIdFromIndex(int index)
		{
			return this.parentIdArray[index];
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00017510 File Offset: 0x00015710
		public float4x4 GetLocalToWorldMatrix(int index)
		{
			float3 v = this.positionArray[index];
			quaternion q = this.rotationArray[index];
			float3 v2 = this.scaleArray[index];
			return Matrix4x4.TRS(v, q, v2);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001755E File Offset: 0x0001575E
		public float4x4 GetWorldToLocalMatrix(int index)
		{
			return math.inverse(this.GetLocalToWorldMatrix(index));
		}

		// Token: 0x04000489 RID: 1161
		internal List<Transform> transformList;

		// Token: 0x0400048A RID: 1162
		internal ExSimpleNativeArray<ExBitFlag8> flagArray;

		// Token: 0x0400048B RID: 1163
		internal ExSimpleNativeArray<float3> initLocalPositionArray;

		// Token: 0x0400048C RID: 1164
		internal ExSimpleNativeArray<quaternion> initLocalRotationArray;

		// Token: 0x0400048D RID: 1165
		internal ExSimpleNativeArray<float3> positionArray;

		// Token: 0x0400048E RID: 1166
		internal ExSimpleNativeArray<quaternion> rotationArray;

		// Token: 0x0400048F RID: 1167
		internal ExSimpleNativeArray<quaternion> inverseRotationArray;

		// Token: 0x04000490 RID: 1168
		internal ExSimpleNativeArray<float3> scaleArray;

		// Token: 0x04000491 RID: 1169
		internal ExSimpleNativeArray<float3> localPositionArray;

		// Token: 0x04000492 RID: 1170
		internal ExSimpleNativeArray<quaternion> localRotationArray;

		// Token: 0x04000493 RID: 1171
		internal ExSimpleNativeArray<int> idArray;

		// Token: 0x04000494 RID: 1172
		internal ExSimpleNativeArray<int> parentIdArray;

		// Token: 0x04000495 RID: 1173
		internal List<int> rootIdList;

		// Token: 0x04000496 RID: 1174
		private bool isDirty;

		// Token: 0x04000497 RID: 1175
		internal TransformAccessArray transformAccessArray;

		// Token: 0x04000498 RID: 1176
		private Queue<int> emptyStack;

		// Token: 0x02000095 RID: 149
		[BurstCompile]
		private struct RestoreTransformJob : IJobParallelForTransform
		{
			// Token: 0x0600024F RID: 591 RVA: 0x0001756C File Offset: 0x0001576C
			public void Execute(int index, TransformAccess transform)
			{
				if (index >= this.count)
				{
					return;
				}
				if (!transform.isValid)
				{
					return;
				}
				transform.localPosition = this.localPositionArray[index];
				transform.localRotation = this.localRotationArray[index];
			}

			// Token: 0x04000499 RID: 1177
			public int count;

			// Token: 0x0400049A RID: 1178
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x0400049B RID: 1179
			[ReadOnly]
			public NativeArray<float3> localPositionArray;

			// Token: 0x0400049C RID: 1180
			[ReadOnly]
			public NativeArray<quaternion> localRotationArray;
		}

		// Token: 0x02000096 RID: 150
		[BurstCompile]
		private struct ReadTransformJob : IJobParallelForTransform
		{
			// Token: 0x06000250 RID: 592 RVA: 0x000175C0 File Offset: 0x000157C0
			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
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

			// Token: 0x0400049D RID: 1181
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x0400049E RID: 1182
			[WriteOnly]
			public NativeArray<float3> positionArray;

			// Token: 0x0400049F RID: 1183
			[WriteOnly]
			public NativeArray<quaternion> rotationArray;

			// Token: 0x040004A0 RID: 1184
			[WriteOnly]
			public NativeArray<float3> scaleList;

			// Token: 0x040004A1 RID: 1185
			[WriteOnly]
			public NativeArray<float3> localPositionArray;

			// Token: 0x040004A2 RID: 1186
			[WriteOnly]
			public NativeArray<quaternion> localRotationArray;

			// Token: 0x040004A3 RID: 1187
			[WriteOnly]
			public NativeArray<quaternion> inverseRotationArray;
		}

		// Token: 0x02000097 RID: 151
		[BurstCompile]
		private struct WriteTransformJob : IJobParallelForTransform
		{
			// Token: 0x06000251 RID: 593 RVA: 0x000176B4 File Offset: 0x000158B4
			public void Execute(int index, TransformAccess transform)
			{
				if (index >= this.count)
				{
					return;
				}
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = this.flagList[index];
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

			// Token: 0x040004A4 RID: 1188
			public int count;

			// Token: 0x040004A5 RID: 1189
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			// Token: 0x040004A6 RID: 1190
			[ReadOnly]
			public NativeArray<float3> worldPositions;

			// Token: 0x040004A7 RID: 1191
			[ReadOnly]
			public NativeArray<quaternion> worldRotations;

			// Token: 0x040004A8 RID: 1192
			[ReadOnly]
			public NativeArray<float3> localPositions;

			// Token: 0x040004A9 RID: 1193
			[ReadOnly]
			public NativeArray<quaternion> localRotations;
		}
	}
}
