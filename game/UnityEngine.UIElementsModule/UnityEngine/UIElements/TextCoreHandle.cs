using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BD RID: 701
	internal struct TextCoreHandle : ITextHandle
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00063001 File Offset: 0x00061201
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00063009 File Offset: 0x00061209
		public Vector2 MeasuredSizes
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<MeasuredSizes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MeasuredSizes>k__BackingField = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00063012 File Offset: 0x00061212
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0006301A File Offset: 0x0006121A
		public Vector2 RoundedSizes
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<RoundedSizes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RoundedSizes>k__BackingField = value;
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00063024 File Offset: 0x00061224
		public static ITextHandle New()
		{
			return new TextCoreHandle
			{
				m_CurrentGenerationSettings = new TextGenerationSettings()
			};
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00063050 File Offset: 0x00061250
		internal TextInfo textInfoMesh
		{
			get
			{
				bool flag = this.m_TextInfoMesh == null;
				if (flag)
				{
					this.m_TextInfoMesh = new TextInfo();
				}
				return this.m_TextInfoMesh;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00063084 File Offset: 0x00061284
		internal static TextInfo textInfoLayout
		{
			get
			{
				bool flag = TextCoreHandle.s_TextInfoLayout == null;
				if (flag)
				{
					TextCoreHandle.s_TextInfoLayout = new TextInfo();
				}
				return TextCoreHandle.s_TextInfoLayout;
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000630B4 File Offset: 0x000612B4
		internal bool IsTextInfoAllocated()
		{
			return this.m_TextInfoMesh != null;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000630D0 File Offset: 0x000612D0
		public bool IsLegacy()
		{
			return false;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000630E3 File Offset: 0x000612E3
		public void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000630F0 File Offset: 0x000612F0
		public bool IsDirty(MeshGenerationContextUtils.TextParams parms)
		{
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode && !this.isDirty;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.m_PreviousGenerationSettingsHash = hashCode;
				this.isDirty = false;
				result = true;
			}
			return result;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0006313C File Offset: 0x0006133C
		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			return TextGenerator.GetCursorPosition(this.textInfoMesh, parms.rect, parms.cursorIndex, true);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00063168 File Offset: 0x00061368
		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			this.UpdatePreferredValues(parms);
			return this.m_PreferredSize.x;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00063190 File Offset: 0x00061390
		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			this.UpdatePreferredValues(parms);
			return this.m_PreferredSize.y;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000631B8 File Offset: 0x000613B8
		public float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint)
		{
			bool flag = this.m_TextInfoMesh == null || this.m_TextInfoMesh.characterCount == 0;
			if (flag)
			{
				this.Update(textParams, pixelPerPoint);
			}
			return this.m_TextInfoMesh.lineInfo[0].lineHeight;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00063208 File Offset: 0x00061408
		public int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint)
		{
			this.Update(parms, pixelPerPoint);
			int num = 0;
			foreach (MeshInfo meshInfo2 in this.textInfoMesh.meshInfo)
			{
				num += meshInfo2.vertexCount;
			}
			return num;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00063254 File Offset: 0x00061454
		ITextHandle ITextHandle.New()
		{
			return TextCoreHandle.New();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0006326C File Offset: 0x0006146C
		public TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			Vector2 vector = parms.rect.size;
			bool flag = Mathf.Abs(parms.rect.size.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(parms.rect.size.y - this.RoundedSizes.y) < 0.01f;
			if (flag)
			{
				vector = this.MeasuredSizes;
				parms.wordWrapWidth = vector.x;
			}
			else
			{
				this.RoundedSizes = vector;
				this.MeasuredSizes = vector;
			}
			parms.rect = new Rect(Vector2.zero, vector);
			bool flag2 = !this.IsDirty(parms);
			TextInfo textInfoMesh;
			if (flag2)
			{
				textInfoMesh = this.textInfoMesh;
			}
			else
			{
				TextCoreHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentGenerationSettings);
				this.m_CurrentGenerationSettings.color = parms.fontColor;
				this.m_CurrentGenerationSettings.inverseYAxis = true;
				this.textInfoMesh.isDirty = true;
				TextGenerator.GenerateText(this.m_CurrentGenerationSettings, this.textInfoMesh);
				textInfoMesh = this.textInfoMesh;
			}
			return textInfoMesh;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00063388 File Offset: 0x00061588
		private void UpdatePreferredValues(MeshGenerationContextUtils.TextParams parms)
		{
			Vector2 size = parms.rect.size;
			parms.rect = new Rect(Vector2.zero, size);
			TextCoreHandle.UpdateGenerationSettingsCommon(parms, TextCoreHandle.s_LayoutSettings);
			this.m_PreferredSize = TextGenerator.GetPreferredValues(TextCoreHandle.s_LayoutSettings, TextCoreHandle.textInfoLayout);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000633D8 File Offset: 0x000615D8
		private static TextOverflowMode GetTextOverflowMode(MeshGenerationContextUtils.TextParams textParams)
		{
			bool flag = textParams.textOverflow == TextOverflow.Clip;
			TextOverflowMode result;
			if (flag)
			{
				result = TextOverflowMode.Masking;
			}
			else
			{
				bool flag2 = textParams.textOverflow != TextOverflow.Ellipsis;
				if (flag2)
				{
					result = TextOverflowMode.Overflow;
				}
				else
				{
					bool flag3 = !textParams.wordWrap && textParams.overflow == OverflowInternal.Hidden;
					if (flag3)
					{
						result = TextOverflowMode.Ellipsis;
					}
					else
					{
						result = TextOverflowMode.Overflow;
					}
				}
			}
			return result;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006342C File Offset: 0x0006162C
		private static void UpdateGenerationSettingsCommon(MeshGenerationContextUtils.TextParams painterParams, TextGenerationSettings settings)
		{
			settings.textSettings = TextUtilities.GetTextSettingsFrom(painterParams);
			bool flag = settings.textSettings == null;
			if (!flag)
			{
				settings.fontAsset = TextUtilities.GetFontAsset(painterParams);
				bool flag2 = settings.fontAsset == null;
				if (!flag2)
				{
					settings.material = settings.fontAsset.material;
					settings.screenRect = painterParams.rect;
					settings.text = (string.IsNullOrEmpty(painterParams.text) ? "​" : (painterParams.text + "​"));
					settings.fontSize = (float)((painterParams.fontSize > 0) ? painterParams.fontSize : settings.fontAsset.faceInfo.pointSize);
					settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(painterParams.fontStyle);
					settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(painterParams.anchor);
					settings.wordWrap = painterParams.wordWrap;
					settings.wordWrappingRatio = 0.4f;
					settings.richText = painterParams.richText;
					settings.overflowMode = TextCoreHandle.GetTextOverflowMode(painterParams);
					settings.characterSpacing = painterParams.letterSpacing.value;
					settings.wordSpacing = painterParams.wordSpacing.value;
					settings.paragraphSpacing = painterParams.paragraphSpacing.value;
				}
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00063578 File Offset: 0x00061778
		public bool IsElided()
		{
			bool flag = this.m_TextInfoMesh == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.m_TextInfoMesh.characterCount == 0;
				result = (flag2 || this.m_TextInfoMesh.textElementInfo[this.m_TextInfoMesh.characterCount - 1].character == '…');
			}
			return result;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000635DA File Offset: 0x000617DA
		// Note: this type is marked as 'beforefieldinit'.
		static TextCoreHandle()
		{
		}

		// Token: 0x04000A3C RID: 2620
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <MeasuredSizes>k__BackingField;

		// Token: 0x04000A3D RID: 2621
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <RoundedSizes>k__BackingField;

		// Token: 0x04000A3E RID: 2622
		private Vector2 m_PreferredSize;

		// Token: 0x04000A3F RID: 2623
		private int m_PreviousGenerationSettingsHash;

		// Token: 0x04000A40 RID: 2624
		private TextGenerationSettings m_CurrentGenerationSettings;

		// Token: 0x04000A41 RID: 2625
		private static TextGenerationSettings s_LayoutSettings = new TextGenerationSettings();

		// Token: 0x04000A42 RID: 2626
		private TextInfo m_TextInfoMesh;

		// Token: 0x04000A43 RID: 2627
		private static TextInfo s_TextInfoLayout;

		// Token: 0x04000A44 RID: 2628
		private bool isDirty;
	}
}
