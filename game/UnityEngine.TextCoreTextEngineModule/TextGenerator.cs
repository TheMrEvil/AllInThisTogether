using System;
using System.Collections.Generic;
using UnityEngine.Profiling;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000028 RID: 40
	internal class TextGenerator
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00008B14 File Offset: 0x00006D14
		private static TextGenerator GetTextGenerator()
		{
			bool flag = TextGenerator.s_TextGenerator == null;
			if (flag)
			{
				TextGenerator.s_TextGenerator = new TextGenerator();
			}
			return TextGenerator.s_TextGenerator;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008B44 File Offset: 0x00006D44
		public static void GenerateText(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			}
			else
			{
				bool flag2 = textInfo == null;
				if (flag2)
				{
					Debug.LogError("Null TextInfo provided to TextGenerator. Cannot update its content.");
				}
				else
				{
					TextGenerator textGenerator = TextGenerator.GetTextGenerator();
					Profiler.BeginSample("TextGenerator.GenerateText");
					textGenerator.Prepare(settings, textInfo);
					FontAsset.UpdateFontAssetInUpdateQueue();
					textGenerator.GenerateTextMesh(settings, textInfo);
					Profiler.EndSample();
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public static Vector2 GetCursorPosition(TextGenerationSettings settings, int index)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
				result = Vector2.zero;
			}
			else
			{
				TextInfo textInfo = new TextInfo();
				TextGenerator.GenerateText(settings, textInfo);
				result = TextGenerator.GetCursorPosition(textInfo, settings.screenRect, index, true);
			}
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008C30 File Offset: 0x00006E30
		public static Vector2 GetCursorPosition(TextInfo textInfo, Rect screenRect, int index, bool inverseYAxis = true)
		{
			Vector2 vector = screenRect.position;
			bool flag = textInfo.characterCount == 0;
			Vector2 result;
			if (flag)
			{
				result = vector;
			}
			else
			{
				TextElementInfo textElementInfo = textInfo.textElementInfo[textInfo.characterCount - 1];
				LineInfo lineInfo = textInfo.lineInfo[textElementInfo.lineNumber];
				float num = lineInfo.lineHeight - (lineInfo.ascender - lineInfo.descender);
				bool flag2 = index >= textInfo.characterCount;
				if (flag2)
				{
					vector += (inverseYAxis ? new Vector2(textElementInfo.xAdvance, screenRect.height - lineInfo.ascender - num) : new Vector2(textElementInfo.xAdvance, lineInfo.descender));
					result = vector;
				}
				else
				{
					textElementInfo = textInfo.textElementInfo[index];
					lineInfo = textInfo.lineInfo[textElementInfo.lineNumber];
					num = lineInfo.lineHeight - (lineInfo.ascender - lineInfo.descender);
					vector += (inverseYAxis ? new Vector2(textElementInfo.origin, screenRect.height - lineInfo.ascender - num) : new Vector2(textElementInfo.origin, lineInfo.descender));
					result = vector;
				}
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008D5C File Offset: 0x00006F5C
		public static float GetPreferredWidth(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			float result;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
				result = 0f;
			}
			else
			{
				TextGenerator textGenerator = TextGenerator.GetTextGenerator();
				textGenerator.Prepare(settings, textInfo);
				result = textGenerator.GetPreferredWidthInternal(settings, textInfo);
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008DBC File Offset: 0x00006FBC
		public static float GetPreferredHeight(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			float result;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
				result = 0f;
			}
			else
			{
				TextGenerator textGenerator = TextGenerator.GetTextGenerator();
				textGenerator.Prepare(settings, textInfo);
				result = textGenerator.GetPreferredHeightInternal(settings, textInfo);
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008E1C File Offset: 0x0000701C
		public static Vector2 GetPreferredValues(TextGenerationSettings settings, TextInfo textInfo)
		{
			bool flag = settings.fontAsset == null || settings.fontAsset.characterLookupTable == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
				result = Vector2.zero;
			}
			else
			{
				TextGenerator textGenerator = TextGenerator.GetTextGenerator();
				textGenerator.Prepare(settings, textInfo);
				result = textGenerator.GetPreferredValuesInternal(settings, textInfo);
			}
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008E7C File Offset: 0x0000707C
		private void Prepare(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			Profiler.BeginSample("TextGenerator.Prepare");
			this.m_Padding = 6f;
			this.m_IsMaskingEnabled = false;
			this.GetSpecialCharacters(generationSettings);
			this.ComputeMarginSize(generationSettings.screenRect, generationSettings.margins);
			TextGeneratorUtilities.StringToCharArray(generationSettings.text, ref this.m_CharBuffer, ref this.m_StyleStack, generationSettings);
			this.SetArraySizes(this.m_CharBuffer, generationSettings, textInfo);
			bool autoSize = generationSettings.autoSize;
			if (autoSize)
			{
				this.m_FontSize = Mathf.Clamp(generationSettings.fontSize, generationSettings.fontSizeMin, generationSettings.fontSizeMax);
			}
			else
			{
				this.m_FontSize = generationSettings.fontSize;
			}
			this.m_MaxFontSize = generationSettings.fontSizeMax;
			this.m_MinFontSize = generationSettings.fontSizeMin;
			this.m_LineSpacingDelta = 0f;
			this.m_CharWidthAdjDelta = 0f;
			this.m_IsCharacterWrappingEnabled = false;
			Profiler.EndSample();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008F5C File Offset: 0x0000715C
		private void GenerateTextMesh(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = textInfo == null;
			if (!flag)
			{
				textInfo.Clear();
				bool flag2 = this.m_CharBuffer == null || this.m_CharBuffer.Length == 0 || this.m_CharBuffer[0] == 0;
				if (flag2)
				{
					TextGenerator.ClearMesh(true, textInfo);
					this.m_PreferredWidth = 0f;
					this.m_PreferredHeight = 0f;
				}
				else
				{
					this.m_CurrentFontAsset = generationSettings.fontAsset;
					this.m_CurrentMaterial = generationSettings.material;
					this.m_CurrentMaterialIndex = 0;
					this.m_MaterialReferenceStack.SetDefault(new MaterialReference(this.m_CurrentMaterialIndex, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
					this.m_CurrentSpriteAsset = generationSettings.spriteAsset;
					int totalCharacterCount = this.m_TotalCharacterCount;
					float num = this.m_FontScale = this.m_FontSize / (float)generationSettings.fontAsset.faceInfo.pointSize * generationSettings.fontAsset.faceInfo.scale;
					float num2 = num;
					this.m_FontScaleMultiplier = 1f;
					this.m_CurrentFontSize = this.m_FontSize;
					this.m_SizeStack.SetDefault(this.m_CurrentFontSize);
					this.m_FontStyleInternal = generationSettings.fontStyle;
					this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
					this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
					this.m_FontStyleStack.Clear();
					this.m_LineJustification = generationSettings.textAlignment;
					this.m_LineJustificationStack.SetDefault(this.m_LineJustification);
					float num3 = 0f;
					float num4 = 1f;
					this.m_BaselineOffset = 0f;
					this.m_BaselineOffsetStack.Clear();
					bool flag3 = false;
					Vector3 zero = Vector3.zero;
					bool flag4 = false;
					Vector3 zero2 = Vector3.zero;
					bool flag5 = false;
					Vector3 vector = Vector3.zero;
					Vector3 vector2 = Vector3.zero;
					this.m_FontColor32 = generationSettings.color;
					this.m_HtmlColor = this.m_FontColor32;
					this.m_UnderlineColor = this.m_HtmlColor;
					this.m_StrikethroughColor = this.m_HtmlColor;
					this.m_ColorStack.SetDefault(this.m_HtmlColor);
					this.m_UnderlineColorStack.SetDefault(this.m_HtmlColor);
					this.m_StrikethroughColorStack.SetDefault(this.m_HtmlColor);
					this.m_HighlightColorStack.SetDefault(this.m_HtmlColor);
					this.m_ColorGradientPreset = null;
					this.m_ColorGradientStack.SetDefault(null);
					this.m_ActionStack.Clear();
					this.m_IsFxMatrixSet = false;
					this.m_LineOffset = 0f;
					this.m_LineHeight = -32767f;
					float num5 = this.m_CurrentFontAsset.faceInfo.lineHeight - (this.m_CurrentFontAsset.faceInfo.ascentLine - this.m_CurrentFontAsset.faceInfo.descentLine);
					this.m_CSpacing = 0f;
					this.m_MonoSpacing = 0f;
					this.m_XAdvance = 0f;
					this.m_TagLineIndent = 0f;
					this.m_TagIndent = 0f;
					this.m_IndentStack.SetDefault(0f);
					this.m_TagNoParsing = false;
					this.m_CharacterCount = 0;
					this.m_FirstCharacterOfLine = 0;
					this.m_LastCharacterOfLine = 0;
					this.m_FirstVisibleCharacterOfLine = 0;
					this.m_LastVisibleCharacterOfLine = 0;
					this.m_MaxLineAscender = -32767f;
					this.m_MaxLineDescender = 32767f;
					this.m_LineNumber = 0;
					this.m_LineVisibleCharacterCount = 0;
					bool flag6 = true;
					this.m_FirstOverflowCharacterIndex = -1;
					this.m_PageNumber = 0;
					int num6 = Mathf.Clamp(generationSettings.pageToDisplay - 1, 0, textInfo.pageInfo.Length - 1);
					int num7 = 0;
					int num8 = 0;
					Vector4 margins = generationSettings.margins;
					float marginWidth = this.m_MarginWidth;
					float marginHeight = this.m_MarginHeight;
					this.m_MarginLeft = 0f;
					this.m_MarginRight = 0f;
					this.m_Width = -1f;
					float num9 = marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight;
					this.m_MeshExtents.min = TextGeneratorUtilities.largePositiveVector2;
					this.m_MeshExtents.max = TextGeneratorUtilities.largeNegativeVector2;
					TextSettings textSettings = generationSettings.textSettings;
					textInfo.ClearLineInfo();
					this.m_MaxCapHeight = 0f;
					this.m_MaxAscender = 0f;
					this.m_MaxDescender = 0f;
					float num10 = 0f;
					float num11 = 0f;
					bool flag7 = false;
					this.m_IsNewPage = false;
					bool flag8 = true;
					this.m_IsNonBreakingSpace = false;
					bool flag9 = false;
					bool flag10 = false;
					int num12 = 0;
					this.SaveWordWrappingState(ref this.m_SavedWordWrapState, -1, -1, textInfo);
					this.SaveWordWrappingState(ref this.m_SavedLineState, -1, -1, textInfo);
					this.m_LoopCountA++;
					int num13 = 0;
					while (num13 < this.m_CharBuffer.Length && this.m_CharBuffer[num13] != 0)
					{
						int num14 = this.m_CharBuffer[num13];
						bool flag11 = generationSettings.richText && num14 == 60;
						if (flag11)
						{
							this.m_IsParsingText = true;
							this.m_TextElementType = TextElementType.Character;
							int num15;
							bool flag12 = this.ValidateHtmlTag(this.m_CharBuffer, num13 + 1, out num15, generationSettings, textInfo);
							if (flag12)
							{
								num13 = num15;
								bool flag13 = this.m_TextElementType == TextElementType.Character;
								if (flag13)
								{
									goto IL_3289;
								}
							}
							goto IL_555;
						}
						this.m_TextElementType = textInfo.textElementInfo[this.m_CharacterCount].elementType;
						this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
						this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
						goto IL_555;
						IL_3289:
						int num16 = num13;
						num13 = num16 + 1;
						continue;
						IL_555:
						int currentMaterialIndex = this.m_CurrentMaterialIndex;
						bool isUsingAlternateTypeface = textInfo.textElementInfo[this.m_CharacterCount].isUsingAlternateTypeface;
						this.m_IsParsingText = false;
						bool flag14 = this.m_CharacterCount < generationSettings.firstVisibleCharacter;
						if (flag14)
						{
							textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
							textInfo.textElementInfo[this.m_CharacterCount].character = '​';
							this.m_CharacterCount++;
							goto IL_3289;
						}
						float num17 = 1f;
						bool flag15 = this.m_TextElementType == TextElementType.Character;
						if (flag15)
						{
							bool flag16 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
							if (flag16)
							{
								bool flag17 = char.IsLower((char)num14);
								if (flag17)
								{
									num14 = (int)char.ToUpper((char)num14);
								}
							}
							else
							{
								bool flag18 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
								if (flag18)
								{
									bool flag19 = char.IsUpper((char)num14);
									if (flag19)
									{
										num14 = (int)char.ToLower((char)num14);
									}
								}
								else
								{
									bool flag20 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
									if (flag20)
									{
										bool flag21 = char.IsLower((char)num14);
										if (flag21)
										{
											num17 = 0.8f;
											num14 = (int)char.ToUpper((char)num14);
										}
									}
								}
							}
						}
						bool flag22 = this.m_TextElementType == TextElementType.Sprite;
						if (flag22)
						{
							SpriteCharacter spriteCharacter = (SpriteCharacter)textInfo.textElementInfo[this.m_CharacterCount].textElement;
							this.m_CurrentSpriteAsset = (spriteCharacter.textAsset as SpriteAsset);
							this.m_SpriteIndex = (int)spriteCharacter.glyphIndex;
							bool flag23 = spriteCharacter == null;
							if (flag23)
							{
								goto IL_3289;
							}
							bool flag24 = num14 == 60;
							if (flag24)
							{
								num14 = 57344 + this.m_SpriteIndex;
							}
							else
							{
								this.m_SpriteColor = Color.white;
							}
							float num18 = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
							num2 = this.m_CurrentFontAsset.faceInfo.ascentLine / spriteCharacter.glyph.metrics.height * spriteCharacter.scale * num18 * generationSettings.scale;
							this.m_CachedTextElement = spriteCharacter;
							textInfo.textElementInfo[this.m_CharacterCount].elementType = TextElementType.Sprite;
							textInfo.textElementInfo[this.m_CharacterCount].scale = num18;
							textInfo.textElementInfo[this.m_CharacterCount].spriteAsset = this.m_CurrentSpriteAsset;
							textInfo.textElementInfo[this.m_CharacterCount].fontAsset = this.m_CurrentFontAsset;
							textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
							this.m_CurrentMaterialIndex = currentMaterialIndex;
							num3 = 0f;
						}
						else
						{
							bool flag25 = this.m_TextElementType == TextElementType.Character;
							if (flag25)
							{
								this.m_CachedTextElement = textInfo.textElementInfo[this.m_CharacterCount].textElement;
								bool flag26 = this.m_CachedTextElement == null;
								if (flag26)
								{
									goto IL_3289;
								}
								this.m_CurrentFontAsset = textInfo.textElementInfo[this.m_CharacterCount].fontAsset;
								this.m_CurrentMaterial = textInfo.textElementInfo[this.m_CharacterCount].material;
								this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
								this.m_FontScale = this.m_CurrentFontSize * num17 / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
								num2 = this.m_FontScale * this.m_FontScaleMultiplier * this.m_CachedTextElement.scale * generationSettings.scale;
								textInfo.textElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
								textInfo.textElementInfo[this.m_CharacterCount].scale = num2;
								num3 = ((this.m_CurrentMaterialIndex == 0) ? this.m_Padding : this.GetPaddingForMaterial(this.m_CurrentMaterial, generationSettings.extraPadding));
							}
						}
						float num19 = num2;
						bool flag27 = num14 == 173;
						if (flag27)
						{
							num2 = 0f;
						}
						textInfo.textElementInfo[this.m_CharacterCount].character = (char)num14;
						textInfo.textElementInfo[this.m_CharacterCount].pointSize = this.m_CurrentFontSize;
						textInfo.textElementInfo[this.m_CharacterCount].color = this.m_HtmlColor;
						textInfo.textElementInfo[this.m_CharacterCount].underlineColor = this.m_UnderlineColor;
						textInfo.textElementInfo[this.m_CharacterCount].strikethroughColor = this.m_StrikethroughColor;
						textInfo.textElementInfo[this.m_CharacterCount].highlightColor = this.m_HighlightColor;
						textInfo.textElementInfo[this.m_CharacterCount].style = this.m_FontStyleInternal;
						textInfo.textElementInfo[this.m_CharacterCount].index = num13;
						GlyphValueRecord a = default(GlyphValueRecord);
						float characterSpacing = generationSettings.characterSpacing;
						bool enableKerning = generationSettings.enableKerning;
						if (enableKerning)
						{
							uint glyphIndex = this.m_CachedTextElement.glyphIndex;
							bool flag28 = this.m_CharacterCount < totalCharacterCount - 1;
							if (flag28)
							{
								uint glyphIndex2 = textInfo.textElementInfo[this.m_CharacterCount + 1].textElement.glyphIndex;
								uint key = glyphIndex2 << 16 | glyphIndex;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								bool flag29 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out glyphPairAdjustmentRecord);
								if (flag29)
								{
									a = glyphPairAdjustmentRecord.firstAdjustmentRecord.glyphValueRecord;
								}
							}
							bool flag30 = this.m_CharacterCount >= 1;
							if (flag30)
							{
								uint glyphIndex3 = textInfo.textElementInfo[this.m_CharacterCount - 1].textElement.glyphIndex;
								uint key2 = glyphIndex << 16 | glyphIndex3;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								bool flag31 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out glyphPairAdjustmentRecord);
								if (flag31)
								{
									a += glyphPairAdjustmentRecord.secondAdjustmentRecord.glyphValueRecord;
								}
							}
						}
						bool isRightToLeft = generationSettings.isRightToLeft;
						if (isRightToLeft)
						{
							this.m_XAdvance -= ((this.m_CachedTextElement.glyph.metrics.horizontalAdvance * num4 + generationSettings.characterSpacing + generationSettings.wordSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
							bool flag32 = char.IsWhiteSpace((char)num14) || num14 == 8203;
							if (flag32)
							{
								this.m_XAdvance -= generationSettings.wordSpacing * num2;
							}
						}
						float num20 = 0f;
						bool flag33 = this.m_MonoSpacing != 0f;
						if (flag33)
						{
							num20 = (this.m_MonoSpacing / 2f - (this.m_CachedTextElement.glyph.metrics.width / 2f + this.m_CachedTextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - this.m_CharWidthAdjDelta);
							this.m_XAdvance += num20;
						}
						bool flag34 = this.m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold;
						float num21;
						if (flag34)
						{
							bool flag35 = this.m_CurrentMaterial.HasProperty(TextShaderUtilities.ID_GradientScale);
							if (flag35)
							{
								float @float = this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale);
								num21 = this.m_CurrentFontAsset.boldStyleWeight / 4f * @float * this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
								bool flag36 = num21 + num3 > @float;
								if (flag36)
								{
									num3 = @float - num21;
								}
							}
							else
							{
								num21 = 0f;
							}
							num4 = 1f + this.m_CurrentFontAsset.boldStyleSpacing * 0.01f;
						}
						else
						{
							bool flag37 = this.m_CurrentMaterial.HasProperty(TextShaderUtilities.ID_GradientScale);
							if (flag37)
							{
								float float2 = this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale);
								num21 = this.m_CurrentFontAsset.regularStyleWeight / 4f * float2 * this.m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
								bool flag38 = num21 + num3 > float2;
								if (flag38)
								{
									num3 = float2 - num21;
								}
							}
							else
							{
								num21 = 0f;
							}
							num4 = 1f;
						}
						float num22 = this.m_CurrentFontAsset.faceInfo.baseline * this.m_FontScale * this.m_FontScaleMultiplier * this.m_CurrentFontAsset.faceInfo.scale;
						Vector3 vector3;
						vector3.x = this.m_XAdvance + (this.m_CachedTextElement.glyph.metrics.horizontalBearingX - num3 - num21 + a.xPlacement) * num2 * (1f - this.m_CharWidthAdjDelta);
						vector3.y = num22 + (this.m_CachedTextElement.glyph.metrics.horizontalBearingY + num3 + a.yPlacement) * num2 - this.m_LineOffset + this.m_BaselineOffset;
						vector3.z = 0f;
						Vector3 vector4;
						vector4.x = vector3.x;
						vector4.y = vector3.y - (this.m_CachedTextElement.glyph.metrics.height + num3 * 2f) * num2;
						vector4.z = 0f;
						Vector3 vector5;
						vector5.x = vector4.x + (this.m_CachedTextElement.glyph.metrics.width + num3 * 2f + num21 * 2f) * num2 * (1f - this.m_CharWidthAdjDelta);
						vector5.y = vector3.y;
						vector5.z = 0f;
						Vector3 vector6;
						vector6.x = vector5.x;
						vector6.y = vector4.y;
						vector6.z = 0f;
						bool flag39 = this.m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic;
						if (flag39)
						{
							float num23 = (float)this.m_CurrentFontAsset.italicStyleSlant * 0.01f;
							Vector3 b = new Vector3(num23 * ((this.m_CachedTextElement.glyph.metrics.horizontalBearingY + num3 + num21) * num2), 0f, 0f);
							Vector3 b2 = new Vector3(num23 * ((this.m_CachedTextElement.glyph.metrics.horizontalBearingY - this.m_CachedTextElement.glyph.metrics.height - num3 - num21) * num2), 0f, 0f);
							vector3 += b;
							vector4 += b2;
							vector5 += b;
							vector6 += b2;
						}
						bool isFxMatrixSet = this.m_IsFxMatrixSet;
						if (isFxMatrixSet)
						{
							Vector3 b3 = (vector5 + vector4) / 2f;
							vector3 = this.m_FxMatrix.MultiplyPoint3x4(vector3 - b3) + b3;
							vector4 = this.m_FxMatrix.MultiplyPoint3x4(vector4 - b3) + b3;
							vector5 = this.m_FxMatrix.MultiplyPoint3x4(vector5 - b3) + b3;
							vector6 = this.m_FxMatrix.MultiplyPoint3x4(vector6 - b3) + b3;
						}
						textInfo.textElementInfo[this.m_CharacterCount].bottomLeft = vector4;
						textInfo.textElementInfo[this.m_CharacterCount].topLeft = vector3;
						textInfo.textElementInfo[this.m_CharacterCount].topRight = vector5;
						textInfo.textElementInfo[this.m_CharacterCount].bottomRight = vector6;
						textInfo.textElementInfo[this.m_CharacterCount].origin = this.m_XAdvance;
						textInfo.textElementInfo[this.m_CharacterCount].baseLine = num22 - this.m_LineOffset + this.m_BaselineOffset;
						textInfo.textElementInfo[this.m_CharacterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
						float num24 = this.m_CurrentFontAsset.faceInfo.ascentLine * ((this.m_TextElementType == TextElementType.Character) ? (num2 / num17) : textInfo.textElementInfo[this.m_CharacterCount].scale) + this.m_BaselineOffset;
						textInfo.textElementInfo[this.m_CharacterCount].ascender = num24 - this.m_LineOffset;
						this.m_MaxLineAscender = ((num24 > this.m_MaxLineAscender) ? num24 : this.m_MaxLineAscender);
						float num25 = this.m_CurrentFontAsset.faceInfo.descentLine * ((this.m_TextElementType == TextElementType.Character) ? (num2 / num17) : textInfo.textElementInfo[this.m_CharacterCount].scale) + this.m_BaselineOffset;
						float num26 = textInfo.textElementInfo[this.m_CharacterCount].descender = num25 - this.m_LineOffset;
						this.m_MaxLineDescender = ((num25 < this.m_MaxLineDescender) ? num25 : this.m_MaxLineDescender);
						bool flag40 = (this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript;
						if (flag40)
						{
							float num27 = (num24 - this.m_BaselineOffset) / this.m_CurrentFontAsset.faceInfo.subscriptSize;
							num24 = this.m_MaxLineAscender;
							this.m_MaxLineAscender = ((num27 > this.m_MaxLineAscender) ? num27 : this.m_MaxLineAscender);
							float num28 = (num25 - this.m_BaselineOffset) / this.m_CurrentFontAsset.faceInfo.subscriptSize;
							num25 = this.m_MaxLineDescender;
							this.m_MaxLineDescender = ((num28 < this.m_MaxLineDescender) ? num28 : this.m_MaxLineDescender);
						}
						bool flag41 = this.m_LineNumber == 0 || this.m_IsNewPage;
						if (flag41)
						{
							this.m_MaxAscender = ((this.m_MaxAscender > num24) ? this.m_MaxAscender : num24);
							this.m_MaxCapHeight = Mathf.Max(this.m_MaxCapHeight, this.m_CurrentFontAsset.faceInfo.capLine * num2 / num17);
						}
						bool flag42 = this.m_LineOffset == 0f;
						if (flag42)
						{
							num10 = ((num10 > num24) ? num10 : num24);
						}
						textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
						bool flag43 = num14 == 9 || num14 == 160 || num14 == 8199 || (!char.IsWhiteSpace((char)num14) && num14 != 8203) || this.m_TextElementType == TextElementType.Sprite;
						if (flag43)
						{
							textInfo.textElementInfo[this.m_CharacterCount].isVisible = true;
							num9 = ((this.m_Width != -1f) ? Mathf.Min(marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight, this.m_Width) : (marginWidth + 0.0001f - this.m_MarginLeft - this.m_MarginRight));
							textInfo.lineInfo[this.m_LineNumber].marginLeft = this.m_MarginLeft;
							bool flag44 = (this.m_LineJustification & (TextAlignment)16) == (TextAlignment)16 || (this.m_LineJustification & (TextAlignment)8) == (TextAlignment)8;
							float num29 = Mathf.Abs(this.m_XAdvance) + ((!generationSettings.isRightToLeft) ? this.m_CachedTextElement.glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_CharWidthAdjDelta) * ((num14 != 173) ? num2 : num19);
							bool flag45 = num29 > num9 * (flag44 ? 1.05f : 1f);
							if (flag45)
							{
								num8 = this.m_CharacterCount - 1;
								bool flag46 = generationSettings.wordWrap && this.m_CharacterCount != this.m_FirstCharacterOfLine;
								if (flag46)
								{
									bool flag47 = num12 == this.m_SavedWordWrapState.previousWordBreak || flag8;
									if (flag47)
									{
										bool flag48 = generationSettings.autoSize && this.m_FontSize > generationSettings.fontSizeMin;
										if (flag48)
										{
											bool flag49 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
											if (flag49)
											{
												this.m_LoopCountA = 0;
												this.m_CharWidthAdjDelta += 0.01f;
												this.GenerateTextMesh(generationSettings, textInfo);
												return;
											}
											this.m_MaxFontSize = this.m_FontSize;
											this.m_FontSize -= Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
											this.m_FontSize = (float)((int)(Mathf.Max(this.m_FontSize, generationSettings.fontSizeMin) * 20f + 0.5f)) / 20f;
											bool flag50 = this.m_LoopCountA > 20;
											if (flag50)
											{
												return;
											}
											this.GenerateTextMesh(generationSettings, textInfo);
											return;
										}
										else
										{
											bool flag51 = !this.m_IsCharacterWrappingEnabled;
											if (flag51)
											{
												bool flag52 = !flag9;
												if (flag52)
												{
													flag9 = true;
												}
												else
												{
													this.m_IsCharacterWrappingEnabled = true;
												}
											}
											else
											{
												flag10 = true;
											}
										}
									}
									num13 = this.RestoreWordWrappingState(ref this.m_SavedWordWrapState, textInfo);
									num12 = num13;
									bool flag53 = this.m_CharBuffer[num13] == 173;
									if (flag53)
									{
										this.m_CharBuffer[num13] = 45;
										this.GenerateTextMesh(generationSettings, textInfo);
										return;
									}
									bool flag54 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f && !this.m_IsNewPage;
									if (flag54)
									{
										float num30 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
										TextGeneratorUtilities.AdjustLineOffset(this.m_FirstCharacterOfLine, this.m_CharacterCount, num30, textInfo);
										this.m_LineOffset += num30;
										this.m_SavedWordWrapState.lineOffset = this.m_LineOffset;
										this.m_SavedWordWrapState.previousLineAscender = this.m_MaxLineAscender;
									}
									this.m_IsNewPage = false;
									float num31 = this.m_MaxLineAscender - this.m_LineOffset;
									float num32 = this.m_MaxLineDescender - this.m_LineOffset;
									this.m_MaxDescender = ((this.m_MaxDescender < num32) ? this.m_MaxDescender : num32);
									bool flag55 = !flag7;
									if (flag55)
									{
										num11 = this.m_MaxDescender;
									}
									bool flag56 = generationSettings.useMaxVisibleDescender && (this.m_CharacterCount >= generationSettings.maxVisibleCharacters || this.m_LineNumber >= generationSettings.maxVisibleLines);
									if (flag56)
									{
										flag7 = true;
									}
									textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex = this.m_FirstCharacterOfLine;
									textInfo.lineInfo[this.m_LineNumber].firstVisibleCharacterIndex = (this.m_FirstVisibleCharacterOfLine = ((this.m_FirstCharacterOfLine > this.m_FirstVisibleCharacterOfLine) ? this.m_FirstCharacterOfLine : this.m_FirstVisibleCharacterOfLine));
									textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex = (this.m_LastCharacterOfLine = ((this.m_CharacterCount - 1 > 0) ? (this.m_CharacterCount - 1) : 0));
									textInfo.lineInfo[this.m_LineNumber].lastVisibleCharacterIndex = (this.m_LastVisibleCharacterOfLine = ((this.m_LastVisibleCharacterOfLine < this.m_FirstVisibleCharacterOfLine) ? this.m_FirstVisibleCharacterOfLine : this.m_LastVisibleCharacterOfLine));
									textInfo.lineInfo[this.m_LineNumber].characterCount = textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex - textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex + 1;
									textInfo.lineInfo[this.m_LineNumber].visibleCharacterCount = this.m_LineVisibleCharacterCount;
									textInfo.lineInfo[this.m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[this.m_FirstVisibleCharacterOfLine].bottomLeft.x, num32);
									textInfo.lineInfo[this.m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].topRight.x, num31);
									textInfo.lineInfo[this.m_LineNumber].length = textInfo.lineInfo[this.m_LineNumber].lineExtents.max.x;
									textInfo.lineInfo[this.m_LineNumber].width = num9;
									textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance - (generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 - this.m_CSpacing;
									textInfo.lineInfo[this.m_LineNumber].baseline = 0f - this.m_LineOffset;
									textInfo.lineInfo[this.m_LineNumber].ascender = num31;
									textInfo.lineInfo[this.m_LineNumber].descender = num32;
									textInfo.lineInfo[this.m_LineNumber].lineHeight = num31 - num32 + num5 * num;
									this.m_FirstCharacterOfLine = this.m_CharacterCount;
									this.m_LineVisibleCharacterCount = 0;
									this.SaveWordWrappingState(ref this.m_SavedLineState, num13, this.m_CharacterCount - 1, textInfo);
									this.m_LineNumber++;
									flag6 = true;
									flag8 = true;
									bool flag57 = this.m_LineNumber >= textInfo.lineInfo.Length;
									if (flag57)
									{
										TextGeneratorUtilities.ResizeLineExtents(this.m_LineNumber, textInfo);
									}
									bool flag58 = this.m_LineHeight == -32767f;
									if (flag58)
									{
										float num33 = textInfo.textElementInfo[this.m_CharacterCount].ascender - textInfo.textElementInfo[this.m_CharacterCount].baseLine;
										float num34 = 0f - this.m_MaxLineDescender + num33 + (num5 + generationSettings.lineSpacing + this.m_LineSpacingDelta) * num;
										this.m_LineOffset += num34;
										this.m_StartOfLineAscender = num33;
									}
									else
									{
										this.m_LineOffset += this.m_LineHeight + generationSettings.lineSpacing * num;
									}
									this.m_MaxLineAscender = -32767f;
									this.m_MaxLineDescender = 32767f;
									this.m_XAdvance = 0f + this.m_TagIndent;
									goto IL_3289;
								}
								else
								{
									bool flag59 = generationSettings.autoSize && this.m_FontSize > generationSettings.fontSizeMin;
									if (flag59)
									{
										bool flag60 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
										if (flag60)
										{
											this.m_LoopCountA = 0;
											this.m_CharWidthAdjDelta += 0.01f;
											this.GenerateTextMesh(generationSettings, textInfo);
											return;
										}
										this.m_MaxFontSize = this.m_FontSize;
										this.m_FontSize -= Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
										this.m_FontSize = (float)((int)(Mathf.Max(this.m_FontSize, generationSettings.fontSizeMin) * 20f + 0.5f)) / 20f;
										bool flag61 = this.m_LoopCountA > 20;
										if (flag61)
										{
											return;
										}
										this.GenerateTextMesh(generationSettings, textInfo);
										return;
									}
									else
									{
										switch (generationSettings.overflowMode)
										{
										case TextOverflowMode.Overflow:
										{
											bool isMaskingEnabled = this.m_IsMaskingEnabled;
											if (isMaskingEnabled)
											{
												this.DisableMasking();
											}
											break;
										}
										case TextOverflowMode.Ellipsis:
										{
											bool isMaskingEnabled2 = this.m_IsMaskingEnabled;
											if (isMaskingEnabled2)
											{
												this.DisableMasking();
											}
											bool flag62 = this.m_CharacterCount < 1;
											if (!flag62)
											{
												this.m_CharBuffer[num13 - 1] = 8230;
												this.m_CharBuffer[num13] = 0;
												bool flag63 = this.m_Ellipsis.character != null;
												if (flag63)
												{
													textInfo.textElementInfo[num8].character = '…';
													textInfo.textElementInfo[num8].textElement = this.m_Ellipsis.character;
													textInfo.textElementInfo[num8].fontAsset = this.m_MaterialReferences[0].fontAsset;
													textInfo.textElementInfo[num8].material = this.m_MaterialReferences[0].material;
													textInfo.textElementInfo[num8].materialReferenceIndex = 0;
												}
												else
												{
													Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + generationSettings.fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).");
												}
												this.m_TotalCharacterCount = num8 + 1;
												this.GenerateTextMesh(generationSettings, textInfo);
												return;
											}
											textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
											break;
										}
										case TextOverflowMode.Masking:
										{
											bool flag64 = !this.m_IsMaskingEnabled;
											if (flag64)
											{
												this.EnableMasking();
											}
											break;
										}
										case TextOverflowMode.Truncate:
										{
											bool isMaskingEnabled3 = this.m_IsMaskingEnabled;
											if (isMaskingEnabled3)
											{
												this.DisableMasking();
											}
											textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
											break;
										}
										case TextOverflowMode.ScrollRect:
										{
											bool flag65 = !this.m_IsMaskingEnabled;
											if (flag65)
											{
												this.EnableMasking();
											}
											break;
										}
										}
									}
								}
							}
							bool flag66 = num14 == 9 || num14 == 160 || num14 == 8199;
							if (flag66)
							{
								textInfo.textElementInfo[this.m_CharacterCount].isVisible = false;
								this.m_LastVisibleCharacterOfLine = this.m_CharacterCount;
								LineInfo[] lineInfo = textInfo.lineInfo;
								int lineNumber = this.m_LineNumber;
								lineInfo[lineNumber].spaceCount = lineInfo[lineNumber].spaceCount + 1;
								textInfo.spaceCount++;
							}
							else
							{
								bool overrideRichTextColors = generationSettings.overrideRichTextColors;
								Color32 vertexColor;
								if (overrideRichTextColors)
								{
									vertexColor = this.m_FontColor32;
								}
								else
								{
									vertexColor = this.m_HtmlColor;
								}
								bool flag67 = this.m_TextElementType == TextElementType.Character;
								if (flag67)
								{
									this.SaveGlyphVertexInfo(num3, num21, vertexColor, generationSettings, textInfo);
								}
								else
								{
									bool flag68 = this.m_TextElementType == TextElementType.Sprite;
									if (flag68)
									{
										this.SaveSpriteVertexInfo(vertexColor, generationSettings, textInfo);
									}
								}
							}
							bool flag69 = textInfo.textElementInfo[this.m_CharacterCount].isVisible && num14 != 173;
							if (flag69)
							{
								bool flag70 = flag6;
								if (flag70)
								{
									flag6 = false;
									this.m_FirstVisibleCharacterOfLine = this.m_CharacterCount;
								}
								this.m_LineVisibleCharacterCount++;
								this.m_LastVisibleCharacterOfLine = this.m_CharacterCount;
							}
						}
						else
						{
							bool flag71 = (num14 == 10 || char.IsSeparator((char)num14)) && num14 != 173 && num14 != 8203 && num14 != 8288;
							if (flag71)
							{
								LineInfo[] lineInfo2 = textInfo.lineInfo;
								int lineNumber2 = this.m_LineNumber;
								lineInfo2[lineNumber2].spaceCount = lineInfo2[lineNumber2].spaceCount + 1;
								textInfo.spaceCount++;
								bool flag72 = num14 == 160;
								if (flag72)
								{
									textInfo.lineInfo[this.m_LineNumber].controlCharacterCount = 1;
								}
							}
						}
						bool flag73 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f && !this.m_IsNewPage;
						if (flag73)
						{
							float num35 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
							TextGeneratorUtilities.AdjustLineOffset(this.m_FirstCharacterOfLine, this.m_CharacterCount, num35, textInfo);
							num26 -= num35;
							this.m_LineOffset += num35;
							this.m_StartOfLineAscender += num35;
							this.m_SavedWordWrapState.lineOffset = this.m_LineOffset;
							this.m_SavedWordWrapState.previousLineAscender = this.m_StartOfLineAscender;
						}
						textInfo.textElementInfo[this.m_CharacterCount].lineNumber = this.m_LineNumber;
						textInfo.textElementInfo[this.m_CharacterCount].pageNumber = this.m_PageNumber;
						bool flag74 = (num14 != 10 && num14 != 13 && num14 != 8230) || textInfo.lineInfo[this.m_LineNumber].characterCount == 1;
						if (flag74)
						{
							textInfo.lineInfo[this.m_LineNumber].alignment = this.m_LineJustification;
						}
						bool flag75 = this.m_MaxAscender - num26 > marginHeight + 0.0001f;
						if (flag75)
						{
							bool flag76 = generationSettings.autoSize && this.m_LineSpacingDelta > generationSettings.lineSpacingMax && this.m_LineNumber > 0;
							if (flag76)
							{
								this.m_LoopCountA = 0;
								this.m_LineSpacingDelta -= 1f;
								this.GenerateTextMesh(generationSettings, textInfo);
								return;
							}
							bool flag77 = generationSettings.autoSize && this.m_FontSize > generationSettings.fontSizeMin;
							if (flag77)
							{
								this.m_MaxFontSize = this.m_FontSize;
								this.m_FontSize -= Mathf.Max((this.m_FontSize - this.m_MinFontSize) / 2f, 0.05f);
								this.m_FontSize = (float)((int)(Mathf.Max(this.m_FontSize, generationSettings.fontSizeMin) * 20f + 0.5f)) / 20f;
								bool flag78 = this.m_LoopCountA > 20;
								if (flag78)
								{
									return;
								}
								this.GenerateTextMesh(generationSettings, textInfo);
								return;
							}
							else
							{
								bool flag79 = this.m_FirstOverflowCharacterIndex == -1;
								if (flag79)
								{
									this.m_FirstOverflowCharacterIndex = this.m_CharacterCount;
								}
								switch (generationSettings.overflowMode)
								{
								case TextOverflowMode.Overflow:
								{
									bool isMaskingEnabled4 = this.m_IsMaskingEnabled;
									if (isMaskingEnabled4)
									{
										this.DisableMasking();
									}
									break;
								}
								case TextOverflowMode.Ellipsis:
								{
									bool isMaskingEnabled5 = this.m_IsMaskingEnabled;
									if (isMaskingEnabled5)
									{
										this.DisableMasking();
									}
									bool flag80 = this.m_LineNumber > 0;
									if (flag80)
									{
										this.m_CharBuffer[textInfo.textElementInfo[num8].index] = 8230;
										this.m_CharBuffer[textInfo.textElementInfo[num8].index + 1] = 0;
										bool flag81 = this.m_Ellipsis.character != null;
										if (flag81)
										{
											textInfo.textElementInfo[num8].character = '…';
											textInfo.textElementInfo[num8].textElement = this.m_Ellipsis.character;
											textInfo.textElementInfo[num8].fontAsset = this.m_MaterialReferences[0].fontAsset;
											textInfo.textElementInfo[num8].material = this.m_MaterialReferences[0].material;
											textInfo.textElementInfo[num8].materialReferenceIndex = 0;
										}
										else
										{
											Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + generationSettings.fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).");
										}
										this.m_TotalCharacterCount = num8 + 1;
										this.GenerateTextMesh(generationSettings, textInfo);
										return;
									}
									break;
								}
								case TextOverflowMode.Masking:
								{
									bool flag82 = !this.m_IsMaskingEnabled;
									if (flag82)
									{
										this.EnableMasking();
									}
									break;
								}
								case TextOverflowMode.Truncate:
								{
									bool isMaskingEnabled6 = this.m_IsMaskingEnabled;
									if (isMaskingEnabled6)
									{
										this.DisableMasking();
									}
									bool flag83 = this.m_LineNumber > 0;
									if (flag83)
									{
										this.m_CharBuffer[textInfo.textElementInfo[num8].index + 1] = 0;
										this.m_TotalCharacterCount = num8 + 1;
										this.GenerateTextMesh(generationSettings, textInfo);
										return;
									}
									TextGenerator.ClearMesh(false, textInfo);
									return;
								}
								case TextOverflowMode.ScrollRect:
								{
									bool flag84 = !this.m_IsMaskingEnabled;
									if (flag84)
									{
										this.EnableMasking();
									}
									break;
								}
								case TextOverflowMode.Page:
								{
									bool isMaskingEnabled7 = this.m_IsMaskingEnabled;
									if (isMaskingEnabled7)
									{
										this.DisableMasking();
									}
									bool flag85 = num14 == 13 || num14 == 10;
									if (!flag85)
									{
										bool flag86 = num13 == 0;
										if (flag86)
										{
											return;
										}
										bool flag87 = num7 == num13;
										if (flag87)
										{
											this.m_CharBuffer[num13] = 0;
										}
										num7 = num13;
										num13 = this.RestoreWordWrappingState(ref this.m_SavedLineState, textInfo);
										this.m_IsNewPage = true;
										this.m_XAdvance = 0f + this.m_TagIndent;
										this.m_LineOffset = 0f;
										this.m_MaxAscender = 0f;
										num10 = 0f;
										this.m_LineNumber++;
										this.m_PageNumber++;
										goto IL_3289;
									}
									break;
								}
								case TextOverflowMode.Linked:
								{
									bool flag88 = this.m_LineNumber > 0;
									if (flag88)
									{
										this.m_CharBuffer[num13] = 0;
										this.m_TotalCharacterCount = this.m_CharacterCount;
										this.GenerateTextMesh(generationSettings, textInfo);
										return;
									}
									TextGenerator.ClearMesh(true, textInfo);
									return;
								}
								}
							}
						}
						bool flag89 = num14 == 9;
						if (flag89)
						{
							float num36 = this.m_CurrentFontAsset.faceInfo.tabWidth * (float)this.m_CurrentFontAsset.tabMultiple * num2;
							float num37 = Mathf.Ceil(this.m_XAdvance / num36) * num36;
							this.m_XAdvance = ((num37 > this.m_XAdvance) ? num37 : (this.m_XAdvance + num36));
						}
						else
						{
							bool flag90 = this.m_MonoSpacing != 0f;
							if (flag90)
							{
								this.m_XAdvance += (this.m_MonoSpacing - num20 + (generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
								bool flag91 = char.IsWhiteSpace((char)num14) || num14 == 8203;
								if (flag91)
								{
									this.m_XAdvance += generationSettings.wordSpacing * num2;
								}
							}
							else
							{
								bool flag92 = !generationSettings.isRightToLeft;
								if (flag92)
								{
									float num38 = 1f;
									bool isFxMatrixSet2 = this.m_IsFxMatrixSet;
									if (isFxMatrixSet2)
									{
										num38 = this.m_FxMatrix.m00;
									}
									this.m_XAdvance += ((this.m_CachedTextElement.glyph.metrics.horizontalAdvance * num38 * num4 + generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing + a.xAdvance) * num2 + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
									bool flag93 = char.IsWhiteSpace((char)num14) || num14 == 8203;
									if (flag93)
									{
										this.m_XAdvance += generationSettings.wordSpacing * num2;
									}
								}
								else
								{
									this.m_XAdvance -= a.xAdvance * num2;
								}
							}
						}
						textInfo.textElementInfo[this.m_CharacterCount].xAdvance = this.m_XAdvance;
						bool flag94 = num14 == 13;
						if (flag94)
						{
							this.m_XAdvance = 0f + this.m_TagIndent;
						}
						bool flag95 = num14 == 10 || this.m_CharacterCount == totalCharacterCount - 1;
						if (flag95)
						{
							bool flag96 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f && !this.m_IsNewPage;
							if (flag96)
							{
								float num39 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
								TextGeneratorUtilities.AdjustLineOffset(this.m_FirstCharacterOfLine, this.m_CharacterCount, num39, textInfo);
								this.m_LineOffset += num39;
							}
							this.m_IsNewPage = false;
							float num40 = this.m_MaxLineAscender - this.m_LineOffset;
							float num41 = this.m_MaxLineDescender - this.m_LineOffset;
							this.m_MaxDescender = ((this.m_MaxDescender < num41) ? this.m_MaxDescender : num41);
							bool flag97 = !flag7;
							if (flag97)
							{
								num11 = this.m_MaxDescender;
							}
							bool flag98 = generationSettings.useMaxVisibleDescender && (this.m_CharacterCount >= generationSettings.maxVisibleCharacters || this.m_LineNumber >= generationSettings.maxVisibleLines);
							if (flag98)
							{
								flag7 = true;
							}
							textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex = this.m_FirstCharacterOfLine;
							textInfo.lineInfo[this.m_LineNumber].firstVisibleCharacterIndex = (this.m_FirstVisibleCharacterOfLine = ((this.m_FirstCharacterOfLine > this.m_FirstVisibleCharacterOfLine) ? this.m_FirstCharacterOfLine : this.m_FirstVisibleCharacterOfLine));
							textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex = (this.m_LastCharacterOfLine = this.m_CharacterCount);
							textInfo.lineInfo[this.m_LineNumber].lastVisibleCharacterIndex = (this.m_LastVisibleCharacterOfLine = ((this.m_LastVisibleCharacterOfLine < this.m_FirstVisibleCharacterOfLine) ? this.m_FirstVisibleCharacterOfLine : this.m_LastVisibleCharacterOfLine));
							textInfo.lineInfo[this.m_LineNumber].characterCount = textInfo.lineInfo[this.m_LineNumber].lastCharacterIndex - textInfo.lineInfo[this.m_LineNumber].firstCharacterIndex + 1;
							textInfo.lineInfo[this.m_LineNumber].visibleCharacterCount = this.m_LineVisibleCharacterCount;
							textInfo.lineInfo[this.m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[this.m_FirstVisibleCharacterOfLine].bottomLeft.x, num41);
							textInfo.lineInfo[this.m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].topRight.x, num40);
							textInfo.lineInfo[this.m_LineNumber].length = textInfo.lineInfo[this.m_LineNumber].lineExtents.max.x - num3 * num2;
							textInfo.lineInfo[this.m_LineNumber].width = num9;
							bool flag99 = textInfo.lineInfo[this.m_LineNumber].characterCount == 1;
							if (flag99)
							{
								textInfo.lineInfo[this.m_LineNumber].alignment = this.m_LineJustification;
							}
							bool isVisible = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].isVisible;
							if (isVisible)
							{
								textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastVisibleCharacterOfLine].xAdvance - (generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 - this.m_CSpacing;
							}
							else
							{
								textInfo.lineInfo[this.m_LineNumber].maxAdvance = textInfo.textElementInfo[this.m_LastCharacterOfLine].xAdvance - (generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 - this.m_CSpacing;
							}
							textInfo.lineInfo[this.m_LineNumber].baseline = 0f - this.m_LineOffset;
							textInfo.lineInfo[this.m_LineNumber].ascender = num40;
							textInfo.lineInfo[this.m_LineNumber].descender = num41;
							textInfo.lineInfo[this.m_LineNumber].lineHeight = num40 - num41 + num5 * num;
							this.m_FirstCharacterOfLine = this.m_CharacterCount + 1;
							this.m_LineVisibleCharacterCount = 0;
							bool flag100 = num14 == 10;
							if (flag100)
							{
								this.SaveWordWrappingState(ref this.m_SavedLineState, num13, this.m_CharacterCount, textInfo);
								this.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_CharacterCount, textInfo);
								this.m_LineNumber++;
								flag6 = true;
								flag9 = false;
								flag8 = true;
								bool flag101 = this.m_LineNumber >= textInfo.lineInfo.Length;
								if (flag101)
								{
									TextGeneratorUtilities.ResizeLineExtents(this.m_LineNumber, textInfo);
								}
								bool flag102 = this.m_LineHeight == -32767f;
								if (flag102)
								{
									float num34 = 0f - this.m_MaxLineDescender + num24 + (num5 + generationSettings.lineSpacing + generationSettings.paragraphSpacing + this.m_LineSpacingDelta) * num;
									this.m_LineOffset += num34;
								}
								else
								{
									this.m_LineOffset += this.m_LineHeight + (generationSettings.lineSpacing + generationSettings.paragraphSpacing) * num;
								}
								this.m_MaxLineAscender = -32767f;
								this.m_MaxLineDescender = 32767f;
								this.m_StartOfLineAscender = num24;
								this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
								num8 = this.m_CharacterCount - 1;
								this.m_CharacterCount++;
								goto IL_3289;
							}
						}
						bool isVisible2 = textInfo.textElementInfo[this.m_CharacterCount].isVisible;
						if (isVisible2)
						{
							this.m_MeshExtents.min.x = Mathf.Min(this.m_MeshExtents.min.x, textInfo.textElementInfo[this.m_CharacterCount].bottomLeft.x);
							this.m_MeshExtents.min.y = Mathf.Min(this.m_MeshExtents.min.y, textInfo.textElementInfo[this.m_CharacterCount].bottomLeft.y);
							this.m_MeshExtents.max.x = Mathf.Max(this.m_MeshExtents.max.x, textInfo.textElementInfo[this.m_CharacterCount].topRight.x);
							this.m_MeshExtents.max.y = Mathf.Max(this.m_MeshExtents.max.y, textInfo.textElementInfo[this.m_CharacterCount].topRight.y);
						}
						bool flag103 = generationSettings.overflowMode == TextOverflowMode.Page && num14 != 13 && num14 != 10;
						if (flag103)
						{
							bool flag104 = this.m_PageNumber + 1 > textInfo.pageInfo.Length;
							if (flag104)
							{
								TextInfo.Resize<PageInfo>(ref textInfo.pageInfo, this.m_PageNumber + 1, true);
							}
							textInfo.pageInfo[this.m_PageNumber].ascender = num10;
							textInfo.pageInfo[this.m_PageNumber].descender = ((num25 < textInfo.pageInfo[this.m_PageNumber].descender) ? num25 : textInfo.pageInfo[this.m_PageNumber].descender);
							bool flag105 = this.m_PageNumber == 0 && this.m_CharacterCount == 0;
							if (flag105)
							{
								textInfo.pageInfo[this.m_PageNumber].firstCharacterIndex = this.m_CharacterCount;
							}
							else
							{
								bool flag106 = this.m_CharacterCount > 0 && this.m_PageNumber != textInfo.textElementInfo[this.m_CharacterCount - 1].pageNumber;
								if (flag106)
								{
									textInfo.pageInfo[this.m_PageNumber - 1].lastCharacterIndex = this.m_CharacterCount - 1;
									textInfo.pageInfo[this.m_PageNumber].firstCharacterIndex = this.m_CharacterCount;
								}
								else
								{
									bool flag107 = this.m_CharacterCount == totalCharacterCount - 1;
									if (flag107)
									{
										textInfo.pageInfo[this.m_PageNumber].lastCharacterIndex = this.m_CharacterCount;
									}
								}
							}
						}
						bool flag108 = generationSettings.wordWrap || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis;
						if (flag108)
						{
							bool flag109 = (char.IsWhiteSpace((char)num14) || num14 == 8203 || num14 == 45 || num14 == 173) && (!this.m_IsNonBreakingSpace || flag9) && num14 != 160 && num14 != 8199 && num14 != 8209 && num14 != 8239 && num14 != 8288;
							if (flag109)
							{
								this.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_CharacterCount, textInfo);
								this.m_IsCharacterWrappingEnabled = false;
								flag8 = false;
							}
							else
							{
								bool flag110 = ((num14 > 4352 && num14 < 4607) || (num14 > 11904 && num14 < 40959) || (num14 > 43360 && num14 < 43391) || (num14 > 44032 && num14 < 55295) || (num14 > 63744 && num14 < 64255) || (num14 > 65072 && num14 < 65103) || (num14 > 65280 && num14 < 65519)) && !this.m_IsNonBreakingSpace;
								if (flag110)
								{
									bool flag111 = flag8 || flag10 || (!textSettings.lineBreakingRules.leadingCharactersLookup.Contains((uint)num14) && this.m_CharacterCount < totalCharacterCount - 1 && !textSettings.lineBreakingRules.followingCharactersLookup.Contains((uint)textInfo.textElementInfo[this.m_CharacterCount + 1].character));
									if (flag111)
									{
										this.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_CharacterCount, textInfo);
										this.m_IsCharacterWrappingEnabled = false;
										flag8 = false;
									}
								}
								else
								{
									bool flag112 = flag8 || this.m_IsCharacterWrappingEnabled || flag10;
									if (flag112)
									{
										this.SaveWordWrappingState(ref this.m_SavedWordWrapState, num13, this.m_CharacterCount, textInfo);
									}
								}
							}
						}
						this.m_CharacterCount++;
						goto IL_3289;
					}
					float num42 = this.m_MaxFontSize - this.m_MinFontSize;
					bool flag113 = !this.m_IsCharacterWrappingEnabled && generationSettings.autoSize && num42 > 0.051f && this.m_FontSize < generationSettings.fontSizeMax;
					if (flag113)
					{
						this.m_MinFontSize = this.m_FontSize;
						this.m_FontSize += Mathf.Max((this.m_MaxFontSize - this.m_FontSize) / 2f, 0.05f);
						this.m_FontSize = (float)((int)(Mathf.Min(this.m_FontSize, generationSettings.fontSizeMax) * 20f + 0.5f)) / 20f;
						bool flag114 = this.m_LoopCountA > 20;
						if (!flag114)
						{
							this.GenerateTextMesh(generationSettings, textInfo);
						}
					}
					else
					{
						this.m_IsCharacterWrappingEnabled = false;
						bool flag115 = this.m_CharacterCount == 0;
						if (flag115)
						{
							TextGenerator.ClearMesh(true, textInfo);
						}
						else
						{
							int vertexCount = this.m_MaterialReferences[0].referenceCount * 4;
							textInfo.meshInfo[0].Clear(false);
							Vector3 a2 = Vector3.zero;
							Vector3[] rectTransformCorners = this.m_RectTransformCorners;
							TextAlignment textAlignment = generationSettings.textAlignment;
							TextAlignment textAlignment2 = textAlignment;
							if (textAlignment2 <= TextAlignment.BottomGeoAligned)
							{
								if (textAlignment2 <= TextAlignment.MiddleRight)
								{
									if (textAlignment2 <= TextAlignment.TopJustified)
									{
										if (textAlignment2 - TextAlignment.TopLeft > 1 && textAlignment2 != TextAlignment.TopRight && textAlignment2 != TextAlignment.TopJustified)
										{
											goto IL_3989;
										}
									}
									else if (textAlignment2 <= TextAlignment.TopGeoAligned)
									{
										if (textAlignment2 != TextAlignment.TopFlush && textAlignment2 != TextAlignment.TopGeoAligned)
										{
											goto IL_3989;
										}
									}
									else
									{
										if (textAlignment2 - TextAlignment.MiddleLeft > 1 && textAlignment2 != TextAlignment.MiddleRight)
										{
											goto IL_3989;
										}
										goto IL_36D7;
									}
									bool flag116 = generationSettings.overflowMode != TextOverflowMode.Page;
									if (flag116)
									{
										a2 = rectTransformCorners[1] + new Vector3(0f + margins.x, 0f - this.m_MaxAscender - margins.y, 0f);
									}
									else
									{
										a2 = rectTransformCorners[1] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[num6].ascender - margins.y, 0f);
									}
									goto IL_3989;
								}
								if (textAlignment2 <= TextAlignment.BottomCenter)
								{
									if (textAlignment2 <= TextAlignment.MiddleFlush)
									{
										if (textAlignment2 != TextAlignment.MiddleJustified && textAlignment2 != TextAlignment.MiddleFlush)
										{
											goto IL_3989;
										}
										goto IL_36D7;
									}
									else
									{
										if (textAlignment2 == TextAlignment.MiddleGeoAligned)
										{
											goto IL_36D7;
										}
										if (textAlignment2 - TextAlignment.BottomLeft > 1)
										{
											goto IL_3989;
										}
									}
								}
								else if (textAlignment2 <= TextAlignment.BottomJustified)
								{
									if (textAlignment2 != TextAlignment.BottomRight && textAlignment2 != TextAlignment.BottomJustified)
									{
										goto IL_3989;
									}
								}
								else if (textAlignment2 != TextAlignment.BottomFlush && textAlignment2 != TextAlignment.BottomGeoAligned)
								{
									goto IL_3989;
								}
								bool flag117 = generationSettings.overflowMode != TextOverflowMode.Page;
								if (flag117)
								{
									a2 = rectTransformCorners[0] + new Vector3(0f + margins.x, 0f - num11 + margins.w, 0f);
								}
								else
								{
									a2 = rectTransformCorners[0] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[num6].descender + margins.w, 0f);
								}
								goto IL_3989;
								IL_36D7:
								bool flag118 = generationSettings.overflowMode != TextOverflowMode.Page;
								if (flag118)
								{
									a2 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MaxAscender + margins.y + num11 - margins.w) / 2f, 0f);
								}
								else
								{
									a2 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (textInfo.pageInfo[num6].ascender + margins.y + textInfo.pageInfo[num6].descender - margins.w) / 2f, 0f);
								}
							}
							else
							{
								if (textAlignment2 <= TextAlignment.MidlineRight)
								{
									if (textAlignment2 <= TextAlignment.BaselineJustified)
									{
										if (textAlignment2 - TextAlignment.BaselineLeft > 1 && textAlignment2 != TextAlignment.BaselineRight && textAlignment2 != TextAlignment.BaselineJustified)
										{
											goto IL_3989;
										}
									}
									else if (textAlignment2 <= TextAlignment.BaselineGeoAligned)
									{
										if (textAlignment2 != TextAlignment.BaselineFlush && textAlignment2 != TextAlignment.BaselineGeoAligned)
										{
											goto IL_3989;
										}
									}
									else
									{
										if (textAlignment2 - TextAlignment.MidlineLeft > 1 && textAlignment2 != TextAlignment.MidlineRight)
										{
											goto IL_3989;
										}
										goto IL_38AC;
									}
									a2 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f, 0f);
									goto IL_3989;
								}
								if (textAlignment2 <= TextAlignment.CaplineCenter)
								{
									if (textAlignment2 <= TextAlignment.MidlineFlush)
									{
										if (textAlignment2 != TextAlignment.MidlineJustified && textAlignment2 != TextAlignment.MidlineFlush)
										{
											goto IL_3989;
										}
										goto IL_38AC;
									}
									else
									{
										if (textAlignment2 == TextAlignment.MidlineGeoAligned)
										{
											goto IL_38AC;
										}
										if (textAlignment2 - TextAlignment.CaplineLeft > 1)
										{
											goto IL_3989;
										}
									}
								}
								else if (textAlignment2 <= TextAlignment.CaplineJustified)
								{
									if (textAlignment2 != TextAlignment.CaplineRight && textAlignment2 != TextAlignment.CaplineJustified)
									{
										goto IL_3989;
									}
								}
								else if (textAlignment2 != TextAlignment.CaplineFlush && textAlignment2 != TextAlignment.CaplineGeoAligned)
								{
									goto IL_3989;
								}
								a2 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MaxCapHeight - margins.y - margins.w) / 2f, 0f);
								goto IL_3989;
								IL_38AC:
								a2 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_MeshExtents.max.y + margins.y + this.m_MeshExtents.min.y - margins.w) / 2f, 0f);
							}
							IL_3989:
							Vector3 vector7 = Vector3.zero;
							int num43 = 0;
							int lineCount = 0;
							int num44 = 0;
							bool flag119 = false;
							bool flag120 = false;
							int num45 = 0;
							Color32 color = Color.white;
							Color32 underlineColor = Color.white;
							Color32 highlightColor = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
							float num46 = 0f;
							float num47 = 0f;
							float num48 = 0f;
							float num49 = 32767f;
							int num50 = 0;
							float num51 = 0f;
							float num52 = 0f;
							float b4 = 0f;
							TextElementInfo[] textElementInfo = textInfo.textElementInfo;
							int i = 0;
							int num16;
							while (i < this.m_CharacterCount)
							{
								FontAsset fontAsset = textElementInfo[i].fontAsset;
								char character = textElementInfo[i].character;
								int lineNumber3 = textElementInfo[i].lineNumber;
								LineInfo lineInfo3 = textInfo.lineInfo[lineNumber3];
								lineCount = lineNumber3 + 1;
								TextAlignment alignment = lineInfo3.alignment;
								TextAlignment textAlignment3 = alignment;
								TextAlignment textAlignment4 = textAlignment3;
								if (textAlignment4 <= TextAlignment.BottomGeoAligned)
								{
									if (textAlignment4 <= TextAlignment.MiddleJustified)
									{
										if (textAlignment4 <= TextAlignment.TopFlush)
										{
											switch (textAlignment4)
											{
											case TextAlignment.TopLeft:
												goto IL_3D07;
											case TextAlignment.TopCenter:
												goto IL_3D61;
											case (TextAlignment)259:
												break;
											case TextAlignment.TopRight:
												goto IL_3DEF;
											default:
												if (textAlignment4 == TextAlignment.TopJustified || textAlignment4 == TextAlignment.TopFlush)
												{
													goto IL_3E55;
												}
												break;
											}
										}
										else
										{
											if (textAlignment4 == TextAlignment.TopGeoAligned)
											{
												goto IL_3D9A;
											}
											switch (textAlignment4)
											{
											case TextAlignment.MiddleLeft:
												goto IL_3D07;
											case TextAlignment.MiddleCenter:
												goto IL_3D61;
											case (TextAlignment)515:
												break;
											case TextAlignment.MiddleRight:
												goto IL_3DEF;
											default:
												if (textAlignment4 == TextAlignment.MiddleJustified)
												{
													goto IL_3E55;
												}
												break;
											}
										}
									}
									else if (textAlignment4 <= TextAlignment.BottomRight)
									{
										if (textAlignment4 == TextAlignment.MiddleFlush)
										{
											goto IL_3E55;
										}
										if (textAlignment4 == TextAlignment.MiddleGeoAligned)
										{
											goto IL_3D9A;
										}
										switch (textAlignment4)
										{
										case TextAlignment.BottomLeft:
											goto IL_3D07;
										case TextAlignment.BottomCenter:
											goto IL_3D61;
										case TextAlignment.BottomRight:
											goto IL_3DEF;
										}
									}
									else
									{
										if (textAlignment4 == TextAlignment.BottomJustified || textAlignment4 == TextAlignment.BottomFlush)
										{
											goto IL_3E55;
										}
										if (textAlignment4 == TextAlignment.BottomGeoAligned)
										{
											goto IL_3D9A;
										}
									}
								}
								else if (textAlignment4 <= TextAlignment.MidlineJustified)
								{
									if (textAlignment4 <= TextAlignment.BaselineFlush)
									{
										switch (textAlignment4)
										{
										case TextAlignment.BaselineLeft:
											goto IL_3D07;
										case TextAlignment.BaselineCenter:
											goto IL_3D61;
										case (TextAlignment)2051:
											break;
										case TextAlignment.BaselineRight:
											goto IL_3DEF;
										default:
											if (textAlignment4 == TextAlignment.BaselineJustified || textAlignment4 == TextAlignment.BaselineFlush)
											{
												goto IL_3E55;
											}
											break;
										}
									}
									else
									{
										if (textAlignment4 == TextAlignment.BaselineGeoAligned)
										{
											goto IL_3D9A;
										}
										switch (textAlignment4)
										{
										case TextAlignment.MidlineLeft:
											goto IL_3D07;
										case TextAlignment.MidlineCenter:
											goto IL_3D61;
										case (TextAlignment)4099:
											break;
										case TextAlignment.MidlineRight:
											goto IL_3DEF;
										default:
											if (textAlignment4 == TextAlignment.MidlineJustified)
											{
												goto IL_3E55;
											}
											break;
										}
									}
								}
								else if (textAlignment4 <= TextAlignment.CaplineRight)
								{
									if (textAlignment4 == TextAlignment.MidlineFlush)
									{
										goto IL_3E55;
									}
									if (textAlignment4 == TextAlignment.MidlineGeoAligned)
									{
										goto IL_3D9A;
									}
									switch (textAlignment4)
									{
									case TextAlignment.CaplineLeft:
										goto IL_3D07;
									case TextAlignment.CaplineCenter:
										goto IL_3D61;
									case TextAlignment.CaplineRight:
										goto IL_3DEF;
									}
								}
								else
								{
									if (textAlignment4 == TextAlignment.CaplineJustified || textAlignment4 == TextAlignment.CaplineFlush)
									{
										goto IL_3E55;
									}
									if (textAlignment4 == TextAlignment.CaplineGeoAligned)
									{
										goto IL_3D9A;
									}
								}
								IL_41E9:
								Vector3 vector8 = a2 + vector7;
								bool isVisible3 = textElementInfo[i].isVisible;
								bool flag121 = isVisible3;
								Vector3 ptr;
								if (flag121)
								{
									TextElementType elementType = textElementInfo[i].elementType;
									TextElementType textElementType = elementType;
									TextElementType textElementType2 = textElementType;
									if (textElementType2 != TextElementType.Character)
									{
										if (textElementType2 != TextElementType.Sprite)
										{
										}
									}
									else
									{
										Extents lineExtents = lineInfo3.lineExtents;
										float num53 = generationSettings.uvLineOffset * (float)lineNumber3 % 1f;
										switch (generationSettings.horizontalMapping)
										{
										case TextureMapping.Character:
											textElementInfo[i].vertexBottomLeft.uv2.x = 0f;
											textElementInfo[i].vertexTopLeft.uv2.x = 0f;
											textElementInfo[i].vertexTopRight.uv2.x = 1f;
											textElementInfo[i].vertexBottomRight.uv2.x = 1f;
											break;
										case TextureMapping.Line:
										{
											bool flag122 = generationSettings.textAlignment != TextAlignment.MiddleJustified;
											if (flag122)
											{
												textElementInfo[i].vertexBottomLeft.uv2.x = (textElementInfo[i].vertexBottomLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
												textElementInfo[i].vertexTopLeft.uv2.x = (textElementInfo[i].vertexTopLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
												textElementInfo[i].vertexTopRight.uv2.x = (textElementInfo[i].vertexTopRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
												textElementInfo[i].vertexBottomRight.uv2.x = (textElementInfo[i].vertexBottomRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
											}
											else
											{
												textElementInfo[i].vertexBottomLeft.uv2.x = (textElementInfo[i].vertexBottomLeft.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
												textElementInfo[i].vertexTopLeft.uv2.x = (textElementInfo[i].vertexTopLeft.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
												textElementInfo[i].vertexTopRight.uv2.x = (textElementInfo[i].vertexTopRight.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
												textElementInfo[i].vertexBottomRight.uv2.x = (textElementInfo[i].vertexBottomRight.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
											}
											break;
										}
										case TextureMapping.Paragraph:
											textElementInfo[i].vertexBottomLeft.uv2.x = (textElementInfo[i].vertexBottomLeft.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
											textElementInfo[i].vertexTopLeft.uv2.x = (textElementInfo[i].vertexTopLeft.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
											textElementInfo[i].vertexTopRight.uv2.x = (textElementInfo[i].vertexTopRight.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
											textElementInfo[i].vertexBottomRight.uv2.x = (textElementInfo[i].vertexBottomRight.position.x + vector7.x - this.m_MeshExtents.min.x) / (this.m_MeshExtents.max.x - this.m_MeshExtents.min.x) + num53;
											break;
										case TextureMapping.MatchAspect:
										{
											switch (generationSettings.verticalMapping)
											{
											case TextureMapping.Character:
												textElementInfo[i].vertexBottomLeft.uv2.y = 0f;
												textElementInfo[i].vertexTopLeft.uv2.y = 1f;
												textElementInfo[i].vertexTopRight.uv2.y = 0f;
												textElementInfo[i].vertexBottomRight.uv2.y = 1f;
												break;
											case TextureMapping.Line:
												textElementInfo[i].vertexBottomLeft.uv2.y = (textElementInfo[i].vertexBottomLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num53;
												textElementInfo[i].vertexTopLeft.uv2.y = (textElementInfo[i].vertexTopLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num53;
												textElementInfo[i].vertexTopRight.uv2.y = textElementInfo[i].vertexBottomLeft.uv2.y;
												textElementInfo[i].vertexBottomRight.uv2.y = textElementInfo[i].vertexTopLeft.uv2.y;
												break;
											case TextureMapping.Paragraph:
												textElementInfo[i].vertexBottomLeft.uv2.y = (textElementInfo[i].vertexBottomLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y) + num53;
												textElementInfo[i].vertexTopLeft.uv2.y = (textElementInfo[i].vertexTopLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y) + num53;
												textElementInfo[i].vertexTopRight.uv2.y = textElementInfo[i].vertexBottomLeft.uv2.y;
												textElementInfo[i].vertexBottomRight.uv2.y = textElementInfo[i].vertexTopLeft.uv2.y;
												break;
											case TextureMapping.MatchAspect:
												Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
												break;
											}
											float num54 = (1f - (textElementInfo[i].vertexBottomLeft.uv2.y + textElementInfo[i].vertexTopLeft.uv2.y) * textElementInfo[i].aspectRatio) / 2f;
											textElementInfo[i].vertexBottomLeft.uv2.x = textElementInfo[i].vertexBottomLeft.uv2.y * textElementInfo[i].aspectRatio + num54 + num53;
											textElementInfo[i].vertexTopLeft.uv2.x = textElementInfo[i].vertexBottomLeft.uv2.x;
											textElementInfo[i].vertexTopRight.uv2.x = textElementInfo[i].vertexTopLeft.uv2.y * textElementInfo[i].aspectRatio + num54 + num53;
											textElementInfo[i].vertexBottomRight.uv2.x = textElementInfo[i].vertexTopRight.uv2.x;
											break;
										}
										}
										switch (generationSettings.verticalMapping)
										{
										case TextureMapping.Character:
											textElementInfo[i].vertexBottomLeft.uv2.y = 0f;
											textElementInfo[i].vertexTopLeft.uv2.y = 1f;
											textElementInfo[i].vertexTopRight.uv2.y = 1f;
											textElementInfo[i].vertexBottomRight.uv2.y = 0f;
											break;
										case TextureMapping.Line:
											textElementInfo[i].vertexBottomLeft.uv2.y = (textElementInfo[i].vertexBottomLeft.position.y - lineInfo3.descender) / (lineInfo3.ascender - lineInfo3.descender);
											textElementInfo[i].vertexTopLeft.uv2.y = (textElementInfo[i].vertexTopLeft.position.y - lineInfo3.descender) / (lineInfo3.ascender - lineInfo3.descender);
											textElementInfo[i].vertexTopRight.uv2.y = textElementInfo[i].vertexTopLeft.uv2.y;
											textElementInfo[i].vertexBottomRight.uv2.y = textElementInfo[i].vertexBottomLeft.uv2.y;
											break;
										case TextureMapping.Paragraph:
											textElementInfo[i].vertexBottomLeft.uv2.y = (textElementInfo[i].vertexBottomLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y);
											textElementInfo[i].vertexTopLeft.uv2.y = (textElementInfo[i].vertexTopLeft.position.y - this.m_MeshExtents.min.y) / (this.m_MeshExtents.max.y - this.m_MeshExtents.min.y);
											textElementInfo[i].vertexTopRight.uv2.y = textElementInfo[i].vertexTopLeft.uv2.y;
											textElementInfo[i].vertexBottomRight.uv2.y = textElementInfo[i].vertexBottomLeft.uv2.y;
											break;
										case TextureMapping.MatchAspect:
										{
											float num55 = (1f - (textElementInfo[i].vertexBottomLeft.uv2.x + textElementInfo[i].vertexTopRight.uv2.x) / textElementInfo[i].aspectRatio) / 2f;
											textElementInfo[i].vertexBottomLeft.uv2.y = num55 + textElementInfo[i].vertexBottomLeft.uv2.x / textElementInfo[i].aspectRatio;
											textElementInfo[i].vertexTopLeft.uv2.y = num55 + textElementInfo[i].vertexTopRight.uv2.x / textElementInfo[i].aspectRatio;
											textElementInfo[i].vertexBottomRight.uv2.y = textElementInfo[i].vertexBottomLeft.uv2.y;
											textElementInfo[i].vertexTopRight.uv2.y = textElementInfo[i].vertexTopLeft.uv2.y;
											break;
										}
										}
										num46 = textElementInfo[i].scale * (1f - this.m_CharWidthAdjDelta) * 1f;
										bool flag123 = !textElementInfo[i].isUsingAlternateTypeface && (textElementInfo[i].style & FontStyles.Bold) == FontStyles.Bold;
										if (flag123)
										{
											num46 *= -1f;
										}
										textElementInfo[i].vertexBottomLeft.uv2.x = 1f;
										textElementInfo[i].vertexBottomLeft.uv2.y = num46;
										textElementInfo[i].vertexTopLeft.uv2.x = 1f;
										textElementInfo[i].vertexTopLeft.uv2.y = num46;
										textElementInfo[i].vertexTopRight.uv2.x = 1f;
										textElementInfo[i].vertexTopRight.uv2.y = num46;
										textElementInfo[i].vertexBottomRight.uv2.x = 1f;
										textElementInfo[i].vertexBottomRight.uv2.y = num46;
									}
									bool flag124 = i < generationSettings.maxVisibleCharacters && num43 < generationSettings.maxVisibleWords && lineNumber3 < generationSettings.maxVisibleLines && generationSettings.overflowMode != TextOverflowMode.Page;
									if (flag124)
									{
										ptr = ref textElementInfo[i].vertexBottomLeft.position;
										ptr += vector8;
										ptr = ref textElementInfo[i].vertexTopLeft.position;
										ptr += vector8;
										ptr = ref textElementInfo[i].vertexTopRight.position;
										ptr += vector8;
										ptr = ref textElementInfo[i].vertexBottomRight.position;
										ptr += vector8;
									}
									else
									{
										bool flag125 = i < generationSettings.maxVisibleCharacters && num43 < generationSettings.maxVisibleWords && lineNumber3 < generationSettings.maxVisibleLines && generationSettings.overflowMode == TextOverflowMode.Page && textElementInfo[i].pageNumber == num6;
										if (flag125)
										{
											ptr = ref textElementInfo[i].vertexBottomLeft.position;
											ptr += vector8;
											ptr = ref textElementInfo[i].vertexTopLeft.position;
											ptr += vector8;
											ptr = ref textElementInfo[i].vertexTopRight.position;
											ptr += vector8;
											ptr = ref textElementInfo[i].vertexBottomRight.position;
											ptr += vector8;
										}
										else
										{
											textElementInfo[i].vertexBottomLeft.position = Vector3.zero;
											textElementInfo[i].vertexTopLeft.position = Vector3.zero;
											textElementInfo[i].vertexTopRight.position = Vector3.zero;
											textElementInfo[i].vertexBottomRight.position = Vector3.zero;
											textElementInfo[i].isVisible = false;
										}
									}
									bool flag126 = elementType == TextElementType.Character;
									if (flag126)
									{
										TextGeneratorUtilities.FillCharacterVertexBuffers(i, generationSettings, textInfo);
									}
									else
									{
										bool flag127 = elementType == TextElementType.Sprite;
										if (flag127)
										{
											TextGeneratorUtilities.FillSpriteVertexBuffers(i, generationSettings, textInfo);
										}
									}
								}
								ptr = ref textInfo.textElementInfo[i].bottomLeft;
								ptr += vector8;
								ptr = ref textInfo.textElementInfo[i].topLeft;
								ptr += vector8;
								ptr = ref textInfo.textElementInfo[i].topRight;
								ptr += vector8;
								ptr = ref textInfo.textElementInfo[i].bottomRight;
								ptr += vector8;
								TextElementInfo[] textElementInfo2 = textInfo.textElementInfo;
								int num56 = i;
								textElementInfo2[num56].origin = textElementInfo2[num56].origin + vector8.x;
								TextElementInfo[] textElementInfo3 = textInfo.textElementInfo;
								int num57 = i;
								textElementInfo3[num57].xAdvance = textElementInfo3[num57].xAdvance + vector8.x;
								TextElementInfo[] textElementInfo4 = textInfo.textElementInfo;
								int num58 = i;
								textElementInfo4[num58].ascender = textElementInfo4[num58].ascender + vector8.y;
								TextElementInfo[] textElementInfo5 = textInfo.textElementInfo;
								int num59 = i;
								textElementInfo5[num59].descender = textElementInfo5[num59].descender + vector8.y;
								TextElementInfo[] textElementInfo6 = textInfo.textElementInfo;
								int num60 = i;
								textElementInfo6[num60].baseLine = textElementInfo6[num60].baseLine + vector8.y;
								bool flag128 = lineNumber3 != num44 || i == this.m_CharacterCount - 1;
								if (flag128)
								{
									bool flag129 = lineNumber3 != num44;
									if (flag129)
									{
										LineInfo[] lineInfo4 = textInfo.lineInfo;
										int num61 = num44;
										lineInfo4[num61].baseline = lineInfo4[num61].baseline + vector8.y;
										LineInfo[] lineInfo5 = textInfo.lineInfo;
										int num62 = num44;
										lineInfo5[num62].ascender = lineInfo5[num62].ascender + vector8.y;
										LineInfo[] lineInfo6 = textInfo.lineInfo;
										int num63 = num44;
										lineInfo6[num63].descender = lineInfo6[num63].descender + vector8.y;
										textInfo.lineInfo[num44].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[num44].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[num44].descender);
										textInfo.lineInfo[num44].lineExtents.max = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[num44].lastVisibleCharacterIndex].topRight.x, textInfo.lineInfo[num44].ascender);
									}
									bool flag130 = i == this.m_CharacterCount - 1;
									if (flag130)
									{
										LineInfo[] lineInfo7 = textInfo.lineInfo;
										int num64 = lineNumber3;
										lineInfo7[num64].baseline = lineInfo7[num64].baseline + vector8.y;
										LineInfo[] lineInfo8 = textInfo.lineInfo;
										int num65 = lineNumber3;
										lineInfo8[num65].ascender = lineInfo8[num65].ascender + vector8.y;
										LineInfo[] lineInfo9 = textInfo.lineInfo;
										int num66 = lineNumber3;
										lineInfo9[num66].descender = lineInfo9[num66].descender + vector8.y;
										textInfo.lineInfo[lineNumber3].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[lineNumber3].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[lineNumber3].descender);
										textInfo.lineInfo[lineNumber3].lineExtents.max = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[lineNumber3].lastVisibleCharacterIndex].topRight.x, textInfo.lineInfo[lineNumber3].ascender);
									}
								}
								bool flag131 = char.IsLetterOrDigit(character) || character == '-' || character == '­' || character == '‐' || character == '‑';
								if (flag131)
								{
									bool flag132 = !flag120;
									if (flag132)
									{
										flag120 = true;
										num45 = i;
									}
									bool flag133 = flag120 && i == this.m_CharacterCount - 1;
									if (flag133)
									{
										int num67 = textInfo.wordInfo.Length;
										int wordCount = textInfo.wordCount;
										bool flag134 = textInfo.wordCount + 1 > num67;
										if (flag134)
										{
											TextInfo.Resize<WordInfo>(ref textInfo.wordInfo, num67 + 1);
										}
										int num68 = i;
										textInfo.wordInfo[wordCount].firstCharacterIndex = num45;
										textInfo.wordInfo[wordCount].lastCharacterIndex = num68;
										textInfo.wordInfo[wordCount].characterCount = num68 - num45 + 1;
										num43++;
										textInfo.wordCount++;
										LineInfo[] lineInfo10 = textInfo.lineInfo;
										int num69 = lineNumber3;
										lineInfo10[num69].wordCount = lineInfo10[num69].wordCount + 1;
									}
								}
								else
								{
									bool flag135 = flag120 || (i == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '​' || i == this.m_CharacterCount - 1));
									if (flag135)
									{
										bool flag136 = i <= 0 || i >= textElementInfo.Length - 1 || i >= this.m_CharacterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(textElementInfo[i - 1].character) || !char.IsLetterOrDigit(textElementInfo[i + 1].character);
										if (flag136)
										{
											int num68 = (i == this.m_CharacterCount - 1 && char.IsLetterOrDigit(character)) ? i : (i - 1);
											flag120 = false;
											int num70 = textInfo.wordInfo.Length;
											int wordCount2 = textInfo.wordCount;
											bool flag137 = textInfo.wordCount + 1 > num70;
											if (flag137)
											{
												TextInfo.Resize<WordInfo>(ref textInfo.wordInfo, num70 + 1);
											}
											textInfo.wordInfo[wordCount2].firstCharacterIndex = num45;
											textInfo.wordInfo[wordCount2].lastCharacterIndex = num68;
											textInfo.wordInfo[wordCount2].characterCount = num68 - num45 + 1;
											num43++;
											textInfo.wordCount++;
											ref int ptr2 = ref textInfo.lineInfo[lineNumber3].wordCount;
											ptr2++;
										}
									}
								}
								bool flag138 = (textInfo.textElementInfo[i].style & FontStyles.Underline) == FontStyles.Underline;
								bool flag139 = flag138;
								if (flag139)
								{
									bool flag140 = true;
									int pageNumber = textInfo.textElementInfo[i].pageNumber;
									bool flag141 = i > generationSettings.maxVisibleCharacters || lineNumber3 > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && pageNumber + 1 != generationSettings.pageToDisplay);
									if (flag141)
									{
										flag140 = false;
									}
									bool flag142 = !char.IsWhiteSpace(character) && character != '​';
									if (flag142)
									{
										num48 = Mathf.Max(num48, textInfo.textElementInfo[i].scale);
										num49 = Mathf.Min((pageNumber == num50) ? num49 : 32767f, textInfo.textElementInfo[i].baseLine + generationSettings.fontAsset.faceInfo.underlineOffset * num48);
										num50 = pageNumber;
									}
									bool flag143 = !flag3 && flag140 && i <= lineInfo3.lastVisibleCharacterIndex && character != '\n' && character != '\r';
									if (flag143)
									{
										bool flag144 = i != lineInfo3.lastVisibleCharacterIndex || !char.IsSeparator(character);
										if (flag144)
										{
											flag3 = true;
											num47 = textInfo.textElementInfo[i].scale;
											bool flag145 = num48 == 0f;
											if (flag145)
											{
												num48 = num47;
											}
											zero = new Vector3(textInfo.textElementInfo[i].bottomLeft.x, num49, 0f);
											color = textInfo.textElementInfo[i].underlineColor;
										}
									}
									bool flag146 = flag3 && this.m_CharacterCount == 1;
									if (flag146)
									{
										flag3 = false;
										Vector3 end = new Vector3(textInfo.textElementInfo[i].topRight.x, num49, 0f);
										float scale = textInfo.textElementInfo[i].scale;
										this.DrawUnderlineMesh(zero, end, ref vertexCount, num47, scale, num48, num46, color, generationSettings, textInfo);
										num48 = 0f;
										num49 = 32767f;
									}
									else
									{
										bool flag147 = flag3 && (i == lineInfo3.lastCharacterIndex || i >= lineInfo3.lastVisibleCharacterIndex);
										if (flag147)
										{
											bool flag148 = char.IsWhiteSpace(character) || character == '​';
											Vector3 end;
											float scale;
											if (flag148)
											{
												int lastVisibleCharacterIndex = lineInfo3.lastVisibleCharacterIndex;
												end = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex].topRight.x, num49, 0f);
												scale = textInfo.textElementInfo[lastVisibleCharacterIndex].scale;
											}
											else
											{
												end = new Vector3(textInfo.textElementInfo[i].topRight.x, num49, 0f);
												scale = textInfo.textElementInfo[i].scale;
											}
											flag3 = false;
											this.DrawUnderlineMesh(zero, end, ref vertexCount, num47, scale, num48, num46, color, generationSettings, textInfo);
											num48 = 0f;
											num49 = 32767f;
										}
										else
										{
											bool flag149 = flag3 && !flag140;
											if (flag149)
											{
												flag3 = false;
												Vector3 end = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, num49, 0f);
												float scale = textInfo.textElementInfo[i - 1].scale;
												this.DrawUnderlineMesh(zero, end, ref vertexCount, num47, scale, num48, num46, color, generationSettings, textInfo);
												num48 = 0f;
												num49 = 32767f;
											}
											else
											{
												bool flag150 = flag3 && i < this.m_CharacterCount - 1 && !ColorUtilities.CompareColors(color, textInfo.textElementInfo[i + 1].underlineColor);
												if (flag150)
												{
													flag3 = false;
													Vector3 end = new Vector3(textInfo.textElementInfo[i].topRight.x, num49, 0f);
													float scale = textInfo.textElementInfo[i].scale;
													this.DrawUnderlineMesh(zero, end, ref vertexCount, num47, scale, num48, num46, color, generationSettings, textInfo);
													num48 = 0f;
													num49 = 32767f;
												}
											}
										}
									}
								}
								else
								{
									bool flag151 = flag3;
									if (flag151)
									{
										flag3 = false;
										Vector3 end = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, num49, 0f);
										float scale = textInfo.textElementInfo[i - 1].scale;
										this.DrawUnderlineMesh(zero, end, ref vertexCount, num47, scale, num48, num46, color, generationSettings, textInfo);
										num48 = 0f;
										num49 = 32767f;
									}
								}
								bool flag152 = (textInfo.textElementInfo[i].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
								float strikethroughOffset = fontAsset.faceInfo.strikethroughOffset;
								bool flag153 = flag152;
								if (flag153)
								{
									bool flag154 = i <= generationSettings.maxVisibleCharacters && lineNumber3 <= generationSettings.maxVisibleLines && (generationSettings.overflowMode != TextOverflowMode.Page || textInfo.textElementInfo[i].pageNumber + 1 == generationSettings.pageToDisplay);
									bool flag155 = !flag4 && flag154 && i <= lineInfo3.lastVisibleCharacterIndex && character != '\n' && character != '\r';
									if (flag155)
									{
										bool flag156 = i != lineInfo3.lastVisibleCharacterIndex || !char.IsSeparator(character);
										if (flag156)
										{
											flag4 = true;
											num51 = textInfo.textElementInfo[i].pointSize;
											num52 = textInfo.textElementInfo[i].scale;
											zero2 = new Vector3(textInfo.textElementInfo[i].bottomLeft.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * num52, 0f);
											underlineColor = textInfo.textElementInfo[i].strikethroughColor;
											b4 = textInfo.textElementInfo[i].baseLine;
										}
									}
									bool flag157 = flag4 && this.m_CharacterCount == 1;
									if (flag157)
									{
										flag4 = false;
										Vector3 end2 = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * num52, 0f);
										this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
									}
									else
									{
										bool flag158 = flag4 && i == lineInfo3.lastCharacterIndex;
										if (flag158)
										{
											bool flag159 = char.IsWhiteSpace(character) || character == '​';
											Vector3 end2;
											if (flag159)
											{
												int lastVisibleCharacterIndex2 = lineInfo3.lastVisibleCharacterIndex;
												end2 = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex2].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num52, 0f);
											}
											else
											{
												end2 = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * num52, 0f);
											}
											flag4 = false;
											this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
										}
										else
										{
											bool flag160 = flag4 && i < this.m_CharacterCount && (textInfo.textElementInfo[i + 1].pointSize != num51 || !TextGeneratorUtilities.Approximately(textInfo.textElementInfo[i + 1].baseLine + vector8.y, b4));
											if (flag160)
											{
												flag4 = false;
												int lastVisibleCharacterIndex3 = lineInfo3.lastVisibleCharacterIndex;
												bool flag161 = i > lastVisibleCharacterIndex3;
												Vector3 end2;
												if (flag161)
												{
													end2 = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex3].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num52, 0f);
												}
												else
												{
													end2 = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * num52, 0f);
												}
												this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
											}
											else
											{
												bool flag162 = flag4 && i < this.m_CharacterCount && fontAsset.GetInstanceID() != textElementInfo[i + 1].fontAsset.GetInstanceID();
												if (flag162)
												{
													flag4 = false;
													Vector3 end2 = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].baseLine + strikethroughOffset * num52, 0f);
													this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
												}
												else
												{
													bool flag163 = flag4 && !flag154;
													if (flag163)
													{
														flag4 = false;
														Vector3 end2 = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, textInfo.textElementInfo[i - 1].baseLine + strikethroughOffset * num52, 0f);
														this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
													}
												}
											}
										}
									}
								}
								else
								{
									bool flag164 = flag4;
									if (flag164)
									{
										flag4 = false;
										Vector3 end2 = new Vector3(textInfo.textElementInfo[i - 1].topRight.x, textInfo.textElementInfo[i - 1].baseLine + strikethroughOffset * num52, 0f);
										this.DrawUnderlineMesh(zero2, end2, ref vertexCount, num52, num52, num52, num46, underlineColor, generationSettings, textInfo);
									}
								}
								bool flag165 = (textInfo.textElementInfo[i].style & FontStyles.Highlight) == FontStyles.Highlight;
								bool flag166 = flag165;
								if (flag166)
								{
									bool flag167 = true;
									int pageNumber2 = textInfo.textElementInfo[i].pageNumber;
									bool flag168 = i > generationSettings.maxVisibleCharacters || lineNumber3 > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && pageNumber2 + 1 != generationSettings.pageToDisplay);
									if (flag168)
									{
										flag167 = false;
									}
									bool flag169 = !flag5 && flag167 && i <= lineInfo3.lastVisibleCharacterIndex && character != '\n' && character != '\r';
									if (flag169)
									{
										bool flag170 = i != lineInfo3.lastVisibleCharacterIndex || !char.IsSeparator(character);
										if (flag170)
										{
											flag5 = true;
											vector = TextGeneratorUtilities.largePositiveVector2;
											vector2 = TextGeneratorUtilities.largeNegativeVector2;
											highlightColor = textInfo.textElementInfo[i].highlightColor;
										}
									}
									bool flag171 = flag5;
									if (flag171)
									{
										Color32 highlightColor2 = textInfo.textElementInfo[i].highlightColor;
										bool flag172 = false;
										bool flag173 = !ColorUtilities.CompareColors(highlightColor, highlightColor2);
										if (flag173)
										{
											vector2.x = (vector2.x + textInfo.textElementInfo[i].bottomLeft.x) / 2f;
											vector.y = Mathf.Min(vector.y, textInfo.textElementInfo[i].descender);
											vector2.y = Mathf.Max(vector2.y, textInfo.textElementInfo[i].ascender);
											this.DrawTextHighlight(vector, vector2, ref vertexCount, highlightColor, generationSettings, textInfo);
											flag5 = true;
											vector = vector2;
											vector2 = new Vector3(textInfo.textElementInfo[i].topRight.x, textInfo.textElementInfo[i].descender, 0f);
											highlightColor = textInfo.textElementInfo[i].highlightColor;
											flag172 = true;
										}
										bool flag174 = !flag172;
										if (flag174)
										{
											vector.x = Mathf.Min(vector.x, textInfo.textElementInfo[i].bottomLeft.x);
											vector.y = Mathf.Min(vector.y, textInfo.textElementInfo[i].descender);
											vector2.x = Mathf.Max(vector2.x, textInfo.textElementInfo[i].topRight.x);
											vector2.y = Mathf.Max(vector2.y, textInfo.textElementInfo[i].ascender);
										}
									}
									bool flag175 = flag5 && this.m_CharacterCount == 1;
									if (flag175)
									{
										flag5 = false;
										this.DrawTextHighlight(vector, vector2, ref vertexCount, highlightColor, generationSettings, textInfo);
									}
									else
									{
										bool flag176 = flag5 && (i == lineInfo3.lastCharacterIndex || i >= lineInfo3.lastVisibleCharacterIndex);
										if (flag176)
										{
											flag5 = false;
											this.DrawTextHighlight(vector, vector2, ref vertexCount, highlightColor, generationSettings, textInfo);
										}
										else
										{
											bool flag177 = flag5 && !flag167;
											if (flag177)
											{
												flag5 = false;
												this.DrawTextHighlight(vector, vector2, ref vertexCount, highlightColor, generationSettings, textInfo);
											}
										}
									}
								}
								else
								{
									bool flag178 = flag5;
									if (flag178)
									{
										flag5 = false;
										this.DrawTextHighlight(vector, vector2, ref vertexCount, highlightColor, generationSettings, textInfo);
									}
								}
								num44 = lineNumber3;
								num16 = i;
								i = num16 + 1;
								continue;
								IL_3D07:
								bool flag179 = !generationSettings.isRightToLeft;
								if (flag179)
								{
									vector7 = new Vector3(0f + lineInfo3.marginLeft, 0f, 0f);
								}
								else
								{
									vector7 = new Vector3(0f - lineInfo3.maxAdvance, 0f, 0f);
								}
								goto IL_41E9;
								IL_3D61:
								vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width / 2f - lineInfo3.maxAdvance / 2f, 0f, 0f);
								goto IL_41E9;
								IL_3D9A:
								vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width / 2f - (lineInfo3.lineExtents.min.x + lineInfo3.lineExtents.max.x) / 2f, 0f, 0f);
								goto IL_41E9;
								IL_3DEF:
								bool flag180 = !generationSettings.isRightToLeft;
								if (flag180)
								{
									vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width - lineInfo3.maxAdvance, 0f, 0f);
								}
								else
								{
									vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width, 0f, 0f);
								}
								goto IL_41E9;
								IL_3E55:
								bool flag181 = character == '­' || character == '​' || character == '⁠';
								if (flag181)
								{
									goto IL_41E9;
								}
								char character2 = textElementInfo[lineInfo3.lastCharacterIndex].character;
								bool flag182 = (alignment & (TextAlignment)16) == (TextAlignment)16;
								bool flag183 = (!char.IsControl(character2) && lineNumber3 < this.m_LineNumber) || flag182 || lineInfo3.maxAdvance > lineInfo3.width;
								if (flag183)
								{
									bool flag184 = lineNumber3 != num44 || i == 0 || i == generationSettings.firstVisibleCharacter;
									if (flag184)
									{
										bool flag185 = !generationSettings.isRightToLeft;
										if (flag185)
										{
											vector7 = new Vector3(lineInfo3.marginLeft, 0f, 0f);
										}
										else
										{
											vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width, 0f, 0f);
										}
										bool flag186 = char.IsSeparator(character);
										flag119 = flag186;
									}
									else
									{
										float num71 = (!generationSettings.isRightToLeft) ? (lineInfo3.width - lineInfo3.maxAdvance) : (lineInfo3.width + lineInfo3.maxAdvance);
										int num72 = lineInfo3.visibleCharacterCount - 1 + lineInfo3.controlCharacterCount;
										int num73 = (textElementInfo[lineInfo3.lastCharacterIndex].isVisible ? lineInfo3.spaceCount : (lineInfo3.spaceCount - 1)) - lineInfo3.controlCharacterCount;
										bool flag187 = flag119;
										if (flag187)
										{
											num73--;
											num72++;
										}
										float num74 = (num73 > 0) ? generationSettings.wordWrappingRatio : 1f;
										bool flag188 = num73 < 1;
										if (flag188)
										{
											num73 = 1;
										}
										bool flag189 = character != '\u00a0' && (character == '\t' || char.IsSeparator(character));
										if (flag189)
										{
											bool flag190 = !generationSettings.isRightToLeft;
											if (flag190)
											{
												vector7 += new Vector3(num71 * (1f - num74) / (float)num73, 0f, 0f);
											}
											else
											{
												vector7 -= new Vector3(num71 * (1f - num74) / (float)num73, 0f, 0f);
											}
										}
										else
										{
											bool flag191 = !generationSettings.isRightToLeft;
											if (flag191)
											{
												vector7 += new Vector3(num71 * num74 / (float)num72, 0f, 0f);
											}
											else
											{
												vector7 -= new Vector3(num71 * num74 / (float)num72, 0f, 0f);
											}
										}
									}
								}
								else
								{
									bool flag192 = !generationSettings.isRightToLeft;
									if (flag192)
									{
										vector7 = new Vector3(lineInfo3.marginLeft, 0f, 0f);
									}
									else
									{
										vector7 = new Vector3(lineInfo3.marginLeft + lineInfo3.width, 0f, 0f);
									}
								}
								goto IL_41E9;
							}
							textInfo.characterCount = this.m_CharacterCount;
							textInfo.spriteCount = this.m_SpriteCount;
							textInfo.lineCount = lineCount;
							textInfo.wordCount = ((num43 != 0 && this.m_CharacterCount > 0) ? num43 : 1);
							textInfo.pageCount = this.m_PageNumber + 1;
							textInfo.meshInfo[this.m_Underline.materialIndex].vertexCount = vertexCount;
							bool flag193 = generationSettings.geometrySortingOrder > VertexSortingOrder.Normal;
							if (flag193)
							{
								textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
							}
							for (int j = 1; j < textInfo.materialCount; j = num16 + 1)
							{
								textInfo.meshInfo[j].ClearUnusedVertices();
								bool flag194 = generationSettings.geometrySortingOrder > VertexSortingOrder.Normal;
								if (flag194)
								{
									textInfo.meshInfo[j].SortGeometry(VertexSortingOrder.Reverse);
								}
								num16 = j;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		private void SaveWordWrappingState(ref WordWrapState state, int index, int count, TextInfo textInfo)
		{
			state.currentFontAsset = this.m_CurrentFontAsset;
			state.currentSpriteAsset = this.m_CurrentSpriteAsset;
			state.currentMaterial = this.m_CurrentMaterial;
			state.currentMaterialIndex = this.m_CurrentMaterialIndex;
			state.previousWordBreak = index;
			state.totalCharacterCount = count;
			state.visibleCharacterCount = this.m_LineVisibleCharacterCount;
			state.visibleLinkCount = textInfo.linkCount;
			state.firstCharacterIndex = this.m_FirstCharacterOfLine;
			state.firstVisibleCharacterIndex = this.m_FirstVisibleCharacterOfLine;
			state.lastVisibleCharIndex = this.m_LastVisibleCharacterOfLine;
			state.fontStyle = this.m_FontStyleInternal;
			state.fontScale = this.m_FontScale;
			state.fontScaleMultiplier = this.m_FontScaleMultiplier;
			state.currentFontSize = this.m_CurrentFontSize;
			state.xAdvance = this.m_XAdvance;
			state.maxCapHeight = this.m_MaxCapHeight;
			state.maxAscender = this.m_MaxAscender;
			state.maxDescender = this.m_MaxDescender;
			state.maxLineAscender = this.m_MaxLineAscender;
			state.maxLineDescender = this.m_MaxLineDescender;
			state.previousLineAscender = this.m_StartOfLineAscender;
			state.preferredWidth = this.m_PreferredWidth;
			state.preferredHeight = this.m_PreferredHeight;
			state.meshExtents = this.m_MeshExtents;
			state.lineNumber = this.m_LineNumber;
			state.lineOffset = this.m_LineOffset;
			state.baselineOffset = this.m_BaselineOffset;
			state.vertexColor = this.m_HtmlColor;
			state.underlineColor = this.m_UnderlineColor;
			state.strikethroughColor = this.m_StrikethroughColor;
			state.highlightColor = this.m_HighlightColor;
			state.isNonBreakingSpace = this.m_IsNonBreakingSpace;
			state.tagNoParsing = this.m_TagNoParsing;
			state.basicStyleStack = this.m_FontStyleStack;
			state.colorStack = this.m_ColorStack;
			state.underlineColorStack = this.m_UnderlineColorStack;
			state.strikethroughColorStack = this.m_StrikethroughColorStack;
			state.highlightColorStack = this.m_HighlightColorStack;
			state.colorGradientStack = this.m_ColorGradientStack;
			state.sizeStack = this.m_SizeStack;
			state.indentStack = this.m_IndentStack;
			state.fontWeightStack = this.m_FontWeightStack;
			state.styleStack = this.m_StyleStack;
			state.baselineStack = this.m_BaselineOffsetStack;
			state.actionStack = this.m_ActionStack;
			state.materialReferenceStack = this.m_MaterialReferenceStack;
			state.lineJustificationStack = this.m_LineJustificationStack;
			state.spriteAnimationId = this.m_SpriteAnimationId;
			bool flag = this.m_LineNumber < textInfo.lineInfo.Length;
			if (flag)
			{
				state.lineInfo = textInfo.lineInfo[this.m_LineNumber];
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000FE44 File Offset: 0x0000E044
		protected int RestoreWordWrappingState(ref WordWrapState state, TextInfo textInfo)
		{
			int previousWordBreak = state.previousWordBreak;
			this.m_CurrentFontAsset = state.currentFontAsset;
			this.m_CurrentSpriteAsset = state.currentSpriteAsset;
			this.m_CurrentMaterial = state.currentMaterial;
			this.m_CurrentMaterialIndex = state.currentMaterialIndex;
			this.m_CharacterCount = state.totalCharacterCount + 1;
			this.m_LineVisibleCharacterCount = state.visibleCharacterCount;
			textInfo.linkCount = state.visibleLinkCount;
			this.m_FirstCharacterOfLine = state.firstCharacterIndex;
			this.m_FirstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
			this.m_LastVisibleCharacterOfLine = state.lastVisibleCharIndex;
			this.m_FontStyleInternal = state.fontStyle;
			this.m_FontScale = state.fontScale;
			this.m_FontScaleMultiplier = state.fontScaleMultiplier;
			this.m_CurrentFontSize = state.currentFontSize;
			this.m_XAdvance = state.xAdvance;
			this.m_MaxCapHeight = state.maxCapHeight;
			this.m_MaxAscender = state.maxAscender;
			this.m_MaxDescender = state.maxDescender;
			this.m_MaxLineAscender = state.maxLineAscender;
			this.m_MaxLineDescender = state.maxLineDescender;
			this.m_StartOfLineAscender = state.previousLineAscender;
			this.m_PreferredWidth = state.preferredWidth;
			this.m_PreferredHeight = state.preferredHeight;
			this.m_MeshExtents = state.meshExtents;
			this.m_LineNumber = state.lineNumber;
			this.m_LineOffset = state.lineOffset;
			this.m_BaselineOffset = state.baselineOffset;
			this.m_HtmlColor = state.vertexColor;
			this.m_UnderlineColor = state.underlineColor;
			this.m_StrikethroughColor = state.strikethroughColor;
			this.m_HighlightColor = state.highlightColor;
			this.m_IsNonBreakingSpace = state.isNonBreakingSpace;
			this.m_TagNoParsing = state.tagNoParsing;
			this.m_FontStyleStack = state.basicStyleStack;
			this.m_ColorStack = state.colorStack;
			this.m_UnderlineColorStack = state.underlineColorStack;
			this.m_StrikethroughColorStack = state.strikethroughColorStack;
			this.m_HighlightColorStack = state.highlightColorStack;
			this.m_ColorGradientStack = state.colorGradientStack;
			this.m_SizeStack = state.sizeStack;
			this.m_IndentStack = state.indentStack;
			this.m_FontWeightStack = state.fontWeightStack;
			this.m_StyleStack = state.styleStack;
			this.m_BaselineOffsetStack = state.baselineStack;
			this.m_ActionStack = state.actionStack;
			this.m_MaterialReferenceStack = state.materialReferenceStack;
			this.m_LineJustificationStack = state.lineJustificationStack;
			this.m_SpriteAnimationId = state.spriteAnimationId;
			bool flag = this.m_LineNumber < textInfo.lineInfo.Length;
			if (flag)
			{
				textInfo.lineInfo[this.m_LineNumber] = state.lineInfo;
			}
			return previousWordBreak;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000100CC File Offset: 0x0000E2CC
		private bool ValidateRichTextTag(string sourceText, ref int readIndex, ref int writeIndex, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			int num = readIndex;
			int num2 = writeIndex;
			int length = sourceText.Length;
			bool flag = false;
			byte b = 0;
			bool flag2 = false;
			int num3 = 0;
			this.m_Attributes[num3].nameHashCode = 0;
			this.m_Attributes[num3].valueHashCode = 0;
			this.m_Attributes[num3].valueLength = 0;
			while (readIndex < length && sourceText[readIndex] > '\0')
			{
				uint num4 = (uint)sourceText[readIndex];
				bool flag3 = writeIndex == this.m_InternalTextParsingBuffer.Length;
				if (flag3)
				{
					TextGeneratorUtilities.ResizeArray<uint>(this.m_InternalTextParsingBuffer);
				}
				this.m_InternalTextParsingBuffer[writeIndex] = num4;
				writeIndex++;
				bool flag4 = num4 == 60U;
				if (flag4)
				{
					bool flag5 = readIndex > num;
					if (flag5)
					{
						break;
					}
				}
				else
				{
					bool flag6 = num4 == 62U;
					if (flag6)
					{
						flag = true;
						break;
					}
					bool flag7 = b == 0;
					if (flag7)
					{
						bool flag8 = (num4 >= 65U && num4 <= 90U) || (num4 >= 97U && num4 <= 122U) || num4 == 47U || num4 == 45U;
						if (flag8)
						{
							this.m_Attributes[num3].nameHashCode = ((this.m_Attributes[num3].nameHashCode << 5) + this.m_Attributes[num3].nameHashCode ^ (int)TextUtilities.ToUpperASCIIFast(num4));
						}
						else
						{
							bool flag9 = num4 == 61U;
							if (flag9)
							{
								b = 1;
							}
							else
							{
								bool flag10 = num4 == 35U;
								if (flag10)
								{
									this.m_Attributes[num3].nameHashCode = 81999901;
									b = 2;
									readIndex--;
								}
								else
								{
									bool flag11 = num4 == 32U;
									if (!flag11)
									{
										break;
									}
									num3++;
									this.m_Attributes[num3].nameHashCode = 0;
									this.m_Attributes[num3].valueHashCode = 0;
									this.m_Attributes[num3].valueLength = 0;
								}
							}
						}
					}
					else
					{
						bool flag12 = b == 1;
						if (flag12)
						{
							flag2 = false;
							b = 2;
							bool flag13 = num4 == 34U;
							if (flag13)
							{
								flag2 = true;
								goto IL_329;
							}
						}
						bool flag14 = b == 2;
						if (flag14)
						{
							bool flag15 = num4 == 34U;
							if (flag15)
							{
								bool flag16 = !flag2;
								if (flag16)
								{
									break;
								}
								b = 0;
							}
							else
							{
								bool flag17 = !flag2 && num4 == 32U;
								if (flag17)
								{
									num3++;
									this.m_Attributes[num3].nameHashCode = 0;
									this.m_Attributes[num3].valueHashCode = 0;
									this.m_Attributes[num3].valueLength = 0;
									b = 0;
								}
								else
								{
									bool flag18 = this.m_Attributes[num3].valueLength == 0;
									if (flag18)
									{
										this.m_Attributes[num3].valueStartIndex = readIndex;
									}
									this.m_Attributes[num3].valueHashCode = ((this.m_Attributes[num3].valueHashCode << 5) + this.m_Attributes[num3].valueHashCode ^ (int)TextUtilities.ToUpperASCIIFast(num4));
									RichTextTagAttribute[] attributes = this.m_Attributes;
									int num5 = num3;
									attributes[num5].valueLength = attributes[num5].valueLength + 1;
								}
							}
						}
					}
				}
				IL_329:
				readIndex++;
			}
			bool flag19 = !flag;
			bool result;
			if (flag19)
			{
				readIndex = num;
				writeIndex = num2;
				result = false;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00010444 File Offset: 0x0000E644
		protected bool ValidateHtmlTag(int[] chars, int startIndex, out int endIndex, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			TextSettings textSettings = generationSettings.textSettings;
			int num = 0;
			byte b = 0;
			TagUnitType tagUnitType = TagUnitType.Pixels;
			TagValueType tagValueType = TagValueType.None;
			int num2 = 0;
			this.m_XmlAttribute[num2].nameHashCode = 0;
			this.m_XmlAttribute[num2].valueType = TagValueType.None;
			this.m_XmlAttribute[num2].valueHashCode = 0;
			this.m_XmlAttribute[num2].valueStartIndex = 0;
			this.m_XmlAttribute[num2].valueLength = 0;
			this.m_XmlAttribute[1].nameHashCode = 0;
			this.m_XmlAttribute[2].nameHashCode = 0;
			this.m_XmlAttribute[3].nameHashCode = 0;
			this.m_XmlAttribute[4].nameHashCode = 0;
			endIndex = startIndex;
			bool flag = false;
			bool flag2 = false;
			int num3 = startIndex;
			while (num3 < chars.Length && chars[num3] != 0 && num < this.m_RichTextTag.Length && chars[num3] != 60)
			{
				uint num4 = (uint)chars[num3];
				bool flag3 = num4 == 62U;
				if (flag3)
				{
					flag2 = true;
					endIndex = num3;
					this.m_RichTextTag[num] = '\0';
					break;
				}
				this.m_RichTextTag[num] = (char)num4;
				num++;
				bool flag4 = b == 1;
				if (flag4)
				{
					bool flag5 = tagValueType == TagValueType.None;
					if (flag5)
					{
						bool flag6 = num4 == 43U || num4 == 45U || num4 == 46U || (num4 >= 48U && num4 <= 57U);
						if (flag6)
						{
							tagValueType = TagValueType.NumericalValue;
							this.m_XmlAttribute[num2].valueType = TagValueType.NumericalValue;
							this.m_XmlAttribute[num2].valueStartIndex = num - 1;
							XmlTagAttribute[] xmlAttribute = this.m_XmlAttribute;
							int num5 = num2;
							xmlAttribute[num5].valueLength = xmlAttribute[num5].valueLength + 1;
						}
						else
						{
							bool flag7 = num4 == 35U;
							if (flag7)
							{
								tagValueType = TagValueType.ColorValue;
								this.m_XmlAttribute[num2].valueType = TagValueType.ColorValue;
								this.m_XmlAttribute[num2].valueStartIndex = num - 1;
								XmlTagAttribute[] xmlAttribute2 = this.m_XmlAttribute;
								int num6 = num2;
								xmlAttribute2[num6].valueLength = xmlAttribute2[num6].valueLength + 1;
							}
							else
							{
								bool flag8 = num4 == 34U;
								if (flag8)
								{
									tagValueType = TagValueType.StringValue;
									this.m_XmlAttribute[num2].valueType = TagValueType.StringValue;
									this.m_XmlAttribute[num2].valueStartIndex = num;
								}
								else
								{
									tagValueType = TagValueType.StringValue;
									this.m_XmlAttribute[num2].valueType = TagValueType.StringValue;
									this.m_XmlAttribute[num2].valueStartIndex = num - 1;
									this.m_XmlAttribute[num2].valueHashCode = ((this.m_XmlAttribute[num2].valueHashCode << 5) + this.m_XmlAttribute[num2].valueHashCode ^ (int)TextUtilities.ToUpperASCIIFast(num4));
									XmlTagAttribute[] xmlAttribute3 = this.m_XmlAttribute;
									int num7 = num2;
									xmlAttribute3[num7].valueLength = xmlAttribute3[num7].valueLength + 1;
								}
							}
						}
					}
					else
					{
						bool flag9 = tagValueType == TagValueType.NumericalValue;
						if (flag9)
						{
							bool flag10 = num4 == 112U || num4 == 101U || num4 == 37U || num4 == 32U;
							if (flag10)
							{
								b = 2;
								tagValueType = TagValueType.None;
								num2++;
								this.m_XmlAttribute[num2].nameHashCode = 0;
								this.m_XmlAttribute[num2].valueType = TagValueType.None;
								this.m_XmlAttribute[num2].valueHashCode = 0;
								this.m_XmlAttribute[num2].valueStartIndex = 0;
								this.m_XmlAttribute[num2].valueLength = 0;
								bool flag11 = num4 == 101U;
								if (flag11)
								{
									tagUnitType = TagUnitType.FontUnits;
								}
								else
								{
									bool flag12 = num4 == 37U;
									if (flag12)
									{
										tagUnitType = TagUnitType.Percentage;
									}
								}
							}
							else
							{
								bool flag13 = b != 2;
								if (flag13)
								{
									XmlTagAttribute[] xmlAttribute4 = this.m_XmlAttribute;
									int num8 = num2;
									xmlAttribute4[num8].valueLength = xmlAttribute4[num8].valueLength + 1;
								}
							}
						}
						else
						{
							bool flag14 = tagValueType == TagValueType.ColorValue;
							if (flag14)
							{
								bool flag15 = num4 != 32U;
								if (flag15)
								{
									XmlTagAttribute[] xmlAttribute5 = this.m_XmlAttribute;
									int num9 = num2;
									xmlAttribute5[num9].valueLength = xmlAttribute5[num9].valueLength + 1;
								}
								else
								{
									b = 2;
									tagValueType = TagValueType.None;
									num2++;
									this.m_XmlAttribute[num2].nameHashCode = 0;
									this.m_XmlAttribute[num2].valueType = TagValueType.None;
									this.m_XmlAttribute[num2].valueHashCode = 0;
									this.m_XmlAttribute[num2].valueStartIndex = 0;
									this.m_XmlAttribute[num2].valueLength = 0;
								}
							}
							else
							{
								bool flag16 = tagValueType == TagValueType.StringValue;
								if (flag16)
								{
									bool flag17 = num4 != 34U;
									if (flag17)
									{
										this.m_XmlAttribute[num2].valueHashCode = ((this.m_XmlAttribute[num2].valueHashCode << 5) + this.m_XmlAttribute[num2].valueHashCode ^ (int)TextUtilities.ToUpperASCIIFast(num4));
										XmlTagAttribute[] xmlAttribute6 = this.m_XmlAttribute;
										int num10 = num2;
										xmlAttribute6[num10].valueLength = xmlAttribute6[num10].valueLength + 1;
									}
									else
									{
										b = 2;
										tagValueType = TagValueType.None;
										num2++;
										this.m_XmlAttribute[num2].nameHashCode = 0;
										this.m_XmlAttribute[num2].valueType = TagValueType.None;
										this.m_XmlAttribute[num2].valueHashCode = 0;
										this.m_XmlAttribute[num2].valueStartIndex = 0;
										this.m_XmlAttribute[num2].valueLength = 0;
									}
								}
							}
						}
					}
				}
				bool flag18 = num4 == 61U;
				if (flag18)
				{
					b = 1;
				}
				bool flag19 = b == 0 && num4 == 32U;
				if (flag19)
				{
					bool flag20 = flag;
					if (flag20)
					{
						return false;
					}
					flag = true;
					b = 2;
					tagValueType = TagValueType.None;
					num2++;
					this.m_XmlAttribute[num2].nameHashCode = 0;
					this.m_XmlAttribute[num2].valueType = TagValueType.None;
					this.m_XmlAttribute[num2].valueHashCode = 0;
					this.m_XmlAttribute[num2].valueStartIndex = 0;
					this.m_XmlAttribute[num2].valueLength = 0;
				}
				bool flag21 = b == 0;
				if (flag21)
				{
					this.m_XmlAttribute[num2].nameHashCode = ((this.m_XmlAttribute[num2].nameHashCode << 5) + this.m_XmlAttribute[num2].nameHashCode ^ (int)TextUtilities.ToUpperASCIIFast(num4));
				}
				bool flag22 = b == 2 && num4 == 32U;
				if (flag22)
				{
					b = 0;
				}
				num3++;
			}
			bool flag23 = !flag2;
			if (flag23)
			{
				return false;
			}
			bool flag24 = this.m_TagNoParsing && this.m_XmlAttribute[0].nameHashCode != -294095813;
			if (flag24)
			{
				return false;
			}
			bool flag25 = this.m_XmlAttribute[0].nameHashCode == -294095813;
			if (flag25)
			{
				this.m_TagNoParsing = false;
				return true;
			}
			bool flag26 = this.m_RichTextTag[0] == '#' && num == 4;
			if (flag26)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag27 = this.m_RichTextTag[0] == '#' && num == 5;
			if (flag27)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag28 = this.m_RichTextTag[0] == '#' && num == 7;
			if (flag28)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			bool flag29 = this.m_RichTextTag[0] == '#' && num == 9;
			if (flag29)
			{
				this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
				this.m_ColorStack.Add(this.m_HtmlColor);
				return true;
			}
			MarkupTag nameHashCode = (MarkupTag)this.m_XmlAttribute[0].nameHashCode;
			MarkupTag markupTag = nameHashCode;
			if (markupTag <= MarkupTag.SLASH_BOLD)
			{
				if (markupTag <= MarkupTag.SPRITE)
				{
					if (markupTag <= MarkupTag.LOWERCASE)
					{
						if (markupTag <= MarkupTag.ACTION)
						{
							if (markupTag <= MarkupTag.FONT_WEIGHT)
							{
								if (markupTag == MarkupTag.GRADIENT)
								{
									int valueHashCode = this.m_XmlAttribute[0].valueHashCode;
									TextColorGradient textColorGradient;
									bool flag30 = MaterialReferenceManager.TryGetColorGradientPreset(valueHashCode, out textColorGradient);
									if (flag30)
									{
										this.m_ColorGradientPreset = textColorGradient;
									}
									else
									{
										bool flag31 = textColorGradient == null;
										if (flag31)
										{
											textColorGradient = Resources.Load<TextColorGradient>(textSettings.defaultColorGradientPresetsPath + new string(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
										}
										bool flag32 = textColorGradient == null;
										if (flag32)
										{
											return false;
										}
										MaterialReferenceManager.AddColorGradientPreset(valueHashCode, textColorGradient);
										this.m_ColorGradientPreset = textColorGradient;
									}
									this.m_ColorGradientStack.Add(this.m_ColorGradientPreset);
									return true;
								}
								if (markupTag != MarkupTag.FONT_WEIGHT)
								{
									goto IL_3844;
								}
								float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								int num12 = (int)num11;
								int num13 = num12;
								if (num13 <= 400)
								{
									if (num13 <= 200)
									{
										if (num13 != 100)
										{
											if (num13 == 200)
											{
												this.m_FontWeightInternal = TextFontWeight.ExtraLight;
											}
										}
										else
										{
											this.m_FontWeightInternal = TextFontWeight.Thin;
										}
									}
									else if (num13 != 300)
									{
										if (num13 == 400)
										{
											this.m_FontWeightInternal = TextFontWeight.Regular;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Light;
									}
								}
								else if (num13 <= 600)
								{
									if (num13 != 500)
									{
										if (num13 == 600)
										{
											this.m_FontWeightInternal = TextFontWeight.SemiBold;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Medium;
									}
								}
								else if (num13 != 700)
								{
									if (num13 != 800)
									{
										if (num13 == 900)
										{
											this.m_FontWeightInternal = TextFontWeight.Black;
										}
									}
									else
									{
										this.m_FontWeightInternal = TextFontWeight.Heavy;
									}
								}
								else
								{
									this.m_FontWeightInternal = TextFontWeight.Bold;
								}
								this.m_FontWeightStack.Add(this.m_FontWeightInternal);
								return true;
							}
							else
							{
								if (markupTag == MarkupTag.SLASH_GRADIENT)
								{
									this.m_ColorGradientPreset = this.m_ColorGradientStack.Remove();
									return true;
								}
								if (markupTag != MarkupTag.ACTION)
								{
									goto IL_3844;
								}
								int valueHashCode2 = this.m_XmlAttribute[0].valueHashCode;
								bool isParsingText = this.m_IsParsingText;
								if (isParsingText)
								{
									this.m_ActionStack.Add(valueHashCode2);
								}
								return false;
							}
						}
						else if (markupTag <= MarkupTag.SLASH_MONOSPACE)
						{
							if (markupTag == MarkupTag.SLASH_MARGIN)
							{
								this.m_MarginLeft = 0f;
								this.m_MarginRight = 0f;
								return true;
							}
							if (markupTag != MarkupTag.SLASH_MONOSPACE)
							{
								goto IL_3844;
							}
							this.m_MonoSpacing = 0f;
							return true;
						}
						else if (markupTag != MarkupTag.CHARACTER_SPACE)
						{
							if (markupTag != MarkupTag.INDENT)
							{
								if (markupTag != MarkupTag.LOWERCASE)
								{
									goto IL_3844;
								}
								this.m_FontStyleInternal |= FontStyles.LowerCase;
								this.m_FontStyleStack.Add(FontStyles.LowerCase);
								return true;
							}
							else
							{
								float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag33 = num11 == -32767f;
								if (flag33)
								{
									return false;
								}
								switch (tagUnitType)
								{
								case TagUnitType.Pixels:
									this.m_TagIndent = num11;
									break;
								case TagUnitType.FontUnits:
									this.m_TagIndent = num11;
									this.m_TagIndent *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
									break;
								case TagUnitType.Percentage:
									this.m_TagIndent = this.m_MarginWidth * num11 / 100f;
									break;
								}
								this.m_IndentStack.Add(this.m_TagIndent);
								this.m_XAdvance = this.m_TagIndent;
								return true;
							}
						}
						else
						{
							float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag34 = num11 == -32767f;
							if (flag34)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_CSpacing = num11;
								break;
							case TagUnitType.FontUnits:
								this.m_CSpacing = num11;
								this.m_CSpacing *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
								break;
							case TagUnitType.Percentage:
								return false;
							}
							return true;
						}
					}
					else if (markupTag <= MarkupTag.MARGIN)
					{
						if (markupTag <= MarkupTag.SLASH_LOWERCASE)
						{
							if (markupTag == MarkupTag.SLASH_INDENT)
							{
								this.m_TagIndent = this.m_IndentStack.Remove();
								return true;
							}
							if (markupTag != MarkupTag.SLASH_LOWERCASE)
							{
								goto IL_3844;
							}
							bool flag35 = (generationSettings.fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase;
							if (flag35)
							{
								bool flag36 = this.m_FontStyleStack.Remove(FontStyles.LowerCase) == 0;
								if (flag36)
								{
									this.m_FontStyleInternal &= ~FontStyles.LowerCase;
								}
							}
							return true;
						}
						else if (markupTag != MarkupTag.SLASH_CHARACTER_SPACE)
						{
							if (markupTag != MarkupTag.MARGIN)
							{
								goto IL_3844;
							}
							float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag37 = num11 == -32767f;
							if (flag37)
							{
								return false;
							}
							this.m_MarginLeft = num11;
							switch (tagUnitType)
							{
							case TagUnitType.FontUnits:
								this.m_MarginLeft *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
								break;
							case TagUnitType.Percentage:
								this.m_MarginLeft = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * this.m_MarginLeft / 100f;
								break;
							}
							this.m_MarginLeft = ((this.m_MarginLeft >= 0f) ? this.m_MarginLeft : 0f);
							this.m_MarginRight = this.m_MarginLeft;
							return true;
						}
						else
						{
							bool flag38 = !this.m_IsParsingText;
							if (flag38)
							{
								return true;
							}
							bool flag39 = this.m_CharacterCount > 0;
							if (flag39)
							{
								this.m_XAdvance -= this.m_CSpacing;
								textInfo.textElementInfo[this.m_CharacterCount - 1].xAdvance = this.m_XAdvance;
							}
							this.m_CSpacing = 0f;
							return true;
						}
					}
					else if (markupTag <= MarkupTag.SLASH_ACTION)
					{
						if (markupTag != MarkupTag.MONOSPACE)
						{
							if (markupTag != MarkupTag.SLASH_ACTION)
							{
								goto IL_3844;
							}
							this.m_ActionStack.Remove();
							return false;
						}
						else
						{
							float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag40 = num11 == -32767f;
							if (flag40)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_MonoSpacing = num11;
								break;
							case TagUnitType.FontUnits:
								this.m_MonoSpacing = num11;
								this.m_MonoSpacing *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
								break;
							case TagUnitType.Percentage:
								return false;
							}
							return true;
						}
					}
					else
					{
						if (markupTag == MarkupTag.SLASH_MATERIAL)
						{
							MaterialReference materialReference = this.m_MaterialReferenceStack.Remove();
							this.m_CurrentMaterial = materialReference.material;
							this.m_CurrentMaterialIndex = materialReference.index;
							return true;
						}
						if (markupTag != MarkupTag.ROTATE)
						{
							if (markupTag != MarkupTag.SPRITE)
							{
								goto IL_3844;
							}
							int valueHashCode3 = this.m_XmlAttribute[0].valueHashCode;
							this.m_SpriteIndex = -1;
							bool flag41 = this.m_XmlAttribute[0].valueType == TagValueType.None || this.m_XmlAttribute[0].valueType == TagValueType.NumericalValue;
							if (flag41)
							{
								bool flag42 = generationSettings.spriteAsset != null;
								if (flag42)
								{
									this.m_CurrentSpriteAsset = generationSettings.spriteAsset;
								}
								else
								{
									bool flag43 = this.m_DefaultSpriteAsset != null;
									if (flag43)
									{
										this.m_CurrentSpriteAsset = this.m_DefaultSpriteAsset;
									}
									else
									{
										bool flag44 = this.m_DefaultSpriteAsset == null;
										if (flag44)
										{
											bool flag45 = textSettings.defaultSpriteAsset != null;
											if (flag45)
											{
												this.m_DefaultSpriteAsset = textSettings.defaultSpriteAsset;
											}
											else
											{
												this.m_DefaultSpriteAsset = Resources.Load<SpriteAsset>("Sprite Assets/Default Sprite Asset");
											}
											this.m_CurrentSpriteAsset = this.m_DefaultSpriteAsset;
										}
									}
								}
								bool flag46 = this.m_CurrentSpriteAsset == null;
								if (flag46)
								{
									return false;
								}
							}
							else
							{
								SpriteAsset spriteAsset;
								bool flag47 = MaterialReferenceManager.TryGetSpriteAsset(valueHashCode3, out spriteAsset);
								if (flag47)
								{
									this.m_CurrentSpriteAsset = spriteAsset;
								}
								else
								{
									bool flag48 = spriteAsset == null;
									if (flag48)
									{
										spriteAsset = Resources.Load<SpriteAsset>(textSettings.defaultSpriteAssetPath + new string(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
									}
									bool flag49 = spriteAsset == null;
									if (flag49)
									{
										return false;
									}
									MaterialReferenceManager.AddSpriteAsset(valueHashCode3, spriteAsset);
									this.m_CurrentSpriteAsset = spriteAsset;
								}
							}
							bool flag50 = this.m_XmlAttribute[0].valueType == TagValueType.NumericalValue;
							if (flag50)
							{
								int num14 = (int)TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag51 = (float)num14 == -32767f;
								if (flag51)
								{
									return false;
								}
								bool flag52 = num14 > this.m_CurrentSpriteAsset.spriteCharacterTable.Count - 1;
								if (flag52)
								{
									return false;
								}
								this.m_SpriteIndex = num14;
							}
							this.m_SpriteColor = Color.white;
							this.m_TintSprite = false;
							int num15 = 0;
							while (num15 < this.m_XmlAttribute.Length && this.m_XmlAttribute[num15].nameHashCode != 0)
							{
								int nameHashCode2 = this.m_XmlAttribute[num15].nameHashCode;
								MarkupTag markupTag2 = (MarkupTag)nameHashCode2;
								MarkupTag markupTag3 = markupTag2;
								if (markupTag3 <= MarkupTag.NAME)
								{
									if (markupTag3 != MarkupTag.ANIM)
									{
										if (markupTag3 != MarkupTag.NAME)
										{
											goto IL_30E3;
										}
										int num16;
										this.m_CurrentSpriteAsset = SpriteAsset.SearchForSpriteByHashCode(this.m_CurrentSpriteAsset, this.m_XmlAttribute[num15].valueHashCode, true, out num16, null);
										bool flag53 = num16 == -1;
										if (flag53)
										{
											return false;
										}
										this.m_SpriteIndex = num16;
									}
									else
									{
										Debug.LogWarning("Sprite animations are not currently supported in TextCore");
									}
								}
								else if (markupTag3 != MarkupTag.TINT)
								{
									if (markupTag3 != MarkupTag.COLOR)
									{
										if (markupTag3 != MarkupTag.INDEX)
										{
											goto IL_30E3;
										}
										int num16 = (int)TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
										bool flag54 = (float)num16 == -32767f;
										if (flag54)
										{
											return false;
										}
										bool flag55 = num16 > this.m_CurrentSpriteAsset.spriteCharacterTable.Count - 1;
										if (flag55)
										{
											return false;
										}
										this.m_SpriteIndex = num16;
									}
									else
									{
										this.m_SpriteColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, this.m_XmlAttribute[num15].valueStartIndex, this.m_XmlAttribute[num15].valueLength);
									}
								}
								else
								{
									this.m_TintSprite = (TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[num15].valueStartIndex, this.m_XmlAttribute[num15].valueLength) != 0f);
								}
								IL_30FF:
								num15++;
								continue;
								IL_30E3:
								bool flag56 = nameHashCode2 != -991527447;
								if (flag56)
								{
									return false;
								}
								goto IL_30FF;
							}
							bool flag57 = this.m_SpriteIndex == -1;
							if (flag57)
							{
								return false;
							}
							this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentSpriteAsset.material, this.m_CurrentSpriteAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
							this.m_TextElementType = TextElementType.Sprite;
							return true;
						}
						else
						{
							float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag58 = num11 == -32767f;
							if (flag58)
							{
								return false;
							}
							this.m_FxMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, num11), Vector3.one);
							this.m_IsFxMatrixSet = true;
							return true;
						}
					}
				}
				else
				{
					if (markupTag <= MarkupTag.NO_PARSE)
					{
						if (markupTag <= MarkupTag.SMALLCAPS)
						{
							if (markupTag <= MarkupTag.LINE_HEIGHT)
							{
								if (markupTag != MarkupTag.LINE_INDENT)
								{
									if (markupTag != MarkupTag.LINE_HEIGHT)
									{
										goto IL_3844;
									}
									float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
									bool flag59 = num11 == -32767f || num11 == 0f;
									if (flag59)
									{
										return false;
									}
									this.m_LineHeight = num11;
									switch (tagUnitType)
									{
									case TagUnitType.FontUnits:
										this.m_LineHeight *= generationSettings.fontAsset.faceInfo.lineHeight * this.m_FontScale;
										break;
									case TagUnitType.Percentage:
										this.m_LineHeight = generationSettings.fontAsset.faceInfo.lineHeight * this.m_LineHeight / 100f * this.m_FontScale;
										break;
									}
									return true;
								}
								else
								{
									float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
									bool flag60 = num11 == -32767f;
									if (flag60)
									{
										return false;
									}
									switch (tagUnitType)
									{
									case TagUnitType.Pixels:
										this.m_TagLineIndent = num11;
										break;
									case TagUnitType.FontUnits:
										this.m_TagLineIndent = num11;
										this.m_TagLineIndent *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
										break;
									case TagUnitType.Percentage:
										this.m_TagLineIndent = this.m_MarginWidth * num11 / 100f;
										break;
									}
									this.m_XAdvance += this.m_TagLineIndent;
									return true;
								}
							}
							else if (markupTag != MarkupTag.SLASH_ALLCAPS)
							{
								if (markupTag != MarkupTag.SMALLCAPS)
								{
									goto IL_3844;
								}
								this.m_FontStyleInternal |= FontStyles.SmallCaps;
								this.m_FontStyleStack.Add(FontStyles.SmallCaps);
								return true;
							}
						}
						else if (markupTag <= MarkupTag.SLASH_FONT_WEIGHT)
						{
							if (markupTag == MarkupTag.SLASH_ROTATE)
							{
								this.m_IsFxMatrixSet = false;
								return true;
							}
							if (markupTag != MarkupTag.SLASH_FONT_WEIGHT)
							{
								goto IL_3844;
							}
							this.m_FontWeightStack.Remove();
							bool flag61 = this.m_FontStyleInternal == FontStyles.Bold;
							if (flag61)
							{
								this.m_FontWeightInternal = TextFontWeight.Bold;
							}
							else
							{
								this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
							}
							return true;
						}
						else if (markupTag != MarkupTag.SLASH_UPPERCASE)
						{
							if (markupTag != MarkupTag.MARGIN_RIGHT)
							{
								if (markupTag != MarkupTag.NO_PARSE)
								{
									goto IL_3844;
								}
								this.m_TagNoParsing = true;
								return true;
							}
							else
							{
								float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag62 = num11 == -32767f;
								if (flag62)
								{
									return false;
								}
								this.m_MarginRight = num11;
								switch (tagUnitType)
								{
								case TagUnitType.FontUnits:
									this.m_MarginRight *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
									break;
								case TagUnitType.Percentage:
									this.m_MarginRight = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * this.m_MarginRight / 100f;
									break;
								}
								this.m_MarginRight = ((this.m_MarginRight >= 0f) ? this.m_MarginRight : 0f);
								return true;
							}
						}
						bool flag63 = (generationSettings.fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase;
						if (flag63)
						{
							bool flag64 = this.m_FontStyleStack.Remove(FontStyles.UpperCase) == 0;
							if (flag64)
							{
								this.m_FontStyleInternal &= ~FontStyles.UpperCase;
							}
						}
						return true;
					}
					if (markupTag <= MarkupTag.BOLD)
					{
						if (markupTag <= MarkupTag.MARGIN_LEFT)
						{
							if (markupTag != MarkupTag.UPPERCASE)
							{
								if (markupTag != MarkupTag.MARGIN_LEFT)
								{
									goto IL_3844;
								}
								float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
								bool flag65 = num11 == -32767f;
								if (flag65)
								{
									return false;
								}
								this.m_MarginLeft = num11;
								switch (tagUnitType)
								{
								case TagUnitType.FontUnits:
									this.m_MarginLeft *= this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
									break;
								case TagUnitType.Percentage:
									this.m_MarginLeft = (this.m_MarginWidth - ((this.m_Width != -1f) ? this.m_Width : 0f)) * this.m_MarginLeft / 100f;
									break;
								}
								this.m_MarginLeft = ((this.m_MarginLeft >= 0f) ? this.m_MarginLeft : 0f);
								return true;
							}
						}
						else
						{
							if (markupTag == MarkupTag.SLASH_VERTICAL_OFFSET)
							{
								this.m_BaselineOffset = 0f;
								return true;
							}
							if (markupTag == MarkupTag.A)
							{
								return false;
							}
							if (markupTag != MarkupTag.BOLD)
							{
								goto IL_3844;
							}
							this.m_FontStyleInternal |= FontStyles.Bold;
							this.m_FontStyleStack.Add(FontStyles.Bold);
							this.m_FontWeightInternal = TextFontWeight.Bold;
							return true;
						}
					}
					else if (markupTag <= MarkupTag.STRIKETHROUGH)
					{
						if (markupTag == MarkupTag.ITALIC)
						{
							this.m_FontStyleInternal |= FontStyles.Italic;
							this.m_FontStyleStack.Add(FontStyles.Italic);
							return true;
						}
						if (markupTag != MarkupTag.STRIKETHROUGH)
						{
							goto IL_3844;
						}
						this.m_FontStyleInternal |= FontStyles.Strikethrough;
						this.m_FontStyleStack.Add(FontStyles.Strikethrough);
						bool flag66 = (long)this.m_XmlAttribute[1].nameHashCode == 81999901L;
						if (flag66)
						{
							this.m_StrikethroughColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
							this.m_StrikethroughColor.a = ((this.m_HtmlColor.a < this.m_StrikethroughColor.a) ? this.m_HtmlColor.a : this.m_StrikethroughColor.a);
						}
						else
						{
							this.m_StrikethroughColor = this.m_HtmlColor;
						}
						this.m_StrikethroughColorStack.Add(this.m_StrikethroughColor);
						return true;
					}
					else
					{
						if (markupTag == MarkupTag.UNDERLINE)
						{
							this.m_FontStyleInternal |= FontStyles.Underline;
							this.m_FontStyleStack.Add(FontStyles.Underline);
							bool flag67 = (long)this.m_XmlAttribute[1].nameHashCode == 81999901L;
							if (flag67)
							{
								this.m_UnderlineColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength);
								this.m_UnderlineColor.a = ((this.m_HtmlColor.a < this.m_UnderlineColor.a) ? this.m_HtmlColor.a : this.m_UnderlineColor.a);
							}
							else
							{
								this.m_UnderlineColor = this.m_HtmlColor;
							}
							this.m_UnderlineColorStack.Add(this.m_UnderlineColor);
							return true;
						}
						if (markupTag == MarkupTag.SLASH_ITALIC)
						{
							bool flag68 = (generationSettings.fontStyle & FontStyles.Italic) != FontStyles.Italic;
							if (flag68)
							{
								bool flag69 = this.m_FontStyleStack.Remove(FontStyles.Italic) == 0;
								if (flag69)
								{
									this.m_FontStyleInternal &= ~FontStyles.Italic;
								}
							}
							return true;
						}
						if (markupTag != MarkupTag.SLASH_BOLD)
						{
							goto IL_3844;
						}
						bool flag70 = (generationSettings.fontStyle & FontStyles.Bold) != FontStyles.Bold;
						if (flag70)
						{
							bool flag71 = this.m_FontStyleStack.Remove(FontStyles.Bold) == 0;
							if (flag71)
							{
								this.m_FontStyleInternal &= ~FontStyles.Bold;
								this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
							}
						}
						return true;
					}
				}
			}
			else if (markupTag <= MarkupTag.SLASH_LINK)
			{
				if (markupTag <= MarkupTag.SLASH_POSITION)
				{
					if (markupTag <= MarkupTag.POSITION)
					{
						if (markupTag <= MarkupTag.SLASH_UNDERLINE)
						{
							if (markupTag == MarkupTag.SLASH_A)
							{
								return true;
							}
							if (markupTag != MarkupTag.SLASH_UNDERLINE)
							{
								goto IL_3844;
							}
							bool flag72 = (generationSettings.fontStyle & FontStyles.Underline) != FontStyles.Underline;
							if (flag72)
							{
								this.m_UnderlineColor = this.m_UnderlineColorStack.Remove();
								bool flag73 = this.m_FontStyleStack.Remove(FontStyles.Underline) == 0;
								if (flag73)
								{
									this.m_FontStyleInternal &= ~FontStyles.Underline;
								}
							}
							return true;
						}
						else
						{
							if (markupTag == MarkupTag.SLASH_STRIKETHROUGH)
							{
								bool flag74 = (generationSettings.fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough;
								if (flag74)
								{
									bool flag75 = this.m_FontStyleStack.Remove(FontStyles.Strikethrough) == 0;
									if (flag75)
									{
										this.m_FontStyleInternal &= ~FontStyles.Strikethrough;
									}
								}
								return true;
							}
							if (markupTag != MarkupTag.POSITION)
							{
								goto IL_3844;
							}
							float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							bool flag76 = num11 == -32767f;
							if (flag76)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_XAdvance = num11;
								return true;
							case TagUnitType.FontUnits:
								this.m_XAdvance = num11 * this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
								return true;
							case TagUnitType.Percentage:
								this.m_XAdvance = this.m_MarginWidth * num11 / 100f;
								return true;
							default:
								return false;
							}
						}
					}
					else if (markupTag <= MarkupTag.SUPERSCRIPT)
					{
						if (markupTag == MarkupTag.SUBSCRIPT)
						{
							this.m_FontScaleMultiplier *= ((this.m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
							this.m_BaselineOffsetStack.Push(this.m_BaselineOffset);
							this.m_BaselineOffset += this.m_CurrentFontAsset.faceInfo.subscriptOffset * this.m_FontScale * this.m_FontScaleMultiplier;
							this.m_FontStyleStack.Add(FontStyles.Subscript);
							this.m_FontStyleInternal |= FontStyles.Subscript;
							return true;
						}
						if (markupTag != MarkupTag.SUPERSCRIPT)
						{
							goto IL_3844;
						}
						this.m_FontScaleMultiplier *= ((this.m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
						this.m_BaselineOffsetStack.Push(this.m_BaselineOffset);
						this.m_BaselineOffset += this.m_CurrentFontAsset.faceInfo.superscriptOffset * this.m_FontScale * this.m_FontScaleMultiplier;
						this.m_FontStyleStack.Add(FontStyles.Superscript);
						this.m_FontStyleInternal |= FontStyles.Superscript;
						return true;
					}
					else
					{
						if (markupTag == MarkupTag.SLASH_SUBSCRIPT)
						{
							bool flag77 = (this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript;
							if (flag77)
							{
								bool flag78 = this.m_FontScaleMultiplier < 1f;
								if (flag78)
								{
									this.m_BaselineOffset = this.m_BaselineOffsetStack.Pop();
									this.m_FontScaleMultiplier /= ((this.m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
								}
								bool flag79 = this.m_FontStyleStack.Remove(FontStyles.Subscript) == 0;
								if (flag79)
								{
									this.m_FontStyleInternal &= ~FontStyles.Subscript;
								}
							}
							return true;
						}
						if (markupTag == MarkupTag.SLASH_SUPERSCRIPT)
						{
							bool flag80 = (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript;
							if (flag80)
							{
								bool flag81 = this.m_FontScaleMultiplier < 1f;
								if (flag81)
								{
									this.m_BaselineOffset = this.m_BaselineOffsetStack.Pop();
									this.m_FontScaleMultiplier /= ((this.m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
								}
								bool flag82 = this.m_FontStyleStack.Remove(FontStyles.Superscript) == 0;
								if (flag82)
								{
									this.m_FontStyleInternal &= ~FontStyles.Superscript;
								}
							}
							return true;
						}
						if (markupTag != MarkupTag.SLASH_POSITION)
						{
							goto IL_3844;
						}
						return true;
					}
				}
				else if (markupTag <= MarkupTag.PAGE)
				{
					if (markupTag <= MarkupTag.LINK)
					{
						if (markupTag != MarkupTag.FONT)
						{
							if (markupTag != MarkupTag.LINK)
							{
								goto IL_3844;
							}
							bool flag83 = this.m_IsParsingText && !this.m_IsCalculatingPreferredValues;
							if (flag83)
							{
								int linkCount = textInfo.linkCount;
								bool flag84 = linkCount + 1 > textInfo.linkInfo.Length;
								if (flag84)
								{
									TextInfo.Resize<LinkInfo>(ref textInfo.linkInfo, linkCount + 1);
								}
								textInfo.linkInfo[linkCount].hashCode = this.m_XmlAttribute[0].valueHashCode;
								textInfo.linkInfo[linkCount].linkTextfirstCharacterIndex = this.m_CharacterCount;
								textInfo.linkInfo[linkCount].linkIdFirstCharacterIndex = startIndex + this.m_XmlAttribute[0].valueStartIndex;
								textInfo.linkInfo[linkCount].linkIdLength = this.m_XmlAttribute[0].valueLength;
								textInfo.linkInfo[linkCount].SetLinkId(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							}
							return true;
						}
						else
						{
							int valueHashCode4 = this.m_XmlAttribute[0].valueHashCode;
							int nameHashCode3 = this.m_XmlAttribute[1].nameHashCode;
							int valueHashCode5 = this.m_XmlAttribute[1].valueHashCode;
							bool flag85 = valueHashCode4 == -620974005;
							if (flag85)
							{
								this.m_CurrentFontAsset = this.m_MaterialReferences[0].fontAsset;
								this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
								this.m_CurrentMaterialIndex = 0;
								this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
								this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[0]);
								return true;
							}
							FontAsset fontAsset;
							bool flag86 = !MaterialReferenceManager.TryGetFontAsset(valueHashCode4, out fontAsset);
							if (flag86)
							{
								fontAsset = Resources.Load<FontAsset>(textSettings.defaultFontAssetPath + new string(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
								bool flag87 = fontAsset == null;
								if (flag87)
								{
									return false;
								}
								MaterialReferenceManager.AddFontAsset(fontAsset);
							}
							bool flag88 = nameHashCode3 == 0 && valueHashCode5 == 0;
							if (flag88)
							{
								this.m_CurrentMaterial = fontAsset.material;
								this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, fontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
								this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
							}
							else
							{
								bool flag89 = (long)nameHashCode3 == 825491659L;
								if (!flag89)
								{
									return false;
								}
								Material material;
								bool flag90 = MaterialReferenceManager.TryGetMaterial(valueHashCode5, out material);
								if (flag90)
								{
									this.m_CurrentMaterial = material;
									this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, fontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
									this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
								}
								else
								{
									material = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(this.m_RichTextTag, this.m_XmlAttribute[1].valueStartIndex, this.m_XmlAttribute[1].valueLength));
									bool flag91 = material == null;
									if (flag91)
									{
										return false;
									}
									MaterialReferenceManager.AddFontMaterial(valueHashCode5, material);
									this.m_CurrentMaterial = material;
									this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, fontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
									this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
								}
							}
							this.m_CurrentFontAsset = fontAsset;
							this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
							return true;
						}
					}
					else
					{
						if (markupTag == MarkupTag.MARK)
						{
							this.m_FontStyleInternal |= FontStyles.Highlight;
							this.m_FontStyleStack.Add(FontStyles.Highlight);
							this.m_HighlightColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
							this.m_HighlightColor.a = ((this.m_HtmlColor.a < this.m_HighlightColor.a) ? this.m_HtmlColor.a : this.m_HighlightColor.a);
							this.m_HighlightColorStack.Add(this.m_HighlightColor);
							return true;
						}
						if (markupTag != MarkupTag.PAGE)
						{
							goto IL_3844;
						}
						bool flag92 = generationSettings.overflowMode == TextOverflowMode.Page;
						if (flag92)
						{
							this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
							this.m_LineOffset = 0f;
							this.m_PageNumber++;
							this.m_IsNewPage = true;
						}
						return true;
					}
				}
				else if (markupTag <= MarkupTag.SIZE)
				{
					if (markupTag == MarkupTag.NO_BREAK)
					{
						this.m_IsNonBreakingSpace = true;
						return true;
					}
					if (markupTag != MarkupTag.SIZE)
					{
						goto IL_3844;
					}
					float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
					bool flag93 = num11 == -32767f;
					if (flag93)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
					{
						bool flag94 = this.m_RichTextTag[5] == '+';
						if (flag94)
						{
							this.m_CurrentFontSize = this.m_FontSize + num11;
							this.m_SizeStack.Add(this.m_CurrentFontSize);
							this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
							return true;
						}
						bool flag95 = this.m_RichTextTag[5] == '-';
						if (flag95)
						{
							this.m_CurrentFontSize = this.m_FontSize + num11;
							this.m_SizeStack.Add(this.m_CurrentFontSize);
							this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
							return true;
						}
						this.m_CurrentFontSize = num11;
						this.m_SizeStack.Add(this.m_CurrentFontSize);
						this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
						return true;
					}
					case TagUnitType.FontUnits:
						this.m_CurrentFontSize = this.m_FontSize * num11;
						this.m_SizeStack.Add(this.m_CurrentFontSize);
						this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
						return true;
					case TagUnitType.Percentage:
						this.m_CurrentFontSize = this.m_FontSize * num11 / 100f;
						this.m_SizeStack.Add(this.m_CurrentFontSize);
						this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
						return true;
					default:
						return false;
					}
				}
				else
				{
					if (markupTag == MarkupTag.SLASH_NO_BREAK)
					{
						this.m_IsNonBreakingSpace = false;
						return true;
					}
					if (markupTag == MarkupTag.SLASH_MARK)
					{
						bool flag96 = (generationSettings.fontStyle & FontStyles.Highlight) != FontStyles.Highlight;
						if (flag96)
						{
							this.m_HighlightColor = this.m_HighlightColorStack.Remove();
							bool flag97 = this.m_FontStyleStack.Remove(FontStyles.Highlight) == 0;
							if (flag97)
							{
								this.m_FontStyleInternal &= ~FontStyles.Highlight;
							}
						}
						return true;
					}
					if (markupTag != MarkupTag.SLASH_LINK)
					{
						goto IL_3844;
					}
					bool flag98 = this.m_IsParsingText && !this.m_IsCalculatingPreferredValues;
					if (flag98)
					{
						bool flag99 = textInfo.linkCount < textInfo.linkInfo.Length;
						if (flag99)
						{
							textInfo.linkInfo[textInfo.linkCount].linkTextLength = this.m_CharacterCount - textInfo.linkInfo[textInfo.linkCount].linkTextfirstCharacterIndex;
							textInfo.linkCount++;
						}
					}
					return true;
				}
			}
			else if (markupTag <= MarkupTag.SCALE)
			{
				if (markupTag <= MarkupTag.ALPHA)
				{
					if (markupTag <= MarkupTag.SLASH_SIZE)
					{
						if (markupTag == MarkupTag.SLASH_FONT)
						{
							MaterialReference materialReference2 = this.m_MaterialReferenceStack.Remove();
							this.m_CurrentFontAsset = materialReference2.fontAsset;
							this.m_CurrentMaterial = materialReference2.material;
							this.m_CurrentMaterialIndex = materialReference2.index;
							this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
							return true;
						}
						if (markupTag != MarkupTag.SLASH_SIZE)
						{
							goto IL_3844;
						}
						this.m_CurrentFontSize = this.m_SizeStack.Remove();
						this.m_FontScale = this.m_CurrentFontSize / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
						return true;
					}
					else
					{
						if (markupTag == MarkupTag.ALIGN)
						{
							MarkupTag valueHashCode6 = (MarkupTag)this.m_XmlAttribute[0].valueHashCode;
							MarkupTag markupTag4 = valueHashCode6;
							if (markupTag4 <= MarkupTag.LEFT)
							{
								if (markupTag4 == MarkupTag.CENTER)
								{
									this.m_LineJustification = TextAlignment.MiddleCenter;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag4 == MarkupTag.LEFT)
								{
									this.m_LineJustification = TextAlignment.MiddleLeft;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
							}
							else
							{
								if (markupTag4 == MarkupTag.FLUSH)
								{
									this.m_LineJustification = TextAlignment.MiddleFlush;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag4 == MarkupTag.RIGHT)
								{
									this.m_LineJustification = TextAlignment.MiddleRight;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
								if (markupTag4 == MarkupTag.JUSTIFIED)
								{
									this.m_LineJustification = TextAlignment.MiddleJustified;
									this.m_LineJustificationStack.Add(this.m_LineJustification);
									return true;
								}
							}
							return false;
						}
						if (markupTag != MarkupTag.ALPHA)
						{
							goto IL_3844;
						}
						bool flag100 = this.m_XmlAttribute[0].valueLength != 3;
						if (flag100)
						{
							return false;
						}
						this.m_HtmlColor.a = (byte)(TextGeneratorUtilities.HexToInt(this.m_RichTextTag[7]) * 16 + TextGeneratorUtilities.HexToInt(this.m_RichTextTag[8]));
						return true;
					}
				}
				else if (markupTag <= MarkupTag.CLASS)
				{
					if (markupTag != MarkupTag.COLOR)
					{
						if (markupTag != MarkupTag.CLASS)
						{
							goto IL_3844;
						}
						return false;
					}
					else
					{
						bool flag101 = this.m_RichTextTag[6] == '#' && num == 10;
						if (flag101)
						{
							this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
							this.m_ColorStack.Add(this.m_HtmlColor);
							return true;
						}
						bool flag102 = this.m_RichTextTag[6] == '#' && num == 11;
						if (flag102)
						{
							this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
							this.m_ColorStack.Add(this.m_HtmlColor);
							return true;
						}
						bool flag103 = this.m_RichTextTag[6] == '#' && num == 13;
						if (flag103)
						{
							this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
							this.m_ColorStack.Add(this.m_HtmlColor);
							return true;
						}
						bool flag104 = this.m_RichTextTag[6] == '#' && num == 15;
						if (flag104)
						{
							this.m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(this.m_RichTextTag, num);
							this.m_ColorStack.Add(this.m_HtmlColor);
							return true;
						}
						MarkupTag valueHashCode7 = (MarkupTag)this.m_XmlAttribute[0].valueHashCode;
						MarkupTag markupTag5 = valueHashCode7;
						if (markupTag5 <= MarkupTag.RED)
						{
							if (markupTag5 <= MarkupTag.ORANGE)
							{
								if (markupTag5 == MarkupTag.PURPLE)
								{
									this.m_HtmlColor = new Color32(160, 32, 240, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (markupTag5 == MarkupTag.ORANGE)
								{
									this.m_HtmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
							else
							{
								if (markupTag5 == MarkupTag.YELLOW)
								{
									this.m_HtmlColor = Color.yellow;
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
								if (markupTag5 == MarkupTag.RED)
								{
									this.m_HtmlColor = Color.red;
									this.m_ColorStack.Add(this.m_HtmlColor);
									return true;
								}
							}
						}
						else if (markupTag5 <= MarkupTag.BLACK)
						{
							if (markupTag5 == MarkupTag.BLUE)
							{
								this.m_HtmlColor = Color.blue;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (markupTag5 == MarkupTag.BLACK)
							{
								this.m_HtmlColor = Color.black;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
						}
						else
						{
							if (markupTag5 == MarkupTag.GREEN)
							{
								this.m_HtmlColor = Color.green;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
							if (markupTag5 == MarkupTag.WHITE)
							{
								this.m_HtmlColor = Color.white;
								this.m_ColorStack.Add(this.m_HtmlColor);
								return true;
							}
						}
						return false;
					}
				}
				else
				{
					if (markupTag == MarkupTag.SLASH_LINE_INDENT)
					{
						this.m_TagLineIndent = 0f;
						return true;
					}
					if (markupTag != MarkupTag.SPACE)
					{
						if (markupTag != MarkupTag.SCALE)
						{
							goto IL_3844;
						}
						float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag105 = num11 == -32767f;
						if (flag105)
						{
							return false;
						}
						this.m_FxMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(num11, 1f, 1f));
						this.m_IsFxMatrixSet = true;
						return true;
					}
					else
					{
						float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag106 = num11 == -32767f;
						if (flag106)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_XAdvance += num11;
							return true;
						case TagUnitType.FontUnits:
							this.m_XAdvance += num11 * this.m_FontScale * generationSettings.fontAsset.faceInfo.tabWidth / (float)generationSettings.fontAsset.tabMultiple;
							return true;
						case TagUnitType.Percentage:
							return false;
						default:
							return false;
						}
					}
				}
			}
			else if (markupTag <= MarkupTag.MATERIAL)
			{
				if (markupTag <= MarkupTag.SLASH_SMALLCAPS)
				{
					if (markupTag != MarkupTag.WIDTH)
					{
						if (markupTag != MarkupTag.SLASH_SMALLCAPS)
						{
							goto IL_3844;
						}
						bool flag107 = (generationSettings.fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps;
						if (flag107)
						{
							bool flag108 = this.m_FontStyleStack.Remove(FontStyles.SmallCaps) == 0;
							if (flag108)
							{
								this.m_FontStyleInternal &= ~FontStyles.SmallCaps;
							}
						}
						return true;
					}
					else
					{
						float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
						bool flag109 = num11 == -32767f;
						if (flag109)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_Width = num11;
							break;
						case TagUnitType.FontUnits:
							return false;
						case TagUnitType.Percentage:
							this.m_Width = this.m_MarginWidth * num11 / 100f;
							break;
						}
						return true;
					}
				}
				else
				{
					if (markupTag == MarkupTag.SLASH_LINE_HEIGHT)
					{
						this.m_LineHeight = -32767f;
						return true;
					}
					if (markupTag != MarkupTag.ALLCAPS)
					{
						if (markupTag != MarkupTag.MATERIAL)
						{
							goto IL_3844;
						}
						int valueHashCode5 = this.m_XmlAttribute[0].valueHashCode;
						bool flag110 = valueHashCode5 == -620974005;
						if (flag110)
						{
							this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
							this.m_CurrentMaterialIndex = 0;
							this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[0]);
							return true;
						}
						Material material;
						bool flag111 = MaterialReferenceManager.TryGetMaterial(valueHashCode5, out material);
						if (flag111)
						{
							this.m_CurrentMaterial = material;
							this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
							this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
						}
						else
						{
							material = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength));
							bool flag112 = material == null;
							if (flag112)
							{
								return false;
							}
							MaterialReferenceManager.AddFontMaterial(valueHashCode5, material);
							this.m_CurrentMaterial = material;
							this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
							this.m_MaterialReferenceStack.Add(this.m_MaterialReferences[this.m_CurrentMaterialIndex]);
						}
						return true;
					}
				}
			}
			else if (markupTag <= MarkupTag.SLASH_ALIGN)
			{
				if (markupTag == MarkupTag.SLASH_COLOR)
				{
					this.m_HtmlColor = this.m_ColorStack.Remove();
					return true;
				}
				if (markupTag != MarkupTag.SLASH_ALIGN)
				{
					goto IL_3844;
				}
				this.m_LineJustification = this.m_LineJustificationStack.Remove();
				return true;
			}
			else
			{
				if (markupTag == MarkupTag.SLASH_WIDTH)
				{
					this.m_Width = -1f;
					return true;
				}
				if (markupTag == MarkupTag.SLASH_SCALE)
				{
					this.m_IsFxMatrixSet = false;
					return true;
				}
				if (markupTag != MarkupTag.VERTICAL_OFFSET)
				{
					goto IL_3844;
				}
				float num11 = TextGeneratorUtilities.ConvertToFloat(this.m_RichTextTag, this.m_XmlAttribute[0].valueStartIndex, this.m_XmlAttribute[0].valueLength);
				bool flag113 = num11 == -32767f;
				if (flag113)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_BaselineOffset = num11;
					return true;
				case TagUnitType.FontUnits:
					this.m_BaselineOffset = num11 * this.m_FontScale * generationSettings.fontAsset.faceInfo.ascentLine;
					return true;
				case TagUnitType.Percentage:
					return false;
				default:
					return false;
				}
			}
			this.m_FontStyleInternal |= FontStyles.UpperCase;
			this.m_FontStyleStack.Add(FontStyles.UpperCase);
			return true;
			IL_3844:
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00013CA0 File Offset: 0x00011EA0
		private void SaveGlyphVertexInfo(float padding, float stylePadding, Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[this.m_CharacterCount].bottomLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[this.m_CharacterCount].topLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[this.m_CharacterCount].topRight;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[this.m_CharacterCount].bottomRight;
			vertexColor.a = ((this.m_FontColor32.a < vertexColor.a) ? this.m_FontColor32.a : vertexColor.a);
			bool flag = generationSettings.fontColorGradient == null;
			if (flag)
			{
				textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = vertexColor;
				textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = vertexColor;
			}
			else
			{
				bool flag2 = !generationSettings.overrideRichTextColors && this.m_ColorStack.index > 1;
				if (flag2)
				{
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = vertexColor;
				}
				else
				{
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = generationSettings.fontColorGradient.bottomLeft * vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = generationSettings.fontColorGradient.topLeft * vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = generationSettings.fontColorGradient.topRight * vertexColor;
					textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = generationSettings.fontColorGradient.bottomRight * vertexColor;
				}
			}
			bool flag3 = this.m_ColorGradientPreset != null;
			if (flag3)
			{
				TextElementInfo[] textElementInfo = textInfo.textElementInfo;
				int characterCount = this.m_CharacterCount;
				textElementInfo[characterCount].vertexBottomLeft.color = textElementInfo[characterCount].vertexBottomLeft.color * this.m_ColorGradientPreset.bottomLeft;
				TextElementInfo[] textElementInfo2 = textInfo.textElementInfo;
				int characterCount2 = this.m_CharacterCount;
				textElementInfo2[characterCount2].vertexTopLeft.color = textElementInfo2[characterCount2].vertexTopLeft.color * this.m_ColorGradientPreset.topLeft;
				TextElementInfo[] textElementInfo3 = textInfo.textElementInfo;
				int characterCount3 = this.m_CharacterCount;
				textElementInfo3[characterCount3].vertexTopRight.color = textElementInfo3[characterCount3].vertexTopRight.color * this.m_ColorGradientPreset.topRight;
				TextElementInfo[] textElementInfo4 = textInfo.textElementInfo;
				int characterCount4 = this.m_CharacterCount;
				textElementInfo4[characterCount4].vertexBottomRight.color = textElementInfo4[characterCount4].vertexBottomRight.color * this.m_ColorGradientPreset.bottomRight;
			}
			bool flag4 = !this.m_IsSdfShader;
			if (flag4)
			{
				stylePadding = 0f;
			}
			Vector2 vector;
			vector.x = ((float)this.m_CachedTextElement.glyph.glyphRect.x - padding - stylePadding) / (float)this.m_CurrentFontAsset.atlasWidth;
			vector.y = ((float)this.m_CachedTextElement.glyph.glyphRect.y - padding - stylePadding) / (float)this.m_CurrentFontAsset.atlasHeight;
			Vector2 vector2;
			vector2.x = vector.x;
			vector2.y = ((float)this.m_CachedTextElement.glyph.glyphRect.y + padding + stylePadding + (float)this.m_CachedTextElement.glyph.glyphRect.height) / (float)this.m_CurrentFontAsset.atlasHeight;
			Vector2 vector3;
			vector3.x = ((float)this.m_CachedTextElement.glyph.glyphRect.x + padding + stylePadding + (float)this.m_CachedTextElement.glyph.glyphRect.width) / (float)this.m_CurrentFontAsset.atlasWidth;
			vector3.y = vector2.y;
			Vector2 uv;
			uv.x = vector3.x;
			uv.y = vector.y;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.uv = vector;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.uv = vector2;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.uv = vector3;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.uv = uv;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0001428C File Offset: 0x0001248C
		private void SaveSpriteVertexInfo(Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[this.m_CharacterCount].bottomLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[this.m_CharacterCount].topLeft;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[this.m_CharacterCount].topRight;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[this.m_CharacterCount].bottomRight;
			bool tintSprites = generationSettings.tintSprites;
			if (tintSprites)
			{
				this.m_TintSprite = true;
			}
			Color32 color = this.m_TintSprite ? ColorUtilities.MultiplyColors(this.m_SpriteColor, vertexColor) : this.m_SpriteColor;
			color.a = ((color.a < this.m_FontColor32.a) ? (color.a = ((color.a < vertexColor.a) ? color.a : vertexColor.a)) : this.m_FontColor32.a);
			Color32 color2 = color;
			Color32 color3 = color;
			Color32 color4 = color;
			Color32 color5 = color;
			bool flag = generationSettings.fontColorGradient != null;
			if (flag)
			{
				color2 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color2, generationSettings.fontColorGradient.bottomLeft) : color2);
				color3 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color3, generationSettings.fontColorGradient.topLeft) : color3);
				color4 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color4, generationSettings.fontColorGradient.topRight) : color4);
				color5 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color5, generationSettings.fontColorGradient.bottomRight) : color5);
			}
			bool flag2 = this.m_ColorGradientPreset != null;
			if (flag2)
			{
				color2 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color2, this.m_ColorGradientPreset.bottomLeft) : color2);
				color3 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color3, this.m_ColorGradientPreset.topLeft) : color3);
				color4 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color4, this.m_ColorGradientPreset.topRight) : color4);
				color5 = (this.m_TintSprite ? ColorUtilities.MultiplyColors(color5, this.m_ColorGradientPreset.bottomRight) : color5);
			}
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.color = color2;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.color = color3;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.color = color4;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.color = color5;
			Vector2 vector = new Vector2((float)this.m_CachedTextElement.glyph.glyphRect.x / (float)this.m_CurrentSpriteAsset.spriteSheet.width, (float)this.m_CachedTextElement.glyph.glyphRect.y / (float)this.m_CurrentSpriteAsset.spriteSheet.height);
			Vector2 vector2 = new Vector2(vector.x, (float)(this.m_CachedTextElement.glyph.glyphRect.y + this.m_CachedTextElement.glyph.glyphRect.height) / (float)this.m_CurrentSpriteAsset.spriteSheet.height);
			Vector2 vector3 = new Vector2((float)(this.m_CachedTextElement.glyph.glyphRect.x + this.m_CachedTextElement.glyph.glyphRect.width) / (float)this.m_CurrentSpriteAsset.spriteSheet.width, vector2.y);
			Vector2 uv = new Vector2(vector3.x, vector.y);
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomLeft.uv = vector;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopLeft.uv = vector2;
			textInfo.textElementInfo[this.m_CharacterCount].vertexTopRight.uv = vector3;
			textInfo.textElementInfo[this.m_CharacterCount].vertexBottomRight.uv = uv;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0001472C File Offset: 0x0001292C
		private void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			TextSettings textSettings = generationSettings.textSettings;
			this.GetUnderlineSpecialCharacter(generationSettings);
			bool flag = this.m_Underline.character == null;
			if (flag)
			{
				bool displayWarnings = textSettings.displayWarnings;
				if (displayWarnings)
				{
					Debug.LogWarning("Unable to add underline since the primary Font Asset doesn't contain the underline character.");
				}
			}
			else
			{
				int materialIndex = this.m_Underline.materialIndex;
				int num = index + 12;
				bool flag2 = num > textInfo.meshInfo[materialIndex].vertices.Length;
				if (flag2)
				{
					textInfo.meshInfo[materialIndex].ResizeMeshInfo(num / 4);
				}
				start.y = Mathf.Min(start.y, end.y);
				end.y = Mathf.Min(start.y, end.y);
				GlyphMetrics metrics = this.m_Underline.character.glyph.metrics;
				GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
				float num2 = metrics.width / 2f * maxScale;
				bool flag3 = end.x - start.x < metrics.width * maxScale;
				if (flag3)
				{
					num2 = (end.x - start.x) / 2f;
				}
				float num3 = this.m_Padding * startScale / maxScale;
				float num4 = this.m_Padding * endScale / maxScale;
				float underlineThickness = this.m_Underline.fontAsset.faceInfo.underlineThickness;
				Vector3[] vertices = textInfo.meshInfo[0].vertices;
				vertices[index] = start + new Vector3(0f, 0f - (underlineThickness + this.m_Padding) * maxScale, 0f);
				vertices[index + 1] = start + new Vector3(0f, this.m_Padding * maxScale, 0f);
				vertices[index + 2] = vertices[index + 1] + new Vector3(num2, 0f, 0f);
				vertices[index + 3] = vertices[index] + new Vector3(num2, 0f, 0f);
				vertices[index + 4] = vertices[index + 3];
				vertices[index + 5] = vertices[index + 2];
				vertices[index + 6] = end + new Vector3(-num2, this.m_Padding * maxScale, 0f);
				vertices[index + 7] = end + new Vector3(-num2, -(underlineThickness + this.m_Padding) * maxScale, 0f);
				vertices[index + 8] = vertices[index + 7];
				vertices[index + 9] = vertices[index + 6];
				vertices[index + 10] = end + new Vector3(0f, this.m_Padding * maxScale, 0f);
				vertices[index + 11] = end + new Vector3(0f, -(underlineThickness + this.m_Padding) * maxScale, 0f);
				bool inverseYAxis = generationSettings.inverseYAxis;
				if (inverseYAxis)
				{
					Vector3 vector;
					vector.x = 0f;
					vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
					vector.z = 0f;
					vertices[index].y = vertices[index].y * -1f + vector.y;
					vertices[index + 1].y = vertices[index + 1].y * -1f + vector.y;
					vertices[index + 2].y = vertices[index + 2].y * -1f + vector.y;
					vertices[index + 3].y = vertices[index + 3].y * -1f + vector.y;
					vertices[index + 4].y = vertices[index + 4].y * -1f + vector.y;
					vertices[index + 5].y = vertices[index + 5].y * -1f + vector.y;
					vertices[index + 6].y = vertices[index + 6].y * -1f + vector.y;
					vertices[index + 7].y = vertices[index + 7].y * -1f + vector.y;
					vertices[index + 8].y = vertices[index + 8].y * -1f + vector.y;
					vertices[index + 9].y = vertices[index + 9].y * -1f + vector.y;
					vertices[index + 10].y = vertices[index + 10].y * -1f + vector.y;
					vertices[index + 11].y = vertices[index + 11].y * -1f + vector.y;
				}
				Vector2[] uvs = textInfo.meshInfo[materialIndex].uvs0;
				int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
				int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
				Vector2 vector2 = new Vector2(((float)glyphRect.x - num3) / (float)atlasWidth, ((float)glyphRect.y - this.m_Padding) / (float)atlasHeight);
				Vector2 vector3 = new Vector2(vector2.x, ((float)(glyphRect.y + glyphRect.height) + this.m_Padding) / (float)atlasHeight);
				Vector2 vector4 = new Vector2(((float)glyphRect.x - num3 + (float)glyphRect.width / 2f) / (float)atlasWidth, vector3.y);
				Vector2 vector5 = new Vector2(vector4.x, vector2.y);
				Vector2 vector6 = new Vector2(((float)glyphRect.x + num4 + (float)glyphRect.width / 2f) / (float)atlasWidth, vector3.y);
				Vector2 vector7 = new Vector2(vector6.x, vector2.y);
				Vector2 vector8 = new Vector2(((float)glyphRect.x + num4 + (float)glyphRect.width) / (float)atlasWidth, vector3.y);
				Vector2 vector9 = new Vector2(vector8.x, vector2.y);
				uvs[index] = vector2;
				uvs[1 + index] = vector3;
				uvs[2 + index] = vector4;
				uvs[3 + index] = vector5;
				uvs[4 + index] = new Vector2(vector4.x - vector4.x * 0.001f, vector2.y);
				uvs[5 + index] = new Vector2(vector4.x - vector4.x * 0.001f, vector3.y);
				uvs[6 + index] = new Vector2(vector4.x + vector4.x * 0.001f, vector3.y);
				uvs[7 + index] = new Vector2(vector4.x + vector4.x * 0.001f, vector2.y);
				uvs[8 + index] = vector7;
				uvs[9 + index] = vector6;
				uvs[10 + index] = vector8;
				uvs[11 + index] = vector9;
				float x = (vertices[index + 2].x - start.x) / (end.x - start.x);
				float scale = Mathf.Abs(sdfScale);
				Vector2[] uvs2 = textInfo.meshInfo[materialIndex].uvs2;
				uvs2[index] = TextGeneratorUtilities.PackUV(0f, 0f, scale);
				uvs2[1 + index] = TextGeneratorUtilities.PackUV(0f, 1f, scale);
				uvs2[2 + index] = TextGeneratorUtilities.PackUV(x, 1f, scale);
				uvs2[3 + index] = TextGeneratorUtilities.PackUV(x, 0f, scale);
				float x2 = (vertices[index + 4].x - start.x) / (end.x - start.x);
				x = (vertices[index + 6].x - start.x) / (end.x - start.x);
				uvs2[4 + index] = TextGeneratorUtilities.PackUV(x2, 0f, scale);
				uvs2[5 + index] = TextGeneratorUtilities.PackUV(x2, 1f, scale);
				uvs2[6 + index] = TextGeneratorUtilities.PackUV(x, 1f, scale);
				uvs2[7 + index] = TextGeneratorUtilities.PackUV(x, 0f, scale);
				x2 = (vertices[index + 8].x - start.x) / (end.x - start.x);
				uvs2[8 + index] = TextGeneratorUtilities.PackUV(x2, 0f, scale);
				uvs2[9 + index] = TextGeneratorUtilities.PackUV(x2, 1f, scale);
				uvs2[10 + index] = TextGeneratorUtilities.PackUV(1f, 1f, scale);
				uvs2[11 + index] = TextGeneratorUtilities.PackUV(1f, 0f, scale);
				underlineColor.a = ((this.m_FontColor32.a < underlineColor.a) ? this.m_FontColor32.a : underlineColor.a);
				Color32[] colors = textInfo.meshInfo[materialIndex].colors32;
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
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00015214 File Offset: 0x00013414
		private void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			TextSettings textSettings = generationSettings.textSettings;
			bool flag = this.m_Underline.character == null;
			if (flag)
			{
				this.GetUnderlineSpecialCharacter(generationSettings);
				bool flag2 = this.m_Underline.character == null;
				if (flag2)
				{
					bool displayWarnings = textSettings.displayWarnings;
					if (displayWarnings)
					{
						Debug.LogWarning("Unable to add highlight since the primary Font Asset doesn't contain the underline character.");
					}
					return;
				}
			}
			int materialIndex = this.m_Underline.materialIndex;
			int num = index + 4;
			bool flag3 = num > textInfo.meshInfo[materialIndex].vertices.Length;
			if (flag3)
			{
				textInfo.meshInfo[materialIndex].ResizeMeshInfo(num / 4);
			}
			Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
			vertices[index] = start;
			vertices[index + 1] = new Vector3(start.x, end.y, 0f);
			vertices[index + 2] = end;
			vertices[index + 3] = new Vector3(end.x, start.y, 0f);
			bool inverseYAxis = generationSettings.inverseYAxis;
			if (inverseYAxis)
			{
				Vector3 vector;
				vector.x = 0f;
				vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
				vector.z = 0f;
				vertices[index].y = vertices[index].y * -1f + vector.y;
				vertices[index + 1].y = vertices[index + 1].y * -1f + vector.y;
				vertices[index + 2].y = vertices[index + 2].y * -1f + vector.y;
				vertices[index + 3].y = vertices[index + 3].y * -1f + vector.y;
			}
			Vector2[] uvs = textInfo.meshInfo[materialIndex].uvs0;
			int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
			int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
			GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
			Vector2 a = new Vector2(((float)glyphRect.x + (float)(glyphRect.width / 2)) / (float)atlasWidth, ((float)glyphRect.y + (float)glyphRect.height / 2f) / (float)atlasHeight);
			uvs[index] = a + new Vector2(-0.0001f, -0.0001f);
			uvs[1 + index] = a + new Vector2(-0.0001f, 0.0001f);
			uvs[2 + index] = a + new Vector2(0.0001f, 0.0001f);
			uvs[3 + index] = a + new Vector2(0.0001f, -0.0001f);
			Vector2[] uvs2 = textInfo.meshInfo[materialIndex].uvs2;
			Vector2 vector2 = new Vector2(0f, 1f);
			uvs2[index] = vector2;
			uvs2[1 + index] = vector2;
			uvs2[2 + index] = vector2;
			uvs2[3 + index] = vector2;
			highlightColor.a = ((this.m_FontColor32.a < highlightColor.a) ? this.m_FontColor32.a : highlightColor.a);
			Color32[] colors = textInfo.meshInfo[materialIndex].colors32;
			colors[index] = highlightColor;
			colors[1 + index] = highlightColor;
			colors[2 + index] = highlightColor;
			colors[3 + index] = highlightColor;
			index += 4;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000155F9 File Offset: 0x000137F9
		private static void ClearMesh(bool updateMesh, TextInfo textInfo)
		{
			textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00015604 File Offset: 0x00013804
		private void EnableMasking()
		{
			this.m_IsMaskingEnabled = true;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0001560E File Offset: 0x0001380E
		private void DisableMasking()
		{
			this.m_IsMaskingEnabled = false;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00015618 File Offset: 0x00013818
		private void SetArraySizes(int[] chars, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			TextSettings textSettings = generationSettings.textSettings;
			int num = 0;
			this.m_TotalCharacterCount = 0;
			this.m_IsUsingBold = false;
			this.m_IsParsingText = false;
			this.m_TagNoParsing = false;
			this.m_FontStyleInternal = generationSettings.fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_CurrentFontAsset = generationSettings.fontAsset;
			this.m_CurrentMaterial = generationSettings.material;
			this.m_CurrentMaterialIndex = 0;
			this.m_MaterialReferenceStack.SetDefault(new MaterialReference(this.m_CurrentMaterialIndex, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
			this.m_MaterialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
			bool flag = textInfo == null;
			if (flag)
			{
				textInfo = new TextInfo();
			}
			this.m_TextElementType = TextElementType.Character;
			int num2 = 0;
			while (num2 < chars.Length && chars[num2] != 0)
			{
				bool flag2 = textInfo.textElementInfo == null || this.m_TotalCharacterCount >= textInfo.textElementInfo.Length;
				if (flag2)
				{
					TextInfo.Resize<TextElementInfo>(ref textInfo.textElementInfo, this.m_TotalCharacterCount + 1, true);
				}
				int num3 = chars[num2];
				bool flag3 = generationSettings.richText && num3 == 60;
				if (!flag3)
				{
					goto IL_2BC;
				}
				int currentMaterialIndex = this.m_CurrentMaterialIndex;
				int num4;
				bool flag4 = this.ValidateHtmlTag(chars, num2 + 1, out num4, generationSettings, textInfo);
				if (!flag4)
				{
					goto IL_2BC;
				}
				num2 = num4;
				bool flag5 = (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold;
				if (flag5)
				{
					this.m_IsUsingBold = true;
				}
				bool flag6 = this.m_TextElementType == TextElementType.Sprite;
				if (flag6)
				{
					MaterialReference[] materialReferences = this.m_MaterialReferences;
					int currentMaterialIndex2 = this.m_CurrentMaterialIndex;
					materialReferences[currentMaterialIndex2].referenceCount = materialReferences[currentMaterialIndex2].referenceCount + 1;
					textInfo.textElementInfo[this.m_TotalCharacterCount].character = (char)(57344 + this.m_SpriteIndex);
					textInfo.textElementInfo[this.m_TotalCharacterCount].spriteIndex = this.m_SpriteIndex;
					textInfo.textElementInfo[this.m_TotalCharacterCount].fontAsset = this.m_CurrentFontAsset;
					textInfo.textElementInfo[this.m_TotalCharacterCount].spriteAsset = this.m_CurrentSpriteAsset;
					textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
					textInfo.textElementInfo[this.m_TotalCharacterCount].textElement = this.m_CurrentSpriteAsset.spriteCharacterTable[this.m_SpriteIndex];
					textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = this.m_TextElementType;
					this.m_TextElementType = TextElementType.Character;
					this.m_CurrentMaterialIndex = currentMaterialIndex;
					num++;
					this.m_TotalCharacterCount++;
				}
				IL_8D6:
				num2++;
				continue;
				IL_2BC:
				bool flag7 = false;
				FontAsset currentFontAsset = this.m_CurrentFontAsset;
				Material currentMaterial = this.m_CurrentMaterial;
				int currentMaterialIndex3 = this.m_CurrentMaterialIndex;
				bool flag8 = this.m_TextElementType == TextElementType.Character;
				if (flag8)
				{
					bool flag9 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
					if (flag9)
					{
						bool flag10 = char.IsLower((char)num3);
						if (flag10)
						{
							num3 = (int)char.ToUpper((char)num3);
						}
					}
					else
					{
						bool flag11 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
						if (flag11)
						{
							bool flag12 = char.IsUpper((char)num3);
							if (flag12)
							{
								num3 = (int)char.ToLower((char)num3);
							}
						}
						else
						{
							bool flag13 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
							if (flag13)
							{
								bool flag14 = char.IsLower((char)num3);
								if (flag14)
								{
									num3 = (int)char.ToUpper((char)num3);
								}
							}
						}
					}
				}
				bool isUsingAlternateTypeface;
				TextElement textElement = this.GetTextElement(generationSettings, (uint)num3, this.m_CurrentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
				bool flag15 = textElement == null;
				if (flag15)
				{
					int num5 = num3;
					num3 = ((textSettings.missingCharacterUnicode == 0) ? 9633 : textSettings.missingCharacterUnicode);
					textElement = FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
					bool flag16 = textElement == null;
					if (flag16)
					{
						bool flag17 = textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0;
						if (flag17)
						{
							textElement = FontAssetUtilities.GetCharacterFromFontAssets((uint)num3, this.m_CurrentFontAsset, textSettings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
						}
					}
					bool flag18 = textElement == null;
					if (flag18)
					{
						bool flag19 = textSettings.defaultFontAsset != null;
						if (flag19)
						{
							textElement = FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, textSettings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
						}
					}
					bool flag20 = textElement == null;
					if (flag20)
					{
						num3 = 32;
						textElement = FontAssetUtilities.GetCharacterFromFontAsset((uint)num3, this.m_CurrentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternateTypeface);
						bool displayWarnings = textSettings.displayWarnings;
						if (displayWarnings)
						{
							string message = (num5 > 65535) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by a space.", num5, generationSettings.fontAsset.name) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by a space.", num5, generationSettings.fontAsset.name);
							Debug.LogWarning(message);
						}
					}
				}
				bool flag21 = textElement.elementType == TextElementType.Character;
				if (flag21)
				{
					bool flag22 = textElement.textAsset.instanceID != this.m_CurrentFontAsset.instanceID;
					if (flag22)
					{
						flag7 = true;
						this.m_CurrentFontAsset = (textElement.textAsset as FontAsset);
					}
				}
				textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = TextElementType.Character;
				textInfo.textElementInfo[this.m_TotalCharacterCount].textElement = textElement;
				textInfo.textElementInfo[this.m_TotalCharacterCount].isUsingAlternateTypeface = isUsingAlternateTypeface;
				textInfo.textElementInfo[this.m_TotalCharacterCount].character = (char)num3;
				textInfo.textElementInfo[this.m_TotalCharacterCount].fontAsset = this.m_CurrentFontAsset;
				bool flag23 = textElement.elementType == TextElementType.Sprite;
				if (flag23)
				{
					SpriteAsset spriteAsset = textElement.textAsset as SpriteAsset;
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(spriteAsset.material, spriteAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					MaterialReference[] materialReferences2 = this.m_MaterialReferences;
					int currentMaterialIndex4 = this.m_CurrentMaterialIndex;
					materialReferences2[currentMaterialIndex4].referenceCount = materialReferences2[currentMaterialIndex4].referenceCount + 1;
					textInfo.textElementInfo[this.m_TotalCharacterCount].elementType = TextElementType.Sprite;
					textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
					textInfo.textElementInfo[this.m_TotalCharacterCount].spriteAsset = spriteAsset;
					textInfo.textElementInfo[this.m_TotalCharacterCount].spriteIndex = (int)textElement.glyphIndex;
					this.m_TextElementType = TextElementType.Character;
					this.m_CurrentMaterialIndex = currentMaterialIndex3;
					num++;
					this.m_TotalCharacterCount++;
					goto IL_8D6;
				}
				bool flag24 = flag7 && this.m_CurrentFontAsset.instanceID != generationSettings.fontAsset.instanceID;
				if (flag24)
				{
					bool matchMaterialPreset = textSettings.matchMaterialPreset;
					if (matchMaterialPreset)
					{
						this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentMaterial, this.m_CurrentFontAsset.material);
					}
					else
					{
						this.m_CurrentMaterial = this.m_CurrentFontAsset.material;
					}
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
				}
				bool flag25 = textElement != null && textElement.glyph.atlasIndex > 0;
				if (flag25)
				{
					this.m_CurrentMaterial = MaterialManager.GetFallbackMaterial(this.m_CurrentFontAsset, this.m_CurrentMaterial, textElement.glyph.atlasIndex);
					this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(this.m_CurrentMaterial, this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
					flag7 = true;
				}
				bool flag26 = !char.IsWhiteSpace((char)num3) && num3 != 8203;
				if (flag26)
				{
					bool flag27 = this.m_MaterialReferences[this.m_CurrentMaterialIndex].referenceCount < 16383;
					if (flag27)
					{
						MaterialReference[] materialReferences3 = this.m_MaterialReferences;
						int currentMaterialIndex5 = this.m_CurrentMaterialIndex;
						materialReferences3[currentMaterialIndex5].referenceCount = materialReferences3[currentMaterialIndex5].referenceCount + 1;
					}
					else
					{
						this.m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_CurrentMaterial), this.m_CurrentFontAsset, ref this.m_MaterialReferences, this.m_MaterialReferenceIndexLookup);
						MaterialReference[] materialReferences4 = this.m_MaterialReferences;
						int currentMaterialIndex6 = this.m_CurrentMaterialIndex;
						materialReferences4[currentMaterialIndex6].referenceCount = materialReferences4[currentMaterialIndex6].referenceCount + 1;
					}
				}
				textInfo.textElementInfo[this.m_TotalCharacterCount].material = this.m_CurrentMaterial;
				textInfo.textElementInfo[this.m_TotalCharacterCount].materialReferenceIndex = this.m_CurrentMaterialIndex;
				this.m_MaterialReferences[this.m_CurrentMaterialIndex].isFallbackMaterial = flag7;
				bool flag28 = flag7;
				if (flag28)
				{
					this.m_MaterialReferences[this.m_CurrentMaterialIndex].fallbackMaterial = currentMaterial;
					this.m_CurrentFontAsset = currentFontAsset;
					this.m_CurrentMaterial = currentMaterial;
					this.m_CurrentMaterialIndex = currentMaterialIndex3;
				}
				this.m_TotalCharacterCount++;
				goto IL_8D6;
			}
			bool isCalculatingPreferredValues = this.m_IsCalculatingPreferredValues;
			if (isCalculatingPreferredValues)
			{
				this.m_IsCalculatingPreferredValues = false;
			}
			else
			{
				textInfo.spriteCount = num;
				int num6 = textInfo.materialCount = this.m_MaterialReferenceIndexLookup.Count;
				bool flag29 = num6 > textInfo.meshInfo.Length;
				if (flag29)
				{
					TextInfo.Resize<MeshInfo>(ref textInfo.meshInfo, num6, false);
				}
				bool flag30 = textInfo.textElementInfo.Length - this.m_TotalCharacterCount > 256;
				if (flag30)
				{
					TextInfo.Resize<TextElementInfo>(ref textInfo.textElementInfo, Mathf.Max(this.m_TotalCharacterCount + 1, 256), true);
				}
				for (int i = 0; i < num6; i++)
				{
					int referenceCount = this.m_MaterialReferences[i].referenceCount;
					bool flag31 = textInfo.meshInfo[i].vertices == null || textInfo.meshInfo[i].vertices.Length < referenceCount * 4;
					if (flag31)
					{
						bool flag32 = textInfo.meshInfo[i].vertices == null;
						if (flag32)
						{
							textInfo.meshInfo[i] = new MeshInfo(referenceCount + 1);
						}
						else
						{
							textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount));
						}
					}
					else
					{
						bool flag33 = textInfo.meshInfo[i].vertices.Length - referenceCount * 4 > 1024;
						if (flag33)
						{
							textInfo.meshInfo[i].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.Max(Mathf.NextPowerOfTwo(referenceCount), 256));
						}
					}
					textInfo.meshInfo[i].material = this.m_MaterialReferences[i].material;
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00016100 File Offset: 0x00014300
		internal TextElement GetTextElement(TextGenerationSettings generationSettings, uint unicode, FontAsset fontAsset, FontStyles fontStyle, TextFontWeight fontWeight, out bool isUsingAlternativeTypeface)
		{
			TextSettings textSettings = generationSettings.textSettings;
			Character character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			bool flag = character != null;
			TextElement result;
			if (flag)
			{
				result = character;
			}
			else
			{
				bool flag2 = fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0;
				if (flag2)
				{
					character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
				}
				bool flag3 = character != null;
				if (flag3)
				{
					fontAsset.AddCharacterToLookupCache(unicode, character);
					result = character;
				}
				else
				{
					bool flag4 = fontAsset.instanceID != generationSettings.fontAsset.instanceID;
					if (flag4)
					{
						character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, generationSettings.fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
						bool flag5 = character != null;
						if (flag5)
						{
							this.m_CurrentMaterialIndex = 0;
							this.m_CurrentMaterial = this.m_MaterialReferences[0].material;
							fontAsset.AddCharacterToLookupCache(unicode, character);
							return character;
						}
						bool flag6 = generationSettings.fontAsset.m_FallbackFontAssetTable != null && generationSettings.fontAsset.m_FallbackFontAssetTable.Count > 0;
						if (flag6)
						{
							character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, generationSettings.fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
						}
						bool flag7 = character != null;
						if (flag7)
						{
							fontAsset.AddCharacterToLookupCache(unicode, character);
							return character;
						}
					}
					bool flag8 = generationSettings.spriteAsset != null;
					if (flag8)
					{
						SpriteCharacter spriteCharacterFromSpriteAsset = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, generationSettings.spriteAsset, true);
						bool flag9 = spriteCharacterFromSpriteAsset != null;
						if (flag9)
						{
							return spriteCharacterFromSpriteAsset;
						}
					}
					bool flag10 = textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0;
					if (flag10)
					{
						character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, textSettings.fallbackFontAssets, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
					}
					bool flag11 = character != null;
					if (flag11)
					{
						fontAsset.AddCharacterToLookupCache(unicode, character);
						result = character;
					}
					else
					{
						bool flag12 = textSettings.defaultFontAsset != null;
						if (flag12)
						{
							character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, textSettings.defaultFontAsset, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
						}
						bool flag13 = character != null;
						if (flag13)
						{
							fontAsset.AddCharacterToLookupCache(unicode, character);
							result = character;
						}
						else
						{
							bool flag14 = textSettings.defaultSpriteAsset != null;
							if (flag14)
							{
								SpriteCharacter spriteCharacterFromSpriteAsset2 = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, textSettings.defaultSpriteAsset, true);
								bool flag15 = spriteCharacterFromSpriteAsset2 != null;
								if (flag15)
								{
									return spriteCharacterFromSpriteAsset2;
								}
							}
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00016348 File Offset: 0x00014548
		private void ComputeMarginSize(Rect rect, Vector4 margins)
		{
			this.m_MarginWidth = rect.width - margins.x - margins.z;
			this.m_MarginHeight = rect.height - margins.y - margins.w;
			this.m_RectTransformCorners[0].x = 0f;
			this.m_RectTransformCorners[0].y = 0f;
			this.m_RectTransformCorners[1].x = 0f;
			this.m_RectTransformCorners[1].y = rect.height;
			this.m_RectTransformCorners[2].x = rect.width;
			this.m_RectTransformCorners[2].y = rect.height;
			this.m_RectTransformCorners[3].x = rect.width;
			this.m_RectTransformCorners[3].y = 0f;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00016444 File Offset: 0x00014644
		protected void GetSpecialCharacters(TextGenerationSettings generationSettings)
		{
			this.GetEllipsisSpecialCharacter(generationSettings);
			this.GetUnderlineSpecialCharacter(generationSettings);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00016458 File Offset: 0x00014658
		protected void GetEllipsisSpecialCharacter(TextGenerationSettings generationSettings)
		{
			FontAsset fontAsset = generationSettings.fontAsset;
			TextSettings textSettings = generationSettings.textSettings;
			bool flag;
			Character character = FontAssetUtilities.GetCharacterFromFontAsset(8230U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			bool flag2 = character == null;
			if (flag2)
			{
				bool flag3 = fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0;
				if (flag3)
				{
					character = FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, fontAsset.m_FallbackFontAssetTable, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
				}
			}
			bool flag4 = character == null;
			if (flag4)
			{
				bool flag5 = textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0;
				if (flag5)
				{
					character = FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, textSettings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
				}
			}
			bool flag6 = character == null;
			if (flag6)
			{
				bool flag7 = textSettings.defaultFontAsset != null;
				if (flag7)
				{
					character = FontAssetUtilities.GetCharacterFromFontAsset(8230U, textSettings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
				}
			}
			bool flag8 = character != null;
			if (flag8)
			{
				this.m_Ellipsis = new TextGenerator.SpecialCharacter(character, 0);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001657C File Offset: 0x0001477C
		protected void GetUnderlineSpecialCharacter(TextGenerationSettings generationSettings)
		{
			FontAsset fontAsset = generationSettings.fontAsset;
			TextSettings textSettings = generationSettings.textSettings;
			bool flag;
			Character characterFromFontAsset = FontAssetUtilities.GetCharacterFromFontAsset(95U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag);
			bool flag2 = characterFromFontAsset != null;
			if (flag2)
			{
				this.m_Underline = new TextGenerator.SpecialCharacter(characterFromFontAsset, 0);
			}
			else
			{
				bool displayWarnings = textSettings.displayWarnings;
				if (displayWarnings)
				{
					Debug.LogWarning("The character used for Underline is not available in font asset [" + fontAsset.name + "].");
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000165F4 File Offset: 0x000147F4
		private float GetPaddingForMaterial(Material material, bool extraPadding)
		{
			TextShaderUtilities.GetShaderPropertyIDs();
			bool flag = material == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				this.m_Padding = TextShaderUtilities.GetPadding(material, extraPadding, this.m_IsUsingBold);
				this.m_IsMaskingEnabled = TextShaderUtilities.IsMaskingEnabled(material);
				this.m_IsSdfShader = material.HasProperty(TextShaderUtilities.ID_WeightNormal);
				result = this.m_Padding;
			}
			return result;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00016658 File Offset: 0x00014858
		private float GetPreferredWidthInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.textSettings == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float defaultFontSize = generationSettings.autoSize ? generationSettings.fontSizeMax : this.m_FontSize;
				this.m_MinFontSize = generationSettings.fontSizeMin;
				this.m_MaxFontSize = generationSettings.fontSizeMax;
				this.m_CharWidthAdjDelta = 0f;
				Vector2 largePositiveVector = TextGeneratorUtilities.largePositiveVector2;
				this.m_RecursiveCount = 0;
				float x = this.CalculatePreferredValues(defaultFontSize, largePositiveVector, true, generationSettings, textInfo).x;
				result = x;
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000166E0 File Offset: 0x000148E0
		private float GetPreferredHeightInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.textSettings == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float defaultFontSize = generationSettings.autoSize ? generationSettings.fontSizeMax : this.m_FontSize;
				this.m_MinFontSize = generationSettings.fontSizeMin;
				this.m_MaxFontSize = generationSettings.fontSizeMax;
				this.m_CharWidthAdjDelta = 0f;
				Vector2 marginSize = new Vector2((this.m_MarginWidth != 0f) ? this.m_MarginWidth : 32767f, 32767f);
				this.m_RecursiveCount = 0;
				float y = this.CalculatePreferredValues(defaultFontSize, marginSize, !generationSettings.autoSize, generationSettings, textInfo).y;
				result = y;
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00016794 File Offset: 0x00014994
		private Vector2 GetPreferredValuesInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			bool flag = generationSettings.textSettings == null;
			Vector2 result;
			if (flag)
			{
				result = Vector2.zero;
			}
			else
			{
				float defaultFontSize = generationSettings.autoSize ? generationSettings.fontSizeMax : this.m_FontSize;
				this.m_MinFontSize = generationSettings.fontSizeMin;
				this.m_MaxFontSize = generationSettings.fontSizeMax;
				this.m_CharWidthAdjDelta = 0f;
				Vector2 marginSize = new Vector2((this.m_MarginWidth != 0f) ? this.m_MarginWidth : 32767f, 32767f);
				this.m_RecursiveCount = 0;
				result = this.CalculatePreferredValues(defaultFontSize, marginSize, !generationSettings.autoSize, generationSettings, textInfo);
			}
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00016838 File Offset: 0x00014A38
		protected virtual Vector2 CalculatePreferredValues(float defaultFontSize, Vector2 marginSize, bool ignoreTextAutoSizing, TextGenerationSettings generationSettings, TextInfo textInfo)
		{
			Profiler.BeginSample("TextGenerator.CalculatePreferredValues");
			bool flag = generationSettings.fontAsset == null || generationSettings.fontAsset.characterLookupTable == null;
			Vector2 result;
			if (flag)
			{
				result = Vector2.zero;
			}
			else
			{
				bool flag2 = this.m_CharBuffer == null || this.m_CharBuffer.Length == 0 || this.m_CharBuffer[0] == 0;
				if (flag2)
				{
					result = Vector2.zero;
				}
				else
				{
					this.m_CurrentFontAsset = generationSettings.fontAsset;
					this.m_CurrentMaterial = generationSettings.material;
					this.m_CurrentMaterialIndex = 0;
					this.m_MaterialReferenceStack.SetDefault(new MaterialReference(0, this.m_CurrentFontAsset, null, this.m_CurrentMaterial, this.m_Padding));
					int totalCharacterCount = this.m_TotalCharacterCount;
					bool flag3 = this.m_InternalTextElementInfo == null || totalCharacterCount > this.m_InternalTextElementInfo.Length;
					if (flag3)
					{
						this.m_InternalTextElementInfo = new TextElementInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
					}
					float num = this.m_FontScale = defaultFontSize / (float)generationSettings.fontAsset.faceInfo.pointSize * generationSettings.fontAsset.faceInfo.scale;
					float num2 = num;
					this.m_FontScaleMultiplier = 1f;
					this.m_CurrentFontSize = defaultFontSize;
					this.m_SizeStack.SetDefault(this.m_CurrentFontSize);
					this.m_FontStyleInternal = generationSettings.fontStyle;
					this.m_LineJustification = generationSettings.textAlignment;
					this.m_LineJustificationStack.SetDefault(this.m_LineJustification);
					this.m_BaselineOffset = 0f;
					this.m_BaselineOffsetStack.Clear();
					this.m_LineOffset = 0f;
					this.m_LineHeight = -32767f;
					float num3 = this.m_CurrentFontAsset.faceInfo.lineHeight - (this.m_CurrentFontAsset.faceInfo.ascentLine - this.m_CurrentFontAsset.faceInfo.descentLine);
					this.m_CSpacing = 0f;
					this.m_MonoSpacing = 0f;
					this.m_XAdvance = 0f;
					float a = 0f;
					this.m_TagLineIndent = 0f;
					this.m_TagIndent = 0f;
					this.m_IndentStack.SetDefault(0f);
					this.m_TagNoParsing = false;
					this.m_CharacterCount = 0;
					this.m_FirstCharacterOfLine = 0;
					this.m_MaxLineAscender = -32767f;
					this.m_MaxLineDescender = 32767f;
					this.m_LineNumber = 0;
					TextSettings textSettings = generationSettings.textSettings;
					float x = marginSize.x;
					this.m_MarginLeft = 0f;
					this.m_MarginRight = 0f;
					this.m_Width = -1f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					this.m_IsCalculatingPreferredValues = true;
					this.m_MaxAscender = 0f;
					this.m_MaxDescender = 0f;
					bool flag4 = true;
					bool flag5 = false;
					WordWrapState wordWrapState = default(WordWrapState);
					this.SaveWordWrappingState(ref wordWrapState, 0, 0, textInfo);
					WordWrapState wordWrapState2 = default(WordWrapState);
					int num7 = 0;
					this.m_RecursiveCount++;
					int num8 = 0;
					while (this.m_CharBuffer[num8] != 0)
					{
						int num9 = this.m_CharBuffer[num8];
						this.m_TextElementType = textInfo.textElementInfo[this.m_CharacterCount].elementType;
						this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
						this.m_CurrentFontAsset = this.m_MaterialReferences[this.m_CurrentMaterialIndex].fontAsset;
						int currentMaterialIndex = this.m_CurrentMaterialIndex;
						bool flag6 = generationSettings.richText && num9 == 60;
						if (flag6)
						{
							this.m_IsParsingText = true;
							this.m_TextElementType = TextElementType.Character;
							int num10;
							bool flag7 = this.ValidateHtmlTag(this.m_CharBuffer, num8 + 1, out num10, generationSettings, textInfo);
							if (flag7)
							{
								num8 = num10;
								bool flag8 = this.m_TextElementType == TextElementType.Character;
								if (flag8)
								{
									goto IL_15CC;
								}
							}
							goto IL_3DF;
						}
						goto IL_3DF;
						IL_15CC:
						num8++;
						continue;
						IL_3DF:
						this.m_IsParsingText = false;
						bool isUsingAlternateTypeface = textInfo.textElementInfo[this.m_CharacterCount].isUsingAlternateTypeface;
						float num11 = 1f;
						bool flag9 = this.m_TextElementType == TextElementType.Character;
						if (flag9)
						{
							bool flag10 = (this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase;
							if (flag10)
							{
								bool flag11 = char.IsLower((char)num9);
								if (flag11)
								{
									num9 = (int)char.ToUpper((char)num9);
								}
							}
							else
							{
								bool flag12 = (this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase;
								if (flag12)
								{
									bool flag13 = char.IsUpper((char)num9);
									if (flag13)
									{
										num9 = (int)char.ToLower((char)num9);
									}
								}
								else
								{
									bool flag14 = (this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps;
									if (flag14)
									{
										bool flag15 = char.IsLower((char)num9);
										if (flag15)
										{
											num11 = 0.8f;
											num9 = (int)char.ToUpper((char)num9);
										}
									}
								}
							}
						}
						bool flag16 = this.m_TextElementType == TextElementType.Sprite;
						if (flag16)
						{
							SpriteCharacter spriteCharacter = (SpriteCharacter)textInfo.textElementInfo[this.m_CharacterCount].textElement;
							this.m_CurrentSpriteAsset = (spriteCharacter.textAsset as SpriteAsset);
							this.m_SpriteIndex = (int)spriteCharacter.glyphIndex;
							bool flag17 = spriteCharacter == null;
							if (flag17)
							{
								goto IL_15CC;
							}
							bool flag18 = num9 == 60;
							if (flag18)
							{
								num9 = 57344 + this.m_SpriteIndex;
							}
							this.m_CurrentFontAsset = generationSettings.fontAsset;
							float num12 = this.m_CurrentFontSize / (float)generationSettings.fontAsset.faceInfo.pointSize * generationSettings.fontAsset.faceInfo.scale;
							num2 = generationSettings.fontAsset.faceInfo.ascentLine / spriteCharacter.glyph.metrics.height * spriteCharacter.scale * num12;
							this.m_CachedTextElement = spriteCharacter;
							this.m_InternalTextElementInfo[this.m_CharacterCount].elementType = TextElementType.Sprite;
							this.m_InternalTextElementInfo[this.m_CharacterCount].scale = num12;
							this.m_CurrentMaterialIndex = currentMaterialIndex;
						}
						else
						{
							bool flag19 = this.m_TextElementType == TextElementType.Character;
							if (flag19)
							{
								this.m_CachedTextElement = textInfo.textElementInfo[this.m_CharacterCount].textElement;
								bool flag20 = this.m_CachedTextElement == null;
								if (flag20)
								{
									goto IL_15CC;
								}
								this.m_CurrentMaterialIndex = textInfo.textElementInfo[this.m_CharacterCount].materialReferenceIndex;
								this.m_FontScale = this.m_CurrentFontSize * num11 / (float)this.m_CurrentFontAsset.faceInfo.pointSize * this.m_CurrentFontAsset.faceInfo.scale;
								num2 = this.m_FontScale * this.m_FontScaleMultiplier * this.m_CachedTextElement.scale;
								this.m_InternalTextElementInfo[this.m_CharacterCount].elementType = TextElementType.Character;
							}
						}
						float num13 = num2;
						bool flag21 = num9 == 173;
						if (flag21)
						{
							num2 = 0f;
						}
						this.m_InternalTextElementInfo[this.m_CharacterCount].character = (char)num9;
						GlyphValueRecord a2 = default(GlyphValueRecord);
						float characterSpacing = generationSettings.characterSpacing;
						bool enableKerning = generationSettings.enableKerning;
						if (enableKerning)
						{
							uint glyphIndex = this.m_CachedTextElement.glyphIndex;
							bool flag22 = this.m_CharacterCount < totalCharacterCount - 1;
							if (flag22)
							{
								uint glyphIndex2 = textInfo.textElementInfo[this.m_CharacterCount + 1].textElement.glyphIndex;
								uint key = glyphIndex2 << 16 | glyphIndex;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								bool flag23 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out glyphPairAdjustmentRecord);
								if (flag23)
								{
									a2 = glyphPairAdjustmentRecord.firstAdjustmentRecord.glyphValueRecord;
								}
							}
							bool flag24 = this.m_CharacterCount >= 1;
							if (flag24)
							{
								uint glyphIndex3 = textInfo.textElementInfo[this.m_CharacterCount - 1].textElement.glyphIndex;
								uint key2 = glyphIndex << 16 | glyphIndex3;
								GlyphPairAdjustmentRecord glyphPairAdjustmentRecord;
								bool flag25 = this.m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out glyphPairAdjustmentRecord);
								if (flag25)
								{
									a2 += glyphPairAdjustmentRecord.secondAdjustmentRecord.glyphValueRecord;
								}
							}
						}
						float num14 = 0f;
						bool flag26 = this.m_MonoSpacing != 0f;
						if (flag26)
						{
							num14 = this.m_MonoSpacing / 2f - (this.m_CachedTextElement.glyph.metrics.width / 2f + this.m_CachedTextElement.glyph.metrics.horizontalBearingX) * num2;
							this.m_XAdvance += num14;
						}
						bool flag27 = this.m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold;
						float num15;
						if (flag27)
						{
							num15 = 1f + this.m_CurrentFontAsset.boldStyleSpacing * 0.01f;
						}
						else
						{
							num15 = 1f;
						}
						this.m_InternalTextElementInfo[this.m_CharacterCount].baseLine = 0f - this.m_LineOffset + this.m_BaselineOffset;
						float num16 = this.m_CurrentFontAsset.faceInfo.ascentLine * ((this.m_TextElementType == TextElementType.Character) ? (num2 / num11) : this.m_InternalTextElementInfo[this.m_CharacterCount].scale) + this.m_BaselineOffset;
						this.m_InternalTextElementInfo[this.m_CharacterCount].ascender = num16 - this.m_LineOffset;
						this.m_MaxLineAscender = ((num16 > this.m_MaxLineAscender) ? num16 : this.m_MaxLineAscender);
						float num17 = this.m_CurrentFontAsset.faceInfo.descentLine * ((this.m_TextElementType == TextElementType.Character) ? (num2 / num11) : this.m_InternalTextElementInfo[this.m_CharacterCount].scale) + this.m_BaselineOffset;
						this.m_MaxLineDescender = ((num17 < this.m_MaxLineDescender) ? num17 : this.m_MaxLineDescender);
						bool flag28 = (this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript;
						if (flag28)
						{
							float num18 = (num16 - this.m_BaselineOffset) / this.m_CurrentFontAsset.faceInfo.subscriptSize;
							num16 = this.m_MaxLineAscender;
							this.m_MaxLineAscender = ((num18 > this.m_MaxLineAscender) ? num18 : this.m_MaxLineAscender);
							float num19 = (num17 - this.m_BaselineOffset) / this.m_CurrentFontAsset.faceInfo.subscriptSize;
							this.m_MaxLineDescender = ((num19 < this.m_MaxLineDescender) ? num19 : this.m_MaxLineDescender);
						}
						bool flag29 = this.m_LineNumber == 0;
						if (flag29)
						{
							this.m_MaxAscender = ((this.m_MaxAscender > num16) ? this.m_MaxAscender : num16);
						}
						bool flag30 = num9 == 9 || (!char.IsWhiteSpace((char)num9) && num9 != 8203) || this.m_TextElementType == TextElementType.Sprite;
						if (flag30)
						{
							float num20 = (this.m_Width != -1f) ? Mathf.Min(x + 0.0001f - this.m_MarginLeft - this.m_MarginRight, this.m_Width) : (x + 0.0001f - this.m_MarginLeft - this.m_MarginRight);
							bool flag31 = (this.m_LineJustification & (TextAlignment)16) == (TextAlignment)16 || (this.m_LineJustification & (TextAlignment)8) == (TextAlignment)8;
							num6 = this.m_XAdvance + this.m_CachedTextElement.glyph.metrics.horizontalAdvance * (1f - this.m_CharWidthAdjDelta) * ((num9 != 173) ? num2 : num13);
							bool flag32 = num6 > num20 * (flag31 ? 1.05f : 1f);
							if (flag32)
							{
								bool flag33 = generationSettings.wordWrap && this.m_CharacterCount != this.m_FirstCharacterOfLine;
								if (flag33)
								{
									bool flag34 = num7 == wordWrapState2.previousWordBreak || flag4;
									if (flag34)
									{
										bool flag35 = !ignoreTextAutoSizing && this.m_CurrentFontSize > generationSettings.fontSizeMin;
										if (flag35)
										{
											bool flag36 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
											if (flag36)
											{
												this.m_RecursiveCount = 0;
												this.m_CharWidthAdjDelta += 0.01f;
												return this.CalculatePreferredValues(defaultFontSize, marginSize, false, generationSettings, textInfo);
											}
											this.m_MaxFontSize = defaultFontSize;
											defaultFontSize -= Mathf.Max((defaultFontSize - this.m_MinFontSize) / 2f, 0.05f);
											defaultFontSize = (float)((int)(Mathf.Max(defaultFontSize, generationSettings.fontSizeMin) * 20f + 0.5f)) / 20f;
											bool flag37 = this.m_RecursiveCount > 20;
											if (flag37)
											{
												return new Vector2(num4, num5);
											}
											return this.CalculatePreferredValues(defaultFontSize, marginSize, false, generationSettings, textInfo);
										}
										else
										{
											bool flag38 = !this.m_IsCharacterWrappingEnabled;
											if (flag38)
											{
												this.m_IsCharacterWrappingEnabled = true;
											}
											else
											{
												flag5 = true;
											}
										}
									}
									num8 = this.RestoreWordWrappingState(ref wordWrapState2, textInfo);
									num7 = num8;
									bool flag39 = this.m_CharBuffer[num8] == 173;
									if (flag39)
									{
										this.m_CharBuffer[num8] = 45;
										return this.CalculatePreferredValues(defaultFontSize, marginSize, true, generationSettings, textInfo);
									}
									bool flag40 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f;
									if (flag40)
									{
										float num21 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
										this.m_LineOffset += num21;
										wordWrapState2.lineOffset = this.m_LineOffset;
										wordWrapState2.previousLineAscender = this.m_MaxLineAscender;
									}
									float num22 = this.m_MaxLineAscender - this.m_LineOffset;
									float num23 = this.m_MaxLineDescender - this.m_LineOffset;
									this.m_MaxDescender = ((this.m_MaxDescender < num23) ? this.m_MaxDescender : num23);
									this.m_FirstCharacterOfLine = this.m_CharacterCount;
									num4 += this.m_XAdvance;
									bool wordWrap = generationSettings.wordWrap;
									if (wordWrap)
									{
										num5 = this.m_MaxAscender - this.m_MaxDescender;
									}
									else
									{
										num5 = Mathf.Max(num5, num22 - num23);
									}
									this.SaveWordWrappingState(ref wordWrapState, num8, this.m_CharacterCount - 1, textInfo);
									this.m_LineNumber++;
									bool flag41 = this.m_LineHeight == -32767f;
									if (flag41)
									{
										float num24 = this.m_InternalTextElementInfo[this.m_CharacterCount].ascender - this.m_InternalTextElementInfo[this.m_CharacterCount].baseLine;
										float num25 = 0f - this.m_MaxLineDescender + num24 + (num3 + generationSettings.lineSpacing + this.m_LineSpacingDelta) * num;
										this.m_LineOffset += num25;
										this.m_StartOfLineAscender = num24;
									}
									else
									{
										this.m_LineOffset += this.m_LineHeight + generationSettings.lineSpacing * num;
									}
									this.m_MaxLineAscender = -32767f;
									this.m_MaxLineDescender = 32767f;
									this.m_XAdvance = 0f + this.m_TagIndent;
									goto IL_15CC;
								}
								else
								{
									bool flag42 = !ignoreTextAutoSizing && defaultFontSize > generationSettings.fontSizeMin;
									if (flag42)
									{
										bool flag43 = this.m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f;
										if (flag43)
										{
											this.m_RecursiveCount = 0;
											this.m_CharWidthAdjDelta += 0.01f;
											return this.CalculatePreferredValues(defaultFontSize, marginSize, false, generationSettings, textInfo);
										}
										this.m_MaxFontSize = defaultFontSize;
										defaultFontSize -= Mathf.Max((defaultFontSize - this.m_MinFontSize) / 2f, 0.05f);
										defaultFontSize = (float)((int)(Mathf.Max(defaultFontSize, generationSettings.fontSizeMin) * 20f + 0.5f)) / 20f;
										bool flag44 = this.m_RecursiveCount > 20;
										if (flag44)
										{
											return new Vector2(num4, num5);
										}
										return this.CalculatePreferredValues(defaultFontSize, marginSize, false, generationSettings, textInfo);
									}
								}
							}
						}
						bool flag45 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f && !this.m_IsNewPage;
						if (flag45)
						{
							float num26 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
							this.m_LineOffset += num26;
							this.m_StartOfLineAscender += num26;
							wordWrapState2.lineOffset = this.m_LineOffset;
							wordWrapState2.previousLineAscender = this.m_StartOfLineAscender;
						}
						bool flag46 = num9 == 9;
						if (flag46)
						{
							float num27 = this.m_CurrentFontAsset.faceInfo.tabWidth * (float)this.m_CurrentFontAsset.tabMultiple * num2;
							float num28 = Mathf.Ceil(this.m_XAdvance / num27) * num27;
							this.m_XAdvance = ((num28 > this.m_XAdvance) ? num28 : (this.m_XAdvance + num27));
						}
						else
						{
							bool flag47 = this.m_MonoSpacing != 0f;
							if (flag47)
							{
								this.m_XAdvance += (this.m_MonoSpacing - num14 + (generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing) * num2 + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
								bool flag48 = char.IsWhiteSpace((char)num9) || num9 == 8203;
								if (flag48)
								{
									this.m_XAdvance += generationSettings.wordSpacing * num2;
								}
							}
							else
							{
								this.m_XAdvance += ((this.m_CachedTextElement.glyph.metrics.horizontalAdvance * num15 + generationSettings.characterSpacing + this.m_CurrentFontAsset.regularStyleSpacing + a2.xAdvance) * num2 + this.m_CSpacing) * (1f - this.m_CharWidthAdjDelta);
								bool flag49 = char.IsWhiteSpace((char)num9) || num9 == 8203;
								if (flag49)
								{
									this.m_XAdvance += generationSettings.wordSpacing * num2;
								}
							}
						}
						bool flag50 = num9 == 13;
						if (flag50)
						{
							a = Mathf.Max(a, num4 + this.m_XAdvance);
							num4 = 0f;
							this.m_XAdvance = 0f + this.m_TagIndent;
						}
						bool flag51 = num9 == 10 || this.m_CharacterCount == totalCharacterCount - 1;
						if (flag51)
						{
							bool flag52 = this.m_LineNumber > 0 && !TextGeneratorUtilities.Approximately(this.m_MaxLineAscender, this.m_StartOfLineAscender) && this.m_LineHeight == -32767f;
							if (flag52)
							{
								float num29 = this.m_MaxLineAscender - this.m_StartOfLineAscender;
								this.m_LineOffset += num29;
							}
							float num30 = this.m_MaxLineDescender - this.m_LineOffset;
							this.m_MaxDescender = ((this.m_MaxDescender < num30) ? this.m_MaxDescender : num30);
							this.m_FirstCharacterOfLine = this.m_CharacterCount + 1;
							bool flag53 = num9 == 10 && this.m_CharacterCount != totalCharacterCount - 1;
							if (flag53)
							{
								a = Mathf.Max(a, num4 + num6);
								num4 = 0f;
							}
							else
							{
								num4 = Mathf.Max(a, num4 + num6);
							}
							num5 = this.m_MaxAscender - this.m_MaxDescender;
							bool flag54 = num9 == 10;
							if (flag54)
							{
								this.SaveWordWrappingState(ref wordWrapState, num8, this.m_CharacterCount, textInfo);
								this.SaveWordWrappingState(ref wordWrapState2, num8, this.m_CharacterCount, textInfo);
								this.m_LineNumber++;
								bool flag55 = this.m_LineHeight == -32767f;
								if (flag55)
								{
									float num25 = 0f - this.m_MaxLineDescender + num16 + (num3 + generationSettings.lineSpacing + generationSettings.paragraphSpacing + this.m_LineSpacingDelta) * num;
									this.m_LineOffset += num25;
								}
								else
								{
									this.m_LineOffset += this.m_LineHeight + (generationSettings.lineSpacing + generationSettings.paragraphSpacing) * num;
								}
								this.m_MaxLineAscender = -32767f;
								this.m_MaxLineDescender = 32767f;
								this.m_StartOfLineAscender = num16;
								this.m_XAdvance = 0f + this.m_TagLineIndent + this.m_TagIndent;
								this.m_CharacterCount++;
								goto IL_15CC;
							}
						}
						bool flag56 = generationSettings.wordWrap || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis;
						if (flag56)
						{
							bool flag57 = (char.IsWhiteSpace((char)num9) || num9 == 8203 || num9 == 45 || num9 == 173) && !this.m_IsNonBreakingSpace && num9 != 160 && num9 != 8209 && num9 != 8239 && num9 != 8288;
							if (flag57)
							{
								this.SaveWordWrappingState(ref wordWrapState2, num8, this.m_CharacterCount, textInfo);
								this.m_IsCharacterWrappingEnabled = false;
								flag4 = false;
							}
							else
							{
								bool flag58 = ((num9 > 4352 && num9 < 4607) || (num9 > 11904 && num9 < 40959) || (num9 > 43360 && num9 < 43391) || (num9 > 44032 && num9 < 55295) || (num9 > 63744 && num9 < 64255) || (num9 > 65072 && num9 < 65103) || (num9 > 65280 && num9 < 65519)) && !this.m_IsNonBreakingSpace;
								if (flag58)
								{
									bool flag59 = flag4 || flag5 || (!textSettings.lineBreakingRules.leadingCharactersLookup.Contains((uint)num9) && this.m_CharacterCount < totalCharacterCount - 1 && !textSettings.lineBreakingRules.followingCharactersLookup.Contains((uint)this.m_InternalTextElementInfo[this.m_CharacterCount + 1].character));
									if (flag59)
									{
										this.SaveWordWrappingState(ref wordWrapState2, num8, this.m_CharacterCount, textInfo);
										this.m_IsCharacterWrappingEnabled = false;
										flag4 = false;
									}
								}
								else
								{
									bool flag60 = flag4 || this.m_IsCharacterWrappingEnabled || flag5;
									if (flag60)
									{
										this.SaveWordWrappingState(ref wordWrapState2, num8, this.m_CharacterCount, textInfo);
									}
								}
							}
						}
						this.m_CharacterCount++;
						goto IL_15CC;
					}
					float num31 = this.m_MaxFontSize - this.m_MinFontSize;
					bool flag61 = !this.m_IsCharacterWrappingEnabled && !ignoreTextAutoSizing && num31 > 0.051f && defaultFontSize < generationSettings.fontSizeMax;
					if (flag61)
					{
						this.m_MinFontSize = defaultFontSize;
						defaultFontSize += Mathf.Max((this.m_MaxFontSize - defaultFontSize) / 2f, 0.05f);
						defaultFontSize = (float)((int)(Mathf.Min(defaultFontSize, generationSettings.fontSizeMax) * 20f + 0.5f)) / 20f;
						bool flag62 = this.m_RecursiveCount > 20;
						if (flag62)
						{
							result = new Vector2(num4, num5);
						}
						else
						{
							result = this.CalculatePreferredValues(defaultFontSize, marginSize, false, generationSettings, textInfo);
						}
					}
					else
					{
						this.m_IsCharacterWrappingEnabled = false;
						this.m_IsCalculatingPreferredValues = false;
						num4 += ((generationSettings.margins.x > 0f) ? generationSettings.margins.x : 0f);
						num4 += ((generationSettings.margins.z > 0f) ? generationSettings.margins.z : 0f);
						num5 += ((generationSettings.margins.y > 0f) ? generationSettings.margins.y : 0f);
						num5 += ((generationSettings.margins.w > 0f) ? generationSettings.margins.w : 0f);
						num4 = (float)((int)(num4 * 100f + 1f)) / 100f;
						num5 = (float)((int)(num5 * 100f + 1f)) / 100f;
						Profiler.EndSample();
						result = new Vector2(num4, num5);
					}
				}
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00017FDC File Offset: 0x000161DC
		public TextGenerator()
		{
		}

		// Token: 0x0400013B RID: 315
		private const int k_Tab = 9;

		// Token: 0x0400013C RID: 316
		private const int k_LineFeed = 10;

		// Token: 0x0400013D RID: 317
		private const int k_CarriageReturn = 13;

		// Token: 0x0400013E RID: 318
		private const int k_Space = 32;

		// Token: 0x0400013F RID: 319
		private const int k_DoubleQuotes = 34;

		// Token: 0x04000140 RID: 320
		private const int k_NumberSign = 35;

		// Token: 0x04000141 RID: 321
		private const int k_PercentSign = 37;

		// Token: 0x04000142 RID: 322
		private const int k_SingleQuote = 39;

		// Token: 0x04000143 RID: 323
		private const int k_Plus = 43;

		// Token: 0x04000144 RID: 324
		private const int k_Minus = 45;

		// Token: 0x04000145 RID: 325
		private const int k_Period = 46;

		// Token: 0x04000146 RID: 326
		private const int k_LesserThan = 60;

		// Token: 0x04000147 RID: 327
		private const int k_Equal = 61;

		// Token: 0x04000148 RID: 328
		private const int k_GreaterThan = 62;

		// Token: 0x04000149 RID: 329
		private const int k_Underline = 95;

		// Token: 0x0400014A RID: 330
		private const int k_NoBreakSpace = 160;

		// Token: 0x0400014B RID: 331
		private const int k_SoftHyphen = 173;

		// Token: 0x0400014C RID: 332
		private const int k_HyphenMinus = 45;

		// Token: 0x0400014D RID: 333
		private const int k_FigureSpace = 8199;

		// Token: 0x0400014E RID: 334
		private const int k_Hyphen = 8208;

		// Token: 0x0400014F RID: 335
		private const int k_NonBreakingHyphen = 8209;

		// Token: 0x04000150 RID: 336
		private const int k_ZeroWidthSpace = 8203;

		// Token: 0x04000151 RID: 337
		private const int k_NarrowNoBreakSpace = 8239;

		// Token: 0x04000152 RID: 338
		private const int k_WordJoiner = 8288;

		// Token: 0x04000153 RID: 339
		private const int k_HorizontalEllipsis = 8230;

		// Token: 0x04000154 RID: 340
		private const int k_RightSingleQuote = 8217;

		// Token: 0x04000155 RID: 341
		private const int k_Square = 9633;

		// Token: 0x04000156 RID: 342
		private const int k_HangulJamoStart = 4352;

		// Token: 0x04000157 RID: 343
		private const int k_HangulJamoEnd = 4607;

		// Token: 0x04000158 RID: 344
		private const int k_CjkStart = 11904;

		// Token: 0x04000159 RID: 345
		private const int k_CjkEnd = 40959;

		// Token: 0x0400015A RID: 346
		private const int k_HangulJameExtendedStart = 43360;

		// Token: 0x0400015B RID: 347
		private const int k_HangulJameExtendedEnd = 43391;

		// Token: 0x0400015C RID: 348
		private const int k_HangulSyllablesStart = 44032;

		// Token: 0x0400015D RID: 349
		private const int k_HangulSyllablesEnd = 55295;

		// Token: 0x0400015E RID: 350
		private const int k_CjkIdeographsStart = 63744;

		// Token: 0x0400015F RID: 351
		private const int k_CjkIdeographsEnd = 64255;

		// Token: 0x04000160 RID: 352
		private const int k_CjkFormsStart = 65072;

		// Token: 0x04000161 RID: 353
		private const int k_CjkFormsEnd = 65103;

		// Token: 0x04000162 RID: 354
		private const int k_CjkHalfwidthStart = 65280;

		// Token: 0x04000163 RID: 355
		private const int k_CjkHalfwidthEnd = 65519;

		// Token: 0x04000164 RID: 356
		private const int k_VerticesMax = 16383;

		// Token: 0x04000165 RID: 357
		private const int k_SpritesStart = 57344;

		// Token: 0x04000166 RID: 358
		private const float k_FloatUnset = -32767f;

		// Token: 0x04000167 RID: 359
		private const int k_MaxCharacters = 8;

		// Token: 0x04000168 RID: 360
		private static TextGenerator s_TextGenerator;

		// Token: 0x04000169 RID: 361
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x0400016A RID: 362
		private float m_MarginWidth;

		// Token: 0x0400016B RID: 363
		private float m_MarginHeight;

		// Token: 0x0400016C RID: 364
		private int[] m_CharBuffer = new int[8];

		// Token: 0x0400016D RID: 365
		private float m_PreferredWidth;

		// Token: 0x0400016E RID: 366
		private float m_PreferredHeight;

		// Token: 0x0400016F RID: 367
		private FontAsset m_CurrentFontAsset;

		// Token: 0x04000170 RID: 368
		private Material m_CurrentMaterial;

		// Token: 0x04000171 RID: 369
		private int m_CurrentMaterialIndex;

		// Token: 0x04000172 RID: 370
		private TextProcessingStack<MaterialReference> m_MaterialReferenceStack = new TextProcessingStack<MaterialReference>(new MaterialReference[16]);

		// Token: 0x04000173 RID: 371
		private float m_Padding;

		// Token: 0x04000174 RID: 372
		private SpriteAsset m_CurrentSpriteAsset;

		// Token: 0x04000175 RID: 373
		private int m_TotalCharacterCount;

		// Token: 0x04000176 RID: 374
		private float m_FontScale;

		// Token: 0x04000177 RID: 375
		private float m_FontSize;

		// Token: 0x04000178 RID: 376
		private float m_FontScaleMultiplier;

		// Token: 0x04000179 RID: 377
		private float m_CurrentFontSize;

		// Token: 0x0400017A RID: 378
		private TextProcessingStack<float> m_SizeStack = new TextProcessingStack<float>(16);

		// Token: 0x0400017B RID: 379
		private FontStyles m_FontStyleInternal = FontStyles.Normal;

		// Token: 0x0400017C RID: 380
		private FontStyleStack m_FontStyleStack;

		// Token: 0x0400017D RID: 381
		private TextFontWeight m_FontWeightInternal = TextFontWeight.Regular;

		// Token: 0x0400017E RID: 382
		private TextProcessingStack<TextFontWeight> m_FontWeightStack = new TextProcessingStack<TextFontWeight>(8);

		// Token: 0x0400017F RID: 383
		private TextAlignment m_LineJustification;

		// Token: 0x04000180 RID: 384
		private TextProcessingStack<TextAlignment> m_LineJustificationStack = new TextProcessingStack<TextAlignment>(16);

		// Token: 0x04000181 RID: 385
		private float m_BaselineOffset;

		// Token: 0x04000182 RID: 386
		private TextProcessingStack<float> m_BaselineOffsetStack = new TextProcessingStack<float>(new float[16]);

		// Token: 0x04000183 RID: 387
		private Color32 m_FontColor32;

		// Token: 0x04000184 RID: 388
		private Color32 m_HtmlColor;

		// Token: 0x04000185 RID: 389
		private Color32 m_UnderlineColor;

		// Token: 0x04000186 RID: 390
		private Color32 m_StrikethroughColor;

		// Token: 0x04000187 RID: 391
		private TextProcessingStack<Color32> m_ColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x04000188 RID: 392
		private TextProcessingStack<Color32> m_UnderlineColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x04000189 RID: 393
		private TextProcessingStack<Color32> m_StrikethroughColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400018A RID: 394
		private TextProcessingStack<Color32> m_HighlightColorStack = new TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x0400018B RID: 395
		private TextColorGradient m_ColorGradientPreset;

		// Token: 0x0400018C RID: 396
		private TextProcessingStack<TextColorGradient> m_ColorGradientStack = new TextProcessingStack<TextColorGradient>(new TextColorGradient[16]);

		// Token: 0x0400018D RID: 397
		private TextProcessingStack<int> m_ActionStack = new TextProcessingStack<int>(new int[16]);

		// Token: 0x0400018E RID: 398
		private bool m_IsFxMatrixSet;

		// Token: 0x0400018F RID: 399
		private float m_LineOffset;

		// Token: 0x04000190 RID: 400
		private float m_LineHeight;

		// Token: 0x04000191 RID: 401
		private float m_CSpacing;

		// Token: 0x04000192 RID: 402
		private float m_MonoSpacing;

		// Token: 0x04000193 RID: 403
		private float m_XAdvance;

		// Token: 0x04000194 RID: 404
		private float m_TagLineIndent;

		// Token: 0x04000195 RID: 405
		private float m_TagIndent;

		// Token: 0x04000196 RID: 406
		private TextProcessingStack<float> m_IndentStack = new TextProcessingStack<float>(new float[16]);

		// Token: 0x04000197 RID: 407
		private bool m_TagNoParsing;

		// Token: 0x04000198 RID: 408
		private int m_CharacterCount;

		// Token: 0x04000199 RID: 409
		private int m_FirstCharacterOfLine;

		// Token: 0x0400019A RID: 410
		private int m_LastCharacterOfLine;

		// Token: 0x0400019B RID: 411
		private int m_FirstVisibleCharacterOfLine;

		// Token: 0x0400019C RID: 412
		private int m_LastVisibleCharacterOfLine;

		// Token: 0x0400019D RID: 413
		private float m_MaxLineAscender;

		// Token: 0x0400019E RID: 414
		private float m_MaxLineDescender;

		// Token: 0x0400019F RID: 415
		private int m_LineNumber;

		// Token: 0x040001A0 RID: 416
		private int m_LineVisibleCharacterCount;

		// Token: 0x040001A1 RID: 417
		private int m_FirstOverflowCharacterIndex;

		// Token: 0x040001A2 RID: 418
		private int m_PageNumber;

		// Token: 0x040001A3 RID: 419
		private float m_MarginLeft;

		// Token: 0x040001A4 RID: 420
		private float m_MarginRight;

		// Token: 0x040001A5 RID: 421
		private float m_Width;

		// Token: 0x040001A6 RID: 422
		private Extents m_MeshExtents;

		// Token: 0x040001A7 RID: 423
		private float m_MaxCapHeight;

		// Token: 0x040001A8 RID: 424
		private float m_MaxAscender;

		// Token: 0x040001A9 RID: 425
		private float m_MaxDescender;

		// Token: 0x040001AA RID: 426
		private bool m_IsNewPage;

		// Token: 0x040001AB RID: 427
		private bool m_IsNonBreakingSpace;

		// Token: 0x040001AC RID: 428
		private WordWrapState m_SavedWordWrapState;

		// Token: 0x040001AD RID: 429
		private WordWrapState m_SavedLineState;

		// Token: 0x040001AE RID: 430
		private int m_LoopCountA;

		// Token: 0x040001AF RID: 431
		private TextElementType m_TextElementType;

		// Token: 0x040001B0 RID: 432
		private bool m_IsParsingText;

		// Token: 0x040001B1 RID: 433
		private int m_SpriteIndex;

		// Token: 0x040001B2 RID: 434
		private Color32 m_SpriteColor;

		// Token: 0x040001B3 RID: 435
		private TextElement m_CachedTextElement;

		// Token: 0x040001B4 RID: 436
		private Color32 m_HighlightColor;

		// Token: 0x040001B5 RID: 437
		private float m_CharWidthAdjDelta;

		// Token: 0x040001B6 RID: 438
		private Matrix4x4 m_FxMatrix;

		// Token: 0x040001B7 RID: 439
		private float m_MaxFontSize;

		// Token: 0x040001B8 RID: 440
		private float m_MinFontSize;

		// Token: 0x040001B9 RID: 441
		private bool m_IsCharacterWrappingEnabled;

		// Token: 0x040001BA RID: 442
		private float m_StartOfLineAscender;

		// Token: 0x040001BB RID: 443
		private float m_LineSpacingDelta;

		// Token: 0x040001BC RID: 444
		private bool m_IsMaskingEnabled;

		// Token: 0x040001BD RID: 445
		private MaterialReference[] m_MaterialReferences = new MaterialReference[8];

		// Token: 0x040001BE RID: 446
		private int m_SpriteCount = 0;

		// Token: 0x040001BF RID: 447
		private TextProcessingStack<int> m_StyleStack = new TextProcessingStack<int>(new int[16]);

		// Token: 0x040001C0 RID: 448
		private int m_SpriteAnimationId;

		// Token: 0x040001C1 RID: 449
		private uint[] m_InternalTextParsingBuffer = new uint[256];

		// Token: 0x040001C2 RID: 450
		private RichTextTagAttribute[] m_Attributes = new RichTextTagAttribute[8];

		// Token: 0x040001C3 RID: 451
		private XmlTagAttribute[] m_XmlAttribute = new XmlTagAttribute[8];

		// Token: 0x040001C4 RID: 452
		private char[] m_RichTextTag = new char[128];

		// Token: 0x040001C5 RID: 453
		private Dictionary<int, int> m_MaterialReferenceIndexLookup = new Dictionary<int, int>();

		// Token: 0x040001C6 RID: 454
		private bool m_IsCalculatingPreferredValues;

		// Token: 0x040001C7 RID: 455
		private SpriteAsset m_DefaultSpriteAsset;

		// Token: 0x040001C8 RID: 456
		private bool m_TintSprite;

		// Token: 0x040001C9 RID: 457
		protected TextGenerator.SpecialCharacter m_Ellipsis;

		// Token: 0x040001CA RID: 458
		protected TextGenerator.SpecialCharacter m_Underline;

		// Token: 0x040001CB RID: 459
		private bool m_IsUsingBold;

		// Token: 0x040001CC RID: 460
		private bool m_IsSdfShader;

		// Token: 0x040001CD RID: 461
		private TextElementInfo[] m_InternalTextElementInfo;

		// Token: 0x040001CE RID: 462
		private int m_RecursiveCount;

		// Token: 0x02000029 RID: 41
		protected struct SpecialCharacter
		{
			// Token: 0x0600013C RID: 316 RVA: 0x0001814C File Offset: 0x0001634C
			public SpecialCharacter(Character character, int materialIndex)
			{
				this.character = character;
				this.fontAsset = (character.textAsset as FontAsset);
				this.material = ((this.fontAsset != null) ? this.fontAsset.material : null);
				this.materialIndex = materialIndex;
			}

			// Token: 0x040001CF RID: 463
			public Character character;

			// Token: 0x040001D0 RID: 464
			public FontAsset fontAsset;

			// Token: 0x040001D1 RID: 465
			public Material material;

			// Token: 0x040001D2 RID: 466
			public int materialIndex;
		}
	}
}
