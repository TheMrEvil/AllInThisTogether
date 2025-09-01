using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public sealed class LensFlareDataElementSRP
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00013F74 File Offset: 0x00012174
		public LensFlareDataElementSRP()
		{
			this.visible = true;
			this.localIntensity = 1f;
			this.position = 0f;
			this.positionOffset = new Vector2(0f, 0f);
			this.angularOffset = 0f;
			this.translationScale = new Vector2(1f, 1f);
			this.lensFlareTexture = null;
			this.uniformScale = 1f;
			this.sizeXY = Vector2.one;
			this.allowMultipleElement = false;
			this.count = 5;
			this.rotation = 0f;
			this.tint = new Color(1f, 1f, 1f, 0.5f);
			this.blendMode = SRPLensFlareBlendMode.Additive;
			this.autoRotate = false;
			this.isFoldOpened = true;
			this.flareType = SRPLensFlareType.Circle;
			this.distribution = SRPLensFlareDistribution.Uniform;
			this.lengthSpread = 1f;
			this.colorGradient = new Gradient();
			this.colorGradient.SetKeys(new GradientColorKey[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.white, 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			});
			this.positionCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, -1f)
			});
			this.scaleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
			this.uniformAngleCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(1f, 0f)
			});
			this.seed = 0;
			this.intensityVariation = 0.75f;
			this.positionVariation = new Vector2(1f, 0f);
			this.scaleVariation = 1f;
			this.rotationVariation = 180f;
			this.enableRadialDistortion = false;
			this.targetSizeDistortion = Vector2.one;
			this.distortionCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, -1f)
			});
			this.distortionRelativeToCenter = false;
			this.fallOff = 1f;
			this.edgeOffset = 0.1f;
			this.sdfRoundness = 0f;
			this.sideCount = 6;
			this.inverseSDF = false;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00014277 File Offset: 0x00012477
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0001427F File Offset: 0x0001247F
		public float localIntensity
		{
			get
			{
				return this.m_LocalIntensity;
			}
			set
			{
				this.m_LocalIntensity = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00014292 File Offset: 0x00012492
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0001429A File Offset: 0x0001249A
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = Mathf.Max(1, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000142A9 File Offset: 0x000124A9
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x000142B1 File Offset: 0x000124B1
		public float intensityVariation
		{
			get
			{
				return this.m_IntensityVariation;
			}
			set
			{
				this.m_IntensityVariation = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000142C4 File Offset: 0x000124C4
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x000142CC File Offset: 0x000124CC
		public float fallOff
		{
			get
			{
				return this.m_FallOff;
			}
			set
			{
				this.m_FallOff = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000142DA File Offset: 0x000124DA
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x000142E2 File Offset: 0x000124E2
		public float edgeOffset
		{
			get
			{
				return this.m_EdgeOffset;
			}
			set
			{
				this.m_EdgeOffset = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000142F0 File Offset: 0x000124F0
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x000142F8 File Offset: 0x000124F8
		public int sideCount
		{
			get
			{
				return this.m_SideCount;
			}
			set
			{
				this.m_SideCount = Mathf.Max(3, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00014307 File Offset: 0x00012507
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001430F File Offset: 0x0001250F
		public float sdfRoundness
		{
			get
			{
				return this.m_SdfRoundness;
			}
			set
			{
				this.m_SdfRoundness = Mathf.Clamp01(value);
			}
		}

		// Token: 0x0400028C RID: 652
		public bool visible;

		// Token: 0x0400028D RID: 653
		public float position;

		// Token: 0x0400028E RID: 654
		public Vector2 positionOffset;

		// Token: 0x0400028F RID: 655
		public float angularOffset;

		// Token: 0x04000290 RID: 656
		public Vector2 translationScale;

		// Token: 0x04000291 RID: 657
		[Min(0f)]
		[SerializeField]
		[FormerlySerializedAs("localIntensity")]
		private float m_LocalIntensity;

		// Token: 0x04000292 RID: 658
		public Texture lensFlareTexture;

		// Token: 0x04000293 RID: 659
		public float uniformScale;

		// Token: 0x04000294 RID: 660
		public Vector2 sizeXY;

		// Token: 0x04000295 RID: 661
		public bool allowMultipleElement;

		// Token: 0x04000296 RID: 662
		[Min(1f)]
		[SerializeField]
		[FormerlySerializedAs("count")]
		private int m_Count;

		// Token: 0x04000297 RID: 663
		public bool preserveAspectRatio;

		// Token: 0x04000298 RID: 664
		public float rotation;

		// Token: 0x04000299 RID: 665
		public Color tint;

		// Token: 0x0400029A RID: 666
		public SRPLensFlareBlendMode blendMode;

		// Token: 0x0400029B RID: 667
		public bool autoRotate;

		// Token: 0x0400029C RID: 668
		public SRPLensFlareType flareType;

		// Token: 0x0400029D RID: 669
		public bool modulateByLightColor;

		// Token: 0x0400029E RID: 670
		[SerializeField]
		private bool isFoldOpened;

		// Token: 0x0400029F RID: 671
		public SRPLensFlareDistribution distribution;

		// Token: 0x040002A0 RID: 672
		public float lengthSpread;

		// Token: 0x040002A1 RID: 673
		public AnimationCurve positionCurve;

		// Token: 0x040002A2 RID: 674
		public AnimationCurve scaleCurve;

		// Token: 0x040002A3 RID: 675
		public int seed;

		// Token: 0x040002A4 RID: 676
		public Gradient colorGradient;

		// Token: 0x040002A5 RID: 677
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("intensityVariation")]
		private float m_IntensityVariation;

		// Token: 0x040002A6 RID: 678
		public Vector2 positionVariation;

		// Token: 0x040002A7 RID: 679
		public float scaleVariation;

		// Token: 0x040002A8 RID: 680
		public float rotationVariation;

		// Token: 0x040002A9 RID: 681
		public bool enableRadialDistortion;

		// Token: 0x040002AA RID: 682
		public Vector2 targetSizeDistortion;

		// Token: 0x040002AB RID: 683
		public AnimationCurve distortionCurve;

		// Token: 0x040002AC RID: 684
		public bool distortionRelativeToCenter;

		// Token: 0x040002AD RID: 685
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("fallOff")]
		private float m_FallOff;

		// Token: 0x040002AE RID: 686
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("edgeOffset")]
		private float m_EdgeOffset;

		// Token: 0x040002AF RID: 687
		[Min(3f)]
		[SerializeField]
		[FormerlySerializedAs("sideCount")]
		private int m_SideCount;

		// Token: 0x040002B0 RID: 688
		[Range(0f, 1f)]
		[SerializeField]
		[FormerlySerializedAs("sdfRoundness")]
		private float m_SdfRoundness;

		// Token: 0x040002B1 RID: 689
		public bool inverseSDF;

		// Token: 0x040002B2 RID: 690
		public float uniformAngle;

		// Token: 0x040002B3 RID: 691
		public AnimationCurve uniformAngleCurve;
	}
}
