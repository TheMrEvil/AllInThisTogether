using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000336 RID: 822
	internal class UIRenderDevice : IDisposable
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000742AD File Offset: 0x000724AD
		internal uint maxVerticesPerPage
		{
			[CompilerGenerated]
			get
			{
				return this.<maxVerticesPerPage>k__BackingField;
			}
		} = 65535;

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000742B5 File Offset: 0x000724B5
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x000742BD File Offset: 0x000724BD
		internal bool breakBatches
		{
			[CompilerGenerated]
			get
			{
				return this.<breakBatches>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<breakBatches>k__BackingField = value;
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000742C8 File Offset: 0x000724C8
		static UIRenderDevice()
		{
			Utility.EngineUpdate += UIRenderDevice.OnEngineUpdateGlobal;
			Utility.FlushPendingResources += UIRenderDevice.OnFlushPendingResources;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000743BE File Offset: 0x000725BE
		public UIRenderDevice(uint initialVertexCapacity = 0U, uint initialIndexCapacity = 0U) : this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000743CC File Offset: 0x000725CC
		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			Debug.Assert(true);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity, 2048U);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				num++;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00074540 File Offset: 0x00072740
		internal static Texture2D defaultShaderInfoTexFloat
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexFloat = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.name = "DefaultShaderInfoTexFloat";
					UIRenderDevice.s_DefaultShaderInfoTexFloat.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexFloat;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00074750 File Offset: 0x00072950
		internal static Texture2D defaultShaderInfoTexARGB8
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexARGB8 == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = new Texture2D(64, 64, TextureFormat.RGBA32, false);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.name = "DefaultShaderInfoTexARGB8";
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexARGB8;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x000748A0 File Offset: 0x00072AA0
		internal static bool vertexTexturingIsAvailable
		{
			get
			{
				bool flag = UIRenderDevice.s_VertexTexturingIsAvailable == null;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_VertexTexturingIsAvailable", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_VertexTexturingIsAvailable = new bool?(tag == "1");
				}
				return UIRenderDevice.s_VertexTexturingIsAvailable.Value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0007491C File Offset: 0x00072B1C
		internal static bool shaderModelIs35
		{
			get
			{
				bool flag = UIRenderDevice.s_ShaderModelIs35 == null;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_ShaderModelIs35", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_ShaderModelIs35 = new bool?(tag == "1");
				}
				return UIRenderDevice.s_ShaderModelIs35.Value;
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00074998 File Offset: 0x00072B98
		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] vertexAttributes = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord5, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord6, VertexAttributeFormat.Float32, 1, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(vertexAttributes);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00074A4C File Offset: 0x00072C4C
		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_StandardMatProps = new MaterialPropertyBlock();
				this.m_DefaultStencilState = Utility.CreateStencilState(new StencilState
				{
					enabled = true,
					readMask = byte.MaxValue,
					writeMask = byte.MaxValue,
					compareFunctionFront = CompareFunction.Equal,
					passOperationFront = StencilOp.Keep,
					failOperationFront = StencilOp.Keep,
					zFailOperationFront = StencilOp.IncrementSaturate,
					compareFunctionBack = CompareFunction.Less,
					passOperationBack = StencilOp.Keep,
					failOperationBack = StencilOp.Keep,
					zFailOperationBack = StencilOp.DecrementSaturate
				});
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00074B14 File Offset: 0x00072D14
		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00074B2F File Offset: 0x00072D2F
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00074B37 File Offset: 0x00072D37
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00074B40 File Offset: 0x00072D40
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00074B52 File Offset: 0x00072D52
		internal void DisposeImmediate()
		{
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			UIRenderDevice.m_SynchronousFree = true;
			this.Dispose();
			UIRenderDevice.m_SynchronousFree = false;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00074B78 File Offset: 0x00072D78
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					UIRenderDevice.DeviceToFree deviceToFree = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0U : Utility.InsertCPUFence()),
						page = this.m_FirstPage
					};
					bool flag = deviceToFree.handle == 0U;
					if (flag)
					{
						deviceToFree.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(deviceToFree);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00074C18 File Offset: 0x00072E18
		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3U;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00074C60 File Offset: 0x00072E60
		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint start = mesh.allocVerts.start;
				NativeSlice<ushort> nativeSlice = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> nativeSlice2;
				ushort num;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out nativeSlice2, out num, out allocToUpdate, false);
				int size = (int)mesh.allocIndices.size;
				int num2 = (int)((uint)num - start);
				for (int i = 0; i < size; i++)
				{
					nativeSlice2[i] = (ushort)((int)nativeSlice[i] + num2);
				}
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00074D5C File Offset: 0x00072F5C
		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
				this.UpdateCopyBackIndices(mesh, true);
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00074E28 File Offset: 0x00073028
		private void UpdateCopyBackIndices(MeshHandle mesh, bool copyBackIndices)
		{
			bool flag = mesh.updateAllocID == 0U;
			if (!flag)
			{
				int index = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.ActiveUpdatesForMeshHandle(mesh);
				UIRenderDevice.AllocToUpdate value = list[index];
				value.copyBackIndices = true;
				list[index] = value;
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00074E70 File Offset: 0x00073070
		internal List<UIRenderDevice.AllocToUpdate> ActiveUpdatesForMeshHandle(MeshHandle mesh)
		{
			return this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00074EA0 File Offset: 0x000730A0
		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0U;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0U;
				if (flag2)
				{
					return true;
				}
				page.vertices.allocator.Free(va);
				va.size = 0U;
			}
			return false;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00074F2C File Offset: 0x0007312C
		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			UIRenderDevice.s_MarkerAllocate.Begin();
			Page page = null;
			Alloc alloc = default(Alloc);
			Alloc alloc2 = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					for (;;)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref alloc, ref alloc2, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = alloc2.size == 0U;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2U);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, this.maxVerticesPerPage);
					uint num = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					num = Math.Max(num, indexCount * 2U);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, num, 4U, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
					alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(alloc.size > 0U);
					Debug.Assert(alloc2.size > 0U);
				}
			}
			else
			{
				this.CompleteCreation();
				Page page2 = this.m_FirstPage;
				Page page3 = this.m_FirstPage;
				int num2 = int.MaxValue;
				while (page2 != null)
				{
					int num3 = page2.vertices.cpuData.Length - (int)vertexCount;
					int num4 = page2.indices.cpuData.Length - (int)indexCount;
					bool flag5 = page2.isEmpty && num3 >= 0 && num4 >= 0 && num3 < num2;
					if (flag5)
					{
						page = page2;
						num2 = num3;
					}
					page3 = page2;
					page2 = page2.next;
				}
				bool flag6 = page == null;
				if (flag6)
				{
					uint vertexMaxCount = (vertexCount > this.maxVerticesPerPage) ? 2U : vertexCount;
					Debug.Assert(vertexCount <= this.maxVerticesPerPage, "Requested Vertex count is above the limit. Alloc will fail.");
					page = new Page(vertexMaxCount, indexCount, 4U, this.m_MockDevice);
					bool flag7 = page3 != null;
					if (flag7)
					{
						page3.next = page;
					}
					else
					{
						this.m_FirstPage = page;
					}
				}
				alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
				alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
			}
			Debug.Assert(alloc.size == vertexCount, "Vertices allocated != Vertices requested");
			Debug.Assert(alloc2.size == indexCount, "Indices allocated != Indices requested");
			bool flag8 = alloc.size != vertexCount || alloc2.size != indexCount;
			if (flag8)
			{
				bool flag9 = alloc.handle != null;
				if (flag9)
				{
					page.vertices.allocator.Free(alloc);
				}
				bool flag10 = alloc2.handle != null;
				if (flag10)
				{
					page.vertices.allocator.Free(alloc2);
				}
				alloc2 = default(Alloc);
				alloc = default(Alloc);
			}
			page.vertices.RegisterUpdate(alloc.start, alloc.size);
			page.indices.RegisterUpdate(alloc2.start, alloc2.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)alloc.start, (int)alloc.size);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)alloc2.start, (int)alloc2.size);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = alloc;
			meshHandle.allocIndices = alloc2;
			meshHandle.allocTime = this.m_FrameIndex;
			UIRenderDevice.s_MarkerAllocate.End();
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0007530C File Offset: 0x0007350C
		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1U;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0U);
			bool flag = mesh.updateAllocID == 0U;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int index = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate3 = list[index];
				Debug.Assert(allocToUpdate3.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= allocToUpdate3.copyBackIndices;
				allocToUpdate.permAllocVerts = allocToUpdate3.permAllocVerts;
				allocToUpdate.permAllocIndices = allocToUpdate3.permAllocIndices;
				allocToUpdate.permPage = allocToUpdate3.permPage;
				allocToUpdate3.allocTime = uint.MaxValue;
				list[index] = allocToUpdate3;
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3U;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000755FC File Offset: 0x000737FC
		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0U;
			if (flag)
			{
				int index = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate = list[index];
				Debug.Assert(allocToUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocVerts,
					page = allocToUpdate.permPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocIndices,
					page = allocToUpdate.permPage,
					vertices = false
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				allocToUpdate.allocTime = uint.MaxValue;
				list[index] = allocToUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int index2 = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[index2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[index2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0U;
			this.m_MeshHandles.Return(mesh);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00075884 File Offset: 0x00073A84
		private static Vector4 GetClipSpaceParams()
		{
			RectInt activeViewport = Utility.GetActiveViewport();
			return new Vector4((float)activeViewport.width * 0.5f, (float)activeViewport.height * 0.5f, 2f / (float)activeViewport.width, 2f / (float)activeViewport.height);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000758DC File Offset: 0x00073ADC
		public void OnFrameRenderingBegin()
		{
			this.AdvanceFrame();
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			UIRenderDevice.s_MarkerBeforeDraw.Begin();
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
			UIRenderDevice.s_MarkerBeforeDraw.End();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00075958 File Offset: 0x00073B58
		private unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00075978 File Offset: 0x00073B78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ApplyDrawCommandState(RenderChainCommand cmd, int textureSlot, Material newMat, bool newMatDiffers, bool newFontDiffers, ref UIRenderDevice.EvaluationState st)
		{
			if (newMatDiffers)
			{
				st.curState.material = newMat;
				st.mustApplyMaterial = true;
			}
			st.curPage = cmd.mesh.allocPage;
			bool flag = cmd.state.texture != TextureId.invalid;
			if (flag)
			{
				bool flag2 = textureSlot < 0;
				if (flag2)
				{
					textureSlot = this.m_TextureSlotManager.FindOldestSlot();
					this.m_TextureSlotManager.Bind(cmd.state.texture, textureSlot, st.stateMatProps);
					st.mustApplyStateBlock = true;
				}
				else
				{
					this.m_TextureSlotManager.MarkUsed(textureSlot);
				}
			}
			if (newFontDiffers)
			{
				st.mustApplyStateBlock = true;
				st.curState.font = cmd.state.font;
				st.stateMatProps.SetTexture(UIRenderDevice.s_FontTexPropID, cmd.state.font);
				st.curState.fontTexSDFScale = cmd.state.fontTexSDFScale;
				st.stateMatProps.SetFloat(UIRenderDevice.s_FontTexSDFScaleID, st.curState.fontTexSDFScale);
			}
			bool flag3 = cmd.state.stencilRef != st.curState.stencilRef;
			if (flag3)
			{
				st.curState.stencilRef = cmd.state.stencilRef;
				st.mustApplyStencil = true;
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00075ADC File Offset: 0x00073CDC
		private void ApplyBatchState(ref UIRenderDevice.EvaluationState st, bool allowMaterialChange)
		{
			bool flag = !this.m_MockDevice;
			if (flag)
			{
				bool mustApplyMaterial = st.mustApplyMaterial;
				if (mustApplyMaterial)
				{
					bool flag2 = !allowMaterialChange;
					if (flag2)
					{
						Debug.LogError("Attempted to change material when it is not allowed to do so.");
						return;
					}
					this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
					st.curState.material.SetPass(0);
					bool flag3 = this.m_StandardMatProps != null;
					if (flag3)
					{
						Utility.SetPropertyBlock(this.m_StandardMatProps);
					}
					st.mustApplyCommonBlock = true;
					st.mustApplyStateBlock = true;
					st.mustApplyStencil = true;
				}
				bool mustApplyStateBlock = st.mustApplyStateBlock;
				if (mustApplyStateBlock)
				{
					Utility.SetPropertyBlock(st.stateMatProps);
				}
				bool mustApplyStencil = st.mustApplyStencil;
				if (mustApplyStencil)
				{
					this.m_DrawStats.stencilRefChanges = this.m_DrawStats.stencilRefChanges + 1U;
					Utility.SetStencilState(this.m_DefaultStencilState, st.curState.stencilRef);
				}
			}
			st.mustApplyMaterial = false;
			st.mustApplyCommonBlock = false;
			st.mustApplyStateBlock = false;
			st.mustApplyStencil = false;
			this.m_TextureSlotManager.StartNewBatch();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00075BE8 File Offset: 0x00073DE8
		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, NativeSlice<Transform3x4> transforms, NativeSlice<Vector4> clipRects, MaterialPropertyBlock stateMatProps, bool allowMaterialChange, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			bool breakBatches = this.breakBatches;
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			drawParams.renderTexture.Add(RenderTexture.active);
			stateMatProps.Clear();
			this.m_TextureSlotManager.Reset();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				bool flag = head != null && head.state.fontTexSDFScale != 0f;
				if (flag)
				{
					this.m_StandardMatProps.SetFloat(UIRenderDevice.s_FontTexSDFScaleID, head.state.fontTexSDFScale);
				}
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = transforms.Length > 0;
				if (flag4)
				{
					Utility.SetVectorArray<Transform3x4>(this.m_StandardMatProps, UIRenderDevice.s_TransformsPropID, transforms);
				}
				bool flag5 = clipRects.Length > 0;
				if (flag5)
				{
					Utility.SetVectorArray<Vector4>(this.m_StandardMatProps, UIRenderDevice.s_ClipRectsPropID, clipRects);
				}
				this.m_StandardMatProps.SetVector(UIRenderDevice.s_ClipSpaceParamsID, UIRenderDevice.GetClipSpaceParams());
				Utility.SetPropertyBlock(this.m_StandardMatProps);
			}
			int num = 1024;
			DrawBufferRange* ptr = stackalloc DrawBufferRange[checked(unchecked((UIntPtr)num) * (UIntPtr)sizeof(DrawBufferRange))];
			int num2 = num - 1;
			int num3 = 0;
			int num4 = 0;
			DrawBufferRange drawBufferRange = default(DrawBufferRange);
			int num5 = -1;
			UIRenderDevice.EvaluationState evaluationState = new UIRenderDevice.EvaluationState
			{
				stateMatProps = stateMatProps,
				defaultMat = defaultMat,
				curState = new State
				{
					material = initialMat
				},
				mustApplyCommonBlock = true,
				mustApplyStateBlock = true,
				mustApplyStencil = true
			};
			while (head != null)
			{
				this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
				this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1U : 0U);
				bool flag6 = drawBufferRange.indexCount > 0 && num4 == num - 1;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				int num6 = -1;
				Material material = null;
				bool newMatDiffers = false;
				bool newFontDiffers = false;
				bool flag10 = head.type == CommandType.Draw;
				if (flag10)
				{
					material = ((head.state.material != null) ? head.state.material : defaultMat);
					bool flag11 = material != evaluationState.curState.material;
					if (flag11)
					{
						flag9 = true;
						newMatDiffers = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag12 = head.mesh.allocPage != evaluationState.curPage;
					if (flag12)
					{
						flag9 = true;
						flag7 = true;
						flag8 = true;
					}
					else
					{
						bool flag13 = (long)num5 != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset));
						if (flag13)
						{
							flag7 = true;
						}
					}
					bool flag14 = head.state.texture != TextureId.invalid;
					if (flag14)
					{
						flag9 = true;
						num6 = this.m_TextureSlotManager.IndexOf(head.state.texture);
						bool flag15 = num6 < 0 && this.m_TextureSlotManager.FreeSlots < 1;
						if (flag15)
						{
							flag7 = true;
							flag8 = true;
						}
					}
					bool flag16 = head.state.font != null && head.state.font != evaluationState.curState.font;
					if (flag16)
					{
						flag9 = true;
						newFontDiffers = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag17 = head.state.stencilRef != evaluationState.curState.stencilRef;
					if (flag17)
					{
						flag9 = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag18 = flag7 && flag6;
					if (flag18)
					{
						flag8 = true;
					}
				}
				else
				{
					flag7 = true;
					flag8 = true;
				}
				bool flag19 = breakBatches;
				if (flag19)
				{
					flag7 = true;
					flag8 = true;
				}
				bool flag20 = flag7;
				if (flag20)
				{
					bool flag21 = drawBufferRange.indexCount > 0;
					if (flag21)
					{
						int num7 = num3 + num4++ & num2;
						ptr[num7] = drawBufferRange;
						Debug.Assert(num4 < num || flag8);
						drawBufferRange = default(DrawBufferRange);
						this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1U;
					}
					bool flag22 = head.type == CommandType.Draw;
					if (flag22)
					{
						drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
						drawBufferRange.indexCount = head.indexCount;
						drawBufferRange.vertsReferenced = (int)head.mesh.allocVerts.size;
						drawBufferRange.minIndexVal = (int)head.mesh.allocVerts.start;
						num5 = drawBufferRange.firstIndex + head.indexCount;
						this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					}
					bool flag23 = flag8;
					if (flag23)
					{
						bool flag24 = num4 > 0;
						if (flag24)
						{
							this.ApplyBatchState(ref evaluationState, allowMaterialChange);
							this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage);
						}
						bool flag25 = head.type > CommandType.Draw;
						if (flag25)
						{
							bool flag26 = !this.m_MockDevice;
							if (flag26)
							{
								head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
							}
							bool flag27 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull || head.type == CommandType.BlitToPreviousRT || head.type == CommandType.PushRenderTexture || head.type == CommandType.PopDefaultMaterial || head.type == CommandType.PushDefaultMaterial;
							if (flag27)
							{
								evaluationState.curState.material = null;
								evaluationState.mustApplyMaterial = false;
								this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1U;
								bool flag28 = head.type == CommandType.PopDefaultMaterial;
								if (flag28)
								{
									int index = drawParams.defaultMaterial.Count - 1;
									defaultMat = drawParams.defaultMaterial[index];
									drawParams.defaultMaterial.RemoveAt(index);
								}
								bool flag29 = head.type == CommandType.PushDefaultMaterial;
								if (flag29)
								{
									drawParams.defaultMaterial.Add(defaultMat);
									defaultMat = head.state.material;
								}
							}
						}
					}
					bool flag30 = head.type == CommandType.Draw && flag9;
					if (flag30)
					{
						this.ApplyDrawCommandState(head, num6, material, newMatDiffers, newFontDiffers, ref evaluationState);
					}
					head = head.next;
				}
				else
				{
					bool flag31 = drawBufferRange.indexCount == 0;
					if (flag31)
					{
						num5 = (drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
					}
					drawBufferRange.indexCount += head.indexCount;
					int minIndexVal = drawBufferRange.minIndexVal;
					int start = (int)head.mesh.allocVerts.start;
					int a = drawBufferRange.minIndexVal + drawBufferRange.vertsReferenced;
					int b = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
					drawBufferRange.minIndexVal = Mathf.Min(minIndexVal, start);
					drawBufferRange.vertsReferenced = Mathf.Max(a, b) - drawBufferRange.minIndexVal;
					num5 += head.indexCount;
					this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					bool flag32 = flag9;
					if (flag32)
					{
						this.ApplyDrawCommandState(head, num6, material, newMatDiffers, newFontDiffers, ref evaluationState);
					}
					head = head.next;
				}
			}
			bool flag33 = drawBufferRange.indexCount > 0;
			if (flag33)
			{
				int num8 = num3 + num4++ & num2;
				ptr[num8] = drawBufferRange;
			}
			bool flag34 = num4 > 0;
			if (flag34)
			{
				this.ApplyBatchState(ref evaluationState, allowMaterialChange);
				this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage);
			}
			this.UpdateFenceValue();
			Utility.ProfileDrawChainEnd();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00076394 File Offset: 0x00074594
		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint num = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* ptr2 = ptr;
					bool flag3;
					do
					{
						uint num2 = *ptr2;
						bool flag2 = num - num2 <= 0U;
						if (flag2)
						{
							break;
						}
						int num3 = Interlocked.CompareExchange(ref *(int*)ptr2, (int)num, (int)num2);
						flag3 = ((long)num3 == (long)((ulong)num2));
					}
					while (!flag3);
				}
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00076414 File Offset: 0x00074614
		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage)
		{
			Debug.Assert(rangesReady > 0);
			bool flag = rangesStart + rangesReady <= rangesCount;
			if (flag)
			{
				bool flag2 = !this.m_MockDevice;
				if (flag2)
				{
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady));
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1U;
			}
			else
			{
				int num = rangesCount - rangesStart;
				int count = rangesReady - num;
				bool flag3 = !this.m_MockDevice;
				if (flag3)
				{
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), num));
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, count));
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2U;
			}
			rangesStart = (rangesStart + rangesReady & rangesCount - 1);
			rangesReady = 0;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00076528 File Offset: 0x00074728
		private unsafe void DrawRanges<I, T>(Utility.GPUBuffer<I> ib, Utility.GPUBuffer<T> vb, NativeSlice<DrawBufferRange> ranges) where I : struct where T : struct
		{
			IntPtr* ptr = stackalloc IntPtr[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr))];
			*ptr = vb.BufferPointer;
			Utility.DrawRanges(ib.BufferPointer, ptr, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00076570 File Offset: 0x00074770
		internal void WaitOnAllCpuFences()
		{
			for (int i = 0; i < this.m_Fences.Length; i++)
			{
				this.WaitOnCpuFence(this.m_Fences[i]);
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000765A4 File Offset: 0x000747A4
		private void WaitOnCpuFence(uint fence)
		{
			bool flag = fence != 0U && !Utility.CPUFencePassed(fence);
			if (flag)
			{
				UIRenderDevice.s_MarkerFence.Begin();
				Utility.WaitForCPUFencePassed(fence);
				UIRenderDevice.s_MarkerFence.End();
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000765E4 File Offset: 0x000747E4
		public void AdvanceFrame()
		{
			UIRenderDevice.s_MarkerAdvanceFrame.Begin();
			this.m_FrameIndex += 1U;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int num = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint fence = this.m_Fences[num];
				this.WaitOnCpuFence(fence);
				this.m_Fences[num] = 0U;
			}
			this.m_NextUpdateID = 1U;
			List<UIRenderDevice.AllocToFree> list = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree allocToFree in list)
			{
				bool vertices = allocToFree.vertices;
				if (vertices)
				{
					allocToFree.page.vertices.allocator.Free(allocToFree.alloc);
				}
				else
				{
					allocToFree.page.indices.allocator.Free(allocToFree.alloc);
				}
			}
			list.Clear();
			List<UIRenderDevice.AllocToUpdate> list2 = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate allocToUpdate in list2)
			{
				bool flag2 = allocToUpdate.meshHandle.updateAllocID == allocToUpdate.id && allocToUpdate.meshHandle.allocTime == allocToUpdate.allocTime;
				if (flag2)
				{
					NativeSlice<Vertex> slice = new NativeSlice<Vertex>(allocToUpdate.meshHandle.allocPage.vertices.cpuData, (int)allocToUpdate.meshHandle.allocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					NativeSlice<Vertex> nativeSlice = new NativeSlice<Vertex>(allocToUpdate.permPage.vertices.cpuData, (int)allocToUpdate.permAllocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					nativeSlice.CopyFrom(slice);
					allocToUpdate.permPage.vertices.RegisterUpdate(allocToUpdate.permAllocVerts.start, allocToUpdate.meshHandle.allocVerts.size);
					bool copyBackIndices = allocToUpdate.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> nativeSlice2 = new NativeSlice<ushort>(allocToUpdate.meshHandle.allocPage.indices.cpuData, (int)allocToUpdate.meshHandle.allocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						NativeSlice<ushort> nativeSlice3 = new NativeSlice<ushort>(allocToUpdate.permPage.indices.cpuData, (int)allocToUpdate.permAllocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						int length = nativeSlice3.Length;
						int num2 = (int)(allocToUpdate.permAllocVerts.start - allocToUpdate.meshHandle.allocVerts.start);
						for (int i = 0; i < length; i++)
						{
							nativeSlice3[i] = (ushort)((int)nativeSlice2[i] + num2);
						}
						allocToUpdate.permPage.indices.RegisterUpdate(allocToUpdate.permAllocIndices.start, allocToUpdate.meshHandle.allocIndices.size);
					}
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocVerts,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = true
					});
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocIndices,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = false
					});
					allocToUpdate.meshHandle.allocVerts = allocToUpdate.permAllocVerts;
					allocToUpdate.meshHandle.allocIndices = allocToUpdate.permAllocIndices;
					allocToUpdate.meshHandle.allocPage = allocToUpdate.permPage;
					allocToUpdate.meshHandle.updateAllocID = 0U;
				}
			}
			list2.Clear();
			this.PruneUnusedPages();
			UIRenderDevice.s_MarkerAdvanceFrame.End();
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00076A58 File Offset: 0x00074C58
		private void PruneUnusedPages()
		{
			Page page4;
			Page page3;
			Page page2;
			Page page = page2 = (page3 = (page4 = null));
			Page next;
			for (Page page5 = this.m_FirstPage; page5 != null; page5 = next)
			{
				bool flag = !page5.isEmpty;
				if (flag)
				{
					page5.framesEmpty = 0;
				}
				else
				{
					page5.framesEmpty++;
				}
				bool flag2 = page5.framesEmpty < 60;
				if (flag2)
				{
					bool flag3 = page2 != null;
					if (flag3)
					{
						page.next = page5;
					}
					else
					{
						page2 = page5;
					}
					page = page5;
				}
				else
				{
					bool flag4 = page3 != null;
					if (flag4)
					{
						page4.next = page5;
					}
					else
					{
						page3 = page5;
					}
					page4 = page5;
				}
				next = page5.next;
				page5.next = null;
			}
			this.m_FirstPage = page2;
			Page next2;
			for (Page page5 = page3; page5 != null; page5 = next2)
			{
				next2 = page5.next;
				page5.next = null;
				page5.Dispose();
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00076B38 File Offset: 0x00074D38
		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
			bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
			if (flag)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
				UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
			}
			bool flag2 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
			if (flag2)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
				UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00076B96 File Offset: 0x00074D96
		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00076BA5 File Offset: 0x00074DA5
		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00076BB4 File Offset: 0x00074DB4
		internal UIRenderDevice.AllocationStatistics GatherAllocationStatistics()
		{
			UIRenderDevice.AllocationStatistics allocationStatistics = default(UIRenderDevice.AllocationStatistics);
			allocationStatistics.completeInit = this.fullyCreated;
			allocationStatistics.freesDeferred = new int[this.m_DeferredFrees.Count];
			for (int i = 0; i < this.m_DeferredFrees.Count; i++)
			{
				allocationStatistics.freesDeferred[i] = this.m_DeferredFrees[i].Count;
			}
			int num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				num++;
			}
			allocationStatistics.pages = new UIRenderDevice.AllocationStatistics.PageStatistics[num];
			num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				allocationStatistics.pages[num].vertices = page.vertices.allocator.GatherStatistics();
				allocationStatistics.pages[num].indices = page.indices.allocator.GatherStatistics();
				num++;
			}
			return allocationStatistics;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00076CC0 File Offset: 0x00074EC0
		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00076CD8 File Offset: 0x00074ED8
		private static void ProcessDeviceFreeQueue()
		{
			UIRenderDevice.s_MarkerFree.Begin();
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> first = UIRenderDevice.m_DeviceFreeQueue.First; first != null; first = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(first.Value.handle);
				if (flag)
				{
					break;
				}
				first.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				bool flag3 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
				if (flag3)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
					UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
				}
				bool flag4 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
				if (flag4)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
				}
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
			UIRenderDevice.s_MarkerFree.End();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00076DEB File Offset: 0x00074FEB
		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00076DF4 File Offset: 0x00074FF4
		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x04000C67 RID: 3175
		internal const uint k_MaxQueuedFrameCount = 4U;

		// Token: 0x04000C68 RID: 3176
		internal const int k_PruneEmptyPageFrameCount = 60;

		// Token: 0x04000C69 RID: 3177
		private readonly bool m_MockDevice;

		// Token: 0x04000C6A RID: 3178
		private IntPtr m_DefaultStencilState;

		// Token: 0x04000C6B RID: 3179
		private IntPtr m_VertexDecl;

		// Token: 0x04000C6C RID: 3180
		private Page m_FirstPage;

		// Token: 0x04000C6D RID: 3181
		private uint m_NextPageVertexCount;

		// Token: 0x04000C6E RID: 3182
		private uint m_LargeMeshVertexCount;

		// Token: 0x04000C6F RID: 3183
		private float m_IndexToVertexCountRatio;

		// Token: 0x04000C70 RID: 3184
		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		// Token: 0x04000C71 RID: 3185
		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		// Token: 0x04000C72 RID: 3186
		private uint[] m_Fences;

		// Token: 0x04000C73 RID: 3187
		private MaterialPropertyBlock m_StandardMatProps;

		// Token: 0x04000C74 RID: 3188
		private uint m_FrameIndex;

		// Token: 0x04000C75 RID: 3189
		private uint m_NextUpdateID = 1U;

		// Token: 0x04000C76 RID: 3190
		private UIRenderDevice.DrawStatistics m_DrawStats;

		// Token: 0x04000C77 RID: 3191
		private readonly LinkedPool<MeshHandle> m_MeshHandles = new LinkedPool<MeshHandle>(() => new MeshHandle(), delegate(MeshHandle mh)
		{
		}, 10000);

		// Token: 0x04000C78 RID: 3192
		private readonly DrawParams m_DrawParams = new DrawParams();

		// Token: 0x04000C79 RID: 3193
		private readonly TextureSlotManager m_TextureSlotManager = new TextureSlotManager();

		// Token: 0x04000C7A RID: 3194
		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();

		// Token: 0x04000C7B RID: 3195
		private static int m_ActiveDeviceCount = 0;

		// Token: 0x04000C7C RID: 3196
		private static bool m_SubscribedToNotifications;

		// Token: 0x04000C7D RID: 3197
		private static bool m_SynchronousFree;

		// Token: 0x04000C7E RID: 3198
		private static readonly int s_FontTexPropID = Shader.PropertyToID("_FontTex");

		// Token: 0x04000C7F RID: 3199
		private static readonly int s_FontTexSDFScaleID = Shader.PropertyToID("_FontTexSDFScale");

		// Token: 0x04000C80 RID: 3200
		private static readonly int s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");

		// Token: 0x04000C81 RID: 3201
		private static readonly int s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");

		// Token: 0x04000C82 RID: 3202
		private static readonly int s_TransformsPropID = Shader.PropertyToID("_Transforms");

		// Token: 0x04000C83 RID: 3203
		private static readonly int s_ClipRectsPropID = Shader.PropertyToID("_ClipRects");

		// Token: 0x04000C84 RID: 3204
		private static readonly int s_ClipSpaceParamsID = Shader.PropertyToID("_ClipSpaceParams");

		// Token: 0x04000C85 RID: 3205
		private static ProfilerMarker s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");

		// Token: 0x04000C86 RID: 3206
		private static ProfilerMarker s_MarkerFree = new ProfilerMarker("UIR.Free");

		// Token: 0x04000C87 RID: 3207
		private static ProfilerMarker s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");

		// Token: 0x04000C88 RID: 3208
		private static ProfilerMarker s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");

		// Token: 0x04000C89 RID: 3209
		private static ProfilerMarker s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");

		// Token: 0x04000C8A RID: 3210
		private static bool? s_VertexTexturingIsAvailable;

		// Token: 0x04000C8B RID: 3211
		private const string k_VertexTexturingIsAvailableTag = "UIE_VertexTexturingIsAvailable";

		// Token: 0x04000C8C RID: 3212
		private const string k_VertexTexturingIsAvailableTrue = "1";

		// Token: 0x04000C8D RID: 3213
		private static bool? s_ShaderModelIs35;

		// Token: 0x04000C8E RID: 3214
		private const string k_ShaderModelIs35Tag = "UIE_ShaderModelIs35";

		// Token: 0x04000C8F RID: 3215
		private const string k_ShaderModelIs35True = "1";

		// Token: 0x04000C90 RID: 3216
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly uint <maxVerticesPerPage>k__BackingField;

		// Token: 0x04000C91 RID: 3217
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <breakBatches>k__BackingField;

		// Token: 0x04000C92 RID: 3218
		private static Texture2D s_DefaultShaderInfoTexFloat;

		// Token: 0x04000C93 RID: 3219
		private static Texture2D s_DefaultShaderInfoTexARGB8;

		// Token: 0x04000C94 RID: 3220
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;

		// Token: 0x02000337 RID: 823
		internal struct AllocToUpdate
		{
			// Token: 0x04000C95 RID: 3221
			public uint id;

			// Token: 0x04000C96 RID: 3222
			public uint allocTime;

			// Token: 0x04000C97 RID: 3223
			public MeshHandle meshHandle;

			// Token: 0x04000C98 RID: 3224
			public Alloc permAllocVerts;

			// Token: 0x04000C99 RID: 3225
			public Alloc permAllocIndices;

			// Token: 0x04000C9A RID: 3226
			public Page permPage;

			// Token: 0x04000C9B RID: 3227
			public bool copyBackIndices;
		}

		// Token: 0x02000338 RID: 824
		private struct AllocToFree
		{
			// Token: 0x04000C9C RID: 3228
			public Alloc alloc;

			// Token: 0x04000C9D RID: 3229
			public Page page;

			// Token: 0x04000C9E RID: 3230
			public bool vertices;
		}

		// Token: 0x02000339 RID: 825
		private struct DeviceToFree
		{
			// Token: 0x06001AD9 RID: 6873 RVA: 0x00076E04 File Offset: 0x00075004
			public void Dispose()
			{
				while (this.page != null)
				{
					Page page = this.page;
					this.page = this.page.next;
					page.Dispose();
				}
			}

			// Token: 0x04000C9F RID: 3231
			public uint handle;

			// Token: 0x04000CA0 RID: 3232
			public Page page;
		}

		// Token: 0x0200033A RID: 826
		private struct EvaluationState
		{
			// Token: 0x04000CA1 RID: 3233
			public MaterialPropertyBlock stateMatProps;

			// Token: 0x04000CA2 RID: 3234
			public Material defaultMat;

			// Token: 0x04000CA3 RID: 3235
			public State curState;

			// Token: 0x04000CA4 RID: 3236
			public Page curPage;

			// Token: 0x04000CA5 RID: 3237
			public bool mustApplyMaterial;

			// Token: 0x04000CA6 RID: 3238
			public bool mustApplyCommonBlock;

			// Token: 0x04000CA7 RID: 3239
			public bool mustApplyStateBlock;

			// Token: 0x04000CA8 RID: 3240
			public bool mustApplyStencil;
		}

		// Token: 0x0200033B RID: 827
		internal struct AllocationStatistics
		{
			// Token: 0x04000CA9 RID: 3241
			public UIRenderDevice.AllocationStatistics.PageStatistics[] pages;

			// Token: 0x04000CAA RID: 3242
			public int[] freesDeferred;

			// Token: 0x04000CAB RID: 3243
			public bool completeInit;

			// Token: 0x0200033C RID: 828
			public struct PageStatistics
			{
				// Token: 0x04000CAC RID: 3244
				internal HeapStatistics vertices;

				// Token: 0x04000CAD RID: 3245
				internal HeapStatistics indices;
			}
		}

		// Token: 0x0200033D RID: 829
		internal struct DrawStatistics
		{
			// Token: 0x04000CAE RID: 3246
			public int currentFrameIndex;

			// Token: 0x04000CAF RID: 3247
			public uint totalIndices;

			// Token: 0x04000CB0 RID: 3248
			public uint commandCount;

			// Token: 0x04000CB1 RID: 3249
			public uint drawCommandCount;

			// Token: 0x04000CB2 RID: 3250
			public uint materialSetCount;

			// Token: 0x04000CB3 RID: 3251
			public uint drawRangeCount;

			// Token: 0x04000CB4 RID: 3252
			public uint drawRangeCallCount;

			// Token: 0x04000CB5 RID: 3253
			public uint immediateDraws;

			// Token: 0x04000CB6 RID: 3254
			public uint stencilRefChanges;
		}

		// Token: 0x0200033E RID: 830
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001ADA RID: 6874 RVA: 0x00076E42 File Offset: 0x00075042
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x00076E4E File Offset: 0x0007504E
			internal MeshHandle <.ctor>b__53_0()
			{
				return new MeshHandle();
			}

			// Token: 0x06001ADD RID: 6877 RVA: 0x00002166 File Offset: 0x00000366
			internal void <.ctor>b__53_1(MeshHandle mh)
			{
			}

			// Token: 0x04000CB7 RID: 3255
			public static readonly UIRenderDevice.<>c <>9 = new UIRenderDevice.<>c();

			// Token: 0x04000CB8 RID: 3256
			public static Func<MeshHandle> <>9__53_0;

			// Token: 0x04000CB9 RID: 3257
			public static Action<MeshHandle> <>9__53_1;
		}
	}
}
