using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000044 RID: 68
	[AddComponentMenu("UI/Effects/Shadow", 80)]
	public class Shadow : BaseMeshEffect
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00016928 File Offset: 0x00014B28
		protected Shadow()
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00016976 File Offset: 0x00014B76
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001697E File Offset: 0x00014B7E
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000169A0 File Offset: 0x00014BA0
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000169A8 File Offset: 0x00014BA8
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00016A48 File Offset: 0x00014C48
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00016A50 File Offset: 0x00014C50
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

		// Token: 0x060004B8 RID: 1208 RVA: 0x00016A74 File Offset: 0x00014C74
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count + end - start;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00016B28 File Offset: 0x00014D28
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00016B3C File Offset: 0x00014D3C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = CollectionPool<List<UIVertex>, UIVertex>.Get();
			vh.GetUIVertexStream(list);
			this.ApplyShadow(list, this.effectColor, 0, list.Count, this.effectDistance.x, this.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			CollectionPool<List<UIVertex>, UIVertex>.Release(list);
		}

		// Token: 0x04000191 RID: 401
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000192 RID: 402
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000193 RID: 403
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000194 RID: 404
		private const float kMaxEffectDistance = 600f;
	}
}
