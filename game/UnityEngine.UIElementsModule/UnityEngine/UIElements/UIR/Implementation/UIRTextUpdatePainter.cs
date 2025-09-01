using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x02000353 RID: 851
	internal class UIRTextUpdatePainter : IStylePainter, IDisposable
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0007E9A4 File Offset: 0x0007CBA4
		public MeshGenerationContext meshGenerationContext
		{
			[CompilerGenerated]
			get
			{
				return this.<meshGenerationContext>k__BackingField;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0007E9AC File Offset: 0x0007CBAC
		public UIRTextUpdatePainter()
		{
			this.meshGenerationContext = new MeshGenerationContext(this);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0007E9C4 File Offset: 0x0007CBC4
		public void Begin(VisualElement ve, UIRenderDevice device)
		{
			Debug.Assert(ve.renderChainData.usesLegacyText && ve.renderChainData.textEntries.Count > 0);
			this.m_CurrentElement = ve;
			this.m_TextEntryIndex = 0;
			Alloc allocVerts = ve.renderChainData.data.allocVerts;
			NativeSlice<Vertex> slice = ve.renderChainData.data.allocPage.vertices.cpuData.Slice((int)allocVerts.start, (int)allocVerts.size);
			device.Update(ve.renderChainData.data, ve.renderChainData.data.allocVerts.size, out this.m_MeshDataVerts);
			RenderChainTextEntry renderChainTextEntry = ve.renderChainData.textEntries[0];
			bool flag = ve.renderChainData.textEntries.Count > 1 || renderChainTextEntry.vertexCount != this.m_MeshDataVerts.Length;
			if (flag)
			{
				this.m_MeshDataVerts.CopyFrom(slice);
			}
			int firstVertex = renderChainTextEntry.firstVertex;
			this.m_XFormClipPages = slice[firstVertex].xformClipPages;
			this.m_IDs = slice[firstVertex].ids;
			this.m_Flags = slice[firstVertex].flags;
			this.m_OpacityColorPages = slice[firstVertex].opacityColorPages;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0007EB17 File Offset: 0x0007CD17
		public void End()
		{
			Debug.Assert(this.m_TextEntryIndex == this.m_CurrentElement.renderChainData.textEntries.Count);
			this.m_CurrentElement = null;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0007EB44 File Offset: 0x0007CD44
		public void Dispose()
		{
			bool isCreated = this.m_DudVerts.IsCreated;
			if (isCreated)
			{
				this.m_DudVerts.Dispose();
			}
			bool isCreated2 = this.m_DudIndices.IsCreated;
			if (isCreated2)
			{
				this.m_DudIndices.Dispose();
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00002166 File Offset: 0x00000366
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00002166 File Offset: 0x00000366
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00002166 File Offset: 0x00000366
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0007EB88 File Offset: 0x0007CD88
		public VisualElement visualElement
		{
			get
			{
				return this.m_CurrentElement;
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0007EBA0 File Offset: 0x0007CDA0
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			bool flag = this.m_DudVerts.Length < vertexCount;
			if (flag)
			{
				bool isCreated = this.m_DudVerts.IsCreated;
				if (isCreated)
				{
					this.m_DudVerts.Dispose();
				}
				this.m_DudVerts = new NativeArray<Vertex>(vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			bool flag2 = this.m_DudIndices.Length < indexCount;
			if (flag2)
			{
				bool isCreated2 = this.m_DudIndices.IsCreated;
				if (isCreated2)
				{
					this.m_DudIndices.Dispose();
				}
				this.m_DudIndices = new NativeArray<ushort>(indexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			return new MeshWriteData
			{
				m_Vertices = this.m_DudVerts.Slice(0, vertexCount),
				m_Indices = this.m_DudIndices.Slice(0, indexCount)
			};
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0007EC5C File Offset: 0x0007CE5C
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = !TextUtilities.IsFontAssigned(textParams);
			if (!flag)
			{
				float scaling = TextNative.ComputeTextScaling(this.m_CurrentElement.worldTransform, pixelsPerPoint);
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, scaling);
				using (NativeArray<TextVertex> vertices = TextNative.GetVertices(textNativeSettings))
				{
					List<RenderChainTextEntry> textEntries = this.m_CurrentElement.renderChainData.textEntries;
					int textEntryIndex = this.m_TextEntryIndex;
					this.m_TextEntryIndex = textEntryIndex + 1;
					RenderChainTextEntry renderChainTextEntry = textEntries[textEntryIndex];
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					MeshBuilder.UpdateText(vertices, offset, this.m_CurrentElement.renderChainData.verticesSpace, this.m_XFormClipPages, this.m_IDs, this.m_Flags, this.m_OpacityColorPages, this.m_MeshDataVerts.Slice(renderChainTextEntry.firstVertex, renderChainTextEntry.vertexCount));
					renderChainTextEntry.command.state.font = textParams.font.material.mainTexture;
				}
			}
		}

		// Token: 0x04000D54 RID: 3412
		private VisualElement m_CurrentElement;

		// Token: 0x04000D55 RID: 3413
		private int m_TextEntryIndex;

		// Token: 0x04000D56 RID: 3414
		private NativeArray<Vertex> m_DudVerts;

		// Token: 0x04000D57 RID: 3415
		private NativeArray<ushort> m_DudIndices;

		// Token: 0x04000D58 RID: 3416
		private NativeSlice<Vertex> m_MeshDataVerts;

		// Token: 0x04000D59 RID: 3417
		private Color32 m_XFormClipPages;

		// Token: 0x04000D5A RID: 3418
		private Color32 m_IDs;

		// Token: 0x04000D5B RID: 3419
		private Color32 m_Flags;

		// Token: 0x04000D5C RID: 3420
		private Color32 m_OpacityColorPages;

		// Token: 0x04000D5D RID: 3421
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly MeshGenerationContext <meshGenerationContext>k__BackingField;
	}
}
