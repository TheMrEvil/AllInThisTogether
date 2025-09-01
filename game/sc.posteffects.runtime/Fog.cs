using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000018 RID: 24
	[PostProcess(typeof(FogRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Environment/Screen-Space Fog", true)]
	[Serializable]
	public sealed class Fog : PostProcessEffectSettings
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003C8B File Offset: 0x00001E8B
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C98 File Offset: 0x00001E98
		public Fog()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003FA8 File Offset: 0x000021A8
		// Note: this type is marked as 'beforefieldinit'.
		static Fog()
		{
		}

		// Token: 0x04000059 RID: 89
		[DisplayName("Use scene's settings")]
		[Tooltip("Use the settings of the current active scene found under the Lighting tab\n\nThis is also advisable for third-party scripts that modify fog settings\n\nThis will force the effect to use the scene's fog color")]
		public BoolParameter useSceneSettings = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400005A RID: 90
		[DisplayName("Mode")]
		[Tooltip("Sets how the fog distance is calculated")]
		public Fog.FogModeParameter fogMode = new Fog.FogModeParameter
		{
			value = FogMode.Exponential
		};

		// Token: 0x0400005B RID: 91
		[Range(0f, 1f)]
		public FloatParameter globalDensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400005C RID: 92
		[DisplayName("Start")]
		public FloatParameter fogStartDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400005D RID: 93
		[DisplayName("End")]
		public FloatParameter fogEndDistance = new FloatParameter
		{
			value = 600f
		};

		// Token: 0x0400005E RID: 94
		[Space]
		[Tooltip("Color: use a uniform color for the fog\n\nGradient: sample a gradient texture to control the fog color over distance, the alpha channel controls the density\n\nSkybox: Sample the skybox's color for the fog, only works well with low detail skies")]
		public Fog.FogColorSourceParameter colorSource = new Fog.FogColorSourceParameter
		{
			value = Fog.FogColorSource.UniformColor
		};

		// Token: 0x0400005F RID: 95
		[DisplayName("Mipmap")]
		[Tooltip("Set the mipmap level for the skybox texture")]
		[Range(0f, 8f)]
		public FloatParameter skyboxMipLevel = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000060 RID: 96
		[DisplayName("Color")]
		[ColorUsage(true, true)]
		public ColorParameter fogColor = new ColorParameter
		{
			value = new Color(0.76f, 0.94f, 1f, 1f)
		};

		// Token: 0x04000061 RID: 97
		[DisplayName("Texture")]
		public TextureParameter fogColorGradient = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000062 RID: 98
		[Tooltip("Automatic mode uses the current camera's far clipping plane to set the max distance\n\nOtherwise, a fixed value may be used instead")]
		public FloatParameter gradientDistance = new FloatParameter
		{
			value = 1000f
		};

		// Token: 0x04000063 RID: 99
		public BoolParameter gradientUseFarClipPlane = new BoolParameter
		{
			value = true
		};

		// Token: 0x04000064 RID: 100
		[Header("Distance")]
		[DisplayName("Enable")]
		public BoolParameter distanceFog = new BoolParameter
		{
			value = true
		};

		// Token: 0x04000065 RID: 101
		[Range(0.001f, 1f)]
		[DisplayName("Density")]
		public FloatParameter distanceDensity = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000066 RID: 102
		[Tooltip("Distance based on radial distance from viewer, rather than parrallel")]
		public BoolParameter useRadialDistance = new BoolParameter
		{
			value = true
		};

		// Token: 0x04000067 RID: 103
		[Header("Skybox")]
		[Range(0f, 1f)]
		[Tooltip("Determines how much the fog influences the skybox")]
		public FloatParameter skyboxInfluence = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000068 RID: 104
		[Header("Directional Light")]
		[Tooltip("Translates the a Directional Light's direction and color into the fog. Creates a faux-atmospheric scattering effect.")]
		public BoolParameter enableDirectionalLight = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000069 RID: 105
		[Tooltip("Use the intensity of the Directional Light that's set as the caster")]
		public BoolParameter useLightDirection = new BoolParameter
		{
			value = true
		};

		// Token: 0x0400006A RID: 106
		[Tooltip("Use the color of the Directional Light that's set as the caster")]
		public BoolParameter useLightColor = new BoolParameter
		{
			value = true
		};

		// Token: 0x0400006B RID: 107
		[Tooltip("Use the intensity of the Directional Light that's set as the caster")]
		public BoolParameter useLightIntensity = new BoolParameter
		{
			value = true
		};

		// Token: 0x0400006C RID: 108
		public static Vector3 LightDirection = Vector3.zero;

		// Token: 0x0400006D RID: 109
		[DisplayName("Color")]
		[ColorUsage(true, true)]
		public ColorParameter lightColor = new ColorParameter
		{
			value = new Color(1f, 0.89f, 0.55f, 1f)
		};

		// Token: 0x0400006E RID: 110
		[Max(1f)]
		public Vector3Parameter lightDirection = new Vector3Parameter
		{
			value = new Vector3(0f, 0.5f, -1f)
		};

		// Token: 0x0400006F RID: 111
		public FloatParameter lightIntensity = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000070 RID: 112
		[Header("Height")]
		[DisplayName("Enable")]
		[Tooltip("Enable vertical height fog")]
		public BoolParameter heightFog = new BoolParameter
		{
			value = true
		};

		// Token: 0x04000071 RID: 113
		[Tooltip("Height relative to 0 world height position")]
		public FloatParameter height = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x04000072 RID: 114
		[Range(0.001f, 1f)]
		[DisplayName("Density")]
		public FloatParameter heightDensity = new FloatParameter
		{
			value = 0.75f
		};

		// Token: 0x04000073 RID: 115
		[Header("Height noise (2D)")]
		[DisplayName("Enable")]
		[Tooltip("Enables height fog density variation through the use of a texture")]
		public BoolParameter heightFogNoise = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000074 RID: 116
		[DisplayName("Texture (R)")]
		[Tooltip("The density is read from this texture's red color channel")]
		public TextureParameter heightNoiseTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000075 RID: 117
		[Range(0f, 1f)]
		[DisplayName("Size")]
		public FloatParameter heightNoiseSize = new FloatParameter
		{
			value = 0.25f
		};

		// Token: 0x04000076 RID: 118
		[Range(0f, 1f)]
		[DisplayName("Strength")]
		public FloatParameter heightNoiseStrength = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000077 RID: 119
		[Range(0f, 10f)]
		[DisplayName("Speed")]
		public FloatParameter heightNoiseSpeed = new FloatParameter
		{
			value = 2f
		};

		// Token: 0x04000078 RID: 120
		[Header("Light scattering")]
		[DisplayName("Enable")]
		[Tooltip("Execute a bloom pass to diffuse light in dense fog")]
		public BoolParameter lightScattering = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000079 RID: 121
		[Space]
		[UnityEngine.Rendering.PostProcessing.Min(0f)]
		[DisplayName("Intensity")]
		public FloatParameter scatterIntensity = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x0400007A RID: 122
		[UnityEngine.Rendering.PostProcessing.Min(0f)]
		[DisplayName("Threshold")]
		[Tooltip("Filters out pixels under this level of brightness. Value is in gamma-space.")]
		public FloatParameter scatterThreshold = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400007B RID: 123
		[Range(1f, 10f)]
		[DisplayName("Diffusion")]
		public FloatParameter scatterDiffusion = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x0400007C RID: 124
		[Range(0f, 1f)]
		[DisplayName("Smoothness")]
		[Tooltip("Makes transitions between under/over-threshold gradual. 0 for a hard threshold, 1 for a soft threshold).")]
		public FloatParameter scatterSoftKnee = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x0200005E RID: 94
		[Serializable]
		public sealed class FogModeParameter : ParameterOverride<FogMode>
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x00008518 File Offset: 0x00006718
			public FogModeParameter()
			{
			}
		}

		// Token: 0x0200005F RID: 95
		public enum FogColorSource
		{
			// Token: 0x0400018B RID: 395
			UniformColor,
			// Token: 0x0400018C RID: 396
			GradientTexture,
			// Token: 0x0400018D RID: 397
			SkyboxColor
		}

		// Token: 0x02000060 RID: 96
		[Serializable]
		public sealed class FogColorSourceParameter : ParameterOverride<Fog.FogColorSource>
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x00008520 File Offset: 0x00006720
			public FogColorSourceParameter()
			{
			}
		}
	}
}
