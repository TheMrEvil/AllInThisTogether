using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000039 RID: 57
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/Legacy/Text", 100)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00014CEA File Offset: 0x00012EEA
		protected Text()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00014D1C File Offset: 0x00012F1C
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCache) == null)
				{
					result = (this.m_TextCache = ((this.m_Text.Length != 0) ? new TextGenerator(this.m_Text.Length) : new TextGenerator()));
				}
				return result;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00014D60 File Offset: 0x00012F60
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCacheForLayout) == null)
				{
					result = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return result;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00014D88 File Offset: 0x00012F88
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014E00 File Offset: 0x00013000
		public void FontTextureChanged()
		{
			if (!this)
			{
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
				return;
			}
			this.SetAllDirty();
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00014E4E File Offset: 0x0001304E
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00014E5C File Offset: 0x0001305C
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				if (base.isActiveAndEnabled)
				{
					FontUpdateTracker.UntrackText(this);
				}
				this.m_FontData.font = value;
				if (base.isActiveAndEnabled)
				{
					FontUpdateTracker.TrackText(this);
				}
				this.SetAllDirty();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00014EAB File Offset: 0x000130AB
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00014EB4 File Offset: 0x000130B4
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					if (this.m_Text != value)
					{
						this.m_Text = value;
						this.SetVerticesDirty();
						this.SetLayoutDirty();
					}
					return;
				}
				if (string.IsNullOrEmpty(this.m_Text))
				{
					return;
				}
				this.m_Text = "";
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014F0A File Offset: 0x0001310A
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00014F17 File Offset: 0x00013117
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00014F40 File Offset: 0x00013140
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00014F4D File Offset: 0x0001314D
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00014F76 File Offset: 0x00013176
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00014F83 File Offset: 0x00013183
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00014FAC File Offset: 0x000131AC
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00014FB9 File Offset: 0x000131B9
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00014FE2 File Offset: 0x000131E2
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00014FEF File Offset: 0x000131EF
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00015018 File Offset: 0x00013218
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00015025 File Offset: 0x00013225
		public bool alignByGeometry
		{
			get
			{
				return this.m_FontData.alignByGeometry;
			}
			set
			{
				if (this.m_FontData.alignByGeometry == value)
				{
					return;
				}
				this.m_FontData.alignByGeometry = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00015048 File Offset: 0x00013248
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00015055 File Offset: 0x00013255
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001507E File Offset: 0x0001327E
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001508B File Offset: 0x0001328B
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000150B4 File Offset: 0x000132B4
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000150C1 File Offset: 0x000132C1
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000150EA File Offset: 0x000132EA
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000150F7 File Offset: 0x000132F7
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00015120 File Offset: 0x00013320
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0001512D File Offset: 0x0001332D
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00015158 File Offset: 0x00013358
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000151D6 File Offset: 0x000133D6
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000151EF File Offset: 0x000133EF
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000151FD File Offset: 0x000133FD
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00015213 File Offset: 0x00013413
		internal void AssignDefaultFont()
		{
			this.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00015225 File Offset: 0x00013425
		internal void AssignDefaultFontIfNecessary()
		{
			if (this.font == null)
			{
				this.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00015248 File Offset: 0x00013448
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings result = default(TextGenerationSettings);
			result.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				result.fontSize = this.m_FontData.fontSize;
				result.resizeTextMinSize = this.m_FontData.minSize;
				result.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			result.textAnchor = this.m_FontData.alignment;
			result.alignByGeometry = this.m_FontData.alignByGeometry;
			result.scaleFactor = this.pixelsPerUnit;
			result.color = this.color;
			result.font = this.font;
			result.pivot = base.rectTransform.pivot;
			result.richText = this.m_FontData.richText;
			result.lineSpacing = this.m_FontData.lineSpacing;
			result.fontStyle = this.m_FontData.fontStyle;
			result.resizeTextForBestFit = this.m_FontData.bestFit;
			result.updateBounds = false;
			result.horizontalOverflow = this.m_FontData.horizontalOverflow;
			result.verticalOverflow = this.m_FontData.verticalOverflow;
			return result;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015388 File Offset: 0x00013588
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001545C File Offset: 0x0001365C
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 size = base.rectTransform.rect.size;
			TextGenerationSettings generationSettings = this.GetGenerationSettings(size);
			this.cachedTextGenerator.PopulateWithErrors(this.text, generationSettings, base.gameObject);
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float d = 1f / this.pixelsPerUnit;
			int count = verts.Count;
			if (count <= 0)
			{
				toFill.Clear();
				return;
			}
			Vector2 vector = new Vector2(verts[0].position.x, verts[0].position.y) * d;
			vector = base.PixelAdjustPoint(vector) - vector;
			toFill.Clear();
			if (vector != Vector2.zero)
			{
				for (int i = 0; i < count; i++)
				{
					int num = i & 3;
					this.m_TempVerts[num] = verts[i];
					UIVertex[] tempVerts = this.m_TempVerts;
					int num2 = num;
					tempVerts[num2].position = tempVerts[num2].position * d;
					UIVertex[] tempVerts2 = this.m_TempVerts;
					int num3 = num;
					tempVerts2[num3].position.x = tempVerts2[num3].position.x + vector.x;
					UIVertex[] tempVerts3 = this.m_TempVerts;
					int num4 = num;
					tempVerts3[num4].position.y = tempVerts3[num4].position.y + vector.y;
					if (num == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					int num5 = j & 3;
					this.m_TempVerts[num5] = verts[j];
					UIVertex[] tempVerts4 = this.m_TempVerts;
					int num6 = num5;
					tempVerts4[num6].position = tempVerts4[num6].position * d;
					if (num5 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001564C File Offset: 0x0001384C
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001564E File Offset: 0x0001384E
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00015650 File Offset: 0x00013850
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00015658 File Offset: 0x00013858
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001568A File Offset: 0x0001388A
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00015691 File Offset: 0x00013891
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00015698 File Offset: 0x00013898
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(new Vector2(base.GetPixelAdjustedRect().size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000156E2 File Offset: 0x000138E2
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000156E9 File Offset: 0x000138E9
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x04000170 RID: 368
		[TextArea(3, 10)]
		[SerializeField]
		protected string m_Text = string.Empty;

		// Token: 0x04000171 RID: 369
		private TextGenerator m_TextCache;

		// Token: 0x04000172 RID: 370
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x04000173 RID: 371
		protected static Material s_DefaultText;

		// Token: 0x04000174 RID: 372
		[NonSerialized]
		protected bool m_DisableFontTextureRebuiltCallback;

		// Token: 0x04000175 RID: 373
		private readonly UIVertex[] m_TempVerts = new UIVertex[4];
	}
}
