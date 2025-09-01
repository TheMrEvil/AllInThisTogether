using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000066 RID: 102
	public abstract class TMP_Text : MaskableGraphic
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0002784B File Offset: 0x00025A4B
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00027864 File Offset: 0x00025A64
		public virtual string text
		{
			get
			{
				if (this.m_IsTextBackingStringDirty)
				{
					return this.InternalTextBackingArrayToString();
				}
				return this.m_text;
			}
			set
			{
				if (!this.m_IsTextBackingStringDirty && this.m_text != null && value != null && this.m_text.Length == value.Length && this.m_text == value)
				{
					return;
				}
				this.m_IsTextBackingStringDirty = false;
				this.m_text = value;
				this.m_inputSource = TMP_Text.TextInputSources.TextString;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000278CE File Offset: 0x00025ACE
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x000278D6 File Offset: 0x00025AD6
		public ITextPreprocessor textPreprocessor
		{
			get
			{
				return this.m_TextPreprocessor;
			}
			set
			{
				this.m_TextPreprocessor = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000278DF File Offset: 0x00025ADF
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x000278E7 File Offset: 0x00025AE7
		public bool isRightToLeftText
		{
			get
			{
				return this.m_isRightToLeft;
			}
			set
			{
				if (this.m_isRightToLeft == value)
				{
					return;
				}
				this.m_isRightToLeft = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0002790D File Offset: 0x00025B0D
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00027915 File Offset: 0x00025B15
		public TMP_FontAsset font
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				if (this.m_fontAsset == value)
				{
					return;
				}
				this.m_fontAsset = value;
				this.LoadFontAsset();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00027946 File Offset: 0x00025B46
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0002794E File Offset: 0x00025B4E
		public virtual Material fontSharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				if (this.m_sharedMaterial == value)
				{
					return;
				}
				this.SetSharedMaterial(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00027979 File Offset: 0x00025B79
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00027981 File Offset: 0x00025B81
		public virtual Material[] fontSharedMaterials
		{
			get
			{
				return this.GetSharedMaterials();
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0002799D File Offset: 0x00025B9D
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000279AC File Offset: 0x00025BAC
		public Material fontMaterial
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00027A01 File Offset: 0x00025C01
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00027A0F File Offset: 0x00025C0F
		public virtual Material[] fontMaterials
		{
			get
			{
				return this.GetMaterials(this.m_fontSharedMaterials);
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00027A2B File Offset: 0x00025C2B
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00027A33 File Offset: 0x00025C33
		public override Color color
		{
			get
			{
				return this.m_fontColor;
			}
			set
			{
				if (this.m_fontColor == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_fontColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00027A58 File Offset: 0x00025C58
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00027A65 File Offset: 0x00025C65
		public float alpha
		{
			get
			{
				return this.m_fontColor.a;
			}
			set
			{
				if (this.m_fontColor.a == value)
				{
					return;
				}
				this.m_fontColor.a = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00027A8F File Offset: 0x00025C8F
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00027A97 File Offset: 0x00025C97
		public bool enableVertexGradient
		{
			get
			{
				return this.m_enableVertexGradient;
			}
			set
			{
				if (this.m_enableVertexGradient == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableVertexGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00027AB7 File Offset: 0x00025CB7
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00027ABF File Offset: 0x00025CBF
		public VertexGradient colorGradient
		{
			get
			{
				return this.m_fontColorGradient;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00027AD5 File Offset: 0x00025CD5
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00027ADD File Offset: 0x00025CDD
		public TMP_ColorGradient colorGradientPreset
		{
			get
			{
				return this.m_fontColorGradientPreset;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradientPreset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00027AF3 File Offset: 0x00025CF3
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00027AFB File Offset: 0x00025CFB
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00027B17 File Offset: 0x00025D17
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00027B1F File Offset: 0x00025D1F
		public bool tintAllSprites
		{
			get
			{
				return this.m_tintAllSprites;
			}
			set
			{
				if (this.m_tintAllSprites == value)
				{
					return;
				}
				this.m_tintAllSprites = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00027B3F File Offset: 0x00025D3F
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00027B47 File Offset: 0x00025D47
		public TMP_StyleSheet styleSheet
		{
			get
			{
				return this.m_StyleSheet;
			}
			set
			{
				this.m_StyleSheet = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00027B63 File Offset: 0x00025D63
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00027BA1 File Offset: 0x00025DA1
		public TMP_Style textStyle
		{
			get
			{
				this.m_TextStyle = this.GetStyle(this.m_TextStyleHashCode);
				if (this.m_TextStyle == null)
				{
					this.m_TextStyle = TMP_Style.NormalStyle;
					this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				}
				return this.m_TextStyle;
			}
			set
			{
				this.m_TextStyle = value;
				this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00027BCE File Offset: 0x00025DCE
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00027BD6 File Offset: 0x00025DD6
		public bool overrideColorTags
		{
			get
			{
				return this.m_overrideHtmlColors;
			}
			set
			{
				if (this.m_overrideHtmlColors == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_overrideHtmlColors = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00027BF6 File Offset: 0x00025DF6
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00027C2E File Offset: 0x00025E2E
		public Color32 faceColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_faceColor;
				}
				this.m_faceColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_FaceColor);
				return this.m_faceColor;
			}
			set
			{
				if (this.m_faceColor.Compare(value))
				{
					return;
				}
				this.SetFaceColor(value);
				this.m_havePropertiesChanged = true;
				this.m_faceColor = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00027C60 File Offset: 0x00025E60
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00027C98 File Offset: 0x00025E98
		public Color32 outlineColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineColor;
				}
				this.m_outlineColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
				return this.m_outlineColor;
			}
			set
			{
				if (this.m_outlineColor.Compare(value))
				{
					return;
				}
				this.SetOutlineColor(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00027CC4 File Offset: 0x00025EC4
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00027CF7 File Offset: 0x00025EF7
		public float outlineWidth
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineWidth;
				}
				this.m_outlineWidth = this.m_sharedMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
				return this.m_outlineWidth;
			}
			set
			{
				if (this.m_outlineWidth == value)
				{
					return;
				}
				this.SetOutlineThickness(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineWidth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00027D1E File Offset: 0x00025F1E
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00027D26 File Offset: 0x00025F26
		public float fontSize
		{
			get
			{
				return this.m_fontSize;
			}
			set
			{
				if (this.m_fontSize == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_fontSize = value;
				if (!this.m_enableAutoSizing)
				{
					this.m_fontSizeBase = this.m_fontSize;
				}
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00027D60 File Offset: 0x00025F60
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00027D68 File Offset: 0x00025F68
		public FontWeight fontWeight
		{
			get
			{
				return this.m_fontWeight;
			}
			set
			{
				if (this.m_fontWeight == value)
				{
					return;
				}
				this.m_fontWeight = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00027D90 File Offset: 0x00025F90
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font)
				{
					return canvas.scaleFactor;
				}
				if (this.m_currentFontAsset == null || this.m_currentFontAsset.faceInfo.pointSize <= 0 || this.m_fontSize <= 0f)
				{
					return 1f;
				}
				return this.m_fontSize / (float)this.m_currentFontAsset.faceInfo.pointSize;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00027E18 File Offset: 0x00026018
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00027E20 File Offset: 0x00026020
		public bool enableAutoSizing
		{
			get
			{
				return this.m_enableAutoSizing;
			}
			set
			{
				if (this.m_enableAutoSizing == value)
				{
					return;
				}
				this.m_enableAutoSizing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00027E3F File Offset: 0x0002603F
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00027E47 File Offset: 0x00026047
		public float fontSizeMin
		{
			get
			{
				return this.m_fontSizeMin;
			}
			set
			{
				if (this.m_fontSizeMin == value)
				{
					return;
				}
				this.m_fontSizeMin = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00027E66 File Offset: 0x00026066
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00027E6E File Offset: 0x0002606E
		public float fontSizeMax
		{
			get
			{
				return this.m_fontSizeMax;
			}
			set
			{
				if (this.m_fontSizeMax == value)
				{
					return;
				}
				this.m_fontSizeMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00027E8D File Offset: 0x0002608D
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00027E95 File Offset: 0x00026095
		public FontStyles fontStyle
		{
			get
			{
				return this.m_fontStyle;
			}
			set
			{
				if (this.m_fontStyle == value)
				{
					return;
				}
				this.m_fontStyle = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00027EBB File Offset: 0x000260BB
		public bool isUsingBold
		{
			get
			{
				return this.m_isUsingBold;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00027EC3 File Offset: 0x000260C3
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00027ECB File Offset: 0x000260CB
		public HorizontalAlignmentOptions horizontalAlignment
		{
			get
			{
				return this.m_HorizontalAlignment;
			}
			set
			{
				if (this.m_HorizontalAlignment == value)
				{
					return;
				}
				this.m_HorizontalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00027EEB File Offset: 0x000260EB
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00027EF3 File Offset: 0x000260F3
		public VerticalAlignmentOptions verticalAlignment
		{
			get
			{
				return this.m_VerticalAlignment;
			}
			set
			{
				if (this.m_VerticalAlignment == value)
				{
					return;
				}
				this.m_VerticalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00027F13 File Offset: 0x00026113
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00027F24 File Offset: 0x00026124
		public TextAlignmentOptions alignment
		{
			get
			{
				return (TextAlignmentOptions)(this.m_HorizontalAlignment | (HorizontalAlignmentOptions)this.m_VerticalAlignment);
			}
			set
			{
				HorizontalAlignmentOptions horizontalAlignmentOptions = (HorizontalAlignmentOptions)(value & (TextAlignmentOptions)255);
				VerticalAlignmentOptions verticalAlignmentOptions = (VerticalAlignmentOptions)(value & (TextAlignmentOptions)65280);
				if (this.m_HorizontalAlignment == horizontalAlignmentOptions && this.m_VerticalAlignment == verticalAlignmentOptions)
				{
					return;
				}
				this.m_HorizontalAlignment = horizontalAlignmentOptions;
				this.m_VerticalAlignment = verticalAlignmentOptions;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00027F6F File Offset: 0x0002616F
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00027F77 File Offset: 0x00026177
		public float characterSpacing
		{
			get
			{
				return this.m_characterSpacing;
			}
			set
			{
				if (this.m_characterSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_characterSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00027F9D File Offset: 0x0002619D
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00027FA5 File Offset: 0x000261A5
		public float wordSpacing
		{
			get
			{
				return this.m_wordSpacing;
			}
			set
			{
				if (this.m_wordSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_wordSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00027FCB File Offset: 0x000261CB
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00027FD3 File Offset: 0x000261D3
		public float lineSpacing
		{
			get
			{
				return this.m_lineSpacing;
			}
			set
			{
				if (this.m_lineSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00027FF9 File Offset: 0x000261F9
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00028001 File Offset: 0x00026201
		public float lineSpacingAdjustment
		{
			get
			{
				return this.m_lineSpacingMax;
			}
			set
			{
				if (this.m_lineSpacingMax == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_lineSpacingMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00028027 File Offset: 0x00026227
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0002802F File Offset: 0x0002622F
		public float paragraphSpacing
		{
			get
			{
				return this.m_paragraphSpacing;
			}
			set
			{
				if (this.m_paragraphSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_paragraphSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00028055 File Offset: 0x00026255
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0002805D File Offset: 0x0002625D
		public float characterWidthAdjustment
		{
			get
			{
				return this.m_charWidthMaxAdj;
			}
			set
			{
				if (this.m_charWidthMaxAdj == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_charWidthMaxAdj = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00028083 File Offset: 0x00026283
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0002808B File Offset: 0x0002628B
		public bool enableWordWrapping
		{
			get
			{
				return this.m_enableWordWrapping;
			}
			set
			{
				if (this.m_enableWordWrapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableWordWrapping = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000280B1 File Offset: 0x000262B1
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000280B9 File Offset: 0x000262B9
		public float wordWrappingRatios
		{
			get
			{
				return this.m_wordWrappingRatios;
			}
			set
			{
				if (this.m_wordWrappingRatios == value)
				{
					return;
				}
				this.m_wordWrappingRatios = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000280DF File Offset: 0x000262DF
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000280E7 File Offset: 0x000262E7
		public TextOverflowModes overflowMode
		{
			get
			{
				return this.m_overflowMode;
			}
			set
			{
				if (this.m_overflowMode == value)
				{
					return;
				}
				this.m_overflowMode = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0002810D File Offset: 0x0002630D
		public bool isTextOverflowing
		{
			get
			{
				return this.m_firstOverflowCharacterIndex != -1;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0002811B File Offset: 0x0002631B
		public int firstOverflowCharacterIndex
		{
			get
			{
				return this.m_firstOverflowCharacterIndex;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00028123 File Offset: 0x00026323
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0002812C File Offset: 0x0002632C
		public TMP_Text linkedTextComponent
		{
			get
			{
				return this.m_linkedTextComponent;
			}
			set
			{
				if (value == null)
				{
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
				}
				else
				{
					if (this.IsSelfOrLinkedAncestor(value))
					{
						return;
					}
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
					this.m_linkedTextComponent.parentLinkedComponent = this;
				}
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00028193 File Offset: 0x00026393
		public bool isTextTruncated
		{
			get
			{
				return this.m_isTextTruncated;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0002819B File Offset: 0x0002639B
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x000281A3 File Offset: 0x000263A3
		public bool enableKerning
		{
			get
			{
				return this.m_enableKerning;
			}
			set
			{
				if (this.m_enableKerning == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableKerning = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000281C9 File Offset: 0x000263C9
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000281D1 File Offset: 0x000263D1
		public bool extraPadding
		{
			get
			{
				return this.m_enableExtraPadding;
			}
			set
			{
				if (this.m_enableExtraPadding == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableExtraPadding = value;
				this.UpdateMeshPadding();
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000281F7 File Offset: 0x000263F7
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000281FF File Offset: 0x000263FF
		public bool richText
		{
			get
			{
				return this.m_isRichText;
			}
			set
			{
				if (this.m_isRichText == value)
				{
					return;
				}
				this.m_isRichText = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00028225 File Offset: 0x00026425
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0002822D File Offset: 0x0002642D
		public bool parseCtrlCharacters
		{
			get
			{
				return this.m_parseCtrlCharacters;
			}
			set
			{
				if (this.m_parseCtrlCharacters == value)
				{
					return;
				}
				this.m_parseCtrlCharacters = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00028253 File Offset: 0x00026453
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0002825B File Offset: 0x0002645B
		public bool isOverlay
		{
			get
			{
				return this.m_isOverlay;
			}
			set
			{
				if (this.m_isOverlay == value)
				{
					return;
				}
				this.m_isOverlay = value;
				this.SetShaderDepth();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00028281 File Offset: 0x00026481
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00028289 File Offset: 0x00026489
		public bool isOrthographic
		{
			get
			{
				return this.m_isOrthographic;
			}
			set
			{
				if (this.m_isOrthographic == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isOrthographic = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000282A9 File Offset: 0x000264A9
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000282B1 File Offset: 0x000264B1
		public bool enableCulling
		{
			get
			{
				return this.m_isCullingEnabled;
			}
			set
			{
				if (this.m_isCullingEnabled == value)
				{
					return;
				}
				this.m_isCullingEnabled = value;
				this.SetCulling();
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000282D1 File Offset: 0x000264D1
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x000282D9 File Offset: 0x000264D9
		public bool ignoreVisibility
		{
			get
			{
				return this.m_ignoreCulling;
			}
			set
			{
				if (this.m_ignoreCulling == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_ignoreCulling = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000282F3 File Offset: 0x000264F3
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x000282FB File Offset: 0x000264FB
		public TextureMappingOptions horizontalMapping
		{
			get
			{
				return this.m_horizontalMapping;
			}
			set
			{
				if (this.m_horizontalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_horizontalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0002831B File Offset: 0x0002651B
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00028323 File Offset: 0x00026523
		public TextureMappingOptions verticalMapping
		{
			get
			{
				return this.m_verticalMapping;
			}
			set
			{
				if (this.m_verticalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_verticalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00028343 File Offset: 0x00026543
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0002834B File Offset: 0x0002654B
		public float mappingUvLineOffset
		{
			get
			{
				return this.m_uvLineOffset;
			}
			set
			{
				if (this.m_uvLineOffset == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_uvLineOffset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0002836B File Offset: 0x0002656B
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00028373 File Offset: 0x00026573
		public TextRenderFlags renderMode
		{
			get
			{
				return this.m_renderMode;
			}
			set
			{
				if (this.m_renderMode == value)
				{
					return;
				}
				this.m_renderMode = value;
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0002838D File Offset: 0x0002658D
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00028395 File Offset: 0x00026595
		public VertexSortingOrder geometrySortingOrder
		{
			get
			{
				return this.m_geometrySortingOrder;
			}
			set
			{
				this.m_geometrySortingOrder = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000283AB File Offset: 0x000265AB
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000283B3 File Offset: 0x000265B3
		public bool isTextObjectScaleStatic
		{
			get
			{
				return this.m_IsTextObjectScaleStatic;
			}
			set
			{
				this.m_IsTextObjectScaleStatic = value;
				if (this.m_IsTextObjectScaleStatic)
				{
					TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
					return;
				}
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000283D1 File Offset: 0x000265D1
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000283D9 File Offset: 0x000265D9
		public bool vertexBufferAutoSizeReduction
		{
			get
			{
				return this.m_VertexBufferAutoSizeReduction;
			}
			set
			{
				this.m_VertexBufferAutoSizeReduction = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000283EF File Offset: 0x000265EF
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000283F7 File Offset: 0x000265F7
		public int firstVisibleCharacter
		{
			get
			{
				return this.m_firstVisibleCharacter;
			}
			set
			{
				if (this.m_firstVisibleCharacter == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_firstVisibleCharacter = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00028417 File Offset: 0x00026617
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0002841F File Offset: 0x0002661F
		public int maxVisibleCharacters
		{
			get
			{
				return this.m_maxVisibleCharacters;
			}
			set
			{
				if (this.m_maxVisibleCharacters == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleCharacters = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0002843F File Offset: 0x0002663F
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00028447 File Offset: 0x00026647
		public int maxVisibleWords
		{
			get
			{
				return this.m_maxVisibleWords;
			}
			set
			{
				if (this.m_maxVisibleWords == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleWords = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00028467 File Offset: 0x00026667
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0002846F File Offset: 0x0002666F
		public int maxVisibleLines
		{
			get
			{
				return this.m_maxVisibleLines;
			}
			set
			{
				if (this.m_maxVisibleLines == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleLines = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0002848F File Offset: 0x0002668F
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00028497 File Offset: 0x00026697
		public bool useMaxVisibleDescender
		{
			get
			{
				return this.m_useMaxVisibleDescender;
			}
			set
			{
				if (this.m_useMaxVisibleDescender == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_useMaxVisibleDescender = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000284B7 File Offset: 0x000266B7
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x000284BF File Offset: 0x000266BF
		public int pageToDisplay
		{
			get
			{
				return this.m_pageToDisplay;
			}
			set
			{
				if (this.m_pageToDisplay == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_pageToDisplay = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000284DF File Offset: 0x000266DF
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000284E7 File Offset: 0x000266E7
		public virtual Vector4 margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				if (this.m_margin == value)
				{
					return;
				}
				this.m_margin = value;
				this.ComputeMarginSize();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00028512 File Offset: 0x00026712
		public TMP_TextInfo textInfo
		{
			get
			{
				return this.m_textInfo;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0002851A File Offset: 0x0002671A
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00028522 File Offset: 0x00026722
		public bool havePropertiesChanged
		{
			get
			{
				return this.m_havePropertiesChanged;
			}
			set
			{
				if (this.m_havePropertiesChanged == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0002853B File Offset: 0x0002673B
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00028543 File Offset: 0x00026743
		public bool isUsingLegacyAnimationComponent
		{
			get
			{
				return this.m_isUsingLegacyAnimationComponent;
			}
			set
			{
				this.m_isUsingLegacyAnimationComponent = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0002854C File Offset: 0x0002674C
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0002856E File Offset: 0x0002676E
		public new RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00028590 File Offset: 0x00026790
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00028598 File Offset: 0x00026798
		public virtual bool autoSizeTextContainer
		{
			[CompilerGenerated]
			get
			{
				return this.<autoSizeTextContainer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<autoSizeTextContainer>k__BackingField = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000285A1 File Offset: 0x000267A1
		public virtual Mesh mesh
		{
			get
			{
				return this.m_mesh;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000285A9 File Offset: 0x000267A9
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000285B1 File Offset: 0x000267B1
		public bool isVolumetricText
		{
			get
			{
				return this.m_isVolumetricText;
			}
			set
			{
				if (this.m_isVolumetricText == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.m_textInfo.ResetVertexLayout(value);
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000285DC File Offset: 0x000267DC
		public Bounds bounds
		{
			get
			{
				if (this.m_mesh == null)
				{
					return default(Bounds);
				}
				return this.GetCompoundBounds();
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00028608 File Offset: 0x00026808
		public Bounds textBounds
		{
			get
			{
				if (this.m_textInfo == null)
				{
					return default(Bounds);
				}
				return this.GetTextBounds();
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004C0 RID: 1216 RVA: 0x00028630 File Offset: 0x00026830
		// (remove) Token: 0x060004C1 RID: 1217 RVA: 0x00028664 File Offset: 0x00026864
		public static event Func<int, string, TMP_FontAsset> OnFontAssetRequest
		{
			[CompilerGenerated]
			add
			{
				Func<int, string, TMP_FontAsset> func = TMP_Text.OnFontAssetRequest;
				Func<int, string, TMP_FontAsset> func2;
				do
				{
					func2 = func;
					Func<int, string, TMP_FontAsset> value2 = (Func<int, string, TMP_FontAsset>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<int, string, TMP_FontAsset>>(ref TMP_Text.OnFontAssetRequest, value2, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<int, string, TMP_FontAsset> func = TMP_Text.OnFontAssetRequest;
				Func<int, string, TMP_FontAsset> func2;
				do
				{
					func2 = func;
					Func<int, string, TMP_FontAsset> value2 = (Func<int, string, TMP_FontAsset>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<int, string, TMP_FontAsset>>(ref TMP_Text.OnFontAssetRequest, value2, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C2 RID: 1218 RVA: 0x00028698 File Offset: 0x00026898
		// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x000286CC File Offset: 0x000268CC
		public static event Func<int, string, TMP_SpriteAsset> OnSpriteAssetRequest
		{
			[CompilerGenerated]
			add
			{
				Func<int, string, TMP_SpriteAsset> func = TMP_Text.OnSpriteAssetRequest;
				Func<int, string, TMP_SpriteAsset> func2;
				do
				{
					func2 = func;
					Func<int, string, TMP_SpriteAsset> value2 = (Func<int, string, TMP_SpriteAsset>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<int, string, TMP_SpriteAsset>>(ref TMP_Text.OnSpriteAssetRequest, value2, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<int, string, TMP_SpriteAsset> func = TMP_Text.OnSpriteAssetRequest;
				Func<int, string, TMP_SpriteAsset> func2;
				do
				{
					func2 = func;
					Func<int, string, TMP_SpriteAsset> value2 = (Func<int, string, TMP_SpriteAsset>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<int, string, TMP_SpriteAsset>>(ref TMP_Text.OnSpriteAssetRequest, value2, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x00028700 File Offset: 0x00026900
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00028738 File Offset: 0x00026938
		public virtual event Action<TMP_TextInfo> OnPreRenderText
		{
			[CompilerGenerated]
			add
			{
				Action<TMP_TextInfo> action = this.OnPreRenderText;
				Action<TMP_TextInfo> action2;
				do
				{
					action2 = action;
					Action<TMP_TextInfo> value2 = (Action<TMP_TextInfo>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<TMP_TextInfo>>(ref this.OnPreRenderText, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<TMP_TextInfo> action = this.OnPreRenderText;
				Action<TMP_TextInfo> action2;
				do
				{
					action2 = action;
					Action<TMP_TextInfo> value2 = (Action<TMP_TextInfo>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<TMP_TextInfo>>(ref this.OnPreRenderText, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00028770 File Offset: 0x00026970
		protected TMP_SpriteAnimator spriteAnimator
		{
			get
			{
				if (this.m_spriteAnimator == null)
				{
					this.m_spriteAnimator = base.GetComponent<TMP_SpriteAnimator>();
					if (this.m_spriteAnimator == null)
					{
						this.m_spriteAnimator = base.gameObject.AddComponent<TMP_SpriteAnimator>();
					}
				}
				return this.m_spriteAnimator;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000287BC File Offset: 0x000269BC
		public float flexibleHeight
		{
			get
			{
				return this.m_flexibleHeight;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000287C4 File Offset: 0x000269C4
		public float flexibleWidth
		{
			get
			{
				return this.m_flexibleWidth;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000287CC File Offset: 0x000269CC
		public float minWidth
		{
			get
			{
				return this.m_minWidth;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000287D4 File Offset: 0x000269D4
		public float minHeight
		{
			get
			{
				return this.m_minHeight;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000287DC File Offset: 0x000269DC
		public float maxWidth
		{
			get
			{
				return this.m_maxWidth;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000287E4 File Offset: 0x000269E4
		public float maxHeight
		{
			get
			{
				return this.m_maxHeight;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000287EC File Offset: 0x000269EC
		protected LayoutElement layoutElement
		{
			get
			{
				if (this.m_LayoutElement == null)
				{
					this.m_LayoutElement = base.GetComponent<LayoutElement>();
				}
				return this.m_LayoutElement;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0002880E File Offset: 0x00026A0E
		public virtual float preferredWidth
		{
			get
			{
				this.m_preferredWidth = this.GetPreferredWidth();
				return this.m_preferredWidth;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00028822 File Offset: 0x00026A22
		public virtual float preferredHeight
		{
			get
			{
				this.m_preferredHeight = this.GetPreferredHeight();
				return this.m_preferredHeight;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00028836 File Offset: 0x00026A36
		public virtual float renderedWidth
		{
			get
			{
				return this.GetRenderedWidth();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0002883E File Offset: 0x00026A3E
		public virtual float renderedHeight
		{
			get
			{
				return this.GetRenderedHeight();
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00028846 File Offset: 0x00026A46
		public int layoutPriority
		{
			get
			{
				return this.m_layoutPriority;
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0002884E File Offset: 0x00026A4E
		protected virtual void LoadFontAsset()
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00028850 File Offset: 0x00026A50
		protected virtual void SetSharedMaterial(Material mat)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00028852 File Offset: 0x00026A52
		protected virtual Material GetMaterial(Material mat)
		{
			return null;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00028855 File Offset: 0x00026A55
		protected virtual void SetFontBaseMaterial(Material mat)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00028857 File Offset: 0x00026A57
		protected virtual Material[] GetSharedMaterials()
		{
			return null;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002885A File Offset: 0x00026A5A
		protected virtual void SetSharedMaterials(Material[] materials)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0002885C File Offset: 0x00026A5C
		protected virtual Material[] GetMaterials(Material[] mats)
		{
			return null;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0002885F File Offset: 0x00026A5F
		protected virtual Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002888C File Offset: 0x00026A8C
		protected void SetVertexColorGradient(TMP_ColorGradient gradient)
		{
			if (gradient == null)
			{
				return;
			}
			this.m_fontColorGradient.bottomLeft = gradient.bottomLeft;
			this.m_fontColorGradient.bottomRight = gradient.bottomRight;
			this.m_fontColorGradient.topLeft = gradient.topLeft;
			this.m_fontColorGradient.topRight = gradient.topRight;
			this.SetVerticesDirty();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000288ED File Offset: 0x00026AED
		protected void SetTextSortingOrder(VertexSortingOrder order)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000288EF File Offset: 0x00026AEF
		protected void SetTextSortingOrder(int[] order)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000288F1 File Offset: 0x00026AF1
		protected virtual void SetFaceColor(Color32 color)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000288F3 File Offset: 0x00026AF3
		protected virtual void SetOutlineColor(Color32 color)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000288F5 File Offset: 0x00026AF5
		protected virtual void SetOutlineThickness(float thickness)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000288F7 File Offset: 0x00026AF7
		protected virtual void SetShaderDepth()
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000288F9 File Offset: 0x00026AF9
		protected virtual void SetCulling()
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000288FB File Offset: 0x00026AFB
		internal virtual void UpdateCulling()
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00028900 File Offset: 0x00026B00
		protected virtual float GetPaddingForMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_sharedMaterial == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00028970 File Offset: 0x00026B70
		protected virtual float GetPaddingForMaterial(Material mat)
		{
			if (mat == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(mat, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000289CC File Offset: 0x00026BCC
		protected virtual Vector3[] GetTextContainerLocalCorners()
		{
			return null;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000289CF File Offset: 0x00026BCF
		public virtual void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000289D1 File Offset: 0x00026BD1
		public virtual void UpdateGeometry(Mesh mesh, int index)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000289D3 File Offset: 0x00026BD3
		public virtual void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000289D5 File Offset: 0x00026BD5
		public virtual void UpdateVertexData()
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000289D7 File Offset: 0x00026BD7
		public virtual void SetVertices(Vector3[] vertices)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000289D9 File Offset: 0x00026BD9
		public virtual void UpdateMeshPadding()
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000289DB File Offset: 0x00026BDB
		public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
			this.InternalCrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000289F3 File Offset: 0x00026BF3
		public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
			this.InternalCrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00028A07 File Offset: 0x00026C07
		protected virtual void InternalCrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00028A09 File Offset: 0x00026C09
		protected virtual void InternalCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00028A0C File Offset: 0x00026C0C
		protected void ParseInputText()
		{
			switch (this.m_inputSource)
			{
			case TMP_Text.TextInputSources.TextInputBox:
			case TMP_Text.TextInputSources.TextString:
				this.PopulateTextBackingArray((this.m_TextPreprocessor == null) ? this.m_text : this.m_TextPreprocessor.PreprocessText(this.m_text));
				this.PopulateTextProcessingArray();
				break;
			}
			this.SetArraySizes(this.m_TextProcessingArray);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00028A74 File Offset: 0x00026C74
		private void PopulateTextBackingArray(string sourceText)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.PopulateTextBackingArray(sourceText, 0, length);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00028A98 File Offset: 0x00026C98
		private void PopulateTextBackingArray(string sourceText, int start, int length)
		{
			int num = 0;
			int i;
			if (sourceText == null)
			{
				i = 0;
				length = 0;
			}
			else
			{
				i = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int num2 = i + length;
			while (i < num2)
			{
				this.m_TextBackingArray[num] = (uint)sourceText[i];
				num++;
				i++;
			}
			this.m_TextBackingArray[num] = 0U;
			this.m_TextBackingArray.Count = num;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00028B38 File Offset: 0x00026D38
		private void PopulateTextBackingArray(StringBuilder sourceText, int start, int length)
		{
			int num = 0;
			int i;
			if (sourceText == null)
			{
				i = 0;
				length = 0;
			}
			else
			{
				i = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int num2 = i + length;
			while (i < num2)
			{
				this.m_TextBackingArray[num] = (uint)sourceText[i];
				num++;
				i++;
			}
			this.m_TextBackingArray[num] = 0U;
			this.m_TextBackingArray.Count = num;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00028BD8 File Offset: 0x00026DD8
		private void PopulateTextBackingArray(char[] sourceText, int start, int length)
		{
			int num = 0;
			int i;
			if (sourceText == null)
			{
				i = 0;
				length = 0;
			}
			else
			{
				i = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int num2 = i + length;
			while (i < num2)
			{
				this.m_TextBackingArray[num] = (uint)sourceText[i];
				num++;
				i++;
			}
			this.m_TextBackingArray[num] = 0U;
			this.m_TextBackingArray.Count = num;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00028C6C File Offset: 0x00026E6C
		private void PopulateTextProcessingArray()
		{
			int count = this.m_TextBackingArray.Count;
			if (this.m_TextProcessingArray.Length < count)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray, count);
			}
			TMP_TextProcessingStack<int>.SetDefault(this.m_TextStyleStacks, 0);
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref this.m_TextProcessingArray, ref num);
			}
			int i = 0;
			while (i < count)
			{
				uint num2 = this.m_TextBackingArray[i];
				if (num2 == 0U)
				{
					break;
				}
				if (this.m_inputSource != TMP_Text.TextInputSources.TextInputBox || num2 != 92U || i >= count - 1)
				{
					goto IL_429;
				}
				uint num3 = this.m_TextBackingArray[i + 1];
				if (num3 != 85U)
				{
					if (num3 != 92U)
					{
						switch (num3)
						{
						case 110U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_429;
							}
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 10;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 1;
							i++;
							num++;
							break;
						case 111U:
						case 112U:
						case 113U:
						case 115U:
							goto IL_429;
						case 114U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_429;
							}
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 13;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 1;
							i++;
							num++;
							break;
						case 116U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_429;
							}
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 9;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 1;
							i++;
							num++;
							break;
						case 117U:
							if (count <= i + 5)
							{
								goto IL_429;
							}
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = this.GetUTF16(this.m_TextBackingArray, i + 2);
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 6;
							i += 5;
							num++;
							break;
						case 118U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_429;
							}
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 11;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 1;
							i++;
							num++;
							break;
						default:
							goto IL_429;
						}
					}
					else
					{
						if (!this.m_parseCtrlCharacters || count <= i + 2)
						{
							goto IL_429;
						}
						if (num + 2 > this.m_TextProcessingArray.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
						}
						this.m_TextProcessingArray[num].unicode = (int)this.m_TextBackingArray[i + 1];
						this.m_TextProcessingArray[num].stringIndex = i;
						this.m_TextProcessingArray[num].length = 1;
						this.m_TextProcessingArray[num + 1].unicode = (int)this.m_TextBackingArray[i + 2];
						this.m_TextProcessingArray[num + 1].stringIndex = i;
						this.m_TextProcessingArray[num + 1].length = 1;
						i += 2;
						num += 2;
					}
				}
				else
				{
					if (count <= i + 9)
					{
						goto IL_429;
					}
					if (num == this.m_TextProcessingArray.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
					}
					this.m_TextProcessingArray[num].unicode = this.GetUTF32(this.m_TextBackingArray, i + 2);
					this.m_TextProcessingArray[num].stringIndex = i;
					this.m_TextProcessingArray[num].length = 10;
					i += 9;
					num++;
				}
				IL_76A:
				i++;
				continue;
				IL_429:
				if (num2 >= 55296U && num2 <= 56319U && count > i + 1 && this.m_TextBackingArray[i + 1] >= 56320U && this.m_TextBackingArray[i + 1] <= 57343U)
				{
					if (num == this.m_TextProcessingArray.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
					}
					this.m_TextProcessingArray[num].unicode = (int)TMP_TextParsingUtilities.ConvertToUTF32(num2, this.m_TextBackingArray[i + 1]);
					this.m_TextProcessingArray[num].stringIndex = i;
					this.m_TextProcessingArray[num].length = 2;
					i++;
					num++;
					goto IL_76A;
				}
				if (num2 == 60U && this.m_isRichText)
				{
					MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(this.m_TextBackingArray, i + 1);
					if (markupTagHashCode <= MarkupTag.NBSP)
					{
						if (markupTagHashCode == MarkupTag.BR)
						{
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 10;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 4;
							num++;
							i += 3;
							goto IL_76A;
						}
						if (markupTagHashCode == MarkupTag.NBSP)
						{
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 160;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 6;
							num++;
							i += 5;
							goto IL_76A;
						}
					}
					else
					{
						if (markupTagHashCode == MarkupTag.ZWSP)
						{
							if (num == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[num].unicode = 8203;
							this.m_TextProcessingArray[num].stringIndex = i;
							this.m_TextProcessingArray[num].length = 6;
							num++;
							i += 5;
							goto IL_76A;
						}
						if (markupTagHashCode != MarkupTag.STYLE)
						{
							if (markupTagHashCode == MarkupTag.SLASH_STYLE)
							{
								int j = num;
								this.ReplaceClosingStyleTag(ref this.m_TextBackingArray, i, ref this.m_TextProcessingArray, ref num);
								while (j < num)
								{
									this.m_TextProcessingArray[j].stringIndex = i;
									this.m_TextProcessingArray[j].length = 8;
									j++;
								}
								i += 7;
								goto IL_76A;
							}
						}
						else
						{
							int k = num;
							int num4;
							if (this.ReplaceOpeningStyleTag(ref this.m_TextBackingArray, i, out num4, ref this.m_TextProcessingArray, ref num))
							{
								while (k < num)
								{
									this.m_TextProcessingArray[k].stringIndex = i;
									this.m_TextProcessingArray[k].length = num4 - i + 1;
									k++;
								}
								i = num4;
								goto IL_76A;
							}
						}
					}
				}
				if (num == this.m_TextProcessingArray.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
				}
				this.m_TextProcessingArray[num].unicode = (int)num2;
				this.m_TextProcessingArray[num].stringIndex = i;
				this.m_TextProcessingArray[num].length = 1;
				num++;
				goto IL_76A;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextProcessingArray, ref num);
			}
			if (num == this.m_TextProcessingArray.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextProcessingArray);
			}
			this.m_TextProcessingArray[num].unicode = 0;
			this.m_InternalTextProcessingArraySize = num;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00029448 File Offset: 0x00027648
		private void SetTextInternal(string sourceText)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.PopulateTextBackingArray(sourceText, 0, length);
			TMP_Text.TextInputSources inputSource = this.m_inputSource;
			this.m_inputSource = TMP_Text.TextInputSources.TextString;
			this.PopulateTextProcessingArray();
			this.m_inputSource = inputSource;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00029488 File Offset: 0x00027688
		public void SetText(string sourceText, bool syncTextInputBox = true)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.PopulateTextBackingArray(sourceText, 0, length);
			this.m_text = sourceText;
			this.m_inputSource = TMP_Text.TextInputSources.TextString;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000294D4 File Offset: 0x000276D4
		public void SetText(string sourceText, float arg0)
		{
			this.SetText(sourceText, arg0, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002950C File Offset: 0x0002770C
		public void SetText(string sourceText, float arg0, float arg1)
		{
			this.SetText(sourceText, arg0, arg1, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00029540 File Offset: 0x00027740
		public void SetText(string sourceText, float arg0, float arg1, float arg2)
		{
			this.SetText(sourceText, arg0, arg1, arg2, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00029574 File Offset: 0x00027774
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, 0f, 0f, 0f, 0f);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000295A4 File Offset: 0x000277A4
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, 0f, 0f, 0f);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000295D0 File Offset: 0x000277D0
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, arg5, 0f, 0f);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000295F8 File Offset: 0x000277F8
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5, float arg6)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, arg5, arg6, 0f);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00029620 File Offset: 0x00027820
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int i = 0;
			int num5 = 0;
			while (i < sourceText.Length)
			{
				char c = sourceText[i];
				if (c == '{')
				{
					num4 = 1;
				}
				else if (c == '}')
				{
					switch (num)
					{
					case 0:
						this.AddFloatToInternalTextBackingArray(arg0, num2, num3, ref num5);
						break;
					case 1:
						this.AddFloatToInternalTextBackingArray(arg1, num2, num3, ref num5);
						break;
					case 2:
						this.AddFloatToInternalTextBackingArray(arg2, num2, num3, ref num5);
						break;
					case 3:
						this.AddFloatToInternalTextBackingArray(arg3, num2, num3, ref num5);
						break;
					case 4:
						this.AddFloatToInternalTextBackingArray(arg4, num2, num3, ref num5);
						break;
					case 5:
						this.AddFloatToInternalTextBackingArray(arg5, num2, num3, ref num5);
						break;
					case 6:
						this.AddFloatToInternalTextBackingArray(arg6, num2, num3, ref num5);
						break;
					case 7:
						this.AddFloatToInternalTextBackingArray(arg7, num2, num3, ref num5);
						break;
					}
					num = 0;
					num4 = 0;
					num2 = 0;
					num3 = 0;
				}
				else if (num4 == 1 && c >= '0' && c <= '8')
				{
					num = (int)(c - '0');
					num4 = 2;
				}
				else
				{
					if (num4 == 2)
					{
						if (c == ':')
						{
							goto IL_150;
						}
						if (c == '.')
						{
							num4 = 3;
							goto IL_150;
						}
						if (c == '#')
						{
							goto IL_150;
						}
						if (c == '0')
						{
							num2++;
							goto IL_150;
						}
						if (c == ',')
						{
							goto IL_150;
						}
						if (c >= '1' && c <= '9')
						{
							num3 = (int)(c - '0');
							goto IL_150;
						}
					}
					if (num4 == 3 && c == '0')
					{
						num3++;
					}
					else
					{
						this.m_TextBackingArray[num5] = (uint)c;
						num5++;
					}
				}
				IL_150:
				i++;
			}
			this.m_TextBackingArray[num5] = 0U;
			this.m_TextBackingArray.Count = num5;
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetText;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000297D4 File Offset: 0x000279D4
		public void SetText(StringBuilder sourceText)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.SetText(sourceText, 0, length);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000297F7 File Offset: 0x000279F7
		private void SetText(StringBuilder sourceText, int start, int length)
		{
			this.PopulateTextBackingArray(sourceText, start, length);
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetTextArray;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002982C File Offset: 0x00027A2C
		public void SetText(char[] sourceText)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.SetCharArray(sourceText, 0, length);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002984C File Offset: 0x00027A4C
		public void SetText(char[] sourceText, int start, int length)
		{
			this.SetCharArray(sourceText, start, length);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00029858 File Offset: 0x00027A58
		public void SetCharArray(char[] sourceText)
		{
			int length = (sourceText == null) ? 0 : sourceText.Length;
			this.SetCharArray(sourceText, 0, length);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00029878 File Offset: 0x00027A78
		public void SetCharArray(char[] sourceText, int start, int length)
		{
			this.PopulateTextBackingArray(sourceText, start, length);
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetTextArray;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000298AC File Offset: 0x00027AAC
		private TMP_Style GetStyle(int hashCode)
		{
			TMP_Style tmp_Style = null;
			if (this.m_StyleSheet != null)
			{
				tmp_Style = this.m_StyleSheet.GetStyle(hashCode);
				if (tmp_Style != null)
				{
					return tmp_Style;
				}
			}
			if (TMP_Settings.defaultStyleSheet != null)
			{
				tmp_Style = TMP_Settings.defaultStyleSheet.GetStyle(hashCode);
			}
			return tmp_Style;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000298F8 File Offset: 0x00027AF8
		private bool ReplaceOpeningStyleTag(ref TMP_Text.TextBackingContainer sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(styleHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_237;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleOpeningTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_237;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_237;
						}
						this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_237;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_263:
				i++;
				continue;
				IL_237:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_263;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00029B88 File Offset: 0x00027D88
		private bool ReplaceOpeningStyleTag(ref int[] sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(styleHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_237;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleOpeningTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_237;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_237;
						}
						this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_237;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_263:
				i++;
				continue;
				IL_237:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_263;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00029E18 File Offset: 0x00028018
		private void ReplaceClosingStyleTag(ref TMP_Text.TextBackingContainer sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int hashCode = this.m_TextStyleStacks[this.m_TextStyleStackDepth + 1].Pop();
			TMP_Style style = this.GetStyle(hashCode);
			if (style == null)
			{
				return;
			}
			this.m_TextStyleStackDepth++;
			int num = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleClosingTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleClosingTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_218;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleClosingTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_218;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_218;
						}
						this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_218;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_241:
				i++;
				continue;
				IL_218:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_241;
			}
			this.m_TextStyleStackDepth--;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002A084 File Offset: 0x00028284
		private void ReplaceClosingStyleTag(ref int[] sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int hashCode = this.m_TextStyleStacks[this.m_TextStyleStackDepth + 1].Pop();
			TMP_Style style = this.GetStyle(hashCode);
			if (style == null)
			{
				return;
			}
			this.m_TextStyleStackDepth++;
			int num = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleClosingTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleClosingTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_218;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleClosingTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_218;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_218;
						}
						this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_218;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_241:
				i++;
				continue;
				IL_218:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_241;
			}
			this.m_TextStyleStackDepth--;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002A2F0 File Offset: 0x000284F0
		private bool InsertOpeningStyleTag(TMP_Style style, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			if (style == null)
			{
				return false;
			}
			this.m_TextStyleStacks[0].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_1DC;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleOpeningTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_1DC;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_1DC;
						}
						this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_1DC;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_204:
				i++;
				continue;
				IL_1DC:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_204;
			}
			this.m_TextStyleStackDepth = 0;
			return true;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002A514 File Offset: 0x00028714
		private void InsertClosingStyleTag(ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int hashCode = this.m_TextStyleStacks[0].Pop();
			TMP_Style style = this.GetStyle(hashCode);
			int num = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleClosingTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleClosingTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_1CA;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleClosingTagArray, i + 1);
				if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_1CA;
						}
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 160;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (writeIndex == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[writeIndex].unicode = 10;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_1CA;
						}
						this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int num4;
						if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num4, ref charBuffer, ref writeIndex))
						{
							goto IL_1CA;
						}
						i = num4;
					}
				}
				else
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 8203;
					writeIndex++;
					i += 5;
				}
				IL_1EF:
				i++;
				continue;
				IL_1CA:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_1EF;
			}
			this.m_TextStyleStackDepth = 0;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002A724 File Offset: 0x00028924
		private int GetMarkupTagHashCode(int[] tagDefinition, int readIndex)
		{
			int num = 0;
			int num2 = readIndex + 16;
			int num3 = tagDefinition.Length;
			while (readIndex < num2 && readIndex < num3)
			{
				int num4 = tagDefinition[readIndex];
				if (num4 == 62 || num4 == 61 || num4 == 32)
				{
					return num;
				}
				num = ((num << 5) + num ^ (int)TMP_TextUtilities.ToUpperASCIIFast((uint)num4));
				readIndex++;
			}
			return num;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002A770 File Offset: 0x00028970
		private int GetMarkupTagHashCode(TMP_Text.TextBackingContainer tagDefinition, int readIndex)
		{
			int num = 0;
			int num2 = readIndex + 16;
			int capacity = tagDefinition.Capacity;
			while (readIndex < num2 && readIndex < capacity)
			{
				uint num3 = tagDefinition[readIndex];
				if (num3 == 62U || num3 == 61U || num3 == 32U)
				{
					return num;
				}
				num = ((num << 5) + num ^ (int)TMP_TextUtilities.ToUpperASCIIFast(num3));
				readIndex++;
			}
			return num;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002A7C4 File Offset: 0x000289C4
		private int GetStyleHashCode(ref int[] text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != 34)
				{
					if (text[i] == 62)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast((char)text[i]));
				}
			}
			return num;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002A80C File Offset: 0x00028A0C
		private int GetStyleHashCode(ref TMP_Text.TextBackingContainer text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Capacity; i++)
			{
				if (text[i] != 34U)
				{
					if (text[i] == 62U)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast((char)text[i]));
				}
			}
			return num;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002A860 File Offset: 0x00028A60
		private void ResizeInternalArray<T>(ref T[] array)
		{
			int newSize = Mathf.NextPowerOfTwo(array.Length + 1);
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002A880 File Offset: 0x00028A80
		private void ResizeInternalArray<T>(ref T[] array, int size)
		{
			size = Mathf.NextPowerOfTwo(size + 1);
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002A894 File Offset: 0x00028A94
		private void AddFloatToInternalTextBackingArray(float value, int padding, int precision, ref int writeIndex)
		{
			if (value < 0f)
			{
				this.m_TextBackingArray[writeIndex] = 45U;
				writeIndex++;
				value = -value;
			}
			decimal num = (decimal)value;
			if (padding == 0 && precision == 0)
			{
				precision = 9;
			}
			else
			{
				num += this.k_Power[Mathf.Min(9, precision)];
			}
			long num2 = (long)num;
			this.AddIntegerToInternalTextBackingArray((double)num2, padding, ref writeIndex);
			if (precision > 0)
			{
				num -= num2;
				if (num != 0m)
				{
					int num3 = writeIndex;
					writeIndex = num3 + 1;
					this.m_TextBackingArray[num3] = 46U;
					for (int i = 0; i < precision; i++)
					{
						num *= 10m;
						long num4 = (long)num;
						num3 = writeIndex;
						writeIndex = num3 + 1;
						this.m_TextBackingArray[num3] = (uint)((ushort)(num4 + 48L));
						num -= num4;
						if (num == 0m)
						{
							i = precision;
						}
					}
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002A99C File Offset: 0x00028B9C
		private void AddIntegerToInternalTextBackingArray(double number, int padding, ref int writeIndex)
		{
			int num = 0;
			int num2 = writeIndex;
			do
			{
				this.m_TextBackingArray[num2++] = (uint)((ushort)(number % 10.0 + 48.0));
				number /= 10.0;
				num++;
			}
			while (number > 0.999999999999999 || num < padding);
			int num3 = num2;
			while (writeIndex + 1 < num2)
			{
				num2--;
				uint value = this.m_TextBackingArray[writeIndex];
				this.m_TextBackingArray[writeIndex] = this.m_TextBackingArray[num2];
				this.m_TextBackingArray[num2] = value;
				writeIndex++;
			}
			writeIndex = num3;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002AA44 File Offset: 0x00028C44
		private string InternalTextBackingArrayToString()
		{
			char[] array = new char[this.m_TextBackingArray.Count];
			for (int i = 0; i < this.m_TextBackingArray.Capacity; i++)
			{
				char c = (char)this.m_TextBackingArray[i];
				if (c == '\0')
				{
					break;
				}
				array[i] = c;
			}
			this.m_IsTextBackingStringDirty = false;
			return new string(array);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002AA9A File Offset: 0x00028C9A
		internal virtual int SetArraySizes(TMP_Text.UnicodeChar[] unicodeChars)
		{
			return 0;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002AAA0 File Offset: 0x00028CA0
		public Vector2 GetPreferredValues()
		{
			this.m_isPreferredWidthDirty = true;
			float preferredWidth = this.GetPreferredWidth();
			this.m_isPreferredHeightDirty = true;
			float preferredHeight = this.GetPreferredHeight();
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002AADC File Offset: 0x00028CDC
		public Vector2 GetPreferredValues(float width, float height)
		{
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			Vector2 margin = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(margin);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002AB14 File Offset: 0x00028D14
		public Vector2 GetPreferredValues(string text)
		{
			this.m_isCalculatingPreferredValues = true;
			this.SetTextInternal(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			Vector2 margin = TMP_Text.k_LargePositiveVector2;
			float preferredWidth = this.GetPreferredWidth(margin);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002AB58 File Offset: 0x00028D58
		public Vector2 GetPreferredValues(string text, float width, float height)
		{
			this.m_isCalculatingPreferredValues = true;
			this.SetTextInternal(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			Vector2 margin = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(margin);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002ABA0 File Offset: 0x00028DA0
		protected float GetPreferredWidth()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			if (!this.m_isPreferredWidthDirty)
			{
				return this.m_preferredWidth;
			}
			float num = this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize;
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 marginSize = TMP_Text.k_LargePositiveVector2;
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			this.m_AutoSizeIterationCount = 0;
			float x = this.CalculatePreferredValues(ref num, marginSize, false, false).x;
			this.m_isPreferredWidthDirty = false;
			return x;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002AC3C File Offset: 0x00028E3C
		private float GetPreferredWidth(Vector2 margin)
		{
			float num = this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize;
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_AutoSizeIterationCount = 0;
			return this.CalculatePreferredValues(ref num, margin, false, false).x;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002AC9C File Offset: 0x00028E9C
		protected float GetPreferredHeight()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			if (!this.m_isPreferredHeightDirty)
			{
				return this.m_preferredHeight;
			}
			float num = this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize;
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 marginSize = new Vector2((this.m_marginWidth != 0f) ? this.m_marginWidth : TMP_Text.k_LargePositiveFloat, TMP_Text.k_LargePositiveFloat);
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			this.m_IsAutoSizePointSizeSet = false;
			this.m_AutoSizeIterationCount = 0;
			float result = 0f;
			while (!this.m_IsAutoSizePointSizeSet)
			{
				result = this.CalculatePreferredValues(ref num, marginSize, this.m_enableAutoSizing, this.m_enableWordWrapping).y;
				this.m_AutoSizeIterationCount++;
			}
			this.m_isPreferredHeightDirty = false;
			return result;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002AD88 File Offset: 0x00028F88
		private float GetPreferredHeight(Vector2 margin)
		{
			float num = this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize;
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_IsAutoSizePointSizeSet = false;
			this.m_AutoSizeIterationCount = 0;
			float result = 0f;
			while (!this.m_IsAutoSizePointSizeSet)
			{
				result = this.CalculatePreferredValues(ref num, margin, this.m_enableAutoSizing, this.m_enableWordWrapping).y;
				this.m_AutoSizeIterationCount++;
			}
			return result;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002AE18 File Offset: 0x00029018
		public Vector2 GetRenderedValues()
		{
			return this.GetTextBounds().size;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002AE38 File Offset: 0x00029038
		public Vector2 GetRenderedValues(bool onlyVisibleCharacters)
		{
			return this.GetTextBounds(onlyVisibleCharacters).size;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002AE59 File Offset: 0x00029059
		private float GetRenderedWidth()
		{
			return this.GetRenderedValues().x;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002AE66 File Offset: 0x00029066
		protected float GetRenderedWidth(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).x;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002AE74 File Offset: 0x00029074
		private float GetRenderedHeight()
		{
			return this.GetRenderedValues().y;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002AE81 File Offset: 0x00029081
		protected float GetRenderedHeight(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).y;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002AE90 File Offset: 0x00029090
		protected virtual Vector2 CalculatePreferredValues(ref float fontSize, Vector2 marginSize, bool isTextAutoSizingEnabled, bool isWordWrappingEnabled)
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID().ToString());
				this.m_IsAutoSizePointSizeSet = true;
				return Vector2.zero;
			}
			if (this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0)
			{
				this.m_IsAutoSizePointSizeSet = true;
				return Vector2.zero;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(0, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			int totalCharacterCount = this.m_totalCharacterCount;
			if (this.m_internalCharacterInfo == null || totalCharacterCount > this.m_internalCharacterInfo.Length)
			{
				this.m_internalCharacterInfo = new TMP_CharacterInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
			}
			float num = fontSize / (float)this.m_fontAsset.faceInfo.pointSize * this.m_fontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
			float num2 = num;
			float num3 = fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float num4 = this.m_currentFontAsset.faceInfo.lineHeight - (this.m_currentFontAsset.faceInfo.ascentLine - this.m_currentFontAsset.faceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			float a = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_IsDrivenLineSpacing = false;
			float x = marginSize.x;
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			float num5 = 0f;
			float num6 = 0f;
			this.m_width = -1f;
			float num7 = x + 0.0001f - this.m_marginLeft - this.m_marginRight;
			float num8 = 0f;
			float num9 = 0f;
			float num10 = 0f;
			this.m_isCalculatingPreferredValues = true;
			this.m_maxCapHeight = 0f;
			this.m_maxTextAscender = 0f;
			this.m_ElementDescender = 0f;
			bool flag = false;
			bool flag2 = true;
			this.m_isNonBreakingSpace = false;
			bool flag3 = false;
			TMP_Text.CharacterSubstitution characterSubstitution = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool flag4 = false;
			WordWrapState wordWrapState = default(WordWrapState);
			WordWrapState wordWrapState2 = default(WordWrapState);
			WordWrapState wordWrapState3 = default(WordWrapState);
			this.m_AutoSizeIterationCount++;
			int num11 = 0;
			while (num11 < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[num11].unicode != 0)
			{
				int num12 = this.m_TextProcessingArray[num11].unicode;
				if (!this.m_isRichText || num12 != 60)
				{
					this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					goto IL_438;
				}
				this.m_isParsingText = true;
				this.m_textElementType = TMP_TextElementType.Character;
				int num13;
				if (!this.ValidateHtmlTag(this.m_TextProcessingArray, num11 + 1, out num13))
				{
					goto IL_438;
				}
				num11 = num13;
				if (this.m_textElementType != TMP_TextElementType.Character)
				{
					goto IL_438;
				}
				IL_19D3:
				num11++;
				continue;
				IL_438:
				int currentMaterialIndex = this.m_currentMaterialIndex;
				bool isUsingAlternateTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
				this.m_isParsingText = false;
				bool flag5 = false;
				if (characterSubstitution.index == this.m_characterCount)
				{
					num12 = (int)characterSubstitution.unicode;
					this.m_textElementType = TMP_TextElementType.Character;
					flag5 = true;
					if (num12 != 3)
					{
						if (num12 != 45)
						{
							if (num12 == 8230)
							{
								this.m_internalCharacterInfo[this.m_characterCount].textElement = this.m_Ellipsis.character;
								this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
								this.m_internalCharacterInfo[this.m_characterCount].fontAsset = this.m_Ellipsis.fontAsset;
								this.m_internalCharacterInfo[this.m_characterCount].material = this.m_Ellipsis.material;
								this.m_internalCharacterInfo[this.m_characterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
								this.m_isTextTruncated = true;
								characterSubstitution.index = this.m_characterCount + 1;
								characterSubstitution.unicode = 3U;
							}
						}
					}
					else
					{
						this.m_internalCharacterInfo[this.m_characterCount].textElement = this.m_currentFontAsset.characterLookupTable[3U];
						this.m_isTextTruncated = true;
					}
				}
				if (this.m_characterCount < this.m_firstVisibleCharacter && num12 != 3)
				{
					this.m_internalCharacterInfo[this.m_characterCount].isVisible = false;
					this.m_internalCharacterInfo[this.m_characterCount].character = '​';
					this.m_internalCharacterInfo[this.m_characterCount].lineNumber = 0;
					this.m_characterCount++;
					goto IL_19D3;
				}
				float num14 = 1f;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num12))
						{
							num12 = (int)char.ToUpper((char)num12);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num12))
						{
							num12 = (int)char.ToLower((char)num12);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num12))
					{
						num14 = 0.8f;
						num12 = (int)char.ToUpper((char)num12);
					}
				}
				float num15 = 0f;
				float num16 = 0f;
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_currentSpriteAsset = this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset;
					this.m_spriteIndex = this.m_textInfo.characterInfo[this.m_characterCount].spriteIndex;
					TMP_SpriteCharacter tmp_SpriteCharacter = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					if (tmp_SpriteCharacter == null)
					{
						goto IL_19D3;
					}
					if (num12 == 60)
					{
						num12 = 57344 + this.m_spriteIndex;
					}
					if (this.m_currentSpriteAsset.faceInfo.pointSize > 0)
					{
						float num17 = this.m_currentFontSize / (float)this.m_currentSpriteAsset.faceInfo.pointSize * this.m_currentSpriteAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale * num17;
						num15 = this.m_currentSpriteAsset.faceInfo.ascentLine;
						num16 = this.m_currentSpriteAsset.faceInfo.descentLine;
					}
					else
					{
						float num18 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = this.m_currentFontAsset.faceInfo.ascentLine / tmp_SpriteCharacter.glyph.metrics.height * tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale * num18;
						float num19 = num18 / num2;
						num15 = this.m_currentFontAsset.faceInfo.ascentLine * num19;
						num16 = this.m_currentFontAsset.faceInfo.descentLine * num19;
					}
					this.m_cached_TextElement = tmp_SpriteCharacter;
					this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
					this.m_internalCharacterInfo[this.m_characterCount].scale = num2;
					this.m_currentMaterialIndex = currentMaterialIndex;
				}
				else if (this.m_textElementType == TMP_TextElementType.Character)
				{
					this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
					if (this.m_cached_TextElement == null)
					{
						goto IL_19D3;
					}
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					float num20;
					if (flag5 && this.m_TextProcessingArray[num11].unicode == 10 && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num20 = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize * num14 / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					}
					else
					{
						num20 = this.m_currentFontSize * num14 / (float)this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					}
					if (flag5 && num12 == 8230)
					{
						num15 = 0f;
						num16 = 0f;
					}
					else
					{
						num15 = this.m_currentFontAsset.m_FaceInfo.ascentLine;
						num16 = this.m_currentFontAsset.m_FaceInfo.descentLine;
					}
					num2 = num20 * this.m_fontScaleMultiplier * this.m_cached_TextElement.scale;
					this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
				}
				float num21 = num2;
				if (num12 == 173 || num12 == 3)
				{
					num2 = 0f;
				}
				this.m_internalCharacterInfo[this.m_characterCount].character = (char)num12;
				GlyphMetrics metrics = this.m_cached_TextElement.m_Glyph.metrics;
				bool flag6 = num12 <= 65535 && char.IsWhiteSpace((char)num12);
				TMP_GlyphValueRecord tmp_GlyphValueRecord = default(TMP_GlyphValueRecord);
				float num22 = this.m_characterSpacing;
				this.m_GlyphHorizontalAdvanceAdjustment = 0f;
				if (this.m_enableKerning)
				{
					uint glyphIndex = this.m_cached_TextElement.m_GlyphIndex;
					if (this.m_characterCount < totalCharacterCount - 1)
					{
						uint key = this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16 | glyphIndex;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord = tmp_GlyphPairAdjustmentRecord.m_FirstAdjustmentRecord.m_GlyphValueRecord;
							num22 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num22);
						}
					}
					if (this.m_characterCount >= 1)
					{
						uint glyphIndex2 = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
						uint key2 = glyphIndex << 16 | glyphIndex2;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord += tmp_GlyphPairAdjustmentRecord.m_SecondAdjustmentRecord.m_GlyphValueRecord;
							num22 = (((tmp_GlyphPairAdjustmentRecord.m_FeatureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num22);
						}
					}
					this.m_GlyphHorizontalAdvanceAdjustment = tmp_GlyphValueRecord.m_XAdvance;
				}
				float num23 = 0f;
				if (this.m_monoSpacing != 0f)
				{
					num23 = (this.m_monoSpacing / 2f - (this.m_cached_TextElement.glyph.metrics.width / 2f + this.m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
					this.m_xAdvance += num23;
				}
				float num24 = 0f;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					num24 = this.m_currentFontAsset.boldSpacing;
				}
				this.m_internalCharacterInfo[this.m_characterCount].baseLine = 0f - this.m_lineOffset + this.m_baselineOffset;
				float num25 = (this.m_textElementType == TMP_TextElementType.Character) ? (num15 * num2 / num14 + this.m_baselineOffset) : (num15 * num2 + this.m_baselineOffset);
				float num26 = (this.m_textElementType == TMP_TextElementType.Character) ? (num16 * num2 / num14 + this.m_baselineOffset) : (num16 * num2 + this.m_baselineOffset);
				float num27 = num25;
				float num28 = num26;
				bool flag7 = this.m_characterCount == this.m_firstCharacterOfLine;
				if (flag7 || !flag6)
				{
					if (this.m_baselineOffset != 0f)
					{
						num27 = Mathf.Max((num25 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num27);
						num28 = Mathf.Min((num26 - this.m_baselineOffset) / this.m_fontScaleMultiplier, num28);
					}
					this.m_maxLineAscender = Mathf.Max(num27, this.m_maxLineAscender);
					this.m_maxLineDescender = Mathf.Min(num28, this.m_maxLineDescender);
				}
				if (flag7 || !flag6)
				{
					this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender = num27;
					this.m_internalCharacterInfo[this.m_characterCount].adjustedDescender = num28;
					this.m_ElementAscender = (this.m_internalCharacterInfo[this.m_characterCount].ascender = num25 - this.m_lineOffset);
					this.m_ElementDescender = (this.m_internalCharacterInfo[this.m_characterCount].descender = num26 - this.m_lineOffset);
				}
				else
				{
					this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender = this.m_maxLineAscender;
					this.m_internalCharacterInfo[this.m_characterCount].adjustedDescender = this.m_maxLineDescender;
					this.m_ElementAscender = (this.m_internalCharacterInfo[this.m_characterCount].ascender = this.m_maxLineAscender - this.m_lineOffset);
					this.m_ElementDescender = (this.m_internalCharacterInfo[this.m_characterCount].descender = this.m_maxLineDescender - this.m_lineOffset);
				}
				if ((this.m_lineNumber == 0 || this.m_isNewPage) && (flag7 || !flag6))
				{
					this.m_maxTextAscender = this.m_maxLineAscender;
					this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * num2 / num14);
				}
				if (this.m_lineOffset == 0f && (!flag6 || this.m_characterCount == this.m_firstCharacterOfLine))
				{
					this.m_PageAscender = ((this.m_PageAscender > num25) ? this.m_PageAscender : num25);
				}
				bool flag8 = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
				if (num12 == 9 || (!flag6 && num12 != 8203 && num12 != 173 && num12 != 3) || (num12 == 173 && !flag4) || this.m_textElementType == TMP_TextElementType.Sprite)
				{
					num7 = ((this.m_width != -1f) ? Mathf.Min(x + 0.0001f - this.m_marginLeft - this.m_marginRight, this.m_width) : (x + 0.0001f - this.m_marginLeft - this.m_marginRight));
					num10 = Mathf.Abs(this.m_xAdvance) + metrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * ((num12 == 173) ? num21 : num2);
					int characterCount = this.m_characterCount;
					if (num10 > num7 * (flag8 ? 1.05f : 1f) && isWordWrappingEnabled && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num11 = this.RestoreWordWrappingState(ref wordWrapState);
						if (this.m_internalCharacterInfo[this.m_characterCount - 1].character == '­' && !flag4 && this.m_overflowMode == TextOverflowModes.Overflow)
						{
							characterSubstitution.index = this.m_characterCount - 1;
							characterSubstitution.unicode = 45U;
							num11--;
							this.m_characterCount--;
							goto IL_19D3;
						}
						flag4 = false;
						if (this.m_internalCharacterInfo[this.m_characterCount].character == '­')
						{
							flag4 = true;
							goto IL_19D3;
						}
						if (isTextAutoSizingEnabled && flag2)
						{
							if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								float num29 = num10;
								if (this.m_charWidthAdjDelta > 0f)
								{
									num29 /= 1f - this.m_charWidthAdjDelta;
								}
								float num30 = num10 - (num7 - 0.0001f) * (flag8 ? 1.05f : 1f);
								this.m_charWidthAdjDelta += num30 / num29;
								this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
								return Vector2.zero;
							}
							if (fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
							{
								this.m_maxFontSize = fontSize;
								float num31 = Mathf.Max((fontSize - this.m_minFontSize) / 2f, 0.05f);
								fontSize -= num31;
								fontSize = Mathf.Max((float)((int)(fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
								return Vector2.zero;
							}
						}
						float num32 = this.m_maxLineAscender - this.m_startOfLineAscender;
						if (this.m_lineOffset > 0f && Math.Abs(num32) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
						{
							this.m_ElementDescender -= num32;
							this.m_lineOffset += num32;
						}
						float num33 = this.m_maxLineAscender - this.m_lineOffset;
						float num34 = this.m_maxLineDescender - this.m_lineOffset;
						this.m_ElementDescender = ((this.m_ElementDescender < num34) ? this.m_ElementDescender : num34);
						if (!flag)
						{
							float elementDescender = this.m_ElementDescender;
						}
						if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
						{
							flag = true;
						}
						this.m_firstCharacterOfLine = this.m_characterCount;
						this.m_lineVisibleCharacterCount = 0;
						num8 += this.m_xAdvance;
						if (isWordWrappingEnabled)
						{
							num9 = this.m_maxTextAscender - this.m_ElementDescender;
						}
						else
						{
							num9 = Mathf.Max(num9, num33 - num34);
						}
						this.SaveWordWrappingState(ref wordWrapState2, num11, this.m_characterCount - 1);
						this.m_lineNumber++;
						float adjustedAscender = this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender;
						if (this.m_lineHeight == -32767f)
						{
							this.m_lineOffset += 0f - this.m_maxLineDescender + adjustedAscender + (num4 + this.m_lineSpacingDelta) * num + this.m_lineSpacing * num3;
							this.m_IsDrivenLineSpacing = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * num3;
							this.m_IsDrivenLineSpacing = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = adjustedAscender;
						this.m_xAdvance = 0f + this.tag_Indent;
						flag2 = true;
						goto IL_19D3;
					}
					else
					{
						num5 = this.m_marginLeft;
						num6 = this.m_marginRight;
					}
				}
				if (num12 == 9)
				{
					float num35 = this.m_currentFontAsset.faceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * num2;
					float num36 = Mathf.Ceil(this.m_xAdvance / num35) * num35;
					this.m_xAdvance = ((num36 > this.m_xAdvance) ? num36 : (this.m_xAdvance + num35));
				}
				else if (this.m_monoSpacing != 0f)
				{
					this.m_xAdvance += (this.m_monoSpacing - num23 + (this.m_currentFontAsset.normalSpacingOffset + num22) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag6 || num12 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				else
				{
					this.m_xAdvance += ((metrics.horizontalAdvance + tmp_GlyphValueRecord.xAdvance) * num2 + (this.m_currentFontAsset.normalSpacingOffset + num22 + num24) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (flag6 || num12 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				if (num12 == 13)
				{
					a = Mathf.Max(a, num8 + this.m_xAdvance);
					num8 = 0f;
					this.m_xAdvance = 0f + this.tag_Indent;
				}
				if (num12 == 10 || num12 == 11 || num12 == 3 || num12 == 8232 || num12 == 8233 || this.m_characterCount == totalCharacterCount - 1)
				{
					float num37 = this.m_maxLineAscender - this.m_startOfLineAscender;
					if (this.m_lineOffset > 0f && Math.Abs(num37) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
					{
						this.m_ElementDescender -= num37;
						this.m_lineOffset += num37;
					}
					this.m_isNewPage = false;
					float num38 = this.m_maxLineDescender - this.m_lineOffset;
					this.m_ElementDescender = ((this.m_ElementDescender < num38) ? this.m_ElementDescender : num38);
					if (this.m_characterCount == totalCharacterCount - 1)
					{
						num8 = Mathf.Max(a, num8 + num10 + num5 + num6);
					}
					else
					{
						a = Mathf.Max(a, num8 + num10 + num5 + num6);
						num8 = 0f;
					}
					num9 = this.m_maxTextAscender - this.m_ElementDescender;
					if (num12 == 10 || num12 == 11 || num12 == 45 || num12 == 8232 || num12 == 8233)
					{
						this.SaveWordWrappingState(ref wordWrapState2, num11, this.m_characterCount);
						this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
						this.m_lineNumber++;
						this.m_firstCharacterOfLine = this.m_characterCount + 1;
						float adjustedAscender2 = this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender;
						if (this.m_lineHeight == -32767f)
						{
							float num39 = 0f - this.m_maxLineDescender + adjustedAscender2 + (num4 + this.m_lineSpacingDelta) * num + (this.m_lineSpacing + ((num12 == 10 || num12 == 8233) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_lineOffset += num39;
							this.m_IsDrivenLineSpacing = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((num12 == 10 || num12 == 8233) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_IsDrivenLineSpacing = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = adjustedAscender2;
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						this.m_characterCount++;
						goto IL_19D3;
					}
					if (num12 == 3)
					{
						num11 = this.m_TextProcessingArray.Length;
					}
				}
				if (isWordWrappingEnabled || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis)
				{
					if ((flag6 || num12 == 8203 || num12 == 45 || num12 == 173) && !this.m_isNonBreakingSpace && num12 != 160 && num12 != 8199 && num12 != 8209 && num12 != 8239 && num12 != 8288)
					{
						this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
						flag2 = false;
						flag3 = false;
						wordWrapState3.previous_WordBreak = -1;
					}
					else if (!this.m_isNonBreakingSpace && ((((num12 > 4352 && num12 < 4607) || (num12 > 43360 && num12 < 43391) || (num12 > 44032 && num12 < 55295)) && !TMP_Settings.useModernHangulLineBreakingRules) || (num12 > 11904 && num12 < 40959) || (num12 > 63744 && num12 < 64255) || (num12 > 65072 && num12 < 65103) || (num12 > 65280 && num12 < 65519)))
					{
						bool flag9 = TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num12);
						bool flag10 = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.ContainsKey((int)this.m_internalCharacterInfo[this.m_characterCount + 1].character);
						if (flag2 || !flag9)
						{
							if (!flag10)
							{
								this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
								flag2 = false;
							}
							if (flag2)
							{
								if (flag6)
								{
									this.SaveWordWrappingState(ref wordWrapState3, num11, this.m_characterCount);
								}
								this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
							}
						}
						flag3 = true;
					}
					else if (flag3)
					{
						if (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num12))
						{
							this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
						}
						flag3 = false;
					}
					else if (flag2)
					{
						if (flag6 || (num12 == 173 && !flag4))
						{
							this.SaveWordWrappingState(ref wordWrapState3, num11, this.m_characterCount);
						}
						this.SaveWordWrappingState(ref wordWrapState, num11, this.m_characterCount);
						flag3 = false;
					}
				}
				this.m_characterCount++;
				goto IL_19D3;
			}
			float num40 = this.m_maxFontSize - this.m_minFontSize;
			if (isTextAutoSizingEnabled && num40 > 0.051f && fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = fontSize;
				float num41 = Mathf.Max((this.m_maxFontSize - fontSize) / 2f, 0.05f);
				fontSize += num41;
				fontSize = Mathf.Min((float)((int)(fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return Vector2.zero;
			}
			this.m_IsAutoSizePointSizeSet = true;
			this.m_isCalculatingPreferredValues = false;
			num8 += ((this.m_margin.x > 0f) ? this.m_margin.x : 0f);
			num8 += ((this.m_margin.z > 0f) ? this.m_margin.z : 0f);
			num9 += ((this.m_margin.y > 0f) ? this.m_margin.y : 0f);
			num9 += ((this.m_margin.w > 0f) ? this.m_margin.w : 0f);
			num8 = (float)((int)(num8 * 100f + 1f)) / 100f;
			num9 = (float)((int)(num9 * 100f + 1f)) / 100f;
			return new Vector2(num8, num9);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002CA34 File Offset: 0x0002AC34
		protected virtual Bounds GetCompoundBounds()
		{
			return default(Bounds);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002CA4A File Offset: 0x0002AC4A
		internal virtual Rect GetCanvasSpaceClippingRect()
		{
			return Rect.zero;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002CA54 File Offset: 0x0002AC54
		protected Bounds GetTextBounds()
		{
			if (this.m_textInfo == null || this.m_textInfo.characterCount > this.m_textInfo.characterInfo.Length)
			{
				return default(Bounds);
			}
			Extents extents = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int num = 0;
			while (num < this.m_textInfo.characterCount && num < this.m_textInfo.characterInfo.Length)
			{
				if (this.m_textInfo.characterInfo[num].isVisible)
				{
					extents.min.x = Mathf.Min(extents.min.x, this.m_textInfo.characterInfo[num].origin);
					extents.min.y = Mathf.Min(extents.min.y, this.m_textInfo.characterInfo[num].descender);
					extents.max.x = Mathf.Max(extents.max.x, this.m_textInfo.characterInfo[num].xAdvance);
					extents.max.y = Mathf.Max(extents.max.y, this.m_textInfo.characterInfo[num].ascender);
				}
				num++;
			}
			Vector2 v;
			v.x = extents.max.x - extents.min.x;
			v.y = extents.max.y - extents.min.y;
			return new Bounds((extents.min + extents.max) / 2f, v);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0002CC14 File Offset: 0x0002AE14
		protected Bounds GetTextBounds(bool onlyVisibleCharacters)
		{
			if (this.m_textInfo == null)
			{
				return default(Bounds);
			}
			Extents extents = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int num = 0;
			while (num < this.m_textInfo.characterCount && ((num <= this.maxVisibleCharacters && this.m_textInfo.characterInfo[num].lineNumber <= this.m_maxVisibleLines) || !onlyVisibleCharacters))
			{
				if (!onlyVisibleCharacters || this.m_textInfo.characterInfo[num].isVisible)
				{
					extents.min.x = Mathf.Min(extents.min.x, this.m_textInfo.characterInfo[num].origin);
					extents.min.y = Mathf.Min(extents.min.y, this.m_textInfo.characterInfo[num].descender);
					extents.max.x = Mathf.Max(extents.max.x, this.m_textInfo.characterInfo[num].xAdvance);
					extents.max.y = Mathf.Max(extents.max.y, this.m_textInfo.characterInfo[num].ascender);
				}
				num++;
			}
			Vector2 v;
			v.x = extents.max.x - extents.min.x;
			v.y = extents.max.y - extents.min.y;
			return new Bounds((extents.min + extents.max) / 2f, v);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		protected void AdjustLineOffset(int startIndex, int endIndex, float offset)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
				int num = i;
				characterInfo[num].bottomLeft = characterInfo[num].bottomLeft - vector;
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num2 = i;
				characterInfo2[num2].topLeft = characterInfo2[num2].topLeft - vector;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num3 = i;
				characterInfo3[num3].topRight = characterInfo3[num3].topRight - vector;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num4 = i;
				characterInfo4[num4].bottomRight = characterInfo4[num4].bottomRight - vector;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num5 = i;
				characterInfo5[num5].ascender = characterInfo5[num5].ascender - vector.y;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num6 = i;
				characterInfo6[num6].baseLine = characterInfo6[num6].baseLine - vector.y;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num7 = i;
				characterInfo7[num7].descender = characterInfo7[num7].descender - vector.y;
				if (this.m_textInfo.characterInfo[i].isVisible)
				{
					TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
					int num8 = i;
					characterInfo8[num8].vertex_BL.position = characterInfo8[num8].vertex_BL.position - vector;
					TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
					int num9 = i;
					characterInfo9[num9].vertex_TL.position = characterInfo9[num9].vertex_TL.position - vector;
					TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
					int num10 = i;
					characterInfo10[num10].vertex_TR.position = characterInfo10[num10].vertex_TR.position - vector;
					TMP_CharacterInfo[] characterInfo11 = this.m_textInfo.characterInfo;
					int num11 = i;
					characterInfo11[num11].vertex_BR.position = characterInfo11[num11].vertex_BR.position - vector;
				}
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
		protected void ResizeLineExtents(int size)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
			TMP_LineInfo[] array = new TMP_LineInfo[size];
			for (int i = 0; i < size; i++)
			{
				if (i < this.m_textInfo.lineInfo.Length)
				{
					array[i] = this.m_textInfo.lineInfo[i];
				}
				else
				{
					array[i].lineExtents.min = TMP_Text.k_LargePositiveVector2;
					array[i].lineExtents.max = TMP_Text.k_LargeNegativeVector2;
					array[i].ascender = TMP_Text.k_LargeNegativeFloat;
					array[i].descender = TMP_Text.k_LargePositiveFloat;
				}
			}
			this.m_textInfo.lineInfo = array;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0002D097 File Offset: 0x0002B297
		public virtual TMP_TextInfo GetTextInfo(string text)
		{
			return null;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002D09A File Offset: 0x0002B29A
		public virtual void ComputeMarginSize()
		{
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002D09C File Offset: 0x0002B29C
		protected void InsertNewLine(int i, float baseScale, float currentElementScale, float currentEmScale, float glyphAdjustment, float boldSpacingAdjustment, float characterSpacingAdjustment, float width, float lineGap, ref bool isMaxVisibleDescenderSet, ref float maxVisibleDescender)
		{
			float num = this.m_maxLineAscender - this.m_startOfLineAscender;
			if (this.m_lineOffset > 0f && Math.Abs(num) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
			{
				this.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, num);
				this.m_ElementDescender -= num;
				this.m_lineOffset += num;
			}
			float num2 = this.m_maxLineAscender - this.m_lineOffset;
			float num3 = this.m_maxLineDescender - this.m_lineOffset;
			this.m_ElementDescender = ((this.m_ElementDescender < num3) ? this.m_ElementDescender : num3);
			if (!isMaxVisibleDescenderSet)
			{
				maxVisibleDescender = this.m_ElementDescender;
			}
			if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
			{
				isMaxVisibleDescenderSet = true;
			}
			this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
			this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
			int num4 = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = ((this.m_characterCount - 1 > 0) ? (this.m_characterCount - 1) : 0));
			this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
			this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
			this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, num3);
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, num2);
			this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x;
			this.m_textInfo.lineInfo[this.m_lineNumber].width = width;
			float num5 = (glyphAdjustment * currentElementScale + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale - this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
			float xAdvance = this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance + (this.m_isRightToLeft ? num5 : (-num5));
			this.m_textInfo.characterInfo[num4].xAdvance = xAdvance;
			this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
			this.m_textInfo.lineInfo[this.m_lineNumber].ascender = num2;
			this.m_textInfo.lineInfo[this.m_lineNumber].descender = num3;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = num2 - num3 + lineGap * baseScale;
			this.m_firstCharacterOfLine = this.m_characterCount;
			this.m_lineVisibleCharacterCount = 0;
			this.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, i, this.m_characterCount - 1);
			this.m_lineNumber++;
			if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
			{
				this.ResizeLineExtents(this.m_lineNumber);
			}
			if (this.m_lineHeight == -32767f)
			{
				float adjustedAscender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
				float num6 = 0f - this.m_maxLineDescender + adjustedAscender + (lineGap + this.m_lineSpacingDelta) * baseScale + this.m_lineSpacing * currentEmScale;
				this.m_lineOffset += num6;
				this.m_startOfLineAscender = adjustedAscender;
			}
			else
			{
				this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * currentEmScale;
			}
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_xAdvance = 0f + this.tag_Indent;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		protected void SaveWordWrappingState(ref WordWrapState state, int index, int count)
		{
			state.currentFontAsset = this.m_currentFontAsset;
			state.currentSpriteAsset = this.m_currentSpriteAsset;
			state.currentMaterial = this.m_currentMaterial;
			state.currentMaterialIndex = this.m_currentMaterialIndex;
			state.previous_WordBreak = index;
			state.total_CharacterCount = count;
			state.visible_CharacterCount = this.m_lineVisibleCharacterCount;
			state.visible_LinkCount = this.m_textInfo.linkCount;
			state.firstCharacterIndex = this.m_firstCharacterOfLine;
			state.firstVisibleCharacterIndex = this.m_firstVisibleCharacterOfLine;
			state.lastVisibleCharIndex = this.m_lastVisibleCharacterOfLine;
			state.fontStyle = this.m_FontStyleInternal;
			state.italicAngle = this.m_ItalicAngle;
			state.fontScaleMultiplier = this.m_fontScaleMultiplier;
			state.currentFontSize = this.m_currentFontSize;
			state.xAdvance = this.m_xAdvance;
			state.maxCapHeight = this.m_maxCapHeight;
			state.maxAscender = this.m_maxTextAscender;
			state.maxDescender = this.m_ElementDescender;
			state.startOfLineAscender = this.m_startOfLineAscender;
			state.maxLineAscender = this.m_maxLineAscender;
			state.maxLineDescender = this.m_maxLineDescender;
			state.pageAscender = this.m_PageAscender;
			state.preferredWidth = this.m_preferredWidth;
			state.preferredHeight = this.m_preferredHeight;
			state.meshExtents = this.m_meshExtents;
			state.lineNumber = this.m_lineNumber;
			state.lineOffset = this.m_lineOffset;
			state.baselineOffset = this.m_baselineOffset;
			state.isDrivenLineSpacing = this.m_IsDrivenLineSpacing;
			state.glyphHorizontalAdvanceAdjustment = this.m_GlyphHorizontalAdvanceAdjustment;
			state.cSpace = this.m_cSpacing;
			state.mSpace = this.m_monoSpacing;
			state.horizontalAlignment = this.m_lineJustification;
			state.marginLeft = this.m_marginLeft;
			state.marginRight = this.m_marginRight;
			state.vertexColor = this.m_htmlColor;
			state.underlineColor = this.m_underlineColor;
			state.strikethroughColor = this.m_strikethroughColor;
			state.isNonBreakingSpace = this.m_isNonBreakingSpace;
			state.tagNoParsing = this.tag_NoParsing;
			state.basicStyleStack = this.m_fontStyleStack;
			state.italicAngleStack = this.m_ItalicAngleStack;
			state.colorStack = this.m_colorStack;
			state.underlineColorStack = this.m_underlineColorStack;
			state.strikethroughColorStack = this.m_strikethroughColorStack;
			state.highlightStateStack = this.m_HighlightStateStack;
			state.colorGradientStack = this.m_colorGradientStack;
			state.sizeStack = this.m_sizeStack;
			state.indentStack = this.m_indentStack;
			state.fontWeightStack = this.m_FontWeightStack;
			state.baselineStack = this.m_baselineOffsetStack;
			state.actionStack = this.m_actionStack;
			state.materialReferenceStack = TMP_Text.m_materialReferenceStack;
			state.lineJustificationStack = this.m_lineJustificationStack;
			state.spriteAnimationID = this.m_spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				state.lineInfo = this.m_textInfo.lineInfo[this.m_lineNumber];
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		protected int RestoreWordWrappingState(ref WordWrapState state)
		{
			int previous_WordBreak = state.previous_WordBreak;
			this.m_currentFontAsset = state.currentFontAsset;
			this.m_currentSpriteAsset = state.currentSpriteAsset;
			this.m_currentMaterial = state.currentMaterial;
			this.m_currentMaterialIndex = state.currentMaterialIndex;
			this.m_characterCount = state.total_CharacterCount + 1;
			this.m_lineVisibleCharacterCount = state.visible_CharacterCount;
			this.m_textInfo.linkCount = state.visible_LinkCount;
			this.m_firstCharacterOfLine = state.firstCharacterIndex;
			this.m_firstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
			this.m_lastVisibleCharacterOfLine = state.lastVisibleCharIndex;
			this.m_FontStyleInternal = state.fontStyle;
			this.m_ItalicAngle = state.italicAngle;
			this.m_fontScaleMultiplier = state.fontScaleMultiplier;
			this.m_currentFontSize = state.currentFontSize;
			this.m_xAdvance = state.xAdvance;
			this.m_maxCapHeight = state.maxCapHeight;
			this.m_maxTextAscender = state.maxAscender;
			this.m_ElementDescender = state.maxDescender;
			this.m_startOfLineAscender = state.startOfLineAscender;
			this.m_maxLineAscender = state.maxLineAscender;
			this.m_maxLineDescender = state.maxLineDescender;
			this.m_PageAscender = state.pageAscender;
			this.m_preferredWidth = state.preferredWidth;
			this.m_preferredHeight = state.preferredHeight;
			this.m_meshExtents = state.meshExtents;
			this.m_lineNumber = state.lineNumber;
			this.m_lineOffset = state.lineOffset;
			this.m_baselineOffset = state.baselineOffset;
			this.m_IsDrivenLineSpacing = state.isDrivenLineSpacing;
			this.m_GlyphHorizontalAdvanceAdjustment = state.glyphHorizontalAdvanceAdjustment;
			this.m_cSpacing = state.cSpace;
			this.m_monoSpacing = state.mSpace;
			this.m_lineJustification = state.horizontalAlignment;
			this.m_marginLeft = state.marginLeft;
			this.m_marginRight = state.marginRight;
			this.m_htmlColor = state.vertexColor;
			this.m_underlineColor = state.underlineColor;
			this.m_strikethroughColor = state.strikethroughColor;
			this.m_isNonBreakingSpace = state.isNonBreakingSpace;
			this.tag_NoParsing = state.tagNoParsing;
			this.m_fontStyleStack = state.basicStyleStack;
			this.m_ItalicAngleStack = state.italicAngleStack;
			this.m_colorStack = state.colorStack;
			this.m_underlineColorStack = state.underlineColorStack;
			this.m_strikethroughColorStack = state.strikethroughColorStack;
			this.m_HighlightStateStack = state.highlightStateStack;
			this.m_colorGradientStack = state.colorGradientStack;
			this.m_sizeStack = state.sizeStack;
			this.m_indentStack = state.indentStack;
			this.m_FontWeightStack = state.fontWeightStack;
			this.m_baselineOffsetStack = state.baselineStack;
			this.m_actionStack = state.actionStack;
			TMP_Text.m_materialReferenceStack = state.materialReferenceStack;
			this.m_lineJustificationStack = state.lineJustificationStack;
			this.m_spriteAnimationID = state.spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				this.m_textInfo.lineInfo[this.m_lineNumber] = state.lineInfo;
			}
			return previous_WordBreak;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002DB94 File Offset: 0x0002BD94
		protected virtual void SaveGlyphVertexInfo(float padding, float style_padding, Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			vertexColor.a = ((this.m_fontColor32.a < vertexColor.a) ? this.m_fontColor32.a : vertexColor.a);
			if (!this.m_enableVertexGradient)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (!this.m_overrideHtmlColors && this.m_colorStack.index > 1)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (this.m_fontColorGradientPreset != null)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradientPreset.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradientPreset.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradientPreset.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradientPreset.bottomRight * vertexColor;
			}
			else
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradient.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradient.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradient.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradient.bottomRight * vertexColor;
			}
			if (this.m_colorGradientPreset != null)
			{
				if (this.m_colorGradientPresetIsTinted)
				{
					TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
					int characterCount = this.m_characterCount;
					characterInfo[characterCount].vertex_BL.color = characterInfo[characterCount].vertex_BL.color * this.m_colorGradientPreset.bottomLeft;
					TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
					int characterCount2 = this.m_characterCount;
					characterInfo2[characterCount2].vertex_TL.color = characterInfo2[characterCount2].vertex_TL.color * this.m_colorGradientPreset.topLeft;
					TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
					int characterCount3 = this.m_characterCount;
					characterInfo3[characterCount3].vertex_TR.color = characterInfo3[characterCount3].vertex_TR.color * this.m_colorGradientPreset.topRight;
					TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
					int characterCount4 = this.m_characterCount;
					characterInfo4[characterCount4].vertex_BR.color = characterInfo4[characterCount4].vertex_BR.color * this.m_colorGradientPreset.bottomRight;
				}
				else
				{
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_colorGradientPreset.bottomLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_colorGradientPreset.topLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_colorGradientPreset.topRight.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_colorGradientPreset.bottomRight.MinAlpha(vertexColor);
				}
			}
			if (!this.m_isSDFShader)
			{
				style_padding = 0f;
			}
			GlyphRect glyphRect = this.m_cached_TextElement.m_Glyph.glyphRect;
			Vector2 vector;
			vector.x = ((float)glyphRect.x - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasWidth;
			vector.y = ((float)glyphRect.y - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 vector2;
			vector2.x = vector.x;
			vector2.y = ((float)glyphRect.y + padding + style_padding + (float)glyphRect.height) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 vector3;
			vector3.x = ((float)glyphRect.x + padding + style_padding + (float)glyphRect.width) / (float)this.m_currentFontAsset.m_AtlasWidth;
			vector3.y = vector2.y;
			Vector2 uv;
			uv.x = vector3.x;
			uv.y = vector.y;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = vector;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = vector2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = vector3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = uv;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002E370 File Offset: 0x0002C570
		protected virtual void SaveSpriteVertexInfo(Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			if (this.m_tintAllSprites)
			{
				this.m_tintSprite = true;
			}
			Color32 color = this.m_tintSprite ? this.m_spriteColor.Multiply(vertexColor) : this.m_spriteColor;
			color.a = ((color.a < this.m_fontColor32.a) ? (color.a = ((color.a < vertexColor.a) ? color.a : vertexColor.a)) : this.m_fontColor32.a);
			Color32 color2 = color;
			Color32 color3 = color;
			Color32 color4 = color;
			Color32 color5 = color;
			if (this.m_enableVertexGradient)
			{
				if (this.m_fontColorGradientPreset != null)
				{
					color2 = (this.m_tintSprite ? color2.Multiply(this.m_fontColorGradientPreset.bottomLeft) : color2);
					color3 = (this.m_tintSprite ? color3.Multiply(this.m_fontColorGradientPreset.topLeft) : color3);
					color4 = (this.m_tintSprite ? color4.Multiply(this.m_fontColorGradientPreset.topRight) : color4);
					color5 = (this.m_tintSprite ? color5.Multiply(this.m_fontColorGradientPreset.bottomRight) : color5);
				}
				else
				{
					color2 = (this.m_tintSprite ? color2.Multiply(this.m_fontColorGradient.bottomLeft) : color2);
					color3 = (this.m_tintSprite ? color3.Multiply(this.m_fontColorGradient.topLeft) : color3);
					color4 = (this.m_tintSprite ? color4.Multiply(this.m_fontColorGradient.topRight) : color4);
					color5 = (this.m_tintSprite ? color5.Multiply(this.m_fontColorGradient.bottomRight) : color5);
				}
			}
			if (this.m_colorGradientPreset != null)
			{
				color2 = (this.m_tintSprite ? color2.Multiply(this.m_colorGradientPreset.bottomLeft) : color2);
				color3 = (this.m_tintSprite ? color3.Multiply(this.m_colorGradientPreset.topLeft) : color3);
				color4 = (this.m_tintSprite ? color4.Multiply(this.m_colorGradientPreset.topRight) : color4);
				color5 = (this.m_tintSprite ? color5.Multiply(this.m_colorGradientPreset.bottomRight) : color5);
			}
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = color2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = color3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = color4;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = color5;
			GlyphRect glyphRect = this.m_cached_TextElement.m_Glyph.glyphRect;
			Vector2 vector = new Vector2((float)glyphRect.x / (float)this.m_currentSpriteAsset.spriteSheet.width, (float)glyphRect.y / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 vector2 = new Vector2(vector.x, (float)(glyphRect.y + glyphRect.height) / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 vector3 = new Vector2((float)(glyphRect.x + glyphRect.width) / (float)this.m_currentSpriteAsset.spriteSheet.width, vector2.y);
			Vector2 uv = new Vector2(vector3.x, vector.y);
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = vector;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = vector2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = vector3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = uv;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002E890 File Offset: 0x0002CA90
		protected virtual void FillCharacterVertexBuffers(int i, int index_X4)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialReferenceIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + 4) / 4));
			}
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0002EC2C File Offset: 0x0002CE2C
		protected virtual void FillCharacterVertexBuffers(int i, int index_X4, bool isVolumetric)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialReferenceIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + (isVolumetric ? 8 : 4)) / 4));
			}
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[4 + index_X4] = characterInfo[i].vertex_BL.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[5 + index_X4] = characterInfo[i].vertex_TL.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[6 + index_X4] = characterInfo[i].vertex_TR.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[7 + index_X4] = characterInfo[i].vertex_BR.uv;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[4 + index_X4] = characterInfo[i].vertex_BL.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[5 + index_X4] = characterInfo[i].vertex_TL.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[6 + index_X4] = characterInfo[i].vertex_TR.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[7 + index_X4] = characterInfo[i].vertex_BR.uv2;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			if (isVolumetric)
			{
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 128, byte.MaxValue);
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[4 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[5 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[6 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[7 + index_X4] = color;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + ((!isVolumetric) ? 4 : 8);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002F1F4 File Offset: 0x0002D3F4
		protected virtual void FillSpriteVertexBuffers(int i, int index_X4)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialReferenceIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + 4) / 4));
			}
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002F590 File Offset: 0x0002D790
		protected virtual void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor)
		{
			this.GetUnderlineSpecialCharacter(this.m_fontAsset);
			if (this.m_Underline.character == null)
			{
				if (!TMP_Settings.warningsDisabled)
				{
					UnityEngine.Debug.LogWarning("Unable to add underline since the primary Font Asset doesn't contain the underline character.", this);
				}
				return;
			}
			int materialIndex = this.m_Underline.materialIndex;
			int num = index + 12;
			if (num > this.m_textInfo.meshInfo[materialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialIndex].ResizeMeshInfo(num / 4);
			}
			start.y = Mathf.Min(start.y, end.y);
			end.y = Mathf.Min(start.y, end.y);
			GlyphMetrics metrics = this.m_Underline.character.glyph.metrics;
			GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
			float num2 = metrics.width / 2f * maxScale;
			if (end.x - start.x < metrics.width * maxScale)
			{
				num2 = (end.x - start.x) / 2f;
			}
			float num3 = this.m_padding * startScale / maxScale;
			float num4 = this.m_padding * endScale / maxScale;
			float underlineThickness = this.m_Underline.fontAsset.faceInfo.underlineThickness;
			Vector3[] vertices = this.m_textInfo.meshInfo[materialIndex].vertices;
			vertices[index] = start + new Vector3(0f, 0f - (underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 1] = start + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 2] = vertices[index + 1] + new Vector3(num2, 0f, 0f);
			vertices[index + 3] = vertices[index] + new Vector3(num2, 0f, 0f);
			vertices[index + 4] = vertices[index + 3];
			vertices[index + 5] = vertices[index + 2];
			vertices[index + 6] = end + new Vector3(-num2, this.m_padding * maxScale, 0f);
			vertices[index + 7] = end + new Vector3(-num2, -(underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 8] = vertices[index + 7];
			vertices[index + 9] = vertices[index + 6];
			vertices[index + 10] = end + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 11] = end + new Vector3(0f, -(underlineThickness + this.m_padding) * maxScale, 0f);
			Vector2[] uvs = this.m_textInfo.meshInfo[materialIndex].uvs0;
			int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
			int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
			Vector2 vector = new Vector2(((float)glyphRect.x - num3) / (float)atlasWidth, ((float)glyphRect.y - this.m_padding) / (float)atlasHeight);
			Vector2 vector2 = new Vector2(vector.x, ((float)(glyphRect.y + glyphRect.height) + this.m_padding) / (float)atlasHeight);
			Vector2 vector3 = new Vector2(((float)glyphRect.x - num3 + (float)glyphRect.width / 2f) / (float)atlasWidth, vector2.y);
			Vector2 vector4 = new Vector2(vector3.x, vector.y);
			Vector2 vector5 = new Vector2(((float)glyphRect.x + num4 + (float)glyphRect.width / 2f) / (float)atlasWidth, vector2.y);
			Vector2 vector6 = new Vector2(vector5.x, vector.y);
			Vector2 vector7 = new Vector2(((float)glyphRect.x + num4 + (float)glyphRect.width) / (float)atlasWidth, vector2.y);
			Vector2 vector8 = new Vector2(vector7.x, vector.y);
			uvs[index] = vector;
			uvs[1 + index] = vector2;
			uvs[2 + index] = vector3;
			uvs[3 + index] = vector4;
			uvs[4 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector.y);
			uvs[5 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector2.y);
			uvs[6 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector2.y);
			uvs[7 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector.y);
			uvs[8 + index] = vector6;
			uvs[9 + index] = vector5;
			uvs[10 + index] = vector7;
			uvs[11 + index] = vector8;
			float x = (vertices[index + 2].x - start.x) / (end.x - start.x);
			float scale = Mathf.Abs(sdfScale);
			Vector2[] uvs2 = this.m_textInfo.meshInfo[materialIndex].uvs2;
			uvs2[index] = this.PackUV(0f, 0f, scale);
			uvs2[1 + index] = this.PackUV(0f, 1f, scale);
			uvs2[2 + index] = this.PackUV(x, 1f, scale);
			uvs2[3 + index] = this.PackUV(x, 0f, scale);
			float x2 = (vertices[index + 4].x - start.x) / (end.x - start.x);
			x = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[4 + index] = this.PackUV(x2, 0f, scale);
			uvs2[5 + index] = this.PackUV(x2, 1f, scale);
			uvs2[6 + index] = this.PackUV(x, 1f, scale);
			uvs2[7 + index] = this.PackUV(x, 0f, scale);
			x2 = (vertices[index + 8].x - start.x) / (end.x - start.x);
			uvs2[8 + index] = this.PackUV(x2, 0f, scale);
			uvs2[9 + index] = this.PackUV(x2, 1f, scale);
			uvs2[10 + index] = this.PackUV(1f, 1f, scale);
			uvs2[11 + index] = this.PackUV(1f, 0f, scale);
			underlineColor.a = ((this.m_fontColor32.a < underlineColor.a) ? this.m_fontColor32.a : underlineColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[materialIndex].colors32;
			colors[index] = underlineColor;
			colors[1 + index] = underlineColor;
			colors[2 + index] = underlineColor;
			colors[3 + index] = underlineColor;
			colors[4 + index] = underlineColor;
			colors[5 + index] = underlineColor;
			colors[6 + index] = underlineColor;
			colors[7 + index] = underlineColor;
			colors[8 + index] = underlineColor;
			colors[9 + index] = underlineColor;
			colors[10 + index] = underlineColor;
			colors[11 + index] = underlineColor;
			index += 12;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002FDD8 File Offset: 0x0002DFD8
		protected virtual void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor)
		{
			if (this.m_Underline.character == null)
			{
				this.GetUnderlineSpecialCharacter(this.m_fontAsset);
				if (this.m_Underline.character == null)
				{
					if (!TMP_Settings.warningsDisabled)
					{
						UnityEngine.Debug.LogWarning("Unable to add highlight since the primary Font Asset doesn't contain the underline character.", this);
					}
					return;
				}
			}
			int materialIndex = this.m_Underline.materialIndex;
			int num = index + 4;
			if (num > this.m_textInfo.meshInfo[materialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialIndex].ResizeMeshInfo(num / 4);
			}
			Vector3[] vertices = this.m_textInfo.meshInfo[materialIndex].vertices;
			vertices[index] = start;
			vertices[index + 1] = new Vector3(start.x, end.y, 0f);
			vertices[index + 2] = end;
			vertices[index + 3] = new Vector3(end.x, start.y, 0f);
			Vector2[] uvs = this.m_textInfo.meshInfo[materialIndex].uvs0;
			int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
			int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
			GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
			Vector2 vector = new Vector2(((float)glyphRect.x + (float)(glyphRect.width / 2)) / (float)atlasWidth, ((float)glyphRect.y + (float)glyphRect.height / 2f) / (float)atlasHeight);
			uvs[index] = vector;
			uvs[1 + index] = vector;
			uvs[2 + index] = vector;
			uvs[3 + index] = vector;
			Vector2[] uvs2 = this.m_textInfo.meshInfo[materialIndex].uvs2;
			Vector2 vector2 = new Vector2(0f, 1f);
			uvs2[index] = vector2;
			uvs2[1 + index] = vector2;
			uvs2[2 + index] = vector2;
			uvs2[3 + index] = vector2;
			highlightColor.a = ((this.m_fontColor32.a < highlightColor.a) ? this.m_fontColor32.a : highlightColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[materialIndex].colors32;
			colors[index] = highlightColor;
			colors[1 + index] = highlightColor;
			colors[2 + index] = highlightColor;
			colors[3 + index] = highlightColor;
			index += 4;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0003004C File Offset: 0x0002E24C
		protected void LoadDefaultSettings()
		{
			if (this.m_fontSize == -99f || this.m_isWaitingOnResourceLoad)
			{
				this.m_rectTransform = this.rectTransform;
				if (TMP_Settings.autoSizeTextContainer)
				{
					this.autoSizeTextContainer = true;
				}
				else if (base.GetType() == typeof(TextMeshPro))
				{
					if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
					{
						this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProTextContainerSize;
					}
				}
				else if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
				{
					this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProUITextContainerSize;
				}
				this.m_enableWordWrapping = TMP_Settings.enableWordWrapping;
				this.m_enableKerning = TMP_Settings.enableKerning;
				this.m_enableExtraPadding = TMP_Settings.enableExtraPadding;
				this.m_tintAllSprites = TMP_Settings.enableTintAllSprites;
				this.m_parseCtrlCharacters = TMP_Settings.enableParseEscapeCharacters;
				this.m_fontSize = (this.m_fontSizeBase = TMP_Settings.defaultFontSize);
				this.m_fontSizeMin = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMinRatio;
				this.m_fontSizeMax = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMaxRatio;
				this.m_isWaitingOnResourceLoad = false;
				this.raycastTarget = TMP_Settings.enableRaycastTarget;
				this.m_IsTextObjectScaleStatic = TMP_Settings.isTextObjectScaleStatic;
			}
			else if (this.m_textAlignment < (TextAlignmentOptions)255)
			{
				this.m_textAlignment = TMP_Compatibility.ConvertTextAlignmentEnumValues(this.m_textAlignment);
			}
			if (this.m_textAlignment != TextAlignmentOptions.Converted)
			{
				this.m_HorizontalAlignment = (HorizontalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)255);
				this.m_VerticalAlignment = (VerticalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)65280);
				this.m_textAlignment = TextAlignmentOptions.Converted;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000301F0 File Offset: 0x0002E3F0
		protected void GetSpecialCharacters(TMP_FontAsset fontAsset)
		{
			this.GetEllipsisSpecialCharacter(fontAsset);
			this.GetUnderlineSpecialCharacter(fontAsset);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00030200 File Offset: 0x0002E400
		protected void GetEllipsisSpecialCharacter(TMP_FontAsset fontAsset)
		{
			bool flag;
			TMP_Character tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			if (tmp_Character == null && fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, fontAsset.m_FallbackFontAssetTable, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			}
			if (tmp_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			}
			if (tmp_Character == null && TMP_Settings.defaultFontAsset != null)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230U, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			}
			if (tmp_Character != null)
			{
				this.m_Ellipsis = new TMP_Text.SpecialCharacter(tmp_Character, 0);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000302D8 File Offset: 0x0002E4D8
		protected void GetUnderlineSpecialCharacter(TMP_FontAsset fontAsset)
		{
			bool flag;
			TMP_Character characterFromFontAsset = TMP_FontAssetUtilities.GetCharacterFromFontAsset(95U, fontAsset, false, FontStyles.Normal, FontWeight.Regular, out flag);
			if (characterFromFontAsset != null)
			{
				this.m_Underline = new TMP_Text.SpecialCharacter(characterFromFontAsset, 0);
				return;
			}
			if (!TMP_Settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("The character used for Underline is not available in font asset [" + fontAsset.name + "].", this);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0003032C File Offset: 0x0002E52C
		protected void ReplaceTagWithCharacter(int[] chars, int insertionIndex, int tagLength, char c)
		{
			chars[insertionIndex] = (int)c;
			for (int i = insertionIndex + tagLength; i < chars.Length; i++)
			{
				chars[i - 3] = chars[i];
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00030358 File Offset: 0x0002E558
		protected TMP_FontAsset GetFontAssetForWeight(int fontWeight)
		{
			bool flag = (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic || (this.m_fontStyle & FontStyles.Italic) == FontStyles.Italic;
			int num = fontWeight / 100;
			TMP_FontAsset result;
			if (flag)
			{
				result = this.m_currentFontAsset.fontWeightTable[num].italicTypeface;
			}
			else
			{
				result = this.m_currentFontAsset.fontWeightTable[num].regularTypeface;
			}
			return result;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000303B8 File Offset: 0x0002E5B8
		internal TMP_TextElement GetTextElement(uint unicode, TMP_FontAsset fontAsset, FontStyles fontStyle, FontWeight fontWeight, out bool isUsingAlternativeTypeface)
		{
			TMP_Character tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			if (tmp_Character != null)
			{
				return tmp_Character;
			}
			if (fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (tmp_Character != null)
			{
				return tmp_Character;
			}
			if (fontAsset.instanceID != this.m_fontAsset.instanceID)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
				if (tmp_Character != null)
				{
					this.m_currentMaterialIndex = 0;
					this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
					return tmp_Character;
				}
				if (this.m_fontAsset.m_FallbackFontAssetTable != null && this.m_fontAsset.m_FallbackFontAssetTable.Count > 0)
				{
					tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, this.m_fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
				}
				if (tmp_Character != null)
				{
					return tmp_Character;
				}
			}
			if (this.m_spriteAsset != null)
			{
				TMP_SpriteCharacter spriteCharacterFromSpriteAsset = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, this.m_spriteAsset, true);
				if (spriteCharacterFromSpriteAsset != null)
				{
					return spriteCharacterFromSpriteAsset;
				}
			}
			if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, TMP_Settings.fallbackFontAssets, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (tmp_Character != null)
			{
				return tmp_Character;
			}
			if (TMP_Settings.defaultFontAsset != null)
			{
				tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, TMP_Settings.defaultFontAsset, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (tmp_Character != null)
			{
				return tmp_Character;
			}
			if (TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteCharacter spriteCharacterFromSpriteAsset2 = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, TMP_Settings.defaultSpriteAsset, true);
				if (spriteCharacterFromSpriteAsset2 != null)
				{
					return spriteCharacterFromSpriteAsset2;
				}
			}
			return null;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0003051E File Offset: 0x0002E71E
		protected virtual void SetActiveSubMeshes(bool state)
		{
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00030520 File Offset: 0x0002E720
		protected virtual void DestroySubMeshObjects()
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00030522 File Offset: 0x0002E722
		public virtual void ClearMesh()
		{
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00030524 File Offset: 0x0002E724
		public virtual void ClearMesh(bool uploadGeometry)
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00030528 File Offset: 0x0002E728
		public virtual string GetParsedText()
		{
			if (this.m_textInfo == null)
			{
				return string.Empty;
			}
			int characterCount = this.m_textInfo.characterCount;
			char[] array = new char[characterCount];
			int num = 0;
			while (num < characterCount && num < this.m_textInfo.characterInfo.Length)
			{
				array[num] = this.m_textInfo.characterInfo[num].character;
				num++;
			}
			return new string(array);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00030591 File Offset: 0x0002E791
		internal bool IsSelfOrLinkedAncestor(TMP_Text targetTextComponent)
		{
			return targetTextComponent == null || (this.parentLinkedComponent != null && this.parentLinkedComponent.IsSelfOrLinkedAncestor(targetTextComponent)) || base.GetInstanceID() == targetTextComponent.GetInstanceID();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000305D0 File Offset: 0x0002E7D0
		internal void ReleaseLinkedTextComponent(TMP_Text targetTextComponent)
		{
			if (targetTextComponent == null)
			{
				return;
			}
			TMP_Text linkedTextComponent = targetTextComponent.linkedTextComponent;
			if (linkedTextComponent != null)
			{
				this.ReleaseLinkedTextComponent(linkedTextComponent);
			}
			targetTextComponent.text = string.Empty;
			targetTextComponent.firstVisibleCharacter = 0;
			targetTextComponent.linkedTextComponent = null;
			targetTextComponent.parentLinkedComponent = null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00030620 File Offset: 0x0002E820
		protected Vector2 PackUV(float x, float y, float scale)
		{
			Vector2 vector;
			vector.x = (float)((int)(x * 511f));
			vector.y = (float)((int)(y * 511f));
			vector.x = vector.x * 4096f + vector.y;
			vector.y = scale;
			return vector;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00030670 File Offset: 0x0002E870
		protected float PackUV(float x, float y)
		{
			float num = (float)((double)((int)(x * 511f)));
			double num2 = (double)((int)(y * 511f));
			return (float)((double)num * 4096.0 + num2);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0003069D File Offset: 0x0002E89D
		internal virtual void InternalUpdate()
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000306A0 File Offset: 0x0002E8A0
		protected int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00030770 File Offset: 0x0002E970
		protected int GetUTF16(string text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 12) + (this.HexToInt(text[i + 1]) << 8) + (this.HexToInt(text[i + 2]) << 4) + this.HexToInt(text[i + 3]);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000307C3 File Offset: 0x0002E9C3
		protected int GetUTF16(int[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000307FF File Offset: 0x0002E9FF
		internal int GetUTF16(uint[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0003083C File Offset: 0x0002EA3C
		protected int GetUTF16(StringBuilder text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 12) + (this.HexToInt(text[i + 1]) << 8) + (this.HexToInt(text[i + 2]) << 4) + this.HexToInt(text[i + 3]);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00030890 File Offset: 0x0002EA90
		private int GetUTF16(TMP_Text.TextBackingContainer text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000308EC File Offset: 0x0002EAEC
		protected int GetUTF32(string text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 28) + (this.HexToInt(text[i + 1]) << 24) + (this.HexToInt(text[i + 2]) << 20) + (this.HexToInt(text[i + 3]) << 16) + (this.HexToInt(text[i + 4]) << 12) + (this.HexToInt(text[i + 5]) << 8) + (this.HexToInt(text[i + 6]) << 4) + this.HexToInt(text[i + 7]);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0003098C File Offset: 0x0002EB8C
		protected int GetUTF32(int[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00030A14 File Offset: 0x0002EC14
		internal int GetUTF32(uint[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00030A9C File Offset: 0x0002EC9C
		protected int GetUTF32(StringBuilder text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 28) + (this.HexToInt(text[i + 1]) << 24) + (this.HexToInt(text[i + 2]) << 20) + (this.HexToInt(text[i + 3]) << 16) + (this.HexToInt(text[i + 4]) << 12) + (this.HexToInt(text[i + 5]) << 8) + (this.HexToInt(text[i + 6]) << 4) + this.HexToInt(text[i + 7]);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00030B3C File Offset: 0x0002ED3C
		private int GetUTF32(TMP_Text.TextBackingContainer text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00030BEC File Offset: 0x0002EDEC
		protected Color32 HexCharsToColor(char[] hexChars, int tagCount)
		{
			if (tagCount == 4)
			{
				byte r = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[1]));
				byte g = (byte)(this.HexToInt(hexChars[2]) * 16 + this.HexToInt(hexChars[2]));
				byte b = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[3]));
				return new Color32(r, g, b, byte.MaxValue);
			}
			if (tagCount == 5)
			{
				byte r2 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[1]));
				byte g2 = (byte)(this.HexToInt(hexChars[2]) * 16 + this.HexToInt(hexChars[2]));
				byte b2 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[3]));
				byte a = (byte)(this.HexToInt(hexChars[4]) * 16 + this.HexToInt(hexChars[4]));
				return new Color32(r2, g2, b2, a);
			}
			if (tagCount == 7)
			{
				byte r3 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[2]));
				byte g3 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[4]));
				byte b3 = (byte)(this.HexToInt(hexChars[5]) * 16 + this.HexToInt(hexChars[6]));
				return new Color32(r3, g3, b3, byte.MaxValue);
			}
			if (tagCount == 9)
			{
				byte r4 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[2]));
				byte g4 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[4]));
				byte b4 = (byte)(this.HexToInt(hexChars[5]) * 16 + this.HexToInt(hexChars[6]));
				byte a2 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				return new Color32(r4, g4, b4, a2);
			}
			if (tagCount == 10)
			{
				byte r5 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[7]));
				byte g5 = (byte)(this.HexToInt(hexChars[8]) * 16 + this.HexToInt(hexChars[8]));
				byte b5 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[9]));
				return new Color32(r5, g5, b5, byte.MaxValue);
			}
			if (tagCount == 11)
			{
				byte r6 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[7]));
				byte g6 = (byte)(this.HexToInt(hexChars[8]) * 16 + this.HexToInt(hexChars[8]));
				byte b6 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[9]));
				byte a3 = (byte)(this.HexToInt(hexChars[10]) * 16 + this.HexToInt(hexChars[10]));
				return new Color32(r6, g6, b6, a3);
			}
			if (tagCount == 13)
			{
				byte r7 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				byte g7 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[10]));
				byte b7 = (byte)(this.HexToInt(hexChars[11]) * 16 + this.HexToInt(hexChars[12]));
				return new Color32(r7, g7, b7, byte.MaxValue);
			}
			if (tagCount == 15)
			{
				byte r8 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				byte g8 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[10]));
				byte b8 = (byte)(this.HexToInt(hexChars[11]) * 16 + this.HexToInt(hexChars[12]));
				byte a4 = (byte)(this.HexToInt(hexChars[13]) * 16 + this.HexToInt(hexChars[14]));
				return new Color32(r8, g8, b8, a4);
			}
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00030F58 File Offset: 0x0002F158
		protected Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
		{
			if (length == 7)
			{
				byte r = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16 + this.HexToInt(hexChars[startIndex + 2]));
				byte g = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16 + this.HexToInt(hexChars[startIndex + 4]));
				byte b = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16 + this.HexToInt(hexChars[startIndex + 6]));
				return new Color32(r, g, b, byte.MaxValue);
			}
			if (length == 9)
			{
				byte r2 = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16 + this.HexToInt(hexChars[startIndex + 2]));
				byte g2 = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16 + this.HexToInt(hexChars[startIndex + 4]));
				byte b2 = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16 + this.HexToInt(hexChars[startIndex + 6]));
				byte a = (byte)(this.HexToInt(hexChars[startIndex + 7]) * 16 + this.HexToInt(hexChars[startIndex + 8]));
				return new Color32(r2, g2, b2, a);
			}
			return TMP_Text.s_colorWhite;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00031050 File Offset: 0x0002F250
		private int GetAttributeParameters(char[] chars, int startIndex, int length, ref float[] parameters)
		{
			int i = startIndex;
			int num = 0;
			while (i < startIndex + length)
			{
				parameters[num] = this.ConvertToFloat(chars, startIndex, length, out i);
				length -= i - startIndex + 1;
				startIndex = i + 1;
				num++;
			}
			return num;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0003108C File Offset: 0x0002F28C
		protected float ConvertToFloat(char[] chars, int startIndex, int length)
		{
			int num;
			return this.ConvertToFloat(chars, startIndex, length, out num);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000310A4 File Offset: 0x0002F2A4
		protected float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
		{
			if (startIndex == 0)
			{
				lastIndex = 0;
				return -32768f;
			}
			int num = startIndex + length;
			bool flag = true;
			float num2 = 0f;
			int num3 = 1;
			if (chars[startIndex] == '+')
			{
				num3 = 1;
				startIndex++;
			}
			else if (chars[startIndex] == '-')
			{
				num3 = -1;
				startIndex++;
			}
			float num4 = 0f;
			for (int i = startIndex; i < num; i++)
			{
				uint num5 = (uint)chars[i];
				if ((num5 >= 48U && num5 <= 57U) || num5 == 46U)
				{
					if (num5 == 46U)
					{
						flag = false;
						num2 = 0.1f;
					}
					else if (flag)
					{
						num4 = num4 * 10f + (float)((ulong)(num5 - 48U) * (ulong)((long)num3));
					}
					else
					{
						num4 += (num5 - 48U) * num2 * (float)num3;
						num2 *= 0.1f;
					}
				}
				else if (num5 == 44U)
				{
					if (i + 1 < num && chars[i + 1] == ' ')
					{
						lastIndex = i + 1;
					}
					else
					{
						lastIndex = i;
					}
					if (num4 > 32767f)
					{
						return -32768f;
					}
					return num4;
				}
			}
			lastIndex = num;
			if (num4 > 32767f)
			{
				return -32768f;
			}
			return num4;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000311B0 File Offset: 0x0002F3B0
		internal bool ValidateHtmlTag(TMP_Text.UnicodeChar[] chars, int startIndex, out int endIndex)
		{
			int num = 0;
			byte b = 0;
			int num2 = 0;
			TMP_Text.m_xmlAttribute[num2].nameHashCode = 0;
			TMP_Text.m_xmlAttribute[num2].valueHashCode = 0;
			TMP_Text.m_xmlAttribute[num2].valueStartIndex = 0;
			TMP_Text.m_xmlAttribute[num2].valueLength = 0;
			TagValueType tagValueType = TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.None;
			TagUnitType tagUnitType = TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
			TMP_Text.m_xmlAttribute[1].nameHashCode = 0;
			TMP_Text.m_xmlAttribute[2].nameHashCode = 0;
			TMP_Text.m_xmlAttribute[3].nameHashCode = 0;
			TMP_Text.m_xmlAttribute[4].nameHashCode = 0;
			endIndex = startIndex;
			bool flag = false;
			bool flag2 = false;
			int num3 = startIndex;
			while (num3 < chars.Length && chars[num3].unicode != 0 && num < TMP_Text.m_htmlTag.Length && chars[num3].unicode != 60)
			{
				int unicode = chars[num3].unicode;
				if (unicode == 62)
				{
					flag2 = true;
					endIndex = num3;
					TMP_Text.m_htmlTag[num] = '\0';
					break;
				}
				TMP_Text.m_htmlTag[num] = (char)unicode;
				num++;
				if (b == 1)
				{
					if (tagValueType == TagValueType.None)
					{
						if (unicode == 43 || unicode == 45 || unicode == 46 || (unicode >= 48 && unicode <= 57))
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.NumericalValue);
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = num - 1;
							RichTextTagAttribute[] xmlAttribute = TMP_Text.m_xmlAttribute;
							int num4 = num2;
							xmlAttribute[num4].valueLength = xmlAttribute[num4].valueLength + 1;
						}
						else if (unicode == 35)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.ColorValue);
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = num - 1;
							RichTextTagAttribute[] xmlAttribute2 = TMP_Text.m_xmlAttribute;
							int num5 = num2;
							xmlAttribute2[num5].valueLength = xmlAttribute2[num5].valueLength + 1;
						}
						else if (unicode == 34)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = num;
						}
						else
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = num - 1;
							TMP_Text.m_xmlAttribute[num2].valueHashCode = ((TMP_Text.m_xmlAttribute[num2].valueHashCode << 5) + TMP_Text.m_xmlAttribute[num2].valueHashCode ^ unicode);
							RichTextTagAttribute[] xmlAttribute3 = TMP_Text.m_xmlAttribute;
							int num6 = num2;
							xmlAttribute3[num6].valueLength = xmlAttribute3[num6].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.NumericalValue)
					{
						if (unicode == 112 || unicode == 101 || unicode == 37 || unicode == 32)
						{
							b = 2;
							tagValueType = TagValueType.None;
							if (unicode != 37)
							{
								if (unicode == 101)
								{
									tagUnitType = (TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.FontUnits);
								}
								else
								{
									tagUnitType = (TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels);
								}
							}
							else
							{
								tagUnitType = (TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Percentage);
							}
							num2++;
							TMP_Text.m_xmlAttribute[num2].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[num2].valueLength = 0;
						}
						else if (b != 2)
						{
							RichTextTagAttribute[] xmlAttribute4 = TMP_Text.m_xmlAttribute;
							int num7 = num2;
							xmlAttribute4[num7].valueLength = xmlAttribute4[num7].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.ColorValue)
					{
						if (unicode != 32)
						{
							RichTextTagAttribute[] xmlAttribute5 = TMP_Text.m_xmlAttribute;
							int num8 = num2;
							xmlAttribute5[num8].valueLength = xmlAttribute5[num8].valueLength + 1;
						}
						else
						{
							b = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							num2++;
							TMP_Text.m_xmlAttribute[num2].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[num2].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[num2].valueLength = 0;
						}
					}
					else if (tagValueType == TagValueType.StringValue)
					{
						if (unicode != 34)
						{
							TMP_Text.m_xmlAttribute[num2].valueHashCode = ((TMP_Text.m_xmlAttribute[num2].valueHashCode << 5) + TMP_Text.m_xmlAttribute[num2].valueHashCode ^ unicode);
							RichTextTagAttribute[] xmlAttribute6 = TMP_Text.m_xmlAttribute;
							int num9 = num2;
							xmlAttribute6[num9].valueLength = xmlAttribute6[num9].valueLength + 1;
						}
						else
						{
							b = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							num2++;
							TMP_Text.m_xmlAttribute[num2].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[num2].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[num2].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[num2].valueLength = 0;
						}
					}
				}
				if (unicode == 61)
				{
					b = 1;
				}
				if (b == 0 && unicode == 32)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					TMP_Text.m_xmlAttribute[num2].nameHashCode = 0;
					TMP_Text.m_xmlAttribute[num2].valueType = TagValueType.None;
					TMP_Text.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
					TMP_Text.m_xmlAttribute[num2].valueHashCode = 0;
					TMP_Text.m_xmlAttribute[num2].valueStartIndex = 0;
					TMP_Text.m_xmlAttribute[num2].valueLength = 0;
				}
				if (b == 0)
				{
					TMP_Text.m_xmlAttribute[num2].nameHashCode = (TMP_Text.m_xmlAttribute[num2].nameHashCode << 3) - TMP_Text.m_xmlAttribute[num2].nameHashCode + unicode;
				}
				if (b == 2 && unicode == 32)
				{
					b = 0;
				}
				num3++;
			}
			if (!flag2)
			{
				return false;
			}
			if (this.tag_NoParsing && TMP_Text.m_xmlAttribute[0].nameHashCode != 53822163 && TMP_Text.m_xmlAttribute[0].nameHashCode != 49429939)
			{
				return false;
			}
			if (TMP_Text.m_xmlAttribute[0].nameHashCode == 53822163 || TMP_Text.m_xmlAttribute[0].nameHashCode == 49429939)
			{
				this.tag_NoParsing = false;
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && num == 4)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && num == 5)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && num == 7)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && num == 9)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			int nameHashCode = TMP_Text.m_xmlAttribute[0].nameHashCode;
			float num10;
			if (nameHashCode <= 186622)
			{
				if (nameHashCode <= 2963)
				{
					if (nameHashCode > 98)
					{
						if (nameHashCode <= 434)
						{
							if (nameHashCode <= 402)
							{
								if (nameHashCode <= 115)
								{
									if (nameHashCode == 105)
									{
										goto IL_12BF;
									}
									if (nameHashCode != 115)
									{
										return false;
									}
									goto IL_13B5;
								}
								else
								{
									if (nameHashCode == 117)
									{
										goto IL_14CB;
									}
									if (nameHashCode != 395)
									{
										if (nameHashCode != 402)
										{
											return false;
										}
										goto IL_137A;
									}
								}
							}
							else if (nameHashCode <= 414)
							{
								if (nameHashCode == 412)
								{
									goto IL_148D;
								}
								if (nameHashCode != 414)
								{
									return false;
								}
								goto IL_15A1;
							}
							else
							{
								if (nameHashCode == 426)
								{
									return true;
								}
								if (nameHashCode != 427)
								{
									if (nameHashCode != 434)
									{
										return false;
									}
									goto IL_137A;
								}
							}
							if ((this.m_fontStyle & FontStyles.Bold) != FontStyles.Bold && this.m_fontStyleStack.Remove(FontStyles.Bold) == 0)
							{
								this.m_FontStyleInternal &= ~FontStyles.Bold;
								this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
							}
							return true;
							IL_137A:
							if ((this.m_fontStyle & FontStyles.Italic) != FontStyles.Italic)
							{
								this.m_ItalicAngle = this.m_ItalicAngleStack.Remove();
								if (this.m_fontStyleStack.Remove(FontStyles.Italic) == 0)
								{
									this.m_FontStyleInternal &= ~FontStyles.Italic;
								}
							}
							return true;
						}
						if (nameHashCode <= 670)
						{
							if (nameHashCode <= 446)
							{
								if (nameHashCode == 444)
								{
									goto IL_148D;
								}
								if (nameHashCode != 446)
								{
									return false;
								}
								goto IL_15A1;
							}
							else
							{
								if (nameHashCode == 656)
								{
									return false;
								}
								if (nameHashCode == 660)
								{
									return true;
								}
								if (nameHashCode != 670)
								{
									return false;
								}
							}
						}
						else if (nameHashCode <= 916)
						{
							if (nameHashCode == 912)
							{
								return false;
							}
							if (nameHashCode != 916)
							{
								return false;
							}
							return true;
						}
						else if (nameHashCode != 926)
						{
							if (nameHashCode == 2959)
							{
								return false;
							}
							if (nameHashCode != 2963)
							{
								return false;
							}
							return true;
						}
						return true;
						IL_148D:
						if ((this.m_fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough && this.m_fontStyleStack.Remove(FontStyles.Strikethrough) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Strikethrough;
						}
						this.m_strikethroughColor = this.m_strikethroughColorStack.Remove();
						return true;
						IL_15A1:
						if ((this.m_fontStyle & FontStyles.Underline) != FontStyles.Underline)
						{
							this.m_underlineColor = this.m_underlineColorStack.Remove();
							if (this.m_fontStyleStack.Remove(FontStyles.Underline) == 0)
							{
								this.m_FontStyleInternal &= ~FontStyles.Underline;
							}
						}
						this.m_underlineColor = this.m_underlineColorStack.Remove();
						return true;
					}
					if (nameHashCode <= -855002522)
					{
						if (nameHashCode <= -1690034531)
						{
							if (nameHashCode <= -1883544150)
							{
								if (nameHashCode == -1885698441)
								{
									goto IL_1C82;
								}
								if (nameHashCode != -1883544150)
								{
									return false;
								}
							}
							else
							{
								if (nameHashCode == -1847322671)
								{
									goto IL_3500;
								}
								if (nameHashCode == -1831660941)
								{
									goto IL_34B4;
								}
								if (nameHashCode != -1690034531)
								{
									return false;
								}
								goto IL_398D;
							}
						}
						else if (nameHashCode <= -1632103439)
						{
							if (nameHashCode != -1668324918)
							{
								if (nameHashCode != -1632103439)
								{
									return false;
								}
								goto IL_3500;
							}
						}
						else
						{
							if (nameHashCode == -1616441709)
							{
								goto IL_34B4;
							}
							if (nameHashCode == -884817987)
							{
								goto IL_398D;
							}
							if (nameHashCode != -855002522)
							{
								return false;
							}
							goto IL_38A9;
						}
						if ((this.m_fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase && this.m_fontStyleStack.Remove(FontStyles.LowerCase) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.LowerCase;
						}
						return true;
						IL_3500:
						if ((this.m_fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps && this.m_fontStyleStack.Remove(FontStyles.SmallCaps) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.SmallCaps;
						}
						return true;
						IL_398D:
						num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (num10 == -32768f)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_marginRight = num10 * (this.m_isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							this.m_marginRight = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
							break;
						case TagUnitType.Percentage:
							this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num10 / 100f;
							break;
						}
						this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
						return true;
					}
					else
					{
						if (nameHashCode > -330774850)
						{
							if (nameHashCode <= 73)
							{
								if (nameHashCode != 66)
								{
									if (nameHashCode != 73)
									{
										return false;
									}
									goto IL_12BF;
								}
							}
							else
							{
								if (nameHashCode == 83)
								{
									goto IL_13B5;
								}
								if (nameHashCode == 85)
								{
									goto IL_14CB;
								}
								if (nameHashCode != 98)
								{
									return false;
								}
							}
							this.m_FontStyleInternal |= FontStyles.Bold;
							this.m_fontStyleStack.Add(FontStyles.Bold);
							this.m_FontWeightInternal = FontWeight.Bold;
							return true;
						}
						if (nameHashCode <= -842656867)
						{
							if (nameHashCode == -842693512)
							{
								goto IL_3A71;
							}
							if (nameHashCode != -842656867)
							{
								return false;
							}
							goto IL_2EB6;
						}
						else
						{
							if (nameHashCode == -445573839)
							{
								goto IL_3B79;
							}
							if (nameHashCode == -445537194)
							{
								goto IL_2F72;
							}
							if (nameHashCode != -330774850)
							{
								return false;
							}
							goto IL_1B48;
						}
					}
					IL_12BF:
					this.m_FontStyleInternal |= FontStyles.Italic;
					this.m_fontStyleStack.Add(FontStyles.Italic);
					if (TMP_Text.m_xmlAttribute[1].nameHashCode == 276531 || TMP_Text.m_xmlAttribute[1].nameHashCode == 186899)
					{
						this.m_ItalicAngle = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
						if (this.m_ItalicAngle < -180 || this.m_ItalicAngle > 180)
						{
							return false;
						}
					}
					else
					{
						this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
					}
					this.m_ItalicAngleStack.Add(this.m_ItalicAngle);
					return true;
					IL_13B5:
					this.m_FontStyleInternal |= FontStyles.Strikethrough;
					this.m_fontStyleStack.Add(FontStyles.Strikethrough);
					if (TMP_Text.m_xmlAttribute[1].nameHashCode == 281955 || TMP_Text.m_xmlAttribute[1].nameHashCode == 192323)
					{
						this.m_strikethroughColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
						this.m_strikethroughColor.a = ((this.m_htmlColor.a < this.m_strikethroughColor.a) ? this.m_htmlColor.a : this.m_strikethroughColor.a);
					}
					else
					{
						this.m_strikethroughColor = this.m_htmlColor;
					}
					this.m_strikethroughColorStack.Add(this.m_strikethroughColor);
					return true;
					IL_14CB:
					this.m_FontStyleInternal |= FontStyles.Underline;
					this.m_fontStyleStack.Add(FontStyles.Underline);
					if (TMP_Text.m_xmlAttribute[1].nameHashCode == 281955 || TMP_Text.m_xmlAttribute[1].nameHashCode == 192323)
					{
						this.m_underlineColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
						this.m_underlineColor.a = ((this.m_htmlColor.a < this.m_underlineColor.a) ? this.m_htmlColor.a : this.m_underlineColor.a);
					}
					else
					{
						this.m_underlineColor = this.m_htmlColor;
					}
					this.m_underlineColorStack.Add(this.m_underlineColor);
					return true;
				}
				if (nameHashCode > 31169)
				{
					if (nameHashCode > 143092)
					{
						if (nameHashCode <= 155892)
						{
							if (nameHashCode <= 144016)
							{
								if (nameHashCode == 143113)
								{
									goto IL_25D4;
								}
								if (nameHashCode != 144016)
								{
									return false;
								}
							}
							else
							{
								if (nameHashCode == 145592)
								{
									goto IL_1F75;
								}
								if (nameHashCode == 154158)
								{
									goto IL_221A;
								}
								if (nameHashCode != 155892)
								{
									return false;
								}
								goto IL_17F1;
							}
						}
						else if (nameHashCode <= 156816)
						{
							if (nameHashCode == 155913)
							{
								goto IL_25D4;
							}
							if (nameHashCode != 156816)
							{
								return false;
							}
						}
						else
						{
							if (nameHashCode == 158392)
							{
								goto IL_1F75;
							}
							if (nameHashCode == 186285)
							{
								goto IL_265A;
							}
							if (nameHashCode != 186622)
							{
								return false;
							}
							goto IL_245B;
						}
						this.m_isNonBreakingSpace = false;
						return true;
						IL_1F75:
						this.m_currentFontSize = this.m_sizeStack.Remove();
						return true;
						IL_25D4:
						if (this.m_isParsingText && !this.m_isCalculatingPreferredValues && this.m_textInfo.linkCount < this.m_textInfo.linkInfo.Length)
						{
							this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextLength = this.m_characterCount - this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextfirstCharacterIndex;
							this.m_textInfo.linkCount++;
						}
						return true;
					}
					if (nameHashCode <= 43066)
					{
						if (nameHashCode <= 32745)
						{
							if (nameHashCode != 31191)
							{
								if (nameHashCode != 32745)
								{
									return false;
								}
								goto IL_1E64;
							}
						}
						else
						{
							if (nameHashCode == 41311)
							{
								goto IL_1F88;
							}
							if (nameHashCode == 43045)
							{
								goto IL_15ED;
							}
							if (nameHashCode != 43066)
							{
								return false;
							}
							goto IL_24A0;
						}
					}
					else if (nameHashCode <= 43991)
					{
						if (nameHashCode == 43969)
						{
							goto IL_1E52;
						}
						if (nameHashCode != 43991)
						{
							return false;
						}
					}
					else
					{
						if (nameHashCode == 45545)
						{
							goto IL_1E64;
						}
						if (nameHashCode == 141358)
						{
							goto IL_221A;
						}
						if (nameHashCode != 143092)
						{
							return false;
						}
						goto IL_17F1;
					}
					if (this.m_overflowMode == TextOverflowModes.Page)
					{
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						this.m_lineOffset = 0f;
						this.m_pageNumber++;
						this.m_isNewPage = true;
					}
					return true;
					IL_1E64:
					num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (num10 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						if (TMP_Text.m_htmlTag[5] == '+')
						{
							this.m_currentFontSize = this.m_fontSize + num10;
							this.m_sizeStack.Add(this.m_currentFontSize);
							return true;
						}
						if (TMP_Text.m_htmlTag[5] == '-')
						{
							this.m_currentFontSize = this.m_fontSize + num10;
							this.m_sizeStack.Add(this.m_currentFontSize);
							return true;
						}
						this.m_currentFontSize = num10;
						this.m_sizeStack.Add(this.m_currentFontSize);
						return true;
					case TagUnitType.FontUnits:
						this.m_currentFontSize = this.m_fontSize * num10;
						this.m_sizeStack.Add(this.m_currentFontSize);
						return true;
					case TagUnitType.Percentage:
						this.m_currentFontSize = this.m_fontSize * num10 / 100f;
						this.m_sizeStack.Add(this.m_currentFontSize);
						return true;
					default:
						return false;
					}
					IL_17F1:
					if ((this.m_fontStyle & FontStyles.Highlight) != FontStyles.Highlight)
					{
						this.m_HighlightStateStack.Remove();
						if (this.m_fontStyleStack.Remove(FontStyles.Highlight) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Highlight;
						}
					}
					return true;
					IL_221A:
					MaterialReference materialReference = TMP_Text.m_materialReferenceStack.Remove();
					this.m_currentFontAsset = materialReference.fontAsset;
					this.m_currentMaterial = materialReference.material;
					this.m_currentMaterialIndex = materialReference.index;
					return true;
				}
				if (nameHashCode > 6566)
				{
					if (nameHashCode <= 22673)
					{
						if (nameHashCode <= 20849)
						{
							if (nameHashCode == 20677)
							{
								goto IL_1D62;
							}
							if (nameHashCode != 20849)
							{
								return false;
							}
						}
						else
						{
							if (nameHashCode == 20863)
							{
								goto IL_1AAB;
							}
							if (nameHashCode == 22501)
							{
								goto IL_1D62;
							}
							if (nameHashCode != 22673)
							{
								return false;
							}
						}
						if ((this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript)
						{
							if (this.m_fontScaleMultiplier < 1f)
							{
								this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
								this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
							}
							if (this.m_fontStyleStack.Remove(FontStyles.Subscript) == 0)
							{
								this.m_FontStyleInternal &= ~FontStyles.Subscript;
							}
						}
						return true;
						IL_1D62:
						this.m_isIgnoringAlignment = false;
						return true;
					}
					if (nameHashCode <= 28511)
					{
						if (nameHashCode != 22687)
						{
							if (nameHashCode != 28511)
							{
								return false;
							}
							goto IL_1F88;
						}
					}
					else
					{
						if (nameHashCode == 30245)
						{
							goto IL_15ED;
						}
						if (nameHashCode == 30266)
						{
							goto IL_24A0;
						}
						if (nameHashCode != 31169)
						{
							return false;
						}
						goto IL_1E52;
					}
					IL_1AAB:
					if ((this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
					{
						if (this.m_fontScaleMultiplier < 1f)
						{
							this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
							this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
						}
						if (this.m_fontStyleStack.Remove(FontStyles.Superscript) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Superscript;
						}
					}
					return true;
				}
				if (nameHashCode <= 4556)
				{
					if (nameHashCode <= 3215)
					{
						if (nameHashCode != 2973)
						{
							if (nameHashCode != 3215)
							{
								return false;
							}
							return false;
						}
					}
					else
					{
						if (nameHashCode == 3219)
						{
							return true;
						}
						if (nameHashCode != 3229)
						{
							if (nameHashCode != 4556)
							{
								return false;
							}
							goto IL_1CB7;
						}
					}
					return true;
				}
				if (nameHashCode <= 4742)
				{
					if (nameHashCode != 4728)
					{
						if (nameHashCode != 4742)
						{
							return false;
						}
						goto IL_19BF;
					}
				}
				else
				{
					if (nameHashCode == 6380)
					{
						goto IL_1CB7;
					}
					if (nameHashCode != 6552)
					{
						if (nameHashCode != 6566)
						{
							return false;
						}
						goto IL_19BF;
					}
				}
				this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
				this.m_baselineOffsetStack.Push(this.m_baselineOffset);
				float num11 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
				this.m_baselineOffset += this.m_currentFontAsset.faceInfo.subscriptOffset * num11 * this.m_fontScaleMultiplier;
				this.m_fontStyleStack.Add(FontStyles.Subscript);
				this.m_FontStyleInternal |= FontStyles.Subscript;
				return true;
				IL_19BF:
				this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
				this.m_baselineOffsetStack.Push(this.m_baselineOffset);
				num11 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
				this.m_baselineOffset += this.m_currentFontAsset.faceInfo.superscriptOffset * num11 * this.m_fontScaleMultiplier;
				this.m_fontStyleStack.Add(FontStyles.Superscript);
				this.m_FontStyleInternal |= FontStyles.Superscript;
				return true;
				IL_1CB7:
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_xAdvance = num10 * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.FontUnits:
					this.m_xAdvance = num10 * this.m_currentFontSize * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.Percentage:
					this.m_xAdvance = this.m_marginWidth * num10 / 100f;
					return true;
				default:
					return false;
				}
				IL_15ED:
				this.m_FontStyleInternal |= FontStyles.Highlight;
				this.m_fontStyleStack.Add(FontStyles.Highlight);
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
				TMP_Offset tmp_Offset = TMP_Offset.zero;
				int num12 = 0;
				while (num12 < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[num12].nameHashCode != 0)
				{
					int nameHashCode2 = TMP_Text.m_xmlAttribute[num12].nameHashCode;
					if (nameHashCode2 <= 43045)
					{
						if (nameHashCode2 == 30245 || nameHashCode2 == 43045)
						{
							if (TMP_Text.m_xmlAttribute[num12].valueType == TagValueType.ColorValue)
							{
								color = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							}
						}
					}
					else if (nameHashCode2 != 281955)
					{
						if (nameHashCode2 == 15087385)
						{
							if (this.GetAttributeParameters(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num12].valueStartIndex, TMP_Text.m_xmlAttribute[num12].valueLength, ref TMP_Text.m_attributeParameterValues) != 4)
							{
								return false;
							}
							tmp_Offset = new TMP_Offset(TMP_Text.m_attributeParameterValues[0], TMP_Text.m_attributeParameterValues[1], TMP_Text.m_attributeParameterValues[2], TMP_Text.m_attributeParameterValues[3]);
							tmp_Offset *= this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
						}
					}
					else
					{
						color = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num12].valueStartIndex, TMP_Text.m_xmlAttribute[num12].valueLength);
					}
					num12++;
				}
				color.a = ((this.m_htmlColor.a < color.a) ? this.m_htmlColor.a : color.a);
				HighlightState item = new HighlightState(color, tmp_Offset);
				this.m_HighlightStateStack.Push(item);
				return true;
				IL_1E52:
				this.m_isNonBreakingSpace = true;
				return true;
				IL_1F88:
				int valueHashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
				int nameHashCode3 = TMP_Text.m_xmlAttribute[1].nameHashCode;
				int valueHashCode2 = TMP_Text.m_xmlAttribute[1].valueHashCode;
				if (valueHashCode == 764638571 || valueHashCode == 523367755)
				{
					this.m_currentFontAsset = TMP_Text.m_materialReferences[0].fontAsset;
					this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
					this.m_currentMaterialIndex = 0;
					TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[0]);
					return true;
				}
				TMP_FontAsset tmp_FontAsset;
				MaterialReferenceManager.TryGetFontAsset(valueHashCode, out tmp_FontAsset);
				if (tmp_FontAsset == null)
				{
					Func<int, string, TMP_FontAsset> onFontAssetRequest = TMP_Text.OnFontAssetRequest;
					tmp_FontAsset = ((onFontAssetRequest != null) ? onFontAssetRequest(valueHashCode, new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength)) : null);
					if (tmp_FontAsset == null)
					{
						tmp_FontAsset = Resources.Load<TMP_FontAsset>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
					}
					if (tmp_FontAsset == null)
					{
						return false;
					}
					MaterialReferenceManager.AddFontAsset(tmp_FontAsset);
				}
				if (nameHashCode3 == 0 && valueHashCode2 == 0)
				{
					this.m_currentMaterial = tmp_FontAsset.material;
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
				}
				else
				{
					if (nameHashCode3 != 103415287 && nameHashCode3 != 72669687)
					{
						return false;
					}
					Material material;
					if (MaterialReferenceManager.TryGetMaterial(valueHashCode2, out material))
					{
						this.m_currentMaterial = material;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
					}
					else
					{
						material = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength));
						if (material == null)
						{
							return false;
						}
						MaterialReferenceManager.AddFontMaterial(valueHashCode2, material);
						this.m_currentMaterial = material;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
					}
				}
				this.m_currentFontAsset = tmp_FontAsset;
				return true;
				IL_24A0:
				if (this.m_isParsingText && !this.m_isCalculatingPreferredValues)
				{
					int linkCount = this.m_textInfo.linkCount;
					if (linkCount + 1 > this.m_textInfo.linkInfo.Length)
					{
						TMP_TextInfo.Resize<TMP_LinkInfo>(ref this.m_textInfo.linkInfo, linkCount + 1);
					}
					this.m_textInfo.linkInfo[linkCount].textComponent = this;
					this.m_textInfo.linkInfo[linkCount].hashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
					this.m_textInfo.linkInfo[linkCount].linkTextfirstCharacterIndex = this.m_characterCount;
					this.m_textInfo.linkInfo[linkCount].linkIdFirstCharacterIndex = startIndex + TMP_Text.m_xmlAttribute[0].valueStartIndex;
					this.m_textInfo.linkInfo[linkCount].linkIdLength = TMP_Text.m_xmlAttribute[0].valueLength;
					this.m_textInfo.linkInfo[linkCount].SetLinkID(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				}
				return true;
			}
			int num13;
			if (nameHashCode <= 6886018)
			{
				if (nameHashCode <= 1071884)
				{
					if (nameHashCode > 315682)
					{
						if (nameHashCode <= 982252)
						{
							if (nameHashCode <= 320078)
							{
								if (nameHashCode == 317446)
								{
									return false;
								}
								if (nameHashCode != 320078)
								{
									return false;
								}
								goto IL_23B7;
							}
							else
							{
								if (nameHashCode == 327550)
								{
									goto IL_2744;
								}
								if (nameHashCode != 976214)
								{
									if (nameHashCode != 982252)
									{
										return false;
									}
									goto IL_2DCA;
								}
							}
						}
						else if (nameHashCode <= 1017743)
						{
							if (nameHashCode == 1015979)
							{
								goto IL_3C94;
							}
							if (nameHashCode != 1017743)
							{
								return false;
							}
							return true;
						}
						else
						{
							if (nameHashCode == 1027847)
							{
								goto IL_27C9;
							}
							if (nameHashCode != 1065846)
							{
								if (nameHashCode != 1071884)
								{
									return false;
								}
								goto IL_2DCA;
							}
						}
						this.m_lineJustification = this.m_lineJustificationStack.Remove();
						return true;
						IL_2DCA:
						this.m_htmlColor = this.m_colorStack.Remove();
						return true;
					}
					if (nameHashCode <= 237918)
					{
						if (nameHashCode <= 226050)
						{
							if (nameHashCode != 192323)
							{
								if (nameHashCode != 226050)
								{
									return false;
								}
								goto IL_3C2D;
							}
						}
						else
						{
							if (nameHashCode == 227814)
							{
								return false;
							}
							if (nameHashCode == 230446)
							{
								goto IL_23B7;
							}
							if (nameHashCode != 237918)
							{
								return false;
							}
							goto IL_2744;
						}
					}
					else if (nameHashCode <= 276254)
					{
						if (nameHashCode == 275917)
						{
							goto IL_265A;
						}
						if (nameHashCode != 276254)
						{
							return false;
						}
						goto IL_245B;
					}
					else
					{
						if (nameHashCode == 280416)
						{
							return false;
						}
						if (nameHashCode != 281955)
						{
							if (nameHashCode != 315682)
							{
								return false;
							}
							goto IL_3C2D;
						}
					}
					if (TMP_Text.m_htmlTag[6] == '#' && num == 10)
					{
						this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (TMP_Text.m_htmlTag[6] == '#' && num == 11)
					{
						this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (TMP_Text.m_htmlTag[6] == '#' && num == 13)
					{
						this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (TMP_Text.m_htmlTag[6] == '#' && num == 15)
					{
						this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					num13 = TMP_Text.m_xmlAttribute[0].valueHashCode;
					if (num13 <= 3680713)
					{
						if (num13 <= -36881330)
						{
							if (num13 == -992792864)
							{
								this.m_htmlColor = new Color32(173, 216, 230, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num13 == -36881330)
							{
								this.m_htmlColor = new Color32(160, 32, 240, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
						else
						{
							if (num13 == 125395)
							{
								this.m_htmlColor = Color.red;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num13 == 3573310)
							{
								this.m_htmlColor = Color.blue;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num13 == 3680713)
							{
								this.m_htmlColor = new Color32(128, 128, 128, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
					}
					else if (num13 <= 117905991)
					{
						if (num13 == 26556144)
						{
							this.m_htmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (num13 == 117905991)
						{
							this.m_htmlColor = Color.black;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
					}
					else
					{
						if (num13 == 121463835)
						{
							this.m_htmlColor = Color.green;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (num13 == 140357351)
						{
							this.m_htmlColor = Color.white;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (num13 == 554054276)
						{
							this.m_htmlColor = Color.yellow;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
					}
					return false;
					IL_3C2D:
					num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (num10 == -32768f)
					{
						return false;
					}
					this.m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(num10, 1f, 1f));
					this.m_isFXMatrixSet = true;
					return true;
					IL_23B7:
					num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (num10 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_xAdvance += num10 * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
					case TagUnitType.FontUnits:
						this.m_xAdvance += num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
						return true;
					case TagUnitType.Percentage:
						return false;
					default:
						return false;
					}
					IL_2744:
					num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (num10 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_width = num10 * (this.m_isOrthographic ? 1f : 0.1f);
						break;
					case TagUnitType.FontUnits:
						return false;
					case TagUnitType.Percentage:
						this.m_width = this.m_marginWidth * num10 / 100f;
						break;
					}
					return true;
				}
				if (nameHashCode <= 1619421)
				{
					if (nameHashCode <= 1356515)
					{
						if (nameHashCode <= 1107375)
						{
							if (nameHashCode == 1105611)
							{
								goto IL_3C94;
							}
							if (nameHashCode != 1107375)
							{
								return false;
							}
							return true;
						}
						else
						{
							if (nameHashCode == 1117479)
							{
								goto IL_27C9;
							}
							if (nameHashCode == 1286342)
							{
								goto IL_3B8F;
							}
							if (nameHashCode != 1356515)
							{
								return false;
							}
						}
					}
					else if (nameHashCode <= 1482398)
					{
						if (nameHashCode == 1441524)
						{
							goto IL_2DDD;
						}
						if (nameHashCode != 1482398)
						{
							return false;
						}
						goto IL_352D;
					}
					else
					{
						if (nameHashCode == 1524585)
						{
							goto IL_2D25;
						}
						if (nameHashCode == 1600507)
						{
							goto IL_3C9D;
						}
						if (nameHashCode != 1619421)
						{
							return false;
						}
						goto IL_2F7F;
					}
				}
				else if (nameHashCode <= 2109854)
				{
					if (nameHashCode <= 1913798)
					{
						if (nameHashCode == 1750458)
						{
							return false;
						}
						if (nameHashCode != 1913798)
						{
							return false;
						}
						goto IL_3B8F;
					}
					else if (nameHashCode != 1983971)
					{
						if (nameHashCode == 2068980)
						{
							goto IL_2DDD;
						}
						if (nameHashCode != 2109854)
						{
							return false;
						}
						goto IL_352D;
					}
				}
				else if (nameHashCode <= 2227963)
				{
					if (nameHashCode == 2152041)
					{
						goto IL_2D25;
					}
					if (nameHashCode != 2227963)
					{
						return false;
					}
					goto IL_3C9D;
				}
				else
				{
					if (nameHashCode == 2246877)
					{
						goto IL_2F7F;
					}
					if (nameHashCode == 6815845)
					{
						goto IL_3BDE;
					}
					if (nameHashCode != 6886018)
					{
						return false;
					}
					goto IL_2CCF;
				}
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_cSpacing = num10 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_cSpacing = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					return false;
				}
				return true;
				IL_2D25:
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_monoSpacing = num10 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_monoSpacing = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					return false;
				}
				return true;
				IL_2DDD:
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.tag_Indent = num10 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.tag_Indent = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					this.tag_Indent = this.m_marginWidth * num10 / 100f;
					break;
				}
				this.m_indentStack.Add(this.tag_Indent);
				this.m_xAdvance = this.tag_Indent;
				return true;
				IL_2F7F:
				int valueHashCode3 = TMP_Text.m_xmlAttribute[0].valueHashCode;
				this.m_spriteIndex = -1;
				TMP_SpriteAsset tmp_SpriteAsset;
				if (TMP_Text.m_xmlAttribute[0].valueType == TagValueType.None || TMP_Text.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
				{
					if (this.m_spriteAsset != null)
					{
						this.m_currentSpriteAsset = this.m_spriteAsset;
					}
					else if (this.m_defaultSpriteAsset != null)
					{
						this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
					}
					else if (this.m_defaultSpriteAsset == null)
					{
						if (TMP_Settings.defaultSpriteAsset != null)
						{
							this.m_defaultSpriteAsset = TMP_Settings.defaultSpriteAsset;
						}
						else
						{
							this.m_defaultSpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Default Sprite Asset");
						}
						this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
					}
					if (this.m_currentSpriteAsset == null)
					{
						return false;
					}
				}
				else if (MaterialReferenceManager.TryGetSpriteAsset(valueHashCode3, out tmp_SpriteAsset))
				{
					this.m_currentSpriteAsset = tmp_SpriteAsset;
				}
				else
				{
					if (tmp_SpriteAsset == null)
					{
						Func<int, string, TMP_SpriteAsset> onSpriteAssetRequest = TMP_Text.OnSpriteAssetRequest;
						tmp_SpriteAsset = ((onSpriteAssetRequest != null) ? onSpriteAssetRequest(valueHashCode3, new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength)) : null);
						if (tmp_SpriteAsset == null)
						{
							tmp_SpriteAsset = Resources.Load<TMP_SpriteAsset>(TMP_Settings.defaultSpriteAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
						}
					}
					if (tmp_SpriteAsset == null)
					{
						return false;
					}
					MaterialReferenceManager.AddSpriteAsset(valueHashCode3, tmp_SpriteAsset);
					this.m_currentSpriteAsset = tmp_SpriteAsset;
				}
				if (TMP_Text.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
				{
					int num14 = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (num14 == -32768)
					{
						return false;
					}
					if (num14 > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
					{
						return false;
					}
					this.m_spriteIndex = num14;
				}
				this.m_spriteColor = TMP_Text.s_colorWhite;
				this.m_tintSprite = false;
				int num15 = 0;
				while (num15 < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[num15].nameHashCode != 0)
				{
					int nameHashCode4 = TMP_Text.m_xmlAttribute[num15].nameHashCode;
					int num16 = 0;
					if (nameHashCode4 <= 43347)
					{
						if (nameHashCode4 <= 30547)
						{
							if (nameHashCode4 == 26705)
							{
								goto IL_335D;
							}
							if (nameHashCode4 != 30547)
							{
								goto IL_33D9;
							}
						}
						else
						{
							if (nameHashCode4 == 33019)
							{
								goto IL_32E3;
							}
							if (nameHashCode4 == 39505)
							{
								goto IL_335D;
							}
							if (nameHashCode4 != 43347)
							{
								goto IL_33D9;
							}
						}
						this.m_currentSpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCode(this.m_currentSpriteAsset, TMP_Text.m_xmlAttribute[num15].valueHashCode, true, out num16);
						if (num16 == -1)
						{
							return false;
						}
						this.m_spriteIndex = num16;
						goto IL_33ED;
						IL_335D:
						if (this.GetAttributeParameters(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num15].valueStartIndex, TMP_Text.m_xmlAttribute[num15].valueLength, ref TMP_Text.m_attributeParameterValues) != 3)
						{
							return false;
						}
						this.m_spriteIndex = (int)TMP_Text.m_attributeParameterValues[0];
						if (this.m_isParsingText)
						{
							this.spriteAnimator.DoSpriteAnimation(this.m_characterCount, this.m_currentSpriteAsset, this.m_spriteIndex, (int)TMP_Text.m_attributeParameterValues[1], (int)TMP_Text.m_attributeParameterValues[2]);
						}
					}
					else
					{
						if (nameHashCode4 <= 192323)
						{
							if (nameHashCode4 == 45819)
							{
								goto IL_32E3;
							}
							if (nameHashCode4 != 192323)
							{
								goto IL_33D9;
							}
						}
						else
						{
							if (nameHashCode4 != 205930)
							{
								if (nameHashCode4 == 281955)
								{
									goto IL_3325;
								}
								if (nameHashCode4 != 295562)
								{
									goto IL_33D9;
								}
							}
							num16 = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
							if (num16 == -32768)
							{
								return false;
							}
							if (num16 > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
							{
								return false;
							}
							this.m_spriteIndex = num16;
							goto IL_33ED;
						}
						IL_3325:
						this.m_spriteColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num15].valueStartIndex, TMP_Text.m_xmlAttribute[num15].valueLength);
					}
					IL_33ED:
					num15++;
					continue;
					IL_32E3:
					this.m_tintSprite = (this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num15].valueStartIndex, TMP_Text.m_xmlAttribute[num15].valueLength) != 0f);
					goto IL_33ED;
					IL_33D9:
					if (nameHashCode4 != 2246877 && nameHashCode4 != 1619421)
					{
						return false;
					}
					goto IL_33ED;
				}
				if (this.m_spriteIndex == -1)
				{
					return false;
				}
				this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentSpriteAsset.material, this.m_currentSpriteAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
				this.m_textElementType = TMP_TextElementType.Sprite;
				return true;
				IL_352D:
				TagValueType valueType = TMP_Text.m_xmlAttribute[0].valueType;
				if (valueType == TagValueType.None)
				{
					int num17 = 1;
					while (num17 < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[num17].nameHashCode != 0)
					{
						int nameHashCode5 = TMP_Text.m_xmlAttribute[num17].nameHashCode;
						if (nameHashCode5 != 42823)
						{
							if (nameHashCode5 == 315620)
							{
								num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num17].valueStartIndex, TMP_Text.m_xmlAttribute[num17].valueLength);
								if (num10 == -32768f)
								{
									return false;
								}
								switch (TMP_Text.m_xmlAttribute[num17].unitType)
								{
								case TagUnitType.Pixels:
									this.m_marginRight = num10 * (this.m_isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_marginRight = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
									break;
								case TagUnitType.Percentage:
									this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num10 / 100f;
									break;
								}
								this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
							}
						}
						else
						{
							num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num17].valueStartIndex, TMP_Text.m_xmlAttribute[num17].valueLength);
							if (num10 == -32768f)
							{
								return false;
							}
							switch (TMP_Text.m_xmlAttribute[num17].unitType)
							{
							case TagUnitType.Pixels:
								this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num10 / 100f;
								break;
							}
							this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
						}
						num17++;
					}
					return true;
				}
				if (valueType != TagValueType.NumericalValue)
				{
					return false;
				}
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num10 / 100f;
					break;
				}
				this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
				this.m_marginRight = this.m_marginLeft;
				return true;
				IL_3B8F:
				int valueHashCode4 = TMP_Text.m_xmlAttribute[0].valueHashCode;
				if (this.m_isParsingText)
				{
					this.m_actionStack.Add(valueHashCode4);
					UnityEngine.Debug.Log("Action ID: [" + valueHashCode4.ToString() + "] First character index: " + this.m_characterCount.ToString());
				}
				return true;
				IL_3C9D:
				num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (num10 == -32768f)
				{
					return false;
				}
				this.m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, num10), Vector3.one);
				this.m_isFXMatrixSet = true;
				return true;
				IL_27C9:
				this.m_width = -1f;
				return true;
				IL_3C94:
				this.m_isFXMatrixSet = false;
				return true;
			}
			if (nameHashCode > 54741026)
			{
				if (nameHashCode <= 514803617)
				{
					if (nameHashCode <= 340349191)
					{
						if (nameHashCode <= 72669687)
						{
							if (nameHashCode == 69403544)
							{
								goto IL_2AF5;
							}
							if (nameHashCode != 72669687)
							{
								return false;
							}
						}
						else
						{
							if (nameHashCode == 100149144)
							{
								goto IL_2AF5;
							}
							if (nameHashCode != 103415287)
							{
								if (nameHashCode != 340349191)
								{
									return false;
								}
								goto IL_2C26;
							}
						}
						int valueHashCode2 = TMP_Text.m_xmlAttribute[0].valueHashCode;
						if (valueHashCode2 == 764638571 || valueHashCode2 == 523367755)
						{
							this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
							this.m_currentMaterialIndex = 0;
							TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[0]);
							return true;
						}
						Material material;
						if (MaterialReferenceManager.TryGetMaterial(valueHashCode2, out material))
						{
							this.m_currentMaterial = material;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
							TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
						}
						else
						{
							material = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
							if (material == null)
							{
								return false;
							}
							MaterialReferenceManager.AddFontMaterial(valueHashCode2, material);
							this.m_currentMaterial = material;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
							TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
						}
						return true;
						IL_2AF5:
						int valueHashCode5 = TMP_Text.m_xmlAttribute[0].valueHashCode;
						TMP_ColorGradient tmp_ColorGradient;
						if (MaterialReferenceManager.TryGetColorGradientPreset(valueHashCode5, out tmp_ColorGradient))
						{
							this.m_colorGradientPreset = tmp_ColorGradient;
						}
						else
						{
							if (tmp_ColorGradient == null)
							{
								tmp_ColorGradient = Resources.Load<TMP_ColorGradient>(TMP_Settings.defaultColorGradientPresetsPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
							}
							if (tmp_ColorGradient == null)
							{
								return false;
							}
							MaterialReferenceManager.AddColorGradientPreset(valueHashCode5, tmp_ColorGradient);
							this.m_colorGradientPreset = tmp_ColorGradient;
						}
						this.m_colorGradientPresetIsTinted = false;
						int num18 = 1;
						while (num18 < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[num18].nameHashCode != 0)
						{
							int nameHashCode6 = TMP_Text.m_xmlAttribute[num18].nameHashCode;
							if (nameHashCode6 == 33019 || nameHashCode6 == 45819)
							{
								this.m_colorGradientPresetIsTinted = (this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[num18].valueStartIndex, TMP_Text.m_xmlAttribute[num18].valueLength) != 0f);
							}
							num18++;
						}
						this.m_colorGradientStack.Add(this.m_colorGradientPreset);
						return true;
					}
					if (nameHashCode <= 371094791)
					{
						if (nameHashCode != 343615334)
						{
							if (nameHashCode != 371094791)
							{
								return false;
							}
							goto IL_2C26;
						}
					}
					else if (nameHashCode != 374360934)
					{
						if (nameHashCode == 457225591)
						{
							goto IL_1C82;
						}
						if (nameHashCode != 514803617)
						{
							return false;
						}
						goto IL_344E;
					}
					MaterialReference materialReference2 = TMP_Text.m_materialReferenceStack.Remove();
					this.m_currentMaterial = materialReference2.material;
					this.m_currentMaterialIndex = materialReference2.index;
					return true;
					IL_2C26:
					this.m_colorGradientPreset = this.m_colorGradientStack.Remove();
					return true;
				}
				if (nameHashCode <= 781906058)
				{
					if (nameHashCode <= 566686826)
					{
						if (nameHashCode != 551025096)
						{
							if (nameHashCode != 566686826)
							{
								return false;
							}
							goto IL_3495;
						}
					}
					else
					{
						if (nameHashCode == 730022849)
						{
							goto IL_344E;
						}
						if (nameHashCode != 766244328)
						{
							if (nameHashCode != 781906058)
							{
								return false;
							}
							goto IL_3495;
						}
					}
					this.m_FontStyleInternal |= FontStyles.SmallCaps;
					this.m_fontStyleStack.Add(FontStyles.SmallCaps);
					return true;
				}
				if (nameHashCode <= 1109386397)
				{
					if (nameHashCode == 1100728678)
					{
						goto IL_38A9;
					}
					if (nameHashCode == 1109349752)
					{
						goto IL_3A71;
					}
					if (nameHashCode != 1109386397)
					{
						return false;
					}
					goto IL_2EB6;
				}
				else
				{
					if (nameHashCode == 1897350193)
					{
						goto IL_3B79;
					}
					if (nameHashCode == 1897386838)
					{
						goto IL_2F72;
					}
					if (nameHashCode != 2012149182)
					{
						return false;
					}
					goto IL_1B48;
				}
				IL_344E:
				this.m_FontStyleInternal |= FontStyles.LowerCase;
				this.m_fontStyleStack.Add(FontStyles.LowerCase);
				return true;
			}
			if (nameHashCode <= 7757466)
			{
				if (nameHashCode <= 7443301)
				{
					if (nameHashCode <= 7011901)
					{
						if (nameHashCode == 6971027)
						{
							goto IL_2EA3;
						}
						if (nameHashCode != 7011901)
						{
							return false;
						}
						goto IL_3891;
					}
					else if (nameHashCode != 7054088)
					{
						if (nameHashCode == 7130010)
						{
							goto IL_3D04;
						}
						if (nameHashCode != 7443301)
						{
							return false;
						}
						goto IL_3BDE;
					}
				}
				else if (nameHashCode <= 7598483)
				{
					if (nameHashCode == 7513474)
					{
						goto IL_2CCF;
					}
					if (nameHashCode != 7598483)
					{
						return false;
					}
					goto IL_2EA3;
				}
				else
				{
					if (nameHashCode == 7639357)
					{
						goto IL_3891;
					}
					if (nameHashCode != 7681544)
					{
						if (nameHashCode != 7757466)
						{
							return false;
						}
						goto IL_3D04;
					}
				}
				this.m_monoSpacing = 0f;
				return true;
				IL_2EA3:
				this.tag_Indent = this.m_indentStack.Remove();
				return true;
				IL_3891:
				this.m_marginLeft = 0f;
				this.m_marginRight = 0f;
				return true;
				IL_3D04:
				this.m_isFXMatrixSet = false;
				return true;
			}
			if (nameHashCode <= 15115642)
			{
				if (nameHashCode <= 10723418)
				{
					if (nameHashCode == 9133802)
					{
						goto IL_3495;
					}
					if (nameHashCode != 10723418)
					{
						return false;
					}
				}
				else
				{
					if (nameHashCode == 11642281)
					{
						goto IL_1D6B;
					}
					if (nameHashCode == 13526026)
					{
						goto IL_3495;
					}
					if (nameHashCode != 15115642)
					{
						return false;
					}
				}
				this.tag_NoParsing = true;
				return true;
			}
			if (nameHashCode > 47840323)
			{
				if (nameHashCode != 50348802)
				{
					if (nameHashCode == 52232547)
					{
						goto IL_34B4;
					}
					if (nameHashCode != 54741026)
					{
						return false;
					}
				}
				this.m_baselineOffset = 0f;
				return true;
			}
			if (nameHashCode != 16034505)
			{
				if (nameHashCode != 47840323)
				{
					return false;
				}
				goto IL_34B4;
			}
			IL_1D6B:
			num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
			if (num10 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_baselineOffset = num10 * (this.m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				this.m_baselineOffset = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
			IL_3495:
			this.m_FontStyleInternal |= FontStyles.UpperCase;
			this.m_fontStyleStack.Add(FontStyles.UpperCase);
			return true;
			IL_2CCF:
			if (!this.m_isParsingText)
			{
				return true;
			}
			if (this.m_characterCount > 0)
			{
				this.m_xAdvance -= this.m_cSpacing;
				this.m_textInfo.characterInfo[this.m_characterCount - 1].xAdvance = this.m_xAdvance;
			}
			this.m_cSpacing = 0f;
			return true;
			IL_3BDE:
			if (this.m_isParsingText)
			{
				UnityEngine.Debug.Log("Action ID: [" + this.m_actionStack.CurrentItem().ToString() + "] Last character index: " + (this.m_characterCount - 1).ToString());
			}
			this.m_actionStack.Remove();
			return true;
			IL_1B48:
			num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
			if (num10 == -32768f)
			{
				return false;
			}
			num13 = (int)num10;
			if (num13 <= 400)
			{
				if (num13 <= 200)
				{
					if (num13 != 100)
					{
						if (num13 == 200)
						{
							this.m_FontWeightInternal = FontWeight.ExtraLight;
						}
					}
					else
					{
						this.m_FontWeightInternal = FontWeight.Thin;
					}
				}
				else if (num13 != 300)
				{
					if (num13 == 400)
					{
						this.m_FontWeightInternal = FontWeight.Regular;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Light;
				}
			}
			else if (num13 <= 600)
			{
				if (num13 != 500)
				{
					if (num13 == 600)
					{
						this.m_FontWeightInternal = FontWeight.SemiBold;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Medium;
				}
			}
			else if (num13 != 700)
			{
				if (num13 != 800)
				{
					if (num13 == 900)
					{
						this.m_FontWeightInternal = FontWeight.Black;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Heavy;
				}
			}
			else
			{
				this.m_FontWeightInternal = FontWeight.Bold;
			}
			this.m_FontWeightStack.Add(this.m_FontWeightInternal);
			return true;
			IL_1C82:
			this.m_FontWeightStack.Remove();
			if (this.m_FontStyleInternal == FontStyles.Bold)
			{
				this.m_FontWeightInternal = FontWeight.Bold;
			}
			else
			{
				this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
			}
			return true;
			IL_245B:
			if (TMP_Text.m_xmlAttribute[0].valueLength != 3)
			{
				return false;
			}
			this.m_htmlColor.a = (byte)(this.HexToInt(TMP_Text.m_htmlTag[7]) * 16 + this.HexToInt(TMP_Text.m_htmlTag[8]));
			return true;
			IL_265A:
			num13 = TMP_Text.m_xmlAttribute[0].valueHashCode;
			if (num13 <= -458210101)
			{
				if (num13 == -523808257)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Justified;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num13 == -458210101)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Center;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
			}
			else
			{
				if (num13 == 3774683)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Left;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num13 == 122383428)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Flush;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num13 == 136703040)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Right;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
			}
			return false;
			IL_2EB6:
			num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
			if (num10 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.tag_LineIndent = num10 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.tag_LineIndent = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				this.tag_LineIndent = this.m_marginWidth * num10 / 100f;
				break;
			}
			this.m_xAdvance += this.tag_LineIndent;
			return true;
			IL_2F72:
			this.tag_LineIndent = 0f;
			return true;
			IL_34B4:
			if ((this.m_fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase && this.m_fontStyleStack.Remove(FontStyles.UpperCase) == 0)
			{
				this.m_FontStyleInternal &= ~FontStyles.UpperCase;
			}
			return true;
			IL_38A9:
			num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
			if (num10 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.m_marginLeft = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num10 / 100f;
				break;
			}
			this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
			return true;
			IL_3A71:
			num10 = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
			if (num10 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_lineHeight = num10 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.m_lineHeight = num10 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
			{
				float num11 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
				this.m_lineHeight = this.m_fontAsset.faceInfo.lineHeight * num10 / 100f * num11;
				break;
			}
			}
			return true;
			IL_3B79:
			this.m_lineHeight = -32767f;
			return true;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00034EDC File Offset: 0x000330DC
		protected TMP_Text()
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00035254 File Offset: 0x00033454
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_Text()
		{
		}

		// Token: 0x04000434 RID: 1076
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_text;

		// Token: 0x04000435 RID: 1077
		private bool m_IsTextBackingStringDirty;

		// Token: 0x04000436 RID: 1078
		[SerializeField]
		protected ITextPreprocessor m_TextPreprocessor;

		// Token: 0x04000437 RID: 1079
		[SerializeField]
		protected bool m_isRightToLeft;

		// Token: 0x04000438 RID: 1080
		[SerializeField]
		protected TMP_FontAsset m_fontAsset;

		// Token: 0x04000439 RID: 1081
		protected TMP_FontAsset m_currentFontAsset;

		// Token: 0x0400043A RID: 1082
		protected bool m_isSDFShader;

		// Token: 0x0400043B RID: 1083
		[SerializeField]
		protected Material m_sharedMaterial;

		// Token: 0x0400043C RID: 1084
		protected Material m_currentMaterial;

		// Token: 0x0400043D RID: 1085
		protected static MaterialReference[] m_materialReferences = new MaterialReference[4];

		// Token: 0x0400043E RID: 1086
		protected static Dictionary<int, int> m_materialReferenceIndexLookup = new Dictionary<int, int>();

		// Token: 0x0400043F RID: 1087
		protected static TMP_TextProcessingStack<MaterialReference> m_materialReferenceStack = new TMP_TextProcessingStack<MaterialReference>(new MaterialReference[16]);

		// Token: 0x04000440 RID: 1088
		protected int m_currentMaterialIndex;

		// Token: 0x04000441 RID: 1089
		[SerializeField]
		protected Material[] m_fontSharedMaterials;

		// Token: 0x04000442 RID: 1090
		[SerializeField]
		protected Material m_fontMaterial;

		// Token: 0x04000443 RID: 1091
		[SerializeField]
		protected Material[] m_fontMaterials;

		// Token: 0x04000444 RID: 1092
		protected bool m_isMaterialDirty;

		// Token: 0x04000445 RID: 1093
		[SerializeField]
		protected Color32 m_fontColor32 = Color.white;

		// Token: 0x04000446 RID: 1094
		[SerializeField]
		protected Color m_fontColor = Color.white;

		// Token: 0x04000447 RID: 1095
		protected static Color32 s_colorWhite = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000448 RID: 1096
		protected Color32 m_underlineColor = TMP_Text.s_colorWhite;

		// Token: 0x04000449 RID: 1097
		protected Color32 m_strikethroughColor = TMP_Text.s_colorWhite;

		// Token: 0x0400044A RID: 1098
		[SerializeField]
		protected bool m_enableVertexGradient;

		// Token: 0x0400044B RID: 1099
		[SerializeField]
		protected ColorMode m_colorMode = ColorMode.FourCornersGradient;

		// Token: 0x0400044C RID: 1100
		[SerializeField]
		protected VertexGradient m_fontColorGradient = new VertexGradient(Color.white);

		// Token: 0x0400044D RID: 1101
		[SerializeField]
		protected TMP_ColorGradient m_fontColorGradientPreset;

		// Token: 0x0400044E RID: 1102
		[SerializeField]
		protected TMP_SpriteAsset m_spriteAsset;

		// Token: 0x0400044F RID: 1103
		[SerializeField]
		protected bool m_tintAllSprites;

		// Token: 0x04000450 RID: 1104
		protected bool m_tintSprite;

		// Token: 0x04000451 RID: 1105
		protected Color32 m_spriteColor;

		// Token: 0x04000452 RID: 1106
		[SerializeField]
		protected TMP_StyleSheet m_StyleSheet;

		// Token: 0x04000453 RID: 1107
		internal TMP_Style m_TextStyle;

		// Token: 0x04000454 RID: 1108
		[SerializeField]
		protected int m_TextStyleHashCode;

		// Token: 0x04000455 RID: 1109
		[SerializeField]
		protected bool m_overrideHtmlColors;

		// Token: 0x04000456 RID: 1110
		[SerializeField]
		protected Color32 m_faceColor = Color.white;

		// Token: 0x04000457 RID: 1111
		protected Color32 m_outlineColor = Color.black;

		// Token: 0x04000458 RID: 1112
		protected float m_outlineWidth;

		// Token: 0x04000459 RID: 1113
		[SerializeField]
		protected float m_fontSize = -99f;

		// Token: 0x0400045A RID: 1114
		protected float m_currentFontSize;

		// Token: 0x0400045B RID: 1115
		[SerializeField]
		protected float m_fontSizeBase = 36f;

		// Token: 0x0400045C RID: 1116
		protected TMP_TextProcessingStack<float> m_sizeStack = new TMP_TextProcessingStack<float>(16);

		// Token: 0x0400045D RID: 1117
		[SerializeField]
		protected FontWeight m_fontWeight = FontWeight.Regular;

		// Token: 0x0400045E RID: 1118
		protected FontWeight m_FontWeightInternal = FontWeight.Regular;

		// Token: 0x0400045F RID: 1119
		protected TMP_TextProcessingStack<FontWeight> m_FontWeightStack = new TMP_TextProcessingStack<FontWeight>(8);

		// Token: 0x04000460 RID: 1120
		[SerializeField]
		protected bool m_enableAutoSizing;

		// Token: 0x04000461 RID: 1121
		protected float m_maxFontSize;

		// Token: 0x04000462 RID: 1122
		protected float m_minFontSize;

		// Token: 0x04000463 RID: 1123
		protected int m_AutoSizeIterationCount;

		// Token: 0x04000464 RID: 1124
		protected int m_AutoSizeMaxIterationCount = 100;

		// Token: 0x04000465 RID: 1125
		protected bool m_IsAutoSizePointSizeSet;

		// Token: 0x04000466 RID: 1126
		[SerializeField]
		protected float m_fontSizeMin;

		// Token: 0x04000467 RID: 1127
		[SerializeField]
		protected float m_fontSizeMax;

		// Token: 0x04000468 RID: 1128
		[SerializeField]
		protected FontStyles m_fontStyle;

		// Token: 0x04000469 RID: 1129
		protected FontStyles m_FontStyleInternal;

		// Token: 0x0400046A RID: 1130
		protected TMP_FontStyleStack m_fontStyleStack;

		// Token: 0x0400046B RID: 1131
		protected bool m_isUsingBold;

		// Token: 0x0400046C RID: 1132
		[SerializeField]
		protected HorizontalAlignmentOptions m_HorizontalAlignment = HorizontalAlignmentOptions.Left;

		// Token: 0x0400046D RID: 1133
		[SerializeField]
		protected VerticalAlignmentOptions m_VerticalAlignment = VerticalAlignmentOptions.Top;

		// Token: 0x0400046E RID: 1134
		[SerializeField]
		[FormerlySerializedAs("m_lineJustification")]
		protected TextAlignmentOptions m_textAlignment = TextAlignmentOptions.Converted;

		// Token: 0x0400046F RID: 1135
		protected HorizontalAlignmentOptions m_lineJustification;

		// Token: 0x04000470 RID: 1136
		protected TMP_TextProcessingStack<HorizontalAlignmentOptions> m_lineJustificationStack = new TMP_TextProcessingStack<HorizontalAlignmentOptions>(new HorizontalAlignmentOptions[16]);

		// Token: 0x04000471 RID: 1137
		protected Vector3[] m_textContainerLocalCorners = new Vector3[4];

		// Token: 0x04000472 RID: 1138
		[SerializeField]
		protected float m_characterSpacing;

		// Token: 0x04000473 RID: 1139
		protected float m_cSpacing;

		// Token: 0x04000474 RID: 1140
		protected float m_monoSpacing;

		// Token: 0x04000475 RID: 1141
		[SerializeField]
		protected float m_wordSpacing;

		// Token: 0x04000476 RID: 1142
		[SerializeField]
		protected float m_lineSpacing;

		// Token: 0x04000477 RID: 1143
		protected float m_lineSpacingDelta;

		// Token: 0x04000478 RID: 1144
		protected float m_lineHeight = -32767f;

		// Token: 0x04000479 RID: 1145
		protected bool m_IsDrivenLineSpacing;

		// Token: 0x0400047A RID: 1146
		[SerializeField]
		protected float m_lineSpacingMax;

		// Token: 0x0400047B RID: 1147
		[SerializeField]
		protected float m_paragraphSpacing;

		// Token: 0x0400047C RID: 1148
		[SerializeField]
		protected float m_charWidthMaxAdj;

		// Token: 0x0400047D RID: 1149
		protected float m_charWidthAdjDelta;

		// Token: 0x0400047E RID: 1150
		[SerializeField]
		protected bool m_enableWordWrapping;

		// Token: 0x0400047F RID: 1151
		protected bool m_isCharacterWrappingEnabled;

		// Token: 0x04000480 RID: 1152
		protected bool m_isNonBreakingSpace;

		// Token: 0x04000481 RID: 1153
		protected bool m_isIgnoringAlignment;

		// Token: 0x04000482 RID: 1154
		[SerializeField]
		protected float m_wordWrappingRatios = 0.4f;

		// Token: 0x04000483 RID: 1155
		[SerializeField]
		protected TextOverflowModes m_overflowMode;

		// Token: 0x04000484 RID: 1156
		protected int m_firstOverflowCharacterIndex = -1;

		// Token: 0x04000485 RID: 1157
		[SerializeField]
		protected TMP_Text m_linkedTextComponent;

		// Token: 0x04000486 RID: 1158
		[SerializeField]
		internal TMP_Text parentLinkedComponent;

		// Token: 0x04000487 RID: 1159
		protected bool m_isTextTruncated;

		// Token: 0x04000488 RID: 1160
		[SerializeField]
		protected bool m_enableKerning;

		// Token: 0x04000489 RID: 1161
		protected float m_GlyphHorizontalAdvanceAdjustment;

		// Token: 0x0400048A RID: 1162
		[SerializeField]
		protected bool m_enableExtraPadding;

		// Token: 0x0400048B RID: 1163
		[SerializeField]
		protected bool checkPaddingRequired;

		// Token: 0x0400048C RID: 1164
		[SerializeField]
		protected bool m_isRichText = true;

		// Token: 0x0400048D RID: 1165
		[SerializeField]
		protected bool m_parseCtrlCharacters = true;

		// Token: 0x0400048E RID: 1166
		protected bool m_isOverlay;

		// Token: 0x0400048F RID: 1167
		[SerializeField]
		protected bool m_isOrthographic;

		// Token: 0x04000490 RID: 1168
		[SerializeField]
		protected bool m_isCullingEnabled;

		// Token: 0x04000491 RID: 1169
		protected bool m_isMaskingEnabled;

		// Token: 0x04000492 RID: 1170
		protected bool isMaskUpdateRequired;

		// Token: 0x04000493 RID: 1171
		protected bool m_ignoreCulling = true;

		// Token: 0x04000494 RID: 1172
		[SerializeField]
		protected TextureMappingOptions m_horizontalMapping;

		// Token: 0x04000495 RID: 1173
		[SerializeField]
		protected TextureMappingOptions m_verticalMapping;

		// Token: 0x04000496 RID: 1174
		[SerializeField]
		protected float m_uvLineOffset;

		// Token: 0x04000497 RID: 1175
		protected TextRenderFlags m_renderMode = TextRenderFlags.Render;

		// Token: 0x04000498 RID: 1176
		[SerializeField]
		protected VertexSortingOrder m_geometrySortingOrder;

		// Token: 0x04000499 RID: 1177
		[SerializeField]
		protected bool m_IsTextObjectScaleStatic;

		// Token: 0x0400049A RID: 1178
		[SerializeField]
		protected bool m_VertexBufferAutoSizeReduction;

		// Token: 0x0400049B RID: 1179
		protected int m_firstVisibleCharacter;

		// Token: 0x0400049C RID: 1180
		protected int m_maxVisibleCharacters = 99999;

		// Token: 0x0400049D RID: 1181
		protected int m_maxVisibleWords = 99999;

		// Token: 0x0400049E RID: 1182
		protected int m_maxVisibleLines = 99999;

		// Token: 0x0400049F RID: 1183
		[SerializeField]
		protected bool m_useMaxVisibleDescender = true;

		// Token: 0x040004A0 RID: 1184
		[SerializeField]
		protected int m_pageToDisplay = 1;

		// Token: 0x040004A1 RID: 1185
		protected bool m_isNewPage;

		// Token: 0x040004A2 RID: 1186
		[SerializeField]
		protected Vector4 m_margin = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040004A3 RID: 1187
		protected float m_marginLeft;

		// Token: 0x040004A4 RID: 1188
		protected float m_marginRight;

		// Token: 0x040004A5 RID: 1189
		protected float m_marginWidth;

		// Token: 0x040004A6 RID: 1190
		protected float m_marginHeight;

		// Token: 0x040004A7 RID: 1191
		protected float m_width = -1f;

		// Token: 0x040004A8 RID: 1192
		protected TMP_TextInfo m_textInfo;

		// Token: 0x040004A9 RID: 1193
		protected bool m_havePropertiesChanged;

		// Token: 0x040004AA RID: 1194
		[SerializeField]
		protected bool m_isUsingLegacyAnimationComponent;

		// Token: 0x040004AB RID: 1195
		protected Transform m_transform;

		// Token: 0x040004AC RID: 1196
		protected RectTransform m_rectTransform;

		// Token: 0x040004AD RID: 1197
		protected Vector2 m_PreviousRectTransformSize;

		// Token: 0x040004AE RID: 1198
		protected Vector2 m_PreviousPivotPosition;

		// Token: 0x040004AF RID: 1199
		[CompilerGenerated]
		private bool <autoSizeTextContainer>k__BackingField;

		// Token: 0x040004B0 RID: 1200
		protected bool m_autoSizeTextContainer;

		// Token: 0x040004B1 RID: 1201
		protected Mesh m_mesh;

		// Token: 0x040004B2 RID: 1202
		[SerializeField]
		protected bool m_isVolumetricText;

		// Token: 0x040004B3 RID: 1203
		[CompilerGenerated]
		private static Func<int, string, TMP_FontAsset> OnFontAssetRequest;

		// Token: 0x040004B4 RID: 1204
		[CompilerGenerated]
		private static Func<int, string, TMP_SpriteAsset> OnSpriteAssetRequest;

		// Token: 0x040004B5 RID: 1205
		[CompilerGenerated]
		private Action<TMP_TextInfo> OnPreRenderText = delegate(TMP_TextInfo <p0>)
		{
		};

		// Token: 0x040004B6 RID: 1206
		protected TMP_SpriteAnimator m_spriteAnimator;

		// Token: 0x040004B7 RID: 1207
		protected float m_flexibleHeight = -1f;

		// Token: 0x040004B8 RID: 1208
		protected float m_flexibleWidth = -1f;

		// Token: 0x040004B9 RID: 1209
		protected float m_minWidth;

		// Token: 0x040004BA RID: 1210
		protected float m_minHeight;

		// Token: 0x040004BB RID: 1211
		protected float m_maxWidth;

		// Token: 0x040004BC RID: 1212
		protected float m_maxHeight;

		// Token: 0x040004BD RID: 1213
		protected LayoutElement m_LayoutElement;

		// Token: 0x040004BE RID: 1214
		protected float m_preferredWidth;

		// Token: 0x040004BF RID: 1215
		protected float m_renderedWidth;

		// Token: 0x040004C0 RID: 1216
		protected bool m_isPreferredWidthDirty;

		// Token: 0x040004C1 RID: 1217
		protected float m_preferredHeight;

		// Token: 0x040004C2 RID: 1218
		protected float m_renderedHeight;

		// Token: 0x040004C3 RID: 1219
		protected bool m_isPreferredHeightDirty;

		// Token: 0x040004C4 RID: 1220
		protected bool m_isCalculatingPreferredValues;

		// Token: 0x040004C5 RID: 1221
		protected int m_layoutPriority;

		// Token: 0x040004C6 RID: 1222
		protected bool m_isLayoutDirty;

		// Token: 0x040004C7 RID: 1223
		protected bool m_isAwake;

		// Token: 0x040004C8 RID: 1224
		internal bool m_isWaitingOnResourceLoad;

		// Token: 0x040004C9 RID: 1225
		internal TMP_Text.TextInputSources m_inputSource;

		// Token: 0x040004CA RID: 1226
		protected float m_fontScaleMultiplier;

		// Token: 0x040004CB RID: 1227
		private static char[] m_htmlTag = new char[128];

		// Token: 0x040004CC RID: 1228
		private static RichTextTagAttribute[] m_xmlAttribute = new RichTextTagAttribute[8];

		// Token: 0x040004CD RID: 1229
		private static float[] m_attributeParameterValues = new float[16];

		// Token: 0x040004CE RID: 1230
		protected float tag_LineIndent;

		// Token: 0x040004CF RID: 1231
		protected float tag_Indent;

		// Token: 0x040004D0 RID: 1232
		protected TMP_TextProcessingStack<float> m_indentStack = new TMP_TextProcessingStack<float>(new float[16]);

		// Token: 0x040004D1 RID: 1233
		protected bool tag_NoParsing;

		// Token: 0x040004D2 RID: 1234
		protected bool m_isParsingText;

		// Token: 0x040004D3 RID: 1235
		protected Matrix4x4 m_FXMatrix;

		// Token: 0x040004D4 RID: 1236
		protected bool m_isFXMatrixSet;

		// Token: 0x040004D5 RID: 1237
		internal TMP_Text.UnicodeChar[] m_TextProcessingArray = new TMP_Text.UnicodeChar[8];

		// Token: 0x040004D6 RID: 1238
		internal int m_InternalTextProcessingArraySize;

		// Token: 0x040004D7 RID: 1239
		private TMP_CharacterInfo[] m_internalCharacterInfo;

		// Token: 0x040004D8 RID: 1240
		protected int m_totalCharacterCount;

		// Token: 0x040004D9 RID: 1241
		protected static WordWrapState m_SavedWordWrapState = default(WordWrapState);

		// Token: 0x040004DA RID: 1242
		protected static WordWrapState m_SavedLineState = default(WordWrapState);

		// Token: 0x040004DB RID: 1243
		protected static WordWrapState m_SavedEllipsisState = default(WordWrapState);

		// Token: 0x040004DC RID: 1244
		protected static WordWrapState m_SavedLastValidState = default(WordWrapState);

		// Token: 0x040004DD RID: 1245
		protected static WordWrapState m_SavedSoftLineBreakState = default(WordWrapState);

		// Token: 0x040004DE RID: 1246
		internal static TMP_TextProcessingStack<WordWrapState> m_EllipsisInsertionCandidateStack = new TMP_TextProcessingStack<WordWrapState>(8, 8);

		// Token: 0x040004DF RID: 1247
		protected int m_characterCount;

		// Token: 0x040004E0 RID: 1248
		protected int m_firstCharacterOfLine;

		// Token: 0x040004E1 RID: 1249
		protected int m_firstVisibleCharacterOfLine;

		// Token: 0x040004E2 RID: 1250
		protected int m_lastCharacterOfLine;

		// Token: 0x040004E3 RID: 1251
		protected int m_lastVisibleCharacterOfLine;

		// Token: 0x040004E4 RID: 1252
		protected int m_lineNumber;

		// Token: 0x040004E5 RID: 1253
		protected int m_lineVisibleCharacterCount;

		// Token: 0x040004E6 RID: 1254
		protected int m_pageNumber;

		// Token: 0x040004E7 RID: 1255
		protected float m_PageAscender;

		// Token: 0x040004E8 RID: 1256
		protected float m_maxTextAscender;

		// Token: 0x040004E9 RID: 1257
		protected float m_maxCapHeight;

		// Token: 0x040004EA RID: 1258
		protected float m_ElementAscender;

		// Token: 0x040004EB RID: 1259
		protected float m_ElementDescender;

		// Token: 0x040004EC RID: 1260
		protected float m_maxLineAscender;

		// Token: 0x040004ED RID: 1261
		protected float m_maxLineDescender;

		// Token: 0x040004EE RID: 1262
		protected float m_startOfLineAscender;

		// Token: 0x040004EF RID: 1263
		protected float m_startOfLineDescender;

		// Token: 0x040004F0 RID: 1264
		protected float m_lineOffset;

		// Token: 0x040004F1 RID: 1265
		protected Extents m_meshExtents;

		// Token: 0x040004F2 RID: 1266
		protected Color32 m_htmlColor = new Color(255f, 255f, 255f, 128f);

		// Token: 0x040004F3 RID: 1267
		protected TMP_TextProcessingStack<Color32> m_colorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004F4 RID: 1268
		protected TMP_TextProcessingStack<Color32> m_underlineColorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004F5 RID: 1269
		protected TMP_TextProcessingStack<Color32> m_strikethroughColorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004F6 RID: 1270
		protected TMP_TextProcessingStack<HighlightState> m_HighlightStateStack = new TMP_TextProcessingStack<HighlightState>(new HighlightState[16]);

		// Token: 0x040004F7 RID: 1271
		protected TMP_ColorGradient m_colorGradientPreset;

		// Token: 0x040004F8 RID: 1272
		protected TMP_TextProcessingStack<TMP_ColorGradient> m_colorGradientStack = new TMP_TextProcessingStack<TMP_ColorGradient>(new TMP_ColorGradient[16]);

		// Token: 0x040004F9 RID: 1273
		protected bool m_colorGradientPresetIsTinted;

		// Token: 0x040004FA RID: 1274
		protected float m_tabSpacing;

		// Token: 0x040004FB RID: 1275
		protected float m_spacing;

		// Token: 0x040004FC RID: 1276
		protected TMP_TextProcessingStack<int>[] m_TextStyleStacks = new TMP_TextProcessingStack<int>[8];

		// Token: 0x040004FD RID: 1277
		protected int m_TextStyleStackDepth;

		// Token: 0x040004FE RID: 1278
		protected TMP_TextProcessingStack<int> m_ItalicAngleStack = new TMP_TextProcessingStack<int>(new int[16]);

		// Token: 0x040004FF RID: 1279
		protected int m_ItalicAngle;

		// Token: 0x04000500 RID: 1280
		protected TMP_TextProcessingStack<int> m_actionStack = new TMP_TextProcessingStack<int>(new int[16]);

		// Token: 0x04000501 RID: 1281
		protected float m_padding;

		// Token: 0x04000502 RID: 1282
		protected float m_baselineOffset;

		// Token: 0x04000503 RID: 1283
		protected TMP_TextProcessingStack<float> m_baselineOffsetStack = new TMP_TextProcessingStack<float>(new float[16]);

		// Token: 0x04000504 RID: 1284
		protected float m_xAdvance;

		// Token: 0x04000505 RID: 1285
		protected TMP_TextElementType m_textElementType;

		// Token: 0x04000506 RID: 1286
		protected TMP_TextElement m_cached_TextElement;

		// Token: 0x04000507 RID: 1287
		protected TMP_Text.SpecialCharacter m_Ellipsis;

		// Token: 0x04000508 RID: 1288
		protected TMP_Text.SpecialCharacter m_Underline;

		// Token: 0x04000509 RID: 1289
		protected TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x0400050A RID: 1290
		protected TMP_SpriteAsset m_currentSpriteAsset;

		// Token: 0x0400050B RID: 1291
		protected int m_spriteCount;

		// Token: 0x0400050C RID: 1292
		protected int m_spriteIndex;

		// Token: 0x0400050D RID: 1293
		protected int m_spriteAnimationID;

		// Token: 0x0400050E RID: 1294
		private static ProfilerMarker k_ParseTextMarker = new ProfilerMarker("TMP Parse Text");

		// Token: 0x0400050F RID: 1295
		private static ProfilerMarker k_InsertNewLineMarker = new ProfilerMarker("TMP.InsertNewLine");

		// Token: 0x04000510 RID: 1296
		protected bool m_ignoreActiveState;

		// Token: 0x04000511 RID: 1297
		private TMP_Text.TextBackingContainer m_TextBackingArray = new TMP_Text.TextBackingContainer(4);

		// Token: 0x04000512 RID: 1298
		private readonly decimal[] k_Power = new decimal[]
		{
			0.5m,
			0.05m,
			0.005m,
			0.0005m,
			0.00005m,
			0.000005m,
			0.0000005m,
			0.00000005m,
			0.000000005m,
			0.0000000005m
		};

		// Token: 0x04000513 RID: 1299
		protected static Vector2 k_LargePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

		// Token: 0x04000514 RID: 1300
		protected static Vector2 k_LargeNegativeVector2 = new Vector2(-2.1474836E+09f, -2.1474836E+09f);

		// Token: 0x04000515 RID: 1301
		protected static float k_LargePositiveFloat = 32767f;

		// Token: 0x04000516 RID: 1302
		protected static float k_LargeNegativeFloat = -32767f;

		// Token: 0x04000517 RID: 1303
		protected static int k_LargePositiveInt = int.MaxValue;

		// Token: 0x04000518 RID: 1304
		protected static int k_LargeNegativeInt = -2147483647;

		// Token: 0x020000A1 RID: 161
		protected struct CharacterSubstitution
		{
			// Token: 0x06000643 RID: 1603 RVA: 0x000390BB File Offset: 0x000372BB
			public CharacterSubstitution(int index, uint unicode)
			{
				this.index = index;
				this.unicode = unicode;
			}

			// Token: 0x040005F8 RID: 1528
			public int index;

			// Token: 0x040005F9 RID: 1529
			public uint unicode;
		}

		// Token: 0x020000A2 RID: 162
		internal enum TextInputSources
		{
			// Token: 0x040005FB RID: 1531
			TextInputBox,
			// Token: 0x040005FC RID: 1532
			SetText,
			// Token: 0x040005FD RID: 1533
			SetTextArray,
			// Token: 0x040005FE RID: 1534
			TextString
		}

		// Token: 0x020000A3 RID: 163
		[DebuggerDisplay("Unicode ({unicode})  '{(char)unicode}'")]
		internal struct UnicodeChar
		{
			// Token: 0x040005FF RID: 1535
			public int unicode;

			// Token: 0x04000600 RID: 1536
			public int stringIndex;

			// Token: 0x04000601 RID: 1537
			public int length;
		}

		// Token: 0x020000A4 RID: 164
		protected struct SpecialCharacter
		{
			// Token: 0x06000644 RID: 1604 RVA: 0x000390CC File Offset: 0x000372CC
			public SpecialCharacter(TMP_Character character, int materialIndex)
			{
				this.character = character;
				this.fontAsset = (character.textAsset as TMP_FontAsset);
				this.material = ((this.fontAsset != null) ? this.fontAsset.material : null);
				this.materialIndex = materialIndex;
			}

			// Token: 0x04000602 RID: 1538
			public TMP_Character character;

			// Token: 0x04000603 RID: 1539
			public TMP_FontAsset fontAsset;

			// Token: 0x04000604 RID: 1540
			public Material material;

			// Token: 0x04000605 RID: 1541
			public int materialIndex;
		}

		// Token: 0x020000A5 RID: 165
		private struct TextBackingContainer
		{
			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000645 RID: 1605 RVA: 0x0003911A File Offset: 0x0003731A
			public int Capacity
			{
				get
				{
					return this.m_Array.Length;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000646 RID: 1606 RVA: 0x00039124 File Offset: 0x00037324
			// (set) Token: 0x06000647 RID: 1607 RVA: 0x0003912C File Offset: 0x0003732C
			public int Count
			{
				get
				{
					return this.m_Count;
				}
				set
				{
					this.m_Count = value;
				}
			}

			// Token: 0x17000172 RID: 370
			public uint this[int index]
			{
				get
				{
					return this.m_Array[index];
				}
				set
				{
					if (index >= this.m_Array.Length)
					{
						this.Resize(index);
					}
					this.m_Array[index] = value;
				}
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x0003915C File Offset: 0x0003735C
			public TextBackingContainer(int size)
			{
				this.m_Array = new uint[size];
				this.m_Count = 0;
			}

			// Token: 0x0600064B RID: 1611 RVA: 0x00039171 File Offset: 0x00037371
			public void Resize(int size)
			{
				size = Mathf.NextPowerOfTwo(size + 1);
				Array.Resize<uint>(ref this.m_Array, size);
			}

			// Token: 0x04000606 RID: 1542
			private uint[] m_Array;

			// Token: 0x04000607 RID: 1543
			private int m_Count;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600064C RID: 1612 RVA: 0x00039189 File Offset: 0x00037389
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x00039195 File Offset: 0x00037395
			public <>c()
			{
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x0003919D File Offset: 0x0003739D
			internal void <.ctor>b__622_0(TMP_TextInfo <p0>)
			{
			}

			// Token: 0x04000608 RID: 1544
			public static readonly TMP_Text.<>c <>9 = new TMP_Text.<>c();

			// Token: 0x04000609 RID: 1545
			public static Action<TMP_TextInfo> <>9__622_0;
		}
	}
}
