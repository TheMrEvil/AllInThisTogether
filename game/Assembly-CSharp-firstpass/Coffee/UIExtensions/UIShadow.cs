using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("UI/MeshEffectForTextMeshPro/UIShadow", 100)]
	public class UIShadow : BaseMeshEffect
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00027D7A File Offset: 0x00025F7A
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00027D82 File Offset: 0x00025F82
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00027DA4 File Offset: 0x00025FA4
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00027DAC File Offset: 0x00025FAC
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00027E4C File Offset: 0x0002604C
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00027E54 File Offset: 0x00026054
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00027E76 File Offset: 0x00026076
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00027E7E File Offset: 0x0002607E
		[Obsolete("Use blurFactor instead (UnityUpgradable) -> blurFactor")]
		public float blur
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 2f);
				this._SetDirty();
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00027E9C File Offset: 0x0002609C
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00027EA4 File Offset: 0x000260A4
		public float blurFactor
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 2f);
				this._SetDirty();
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00027EC2 File Offset: 0x000260C2
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00027ECA File Offset: 0x000260CA
		public ShadowStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this._SetDirty();
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00027ED9 File Offset: 0x000260D9
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00027EE1 File Offset: 0x000260E1
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00027EEC File Offset: 0x000260EC
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled || vh.currentVertCount <= 0 || this.m_Style == ShadowStyle.None)
			{
				return;
			}
			vh.GetUIVertexStream(UIShadow.s_Verts);
			base.GetComponents<UIShadow>(UIShadow.tmpShadows);
			foreach (UIShadow uishadow in UIShadow.tmpShadows)
			{
				if (uishadow.isActiveAndEnabled)
				{
					if (!(uishadow == this))
					{
						break;
					}
					using (List<UIShadow>.Enumerator enumerator2 = UIShadow.tmpShadows.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							UIShadow uishadow2 = enumerator2.Current;
							uishadow2._graphicVertexCount = UIShadow.s_Verts.Count;
						}
						break;
					}
				}
			}
			UIShadow.tmpShadows.Clear();
			int num = UIShadow.s_Verts.Count - this._graphicVertexCount;
			int count = UIShadow.s_Verts.Count;
			this._ApplyShadow(UIShadow.s_Verts, this.effectColor, ref num, ref count, this.effectDistance, this.style, this.useGraphicAlpha);
			vh.Clear();
			vh.AddUIVertexTriangleStream(UIShadow.s_Verts);
			UIShadow.s_Verts.Clear();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002802C File Offset: 0x0002622C
		private void _ApplyShadow(List<UIVertex> verts, Color color, ref int start, ref int end, Vector2 effectDistance, ShadowStyle style, bool useGraphicAlpha)
		{
			if (style == ShadowStyle.None || color.a <= 0f)
			{
				return;
			}
			this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, effectDistance.x, effectDistance.y, useGraphicAlpha);
			if (ShadowStyle.Shadow3 == style)
			{
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, effectDistance.x, 0f, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, 0f, effectDistance.y, useGraphicAlpha);
				return;
			}
			if (ShadowStyle.Outline == style)
			{
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, effectDistance.x, -effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, -effectDistance.x, effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, -effectDistance.x, -effectDistance.y, useGraphicAlpha);
				return;
			}
			if (ShadowStyle.Outline8 == style)
			{
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, effectDistance.x, -effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, -effectDistance.x, effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, -effectDistance.x, -effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, -effectDistance.x, 0f, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, 0f, -effectDistance.y, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, effectDistance.x, 0f, useGraphicAlpha);
				this._ApplyShadowZeroAlloc(UIShadow.s_Verts, color, ref start, ref end, 0f, effectDistance.y, useGraphicAlpha);
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000281F0 File Offset: 0x000263F0
		private void _ApplyShadowZeroAlloc(List<UIVertex> verts, Color color, ref int start, ref int end, float x, float y, bool useGraphicAlpha)
		{
			int i = end - start;
			int num = verts.Count + i;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			UIVertex uivertex = default(UIVertex);
			for (int j = 0; j < i; j++)
			{
				verts.Add(uivertex);
			}
			int num2 = verts.Count - 1;
			while (i <= num2)
			{
				verts[num2] = verts[num2 - i];
				num2--;
			}
			for (int k = 0; k < i; k++)
			{
				uivertex = verts[k + start + i];
				Vector3 position = uivertex.position;
				uivertex.position.Set(position.x + x, position.y + y, position.z);
				Color effectColor = this.effectColor;
				effectColor.a = (useGraphicAlpha ? (color.a * (float)uivertex.color.a / 255f) : color.a);
				uivertex.color = effectColor;
				verts[k] = uivertex;
			}
			start = end;
			end = verts.Count;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002830D File Offset: 0x0002650D
		private void _SetDirty()
		{
			if (base.graphic)
			{
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00028328 File Offset: 0x00026528
		public UIShadow()
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00028393 File Offset: 0x00026593
		// Note: this type is marked as 'beforefieldinit'.
		static UIShadow()
		{
		}

		// Token: 0x040004FE RID: 1278
		[Tooltip("How far is the blurring shadow from the graphic.")]
		[FormerlySerializedAs("m_Blur")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor = 1f;

		// Token: 0x040004FF RID: 1279
		[Tooltip("Shadow effect style.")]
		[SerializeField]
		private ShadowStyle m_Style = ShadowStyle.Shadow;

		// Token: 0x04000500 RID: 1280
		[HideInInspector]
		[Obsolete]
		[SerializeField]
		private List<UIShadow.AdditionalShadow> m_AdditionalShadows = new List<UIShadow.AdditionalShadow>();

		// Token: 0x04000501 RID: 1281
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000502 RID: 1282
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000503 RID: 1283
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000504 RID: 1284
		private const float kMaxEffectDistance = 600f;

		// Token: 0x04000505 RID: 1285
		private int _graphicVertexCount;

		// Token: 0x04000506 RID: 1286
		private static readonly List<UIShadow> tmpShadows = new List<UIShadow>();

		// Token: 0x04000507 RID: 1287
		private static readonly List<UIVertex> s_Verts = new List<UIVertex>();

		// Token: 0x020001C8 RID: 456
		[Obsolete]
		[Serializable]
		public class AdditionalShadow
		{
			// Token: 0x06000F9B RID: 3995 RVA: 0x00063A24 File Offset: 0x00061C24
			public AdditionalShadow()
			{
			}

			// Token: 0x04000DED RID: 3565
			[FormerlySerializedAs("shadowBlur")]
			[Range(0f, 1f)]
			public float blur = 0.25f;

			// Token: 0x04000DEE RID: 3566
			[FormerlySerializedAs("shadowMode")]
			public ShadowStyle style = ShadowStyle.Shadow;

			// Token: 0x04000DEF RID: 3567
			[FormerlySerializedAs("shadowColor")]
			public Color effectColor = Color.black;

			// Token: 0x04000DF0 RID: 3568
			public Vector2 effectDistance = new Vector2(1f, -1f);

			// Token: 0x04000DF1 RID: 3569
			public bool useGraphicAlpha = true;
		}
	}
}
