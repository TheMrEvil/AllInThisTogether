using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace PI.NGSS
{
	// Token: 0x0200038F RID: 911
	[RequireComponent(typeof(Light))]
	[ExecuteInEditMode]
	public class NGSS_Directional : MonoBehaviour
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000B4309 File Offset: 0x000B2509
		private Light DirLight
		{
			get
			{
				if (this._DirLight == null)
				{
					this._DirLight = base.GetComponent<Light>();
				}
				return this._DirLight;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000B4334 File Offset: 0x000B2534
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x000B432B File Offset: 0x000B252B
		private Material mMaterial
		{
			get
			{
				if (this._mMaterial)
				{
					return this._mMaterial;
				}
				if (this.denoiserShader == null)
				{
					this.denoiserShader = Shader.Find("Hidden/NGSS_DenoiseShader");
				}
				this._mMaterial = new Material(this.denoiserShader);
				if (this._mMaterial)
				{
					return this._mMaterial;
				}
				Debug.LogWarning("NGSS Warning: can't find NGSS_DenoiseShader shader, make sure it's on your project.", this);
				base.enabled = false;
				return this._mMaterial;
			}
			set
			{
				this._mMaterial = value;
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000B43B0 File Offset: 0x000B25B0
		private void OnDisable()
		{
			this.isInitialized = false;
			if (this.NGSS_KEEP_ONDISABLE)
			{
				return;
			}
			if (this.isGraphicSet)
			{
				this.isGraphicSet = false;
				GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, Shader.Find("Hidden/Internal-ScreenSpaceShadows"));
				GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseBuiltin);
			}
			if (this.mMaterial)
			{
				UnityEngine.Object.DestroyImmediate(this.mMaterial);
				this.mMaterial = null;
			}
			this.RemoveCommandBuffer();
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000B4418 File Offset: 0x000B2618
		private void RemoveCommandBuffer()
		{
			if (this._computeDenoiseCB == null || this._DirLight == null)
			{
				return;
			}
			foreach (CommandBuffer commandBuffer in this._DirLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask))
			{
				if (!(commandBuffer.name != this._computeDenoiseCB.name))
				{
					this._DirLight.RemoveCommandBuffer(LightEvent.AfterScreenspaceMask, commandBuffer);
					break;
				}
			}
			this._computeDenoiseCB = null;
			foreach (CommandBuffer commandBuffer2 in this._DirLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask))
			{
				if (!(commandBuffer2.name != this._blendDenoiseCB.name))
				{
					this._DirLight.RemoveCommandBuffer(LightEvent.AfterScreenspaceMask, commandBuffer2);
					break;
				}
			}
			this._blendDenoiseCB = null;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000B44D6 File Offset: 0x000B26D6
		private void OnEnable()
		{
			if (this.IsNotSupported())
			{
				Debug.LogWarning("Unsupported graphics API, NGSS requires at least SM3.0 or higher and DX9 is not supported.", this);
				base.enabled = false;
				return;
			}
			this.Init();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000B44FC File Offset: 0x000B26FC
		private void Init()
		{
			if (this.isInitialized)
			{
				return;
			}
			if (!this.isGraphicSet)
			{
				GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseCustom);
				GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, Shader.Find("Hidden/NGSS_Directional"));
				this.isGraphicSet = true;
			}
			if (this.NGSS_NOISE_TEXTURE == null)
			{
				this.NGSS_NOISE_TEXTURE = Resources.Load<Texture2D>("BlueNoise_R8_8");
			}
			Shader.SetGlobalTexture("_BlueNoiseTextureDir", this.NGSS_NOISE_TEXTURE);
			if (this.NGSS_DENOISER_ENABLED)
			{
				this.AddCommandBuffer();
			}
			this.isInitialized = true;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000B457C File Offset: 0x000B277C
		private void AddCommandBuffer()
		{
			if (this._DirLight == null)
			{
				return;
			}
			this._computeDenoiseCB = new CommandBuffer();
			this._blendDenoiseCB = new CommandBuffer();
			this._computeDenoiseCB.name = "NGSS_Directional Denoiser Computation";
			this._blendDenoiseCB.name = "NGSS_Directional Denoiser Blending";
			int nameID = Shader.PropertyToID("NGSS_DenoiseTexture1");
			int nameID2 = Shader.PropertyToID("NGSS_DenoiseTexture2");
			this._blendDenoiseCB.Blit(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CurrentActive, this.mMaterial, 1);
			this._computeDenoiseCB.GetTemporaryRT(nameID, -1, -1, 0, FilterMode.Bilinear, RenderTextureFormat.R8);
			this._computeDenoiseCB.GetTemporaryRT(nameID2, -1, -1, 0, FilterMode.Bilinear, RenderTextureFormat.R8);
			for (int i = 1; i <= this.NGSS_DENOISER_PASSES; i++)
			{
				this._computeDenoiseCB.SetGlobalVector(this._denoiseKernelID, new Vector2(0f, 1f));
				if (i == 1)
				{
					this._computeDenoiseCB.Blit(BuiltinRenderTextureType.CurrentActive, nameID2, this.mMaterial, 0);
				}
				else
				{
					this._computeDenoiseCB.Blit(nameID, nameID2, this.mMaterial, 0);
				}
				this._computeDenoiseCB.SetGlobalVector(this._denoiseKernelID, new Vector2(1f, 0f));
				this._computeDenoiseCB.Blit(nameID2, nameID, this.mMaterial, 0);
			}
			this._computeDenoiseCB.SetGlobalTexture(this._denoiseTextureID, nameID);
			this._computeDenoiseCB.ReleaseTemporaryRT(nameID);
			this._computeDenoiseCB.ReleaseTemporaryRT(nameID2);
			this.mMaterial.SetFloat(this._denoiseEdgeToleranceID, this.NGSS_DENOISER_EDGE_SOFTNESS);
			this.mMaterial.SetFloat(this._denoiseSoftnessID, this.NGSS_DENOISER_SOFTNESS);
			bool flag = true;
			CommandBuffer[] commandBuffers = this._DirLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask);
			for (int j = 0; j < commandBuffers.Length; j++)
			{
				if (!(commandBuffers[j].name != this._computeDenoiseCB.name))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this._DirLight.AddCommandBuffer(LightEvent.AfterScreenspaceMask, this._computeDenoiseCB);
			}
			flag = true;
			commandBuffers = this._DirLight.GetCommandBuffers(LightEvent.AfterScreenspaceMask);
			for (int j = 0; j < commandBuffers.Length; j++)
			{
				if (!(commandBuffers[j].name != this._blendDenoiseCB.name))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this._DirLight.AddCommandBuffer(LightEvent.AfterScreenspaceMask, this._blendDenoiseCB);
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000B47F4 File Offset: 0x000B29F4
		private bool IsNotSupported()
		{
			return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000B4800 File Offset: 0x000B2A00
		private void Update()
		{
			if (Application.isPlaying && this.NGSS_NO_UPDATE_ON_PLAY && this.isSetup)
			{
				return;
			}
			if (this.DirLight.shadows == LightShadows.None || this.DirLight.type != LightType.Directional)
			{
				return;
			}
			Shader.SetGlobalFloat(this._ngssDirSamplingDistanceid, this.NGSS_SAMPLING_DISTANCE);
			this.NGSS_SAMPLING_TEST = Mathf.Clamp(this.NGSS_SAMPLING_TEST, 4, this.NGSS_SAMPLING_FILTER);
			Shader.SetGlobalFloat(this._ngssTestSamplersDirid, (float)this.NGSS_SAMPLING_TEST);
			Shader.SetGlobalFloat(this._ngssFilterSamplersDirid, (float)this.NGSS_SAMPLING_FILTER);
			Shader.SetGlobalFloat(this._ngssGlobalSoftnessid, (QualitySettings.shadowProjection == ShadowProjection.CloseFit) ? this.NGSS_SHADOWS_SOFTNESS : (this.NGSS_SHADOWS_SOFTNESS * 2f / (QualitySettings.shadowDistance * 0.66f) * ((QualitySettings.shadowCascades == 2) ? 1.5f : ((QualitySettings.shadowCascades == 4) ? 1f : 0.25f))));
			Shader.SetGlobalFloat(this._ngssGlobalSoftnessOptimizedid, this.NGSS_SHADOWS_SOFTNESS / QualitySettings.shadowDistance);
			int num = (int)Mathf.Sqrt((float)this.NGSS_SAMPLING_FILTER);
			Shader.SetGlobalInt(this._ngssOptimizedIterationsid, (num % 2 == 0) ? (num + 1) : num);
			Shader.SetGlobalInt(this._ngssOptimizedSamplersid, this.NGSS_SAMPLING_FILTER);
			if (this._DENOISER_ENABLED != this.NGSS_DENOISER_ENABLED)
			{
				this._DENOISER_ENABLED = this.NGSS_DENOISER_ENABLED;
				this.RemoveCommandBuffer();
				if (this.NGSS_DENOISER_ENABLED)
				{
					this.AddCommandBuffer();
				}
			}
			if (this._DENOISER_PASSES != this.NGSS_DENOISER_PASSES)
			{
				this._DENOISER_PASSES = this.NGSS_DENOISER_PASSES;
				this.RemoveCommandBuffer();
				if (this.NGSS_DENOISER_ENABLED)
				{
					this.AddCommandBuffer();
				}
			}
			if (this.NGSS_DENOISER_ENABLED)
			{
				this.mMaterial.SetFloat(this._denoiseEdgeToleranceID, this.NGSS_DENOISER_EDGE_SOFTNESS);
				this.mMaterial.SetFloat(this._denoiseSoftnessID, this.NGSS_DENOISER_SOFTNESS);
			}
			if (this.NGSS_RECEIVER_PLANE_BIAS)
			{
				Shader.EnableKeyword("NGSS_USE_RECEIVER_PLANE_BIAS");
			}
			else
			{
				Shader.DisableKeyword("NGSS_USE_RECEIVER_PLANE_BIAS");
			}
			Shader.SetGlobalFloat(this._ngssNoiseToDitheringScaleDirid, (float)this.NGSS_NOISE_TO_DITHERING_SCALE);
			if (this.NGSS_PCSS_ENABLED)
			{
				float num2 = this.NGSS_PCSS_SOFTNESS_NEAR * 0.25f;
				float num3 = this.NGSS_PCSS_SOFTNESS_FAR * 0.25f;
				Shader.SetGlobalFloat(this._ngssPcssFilterDirMinid, (num2 > num3) ? num3 : num2);
				Shader.SetGlobalFloat(this._ngssPcssFilterDirMaxid, (num3 < num2) ? num2 : num3);
				Shader.EnableKeyword("NGSS_PCSS_FILTER_DIR");
			}
			else
			{
				Shader.DisableKeyword("NGSS_PCSS_FILTER_DIR");
			}
			if (this.NGSS_SHADOWS_RESOLUTION != NGSS_Directional.ShadowMapResolution.UseQualitySettings)
			{
				this.DirLight.shadowCustomResolution = (int)this.NGSS_SHADOWS_RESOLUTION;
			}
			else
			{
				this.DirLight.shadowCustomResolution = 0;
				this.DirLight.shadowResolution = LightShadowResolution.FromQualitySettings;
			}
			if (QualitySettings.shadowCascades > 1)
			{
				Shader.SetGlobalFloat(this._ngssCascadesSoftnessNormalizationid, this.NGSS_CASCADES_SOFTNESS_NORMALIZATION);
				Shader.SetGlobalFloat(this._ngssCascadesCountid, (float)QualitySettings.shadowCascades);
				Shader.SetGlobalVector(this._ngssCascadesSplitsid, (QualitySettings.shadowCascades == 2) ? new Vector4(QualitySettings.shadowCascade2Split, 1f, 1f, 1f) : new Vector4(QualitySettings.shadowCascade4Split.x, QualitySettings.shadowCascade4Split.y, QualitySettings.shadowCascade4Split.z, 1f));
			}
			if (this.NGSS_CASCADES_BLENDING && QualitySettings.shadowCascades > 1)
			{
				Shader.EnableKeyword("NGSS_USE_CASCADE_BLENDING");
				Shader.SetGlobalFloat(this._ngssCascadeBlendDistanceid, this.NGSS_CASCADES_BLENDING_VALUE * 0.125f);
			}
			else
			{
				Shader.DisableKeyword("NGSS_USE_CASCADE_BLENDING");
			}
			this.isSetup = true;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000B4B4C File Offset: 0x000B2D4C
		public NGSS_Directional()
		{
		}

		// Token: 0x04001E59 RID: 7769
		[Header("MAIN SETTINGS")]
		public Shader denoiserShader;

		// Token: 0x04001E5A RID: 7770
		[Tooltip("If disabled, NGSS Directional shadows replacement will be removed from Graphics settings when OnDisable is called in this component.")]
		public bool NGSS_KEEP_ONDISABLE = true;

		// Token: 0x04001E5B RID: 7771
		[Tooltip("Check this option if you don't need to update shadows variables at runtime, only once when scene loads.\nUseful to save some CPU cycles.")]
		public bool NGSS_NO_UPDATE_ON_PLAY;

		// Token: 0x04001E5C RID: 7772
		[Tooltip("Shadows resolution.\nUseQualitySettings = From Quality Settings, SuperLow = 512, Low = 1024, Med = 2048, High = 4096, Ultra = 8192, Mega = 16384.")]
		public NGSS_Directional.ShadowMapResolution NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;

		// Token: 0x04001E5D RID: 7773
		[Header("BASE SAMPLING")]
		[Tooltip("Used to test blocker search and early bail out algorithms. Keep it as low as possible, might lead to white noise if too low.\nRecommended values: Mobile = 8, Consoles & VR = 16, Desktop = 24")]
		[Range(4f, 32f)]
		public int NGSS_SAMPLING_TEST = 16;

		// Token: 0x04001E5E RID: 7774
		[Tooltip("Number of samplers per pixel used for PCF and PCSS shadows algorithms.\nRecommended values: Mobile = 16, Consoles & VR = 32, Desktop Med = 48, Desktop High = 64, Desktop Ultra = 128")]
		[Range(8f, 128f)]
		public int NGSS_SAMPLING_FILTER = 48;

		// Token: 0x04001E5F RID: 7775
		[Tooltip("New optimization that reduces sampling over distance. Interpolates current sampling set (TEST and FILTER) down to 4spp when reaching this distance.")]
		[Range(0f, 500f)]
		public float NGSS_SAMPLING_DISTANCE = 75f;

		// Token: 0x04001E60 RID: 7776
		[Header("SHADOW SOFTNESS")]
		[Tooltip("Overall shadows softness.")]
		[Range(0f, 3f)]
		public float NGSS_SHADOWS_SOFTNESS = 1f;

		// Token: 0x04001E61 RID: 7777
		[Header("PCSS")]
		[Tooltip("PCSS Requires inline sampling and SM3.5.\nProvides Area Light soft-shadows.\nDisable it if you are looking for PCF filtering (uniform soft-shadows) which runs with SM3.0.")]
		public bool NGSS_PCSS_ENABLED;

		// Token: 0x04001E62 RID: 7778
		[Tooltip("How soft shadows are when close to caster.")]
		[Range(0f, 2f)]
		public float NGSS_PCSS_SOFTNESS_NEAR = 0.125f;

		// Token: 0x04001E63 RID: 7779
		[Tooltip("How soft shadows are when far from caster.")]
		[Range(0f, 2f)]
		public float NGSS_PCSS_SOFTNESS_FAR = 1f;

		// Token: 0x04001E64 RID: 7780
		[Header("NOISE")]
		[Tooltip("If zero = 100% noise.\nIf one = 100% dithering.\nUseful when fighting banding.")]
		[Range(0f, 1f)]
		public int NGSS_NOISE_TO_DITHERING_SCALE;

		// Token: 0x04001E65 RID: 7781
		[Tooltip("If you set the noise scale value to something less than 1 you need to input a noise texture.\nRecommended noise textures are blue noise signals.")]
		public Texture2D NGSS_NOISE_TEXTURE;

		// Token: 0x04001E66 RID: 7782
		[Header("DENOISER")]
		[Tooltip("Separable low pass filter that help fight artifacts and noise in shadows.\nRequires Cascaded Shadows to be enabled in the Editor Graphics Settings.")]
		public bool NGSS_DENOISER_ENABLED = true;

		// Token: 0x04001E67 RID: 7783
		[Tooltip("How many iterations the Denoiser algorithm should do.")]
		[Range(1f, 4f)]
		public int NGSS_DENOISER_PASSES = 1;

		// Token: 0x04001E68 RID: 7784
		[Tooltip("Overall Denoiser softness.")]
		[Range(0.01f, 1f)]
		public float NGSS_DENOISER_SOFTNESS = 1f;

		// Token: 0x04001E69 RID: 7785
		[Tooltip("The amount of shadow edges the Denoiser can tolerate during denoising.")]
		[Range(0.01f, 1f)]
		public float NGSS_DENOISER_EDGE_SOFTNESS = 1f;

		// Token: 0x04001E6A RID: 7786
		[Header("BIAS")]
		[Tooltip("This estimates receiver slope using derivatives and tries to tilt the filtering kernel along it.\nHowever, when doing it in screenspace from the depth texture can leads to shadow artifacts.\nThus it is disabled by default.")]
		public bool NGSS_RECEIVER_PLANE_BIAS;

		// Token: 0x04001E6B RID: 7787
		[Header("CASCADES")]
		[Tooltip("Blends cascades at seams intersection.\nAdditional overhead required for this option.")]
		public bool NGSS_CASCADES_BLENDING = true;

		// Token: 0x04001E6C RID: 7788
		[Tooltip("Tweak this value to adjust the blending transition between cascades.")]
		[Range(0f, 2f)]
		public float NGSS_CASCADES_BLENDING_VALUE = 1f;

		// Token: 0x04001E6D RID: 7789
		[Range(0f, 1f)]
		[Tooltip("If one, softness across cascades will be matched using splits distribution, resulting in realistic soft-ness over distance.\nIf zero the softness distribution will be based on cascade index, resulting in blurrier shadows over distance thus less realistic.")]
		public float NGSS_CASCADES_SOFTNESS_NORMALIZATION = 1f;

		// Token: 0x04001E6E RID: 7790
		private int _ngssDirSamplingDistanceid = Shader.PropertyToID("NGSS_DIR_SAMPLING_DISTANCE");

		// Token: 0x04001E6F RID: 7791
		private int _ngssTestSamplersDirid = Shader.PropertyToID("NGSS_TEST_SAMPLERS_DIR");

		// Token: 0x04001E70 RID: 7792
		private int _ngssFilterSamplersDirid = Shader.PropertyToID("NGSS_FILTER_SAMPLERS_DIR");

		// Token: 0x04001E71 RID: 7793
		private int _ngssGlobalSoftnessid = Shader.PropertyToID("NGSS_GLOBAL_SOFTNESS");

		// Token: 0x04001E72 RID: 7794
		private int _ngssGlobalSoftnessOptimizedid = Shader.PropertyToID("NGSS_GLOBAL_SOFTNESS_OPTIMIZED");

		// Token: 0x04001E73 RID: 7795
		private int _ngssOptimizedIterationsid = Shader.PropertyToID("NGSS_OPTIMIZED_ITERATIONS");

		// Token: 0x04001E74 RID: 7796
		private int _ngssOptimizedSamplersid = Shader.PropertyToID("NGSS_OPTIMIZED_SAMPLERS");

		// Token: 0x04001E75 RID: 7797
		private int _ngssNoiseToDitheringScaleDirid = Shader.PropertyToID("NGSS_NOISE_TO_DITHERING_SCALE_DIR");

		// Token: 0x04001E76 RID: 7798
		private int _ngssPcssFilterDirMinid = Shader.PropertyToID("NGSS_PCSS_FILTER_DIR_MIN");

		// Token: 0x04001E77 RID: 7799
		private int _ngssPcssFilterDirMaxid = Shader.PropertyToID("NGSS_PCSS_FILTER_DIR_MAX");

		// Token: 0x04001E78 RID: 7800
		private int _ngssCascadesSoftnessNormalizationid = Shader.PropertyToID("NGSS_CASCADES_SOFTNESS_NORMALIZATION");

		// Token: 0x04001E79 RID: 7801
		private int _ngssCascadesCountid = Shader.PropertyToID("NGSS_CASCADES_COUNT");

		// Token: 0x04001E7A RID: 7802
		private int _ngssCascadesSplitsid = Shader.PropertyToID("NGSS_CASCADES_SPLITS");

		// Token: 0x04001E7B RID: 7803
		private int _ngssCascadeBlendDistanceid = Shader.PropertyToID("NGSS_CASCADE_BLEND_DISTANCE");

		// Token: 0x04001E7C RID: 7804
		private int _denoiseKernelID = Shader.PropertyToID("DenoiseKernel");

		// Token: 0x04001E7D RID: 7805
		private int _denoiseSoftnessID = Shader.PropertyToID("DenoiseSoftness");

		// Token: 0x04001E7E RID: 7806
		private int _denoiseTextureID = Shader.PropertyToID("NGSS_DenoiseTexture");

		// Token: 0x04001E7F RID: 7807
		private int _denoiseEdgeToleranceID = Shader.PropertyToID("DenoiseEdgeTolerance");

		// Token: 0x04001E80 RID: 7808
		private CommandBuffer _computeDenoiseCB;

		// Token: 0x04001E81 RID: 7809
		private CommandBuffer _blendDenoiseCB;

		// Token: 0x04001E82 RID: 7810
		private bool _DENOISER_ENABLED;

		// Token: 0x04001E83 RID: 7811
		private int _DENOISER_PASSES = 1;

		// Token: 0x04001E84 RID: 7812
		private bool isSetup;

		// Token: 0x04001E85 RID: 7813
		private bool isInitialized;

		// Token: 0x04001E86 RID: 7814
		private bool isGraphicSet;

		// Token: 0x04001E87 RID: 7815
		private Light _DirLight;

		// Token: 0x04001E88 RID: 7816
		private Material _mMaterial;

		// Token: 0x02000688 RID: 1672
		public enum ShadowMapResolution
		{
			// Token: 0x04002C01 RID: 11265
			UseQualitySettings = 256,
			// Token: 0x04002C02 RID: 11266
			VeryLow = 512,
			// Token: 0x04002C03 RID: 11267
			Low = 1024,
			// Token: 0x04002C04 RID: 11268
			Med = 2048,
			// Token: 0x04002C05 RID: 11269
			High = 4096,
			// Token: 0x04002C06 RID: 11270
			Ultra = 8192,
			// Token: 0x04002C07 RID: 11271
			Mega = 16384
		}
	}
}
