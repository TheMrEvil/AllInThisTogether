using System;
using Boxophobic.StyledGUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace AtmosphericHeightFog
{
	// Token: 0x02000007 RID: 7
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[ExecuteInEditMode]
	public class HeightFogGlobal : StyledMonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void OnEnable()
		{
			base.gameObject.name = "Height Fog Global";
			if (!this.manualPositionAndScale)
			{
				base.gameObject.transform.position = Vector3.zero;
				base.gameObject.transform.rotation = Quaternion.identity;
			}
			this.GetCamera();
			this.GetDirectional();
			if (this.mainCamera != null && (this.mainCamera.depthTextureMode != DepthTextureMode.Depth || this.mainCamera.depthTextureMode != DepthTextureMode.DepthNormals))
			{
				this.mainCamera.depthTextureMode = DepthTextureMode.Depth;
			}
			Mesh builtinResource = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
			base.gameObject.GetComponent<MeshFilter>().sharedMesh = builtinResource;
			this.localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
			this.localMaterial.name = "Local";
			this.overrideMaterial = new Material(this.localMaterial);
			this.overrideMaterial.name = "Override";
			this.blendMaterial = new Material(this.localMaterial);
			this.blendMaterial.name = "Blend";
			this.globalMaterial = new Material(Shader.Find("Hidden/BOXOPHOBIC/Atmospherics/Height Fog Global"));
			this.globalMaterial.name = "Height Fog Global";
			this.missingMaterial = Resources.Load<Material>("Height Fog Preset");
			this.meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
			this.meshRenderer.sharedMaterial = this.globalMaterial;
			this.meshRenderer.enabled = true;
			Shader.SetGlobalFloat("AHF_Enabled", 1f);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021D6 File Offset: 0x000003D6
		private void OnDisable()
		{
			base.gameObject.GetComponent<MeshRenderer>().enabled = false;
			Shader.SetGlobalFloat("AHF_Enabled", 0f);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021F8 File Offset: 0x000003F8
		private void Update()
		{
			if (this.mainCamera == null)
			{
				return;
			}
			if (!this.manualPositionAndScale)
			{
				this.SetFogSphereSize();
				this.SetFogSpherePosition();
			}
			if (this.renderMode == FogRendering.RenderAsGlobalOverlay)
			{
				this.meshRenderer.enabled = true;
			}
			else
			{
				this.meshRenderer.enabled = false;
			}
			this.currentMaterial = this.localMaterial;
			if (this.fogMode == FogMode.UseScriptSettings)
			{
				this.SetLocalMaterial();
				this.messageTimeOfDay = false;
				this.messagePreset = false;
			}
			else if (this.fogMode == FogMode.UsePresetSettings)
			{
				if (this.presetMaterial != null && this.presetMaterial.HasProperty("_IsHeightFogPreset"))
				{
					if (!Application.isPlaying)
					{
						this.currentMaterial = this.presetMaterial;
					}
					else if (this.wantMaterial != null)
					{
						this.currentMaterial.Lerp(this.currentMaterial, this.wantMaterial, Time.deltaTime * 0.25f);
					}
					else
					{
						this.currentMaterial.Lerp(this.currentMaterial, this.presetMaterial, this.hasSetupMaterial ? (Time.deltaTime * 0.25f) : 1f);
					}
					this.hasSetupMaterial = true;
					this.messagePreset = false;
				}
				else
				{
					this.currentMaterial = this.missingMaterial;
					this.messagePreset = true;
				}
				this.messageTimeOfDay = false;
			}
			else if (this.fogMode == FogMode.UseTimeOfDay)
			{
				if (this.presetDay != null && this.presetDay.HasProperty("_IsHeightFogPreset") && this.presetNight != null && this.presetNight.HasProperty("_IsHeightFogPreset"))
				{
					this.currentMaterial.Lerp(this.presetDay, this.presetNight, this.timeOfDay);
					this.messageTimeOfDay = false;
				}
				else
				{
					this.currentMaterial = this.missingMaterial;
					this.messageTimeOfDay = true;
				}
				this.messagePreset = false;
			}
			if (this.mainDirectional != null)
			{
				this.currentMaterial.SetVector("_DirectionalDir", -this.mainDirectional.transform.forward);
			}
			else
			{
				this.currentMaterial.SetVector("_DirectionalDir", Vector4.zero);
			}
			if (this.overrideCamToVolumeDistance > this.overrideVolumeDistanceFade)
			{
				this.blendMaterial.CopyPropertiesFromMaterial(this.currentMaterial);
			}
			else if (this.overrideCamToVolumeDistance < this.overrideVolumeDistanceFade)
			{
				float t = 1f - this.overrideCamToVolumeDistance / this.overrideVolumeDistanceFade;
				this.blendMaterial.Lerp(this.currentMaterial, this.overrideMaterial, t);
			}
			this.SetGlobalMaterials();
			this.SetRenderQueue();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002498 File Offset: 0x00000698
		private void GetCamera()
		{
			if (this.mainCamera == null)
			{
				this.mainCamera = Camera.main;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024B4 File Offset: 0x000006B4
		private void GetDirectional()
		{
			if (this.mainDirectional == null)
			{
				Light[] array = UnityEngine.Object.FindObjectsOfType<Light>();
				float num = 0f;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].type == LightType.Directional && array[i].intensity > num)
					{
						this.mainDirectional = array[i];
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002508 File Offset: 0x00000708
		public void SetFogIntensity(float intensity)
		{
			this.IntensityMultiplier = Mathf.Clamp(intensity, 0f, 1f);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002520 File Offset: 0x00000720
		private void SetLocalMaterial()
		{
			this.localMaterial.SetFloat("_FogIntensity", this.fogIntensity * this.IntensityMultiplier);
			this.localMaterial.SetColor("_FogColorStart", this.fogColorStart);
			this.localMaterial.SetColor("_FogColorEnd", this.fogColorEnd);
			this.localMaterial.SetFloat("_FogColorDuo", this.fogColorDuo);
			this.localMaterial.SetFloat("_FogDistanceStart", this.fogDistanceStart);
			this.localMaterial.SetFloat("_FogDistanceEnd", this.fogDistanceEnd);
			this.localMaterial.SetFloat("_FogDistanceFalloff", this.fogDistanceFalloff);
			this.localMaterial.SetFloat("_FogHeightStart", this.fogHeightStart);
			this.localMaterial.SetFloat("_FogHeightEnd", this.fogHeightEnd);
			this.localMaterial.SetFloat("_FogHeightFalloff", this.fogHeightFalloff);
			this.localMaterial.SetFloat("_FarDistanceHeight", this.farDistanceHeight);
			this.localMaterial.SetFloat("_FarDistanceOffset", this.farDistanceOffset);
			this.localMaterial.SetFloat("_SkyboxFogIntensity", this.skyboxFogIntensity);
			this.localMaterial.SetFloat("_SkyboxFogHeight", this.skyboxFogHeight);
			this.localMaterial.SetFloat("_SkyboxFogFalloff", this.skyboxFogFalloff);
			this.localMaterial.SetFloat("_SkyboxFogOffset", this.skyboxFogOffset);
			this.localMaterial.SetFloat("_SkyboxFogBottom", this.skyboxFogBottom);
			this.localMaterial.SetFloat("_SkyboxFogFill", this.skyboxFogFill);
			this.localMaterial.SetFloat("_DirectionalIntensity", this.directionalIntensity);
			this.localMaterial.SetFloat("_DirectionalFalloff", this.directionalFalloff);
			this.localMaterial.SetColor("_DirectionalColor", this.directionalColor);
			this.localMaterial.SetFloat("_NoiseIntensity", this.noiseIntensity);
			this.localMaterial.SetFloat("_NoiseMin", this.noiseMin);
			this.localMaterial.SetFloat("_NoiseMax", this.noiseMax);
			this.localMaterial.SetFloat("_NoiseScale", this.noiseScale);
			this.localMaterial.SetVector("_NoiseSpeed", this.noiseSpeed);
			this.localMaterial.SetFloat("_NoiseDistanceEnd", this.noiseDistanceEnd);
			this.localMaterial.SetFloat("_JitterIntensity", this.jitterIntensity);
			if (this.fogAxisMode == FogAxisMode.XAxis)
			{
				this.localMaterial.SetVector("_FogAxisOption", new Vector4(1f, 0f, 0f, 0f));
			}
			else if (this.fogAxisMode == FogAxisMode.YAxis)
			{
				this.localMaterial.SetVector("_FogAxisOption", new Vector4(0f, 1f, 0f, 0f));
			}
			else if (this.fogAxisMode == FogAxisMode.ZAxis)
			{
				this.localMaterial.SetVector("_FogAxisOption", new Vector4(0f, 0f, 1f, 0f));
			}
			if (this.fogLayersMode == FogLayersMode.MultiplyDistanceAndHeight)
			{
				this.localMaterial.SetFloat("_FogLayersMode", 0f);
			}
			else
			{
				this.localMaterial.SetFloat("_FogLayersMode", 1f);
			}
			if (this.fogCameraMode == FogCameraMode.Perspective)
			{
				this.localMaterial.SetFloat("_FogCameraMode", 0f);
				return;
			}
			if (this.fogCameraMode == FogCameraMode.Orthographic)
			{
				this.localMaterial.SetFloat("_FogCameraMode", 1f);
				return;
			}
			if (this.fogCameraMode == FogCameraMode.Both)
			{
				this.localMaterial.SetFloat("_FogCameraMode", 2f);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000028CC File Offset: 0x00000ACC
		private void SetGlobalMaterials()
		{
			if (!this.blendMaterial.HasProperty("_IsHeightFogPreset"))
			{
				return;
			}
			Shader.SetGlobalFloat("AHF_FogIntensity", this.blendMaterial.GetFloat("_FogIntensity") * this.IntensityMultiplier);
			Shader.SetGlobalVector("AHF_FogAxisOption", this.blendMaterial.GetVector("_FogAxisOption"));
			Shader.SetGlobalFloat("AHF_FogLayersMode", this.blendMaterial.GetFloat("_FogLayersMode"));
			Shader.SetGlobalColor("AHF_FogColorStart", this.blendMaterial.GetColor("_FogColorStart"));
			Shader.SetGlobalColor("AHF_FogColorEnd", this.blendMaterial.GetColor("_FogColorEnd"));
			Shader.SetGlobalFloat("AHF_FogColorDuo", this.blendMaterial.GetFloat("_FogColorDuo"));
			Shader.SetGlobalFloat("AHF_FogDistanceStart", this.blendMaterial.GetFloat("_FogDistanceStart"));
			Shader.SetGlobalFloat("AHF_FogDistanceEnd", this.blendMaterial.GetFloat("_FogDistanceEnd"));
			Shader.SetGlobalFloat("AHF_FogDistanceFalloff", this.blendMaterial.GetFloat("_FogDistanceFalloff"));
			Shader.SetGlobalFloat("AHF_FogHeightStart", this.blendMaterial.GetFloat("_FogHeightStart"));
			Shader.SetGlobalFloat("AHF_FogHeightEnd", this.blendMaterial.GetFloat("_FogHeightEnd"));
			Shader.SetGlobalFloat("AHF_FogHeightFalloff", this.blendMaterial.GetFloat("_FogHeightFalloff"));
			Shader.SetGlobalFloat("AHF_FarDistanceHeight", this.blendMaterial.GetFloat("_FarDistanceHeight"));
			Shader.SetGlobalFloat("AHF_FarDistanceOffset", this.blendMaterial.GetFloat("_FarDistanceOffset"));
			Shader.SetGlobalFloat("AHF_SkyboxFogIntensity", this.blendMaterial.GetFloat("_SkyboxFogIntensity"));
			Shader.SetGlobalFloat("AHF_SkyboxFogHeight", this.blendMaterial.GetFloat("_SkyboxFogHeight"));
			Shader.SetGlobalFloat("AHF_SkyboxFogFalloff", this.blendMaterial.GetFloat("_SkyboxFogFalloff"));
			Shader.SetGlobalFloat("AHF_SkyboxFogOffset", this.blendMaterial.GetFloat("_SkyboxFogOffset"));
			Shader.SetGlobalFloat("AHF_SkyboxFogBottom", this.blendMaterial.GetFloat("_SkyboxFogBottom"));
			Shader.SetGlobalFloat("AHF_SkyboxFogFill", this.blendMaterial.GetFloat("_SkyboxFogFill"));
			Shader.SetGlobalVector("AHF_DirectionalDir", this.blendMaterial.GetVector("_DirectionalDir"));
			Shader.SetGlobalFloat("AHF_DirectionalIntensity", this.blendMaterial.GetFloat("_DirectionalIntensity"));
			Shader.SetGlobalFloat("AHF_DirectionalFalloff", this.blendMaterial.GetFloat("_DirectionalFalloff"));
			Shader.SetGlobalColor("AHF_DirectionalColor", this.blendMaterial.GetColor("_DirectionalColor"));
			Shader.SetGlobalFloat("AHF_NoiseIntensity", this.blendMaterial.GetFloat("_NoiseIntensity"));
			Shader.SetGlobalFloat("AHF_NoiseMin", this.blendMaterial.GetFloat("_NoiseMin"));
			Shader.SetGlobalFloat("AHF_NoiseMax", this.blendMaterial.GetFloat("_NoiseMax"));
			Shader.SetGlobalFloat("AHF_NoiseScale", this.blendMaterial.GetFloat("_NoiseScale"));
			Shader.SetGlobalVector("AHF_NoiseSpeed", this.blendMaterial.GetVector("_NoiseSpeed"));
			Shader.SetGlobalFloat("AHF_NoiseDistanceEnd", this.blendMaterial.GetFloat("_NoiseDistanceEnd"));
			Shader.SetGlobalFloat("AHF_JitterIntensity", this.blendMaterial.GetFloat("_JitterIntensity"));
			int @int = this.blendMaterial.GetInt("_FogCameraMode");
			if (@int == 0)
			{
				Shader.EnableKeyword("AHF_CAMERAMODE_PERSPECTIVE");
				Shader.DisableKeyword("AHF_CAMERAMODE_ORTHOGRAPHIC");
				Shader.DisableKeyword("AHF_CAMERAMODE_BOTH");
				return;
			}
			if (@int == 1)
			{
				Shader.DisableKeyword("AHF_CAMERAMODE_PERSPECTIVE");
				Shader.EnableKeyword("AHF_CAMERAMODE_ORTHOGRAPHIC");
				Shader.DisableKeyword("AHF_CAMERAMODE_BOTH");
				return;
			}
			if (@int == 2)
			{
				Shader.DisableKeyword("AHF_CAMERAMODE_ORTHOGRAPHIC");
				Shader.DisableKeyword("AHF_CAMERAMODE_PERSPECTIVE");
				Shader.EnableKeyword("AHF_CAMERAMODE_BOTH");
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002C94 File Offset: 0x00000E94
		private void SetFogSphereSize()
		{
			float num = this.mainCamera.farClipPlane - 1f;
			base.gameObject.transform.localScale = new Vector3(num, num, num);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002CCB File Offset: 0x00000ECB
		private void SetFogSpherePosition()
		{
			base.transform.position = this.mainCamera.transform.position;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002CE8 File Offset: 0x00000EE8
		private void SetRenderQueue()
		{
			this.globalMaterial.renderQueue = 3000 + this.renderPriority;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002D04 File Offset: 0x00000F04
		public HeightFogGlobal()
		{
		}

		// Token: 0x04000013 RID: 19
		[StyledBanner(0.55f, 0.75f, 1f, "Height Fog Global", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.kfvqsi6kusw4")]
		public bool styledBanner;

		// Token: 0x04000014 RID: 20
		[StyledCategory("Render Settings", 5f, 10f)]
		public bool categoryRender;

		// Token: 0x04000015 RID: 21
		public FogRendering renderMode = FogRendering.RenderAsGlobalOverlay;

		// Token: 0x04000016 RID: 22
		[StyledCategory("Scene Settings")]
		public bool categoryScene;

		// Token: 0x04000017 RID: 23
		public Camera mainCamera;

		// Token: 0x04000018 RID: 24
		public Light mainDirectional;

		// Token: 0x04000019 RID: 25
		[StyledCategory("Preset Settings")]
		public bool categoryMode;

		// Token: 0x0400001A RID: 26
		public FogMode fogMode = FogMode.UseScriptSettings;

		// Token: 0x0400001B RID: 27
		[StyledMessage("Info", "The Preset feature requires a material using the BOXOPHOBIC > Atmospherics > Fog Preset shader.", 10f, 0f)]
		public bool messagePreset;

		// Token: 0x0400001C RID: 28
		[StyledMessage("Info", "The Time Of Day feature works by interpolating two Fog Preset materials using the BOXOPHOBIC > Atmospherics > Fog Preset shader. Please note that not all material properties can be interpolated properly!", 10f, 0f)]
		public bool messageTimeOfDay;

		// Token: 0x0400001D RID: 29
		[Space(10f)]
		public Material presetMaterial;

		// Token: 0x0400001E RID: 30
		[Range(0f, 1f)]
		public float IntensityMultiplier = 1f;

		// Token: 0x0400001F RID: 31
		[HideInInspector]
		public Material wantMaterial;

		// Token: 0x04000020 RID: 32
		[Space(10f)]
		public Material presetDay;

		// Token: 0x04000021 RID: 33
		public Material presetNight;

		// Token: 0x04000022 RID: 34
		[Space(10f)]
		[Range(0f, 1f)]
		public float timeOfDay;

		// Token: 0x04000023 RID: 35
		[StyledCategory("Fog Settings")]
		public bool categoryFog;

		// Token: 0x04000024 RID: 36
		[Range(0f, 1f)]
		public float fogIntensity = 1f;

		// Token: 0x04000025 RID: 37
		[Space(10f)]
		public FogAxisMode fogAxisMode = FogAxisMode.YAxis;

		// Token: 0x04000026 RID: 38
		public FogLayersMode fogLayersMode = FogLayersMode.MultiplyDistanceAndHeight;

		// Token: 0x04000027 RID: 39
		public FogCameraMode fogCameraMode;

		// Token: 0x04000028 RID: 40
		[Space(10f)]
		[FormerlySerializedAs("fogColor")]
		[ColorUsage(false, true)]
		public Color fogColorStart = new Color(0.5f, 0.75f, 1f, 1f);

		// Token: 0x04000029 RID: 41
		[ColorUsage(false, true)]
		public Color fogColorEnd = new Color(0.75f, 1f, 1.25f, 1f);

		// Token: 0x0400002A RID: 42
		[Range(0f, 1f)]
		public float fogColorDuo;

		// Token: 0x0400002B RID: 43
		[Space(10f)]
		public float fogDistanceStart;

		// Token: 0x0400002C RID: 44
		public float fogDistanceEnd = 100f;

		// Token: 0x0400002D RID: 45
		[Range(1f, 8f)]
		public float fogDistanceFalloff = 1f;

		// Token: 0x0400002E RID: 46
		[Space(10f)]
		public float fogHeightStart;

		// Token: 0x0400002F RID: 47
		public float fogHeightEnd = 100f;

		// Token: 0x04000030 RID: 48
		[Range(1f, 8f)]
		public float fogHeightFalloff = 1f;

		// Token: 0x04000031 RID: 49
		[Space(10f)]
		public float farDistanceHeight;

		// Token: 0x04000032 RID: 50
		public float farDistanceOffset;

		// Token: 0x04000033 RID: 51
		[StyledCategory("Skybox Settings")]
		public bool categorySkybox;

		// Token: 0x04000034 RID: 52
		[Range(0f, 1f)]
		public float skyboxFogIntensity = 1f;

		// Token: 0x04000035 RID: 53
		[Range(0f, 8f)]
		public float skyboxFogHeight = 1f;

		// Token: 0x04000036 RID: 54
		[Range(1f, 8f)]
		public float skyboxFogFalloff = 1f;

		// Token: 0x04000037 RID: 55
		[Range(-1f, 1f)]
		public float skyboxFogOffset;

		// Token: 0x04000038 RID: 56
		[Range(0f, 1f)]
		public float skyboxFogBottom;

		// Token: 0x04000039 RID: 57
		[Range(0f, 1f)]
		public float skyboxFogFill;

		// Token: 0x0400003A RID: 58
		[StyledCategory("Directional Settings")]
		public bool categoryDirectional;

		// Token: 0x0400003B RID: 59
		[Range(0f, 1f)]
		public float directionalIntensity = 1f;

		// Token: 0x0400003C RID: 60
		[Range(1f, 8f)]
		public float directionalFalloff = 1f;

		// Token: 0x0400003D RID: 61
		[ColorUsage(false, true)]
		public Color directionalColor = new Color(1f, 0.75f, 0.5f, 1f);

		// Token: 0x0400003E RID: 62
		[StyledCategory("Noise Settings")]
		public bool categoryNoise;

		// Token: 0x0400003F RID: 63
		[Range(0f, 1f)]
		public float noiseIntensity = 1f;

		// Token: 0x04000040 RID: 64
		[Range(0f, 1f)]
		public float noiseMin;

		// Token: 0x04000041 RID: 65
		[Range(0f, 1f)]
		public float noiseMax = 1f;

		// Token: 0x04000042 RID: 66
		public float noiseScale = 30f;

		// Token: 0x04000043 RID: 67
		public Vector3 noiseSpeed = new Vector3(0.5f, 0f, 0.5f);

		// Token: 0x04000044 RID: 68
		[Space(10f)]
		public float noiseDistanceEnd = 200f;

		// Token: 0x04000045 RID: 69
		[StyledCategory("Advanced Settings")]
		public bool categoryAdvanced;

		// Token: 0x04000046 RID: 70
		public float jitterIntensity;

		// Token: 0x04000047 RID: 71
		public int renderPriority = 1;

		// Token: 0x04000048 RID: 72
		[Space(10f)]
		public bool manualPositionAndScale;

		// Token: 0x04000049 RID: 73
		[StyledSpace(5)]
		public bool styledSpace0;

		// Token: 0x0400004A RID: 74
		private Material localMaterial;

		// Token: 0x0400004B RID: 75
		private Material blendMaterial;

		// Token: 0x0400004C RID: 76
		private Material globalMaterial;

		// Token: 0x0400004D RID: 77
		private Material missingMaterial;

		// Token: 0x0400004E RID: 78
		private Material currentMaterial;

		// Token: 0x0400004F RID: 79
		private MeshRenderer meshRenderer;

		// Token: 0x04000050 RID: 80
		[HideInInspector]
		public Material overrideMaterial;

		// Token: 0x04000051 RID: 81
		[HideInInspector]
		public float overrideCamToVolumeDistance = 1f;

		// Token: 0x04000052 RID: 82
		[HideInInspector]
		public float overrideVolumeDistanceFade;

		// Token: 0x04000053 RID: 83
		[HideInInspector]
		public int version;

		// Token: 0x04000054 RID: 84
		private bool hasSetupMaterial;
	}
}
