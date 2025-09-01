using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000148 RID: 328
	[NativeHeader("Runtime/Graphics/QualitySettingsTypes.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	public sealed class RenderSettings : Object
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00010730 File Offset: 0x0000E930
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00010747 File Offset: 0x0000E947
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return RenderSettings.ambientIntensity;
			}
			set
			{
				RenderSettings.ambientIntensity = value;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0000E886 File Offset: 0x0000CA86
		private RenderSettings()
		{
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C0C RID: 3084
		// (set) Token: 0x06000C0D RID: 3085
		[NativeProperty("UseFog")]
		public static extern bool fog { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C0E RID: 3086
		// (set) Token: 0x06000C0F RID: 3087
		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000C10 RID: 3088
		// (set) Token: 0x06000C11 RID: 3089
		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C12 RID: 3090
		// (set) Token: 0x06000C13 RID: 3091
		public static extern FogMode fogMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00010754 File Offset: 0x0000E954
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00010769 File Offset: 0x0000E969
		public static Color fogColor
		{
			get
			{
				Color result;
				RenderSettings.get_fogColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_fogColor_Injected(ref value);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C16 RID: 3094
		// (set) Token: 0x06000C17 RID: 3095
		public static extern float fogDensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C18 RID: 3096
		// (set) Token: 0x06000C19 RID: 3097
		public static extern AmbientMode ambientMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00010774 File Offset: 0x0000E974
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00010789 File Offset: 0x0000E989
		public static Color ambientSkyColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientSkyColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientSkyColor_Injected(ref value);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00010794 File Offset: 0x0000E994
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000107A9 File Offset: 0x0000E9A9
		public static Color ambientEquatorColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientEquatorColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientEquatorColor_Injected(ref value);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000107B4 File Offset: 0x0000E9B4
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000107C9 File Offset: 0x0000E9C9
		public static Color ambientGroundColor
		{
			get
			{
				Color result;
				RenderSettings.get_ambientGroundColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientGroundColor_Injected(ref value);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000C20 RID: 3104
		// (set) Token: 0x06000C21 RID: 3105
		public static extern float ambientIntensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000107D4 File Offset: 0x0000E9D4
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x000107E9 File Offset: 0x0000E9E9
		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				Color result;
				RenderSettings.get_ambientLight_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientLight_Injected(ref value);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000107F4 File Offset: 0x0000E9F4
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00010809 File Offset: 0x0000EA09
		public static Color subtractiveShadowColor
		{
			get
			{
				Color result;
				RenderSettings.get_subtractiveShadowColor_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_subtractiveShadowColor_Injected(ref value);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000C26 RID: 3110
		// (set) Token: 0x06000C27 RID: 3111
		[NativeProperty("SkyboxMaterial")]
		public static extern Material skybox { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000C28 RID: 3112
		// (set) Token: 0x06000C29 RID: 3113
		public static extern Light sun { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00010814 File Offset: 0x0000EA14
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00010829 File Offset: 0x0000EA29
		public static SphericalHarmonicsL2 ambientProbe
		{
			[NativeMethod("GetFinalAmbientProbe")]
			get
			{
				SphericalHarmonicsL2 result;
				RenderSettings.get_ambientProbe_Injected(out result);
				return result;
			}
			set
			{
				RenderSettings.set_ambientProbe_Injected(ref value);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000C2C RID: 3116
		// (set) Token: 0x06000C2D RID: 3117
		public static extern Texture customReflection { [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeThrows] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000C2E RID: 3118
		// (set) Token: 0x06000C2F RID: 3119
		public static extern float reflectionIntensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000C30 RID: 3120
		// (set) Token: 0x06000C31 RID: 3121
		public static extern int reflectionBounces { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000C32 RID: 3122
		[NativeProperty("GeneratedSkyboxReflection")]
		internal static extern Cubemap defaultReflection { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000C33 RID: 3123
		// (set) Token: 0x06000C34 RID: 3124
		public static extern DefaultReflectionMode defaultReflectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000C35 RID: 3125
		// (set) Token: 0x06000C36 RID: 3126
		public static extern int defaultReflectionResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000C37 RID: 3127
		// (set) Token: 0x06000C38 RID: 3128
		public static extern float haloStrength { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000C39 RID: 3129
		// (set) Token: 0x06000C3A RID: 3130
		public static extern float flareStrength { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C3B RID: 3131
		// (set) Token: 0x06000C3C RID: 3132
		public static extern float flareFadeSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000C3D RID: 3133
		[FreeFunction("GetRenderSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object GetRenderSettings();

		// Token: 0x06000C3E RID: 3134
		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x06000C3F RID: 3135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_fogColor_Injected(out Color ret);

		// Token: 0x06000C40 RID: 3136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fogColor_Injected(ref Color value);

		// Token: 0x06000C41 RID: 3137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		// Token: 0x06000C42 RID: 3138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientSkyColor_Injected(ref Color value);

		// Token: 0x06000C43 RID: 3139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		// Token: 0x06000C44 RID: 3140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientEquatorColor_Injected(ref Color value);

		// Token: 0x06000C45 RID: 3141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		// Token: 0x06000C46 RID: 3142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientGroundColor_Injected(ref Color value);

		// Token: 0x06000C47 RID: 3143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientLight_Injected(out Color ret);

		// Token: 0x06000C48 RID: 3144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientLight_Injected(ref Color value);

		// Token: 0x06000C49 RID: 3145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		// Token: 0x06000C4A RID: 3146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subtractiveShadowColor_Injected(ref Color value);

		// Token: 0x06000C4B RID: 3147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		// Token: 0x06000C4C RID: 3148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ambientProbe_Injected(ref SphericalHarmonicsL2 value);
	}
}
