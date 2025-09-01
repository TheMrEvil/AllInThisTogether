using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000156 RID: 342
	[NativeHeader("Runtime/Export/Graphics/Light.bindings.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Camera/Light.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Light : Behaviour
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E42 RID: 3650
		// (set) Token: 0x06000E43 RID: 3651
		[NativeProperty("LightType")]
		public extern LightType type { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E44 RID: 3652
		// (set) Token: 0x06000E45 RID: 3653
		[NativeProperty("LightShape")]
		public extern LightShape shape { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E46 RID: 3654
		// (set) Token: 0x06000E47 RID: 3655
		public extern float spotAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E48 RID: 3656
		// (set) Token: 0x06000E49 RID: 3657
		public extern float innerSpotAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00012B9C File Offset: 0x00010D9C
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x00012BB2 File Offset: 0x00010DB2
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E4C RID: 3660
		// (set) Token: 0x06000E4D RID: 3661
		public extern float colorTemperature { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E4E RID: 3662
		// (set) Token: 0x06000E4F RID: 3663
		public extern bool useColorTemperature { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E50 RID: 3664
		// (set) Token: 0x06000E51 RID: 3665
		public extern float intensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E52 RID: 3666
		// (set) Token: 0x06000E53 RID: 3667
		public extern float bounceIntensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E54 RID: 3668
		// (set) Token: 0x06000E55 RID: 3669
		public extern bool useBoundingSphereOverride { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00012BBC File Offset: 0x00010DBC
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00012BD2 File Offset: 0x00010DD2
		public Vector4 boundingSphereOverride
		{
			get
			{
				Vector4 result;
				this.get_boundingSphereOverride_Injected(out result);
				return result;
			}
			set
			{
				this.set_boundingSphereOverride_Injected(ref value);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E58 RID: 3672
		// (set) Token: 0x06000E59 RID: 3673
		public extern bool useViewFrustumForShadowCasterCull { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E5A RID: 3674
		// (set) Token: 0x06000E5B RID: 3675
		public extern int shadowCustomResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E5C RID: 3676
		// (set) Token: 0x06000E5D RID: 3677
		public extern float shadowBias { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E5E RID: 3678
		// (set) Token: 0x06000E5F RID: 3679
		public extern float shadowNormalBias { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E60 RID: 3680
		// (set) Token: 0x06000E61 RID: 3681
		public extern float shadowNearPlane { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E62 RID: 3682
		// (set) Token: 0x06000E63 RID: 3683
		public extern bool useShadowMatrixOverride { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00012BDC File Offset: 0x00010DDC
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00012BF2 File Offset: 0x00010DF2
		public Matrix4x4 shadowMatrixOverride
		{
			get
			{
				Matrix4x4 result;
				this.get_shadowMatrixOverride_Injected(out result);
				return result;
			}
			set
			{
				this.set_shadowMatrixOverride_Injected(ref value);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E66 RID: 3686
		// (set) Token: 0x06000E67 RID: 3687
		public extern float range { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E68 RID: 3688
		// (set) Token: 0x06000E69 RID: 3689
		public extern Flare flare { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00012BFC File Offset: 0x00010DFC
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00012C12 File Offset: 0x00010E12
		public LightBakingOutput bakingOutput
		{
			get
			{
				LightBakingOutput result;
				this.get_bakingOutput_Injected(out result);
				return result;
			}
			set
			{
				this.set_bakingOutput_Injected(ref value);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E6C RID: 3692
		// (set) Token: 0x06000E6D RID: 3693
		public extern int cullingMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E6E RID: 3694
		// (set) Token: 0x06000E6F RID: 3695
		public extern int renderingLayerMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E70 RID: 3696
		// (set) Token: 0x06000E71 RID: 3697
		public extern LightShadowCasterMode lightShadowCasterMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E72 RID: 3698
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E73 RID: 3699
		// (set) Token: 0x06000E74 RID: 3700
		public extern LightShadows shadows { [NativeMethod("GetShadowType")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E75 RID: 3701
		// (set) Token: 0x06000E76 RID: 3702
		public extern float shadowStrength { [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E77 RID: 3703
		// (set) Token: 0x06000E78 RID: 3704
		public extern LightShadowResolution shadowResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00012C1C File Offset: 0x00010E1C
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00004563 File Offset: 0x00002763
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00012C34 File Offset: 0x00010E34
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E7D RID: 3709
		// (set) Token: 0x06000E7E RID: 3710
		public extern float[] layerShadowCullDistances { [FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E7F RID: 3711
		// (set) Token: 0x06000E80 RID: 3712
		public extern float cookieSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E81 RID: 3713
		// (set) Token: 0x06000E82 RID: 3714
		public extern Texture cookie { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E83 RID: 3715
		// (set) Token: 0x06000E84 RID: 3716
		public extern LightRenderMode renderMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00012C4C File Offset: 0x00010E4C
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00012C64 File Offset: 0x00010E64
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		public int bakedIndex
		{
			get
			{
				return this.m_BakedIndex;
			}
			set
			{
				this.m_BakedIndex = value;
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00012C6E File Offset: 0x00010E6E
		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			this.AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		// Token: 0x06000E88 RID: 3720
		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask);

		// Token: 0x06000E89 RID: 3721 RVA: 0x00012C7F File Offset: 0x00010E7F
		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			this.AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		// Token: 0x06000E8A RID: 3722
		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		// Token: 0x06000E8B RID: 3723
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

		// Token: 0x06000E8C RID: 3724
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(LightEvent evt);

		// Token: 0x06000E8D RID: 3725
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x06000E8E RID: 3726
		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E8F RID: 3727
		public extern int commandBufferCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00012C94 File Offset: 0x00010E94
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x00012CAB File Offset: 0x00010EAB
		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		// Token: 0x06000E92 RID: 3730
		[FreeFunction("Light_Bindings::GetLights")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00012CB8 File Offset: 0x00010EB8
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00012CD0 File Offset: 0x00010ED0
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00012CE8 File Offset: 0x00010EE8
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000084C0 File Offset: 0x000066C0
		public Light()
		{
		}

		// Token: 0x06000E9A RID: 3738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000E9B RID: 3739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06000E9C RID: 3740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_boundingSphereOverride_Injected(out Vector4 ret);

		// Token: 0x06000E9D RID: 3741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_boundingSphereOverride_Injected(ref Vector4 value);

		// Token: 0x06000E9E RID: 3742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_shadowMatrixOverride_Injected(out Matrix4x4 ret);

		// Token: 0x06000E9F RID: 3743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_shadowMatrixOverride_Injected(ref Matrix4x4 value);

		// Token: 0x06000EA0 RID: 3744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bakingOutput_Injected(out LightBakingOutput ret);

		// Token: 0x06000EA1 RID: 3745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bakingOutput_Injected(ref LightBakingOutput value);

		// Token: 0x04000437 RID: 1079
		private int m_BakedIndex;
	}
}
