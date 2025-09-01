using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000363 RID: 867
	internal class StylePropertyReader
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x00082EC3 File Offset: 0x000810C3
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x00082ECB File Offset: 0x000810CB
		public StyleProperty property
		{
			[CompilerGenerated]
			get
			{
				return this.<property>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<property>k__BackingField = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x00082ED4 File Offset: 0x000810D4
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x00082EDC File Offset: 0x000810DC
		public StylePropertyId propertyId
		{
			[CompilerGenerated]
			get
			{
				return this.<propertyId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<propertyId>k__BackingField = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00082EE5 File Offset: 0x000810E5
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x00082EED File Offset: 0x000810ED
		public int valueCount
		{
			[CompilerGenerated]
			get
			{
				return this.<valueCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<valueCount>k__BackingField = value;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00082EF6 File Offset: 0x000810F6
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00082EFE File Offset: 0x000810FE
		public float dpiScaling
		{
			[CompilerGenerated]
			get
			{
				return this.<dpiScaling>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dpiScaling>k__BackingField = value;
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00082F08 File Offset: 0x00081108
		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00082F5D File Offset: 0x0008115D
		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00082F88 File Offset: 0x00081188
		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00082FC8 File Offset: 0x000811C8
		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00082FF0 File Offset: 0x000811F0
		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00083024 File Offset: 0x00081224
		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0008305C File Offset: 0x0008125C
		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.handle.valueType == StyleValueType.Keyword && stylePropertyValue.handle.valueIndex == (int)keyword;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000830A4 File Offset: 0x000812A4
		public string ReadAsString(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000830DC File Offset: 0x000812DC
		public Length ReadLength(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			Length result;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.Auto)
				{
					if (styleValueKeyword2 != StyleValueKeyword.None)
					{
						result = default(Length);
					}
					else
					{
						result = Length.None();
					}
				}
				else
				{
					result = Length.Auto();
				}
			}
			else
			{
				result = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToLength();
			}
			return result;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00083174 File Offset: 0x00081374
		public TimeValue ReadTimeValue(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000831B4 File Offset: 0x000813B4
		public Translate ReadTranslate(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = (this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue);
			StylePropertyValue val3 = (this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue);
			return StylePropertyReader.ReadTranslate(this.valueCount, val, val2, val3);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00083240 File Offset: 0x00081440
		public TransformOrigin ReadTransformOrigin(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = (this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue);
			StylePropertyValue zVvalue = (this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue);
			return StylePropertyReader.ReadTransformOrigin(this.valueCount, val, val2, zVvalue);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000832CC File Offset: 0x000814CC
		public Rotate ReadRotate(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = (this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue);
			StylePropertyValue val3 = (this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue);
			StylePropertyValue val4 = (this.valueCount > 3) ? this.m_Values[this.m_CurrentValueIndex + index + 3] : default(StylePropertyValue);
			return StylePropertyReader.ReadRotate(this.valueCount, val, val2, val3, val4);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00083384 File Offset: 0x00081584
		public Scale ReadScale(int index)
		{
			StylePropertyValue val = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue val2 = (this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue);
			StylePropertyValue val3 = (this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue);
			return StylePropertyReader.ReadScale(this.valueCount, val, val2, val3);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00083410 File Offset: 0x00081610
		public float ReadFloat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00083448 File Offset: 0x00081648
		public int ReadInt(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return (int)stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00083480 File Offset: 0x00081680
		public Color ReadColor(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			Color result = Color.clear;
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				StyleSheetColor.TryGetColor(text.ToLower(), out result);
			}
			else
			{
				result = stylePropertyValue.sheet.ReadColor(stylePropertyValue.handle);
			}
			return result;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000834FC File Offset: 0x000816FC
		public int ReadEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = stylePropertyValue.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string value;
			if (flag)
			{
				StyleValueKeyword svk = stylePropertyValue.sheet.ReadKeyword(handle);
				value = svk.ToUssString();
			}
			else
			{
				value = stylePropertyValue.sheet.ReadEnum(handle);
			}
			int result;
			StylePropertyUtil.TryGetEnumIntValue(enumType, value, out result);
			return result;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00083574 File Offset: 0x00081774
		public FontDefinition ReadFontDefinition(int index)
		{
			FontAsset fontAsset = null;
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = (stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font);
						bool flag = font == null;
						if (flag)
						{
							fontAsset = (stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as FontAsset);
						}
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						font = (Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font);
						bool flag3 = font == null;
						if (flag3)
						{
							fontAsset = (Panel.LoadResource(text, typeof(FontAsset), this.dpiScaling) as FontAsset);
						}
					}
					bool flag4 = fontAsset == null && font == null;
					if (flag4)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag5 = stylePropertyValue.handle.valueIndex != 6;
				if (flag5)
				{
					string str = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(str + valueIndex.ToString());
				}
			}
			bool flag6 = font != null;
			FontDefinition result;
			if (flag6)
			{
				result = FontDefinition.FromFont(font);
			}
			else
			{
				bool flag7 = fontAsset != null;
				if (flag7)
				{
					result = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					result = default(FontDefinition);
				}
			}
			return result;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00083754 File Offset: 0x00081954
		public Font ReadFont(int index)
		{
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = (stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font);
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						font = (Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font);
					}
					bool flag2 = font == null;
					if (flag2)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag3 = stylePropertyValue.handle.valueIndex != 6;
				if (flag3)
				{
					string str = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(str + valueIndex.ToString());
				}
			}
			return font;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00083898 File Offset: 0x00081A98
		public Background ReadBackground(int index)
		{
			ImageSource imageSource = default(ImageSource);
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = stylePropertyValue.handle.valueIndex != 6;
				if (flag2)
				{
					string str = "Invalid keyword for image source ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(str + valueIndex.ToString());
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource);
				if (flag3)
				{
				}
			}
			bool flag4 = imageSource.texture != null;
			Background result;
			if (flag4)
			{
				result = Background.FromTexture2D(imageSource.texture);
			}
			else
			{
				bool flag5 = imageSource.sprite != null;
				if (flag5)
				{
					result = Background.FromSprite(imageSource.sprite);
				}
				else
				{
					bool flag6 = imageSource.vectorImage != null;
					if (flag6)
					{
						result = Background.FromVectorImage(imageSource.vectorImage);
					}
					else
					{
						bool flag7 = imageSource.renderTexture != null;
						if (flag7)
						{
							result = Background.FromRenderTexture(imageSource.renderTexture);
						}
						else
						{
							result = default(Background);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000839C8 File Offset: 0x00081BC8
		public Cursor ReadCursor(int index)
		{
			float x = 0f;
			float y = 0f;
			int defaultCursorId = 0;
			Texture2D texture = null;
			StyleValueType valueType = this.GetValueType(index);
			bool flag = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage || valueType == StyleValueType.MissingAssetReference;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.valueCount < 1;
				if (flag3)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource imageSource = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag4 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out imageSource);
					if (flag4)
					{
						texture = imageSource.texture;
						bool flag5 = this.valueCount >= 3;
						if (flag5)
						{
							StylePropertyValue value2 = this.GetValue(index + 1);
							StylePropertyValue value3 = this.GetValue(index + 2);
							bool flag6 = value2.handle.valueType != StyleValueType.Float || value3.handle.valueType != StyleValueType.Float;
							if (flag6)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								x = value2.sheet.ReadFloat(value2.handle);
								y = value3.sheet.ReadFloat(value3.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = StylePropertyReader.getCursorIdFunc != null;
				if (flag7)
				{
					StylePropertyValue value4 = this.GetValue(index);
					defaultCursorId = StylePropertyReader.getCursorIdFunc(value4.sheet, value4.handle);
				}
			}
			return new Cursor
			{
				texture = texture,
				hotspot = new Vector2(x, y),
				defaultCursorId = defaultCursorId
			};
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00083B6C File Offset: 0x00081D6C
		public TextShadow ReadTextShadow(int index)
		{
			float x = 0f;
			float y = 0f;
			float blurRadius = 0f;
			Color color = Color.clear;
			bool flag = this.valueCount >= 2;
			if (flag)
			{
				int num = index;
				StyleValueType valueType = this.GetValueType(num);
				bool flag2 = false;
				bool flag3 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
				if (flag3)
				{
					color = this.ReadColor(num++);
					flag2 = true;
				}
				bool flag4 = num + 1 < this.valueCount;
				if (flag4)
				{
					valueType = this.GetValueType(num);
					StyleValueType valueType2 = this.GetValueType(num + 1);
					bool flag5 = (valueType == StyleValueType.Dimension || valueType == StyleValueType.Float) && (valueType2 == StyleValueType.Dimension || valueType2 == StyleValueType.Float);
					if (flag5)
					{
						StylePropertyValue value = this.GetValue(num++);
						StylePropertyValue value2 = this.GetValue(num++);
						x = value.sheet.ReadDimension(value.handle).value;
						y = value2.sheet.ReadDimension(value2.handle).value;
					}
				}
				bool flag6 = num < this.valueCount;
				if (flag6)
				{
					valueType = this.GetValueType(num);
					bool flag7 = valueType == StyleValueType.Dimension || valueType == StyleValueType.Float;
					if (flag7)
					{
						StylePropertyValue value3 = this.GetValue(num++);
						blurRadius = value3.sheet.ReadDimension(value3.handle).value;
					}
					else
					{
						bool flag8 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
						if (flag8)
						{
							bool flag9 = !flag2;
							if (flag9)
							{
								color = this.ReadColor(num);
							}
						}
					}
				}
				bool flag10 = num < this.valueCount;
				if (flag10)
				{
					valueType = this.GetValueType(num);
					bool flag11 = valueType == StyleValueType.Color || valueType == StyleValueType.Enum;
					if (flag11)
					{
						bool flag12 = !flag2;
						if (flag12)
						{
							color = this.ReadColor(num);
						}
					}
				}
			}
			return new TextShadow
			{
				offset = new Vector2(x, y),
				blurRadius = blurRadius,
				color = color
			};
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00083D7C File Offset: 0x00081F7C
		public void ReadListEasingFunction(List<EasingFunction> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				StyleValueHandle handle = stylePropertyValue.handle;
				bool flag = handle.valueType == StyleValueType.Enum;
				if (flag)
				{
					string value = stylePropertyValue.sheet.ReadEnum(handle);
					int mode;
					StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.EasingMode, value, out mode);
					list.Add(new EasingFunction((EasingMode)mode));
					index++;
				}
				bool flag2 = index < this.valueCount;
				if (flag2)
				{
					bool flag3 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag3)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00083E3C File Offset: 0x0008203C
		public void ReadListTimeValue(List<TimeValue> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				TimeValue item = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
				list.Add(item);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00083EE0 File Offset: 0x000820E0
		public void ReadListStylePropertyName(List<StylePropertyName> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string name = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(new StylePropertyName(name));
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00083F80 File Offset: 0x00082180
		public void ReadListString(List<string> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string item = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(item);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0008401C File Offset: 0x0008221C
		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			foreach (StyleProperty styleProperty in this.m_Properties)
			{
				int num = 0;
				bool flag = true;
				bool requireVariableResolve = styleProperty.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(styleProperty, this.m_Sheet, styleProperty.values);
					int num2 = 0;
					while (num2 < styleProperty.values.Length && flag)
					{
						StyleValueHandle handle = styleProperty.values[num2];
						bool flag2 = handle.IsVarFunction();
						if (flag2)
						{
							flag = this.m_Resolver.ResolveVarFunction(ref num2);
						}
						else
						{
							this.m_Resolver.AddValue(handle);
						}
						num2++;
					}
					bool flag3 = flag && this.m_Resolver.ValidateResolvedValues();
					if (flag3)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						num += this.m_Resolver.resolvedValues.Count;
					}
					else
					{
						StyleValueHandle handle2 = new StyleValueHandle
						{
							valueType = StyleValueType.Keyword,
							valueIndex = 3
						};
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = handle2
						});
						num++;
					}
				}
				else
				{
					num = styleProperty.values.Length;
					for (int j = 0; j < num; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleProperty.values[j]
						});
					}
				}
				this.m_ValueCount.Add(num);
			}
			this.SetCurrentProperty();
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00084208 File Offset: 0x00082408
		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00084288 File Offset: 0x00082488
		public static TransformOrigin ReadTransformOrigin(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue zVvalue)
		{
			Length x = Length.Percent(50f);
			Length y = Length.Percent(50f);
			float z = 0f;
			switch (valCount)
			{
			case 1:
			{
				bool flag;
				bool flag2;
				Length length = StylePropertyReader.ReadTransformOriginEnum(val1, out flag, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					x = length;
				}
				else
				{
					y = length;
				}
				goto IL_F3;
			}
			case 2:
				break;
			case 3:
			{
				bool flag4 = zVvalue.handle.valueType == StyleValueType.Dimension || zVvalue.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					Dimension dimension = zVvalue.sheet.ReadDimension(zVvalue.handle);
					z = dimension.value;
				}
				break;
			}
			default:
				goto IL_F3;
			}
			bool flag5;
			bool flag6;
			Length length2 = StylePropertyReader.ReadTransformOriginEnum(val1, out flag5, out flag6);
			bool flag7;
			bool flag8;
			Length length3 = StylePropertyReader.ReadTransformOriginEnum(val2, out flag7, out flag8);
			bool flag9 = !flag6 || !flag7;
			if (flag9)
			{
				bool flag10 = flag8 && flag5;
				if (flag10)
				{
					x = length3;
					y = length2;
				}
			}
			else
			{
				x = length2;
				y = length3;
			}
			IL_F3:
			return new TransformOrigin(x, y, z);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00084398 File Offset: 0x00082598
		private static Length ReadTransformOriginEnum(StylePropertyValue value, out bool isVertical, out bool isHorizontal)
		{
			bool flag = value.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				switch (StylePropertyReader.ReadEnum(StyleEnumType.TransformOriginOffset, value))
				{
				case 1:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(0f);
				case 2:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(100f);
				case 3:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(0f);
				case 4:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(100f);
				case 5:
					isVertical = true;
					isHorizontal = true;
					return Length.Percent(50f);
				}
			}
			else
			{
				bool flag2 = value.handle.valueType == StyleValueType.Dimension || value.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					isVertical = true;
					isHorizontal = true;
					return value.sheet.ReadDimension(value.handle).ToLength();
				}
			}
			isVertical = false;
			isHorizontal = false;
			return Length.Percent(50f);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000844BC File Offset: 0x000826BC
		public static Translate ReadTranslate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Translate result;
			if (flag)
			{
				result = Translate.None();
			}
			else
			{
				Length x = 0f;
				Length y = 0f;
				float z = 0f;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						x = val1.sheet.ReadDimension(val1.handle).ToLength();
						y = val1.sheet.ReadDimension(val1.handle).ToLength();
					}
					goto IL_1C3;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						Dimension dimension = val3.sheet.ReadDimension(val3.handle);
						bool flag4 = dimension.unit != Dimension.Unit.Pixel && dimension.unit > Dimension.Unit.Unitless;
						if (flag4)
						{
							z = dimension.value;
						}
					}
					break;
				}
				default:
					goto IL_1C3;
				}
				bool flag5 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					x = val1.sheet.ReadDimension(val1.handle).ToLength();
				}
				bool flag6 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag6)
				{
					y = val2.sheet.ReadDimension(val2.handle).ToLength();
				}
				IL_1C3:
				result = new Translate(x, y, z);
			}
			return result;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0008469C File Offset: 0x0008289C
		public static Scale ReadScale(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Scale result;
			if (flag)
			{
				result = Scale.None();
			}
			else
			{
				Vector3 one = Vector3.one;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						one.x = val1.sheet.ReadFloat(val1.handle);
						one.y = one.x;
					}
					goto IL_173;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						one.z = val3.sheet.ReadFloat(val3.handle);
					}
					break;
				}
				default:
					goto IL_173;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					one.x = val1.sheet.ReadFloat(val1.handle);
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					one.y = val2.sheet.ReadFloat(val2.handle);
				}
				IL_173:
				result = new Scale(one);
			}
			return result;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00084828 File Offset: 0x00082A28
		public static Rotate ReadRotate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3, StylePropertyValue val4)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Rotate result;
			if (flag)
			{
				result = Rotate.None();
			}
			else
			{
				Rotate rotate = Rotate.Initial();
				if (valCount == 1)
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension;
					if (flag2)
					{
						rotate.angle = StylePropertyReader.ReadAngle(val1);
					}
				}
				result = rotate;
			}
			return result;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000848A4 File Offset: 0x00082AA4
		private static int ReadEnum(StyleEnumType enumType, StylePropertyValue value)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string value2;
			if (flag)
			{
				StyleValueKeyword svk = value.sheet.ReadKeyword(handle);
				value2 = svk.ToUssString();
			}
			else
			{
				value2 = value.sheet.ReadEnum(handle);
			}
			int result;
			StylePropertyUtil.TryGetEnumIntValue(enumType, value2, out result);
			return result;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00084904 File Offset: 0x00082B04
		public static Angle ReadAngle(StylePropertyValue value)
		{
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			Angle result;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.None)
				{
					result = default(Angle);
				}
				else
				{
					result = Angle.None();
				}
			}
			else
			{
				result = value.sheet.ReadDimension(value.handle).ToAngle();
			}
			return result;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00084974 File Offset: 0x00082B74
		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType <= StyleValueType.AssetReference)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType == StyleValueType.AssetReference)
					{
						Object @object = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
						source.texture = (@object as Texture2D);
						source.sprite = (@object as Sprite);
						source.vectorImage = (@object as VectorImage);
						source.renderTexture = (@object as RenderTexture);
						bool flag = source.IsNull();
						if (flag)
						{
							Debug.LogWarning("Invalid image specified");
							return false;
						}
						goto IL_254;
					}
				}
				else
				{
					string text = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						source.sprite = (Panel.LoadResource(text, typeof(Sprite), dpiScaling) as Sprite);
						bool flag3 = source.IsNull();
						if (flag3)
						{
							source.texture = (Panel.LoadResource(text, typeof(Texture2D), dpiScaling) as Texture2D);
						}
						bool flag4 = source.IsNull();
						if (flag4)
						{
							source.vectorImage = (Panel.LoadResource(text, typeof(VectorImage), dpiScaling) as VectorImage);
						}
						bool flag5 = source.IsNull();
						if (flag5)
						{
							source.renderTexture = (Panel.LoadResource(text, typeof(RenderTexture), dpiScaling) as RenderTexture);
						}
					}
					bool flag6 = source.IsNull();
					if (flag6)
					{
						Debug.LogWarning(string.Format("Image not found for path: {0}", text));
						return false;
					}
					goto IL_254;
				}
			}
			else if (styleValueType != StyleValueType.ScalableImage)
			{
				if (styleValueType == StyleValueType.MissingAssetReference)
				{
					return false;
				}
			}
			else
			{
				ScalableImage scalableImage = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
				bool flag7 = scalableImage.normalImage == null && scalableImage.highResolutionImage == null;
				if (flag7)
				{
					Debug.LogWarning("Invalid scalable image specified");
					return false;
				}
				source.texture = scalableImage.normalImage;
				bool flag8 = !Mathf.Approximately(dpiScaling % 1f, 0f);
				if (flag8)
				{
					source.texture.filterMode = FilterMode.Bilinear;
				}
				goto IL_254;
			}
			Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType.ToString());
			return false;
			IL_254:
			return true;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00084BDC File Offset: 0x00082DDC
		public StylePropertyReader()
		{
		}

		// Token: 0x04000DF6 RID: 3574
		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc;

		// Token: 0x04000DF7 RID: 3575
		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		// Token: 0x04000DF8 RID: 3576
		private List<int> m_ValueCount = new List<int>();

		// Token: 0x04000DF9 RID: 3577
		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		// Token: 0x04000DFA RID: 3578
		private StyleSheet m_Sheet;

		// Token: 0x04000DFB RID: 3579
		private StyleProperty[] m_Properties;

		// Token: 0x04000DFC RID: 3580
		private StylePropertyId[] m_PropertyIds;

		// Token: 0x04000DFD RID: 3581
		private int m_CurrentValueIndex;

		// Token: 0x04000DFE RID: 3582
		private int m_CurrentPropertyIndex;

		// Token: 0x04000DFF RID: 3583
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private StyleProperty <property>k__BackingField;

		// Token: 0x04000E00 RID: 3584
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private StylePropertyId <propertyId>k__BackingField;

		// Token: 0x04000E01 RID: 3585
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <valueCount>k__BackingField;

		// Token: 0x04000E02 RID: 3586
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <dpiScaling>k__BackingField;

		// Token: 0x02000364 RID: 868
		// (Invoke) Token: 0x06001C27 RID: 7207
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);
	}
}
