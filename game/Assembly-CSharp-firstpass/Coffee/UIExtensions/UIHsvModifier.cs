using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200009E RID: 158
	[AddComponentMenu("UI/UIEffect/UIHsvModifier", 4)]
	public class UIHsvModifier : UIEffectBase
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00029A54 File Offset: 0x00027C54
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00029A5C File Offset: 0x00027C5C
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				if (this.m_TargetColor != value)
				{
					this.m_TargetColor = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00029A79 File Offset: 0x00027C79
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00029A81 File Offset: 0x00027C81
		public float range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				if (!Mathf.Approximately(this.m_Range, value))
				{
					this.m_Range = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00029AB0 File Offset: 0x00027CB0
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public float saturation
		{
			get
			{
				return this.m_Saturation;
			}
			set
			{
				value = Mathf.Clamp(value, -0.5f, 0.5f);
				if (!Mathf.Approximately(this.m_Saturation, value))
				{
					this.m_Saturation = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00029AE7 File Offset: 0x00027CE7
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00029AEF File Offset: 0x00027CEF
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				value = Mathf.Clamp(value, -0.5f, 0.5f);
				if (!Mathf.Approximately(this.m_Value, value))
				{
					this.m_Value = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00029B1E File Offset: 0x00027D1E
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00029B26 File Offset: 0x00027D26
		public float hue
		{
			get
			{
				return this.m_Hue;
			}
			set
			{
				value = Mathf.Clamp(value, -0.5f, 0.5f);
				if (!Mathf.Approximately(this.m_Hue, value))
				{
					this.m_Hue = value;
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00029B55 File Offset: 0x00027D55
		public override ParameterTexture ptex
		{
			get
			{
				return UIHsvModifier._ptex;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00029B5C File Offset: 0x00027D5C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			float normalizedIndex = this.ptex.GetNormalizedIndex(this);
			UIVertex uivertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				uivertex.uv0 = new Vector2(Packer.ToFloat(uivertex.uv0.x, uivertex.uv0.y), normalizedIndex);
				vh.SetUIVertex(uivertex, i);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00029BD8 File Offset: 0x00027DD8
		protected override void SetDirty()
		{
			float value;
			float value2;
			float value3;
			Color.RGBToHSV(this.m_TargetColor, out value, out value2, out value3);
			this.ptex.RegisterMaterial(base.targetGraphic.material);
			this.ptex.SetData(this, 0, value);
			this.ptex.SetData(this, 1, value2);
			this.ptex.SetData(this, 2, value3);
			this.ptex.SetData(this, 3, this.m_Range);
			this.ptex.SetData(this, 4, this.m_Hue + 0.5f);
			this.ptex.SetData(this, 5, this.m_Saturation + 0.5f);
			this.ptex.SetData(this, 6, this.m_Value + 0.5f);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00029C94 File Offset: 0x00027E94
		public UIHsvModifier()
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00029CB2 File Offset: 0x00027EB2
		// Note: this type is marked as 'beforefieldinit'.
		static UIHsvModifier()
		{
		}

		// Token: 0x04000560 RID: 1376
		public const string shaderName = "UI/Hidden/UI-Effect-HSV";

		// Token: 0x04000561 RID: 1377
		private static readonly ParameterTexture _ptex = new ParameterTexture(7, 128, "_ParamTex");

		// Token: 0x04000562 RID: 1378
		[Header("Target")]
		[Tooltip("Target color to affect hsv shift.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_TargetColor = Color.red;

		// Token: 0x04000563 RID: 1379
		[Tooltip("Color range to affect hsv shift [0 ~ 1].")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Range = 0.1f;

		// Token: 0x04000564 RID: 1380
		[Header("Adjustment")]
		[Tooltip("Hue shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Hue;

		// Token: 0x04000565 RID: 1381
		[Tooltip("Saturation shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Saturation;

		// Token: 0x04000566 RID: 1382
		[Tooltip("Value shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Value;
	}
}
