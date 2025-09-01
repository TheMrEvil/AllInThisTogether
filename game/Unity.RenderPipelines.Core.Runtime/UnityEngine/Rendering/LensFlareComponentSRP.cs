using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007C RID: 124
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Lens Flare (SRP)")]
	public sealed class LensFlareComponentSRP : MonoBehaviour
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00013DA8 File Offset: 0x00011FA8
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00013DB0 File Offset: 0x00011FB0
		public LensFlareDataSRP lensFlareData
		{
			get
			{
				return this.m_LensFlareData;
			}
			set
			{
				this.m_LensFlareData = value;
				this.OnValidate();
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public float celestialProjectedOcclusionRadius(Camera mainCam)
		{
			float num = (float)Math.Tan((double)LensFlareComponentSRP.sCelestialAngularRadius) * mainCam.farClipPlane;
			return this.occlusionRadius * num;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013DE9 File Offset: 0x00011FE9
		private void OnEnable()
		{
			if (this.lensFlareData)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013E0F File Offset: 0x0001200F
		private void OnDisable()
		{
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013E1C File Offset: 0x0001201C
		private void OnValidate()
		{
			if (base.isActiveAndEnabled && this.lensFlareData != null)
			{
				LensFlareCommonSRP.Instance.AddData(this);
				return;
			}
			LensFlareCommonSRP.Instance.RemoveData(this);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013E4C File Offset: 0x0001204C
		public LensFlareComponentSRP()
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013F67 File Offset: 0x00012167
		// Note: this type is marked as 'beforefieldinit'.
		static LensFlareComponentSRP()
		{
		}

		// Token: 0x04000270 RID: 624
		[SerializeField]
		private LensFlareDataSRP m_LensFlareData;

		// Token: 0x04000271 RID: 625
		[Min(0f)]
		public float intensity = 1f;

		// Token: 0x04000272 RID: 626
		[Min(1E-05f)]
		public float maxAttenuationDistance = 100f;

		// Token: 0x04000273 RID: 627
		[Min(1E-05f)]
		public float maxAttenuationScale = 100f;

		// Token: 0x04000274 RID: 628
		public AnimationCurve distanceAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000275 RID: 629
		public AnimationCurve scaleByDistanceCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000276 RID: 630
		public bool attenuationByLightShape = true;

		// Token: 0x04000277 RID: 631
		public AnimationCurve radialScreenAttenuationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04000278 RID: 632
		public bool useOcclusion;

		// Token: 0x04000279 RID: 633
		[Min(0f)]
		public float occlusionRadius = 0.1f;

		// Token: 0x0400027A RID: 634
		[Range(1f, 64f)]
		public uint sampleCount = 32U;

		// Token: 0x0400027B RID: 635
		public float occlusionOffset = 0.05f;

		// Token: 0x0400027C RID: 636
		[Min(0f)]
		public float scale = 1f;

		// Token: 0x0400027D RID: 637
		public bool allowOffScreen;

		// Token: 0x0400027E RID: 638
		private static float sCelestialAngularRadius = 0.057595868f;
	}
}
