using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000024 RID: 36
	public struct RenderGraphBuilder : IDisposable
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000097BD File Offset: 0x000079BD
		public TextureHandle UseColorBuffer(in TextureHandle input, int index)
		{
			this.CheckResource(input.handle, true);
			this.m_Resources.IncrementWriteCount(input.handle);
			this.m_RenderPass.SetColorBuffer(input, index);
			return input;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000097F8 File Offset: 0x000079F8
		public TextureHandle UseDepthBuffer(in TextureHandle input, DepthAccess flags)
		{
			this.CheckResource(input.handle, true);
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.m_Resources.IncrementWriteCount(input.handle);
			}
			if ((flags & DepthAccess.Read) != (DepthAccess)0 && !this.m_Resources.IsRenderGraphResourceImported(input.handle) && this.m_Resources.TextureNeedsFallback(input))
			{
				this.WriteTexture(input);
			}
			this.m_RenderPass.SetDepthBuffer(input, flags);
			return input;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009870 File Offset: 0x00007A70
		public TextureHandle ReadTexture(in TextureHandle input)
		{
			this.CheckResource(input.handle, false);
			if (!this.m_Resources.IsRenderGraphResourceImported(input.handle) && this.m_Resources.TextureNeedsFallback(input))
			{
				TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(input.handle);
				if (!textureResourceDesc.bindTextureMS)
				{
					if (textureResourceDesc.dimension == TextureXR.dimension)
					{
						return this.m_RenderGraph.defaultResources.blackTextureXR;
					}
					if (textureResourceDesc.dimension == TextureDimension.Tex3D)
					{
						return this.m_RenderGraph.defaultResources.blackTexture3DXR;
					}
					return this.m_RenderGraph.defaultResources.blackTexture;
				}
				else
				{
					if (!textureResourceDesc.clearBuffer)
					{
						this.m_Resources.ForceTextureClear(input.handle, Color.black);
					}
					this.WriteTexture(input);
				}
			}
			this.m_RenderPass.AddResourceRead(input.handle);
			return input;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009951 File Offset: 0x00007B51
		public TextureHandle WriteTexture(in TextureHandle input)
		{
			this.CheckResource(input.handle, false);
			this.m_Resources.IncrementWriteCount(input.handle);
			this.m_RenderPass.AddResourceWrite(input.handle);
			return input;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009988 File Offset: 0x00007B88
		public TextureHandle ReadWriteTexture(in TextureHandle input)
		{
			this.CheckResource(input.handle, false);
			this.m_Resources.IncrementWriteCount(input.handle);
			this.m_RenderPass.AddResourceWrite(input.handle);
			this.m_RenderPass.AddResourceRead(input.handle);
			return input;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000099DC File Offset: 0x00007BDC
		public TextureHandle CreateTransientTexture(in TextureDesc desc)
		{
			TextureHandle result = this.m_Resources.CreateTexture(desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(result.handle);
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009A14 File Offset: 0x00007C14
		public TextureHandle CreateTransientTexture(in TextureHandle texture)
		{
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(texture.handle);
			TextureHandle result = this.m_Resources.CreateTexture(textureResourceDesc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(result.handle);
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009A5F File Offset: 0x00007C5F
		public RendererListHandle UseRendererList(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(input);
			return input;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009A78 File Offset: 0x00007C78
		public ComputeBufferHandle ReadComputeBuffer(in ComputeBufferHandle input)
		{
			this.CheckResource(input.handle, false);
			this.m_RenderPass.AddResourceRead(input.handle);
			return input;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009A9E File Offset: 0x00007C9E
		public ComputeBufferHandle WriteComputeBuffer(in ComputeBufferHandle input)
		{
			this.CheckResource(input.handle, false);
			this.m_RenderPass.AddResourceWrite(input.handle);
			this.m_Resources.IncrementWriteCount(input.handle);
			return input;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public ComputeBufferHandle CreateTransientComputeBuffer(in ComputeBufferDesc desc)
		{
			ComputeBufferHandle result = this.m_Resources.CreateComputeBuffer(desc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(result.handle);
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009B10 File Offset: 0x00007D10
		public ComputeBufferHandle CreateTransientComputeBuffer(in ComputeBufferHandle computebuffer)
		{
			ComputeBufferDesc computeBufferResourceDesc = this.m_Resources.GetComputeBufferResourceDesc(computebuffer.handle);
			ComputeBufferHandle result = this.m_Resources.CreateComputeBuffer(computeBufferResourceDesc, this.m_RenderPass.index);
			this.m_RenderPass.AddTransientResource(result.handle);
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009B5B File Offset: 0x00007D5B
		public void SetRenderFunc<PassData>(RenderFunc<PassData> renderFunc) where PassData : class, new()
		{
			((RenderGraphPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009B6E File Offset: 0x00007D6E
		public void EnableAsyncCompute(bool value)
		{
			this.m_RenderPass.EnableAsyncCompute(value);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009B7C File Offset: 0x00007D7C
		public void AllowPassCulling(bool value)
		{
			this.m_RenderPass.AllowPassCulling(value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009B8A File Offset: 0x00007D8A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009B93 File Offset: 0x00007D93
		public void AllowRendererListCulling(bool value)
		{
			this.m_RenderPass.AllowRendererListCulling(value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009BA1 File Offset: 0x00007DA1
		public RendererListHandle DependsOn(in RendererListHandle input)
		{
			this.m_RenderPass.UseRendererList(input);
			return input;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009BBA File Offset: 0x00007DBA
		internal RenderGraphBuilder(RenderGraphPass renderPass, RenderGraphResourceRegistry resources, RenderGraph renderGraph)
		{
			this.m_RenderPass = renderPass;
			this.m_Resources = resources;
			this.m_RenderGraph = renderGraph;
			this.m_Disposed = false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009BD8 File Offset: 0x00007DD8
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_RenderGraph.OnPassAdded(this.m_RenderPass);
			this.m_Disposed = true;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009BFB File Offset: 0x00007DFB
		private void CheckResource(in ResourceHandle res, bool dontCheckTransientReadWrite = false)
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009BFD File Offset: 0x00007DFD
		internal void GenerateDebugData(bool value)
		{
			this.m_RenderPass.GenerateDebugData(value);
		}

		// Token: 0x040000FC RID: 252
		private RenderGraphPass m_RenderPass;

		// Token: 0x040000FD RID: 253
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x040000FE RID: 254
		private RenderGraph m_RenderGraph;

		// Token: 0x040000FF RID: 255
		private bool m_Disposed;
	}
}
