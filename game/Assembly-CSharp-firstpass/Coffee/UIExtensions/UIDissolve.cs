using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200008F RID: 143
	[AddComponentMenu("UI/MeshEffectForTextMeshPro/UIDissolve", 3)]
	public class UIDissolve : UIEffectBase
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000270FA File Offset: 0x000252FA
		public override AdditionalCanvasShaderChannels requiredChannels
		{
			get
			{
				return AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000270FD File Offset: 0x000252FD
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00027105 File Offset: 0x00025305
		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float location
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00027134 File Offset: 0x00025334
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0002713C File Offset: 0x0002533C
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0002716B File Offset: 0x0002536B
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00027173 File Offset: 0x00025373
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Width, value))
				{
					this.m_Width = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000271A2 File Offset: 0x000253A2
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x000271AA File Offset: 0x000253AA
		public float softness
		{
			get
			{
				return this.m_Softness;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Softness, value))
				{
					this.m_Softness = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000271D9 File Offset: 0x000253D9
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x000271E1 File Offset: 0x000253E1
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (this.m_Color != value)
				{
					this.m_Color = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000271FE File Offset: 0x000253FE
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0002721A File Offset: 0x0002541A
		public Texture noiseTexture
		{
			get
			{
				return this.m_NoiseTexture ?? this.material.GetTexture("_NoiseTex");
			}
			set
			{
				if (this.m_NoiseTexture != value)
				{
					this.m_NoiseTexture = value;
					if (base.graphic)
					{
						this.ModifyMaterial();
					}
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00027244 File Offset: 0x00025444
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0002724C File Offset: 0x0002544C
		public EffectArea effectArea
		{
			get
			{
				return this.m_EffectArea;
			}
			set
			{
				if (this.m_EffectArea != value)
				{
					this.m_EffectArea = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00027264 File Offset: 0x00025464
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0002726C File Offset: 0x0002546C
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
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00027284 File Offset: 0x00025484
		public ColorMode colorMode
		{
			get
			{
				return this.m_ColorMode;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0002728C File Offset: 0x0002548C
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00027299 File Offset: 0x00025499
		[Obsolete("Use Play/Stop method instead")]
		public bool play
		{
			get
			{
				return this._player.play;
			}
			set
			{
				this._player.play = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000272A7 File Offset: 0x000254A7
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x000272B4 File Offset: 0x000254B4
		[Obsolete]
		public bool loop
		{
			get
			{
				return this._player.loop;
			}
			set
			{
				this._player.loop = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000272C2 File Offset: 0x000254C2
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x000272CF File Offset: 0x000254CF
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000272E7 File Offset: 0x000254E7
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000272F4 File Offset: 0x000254F4
		[Obsolete]
		public float loopDelay
		{
			get
			{
				return this._player.loopDelay;
			}
			set
			{
				this._player.loopDelay = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0002730C File Offset: 0x0002550C
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00027319 File Offset: 0x00025519
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00027327 File Offset: 0x00025527
		public override ParameterTexture ptex
		{
			get
			{
				return UIDissolve._ptex;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00027330 File Offset: 0x00025530
		public override void ModifyMaterial()
		{
			if (base.isTMPro)
			{
				return;
			}
			ulong num = (ulong)(this.m_NoiseTexture ? this.m_NoiseTexture.GetInstanceID() : 0) + 4294967296UL + (ulong)((ulong)((long)this.m_ColorMode) << 36);
			if (this._materialCache != null && (this._materialCache.hash != num || !base.isActiveAndEnabled || !this.m_EffectMaterial))
			{
				MaterialCache.Unregister(this._materialCache);
				this._materialCache = null;
			}
			if (!base.isActiveAndEnabled || !this.m_EffectMaterial)
			{
				this.material = null;
				return;
			}
			if (!this.m_NoiseTexture)
			{
				this.material = this.m_EffectMaterial;
				return;
			}
			if (this._materialCache != null && this._materialCache.hash == num)
			{
				this.material = this._materialCache.material;
				return;
			}
			this._materialCache = MaterialCache.Register(num, this.m_NoiseTexture, delegate()
			{
				Material material = new Material(this.m_EffectMaterial);
				material.name = material.name + "_" + this.m_NoiseTexture.name;
				material.SetTexture("_NoiseTex", this.m_NoiseTexture);
				return material;
			});
			this.material = this._materialCache.material;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00027444 File Offset: 0x00025644
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			bool isText = base.isTMPro || base.graphic is Text;
			float normalizedIndex = this.ptex.GetNormalizedIndex(this);
			Texture noiseTexture = this.noiseTexture;
			float aspectRatio = (this.m_KeepAspectRatio && noiseTexture) ? ((float)noiseTexture.width / (float)noiseTexture.height) : -1f;
			Rect effectArea = this.m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, aspectRatio);
			UIVertex uivertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				float x;
				float y;
				this.m_EffectArea.GetPositionFactor(i, effectArea, uivertex.position, isText, base.isTMPro, out x, out y);
				if (base.isTMPro)
				{
					uivertex.uv2 = new Vector2(Packer.ToFloat(x, y, normalizedIndex), 0f);
				}
				else
				{
					uivertex.uv0 = new Vector2(Packer.ToFloat(uivertex.uv0.x, uivertex.uv0.y), Packer.ToFloat(x, y, normalizedIndex));
				}
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002758C File Offset: 0x0002578C
		protected override void SetDirty()
		{
			foreach (Material mat in this.materials)
			{
				this.ptex.RegisterMaterial(mat);
			}
			this.ptex.SetData(this, 0, this.m_EffectFactor);
			this.ptex.SetData(this, 1, this.m_Width);
			this.ptex.SetData(this, 2, this.m_Softness);
			this.ptex.SetData(this, 4, this.m_Color.r);
			this.ptex.SetData(this, 5, this.m_Color.g);
			this.ptex.SetData(this, 6, this.m_Color.b);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002763F File Offset: 0x0002583F
		public void Play()
		{
			this._player.Play(null);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002764D File Offset: 0x0002584D
		public void Stop()
		{
			this._player.Stop();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002765A File Offset: 0x0002585A
		protected override void OnEnable()
		{
			base.OnEnable();
			this._player.OnEnable(delegate(float f)
			{
				this.effectFactor = f;
			});
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00027679 File Offset: 0x00025879
		protected override void OnDisable()
		{
			base.OnDisable();
			MaterialCache.Unregister(this._materialCache);
			this._materialCache = null;
			this._player.OnDisable();
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000276A0 File Offset: 0x000258A0
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

		// Token: 0x06000544 RID: 1348 RVA: 0x000276C8 File Offset: 0x000258C8
		public UIDissolve()
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00027728 File Offset: 0x00025928
		// Note: this type is marked as 'beforefieldinit'.
		static UIDissolve()
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002773F File Offset: 0x0002593F
		[CompilerGenerated]
		private Material <ModifyMaterial>b__58_0()
		{
			Material material = new Material(this.m_EffectMaterial);
			material.name = material.name + "_" + this.m_NoiseTexture.name;
			material.SetTexture("_NoiseTex", this.m_NoiseTexture);
			return material;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002777E File Offset: 0x0002597E
		[CompilerGenerated]
		private void <OnEnable>b__63_0(float f)
		{
			this.effectFactor = f;
		}

		// Token: 0x040004E2 RID: 1250
		public const string shaderName = "UI/Hidden/UI-Effect-Dissolve";

		// Token: 0x040004E3 RID: 1251
		private static readonly ParameterTexture _ptex = new ParameterTexture(8, 128, "_ParamTex");

		// Token: 0x040004E4 RID: 1252
		[Tooltip("Current location[0-1] for dissolve effect. 0 is not dissolved, 1 is completely dissolved.")]
		[FormerlySerializedAs("m_Location")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor = 0.5f;

		// Token: 0x040004E5 RID: 1253
		[Tooltip("Edge width.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Width = 0.5f;

		// Token: 0x040004E6 RID: 1254
		[Tooltip("Edge softness.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Softness = 0.5f;

		// Token: 0x040004E7 RID: 1255
		[Tooltip("Edge color.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_Color = new Color(0f, 0.25f, 1f);

		// Token: 0x040004E8 RID: 1256
		[Tooltip("Edge color effect mode.")]
		[SerializeField]
		private ColorMode m_ColorMode = ColorMode.Add;

		// Token: 0x040004E9 RID: 1257
		[Tooltip("Noise texture for dissolving (single channel texture).")]
		[SerializeField]
		private Texture m_NoiseTexture;

		// Token: 0x040004EA RID: 1258
		[Tooltip("The area for effect.")]
		[SerializeField]
		protected EffectArea m_EffectArea;

		// Token: 0x040004EB RID: 1259
		[Tooltip("Keep effect aspect ratio.")]
		[SerializeField]
		private bool m_KeepAspectRatio;

		// Token: 0x040004EC RID: 1260
		[Header("Effect Player")]
		[SerializeField]
		private EffectPlayer m_Player;

		// Token: 0x040004ED RID: 1261
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0.1f, 10f)]
		private float m_Duration = 1f;

		// Token: 0x040004EE RID: 1262
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private AnimatorUpdateMode m_UpdateMode;

		// Token: 0x040004EF RID: 1263
		private MaterialCache _materialCache;
	}
}
