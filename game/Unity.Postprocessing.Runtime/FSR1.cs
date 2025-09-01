using System;
using UnityEngine.Rendering.PostProcessing.FSR;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000029 RID: 41
	[Preserve]
	[Serializable]
	public class FSR1
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000577F File Offset: 0x0000397F
		public void OnMipmapSingleTexture(Texture texture)
		{
			MipMapUtils.OnMipMapSingleTexture(texture, (float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000057A5 File Offset: 0x000039A5
		public void OnMipMapAllTextures()
		{
			MipMapUtils.OnMipMapAllTextures((float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000057CA File Offset: 0x000039CA
		public void OnResetAllMipMaps()
		{
			MipMapUtils.OnResetAllMipMaps();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000057D1 File Offset: 0x000039D1
		public bool IsSupported()
		{
			return SystemInfo.supportsComputeShaders;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000057D8 File Offset: 0x000039D8
		internal void Release()
		{
			this.computeShaderEASU = null;
			this.computeShaderRCAS = null;
			if (this.outputImage)
			{
				this.outputImage.Release();
				this.outputImage = null;
			}
			if (this.EASUParametersCB != null)
			{
				this.EASUParametersCB.Dispose();
				this.EASUParametersCB = null;
			}
			if (this.outputImage2)
			{
				this.outputImage2.Release();
				this.outputImage2 = null;
			}
			if (this.RCASParametersCB != null)
			{
				this.RCASParametersCB.Dispose();
				this.RCASParametersCB = null;
			}
			MipMapUtils.OnResetAllMipMaps();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000586C File Offset: 0x00003A6C
		public void Init()
		{
			this.computeShaderEASU = (ComputeShader)Resources.Load("FSR1/EdgeAdaptiveScaleUpsampling");
			this.computeShaderRCAS = (ComputeShader)Resources.Load("FSR1/RobustContrastAdaptiveSharpen");
			this.EASUParametersCB = new ComputeBuffer(4, 16);
			this.EASUParametersCB.name = "EASU Parameters";
			this.RCASParametersCB = new ComputeBuffer(1, 16);
			this.RCASParametersCB.name = "RCAS Parameters";
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000058E0 File Offset: 0x00003AE0
		internal void ConfigureCameraViewport(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			this._originalRect = camera.rect;
			this.displaySize = new Vector2Int(camera.pixelWidth, camera.pixelHeight);
			this.renderSize = new Vector2Int((int)((float)this.displaySize.x / this.scaleFactor), (int)((float)this.displaySize.y / this.scaleFactor));
			if (this.qualityMode == QualityMode.Off)
			{
				this.Release();
			}
			camera.aspect = (float)this.displaySize.x * this._originalRect.width / ((float)this.displaySize.y * this._originalRect.height);
			if (context.camera.stereoEnabled)
			{
				camera.rect = new Rect(0f, 0f, this._originalRect.width * (float)this.renderSize.x / (float)this.displaySize.x, this._originalRect.height * (float)this.renderSize.y / (float)this.displaySize.y);
				return;
			}
			camera.rect = new Rect(0f, 0f, this._originalRect.width * (float)this.renderSize.x / (float)this.displaySize.x, this._originalRect.height * (float)this.renderSize.y / (float)this.displaySize.y);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005A5B File Offset: 0x00003C5B
		public void ResetCameraViewport(PostProcessRenderContext context)
		{
			if (context.camera.stereoEnabled)
			{
				context.camera.rect = this._originalRect;
				return;
			}
			context.camera.rect = this._originalRect;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005A90 File Offset: 0x00003C90
		internal void Render(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			if (this.qualityMode == QualityMode.Off)
			{
				this.scaleFactor = this.GetScaling();
				command.Blit(context.source, context.destination);
				return;
			}
			if (this.computeShaderEASU == null || this.computeShaderRCAS == null)
			{
				this.Init();
			}
			if (this.autoTextureUpdate)
			{
				MipMapUtils.AutoUpdateMipMaps((float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride, this.updateFrequency, ref this._prevMipMapBias, ref this._mipMapTimer, ref this._previousLength);
			}
			command.BeginSample("FSR1");
			if (this.outputImage == null || this.outputImage2 == null || this.displaySize.x != this._prevDisplaySize.x || this.displaySize.y != this._prevDisplaySize.y || this.qualityMode != this._prevQualityMode || this.Sharpening != this._prevSharpening)
			{
				this._prevQualityMode = this.qualityMode;
				this._prevDisplaySize = this.displaySize;
				this._prevSharpening = this.Sharpening;
				this.scaleFactor = this.GetScaling();
				this._mipMapTimer = float.PositiveInfinity;
				float t = (this.scaleFactor - 1.3f) / 0.70000005f;
				float mipMapBias = -Mathf.Lerp(0.38f, 1f, t);
				if (this.outputImage)
				{
					this.outputImage.Release();
				}
				this.outputImage = new RenderTexture(this.displaySize.x, this.displaySize.y, 0, context.sourceFormat, RenderTextureReadWrite.Linear);
				this.outputImage.enableRandomWrite = true;
				this.outputImage.mipMapBias = mipMapBias;
				this.outputImage.Create();
				if (this.Sharpening)
				{
					if (this.outputImage2)
					{
						this.outputImage2.Release();
					}
					this.outputImage2 = new RenderTexture(this.displaySize.x, this.displaySize.y, 0, context.sourceFormat, RenderTextureReadWrite.Linear);
					this.outputImage2.enableRandomWrite = true;
					this.outputImage2.Create();
				}
			}
			command.SetComputeVectorParam(this.computeShaderEASU, FSR1._EASUViewportSize, new Vector4((float)this.renderSize.x, (float)this.renderSize.y));
			command.SetComputeVectorParam(this.computeShaderEASU, FSR1._EASUInputImageSize, new Vector4((float)this.renderSize.x, (float)this.renderSize.y));
			command.SetComputeVectorParam(this.computeShaderEASU, FSR1._EASUOutputSize, new Vector4((float)this.displaySize.x, (float)this.displaySize.y, 1f / (float)this.displaySize.x, 1f / (float)this.displaySize.y));
			command.SetComputeBufferParam(this.computeShaderEASU, 1, FSR1._EASUParameters, this.EASUParametersCB);
			command.DispatchCompute(this.computeShaderEASU, 1, 1, 1, 1);
			command.SetComputeTextureParam(this.computeShaderEASU, 0, FSR1.InputTexture, context.source);
			command.SetComputeTextureParam(this.computeShaderEASU, 0, FSR1.OutputTexture, this.outputImage);
			int threadGroupsX = (this.outputImage.width + 8 - 1) / 8;
			int threadGroupsY = (this.outputImage.height + 8 - 1) / 8;
			command.SetComputeBufferParam(this.computeShaderEASU, 0, FSR1._EASUParameters, this.EASUParametersCB);
			command.DispatchCompute(this.computeShaderEASU, 0, threadGroupsX, threadGroupsY, 1);
			if (this.Sharpening)
			{
				command.SetComputeBufferParam(this.computeShaderRCAS, 1, FSR1._RCASParameters, this.RCASParametersCB);
				command.SetComputeFloatParam(this.computeShaderRCAS, FSR1._RCASScale, 1f - this.sharpness);
				command.DispatchCompute(this.computeShaderRCAS, 1, 1, 1, 1);
				command.SetComputeBufferParam(this.computeShaderRCAS, 0, FSR1._RCASParameters, this.RCASParametersCB);
				command.SetComputeTextureParam(this.computeShaderRCAS, 0, FSR1.InputTexture, this.outputImage);
				command.SetComputeTextureParam(this.computeShaderRCAS, 0, FSR1.OutputTexture, this.outputImage2);
				command.DispatchCompute(this.computeShaderRCAS, 0, threadGroupsX, threadGroupsY, 1);
			}
			command.BlitFullscreenTriangle(this.Sharpening ? this.outputImage2 : this.outputImage, context.destination, false, new Rect?(new Rect(0f, 0f, (float)this.displaySize.x, (float)this.displaySize.y)), false);
			command.EndSample("FSR1");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005F38 File Offset: 0x00004138
		private float GetScaling()
		{
			if (this.qualityMode == QualityMode.Off)
			{
				return 1f;
			}
			if (this.qualityMode == QualityMode.Native)
			{
				return 1f;
			}
			if (this.qualityMode == QualityMode.Quality)
			{
				return 1.5f;
			}
			if (this.qualityMode == QualityMode.Balanced)
			{
				return 1.7f;
			}
			if (this.qualityMode == QualityMode.Performance)
			{
				return 2f;
			}
			return 3f;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005F94 File Offset: 0x00004194
		public FSR1()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006024 File Offset: 0x00004224
		// Note: this type is marked as 'beforefieldinit'.
		static FSR1()
		{
		}

		// Token: 0x040000AB RID: 171
		[Tooltip("Fallback AA for when FSR 3 is not supported")]
		public PostProcessLayer.Antialiasing fallBackAA;

		// Token: 0x040000AC RID: 172
		[Header("FSR Compute Shaders")]
		public ComputeShader computeShaderEASU;

		// Token: 0x040000AD RID: 173
		public ComputeShader computeShaderRCAS;

		// Token: 0x040000AE RID: 174
		[Header("FSR 1 Settings")]
		public QualityMode qualityMode = QualityMode.Quality;

		// Token: 0x040000AF RID: 175
		public float scaleFactor = new FloatParameter
		{
			value = 1.3f
		};

		// Token: 0x040000B0 RID: 176
		public bool Sharpening = new BoolParameter
		{
			value = true
		};

		// Token: 0x040000B1 RID: 177
		[Range(0f, 1f)]
		[Tooltip("0 = sharpest, 2 = less sharp")]
		public float sharpness = new FloatParameter
		{
			value = 0.2f
		};

		// Token: 0x040000B2 RID: 178
		private static readonly int _RCASScale = Shader.PropertyToID("_RCASScale");

		// Token: 0x040000B3 RID: 179
		private static readonly int _RCASParameters = Shader.PropertyToID("_RCASParameters");

		// Token: 0x040000B4 RID: 180
		private static readonly int _EASUViewportSize = Shader.PropertyToID("_EASUViewportSize");

		// Token: 0x040000B5 RID: 181
		private static readonly int _EASUInputImageSize = Shader.PropertyToID("_EASUInputImageSize");

		// Token: 0x040000B6 RID: 182
		private static readonly int _EASUOutputSize = Shader.PropertyToID("_EASUOutputSize");

		// Token: 0x040000B7 RID: 183
		private static readonly int _EASUParameters = Shader.PropertyToID("_EASUParameters");

		// Token: 0x040000B8 RID: 184
		private static readonly int InputTexture = Shader.PropertyToID("InputTexture");

		// Token: 0x040000B9 RID: 185
		private static readonly int OutputTexture = Shader.PropertyToID("OutputTexture");

		// Token: 0x040000BA RID: 186
		public RenderTexture outputImage;

		// Token: 0x040000BB RID: 187
		public RenderTexture outputImage2;

		// Token: 0x040000BC RID: 188
		private ComputeBuffer EASUParametersCB;

		// Token: 0x040000BD RID: 189
		private ComputeBuffer RCASParametersCB;

		// Token: 0x040000BE RID: 190
		public Vector2Int renderSize;

		// Token: 0x040000BF RID: 191
		public Vector2Int displaySize;

		// Token: 0x040000C0 RID: 192
		private QualityMode _prevQualityMode;

		// Token: 0x040000C1 RID: 193
		private Vector2Int _prevDisplaySize;

		// Token: 0x040000C2 RID: 194
		private bool _prevSharpening;

		// Token: 0x040000C3 RID: 195
		private Rect _originalRect;

		// Token: 0x040000C4 RID: 196
		[Header("MipMap Settings")]
		public bool autoTextureUpdate = true;

		// Token: 0x040000C5 RID: 197
		public float updateFrequency = 2f;

		// Token: 0x040000C6 RID: 198
		[Range(0f, 1f)]
		public float mipMapBiasOverride = 1f;

		// Token: 0x040000C7 RID: 199
		protected ulong _previousLength;

		// Token: 0x040000C8 RID: 200
		protected float _prevMipMapBias;

		// Token: 0x040000C9 RID: 201
		protected float _mipMapTimer = float.MaxValue;
	}
}
