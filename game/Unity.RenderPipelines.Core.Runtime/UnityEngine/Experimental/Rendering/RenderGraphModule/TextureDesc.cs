using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200003C RID: 60
	public struct TextureDesc
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000BDF9 File Offset: 0x00009FF9
		private void InitDefaultValues(bool dynamicResolution, bool xrReady)
		{
			this.useDynamicScale = dynamicResolution;
			if (xrReady)
			{
				this.slices = TextureXR.slices;
				this.dimension = TextureXR.dimension;
				return;
			}
			this.slices = 1;
			this.dimension = TextureDimension.Tex2D;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000BE2A File Offset: 0x0000A02A
		public TextureDesc(int width, int height, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Explicit;
			this.width = width;
			this.height = height;
			this.msaaSamples = MSAASamples.None;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000BE58 File Offset: 0x0000A058
		public TextureDesc(Vector2 scale, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Scale;
			this.scale = scale;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000BE85 File Offset: 0x0000A085
		public TextureDesc(ScaleFunc func, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Functor;
			this.func = func;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public TextureDesc(TextureDesc input)
		{
			this = input;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		public override int GetHashCode()
		{
			int num = 17;
			switch (this.sizeMode)
			{
			case TextureSizeMode.Explicit:
				num = num * 23 + this.width;
				num = num * 23 + this.height;
				break;
			case TextureSizeMode.Scale:
				num = num * 23 + this.scale.x.GetHashCode();
				num = num * 23 + this.scale.y.GetHashCode();
				break;
			case TextureSizeMode.Functor:
				if (this.func != null)
				{
					num = num * 23 + this.func.GetHashCode();
				}
				break;
			}
			num = num * 23 + this.mipMapBias.GetHashCode();
			num = num * 23 + this.slices;
			num = (int)(num * 23 + this.depthBufferBits);
			num = (int)(num * 23 + this.colorFormat);
			num = (int)(num * 23 + this.filterMode);
			num = (int)(num * 23 + this.wrapMode);
			num = (int)(num * 23 + this.dimension);
			num = (int)(num * 23 + this.memoryless);
			num = num * 23 + this.anisoLevel;
			num = num * 23 + (this.enableRandomWrite ? 1 : 0);
			num = num * 23 + (this.useMipMap ? 1 : 0);
			num = num * 23 + (this.autoGenerateMips ? 1 : 0);
			num = num * 23 + (this.isShadowMap ? 1 : 0);
			num = num * 23 + (this.bindTextureMS ? 1 : 0);
			num = num * 23 + (this.useDynamicScale ? 1 : 0);
			num = (int)(num * 23 + this.msaaSamples);
			return num * 23 + (this.fastMemoryDesc.inFastMemory ? 1 : 0);
		}

		// Token: 0x04000168 RID: 360
		public TextureSizeMode sizeMode;

		// Token: 0x04000169 RID: 361
		public int width;

		// Token: 0x0400016A RID: 362
		public int height;

		// Token: 0x0400016B RID: 363
		public int slices;

		// Token: 0x0400016C RID: 364
		public Vector2 scale;

		// Token: 0x0400016D RID: 365
		public ScaleFunc func;

		// Token: 0x0400016E RID: 366
		public DepthBits depthBufferBits;

		// Token: 0x0400016F RID: 367
		public GraphicsFormat colorFormat;

		// Token: 0x04000170 RID: 368
		public FilterMode filterMode;

		// Token: 0x04000171 RID: 369
		public TextureWrapMode wrapMode;

		// Token: 0x04000172 RID: 370
		public TextureDimension dimension;

		// Token: 0x04000173 RID: 371
		public bool enableRandomWrite;

		// Token: 0x04000174 RID: 372
		public bool useMipMap;

		// Token: 0x04000175 RID: 373
		public bool autoGenerateMips;

		// Token: 0x04000176 RID: 374
		public bool isShadowMap;

		// Token: 0x04000177 RID: 375
		public int anisoLevel;

		// Token: 0x04000178 RID: 376
		public float mipMapBias;

		// Token: 0x04000179 RID: 377
		public MSAASamples msaaSamples;

		// Token: 0x0400017A RID: 378
		public bool bindTextureMS;

		// Token: 0x0400017B RID: 379
		public bool useDynamicScale;

		// Token: 0x0400017C RID: 380
		public RenderTextureMemoryless memoryless;

		// Token: 0x0400017D RID: 381
		public string name;

		// Token: 0x0400017E RID: 382
		public FastMemoryDesc fastMemoryDesc;

		// Token: 0x0400017F RID: 383
		public bool fallBackToBlackTexture;

		// Token: 0x04000180 RID: 384
		public bool clearBuffer;

		// Token: 0x04000181 RID: 385
		public Color clearColor;
	}
}
