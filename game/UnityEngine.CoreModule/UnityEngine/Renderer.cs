using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000147 RID: 327
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/Renderer.h")]
	public class Renderer : Component
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00010414 File Offset: 0x0000E614
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0001042F File Offset: 0x0000E62F
		[Obsolete("Use shadowCastingMode instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool castShadows
		{
			get
			{
				return this.shadowCastingMode > ShadowCastingMode.Off;
			}
			set
			{
				this.shadowCastingMode = (value ? ShadowCastingMode.On : ShadowCastingMode.Off);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00010440 File Offset: 0x0000E640
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0001045B File Offset: 0x0000E65B
		[Obsolete("Use motionVectorGenerationMode instead.", false)]
		public bool motionVectors
		{
			get
			{
				return this.motionVectorGenerationMode == MotionVectorGenerationMode.Object;
			}
			set
			{
				this.motionVectorGenerationMode = (value ? MotionVectorGenerationMode.Object : MotionVectorGenerationMode.Camera);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001046C File Offset: 0x0000E66C
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00010487 File Offset: 0x0000E687
		[Obsolete("Use lightProbeUsage instead.", false)]
		public bool useLightProbes
		{
			get
			{
				return this.lightProbeUsage > LightProbeUsage.Off;
			}
			set
			{
				this.lightProbeUsage = (value ? LightProbeUsage.BlendProbes : LightProbeUsage.Off);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00010498 File Offset: 0x0000E698
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x000104AE File Offset: 0x0000E6AE
		public Bounds bounds
		{
			[FreeFunction(Name = "RendererScripting::GetWorldBounds", HasExplicitThis = true)]
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
			[NativeName("SetWorldAABB")]
			set
			{
				this.set_bounds_Injected(ref value);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000104B8 File Offset: 0x0000E6B8
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x000104CE File Offset: 0x0000E6CE
		public Bounds localBounds
		{
			[FreeFunction(Name = "RendererScripting::GetLocalBounds", HasExplicitThis = true)]
			get
			{
				Bounds result;
				this.get_localBounds_Injected(out result);
				return result;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		// Token: 0x06000BA2 RID: 2978
		[NativeName("ResetWorldAABB")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetBounds();

		// Token: 0x06000BA3 RID: 2979
		[NativeName("ResetLocalAABB")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetLocalBounds();

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000104D8 File Offset: 0x0000E6D8
		[FreeFunction(Name = "RendererScripting::SetStaticLightmapST", HasExplicitThis = true)]
		private void SetStaticLightmapST(Vector4 st)
		{
			this.SetStaticLightmapST_Injected(ref st);
		}

		// Token: 0x06000BA5 RID: 2981
		[FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material GetMaterial();

		// Token: 0x06000BA6 RID: 2982
		[FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material GetSharedMaterial();

		// Token: 0x06000BA7 RID: 2983
		[FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMaterial(Material m);

		// Token: 0x06000BA8 RID: 2984
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material[] GetMaterialArray();

		// Token: 0x06000BA9 RID: 2985
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CopyMaterialArray([Out] Material[] m);

		// Token: 0x06000BAA RID: 2986
		[FreeFunction(Name = "RendererScripting::GetSharedMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CopySharedMaterialArray([Out] Material[] m);

		// Token: 0x06000BAB RID: 2987
		[FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMaterialArray([NotNull("ArgumentNullException")] Material[] m);

		// Token: 0x06000BAC RID: 2988
		[FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_SetPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x06000BAD RID: 2989
		[FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetPropertyBlock([NotNull("ArgumentNullException")] MaterialPropertyBlock dest);

		// Token: 0x06000BAE RID: 2990
		[FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex);

		// Token: 0x06000BAF RID: 2991
		[FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetPropertyBlockMaterialIndex([NotNull("ArgumentNullException")] MaterialPropertyBlock dest, int materialIndex);

		// Token: 0x06000BB0 RID: 2992
		[FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasPropertyBlock();

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000104E2 File Offset: 0x0000E6E2
		public void SetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_SetPropertyBlock(properties);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000104ED File Offset: 0x0000E6ED
		public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_SetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000104F9 File Offset: 0x0000E6F9
		public void GetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_GetPropertyBlock(properties);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00010504 File Offset: 0x0000E704
		public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_GetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x06000BB5 RID: 2997
		[FreeFunction(Name = "RendererScripting::GetClosestReflectionProbes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetClosestReflectionProbesInternal(object result);

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BB6 RID: 2998
		// (set) Token: 0x06000BB7 RID: 2999
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BB8 RID: 3000
		public extern bool isVisible { [NativeName("IsVisibleInScene")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BB9 RID: 3001
		// (set) Token: 0x06000BBA RID: 3002
		public extern ShadowCastingMode shadowCastingMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BBB RID: 3003
		// (set) Token: 0x06000BBC RID: 3004
		public extern bool receiveShadows { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BBD RID: 3005
		// (set) Token: 0x06000BBE RID: 3006
		public extern bool forceRenderingOff { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000BBF RID: 3007
		[NativeName("GetIsStaticShadowCaster")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetIsStaticShadowCaster();

		// Token: 0x06000BC0 RID: 3008
		[NativeName("SetIsStaticShadowCaster")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIsStaticShadowCaster(bool value);

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00010510 File Offset: 0x0000E710
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00010528 File Offset: 0x0000E728
		public bool staticShadowCaster
		{
			get
			{
				return this.GetIsStaticShadowCaster();
			}
			set
			{
				this.SetIsStaticShadowCaster(value);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BC3 RID: 3011
		// (set) Token: 0x06000BC4 RID: 3012
		public extern MotionVectorGenerationMode motionVectorGenerationMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BC5 RID: 3013
		// (set) Token: 0x06000BC6 RID: 3014
		public extern LightProbeUsage lightProbeUsage { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BC7 RID: 3015
		// (set) Token: 0x06000BC8 RID: 3016
		public extern ReflectionProbeUsage reflectionProbeUsage { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BC9 RID: 3017
		// (set) Token: 0x06000BCA RID: 3018
		public extern uint renderingLayerMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BCB RID: 3019
		// (set) Token: 0x06000BCC RID: 3020
		public extern int rendererPriority { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BCD RID: 3021
		// (set) Token: 0x06000BCE RID: 3022
		public extern RayTracingMode rayTracingMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000BCF RID: 3023
		// (set) Token: 0x06000BD0 RID: 3024
		public extern string sortingLayerName { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000BD1 RID: 3025
		// (set) Token: 0x06000BD2 RID: 3026
		public extern int sortingLayerID { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000BD3 RID: 3027
		// (set) Token: 0x06000BD4 RID: 3028
		public extern int sortingOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000BD5 RID: 3029
		// (set) Token: 0x06000BD6 RID: 3030
		internal extern int sortingGroupID { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000BD7 RID: 3031
		// (set) Token: 0x06000BD8 RID: 3032
		internal extern int sortingGroupOrder { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000BD9 RID: 3033
		// (set) Token: 0x06000BDA RID: 3034
		[NativeProperty("IsDynamicOccludee")]
		public extern bool allowOcclusionWhenDynamic { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000BDB RID: 3035
		// (set) Token: 0x06000BDC RID: 3036
		[NativeProperty("StaticBatchRoot")]
		internal extern Transform staticBatchRootTransform { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000BDD RID: 3037
		internal extern int staticBatchIndex { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000BDE RID: 3038
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetStaticBatchInfo(int firstSubMesh, int subMeshCount);

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000BDF RID: 3039
		public extern bool isPartOfStaticBatch { [NativeName("IsPartOfStaticBatch")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00010534 File Offset: 0x0000E734
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_worldToLocalMatrix_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001054C File Offset: 0x0000E74C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_localToWorldMatrix_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BE2 RID: 3042
		// (set) Token: 0x06000BE3 RID: 3043
		public extern GameObject lightProbeProxyVolumeOverride { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BE4 RID: 3044
		// (set) Token: 0x06000BE5 RID: 3045
		public extern Transform probeAnchor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000BE6 RID: 3046
		[NativeName("GetLightmapIndexInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLightmapIndex(LightmapType lt);

		// Token: 0x06000BE7 RID: 3047
		[NativeName("SetLightmapIndexInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLightmapIndex(int index, LightmapType lt);

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00010564 File Offset: 0x0000E764
		[NativeName("GetLightmapST")]
		private Vector4 GetLightmapST(LightmapType lt)
		{
			Vector4 result;
			this.GetLightmapST_Injected(lt, out result);
			return result;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001057B File Offset: 0x0000E77B
		[NativeName("SetLightmapST")]
		private void SetLightmapST(Vector4 st, LightmapType lt)
		{
			this.SetLightmapST_Injected(ref st, lt);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00010588 File Offset: 0x0000E788
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x000105A1 File Offset: 0x0000E7A1
		public int lightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.StaticLightmap);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000105B0 File Offset: 0x0000E7B0
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x000105C9 File Offset: 0x0000E7C9
		public int realtimeLightmapIndex
		{
			get
			{
				return this.GetLightmapIndex(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapIndex(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000105D8 File Offset: 0x0000E7D8
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000105F1 File Offset: 0x0000E7F1
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.StaticLightmap);
			}
			set
			{
				this.SetStaticLightmapST(value);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000105FC File Offset: 0x0000E7FC
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00010615 File Offset: 0x0000E815
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.GetLightmapST(LightmapType.DynamicLightmap);
			}
			set
			{
				this.SetLightmapST(value, LightmapType.DynamicLightmap);
			}
		}

		// Token: 0x06000BF2 RID: 3058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMaterialCount();

		// Token: 0x06000BF3 RID: 3059
		[NativeName("GetMaterialArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Material[] GetSharedMaterialArray();

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00010624 File Offset: 0x0000E824
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0001063C File Offset: 0x0000E83C
		public Material[] materials
		{
			get
			{
				return this.GetMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00010648 File Offset: 0x0000E848
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x00010660 File Offset: 0x0000E860
		public Material material
		{
			get
			{
				return this.GetMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0001066C File Offset: 0x0000E86C
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00010660 File Offset: 0x0000E860
		public Material sharedMaterial
		{
			get
			{
				return this.GetSharedMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00010684 File Offset: 0x0000E884
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0001063C File Offset: 0x0000E83C
		public Material[] sharedMaterials
		{
			get
			{
				return this.GetSharedMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001069C File Offset: 0x0000E89C
		public void GetMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopyMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000106DC File Offset: 0x0000E8DC
		public void GetSharedMaterials(List<Material> m)
		{
			bool flag = m == null;
			if (flag)
			{
				throw new ArgumentNullException("The result material list cannot be null.", "m");
			}
			NoAllocHelpers.EnsureListElemCount<Material>(m, this.GetMaterialCount());
			this.CopySharedMaterialArray(NoAllocHelpers.ExtractArrayFromListT<Material>(m));
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001071C File Offset: 0x0000E91C
		public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
		{
			this.GetClosestReflectionProbesInternal(result);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00010727 File Offset: 0x0000E927
		public Renderer()
		{
		}

		// Token: 0x06000C00 RID: 3072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000C01 RID: 3073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_bounds_Injected(ref Bounds value);

		// Token: 0x06000C02 RID: 3074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x06000C03 RID: 3075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localBounds_Injected(ref Bounds value);

		// Token: 0x06000C04 RID: 3076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStaticLightmapST_Injected(ref Vector4 st);

		// Token: 0x06000C05 RID: 3077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000C06 RID: 3078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000C07 RID: 3079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLightmapST_Injected(LightmapType lt, out Vector4 ret);

		// Token: 0x06000C08 RID: 3080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLightmapST_Injected(ref Vector4 st, LightmapType lt);
	}
}
