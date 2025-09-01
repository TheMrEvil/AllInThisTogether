using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace VolumetricLights
{
	// Token: 0x02000023 RID: 35
	[CreateAssetMenu(menuName = "Data/Volumetric Light Profile", fileName = "VolumetricLightProfile", order = 335)]
	public class VolumetricLightProfile : ScriptableObject
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00007551 File Offset: 0x00005751
		private void OnEnable()
		{
			if (this.noiseTexture == null)
			{
				this.noiseTexture = Resources.Load<Texture3D>("Textures/NoiseTex3D1");
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007574 File Offset: 0x00005774
		private void OnValidate()
		{
			RaymarchPresets raymarchPresets = this.raymarchPreset;
			if (raymarchPresets <= RaymarchPresets.Faster)
			{
				if (raymarchPresets != RaymarchPresets.Default)
				{
					if (raymarchPresets == RaymarchPresets.Faster)
					{
						this.raymarchQuality = 4;
						this.raymarchMinStep = 0.2f;
						this.jittering = 1f;
					}
				}
				else
				{
					this.raymarchQuality = 8;
					this.raymarchMinStep = 0.1f;
					this.jittering = 0.5f;
				}
			}
			else if (raymarchPresets != RaymarchPresets.EvenFaster)
			{
				if (raymarchPresets == RaymarchPresets.LightSpeed)
				{
					this.raymarchQuality = 1;
					this.raymarchMinStep = 8f;
					this.jittering = 4f;
				}
			}
			else
			{
				this.raymarchQuality = 2;
				this.raymarchMinStep = 1f;
				this.jittering = 4f;
			}
			this.tipRadius = Mathf.Max(0f, this.tipRadius);
			this.density = Mathf.Max(0f, this.density);
			this.noiseScale = Mathf.Max(0.1f, this.noiseScale);
			this.diffusionIntensity = Mathf.Max(0f, this.diffusionIntensity);
			this.dustMaxSize = Mathf.Max(this.dustMaxSize, this.dustMinSize);
			this.rangeFallOff = Mathf.Max(this.rangeFallOff, 0f);
			this.brightness = Mathf.Max(this.brightness, 0f);
			this.penumbra = Mathf.Max(0.002f, this.penumbra);
			this.attenCoefConstant = Mathf.Max(0.0001f, this.attenCoefConstant);
			this.attenCoefLinear = Mathf.Max(0f, this.attenCoefLinear);
			this.attenCoefQuadratic = Mathf.Max(0f, this.attenCoefQuadratic);
			this.dustBrightness = Mathf.Max(0f, this.dustBrightness);
			this.dustMinSize = Mathf.Max(0f, this.dustMinSize);
			this.dustMaxSize = Mathf.Max(0f, this.dustMaxSize);
			this.shadowNearDistance = Mathf.Max(0f, this.shadowNearDistance);
			this.dustDistanceAttenuation = Mathf.Max(0f, this.dustDistanceAttenuation);
			this.raymarchMinStep = Mathf.Max(0.1f, this.raymarchMinStep);
			this.jittering = Mathf.Max(0f, this.jittering);
			this.dithering = Mathf.Max(0f, this.dithering);
			this.distanceStartDimming = Mathf.Max(0f, this.distanceStartDimming);
			this.distanceDeactivation = Mathf.Max(0f, this.distanceDeactivation);
			this.distanceStartDimming = Mathf.Min(this.distanceStartDimming, this.distanceDeactivation);
			this.shadowIntensity = Mathf.Max(0f, this.shadowIntensity);
			this.raymarchMaxSteps = Mathf.Max(1, this.raymarchMaxSteps);
			this.shadowTranslucencyIntensity = Mathf.Max(0f, this.shadowTranslucencyIntensity);
			foreach (VolumetricLight volumetricLight in UnityEngine.Object.FindObjectsOfType<VolumetricLight>())
			{
				if (volumetricLight != null && volumetricLight.profileSync && volumetricLight.profile == this)
				{
					this.ApplyTo(volumetricLight);
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007884 File Offset: 0x00005A84
		public void ApplyTo(VolumetricLight vl)
		{
			vl.blendMode = this.blendMode;
			vl.raymarchPreset = this.raymarchPreset;
			vl.raymarchMinStep = this.raymarchMinStep;
			vl.raymarchQuality = this.raymarchQuality;
			vl.raymarchMaxSteps = this.raymarchMaxSteps;
			vl.jittering = this.jittering;
			vl.dithering = this.dithering;
			vl.useBlueNoise = this.useBlueNoise;
			vl.animatedBlueNoise = this.animatedBlueNoise;
			vl.renderQueue = this.renderQueue;
			vl.sortingLayerID = this.sortingLayerID;
			vl.sortingOrder = this.sortingOrder;
			vl.alwaysOn = this.alwaysOn;
			vl.castDirectLight = this.castDirectLight;
			vl.directLightMultiplier = this.directLightMultiplier;
			vl.directLightSmoothSamples = this.directLightSmoothSamples;
			vl.directLightSmoothRadius = this.directLightSmoothRadius;
			vl.directLightBlendMode = this.directLightBlendMode;
			vl.autoToggle = this.autoToggle;
			vl.distanceStartDimming = this.distanceStartDimming;
			vl.distanceDeactivation = this.distanceDeactivation;
			vl.autoToggleCheckInterval = this.autoToggleCheckInterval;
			vl.useNoise = this.useNoise;
			vl.noiseTexture = this.noiseTexture;
			vl.noiseStrength = this.noiseStrength;
			vl.noiseScale = this.noiseScale;
			vl.noiseFinalMultiplier = this.noiseFinalMultiplier;
			vl.density = this.density;
			vl.mediumAlbedo = this.mediumAlbedo;
			vl.brightness = this.brightness;
			vl.attenuationMode = this.attenuationMode;
			vl.attenCoefConstant = this.attenCoefConstant;
			vl.attenCoefLinear = this.attenCoefLinear;
			vl.attenCoefQuadratic = this.attenCoefQuadratic;
			vl.rangeFallOff = this.rangeFallOff;
			vl.diffusionIntensity = this.diffusionIntensity;
			vl.penumbra = this.penumbra;
			vl.tipRadius = this.tipRadius;
			vl.cookieTexture = this.cookieTexture;
			vl.cookieScale = this.cookieScale;
			vl.cookieSpeed = this.cookieSpeed;
			vl.cookieOffset = this.cookieOffset;
			vl.frustumAngle = this.frustumAngle;
			vl.windDirection = this.windDirection;
			vl.enableDustParticles = this.enableDustParticles;
			vl.dustBrightness = this.dustBrightness;
			vl.dustMinSize = this.dustMinSize;
			vl.dustMaxSize = this.dustMaxSize;
			vl.dustWindSpeed = this.dustWindSpeed;
			vl.dustDistanceAttenuation = this.dustDistanceAttenuation;
			vl.dustAutoToggle = this.dustAutoToggle;
			vl.dustDistanceDeactivation = this.dustDistanceDeactivation;
			vl.dustPrewarm = this.dustPrewarm;
			vl.enableShadows = this.enableShadows;
			vl.shadowIntensity = this.shadowIntensity;
			vl.shadowTranslucency = this.shadowTranslucency;
			vl.shadowTranslucencyIntensity = this.shadowTranslucencyIntensity;
			vl.shadowTranslucencyBlend = this.shadowTranslucencyBlend;
			vl.shadowResolution = this.shadowResolution;
			vl.shadowCullingMask = this.shadowCullingMask;
			vl.shadowBakeInterval = this.shadowBakeInterval;
			vl.shadowNearDistance = this.shadowNearDistance;
			vl.shadowAutoToggle = this.shadowAutoToggle;
			vl.shadowDistanceDeactivation = this.shadowDistanceDeactivation;
			vl.shadowBakeMode = this.shadowBakeMode;
			vl.shadowUseDefaultRTFormat = this.shadowUseDefaultRTFormat;
			vl.shadowOptimizeShadowCasters = this.shadowOptimizeShadowCasters;
			vl.shadowOrientation = this.shadowOrientation;
			vl.shadowDirection = this.shadowDirection;
			vl.UpdateMaterialProperties();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public void LoadFrom(VolumetricLight vl)
		{
			this.blendMode = vl.blendMode;
			this.raymarchPreset = vl.raymarchPreset;
			this.raymarchMinStep = vl.raymarchMinStep;
			this.raymarchQuality = vl.raymarchQuality;
			this.raymarchMaxSteps = vl.raymarchMaxSteps;
			this.jittering = vl.jittering;
			this.dithering = vl.dithering;
			this.useBlueNoise = vl.useBlueNoise;
			this.animatedBlueNoise = vl.animatedBlueNoise;
			this.renderQueue = vl.renderQueue;
			this.sortingLayerID = vl.sortingLayerID;
			this.sortingOrder = vl.sortingOrder;
			this.alwaysOn = vl.alwaysOn;
			this.castDirectLight = vl.castDirectLight;
			this.directLightMultiplier = vl.directLightMultiplier;
			this.directLightSmoothSamples = vl.directLightSmoothSamples;
			this.directLightSmoothRadius = vl.directLightSmoothRadius;
			this.directLightBlendMode = vl.directLightBlendMode;
			this.autoToggle = vl.autoToggle;
			this.distanceStartDimming = vl.distanceStartDimming;
			this.distanceDeactivation = vl.distanceDeactivation;
			this.autoToggleCheckInterval = vl.autoToggleCheckInterval;
			this.useNoise = vl.useNoise;
			this.noiseTexture = vl.noiseTexture;
			this.noiseStrength = vl.noiseStrength;
			this.noiseScale = vl.noiseScale;
			this.noiseFinalMultiplier = vl.noiseFinalMultiplier;
			this.density = vl.density;
			this.mediumAlbedo = vl.mediumAlbedo;
			this.brightness = vl.brightness;
			this.attenuationMode = vl.attenuationMode;
			this.attenCoefConstant = vl.attenCoefConstant;
			this.attenCoefLinear = vl.attenCoefLinear;
			this.attenCoefQuadratic = vl.attenCoefQuadratic;
			this.rangeFallOff = vl.rangeFallOff;
			this.diffusionIntensity = vl.diffusionIntensity;
			this.penumbra = vl.penumbra;
			this.tipRadius = vl.tipRadius;
			this.cookieTexture = vl.cookieTexture;
			this.cookieScale = vl.cookieScale;
			this.cookieOffset = vl.cookieOffset;
			this.cookieSpeed = vl.cookieSpeed;
			this.frustumAngle = vl.frustumAngle;
			this.windDirection = vl.windDirection;
			this.enableDustParticles = vl.enableDustParticles;
			this.dustBrightness = vl.dustBrightness;
			this.dustMinSize = vl.dustMinSize;
			this.dustMaxSize = vl.dustMaxSize;
			this.dustWindSpeed = vl.dustWindSpeed;
			this.dustDistanceAttenuation = vl.dustDistanceAttenuation;
			this.dustPrewarm = vl.dustPrewarm;
			this.dustAutoToggle = vl.dustAutoToggle;
			this.dustDistanceDeactivation = vl.dustDistanceDeactivation;
			this.enableShadows = vl.enableShadows;
			this.shadowIntensity = vl.shadowIntensity;
			this.shadowTranslucency = vl.shadowTranslucency;
			this.updateTranslucency = vl.updateTranslucency;
			this.shadowTranslucencyIntensity = vl.shadowTranslucencyIntensity;
			this.shadowTranslucencyBlend = vl.shadowTranslucencyBlend;
			this.shadowResolution = vl.shadowResolution;
			this.shadowCullingMask = vl.shadowCullingMask;
			this.shadowBakeInterval = vl.shadowBakeInterval;
			this.shadowNearDistance = vl.shadowNearDistance;
			this.shadowAutoToggle = vl.shadowAutoToggle;
			this.shadowDistanceDeactivation = vl.shadowDistanceDeactivation;
			this.shadowBakeMode = vl.shadowBakeMode;
			this.shadowUseDefaultRTFormat = vl.shadowUseDefaultRTFormat;
			this.shadowOptimizeShadowCasters = vl.shadowOptimizeShadowCasters;
			this.shadowOrientation = vl.shadowOrientation;
			this.shadowDirection = vl.shadowDirection;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007F2C File Offset: 0x0000612C
		public VolumetricLightProfile()
		{
		}

		// Token: 0x0400010E RID: 270
		[Header("Rendering")]
		public BlendMode blendMode;

		// Token: 0x0400010F RID: 271
		public RaymarchPresets raymarchPreset;

		// Token: 0x04000110 RID: 272
		[Tooltip("Determines the general accuracy of the effect. The greater this value, the more accurate effect (shadow occlusion as well). Try to keep this value as low as possible while maintainig a good visual result. If you need better performance increase the 'Raymarch Min Step' and then 'Jittering' amount to improve quality.")]
		[Range(1f, 256f)]
		public int raymarchQuality = 8;

		// Token: 0x04000111 RID: 273
		[Tooltip("Determines the minimum step size. Increase to improve performance / decrease to improve accuracy. When increasing this value, you can also increase 'Jittering' amount to improve quality.")]
		public float raymarchMinStep = 0.1f;

		// Token: 0x04000112 RID: 274
		[Tooltip("Maximum number of raymarch steps. This value represents a hard maximum, usually each ray uses less samples but it can be topped by lowering this value.")]
		public int raymarchMaxSteps = 200;

		// Token: 0x04000113 RID: 275
		[Tooltip("Increase to reduce inaccuracy due to low number of samples (due to a high raymarch step size).")]
		public float jittering = 0.5f;

		// Token: 0x04000114 RID: 276
		[Tooltip("Increase to reduce banding artifacts. Usually jittering has a bigger impact in reducing artifacts.")]
		[Range(0f, 2f)]
		public float dithering = 1f;

		// Token: 0x04000115 RID: 277
		[Tooltip("Uses blue noise for jittering computation reducing moiré pattern of normal jitter. Usually not needed unless you use shadow occlusion. It adds an additional texture fetch so use only if it provides you a clear visual improvement.")]
		public bool useBlueNoise;

		// Token: 0x04000116 RID: 278
		public bool animatedBlueNoise = true;

		// Token: 0x04000117 RID: 279
		[Tooltip("The render queue for this renderer. By default, all transparent objects use a render queue of 3000. Use a lower value to render before all transparent objects.")]
		public int renderQueue = 3101;

		// Token: 0x04000118 RID: 280
		[Tooltip("Optional sorting layer Id (number) for this renderer. By default 0. Usually used to control the order with other transparent renderers, like Sprite Renderer.")]
		public int sortingLayerID;

		// Token: 0x04000119 RID: 281
		[Tooltip("Optional sorting order for this renderer. Used to control the order with other transparent renderers, like Sprite Renderer.")]
		public int sortingOrder;

		// Token: 0x0400011A RID: 282
		[Tooltip("Ignores light enable state. Always show volumetric fog. This option is useful to produce fake volumetric lights.")]
		public bool alwaysOn;

		// Token: 0x0400011B RID: 283
		[Tooltip("If the volumetric light effect may illuminate the objects. Only available in deferred rendering path.")]
		public bool castDirectLight;

		// Token: 0x0400011C RID: 284
		[Tooltip("Multiplier for the direct light")]
		public float directLightMultiplier = 1f;

		// Token: 0x0400011D RID: 285
		[Tooltip("Number of samples to creates smooth shadows")]
		[Range(1f, 32f)]
		public int directLightSmoothSamples = 8;

		// Token: 0x0400011E RID: 286
		[Tooltip("Radius for the smooth shadow")]
		[Range(0f, 8f)]
		public float directLightSmoothRadius = 4f;

		// Token: 0x0400011F RID: 287
		[Tooltip("Blending mode for the direct light effect. Blend mode requires deferred rendering mode and is slower.")]
		public DirectLightBlendMode directLightBlendMode;

		// Token: 0x04000120 RID: 288
		[Tooltip("Fully enable/disable volumetric effect when far from main camera in order to optimize performance")]
		public bool autoToggle;

		// Token: 0x04000121 RID: 289
		[Tooltip("Distance to the light source at which the volumetric effect starts to dim. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float distanceStartDimming = 70f;

		// Token: 0x04000122 RID: 290
		[Tooltip("Distance to the light source at which the volumetric effect is fully deactivated. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float distanceDeactivation = 100f;

		// Token: 0x04000123 RID: 291
		[Tooltip("Minimum time between distance checks")]
		public float autoToggleCheckInterval = 1f;

		// Token: 0x04000124 RID: 292
		[Header("Appearance")]
		public bool useNoise = true;

		// Token: 0x04000125 RID: 293
		public Texture3D noiseTexture;

		// Token: 0x04000126 RID: 294
		[Range(0f, 3f)]
		public float noiseStrength = 1f;

		// Token: 0x04000127 RID: 295
		public float noiseScale = 5f;

		// Token: 0x04000128 RID: 296
		public float noiseFinalMultiplier = 1f;

		// Token: 0x04000129 RID: 297
		public float density = 0.2f;

		// Token: 0x0400012A RID: 298
		public Color mediumAlbedo = Color.white;

		// Token: 0x0400012B RID: 299
		[Tooltip("Overall brightness multiplier.")]
		public float brightness = 1f;

		// Token: 0x0400012C RID: 300
		[Tooltip("Attenuation Mode")]
		public AttenuationMode attenuationMode;

		// Token: 0x0400012D RID: 301
		[Tooltip("Constant coefficient (a) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefConstant = 1f;

		// Token: 0x0400012E RID: 302
		[Tooltip("Linear coefficient (b) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefLinear = 2f;

		// Token: 0x0400012F RID: 303
		[Tooltip("Quadratic coefficient (c) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefQuadratic = 1f;

		// Token: 0x04000130 RID: 304
		[Tooltip("Attenuation of light intensity based on square of distance. Plays with brightness to achieve a more linear or realistic (quadratic attenuation effect).")]
		[FormerlySerializedAs("distanceFallOff")]
		public float rangeFallOff = 1f;

		// Token: 0x04000131 RID: 305
		[Tooltip("Brightiness increase when looking against light source.")]
		public float diffusionIntensity;

		// Token: 0x04000132 RID: 306
		[Range(0f, 1f)]
		[Tooltip("Smooth edges")]
		[FormerlySerializedAs("border")]
		public float penumbra = 0.5f;

		// Token: 0x04000133 RID: 307
		[Header("Spot Light")]
		[Tooltip("Radius of the tip of the cone. Only applies to spot lights.")]
		public float tipRadius;

		// Token: 0x04000134 RID: 308
		[Tooltip("Custom cookie texture (RGB).")]
		public Texture2D cookieTexture;

		// Token: 0x04000135 RID: 309
		public Vector2 cookieScale = Vector2.one;

		// Token: 0x04000136 RID: 310
		public Vector2 cookieOffset;

		// Token: 0x04000137 RID: 311
		public Vector2 cookieSpeed;

		// Token: 0x04000138 RID: 312
		[Header("Area Light")]
		[Range(0f, 80f)]
		public float frustumAngle;

		// Token: 0x04000139 RID: 313
		[Header("Animation")]
		[Tooltip("Noise animation direction and speed.")]
		public Vector3 windDirection = new Vector3(0.03f, 0.02f, 0f);

		// Token: 0x0400013A RID: 314
		[Header("Dust Particles")]
		public bool enableDustParticles;

		// Token: 0x0400013B RID: 315
		public float dustBrightness = 0.6f;

		// Token: 0x0400013C RID: 316
		public float dustMinSize = 0.01f;

		// Token: 0x0400013D RID: 317
		public float dustMaxSize = 0.02f;

		// Token: 0x0400013E RID: 318
		public float dustWindSpeed = 1f;

		// Token: 0x0400013F RID: 319
		[Tooltip("Dims particle intensity beyond this distance to camera")]
		public float dustDistanceAttenuation = 10f;

		// Token: 0x04000140 RID: 320
		[Tooltip("Fully enable/disable particle system renderer when far from main camera in order to optimize performance")]
		public bool dustAutoToggle;

		// Token: 0x04000141 RID: 321
		[Tooltip("Distance to the light source at which the particule system is fully deactivated. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float dustDistanceDeactivation = 70f;

		// Token: 0x04000142 RID: 322
		[Tooltip("Prewarms/populates dust when the volumetric light is enabled to ensure there're enough visible particles from start. Disabling this option can improve performance when many lights are activated at the same time.")]
		public bool dustPrewarm = true;

		// Token: 0x04000143 RID: 323
		[Header("Shadow Occlusion")]
		public bool enableShadows;

		// Token: 0x04000144 RID: 324
		public float shadowIntensity = 0.7f;

		// Token: 0x04000145 RID: 325
		[Tooltip("Enable translucent shadow map")]
		public bool shadowTranslucency;

		// Token: 0x04000146 RID: 326
		[Tooltip("Customizable intensity for the translucent map sampling")]
		public float shadowTranslucencyIntensity = 1f;

		// Token: 0x04000147 RID: 327
		public bool updateTranslucency;

		// Token: 0x04000148 RID: 328
		[Tooltip("Amount of colorization")]
		[Range(0f, 1f)]
		public float shadowTranslucencyBlend = 0.5f;

		// Token: 0x04000149 RID: 329
		public ShadowResolution shadowResolution = ShadowResolution._256;

		// Token: 0x0400014A RID: 330
		public LayerMask shadowCullingMask = -3;

		// Token: 0x0400014B RID: 331
		public ShadowBakeInterval shadowBakeInterval;

		// Token: 0x0400014C RID: 332
		public float shadowNearDistance = 0.1f;

		// Token: 0x0400014D RID: 333
		[Tooltip("Fully enable/disable shadows when far from main camera in order to optimize performance")]
		public bool shadowAutoToggle;

		// Token: 0x0400014E RID: 334
		[Tooltip("Max distance to main camera to render shadows for this volumetric light.")]
		public float shadowDistanceDeactivation = 250f;

		// Token: 0x0400014F RID: 335
		[Tooltip("Compute shadows in a half-sphere oriented to camera (faster) or in a cubemap but render one face per frame (slower) or all 6 faces per frame (slowest).")]
		public ShadowBakeMode shadowBakeMode;

		// Token: 0x04000150 RID: 336
		[Tooltip("Use default render texture format when rendering shadows. Disabling this option will force shadows to render to a single depth buffer, which is faster, but can cause issues with shaders using Grab Pass.")]
		public bool shadowUseDefaultRTFormat = true;

		// Token: 0x04000151 RID: 337
		[Tooltip("When enabled, a fast shader will be used to render object shadows instead of their regular shaders.")]
		public bool shadowOptimizeShadowCasters = true;

		// Token: 0x04000152 RID: 338
		public ShadowOrientation shadowOrientation;

		// Token: 0x04000153 RID: 339
		public Vector3 shadowDirection = Vector3.down;
	}
}
