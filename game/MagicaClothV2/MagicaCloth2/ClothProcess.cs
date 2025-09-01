using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000008 RID: 8
	public class ClothProcess : IDisposable, IValid, ITransform
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D4 File Offset: 0x000002D4
		internal void Init()
		{
			if (this.IsState(3))
			{
				return;
			}
			this.SetState(0, false);
			this.result.SetProcess();
			ClothSerializeData serializeData = this.cloth.SerializeData;
			if (!serializeData.IsValid())
			{
				this.result.SetResult(Define.Result.Empty);
				return;
			}
			this.clothType = serializeData.clothType;
			this.reductionSettings = serializeData.reductionSetting;
			this.parameters = serializeData.GetClothParameters();
			this.clothTransformRecord = new TransformRecord(this.cloth.ClothTransform);
			this.normalAdjustmentTransformRecord = new TransformRecord(serializeData.normalAlignmentSetting.adjustmentTransform ? serializeData.normalAlignmentSetting.adjustmentTransform : this.cloth.ClothTransform);
			if (this.clothType == ClothProcess.ClothType.MeshCloth)
			{
				using (List<Renderer>.Enumerator enumerator = serializeData.sourceRenderers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Renderer renderer = enumerator.Current;
						if (renderer)
						{
							int num = this.AddRenderer(renderer);
							if (num == 0)
							{
								this.result.SetError(Define.Result.ClothInit_FailedAddRenderer);
								return;
							}
							RenderData rendererData = MagicaManager.Render.GetRendererData(num);
							if (rendererData.Result.IsFaild())
							{
								this.result.Set(rendererData.Result);
								return;
							}
						}
					}
					goto IL_163;
				}
			}
			if (this.clothType == ClothProcess.ClothType.BoneCloth)
			{
				this.AddBoneCloth(serializeData.rootBones, serializeData.connectionMode);
			}
			IL_163:
			int count = serializeData.customSkinningSetting.skinningBones.Count;
			for (int i = 0; i < count; i++)
			{
				this.customSkinningBoneRecords.Add(new TransformRecord(serializeData.customSkinningSetting.skinningBones[i]));
			}
			this.result.SetSuccess();
			this.SetState(0, true);
			this.SetState(3, true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022B4 File Offset: 0x000004B4
		private int AddRenderer(Renderer ren)
		{
			if (ren == null)
			{
				return 0;
			}
			if (this.renderHandleList == null)
			{
				return 0;
			}
			int num = ren.GetInstanceID();
			if (!this.renderHandleList.Contains(num))
			{
				num = MagicaManager.Render.AddRenderer(ren);
				if (num != 0)
				{
					object obj = this.lockObject;
					lock (obj)
					{
						if (!this.renderHandleList.Contains(num))
						{
							this.renderHandleList.Add(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002344 File Offset: 0x00000544
		internal void RemoveRenderer(int renderHandle)
		{
			if (this.renderHandleList == null)
			{
				return;
			}
			if (this.renderHandleList.Contains(renderHandle))
			{
				object obj = this.lockObject;
				lock (obj)
				{
					MagicaManager.Render.RemoveRenderer(renderHandle);
					this.renderHandleList.Remove(renderHandle);
					int count = this.renderMeshInfoList.Count;
					for (int i = 0; i < count; i++)
					{
						ClothProcess.RenderMeshInfo renderMeshInfo = this.renderMeshInfoList[i];
						if (renderMeshInfo != null && renderMeshInfo.renderHandle == renderHandle)
						{
							VirtualMesh renderMesh = renderMeshInfo.renderMesh;
							if (renderMesh != null)
							{
								renderMesh.Dispose();
							}
							this.renderMeshInfoList[i] = null;
						}
					}
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002408 File Offset: 0x00000608
		private void AddBoneCloth(List<Transform> rootTransforms, RenderSetupData.BoneConnectionMode connectionMode)
		{
			this.boneClothSetupData = new RenderSetupData(this.clothTransformRecord.transform, rootTransforms, connectionMode, this.cloth.name);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002430 File Offset: 0x00000630
		internal void StartUse()
		{
			if (!MagicaManager.IsPlaying())
			{
				return;
			}
			this.SetState(1, true);
			MagicaManager.Team.SetEnable(this.TeamId, true);
			if (this.renderHandleList != null)
			{
				foreach (int handle in this.renderHandleList)
				{
					MagicaManager.Render.StartUse(this, handle);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024B4 File Offset: 0x000006B4
		internal void EndUse()
		{
			if (!MagicaManager.IsPlaying())
			{
				return;
			}
			this.SetState(1, false);
			MagicaManager.Team.SetEnable(this.TeamId, false);
			if (this.renderHandleList != null)
			{
				foreach (int handle in this.renderHandleList)
				{
					MagicaManager.Render.EndUse(this, handle);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002538 File Offset: 0x00000738
		internal void DataUpdate()
		{
			this.cloth.SerializeData.DataValidate();
			if (Application.isPlaying)
			{
				this.SetState(2, true);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000255C File Offset: 0x0000075C
		internal bool StartBuild()
		{
			if (this.IsValid() && this.IsState(3) && !this.IsState(4))
			{
				this.result.SetProcess();
				this.SetState(4, true);
				this.BuildAsync(this.cts.Token);
				return true;
			}
			object obj = "Cloth build failure!: " + this.cloth.name;
			Develop.LogError(obj);
			MagicaCloth cloth = this.cloth;
			if (cloth != null)
			{
				Action<bool> onBuildComplete = cloth.OnBuildComplete;
				if (onBuildComplete != null)
				{
					onBuildComplete(false);
				}
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025E5 File Offset: 0x000007E5
		internal bool AutoBuild()
		{
			return !this.IsState(6) && this.StartBuild();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025F8 File Offset: 0x000007F8
		private Task BuildAsync(CancellationToken ct)
		{
			ClothProcess.<BuildAsync>d__9 <BuildAsync>d__;
			<BuildAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<BuildAsync>d__.<>4__this = this;
			<BuildAsync>d__.ct = ct;
			<BuildAsync>d__.<>1__state = -1;
			<BuildAsync>d__.<>t__builder.Start<ClothProcess.<BuildAsync>d__9>(ref <BuildAsync>d__);
			return <BuildAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002644 File Offset: 0x00000844
		public ResultCode GenerateSelectionDataFromPaintMap(TransformRecord clothTransformRecord, VirtualMesh renderMesh, ClothProcess.PaintMapData paintMapData, out SelectionData selectionData)
		{
			ResultCode resultCode = default(ResultCode);
			resultCode.SetProcess();
			selectionData = new SelectionData();
			try
			{
				if (paintMapData == null)
				{
					resultCode.SetError(Define.Result.CreateCloth_PaintMapCountMismatch);
					throw new MagicaClothProcessingException();
				}
				int vertexCount = renderMesh.VertexCount;
				using (NativeArray<float3> positionList = new NativeArray<float3>(vertexCount, Allocator.TempJob, NativeArrayOptions.ClearMemory))
				{
					using (NativeArray<VertexAttribute> attributeList = new NativeArray<VertexAttribute>(vertexCount, Allocator.TempJob, NativeArrayOptions.ClearMemory))
					{
						float4x4 float4x = clothTransformRecord.worldToLocalMatrix;
						float4x4 toM = MathUtility.Transform(renderMesh.initLocalToWorld, float4x);
						int2 xySize = new int2(paintMapData.paintMapWidth, paintMapData.paintMapHeight);
						using (NativeArray<Color32> attributeMapData = new NativeArray<Color32>(paintMapData.paintData, Allocator.TempJob))
						{
							new ClothProcess.GenerateSelectionJob
							{
								offset = 0,
								positionList = positionList,
								attributeList = attributeList,
								attributeMapWidth = paintMapData.paintMapWidth,
								toM = toM,
								xySize = xySize,
								attributeReadFlag = paintMapData.paintReadFlag,
								attributeMapData = attributeMapData,
								uvs = renderMesh.uv.GetNativeArray(),
								vertexs = renderMesh.localPositions.GetNativeArray()
							}.Run(vertexCount);
							selectionData.positions = positionList.ToArray();
							selectionData.attributes = attributeList.ToArray();
							SelectionData selectionData2 = selectionData;
							float value = renderMesh.maxVertexDistance.Value;
							selectionData2.maxConnectionDistance = MathUtility.TransformDistance(value, toM);
							selectionData.userEdit = true;
							resultCode.SetSuccess();
						}
					}
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (resultCode.IsNone())
				{
					resultCode.SetError(Define.Result.CreateCloth_InvalidPaintMap);
				}
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				resultCode.SetError(Define.Result.CreateCloth_InvalidPaintMap);
			}
			return resultCode;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000287C File Offset: 0x00000A7C
		public ResultCode GeneratePaintMapDataList(List<ClothProcess.PaintMapData> dataList)
		{
			ResultCode resultCode = default(ResultCode);
			resultCode.SetProcess();
			try
			{
				int count = this.cloth.SerializeData.paintMaps.Count;
				ExBitFlag8 paintReadFlag = new ExBitFlag8(3);
				if (this.cloth.SerializeData.paintMode == ClothSerializeData.PaintMode.Texture_Fixed_Move_Limit)
				{
					paintReadFlag.SetFlag(4, true);
				}
				for (int i = 0; i < count; i++)
				{
					Texture2D texture2D = this.cloth.SerializeData.paintMaps[i];
					if (texture2D == null)
					{
						resultCode.SetError(Define.Result.Init_InvalidPaintMap);
						throw new MagicaClothProcessingException();
					}
					if (!texture2D.isReadable)
					{
						resultCode.SetError(Define.Result.Init_PaintMapNotReadable);
						throw new MagicaClothProcessingException();
					}
					int num = texture2D.width;
					int num2 = texture2D.height;
					int num3 = num * num2;
					int num4 = 1;
					while (num4 < texture2D.mipmapCount && num3 > 16384)
					{
						num4++;
						num3 /= 4;
						num /= 2;
						num2 /= 2;
					}
					dataList.Add(new ClothProcess.PaintMapData
					{
						paintData = texture2D.GetPixels32(num4 - 1),
						paintMapWidth = num,
						paintMapHeight = num2,
						paintReadFlag = paintReadFlag
					});
				}
				resultCode.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				resultCode.SetError(Define.Result.Init_InvalidPaintMap);
			}
			return resultCode;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A0C File Offset: 0x00000C0C
		internal int GetColliderIndex(ColliderComponent col)
		{
			return Array.IndexOf<ColliderComponent>(this.colliderArray, col);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002A1A File Offset: 0x00000C1A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002A22 File Offset: 0x00000C22
		public MagicaCloth cloth
		{
			[CompilerGenerated]
			get
			{
				return this.<cloth>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<cloth>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002A2B File Offset: 0x00000C2B
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002A33 File Offset: 0x00000C33
		internal TransformRecord clothTransformRecord
		{
			[CompilerGenerated]
			get
			{
				return this.<clothTransformRecord>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<clothTransformRecord>k__BackingField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002A44 File Offset: 0x00000C44
		internal TransformRecord normalAdjustmentTransformRecord
		{
			[CompilerGenerated]
			get
			{
				return this.<normalAdjustmentTransformRecord>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<normalAdjustmentTransformRecord>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002A4D File Offset: 0x00000C4D
		public ResultCode Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A55 File Offset: 0x00000C55
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002A5D File Offset: 0x00000C5D
		internal ClothProcess.ClothType clothType
		{
			[CompilerGenerated]
			get
			{
				return this.<clothType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<clothType>k__BackingField = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A66 File Offset: 0x00000C66
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002A6E File Offset: 0x00000C6E
		public ClothParameters parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<parameters>k__BackingField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002A77 File Offset: 0x00000C77
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002A7F File Offset: 0x00000C7F
		public VirtualMesh ProxyMesh
		{
			[CompilerGenerated]
			get
			{
				return this.<ProxyMesh>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProxyMesh>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002A88 File Offset: 0x00000C88
		internal int ColliderCount
		{
			get
			{
				ColliderComponent[] array = this.colliderArray;
				if (array == null)
				{
					return 0;
				}
				return array.Count((ColliderComponent x) => x != null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002ABA File Offset: 0x00000CBA
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public int TeamId
		{
			[CompilerGenerated]
			get
			{
				return this.<TeamId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TeamId>k__BackingField = value;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002ACC File Offset: 0x00000CCC
		public BitField32 GetStateFlag()
		{
			object obj = this.lockState;
			BitField32 bitField;
			lock (obj)
			{
				bitField = this.stateFlag;
			}
			return bitField;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B10 File Offset: 0x00000D10
		public bool IsState(int state)
		{
			object obj = this.lockState;
			bool flag2;
			lock (obj)
			{
				flag2 = this.stateFlag.IsSet(state);
			}
			return flag2;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B5C File Offset: 0x00000D5C
		public void SetState(int state, bool sw)
		{
			object obj = this.lockState;
			lock (obj)
			{
				this.stateFlag.SetBits(state, sw);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public bool IsValid()
		{
			return this.IsState(0);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public bool IsEnable
		{
			get
			{
				return this.IsValid() && this.TeamId != 0 && MagicaManager.Team.IsEnable(this.TeamId);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public bool HasProxyMesh
		{
			get
			{
				if (!this.IsValid() || this.TeamId == 0)
				{
					return false;
				}
				VirtualMesh proxyMesh = this.ProxyMesh;
				return proxyMesh != null && proxyMesh.IsSuccess;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BFC File Offset: 0x00000DFC
		public ClothProcess()
		{
			this.result = ResultCode.Empty;
			this.colliderArray = new ColliderComponent[32];
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C70 File Offset: 0x00000E70
		public void Dispose()
		{
			this.SetState(0, false);
			this.result.Clear();
			this.cts.Cancel();
			if (!MagicaManager.IsPlaying())
			{
				return;
			}
			MagicaManager.Simulation.ExitProxyMesh(this);
			MagicaManager.VMesh.ExitProxyMesh(this.TeamId);
			MagicaManager.Collider.Exit(this);
			MagicaManager.Cloth.RemoveCloth(this);
			object obj = this.lockObject;
			lock (obj)
			{
				foreach (ClothProcess.RenderMeshInfo renderMeshInfo in this.renderMeshInfoList)
				{
					if (renderMeshInfo != null)
					{
						VirtualMesh renderMesh = renderMeshInfo.renderMesh;
						if (renderMesh != null)
						{
							renderMesh.Dispose();
						}
					}
				}
				this.renderMeshInfoList.Clear();
				this.renderMeshInfoList = null;
			}
			obj = this.lockObject;
			lock (obj)
			{
				foreach (int handle in this.renderHandleList)
				{
					MagicaManager.Render.RemoveRenderer(handle);
				}
				this.renderHandleList.Clear();
				this.renderHandleList = null;
			}
			RenderSetupData renderSetupData = this.boneClothSetupData;
			if (renderSetupData != null)
			{
				renderSetupData.Dispose();
			}
			this.boneClothSetupData = null;
			VirtualMesh proxyMesh = this.ProxyMesh;
			if (proxyMesh != null)
			{
				proxyMesh.Dispose();
			}
			this.ProxyMesh = null;
			this.colliderArray = null;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E24 File Offset: 0x00001024
		internal void IncrementSuspendCounter()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.suspendCounter++;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E74 File Offset: 0x00001074
		internal void DecrementSuspendCounter()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.suspendCounter--;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002EC4 File Offset: 0x000010C4
		internal int GetSuspendCounter()
		{
			return this.suspendCounter;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002ECE File Offset: 0x000010CE
		public ClothProcess.RenderMeshInfo GetRenderMeshInfo(int index)
		{
			if (index >= 0 && index < this.renderMeshInfoList.Count)
			{
				return this.renderMeshInfoList[index];
			}
			return null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EF0 File Offset: 0x000010F0
		internal void SyncParameters()
		{
			this.parameters = this.cloth.SerializeData.GetClothParameters();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F08 File Offset: 0x00001108
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			this.cloth.SerializeData.GetUsedTransform(transformSet);
			TransformRecord clothTransformRecord = this.clothTransformRecord;
			if (clothTransformRecord != null)
			{
				clothTransformRecord.GetUsedTransform(transformSet);
			}
			RenderSetupData renderSetupData = this.boneClothSetupData;
			if (renderSetupData != null)
			{
				renderSetupData.GetUsedTransform(transformSet);
			}
			this.renderHandleList.ForEach(delegate(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).GetUsedTransform(transformSet);
			});
			this.customSkinningBoneRecords.ForEach(delegate(TransformRecord rd)
			{
				rd.GetUsedTransform(transformSet);
			});
			TransformRecord normalAdjustmentTransformRecord = this.normalAdjustmentTransformRecord;
			if (normalAdjustmentTransformRecord == null)
			{
				return;
			}
			normalAdjustmentTransformRecord.GetUsedTransform(transformSet);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002FAC File Offset: 0x000011AC
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			this.cloth.SerializeData.ReplaceTransform(replaceDict);
			TransformRecord clothTransformRecord = this.clothTransformRecord;
			if (clothTransformRecord != null)
			{
				clothTransformRecord.ReplaceTransform(replaceDict);
			}
			RenderSetupData renderSetupData = this.boneClothSetupData;
			if (renderSetupData != null)
			{
				renderSetupData.ReplaceTransform(replaceDict);
			}
			this.renderHandleList.ForEach(delegate(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).ReplaceTransform(replaceDict);
			});
			this.customSkinningBoneRecords.ForEach(delegate(TransformRecord rd)
			{
				rd.ReplaceTransform(replaceDict);
			});
			TransformRecord normalAdjustmentTransformRecord = this.normalAdjustmentTransformRecord;
			if (normalAdjustmentTransformRecord == null)
			{
				return;
			}
			normalAdjustmentTransformRecord.ReplaceTransform(replaceDict);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000304E File Offset: 0x0000124E
		public string Name
		{
			get
			{
				MagicaCloth cloth = this.cloth;
				return ((cloth != null) ? cloth.name : null) ?? "(none)";
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000306C File Offset: 0x0000126C
		internal bool GenerateInitialization()
		{
			this.result.SetProcess();
			if (!this.cloth.SerializeData.IsValid())
			{
				this.result.SetError(Define.Result.CreateCloth_InvalidSerializeData);
				return false;
			}
			this.cloth.SerializeData.DataValidate();
			this.Init();
			return !this.result.IsError();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030D0 File Offset: 0x000012D0
		internal bool GenerateBoneClothSelection()
		{
			Transform transform = this.cloth.transform;
			RenderSetupData renderSetupData = this.boneClothSetupData;
			int skinBoneCount = renderSetupData.skinBoneCount;
			SelectionData selectionData = new SelectionData(skinBoneCount);
			for (int i = 0; i < skinBoneCount; i++)
			{
				Vector3 v = transform.InverseTransformPoint(renderSetupData.transformPositions[i]);
				selectionData.positions[i] = v;
				selectionData.attributes[i] = VertexAttribute.Move;
			}
			float num = 0f;
			for (int j = 0; j < skinBoneCount; j++)
			{
				int parentTransformIndex = renderSetupData.GetParentTransformIndex(j, true);
				if (parentTransformIndex >= 0)
				{
					float y = math.distance(selectionData.positions[j], selectionData.positions[parentTransformIndex]);
					num = math.max(num, y);
				}
			}
			selectionData.maxConnectionDistance = num;
			foreach (Transform transform2 in this.cloth.SerializeData.rootBones)
			{
				if (transform2)
				{
					int transformIndexFromId = renderSetupData.GetTransformIndexFromId(transform2.GetInstanceID());
					selectionData.attributes[transformIndexFromId] = VertexAttribute.Fixed;
				}
			}
			selectionData.userEdit = true;
			this.cloth.GetSerializeData2().selectionData = selectionData;
			return true;
		}

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private MagicaCloth <cloth>k__BackingField;

		// Token: 0x0400001E RID: 30
		public const int State_Valid = 0;

		// Token: 0x0400001F RID: 31
		public const int State_Enable = 1;

		// Token: 0x04000020 RID: 32
		public const int State_ParameterDirty = 2;

		// Token: 0x04000021 RID: 33
		public const int State_InitComplete = 3;

		// Token: 0x04000022 RID: 34
		public const int State_Build = 4;

		// Token: 0x04000023 RID: 35
		public const int State_Running = 5;

		// Token: 0x04000024 RID: 36
		public const int State_DisableAutoBuild = 6;

		// Token: 0x04000025 RID: 37
		internal BitField32 stateFlag;

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		private TransformRecord <clothTransformRecord>k__BackingField;

		// Token: 0x04000027 RID: 39
		private List<int> renderHandleList = new List<int>();

		// Token: 0x04000028 RID: 40
		internal RenderSetupData boneClothSetupData;

		// Token: 0x04000029 RID: 41
		internal List<ClothProcess.RenderMeshInfo> renderMeshInfoList = new List<ClothProcess.RenderMeshInfo>();

		// Token: 0x0400002A RID: 42
		internal List<TransformRecord> customSkinningBoneRecords = new List<TransformRecord>();

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		private TransformRecord <normalAdjustmentTransformRecord>k__BackingField;

		// Token: 0x0400002C RID: 44
		internal ResultCode result;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private ClothProcess.ClothType <clothType>k__BackingField;

		// Token: 0x0400002E RID: 46
		private ReductionSettings reductionSettings;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		private ClothParameters <parameters>k__BackingField;

		// Token: 0x04000030 RID: 48
		[CompilerGenerated]
		private VirtualMesh <ProxyMesh>k__BackingField;

		// Token: 0x04000031 RID: 49
		internal ColliderComponent[] colliderArray;

		// Token: 0x04000032 RID: 50
		[CompilerGenerated]
		private int <TeamId>k__BackingField;

		// Token: 0x04000033 RID: 51
		internal InertiaConstraint.ConstraintData inertiaConstraintData;

		// Token: 0x04000034 RID: 52
		internal DistanceConstraint.ConstraintData distanceConstraintData;

		// Token: 0x04000035 RID: 53
		internal TriangleBendingConstraint.ConstraintData bendingConstraintData;

		// Token: 0x04000036 RID: 54
		private CancellationTokenSource cts = new CancellationTokenSource();

		// Token: 0x04000037 RID: 55
		private volatile object lockObject = new object();

		// Token: 0x04000038 RID: 56
		private volatile object lockState = new object();

		// Token: 0x04000039 RID: 57
		private volatile int suspendCounter;

		// Token: 0x02000009 RID: 9
		[BurstCompile]
		private struct GenerateSelectionJob : IJobParallelFor
		{
			// Token: 0x06000039 RID: 57 RVA: 0x00003238 File Offset: 0x00001438
			public void Execute(int vindex)
			{
				int2 @int = (int2)((this.uvs[vindex] % 1f + 1f) % 1f * this.xySize);
				Color32 color = this.attributeMapData[@int.y * this.attributeMapWidth + @int.x];
				VertexAttribute value = default(VertexAttribute);
				if (this.attributeReadFlag.IsSet(2) && color.g > 32)
				{
					value.SetFlag(2, true);
				}
				else if (this.attributeReadFlag.IsSet(1) && color.r > 32)
				{
					value.SetFlag(1, true);
				}
				if (this.attributeReadFlag.IsSet(4) && color.b <= 32)
				{
					value.SetFlag(8, true);
				}
				float3 value2 = math.transform(this.toM, this.vertexs[vindex]);
				this.positionList[this.offset + vindex] = value2;
				this.attributeList[this.offset + vindex] = value;
			}

			// Token: 0x0400003A RID: 58
			public int offset;

			// Token: 0x0400003B RID: 59
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> positionList;

			// Token: 0x0400003C RID: 60
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> attributeList;

			// Token: 0x0400003D RID: 61
			public int attributeMapWidth;

			// Token: 0x0400003E RID: 62
			public float4x4 toM;

			// Token: 0x0400003F RID: 63
			public int2 xySize;

			// Token: 0x04000040 RID: 64
			public ExBitFlag8 attributeReadFlag;

			// Token: 0x04000041 RID: 65
			[ReadOnly]
			public NativeArray<Color32> attributeMapData;

			// Token: 0x04000042 RID: 66
			[ReadOnly]
			public NativeArray<float2> uvs;

			// Token: 0x04000043 RID: 67
			[ReadOnly]
			public NativeArray<float3> vertexs;
		}

		// Token: 0x0200000A RID: 10
		public class RenderMeshInfo
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002058 File Offset: 0x00000258
			public RenderMeshInfo()
			{
			}

			// Token: 0x04000044 RID: 68
			public int renderHandle;

			// Token: 0x04000045 RID: 69
			public VirtualMesh renderMesh;

			// Token: 0x04000046 RID: 70
			public DataChunk mappingChunk;
		}

		// Token: 0x0200000B RID: 11
		public class PaintMapData
		{
			// Token: 0x0600003B RID: 59 RVA: 0x00002058 File Offset: 0x00000258
			public PaintMapData()
			{
			}

			// Token: 0x04000047 RID: 71
			public const byte ReadFlag_Fixed = 1;

			// Token: 0x04000048 RID: 72
			public const byte ReadFlag_Move = 2;

			// Token: 0x04000049 RID: 73
			public const byte ReadFlag_Limit = 4;

			// Token: 0x0400004A RID: 74
			public Color32[] paintData;

			// Token: 0x0400004B RID: 75
			public int paintMapWidth;

			// Token: 0x0400004C RID: 76
			public int paintMapHeight;

			// Token: 0x0400004D RID: 77
			public ExBitFlag8 paintReadFlag;
		}

		// Token: 0x0200000C RID: 12
		public enum ClothType
		{
			// Token: 0x0400004F RID: 79
			MeshCloth,
			// Token: 0x04000050 RID: 80
			BoneCloth
		}

		// Token: 0x0200000D RID: 13
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00003354 File Offset: 0x00001554
			internal void <BuildAsync>b__1()
			{
				try
				{
					this.ct.ThrowIfCancellationRequested();
					ClothProcess clothProcess = this.<>4__this;
					VirtualMesh virtualMesh = this.<>4__this.ProxyMesh;
					ClothParameters parameters = this.<>4__this.parameters;
					clothProcess.distanceConstraintData = DistanceConstraint.CreateData(virtualMesh, parameters);
					if (this.<>4__this.distanceConstraintData.result.IsError())
					{
						this.<>4__this.result = this.<>4__this.distanceConstraintData.result;
					}
					this.ct.ThrowIfCancellationRequested();
					ClothProcess clothProcess2 = this.<>4__this;
					VirtualMesh virtualMesh2 = this.<>4__this.ProxyMesh;
					parameters = this.<>4__this.parameters;
					clothProcess2.bendingConstraintData = TriangleBendingConstraint.CreateData(virtualMesh2, parameters);
					if (this.<>4__this.bendingConstraintData != null && this.<>4__this.bendingConstraintData.result.IsError())
					{
						this.<>4__this.result = this.<>4__this.bendingConstraintData.result;
					}
					this.ct.ThrowIfCancellationRequested();
					ClothProcess clothProcess3 = this.<>4__this;
					VirtualMesh virtualMesh3 = this.<>4__this.ProxyMesh;
					parameters = this.<>4__this.parameters;
					clothProcess3.inertiaConstraintData = InertiaConstraint.CreateData(virtualMesh3, parameters);
					if (this.<>4__this.inertiaConstraintData.result.IsError())
					{
						this.<>4__this.result = this.<>4__this.inertiaConstraintData.result;
					}
					if (this.<>4__this.result.IsError())
					{
						throw new MagicaClothProcessingException();
					}
				}
				catch (MagicaClothProcessingException)
				{
					throw;
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
					this.<>4__this.result.SetError(Define.Result.Constraint_Exception);
					throw;
				}
			}

			// Token: 0x04000051 RID: 81
			public CancellationToken ct;

			// Token: 0x04000052 RID: 82
			public VirtualMesh proxyMesh;

			// Token: 0x04000053 RID: 83
			public ClothProcess <>4__this;

			// Token: 0x04000054 RID: 84
			public List<ClothProcess.RenderMeshInfo> renderMeshInfos;
		}

		// Token: 0x0200000E RID: 14
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_1
		{
			// Token: 0x0600003E RID: 62 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass9_1()
			{
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00003510 File Offset: 0x00001710
			internal void <BuildAsync>b__0()
			{
				try
				{
					this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
					this.CS$<>8__locals1.proxyMesh = new VirtualMesh("Proxy");
					this.CS$<>8__locals1.proxyMesh.result.SetProcess();
					List<int> list = null;
					if (this.CS$<>8__locals1.<>4__this.clothType == ClothProcess.ClothType.MeshCloth)
					{
						this.CS$<>8__locals1.proxyMesh.SetTransform(this.CS$<>8__locals1.<>4__this.clothTransformRecord, null);
						object lockObject = this.CS$<>8__locals1.<>4__this.lockObject;
						lock (lockObject)
						{
							list = new List<int>(this.CS$<>8__locals1.<>4__this.renderHandleList);
						}
					}
					SelectionData selectionData = this.usePaintMap ? new SelectionData() : this.sdata2.selectionData;
					bool flag2 = selectionData != null && selectionData.IsValid();
					if (this.CS$<>8__locals1.<>4__this.clothType == ClothProcess.ClothType.MeshCloth)
					{
						if (this.CS$<>8__locals1.<>4__this.renderHandleList.Count == 0)
						{
							this.CS$<>8__locals1.<>4__this.result.SetError(Define.Result.ClothProcess_InvalidRenderHandleList);
							throw new MagicaClothProcessingException();
						}
						for (int i = 0; i < list.Count; i++)
						{
							this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
							int num = list[i];
							RenderData rendererData = MagicaManager.Render.GetRendererData(num);
							VirtualMesh virtualMesh = new VirtualMesh("[" + rendererData.Name + "]");
							virtualMesh.result.SetProcess();
							virtualMesh.ImportFrom(rendererData);
							if (virtualMesh.IsError)
							{
								this.CS$<>8__locals1.<>4__this.result = virtualMesh.result;
								throw new MagicaClothProcessingException();
							}
							SelectionData selectionData2 = selectionData;
							if (this.usePaintMap)
							{
								ResultCode result = this.CS$<>8__locals1.<>4__this.GenerateSelectionDataFromPaintMap(this.CS$<>8__locals1.<>4__this.clothTransformRecord, virtualMesh, this.paintMapDataList[i], out selectionData2);
								if (result.IsError())
								{
									this.CS$<>8__locals1.<>4__this.result = result;
									throw new MagicaClothProcessingException();
								}
								selectionData.Merge(selectionData2);
							}
							flag2 = (selectionData != null && selectionData.IsValid());
							this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
							if (selectionData2 != null && selectionData2.IsValid())
							{
								float mergin = virtualMesh.CalcSelectionMergin(this.CS$<>8__locals1.<>4__this.reductionSettings);
								virtualMesh.SelectionMesh(selectionData2, this.CS$<>8__locals1.<>4__this.clothTransformRecord.localToWorldMatrix, mergin);
								if (virtualMesh.IsError)
								{
									this.CS$<>8__locals1.<>4__this.result = virtualMesh.result;
									throw new MagicaClothProcessingException();
								}
							}
							this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
							virtualMesh.result.SetSuccess();
							this.CS$<>8__locals1.proxyMesh.AddMesh(virtualMesh);
							ClothProcess.RenderMeshInfo renderMeshInfo = new ClothProcess.RenderMeshInfo();
							renderMeshInfo.renderHandle = num;
							renderMeshInfo.renderMesh = virtualMesh;
							this.CS$<>8__locals1.renderMeshInfos.Add(renderMeshInfo);
						}
					}
					else if (this.CS$<>8__locals1.<>4__this.clothType == ClothProcess.ClothType.BoneCloth)
					{
						this.CS$<>8__locals1.proxyMesh.ImportFrom(this.CS$<>8__locals1.<>4__this.boneClothSetupData);
						if (this.CS$<>8__locals1.proxyMesh.IsError)
						{
							this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
							throw new MagicaClothProcessingException();
						}
						if (!flag2)
						{
							selectionData = new SelectionData(this.CS$<>8__locals1.proxyMesh, float4x4.identity);
							if (selectionData.Count > 0)
							{
								selectionData.Fill(VertexAttribute.Move);
								foreach (int id in this.CS$<>8__locals1.<>4__this.boneClothSetupData.rootTransformIdList)
								{
									int transformIndexFromId = this.CS$<>8__locals1.<>4__this.boneClothSetupData.GetTransformIndexFromId(id);
									selectionData.attributes[transformIndexFromId] = VertexAttribute.Fixed;
								}
								flag2 = selectionData.IsValid();
							}
						}
					}
					if (this.CS$<>8__locals1.<>4__this.clothType == ClothProcess.ClothType.MeshCloth && this.CS$<>8__locals1.proxyMesh.VertexCount > 1)
					{
						this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						if (this.CS$<>8__locals1.<>4__this.reductionSettings.IsEnabled)
						{
							this.CS$<>8__locals1.proxyMesh.Reduction(this.CS$<>8__locals1.<>4__this.reductionSettings, this.CS$<>8__locals1.ct);
							if (this.CS$<>8__locals1.proxyMesh.IsError)
							{
								this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
								throw new MagicaClothProcessingException();
							}
						}
					}
					if (!this.CS$<>8__locals1.proxyMesh.joinIndices.IsCreated)
					{
						this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						this.CS$<>8__locals1.proxyMesh.joinIndices = new NativeArray<int>(this.CS$<>8__locals1.proxyMesh.VertexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
						JobUtility.SerialNumberRun(this.CS$<>8__locals1.proxyMesh.joinIndices, this.CS$<>8__locals1.proxyMesh.VertexCount);
					}
					this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
					this.CS$<>8__locals1.proxyMesh.Optimization();
					if (this.CS$<>8__locals1.proxyMesh.IsError)
					{
						this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
						throw new MagicaClothProcessingException();
					}
					if (flag2)
					{
						this.CS$<>8__locals1.proxyMesh.ApplySelectionAttribute(selectionData);
						if (this.CS$<>8__locals1.proxyMesh.IsError)
						{
							this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
							throw new MagicaClothProcessingException();
						}
					}
					this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
					this.CS$<>8__locals1.proxyMesh.ConvertProxyMesh(this.sdata, this.CS$<>8__locals1.<>4__this.clothTransformRecord, this.CS$<>8__locals1.<>4__this.customSkinningBoneRecords, this.CS$<>8__locals1.<>4__this.normalAdjustmentTransformRecord);
					if (this.CS$<>8__locals1.proxyMesh.IsError)
					{
						this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
						throw new MagicaClothProcessingException();
					}
					if (this.CS$<>8__locals1.proxyMesh.VertexCount > 32767)
					{
						this.CS$<>8__locals1.<>4__this.result.SetError(Define.Result.ProxyMesh_Over32767Vertices);
						throw new MagicaClothProcessingException();
					}
					if (this.CS$<>8__locals1.proxyMesh.EdgeCount > 32767)
					{
						this.CS$<>8__locals1.<>4__this.result.SetError(Define.Result.ProxyMesh_Over32767Edges);
						throw new MagicaClothProcessingException();
					}
					if (this.CS$<>8__locals1.proxyMesh.TriangleCount > 32767)
					{
						this.CS$<>8__locals1.<>4__this.result.SetError(Define.Result.ProxyMesh_Over32767Triangles);
						throw new MagicaClothProcessingException();
					}
					this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
					if (this.CS$<>8__locals1.proxyMesh.IsError)
					{
						this.CS$<>8__locals1.<>4__this.result = this.CS$<>8__locals1.proxyMesh.result;
						throw new MagicaClothProcessingException();
					}
					this.CS$<>8__locals1.proxyMesh.result.SetSuccess();
					if (this.CS$<>8__locals1.<>4__this.clothType == ClothProcess.ClothType.MeshCloth)
					{
						foreach (ClothProcess.RenderMeshInfo renderMeshInfo2 in this.CS$<>8__locals1.renderMeshInfos)
						{
							this.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
							VirtualMesh renderMesh = renderMeshInfo2.renderMesh;
							renderMesh.Mapping(this.CS$<>8__locals1.proxyMesh);
							if (renderMesh.IsError)
							{
								this.CS$<>8__locals1.<>4__this.result = renderMesh.result;
								throw new MagicaClothProcessingException();
							}
						}
					}
				}
				catch (MagicaClothProcessingException)
				{
					throw;
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
					this.CS$<>8__locals1.<>4__this.result.SetError(Define.Result.ClothProcess_Exception);
					throw;
				}
			}

			// Token: 0x04000055 RID: 85
			public bool usePaintMap;

			// Token: 0x04000056 RID: 86
			public ClothSerializeData2 sdata2;

			// Token: 0x04000057 RID: 87
			public List<ClothProcess.PaintMapData> paintMapDataList;

			// Token: 0x04000058 RID: 88
			public ClothSerializeData sdata;

			// Token: 0x04000059 RID: 89
			public ClothProcess.<>c__DisplayClass9_0 CS$<>8__locals1;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <BuildAsync>d__9 : IAsyncStateMachine
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00003DD8 File Offset: 0x00001FD8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ClothProcess clothProcess = this.<>4__this;
				try
				{
					if (num > 2)
					{
						this.<>8__2 = new ClothProcess.<>c__DisplayClass9_0();
						this.<>8__2.ct = this.ct;
						this.<>8__2.<>4__this = this.<>4__this;
						clothProcess.result.SetProcess();
						this.<>8__2.renderMeshInfos = new List<ClothProcess.RenderMeshInfo>();
						this.<>8__2.proxyMesh = null;
					}
					try
					{
						TaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_2CB;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_4BD;
						default:
							this.<>8__1 = new ClothProcess.<>c__DisplayClass9_1();
							this.<>8__1.CS$<>8__locals1 = this.<>8__2;
							this.<>8__1.sdata = clothProcess.cloth.SerializeData;
							this.<>8__1.sdata2 = clothProcess.cloth.GetSerializeData2();
							if (clothProcess.cloth.SyncCloth)
							{
								MagicaCloth syncCloth = clothProcess.cloth.SyncCloth;
								while (syncCloth != clothProcess.cloth && syncCloth != null)
								{
									syncCloth.Process.IncrementSuspendCounter();
									syncCloth = syncCloth.SyncCloth;
								}
							}
							this.<>8__1.usePaintMap = false;
							this.<>8__1.paintMapDataList = new List<ClothProcess.PaintMapData>();
							if (this.<>8__1.sdata.clothType == ClothProcess.ClothType.MeshCloth && this.<>8__1.sdata.paintMode != ClothSerializeData.PaintMode.Manual)
							{
								ResultCode result = clothProcess.GeneratePaintMapDataList(this.<>8__1.paintMapDataList);
								if (result.IsError())
								{
									clothProcess.result = result;
									throw new MagicaClothProcessingException();
								}
								if (this.<>8__1.paintMapDataList.Count != clothProcess.renderHandleList.Count)
								{
									clothProcess.result.SetError(Define.Result.CreateCloth_PaintMapCountMismatch);
									throw new MagicaClothProcessingException();
								}
								this.<>8__1.usePaintMap = true;
							}
							awaiter = Task.Run(new Action(this.<>8__1.<BuildAsync>b__0), this.<>8__1.CS$<>8__locals1.ct).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ClothProcess.<BuildAsync>d__9>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						this.<syncCloth>5__2 = clothProcess.cloth.SyncCloth;
						if (this.<syncCloth>5__2 != null)
						{
							this.<timeOutCount>5__3 = 100;
							goto IL_2F9;
						}
						goto IL_33E;
						IL_2CB:
						awaiter.GetResult();
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						int num2 = this.<timeOutCount>5__3;
						this.<timeOutCount>5__3 = num2 - 1;
						IL_2F9:
						object obj;
						if (this.<syncCloth>5__2.Process.IsEnable || this.<timeOutCount>5__3 < 0)
						{
							if (!this.<syncCloth>5__2.Process.IsEnable)
							{
								this.<syncCloth>5__2 = null;
								obj = "Sync timeout! Is there a deadlock between synchronous cloths?";
								Develop.LogWarning(obj);
							}
						}
						else
						{
							awaiter = Task.Delay(20).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ClothProcess.<BuildAsync>d__9>(ref awaiter, ref this);
								return;
							}
							goto IL_2CB;
						}
						IL_33E:
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						if (!clothProcess.IsValid())
						{
							clothProcess.result.SetError(Define.Result.ClothProcess_Invalid);
							throw new MagicaClothProcessingException();
						}
						if (!MagicaManager.IsPlaying())
						{
							clothProcess.result.SetError(Define.Result.ClothProcess_Invalid);
							throw new MagicaClothProcessingException();
						}
						clothProcess.SetState(2, true);
						obj = clothProcess.lockObject;
						bool flag = false;
						try
						{
							Monitor.Enter(obj, ref flag);
							clothProcess.ProxyMesh = this.<>8__1.CS$<>8__locals1.proxyMesh;
							this.<>8__1.CS$<>8__locals1.proxyMesh = null;
							ClothProcess clothProcess2 = clothProcess;
							ClothManager cloth = MagicaManager.Cloth;
							ClothProcess cprocess = clothProcess;
							ClothParameters parameters = clothProcess.parameters;
							clothProcess2.TeamId = cloth.AddCloth(cprocess, parameters);
							MagicaManager.VMesh.RegisterProxyMesh(clothProcess.TeamId, clothProcess.ProxyMesh);
							MagicaManager.Simulation.RegisterProxyMesh(clothProcess);
							MagicaManager.Collider.Register(clothProcess);
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(obj);
							}
						}
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						awaiter = Task.Run(new Action(this.<>8__1.CS$<>8__locals1.<BuildAsync>b__1), this.<>8__1.CS$<>8__locals1.ct).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ClothProcess.<BuildAsync>d__9>(ref awaiter, ref this);
							return;
						}
						IL_4BD:
						awaiter.GetResult();
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						MagicaManager.Simulation.RegisterConstraint(clothProcess);
						obj = clothProcess.lockObject;
						flag = false;
						try
						{
							Monitor.Enter(obj, ref flag);
							List<ClothProcess.RenderMeshInfo>.Enumerator enumerator;
							if (clothProcess.clothType == ClothProcess.ClothType.MeshCloth)
							{
								enumerator = this.<>8__1.CS$<>8__locals1.renderMeshInfos.GetEnumerator();
								try
								{
									while (enumerator.MoveNext())
									{
										ClothProcess.RenderMeshInfo renderMeshInfo = enumerator.Current;
										if (!renderMeshInfo.renderMesh.IsError && renderMeshInfo.renderMesh.IsMapping)
										{
											renderMeshInfo.mappingChunk = MagicaManager.VMesh.RegisterMappingMesh(clothProcess.TeamId, renderMeshInfo.renderMesh);
											renderMeshInfo.renderMesh.result.SetSuccess();
											if (clothProcess.IsState(1))
											{
												MagicaManager.Render.StartUse(clothProcess, renderMeshInfo.renderHandle);
											}
										}
									}
								}
								finally
								{
									if (num < 0)
									{
										((IDisposable)enumerator).Dispose();
									}
								}
							}
							enumerator = this.<>8__1.CS$<>8__locals1.renderMeshInfos.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									ClothProcess.RenderMeshInfo item = enumerator.Current;
									clothProcess.renderMeshInfoList.Add(item);
								}
							}
							finally
							{
								if (num < 0)
								{
									((IDisposable)enumerator).Dispose();
								}
							}
							this.<>8__1.CS$<>8__locals1.renderMeshInfos.Clear();
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(obj);
							}
						}
						this.<>8__1.CS$<>8__locals1.ct.ThrowIfCancellationRequested();
						MagicaManager.Team.SetEnable(clothProcess.TeamId, clothProcess.IsState(1));
						clothProcess.result.SetSuccess();
						clothProcess.SetState(5, true);
						this.<>8__1 = null;
						this.<syncCloth>5__2 = null;
					}
					catch (MagicaClothProcessingException)
					{
						if (!clothProcess.result.IsError())
						{
							clothProcess.result.SetError(Define.Result.ClothProcess_UnknownError);
						}
					}
					catch (OperationCanceledException)
					{
						clothProcess.result.SetCancel();
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
						clothProcess.result.SetError(Define.Result.ClothProcess_Exception);
					}
					finally
					{
						if (num < 0)
						{
							List<ClothProcess.RenderMeshInfo>.Enumerator enumerator = this.<>8__2.renderMeshInfos.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									ClothProcess.RenderMeshInfo renderMeshInfo2 = enumerator.Current;
									if (renderMeshInfo2 != null)
									{
										VirtualMesh renderMesh = renderMeshInfo2.renderMesh;
										if (renderMesh != null)
										{
											renderMesh.Dispose();
										}
									}
								}
							}
							finally
							{
								if (num < 0)
								{
									((IDisposable)enumerator).Dispose();
								}
							}
							VirtualMesh proxyMesh = this.<>8__2.proxyMesh;
							if (proxyMesh != null)
							{
								proxyMesh.Dispose();
							}
							MagicaCloth cloth2 = clothProcess.cloth;
							if ((cloth2 != null) ? cloth2.SyncCloth : null)
							{
								MagicaCloth syncCloth2 = clothProcess.cloth.SyncCloth;
								while (syncCloth2 != clothProcess.cloth && syncCloth2 != null)
								{
									syncCloth2.Process.DecrementSuspendCounter();
									syncCloth2 = syncCloth2.SyncCloth;
								}
							}
							MagicaCloth cloth3 = clothProcess.cloth;
							if (cloth3 != null)
							{
								Action<bool> onBuildComplete = cloth3.OnBuildComplete;
								if (onBuildComplete != null)
								{
									onBuildComplete(clothProcess.result.IsSuccess());
								}
							}
						}
					}
				}
				catch (Exception exception2)
				{
					this.<>1__state = -2;
					this.<>8__2 = null;
					this.<>t__builder.SetException(exception2);
					return;
				}
				this.<>1__state = -2;
				this.<>8__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000041 RID: 65 RVA: 0x000046B4 File Offset: 0x000028B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400005A RID: 90
			public int <>1__state;

			// Token: 0x0400005B RID: 91
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400005C RID: 92
			public CancellationToken ct;

			// Token: 0x0400005D RID: 93
			public ClothProcess <>4__this;

			// Token: 0x0400005E RID: 94
			private ClothProcess.<>c__DisplayClass9_1 <>8__1;

			// Token: 0x0400005F RID: 95
			private ClothProcess.<>c__DisplayClass9_0 <>8__2;

			// Token: 0x04000060 RID: 96
			private MagicaCloth <syncCloth>5__2;

			// Token: 0x04000061 RID: 97
			private TaskAwaiter <>u__1;

			// Token: 0x04000062 RID: 98
			private int <timeOutCount>5__3;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000042 RID: 66 RVA: 0x000046C2 File Offset: 0x000028C2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002058 File Offset: 0x00000258
			public <>c()
			{
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000046CE File Offset: 0x000028CE
			internal bool <get_ColliderCount>b__59_0(ColliderComponent x)
			{
				return x != null;
			}

			// Token: 0x04000063 RID: 99
			public static readonly ClothProcess.<>c <>9 = new ClothProcess.<>c();

			// Token: 0x04000064 RID: 100
			public static Func<ColliderComponent, bool> <>9__59_0;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class <>c__DisplayClass86_0
		{
			// Token: 0x06000045 RID: 69 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass86_0()
			{
			}

			// Token: 0x06000046 RID: 70 RVA: 0x000046D7 File Offset: 0x000028D7
			internal void <GetUsedTransform>b__0(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).GetUsedTransform(this.transformSet);
			}

			// Token: 0x06000047 RID: 71 RVA: 0x000046EF File Offset: 0x000028EF
			internal void <GetUsedTransform>b__1(TransformRecord rd)
			{
				rd.GetUsedTransform(this.transformSet);
			}

			// Token: 0x04000065 RID: 101
			public HashSet<Transform> transformSet;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class <>c__DisplayClass87_0
		{
			// Token: 0x06000048 RID: 72 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass87_0()
			{
			}

			// Token: 0x06000049 RID: 73 RVA: 0x000046FD File Offset: 0x000028FD
			internal void <ReplaceTransform>b__0(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).ReplaceTransform(this.replaceDict);
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00004715 File Offset: 0x00002915
			internal void <ReplaceTransform>b__1(TransformRecord rd)
			{
				rd.ReplaceTransform(this.replaceDict);
			}

			// Token: 0x04000066 RID: 102
			public Dictionary<int, Transform> replaceDict;
		}
	}
}
