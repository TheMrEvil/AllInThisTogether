using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x02000090 RID: 144
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/MeshEffectForTextMeshPro/UIFlip", 102)]
	public class UIFlip : BaseMeshEffect
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00027787 File Offset: 0x00025987
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0002778F File Offset: 0x0002598F
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002779E File Offset: 0x0002599E
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x000277A6 File Offset: 0x000259A6
		public bool vertical
		{
			get
			{
				return this.m_Veritical;
			}
			set
			{
				this.m_Veritical = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000277B8 File Offset: 0x000259B8
		public override void ModifyMesh(VertexHelper vh)
		{
			RectTransform rectTransform = base.graphic.rectTransform;
			UIVertex uivertex = default(UIVertex);
			Vector2 center = rectTransform.rect.center;
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				Vector3 position = uivertex.position;
				uivertex.position = new Vector3(this.m_Horizontal ? (-position.x) : position.x, this.m_Veritical ? (-position.y) : position.y);
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00027849 File Offset: 0x00025A49
		public UIFlip()
		{
		}

		// Token: 0x040004F0 RID: 1264
		[Tooltip("Flip horizontally.")]
		[SerializeField]
		private bool m_Horizontal;

		// Token: 0x040004F1 RID: 1265
		[Tooltip("Flip vertically.")]
		[SerializeField]
		private bool m_Veritical;
	}
}
