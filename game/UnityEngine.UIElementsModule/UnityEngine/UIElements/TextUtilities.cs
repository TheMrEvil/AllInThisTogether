using System;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BF RID: 703
	internal static class TextUtilities
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x00063888 File Offset: 0x00061A88
		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000638E8 File Offset: 0x00061AE8
		internal static Vector2 MeasureVisualElementTextSize(VisualElement ve, string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode, ITextHandle textHandle)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = textToMeasure == null || !TextUtilities.IsFontAssigned(ve);
			Vector2 result;
			if (flag)
			{
				result = new Vector2(num, num2);
			}
			else
			{
				float scaledPixelsPerPoint = ve.scaledPixelsPerPoint;
				bool flag2 = widthMode == VisualElement.MeasureMode.Exactly;
				if (flag2)
				{
					num = width;
				}
				else
				{
					MeshGenerationContextUtils.TextParams parms = MeshGenerationContextUtils.TextParams.MakeStyleBased(ve, textToMeasure);
					parms.wordWrap = false;
					parms.rect = new Rect(parms.rect.x, parms.rect.y, width, height);
					num = textHandle.ComputeTextWidth(parms, scaledPixelsPerPoint);
					bool flag3 = widthMode == VisualElement.MeasureMode.AtMost;
					if (flag3)
					{
						num = Mathf.Min(num, width);
					}
				}
				bool flag4 = heightMode == VisualElement.MeasureMode.Exactly;
				if (flag4)
				{
					num2 = height;
				}
				else
				{
					MeshGenerationContextUtils.TextParams parms2 = MeshGenerationContextUtils.TextParams.MakeStyleBased(ve, textToMeasure);
					parms2.wordWrapWidth = num;
					parms2.rect = new Rect(parms2.rect.x, parms2.rect.y, width, height);
					num2 = textHandle.ComputeTextHeight(parms2, scaledPixelsPerPoint);
					bool flag5 = heightMode == VisualElement.MeasureMode.AtMost;
					if (flag5)
					{
						num2 = Mathf.Min(num2, height);
					}
				}
				float x = AlignmentUtils.CeilToPixelGrid(num, scaledPixelsPerPoint, 0f);
				float y = AlignmentUtils.CeilToPixelGrid(num2, scaledPixelsPerPoint, 0f);
				Vector2 vector = new Vector2(x, y);
				textHandle.MeasuredSizes = new Vector2(num, num2);
				textHandle.RoundedSizes = vector;
				result = vector;
			}
			return result;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00063A48 File Offset: 0x00061C48
		internal static FontAsset GetFontAsset(MeshGenerationContextUtils.TextParams textParam)
		{
			PanelTextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(textParam);
			bool flag = textParam.fontDefinition.fontAsset != null;
			FontAsset result;
			if (flag)
			{
				result = textParam.fontDefinition.fontAsset;
			}
			else
			{
				bool flag2 = textParam.fontDefinition.font != null;
				if (flag2)
				{
					result = textSettingsFrom.GetCachedFontAsset(textParam.fontDefinition.font);
				}
				else
				{
					result = textSettingsFrom.GetCachedFontAsset(textParam.font);
				}
			}
			return result;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00063AC0 File Offset: 0x00061CC0
		internal static FontAsset GetFontAsset(VisualElement ve)
		{
			bool flag = ve.computedStyle.unityFontDefinition.fontAsset != null;
			FontAsset result;
			if (flag)
			{
				result = ve.computedStyle.unityFontDefinition.fontAsset;
			}
			else
			{
				PanelTextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(ve);
				bool flag2 = ve.computedStyle.unityFontDefinition.font != null;
				if (flag2)
				{
					result = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFontDefinition.font);
				}
				else
				{
					result = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFont);
				}
			}
			return result;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00063B58 File Offset: 0x00061D58
		internal static Font GetFont(MeshGenerationContextUtils.TextParams textParam)
		{
			bool flag = textParam.fontDefinition.font != null;
			Font result;
			if (flag)
			{
				result = textParam.fontDefinition.font;
			}
			else
			{
				bool flag2 = textParam.font != null;
				if (flag2)
				{
					result = textParam.font;
				}
				else
				{
					FontAsset fontAsset = textParam.fontDefinition.fontAsset;
					result = ((fontAsset != null) ? fontAsset.sourceFontFile : null);
				}
			}
			return result;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00063BC0 File Offset: 0x00061DC0
		internal unsafe static Font GetFont(VisualElement ve)
		{
			ComputedStyle computedStyle = *ve.computedStyle;
			bool flag = computedStyle.unityFontDefinition.font != null;
			Font result;
			if (flag)
			{
				result = computedStyle.unityFontDefinition.font;
			}
			else
			{
				bool flag2 = computedStyle.unityFont != null;
				if (flag2)
				{
					result = computedStyle.unityFont;
				}
				else
				{
					FontAsset fontAsset = computedStyle.unityFontDefinition.fontAsset;
					result = ((fontAsset != null) ? fontAsset.sourceFontFile : null);
				}
			}
			return result;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00063C44 File Offset: 0x00061E44
		internal static bool IsFontAssigned(VisualElement ve)
		{
			return ve.computedStyle.unityFont != null || !ve.computedStyle.unityFontDefinition.IsEmpty();
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00063C84 File Offset: 0x00061E84
		internal static bool IsFontAssigned(MeshGenerationContextUtils.TextParams textParams)
		{
			return textParams.font != null || !textParams.fontDefinition.IsEmpty();
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00063CB8 File Offset: 0x00061EB8
		internal static PanelTextSettings GetTextSettingsFrom(VisualElement ve)
		{
			RuntimePanel runtimePanel = ve.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			PanelTextSettings result;
			if (flag)
			{
				result = (runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings);
			}
			else
			{
				result = PanelTextSettings.defaultPanelTextSettings;
			}
			return result;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00063CFC File Offset: 0x00061EFC
		internal static PanelTextSettings GetTextSettingsFrom(MeshGenerationContextUtils.TextParams textParam)
		{
			RuntimePanel runtimePanel = textParam.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			PanelTextSettings result;
			if (flag)
			{
				result = (runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings);
			}
			else
			{
				result = PanelTextSettings.defaultPanelTextSettings;
			}
			return result;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00063D40 File Offset: 0x00061F40
		internal unsafe static TextCoreSettings GetTextCoreSettingsForElement(VisualElement ve)
		{
			FontAsset fontAsset = TextUtilities.GetFontAsset(ve);
			bool flag = fontAsset == null;
			TextCoreSettings result;
			if (flag)
			{
				result = default(TextCoreSettings);
			}
			else
			{
				IResolvedStyle resolvedStyle = ve.resolvedStyle;
				ComputedStyle computedStyle = *ve.computedStyle;
				float num = 1f / (float)fontAsset.atlasPadding;
				float num2 = (float)fontAsset.faceInfo.pointSize / ve.computedStyle.fontSize.value;
				float num3 = num * num2;
				float num4 = Mathf.Max(0f, resolvedStyle.unityTextOutlineWidth * num3);
				float underlaySoftness = Mathf.Max(0f, computedStyle.textShadow.blurRadius * num3);
				Vector2 underlayOffset = computedStyle.textShadow.offset * num3;
				Color color = resolvedStyle.color;
				Color unityTextOutlineColor = resolvedStyle.unityTextOutlineColor;
				bool flag2 = num4 < 1E-30f;
				if (flag2)
				{
					unityTextOutlineColor.a = 0f;
				}
				result = new TextCoreSettings
				{
					faceColor = color,
					outlineColor = unityTextOutlineColor,
					outlineWidth = num4,
					underlayColor = computedStyle.textShadow.color,
					underlayOffset = underlayOffset,
					underlaySoftness = underlaySoftness
				};
			}
			return result;
		}
	}
}
