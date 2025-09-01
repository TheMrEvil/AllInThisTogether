using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace HorizonBasedAmbientOcclusion
{
	// Token: 0x02000002 RID: 2
	[ExecuteInEditMode]
	[ImageEffectAllowedInSceneView]
	[AddComponentMenu("Image Effects/HBAO")]
	[RequireComponent(typeof(Camera))]
	public class HBAO : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public HBAO.Presets presets
		{
			get
			{
				return this.m_Presets;
			}
			set
			{
				this.m_Presets = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public HBAO.GeneralSettings generalSettings
		{
			get
			{
				return this.m_GeneralSettings;
			}
			set
			{
				this.m_GeneralSettings = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public HBAO.AOSettings aoSettings
		{
			get
			{
				return this.m_AOSettings;
			}
			set
			{
				this.m_AOSettings = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public HBAO.TemporalFilterSettings temporalFilterSettings
		{
			get
			{
				return this.m_TemporalFilterSettings;
			}
			set
			{
				this.m_TemporalFilterSettings = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public HBAO.BlurSettings blurSettings
		{
			get
			{
				return this.m_BlurSettings;
			}
			set
			{
				this.m_BlurSettings = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		public HBAO.ColorBleedingSettings colorBleedingSettings
		{
			get
			{
				return this.m_ColorBleedingSettings;
			}
			set
			{
				this.m_ColorBleedingSettings = value;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020B6 File Offset: 0x000002B6
		public HBAO.Preset GetCurrentPreset()
		{
			return this.m_Presets.preset;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020C4 File Offset: 0x000002C4
		public void ApplyPreset(HBAO.Preset preset)
		{
			if (preset == HBAO.Preset.Custom)
			{
				this.m_Presets.preset = preset;
				return;
			}
			HBAO.DebugMode debugMode = this.generalSettings.debugMode;
			this.m_GeneralSettings = HBAO.GeneralSettings.defaults;
			this.m_AOSettings = HBAO.AOSettings.defaults;
			this.m_ColorBleedingSettings = HBAO.ColorBleedingSettings.defaults;
			this.m_BlurSettings = HBAO.BlurSettings.defaults;
			this.SetDebugMode(debugMode);
			switch (preset)
			{
			case HBAO.Preset.FastestPerformance:
				this.SetQuality(HBAO.Quality.Lowest);
				this.SetAoRadius(0.5f);
				this.SetAoMaxRadiusPixels(64f);
				this.SetBlurType(HBAO.BlurType.ExtraWide);
				break;
			case HBAO.Preset.FastPerformance:
				this.SetQuality(HBAO.Quality.Low);
				this.SetAoRadius(0.5f);
				this.SetAoMaxRadiusPixels(64f);
				this.SetBlurType(HBAO.BlurType.Wide);
				break;
			case HBAO.Preset.HighQuality:
				this.SetQuality(HBAO.Quality.High);
				this.SetAoRadius(1f);
				break;
			case HBAO.Preset.HighestQuality:
				this.SetQuality(HBAO.Quality.Highest);
				this.SetAoRadius(1.2f);
				this.SetAoMaxRadiusPixels(256f);
				this.SetBlurType(HBAO.BlurType.Narrow);
				break;
			}
			this.m_Presets.preset = preset;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D0 File Offset: 0x000003D0
		public HBAO.PipelineStage GetPipelineStage()
		{
			return this.m_GeneralSettings.pipelineStage;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021DD File Offset: 0x000003DD
		public void SetPipelineStage(HBAO.PipelineStage pipelineStage)
		{
			this.m_GeneralSettings.pipelineStage = pipelineStage;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021EB File Offset: 0x000003EB
		public HBAO.Quality GetQuality()
		{
			return this.m_GeneralSettings.quality;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F8 File Offset: 0x000003F8
		public void SetQuality(HBAO.Quality quality)
		{
			this.m_GeneralSettings.quality = quality;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002206 File Offset: 0x00000406
		public HBAO.Deinterleaving GetDeinterleaving()
		{
			return this.m_GeneralSettings.deinterleaving;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002213 File Offset: 0x00000413
		public void SetDeinterleaving(HBAO.Deinterleaving deinterleaving)
		{
			this.m_GeneralSettings.deinterleaving = deinterleaving;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002221 File Offset: 0x00000421
		public HBAO.Resolution GetResolution()
		{
			return this.m_GeneralSettings.resolution;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000222E File Offset: 0x0000042E
		public void SetResolution(HBAO.Resolution resolution)
		{
			this.m_GeneralSettings.resolution = resolution;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000223C File Offset: 0x0000043C
		public HBAO.NoiseType GetNoiseType()
		{
			return this.m_GeneralSettings.noiseType;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002249 File Offset: 0x00000449
		public void SetNoiseType(HBAO.NoiseType noiseType)
		{
			this.m_GeneralSettings.noiseType = noiseType;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002257 File Offset: 0x00000457
		public HBAO.DebugMode GetDebugMode()
		{
			return this.m_GeneralSettings.debugMode;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002264 File Offset: 0x00000464
		public void SetDebugMode(HBAO.DebugMode debugMode)
		{
			this.m_GeneralSettings.debugMode = debugMode;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002272 File Offset: 0x00000472
		public float GetAoRadius()
		{
			return this.m_AOSettings.radius;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000227F File Offset: 0x0000047F
		public void SetAoRadius(float radius)
		{
			this.m_AOSettings.radius = Mathf.Clamp(radius, 0.25f, 5f);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000229C File Offset: 0x0000049C
		public float GetAoMaxRadiusPixels()
		{
			return this.m_AOSettings.maxRadiusPixels;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022A9 File Offset: 0x000004A9
		public void SetAoMaxRadiusPixels(float maxRadiusPixels)
		{
			this.m_AOSettings.maxRadiusPixels = Mathf.Clamp(maxRadiusPixels, 16f, 256f);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022C6 File Offset: 0x000004C6
		public float GetAoBias()
		{
			return this.m_AOSettings.bias;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022D3 File Offset: 0x000004D3
		public void SetAoBias(float bias)
		{
			this.m_AOSettings.bias = Mathf.Clamp(bias, 0f, 0.5f);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022F0 File Offset: 0x000004F0
		public float GetAoOffscreenSamplesContribution()
		{
			return this.m_AOSettings.offscreenSamplesContribution;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022FD File Offset: 0x000004FD
		public void SetAoOffscreenSamplesContribution(float contribution)
		{
			this.m_AOSettings.offscreenSamplesContribution = Mathf.Clamp01(contribution);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002310 File Offset: 0x00000510
		public float GetAoMaxDistance()
		{
			return this.m_AOSettings.maxDistance;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000231D File Offset: 0x0000051D
		public void SetAoMaxDistance(float maxDistance)
		{
			this.m_AOSettings.maxDistance = maxDistance;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000232B File Offset: 0x0000052B
		public float GetAoDistanceFalloff()
		{
			return this.m_AOSettings.distanceFalloff;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002338 File Offset: 0x00000538
		public void SetAoDistanceFalloff(float distanceFalloff)
		{
			this.m_AOSettings.distanceFalloff = distanceFalloff;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002346 File Offset: 0x00000546
		public HBAO.PerPixelNormals GetAoPerPixelNormals()
		{
			return this.m_AOSettings.perPixelNormals;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002353 File Offset: 0x00000553
		public void SetAoPerPixelNormals(HBAO.PerPixelNormals perPixelNormals)
		{
			this.m_AOSettings.perPixelNormals = perPixelNormals;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002361 File Offset: 0x00000561
		public Color GetAoColor()
		{
			return this.m_AOSettings.baseColor;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000236E File Offset: 0x0000056E
		public void SetAoColor(Color color)
		{
			this.m_AOSettings.baseColor = color;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000237C File Offset: 0x0000057C
		public float GetAoIntensity()
		{
			return this.m_AOSettings.intensity;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002389 File Offset: 0x00000589
		public void SetAoIntensity(float intensity)
		{
			this.m_AOSettings.intensity = Mathf.Clamp(intensity, 0f, 4f);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023A6 File Offset: 0x000005A6
		public bool UseMultiBounce()
		{
			return this.m_AOSettings.useMultiBounce;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023B3 File Offset: 0x000005B3
		public void EnableMultiBounce(bool enabled = true)
		{
			this.m_AOSettings.useMultiBounce = enabled;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023C1 File Offset: 0x000005C1
		public float GetAoMultiBounceInfluence()
		{
			return this.m_AOSettings.multiBounceInfluence;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023CE File Offset: 0x000005CE
		public void SetAoMultiBounceInfluence(float multiBounceInfluence)
		{
			this.m_AOSettings.multiBounceInfluence = Mathf.Clamp01(multiBounceInfluence);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000023E1 File Offset: 0x000005E1
		public bool IsTemporalFilterEnabled()
		{
			return this.m_TemporalFilterSettings.enabled;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023EE File Offset: 0x000005EE
		public void EnableTemporalFilter(bool enabled = true)
		{
			this.m_TemporalFilterSettings.enabled = enabled;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000023FC File Offset: 0x000005FC
		public HBAO.VarianceClipping GetTemporalFilterVarianceClipping()
		{
			return this.m_TemporalFilterSettings.varianceClipping;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002409 File Offset: 0x00000609
		public void SetTemporalFilterVarianceClipping(HBAO.VarianceClipping varianceClipping)
		{
			this.m_TemporalFilterSettings.varianceClipping = varianceClipping;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002417 File Offset: 0x00000617
		public HBAO.BlurType GetBlurType()
		{
			return this.m_BlurSettings.type;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002424 File Offset: 0x00000624
		public void SetBlurType(HBAO.BlurType blurType)
		{
			this.m_BlurSettings.type = blurType;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002432 File Offset: 0x00000632
		public float GetBlurSharpness()
		{
			return this.m_BlurSettings.sharpness;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000243F File Offset: 0x0000063F
		public void SetBlurSharpness(float sharpness)
		{
			this.m_BlurSettings.sharpness = Mathf.Clamp(sharpness, 0f, 16f);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000245C File Offset: 0x0000065C
		public bool IsColorBleedingEnabled()
		{
			return this.m_ColorBleedingSettings.enabled;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002469 File Offset: 0x00000669
		public void EnableColorBleeding(bool enabled = true)
		{
			this.m_ColorBleedingSettings.enabled = enabled;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002477 File Offset: 0x00000677
		public float GetColorBleedingSaturation()
		{
			return this.m_ColorBleedingSettings.saturation;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002484 File Offset: 0x00000684
		public void SetColorBleedingSaturation(float saturation)
		{
			this.m_ColorBleedingSettings.saturation = Mathf.Clamp(saturation, 0f, 4f);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024A1 File Offset: 0x000006A1
		public float GetColorBleedingAlbedoMultiplier()
		{
			return this.m_ColorBleedingSettings.albedoMultiplier;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024AE File Offset: 0x000006AE
		public void SetColorBleedingAlbedoMultiplier(float albedoMultiplier)
		{
			this.m_ColorBleedingSettings.albedoMultiplier = Mathf.Clamp(albedoMultiplier, 0f, 32f);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000024CB File Offset: 0x000006CB
		public float GetColorBleedingBrightnessMask()
		{
			return this.m_ColorBleedingSettings.brightnessMask;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000024D8 File Offset: 0x000006D8
		public void SetColorBleedingBrightnessMask(float brightnessMask)
		{
			this.m_ColorBleedingSettings.brightnessMask = Mathf.Clamp01(brightnessMask);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000024EB File Offset: 0x000006EB
		public Vector2 GetColorBleedingBrightnessMaskRange()
		{
			return this.m_ColorBleedingSettings.brightnessMaskRange;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000024F8 File Offset: 0x000006F8
		public void SetColorBleedingBrightnessMaskRange(Vector2 brightnessMaskRange)
		{
			brightnessMaskRange.x = Mathf.Clamp(brightnessMaskRange.x, 0f, 2f);
			brightnessMaskRange.y = Mathf.Clamp(brightnessMaskRange.y, 0f, 2f);
			brightnessMaskRange.x = Mathf.Min(brightnessMaskRange.x, brightnessMaskRange.y);
			this.m_ColorBleedingSettings.brightnessMaskRange = brightnessMaskRange;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002561 File Offset: 0x00000761
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002569 File Offset: 0x00000769
		private Material material
		{
			[CompilerGenerated]
			get
			{
				return this.<material>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<material>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002572 File Offset: 0x00000772
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000257A File Offset: 0x0000077A
		private Camera hbaoCamera
		{
			[CompilerGenerated]
			get
			{
				return this.<hbaoCamera>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<hbaoCamera>k__BackingField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002583 File Offset: 0x00000783
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000258B File Offset: 0x0000078B
		private CommandBuffer cmdBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<cmdBuffer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<cmdBuffer>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000259C File Offset: 0x0000079C
		private int width
		{
			[CompilerGenerated]
			get
			{
				return this.<width>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<width>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000025AD File Offset: 0x000007AD
		private int height
		{
			[CompilerGenerated]
			get
			{
				return this.<height>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<height>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000025B6 File Offset: 0x000007B6
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000025BE File Offset: 0x000007BE
		private bool stereoActive
		{
			[CompilerGenerated]
			get
			{
				return this.<stereoActive>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<stereoActive>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000025C7 File Offset: 0x000007C7
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000025CF File Offset: 0x000007CF
		private int numberOfEyes
		{
			[CompilerGenerated]
			get
			{
				return this.<numberOfEyes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<numberOfEyes>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000025D8 File Offset: 0x000007D8
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000025E0 File Offset: 0x000007E0
		private int xrActiveEye
		{
			[CompilerGenerated]
			get
			{
				return this.<xrActiveEye>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<xrActiveEye>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000025E9 File Offset: 0x000007E9
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000025F1 File Offset: 0x000007F1
		private int screenWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<screenWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<screenWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000025FA File Offset: 0x000007FA
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002602 File Offset: 0x00000802
		private int screenHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<screenHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<screenHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000260B File Offset: 0x0000080B
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002613 File Offset: 0x00000813
		private int aoWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<aoWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<aoWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000261C File Offset: 0x0000081C
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002624 File Offset: 0x00000824
		private int aoHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<aoHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<aoHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000262D File Offset: 0x0000082D
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002635 File Offset: 0x00000835
		private int reinterleavedAoWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<reinterleavedAoWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reinterleavedAoWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000263E File Offset: 0x0000083E
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002646 File Offset: 0x00000846
		private int reinterleavedAoHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<reinterleavedAoHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reinterleavedAoHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000264F File Offset: 0x0000084F
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002657 File Offset: 0x00000857
		private int deinterleavedAoWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<deinterleavedAoWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<deinterleavedAoWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002668 File Offset: 0x00000868
		private int deinterleavedAoHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<deinterleavedAoHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<deinterleavedAoHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002679 File Offset: 0x00000879
		private int frameCount
		{
			[CompilerGenerated]
			get
			{
				return this.<frameCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<frameCount>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002682 File Offset: 0x00000882
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000268A File Offset: 0x0000088A
		private bool motionVectorsSupported
		{
			[CompilerGenerated]
			get
			{
				return this.<motionVectorsSupported>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<motionVectorsSupported>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000269B File Offset: 0x0000089B
		private RenderTexture aoHistoryBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<aoHistoryBuffer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<aoHistoryBuffer>k__BackingField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000026A4 File Offset: 0x000008A4
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000026AC File Offset: 0x000008AC
		private RenderTexture colorBleedingHistoryBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<colorBleedingHistoryBuffer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<colorBleedingHistoryBuffer>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000026BD File Offset: 0x000008BD
		private Texture2D noiseTex
		{
			[CompilerGenerated]
			get
			{
				return this.<noiseTex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<noiseTex>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000026C8 File Offset: 0x000008C8
		private Mesh fullscreenTriangle
		{
			get
			{
				if (this.m_FullscreenTriangle != null)
				{
					return this.m_FullscreenTriangle;
				}
				this.m_FullscreenTriangle = new Mesh
				{
					name = "Fullscreen Triangle"
				};
				this.m_FullscreenTriangle.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 3f, 0f),
					new Vector3(3f, -1f, 0f)
				});
				this.m_FullscreenTriangle.SetIndices(new int[]
				{
					0,
					1,
					2
				}, MeshTopology.Triangles, 0, false);
				this.m_FullscreenTriangle.UploadMeshData(false);
				return this.m_FullscreenTriangle;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000278C File Offset: 0x0000098C
		private CameraEvent cameraEvent
		{
			get
			{
				if (this.generalSettings.debugMode != HBAO.DebugMode.Disabled)
				{
					return CameraEvent.BeforeImageEffectsOpaque;
				}
				switch (this.generalSettings.pipelineStage)
				{
				case HBAO.PipelineStage.AfterLighting:
					return CameraEvent.AfterLighting;
				case HBAO.PipelineStage.BeforeReflections:
					return CameraEvent.BeforeReflections;
				}
				return CameraEvent.BeforeImageEffectsOpaque;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000027D0 File Offset: 0x000009D0
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000029FE File Offset: 0x00000BFE
		private bool isCommandBufferDirty
		{
			get
			{
				if (!this.m_IsCommandBufferDirty)
				{
					HBAO.PipelineStage? previousPipelineStage = this.m_PreviousPipelineStage;
					HBAO.PipelineStage pipelineStage = this.generalSettings.pipelineStage;
					if (previousPipelineStage.GetValueOrDefault() == pipelineStage & previousPipelineStage != null)
					{
						HBAO.Resolution? previousResolution = this.m_PreviousResolution;
						HBAO.Resolution resolution = this.generalSettings.resolution;
						if (previousResolution.GetValueOrDefault() == resolution & previousResolution != null)
						{
							HBAO.DebugMode? previousDebugMode = this.m_PreviousDebugMode;
							HBAO.DebugMode debugMode = this.generalSettings.debugMode;
							if ((previousDebugMode.GetValueOrDefault() == debugMode & previousDebugMode != null) && this.m_PreviousAllowHDR == this.hbaoCamera.allowHDR && this.m_PreviousWidth == this.width && this.m_PreviousHeight == this.height)
							{
								HBAO.Deinterleaving? previousDeinterleaving = this.m_PreviousDeinterleaving;
								HBAO.Deinterleaving deinterleaving = this.generalSettings.deinterleaving;
								if (previousDeinterleaving.GetValueOrDefault() == deinterleaving & previousDeinterleaving != null)
								{
									HBAO.BlurType? previousBlurAmount = this.m_PreviousBlurAmount;
									HBAO.BlurType type = this.blurSettings.type;
									if ((previousBlurAmount.GetValueOrDefault() == type & previousBlurAmount != null) && this.m_PreviousColorBleedingEnabled == this.colorBleedingSettings.enabled && this.m_PreviousTemporalFilterEnabled == this.temporalFilterSettings.enabled && this.m_PreviousRenderingPath == this.hbaoCamera.actualRenderingPath)
									{
										return false;
									}
								}
							}
						}
					}
				}
				this.m_PreviousPipelineStage = new HBAO.PipelineStage?(this.generalSettings.pipelineStage);
				this.m_PreviousResolution = new HBAO.Resolution?(this.generalSettings.resolution);
				this.m_PreviousDebugMode = new HBAO.DebugMode?(this.generalSettings.debugMode);
				this.m_PreviousAllowHDR = this.hbaoCamera.allowHDR;
				this.m_PreviousWidth = this.width;
				this.m_PreviousHeight = this.height;
				this.m_PreviousDeinterleaving = new HBAO.Deinterleaving?(this.generalSettings.deinterleaving);
				this.m_PreviousBlurAmount = new HBAO.BlurType?(this.blurSettings.type);
				this.m_PreviousColorBleedingEnabled = this.colorBleedingSettings.enabled;
				this.m_PreviousTemporalFilterEnabled = this.temporalFilterSettings.enabled;
				this.m_PreviousRenderingPath = this.hbaoCamera.actualRenderingPath;
				return true;
			}
			set
			{
				this.m_IsCommandBufferDirty = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002A07 File Offset: 0x00000C07
		private static RenderTextureFormat defaultHDRRenderTextureFormat
		{
			get
			{
				return RenderTextureFormat.DefaultHDR;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002A0B File Offset: 0x00000C0B
		private RenderTextureFormat sourceFormat
		{
			get
			{
				if (!this.hbaoCamera.allowHDR)
				{
					return RenderTextureFormat.Default;
				}
				return HBAO.defaultHDRRenderTextureFormat;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002A21 File Offset: 0x00000C21
		private static RenderTextureFormat colorFormat
		{
			get
			{
				if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
				{
					return RenderTextureFormat.Default;
				}
				return RenderTextureFormat.ARGBHalf;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002A2E File Offset: 0x00000C2E
		private static RenderTextureFormat depthFormat
		{
			get
			{
				if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RFloat))
				{
					return RenderTextureFormat.RHalf;
				}
				return RenderTextureFormat.RFloat;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002A3E File Offset: 0x00000C3E
		private static RenderTextureFormat normalsFormat
		{
			get
			{
				if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB2101010))
				{
					return RenderTextureFormat.Default;
				}
				return RenderTextureFormat.ARGB2101010;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002A4B File Offset: 0x00000C4B
		private static bool isLinearColorSpace
		{
			get
			{
				return QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002A55 File Offset: 0x00000C55
		private bool renderingInSceneView
		{
			get
			{
				return this.hbaoCamera.cameraType == CameraType.SceneView;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002A68 File Offset: 0x00000C68
		private void OnEnable()
		{
			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				Debug.LogWarning("HBAO shader is not supported on this platform.");
				base.enabled = false;
				return;
			}
			if (this.hbaoShader == null)
			{
				this.hbaoShader = Shader.Find("Hidden/HBAO");
			}
			if (this.hbaoShader == null)
			{
				Debug.LogError("HBAO shader was not found...");
				return;
			}
			if (!this.hbaoShader.isSupported)
			{
				Debug.LogWarning("HBAO shader is not supported on this platform.");
				base.enabled = false;
				return;
			}
			this.Initialize();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002AEC File Offset: 0x00000CEC
		private void OnDisable()
		{
			this.ClearCommandBuffer(this.cmdBuffer);
			this.ReleaseHistoryBuffers();
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
			if (this.noiseTex != null)
			{
				UnityEngine.Object.DestroyImmediate(this.noiseTex);
			}
			if (this.fullscreenTriangle != null)
			{
				UnityEngine.Object.DestroyImmediate(this.fullscreenTriangle);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002B58 File Offset: 0x00000D58
		private void OnPreRender()
		{
			if (this.hbaoShader == null || this.hbaoCamera == null)
			{
				return;
			}
			this.FetchRenderParameters();
			this.CheckParameters();
			this.UpdateMaterialProperties();
			this.UpdateShaderKeywords();
			if (this.isCommandBufferDirty)
			{
				this.ClearCommandBuffer(this.cmdBuffer);
				this.BuildCommandBuffer(this.cmdBuffer, this.cameraEvent);
				this.hbaoCamera.AddCommandBuffer(this.cameraEvent, this.cmdBuffer);
				this.isCommandBufferDirty = false;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private void OnPostRender()
		{
			int frameCount = this.frameCount;
			this.frameCount = frameCount + 1;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002BFD File Offset: 0x00000DFD
		private void OnValidate()
		{
			if (this.hbaoShader == null || this.hbaoCamera == null)
			{
				return;
			}
			this.CheckParameters();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C24 File Offset: 0x00000E24
		private void Initialize()
		{
			this.m_sourceDescriptor = new RenderTextureDescriptor(0, 0);
			this.hbaoCamera = base.GetComponent<Camera>();
			this.hbaoCamera.forceIntoRenderTexture = true;
			this.material = new Material(this.hbaoShader);
			this.material.hideFlags = HideFlags.HideAndDontSave;
			this.motionVectorsSupported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf);
			this.cmdBuffer = new CommandBuffer
			{
				name = "HBAO"
			};
			this.isCommandBufferDirty = true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private void FetchRenderParameters()
		{
			this.width = this.hbaoCamera.pixelWidth;
			this.height = this.hbaoCamera.pixelHeight;
			this.m_sourceDescriptor.width = this.width;
			this.m_sourceDescriptor.height = this.height;
			this.screenWidth = this.width;
			this.screenHeight = this.height;
			this.stereoActive = false;
			this.numberOfEyes = 1;
			int num = (this.generalSettings.resolution == HBAO.Resolution.Full) ? 1 : ((this.generalSettings.deinterleaving == HBAO.Deinterleaving.Disabled) ? 2 : 1);
			if (num > 1)
			{
				this.aoWidth = (this.width + this.width % 2) / num;
				this.aoHeight = (this.height + this.height % 2) / num;
			}
			else
			{
				this.aoWidth = this.width;
				this.aoHeight = this.height;
			}
			this.reinterleavedAoWidth = this.width + ((this.width % 4 == 0) ? 0 : (4 - this.width % 4));
			this.reinterleavedAoHeight = this.height + ((this.height % 4 == 0) ? 0 : (4 - this.height % 4));
			this.deinterleavedAoWidth = this.reinterleavedAoWidth / 4;
			this.deinterleavedAoHeight = this.reinterleavedAoHeight / 4;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002DEC File Offset: 0x00000FEC
		private void AllocateHistoryBuffers()
		{
			this.ReleaseHistoryBuffers();
			int depthBufferBits = 0;
			int num = this.aoWidth;
			int num2 = this.aoHeight;
			this.aoHistoryBuffer = this.GetScreenSpaceRT(depthBufferBits, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num, num2);
			if (this.colorBleedingSettings.enabled)
			{
				int depthBufferBits2 = 0;
				num2 = this.aoWidth;
				num = this.aoHeight;
				this.colorBleedingHistoryBuffer = this.GetScreenSpaceRT(depthBufferBits2, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num2, num);
			}
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = this.aoHistoryBuffer;
			GL.Clear(false, true, Color.white);
			if (this.colorBleedingSettings.enabled)
			{
				RenderTexture.active = this.colorBleedingHistoryBuffer;
				GL.Clear(false, true, new Color(0f, 0f, 0f, 1f));
			}
			RenderTexture.active = active;
			this.frameCount = 0;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002EB4 File Offset: 0x000010B4
		private void ReleaseHistoryBuffers()
		{
			if (this.aoHistoryBuffer != null)
			{
				this.aoHistoryBuffer.Release();
			}
			if (this.colorBleedingHistoryBuffer != null)
			{
				this.colorBleedingHistoryBuffer.Release();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EE8 File Offset: 0x000010E8
		private void ClearCommandBuffer(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.hbaoCamera != null)
				{
					this.hbaoCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, cmd);
					this.hbaoCamera.RemoveCommandBuffer(CameraEvent.AfterLighting, cmd);
					this.hbaoCamera.RemoveCommandBuffer(CameraEvent.BeforeReflections, cmd);
				}
				cmd.Clear();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F38 File Offset: 0x00001138
		private void BuildCommandBuffer(CommandBuffer cmd, CameraEvent cameraEvent)
		{
			if (this.generalSettings.deinterleaving == HBAO.Deinterleaving.Disabled)
			{
				int hbaoTex = HBAO.ShaderProperties.hbaoTex;
				int depthBufferBits = 0;
				int num = this.aoWidth;
				int num2 = this.aoHeight;
				this.GetScreenSpaceTemporaryRT(cmd, hbaoTex, depthBufferBits, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num, num2);
				this.AO(cmd);
			}
			else
			{
				int hbaoTex2 = HBAO.ShaderProperties.hbaoTex;
				int depthBufferBits2 = 0;
				int num2 = this.reinterleavedAoWidth;
				int num = this.reinterleavedAoHeight;
				this.GetScreenSpaceTemporaryRT(cmd, hbaoTex2, depthBufferBits2, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num2, num);
				this.DeinterleavedAO(cmd);
			}
			this.Blur(cmd);
			this.TemporalFilter(cmd);
			this.Composite(cmd, cameraEvent);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.hbaoTex);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002FCC File Offset: 0x000011CC
		private void AO(CommandBuffer cmd)
		{
			this.BlitFullscreenTriangleWithClear(cmd, BuiltinRenderTextureType.CameraTarget, HBAO.ShaderProperties.hbaoTex, this.material, new Color(0f, 0f, 0f, 1f), 0);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003010 File Offset: 0x00001210
		private void DeinterleavedAO(CommandBuffer cmd)
		{
			int num3;
			int num4;
			for (int i = 0; i < 4; i++)
			{
				RenderTargetIdentifier[] destinations = new RenderTargetIdentifier[]
				{
					HBAO.ShaderProperties.depthSliceTex[i << 2],
					HBAO.ShaderProperties.depthSliceTex[(i << 2) + 1],
					HBAO.ShaderProperties.depthSliceTex[(i << 2) + 2],
					HBAO.ShaderProperties.depthSliceTex[(i << 2) + 3]
				};
				RenderTargetIdentifier[] destinations2 = new RenderTargetIdentifier[]
				{
					HBAO.ShaderProperties.normalsSliceTex[i << 2],
					HBAO.ShaderProperties.normalsSliceTex[(i << 2) + 1],
					HBAO.ShaderProperties.normalsSliceTex[(i << 2) + 2],
					HBAO.ShaderProperties.normalsSliceTex[(i << 2) + 3]
				};
				int num = (i & 1) << 1;
				int num2 = i >> 1 << 1;
				cmd.SetGlobalVector(HBAO.ShaderProperties.deinterleaveOffset[0], new Vector2((float)num, (float)num2));
				cmd.SetGlobalVector(HBAO.ShaderProperties.deinterleaveOffset[1], new Vector2((float)(num + 1), (float)num2));
				cmd.SetGlobalVector(HBAO.ShaderProperties.deinterleaveOffset[2], new Vector2((float)num, (float)(num2 + 1)));
				cmd.SetGlobalVector(HBAO.ShaderProperties.deinterleaveOffset[3], new Vector2((float)(num + 1), (float)(num2 + 1)));
				for (int j = 0; j < 4; j++)
				{
					int nameID = HBAO.ShaderProperties.depthSliceTex[j + 4 * i];
					int depthBufferBits = 0;
					num3 = this.deinterleavedAoWidth;
					num4 = this.deinterleavedAoHeight;
					this.GetScreenSpaceTemporaryRT(cmd, nameID, depthBufferBits, HBAO.depthFormat, RenderTextureReadWrite.Linear, FilterMode.Point, num3, num4);
					int nameID2 = HBAO.ShaderProperties.normalsSliceTex[j + 4 * i];
					int depthBufferBits2 = 0;
					num4 = this.deinterleavedAoWidth;
					num3 = this.deinterleavedAoHeight;
					this.GetScreenSpaceTemporaryRT(cmd, nameID2, depthBufferBits2, HBAO.normalsFormat, RenderTextureReadWrite.Linear, FilterMode.Point, num4, num3);
				}
				this.BlitFullscreenTriangle(cmd, BuiltinRenderTextureType.CameraTarget, destinations, this.material, 2);
				this.BlitFullscreenTriangle(cmd, BuiltinRenderTextureType.CameraTarget, destinations2, this.material, 3);
			}
			for (int k = 0; k < 16; k++)
			{
				cmd.SetGlobalTexture(HBAO.ShaderProperties.depthTex, HBAO.ShaderProperties.depthSliceTex[k]);
				cmd.SetGlobalTexture(HBAO.ShaderProperties.normalsTex, HBAO.ShaderProperties.normalsSliceTex[k]);
				cmd.SetGlobalVector(HBAO.ShaderProperties.jitter, HBAO.s_jitter[k]);
				int nameID3 = HBAO.ShaderProperties.aoSliceTex[k];
				int depthBufferBits3 = 0;
				num3 = this.deinterleavedAoWidth;
				num4 = this.deinterleavedAoHeight;
				this.GetScreenSpaceTemporaryRT(cmd, nameID3, depthBufferBits3, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Point, num3, num4);
				this.BlitFullscreenTriangleWithClear(cmd, BuiltinRenderTextureType.CameraTarget, HBAO.ShaderProperties.aoSliceTex[k], this.material, new Color(0f, 0f, 0f, 1f), 1);
				this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.depthSliceTex[k]);
				this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.normalsSliceTex[k]);
			}
			int tempTex = HBAO.ShaderProperties.tempTex;
			int depthBufferBits4 = 0;
			num4 = this.reinterleavedAoWidth;
			num3 = this.reinterleavedAoHeight;
			this.GetScreenSpaceTemporaryRT(cmd, tempTex, depthBufferBits4, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num4, num3);
			for (int l = 0; l < 16; l++)
			{
				cmd.SetGlobalVector(HBAO.ShaderProperties.atlasOffset, new Vector2((float)(((l & 1) + ((l & 7) >> 2 << 1)) * this.deinterleavedAoWidth), (float)((((l & 3) >> 1) + (l >> 3 << 1)) * this.deinterleavedAoHeight)));
				this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.aoSliceTex[l], HBAO.ShaderProperties.tempTex, this.material, 4);
				this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.aoSliceTex[l]);
			}
			HBAO.ApplyFlip(cmd, true);
			this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex, HBAO.ShaderProperties.hbaoTex, this.material, 5);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033D8 File Offset: 0x000015D8
		private void Blur(CommandBuffer cmd)
		{
			if (this.blurSettings.type != HBAO.BlurType.None)
			{
				int tempTex = HBAO.ShaderProperties.tempTex;
				int depthBufferBits = 0;
				int aoWidth = this.aoWidth;
				int aoHeight = this.aoHeight;
				this.GetScreenSpaceTemporaryRT(cmd, tempTex, depthBufferBits, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, aoWidth, aoHeight);
				cmd.SetGlobalVector(HBAO.ShaderProperties.blurDeltaUV, new Vector2(1f / (float)this.width, 0f));
				this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.hbaoTex, HBAO.ShaderProperties.tempTex, this.material, 6);
				cmd.SetGlobalVector(HBAO.ShaderProperties.blurDeltaUV, new Vector2(0f, 1f / (float)this.height));
				this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex, HBAO.ShaderProperties.hbaoTex, this.material, 6);
				this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000034B8 File Offset: 0x000016B8
		private void TemporalFilter(CommandBuffer cmd)
		{
			if (this.temporalFilterSettings.enabled && !this.renderingInSceneView)
			{
				this.AllocateHistoryBuffers();
				int num;
				int num2;
				if (this.colorBleedingSettings.enabled)
				{
					RenderTargetIdentifier[] destinations = new RenderTargetIdentifier[]
					{
						this.aoHistoryBuffer,
						this.colorBleedingHistoryBuffer
					};
					int tempTex = HBAO.ShaderProperties.tempTex;
					int depthBufferBits = 0;
					num = this.aoWidth;
					num2 = this.aoHeight;
					this.GetScreenSpaceTemporaryRT(cmd, tempTex, depthBufferBits, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num, num2);
					int tempTex2 = HBAO.ShaderProperties.tempTex2;
					int depthBufferBits2 = 0;
					num2 = this.aoWidth;
					num = this.aoHeight;
					this.GetScreenSpaceTemporaryRT(cmd, tempTex2, depthBufferBits2, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num2, num);
					this.BlitFullscreenTriangle(cmd, this.aoHistoryBuffer, HBAO.ShaderProperties.tempTex2, this.material, 8);
					this.BlitFullscreenTriangle(cmd, this.colorBleedingHistoryBuffer, HBAO.ShaderProperties.tempTex, this.material, 8);
					this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex2, destinations, this.material, 7);
					this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
					this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex2);
					cmd.SetGlobalTexture(HBAO.ShaderProperties.hbaoTex, this.colorBleedingHistoryBuffer);
					return;
				}
				int tempTex3 = HBAO.ShaderProperties.tempTex;
				int depthBufferBits3 = 0;
				num = this.aoWidth;
				num2 = this.aoHeight;
				this.GetScreenSpaceTemporaryRT(cmd, tempTex3, depthBufferBits3, HBAO.colorFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num, num2);
				this.BlitFullscreenTriangle(cmd, this.aoHistoryBuffer, HBAO.ShaderProperties.tempTex, this.material, 8);
				this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex, this.aoHistoryBuffer, this.material, 7);
				this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
				cmd.SetGlobalTexture(HBAO.ShaderProperties.hbaoTex, this.aoHistoryBuffer);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003684 File Offset: 0x00001884
		private void Composite(CommandBuffer cmd, CameraEvent cameraEvent)
		{
			if (this.generalSettings.debugMode != HBAO.DebugMode.Disabled)
			{
				this.CompositeBeforeImageEffectsOpaque(cmd, (this.generalSettings.debugMode == HBAO.DebugMode.ViewNormals) ? 12 : 9);
				return;
			}
			if (cameraEvent == CameraEvent.BeforeReflections)
			{
				this.CompositeBeforeReflections(cmd);
				return;
			}
			if (cameraEvent == CameraEvent.AfterLighting)
			{
				this.CompositeAfterLighting(cmd);
				return;
			}
			this.CompositeBeforeImageEffectsOpaque(cmd, 9);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036DC File Offset: 0x000018DC
		private void CompositeBeforeReflections(CommandBuffer cmd)
		{
			bool allowHDR = this.hbaoCamera.allowHDR;
			RenderTargetIdentifier[] array = new RenderTargetIdentifier[]
			{
				BuiltinRenderTextureType.GBuffer0,
				allowHDR ? BuiltinRenderTextureType.CameraTarget : BuiltinRenderTextureType.GBuffer3
			};
			this.GetScreenSpaceTemporaryRT(cmd, HBAO.ShaderProperties.tempTex, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0);
			this.GetScreenSpaceTemporaryRT(cmd, HBAO.ShaderProperties.tempTex2, 0, allowHDR ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0);
			this.BlitFullscreenTriangle(cmd, array[0], HBAO.ShaderProperties.tempTex, this.material, 8);
			this.BlitFullscreenTriangle(cmd, array[1], HBAO.ShaderProperties.tempTex2, this.material, 8);
			this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex2, array, this.material, 11);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex2);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000037B8 File Offset: 0x000019B8
		private void CompositeAfterLighting(CommandBuffer cmd)
		{
			bool allowHDR = this.hbaoCamera.allowHDR;
			BuiltinRenderTextureType type = allowHDR ? BuiltinRenderTextureType.CameraTarget : BuiltinRenderTextureType.GBuffer3;
			this.GetScreenSpaceTemporaryRT(cmd, HBAO.ShaderProperties.tempTex, 0, allowHDR ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0);
			this.BlitFullscreenTriangle(cmd, type, HBAO.ShaderProperties.tempTex, this.material, 8);
			this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex, type, this.material, 10);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000383C File Offset: 0x00001A3C
		private void CompositeBeforeImageEffectsOpaque(CommandBuffer cmd, int finalPassId = 9)
		{
			this.GetScreenSpaceTemporaryRT(cmd, HBAO.ShaderProperties.tempTex, 0, this.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0);
			if (this.stereoActive && this.hbaoCamera.actualRenderingPath != RenderingPath.DeferredShading)
			{
				cmd.Blit(BuiltinRenderTextureType.CameraTarget, HBAO.ShaderProperties.tempTex);
			}
			else
			{
				this.BlitFullscreenTriangle(cmd, BuiltinRenderTextureType.CameraTarget, HBAO.ShaderProperties.tempTex, this.material, 8);
			}
			HBAO.ApplyFlip(cmd, SystemInfo.graphicsUVStartsAtTop);
			this.BlitFullscreenTriangle(cmd, HBAO.ShaderProperties.tempTex, BuiltinRenderTextureType.CameraTarget, this.material, finalPassId);
			this.ReleaseTemporaryRT(cmd, HBAO.ShaderProperties.tempTex);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038E4 File Offset: 0x00001AE4
		private void UpdateMaterialProperties()
		{
			float num = Mathf.Tan(0.5f * this.hbaoCamera.fieldOfView * 0.017453292f);
			float num2 = 1f / (1f / num * ((float)this.screenHeight / (float)this.screenWidth));
			float num3 = 1f / (1f / num);
			float num4 = Mathf.Max(16f, this.aoSettings.maxRadiusPixels * Mathf.Sqrt((float)(this.screenWidth * this.numberOfEyes * this.screenHeight) / 2073600f));
			num4 /= (float)((this.generalSettings.deinterleaving == HBAO.Deinterleaving.x4) ? 4 : 1);
			Vector4 value = (this.generalSettings.deinterleaving == HBAO.Deinterleaving.x4) ? new Vector4((float)this.reinterleavedAoWidth / (float)this.width, (float)this.reinterleavedAoHeight / (float)this.height, 1f / ((float)this.reinterleavedAoWidth / (float)this.width), 1f / ((float)this.reinterleavedAoHeight / (float)this.height)) : ((this.generalSettings.resolution == HBAO.Resolution.Half) ? new Vector4(((float)this.width + 0.5f) / (float)this.width, ((float)this.height + 0.5f) / (float)this.height, 1f, 1f) : Vector4.one);
			this.material.SetTexture(HBAO.ShaderProperties.noiseTex, this.noiseTex);
			this.material.SetVector(HBAO.ShaderProperties.inputTexelSize, new Vector4(1f / (float)this.width, 1f / (float)this.height, (float)this.width, (float)this.height));
			this.material.SetVector(HBAO.ShaderProperties.aoTexelSize, new Vector4(1f / (float)this.aoWidth, 1f / (float)this.aoHeight, (float)this.aoWidth, (float)this.aoHeight));
			this.material.SetVector(HBAO.ShaderProperties.deinterleavedAOTexelSize, new Vector4(1f / (float)this.deinterleavedAoWidth, 1f / (float)this.deinterleavedAoHeight, (float)this.deinterleavedAoWidth, (float)this.deinterleavedAoHeight));
			this.material.SetVector(HBAO.ShaderProperties.reinterleavedAOTexelSize, new Vector4(1f / (float)this.reinterleavedAoWidth, 1f / (float)this.reinterleavedAoHeight, (float)this.reinterleavedAoWidth, (float)this.reinterleavedAoHeight));
			this.material.SetVector(HBAO.ShaderProperties.targetScale, value);
			this.material.SetVector(HBAO.ShaderProperties.uvToView, new Vector4(2f * num2, -2f * num3, -1f * num2, 1f * num3));
			this.material.SetMatrix(HBAO.ShaderProperties.worldToCameraMatrix, this.hbaoCamera.worldToCameraMatrix);
			this.material.SetFloat(HBAO.ShaderProperties.radius, this.aoSettings.radius * 0.5f * ((float)(this.screenHeight / ((this.generalSettings.deinterleaving == HBAO.Deinterleaving.x4) ? 4 : 1)) / (num * 2f)));
			this.material.SetFloat(HBAO.ShaderProperties.maxRadiusPixels, num4);
			this.material.SetFloat(HBAO.ShaderProperties.negInvRadius2, -1f / (this.aoSettings.radius * this.aoSettings.radius));
			this.material.SetFloat(HBAO.ShaderProperties.angleBias, this.aoSettings.bias);
			this.material.SetFloat(HBAO.ShaderProperties.aoMultiplier, 2f * (1f / (1f - this.aoSettings.bias)));
			this.material.SetFloat(HBAO.ShaderProperties.intensity, HBAO.isLinearColorSpace ? this.aoSettings.intensity : (this.aoSettings.intensity * 0.45454547f));
			this.material.SetColor(HBAO.ShaderProperties.baseColor, this.aoSettings.baseColor);
			this.material.SetFloat(HBAO.ShaderProperties.multiBounceInfluence, this.aoSettings.multiBounceInfluence);
			this.material.SetFloat(HBAO.ShaderProperties.offscreenSamplesContrib, this.aoSettings.offscreenSamplesContribution);
			this.material.SetFloat(HBAO.ShaderProperties.maxDistance, this.aoSettings.maxDistance);
			this.material.SetFloat(HBAO.ShaderProperties.distanceFalloff, this.aoSettings.distanceFalloff);
			this.material.SetFloat(HBAO.ShaderProperties.blurSharpness, this.blurSettings.sharpness);
			this.material.SetFloat(HBAO.ShaderProperties.colorBleedSaturation, this.colorBleedingSettings.saturation);
			this.material.SetFloat(HBAO.ShaderProperties.albedoMultiplier, this.colorBleedingSettings.albedoMultiplier);
			this.material.SetFloat(HBAO.ShaderProperties.colorBleedBrightnessMask, this.colorBleedingSettings.brightnessMask);
			this.material.SetVector(HBAO.ShaderProperties.colorBleedBrightnessMaskRange, HBAO.AdjustBrightnessMaskToGammaSpace(new Vector2(Mathf.Pow(this.colorBleedingSettings.brightnessMaskRange.x, 3f), Mathf.Pow(this.colorBleedingSettings.brightnessMaskRange.y, 3f))));
			this.material.SetVector(HBAO.ShaderProperties.temporalParams, (this.temporalFilterSettings.enabled && !this.renderingInSceneView) ? new Vector2(HBAO.s_temporalRotations[this.frameCount % 6] / 360f, HBAO.s_temporalOffsets[this.frameCount % 4]) : Vector2.zero);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003E3C File Offset: 0x0000203C
		private void UpdateShaderKeywords()
		{
			if (this.m_ShaderKeywords == null || this.m_ShaderKeywords.Length != 13)
			{
				this.m_ShaderKeywords = new string[13];
			}
			this.m_ShaderKeywords[0] = HBAO.ShaderProperties.GetOrthographicOrDeferredKeyword(this.hbaoCamera.orthographic, this.generalSettings);
			this.m_ShaderKeywords[1] = HBAO.ShaderProperties.GetDirectionsKeyword(this.generalSettings);
			this.m_ShaderKeywords[2] = HBAO.ShaderProperties.GetStepsKeyword(this.generalSettings);
			this.m_ShaderKeywords[3] = HBAO.ShaderProperties.GetNoiseKeyword(this.generalSettings);
			this.m_ShaderKeywords[4] = HBAO.ShaderProperties.GetDeinterleavingKeyword(this.generalSettings);
			this.m_ShaderKeywords[5] = HBAO.ShaderProperties.GetDebugKeyword(this.generalSettings);
			this.m_ShaderKeywords[6] = HBAO.ShaderProperties.GetMultibounceKeyword(this.aoSettings);
			this.m_ShaderKeywords[7] = HBAO.ShaderProperties.GetOffscreenSamplesContributionKeyword(this.aoSettings);
			this.m_ShaderKeywords[8] = HBAO.ShaderProperties.GetPerPixelNormalsKeyword(this.aoSettings);
			this.m_ShaderKeywords[9] = HBAO.ShaderProperties.GetBlurRadiusKeyword(this.blurSettings);
			this.m_ShaderKeywords[10] = HBAO.ShaderProperties.GetVarianceClippingKeyword(this.temporalFilterSettings);
			this.m_ShaderKeywords[11] = HBAO.ShaderProperties.GetColorBleedingKeyword(this.colorBleedingSettings);
			this.m_ShaderKeywords[12] = HBAO.ShaderProperties.GetLightingLogEncodedKeyword(this.hbaoCamera.allowHDR);
			this.material.shaderKeywords = this.m_ShaderKeywords;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003F88 File Offset: 0x00002188
		private void CheckParameters()
		{
			this.hbaoCamera.depthTextureMode |= DepthTextureMode.Depth;
			if (this.aoSettings.perPixelNormals == HBAO.PerPixelNormals.Camera)
			{
				this.hbaoCamera.depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			if (this.temporalFilterSettings.enabled)
			{
				this.hbaoCamera.depthTextureMode |= DepthTextureMode.MotionVectors;
			}
			if (this.hbaoCamera.actualRenderingPath != RenderingPath.DeferredShading && this.aoSettings.perPixelNormals == HBAO.PerPixelNormals.GBuffer)
			{
				this.SetAoPerPixelNormals(HBAO.PerPixelNormals.Camera);
			}
			if (this.generalSettings.deinterleaving != HBAO.Deinterleaving.Disabled && SystemInfo.supportedRenderTargetCount < 4)
			{
				this.SetDeinterleaving(HBAO.Deinterleaving.Disabled);
			}
			if (this.generalSettings.pipelineStage != HBAO.PipelineStage.BeforeImageEffectsOpaque && this.hbaoCamera.actualRenderingPath != RenderingPath.DeferredShading)
			{
				this.SetPipelineStage(HBAO.PipelineStage.BeforeImageEffectsOpaque);
			}
			if (this.generalSettings.pipelineStage != HBAO.PipelineStage.BeforeImageEffectsOpaque && this.aoSettings.perPixelNormals == HBAO.PerPixelNormals.Camera)
			{
				this.SetAoPerPixelNormals(HBAO.PerPixelNormals.GBuffer);
			}
			if (this.stereoActive && this.hbaoCamera.actualRenderingPath != RenderingPath.DeferredShading && this.aoSettings.perPixelNormals != HBAO.PerPixelNormals.Reconstruct)
			{
				this.SetAoPerPixelNormals(HBAO.PerPixelNormals.Reconstruct);
			}
			if (this.temporalFilterSettings.enabled && !this.motionVectorsSupported)
			{
				this.EnableTemporalFilter(false);
			}
			if (this.colorBleedingSettings.enabled && this.temporalFilterSettings.enabled && SystemInfo.supportedRenderTargetCount < 2)
			{
				this.EnableTemporalFilter(false);
			}
			if (!(this.noiseTex == null))
			{
				HBAO.NoiseType? previousNoiseType = this.m_PreviousNoiseType;
				HBAO.NoiseType noiseType = this.generalSettings.noiseType;
				if (previousNoiseType.GetValueOrDefault() == noiseType & previousNoiseType != null)
				{
					return;
				}
			}
			if (this.noiseTex != null)
			{
				UnityEngine.Object.DestroyImmediate(this.noiseTex);
			}
			this.CreateNoiseTexture();
			this.m_PreviousNoiseType = new HBAO.NoiseType?(this.generalSettings.noiseType);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004148 File Offset: 0x00002348
		private RenderTextureDescriptor GetDefaultDescriptor(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
		{
			RenderTextureDescriptor result = new RenderTextureDescriptor(this.m_sourceDescriptor.width, this.m_sourceDescriptor.height, this.m_sourceDescriptor.colorFormat, depthBufferBits);
			result.dimension = this.m_sourceDescriptor.dimension;
			result.volumeDepth = this.m_sourceDescriptor.volumeDepth;
			result.vrUsage = this.m_sourceDescriptor.vrUsage;
			result.msaaSamples = this.m_sourceDescriptor.msaaSamples;
			result.memoryless = this.m_sourceDescriptor.memoryless;
			result.useMipMap = this.m_sourceDescriptor.useMipMap;
			result.autoGenerateMips = this.m_sourceDescriptor.autoGenerateMips;
			result.enableRandomWrite = this.m_sourceDescriptor.enableRandomWrite;
			result.shadowSamplingMode = this.m_sourceDescriptor.shadowSamplingMode;
			if (this.hbaoCamera.allowDynamicResolution)
			{
				result.useDynamicScale = true;
			}
			if (colorFormat != RenderTextureFormat.Default)
			{
				result.colorFormat = colorFormat;
			}
			if (readWrite == RenderTextureReadWrite.sRGB)
			{
				result.sRGB = true;
			}
			else if (readWrite == RenderTextureReadWrite.Linear)
			{
				result.sRGB = false;
			}
			else if (readWrite == RenderTextureReadWrite.Default)
			{
				result.sRGB = HBAO.isLinearColorSpace;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004270 File Offset: 0x00002470
		private RenderTexture GetScreenSpaceRT(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, FilterMode filter = FilterMode.Bilinear, int widthOverride = 0, int heightOverride = 0)
		{
			RenderTextureDescriptor defaultDescriptor = this.GetDefaultDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				defaultDescriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				defaultDescriptor.height = heightOverride;
			}
			if (this.stereoActive && defaultDescriptor.dimension == TextureDimension.Tex2DArray)
			{
				defaultDescriptor.dimension = TextureDimension.Tex2D;
			}
			return new RenderTexture(defaultDescriptor)
			{
				filterMode = filter
			};
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000042CC File Offset: 0x000024CC
		private void GetScreenSpaceTemporaryRT(CommandBuffer cmd, int nameID, int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, FilterMode filter = FilterMode.Bilinear, int widthOverride = 0, int heightOverride = 0)
		{
			RenderTextureDescriptor defaultDescriptor = this.GetDefaultDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				defaultDescriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				defaultDescriptor.height = heightOverride;
			}
			if (this.stereoActive && defaultDescriptor.dimension == TextureDimension.Tex2DArray)
			{
				defaultDescriptor.dimension = TextureDimension.Tex2D;
			}
			cmd.GetTemporaryRT(nameID, defaultDescriptor, filter);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004325 File Offset: 0x00002525
		private void ReleaseTemporaryRT(CommandBuffer cmd, int nameID)
		{
			cmd.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000432E File Offset: 0x0000252E
		private void BlitFullscreenTriangle(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass = 0)
		{
			cmd.SetGlobalTexture(HBAO.ShaderProperties.mainTex, source);
			cmd.SetRenderTarget(destination);
			cmd.DrawMesh(this.fullscreenTriangle, Matrix4x4.identity, material, 0, pass);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004359 File Offset: 0x00002559
		private void BlitFullscreenTriangle(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier[] destinations, Material material, int pass = 0)
		{
			cmd.SetGlobalTexture(HBAO.ShaderProperties.mainTex, source);
			cmd.SetRenderTarget(destinations, destinations[0]);
			cmd.DrawMesh(this.fullscreenTriangle, Matrix4x4.identity, material, 0, pass);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000438B File Offset: 0x0000258B
		private void BlitFullscreenTriangleWithClear(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, Color clearColor, int pass = 0)
		{
			cmd.SetGlobalTexture(HBAO.ShaderProperties.mainTex, source);
			cmd.SetRenderTarget(destination);
			cmd.ClearRenderTarget(false, true, clearColor);
			cmd.DrawMesh(this.fullscreenTriangle, Matrix4x4.identity, material, 0, pass);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043C0 File Offset: 0x000025C0
		private static void ApplyFlip(CommandBuffer cmd, bool flip = true)
		{
			if (flip)
			{
				cmd.SetGlobalVector(HBAO.ShaderProperties.uvTransform, new Vector4(1f, -1f, 0f, 1f));
				return;
			}
			cmd.SetGlobalVector(HBAO.ShaderProperties.uvTransform, new Vector4(1f, 1f, 0f, 0f));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004419 File Offset: 0x00002619
		private static Vector2 AdjustBrightnessMaskToGammaSpace(Vector2 v)
		{
			if (!HBAO.isLinearColorSpace)
			{
				return HBAO.ToGammaSpace(v);
			}
			return v;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000442A File Offset: 0x0000262A
		private static float ToGammaSpace(float v)
		{
			return Mathf.Pow(v, 0.45454547f);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004437 File Offset: 0x00002637
		private static Vector2 ToGammaSpace(Vector2 v)
		{
			return new Vector2(HBAO.ToGammaSpace(v.x), HBAO.ToGammaSpace(v.y));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004454 File Offset: 0x00002654
		private void CreateNoiseTexture()
		{
			this.noiseTex = new Texture2D(4, 4, SystemInfo.SupportsTextureFormat(TextureFormat.RGHalf) ? TextureFormat.RGHalf : TextureFormat.RGB24, false, true);
			this.noiseTex.filterMode = FilterMode.Point;
			this.noiseTex.wrapMode = TextureWrapMode.Repeat;
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					float r = (this.generalSettings.noiseType != HBAO.NoiseType.Dither) ? (0.25f * (0.0625f * (float)((i + j & 3) << 2) + (float)(i & 3))) : HBAO.MersenneTwister.Numbers[num++];
					float g = (this.generalSettings.noiseType != HBAO.NoiseType.Dither) ? (0.25f * (float)(j - i & 3)) : HBAO.MersenneTwister.Numbers[num++];
					Color color = new Color(r, g, 0f);
					this.noiseTex.SetPixel(i, j, color);
				}
			}
			this.noiseTex.Apply();
			int k = 0;
			int num2 = 0;
			while (k < HBAO.s_jitter.Length)
			{
				float x = HBAO.MersenneTwister.Numbers[num2++];
				float y = HBAO.MersenneTwister.Numbers[num2++];
				HBAO.s_jitter[k] = new Vector2(x, y);
				k++;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000458C File Offset: 0x0000278C
		public HBAO()
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045E1 File Offset: 0x000027E1
		// Note: this type is marked as 'beforefieldinit'.
		static HBAO()
		{
		}

		// Token: 0x04000001 RID: 1
		public Shader hbaoShader;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.Presets m_Presets = HBAO.Presets.defaults;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.GeneralSettings m_GeneralSettings = HBAO.GeneralSettings.defaults;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.AOSettings m_AOSettings = HBAO.AOSettings.defaults;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.TemporalFilterSettings m_TemporalFilterSettings = HBAO.TemporalFilterSettings.defaults;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.BlurSettings m_BlurSettings = HBAO.BlurSettings.defaults;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		[HBAO.SettingsGroup]
		private HBAO.ColorBleedingSettings m_ColorBleedingSettings = HBAO.ColorBleedingSettings.defaults;

		// Token: 0x04000008 RID: 8
		private static readonly Vector2[] s_jitter = new Vector2[16];

		// Token: 0x04000009 RID: 9
		private static readonly float[] s_temporalRotations = new float[]
		{
			60f,
			300f,
			180f,
			240f,
			120f,
			0f
		};

		// Token: 0x0400000A RID: 10
		private static readonly float[] s_temporalOffsets = new float[]
		{
			0f,
			0.5f,
			0.25f,
			0.75f
		};

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private Material <material>k__BackingField;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private Camera <hbaoCamera>k__BackingField;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private CommandBuffer <cmdBuffer>k__BackingField;

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		private int <width>k__BackingField;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private int <height>k__BackingField;

		// Token: 0x04000010 RID: 16
		[CompilerGenerated]
		private bool <stereoActive>k__BackingField;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private int <numberOfEyes>k__BackingField;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private int <xrActiveEye>k__BackingField;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private int <screenWidth>k__BackingField;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private int <screenHeight>k__BackingField;

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		private int <aoWidth>k__BackingField;

		// Token: 0x04000016 RID: 22
		[CompilerGenerated]
		private int <aoHeight>k__BackingField;

		// Token: 0x04000017 RID: 23
		[CompilerGenerated]
		private int <reinterleavedAoWidth>k__BackingField;

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private int <reinterleavedAoHeight>k__BackingField;

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		private int <deinterleavedAoWidth>k__BackingField;

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private int <deinterleavedAoHeight>k__BackingField;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private int <frameCount>k__BackingField;

		// Token: 0x0400001C RID: 28
		[CompilerGenerated]
		private bool <motionVectorsSupported>k__BackingField;

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private RenderTexture <aoHistoryBuffer>k__BackingField;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private RenderTexture <colorBleedingHistoryBuffer>k__BackingField;

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		private Texture2D <noiseTex>k__BackingField;

		// Token: 0x04000020 RID: 32
		private RenderTextureDescriptor m_sourceDescriptor;

		// Token: 0x04000021 RID: 33
		private string[] m_ShaderKeywords;

		// Token: 0x04000022 RID: 34
		private bool m_IsCommandBufferDirty;

		// Token: 0x04000023 RID: 35
		private Mesh m_FullscreenTriangle;

		// Token: 0x04000024 RID: 36
		private HBAO.PipelineStage? m_PreviousPipelineStage;

		// Token: 0x04000025 RID: 37
		private HBAO.Resolution? m_PreviousResolution;

		// Token: 0x04000026 RID: 38
		private HBAO.Deinterleaving? m_PreviousDeinterleaving;

		// Token: 0x04000027 RID: 39
		private HBAO.DebugMode? m_PreviousDebugMode;

		// Token: 0x04000028 RID: 40
		private HBAO.NoiseType? m_PreviousNoiseType;

		// Token: 0x04000029 RID: 41
		private HBAO.BlurType? m_PreviousBlurAmount;

		// Token: 0x0400002A RID: 42
		private int m_PreviousWidth;

		// Token: 0x0400002B RID: 43
		private int m_PreviousHeight;

		// Token: 0x0400002C RID: 44
		private bool m_PreviousAllowHDR;

		// Token: 0x0400002D RID: 45
		private bool m_PreviousColorBleedingEnabled;

		// Token: 0x0400002E RID: 46
		private bool m_PreviousTemporalFilterEnabled;

		// Token: 0x0400002F RID: 47
		private RenderingPath m_PreviousRenderingPath;

		// Token: 0x02000004 RID: 4
		public enum Preset
		{
			// Token: 0x04000034 RID: 52
			FastestPerformance,
			// Token: 0x04000035 RID: 53
			FastPerformance,
			// Token: 0x04000036 RID: 54
			Normal,
			// Token: 0x04000037 RID: 55
			HighQuality,
			// Token: 0x04000038 RID: 56
			HighestQuality,
			// Token: 0x04000039 RID: 57
			Custom
		}

		// Token: 0x02000005 RID: 5
		public enum PipelineStage
		{
			// Token: 0x0400003B RID: 59
			BeforeImageEffectsOpaque,
			// Token: 0x0400003C RID: 60
			AfterLighting,
			// Token: 0x0400003D RID: 61
			BeforeReflections
		}

		// Token: 0x02000006 RID: 6
		public enum Quality
		{
			// Token: 0x0400003F RID: 63
			Lowest,
			// Token: 0x04000040 RID: 64
			Low,
			// Token: 0x04000041 RID: 65
			Medium,
			// Token: 0x04000042 RID: 66
			High,
			// Token: 0x04000043 RID: 67
			Highest
		}

		// Token: 0x02000007 RID: 7
		public enum Resolution
		{
			// Token: 0x04000045 RID: 69
			Full,
			// Token: 0x04000046 RID: 70
			Half
		}

		// Token: 0x02000008 RID: 8
		public enum NoiseType
		{
			// Token: 0x04000048 RID: 72
			Dither,
			// Token: 0x04000049 RID: 73
			InterleavedGradientNoise,
			// Token: 0x0400004A RID: 74
			SpatialDistribution
		}

		// Token: 0x02000009 RID: 9
		public enum Deinterleaving
		{
			// Token: 0x0400004C RID: 76
			Disabled,
			// Token: 0x0400004D RID: 77
			x4
		}

		// Token: 0x0200000A RID: 10
		public enum DebugMode
		{
			// Token: 0x0400004F RID: 79
			Disabled,
			// Token: 0x04000050 RID: 80
			AOOnly,
			// Token: 0x04000051 RID: 81
			ColorBleedingOnly,
			// Token: 0x04000052 RID: 82
			SplitWithoutAOAndWithAO,
			// Token: 0x04000053 RID: 83
			SplitWithAOAndAOOnly,
			// Token: 0x04000054 RID: 84
			SplitWithoutAOAndAOOnly,
			// Token: 0x04000055 RID: 85
			ViewNormals
		}

		// Token: 0x0200000B RID: 11
		public enum BlurType
		{
			// Token: 0x04000057 RID: 87
			None,
			// Token: 0x04000058 RID: 88
			Narrow,
			// Token: 0x04000059 RID: 89
			Medium,
			// Token: 0x0400005A RID: 90
			Wide,
			// Token: 0x0400005B RID: 91
			ExtraWide
		}

		// Token: 0x0200000C RID: 12
		public enum PerPixelNormals
		{
			// Token: 0x0400005D RID: 93
			GBuffer,
			// Token: 0x0400005E RID: 94
			Camera,
			// Token: 0x0400005F RID: 95
			Reconstruct
		}

		// Token: 0x0200000D RID: 13
		public enum VarianceClipping
		{
			// Token: 0x04000061 RID: 97
			Disabled,
			// Token: 0x04000062 RID: 98
			_4Tap,
			// Token: 0x04000063 RID: 99
			_8Tap
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public struct Presets
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600009C RID: 156 RVA: 0x0000461C File Offset: 0x0000281C
			[SerializeField]
			public static HBAO.Presets defaults
			{
				get
				{
					return new HBAO.Presets
					{
						preset = HBAO.Preset.Normal
					};
				}
			}

			// Token: 0x04000064 RID: 100
			public HBAO.Preset preset;
		}

		// Token: 0x0200000F RID: 15
		[Serializable]
		public struct GeneralSettings
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600009D RID: 157 RVA: 0x0000463C File Offset: 0x0000283C
			[SerializeField]
			public static HBAO.GeneralSettings defaults
			{
				get
				{
					return new HBAO.GeneralSettings
					{
						pipelineStage = HBAO.PipelineStage.BeforeImageEffectsOpaque,
						quality = HBAO.Quality.Medium,
						deinterleaving = HBAO.Deinterleaving.Disabled,
						resolution = HBAO.Resolution.Full,
						noiseType = HBAO.NoiseType.Dither,
						debugMode = HBAO.DebugMode.Disabled
					};
				}
			}

			// Token: 0x04000065 RID: 101
			[Tooltip("The stage the AO is injected into the rendering pipeline.")]
			[Space(6f)]
			public HBAO.PipelineStage pipelineStage;

			// Token: 0x04000066 RID: 102
			[Tooltip("The quality of the AO.")]
			[Space(10f)]
			public HBAO.Quality quality;

			// Token: 0x04000067 RID: 103
			[Tooltip("The deinterleaving factor.")]
			public HBAO.Deinterleaving deinterleaving;

			// Token: 0x04000068 RID: 104
			[Tooltip("The resolution at which the AO is calculated.")]
			public HBAO.Resolution resolution;

			// Token: 0x04000069 RID: 105
			[Tooltip("The type of noise to use.")]
			[Space(10f)]
			public HBAO.NoiseType noiseType;

			// Token: 0x0400006A RID: 106
			[Tooltip("The debug mode actually displayed on screen.")]
			[Space(10f)]
			public HBAO.DebugMode debugMode;
		}

		// Token: 0x02000010 RID: 16
		[Serializable]
		public struct AOSettings
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00004684 File Offset: 0x00002884
			[SerializeField]
			public static HBAO.AOSettings defaults
			{
				get
				{
					return new HBAO.AOSettings
					{
						radius = 0.8f,
						maxRadiusPixels = 128f,
						bias = 0.05f,
						intensity = 1f,
						useMultiBounce = false,
						multiBounceInfluence = 1f,
						offscreenSamplesContribution = 0f,
						maxDistance = 150f,
						distanceFalloff = 50f,
						perPixelNormals = HBAO.PerPixelNormals.GBuffer,
						baseColor = Color.black
					};
				}
			}

			// Token: 0x0400006B RID: 107
			[Tooltip("AO radius: this is the distance outside which occluders are ignored.")]
			[Space(6f)]
			[Range(0.25f, 5f)]
			public float radius;

			// Token: 0x0400006C RID: 108
			[Tooltip("Maximum radius in pixels: this prevents the radius to grow too much with close-up object and impact on performances.")]
			[Range(16f, 256f)]
			public float maxRadiusPixels;

			// Token: 0x0400006D RID: 109
			[Tooltip("For low-tessellated geometry, occlusion variations tend to appear at creases and ridges, which betray the underlying tessellation. To remove these artifacts, we use an angle bias parameter which restricts the hemisphere.")]
			[Range(0f, 0.5f)]
			public float bias;

			// Token: 0x0400006E RID: 110
			[Tooltip("This value allows to scale up the ambient occlusion values.")]
			[Range(0f, 4f)]
			public float intensity;

			// Token: 0x0400006F RID: 111
			[Tooltip("Enable/disable MultiBounce approximation.")]
			public bool useMultiBounce;

			// Token: 0x04000070 RID: 112
			[Tooltip("MultiBounce approximation influence.")]
			[Range(0f, 1f)]
			public float multiBounceInfluence;

			// Token: 0x04000071 RID: 113
			[Tooltip("The amount of AO offscreen samples are contributing.")]
			[Range(0f, 1f)]
			public float offscreenSamplesContribution;

			// Token: 0x04000072 RID: 114
			[Tooltip("The max distance to display AO.")]
			[Space(10f)]
			public float maxDistance;

			// Token: 0x04000073 RID: 115
			[Tooltip("The distance before max distance at which AO start to decrease.")]
			public float distanceFalloff;

			// Token: 0x04000074 RID: 116
			[Tooltip("The type of per pixel normals to use.")]
			[Space(10f)]
			public HBAO.PerPixelNormals perPixelNormals;

			// Token: 0x04000075 RID: 117
			[Tooltip("This setting allow you to set the base color if the AO, the alpha channel value is unused.")]
			[Space(10f)]
			public Color baseColor;
		}

		// Token: 0x02000011 RID: 17
		[Serializable]
		public struct TemporalFilterSettings
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600009F RID: 159 RVA: 0x00004718 File Offset: 0x00002918
			[SerializeField]
			public static HBAO.TemporalFilterSettings defaults
			{
				get
				{
					return new HBAO.TemporalFilterSettings
					{
						enabled = false,
						varianceClipping = HBAO.VarianceClipping._4Tap
					};
				}
			}

			// Token: 0x04000076 RID: 118
			[Space(6f)]
			public bool enabled;

			// Token: 0x04000077 RID: 119
			[Tooltip("The type of variance clipping to use.")]
			public HBAO.VarianceClipping varianceClipping;
		}

		// Token: 0x02000012 RID: 18
		[Serializable]
		public struct BlurSettings
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004740 File Offset: 0x00002940
			[SerializeField]
			public static HBAO.BlurSettings defaults
			{
				get
				{
					return new HBAO.BlurSettings
					{
						type = HBAO.BlurType.Medium,
						sharpness = 8f
					};
				}
			}

			// Token: 0x04000078 RID: 120
			[Tooltip("The type of blur to use.")]
			[Space(6f)]
			public HBAO.BlurType type;

			// Token: 0x04000079 RID: 121
			[Tooltip("This parameter controls the depth-dependent weight of the bilateral filter, to avoid bleeding across edges. A zero sharpness is a pure Gaussian blur. Increasing the blur sharpness removes bleeding by using lower weights for samples with large depth delta from the current pixel.")]
			[Space(10f)]
			[Range(0f, 16f)]
			public float sharpness;
		}

		// Token: 0x02000013 RID: 19
		[Serializable]
		public struct ColorBleedingSettings
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000476C File Offset: 0x0000296C
			[SerializeField]
			public static HBAO.ColorBleedingSettings defaults
			{
				get
				{
					return new HBAO.ColorBleedingSettings
					{
						enabled = false,
						saturation = 1f,
						albedoMultiplier = 4f,
						brightnessMask = 1f,
						brightnessMaskRange = new Vector2(0f, 0.5f)
					};
				}
			}

			// Token: 0x0400007A RID: 122
			[Space(6f)]
			public bool enabled;

			// Token: 0x0400007B RID: 123
			[Tooltip("This value allows to control the saturation of the color bleeding.")]
			[Space(10f)]
			[Range(0f, 4f)]
			public float saturation;

			// Token: 0x0400007C RID: 124
			[Tooltip("This value allows to scale the contribution of the color bleeding samples.")]
			[Range(0f, 32f)]
			public float albedoMultiplier;

			// Token: 0x0400007D RID: 125
			[Tooltip("Use masking on emissive pixels")]
			[Range(0f, 1f)]
			public float brightnessMask;

			// Token: 0x0400007E RID: 126
			[Tooltip("Brightness level where masking starts/ends")]
			[HBAO.MinMaxSliderAttribute(0f, 2f)]
			public Vector2 brightnessMaskRange;
		}

		// Token: 0x02000014 RID: 20
		[AttributeUsage(AttributeTargets.Field)]
		public class SettingsGroup : Attribute
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x000047C4 File Offset: 0x000029C4
			public SettingsGroup()
			{
			}
		}

		// Token: 0x02000015 RID: 21
		public class MinMaxSliderAttribute : PropertyAttribute
		{
			// Token: 0x060000A3 RID: 163 RVA: 0x000047CC File Offset: 0x000029CC
			public MinMaxSliderAttribute(float min, float max)
			{
				this.min = min;
				this.max = max;
			}

			// Token: 0x0400007F RID: 127
			public readonly float max;

			// Token: 0x04000080 RID: 128
			public readonly float min;
		}

		// Token: 0x02000016 RID: 22
		private static class Pass
		{
			// Token: 0x04000081 RID: 129
			public const int AO = 0;

			// Token: 0x04000082 RID: 130
			public const int AO_Deinterleaved = 1;

			// Token: 0x04000083 RID: 131
			public const int Deinterleave_Depth = 2;

			// Token: 0x04000084 RID: 132
			public const int Deinterleave_Normals = 3;

			// Token: 0x04000085 RID: 133
			public const int Atlas_AO_Deinterleaved = 4;

			// Token: 0x04000086 RID: 134
			public const int Reinterleave_AO = 5;

			// Token: 0x04000087 RID: 135
			public const int Blur = 6;

			// Token: 0x04000088 RID: 136
			public const int Temporal_Filter = 7;

			// Token: 0x04000089 RID: 137
			public const int Copy = 8;

			// Token: 0x0400008A RID: 138
			public const int Composite = 9;

			// Token: 0x0400008B RID: 139
			public const int Composite_AfterLighting = 10;

			// Token: 0x0400008C RID: 140
			public const int Composite_BeforeReflections = 11;

			// Token: 0x0400008D RID: 141
			public const int Debug_ViewNormals = 12;
		}

		// Token: 0x02000017 RID: 23
		private static class ShaderProperties
		{
			// Token: 0x060000A4 RID: 164 RVA: 0x000047E4 File Offset: 0x000029E4
			static ShaderProperties()
			{
				for (int i = 0; i < 16; i++)
				{
					HBAO.ShaderProperties.depthSliceTex[i] = Shader.PropertyToID("_DepthSliceTex" + i.ToString());
					HBAO.ShaderProperties.normalsSliceTex[i] = Shader.PropertyToID("_NormalsSliceTex" + i.ToString());
					HBAO.ShaderProperties.aoSliceTex[i] = Shader.PropertyToID("_AOSliceTex" + i.ToString());
				}
				HBAO.ShaderProperties.deinterleaveOffset = new int[]
				{
					Shader.PropertyToID("_Deinterleave_Offset00"),
					Shader.PropertyToID("_Deinterleave_Offset10"),
					Shader.PropertyToID("_Deinterleave_Offset01"),
					Shader.PropertyToID("_Deinterleave_Offset11")
				};
				HBAO.ShaderProperties.atlasOffset = Shader.PropertyToID("_AtlasOffset");
				HBAO.ShaderProperties.jitter = Shader.PropertyToID("_Jitter");
				HBAO.ShaderProperties.uvTransform = Shader.PropertyToID("_UVTransform");
				HBAO.ShaderProperties.inputTexelSize = Shader.PropertyToID("_Input_TexelSize");
				HBAO.ShaderProperties.aoTexelSize = Shader.PropertyToID("_AO_TexelSize");
				HBAO.ShaderProperties.deinterleavedAOTexelSize = Shader.PropertyToID("_DeinterleavedAO_TexelSize");
				HBAO.ShaderProperties.reinterleavedAOTexelSize = Shader.PropertyToID("_ReinterleavedAO_TexelSize");
				HBAO.ShaderProperties.uvToView = Shader.PropertyToID("_UVToView");
				HBAO.ShaderProperties.worldToCameraMatrix = Shader.PropertyToID("_WorldToCameraMatrix");
				HBAO.ShaderProperties.targetScale = Shader.PropertyToID("_TargetScale");
				HBAO.ShaderProperties.radius = Shader.PropertyToID("_Radius");
				HBAO.ShaderProperties.maxRadiusPixels = Shader.PropertyToID("_MaxRadiusPixels");
				HBAO.ShaderProperties.negInvRadius2 = Shader.PropertyToID("_NegInvRadius2");
				HBAO.ShaderProperties.angleBias = Shader.PropertyToID("_AngleBias");
				HBAO.ShaderProperties.aoMultiplier = Shader.PropertyToID("_AOmultiplier");
				HBAO.ShaderProperties.intensity = Shader.PropertyToID("_Intensity");
				HBAO.ShaderProperties.multiBounceInfluence = Shader.PropertyToID("_MultiBounceInfluence");
				HBAO.ShaderProperties.offscreenSamplesContrib = Shader.PropertyToID("_OffscreenSamplesContrib");
				HBAO.ShaderProperties.maxDistance = Shader.PropertyToID("_MaxDistance");
				HBAO.ShaderProperties.distanceFalloff = Shader.PropertyToID("_DistanceFalloff");
				HBAO.ShaderProperties.baseColor = Shader.PropertyToID("_BaseColor");
				HBAO.ShaderProperties.colorBleedSaturation = Shader.PropertyToID("_ColorBleedSaturation");
				HBAO.ShaderProperties.albedoMultiplier = Shader.PropertyToID("_AlbedoMultiplier");
				HBAO.ShaderProperties.colorBleedBrightnessMask = Shader.PropertyToID("_ColorBleedBrightnessMask");
				HBAO.ShaderProperties.colorBleedBrightnessMaskRange = Shader.PropertyToID("_ColorBleedBrightnessMaskRange");
				HBAO.ShaderProperties.blurDeltaUV = Shader.PropertyToID("_BlurDeltaUV");
				HBAO.ShaderProperties.blurSharpness = Shader.PropertyToID("_BlurSharpness");
				HBAO.ShaderProperties.temporalParams = Shader.PropertyToID("_TemporalParams");
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x00004AC5 File Offset: 0x00002CC5
			public static string GetOrthographicOrDeferredKeyword(bool orthographic, HBAO.GeneralSettings settings)
			{
				if (orthographic)
				{
					return "ORTHOGRAPHIC_PROJECTION";
				}
				if (settings.pipelineStage == HBAO.PipelineStage.BeforeImageEffectsOpaque)
				{
					return "__";
				}
				return "DEFERRED_SHADING";
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00004AE4 File Offset: 0x00002CE4
			public static string GetDirectionsKeyword(HBAO.GeneralSettings settings)
			{
				switch (settings.quality)
				{
				case HBAO.Quality.Lowest:
					return "DIRECTIONS_3";
				case HBAO.Quality.Low:
					return "DIRECTIONS_4";
				case HBAO.Quality.Medium:
					return "DIRECTIONS_6";
				case HBAO.Quality.High:
					return "DIRECTIONS_8";
				case HBAO.Quality.Highest:
					return "DIRECTIONS_8";
				default:
					return "DIRECTIONS_6";
				}
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x00004B38 File Offset: 0x00002D38
			public static string GetStepsKeyword(HBAO.GeneralSettings settings)
			{
				switch (settings.quality)
				{
				case HBAO.Quality.Lowest:
					return "STEPS_2";
				case HBAO.Quality.Low:
					return "STEPS_3";
				case HBAO.Quality.Medium:
					return "STEPS_4";
				case HBAO.Quality.High:
					return "STEPS_4";
				case HBAO.Quality.Highest:
					return "STEPS_6";
				default:
					return "STEPS_4";
				}
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x00004B8C File Offset: 0x00002D8C
			public static string GetNoiseKeyword(HBAO.GeneralSettings settings)
			{
				switch (settings.noiseType)
				{
				case HBAO.NoiseType.InterleavedGradientNoise:
					return "INTERLEAVED_GRADIENT_NOISE";
				}
				return "__";
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x00004BC0 File Offset: 0x00002DC0
			public static string GetDeinterleavingKeyword(HBAO.GeneralSettings settings)
			{
				HBAO.Deinterleaving deinterleaving = settings.deinterleaving;
				if (deinterleaving != HBAO.Deinterleaving.Disabled && deinterleaving == HBAO.Deinterleaving.x4)
				{
					return "DEINTERLEAVED";
				}
				return "__";
			}

			// Token: 0x060000AA RID: 170 RVA: 0x00004BE8 File Offset: 0x00002DE8
			public static string GetDebugKeyword(HBAO.GeneralSettings settings)
			{
				switch (settings.debugMode)
				{
				case HBAO.DebugMode.AOOnly:
					return "DEBUG_AO";
				case HBAO.DebugMode.ColorBleedingOnly:
					return "DEBUG_COLORBLEEDING";
				case HBAO.DebugMode.SplitWithoutAOAndWithAO:
					return "DEBUG_NOAO_AO";
				case HBAO.DebugMode.SplitWithAOAndAOOnly:
					return "DEBUG_AO_AOONLY";
				case HBAO.DebugMode.SplitWithoutAOAndAOOnly:
					return "DEBUG_NOAO_AOONLY";
				}
				return "__";
			}

			// Token: 0x060000AB RID: 171 RVA: 0x00004C3F File Offset: 0x00002E3F
			public static string GetMultibounceKeyword(HBAO.AOSettings settings)
			{
				if (!settings.useMultiBounce)
				{
					return "__";
				}
				return "MULTIBOUNCE";
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00004C54 File Offset: 0x00002E54
			public static string GetOffscreenSamplesContributionKeyword(HBAO.AOSettings settings)
			{
				if (settings.offscreenSamplesContribution <= 0f)
				{
					return "__";
				}
				return "OFFSCREEN_SAMPLES_CONTRIBUTION";
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00004C70 File Offset: 0x00002E70
			public static string GetPerPixelNormalsKeyword(HBAO.AOSettings settings)
			{
				switch (settings.perPixelNormals)
				{
				case HBAO.PerPixelNormals.Camera:
					return "NORMALS_CAMERA";
				case HBAO.PerPixelNormals.Reconstruct:
					return "NORMALS_RECONSTRUCT";
				}
				return "__";
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00004CAC File Offset: 0x00002EAC
			public static string GetBlurRadiusKeyword(HBAO.BlurSettings settings)
			{
				switch (settings.type)
				{
				case HBAO.BlurType.Narrow:
					return "BLUR_RADIUS_2";
				case HBAO.BlurType.Medium:
					return "BLUR_RADIUS_3";
				case HBAO.BlurType.Wide:
					return "BLUR_RADIUS_4";
				case HBAO.BlurType.ExtraWide:
					return "BLUR_RADIUS_5";
				}
				return "BLUR_RADIUS_3";
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00004CFC File Offset: 0x00002EFC
			public static string GetVarianceClippingKeyword(HBAO.TemporalFilterSettings settings)
			{
				switch (settings.varianceClipping)
				{
				case HBAO.VarianceClipping._4Tap:
					return "VARIANCE_CLIPPING_4TAP";
				case HBAO.VarianceClipping._8Tap:
					return "VARIANCE_CLIPPING_8TAP";
				}
				return "__";
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00004D35 File Offset: 0x00002F35
			public static string GetColorBleedingKeyword(HBAO.ColorBleedingSettings settings)
			{
				if (!settings.enabled)
				{
					return "__";
				}
				return "COLOR_BLEEDING";
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00004D4A File Offset: 0x00002F4A
			public static string GetLightingLogEncodedKeyword(bool hdr)
			{
				if (!hdr)
				{
					return "LIGHTING_LOG_ENCODED";
				}
				return "__";
			}

			// Token: 0x0400008E RID: 142
			public static int mainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x0400008F RID: 143
			public static int hbaoTex = Shader.PropertyToID("_HBAOTex");

			// Token: 0x04000090 RID: 144
			public static int tempTex = Shader.PropertyToID("_TempTex");

			// Token: 0x04000091 RID: 145
			public static int tempTex2 = Shader.PropertyToID("_TempTex2");

			// Token: 0x04000092 RID: 146
			public static int noiseTex = Shader.PropertyToID("_NoiseTex");

			// Token: 0x04000093 RID: 147
			public static int depthTex = Shader.PropertyToID("_DepthTex");

			// Token: 0x04000094 RID: 148
			public static int normalsTex = Shader.PropertyToID("_NormalsTex");

			// Token: 0x04000095 RID: 149
			public static int[] depthSliceTex = new int[16];

			// Token: 0x04000096 RID: 150
			public static int[] normalsSliceTex = new int[16];

			// Token: 0x04000097 RID: 151
			public static int[] aoSliceTex = new int[16];

			// Token: 0x04000098 RID: 152
			public static int[] deinterleaveOffset;

			// Token: 0x04000099 RID: 153
			public static int atlasOffset;

			// Token: 0x0400009A RID: 154
			public static int jitter;

			// Token: 0x0400009B RID: 155
			public static int uvTransform;

			// Token: 0x0400009C RID: 156
			public static int inputTexelSize;

			// Token: 0x0400009D RID: 157
			public static int aoTexelSize;

			// Token: 0x0400009E RID: 158
			public static int deinterleavedAOTexelSize;

			// Token: 0x0400009F RID: 159
			public static int reinterleavedAOTexelSize;

			// Token: 0x040000A0 RID: 160
			public static int uvToView;

			// Token: 0x040000A1 RID: 161
			public static int worldToCameraMatrix;

			// Token: 0x040000A2 RID: 162
			public static int targetScale;

			// Token: 0x040000A3 RID: 163
			public static int radius;

			// Token: 0x040000A4 RID: 164
			public static int maxRadiusPixels;

			// Token: 0x040000A5 RID: 165
			public static int negInvRadius2;

			// Token: 0x040000A6 RID: 166
			public static int angleBias;

			// Token: 0x040000A7 RID: 167
			public static int aoMultiplier;

			// Token: 0x040000A8 RID: 168
			public static int intensity;

			// Token: 0x040000A9 RID: 169
			public static int multiBounceInfluence;

			// Token: 0x040000AA RID: 170
			public static int offscreenSamplesContrib;

			// Token: 0x040000AB RID: 171
			public static int maxDistance;

			// Token: 0x040000AC RID: 172
			public static int distanceFalloff;

			// Token: 0x040000AD RID: 173
			public static int baseColor;

			// Token: 0x040000AE RID: 174
			public static int colorBleedSaturation;

			// Token: 0x040000AF RID: 175
			public static int albedoMultiplier;

			// Token: 0x040000B0 RID: 176
			public static int colorBleedBrightnessMask;

			// Token: 0x040000B1 RID: 177
			public static int colorBleedBrightnessMaskRange;

			// Token: 0x040000B2 RID: 178
			public static int blurDeltaUV;

			// Token: 0x040000B3 RID: 179
			public static int blurSharpness;

			// Token: 0x040000B4 RID: 180
			public static int temporalParams;
		}

		// Token: 0x02000018 RID: 24
		private static class MersenneTwister
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00004D5A File Offset: 0x00002F5A
			// Note: this type is marked as 'beforefieldinit'.
			static MersenneTwister()
			{
			}

			// Token: 0x040000B5 RID: 181
			public static float[] Numbers = new float[]
			{
				0.556725f,
				0.00552f,
				0.708315f,
				0.583199f,
				0.236644f,
				0.99238f,
				0.981091f,
				0.119804f,
				0.510866f,
				0.560499f,
				0.961497f,
				0.557862f,
				0.539955f,
				0.332871f,
				0.417807f,
				0.920779f,
				0.730747f,
				0.07669f,
				0.008562f,
				0.660104f,
				0.428921f,
				0.511342f,
				0.587871f,
				0.906406f,
				0.43798f,
				0.620309f,
				0.062196f,
				0.119485f,
				0.235646f,
				0.795892f,
				0.044437f,
				0.617311f
			};
		}
	}
}
