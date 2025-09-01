using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x0200034F RID: 847
	internal class UIRStylePainter : IStylePainter, IDisposable
	{
		// Token: 0x06001B43 RID: 6979 RVA: 0x0007C390 File Offset: 0x0007A590
		private MeshWriteData GetPooledMeshWriteData()
		{
			bool flag = this.m_NextMeshWriteDataPoolItem == this.m_MeshWriteDataPool.Count;
			if (flag)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
			List<MeshWriteData> meshWriteDataPool = this.m_MeshWriteDataPool;
			int nextMeshWriteDataPoolItem = this.m_NextMeshWriteDataPoolItem;
			this.m_NextMeshWriteDataPoolItem = nextMeshWriteDataPoolItem + 1;
			return meshWriteDataPool[nextMeshWriteDataPoolItem];
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0007C3E8 File Offset: 0x0007A5E8
		private MeshWriteData AllocRawVertsIndices(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			this.m_CurrentEntry.vertices = this.m_VertsPool.Alloc(vertexCount);
			this.m_CurrentEntry.indices = this.m_IndicesPool.Alloc(indexCount);
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices);
			return pooledMeshWriteData;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0007C450 File Offset: 0x0007A650
		private MeshWriteData AllocThroughDrawMesh(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			return this.DrawMesh((int)vertexCount, (int)indexCount, allocatorData.texture, allocatorData.material, allocatorData.flags);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0007C47C File Offset: 0x0007A67C
		private MeshWriteData AllocThroughDrawGradients(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			return this.AddGradientsEntry((int)vertexCount, (int)indexCount, allocatorData.svgTexture, allocatorData.material, allocatorData.flags);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0007C4A8 File Offset: 0x0007A6A8
		public UIRStylePainter(RenderChain renderChain)
		{
			this.m_Owner = renderChain;
			this.meshGenerationContext = new MeshGenerationContext(this);
			this.m_Atlas = renderChain.atlas;
			this.m_VectorImageManager = renderChain.vectorImageManager;
			this.m_AllocRawVertsIndicesDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocRawVertsIndices);
			this.m_AllocThroughDrawMeshDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocThroughDrawMesh);
			this.m_AllocThroughDrawGradientsDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocThroughDrawGradients);
			int num = 32;
			this.m_MeshWriteDataPool = new List<MeshWriteData>(num);
			for (int i = 0; i < num; i++)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0007C58B File Offset: 0x0007A78B
		public MeshGenerationContext meshGenerationContext
		{
			[CompilerGenerated]
			get
			{
				return this.<meshGenerationContext>k__BackingField;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0007C593 File Offset: 0x0007A793
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0007C59B File Offset: 0x0007A79B
		public VisualElement currentElement
		{
			[CompilerGenerated]
			get
			{
				return this.<currentElement>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<currentElement>k__BackingField = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0007C5A4 File Offset: 0x0007A7A4
		public List<UIRStylePainter.Entry> entries
		{
			get
			{
				return this.m_Entries;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0007C5BC File Offset: 0x0007A7BC
		public UIRStylePainter.ClosingInfo closingInfo
		{
			get
			{
				return this.m_ClosingInfo;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0007C5D4 File Offset: 0x0007A7D4
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0007C5DC File Offset: 0x0007A7DC
		public int totalVertices
		{
			[CompilerGenerated]
			get
			{
				return this.<totalVertices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<totalVertices>k__BackingField = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0007C5E5 File Offset: 0x0007A7E5
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0007C5ED File Offset: 0x0007A7ED
		public int totalIndices
		{
			[CompilerGenerated]
			get
			{
				return this.<totalIndices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<totalIndices>k__BackingField = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0007C5F6 File Offset: 0x0007A7F6
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0007C5FE File Offset: 0x0007A7FE
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

		// Token: 0x06001B53 RID: 6995 RVA: 0x0007C607 File Offset: 0x0007A807
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0007C61C File Offset: 0x0007A81C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_IndicesPool.Dispose();
					this.m_VertsPool.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0007C660 File Offset: 0x0007A860
		public void Begin(VisualElement ve)
		{
			this.currentElement = ve;
			this.m_NextMeshWriteDataPoolItem = 0;
			this.m_SVGBackgroundEntryIndex = -1;
			this.currentElement.renderChainData.usesLegacyText = (this.currentElement.renderChainData.disableNudging = false);
			this.currentElement.renderChainData.displacementUVStart = (this.currentElement.renderChainData.displacementUVEnd = 0);
			this.m_MaskDepth = 0;
			this.m_StencilRef = 0;
			VisualElement parent = this.currentElement.hierarchy.parent;
			bool flag = parent != null;
			if (flag)
			{
				this.m_MaskDepth = parent.renderChainData.childrenMaskDepth;
				this.m_StencilRef = parent.renderChainData.childrenStencilRef;
			}
			bool flag2 = (this.currentElement.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			bool flag3 = flag2;
			if (flag3)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.owner = this.currentElement;
				renderChainCommand.type = CommandType.PushView;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popViewMatrix = true);
			}
			bool flag4 = parent != null;
			if (flag4)
			{
				this.m_ClipRectID = (flag2 ? UIRVEShaderInfoAllocator.infiniteClipRect : parent.renderChainData.clipRectID);
			}
			else
			{
				this.m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
			}
			bool flag5 = ve.subRenderTargetMode > VisualElement.RenderTargetMode.None;
			if (flag5)
			{
				RenderChainCommand renderChainCommand2 = this.m_Owner.AllocCommand();
				renderChainCommand2.owner = this.currentElement;
				renderChainCommand2.type = CommandType.PushRenderTexture;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand2
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.blitAndPopRenderTexture = true);
				bool flag6 = this.m_MaskDepth > 0 || this.m_StencilRef > 0;
				if (flag6)
				{
					Debug.LogError("The RenderTargetMode feature must not be used within a stencil mask.");
				}
			}
			bool flag7 = ve.defaultMaterial != null;
			if (flag7)
			{
				RenderChainCommand renderChainCommand3 = this.m_Owner.AllocCommand();
				renderChainCommand3.owner = this.currentElement;
				renderChainCommand3.type = CommandType.PushDefaultMaterial;
				renderChainCommand3.state.material = ve.defaultMaterial;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand3
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.PopDefaultMaterial = true);
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0007C8DB File Offset: 0x0007AADB
		public void LandClipUnregisterMeshDrawCommand(RenderChainCommand cmd)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipUnregisterDrawCommand = cmd;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0007C8FB File Offset: 0x0007AAFB
		public void LandClipRegisterMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, int indexOffset)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipperRegisterVertices = vertices;
			this.m_ClosingInfo.clipperRegisterIndices = indices;
			this.m_ClosingInfo.clipperRegisterIndexOffset = indexOffset;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0007C934 File Offset: 0x0007AB34
		public MeshWriteData AddGradientsEntry(int vertexCount, int indexCount, TextureId texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			bool flag = vertexCount == 0 || indexCount == 0;
			MeshWriteData result;
			if (flag)
			{
				pooledMeshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				result = pooledMeshWriteData;
			}
			else
			{
				this.m_CurrentEntry = new UIRStylePainter.Entry
				{
					vertices = this.m_VertsPool.Alloc((uint)vertexCount),
					indices = this.m_IndicesPool.Alloc((uint)indexCount),
					material = material,
					texture = texture,
					clipRectID = this.m_ClipRectID,
					stencilRef = this.m_StencilRef,
					maskDepth = this.m_MaskDepth,
					addFlags = VertexFlags.IsSvgGradients
				};
				Debug.Assert(this.m_CurrentEntry.vertices.Length == vertexCount);
				Debug.Assert(this.m_CurrentEntry.indices.Length == indexCount);
				pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices, new Rect(0f, 0f, 1f, 1f));
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
				result = pooledMeshWriteData;
			}
			return result;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0007CAB8 File Offset: 0x0007ACB8
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			bool flag = vertexCount == 0 || indexCount == 0;
			MeshWriteData result;
			if (flag)
			{
				pooledMeshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				result = pooledMeshWriteData;
			}
			else
			{
				this.m_CurrentEntry = new UIRStylePainter.Entry
				{
					vertices = this.m_VertsPool.Alloc((uint)vertexCount),
					indices = this.m_IndicesPool.Alloc((uint)indexCount),
					material = material,
					uvIsDisplacement = ((flags & MeshGenerationContext.MeshFlags.UVisDisplacement) == MeshGenerationContext.MeshFlags.UVisDisplacement),
					clipRectID = this.m_ClipRectID,
					stencilRef = this.m_StencilRef,
					maskDepth = this.m_MaskDepth,
					addFlags = VertexFlags.IsSolid
				};
				Debug.Assert(this.m_CurrentEntry.vertices.Length == vertexCount);
				Debug.Assert(this.m_CurrentEntry.indices.Length == indexCount);
				Rect uvRegion = new Rect(0f, 0f, 1f, 1f);
				bool flag2 = texture != null;
				if (flag2)
				{
					TextureId textureId;
					RectInt rectInt;
					bool flag3 = (flags & MeshGenerationContext.MeshFlags.SkipDynamicAtlas) != MeshGenerationContext.MeshFlags.SkipDynamicAtlas && this.m_Atlas != null && this.m_Atlas.TryGetAtlas(this.currentElement, texture as Texture2D, out textureId, out rectInt);
					if (flag3)
					{
						this.m_CurrentEntry.addFlags = VertexFlags.IsDynamic;
						uvRegion = new Rect((float)rectInt.x, (float)rectInt.y, (float)rectInt.width, (float)rectInt.height);
						this.m_CurrentEntry.texture = textureId;
						this.m_Owner.AppendTexture(this.currentElement, texture, textureId, true);
					}
					else
					{
						TextureId textureId2 = TextureRegistry.instance.Acquire(texture);
						this.m_CurrentEntry.addFlags = VertexFlags.IsTextured;
						this.m_CurrentEntry.texture = textureId2;
						this.m_Owner.AppendTexture(this.currentElement, texture, textureId2, false);
					}
				}
				pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices, uvRegion);
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
				result = pooledMeshWriteData;
			}
			return result;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0007CD24 File Offset: 0x0007AF24
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = !TextUtilities.IsFontAssigned(textParams);
			if (!flag)
			{
				bool flag2 = handle.IsLegacy();
				if (flag2)
				{
					this.DrawTextNative(textParams, handle, pixelsPerPoint);
				}
				else
				{
					this.DrawTextCore(textParams, handle, pixelsPerPoint);
				}
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0007CD64 File Offset: 0x0007AF64
		internal void DrawTextNative(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			float scaling = TextUtilities.ComputeTextScaling(this.currentElement.worldTransform, pixelsPerPoint);
			using (NativeArray<TextVertex> vertices = ((TextNativeHandle)handle).GetVertices(textParams, scaling))
			{
				bool flag = vertices.Length == 0;
				if (!flag)
				{
					TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, scaling);
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					this.m_CurrentEntry.isTextEntry = true;
					this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
					this.m_CurrentEntry.stencilRef = this.m_StencilRef;
					this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
					MeshBuilder.MakeText(vertices, offset, new MeshBuilder.AllocMeshData
					{
						alloc = this.m_AllocRawVertsIndicesDelegate
					});
					this.m_CurrentEntry.font = textParams.font.material.mainTexture;
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_CurrentEntry = default(UIRStylePainter.Entry);
					this.currentElement.renderChainData.usesLegacyText = true;
					this.currentElement.renderChainData.disableNudging = true;
				}
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0007CEE8 File Offset: 0x0007B0E8
		internal void DrawTextCore(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			TextInfo textInfo = handle.Update(textParams, pixelsPerPoint);
			for (int i = 0; i < textInfo.materialCount; i++)
			{
				bool flag = textInfo.meshInfo[i].vertexCount == 0;
				if (!flag)
				{
					this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
					this.m_CurrentEntry.stencilRef = this.m_StencilRef;
					this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
					bool flag2 = textInfo.meshInfo[i].material.name.Contains("Sprite");
					if (flag2)
					{
						Texture mainTexture = textInfo.meshInfo[i].material.mainTexture;
						TextureId textureId = TextureRegistry.instance.Acquire(mainTexture);
						this.m_CurrentEntry.texture = textureId;
						this.m_Owner.AppendTexture(this.currentElement, mainTexture, textureId, false);
						MeshBuilder.MakeText(textInfo.meshInfo[i], textParams.rect.min, new MeshBuilder.AllocMeshData
						{
							alloc = this.m_AllocRawVertsIndicesDelegate
						}, VertexFlags.IsTextured, false);
					}
					else
					{
						this.m_CurrentEntry.isTextEntry = true;
						this.m_CurrentEntry.fontTexSDFScale = textInfo.meshInfo[i].material.GetFloat(TextShaderUtilities.ID_GradientScale);
						this.m_CurrentEntry.font = textInfo.meshInfo[i].material.mainTexture;
						bool isDynamicColor = RenderEvents.NeedsColorID(this.currentElement);
						MeshBuilder.MakeText(textInfo.meshInfo[i], textParams.rect.min, new MeshBuilder.AllocMeshData
						{
							alloc = this.m_AllocRawVertsIndicesDelegate
						}, VertexFlags.IsText, isDynamicColor);
					}
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_CurrentEntry = default(UIRStylePainter.Entry);
				}
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0007D114 File Offset: 0x0007B314
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			bool flag = rectParams.rect.width < 1E-30f || rectParams.rect.height < 1E-30f;
			if (!flag)
			{
				MeshBuilder.AllocMeshData meshAlloc = new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocThroughDrawMeshDelegate,
					texture = rectParams.texture,
					material = rectParams.material,
					flags = rectParams.meshFlags
				};
				bool flag2 = rectParams.vectorImage != null;
				if (flag2)
				{
					this.DrawVectorImage(rectParams);
				}
				else
				{
					bool flag3 = rectParams.sprite != null;
					if (flag3)
					{
						this.DrawSprite(rectParams);
					}
					else
					{
						bool flag4 = rectParams.texture != null;
						if (flag4)
						{
							MeshBuilder.MakeTexturedRect(rectParams, 0f, meshAlloc, rectParams.colorPage);
						}
						else
						{
							this.ApplyInset(ref rectParams, rectParams.texture);
							MeshBuilder.MakeSolidRect(rectParams, 0f, meshAlloc);
						}
					}
				}
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0007D210 File Offset: 0x0007B410
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
			MeshBuilder.MakeBorder(borderParams, 0f, new MeshBuilder.AllocMeshData
			{
				alloc = this.m_AllocThroughDrawMeshDelegate,
				material = borderParams.material,
				texture = null
			});
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0007D258 File Offset: 0x0007B458
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
			RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
			renderChainCommand.type = (cullingEnabled ? CommandType.ImmediateCull : CommandType.Immediate);
			renderChainCommand.owner = this.currentElement;
			renderChainCommand.callback = callback;
			this.m_Entries.Add(new UIRStylePainter.Entry
			{
				customCommand = renderChainCommand
			});
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
		public VisualElement visualElement
		{
			get
			{
				return this.currentElement;
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0007D2C8 File Offset: 0x0007B4C8
		public unsafe void DrawVisualElementBackground()
		{
			bool flag = this.currentElement.layout.width <= 1E-30f || this.currentElement.layout.height <= 1E-30f;
			if (!flag)
			{
				ComputedStyle computedStyle = *this.currentElement.computedStyle;
				bool flag2 = computedStyle.backgroundColor != Color.clear;
				if (flag2)
				{
					MeshGenerationContextUtils.RectangleParams rectParams = new MeshGenerationContextUtils.RectangleParams
					{
						rect = this.currentElement.rect,
						color = computedStyle.backgroundColor,
						colorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.backgroundColorID),
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectParams.topLeftRadius, out rectParams.bottomLeftRadius, out rectParams.topRightRadius, out rectParams.bottomRightRadius);
					MeshGenerationContextUtils.AdjustBackgroundSizeForBorders(this.currentElement, ref rectParams);
					this.DrawRectangle(rectParams);
				}
				Vector4 vector = new Vector4((float)computedStyle.unitySliceLeft, (float)computedStyle.unitySliceTop, (float)computedStyle.unitySliceRight, (float)computedStyle.unitySliceBottom);
				MeshGenerationContextUtils.RectangleParams rectangleParams = default(MeshGenerationContextUtils.RectangleParams);
				MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectangleParams.topLeftRadius, out rectangleParams.bottomLeftRadius, out rectangleParams.topRightRadius, out rectangleParams.bottomRightRadius);
				Background backgroundImage = computedStyle.backgroundImage;
				bool flag3 = backgroundImage.texture != null || backgroundImage.sprite != null || backgroundImage.vectorImage != null || backgroundImage.renderTexture != null;
				if (flag3)
				{
					MeshGenerationContextUtils.RectangleParams rectParams2 = default(MeshGenerationContextUtils.RectangleParams);
					float num = 1f;
					bool flag4 = backgroundImage.texture != null;
					if (flag4)
					{
						rectParams2 = MeshGenerationContextUtils.RectangleParams.MakeTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.texture, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
					}
					else
					{
						bool flag5 = backgroundImage.sprite != null;
						if (flag5)
						{
							rectParams2 = MeshGenerationContextUtils.RectangleParams.MakeSprite(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.sprite, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType, rectangleParams.HasRadius(Tessellation.kEpsilon), ref vector);
							num *= UIElementsUtility.PixelsPerUnitScaleForElement(this.visualElement, backgroundImage.sprite);
						}
						else
						{
							bool flag6 = backgroundImage.renderTexture != null;
							if (flag6)
							{
								rectParams2 = MeshGenerationContextUtils.RectangleParams.MakeTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.renderTexture, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
							}
							else
							{
								bool flag7 = backgroundImage.vectorImage != null;
								if (flag7)
								{
									rectParams2 = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.vectorImage, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
								}
							}
						}
					}
					rectParams2.topLeftRadius = rectangleParams.topLeftRadius;
					rectParams2.topRightRadius = rectangleParams.topRightRadius;
					rectParams2.bottomRightRadius = rectangleParams.bottomRightRadius;
					rectParams2.bottomLeftRadius = rectangleParams.bottomLeftRadius;
					bool flag8 = vector != Vector4.zero;
					if (flag8)
					{
						rectParams2.leftSlice = Mathf.RoundToInt(vector.x);
						rectParams2.topSlice = Mathf.RoundToInt(vector.y);
						rectParams2.rightSlice = Mathf.RoundToInt(vector.z);
						rectParams2.bottomSlice = Mathf.RoundToInt(vector.w);
					}
					rectParams2.color = computedStyle.unityBackgroundImageTintColor;
					rectParams2.colorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.tintColorID);
					rectParams2.sliceScale = num;
					MeshGenerationContextUtils.AdjustBackgroundSizeForBorders(this.currentElement, ref rectParams2);
					this.DrawRectangle(rectParams2);
				}
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0007D73C File Offset: 0x0007B93C
		public void DrawVisualElementBorder()
		{
			bool flag = this.currentElement.layout.width >= 1E-30f && this.currentElement.layout.height >= 1E-30f;
			if (flag)
			{
				IResolvedStyle resolvedStyle = this.currentElement.resolvedStyle;
				bool flag2 = (resolvedStyle.borderLeftColor != Color.clear && resolvedStyle.borderLeftWidth > 0f) || (resolvedStyle.borderTopColor != Color.clear && resolvedStyle.borderTopWidth > 0f) || (resolvedStyle.borderRightColor != Color.clear && resolvedStyle.borderRightWidth > 0f) || (resolvedStyle.borderBottomColor != Color.clear && resolvedStyle.borderBottomWidth > 0f);
				if (flag2)
				{
					MeshGenerationContextUtils.BorderParams borderParams = new MeshGenerationContextUtils.BorderParams
					{
						rect = this.currentElement.rect,
						leftColor = resolvedStyle.borderLeftColor,
						topColor = resolvedStyle.borderTopColor,
						rightColor = resolvedStyle.borderRightColor,
						bottomColor = resolvedStyle.borderBottomColor,
						leftWidth = resolvedStyle.borderLeftWidth,
						topWidth = resolvedStyle.borderTopWidth,
						rightWidth = resolvedStyle.borderRightWidth,
						bottomWidth = resolvedStyle.borderBottomWidth,
						leftColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderLeftColorID),
						topColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderTopColorID),
						rightColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderRightColorID),
						bottomColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderBottomColorID),
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out borderParams.topLeftRadius, out borderParams.bottomLeftRadius, out borderParams.topRightRadius, out borderParams.bottomRightRadius);
					this.DrawBorder(borderParams);
				}
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007D98C File Offset: 0x0007BB8C
		public void ApplyVisualElementClipping()
		{
			bool flag = this.currentElement.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.type = CommandType.PushScissor;
				renderChainCommand.owner = this.currentElement;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popScissorClip = true);
			}
			else
			{
				bool flag2 = this.currentElement.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					bool flag3 = this.m_MaskDepth > this.m_StencilRef;
					if (flag3)
					{
						this.m_StencilRef++;
						Debug.Assert(this.m_MaskDepth == this.m_StencilRef);
					}
					this.m_ClosingInfo.maskStencilRef = this.m_StencilRef;
					bool flag4 = UIRUtility.IsVectorImageBackground(this.currentElement);
					if (flag4)
					{
						this.GenerateStencilClipEntryForSVGBackground();
					}
					else
					{
						this.GenerateStencilClipEntryForRoundedRectBackground();
					}
					this.m_MaskDepth++;
				}
			}
			this.m_ClipRectID = this.currentElement.renderChainData.clipRectID;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0007DAB8 File Offset: 0x0007BCB8
		private ushort[] AdjustSpriteWinding(Vector2[] vertices, ushort[] indices)
		{
			ushort[] array = new ushort[indices.Length];
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3 b = vertices[(int)indices[i]];
				Vector3 a = vertices[(int)indices[i + 1]];
				Vector3 a2 = vertices[(int)indices[i + 2]];
				Vector3 normalized = (a - b).normalized;
				Vector3 normalized2 = (a2 - b).normalized;
				Vector3 vector = Vector3.Cross(normalized, normalized2);
				bool flag = vector.z >= 0f;
				if (flag)
				{
					array[i] = indices[i + 1];
					array[i + 1] = indices[i];
					array[i + 2] = indices[i + 2];
				}
				else
				{
					array[i] = indices[i];
					array[i + 1] = indices[i + 1];
					array[i + 2] = indices[i + 2];
				}
			}
			return array;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0007DBA8 File Offset: 0x0007BDA8
		public void DrawSprite(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			Sprite sprite = rectParams.sprite;
			bool flag = sprite.texture == null || sprite.triangles.Length == 0;
			if (!flag)
			{
				MeshBuilder.AllocMeshData allocMeshData = new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocThroughDrawMeshDelegate,
					texture = sprite.texture,
					flags = rectParams.meshFlags
				};
				Vector2[] vertices = sprite.vertices;
				ushort[] triangles = sprite.triangles;
				Vector2[] uv = sprite.uv;
				int num = sprite.vertices.Length;
				Vertex[] array = new Vertex[num];
				ushort[] array2 = this.AdjustSpriteWinding(vertices, triangles);
				MeshWriteData meshWriteData = allocMeshData.Allocate((uint)array.Length, (uint)array2.Length);
				Rect uvRegion = meshWriteData.uvRegion;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = vertices[i];
					vector -= rectParams.spriteGeomRect.position;
					vector /= rectParams.spriteGeomRect.size;
					vector.y = 1f - vector.y;
					vector *= rectParams.rect.size;
					vector += rectParams.rect.position;
					Vector2 vector2 = uv[i];
					vector2 *= uvRegion.size;
					vector2 += uvRegion.position;
					array[i] = new Vertex
					{
						position = new Vector3(vector.x, vector.y, Vertex.nearZ),
						tint = rectParams.color,
						uv = vector2
					};
				}
				meshWriteData.SetAllVertices(array);
				meshWriteData.SetAllIndices(array2);
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0007DD80 File Offset: 0x0007BF80
		private void ApplyInset(ref MeshGenerationContextUtils.RectangleParams rectParams, Texture tex)
		{
			Rect rect = rectParams.rect;
			Vector4 rectInset = rectParams.rectInset;
			bool flag = Mathf.Approximately(rect.size.x, 0f) || Mathf.Approximately(rect.size.y, 0f) || rectInset == Vector4.zero;
			if (!flag)
			{
				Rect rect2 = rect;
				rect.x += rectInset.x;
				rect.y += rectInset.y;
				rect.width -= rectInset.x + rectInset.z;
				rect.height -= rectInset.y + rectInset.w;
				rectParams.rect = rect;
				Rect uv = rectParams.uv;
				bool flag2 = tex != null && uv.width > 1E-30f && uv.height > 1E-30f;
				if (flag2)
				{
					Vector2 vector = new Vector2(1f / rect2.width, 1f / rect2.height);
					uv.x += rectInset.x * vector.x;
					uv.y += rectInset.w * vector.y;
					uv.width -= (rectInset.x + rectInset.z) * vector.x;
					uv.height -= (rectInset.y + rectInset.w) * vector.y;
					rectParams.uv = uv;
				}
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0007DF34 File Offset: 0x0007C134
		public void DrawVectorImage(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			VectorImage vectorImage = rectParams.vectorImage;
			Debug.Assert(vectorImage != null);
			int settingIndexOffset = 0;
			MeshBuilder.AllocMeshData allocMeshData = default(MeshBuilder.AllocMeshData);
			bool flag = vectorImage.atlas != null && this.m_VectorImageManager != null;
			if (flag)
			{
				GradientRemap gradientRemap = this.m_VectorImageManager.AddUser(vectorImage, this.currentElement);
				settingIndexOffset = gradientRemap.destIndex;
				bool flag2 = gradientRemap.atlas != TextureId.invalid;
				if (flag2)
				{
					allocMeshData.svgTexture = gradientRemap.atlas;
				}
				else
				{
					allocMeshData.svgTexture = TextureRegistry.instance.Acquire(vectorImage.atlas);
					this.m_Owner.AppendTexture(this.currentElement, vectorImage.atlas, allocMeshData.svgTexture, false);
				}
				allocMeshData.alloc = this.m_AllocThroughDrawGradientsDelegate;
			}
			else
			{
				allocMeshData.alloc = this.m_AllocThroughDrawMeshDelegate;
			}
			int count = this.m_Entries.Count;
			int num;
			int num2;
			MeshBuilder.MakeVectorGraphics(rectParams, settingIndexOffset, allocMeshData, out num, out num2);
			Debug.Assert(count <= this.m_Entries.Count + 1);
			bool flag3 = count != this.m_Entries.Count;
			if (flag3)
			{
				this.m_SVGBackgroundEntryIndex = this.m_Entries.Count - 1;
				bool flag4 = num != 0 && num2 != 0;
				if (flag4)
				{
					UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
					entry.vertices = entry.vertices.Slice(0, num);
					entry.indices = entry.indices.Slice(0, num2);
					this.m_Entries[this.m_SVGBackgroundEntryIndex] = entry;
				}
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
		internal void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.ValidateMeshWriteData();
				this.m_Entries.Clear();
				this.m_VertsPool.SessionDone();
				this.m_IndicesPool.SessionDone();
				this.m_ClosingInfo = default(UIRStylePainter.ClosingInfo);
				this.m_NextMeshWriteDataPoolItem = 0;
				this.currentElement = null;
				this.totalVertices = (this.totalIndices = 0);
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0007E160 File Offset: 0x0007C360
		private void ValidateMeshWriteData()
		{
			for (int i = 0; i < this.m_NextMeshWriteDataPoolItem; i++)
			{
				MeshWriteData meshWriteData = this.m_MeshWriteDataPool[i];
				bool flag = meshWriteData.vertexCount > 0 && meshWriteData.currentVertex < meshWriteData.vertexCount;
				if (flag)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Not enough vertices written in generateVisualContent callback (asked for ",
						meshWriteData.vertexCount.ToString(),
						" but only wrote ",
						meshWriteData.currentVertex.ToString(),
						")"
					}));
					Vertex nextVertex = meshWriteData.m_Vertices[0];
					while (meshWriteData.currentVertex < meshWriteData.vertexCount)
					{
						meshWriteData.SetNextVertex(nextVertex);
					}
				}
				bool flag2 = meshWriteData.indexCount > 0 && meshWriteData.currentIndex < meshWriteData.indexCount;
				if (flag2)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Not enough indices written in generateVisualContent callback (asked for ",
						meshWriteData.indexCount.ToString(),
						" but only wrote ",
						meshWriteData.currentIndex.ToString(),
						")"
					}));
					while (meshWriteData.currentIndex < meshWriteData.indexCount)
					{
						meshWriteData.SetNextIndex(0);
					}
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0007E2B8 File Offset: 0x0007C4B8
		private void GenerateStencilClipEntryForRoundedRectBackground()
		{
			bool flag = this.currentElement.layout.width <= 1E-30f || this.currentElement.layout.height <= 1E-30f;
			if (!flag)
			{
				IResolvedStyle resolvedStyle = this.currentElement.resolvedStyle;
				Vector2 a;
				Vector2 a2;
				Vector2 a3;
				Vector2 a4;
				MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out a, out a2, out a3, out a4);
				float borderTopWidth = resolvedStyle.borderTopWidth;
				float borderLeftWidth = resolvedStyle.borderLeftWidth;
				float borderBottomWidth = resolvedStyle.borderBottomWidth;
				float borderRightWidth = resolvedStyle.borderRightWidth;
				MeshGenerationContextUtils.RectangleParams rectParams = new MeshGenerationContextUtils.RectangleParams
				{
					rect = this.currentElement.rect,
					color = Color.white,
					topLeftRadius = Vector2.Max(Vector2.zero, a - new Vector2(borderLeftWidth, borderTopWidth)),
					topRightRadius = Vector2.Max(Vector2.zero, a3 - new Vector2(borderRightWidth, borderTopWidth)),
					bottomLeftRadius = Vector2.Max(Vector2.zero, a2 - new Vector2(borderLeftWidth, borderBottomWidth)),
					bottomRightRadius = Vector2.Max(Vector2.zero, a4 - new Vector2(borderRightWidth, borderBottomWidth)),
					playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
				};
				rectParams.rect.x = rectParams.rect.x + borderLeftWidth;
				rectParams.rect.y = rectParams.rect.y + borderTopWidth;
				rectParams.rect.width = rectParams.rect.width - (borderLeftWidth + borderRightWidth);
				rectParams.rect.height = rectParams.rect.height - (borderTopWidth + borderBottomWidth);
				bool flag2 = this.currentElement.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
				if (flag2)
				{
					rectParams.rect.x = rectParams.rect.x + resolvedStyle.paddingLeft;
					rectParams.rect.y = rectParams.rect.y + resolvedStyle.paddingTop;
					rectParams.rect.width = rectParams.rect.width - (resolvedStyle.paddingLeft + resolvedStyle.paddingRight);
					rectParams.rect.height = rectParams.rect.height - (resolvedStyle.paddingTop + resolvedStyle.paddingBottom);
				}
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.stencilRef = this.m_StencilRef;
				this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				MeshBuilder.MakeSolidRect(rectParams, 1f, new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocRawVertsIndicesDelegate
				});
				bool flag3 = this.m_CurrentEntry.vertices.Length > 0 && this.m_CurrentEntry.indices.Length > 0;
				if (flag3)
				{
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_ClosingInfo.needsClosing = true;
				}
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0007E608 File Offset: 0x0007C808
		private void GenerateStencilClipEntryForSVGBackground()
		{
			bool flag = this.m_SVGBackgroundEntryIndex == -1;
			if (!flag)
			{
				UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
				Debug.Assert(entry.vertices.Length > 0);
				Debug.Assert(entry.indices.Length > 0);
				this.m_CurrentEntry.vertices = entry.vertices;
				this.m_CurrentEntry.indices = entry.indices;
				this.m_CurrentEntry.uvIsDisplacement = entry.uvIsDisplacement;
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.stencilRef = this.m_StencilRef;
				this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				this.m_ClosingInfo.needsClosing = true;
				int length = this.m_CurrentEntry.vertices.Length;
				NativeSlice<Vertex> vertices = this.m_VertsPool.Alloc((uint)length);
				for (int i = 0; i < length; i++)
				{
					Vertex value = this.m_CurrentEntry.vertices[i];
					value.position.z = 1f;
					vertices[i] = value;
				}
				this.m_CurrentEntry.vertices = vertices;
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_Entries.Add(this.m_CurrentEntry);
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x04000D21 RID: 3361
		private RenderChain m_Owner;

		// Token: 0x04000D22 RID: 3362
		private List<UIRStylePainter.Entry> m_Entries = new List<UIRStylePainter.Entry>();

		// Token: 0x04000D23 RID: 3363
		private AtlasBase m_Atlas;

		// Token: 0x04000D24 RID: 3364
		private VectorImageManager m_VectorImageManager;

		// Token: 0x04000D25 RID: 3365
		private UIRStylePainter.Entry m_CurrentEntry;

		// Token: 0x04000D26 RID: 3366
		private UIRStylePainter.ClosingInfo m_ClosingInfo;

		// Token: 0x04000D27 RID: 3367
		private int m_MaskDepth;

		// Token: 0x04000D28 RID: 3368
		private int m_StencilRef;

		// Token: 0x04000D29 RID: 3369
		private BMPAlloc m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;

		// Token: 0x04000D2A RID: 3370
		private int m_SVGBackgroundEntryIndex = -1;

		// Token: 0x04000D2B RID: 3371
		private UIRStylePainter.TempDataAlloc<Vertex> m_VertsPool = new UIRStylePainter.TempDataAlloc<Vertex>(8192);

		// Token: 0x04000D2C RID: 3372
		private UIRStylePainter.TempDataAlloc<ushort> m_IndicesPool = new UIRStylePainter.TempDataAlloc<ushort>(16384);

		// Token: 0x04000D2D RID: 3373
		private List<MeshWriteData> m_MeshWriteDataPool;

		// Token: 0x04000D2E RID: 3374
		private int m_NextMeshWriteDataPoolItem;

		// Token: 0x04000D2F RID: 3375
		private MeshBuilder.AllocMeshData.Allocator m_AllocRawVertsIndicesDelegate;

		// Token: 0x04000D30 RID: 3376
		private MeshBuilder.AllocMeshData.Allocator m_AllocThroughDrawMeshDelegate;

		// Token: 0x04000D31 RID: 3377
		private MeshBuilder.AllocMeshData.Allocator m_AllocThroughDrawGradientsDelegate;

		// Token: 0x04000D32 RID: 3378
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly MeshGenerationContext <meshGenerationContext>k__BackingField;

		// Token: 0x04000D33 RID: 3379
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <currentElement>k__BackingField;

		// Token: 0x04000D34 RID: 3380
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <totalVertices>k__BackingField;

		// Token: 0x04000D35 RID: 3381
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <totalIndices>k__BackingField;

		// Token: 0x04000D36 RID: 3382
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;

		// Token: 0x02000350 RID: 848
		internal struct Entry
		{
			// Token: 0x04000D37 RID: 3383
			public NativeSlice<Vertex> vertices;

			// Token: 0x04000D38 RID: 3384
			public NativeSlice<ushort> indices;

			// Token: 0x04000D39 RID: 3385
			public Material material;

			// Token: 0x04000D3A RID: 3386
			public Texture custom;

			// Token: 0x04000D3B RID: 3387
			public Texture font;

			// Token: 0x04000D3C RID: 3388
			public float fontTexSDFScale;

			// Token: 0x04000D3D RID: 3389
			public TextureId texture;

			// Token: 0x04000D3E RID: 3390
			public RenderChainCommand customCommand;

			// Token: 0x04000D3F RID: 3391
			public BMPAlloc clipRectID;

			// Token: 0x04000D40 RID: 3392
			public VertexFlags addFlags;

			// Token: 0x04000D41 RID: 3393
			public bool uvIsDisplacement;

			// Token: 0x04000D42 RID: 3394
			public bool isTextEntry;

			// Token: 0x04000D43 RID: 3395
			public bool isClipRegisterEntry;

			// Token: 0x04000D44 RID: 3396
			public int stencilRef;

			// Token: 0x04000D45 RID: 3397
			public int maskDepth;
		}

		// Token: 0x02000351 RID: 849
		internal struct ClosingInfo
		{
			// Token: 0x04000D46 RID: 3398
			public bool needsClosing;

			// Token: 0x04000D47 RID: 3399
			public bool popViewMatrix;

			// Token: 0x04000D48 RID: 3400
			public bool popScissorClip;

			// Token: 0x04000D49 RID: 3401
			public bool blitAndPopRenderTexture;

			// Token: 0x04000D4A RID: 3402
			public bool PopDefaultMaterial;

			// Token: 0x04000D4B RID: 3403
			public RenderChainCommand clipUnregisterDrawCommand;

			// Token: 0x04000D4C RID: 3404
			public NativeSlice<Vertex> clipperRegisterVertices;

			// Token: 0x04000D4D RID: 3405
			public NativeSlice<ushort> clipperRegisterIndices;

			// Token: 0x04000D4E RID: 3406
			public int clipperRegisterIndexOffset;

			// Token: 0x04000D4F RID: 3407
			public int maskStencilRef;
		}

		// Token: 0x02000352 RID: 850
		internal struct TempDataAlloc<T> : IDisposable where T : struct
		{
			// Token: 0x06001B6C RID: 7020 RVA: 0x0007E7AF File Offset: 0x0007C9AF
			public TempDataAlloc(int maxPoolElems)
			{
				this.maxPoolElemCount = maxPoolElems;
				this.pool = default(NativeArray<T>);
				this.excess = new List<NativeArray<T>>();
				this.takenFromPool = 0U;
			}

			// Token: 0x06001B6D RID: 7021 RVA: 0x0007E7D8 File Offset: 0x0007C9D8
			public void Dispose()
			{
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool isCreated = this.pool.IsCreated;
				if (isCreated)
				{
					this.pool.Dispose();
				}
			}

			// Token: 0x06001B6E RID: 7022 RVA: 0x0007E858 File Offset: 0x0007CA58
			internal NativeSlice<T> Alloc(uint count)
			{
				bool flag = (ulong)(this.takenFromPool + count) <= (ulong)((long)this.pool.Length);
				NativeSlice<T> result;
				if (flag)
				{
					NativeSlice<T> nativeSlice = this.pool.Slice((int)this.takenFromPool, (int)count);
					this.takenFromPool += count;
					result = nativeSlice;
				}
				else
				{
					NativeArray<T> nativeArray = new NativeArray<T>((int)count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
					this.excess.Add(nativeArray);
					result = nativeArray;
				}
				return result;
			}

			// Token: 0x06001B6F RID: 7023 RVA: 0x0007E8CC File Offset: 0x0007CACC
			internal void SessionDone()
			{
				int num = this.pool.Length;
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					bool flag = nativeArray.Length < this.maxPoolElemCount;
					if (flag)
					{
						num += nativeArray.Length;
					}
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool flag2 = num > this.pool.Length;
				if (flag2)
				{
					bool isCreated = this.pool.IsCreated;
					if (isCreated)
					{
						this.pool.Dispose();
					}
					this.pool = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				this.takenFromPool = 0U;
			}

			// Token: 0x04000D50 RID: 3408
			private int maxPoolElemCount;

			// Token: 0x04000D51 RID: 3409
			private NativeArray<T> pool;

			// Token: 0x04000D52 RID: 3410
			private List<NativeArray<T>> excess;

			// Token: 0x04000D53 RID: 3411
			private uint takenFromPool;
		}
	}
}
