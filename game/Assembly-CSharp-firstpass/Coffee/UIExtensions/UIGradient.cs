using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x02000091 RID: 145
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/MeshEffectForTextMeshPro/UIGradient", 101)]
	public class UIGradient : BaseMeshEffect
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00027851 File Offset: 0x00025A51
		public Graphic targetGraphic
		{
			get
			{
				return base.graphic;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00027859 File Offset: 0x00025A59
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00027861 File Offset: 0x00025A61
		public UIGradient.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (this.m_Direction != value)
				{
					this.m_Direction = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00027879 File Offset: 0x00025A79
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00027881 File Offset: 0x00025A81
		public Color color1
		{
			get
			{
				return this.m_Color1;
			}
			set
			{
				if (this.m_Color1 != value)
				{
					this.m_Color1 = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002789E File Offset: 0x00025A9E
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000278A6 File Offset: 0x00025AA6
		public Color color2
		{
			get
			{
				return this.m_Color2;
			}
			set
			{
				if (this.m_Color2 != value)
				{
					this.m_Color2 = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000278C3 File Offset: 0x00025AC3
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000278CB File Offset: 0x00025ACB
		public Color color3
		{
			get
			{
				return this.m_Color3;
			}
			set
			{
				if (this.m_Color3 != value)
				{
					this.m_Color3 = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000278E8 File Offset: 0x00025AE8
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000278F0 File Offset: 0x00025AF0
		public Color color4
		{
			get
			{
				return this.m_Color4;
			}
			set
			{
				if (this.m_Color4 != value)
				{
					this.m_Color4 = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0002790D File Offset: 0x00025B0D
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00027932 File Offset: 0x00025B32
		public float rotation
		{
			get
			{
				if (this.m_Direction == UIGradient.Direction.Horizontal)
				{
					return -90f;
				}
				if (this.m_Direction != UIGradient.Direction.Vertical)
				{
					return this.m_Rotation;
				}
				return 0f;
			}
			set
			{
				if (!Mathf.Approximately(this.m_Rotation, value))
				{
					this.m_Rotation = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002794F File Offset: 0x00025B4F
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00027957 File Offset: 0x00025B57
		public float offset
		{
			get
			{
				return this.m_Offset1;
			}
			set
			{
				if (this.m_Offset1 != value)
				{
					this.m_Offset1 = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0002796F File Offset: 0x00025B6F
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00027982 File Offset: 0x00025B82
		public Vector2 offset2
		{
			get
			{
				return new Vector2(this.m_Offset2, this.m_Offset1);
			}
			set
			{
				if (this.m_Offset1 != value.y || this.m_Offset2 != value.x)
				{
					this.m_Offset1 = value.y;
					this.m_Offset2 = value.x;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000279BE File Offset: 0x00025BBE
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000279C6 File Offset: 0x00025BC6
		public UIGradient.GradientStyle gradientStyle
		{
			get
			{
				return this.m_GradientStyle;
			}
			set
			{
				if (this.m_GradientStyle != value)
				{
					this.m_GradientStyle = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000279DE File Offset: 0x00025BDE
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x000279E6 File Offset: 0x00025BE6
		public ColorSpace colorSpace
		{
			get
			{
				return this.m_ColorSpace;
			}
			set
			{
				if (this.m_ColorSpace != value)
				{
					this.m_ColorSpace = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000279FE File Offset: 0x00025BFE
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00027A06 File Offset: 0x00025C06
		public bool ignoreAspectRatio
		{
			get
			{
				return this.m_IgnoreAspectRatio;
			}
			set
			{
				if (this.m_IgnoreAspectRatio != value)
				{
					this.m_IgnoreAspectRatio = value;
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00027A20 File Offset: 0x00025C20
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			Rect rect = default(Rect);
			UIVertex uivertex = default(UIVertex);
			if (this.m_GradientStyle == UIGradient.GradientStyle.Rect)
			{
				rect = base.graphic.rectTransform.rect;
			}
			else if (this.m_GradientStyle == UIGradient.GradientStyle.Split)
			{
				rect.Set(0f, 0f, 1f, 1f);
			}
			else if (this.m_GradientStyle == UIGradient.GradientStyle.Fit)
			{
				rect.xMin = (rect.yMin = float.MaxValue);
				rect.xMax = (rect.yMax = float.MinValue);
				for (int i = 0; i < vh.currentVertCount; i++)
				{
					vh.PopulateUIVertex(ref uivertex, i);
					rect.xMin = Mathf.Min(rect.xMin, uivertex.position.x);
					rect.yMin = Mathf.Min(rect.yMin, uivertex.position.y);
					rect.xMax = Mathf.Max(rect.xMax, uivertex.position.x);
					rect.yMax = Mathf.Max(rect.yMax, uivertex.position.y);
				}
			}
			float f = this.rotation * 0.017453292f;
			Vector2 normalized = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
			if (!this.m_IgnoreAspectRatio && UIGradient.Direction.Angle <= this.m_Direction)
			{
				normalized.x *= rect.height / rect.width;
				normalized = normalized.normalized;
			}
			UIGradient.Matrix2x3 m = new UIGradient.Matrix2x3(rect, normalized.x, normalized.y);
			for (int j = 0; j < vh.currentVertCount; j++)
			{
				vh.PopulateUIVertex(ref uivertex, j);
				Vector2 vector;
				if (this.m_GradientStyle == UIGradient.GradientStyle.Split)
				{
					vector = m * UIGradient.s_SplitedCharacterPosition[j % 4] + this.offset2;
				}
				else
				{
					vector = m * uivertex.position + this.offset2;
				}
				Color color;
				if (this.direction == UIGradient.Direction.Diagonal)
				{
					color = Color.LerpUnclamped(Color.LerpUnclamped(this.m_Color1, this.m_Color2, vector.x), Color.LerpUnclamped(this.m_Color3, this.m_Color4, vector.x), vector.y);
				}
				else
				{
					color = Color.LerpUnclamped(this.m_Color2, this.m_Color1, vector.y);
				}
				uivertex.color *= ((this.m_ColorSpace == ColorSpace.Gamma) ? color.gamma : ((this.m_ColorSpace == ColorSpace.Linear) ? color.linear : color));
				vh.SetUIVertex(uivertex, j);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00027CF0 File Offset: 0x00025EF0
		public UIGradient()
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00027D3D File Offset: 0x00025F3D
		// Note: this type is marked as 'beforefieldinit'.
		static UIGradient()
		{
		}

		// Token: 0x040004F2 RID: 1266
		[Tooltip("Gradient Direction.")]
		[SerializeField]
		private UIGradient.Direction m_Direction;

		// Token: 0x040004F3 RID: 1267
		[Tooltip("Color1: Top or Left.")]
		[SerializeField]
		private Color m_Color1 = Color.white;

		// Token: 0x040004F4 RID: 1268
		[Tooltip("Color2: Bottom or Right.")]
		[SerializeField]
		private Color m_Color2 = Color.white;

		// Token: 0x040004F5 RID: 1269
		[Tooltip("Color3: For diagonal.")]
		[SerializeField]
		private Color m_Color3 = Color.white;

		// Token: 0x040004F6 RID: 1270
		[Tooltip("Color4: For diagonal.")]
		[SerializeField]
		private Color m_Color4 = Color.white;

		// Token: 0x040004F7 RID: 1271
		[Tooltip("Gradient rotation.")]
		[SerializeField]
		[Range(-180f, 180f)]
		private float m_Rotation;

		// Token: 0x040004F8 RID: 1272
		[Tooltip("Gradient offset for Horizontal, Vertical or Angle.")]
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Offset1;

		// Token: 0x040004F9 RID: 1273
		[Tooltip("Gradient offset for Diagonal.")]
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Offset2;

		// Token: 0x040004FA RID: 1274
		[Tooltip("Gradient style for Text.")]
		[SerializeField]
		private UIGradient.GradientStyle m_GradientStyle;

		// Token: 0x040004FB RID: 1275
		[Tooltip("Color space to correct color.")]
		[SerializeField]
		private ColorSpace m_ColorSpace = ColorSpace.Uninitialized;

		// Token: 0x040004FC RID: 1276
		[Tooltip("Ignore aspect ratio.")]
		[SerializeField]
		private bool m_IgnoreAspectRatio = true;

		// Token: 0x040004FD RID: 1277
		private static readonly Vector2[] s_SplitedCharacterPosition = new Vector2[]
		{
			Vector2.up,
			Vector2.one,
			Vector2.right,
			Vector2.zero
		};

		// Token: 0x020001C5 RID: 453
		public enum Direction
		{
			// Token: 0x04000DDF RID: 3551
			Horizontal,
			// Token: 0x04000DE0 RID: 3552
			Vertical,
			// Token: 0x04000DE1 RID: 3553
			Angle,
			// Token: 0x04000DE2 RID: 3554
			Diagonal
		}

		// Token: 0x020001C6 RID: 454
		public enum GradientStyle
		{
			// Token: 0x04000DE4 RID: 3556
			Rect,
			// Token: 0x04000DE5 RID: 3557
			Fit,
			// Token: 0x04000DE6 RID: 3558
			Split
		}

		// Token: 0x020001C7 RID: 455
		private struct Matrix2x3
		{
			// Token: 0x06000F99 RID: 3993 RVA: 0x0006392C File Offset: 0x00061B2C
			public Matrix2x3(Rect rect, float cos, float sin)
			{
				float num = -rect.xMin / rect.width - 0.5f;
				float num2 = -rect.yMin / rect.height - 0.5f;
				this.m00 = cos / rect.width;
				this.m01 = -sin / rect.height;
				this.m02 = num * cos - num2 * sin + 0.5f;
				this.m10 = sin / rect.width;
				this.m11 = cos / rect.height;
				this.m12 = num * sin + num2 * cos + 0.5f;
			}

			// Token: 0x06000F9A RID: 3994 RVA: 0x000639CC File Offset: 0x00061BCC
			public static Vector2 operator *(UIGradient.Matrix2x3 m, Vector2 v)
			{
				return new Vector2(m.m00 * v.x + m.m01 * v.y + m.m02, m.m10 * v.x + m.m11 * v.y + m.m12);
			}

			// Token: 0x04000DE7 RID: 3559
			public float m00;

			// Token: 0x04000DE8 RID: 3560
			public float m01;

			// Token: 0x04000DE9 RID: 3561
			public float m02;

			// Token: 0x04000DEA RID: 3562
			public float m10;

			// Token: 0x04000DEB RID: 3563
			public float m11;

			// Token: 0x04000DEC RID: 3564
			public float m12;
		}
	}
}
