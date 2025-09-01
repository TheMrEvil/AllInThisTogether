using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025F RID: 607
	internal static class MeshGenerationContextUtils
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x00049300 File Offset: 0x00047500
		public static void Rectangle(this MeshGenerationContext mgc, MeshGenerationContextUtils.RectangleParams rectParams)
		{
			mgc.painter.DrawRectangle(rectParams);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00049310 File Offset: 0x00047510
		public static void Border(this MeshGenerationContext mgc, MeshGenerationContextUtils.BorderParams borderParams)
		{
			mgc.painter.DrawBorder(borderParams);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00049320 File Offset: 0x00047520
		public static void Text(this MeshGenerationContext mgc, MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = TextUtilities.IsFontAssigned(textParams);
			if (flag)
			{
				mgc.painter.DrawText(textParams, handle, pixelsPerPoint);
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00049348 File Offset: 0x00047548
		private static Vector2 ConvertBorderRadiusPercentToPoints(Vector2 borderRectSize, Length length)
		{
			float num = length.value;
			float num2 = length.value;
			bool flag = length.unit == LengthUnit.Percent;
			if (flag)
			{
				num = borderRectSize.x * length.value / 100f;
				num2 = borderRectSize.y * length.value / 100f;
			}
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Max(num2, 0f);
			return new Vector2(num, num2);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000493C4 File Offset: 0x000475C4
		public unsafe static void GetVisualElementRadii(VisualElement ve, out Vector2 topLeft, out Vector2 bottomLeft, out Vector2 topRight, out Vector2 bottomRight)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			Vector2 borderRectSize = new Vector2(resolvedStyle.width, resolvedStyle.height);
			ComputedStyle computedStyle = *ve.computedStyle;
			topLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopLeftRadius);
			bottomLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomLeftRadius);
			topRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderTopRightRadius);
			bottomRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(borderRectSize, computedStyle.borderBottomRightRadius);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00049448 File Offset: 0x00047648
		public static void AdjustBackgroundSizeForBorders(VisualElement visualElement, ref MeshGenerationContextUtils.RectangleParams rectParams)
		{
			IResolvedStyle resolvedStyle = visualElement.resolvedStyle;
			Vector4 zero = Vector4.zero;
			bool flag = resolvedStyle.borderLeftWidth >= 1f && resolvedStyle.borderLeftColor.a >= 1f;
			if (flag)
			{
				zero.x = 0.5f;
			}
			bool flag2 = resolvedStyle.borderTopWidth >= 1f && resolvedStyle.borderTopColor.a >= 1f;
			if (flag2)
			{
				zero.y = 0.5f;
			}
			bool flag3 = resolvedStyle.borderRightWidth >= 1f && resolvedStyle.borderRightColor.a >= 1f;
			if (flag3)
			{
				zero.z = 0.5f;
			}
			bool flag4 = resolvedStyle.borderBottomWidth >= 1f && resolvedStyle.borderBottomColor.a >= 1f;
			if (flag4)
			{
				zero.w = 0.5f;
			}
			rectParams.rectInset = zero;
		}

		// Token: 0x02000260 RID: 608
		public struct BorderParams
		{
			// Token: 0x04000865 RID: 2149
			public Rect rect;

			// Token: 0x04000866 RID: 2150
			public Color playmodeTintColor;

			// Token: 0x04000867 RID: 2151
			public Color leftColor;

			// Token: 0x04000868 RID: 2152
			public Color topColor;

			// Token: 0x04000869 RID: 2153
			public Color rightColor;

			// Token: 0x0400086A RID: 2154
			public Color bottomColor;

			// Token: 0x0400086B RID: 2155
			public float leftWidth;

			// Token: 0x0400086C RID: 2156
			public float topWidth;

			// Token: 0x0400086D RID: 2157
			public float rightWidth;

			// Token: 0x0400086E RID: 2158
			public float bottomWidth;

			// Token: 0x0400086F RID: 2159
			public Vector2 topLeftRadius;

			// Token: 0x04000870 RID: 2160
			public Vector2 topRightRadius;

			// Token: 0x04000871 RID: 2161
			public Vector2 bottomRightRadius;

			// Token: 0x04000872 RID: 2162
			public Vector2 bottomLeftRadius;

			// Token: 0x04000873 RID: 2163
			public Material material;

			// Token: 0x04000874 RID: 2164
			internal ColorPage leftColorPage;

			// Token: 0x04000875 RID: 2165
			internal ColorPage topColorPage;

			// Token: 0x04000876 RID: 2166
			internal ColorPage rightColorPage;

			// Token: 0x04000877 RID: 2167
			internal ColorPage bottomColorPage;
		}

		// Token: 0x02000261 RID: 609
		public struct RectangleParams
		{
			// Token: 0x06001283 RID: 4739 RVA: 0x0004954C File Offset: 0x0004774C
			public static MeshGenerationContextUtils.RectangleParams MakeSolid(Rect rect, Color color, ContextType panelContext)
			{
				Color color2 = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					color = color,
					uv = new Rect(0f, 0f, 1f, 1f),
					playmodeTintColor = color2
				};
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x000495B0 File Offset: 0x000477B0
			private static void AdjustUVsForScaleMode(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float num = Mathf.Abs((float)texture.width * uv.width / ((float)texture.height * uv.height));
				float num2 = rect.width / rect.height;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					break;
				case ScaleMode.ScaleAndCrop:
				{
					bool flag = num2 > num;
					if (flag)
					{
						float num3 = uv.height * (num / num2);
						float num4 = (uv.height - num3) * 0.5f;
						uv = new Rect(uv.x, uv.y + num4, uv.width, num3);
					}
					else
					{
						float num5 = uv.width * (num2 / num);
						float num6 = (uv.width - num5) * 0.5f;
						uv = new Rect(uv.x + num6, uv.y, num5, uv.height);
					}
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag2 = num2 > num;
					if (flag2)
					{
						float num7 = num / num2;
						rect = new Rect(rect.xMin + rect.width * (1f - num7) * 0.5f, rect.yMin, num7 * rect.width, rect.height);
					}
					else
					{
						float num8 = num2 / num;
						rect = new Rect(rect.xMin, rect.yMin + rect.height * (1f - num8) * 0.5f, rect.width, num8 * rect.height);
					}
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = rect;
				uvOut = uv;
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x00049754 File Offset: 0x00047954
			private static void AdjustSpriteUVsForScaleMode(Rect containerRect, Rect srcRect, Rect spriteGeomRect, Sprite sprite, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float num = sprite.rect.width / sprite.rect.height;
				float num2 = containerRect.width / containerRect.height;
				Rect rect = spriteGeomRect;
				rect.position -= sprite.bounds.min;
				rect.position /= sprite.bounds.size;
				rect.size /= sprite.bounds.size;
				Vector2 position = rect.position;
				position.y = 1f - rect.size.y - position.y;
				rect.position = position;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
				{
					Vector2 size = containerRect.size;
					containerRect.position = rect.position * size;
					containerRect.size = rect.size * size;
					break;
				}
				case ScaleMode.ScaleAndCrop:
				{
					Rect b = containerRect;
					bool flag = num2 > num;
					if (flag)
					{
						b.height = b.width / num;
						b.position = new Vector2(b.position.x, -(b.height - containerRect.height) / 2f);
					}
					else
					{
						b.width = b.height * num;
						b.position = new Vector2(-(b.width - containerRect.width) / 2f, b.position.y);
					}
					Vector2 size2 = b.size;
					b.position += rect.position * size2;
					b.size = rect.size * size2;
					Rect rect2 = MeshGenerationContextUtils.RectangleParams.RectIntersection(containerRect, b);
					bool flag2 = rect2.width < 1E-30f || rect2.height < 1E-30f;
					if (flag2)
					{
						rect2 = Rect.zero;
					}
					else
					{
						Rect rect3 = rect2;
						rect3.position -= b.position;
						rect3.position /= b.size;
						rect3.size /= b.size;
						Vector2 position2 = rect3.position;
						position2.y = 1f - rect3.size.y - position2.y;
						rect3.position = position2;
						srcRect.position += rect3.position * srcRect.size;
						srcRect.size *= rect3.size;
					}
					containerRect = rect2;
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag3 = num2 > num;
					if (flag3)
					{
						float num3 = num / num2;
						containerRect = new Rect(containerRect.xMin + containerRect.width * (1f - num3) * 0.5f, containerRect.yMin, num3 * containerRect.width, containerRect.height);
					}
					else
					{
						float num4 = num2 / num;
						containerRect = new Rect(containerRect.xMin, containerRect.yMin + containerRect.height * (1f - num4) * 0.5f, containerRect.width, num4 * containerRect.height);
					}
					containerRect.position += rect.position * containerRect.size;
					containerRect.size *= rect.size;
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = containerRect;
				uvOut = srcRect;
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x00049B68 File Offset: 0x00047D68
			private static Rect RectIntersection(Rect a, Rect b)
			{
				Rect zero = Rect.zero;
				zero.min = Vector2.Max(a.min, b.min);
				zero.max = Vector2.Min(a.max, b.max);
				zero.size = Vector2.Max(zero.size, Vector2.zero);
				return zero;
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x00049BD0 File Offset: 0x00047DD0
			private static Rect ComputeGeomRect(Sprite sprite)
			{
				Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 rhs in sprite.vertices)
				{
					vector = Vector2.Min(vector, rhs);
					vector2 = Vector2.Max(vector2, rhs);
				}
				return new Rect(vector, vector2 - vector);
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x00049C48 File Offset: 0x00047E48
			private static Rect ComputeUVRect(Sprite sprite)
			{
				Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 rhs in sprite.uv)
				{
					vector = Vector2.Min(vector, rhs);
					vector2 = Vector2.Max(vector2, rhs);
				}
				return new Rect(vector, vector2 - vector);
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00049CC0 File Offset: 0x00047EC0
			private static Rect ApplyPackingRotation(Rect uv, SpritePackingRotation rotation)
			{
				switch (rotation)
				{
				case SpritePackingRotation.FlipHorizontal:
				{
					uv.position += new Vector2(uv.size.x, 0f);
					Vector2 size = uv.size;
					size.x = -size.x;
					uv.size = size;
					break;
				}
				case SpritePackingRotation.FlipVertical:
				{
					uv.position += new Vector2(0f, uv.size.y);
					Vector2 size2 = uv.size;
					size2.y = -size2.y;
					uv.size = size2;
					break;
				}
				case SpritePackingRotation.Rotate180:
					uv.position += uv.size;
					uv.size = -uv.size;
					break;
				}
				return uv;
			}

			// Token: 0x0600128A RID: 4746 RVA: 0x00049DC4 File Offset: 0x00047FC4
			public static MeshGenerationContextUtils.RectangleParams MakeTextured(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				MeshGenerationContextUtils.RectangleParams.AdjustUVsForScaleMode(rect, uv, texture, scaleMode, out rect, out uv);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					texture = texture,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x0600128B RID: 4747 RVA: 0x00049E38 File Offset: 0x00048038
			public static MeshGenerationContextUtils.RectangleParams MakeSprite(Rect containerRect, Rect subRect, Sprite sprite, ScaleMode scaleMode, ContextType panelContext, bool hasRadius, ref Vector4 slices)
			{
				bool flag = sprite.texture == null;
				MeshGenerationContextUtils.RectangleParams result;
				if (flag)
				{
					Debug.LogWarning("Ignoring textureless sprite named \"" + sprite.name + "\", please import as a VectorImage instead");
					MeshGenerationContextUtils.RectangleParams rectangleParams = default(MeshGenerationContextUtils.RectangleParams);
					result = rectangleParams;
				}
				else
				{
					Color color = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
					Rect rect = MeshGenerationContextUtils.RectangleParams.ComputeGeomRect(sprite);
					Rect rect2 = MeshGenerationContextUtils.RectangleParams.ComputeUVRect(sprite);
					Vector4 border = sprite.border;
					bool flag2 = border != Vector4.zero || slices != Vector4.zero;
					bool flag3 = subRect != new Rect(0f, 0f, 1f, 1f);
					bool flag4 = scaleMode == ScaleMode.ScaleAndCrop || flag2 || hasRadius || flag3;
					bool flag5 = flag4 && sprite.packed && sprite.packingRotation > SpritePackingRotation.None;
					if (flag5)
					{
						rect2 = MeshGenerationContextUtils.RectangleParams.ApplyPackingRotation(rect2, sprite.packingRotation);
					}
					bool flag6 = flag3;
					Rect srcRect;
					if (flag6)
					{
						srcRect = subRect;
						srcRect.position *= rect2.size;
						srcRect.position += rect2.position;
						srcRect.size *= rect2.size;
					}
					else
					{
						srcRect = rect2;
					}
					Rect rect3;
					Rect rect4;
					MeshGenerationContextUtils.RectangleParams.AdjustSpriteUVsForScaleMode(containerRect, srcRect, rect, sprite, scaleMode, out rect3, out rect4);
					MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
					{
						rect = rect3,
						uv = rect4,
						color = Color.white,
						texture = (flag4 ? sprite.texture : null),
						sprite = (flag4 ? null : sprite),
						spriteGeomRect = rect,
						scaleMode = scaleMode,
						playmodeTintColor = color,
						meshFlags = (sprite.packed ? MeshGenerationContext.MeshFlags.SkipDynamicAtlas : MeshGenerationContext.MeshFlags.None)
					};
					MeshGenerationContextUtils.RectangleParams rectangleParams2 = rectangleParams;
					Vector4 vector = new Vector4(border.x, border.w, border.z, border.y);
					bool flag7 = slices != Vector4.zero && vector != Vector4.zero && vector != slices;
					if (flag7)
					{
						Debug.LogWarning(string.Format("Sprite \"{0}\" borders {1} are overridden by style slices {2}", sprite.name, vector, slices));
					}
					else
					{
						bool flag8 = slices == Vector4.zero;
						if (flag8)
						{
							slices = vector;
						}
					}
					result = rectangleParams2;
				}
				return result;
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x0004A0BC File Offset: 0x000482BC
			public static MeshGenerationContextUtils.RectangleParams MakeVectorTextured(Rect rect, Rect uv, VectorImage vectorImage, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = (panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					vectorImage = vectorImage,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x0004A120 File Offset: 0x00048320
			internal bool HasRadius(float epsilon)
			{
				return (this.topLeftRadius.x > epsilon && this.topLeftRadius.y > epsilon) || (this.topRightRadius.x > epsilon && this.topRightRadius.y > epsilon) || (this.bottomRightRadius.x > epsilon && this.bottomRightRadius.y > epsilon) || (this.bottomLeftRadius.x > epsilon && this.bottomLeftRadius.y > epsilon);
			}

			// Token: 0x04000878 RID: 2168
			public Rect rect;

			// Token: 0x04000879 RID: 2169
			public Rect uv;

			// Token: 0x0400087A RID: 2170
			public Color color;

			// Token: 0x0400087B RID: 2171
			public Texture texture;

			// Token: 0x0400087C RID: 2172
			public Sprite sprite;

			// Token: 0x0400087D RID: 2173
			public VectorImage vectorImage;

			// Token: 0x0400087E RID: 2174
			public Material material;

			// Token: 0x0400087F RID: 2175
			public ScaleMode scaleMode;

			// Token: 0x04000880 RID: 2176
			public Color playmodeTintColor;

			// Token: 0x04000881 RID: 2177
			public Vector2 topLeftRadius;

			// Token: 0x04000882 RID: 2178
			public Vector2 topRightRadius;

			// Token: 0x04000883 RID: 2179
			public Vector2 bottomRightRadius;

			// Token: 0x04000884 RID: 2180
			public Vector2 bottomLeftRadius;

			// Token: 0x04000885 RID: 2181
			public int leftSlice;

			// Token: 0x04000886 RID: 2182
			public int topSlice;

			// Token: 0x04000887 RID: 2183
			public int rightSlice;

			// Token: 0x04000888 RID: 2184
			public int bottomSlice;

			// Token: 0x04000889 RID: 2185
			public float sliceScale;

			// Token: 0x0400088A RID: 2186
			internal Rect spriteGeomRect;

			// Token: 0x0400088B RID: 2187
			public Vector4 rectInset;

			// Token: 0x0400088C RID: 2188
			internal ColorPage colorPage;

			// Token: 0x0400088D RID: 2189
			internal MeshGenerationContext.MeshFlags meshFlags;
		}

		// Token: 0x02000262 RID: 610
		public struct TextParams
		{
			// Token: 0x0600128E RID: 4750 RVA: 0x0004A1A8 File Offset: 0x000483A8
			public override int GetHashCode()
			{
				int num = this.rect.GetHashCode();
				num = (num * 397 ^ ((this.text != null) ? this.text.GetHashCode() : 0));
				num = (num * 397 ^ ((this.font != null) ? this.font.GetHashCode() : 0));
				num = (num * 397 ^ this.fontDefinition.GetHashCode());
				num = (num * 397 ^ this.fontSize);
				num = (num * 397 ^ (int)this.fontStyle);
				num = (num * 397 ^ this.fontColor.GetHashCode());
				num = (num * 397 ^ (int)this.anchor);
				num = (num * 397 ^ this.wordWrap.GetHashCode());
				num = (num * 397 ^ this.wordWrapWidth.GetHashCode());
				num = (num * 397 ^ this.richText.GetHashCode());
				num = (num * 397 ^ this.playmodeTintColor.GetHashCode());
				num = (num * 397 ^ this.textOverflow.GetHashCode());
				num = (num * 397 ^ this.textOverflowPosition.GetHashCode());
				num = (num * 397 ^ this.overflow.GetHashCode());
				num = (num * 397 ^ this.letterSpacing.GetHashCode());
				num = (num * 397 ^ this.wordSpacing.GetHashCode());
				return num * 397 ^ this.paragraphSpacing.GetHashCode();
			}

			// Token: 0x0600128F RID: 4751 RVA: 0x0004A368 File Offset: 0x00048568
			internal unsafe static MeshGenerationContextUtils.TextParams MakeStyleBased(VisualElement ve, string text)
			{
				ComputedStyle computedStyle = *ve.computedStyle;
				TextElement textElement = ve as TextElement;
				bool flag = textElement == null;
				MeshGenerationContextUtils.TextParams result = default(MeshGenerationContextUtils.TextParams);
				result.rect = ve.contentRect;
				result.text = text;
				result.fontDefinition = computedStyle.unityFontDefinition;
				result.font = TextUtilities.GetFont(ve);
				result.fontSize = (int)computedStyle.fontSize.value;
				result.fontStyle = computedStyle.unityFontStyleAndWeight;
				result.fontColor = computedStyle.color;
				result.anchor = computedStyle.unityTextAlign;
				result.wordWrap = (computedStyle.whiteSpace == WhiteSpace.Normal);
				result.wordWrapWidth = ((computedStyle.whiteSpace == WhiteSpace.Normal) ? ve.contentRect.width : 0f);
				result.richText = (textElement != null && textElement.enableRichText);
				IPanel panel = ve.panel;
				result.playmodeTintColor = ((panel != null && panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				result.textOverflow = computedStyle.textOverflow;
				result.textOverflowPosition = computedStyle.unityTextOverflowPosition;
				result.overflow = computedStyle.overflow;
				result.letterSpacing = (flag ? 0f : computedStyle.letterSpacing);
				result.wordSpacing = (flag ? 0f : computedStyle.wordSpacing);
				result.paragraphSpacing = (flag ? 0f : computedStyle.unityParagraphSpacing);
				result.panel = ve.panel;
				return result;
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x0004A514 File Offset: 0x00048714
			internal static TextNativeSettings GetTextNativeSettings(MeshGenerationContextUtils.TextParams textParams, float scaling)
			{
				return new TextNativeSettings
				{
					text = textParams.text,
					font = TextUtilities.GetFont(textParams),
					size = textParams.fontSize,
					scaling = scaling,
					style = textParams.fontStyle,
					color = textParams.fontColor,
					anchor = textParams.anchor,
					wordWrap = textParams.wordWrap,
					wordWrapWidth = textParams.wordWrapWidth,
					richText = textParams.richText
				};
			}

			// Token: 0x0400088E RID: 2190
			public Rect rect;

			// Token: 0x0400088F RID: 2191
			public string text;

			// Token: 0x04000890 RID: 2192
			public Font font;

			// Token: 0x04000891 RID: 2193
			public FontDefinition fontDefinition;

			// Token: 0x04000892 RID: 2194
			public int fontSize;

			// Token: 0x04000893 RID: 2195
			public Length letterSpacing;

			// Token: 0x04000894 RID: 2196
			public Length wordSpacing;

			// Token: 0x04000895 RID: 2197
			public Length paragraphSpacing;

			// Token: 0x04000896 RID: 2198
			public FontStyle fontStyle;

			// Token: 0x04000897 RID: 2199
			public Color fontColor;

			// Token: 0x04000898 RID: 2200
			public TextAnchor anchor;

			// Token: 0x04000899 RID: 2201
			public bool wordWrap;

			// Token: 0x0400089A RID: 2202
			public float wordWrapWidth;

			// Token: 0x0400089B RID: 2203
			public bool richText;

			// Token: 0x0400089C RID: 2204
			public Color playmodeTintColor;

			// Token: 0x0400089D RID: 2205
			public TextOverflow textOverflow;

			// Token: 0x0400089E RID: 2206
			public TextOverflowPosition textOverflowPosition;

			// Token: 0x0400089F RID: 2207
			public OverflowInternal overflow;

			// Token: 0x040008A0 RID: 2208
			public IPanel panel;
		}
	}
}
