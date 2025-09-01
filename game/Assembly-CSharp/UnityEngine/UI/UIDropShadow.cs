using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200039D RID: 925
	[AddComponentMenu("UI/Effects/DropShadow", 14)]
	public class UIDropShadow : BaseMeshEffect
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x000B6E10 File Offset: 0x000B5010
		protected UIDropShadow()
		{
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000B6E70 File Offset: 0x000B5070
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x000B6E78 File Offset: 0x000B5078
		public Color effectColor
		{
			get
			{
				return this.shadowColor;
			}
			set
			{
				this.shadowColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000B6E9A File Offset: 0x000B509A
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x000B6EA2 File Offset: 0x000B50A2
		public Vector2 ShadowSpread
		{
			get
			{
				return this.shadowSpread;
			}
			set
			{
				this.shadowSpread = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000B6EC4 File Offset: 0x000B50C4
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x000B6ECC File Offset: 0x000B50CC
		public int Iterations
		{
			get
			{
				return this.iterations;
			}
			set
			{
				this.iterations = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000B6EEE File Offset: 0x000B50EE
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x000B6EF6 File Offset: 0x000B50F6
		public Vector2 EffectDistance
		{
			get
			{
				return this.shadowDistance;
			}
			set
			{
				this.shadowDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000B6F18 File Offset: 0x000B5118
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000B6F20 File Offset: 0x000B5120
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

		// Token: 0x06001E9A RID: 7834 RVA: 0x000B6F44 File Offset: 0x000B5144
		private void DropShadowEffect(List<UIVertex> verts)
		{
			int count = verts.Count;
			List<UIVertex> list = new List<UIVertex>(verts);
			verts.Clear();
			for (int i = 0; i < this.iterations; i++)
			{
				for (int j = 0; j < count; j++)
				{
					UIVertex uivertex = list[j];
					Vector3 position = uivertex.position;
					float num = (float)i / (float)this.iterations;
					position.x *= 1f + this.shadowSpread.x * num * 0.01f;
					position.y *= 1f + this.shadowSpread.y * num * 0.01f;
					position.x += this.shadowDistance.x * num;
					position.y += this.shadowDistance.y * num;
					uivertex.position = position;
					Color32 color = this.shadowColor;
					color.a = (byte)((float)color.a / (float)this.iterations);
					uivertex.color = color;
					verts.Add(uivertex);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				verts.Add(list[k]);
			}
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000B708C File Offset: 0x000B528C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			this.DropShadowEffect(list);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
		}

		// Token: 0x04001ECE RID: 7886
		[SerializeField]
		private Color shadowColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04001ECF RID: 7887
		[SerializeField]
		private Vector2 shadowDistance = new Vector2(1f, -1f);

		// Token: 0x04001ED0 RID: 7888
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04001ED1 RID: 7889
		public int iterations = 5;

		// Token: 0x04001ED2 RID: 7890
		public Vector2 shadowSpread = Vector2.one;
	}
}
