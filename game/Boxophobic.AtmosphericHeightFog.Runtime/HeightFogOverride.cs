using System;
using Boxophobic.StyledGUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace AtmosphericHeightFog
{
	// Token: 0x02000008 RID: 8
	[ExecuteInEditMode]
	[HelpURL("https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq")]
	public class HeightFogOverride : StyledMonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002E64 File Offset: 0x00001064
		private void Start()
		{
			this.volumeCollider = base.GetComponent<Collider>();
			if (this.volumeCollider == null)
			{
				Debug.Log("[Atmospheric Height Fog] Please create override volumes from the GameObject menu > BOXOPHOBIC > Atmospheric Height Fog > Override!");
				UnityEngine.Object.DestroyImmediate(this);
			}
			if (GameObject.Find("Height Fog Global") != null)
			{
				GameObject gameObject = GameObject.Find("Height Fog Global");
				this.globalFog = gameObject.GetComponent<HeightFogGlobal>();
				this.messageNoHeightFogGlobal = false;
			}
			else
			{
				this.messageNoHeightFogGlobal = true;
			}
			this.GetDirectional();
			this.localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
			this.localMaterial.name = "Local";
			this.missingMaterial = Resources.Load<Material>("Height Fog Preset");
			this.SetLocalMaterial();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002F15 File Offset: 0x00001115
		private void OnDisable()
		{
			if (this.globalFog != null)
			{
				this.globalFog.overrideCamToVolumeDistance = 1f;
				this.globalFog.overrideVolumeDistanceFade = 0f;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002F45 File Offset: 0x00001145
		private void OnDestroy()
		{
			if (this.globalFog != null)
			{
				this.globalFog.overrideCamToVolumeDistance = 1f;
				this.globalFog.overrideVolumeDistanceFade = 0f;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002F78 File Offset: 0x00001178
		private void Update()
		{
			this.GetCamera();
			if (this.mainCamera == null || this.globalFog == null)
			{
				return;
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
					this.currentMaterial = this.presetMaterial;
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
			Vector3 position = this.mainCamera.transform.position;
			Vector3 b = this.volumeCollider.ClosestPoint(position);
			float num = Vector3.Distance(position, b);
			if (num > this.volumeDistanceFade && !this.distanceSent)
			{
				this.globalFog.overrideCamToVolumeDistance = float.PositiveInfinity;
				this.distanceSent = true;
				return;
			}
			if (num < this.volumeDistanceFade)
			{
				this.globalFog.overrideMaterial = this.currentMaterial;
				this.globalFog.overrideCamToVolumeDistance = num;
				this.globalFog.overrideVolumeDistanceFade = this.volumeDistanceFade;
				this.distanceSent = false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000031A0 File Offset: 0x000013A0
		private void OnDrawGizmos()
		{
			if (this.volumeCollider == null)
			{
				return;
			}
			Color color = this.volumeGizmoColor;
			float num = 1f;
			if (this.volumeCollider.GetType() == typeof(BoxCollider))
			{
				BoxCollider component = base.GetComponent<BoxCollider>();
				Gizmos.color = new Color(color.r * num, color.g * num, color.b * num, color.a);
				Gizmos.DrawWireCube(base.transform.position, new Vector3(base.transform.lossyScale.x * component.size.x, base.transform.lossyScale.y * component.size.y, base.transform.lossyScale.z * component.size.z));
				Gizmos.color = new Color(color.r * num, color.g * num, color.b * num, color.a * 0.5f);
				Gizmos.DrawWireCube(base.transform.position, new Vector3(base.transform.lossyScale.x * component.size.x + this.volumeDistanceFade * 2f, base.transform.lossyScale.y * component.size.y + this.volumeDistanceFade * 2f, base.transform.lossyScale.z * component.size.z + this.volumeDistanceFade * 2f));
				return;
			}
			SphereCollider component2 = base.GetComponent<SphereCollider>();
			float num2 = Mathf.Max(Mathf.Max(base.gameObject.transform.localScale.x, base.gameObject.transform.localScale.y), base.gameObject.transform.localScale.z);
			Gizmos.color = new Color(color.r * num, color.g * num, color.b * num, color.a);
			Gizmos.DrawWireSphere(base.transform.position, component2.radius * num2);
			Gizmos.color = new Color(color.r * num, color.g * num, color.b * num, color.a * 0.5f);
			Gizmos.DrawWireSphere(base.transform.position, component2.radius * num2 + this.volumeDistanceFade);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003420 File Offset: 0x00001620
		private void GetCamera()
		{
			if (this.mainCamera == null)
			{
				this.mainCamera = Camera.main;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000343C File Offset: 0x0000163C
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

		// Token: 0x06000014 RID: 20 RVA: 0x00003490 File Offset: 0x00001690
		private void SetLocalMaterial()
		{
			this.localMaterial.SetFloat("_FogIntensity", this.fogIntensity);
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
			this.localMaterial.SetFloat("_SkyboxFogBottom", this.skyboxFogFill);
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

		// Token: 0x06000015 RID: 21 RVA: 0x00003834 File Offset: 0x00001A34
		public HeightFogOverride()
		{
		}

		// Token: 0x04000055 RID: 85
		[StyledBanner(0.55f, 0.75f, 1f, "Height Fog Override", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq")]
		public bool styledBanner;

		// Token: 0x04000056 RID: 86
		[StyledMessage("Info", "The Height Fog Global object is missing from your scene! Please add it before using the Height Fog Override component!", 5f, 0f)]
		public bool messageNoHeightFogGlobal;

		// Token: 0x04000057 RID: 87
		[StyledCategory("Volume Settings", 5f, 10f)]
		public bool categoryVolume;

		// Token: 0x04000058 RID: 88
		public float volumeDistanceFade = 3f;

		// Token: 0x04000059 RID: 89
		public Color volumeGizmoColor = Color.white;

		// Token: 0x0400005A RID: 90
		[StyledCategory("Scene Settings")]
		public bool categoryScene;

		// Token: 0x0400005B RID: 91
		public Camera mainCamera;

		// Token: 0x0400005C RID: 92
		public Light mainDirectional;

		// Token: 0x0400005D RID: 93
		[StyledCategory("Preset Settings")]
		public bool categoryMode;

		// Token: 0x0400005E RID: 94
		public FogMode fogMode = FogMode.UseScriptSettings;

		// Token: 0x0400005F RID: 95
		[StyledMessage("Info", "The Preset feature requires a material using the BOXOPHOBIC > Atmospherics > Fog Preset shader.", 10f, 0f)]
		public bool messagePreset;

		// Token: 0x04000060 RID: 96
		[StyledMessage("Info", "The Time Of Day feature works by interpolating two Fog Preset materials using the BOXOPHOBIC > Atmospherics > Fog Preset shader. Please note that not all material properties can be interpolated properly!", 10f, 0f)]
		public bool messageTimeOfDay;

		// Token: 0x04000061 RID: 97
		[Space(10f)]
		public Material presetMaterial;

		// Token: 0x04000062 RID: 98
		[Space(10f)]
		public Material presetDay;

		// Token: 0x04000063 RID: 99
		public Material presetNight;

		// Token: 0x04000064 RID: 100
		[Space(10f)]
		[Range(0f, 1f)]
		public float timeOfDay;

		// Token: 0x04000065 RID: 101
		[StyledCategory("Fog Settings")]
		public bool categoryFog;

		// Token: 0x04000066 RID: 102
		[Range(0f, 1f)]
		public float fogIntensity = 1f;

		// Token: 0x04000067 RID: 103
		[Space(10f)]
		public FogAxisMode fogAxisMode = FogAxisMode.YAxis;

		// Token: 0x04000068 RID: 104
		public FogLayersMode fogLayersMode = FogLayersMode.MultiplyDistanceAndHeight;

		// Token: 0x04000069 RID: 105
		public FogCameraMode fogCameraMode;

		// Token: 0x0400006A RID: 106
		[Space(10f)]
		[FormerlySerializedAs("fogColor")]
		[ColorUsage(false, true)]
		public Color fogColorStart = new Color(0.5f, 0.75f, 0f, 1f);

		// Token: 0x0400006B RID: 107
		[ColorUsage(false, true)]
		public Color fogColorEnd = new Color(0.75f, 1f, 0f, 1f);

		// Token: 0x0400006C RID: 108
		[Range(0f, 1f)]
		public float fogColorDuo;

		// Token: 0x0400006D RID: 109
		[Space(10f)]
		public float fogDistanceStart = -100f;

		// Token: 0x0400006E RID: 110
		public float fogDistanceEnd = 100f;

		// Token: 0x0400006F RID: 111
		[Range(1f, 8f)]
		public float fogDistanceFalloff = 1f;

		// Token: 0x04000070 RID: 112
		[Space(10f)]
		public float fogHeightStart;

		// Token: 0x04000071 RID: 113
		public float fogHeightEnd = 100f;

		// Token: 0x04000072 RID: 114
		[Range(1f, 8f)]
		public float fogHeightFalloff = 1f;

		// Token: 0x04000073 RID: 115
		[Space(10f)]
		public float farDistanceHeight;

		// Token: 0x04000074 RID: 116
		public float farDistanceOffset;

		// Token: 0x04000075 RID: 117
		[StyledCategory("Skybox Settings")]
		public bool categorySkybox;

		// Token: 0x04000076 RID: 118
		[Range(0f, 1f)]
		public float skyboxFogIntensity = 1f;

		// Token: 0x04000077 RID: 119
		[Range(0f, 1f)]
		public float skyboxFogHeight = 1f;

		// Token: 0x04000078 RID: 120
		[Range(1f, 8f)]
		public float skyboxFogFalloff = 1f;

		// Token: 0x04000079 RID: 121
		[Range(-1f, 1f)]
		public float skyboxFogOffset;

		// Token: 0x0400007A RID: 122
		[Range(0f, 1f)]
		public float skyboxFogBottom;

		// Token: 0x0400007B RID: 123
		[Range(0f, 1f)]
		public float skyboxFogFill;

		// Token: 0x0400007C RID: 124
		[StyledCategory("Directional Settings")]
		public bool categoryDirectional;

		// Token: 0x0400007D RID: 125
		[Range(0f, 1f)]
		public float directionalIntensity = 1f;

		// Token: 0x0400007E RID: 126
		[Range(1f, 8f)]
		public float directionalFalloff = 1f;

		// Token: 0x0400007F RID: 127
		[ColorUsage(false, true)]
		public Color directionalColor = new Color(1f, 0.75f, 0.5f, 1f);

		// Token: 0x04000080 RID: 128
		[StyledCategory("Noise Settings")]
		public bool categoryNoise;

		// Token: 0x04000081 RID: 129
		[Range(0f, 1f)]
		public float noiseIntensity = 1f;

		// Token: 0x04000082 RID: 130
		[Range(0f, 1f)]
		public float noiseMin;

		// Token: 0x04000083 RID: 131
		[Range(0f, 1f)]
		public float noiseMax = 1f;

		// Token: 0x04000084 RID: 132
		public float noiseScale = 30f;

		// Token: 0x04000085 RID: 133
		public Vector3 noiseSpeed = new Vector3(0.5f, 0f, 0.5f);

		// Token: 0x04000086 RID: 134
		[Space(10f)]
		public float noiseDistanceEnd = 200f;

		// Token: 0x04000087 RID: 135
		[StyledCategory("Advanced Settings")]
		public bool categoryAdvanced;

		// Token: 0x04000088 RID: 136
		public float jitterIntensity;

		// Token: 0x04000089 RID: 137
		[StyledSpace(5)]
		public bool styledSpace0;

		// Token: 0x0400008A RID: 138
		private Material localMaterial;

		// Token: 0x0400008B RID: 139
		private Material missingMaterial;

		// Token: 0x0400008C RID: 140
		private Material currentMaterial;

		// Token: 0x0400008D RID: 141
		private Collider volumeCollider;

		// Token: 0x0400008E RID: 142
		private HeightFogGlobal globalFog;

		// Token: 0x0400008F RID: 143
		private bool distanceSent;

		// Token: 0x04000090 RID: 144
		[HideInInspector]
		public int version;
	}
}
