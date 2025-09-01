using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.Effects
{
	// Token: 0x02000029 RID: 41
	public class ScalableBlur : IBlurAlgorithm
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006854 File Offset: 0x00004A54
		public ScalableBlur()
		{
			this.blueNoise = Resources.Load<Texture2D>("True Shadow Blue Noise");
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000686C File Offset: 0x00004A6C
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006897 File Offset: 0x00004A97
		private Material Material
		{
			get
			{
				if (this.material == null)
				{
					this.material = new Material(Shader.Find("Hidden/TrueShadow/Generate"));
				}
				return this.material;
			}
			set
			{
				this.material = value;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000068A0 File Offset: 0x00004AA0
		public void Configure(BlurConfig config)
		{
			this.config = (ScalableBlurConfig)config;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000068B0 File Offset: 0x00004AB0
		public void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target)
		{
			float radius = this.config.Radius;
			this.Material.SetFloat(ShaderProperties.blurRadius, radius);
			this.Material.SetVector(ShaderProperties.blurTextureCropRegion, srcCropRegion.ToMinMaxVector());
			int downsampleFactor = (this.config.Iteration > 0) ? 1 : 0;
			int num = Mathf.Max(this.config.Iteration * 2 - 1, 1);
			int num2 = ShaderProperties.intermediateRT[0];
			ScalableBlur.CreateTempRenderTextureFrom(cmd, num2, target, downsampleFactor);
			cmd.Blit(src, num2, this.Material, 1);
			for (int i = 1; i < num; i++)
			{
				this.BlurAtDepth(cmd, i, target);
			}
			this.Material.SetTexture(ScalableBlur.BLUE_NOISE_ID, this.blueNoise);
			this.Material.SetVector(ScalableBlur.TARGET_SIZE_ID, new Vector4((float)target.width, (float)target.height));
			cmd.Blit(ShaderProperties.intermediateRT[num - 1], target, this.Material, 2);
			ScalableBlur.CleanupIntermediateRT(cmd, num);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000069C0 File Offset: 0x00004BC0
		protected virtual void BlurAtDepth(CommandBuffer cmd, int depth, RenderTexture baseTexture)
		{
			int num = Utility.SimplePingPong(depth, this.config.Iteration - 1) + 1;
			num = Mathf.Min(num, this.config.MaxDepth);
			ScalableBlur.CreateTempRenderTextureFrom(cmd, ShaderProperties.intermediateRT[depth], baseTexture, num);
			cmd.Blit(ShaderProperties.intermediateRT[depth - 1], ShaderProperties.intermediateRT[depth], this.Material, 0);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006A2C File Offset: 0x00004C2C
		private static void CreateTempRenderTextureFrom(CommandBuffer cmd, int nameId, RenderTexture src, int downsampleFactor)
		{
			int width = src.width >> downsampleFactor;
			int height = src.height >> downsampleFactor;
			cmd.GetTemporaryRT(nameId, width, height, 0, FilterMode.Bilinear, src.format);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006A64 File Offset: 0x00004C64
		private static void CleanupIntermediateRT(CommandBuffer cmd, int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				cmd.ReleaseTemporaryRT(ShaderProperties.intermediateRT[i]);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006A8A File Offset: 0x00004C8A
		// Note: this type is marked as 'beforefieldinit'.
		static ScalableBlur()
		{
		}

		// Token: 0x040000B1 RID: 177
		private Material material;

		// Token: 0x040000B2 RID: 178
		private ScalableBlurConfig config;

		// Token: 0x040000B3 RID: 179
		private static readonly int BLUE_NOISE_ID = Shader.PropertyToID("_BlueNoise");

		// Token: 0x040000B4 RID: 180
		private static readonly int TARGET_SIZE_ID = Shader.PropertyToID("_TargetSize");

		// Token: 0x040000B5 RID: 181
		private readonly Texture2D blueNoise;

		// Token: 0x040000B6 RID: 182
		private const int BLUR_PASS = 0;

		// Token: 0x040000B7 RID: 183
		private const int CROP_BLUR_PASS = 1;

		// Token: 0x040000B8 RID: 184
		private const int DITHER_BLUR_PASS = 2;
	}
}
