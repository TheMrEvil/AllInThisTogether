using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007F RID: 127
	internal struct CursorPositionStylePainterParameters
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000B940 File Offset: 0x00009B40
		public unsafe static CursorPositionStylePainterParameters GetDefault(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = *ve.computedStyle;
			return new CursorPositionStylePainterParameters
			{
				rect = ve.contentRect,
				text = text,
				font = TextUtilities.GetFont(ve),
				fontSize = (int)computedStyle.fontSize.value,
				fontStyle = computedStyle.unityFontStyleAndWeight,
				anchor = computedStyle.unityTextAlign,
				wordWrapWidth = ((computedStyle.whiteSpace == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = false,
				cursorIndex = 0
			};
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		internal TextNativeSettings GetTextNativeSettings(float scaling)
		{
			return new TextNativeSettings
			{
				text = this.text,
				font = this.font,
				size = this.fontSize,
				scaling = scaling,
				style = this.fontStyle,
				color = Color.white,
				anchor = this.anchor,
				wordWrap = true,
				wordWrapWidth = this.wordWrapWidth,
				richText = this.richText
			};
		}

		// Token: 0x040001A1 RID: 417
		public Rect rect;

		// Token: 0x040001A2 RID: 418
		public string text;

		// Token: 0x040001A3 RID: 419
		public Font font;

		// Token: 0x040001A4 RID: 420
		public int fontSize;

		// Token: 0x040001A5 RID: 421
		public FontStyle fontStyle;

		// Token: 0x040001A6 RID: 422
		public TextAnchor anchor;

		// Token: 0x040001A7 RID: 423
		public float wordWrapWidth;

		// Token: 0x040001A8 RID: 424
		public bool richText;

		// Token: 0x040001A9 RID: 425
		public int cursorIndex;
	}
}
