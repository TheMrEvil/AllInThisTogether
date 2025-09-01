using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KuwaharaFilter
{
	// Token: 0x02000035 RID: 53
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	[ImageEffectAllowedInSceneView]
	public class AnisotropicKuwaharaEffect : MonoBehaviour
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000090C0 File Offset: 0x000072C0
		private void Start()
		{
			this.m_Camera = base.GetComponent<Camera>();
			this.m_Camera.depthTextureMode |= DepthTextureMode.Depth;
			this.m_GaussComputer = Resources.Load<ComputeShader>("Shaders/ComputerGauss");
			this.m_SSTComputer = Resources.Load<ComputeShader>("Shaders/ComputerStructureTensor");
			this.m_TFMComputer = Resources.Load<ComputeShader>("Shaders/ComputerVectorField");
			this.m_LICComputer = Resources.Load<ComputeShader>("Shaders/ComputerLineIntegralConvolution");
			this.m_KuwaharaComputer = Resources.Load<ComputeShader>("Shaders/ComputerAnisotropicKuwahara");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000913C File Offset: 0x0000733C
		private List<float> GenerateGaussKernel(int radius, float sigma)
		{
			List<float> source = (from x in Enumerable.Range(0, 2 * radius + 1)
			select Mathf.Exp(-Mathf.Pow((float)(x - radius), 2f) / (2f * sigma * sigma))).ToList<float>();
			float sum = source.Sum();
			return (from x in source
			select x / sum).ToList<float>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000091A8 File Offset: 0x000073A8
		private void InitializeRenderTexture(int width, int height)
		{
			if (this.m_TextureColor == null || this.m_TextureColor.width != width || this.m_TextureColor.height != height)
			{
				RenderTexture textureColor = this.m_TextureColor;
				if (textureColor != null)
				{
					textureColor.Release();
				}
				RenderTexture textureSST = this.m_TextureSST;
				if (textureSST != null)
				{
					textureSST.Release();
				}
				RenderTexture textureGaussHS = this.m_TextureGaussHS;
				if (textureGaussHS != null)
				{
					textureGaussHS.Release();
				}
				RenderTexture textureGaussVS = this.m_TextureGaussVS;
				if (textureGaussVS != null)
				{
					textureGaussVS.Release();
				}
				RenderTexture textureTFM = this.m_TextureTFM;
				if (textureTFM != null)
				{
					textureTFM.Release();
				}
				RenderTexture textureLIC = this.m_TextureLIC;
				if (textureLIC != null)
				{
					textureLIC.Release();
				}
				RenderTexture textureKuwahara = this.m_TextureKuwahara;
				if (textureKuwahara != null)
				{
					textureKuwahara.Release();
				}
				this.m_TextureColor = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
				this.m_TextureColor.Create();
				this.m_TextureSST = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				this.m_TextureSST.enableRandomWrite = true;
				this.m_TextureSST.Create();
				this.m_TextureGaussHS = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				this.m_TextureGaussHS.enableRandomWrite = true;
				this.m_TextureGaussHS.Create();
				this.m_TextureGaussVS = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				this.m_TextureGaussVS.enableRandomWrite = true;
				this.m_TextureGaussVS.Create();
				this.m_TextureTFM = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				this.m_TextureTFM.enableRandomWrite = true;
				this.m_TextureTFM.Create();
				this.m_TextureLIC = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
				this.m_TextureLIC.enableRandomWrite = true;
				this.m_TextureLIC.Create();
				this.m_TextureKuwahara = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
				this.m_TextureKuwahara.enableRandomWrite = true;
				this.m_TextureKuwahara.Create();
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009368 File Offset: 0x00007568
		private void InitializeComputeBuffer()
		{
			List<float> data = this.GenerateGaussKernel(this.GaussRadius, this.GaussSigma);
			this.m_KernelGaussFilter.SetData<float>(data);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00009394 File Offset: 0x00007594
		private void Update()
		{
			this.m_ConstantBuffer.GaussRadius = this.GaussRadius;
			this.m_ConstantBuffer.KuwaharaRadius = this.KuwaharaRadius;
			this.m_ConstantBuffer.KuwaharaAlpha = this.KuwaharaAlpha;
			this.m_ConstantBuffer.KuwaharaDepthClose = this.DepthClose;
			this.m_ConstantBuffer.KuwaharaDepthFar = this.DepthFar;
			this.m_ConstantBuffer.KuwaharaQ = this.KuwaharaQ;
			this.m_ConstantBuffer.KuwaharaOpacity = this.KuwaharaOpacity;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00009418 File Offset: 0x00007618
		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (this.KuwaharaOpacity <= 0f)
			{
				Graphics.Blit(src, dst);
				return;
			}
			int num = Mathf.CeilToInt(this.ResolutionScale * (float)this.m_Camera.pixelWidth);
			int num2 = Mathf.CeilToInt(this.ResolutionScale * (float)this.m_Camera.pixelHeight);
			this.InitializeRenderTexture(num, num2);
			this.InitializeComputeBuffer();
			Graphics.Blit(src, this.m_TextureColor);
			ConstantBufferVariable.Apply(this.m_SSTComputer, this.m_ConstantBuffer);
			ConstantBufferVariable.Apply(this.m_GaussComputer, this.m_ConstantBuffer);
			ConstantBufferVariable.Apply(this.m_TFMComputer, this.m_ConstantBuffer);
			ConstantBufferVariable.Apply(this.m_LICComputer, this.m_ConstantBuffer);
			ConstantBufferVariable.Apply(this.m_KuwaharaComputer, this.m_ConstantBuffer);
			int kernelIndex = this.m_SSTComputer.FindKernel("StructureTensor");
			this.m_SSTComputer.SetTexture(kernelIndex, "TextureColorSRV", this.m_TextureColor);
			this.m_SSTComputer.SetTexture(kernelIndex, "TextureColorUAV", this.m_TextureSST);
			this.m_SSTComputer.Dispatch(kernelIndex, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			int kernelIndex2 = this.m_GaussComputer.FindKernel("GaussHS");
			this.m_GaussComputer.SetTexture(kernelIndex2, "TextureColorSRV", this.m_TextureSST);
			this.m_GaussComputer.SetTexture(kernelIndex2, "TextureColorUAV", this.m_TextureGaussHS);
			this.m_GaussComputer.SetBuffer(kernelIndex2, "BufferGaussKernel", this.m_KernelGaussFilter);
			this.m_GaussComputer.Dispatch(kernelIndex2, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			int kernelIndex3 = this.m_GaussComputer.FindKernel("GaussVS");
			this.m_GaussComputer.SetTexture(kernelIndex3, "TextureColorSRV", this.m_TextureGaussHS);
			this.m_GaussComputer.SetTexture(kernelIndex3, "TextureColorUAV", this.m_TextureGaussVS);
			this.m_GaussComputer.SetBuffer(kernelIndex3, "BufferGaussKernel", this.m_KernelGaussFilter);
			this.m_GaussComputer.Dispatch(kernelIndex3, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			int kernelIndex4 = this.m_TFMComputer.FindKernel("VectorField");
			this.m_TFMComputer.SetTexture(kernelIndex4, "TextureColorSRV", this.m_TextureGaussVS);
			this.m_TFMComputer.SetTexture(kernelIndex4, "TextureColorUAV", this.m_TextureTFM);
			this.m_TFMComputer.Dispatch(kernelIndex4, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			int kernelIndex5 = this.m_LICComputer.FindKernel("LineIntegralConvolution");
			this.m_LICComputer.SetTexture(kernelIndex5, "TextureTFMSRV", this.m_TextureTFM);
			this.m_LICComputer.SetTexture(kernelIndex5, "TextureColorSRV", src);
			this.m_LICComputer.SetTexture(kernelIndex5, "TextureColorUAV", this.m_TextureLIC);
			this.m_LICComputer.SetBuffer(kernelIndex5, "BufferGaussKernel", this.m_KernelGaussFilter);
			this.m_LICComputer.Dispatch(kernelIndex5, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			int kernelIndex6 = this.m_KuwaharaComputer.FindKernel("AnisotropicKuwahara");
			this.m_KuwaharaComputer.SetTexture(kernelIndex, "TextureStart", this.m_TextureColor);
			this.m_KuwaharaComputer.SetTexture(kernelIndex6, "TextureTFMSRV", this.m_TextureTFM);
			this.m_KuwaharaComputer.SetTexture(kernelIndex6, "TextureColorSRV", this.m_TextureLIC);
			this.m_KuwaharaComputer.SetTexture(kernelIndex6, "TextureColorUAV", this.m_TextureKuwahara);
			this.m_KuwaharaComputer.SetTexture(kernelIndex6, "_CameraDepthTexture", Shader.GetGlobalTexture("_CameraDepthTexture"));
			this.m_KuwaharaComputer.Dispatch(kernelIndex6, Mathf.CeilToInt((float)num / 8f), Mathf.CeilToInt((float)num2 / 8f), 1);
			Graphics.Blit(this.m_TextureKuwahara, dst);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00009800 File Offset: 0x00007A00
		public void OnEnable()
		{
			this.m_KernelGaussFilter = new ComputeBuffer(64, 4, ComputeBufferType.Default);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009811 File Offset: 0x00007A11
		public void OnDisable()
		{
			if (this.m_KernelGaussFilter != null)
			{
				this.m_KernelGaussFilter.Dispose();
				this.m_KernelGaussFilter = null;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00009830 File Offset: 0x00007A30
		public AnisotropicKuwaharaEffect()
		{
		}

		// Token: 0x040001A9 RID: 425
		[Range(0f, 10f)]
		[Tooltip("Warning: A value has aт impact on performance")]
		public int GaussRadius = 5;

		// Token: 0x040001AA RID: 426
		[Range(0.1f, 10f)]
		public float GaussSigma = 8f;

		// Token: 0x040001AB RID: 427
		[Range(0f, 10f)]
		public float KuwaharaAlpha = 1f;

		// Token: 0x040001AC RID: 428
		[Range(0f, 5f)]
		[Tooltip("Warning: A value has aт impact on performance")]
		public int KuwaharaRadius = 2;

		// Token: 0x040001AD RID: 429
		[Range(1f, 20f)]
		public int KuwaharaQ = 8;

		// Token: 0x040001AE RID: 430
		[Range(0.1f, 1f)]
		[Tooltip("Warning: A value has aт impact on performance")]
		public float ResolutionScale = 1f;

		// Token: 0x040001AF RID: 431
		[Range(0f, 0.1f)]
		public float DepthClose = 0.05f;

		// Token: 0x040001B0 RID: 432
		[Range(0f, 0.25f)]
		public float DepthFar = 0.15f;

		// Token: 0x040001B1 RID: 433
		[Range(0f, 1f)]
		public float KuwaharaOpacity = 1f;

		// Token: 0x040001B2 RID: 434
		private ComputeShader m_GaussComputer;

		// Token: 0x040001B3 RID: 435
		private ComputeShader m_SSTComputer;

		// Token: 0x040001B4 RID: 436
		private ComputeShader m_TFMComputer;

		// Token: 0x040001B5 RID: 437
		private ComputeShader m_LICComputer;

		// Token: 0x040001B6 RID: 438
		private ComputeShader m_KuwaharaComputer;

		// Token: 0x040001B7 RID: 439
		private RenderTexture m_TextureColor;

		// Token: 0x040001B8 RID: 440
		private RenderTexture m_TextureSST;

		// Token: 0x040001B9 RID: 441
		private RenderTexture m_TextureGaussHS;

		// Token: 0x040001BA RID: 442
		private RenderTexture m_TextureGaussVS;

		// Token: 0x040001BB RID: 443
		private RenderTexture m_TextureTFM;

		// Token: 0x040001BC RID: 444
		private RenderTexture m_TextureLIC;

		// Token: 0x040001BD RID: 445
		private RenderTexture m_TextureKuwahara;

		// Token: 0x040001BE RID: 446
		private Camera m_Camera;

		// Token: 0x040001BF RID: 447
		private ComputeBuffer m_KernelGaussFilter;

		// Token: 0x040001C0 RID: 448
		private ConstantBufferVariable m_ConstantBuffer = new ConstantBufferVariable();

		// Token: 0x0200019C RID: 412
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x06000ECB RID: 3787 RVA: 0x00060299 File Offset: 0x0005E499
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x06000ECC RID: 3788 RVA: 0x000602A1 File Offset: 0x0005E4A1
			internal float <GenerateGaussKernel>b__0(int x)
			{
				return Mathf.Exp(-Mathf.Pow((float)(x - this.radius), 2f) / (2f * this.sigma * this.sigma));
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x000602D0 File Offset: 0x0005E4D0
			internal float <GenerateGaussKernel>b__1(float x)
			{
				return x / this.sum;
			}

			// Token: 0x04000CBB RID: 3259
			public int radius;

			// Token: 0x04000CBC RID: 3260
			public float sigma;

			// Token: 0x04000CBD RID: 3261
			public float sum;
		}
	}
}
