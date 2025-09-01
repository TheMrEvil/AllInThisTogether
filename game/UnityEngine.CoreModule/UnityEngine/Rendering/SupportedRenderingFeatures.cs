using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000419 RID: 1049
	public class SupportedRenderingFeatures
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0003D778 File Offset: 0x0003B978
		// (set) Token: 0x06002443 RID: 9283 RVA: 0x0003D7A5 File Offset: 0x0003B9A5
		public static SupportedRenderingFeatures active
		{
			get
			{
				bool flag = SupportedRenderingFeatures.s_Active == null;
				if (flag)
				{
					SupportedRenderingFeatures.s_Active = new SupportedRenderingFeatures();
				}
				return SupportedRenderingFeatures.s_Active;
			}
			set
			{
				SupportedRenderingFeatures.s_Active = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x0003D7AE File Offset: 0x0003B9AE
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x0003D7B6 File Offset: 0x0003B9B6
		public SupportedRenderingFeatures.ReflectionProbeModes reflectionProbeModes
		{
			[CompilerGenerated]
			get
			{
				return this.<reflectionProbeModes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reflectionProbeModes>k__BackingField = value;
			}
		} = SupportedRenderingFeatures.ReflectionProbeModes.None;

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x0003D7BF File Offset: 0x0003B9BF
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x0003D7C7 File Offset: 0x0003B9C7
		public SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes
		{
			[CompilerGenerated]
			get
			{
				return this.<defaultMixedLightingModes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<defaultMixedLightingModes>k__BackingField = value;
			}
		} = SupportedRenderingFeatures.LightmapMixedBakeModes.None;

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x0003D7D0 File Offset: 0x0003B9D0
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		public SupportedRenderingFeatures.LightmapMixedBakeModes mixedLightingModes
		{
			[CompilerGenerated]
			get
			{
				return this.<mixedLightingModes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<mixedLightingModes>k__BackingField = value;
			}
		} = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask;

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0003D7E1 File Offset: 0x0003B9E1
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x0003D7E9 File Offset: 0x0003B9E9
		public LightmapBakeType lightmapBakeTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<lightmapBakeTypes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lightmapBakeTypes>k__BackingField = value;
			}
		} = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0003D7F2 File Offset: 0x0003B9F2
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x0003D7FA File Offset: 0x0003B9FA
		public LightmapsMode lightmapsModes
		{
			[CompilerGenerated]
			get
			{
				return this.<lightmapsModes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lightmapsModes>k__BackingField = value;
			}
		} = LightmapsMode.CombinedDirectional;

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0003D803 File Offset: 0x0003BA03
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x0003D80B File Offset: 0x0003BA0B
		public bool enlightenLightmapper
		{
			[CompilerGenerated]
			get
			{
				return this.<enlightenLightmapper>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<enlightenLightmapper>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0003D814 File Offset: 0x0003BA14
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0003D81C File Offset: 0x0003BA1C
		public bool enlighten
		{
			[CompilerGenerated]
			get
			{
				return this.<enlighten>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<enlighten>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0003D825 File Offset: 0x0003BA25
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0003D82D File Offset: 0x0003BA2D
		public bool lightProbeProxyVolumes
		{
			[CompilerGenerated]
			get
			{
				return this.<lightProbeProxyVolumes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lightProbeProxyVolumes>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0003D836 File Offset: 0x0003BA36
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0003D83E File Offset: 0x0003BA3E
		public bool motionVectors
		{
			[CompilerGenerated]
			get
			{
				return this.<motionVectors>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<motionVectors>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x0003D847 File Offset: 0x0003BA47
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x0003D84F File Offset: 0x0003BA4F
		public bool receiveShadows
		{
			[CompilerGenerated]
			get
			{
				return this.<receiveShadows>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<receiveShadows>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0003D858 File Offset: 0x0003BA58
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x0003D860 File Offset: 0x0003BA60
		public bool reflectionProbes
		{
			[CompilerGenerated]
			get
			{
				return this.<reflectionProbes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reflectionProbes>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0003D869 File Offset: 0x0003BA69
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x0003D871 File Offset: 0x0003BA71
		public bool reflectionProbesBlendDistance
		{
			[CompilerGenerated]
			get
			{
				return this.<reflectionProbesBlendDistance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reflectionProbesBlendDistance>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0003D87A File Offset: 0x0003BA7A
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x0003D882 File Offset: 0x0003BA82
		public bool rendererPriority
		{
			[CompilerGenerated]
			get
			{
				return this.<rendererPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rendererPriority>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x0003D88B File Offset: 0x0003BA8B
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0003D893 File Offset: 0x0003BA93
		public bool rendersUIOverlay
		{
			[CompilerGenerated]
			get
			{
				return this.<rendersUIOverlay>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rendersUIOverlay>k__BackingField = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x0003D89C File Offset: 0x0003BA9C
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		public bool overridesEnvironmentLighting
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesEnvironmentLighting>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesEnvironmentLighting>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0003D8AD File Offset: 0x0003BAAD
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x0003D8B5 File Offset: 0x0003BAB5
		public bool overridesFog
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesFog>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesFog>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x0003D8BE File Offset: 0x0003BABE
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x0003D8C6 File Offset: 0x0003BAC6
		public bool overridesRealtimeReflectionProbes
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesRealtimeReflectionProbes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesRealtimeReflectionProbes>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0003D8CF File Offset: 0x0003BACF
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0003D8D7 File Offset: 0x0003BAD7
		public bool overridesOtherLightingSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesOtherLightingSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesOtherLightingSettings>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
		public bool editableMaterialRenderQueue
		{
			[CompilerGenerated]
			get
			{
				return this.<editableMaterialRenderQueue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<editableMaterialRenderQueue>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0003D8F1 File Offset: 0x0003BAF1
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x0003D8F9 File Offset: 0x0003BAF9
		public bool overridesLODBias
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesLODBias>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesLODBias>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0003D902 File Offset: 0x0003BB02
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x0003D90A File Offset: 0x0003BB0A
		public bool overridesMaximumLODLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesMaximumLODLevel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesMaximumLODLevel>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0003D913 File Offset: 0x0003BB13
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x0003D91B File Offset: 0x0003BB1B
		public bool rendererProbes
		{
			[CompilerGenerated]
			get
			{
				return this.<rendererProbes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rendererProbes>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x0003D924 File Offset: 0x0003BB24
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x0003D92C File Offset: 0x0003BB2C
		public bool particleSystemInstancing
		{
			[CompilerGenerated]
			get
			{
				return this.<particleSystemInstancing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<particleSystemInstancing>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x0003D935 File Offset: 0x0003BB35
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x0003D93D File Offset: 0x0003BB3D
		public bool autoAmbientProbeBaking
		{
			[CompilerGenerated]
			get
			{
				return this.<autoAmbientProbeBaking>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<autoAmbientProbeBaking>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x0003D946 File Offset: 0x0003BB46
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x0003D94E File Offset: 0x0003BB4E
		public bool autoDefaultReflectionProbeBaking
		{
			[CompilerGenerated]
			get
			{
				return this.<autoDefaultReflectionProbeBaking>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<autoDefaultReflectionProbeBaking>k__BackingField = value;
			}
		} = true;

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x0003D957 File Offset: 0x0003BB57
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x0003D95F File Offset: 0x0003BB5F
		public bool overridesShadowmask
		{
			[CompilerGenerated]
			get
			{
				return this.<overridesShadowmask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overridesShadowmask>k__BackingField = value;
			}
		} = false;

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x0003D968 File Offset: 0x0003BB68
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x0003D970 File Offset: 0x0003BB70
		public string overrideShadowmaskMessage
		{
			[CompilerGenerated]
			get
			{
				return this.<overrideShadowmaskMessage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<overrideShadowmaskMessage>k__BackingField = value;
			}
		} = "";

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x0003D97C File Offset: 0x0003BB7C
		public string shadowmaskMessage
		{
			get
			{
				bool flag = !this.overridesShadowmask;
				string result;
				if (flag)
				{
					result = "The Shadowmask Mode used at run time can be set in the Quality Settings panel.";
				}
				else
				{
					result = this.overrideShadowmaskMessage;
				}
				return result;
			}
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0003D9AC File Offset: 0x0003BBAC
		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode result;
			SupportedRenderingFeatures.FallbackMixedLightingModeByRef(new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0003D9D0 File Offset: 0x0003BBD0
		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)((void*)fallbackModePtr);
			bool flag = SupportedRenderingFeatures.active.defaultMixedLightingModes != SupportedRenderingFeatures.LightmapMixedBakeModes.None && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.active.defaultMixedLightingModes) == SupportedRenderingFeatures.active.defaultMixedLightingModes;
			if (flag)
			{
				SupportedRenderingFeatures.LightmapMixedBakeModes defaultMixedLightingModes = SupportedRenderingFeatures.active.defaultMixedLightingModes;
				SupportedRenderingFeatures.LightmapMixedBakeModes lightmapMixedBakeModes = defaultMixedLightingModes;
				if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive)
				{
					if (lightmapMixedBakeModes != SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask)
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
					else
					{
						*ptr = MixedLightingMode.Shadowmask;
					}
				}
				else
				{
					*ptr = MixedLightingMode.Subtractive;
				}
			}
			else
			{
				bool flag2 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Shadowmask);
				if (flag2)
				{
					*ptr = MixedLightingMode.Shadowmask;
				}
				else
				{
					bool flag3 = SupportedRenderingFeatures.IsMixedLightingModeSupported(MixedLightingMode.Subtractive);
					if (flag3)
					{
						*ptr = MixedLightingMode.Subtractive;
					}
					else
					{
						*ptr = MixedLightingMode.IndirectOnly;
					}
				}
			}
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0003DA6C File Offset: 0x0003BC6C
		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool result;
			SupportedRenderingFeatures.IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0003DA90 File Offset: 0x0003BC90
		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			bool flag = !SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Mixed);
			if (flag)
			{
				*ptr = false;
			}
			else
			{
				*ptr = ((mixedMode == MixedLightingMode.IndirectOnly && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) == SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) == SupportedRenderingFeatures.LightmapMixedBakeModes.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (SupportedRenderingFeatures.active.mixedLightingModes & SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask) == SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask));
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0003DB1C File Offset: 0x0003BD1C
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			bool flag = bakeType == LightmapBakeType.Mixed;
			if (flag)
			{
				bool flag2 = SupportedRenderingFeatures.IsLightmapBakeTypeSupported(LightmapBakeType.Baked);
				bool flag3 = !flag2 || SupportedRenderingFeatures.active.mixedLightingModes == SupportedRenderingFeatures.LightmapMixedBakeModes.None;
				if (flag3)
				{
					*ptr = false;
					return;
				}
			}
			*ptr = ((SupportedRenderingFeatures.active.lightmapBakeTypes & bakeType) == bakeType);
			bool flag4 = bakeType == LightmapBakeType.Realtime && !SupportedRenderingFeatures.active.enlighten;
			if (flag4)
			{
				*ptr = false;
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0003DB90 File Offset: 0x0003BD90
		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapsModeSupportedByRef(mode, new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = ((SupportedRenderingFeatures.active.lightmapsModes & mode) == mode);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0003DBDC File Offset: 0x0003BDDC
		internal unsafe static bool IsLightmapperSupported(int lightmapper)
		{
			bool result;
			SupportedRenderingFeatures.IsLightmapperSupportedByRef(lightmapper, new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0003DC00 File Offset: 0x0003BE00
		[RequiredByNativeCode]
		internal unsafe static void IsLightmapperSupportedByRef(int lightmapper, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = (lightmapper != 0 || SupportedRenderingFeatures.active.enlightenLightmapper);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0003DC28 File Offset: 0x0003BE28
		[RequiredByNativeCode]
		internal unsafe static void IsUIOverlayRenderedBySRP(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = SupportedRenderingFeatures.active.rendersUIOverlay;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0003DC4C File Offset: 0x0003BE4C
		[RequiredByNativeCode]
		internal unsafe static void IsAutoAmbientProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = SupportedRenderingFeatures.active.autoAmbientProbeBaking;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0003DC70 File Offset: 0x0003BE70
		[RequiredByNativeCode]
		internal unsafe static void IsAutoDefaultReflectionProbeBakingSupported(IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)((void*)isSupportedPtr);
			*ptr = SupportedRenderingFeatures.active.autoDefaultReflectionProbeBaking;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0003DC94 File Offset: 0x0003BE94
		internal unsafe static int FallbackLightmapper()
		{
			int result;
			SupportedRenderingFeatures.FallbackLightmapperByRef(new IntPtr((void*)(&result)));
			return result;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		[RequiredByNativeCode]
		internal unsafe static void FallbackLightmapperByRef(IntPtr lightmapperPtr)
		{
			int* ptr = (int*)((void*)lightmapperPtr);
			*ptr = 1;
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("terrainDetailUnsupported is deprecated.")]
		public bool terrainDetailUnsupported
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
		public SupportedRenderingFeatures()
		{
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0003DDB2 File Offset: 0x0003BFB2
		// Note: this type is marked as 'beforefieldinit'.
		static SupportedRenderingFeatures()
		{
		}

		// Token: 0x04000D6C RID: 3436
		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		// Token: 0x04000D6D RID: 3437
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SupportedRenderingFeatures.ReflectionProbeModes <reflectionProbeModes>k__BackingField;

		// Token: 0x04000D6E RID: 3438
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private SupportedRenderingFeatures.LightmapMixedBakeModes <defaultMixedLightingModes>k__BackingField;

		// Token: 0x04000D6F RID: 3439
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SupportedRenderingFeatures.LightmapMixedBakeModes <mixedLightingModes>k__BackingField;

		// Token: 0x04000D70 RID: 3440
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private LightmapBakeType <lightmapBakeTypes>k__BackingField;

		// Token: 0x04000D71 RID: 3441
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private LightmapsMode <lightmapsModes>k__BackingField;

		// Token: 0x04000D72 RID: 3442
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <enlightenLightmapper>k__BackingField;

		// Token: 0x04000D73 RID: 3443
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <enlighten>k__BackingField;

		// Token: 0x04000D74 RID: 3444
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <lightProbeProxyVolumes>k__BackingField;

		// Token: 0x04000D75 RID: 3445
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <motionVectors>k__BackingField;

		// Token: 0x04000D76 RID: 3446
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <receiveShadows>k__BackingField;

		// Token: 0x04000D77 RID: 3447
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <reflectionProbes>k__BackingField;

		// Token: 0x04000D78 RID: 3448
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <reflectionProbesBlendDistance>k__BackingField;

		// Token: 0x04000D79 RID: 3449
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <rendererPriority>k__BackingField;

		// Token: 0x04000D7A RID: 3450
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <rendersUIOverlay>k__BackingField;

		// Token: 0x04000D7B RID: 3451
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <overridesEnvironmentLighting>k__BackingField;

		// Token: 0x04000D7C RID: 3452
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <overridesFog>k__BackingField;

		// Token: 0x04000D7D RID: 3453
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <overridesRealtimeReflectionProbes>k__BackingField;

		// Token: 0x04000D7E RID: 3454
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <overridesOtherLightingSettings>k__BackingField;

		// Token: 0x04000D7F RID: 3455
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <editableMaterialRenderQueue>k__BackingField;

		// Token: 0x04000D80 RID: 3456
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <overridesLODBias>k__BackingField;

		// Token: 0x04000D81 RID: 3457
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <overridesMaximumLODLevel>k__BackingField;

		// Token: 0x04000D82 RID: 3458
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <rendererProbes>k__BackingField;

		// Token: 0x04000D83 RID: 3459
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <particleSystemInstancing>k__BackingField;

		// Token: 0x04000D84 RID: 3460
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <autoAmbientProbeBaking>k__BackingField;

		// Token: 0x04000D85 RID: 3461
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <autoDefaultReflectionProbeBaking>k__BackingField;

		// Token: 0x04000D86 RID: 3462
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <overridesShadowmask>k__BackingField;

		// Token: 0x04000D87 RID: 3463
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <overrideShadowmaskMessage>k__BackingField;

		// Token: 0x0200041A RID: 1050
		[Flags]
		public enum ReflectionProbeModes
		{
			// Token: 0x04000D89 RID: 3465
			None = 0,
			// Token: 0x04000D8A RID: 3466
			Rotation = 1
		}

		// Token: 0x0200041B RID: 1051
		[Flags]
		public enum LightmapMixedBakeModes
		{
			// Token: 0x04000D8C RID: 3468
			None = 0,
			// Token: 0x04000D8D RID: 3469
			IndirectOnly = 1,
			// Token: 0x04000D8E RID: 3470
			Subtractive = 2,
			// Token: 0x04000D8F RID: 3471
			Shadowmask = 4
		}
	}
}
