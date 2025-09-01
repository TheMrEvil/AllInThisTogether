using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace VolumetricLights
{
	// Token: 0x02000019 RID: 25
	[ExecuteAlways]
	[RequireComponent(typeof(Light))]
	[AddComponentMenu("Effects/Volumetric Light", 1000)]
	[HelpURL("https://kronnect.com/guides/volumetric-lights-2-builtin-volumetric-light-parameters/")]
	[DefaultExecutionOrder(100)]
	public class VolumetricLight : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00003738 File Offset: 0x00001938
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00003770 File Offset: 0x00001970
		public event PropertiesChangedEvent OnPropertiesChanged
		{
			[CompilerGenerated]
			add
			{
				PropertiesChangedEvent propertiesChangedEvent = this.OnPropertiesChanged;
				PropertiesChangedEvent propertiesChangedEvent2;
				do
				{
					propertiesChangedEvent2 = propertiesChangedEvent;
					PropertiesChangedEvent value2 = (PropertiesChangedEvent)Delegate.Combine(propertiesChangedEvent2, value);
					propertiesChangedEvent = Interlocked.CompareExchange<PropertiesChangedEvent>(ref this.OnPropertiesChanged, value2, propertiesChangedEvent2);
				}
				while (propertiesChangedEvent != propertiesChangedEvent2);
			}
			[CompilerGenerated]
			remove
			{
				PropertiesChangedEvent propertiesChangedEvent = this.OnPropertiesChanged;
				PropertiesChangedEvent propertiesChangedEvent2;
				do
				{
					propertiesChangedEvent2 = propertiesChangedEvent;
					PropertiesChangedEvent value2 = (PropertiesChangedEvent)Delegate.Remove(propertiesChangedEvent2, value);
					propertiesChangedEvent = Interlocked.CompareExchange<PropertiesChangedEvent>(ref this.OnPropertiesChanged, value2, propertiesChangedEvent2);
				}
				while (propertiesChangedEvent != propertiesChangedEvent2);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000037A5 File Offset: 0x000019A5
		public Material material
		{
			get
			{
				return this.fogMat;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000037AD File Offset: 0x000019AD
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000037B5 File Offset: 0x000019B5
		[Obsolete("Settings property is now deprecated. Settings are now part of the Volumetric Light component itself, for example: VolumetricLight.density instead of VolumetricLight.settings.density.")]
		public VolumetricLightProfile settings
		{
			get
			{
				return this.profile;
			}
			set
			{
				Debug.Log("Changing values through settings is deprecated. If you want to get or set the profile for this light, use the profile property. Or simply set the properties now directly to the volumetric light component. For example: VolumetricLight.density = xxx.");
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037C1 File Offset: 0x000019C1
		private void OnEnable()
		{
			this.Init();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037CC File Offset: 0x000019CC
		public void Init()
		{
			if (!VolumetricLight.volumetricLights.Contains(this))
			{
				VolumetricLight.volumetricLights.Add(this);
			}
			this.lightComp = base.GetComponent<Light>();
			if (base.gameObject.layer == 0)
			{
				base.gameObject.layer = 1;
			}
			this.SettingsInit();
			this.Refresh();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003822 File Offset: 0x00001A22
		public void Refresh()
		{
			if (!base.enabled)
			{
				return;
			}
			this.CheckProfile();
			this.CheckMesh();
			this.CheckShadowCam();
			this.UpdateMaterialPropertiesNow();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003846 File Offset: 0x00001A46
		private void OnValidate()
		{
			this.requireUpdateMaterial = true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000384F File Offset: 0x00001A4F
		public void OnDidApplyAnimationProperties()
		{
			this.requireUpdateMaterial = true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003858 File Offset: 0x00001A58
		private void OnDisable()
		{
			if (VolumetricLight.volumetricLights.Contains(this))
			{
				VolumetricLight.volumetricLights.Remove(this);
			}
			this.TurnOff();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003879 File Offset: 0x00001A79
		private void TurnOff()
		{
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			this.ShadowsDisable();
			this.ParticlesDisable();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038A1 File Offset: 0x00001AA1
		public void ToggleVolumetrics(bool visible)
		{
			this.isInvisible = !visible;
			this.SetFogMaterial();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038B4 File Offset: 0x00001AB4
		private void OnDestroy()
		{
			if (this.fogMatInvisible != null)
			{
				UnityEngine.Object.DestroyImmediate(this.fogMatInvisible);
				this.fogMatInvisible = null;
			}
			if (this.fogMatLight != null)
			{
				UnityEngine.Object.DestroyImmediate(this.fogMatLight);
				this.fogMatLight = null;
			}
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			this.ShadowsDispose();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003924 File Offset: 0x00001B24
		private void OnWillRenderObject()
		{
			Camera current = Camera.current;
			if (current != null)
			{
				current.depthTextureMode |= DepthTextureMode.Depth;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003950 File Offset: 0x00001B50
		private void LateUpdate()
		{
			if (!this.lightComp.isActiveAndEnabled && !this.alwaysOn)
			{
				if (this.meshRenderer != null && this.meshRenderer.enabled)
				{
					this.TurnOff();
				}
				return;
			}
			if (this.meshRenderer != null && !this.meshRenderer.enabled)
			{
				this.requireUpdateMaterial = true;
			}
			if (this.CheckMesh())
			{
				if (!Application.isPlaying)
				{
					this.ParticlesDisable();
				}
				this.ScheduleShadowCapture();
				this.requireUpdateMaterial = true;
			}
			if (this.requireUpdateMaterial)
			{
				this.requireUpdateMaterial = false;
				this.UpdateMaterialPropertiesNow();
			}
			if (this.fogMat == null || this.meshRenderer == null)
			{
				return;
			}
			this.UpdateVolumeGeometry();
			float time = Time.time;
			if ((this.dustAutoToggle || this.shadowAutoToggle || this.autoToggle) && (!Application.isPlaying || time - this.lastDistanceCheckTime >= this.autoToggleCheckInterval))
			{
				this.lastDistanceCheckTime = time;
				this.ComputeDistanceToCamera();
			}
			float num = this.brightness;
			if (this.autoToggle)
			{
				float num2 = this.distanceDeactivation * this.distanceDeactivation;
				float num3 = this.distanceStartDimming * this.distanceStartDimming;
				if (num3 > num2)
				{
					num3 = num2;
				}
				float num4 = 1f - Mathf.Clamp01((this.distanceToCameraSqr - num3) / (num2 - num3));
				num *= num4;
				bool flag = num4 > 0f;
				if (flag != this.wasInRange)
				{
					this.wasInRange = flag;
					this.meshRenderer.enabled = flag;
				}
			}
			this.UpdateDiffusionTerm();
			if (this.enableDustParticles)
			{
				if (!Application.isPlaying)
				{
					this.ParticlesResetIfTransformChanged();
				}
				this.UpdateParticlesVisibility();
			}
			this.fogMat.SetColor(VolumetricLight.ShaderParams.LightColor, this.lightComp.color * this.mediumAlbedo * (this.lightComp.intensity * num));
			float deltaTime = Time.deltaTime;
			this.windDirectionAcum.x = this.windDirectionAcum.x + this.windDirection.x * deltaTime;
			this.windDirectionAcum.y = this.windDirectionAcum.y + this.windDirection.y * deltaTime;
			this.windDirectionAcum.z = this.windDirectionAcum.z + this.windDirection.z * deltaTime;
			this.windDirectionAcum.w = (this.animatedBlueNoise ? (0.618034f * (float)(Time.frameCount % 480)) : 0f);
			this.fogMat.SetVector(VolumetricLight.ShaderParams.WindDirection, this.windDirectionAcum);
			this.ShadowsUpdate();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void ComputeDistanceToCamera()
		{
			if (this.mainCamera == null)
			{
				this.mainCamera = this.targetCamera;
				if (this.mainCamera == null && Camera.main != null)
				{
					this.mainCamera = Camera.main.transform;
				}
				if (this.mainCamera == null)
				{
					return;
				}
			}
			Vector3 position = this.mainCamera.position;
			Vector3 center = this.bounds.center;
			this.distanceToCameraSqr = (position - center).sqrMagnitude;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C60 File Offset: 0x00001E60
		private void UpdateDiffusionTerm()
		{
			Vector4 value = -base.transform.forward;
			value.w = this.diffusionIntensity;
			this.fogMat.SetVector(VolumetricLight.ShaderParams.ToLightDir, value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void UpdateVolumeGeometry()
		{
			this.NormalizeScale();
			this.UpdateVolumeGeometryMaterial(this.fogMat);
			if (this.enableDustParticles && this.particleMaterial != null)
			{
				this.UpdateVolumeGeometryMaterial(this.particleMaterial);
				this.particleMaterial.SetMatrix(VolumetricLight.ShaderParams.WorldToLocalMatrix, base.transform.worldToLocalMatrix);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D00 File Offset: 0x00001F00
		private void UpdateVolumeGeometryMaterial(Material mat)
		{
			if (mat == null)
			{
				return;
			}
			Vector4 value = base.transform.position;
			value.w = this.tipRadius;
			mat.SetVector(VolumetricLight.ShaderParams.ConeTipData, value);
			Vector4 value2 = base.transform.forward * this.generatedRange;
			float num = this.generatedRange * this.generatedRange;
			value2.w = num;
			mat.SetVector(VolumetricLight.ShaderParams.ConeAxis, value2);
			float num2 = Mathf.Max(0.0001f, this.rangeFallOff);
			float y = -1f / (num * num2);
			float z = num / (num * num2);
			mat.SetVector(VolumetricLight.ShaderParams.ExtraGeoData, new Vector4(this.generatedBaseRadius, y, z, this.generatedRange));
			if (!this.useCustomBounds)
			{
				this.bounds = this.meshRenderer.bounds;
			}
			Bounds bounds = this.bounds;
			if (this.useCustomBounds && this.boundsInLocalSpace)
			{
				bounds.center += base.transform.position;
			}
			mat.SetVector(VolumetricLight.ShaderParams.BoundsCenter, bounds.center);
			mat.SetVector(VolumetricLight.ShaderParams.BoundsExtents, bounds.extents);
			if (this.generatedType == LightType.Area || this.generatedType == LightType.Disc)
			{
				if (this.mf != null && this.mf.sharedMesh != null)
				{
					Bounds bounds2 = this.mf.sharedMesh.bounds;
					mat.SetVector(VolumetricLight.ShaderParams.MeshBoundsCenter, bounds2.center);
					mat.SetVector(VolumetricLight.ShaderParams.MeshBoundsExtents, bounds2.extents);
				}
				float w = (this.generatedAreaFrustumMultiplier - 1f) / this.generatedRange;
				if (this.generatedType == LightType.Area)
				{
					mat.SetVector(VolumetricLight.ShaderParams.AreaExtents, new Vector4(this.areaWidth * 0.5f, this.areaHeight * 0.5f, this.generatedRange, w));
					return;
				}
				mat.SetVector(VolumetricLight.ShaderParams.AreaExtents, new Vector4(this.areaWidth * this.areaWidth, this.areaHeight, this.generatedRange, w));
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F2E File Offset: 0x0000212E
		public void UpdateMaterialProperties()
		{
			this.requireUpdateMaterial = true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F38 File Offset: 0x00002138
		private void UpdateMaterialPropertiesNow()
		{
			this.wasInRange = false;
			this.lastDistanceCheckTime = -999f;
			this.mainCamera = null;
			this.ComputeDistanceToCamera();
			if (this == null || !base.isActiveAndEnabled || this.lightComp == null || (!this.lightComp.isActiveAndEnabled && !this.alwaysOn))
			{
				this.ShadowsDisable();
				return;
			}
			this.SettingsValidate();
			if (this.meshRenderer == null)
			{
				this.meshRenderer = base.GetComponent<MeshRenderer>();
			}
			if (this.fogMatLight == null)
			{
				if (this.meshRenderer != null)
				{
					this.fogMatLight = this.meshRenderer.sharedMaterial;
				}
				if (this.fogMatLight == null)
				{
					this.fogMatLight = new Material(Shader.Find("VolumetricLights/VolumetricLight"));
					this.fogMatLight.hideFlags = HideFlags.DontSave;
				}
			}
			this.fogMat = this.fogMatLight;
			if (this.fogMat == null)
			{
				return;
			}
			this.SetFogMaterial();
			if (this.customRange < 0.001f)
			{
				this.customRange = 0.001f;
			}
			if (this.meshRenderer != null)
			{
				this.meshRenderer.sortingLayerID = this.sortingLayerID;
				this.meshRenderer.sortingOrder = this.sortingOrder;
			}
			this.fogMat.renderQueue = this.renderQueue;
			switch (this.blendMode)
			{
			case BlendMode.Additive:
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendOp, 0);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendSrc, 1);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendDest, 1);
				break;
			case BlendMode.Blend:
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendOp, 0);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendSrc, 1);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendDest, 10);
				break;
			case BlendMode.PreMultiply:
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendOp, 0);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendSrc, 5);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendDest, 1);
				break;
			case BlendMode.Substractive:
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendOp, 2);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendSrc, 1);
				this.fogMat.SetInt(VolumetricLight.ShaderParams.BlendDest, 1);
				break;
			}
			this.fogMat.SetTexture(VolumetricLight.ShaderParams.NoiseTex, this.noiseTexture);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.NoiseStrength, this.noiseStrength);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.NoiseScale, 0.1f / this.noiseScale);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.NoiseFinalMultiplier, this.noiseFinalMultiplier);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.Penumbra, this.penumbra);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.RangeFallOff, this.rangeFallOff);
			this.fogMat.SetFloat(VolumetricLight.ShaderParams.Density, this.density);
			this.fogMat.SetVector(VolumetricLight.ShaderParams.DirectLightData, new Vector4(this.directLightMultiplier, (float)this.directLightSmoothSamples, this.directLightSmoothRadius, 0f));
			this.fogMat.SetVector(VolumetricLight.ShaderParams.FallOff, new Vector4(this.attenCoefConstant, this.attenCoefLinear, this.attenCoefQuadratic, 0f));
			this.fogMat.SetVector(VolumetricLight.ShaderParams.RayMarchSettings, new Vector4((float)this.raymarchQuality, this.dithering * 0.001f, this.jittering, this.raymarchMinStep));
			this.fogMat.SetInt(VolumetricLight.ShaderParams.RayMarchMaxSteps, this.raymarchMaxSteps);
			if (this.jittering > 0f)
			{
				if (VolumetricLight.blueNoiseTex == null)
				{
					VolumetricLight.blueNoiseTex = Resources.Load<Texture2D>("Textures/blueNoiseVL128");
				}
				this.fogMat.SetTexture(VolumetricLight.ShaderParams.BlueNoiseTexture, VolumetricLight.blueNoiseTex);
			}
			if (this.keywords == null)
			{
				this.keywords = new List<string>();
			}
			else
			{
				this.keywords.Clear();
			}
			if (this.useBlueNoise)
			{
				this.keywords.Add("VL_BLUENOISE");
			}
			if (this.useNoise)
			{
				this.keywords.Add("VL_NOISE");
			}
			switch (this.lightComp.type)
			{
			case LightType.Spot:
				if (this.cookieTexture != null)
				{
					this.keywords.Add("VL_SPOT_COOKIE");
					this.fogMat.SetTexture(VolumetricLight.ShaderParams.CookieTexture, this.cookieTexture);
					this.fogMat.SetVector(VolumetricLight.ShaderParams.CookieTexture_ScaleAndSpeed, new Vector4(this.cookieScale.x, this.cookieScale.y, this.cookieSpeed.x, this.cookieSpeed.y));
					this.fogMat.SetVector(VolumetricLight.ShaderParams.CookieTexture_Offset, new Vector4(this.cookieOffset.x, this.cookieOffset.y, 0f, 0f));
				}
				else
				{
					this.keywords.Add("VL_SPOT");
				}
				break;
			case LightType.Point:
				this.keywords.Add("VL_POINT");
				break;
			case LightType.Area:
				this.keywords.Add("VL_AREA_RECT");
				break;
			case LightType.Disc:
				this.keywords.Add("VL_AREA_DISC");
				break;
			}
			if (this.attenuationMode == AttenuationMode.Quadratic)
			{
				this.keywords.Add("VL_PHYSICAL_ATTEN");
			}
			if (this.castDirectLight)
			{
				this.keywords.Add((this.directLightBlendMode == DirectLightBlendMode.Additive) ? "VL_CAST_DIRECT_LIGHT_ADDITIVE" : "VL_CAST_DIRECT_LIGHT_BLEND");
			}
			if (this.useCustomBounds)
			{
				this.keywords.Add("VL_CUSTOM_BOUNDS");
			}
			this.ShadowsSupportCheck();
			if (this.enableShadows)
			{
				if (this.usesCubemap)
				{
					this.keywords.Add("VL_SHADOWS_CUBEMAP");
				}
				else if (this.usesTranslucency)
				{
					this.keywords.Add("VL_SHADOWS_TRANSLUCENCY");
				}
				else
				{
					this.keywords.Add("VL_SHADOWS");
				}
			}
			this.fogMat.enabledKeywords = null;
			this.fogMat.shaderKeywords = this.keywords.ToArray();
			this.ParticlesCheckSupport();
			if (this.OnPropertiesChanged != null)
			{
				this.OnPropertiesChanged(this);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004568 File Offset: 0x00002768
		private void SetFogMaterial()
		{
			if (this.meshRenderer != null)
			{
				if (this.isInvisible || this.density <= 0f || this.mediumAlbedo.a == 0f)
				{
					if (this.fogMatInvisible == null)
					{
						this.fogMatInvisible = new Material(Shader.Find("VolumetricLights/Invisible"));
						this.fogMatInvisible.hideFlags = HideFlags.DontSave;
					}
					this.meshRenderer.sharedMaterial = this.fogMatInvisible;
					return;
				}
				this.meshRenderer.sharedMaterial = this.fogMat;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004600 File Offset: 0x00002800
		public void CheckProfile()
		{
			if (this.profile != null)
			{
				if ("Auto".Equals(this.profile.name))
				{
					this.profile.ApplyTo(this);
					this.profile = null;
					return;
				}
				if (this.profileSync)
				{
					this.profile.ApplyTo(this);
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000465C File Offset: 0x0000285C
		public Bounds GetBounds()
		{
			Bounds result = this.bounds;
			if (this.useCustomBounds && this.boundsInLocalSpace)
			{
				result.center += base.transform.position;
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000469E File Offset: 0x0000289E
		public void SetBounds(Bounds bounds)
		{
			if (this.useCustomBounds && this.boundsInLocalSpace)
			{
				bounds.center -= base.transform.position;
			}
			this.bounds = bounds;
			this.UpdateVolumeGeometry();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000046DC File Offset: 0x000028DC
		private bool CheckMesh()
		{
			if (this.meshRenderer != null && !this.autoToggle)
			{
				bool enabled = this.lightComp.enabled || this.alwaysOn;
				this.meshRenderer.enabled = enabled;
			}
			if (!this.useCustomSize)
			{
				this.customRange = this.lightComp.range;
			}
			bool flag = false;
			this.mf = base.GetComponent<MeshFilter>();
			if (this.mf == null || this.mf.sharedMesh == null)
			{
				flag = true;
			}
			switch (this.lightComp.type)
			{
			case LightType.Spot:
				if (flag || this.generatedType != LightType.Spot || this.customRange != this.generatedRange || this.lightComp.spotAngle != this.generatedSpotAngle || this.tipRadius != this.generatedTipRadius)
				{
					this.GenerateConeMesh();
					return true;
				}
				return false;
			case LightType.Point:
				if (flag || this.generatedType != LightType.Point || this.customRange != this.generatedRange)
				{
					this.GenerateSphereMesh();
					return true;
				}
				return false;
			case LightType.Area:
				if (flag || this.generatedType != LightType.Area || this.customRange != this.generatedRange || this.areaWidth != this.generatedAreaWidth || this.areaHeight != this.generatedAreaHeight || this.frustumAngle != this.generatedAreaFrustumAngle)
				{
					this.GenerateCubeMesh();
					return true;
				}
				return false;
			case LightType.Disc:
				if (flag || this.generatedType != LightType.Disc || this.customRange != this.generatedRange || this.areaWidth != this.generatedAreaWidth || this.frustumAngle != this.generatedAreaFrustumAngle)
				{
					this.GenerateCylinderMesh();
					return true;
				}
				return false;
			}
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			return false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000048A8 File Offset: 0x00002AA8
		private void UpdateMesh(string name)
		{
			this.mf = base.GetComponent<MeshFilter>();
			if (this.mf == null)
			{
				this.mf = base.gameObject.AddComponent<MeshFilter>();
			}
			Mesh mesh = this.mf.sharedMesh;
			if (mesh == null)
			{
				mesh = new Mesh();
			}
			else
			{
				mesh.Clear();
			}
			mesh.name = name;
			mesh.SetVertices(this.vertices);
			mesh.SetIndices(this.indices, MeshTopology.Triangles, 0, true, 0);
			this.mf.mesh = mesh;
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			if (this.meshRenderer == null)
			{
				this.meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
				this.meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				this.meshRenderer.receiveShadows = false;
				return;
			}
			if (!this.autoToggle)
			{
				this.meshRenderer.enabled = true;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000498C File Offset: 0x00002B8C
		private void NormalizeScale()
		{
			Transform parent = base.transform.parent;
			if (parent == null)
			{
				return;
			}
			Vector3 lossyScale = parent.lossyScale;
			if ((lossyScale.x != 1f || lossyScale.y != 1f || lossyScale.z != 1f) && lossyScale.x != 0f && lossyScale.y != 0f && lossyScale.z != 0f)
			{
				lossyScale.x = 1f / lossyScale.x;
				lossyScale.y = 1f / lossyScale.y;
				lossyScale.z = 1f / lossyScale.z;
				if (base.transform.localScale != lossyScale)
				{
					base.transform.localScale = lossyScale;
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004A5C File Offset: 0x00002C5C
		private void GenerateConeMesh()
		{
			this.NormalizeScale();
			this.vertices.Clear();
			this.indices.Clear();
			this.generatedType = LightType.Spot;
			this.generatedTipRadius = this.tipRadius;
			this.generatedSpotAngle = this.lightComp.spotAngle;
			this.generatedRange = this.customRange;
			this.generatedBaseRadius = Mathf.Tan(this.lightComp.spotAngle * 0.017453292f * 0.5f) * this.customRange;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < 32; i++)
			{
				float f = 6.2831855f * (float)i / 32f;
				float num = Mathf.Cos(f);
				float num2 = Mathf.Sin(f);
				zero.x = num * this.generatedTipRadius;
				zero.y = num2 * this.generatedTipRadius;
				zero.z = 0f;
				this.vertices.Add(zero);
				zero.x = num * this.generatedBaseRadius;
				zero.y = num2 * this.generatedBaseRadius;
				zero.z = this.customRange;
				this.vertices.Add(zero);
			}
			int num3 = 64;
			for (int j = 0; j < num3; j += 2)
			{
				int item = j;
				int item2 = j + 1;
				int item3 = (j + 2) % num3;
				int item4 = (j + 3) % num3;
				this.indices.Add(item);
				this.indices.Add(item3);
				this.indices.Add(item2);
				this.indices.Add(item3);
				this.indices.Add(item4);
				this.indices.Add(item2);
			}
			this.vertices.Add(Vector3.zero);
			int item5 = num3;
			for (int k = 0; k < num3; k += 2)
			{
				this.indices.Add(k);
				this.indices.Add(item5);
				this.indices.Add((k + 2) % num3);
			}
			this.vertices.Add(new Vector3(0f, 0f, this.customRange));
			int item6 = num3 + 1;
			for (int l = 0; l < num3; l += 2)
			{
				this.indices.Add(item6);
				this.indices.Add(l + 1);
				this.indices.Add((l + 3) % num3);
			}
			this.UpdateMesh("Capped Cone");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004CBC File Offset: 0x00002EBC
		private void GenerateCubeMesh()
		{
			this.NormalizeScale();
			this.generatedType = LightType.Area;
			this.generatedRange = this.customRange;
			this.generatedAreaWidth = this.areaWidth;
			this.generatedAreaHeight = this.areaHeight;
			this.generatedAreaFrustumAngle = this.frustumAngle;
			this.generatedAreaFrustumMultiplier = 1f + Mathf.Tan(this.frustumAngle * 0.017453292f);
			this.vertices.Clear();
			this.indices.Clear();
			this.AddFace(VolumetricLight.faceVerticesBack);
			this.AddFace(VolumetricLight.faceVerticesForward);
			this.AddFace(VolumetricLight.faceVerticesLeft);
			this.AddFace(VolumetricLight.faceVerticesRight);
			this.AddFace(VolumetricLight.faceVerticesTop);
			this.AddFace(VolumetricLight.faceVerticesBottom);
			this.UpdateMesh("Box");
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004D88 File Offset: 0x00002F88
		private void AddFace(Vector3[] faceVertices)
		{
			int count = this.vertices.Count;
			foreach (Vector3 vector in faceVertices)
			{
				vector.x *= this.generatedAreaWidth;
				vector.y *= this.generatedAreaHeight;
				if (vector.z > 0f)
				{
					vector.x *= this.generatedAreaFrustumMultiplier;
					vector.y *= this.generatedAreaFrustumMultiplier;
				}
				vector.z *= this.generatedRange;
				this.vertices.Add(vector);
			}
			this.indices.Add(count);
			this.indices.Add(count + 1);
			this.indices.Add(count + 3);
			this.indices.Add(count + 3);
			this.indices.Add(count + 2);
			this.indices.Add(count);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004E78 File Offset: 0x00003078
		private void GenerateSphereMesh()
		{
			this.NormalizeScale();
			this.generatedRange = this.customRange;
			this.generatedType = LightType.Point;
			this.vertices.Clear();
			this.indices.Clear();
			this.vertices.Add(Vector3.up * this.generatedRange);
			for (int i = 0; i < 16; i++)
			{
				float f = 3.1415927f * (float)(i + 1) / 17f;
				float num = Mathf.Sin(f);
				float y = Mathf.Cos(f);
				for (int j = 0; j <= 24; j++)
				{
					float f2 = 6.2831855f * (float)((j == 24) ? 0 : j) / 24f;
					float num2 = Mathf.Sin(f2);
					float num3 = Mathf.Cos(f2);
					this.vertices.Add(new Vector3(num * num3, y, num * num2) * this.generatedRange);
				}
			}
			this.vertices.Add(Vector3.down * this.generatedRange);
			for (int k = 0; k < 24; k++)
			{
				this.indices.Add(k + 2);
				this.indices.Add(k + 1);
				this.indices.Add(0);
			}
			for (int l = 0; l < 15; l++)
			{
				for (int m = 0; m < 24; m++)
				{
					int num4 = m + l * 25 + 1;
					int num5 = num4 + 24 + 1;
					this.indices.Add(num4);
					this.indices.Add(num4 + 1);
					this.indices.Add(num5 + 1);
					this.indices.Add(num4);
					this.indices.Add(num5 + 1);
					this.indices.Add(num5);
				}
			}
			int count = this.vertices.Count;
			for (int n = 0; n < 24; n++)
			{
				this.indices.Add(count - 1);
				this.indices.Add(count - (n + 2) - 1);
				this.indices.Add(count - (n + 1) - 1);
			}
			this.UpdateMesh("Sphere");
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000509C File Offset: 0x0000329C
		private void GenerateCylinderMesh()
		{
			this.NormalizeScale();
			this.generatedType = LightType.Disc;
			this.generatedRange = this.customRange;
			this.generatedAreaWidth = (this.generatedAreaHeight = this.areaWidth);
			this.generatedAreaFrustumAngle = this.frustumAngle;
			this.generatedAreaFrustumMultiplier = 1f + Mathf.Tan(this.frustumAngle * 0.017453292f);
			this.vertices.Clear();
			this.indices.Clear();
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < 32; i++)
			{
				float f = 6.2831855f * (float)i / 32f;
				float num = Mathf.Cos(f);
				float num2 = Mathf.Sin(f);
				zero.x = num * this.generatedAreaWidth;
				zero.y = num2 * this.generatedAreaWidth;
				zero.z = 0f;
				this.vertices.Add(zero);
				zero.x *= this.generatedAreaFrustumMultiplier;
				zero.y *= this.generatedAreaFrustumMultiplier;
				zero.z = this.generatedRange;
				this.vertices.Add(zero);
			}
			int num3 = 64;
			for (int j = 0; j < num3; j += 2)
			{
				int item = j;
				int item2 = j + 1;
				int item3 = (j + 2) % num3;
				int item4 = (j + 3) % num3;
				this.indices.Add(item);
				this.indices.Add(item3);
				this.indices.Add(item2);
				this.indices.Add(item3);
				this.indices.Add(item4);
				this.indices.Add(item2);
			}
			this.vertices.Add(Vector3.zero);
			int item5 = num3;
			for (int k = 0; k < num3; k += 2)
			{
				this.indices.Add(k);
				this.indices.Add(item5);
				this.indices.Add((k + 2) % num3);
			}
			this.vertices.Add(new Vector3(0f, 0f, this.generatedRange));
			int item6 = num3 + 1;
			for (int l = 0; l < num3; l += 2)
			{
				this.indices.Add(item6);
				this.indices.Add(l + 1);
				this.indices.Add((l + 3) % num3);
			}
			this.UpdateMesh("Cylinder");
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000052F8 File Offset: 0x000034F8
		private void ParticlesDisable()
		{
			if (Application.isPlaying)
			{
				if (this.psRenderer != null)
				{
					this.psRenderer.enabled = false;
					return;
				}
			}
			else if (this.ps != null)
			{
				this.ps.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005348 File Offset: 0x00003548
		private void ParticlesResetIfTransformChanged()
		{
			if (this.ps != null && (this.ps.transform.position != this.psLastPos || this.ps.transform.rotation != this.psLastRot))
			{
				this.ParticlesPopulate();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000053A4 File Offset: 0x000035A4
		private void ParticlesPopulate()
		{
			if (this.dustPrewarm)
			{
				this.ps.Clear();
				this.ps.Simulate(100f);
			}
			this.psLastPos = this.ps.transform.position;
			this.psLastRot = this.ps.transform.rotation;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005400 File Offset: 0x00003600
		private void ParticlesCheckSupport()
		{
			if (!this.enableDustParticles)
			{
				this.ParticlesDisable();
				return;
			}
			bool flag = false;
			if (this.ps == null)
			{
				Transform transform = base.transform.Find("DustParticles");
				if (transform != null)
				{
					this.ps = transform.GetComponent<ParticleSystem>();
					if (this.ps == null)
					{
						UnityEngine.Object.DestroyImmediate(transform.gameObject);
					}
				}
				if (this.ps == null)
				{
					GameObject gameObject = Resources.Load<GameObject>("Prefabs/DustParticles");
					if (gameObject == null)
					{
						return;
					}
					gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject.name = "DustParticles";
					gameObject.transform.SetParent(base.transform, false);
					this.ps = gameObject.GetComponent<ParticleSystem>();
				}
				this.ps.gameObject.layer = 1;
				flag = true;
			}
			if (this.particleMaterial == null)
			{
				this.particleMaterial = UnityEngine.Object.Instantiate<Material>(Resources.Load<Material>("Materials/DustParticle"));
			}
			if (this.keywords == null)
			{
				this.keywords = new List<string>();
			}
			else
			{
				this.keywords.Clear();
			}
			if (this.useCustomBounds)
			{
				this.keywords.Add("VL_CUSTOM_BOUNDS");
			}
			switch (this.generatedType)
			{
			case LightType.Spot:
				if (this.cookieTexture != null)
				{
					this.keywords.Add("VL_SPOT_COOKIE");
					this.particleMaterial.SetTexture(VolumetricLight.ShaderParams.CookieTexture, this.cookieTexture);
					this.particleMaterial.SetVector(VolumetricLight.ShaderParams.CookieTexture_ScaleAndSpeed, new Vector4(this.cookieScale.x, this.cookieScale.y, this.cookieSpeed.x, this.cookieSpeed.y));
					this.particleMaterial.SetVector(VolumetricLight.ShaderParams.CookieTexture_Offset, new Vector4(this.cookieOffset.x, this.cookieOffset.y, 0f, 0f));
				}
				else
				{
					this.keywords.Add("VL_SPOT");
				}
				break;
			case LightType.Point:
				this.keywords.Add("VL_POINT");
				break;
			case LightType.Area:
				this.keywords.Add("VL_AREA_RECT");
				break;
			case LightType.Disc:
				this.keywords.Add("VL_AREA_DISC");
				break;
			}
			if (this.attenuationMode == AttenuationMode.Quadratic)
			{
				this.keywords.Add("VL_PHYSICAL_ATTEN");
			}
			if (this.enableShadows)
			{
				if (this.usesCubemap)
				{
					this.keywords.Add("VL_SHADOWS_CUBEMAP");
				}
				else if (this.usesTranslucency)
				{
					this.keywords.Add("VL_SHADOWS_TRANSLUCENCY");
				}
				else
				{
					this.keywords.Add("VL_SHADOWS");
				}
			}
			this.particleMaterial.shaderKeywords = this.keywords.ToArray();
			this.particleMaterial.renderQueue = this.renderQueue + 1;
			this.particleMaterial.SetFloat(VolumetricLight.ShaderParams.Penumbra, this.penumbra);
			this.particleMaterial.SetFloat(VolumetricLight.ShaderParams.RangeFallOff, this.rangeFallOff);
			this.particleMaterial.SetVector(VolumetricLight.ShaderParams.FallOff, new Vector3(this.attenCoefConstant, this.attenCoefLinear, this.attenCoefQuadratic));
			this.UpdateParticleColor();
			this.particleMaterial.SetFloat(VolumetricLight.ShaderParams.ParticleDistanceAtten, this.dustDistanceAttenuation * this.dustDistanceAttenuation);
			if (this.psRenderer == null)
			{
				this.psRenderer = this.ps.GetComponent<ParticleSystemRenderer>();
			}
			this.psRenderer.material = this.particleMaterial;
			ParticleSystem.MainModule main = this.ps.main;
			main.simulationSpace = ParticleSystemSimulationSpace.World;
			ParticleSystem.MinMaxCurve startSize = main.startSize;
			startSize.mode = ParticleSystemCurveMode.TwoConstants;
			startSize.constantMin = this.dustMinSize;
			startSize.constantMax = this.dustMaxSize;
			main.startSize = startSize;
			ParticleSystem.ShapeModule shape = this.ps.shape;
			switch (this.generatedType)
			{
			case LightType.Spot:
				shape.shapeType = ParticleSystemShapeType.ConeVolume;
				shape.angle = this.generatedSpotAngle * 0.5f;
				shape.position = Vector3.zero;
				shape.radius = this.tipRadius;
				shape.length = this.generatedRange;
				shape.scale = Vector3.one;
				break;
			case LightType.Point:
				shape.shapeType = ParticleSystemShapeType.Sphere;
				shape.position = Vector3.zero;
				shape.scale = Vector3.one;
				shape.radius = this.generatedRange;
				break;
			case LightType.Area:
			case LightType.Disc:
				shape.shapeType = ParticleSystemShapeType.Box;
				shape.position = new Vector3(0f, 0f, this.generatedRange * 0.5f);
				shape.scale = base.GetComponent<MeshFilter>().sharedMesh.bounds.size;
				break;
			}
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = this.ps.velocityOverLifetime;
			Vector3 vector = base.transform.InverseTransformDirection(this.windDirection);
			ParticleSystem.MinMaxCurve x = velocityOverLifetime.x;
			x.constantMin = (-0.1f + vector.x) * this.dustWindSpeed;
			x.constantMax = (0.1f + vector.x) * this.dustWindSpeed;
			velocityOverLifetime.x = x;
			ParticleSystem.MinMaxCurve y = velocityOverLifetime.y;
			y.constantMin = (-0.1f + vector.y) * this.dustWindSpeed;
			y.constantMax = (0.1f + vector.y) * this.dustWindSpeed;
			velocityOverLifetime.y = y;
			ParticleSystem.MinMaxCurve z = velocityOverLifetime.z;
			z.constantMin = (-0.1f + vector.z) * this.dustWindSpeed;
			z.constantMax = (0.1f + vector.z) * this.dustWindSpeed;
			velocityOverLifetime.z = z;
			if (!this.ps.gameObject.activeSelf)
			{
				this.ps.gameObject.SetActive(true);
			}
			this.UpdateParticlesVisibility();
			if (flag || this.ps.particleCount == 0)
			{
				this.ParticlesPopulate();
			}
			if (!this.ps.isPlaying)
			{
				this.ps.Play();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005A24 File Offset: 0x00003C24
		private void UpdateParticlesVisibility()
		{
			this.UpdateParticleColor();
			if (!Application.isPlaying || this.psRenderer == null)
			{
				return;
			}
			bool flag = this.meshRenderer.isVisible;
			if (flag && this.dustAutoToggle)
			{
				float num = this.dustDistanceDeactivation * this.dustDistanceDeactivation;
				flag = (this.distanceToCameraSqr <= num);
			}
			if (flag)
			{
				if (!this.psRenderer.enabled)
				{
					this.psRenderer.enabled = true;
					return;
				}
			}
			else if (this.psRenderer.enabled)
			{
				this.psRenderer.enabled = false;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005AB8 File Offset: 0x00003CB8
		private void UpdateParticleColor()
		{
			if (this.particleMaterial != null)
			{
				this.particleMaterial.SetColor(VolumetricLight.ShaderParams.ParticleTintColor, this.lightComp.color * this.mediumAlbedo * (this.lightComp.intensity * this.dustBrightness));
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005B10 File Offset: 0x00003D10
		private void SettingsInit()
		{
			if (this.noiseTexture == null)
			{
				this.noiseTexture = Resources.Load<Texture3D>("Textures/NoiseTex3D1");
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005B30 File Offset: 0x00003D30
		private void SettingsValidate()
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
			this.raymarchMaxSteps = Mathf.Max(1, this.raymarchMaxSteps);
			this.jittering = Mathf.Max(0f, this.jittering);
			this.dithering = Mathf.Max(0f, this.dithering);
			this.distanceStartDimming = Mathf.Max(0f, this.distanceStartDimming);
			this.distanceDeactivation = Mathf.Max(0f, this.distanceDeactivation);
			this.distanceStartDimming = Mathf.Min(this.distanceStartDimming, this.distanceDeactivation);
			this.shadowIntensity = Mathf.Max(0f, this.shadowIntensity);
			if (this.shadowDirection == Vector3.zero)
			{
				this.shadowDirection = Vector3.down;
			}
			else
			{
				this.shadowDirection.Normalize();
			}
			this.shadowTranslucencyIntensity = Mathf.Max(0f, this.shadowTranslucencyIntensity);
			this.directLightMultiplier = Mathf.Max(0f, this.directLightMultiplier);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00005E42 File Offset: 0x00004042
		private bool usesCubemap
		{
			get
			{
				return this.shadowBakeMode != ShadowBakeMode.HalfSphere && this.generatedType == LightType.Point;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005E57 File Offset: 0x00004057
		private bool usesTranslucency
		{
			get
			{
				return this.shadowTranslucency && (this.generatedType == LightType.Spot || this.generatedType == LightType.Area || this.generatedType == LightType.Disc);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005E80 File Offset: 0x00004080
		private void CheckShadowCam()
		{
			if (this.cam == null)
			{
				Transform transform = base.transform.Find("OcclusionCam");
				if (transform != null)
				{
					this.cam = transform.GetComponent<Camera>();
					if (this.cam == null)
					{
						UnityEngine.Object.DestroyImmediate(transform.gameObject);
					}
				}
			}
			this.SetupCamRenderingProperties();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005EE0 File Offset: 0x000040E0
		private void ShadowsDisable()
		{
			if (this.cam != null)
			{
				this.cam.enabled = false;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005EFC File Offset: 0x000040FC
		private void ShadowsDispose()
		{
			if (this.cam != null)
			{
				this.cam.targetTexture = null;
				this.cam.enabled = false;
			}
			this.DisposeRTs();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005F2C File Offset: 0x0000412C
		private void DisposeRTs()
		{
			if (this.rt != null)
			{
				this.rt.Release();
				UnityEngine.Object.DestroyImmediate(this.rt);
			}
			if (this.shadowCubemap != null)
			{
				this.shadowCubemap.Release();
				UnityEngine.Object.DestroyImmediate(this.shadowCubemap);
			}
			if (this.translucentMap != null)
			{
				this.translucentMap.Release();
				UnityEngine.Object.DestroyImmediate(this.translucentMap);
			}
			if (this.transpOverrideMaterials != null)
			{
				for (int i = 0; i < this.transpOverrideMaterials.Length; i++)
				{
					UnityEngine.Object.DestroyImmediate(this.transpOverrideMaterials[i]);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005FD0 File Offset: 0x000041D0
		private void ShadowsSupportCheck()
		{
			bool flag = this.cookieTexture != null && this.lightComp.type == LightType.Spot;
			if (!this.enableShadows && !flag)
			{
				this.ShadowsDispose();
				return;
			}
			this.usesReversedZBuffer = SystemInfo.usesReversedZBuffer;
			VolumetricLight.textureScaleAndBias = Matrix4x4.identity;
			VolumetricLight.textureScaleAndBias.m00 = 0.5f;
			VolumetricLight.textureScaleAndBias.m11 = 0.5f;
			VolumetricLight.textureScaleAndBias.m22 = 0.5f;
			VolumetricLight.textureScaleAndBias.m03 = 0.5f;
			VolumetricLight.textureScaleAndBias.m13 = 0.5f;
			VolumetricLight.textureScaleAndBias.m23 = 0.5f;
			if (this.cam == null)
			{
				Transform transform = base.transform.Find("OcclusionCam");
				if (transform != null)
				{
					this.cam = transform.GetComponent<Camera>();
					if (this.cam == null)
					{
						UnityEngine.Object.DestroyImmediate(transform.gameObject);
					}
				}
				if (this.cam == null)
				{
					GameObject gameObject = new GameObject("OcclusionCam", new Type[]
					{
						typeof(Camera)
					});
					gameObject.transform.SetParent(base.transform, false);
					this.cam = gameObject.GetComponent<Camera>();
					this.cam.depthTextureMode = DepthTextureMode.None;
					this.cam.clearFlags = CameraClearFlags.Depth;
					this.cam.allowHDR = false;
					this.cam.allowMSAA = false;
				}
				this.cam.stereoTargetEye = StereoTargetEyeMask.None;
			}
			this.SetupCamRenderingProperties();
			this.cam.nearClipPlane = this.shadowNearDistance;
			this.cam.orthographicSize = Mathf.Max(this.generatedAreaWidth, this.generatedAreaHeight);
			switch (this.generatedType)
			{
			case LightType.Spot:
				this.cam.transform.localRotation = Quaternion.identity;
				this.cam.orthographic = false;
				this.cam.fieldOfView = this.generatedSpotAngle;
				break;
			case LightType.Point:
				this.cam.orthographic = false;
				if (this.shadowBakeMode != ShadowBakeMode.HalfSphere)
				{
					this.cam.fieldOfView = 90f;
				}
				else
				{
					this.cam.fieldOfView = 160f;
				}
				break;
			case LightType.Area:
			case LightType.Disc:
				this.cam.transform.localRotation = Quaternion.identity;
				this.cam.orthographic = true;
				this.cam.orthographicSize *= this.generatedAreaFrustumMultiplier;
				break;
			}
			RenderTextureFormat renderTextureFormat = this.shadowUseDefaultRTFormat ? RenderTextureFormat.Default : RenderTextureFormat.Depth;
			if (this.rt != null && (this.rt.width != (int)this.shadowResolution || renderTextureFormat != this.rt.format))
			{
				if (this.cam.targetTexture == this.rt)
				{
					this.cam.targetTexture = null;
				}
				this.DisposeRTs();
			}
			if (this.rt == null)
			{
				if (this.shadowUseDefaultRTFormat)
				{
					this.rt = new RenderTexture((int)this.shadowResolution, (int)this.shadowResolution, 16);
				}
				else
				{
					this.rt = new RenderTexture((int)this.shadowResolution, (int)this.shadowResolution, 16, RenderTextureFormat.Depth, RenderTextureReadWrite.Linear);
				}
				this.rt.antiAliasing = 1;
				this.rt.useMipMap = false;
			}
			if (this.usesTranslucency && this.translucentMap == null)
			{
				this.translucentMap = new RenderTexture((int)this.shadowResolution, (int)this.shadowResolution, 0, RenderTextureFormat.ARGBHalf);
				this.translucentMap.antiAliasing = 1;
				this.translucentMap.useMipMap = false;
			}
			if (this.usesCubemap && this.shadowCubemap == null)
			{
				this.shadowCubemap = new RenderTexture((int)this.shadowResolution, (int)this.shadowResolution, 0, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
				this.shadowCubemap.dimension = TextureDimension.Cube;
				this.shadowCubemap.antiAliasing = 1;
				this.shadowCubemap.useMipMap = false;
			}
			this.fogMat.SetVector(VolumetricLight.ShaderParams.ShadowIntensity, new Vector4(this.shadowIntensity, 1f - this.shadowIntensity, 0f, 0f));
			if ((this.shadowCullingMask & 2) != 0)
			{
				this.shadowCullingMask &= -3;
			}
			this.cam.cullingMask = this.shadowCullingMask;
			this.cam.targetTexture = this.rt;
			if (this.enableShadows)
			{
				this.shouldOrientToCamera = true;
				this.ScheduleShadowCapture();
				return;
			}
			this.cam.enabled = false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000645C File Offset: 0x0000465C
		private void SetupCamRenderingProperties()
		{
			if (this.cam == null)
			{
				return;
			}
			this.cam.renderingPath = RenderingPath.Forward;
			this.cam.depthTextureMode = DepthTextureMode.None;
			this.cam.clearFlags = CameraClearFlags.Depth;
			this.cam.allowHDR = false;
			this.cam.allowMSAA = false;
			this.cam.stereoTargetEye = StereoTargetEyeMask.None;
			if (this.depthShader == null)
			{
				this.depthShader = Shader.Find("Hidden/VolumetricLights/DepthOnly");
			}
			if (this.shadowOptimizeShadowCasters)
			{
				this.cam.SetReplacementShader(this.depthShader, null);
				return;
			}
			this.cam.ResetReplacementShader();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006504 File Offset: 0x00004704
		public void ScheduleShadowCapture()
		{
			if (this.cam == null)
			{
				return;
			}
			if (this.usesCubemap)
			{
				if (this.copyDepthIntoCubemap == null)
				{
					this.copyDepthIntoCubemap = new Material(Shader.Find("Hidden/VolumetricLights/CopyDepthIntoCubemap"));
				}
				this.copyDepthIntoCubemap.SetVector(VolumetricLight.ShaderParams.LightPos, this.cam.transform.position);
				RenderTexture active = RenderTexture.active;
				int num = (this.shadowBakeMode == ShadowBakeMode.CubemapOneFacePerFrame && this.shadowBakeInterval == ShadowBakeInterval.EveryFrame) ? 1 : 6;
				for (int i = 0; i < num; i++)
				{
					int num2 = this.currentCubemapFace % 6;
					this.cam.transform.forward = VolumetricLight.camFaceDirections[num2];
					this.cam.Render();
					this.copyDepthIntoCubemap.SetMatrix(VolumetricLight.ShaderParams.InvVPMatrix, this.cam.cameraToWorldMatrix * GL.GetGPUProjectionMatrix(this.cam.projectionMatrix, false).inverse);
					this.copyDepthIntoCubemap.SetTexture(VolumetricLight.ShaderParams.ShadowTexture, this.rt, RenderTextureSubElement.Depth);
					Graphics.SetRenderTarget(this.shadowCubemap, 0, (CubemapFace)num2);
					Graphics.Blit(this.rt, this.copyDepthIntoCubemap);
					this.currentCubemapFace++;
				}
				this.cam.enabled = false;
				RenderTexture.active = active;
				this.fogMat.SetTexture(VolumetricLight.ShaderParams.ShadowCubemap, this.shadowCubemap);
				if (this.enableDustParticles && this.particleMaterial != null)
				{
					this.particleMaterial.SetTexture(VolumetricLight.ShaderParams.ShadowCubemap, this.shadowCubemap);
				}
				if (!this.fogMat.IsKeywordEnabled("VL_SHADOWS_CUBEMAP"))
				{
					this.fogMat.EnableKeyword("VL_SHADOWS_CUBEMAP");
					return;
				}
			}
			else
			{
				this.cam.enabled = true;
				this.camStartFrameCount = Time.frameCount;
				string keyword = this.usesTranslucency ? "VL_SHADOWS_TRANSLUCENCY" : "VL_SHADOWS";
				if (!this.fogMat.IsKeywordEnabled(keyword))
				{
					this.fogMat.EnableKeyword(keyword);
				}
				if (this.usesTranslucency)
				{
					this.CaptureTransparents();
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000671C File Offset: 0x0000491C
		private void SetupShadowMatrix()
		{
			if (this.usesCubemap)
			{
				return;
			}
			this.ComputeShadowTransform(this.cam.projectionMatrix, this.cam.worldToCameraMatrix);
			this.fogMat.SetMatrix(VolumetricLight.ShaderParams.ShadowMatrix, this.shadowMatrix);
			RenderTextureSubElement element = this.shadowUseDefaultRTFormat ? RenderTextureSubElement.Depth : RenderTextureSubElement.Default;
			this.fogMat.SetTexture(VolumetricLight.ShaderParams.ShadowTexture, this.cam.targetTexture, element);
			this.fogMat.SetTexture(VolumetricLight.ShaderParams.TranslucencyTexture, this.translucentMap);
			if (this.enableDustParticles && this.particleMaterial != null)
			{
				this.particleMaterial.SetMatrix(VolumetricLight.ShaderParams.ShadowMatrix, this.shadowMatrix);
				this.particleMaterial.SetTexture(VolumetricLight.ShaderParams.ShadowTexture, this.cam.targetTexture, element);
				this.particleMaterial.SetVector(VolumetricLight.ShaderParams.ShadowIntensity, new Vector4(this.shadowIntensity, 1f - this.shadowIntensity, 0f, 0f));
				this.particleMaterial.SetTexture(VolumetricLight.ShaderParams.TranslucencyTexture, this.translucentMap);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006838 File Offset: 0x00004A38
		private void ShadowsUpdate()
		{
			bool flag = this.cookieTexture != null && this.lightComp.type == LightType.Spot;
			if (!this.enableShadows && !flag)
			{
				return;
			}
			if (this.cam == null)
			{
				return;
			}
			int frameCount = Time.frameCount;
			if (!this.meshRenderer.isVisible && frameCount - this.camStartFrameCount > 5)
			{
				if (this.cam.enabled)
				{
					this.ShadowsDisable();
				}
				return;
			}
			Transform transform = this.cam.transform;
			this.cam.farClipPlane = this.generatedRange;
			if (this.generatedType == LightType.Point && this.shadowBakeMode == ShadowBakeMode.HalfSphere)
			{
				if (this.shadowOrientation == ShadowOrientation.ToCamera)
				{
					if (this.enableShadows && this.mainCamera != null)
					{
						if (this.shadowBakeInterval != ShadowBakeInterval.EveryFrame && Vector3.Angle(transform.forward, this.mainCamera.position - this.lastCamPos) > 45f)
						{
							this.shouldOrientToCamera = true;
							this.ScheduleShadowCapture();
						}
						if (this.shouldOrientToCamera || this.shadowBakeInterval == ShadowBakeInterval.EveryFrame)
						{
							this.shouldOrientToCamera = false;
							transform.LookAt(this.mainCamera.position);
						}
					}
				}
				else
				{
					transform.forward = this.shadowDirection;
				}
			}
			this.camTransformChanged = false;
			if (this.lastCamPos != transform.position || this.lastCamRot != transform.rotation)
			{
				this.camTransformChanged = true;
				this.lastCamPos = transform.position;
				this.lastCamRot = transform.rotation;
			}
			if (this.enableShadows)
			{
				this.ShadowCamUpdate();
			}
			if (this.camTransformChanged || flag || this.cam.enabled)
			{
				this.SetupShadowMatrix();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000069F8 File Offset: 0x00004BF8
		private void ShadowCamUpdate()
		{
			if (this.shadowAutoToggle)
			{
				float num = this.shadowDistanceDeactivation * this.shadowDistanceDeactivation;
				if (this.distanceToCameraSqr > num)
				{
					if (this.cam.enabled)
					{
						this.ShadowsDisable();
						if (this.fogMat.IsKeywordEnabled("VL_SHADOWS"))
						{
							this.fogMat.DisableKeyword("VL_SHADOWS");
						}
						if (this.fogMat.IsKeywordEnabled("VL_SHADOWS_TRANSLUCENCY"))
						{
							this.fogMat.DisableKeyword("VL_SHADOWS_TRANSLUCENCY");
						}
						if (this.fogMat.IsKeywordEnabled("VL_SHADOWS_CUBEMAP"))
						{
							this.fogMat.DisableKeyword("VL_SHADOWS_CUBEMAP");
						}
					}
					return;
				}
			}
			if (this.shadowBakeInterval == ShadowBakeInterval.OnStart)
			{
				if (!this.cam.enabled && this.camTransformChanged)
				{
					this.ScheduleShadowCapture();
				}
				else if (Application.isPlaying && Time.frameCount > this.camStartFrameCount + 1)
				{
					this.cam.enabled = false;
				}
			}
			else if (!this.cam.enabled)
			{
				this.ScheduleShadowCapture();
			}
			if (this.usesTranslucency && this.updateTranslucency)
			{
				this.CaptureTransparents();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006B14 File Offset: 0x00004D14
		private void ComputeShadowTransform(Matrix4x4 proj, Matrix4x4 view)
		{
			if (this.usesReversedZBuffer)
			{
				proj.m20 = -proj.m20;
				proj.m21 = -proj.m21;
				proj.m22 = -proj.m22;
				proj.m23 = -proj.m23;
			}
			Matrix4x4 rhs = proj * view;
			this.shadowMatrix = VolumetricLight.textureScaleAndBias * rhs;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006B7C File Offset: 0x00004D7C
		private void CaptureTransparents()
		{
			int count = VolumetricLightsTranslucency.objects.Count;
			if (count == 0)
			{
				return;
			}
			if (this.transpOverrideMat == null)
			{
				Shader shader = Shader.Find("Hidden/VolumetricLights/TransparentMultiply");
				this.transpOverrideMat = new Material(shader);
			}
			if (this.transpDepthOnlyMat == null)
			{
				Shader shader2 = Shader.Find("Hidden/VolumetricLights/TransparentDepthWrite");
				this.transpDepthOnlyMat = new Material(shader2);
			}
			if (this.transpOverrideMaterials == null || this.transpOverrideMaterials.Length < count)
			{
				this.transpOverrideMaterials = new Material[count];
			}
			if (this.cmdTransp == null)
			{
				this.cmdTransp = new CommandBuffer();
			}
			else
			{
				this.cmdTransp.Clear();
			}
			RenderTexture active = RenderTexture.active;
			this.cmdTransp.SetRenderTarget(this.translucentMap);
			this.cmdTransp.ClearRenderTarget(false, true, Color.white);
			this.cmdTransp.SetViewProjectionMatrices(this.cam.worldToCameraMatrix, this.cam.projectionMatrix);
			float num = 10f * this.shadowTranslucencyIntensity / (this.brightness + 0.0001f);
			for (int i = 0; i < count; i++)
			{
				VolumetricLightsTranslucency volumetricLightsTranslucency = VolumetricLightsTranslucency.objects[i];
				if (!(volumetricLightsTranslucency == null) && !(volumetricLightsTranslucency.theRenderer == null) && (volumetricLightsTranslucency.theRenderer.isVisible || volumetricLightsTranslucency.renderIfOffscreen) && volumetricLightsTranslucency.intensityMultiplier > 0f)
				{
					Material sharedMaterial = volumetricLightsTranslucency.theRenderer.sharedMaterial;
					if (!(sharedMaterial == null))
					{
						if (volumetricLightsTranslucency.preserveOriginalShader)
						{
							this.cmdTransp.DrawRenderer(volumetricLightsTranslucency.theRenderer, sharedMaterial);
							this.cmdTransp.DrawRenderer(volumetricLightsTranslucency.theRenderer, this.transpDepthOnlyMat);
						}
						else if (sharedMaterial.HasProperty(VolumetricLight.ShaderParams.MainTex))
						{
							if (this.transpOverrideMaterials[i] == null)
							{
								this.transpOverrideMaterials[i] = UnityEngine.Object.Instantiate<Material>(this.transpOverrideMat);
							}
							Material material = this.transpOverrideMaterials[i];
							material.SetTexture(VolumetricLight.ShaderParams.MainTex, sharedMaterial.GetTexture(VolumetricLight.ShaderParams.MainTex));
							if (sharedMaterial.HasProperty(VolumetricLight.ShaderParams.Color))
							{
								material.SetColor(VolumetricLight.ShaderParams.Color, sharedMaterial.GetColor(VolumetricLight.ShaderParams.Color));
							}
							material.SetVector(VolumetricLight.ShaderParams.TranslucencyIntensity, new Vector4(num * volumetricLightsTranslucency.intensityMultiplier, this.shadowTranslucencyBlend, 0f, 0f));
							Vector2 textureScale = sharedMaterial.GetTextureScale(VolumetricLight.ShaderParams.MainTex);
							Vector2 textureOffset = sharedMaterial.GetTextureOffset(VolumetricLight.ShaderParams.MainTex);
							material.SetVector(VolumetricLight.ShaderParams.MainTex_ST, new Vector4(textureScale.x, textureScale.y, textureOffset.x, textureOffset.y));
							this.cmdTransp.DrawRenderer(volumetricLightsTranslucency.theRenderer, material);
						}
					}
				}
			}
			Graphics.ExecuteCommandBuffer(this.cmdTransp);
			RenderTexture.active = active;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006E68 File Offset: 0x00005068
		public VolumetricLight()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000070B0 File Offset: 0x000052B0
		// Note: this type is marked as 'beforefieldinit'.
		static VolumetricLight()
		{
		}

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private PropertiesChangedEvent OnPropertiesChanged;

		// Token: 0x04000054 RID: 84
		public bool profileSync = true;

		// Token: 0x04000055 RID: 85
		public bool useCustomBounds;

		// Token: 0x04000056 RID: 86
		public Bounds bounds;

		// Token: 0x04000057 RID: 87
		[Tooltip("In enabled, bounds coordinates are relative to the light position")]
		public bool boundsInLocalSpace;

		// Token: 0x04000058 RID: 88
		public VolumetricLightProfile profile;

		// Token: 0x04000059 RID: 89
		public float customRange = 1f;

		// Token: 0x0400005A RID: 90
		[Tooltip("Used for point light occlusion orientation and checking camera distance when autoToggle options are enabled. If not assigned, it will try to use the main camera.")]
		public Transform targetCamera;

		// Token: 0x0400005B RID: 91
		public bool useCustomSize;

		// Token: 0x0400005C RID: 92
		public float areaWidth = 1f;

		// Token: 0x0400005D RID: 93
		public float areaHeight = 1f;

		// Token: 0x0400005E RID: 94
		[NonSerialized]
		public Light lightComp;

		// Token: 0x0400005F RID: 95
		private const float GOLDEN_RATIO = 0.618034f;

		// Token: 0x04000060 RID: 96
		private MeshFilter mf;

		// Token: 0x04000061 RID: 97
		[NonSerialized]
		public MeshRenderer meshRenderer;

		// Token: 0x04000062 RID: 98
		private Material fogMat;

		// Token: 0x04000063 RID: 99
		private Material fogMatLight;

		// Token: 0x04000064 RID: 100
		private Material fogMatInvisible;

		// Token: 0x04000065 RID: 101
		private Vector4 windDirectionAcum;

		// Token: 0x04000066 RID: 102
		private bool requireUpdateMaterial;

		// Token: 0x04000067 RID: 103
		private List<string> keywords;

		// Token: 0x04000068 RID: 104
		private static Texture2D blueNoiseTex;

		// Token: 0x04000069 RID: 105
		private float distanceToCameraSqr;

		// Token: 0x0400006A RID: 106
		[NonSerialized]
		public Transform mainCamera;

		// Token: 0x0400006B RID: 107
		private float lastDistanceCheckTime;

		// Token: 0x0400006C RID: 108
		private bool wasInRange;

		// Token: 0x0400006D RID: 109
		public static List<VolumetricLight> volumetricLights = new List<VolumetricLight>();

		// Token: 0x0400006E RID: 110
		[NonSerialized]
		public bool isInvisible;

		// Token: 0x0400006F RID: 111
		private const int SIDES = 32;

		// Token: 0x04000070 RID: 112
		private readonly List<Vector3> vertices = new List<Vector3>(32);

		// Token: 0x04000071 RID: 113
		private readonly List<int> indices = new List<int>(32);

		// Token: 0x04000072 RID: 114
		public float generatedRange = -1f;

		// Token: 0x04000073 RID: 115
		public float generatedTipRadius = -1f;

		// Token: 0x04000074 RID: 116
		public float generatedSpotAngle = -1f;

		// Token: 0x04000075 RID: 117
		public float generatedBaseRadius;

		// Token: 0x04000076 RID: 118
		public float generatedAreaWidth;

		// Token: 0x04000077 RID: 119
		public float generatedAreaHeight;

		// Token: 0x04000078 RID: 120
		public float generatedAreaFrustumAngle;

		// Token: 0x04000079 RID: 121
		public float generatedAreaFrustumMultiplier;

		// Token: 0x0400007A RID: 122
		public LightType generatedType;

		// Token: 0x0400007B RID: 123
		private static readonly Vector3[] faceVerticesForward = new Vector3[]
		{
			new Vector3(0.5f, -0.5f, 1f),
			new Vector3(0.5f, 0.5f, 1f),
			new Vector3(-0.5f, -0.5f, 1f),
			new Vector3(-0.5f, 0.5f, 1f)
		};

		// Token: 0x0400007C RID: 124
		private static readonly Vector3[] faceVerticesBack = new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, 0f),
			new Vector3(-0.5f, 0.5f, 0f),
			new Vector3(0.5f, -0.5f, 0f),
			new Vector3(0.5f, 0.5f, 0f)
		};

		// Token: 0x0400007D RID: 125
		private static readonly Vector3[] faceVerticesLeft = new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, 1f),
			new Vector3(-0.5f, 0.5f, 1f),
			new Vector3(-0.5f, -0.5f, 0f),
			new Vector3(-0.5f, 0.5f, 0f)
		};

		// Token: 0x0400007E RID: 126
		private static readonly Vector3[] faceVerticesRight = new Vector3[]
		{
			new Vector3(0.5f, -0.5f, 0f),
			new Vector3(0.5f, 0.5f, 0f),
			new Vector3(0.5f, -0.5f, 1f),
			new Vector3(0.5f, 0.5f, 1f)
		};

		// Token: 0x0400007F RID: 127
		private static readonly Vector3[] faceVerticesTop = new Vector3[]
		{
			new Vector3(-0.5f, 0.5f, 0f),
			new Vector3(-0.5f, 0.5f, 1f),
			new Vector3(0.5f, 0.5f, 0f),
			new Vector3(0.5f, 0.5f, 1f)
		};

		// Token: 0x04000080 RID: 128
		private static readonly Vector3[] faceVerticesBottom = new Vector3[]
		{
			new Vector3(0.5f, -0.5f, 0f),
			new Vector3(0.5f, -0.5f, 1f),
			new Vector3(-0.5f, -0.5f, 0f),
			new Vector3(-0.5f, -0.5f, 1f)
		};

		// Token: 0x04000081 RID: 129
		private const string PARTICLE_SYSTEM_NAME = "DustParticles";

		// Token: 0x04000082 RID: 130
		private Material particleMaterial;

		// Token: 0x04000083 RID: 131
		[NonSerialized]
		public ParticleSystem ps;

		// Token: 0x04000084 RID: 132
		private ParticleSystemRenderer psRenderer;

		// Token: 0x04000085 RID: 133
		private Vector3 psLastPos;

		// Token: 0x04000086 RID: 134
		private Quaternion psLastRot;

		// Token: 0x04000087 RID: 135
		[Header("Rendering")]
		public BlendMode blendMode;

		// Token: 0x04000088 RID: 136
		public RaymarchPresets raymarchPreset;

		// Token: 0x04000089 RID: 137
		[Tooltip("Determines the general accuracy of the effect. The greater this value, the more accurate effect (shadow occlusion as well). Try to keep this value as low as possible while maintainig a good visual result. If you need better performance increase the 'Raymarch Min Step' and then 'Jittering' amount to improve quality.")]
		[Range(1f, 256f)]
		public int raymarchQuality = 8;

		// Token: 0x0400008A RID: 138
		[Tooltip("Determines the minimum step size. Increase to improve performance / decrease to improve accuracy. When increasing this value, you can also increase 'Jittering' amount to improve quality.")]
		public float raymarchMinStep = 0.1f;

		// Token: 0x0400008B RID: 139
		[Tooltip("Maximum number of raymarch steps. This value represents a hard maximum, usually each ray uses less samples but it can be topped by lowering this value.")]
		public int raymarchMaxSteps = 200;

		// Token: 0x0400008C RID: 140
		[Tooltip("Increase to reduce inaccuracy due to low number of samples (due to a high raymarch step size).")]
		public float jittering = 0.5f;

		// Token: 0x0400008D RID: 141
		[Tooltip("Increase to reduce banding artifacts. Usually jittering has a bigger impact in reducing artifacts.")]
		public float dithering = 1f;

		// Token: 0x0400008E RID: 142
		[Tooltip("Uses blue noise for jittering computation reducing moiré pattern of normal jitter. Usually not needed unless you use shadow occlusion. It adds an additional texture fetch so use only if it provides you a clear visual improvement.")]
		public bool useBlueNoise;

		// Token: 0x0400008F RID: 143
		public bool animatedBlueNoise = true;

		// Token: 0x04000090 RID: 144
		[Tooltip("The render queue for this renderer. By default, all transparent objects use a render queue of 3000. Use a lower value to render before all transparent objects.")]
		public int renderQueue = 3101;

		// Token: 0x04000091 RID: 145
		[Tooltip("Optional sorting layer Id (number) for this renderer. By default 0. Usually used to control the order with other transparent renderers, like Sprite Renderer.")]
		public int sortingLayerID;

		// Token: 0x04000092 RID: 146
		[Tooltip("Optional sorting order for this renderer. Used to control the order with other transparent renderers, like Sprite Renderer.")]
		public int sortingOrder;

		// Token: 0x04000093 RID: 147
		[Tooltip("Ignores light enable state. Always show volumetric fog. This option is useful to produce fake volumetric lights.")]
		public bool alwaysOn;

		// Token: 0x04000094 RID: 148
		[Tooltip("If the volumetric light effect may illuminate the objects. Only available in deferred rendering path.")]
		public bool castDirectLight;

		// Token: 0x04000095 RID: 149
		[Tooltip("Multiplier for the direct light")]
		public float directLightMultiplier = 1f;

		// Token: 0x04000096 RID: 150
		[Tooltip("Number of samples to creates smooth shadows")]
		[Range(1f, 32f)]
		public int directLightSmoothSamples = 8;

		// Token: 0x04000097 RID: 151
		[Tooltip("Radius for the smooth shadow")]
		[Range(0f, 8f)]
		public float directLightSmoothRadius = 4f;

		// Token: 0x04000098 RID: 152
		[Tooltip("Blending mode for the direct light effect. Blend mode requires deferred rendering mode and is slower.")]
		public DirectLightBlendMode directLightBlendMode;

		// Token: 0x04000099 RID: 153
		[Tooltip("Fully enable/disable volumetric effect when far from main camera in order to optimize performance")]
		public bool autoToggle;

		// Token: 0x0400009A RID: 154
		[Tooltip("Distance to the light source at which the volumetric effect starts to dim. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float distanceStartDimming = 70f;

		// Token: 0x0400009B RID: 155
		[Tooltip("Distance to the light source at which the volumetric effect is fully deactivated. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float distanceDeactivation = 100f;

		// Token: 0x0400009C RID: 156
		[Tooltip("Minimum time between distance checks")]
		public float autoToggleCheckInterval = 1f;

		// Token: 0x0400009D RID: 157
		[Header("Appearance")]
		public bool useNoise = true;

		// Token: 0x0400009E RID: 158
		public Texture3D noiseTexture;

		// Token: 0x0400009F RID: 159
		[Range(0f, 3f)]
		public float noiseStrength = 1f;

		// Token: 0x040000A0 RID: 160
		public float noiseScale = 5f;

		// Token: 0x040000A1 RID: 161
		public float noiseFinalMultiplier = 1f;

		// Token: 0x040000A2 RID: 162
		public float density = 0.2f;

		// Token: 0x040000A3 RID: 163
		public Color mediumAlbedo = Color.white;

		// Token: 0x040000A4 RID: 164
		[Tooltip("Overall brightness multiplier.")]
		public float brightness = 1f;

		// Token: 0x040000A5 RID: 165
		[Tooltip("Attenuation Mode")]
		public AttenuationMode attenuationMode;

		// Token: 0x040000A6 RID: 166
		[Tooltip("Constant coefficient (a) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefConstant = 1f;

		// Token: 0x040000A7 RID: 167
		[Tooltip("Linear coefficient (b) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefLinear = 2f;

		// Token: 0x040000A8 RID: 168
		[Tooltip("Quadratic coefficient (c) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
		public float attenCoefQuadratic = 1f;

		// Token: 0x040000A9 RID: 169
		[Tooltip("Attenuation of light intensity based on square of distance. Plays with brightness to achieve a more linear or realistic (quadratic attenuation effect).")]
		[FormerlySerializedAs("distanceFallOff")]
		public float rangeFallOff = 1f;

		// Token: 0x040000AA RID: 170
		[Tooltip("Brightiness increase when looking against light source.")]
		public float diffusionIntensity;

		// Token: 0x040000AB RID: 171
		[Range(0f, 1f)]
		[Tooltip("Smooth edges")]
		[FormerlySerializedAs("border")]
		public float penumbra = 0.5f;

		// Token: 0x040000AC RID: 172
		[Tooltip("Radius of the tip of the cone. Only applies to spot lights.")]
		public float tipRadius;

		// Token: 0x040000AD RID: 173
		[Tooltip("Custom cookie texture (RGB).")]
		public Texture2D cookieTexture;

		// Token: 0x040000AE RID: 174
		public Vector2 cookieScale = Vector2.one;

		// Token: 0x040000AF RID: 175
		public Vector2 cookieOffset;

		// Token: 0x040000B0 RID: 176
		public Vector2 cookieSpeed;

		// Token: 0x040000B1 RID: 177
		[Range(0f, 80f)]
		public float frustumAngle;

		// Token: 0x040000B2 RID: 178
		[Header("Animation")]
		[Tooltip("Noise animation direction and speed.")]
		public Vector3 windDirection = new Vector3(0.03f, 0.02f, 0f);

		// Token: 0x040000B3 RID: 179
		[Header("Dust Particles")]
		public bool enableDustParticles;

		// Token: 0x040000B4 RID: 180
		public float dustBrightness = 0.6f;

		// Token: 0x040000B5 RID: 181
		public float dustMinSize = 0.01f;

		// Token: 0x040000B6 RID: 182
		public float dustMaxSize = 0.02f;

		// Token: 0x040000B7 RID: 183
		public float dustWindSpeed = 1f;

		// Token: 0x040000B8 RID: 184
		[Tooltip("Dims particle intensity beyond this distance to camera")]
		public float dustDistanceAttenuation = 10f;

		// Token: 0x040000B9 RID: 185
		[Tooltip("Fully enable/disable particle system renderer when far from main camera in order to optimize performance")]
		public bool dustAutoToggle;

		// Token: 0x040000BA RID: 186
		[Tooltip("Distance to the light source at which the particule system is fully deactivated. Note that the distance is to the light position regardless of its light range or volume so you should consider the light area or range into this distance as well.")]
		public float dustDistanceDeactivation = 70f;

		// Token: 0x040000BB RID: 187
		[Tooltip("Prewarms/populates dust when the volumetric light is enabled to ensure there're enough visible particles from start. Disabling this option can improve performance when many lights are activated at the same time.")]
		public bool dustPrewarm = true;

		// Token: 0x040000BC RID: 188
		[Header("Shadow Occlusion")]
		public bool enableShadows;

		// Token: 0x040000BD RID: 189
		public float shadowIntensity = 0.7f;

		// Token: 0x040000BE RID: 190
		[Tooltip("Enable translucent shadow map")]
		public bool shadowTranslucency;

		// Token: 0x040000BF RID: 191
		[Tooltip("Customizable intensity for the translucent map sampling")]
		public float shadowTranslucencyIntensity = 1f;

		// Token: 0x040000C0 RID: 192
		[Tooltip("Amount of colorization")]
		[Range(0f, 1f)]
		public float shadowTranslucencyBlend = 0.5f;

		// Token: 0x040000C1 RID: 193
		public bool updateTranslucency;

		// Token: 0x040000C2 RID: 194
		public ShadowResolution shadowResolution = ShadowResolution._256;

		// Token: 0x040000C3 RID: 195
		public LayerMask shadowCullingMask = -3;

		// Token: 0x040000C4 RID: 196
		public ShadowBakeInterval shadowBakeInterval;

		// Token: 0x040000C5 RID: 197
		public float shadowNearDistance = 0.1f;

		// Token: 0x040000C6 RID: 198
		[Tooltip("Fully enable/disable shadows when far from main camera in order to optimize performance")]
		public bool shadowAutoToggle;

		// Token: 0x040000C7 RID: 199
		[Tooltip("Max distance to main camera to render shadows for this volumetric light.")]
		public float shadowDistanceDeactivation = 250f;

		// Token: 0x040000C8 RID: 200
		[Tooltip("Compute shadows in a half-sphere oriented to camera (faster) or in a cubemap but render one face per frame (slower) or all 6 faces per frame (slowest).")]
		public ShadowBakeMode shadowBakeMode;

		// Token: 0x040000C9 RID: 201
		[Tooltip("Use default render texture format when rendering shadows. Disabling this option will force shadows to render to a single depth buffer, which is faster, but can cause issues with shaders using Grab Pass.")]
		public bool shadowUseDefaultRTFormat;

		// Token: 0x040000CA RID: 202
		[Tooltip("When enabled, a fast shader will be used to render object shadows instead of their regular shaders.")]
		public bool shadowOptimizeShadowCasters = true;

		// Token: 0x040000CB RID: 203
		[Tooltip("Used for point light occlusion orientation.")]
		public ShadowOrientation shadowOrientation;

		// Token: 0x040000CC RID: 204
		[Tooltip("For performance reasons, point light shadows are captured on half a sphere (180º). By default, the shadows are captured in the direction to the user camera but you can specify a fixed direction using this option.")]
		public Vector3 shadowDirection = Vector3.down;

		// Token: 0x040000CD RID: 205
		private const string SHADOW_CAM_NAME = "OcclusionCam";

		// Token: 0x040000CE RID: 206
		private const string m_TranspShader = "Hidden/VolumetricLights/TransparentMultiply";

		// Token: 0x040000CF RID: 207
		private const string m_TranspDepthOnlyShader = "Hidden/VolumetricLights/TransparentDepthWrite";

		// Token: 0x040000D0 RID: 208
		private Camera cam;

		// Token: 0x040000D1 RID: 209
		private RenderTexture rt;

		// Token: 0x040000D2 RID: 210
		private int camStartFrameCount;

		// Token: 0x040000D3 RID: 211
		private Vector3 lastCamPos;

		// Token: 0x040000D4 RID: 212
		private Quaternion lastCamRot;

		// Token: 0x040000D5 RID: 213
		private bool usesReversedZBuffer;

		// Token: 0x040000D6 RID: 214
		private static Matrix4x4 textureScaleAndBias;

		// Token: 0x040000D7 RID: 215
		private Matrix4x4 shadowMatrix;

		// Token: 0x040000D8 RID: 216
		private bool camTransformChanged;

		// Token: 0x040000D9 RID: 217
		private bool shouldOrientToCamera;

		// Token: 0x040000DA RID: 218
		private Shader depthShader;

		// Token: 0x040000DB RID: 219
		private RenderTexture shadowCubemap;

		// Token: 0x040000DC RID: 220
		private static readonly Vector3[] camFaceDirections = new Vector3[]
		{
			Vector3.right,
			Vector3.left,
			Vector3.up,
			Vector3.down,
			Vector3.forward,
			Vector3.back
		};

		// Token: 0x040000DD RID: 221
		private Material copyDepthIntoCubemap;

		// Token: 0x040000DE RID: 222
		private int currentCubemapFace;

		// Token: 0x040000DF RID: 223
		private Material transpOverrideMat;

		// Token: 0x040000E0 RID: 224
		private Material transpDepthOnlyMat;

		// Token: 0x040000E1 RID: 225
		private Material[] transpOverrideMaterials;

		// Token: 0x040000E2 RID: 226
		private RenderTexture translucentMap;

		// Token: 0x040000E3 RID: 227
		private CommandBuffer cmdTransp;

		// Token: 0x02000192 RID: 402
		private static class ShaderParams
		{
			// Token: 0x06000EB2 RID: 3762 RVA: 0x0005F640 File Offset: 0x0005D840
			// Note: this type is marked as 'beforefieldinit'.
			static ShaderParams()
			{
			}

			// Token: 0x04000C61 RID: 3169
			public static int RayMarchSettings = Shader.PropertyToID("_RayMarchSettings");

			// Token: 0x04000C62 RID: 3170
			public static int RayMarchMaxSteps = Shader.PropertyToID("_RayMarchMaxSteps");

			// Token: 0x04000C63 RID: 3171
			public static int Density = Shader.PropertyToID("_Density");

			// Token: 0x04000C64 RID: 3172
			public static int FallOff = Shader.PropertyToID("_FallOff");

			// Token: 0x04000C65 RID: 3173
			public static int RangeFallOff = Shader.PropertyToID("_DistanceFallOff");

			// Token: 0x04000C66 RID: 3174
			public static int Penumbra = Shader.PropertyToID("_Border");

			// Token: 0x04000C67 RID: 3175
			public static int DirectLightData = Shader.PropertyToID("_DirectLightData");

			// Token: 0x04000C68 RID: 3176
			public static int NoiseFinalMultiplier = Shader.PropertyToID("_NoiseFinalMultiplier");

			// Token: 0x04000C69 RID: 3177
			public static int NoiseScale = Shader.PropertyToID("_NoiseScale");

			// Token: 0x04000C6A RID: 3178
			public static int NoiseStrength = Shader.PropertyToID("_NoiseStrength");

			// Token: 0x04000C6B RID: 3179
			public static int NoiseTex = Shader.PropertyToID("_NoiseTex");

			// Token: 0x04000C6C RID: 3180
			public static int BlendDest = Shader.PropertyToID("_BlendDest");

			// Token: 0x04000C6D RID: 3181
			public static int BlendSrc = Shader.PropertyToID("_BlendSrc");

			// Token: 0x04000C6E RID: 3182
			public static int BlendOp = Shader.PropertyToID("_BlendOp");

			// Token: 0x04000C6F RID: 3183
			public static int AreaExtents = Shader.PropertyToID("_AreaExtents");

			// Token: 0x04000C70 RID: 3184
			public static int BoundsExtents = Shader.PropertyToID("_BoundsExtents");

			// Token: 0x04000C71 RID: 3185
			public static int BoundsCenter = Shader.PropertyToID("_BoundsCenter");

			// Token: 0x04000C72 RID: 3186
			public static int MeshBoundsExtents = Shader.PropertyToID("_MeshBoundsExtents");

			// Token: 0x04000C73 RID: 3187
			public static int MeshBoundsCenter = Shader.PropertyToID("_MeshBoundsCenter");

			// Token: 0x04000C74 RID: 3188
			public static int ExtraGeoData = Shader.PropertyToID("_ExtraGeoData");

			// Token: 0x04000C75 RID: 3189
			public static int ConeAxis = Shader.PropertyToID("_ConeAxis");

			// Token: 0x04000C76 RID: 3190
			public static int ConeTipData = Shader.PropertyToID("_ConeTipData");

			// Token: 0x04000C77 RID: 3191
			public static int WorldToLocalMatrix = Shader.PropertyToID("_WorldToLocal");

			// Token: 0x04000C78 RID: 3192
			public static int ToLightDir = Shader.PropertyToID("_ToLightDir");

			// Token: 0x04000C79 RID: 3193
			public static int WindDirection = Shader.PropertyToID("_WindDirection");

			// Token: 0x04000C7A RID: 3194
			public static int LightColor = Shader.PropertyToID("_LightColor");

			// Token: 0x04000C7B RID: 3195
			public static int ParticleTintColor = Shader.PropertyToID("_ParticleTintColor");

			// Token: 0x04000C7C RID: 3196
			public static int ParticleDistanceAtten = Shader.PropertyToID("_ParticleDistanceAtten");

			// Token: 0x04000C7D RID: 3197
			public static int CookieTexture = Shader.PropertyToID("_Cookie2D");

			// Token: 0x04000C7E RID: 3198
			public static int CookieTexture_ScaleAndSpeed = Shader.PropertyToID("_Cookie2D_SS");

			// Token: 0x04000C7F RID: 3199
			public static int CookieTexture_Offset = Shader.PropertyToID("_Cookie2D_Offset");

			// Token: 0x04000C80 RID: 3200
			public static int BlueNoiseTexture = Shader.PropertyToID("_BlueNoise");

			// Token: 0x04000C81 RID: 3201
			public static int ShadowTexture = Shader.PropertyToID("_ShadowTexture");

			// Token: 0x04000C82 RID: 3202
			public static int ShadowCubemap = Shader.PropertyToID("_ShadowCubemap");

			// Token: 0x04000C83 RID: 3203
			public static int ShadowIntensity = Shader.PropertyToID("_ShadowIntensity");

			// Token: 0x04000C84 RID: 3204
			public static int TranslucencyTexture = Shader.PropertyToID("_TranslucencyTexture");

			// Token: 0x04000C85 RID: 3205
			public static int ShadowMatrix = Shader.PropertyToID("_ShadowMatrix");

			// Token: 0x04000C86 RID: 3206
			public static int LightPos = Shader.PropertyToID("_LightPos");

			// Token: 0x04000C87 RID: 3207
			public static int InvVPMatrix = Shader.PropertyToID("_I_VP_Matrix");

			// Token: 0x04000C88 RID: 3208
			public static int MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04000C89 RID: 3209
			public static int TranslucencyIntensity = Shader.PropertyToID("_TranslucencyIntensity");

			// Token: 0x04000C8A RID: 3210
			public static int MainTex_ST = Shader.PropertyToID("_MainTex_ST");

			// Token: 0x04000C8B RID: 3211
			public static int Color = Shader.PropertyToID("_Color");

			// Token: 0x04000C8C RID: 3212
			public const string SKW_NOISE = "VL_NOISE";

			// Token: 0x04000C8D RID: 3213
			public const string SKW_BLUENOISE = "VL_BLUENOISE";

			// Token: 0x04000C8E RID: 3214
			public const string SKW_SPOT = "VL_SPOT";

			// Token: 0x04000C8F RID: 3215
			public const string SKW_SPOT_COOKIE = "VL_SPOT_COOKIE";

			// Token: 0x04000C90 RID: 3216
			public const string SKW_POINT = "VL_POINT";

			// Token: 0x04000C91 RID: 3217
			public const string SKW_AREA_RECT = "VL_AREA_RECT";

			// Token: 0x04000C92 RID: 3218
			public const string SKW_AREA_DISC = "VL_AREA_DISC";

			// Token: 0x04000C93 RID: 3219
			public const string SKW_SHADOWS = "VL_SHADOWS";

			// Token: 0x04000C94 RID: 3220
			public const string SKW_SHADOWS_TRANSLUCENCY = "VL_SHADOWS_TRANSLUCENCY";

			// Token: 0x04000C95 RID: 3221
			public const string SKW_SHADOWS_CUBEMAP = "VL_SHADOWS_CUBEMAP";

			// Token: 0x04000C96 RID: 3222
			public const string SKW_CAST_DIRECT_LIGHT_ADDITIVE = "VL_CAST_DIRECT_LIGHT_ADDITIVE";

			// Token: 0x04000C97 RID: 3223
			public const string SKW_CAST_DIRECT_LIGHT_BLEND = "VL_CAST_DIRECT_LIGHT_BLEND";

			// Token: 0x04000C98 RID: 3224
			public const string SKW_PHYSICAL_ATTEN = "VL_PHYSICAL_ATTEN";

			// Token: 0x04000C99 RID: 3225
			public const string SKW_CUSTOM_BOUNDS = "VL_CUSTOM_BOUNDS";
		}
	}
}
