using System;
using System.Diagnostics;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200003D RID: 61
	[DebuggerDisplay("TextureResource ({desc.name})")]
	internal class TextureResource : RenderGraphResource<TextureDesc, RTHandle>
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000C049 File Offset: 0x0000A249
		public override string GetName()
		{
			if (!this.imported || this.shared)
			{
				return this.desc.name;
			}
			if (this.graphicsResource == null)
			{
				return "null resource";
			}
			return this.graphicsResource.name;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C080 File Offset: 0x0000A280
		public override void CreatePooledGraphicsResource()
		{
			int hashCode = this.desc.GetHashCode();
			if (this.graphicsResource != null)
			{
				throw new InvalidOperationException(string.Format("Trying to create an already created resource ({0}). Resource was probably declared for writing more than once in the same pass.", this.GetName()));
			}
			TexturePool texturePool = this.m_Pool as TexturePool;
			if (!texturePool.TryGetResource(hashCode, out this.graphicsResource))
			{
				this.CreateGraphicsResource("");
			}
			this.cachedHash = hashCode;
			texturePool.RegisterFrameAllocation(this.cachedHash, this.graphicsResource);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		public override void ReleasePooledGraphicsResource(int frameIndex)
		{
			if (this.graphicsResource == null)
			{
				throw new InvalidOperationException("Tried to release a resource (" + this.GetName() + ") that was never created. Check that there is at least one pass writing to it first.");
			}
			TexturePool texturePool = this.m_Pool as TexturePool;
			if (texturePool != null)
			{
				texturePool.ReleaseResource(this.cachedHash, this.graphicsResource, frameIndex);
				texturePool.UnregisterFrameAllocation(this.cachedHash, this.graphicsResource);
			}
			this.Reset(null);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C168 File Offset: 0x0000A368
		public override void CreateGraphicsResource(string name = "")
		{
			if (name == "")
			{
				name = string.Format("RenderGraphTexture_{0}", TextureResource.m_TextureCreationIndex++);
			}
			switch (this.desc.sizeMode)
			{
			case TextureSizeMode.Explicit:
				this.graphicsResource = RTHandles.Alloc(this.desc.width, this.desc.height, this.desc.slices, this.desc.depthBufferBits, this.desc.colorFormat, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.memoryless, name);
				return;
			case TextureSizeMode.Scale:
				this.graphicsResource = RTHandles.Alloc(this.desc.scale, this.desc.slices, this.desc.depthBufferBits, this.desc.colorFormat, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.memoryless, name);
				return;
			case TextureSizeMode.Functor:
				this.graphicsResource = RTHandles.Alloc(this.desc.func, this.desc.slices, this.desc.depthBufferBits, this.desc.colorFormat, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.memoryless, name);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C421 File Offset: 0x0000A621
		public override void ReleaseGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C43C File Offset: 0x0000A63C
		public override void LogCreation(RenderGraphLogger logger)
		{
			logger.LogLine(string.Format("Created Texture: {0} (Cleared: {1})", this.desc.name, this.desc.clearBuffer), Array.Empty<object>());
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000C46E File Offset: 0x0000A66E
		public override void LogRelease(RenderGraphLogger logger)
		{
			logger.LogLine("Released Texture: " + this.desc.name, Array.Empty<object>());
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C490 File Offset: 0x0000A690
		public TextureResource()
		{
		}

		// Token: 0x04000182 RID: 386
		private static int m_TextureCreationIndex;
	}
}
