using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x020000A0 RID: 160
	[AddComponentMenu("UI/UIEffect/UITransitionEffect", 5)]
	public class UITransitionEffect : UIEffectBase
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0002A24D File Offset: 0x0002844D
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0002A255 File Offset: 0x00028455
		public float effectFactor
		{
			get
			{
				return this.m_EffectFactor;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_EffectFactor, value))
				{
					this.m_EffectFactor = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0002A284 File Offset: 0x00028484
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0002A28C File Offset: 0x0002848C
		public Texture transitionTexture
		{
			get
			{
				return this.m_TransitionTexture;
			}
			set
			{
				if (this.m_TransitionTexture != value)
				{
					this.m_TransitionTexture = value;
					if (base.graphic)
					{
						this.ModifyMaterial();
					}
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0002A2B6 File Offset: 0x000284B6
		public UITransitionEffect.EffectMode effectMode
		{
			get
			{
				return this.m_EffectMode;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0002A2BE File Offset: 0x000284BE
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0002A2C6 File Offset: 0x000284C6
		public bool keepAspectRatio
		{
			get
			{
				return this.m_KeepAspectRatio;
			}
			set
			{
				if (this.m_KeepAspectRatio != value)
				{
					this.m_KeepAspectRatio = value;
					base.targetGraphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002A2E3 File Offset: 0x000284E3
		public override ParameterTexture ptex
		{
			get
			{
				return UITransitionEffect._ptex;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0002A2EA File Offset: 0x000284EA
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0002A2F2 File Offset: 0x000284F2
		public float dissolveWidth
		{
			get
			{
				return this.m_DissolveWidth;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_DissolveWidth, value))
				{
					this.m_DissolveWidth = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0002A321 File Offset: 0x00028521
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0002A329 File Offset: 0x00028529
		public float dissolveSoftness
		{
			get
			{
				return this.m_DissolveSoftness;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_DissolveSoftness, value))
				{
					this.m_DissolveSoftness = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002A358 File Offset: 0x00028558
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0002A360 File Offset: 0x00028560
		public Color dissolveColor
		{
			get
			{
				return this.m_DissolveColor;
			}
			set
			{
				if (this.m_DissolveColor != value)
				{
					this.m_DissolveColor = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002A37D File Offset: 0x0002857D
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0002A38A File Offset: 0x0002858A
		public float duration
		{
			get
			{
				return this._player.duration;
			}
			set
			{
				this._player.duration = Mathf.Max(value, 0.1f);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0002A3A2 File Offset: 0x000285A2
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0002A3AA File Offset: 0x000285AA
		public bool passRayOnHidden
		{
			get
			{
				return this.m_PassRayOnHidden;
			}
			set
			{
				this.m_PassRayOnHidden = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002A3B3 File Offset: 0x000285B3
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0002A3C0 File Offset: 0x000285C0
		public AnimatorUpdateMode updateMode
		{
			get
			{
				return this._player.updateMode;
			}
			set
			{
				this._player.updateMode = value;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002A3CE File Offset: 0x000285CE
		public void Show()
		{
			this._player.loop = false;
			this._player.Play(delegate(float f)
			{
				this.effectFactor = f;
			});
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002A3F3 File Offset: 0x000285F3
		public void Hide()
		{
			this._player.loop = false;
			this._player.Play(delegate(float f)
			{
				this.effectFactor = 1f - f;
			});
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002A418 File Offset: 0x00028618
		public override void ModifyMaterial()
		{
			ulong num = (ulong)(this.m_TransitionTexture ? this.m_TransitionTexture.GetInstanceID() : 0) + 8589934592UL + (ulong)((ulong)((long)this.m_EffectMode) << 36);
			if (this._materialCache != null && (this._materialCache.hash != num || !base.isActiveAndEnabled || !this.m_EffectMaterial))
			{
				MaterialCache.Unregister(this._materialCache);
				this._materialCache = null;
			}
			if (!base.isActiveAndEnabled || !this.m_EffectMaterial)
			{
				base.graphic.material = null;
				return;
			}
			if (!this.m_TransitionTexture)
			{
				base.graphic.material = this.m_EffectMaterial;
				return;
			}
			if (this._materialCache != null && this._materialCache.hash == num)
			{
				base.graphic.material = this._materialCache.material;
				return;
			}
			this._materialCache = MaterialCache.Register(num, this.m_TransitionTexture, delegate()
			{
				Material material = new Material(this.m_EffectMaterial);
				material.name = material.name + "_" + this.m_TransitionTexture.name;
				material.SetTexture("_TransitionTexture", this.m_TransitionTexture);
				return material;
			});
			base.graphic.material = this._materialCache.material;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002A538 File Offset: 0x00028738
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			bool isText = base.isTMPro || base.graphic is Text;
			Texture transitionTexture = this.transitionTexture;
			float aspectRatio = (this.m_KeepAspectRatio && transitionTexture) ? ((float)transitionTexture.width / (float)transitionTexture.height) : -1f;
			Rect effectArea = this.m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, aspectRatio);
			float normalizedIndex = this.ptex.GetNormalizedIndex(this);
			UIVertex uivertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				float x;
				float y;
				this.m_EffectArea.GetPositionFactor(i, effectArea, uivertex.position, isText, base.isTMPro, out x, out y);
				uivertex.uv0 = new Vector2(Packer.ToFloat(uivertex.uv0.x, uivertex.uv0.y), Packer.ToFloat(x, y, normalizedIndex));
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002A650 File Offset: 0x00028850
		protected override void OnEnable()
		{
			base.OnEnable();
			this._player.OnEnable(null);
			this._player.loop = false;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002A670 File Offset: 0x00028870
		protected override void OnDisable()
		{
			MaterialCache.Unregister(this._materialCache);
			this._materialCache = null;
			base.OnDisable();
			this._player.OnDisable();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002A698 File Offset: 0x00028898
		protected override void SetDirty()
		{
			this.ptex.RegisterMaterial(base.targetGraphic.material);
			this.ptex.SetData(this, 0, this.m_EffectFactor);
			if (this.m_EffectMode == UITransitionEffect.EffectMode.Dissolve)
			{
				this.ptex.SetData(this, 1, this.m_DissolveWidth);
				this.ptex.SetData(this, 2, this.m_DissolveSoftness);
				this.ptex.SetData(this, 4, this.m_DissolveColor.r);
				this.ptex.SetData(this, 5, this.m_DissolveColor.g);
				this.ptex.SetData(this, 6, this.m_DissolveColor.b);
			}
			if (this.m_PassRayOnHidden)
			{
				base.graphic.raycastTarget = (0f < this.m_EffectFactor);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0002A768 File Offset: 0x00028968
		private EffectPlayer _player
		{
			get
			{
				EffectPlayer result;
				if ((result = this.m_Player) == null)
				{
					result = (this.m_Player = new EffectPlayer());
				}
				return result;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002A790 File Offset: 0x00028990
		public UITransitionEffect()
		{
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002A7E5 File Offset: 0x000289E5
		// Note: this type is marked as 'beforefieldinit'.
		static UITransitionEffect()
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002A7FC File Offset: 0x000289FC
		[CompilerGenerated]
		private void <Show>b__44_0(float f)
		{
			this.effectFactor = f;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002A805 File Offset: 0x00028A05
		[CompilerGenerated]
		private void <Hide>b__45_0(float f)
		{
			this.effectFactor = 1f - f;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002A814 File Offset: 0x00028A14
		[CompilerGenerated]
		private Material <ModifyMaterial>b__46_0()
		{
			Material material = new Material(this.m_EffectMaterial);
			material.name = material.name + "_" + this.m_TransitionTexture.name;
			material.SetTexture("_TransitionTexture", this.m_TransitionTexture);
			return material;
		}

		// Token: 0x04000577 RID: 1399
		public const string shaderName = "UI/Hidden/UI-Effect-Transition";

		// Token: 0x04000578 RID: 1400
		private static readonly ParameterTexture _ptex = new ParameterTexture(8, 128, "_ParamTex");

		// Token: 0x04000579 RID: 1401
		[Tooltip("Effect mode.")]
		[SerializeField]
		private UITransitionEffect.EffectMode m_EffectMode = UITransitionEffect.EffectMode.Cutoff;

		// Token: 0x0400057A RID: 1402
		[Tooltip("Effect factor between 0(hidden) and 1(shown).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor = 1f;

		// Token: 0x0400057B RID: 1403
		[Tooltip("Transition texture (single channel texture).")]
		[SerializeField]
		private Texture m_TransitionTexture;

		// Token: 0x0400057C RID: 1404
		[Tooltip("The area for effect.")]
		[SerializeField]
		private EffectArea m_EffectArea;

		// Token: 0x0400057D RID: 1405
		[Tooltip("Keep effect aspect ratio.")]
		[SerializeField]
		private bool m_KeepAspectRatio;

		// Token: 0x0400057E RID: 1406
		[Tooltip("Dissolve edge width.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_DissolveWidth = 0.5f;

		// Token: 0x0400057F RID: 1407
		[Tooltip("Dissolve edge softness.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_DissolveSoftness = 0.5f;

		// Token: 0x04000580 RID: 1408
		[Tooltip("Dissolve edge color.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_DissolveColor = new Color(0f, 0.25f, 1f);

		// Token: 0x04000581 RID: 1409
		[Tooltip("Disable graphic's raycast target on hidden.")]
		[SerializeField]
		private bool m_PassRayOnHidden;

		// Token: 0x04000582 RID: 1410
		[Header("Effect Player")]
		[SerializeField]
		private EffectPlayer m_Player;

		// Token: 0x04000583 RID: 1411
		private MaterialCache _materialCache;

		// Token: 0x020001D0 RID: 464
		public enum EffectMode
		{
			// Token: 0x04000E05 RID: 3589
			Fade = 1,
			// Token: 0x04000E06 RID: 3590
			Cutoff,
			// Token: 0x04000E07 RID: 3591
			Dissolve
		}
	}
}
