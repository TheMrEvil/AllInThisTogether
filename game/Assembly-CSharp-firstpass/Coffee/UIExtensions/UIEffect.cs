using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200009C RID: 156
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/UIEffect/UIEffect", 1)]
	public class UIEffect : UIEffectBase
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00028B0E File Offset: 0x00026D0E
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00028B16 File Offset: 0x00026D16
		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float toneLevel
		{
			get
			{
				return this.m_EffectFactor;
			}
			set
			{
				this.m_EffectFactor = Mathf.Clamp(value, 0f, 1f);
				this.SetDirty();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00028B34 File Offset: 0x00026D34
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00028B3C File Offset: 0x00026D3C
		public float effectFactor
		{
			get
			{
				return this.m_EffectFactor;
			}
			set
			{
				this.m_EffectFactor = Mathf.Clamp(value, 0f, 1f);
				this.SetDirty();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00028B5A File Offset: 0x00026D5A
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00028B62 File Offset: 0x00026D62
		public float colorFactor
		{
			get
			{
				return this.m_ColorFactor;
			}
			set
			{
				this.m_ColorFactor = Mathf.Clamp(value, 0f, 1f);
				this.SetDirty();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00028B80 File Offset: 0x00026D80
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00028B88 File Offset: 0x00026D88
		[Obsolete("Use blurFactor instead (UnityUpgradable) -> blurFactor")]
		public float blur
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 1f);
				this.SetDirty();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00028BA6 File Offset: 0x00026DA6
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00028BAE File Offset: 0x00026DAE
		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float blurFactor
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 1f);
				this.SetDirty();
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00028BCC File Offset: 0x00026DCC
		[Obsolete("Use effectMode instead (UnityUpgradable) -> effectMode")]
		public EffectMode toneMode
		{
			get
			{
				return this.m_EffectMode;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00028BD4 File Offset: 0x00026DD4
		public EffectMode effectMode
		{
			get
			{
				return this.m_EffectMode;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00028BDC File Offset: 0x00026DDC
		public ColorMode colorMode
		{
			get
			{
				return this.m_ColorMode;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00028BE4 File Offset: 0x00026DE4
		public BlurMode blurMode
		{
			get
			{
				return this.m_BlurMode;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00028BEC File Offset: 0x00026DEC
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00028BF9 File Offset: 0x00026DF9
		public Color effectColor
		{
			get
			{
				return base.graphic.color;
			}
			set
			{
				base.graphic.color = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00028C0D File Offset: 0x00026E0D
		public override ParameterTexture ptex
		{
			get
			{
				return UIEffect._ptex;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00028C14 File Offset: 0x00026E14
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			float normalizedIndex = this.ptex.GetNormalizedIndex(this);
			if (this.m_BlurMode != BlurMode.None && this.m_AdvancedBlur)
			{
				vh.GetUIVertexStream(UIEffectBase.tempVerts);
				vh.Clear();
				int count = UIEffectBase.tempVerts.Count;
				int num = (base.targetGraphic is Text) ? 6 : count;
				Rect rect = default(Rect);
				Rect rect2 = default(Rect);
				Vector3 vector = default(Vector3);
				Vector3 vector2 = default(Vector3);
				Vector3 vector3 = default(Vector3);
				float num2 = (float)this.blurMode * 6f * 2f;
				for (int i = 0; i < count; i += num)
				{
					UIEffect.GetBounds(UIEffectBase.tempVerts, i, num, ref rect, ref rect2, true);
					Vector2 v = new Vector2(Packer.ToFloat(rect2.xMin, rect2.yMin), Packer.ToFloat(rect2.xMax, rect2.yMax));
					for (int j = 0; j < num; j += 6)
					{
						Vector3 position = UIEffectBase.tempVerts[i + j + 1].position;
						Vector3 position2 = UIEffectBase.tempVerts[i + j + 4].position;
						bool flag = num == 6 || !rect.Contains(position) || !rect.Contains(position2);
						if (flag)
						{
							Vector3 a = UIEffectBase.tempVerts[i + j + 1].uv0;
							Vector3 b = UIEffectBase.tempVerts[i + j + 4].uv0;
							Vector3 vector4 = (position + position2) / 2f;
							Vector3 vector5 = (a + b) / 2f;
							vector = position - position2;
							vector.x = 1f + num2 / Mathf.Abs(vector.x);
							vector.y = 1f + num2 / Mathf.Abs(vector.y);
							vector.z = 1f + num2 / Mathf.Abs(vector.z);
							vector2 = vector4 - Vector3.Scale(vector, vector4);
							vector3 = vector5 - Vector3.Scale(vector, vector5);
						}
						for (int k = 0; k < 6; k++)
						{
							UIVertex uivertex = UIEffectBase.tempVerts[i + j + k];
							Vector3 position3 = uivertex.position;
							Vector2 vector6 = uivertex.uv0;
							if (flag && (position3.x < rect.xMin || rect.xMax < position3.x))
							{
								position3.x = position3.x * vector.x + vector2.x;
								vector6.x = vector6.x * vector.x + vector3.x;
							}
							if (flag && (position3.y < rect.yMin || rect.yMax < position3.y))
							{
								position3.y = position3.y * vector.y + vector2.y;
								vector6.y = vector6.y * vector.y + vector3.y;
							}
							uivertex.uv0 = new Vector2(Packer.ToFloat((vector6.x + 0.5f) / 2f, (vector6.y + 0.5f) / 2f), normalizedIndex);
							uivertex.position = position3;
							uivertex.uv1 = v;
							UIEffectBase.tempVerts[i + j + k] = uivertex;
						}
					}
				}
				vh.AddUIVertexTriangleStream(UIEffectBase.tempVerts);
				UIEffectBase.tempVerts.Clear();
				return;
			}
			int currentVertCount = vh.currentVertCount;
			UIVertex uivertex2 = default(UIVertex);
			for (int l = 0; l < currentVertCount; l++)
			{
				vh.PopulateUIVertex(ref uivertex2, l);
				Vector2 vector7 = uivertex2.uv0;
				uivertex2.uv0 = new Vector2(Packer.ToFloat((vector7.x + 0.5f) / 2f, (vector7.y + 0.5f) / 2f), normalizedIndex);
				vh.SetUIVertex(uivertex2, l);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002905C File Offset: 0x0002725C
		protected override void SetDirty()
		{
			this.ptex.RegisterMaterial(this.m_EffectMaterial);
			this.ptex.SetData(this, 0, this.m_EffectFactor);
			this.ptex.SetData(this, 1, this.m_ColorFactor);
			this.ptex.SetData(this, 2, this.m_BlurFactor);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000290B4 File Offset: 0x000272B4
		private static void GetBounds(List<UIVertex> verts, int start, int count, ref Rect posBounds, ref Rect uvBounds, bool global)
		{
			Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
			Vector2 vector3 = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 vector4 = new Vector2(float.MinValue, float.MinValue);
			for (int i = start; i < start + count; i++)
			{
				UIVertex uivertex = verts[i];
				Vector2 vector5 = uivertex.uv0;
				Vector3 position = uivertex.position;
				if (vector.x >= position.x && vector.y >= position.y)
				{
					vector = position;
				}
				else if (vector2.x <= position.x && vector2.y <= position.y)
				{
					vector2 = position;
				}
				if (vector3.x >= vector5.x && vector3.y >= vector5.y)
				{
					vector3 = vector5;
				}
				else if (vector4.x <= vector5.x && vector4.y <= vector5.y)
				{
					vector4 = vector5;
				}
			}
			posBounds.Set(vector.x + 0.001f, vector.y + 0.001f, vector2.x - vector.x - 0.002f, vector2.y - vector.y - 0.002f);
			uvBounds.Set(vector3.x, vector3.y, vector4.x - vector3.x, vector4.y - vector3.y);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002923C File Offset: 0x0002743C
		public UIEffect()
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000292B8 File Offset: 0x000274B8
		// Note: this type is marked as 'beforefieldinit'.
		static UIEffect()
		{
		}

		// Token: 0x04000539 RID: 1337
		public const string shaderName = "UI/Hidden/UI-Effect";

		// Token: 0x0400053A RID: 1338
		private static readonly ParameterTexture _ptex = new ParameterTexture(4, 1024, "_ParamTex");

		// Token: 0x0400053B RID: 1339
		[FormerlySerializedAs("m_ToneLevel")]
		[Tooltip("Effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor = 1f;

		// Token: 0x0400053C RID: 1340
		[Tooltip("Color effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ColorFactor = 1f;

		// Token: 0x0400053D RID: 1341
		[FormerlySerializedAs("m_Blur")]
		[Tooltip("How far is the blurring from the graphic.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor = 1f;

		// Token: 0x0400053E RID: 1342
		[FormerlySerializedAs("m_ToneMode")]
		[Tooltip("Effect mode")]
		[SerializeField]
		private EffectMode m_EffectMode;

		// Token: 0x0400053F RID: 1343
		[Tooltip("Color effect mode")]
		[SerializeField]
		private ColorMode m_ColorMode;

		// Token: 0x04000540 RID: 1344
		[Tooltip("Blur effect mode")]
		[SerializeField]
		private BlurMode m_BlurMode;

		// Token: 0x04000541 RID: 1345
		[Tooltip("Advanced blurring remove common artifacts in the blur effect for uGUI.")]
		[SerializeField]
		private bool m_AdvancedBlur;

		// Token: 0x04000542 RID: 1346
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ShadowBlur = 1f;

		// Token: 0x04000543 RID: 1347
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private ShadowStyle m_ShadowStyle;

		// Token: 0x04000544 RID: 1348
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Color m_ShadowColor = Color.black;

		// Token: 0x04000545 RID: 1349
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000546 RID: 1350
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000547 RID: 1351
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Color m_EffectColor = Color.white;

		// Token: 0x04000548 RID: 1352
		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private List<UIShadow.AdditionalShadow> m_AdditionalShadows = new List<UIShadow.AdditionalShadow>();

		// Token: 0x020001CD RID: 461
		public enum BlurEx
		{
			// Token: 0x04000DF9 RID: 3577
			None,
			// Token: 0x04000DFA RID: 3578
			Ex
		}
	}
}
