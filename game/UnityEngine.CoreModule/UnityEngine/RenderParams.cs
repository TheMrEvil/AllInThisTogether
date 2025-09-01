using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000139 RID: 313
	public struct RenderParams
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public RenderParams(Material mat)
		{
			this.layer = 0;
			this.renderingLayerMask = GraphicsSettings.defaultRenderingLayerMask;
			this.rendererPriority = 0;
			this.worldBounds = new Bounds(Vector3.zero, Vector3.zero);
			this.camera = null;
			this.motionVectorMode = MotionVectorGenerationMode.Camera;
			this.reflectionProbeUsage = ReflectionProbeUsage.Off;
			this.material = mat;
			this.matProps = null;
			this.shadowCastingMode = ShadowCastingMode.Off;
			this.receiveShadows = false;
			this.lightProbeUsage = LightProbeUsage.Off;
			this.lightProbeProxyVolume = null;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0000F080 File Offset: 0x0000D280
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0000F088 File Offset: 0x0000D288
		public int layer
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<layer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<layer>k__BackingField = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0000F091 File Offset: 0x0000D291
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0000F099 File Offset: 0x0000D299
		public uint renderingLayerMask
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<renderingLayerMask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<renderingLayerMask>k__BackingField = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0000F0A2 File Offset: 0x0000D2A2
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0000F0AA File Offset: 0x0000D2AA
		public int rendererPriority
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<rendererPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rendererPriority>k__BackingField = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000F0B3 File Offset: 0x0000D2B3
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0000F0BB File Offset: 0x0000D2BB
		public Bounds worldBounds
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<worldBounds>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<worldBounds>k__BackingField = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public Camera camera
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<camera>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<camera>k__BackingField = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0000F0DD File Offset: 0x0000D2DD
		public MotionVectorGenerationMode motionVectorMode
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<motionVectorMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<motionVectorMode>k__BackingField = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0000F0EE File Offset: 0x0000D2EE
		public ReflectionProbeUsage reflectionProbeUsage
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<reflectionProbeUsage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<reflectionProbeUsage>k__BackingField = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000F0F7 File Offset: 0x0000D2F7
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0000F0FF File Offset: 0x0000D2FF
		public Material material
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<material>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<material>k__BackingField = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0000F108 File Offset: 0x0000D308
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0000F110 File Offset: 0x0000D310
		public MaterialPropertyBlock matProps
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<matProps>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<matProps>k__BackingField = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000F119 File Offset: 0x0000D319
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0000F121 File Offset: 0x0000D321
		public ShadowCastingMode shadowCastingMode
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<shadowCastingMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<shadowCastingMode>k__BackingField = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0000F12A File Offset: 0x0000D32A
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0000F132 File Offset: 0x0000D332
		public bool receiveShadows
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<receiveShadows>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<receiveShadows>k__BackingField = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0000F13B File Offset: 0x0000D33B
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0000F143 File Offset: 0x0000D343
		public LightProbeUsage lightProbeUsage
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<lightProbeUsage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lightProbeUsage>k__BackingField = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0000F14C File Offset: 0x0000D34C
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0000F154 File Offset: 0x0000D354
		public LightProbeProxyVolume lightProbeProxyVolume
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<lightProbeProxyVolume>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lightProbeProxyVolume>k__BackingField = value;
			}
		}

		// Token: 0x040003F0 RID: 1008
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <layer>k__BackingField;

		// Token: 0x040003F1 RID: 1009
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private uint <renderingLayerMask>k__BackingField;

		// Token: 0x040003F2 RID: 1010
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <rendererPriority>k__BackingField;

		// Token: 0x040003F3 RID: 1011
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Bounds <worldBounds>k__BackingField;

		// Token: 0x040003F4 RID: 1012
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Camera <camera>k__BackingField;

		// Token: 0x040003F5 RID: 1013
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MotionVectorGenerationMode <motionVectorMode>k__BackingField;

		// Token: 0x040003F6 RID: 1014
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ReflectionProbeUsage <reflectionProbeUsage>k__BackingField;

		// Token: 0x040003F7 RID: 1015
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Material <material>k__BackingField;

		// Token: 0x040003F8 RID: 1016
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private MaterialPropertyBlock <matProps>k__BackingField;

		// Token: 0x040003F9 RID: 1017
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ShadowCastingMode <shadowCastingMode>k__BackingField;

		// Token: 0x040003FA RID: 1018
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <receiveShadows>k__BackingField;

		// Token: 0x040003FB RID: 1019
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LightProbeUsage <lightProbeUsage>k__BackingField;

		// Token: 0x040003FC RID: 1020
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private LightProbeProxyVolume <lightProbeProxyVolume>k__BackingField;
	}
}
