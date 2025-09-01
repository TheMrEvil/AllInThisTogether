using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000387 RID: 903
	public struct StyleValues
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0008993C File Offset: 0x00087B3C
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00089966 File Offset: 0x00087B66
		public float top
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Top).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Top, value);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00089978 File Offset: 0x00087B78
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x000899A2 File Offset: 0x00087BA2
		public float left
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Left).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Left, value);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x000899B4 File Offset: 0x00087BB4
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x000899DE File Offset: 0x00087BDE
		public float width
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Width).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Width, value);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x000899F0 File Offset: 0x00087BF0
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x00089A1A File Offset: 0x00087C1A
		public float height
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Height).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Height, value);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00089A2C File Offset: 0x00087C2C
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x00089A56 File Offset: 0x00087C56
		public float right
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Right).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Right, value);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00089A68 File Offset: 0x00087C68
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00089A92 File Offset: 0x00087C92
		public float bottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Bottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Bottom, value);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00089AA4 File Offset: 0x00087CA4
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00089ACE File Offset: 0x00087CCE
		public Color color
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.Color).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Color, value);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00089AE0 File Offset: 0x00087CE0
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00089B0A File Offset: 0x00087D0A
		public Color backgroundColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BackgroundColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BackgroundColor, value);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00089B1C File Offset: 0x00087D1C
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x00089B46 File Offset: 0x00087D46
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.UnityBackgroundImageTintColor, value);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00089B58 File Offset: 0x00087D58
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00089B82 File Offset: 0x00087D82
		public Color borderColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BorderColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderColor, value);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00089B94 File Offset: 0x00087D94
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x00089BBE File Offset: 0x00087DBE
		public float marginLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginLeft, value);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00089BD0 File Offset: 0x00087DD0
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x00089BFA File Offset: 0x00087DFA
		public float marginTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginTop, value);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00089C0C File Offset: 0x00087E0C
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x00089C36 File Offset: 0x00087E36
		public float marginRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginRight, value);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00089C48 File Offset: 0x00087E48
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x00089C72 File Offset: 0x00087E72
		public float marginBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginBottom, value);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00089C84 File Offset: 0x00087E84
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x00089CAE File Offset: 0x00087EAE
		public float paddingLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingLeft, value);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00089CC0 File Offset: 0x00087EC0
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x00089CEA File Offset: 0x00087EEA
		public float paddingTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingTop, value);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00089CFC File Offset: 0x00087EFC
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x00089D26 File Offset: 0x00087F26
		public float paddingRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingRight, value);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00089D38 File Offset: 0x00087F38
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00089D62 File Offset: 0x00087F62
		public float paddingBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingBottom, value);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00089D74 File Offset: 0x00087F74
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x00089D9E File Offset: 0x00087F9E
		public float borderLeftWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderLeftWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderLeftWidth, value);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x00089DB0 File Offset: 0x00087FB0
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x00089DDA File Offset: 0x00087FDA
		public float borderRightWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderRightWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderRightWidth, value);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x00089DEC File Offset: 0x00087FEC
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x00089E16 File Offset: 0x00088016
		public float borderTopWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopWidth, value);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00089E28 File Offset: 0x00088028
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00089E52 File Offset: 0x00088052
		public float borderBottomWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomWidth, value);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00089E64 File Offset: 0x00088064
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x00089E8E File Offset: 0x0008808E
		public float borderTopLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopLeftRadius, value);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00089EA0 File Offset: 0x000880A0
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00089ECA File Offset: 0x000880CA
		public float borderTopRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopRightRadius, value);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00089EDC File Offset: 0x000880DC
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x00089F06 File Offset: 0x00088106
		public float borderBottomLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomLeftRadius, value);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00089F18 File Offset: 0x00088118
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x00089F42 File Offset: 0x00088142
		public float borderBottomRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomRightRadius, value);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00089F54 File Offset: 0x00088154
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x00089F7E File Offset: 0x0008817E
		public float opacity
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Opacity).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Opacity, value);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00089F90 File Offset: 0x00088190
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x00089FBA File Offset: 0x000881BA
		public float flexGrow
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexGrow).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00089FCC File Offset: 0x000881CC
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x00089FBA File Offset: 0x000881BA
		public float flexShrink
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexShrink).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00089FF8 File Offset: 0x000881F8
		internal void SetValue(StylePropertyId id, float value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.number = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0008A02C File Offset: 0x0008822C
		internal void SetValue(StylePropertyId id, Color value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.color = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0008A060 File Offset: 0x00088260
		internal StyleValueCollection Values()
		{
			bool flag = this.m_StyleValues == null;
			if (flag)
			{
				this.m_StyleValues = new StyleValueCollection();
			}
			return this.m_StyleValues;
		}

		// Token: 0x04000EA1 RID: 3745
		internal StyleValueCollection m_StyleValues;
	}
}
