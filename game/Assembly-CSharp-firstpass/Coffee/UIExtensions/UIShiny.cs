using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200009F RID: 159
	[AddComponentMenu("UI/UIEffect/UIShiny", 2)]
	public class UIShiny : UIEffectBase
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00029CC9 File Offset: 0x00027EC9
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00029CD1 File Offset: 0x00027ED1
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00029D00 File Offset: 0x00027F00
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00029D08 File Offset: 0x00027F08
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00029D37 File Offset: 0x00027F37
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00029D3F File Offset: 0x00027F3F
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00029D6E File Offset: 0x00027F6E
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00029D76 File Offset: 0x00027F76
		public float softness
		{
			get
			{
				return this.m_Softness;
			}
			set
			{
				value = Mathf.Clamp(value, 0.01f, 1f);
				if (!Mathf.Approximately(this.m_Softness, value))
				{
					this.m_Softness = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00029DA5 File Offset: 0x00027FA5
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00029DAD File Offset: 0x00027FAD
		[Obsolete("Use brightness instead (UnityUpgradable) -> brightness")]
		public float alpha
		{
			get
			{
				return this.m_Brightness;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Brightness, value))
				{
					this.m_Brightness = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00029DDC File Offset: 0x00027FDC
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00029DE4 File Offset: 0x00027FE4
		public float brightness
		{
			get
			{
				return this.m_Brightness;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Brightness, value))
				{
					this.m_Brightness = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00029E13 File Offset: 0x00028013
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00029E1B File Offset: 0x0002801B
		[Obsolete("Use gloss instead (UnityUpgradable) -> gloss")]
		public float highlight
		{
			get
			{
				return this.m_Gloss;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Gloss, value))
				{
					this.m_Gloss = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00029E4A File Offset: 0x0002804A
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00029E52 File Offset: 0x00028052
		public float gloss
		{
			get
			{
				return this.m_Gloss;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Gloss, value))
				{
					this.m_Gloss = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00029E81 File Offset: 0x00028081
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00029E89 File Offset: 0x00028089
		public float rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				if (!Mathf.Approximately(this.m_Rotation, value))
				{
					this.m_Rotation = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00029EA6 File Offset: 0x000280A6
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00029EAE File Offset: 0x000280AE
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
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00029EC6 File Offset: 0x000280C6
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00029ED3 File Offset: 0x000280D3
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00029EE1 File Offset: 0x000280E1
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00029EEE File Offset: 0x000280EE
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00029EFC File Offset: 0x000280FC
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00029F09 File Offset: 0x00028109
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00029F21 File Offset: 0x00028121
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00029F2E File Offset: 0x0002812E
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00029F46 File Offset: 0x00028146
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00029F53 File Offset: 0x00028153
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00029F61 File Offset: 0x00028161
		public override ParameterTexture ptex
		{
			get
			{
				return UIShiny._ptex;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00029F68 File Offset: 0x00028168
		protected override void OnEnable()
		{
			base.OnEnable();
			this._player.OnEnable(delegate(float f)
			{
				this.effectFactor = f;
			});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00029F87 File Offset: 0x00028187
		protected override void OnDisable()
		{
			base.OnDisable();
			this._player.OnDisable();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00029F9C File Offset: 0x0002819C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			bool isText = base.isTMPro || base.graphic is Text;
			float normalizedIndex = this.ptex.GetNormalizedIndex(this);
			Rect effectArea = this.m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, -1f);
			float f = this.m_Rotation * 0.017453292f;
			Vector2 normalized = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
			normalized.x *= effectArea.height / effectArea.width;
			normalized = normalized.normalized;
			UIVertex uivertex = default(UIVertex);
			Matrix2x3 matrix = new Matrix2x3(effectArea, normalized.x, normalized.y);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				Vector2 vector;
				this.m_EffectArea.GetNormalizedFactor(i, matrix, uivertex.position, isText, out vector);
				uivertex.uv0 = new Vector2(Packer.ToFloat(uivertex.uv0.x, uivertex.uv0.y), Packer.ToFloat(vector.y, normalizedIndex));
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002A0D9 File Offset: 0x000282D9
		public void Play()
		{
			this._player.Play(null);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002A0E7 File Offset: 0x000282E7
		public void Stop()
		{
			this._player.Stop();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002A0F4 File Offset: 0x000282F4
		protected override void SetDirty()
		{
			this.ptex.RegisterMaterial(base.targetGraphic.material);
			this.ptex.SetData(this, 0, this.m_EffectFactor);
			this.ptex.SetData(this, 1, this.m_Width);
			this.ptex.SetData(this, 2, this.m_Softness);
			this.ptex.SetData(this, 3, this.m_Brightness);
			this.ptex.SetData(this, 4, this.m_Gloss);
			if (!Mathf.Approximately(this._lastRotation, this.m_Rotation) && base.targetGraphic)
			{
				this._lastRotation = this.m_Rotation;
				base.targetGraphic.SetVerticesDirty();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0002A1B0 File Offset: 0x000283B0
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

		// Token: 0x06000628 RID: 1576 RVA: 0x0002A1D8 File Offset: 0x000283D8
		public UIShiny()
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002A22D File Offset: 0x0002842D
		// Note: this type is marked as 'beforefieldinit'.
		static UIShiny()
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002A244 File Offset: 0x00028444
		[CompilerGenerated]
		private void <OnEnable>b__62_0(float f)
		{
			this.effectFactor = f;
		}

		// Token: 0x04000567 RID: 1383
		public const string shaderName = "UI/Hidden/UI-Effect-Shiny";

		// Token: 0x04000568 RID: 1384
		private static readonly ParameterTexture _ptex = new ParameterTexture(8, 128, "_ParamTex");

		// Token: 0x04000569 RID: 1385
		[Tooltip("Location for shiny effect.")]
		[FormerlySerializedAs("m_Location")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor;

		// Token: 0x0400056A RID: 1386
		[Tooltip("Width for shiny effect.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Width = 0.25f;

		// Token: 0x0400056B RID: 1387
		[Tooltip("Rotation for shiny effect.")]
		[SerializeField]
		[Range(-180f, 180f)]
		private float m_Rotation;

		// Token: 0x0400056C RID: 1388
		[Tooltip("Softness for shiny effect.")]
		[SerializeField]
		[Range(0.01f, 1f)]
		private float m_Softness = 1f;

		// Token: 0x0400056D RID: 1389
		[Tooltip("Brightness for shiny effect.")]
		[FormerlySerializedAs("m_Alpha")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Brightness = 1f;

		// Token: 0x0400056E RID: 1390
		[Tooltip("Gloss factor for shiny effect.")]
		[FormerlySerializedAs("m_Highlight")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Gloss = 1f;

		// Token: 0x0400056F RID: 1391
		[Tooltip("The area for effect.")]
		[SerializeField]
		protected EffectArea m_EffectArea;

		// Token: 0x04000570 RID: 1392
		[SerializeField]
		private EffectPlayer m_Player;

		// Token: 0x04000571 RID: 1393
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private bool m_Play;

		// Token: 0x04000572 RID: 1394
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04000573 RID: 1395
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0.1f, 10f)]
		private float m_Duration = 1f;

		// Token: 0x04000574 RID: 1396
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0f, 10f)]
		private float m_LoopDelay = 1f;

		// Token: 0x04000575 RID: 1397
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private AnimatorUpdateMode m_UpdateMode;

		// Token: 0x04000576 RID: 1398
		private float _lastRotation;
	}
}
