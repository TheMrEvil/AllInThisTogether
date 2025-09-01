using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000146 RID: 326
	public class Image : VisualElement
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002AB00 File Offset: 0x00028D00
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002AB18 File Offset: 0x00028D18
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = value != null && (this.m_Sprite != null || this.m_VectorImage != null);
				if (flag)
				{
					string str = (this.m_Sprite != null) ? "sprite" : "vector image";
					Debug.LogWarning("Image object already has a background, removing " + str);
					this.m_Sprite = null;
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = (value != null);
				bool flag2 = this.m_Image != value;
				if (flag2)
				{
					this.m_Image = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_Image == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002AC0C File Offset: 0x00028E0C
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = value != null && (this.m_Image != null || this.m_VectorImage != null);
				if (flag)
				{
					string str = (this.m_Image != null) ? "texture" : "vector image";
					Debug.LogWarning("Image object already has a background, removing " + str);
					this.m_Image = null;
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = (value != null);
				bool flag2 = this.m_Sprite != value;
				if (flag2)
				{
					this.m_Sprite = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0002ACD0 File Offset: 0x00028ED0
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && (this.m_Image != null || this.m_Sprite != null);
				if (flag)
				{
					string str = (this.m_Image != null) ? "texture" : "sprite";
					Debug.LogWarning("Image object already has a background, removing " + str);
					this.m_Image = null;
					this.m_Sprite = null;
				}
				this.m_ImageIsInline = (value != null);
				bool flag2 = this.m_VectorImage != value;
				if (flag2)
				{
					this.m_VectorImage = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_VectorImage == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002ADAC File Offset: 0x00028FAC
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				bool flag = this.sprite != null;
				if (flag)
				{
					Debug.LogError("Cannot set sourceRect on a sprite image");
				}
				else
				{
					this.CalculateUV(value);
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002ADF8 File Offset: 0x00028FF8
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002AE10 File Offset: 0x00029010
		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002AE1C File Offset: 0x0002901C
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0002AE34 File Offset: 0x00029034
		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleModeIsInline = true;
				this.SetScaleMode(value);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002AE48 File Offset: 0x00029048
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002AE60 File Offset: 0x00029060
		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				this.m_TintColorIsInline = true;
				bool flag = this.m_TintColor != value;
				if (flag)
				{
					this.m_TintColor = value;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002AE9C File Offset: 0x0002909C
		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleToFit;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002AF30 File Offset: 0x00029130
		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 zero = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				zero = new Vector2((float)texture.width, (float)texture.height);
			}
			return zero;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002AF6C File Offset: 0x0002916C
		private Vector2 GetTextureDisplaySize(Sprite sprite)
		{
			Vector2 result = Vector2.zero;
			bool flag = sprite != null;
			if (flag)
			{
				result = sprite.bounds.size * sprite.pixelsPerUnit;
			}
			return result;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002AFB0 File Offset: 0x000291B0
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(num, num2);
			}
			else
			{
				Vector2 vector = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					vector = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						vector = this.GetTextureDisplaySize(this.sprite);
					}
					else
					{
						vector = this.vectorImage.size;
					}
				}
				Rect sourceRect = this.sourceRect;
				bool flag4 = sourceRect != Rect.zero;
				num = (flag4 ? Mathf.Abs(sourceRect.width) : vector.x);
				num2 = (flag4 ? Mathf.Abs(sourceRect.height) : vector.y);
				bool flag5 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					num = Mathf.Min(num, desiredWidth);
				}
				bool flag6 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag6)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
				result = new Vector2(num, num2);
			}
			return result;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002B0DC File Offset: 0x000292DC
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			if (!flag)
			{
				MeshGenerationContextUtils.RectangleParams rectParams = default(MeshGenerationContextUtils.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectParams = MeshGenerationContextUtils.RectangleParams.MakeTextured(base.contentRect, this.uv, this.image, this.scaleMode, base.panel.contextType);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						Vector4 zero = Vector4.zero;
						rectParams = MeshGenerationContextUtils.RectangleParams.MakeSprite(base.contentRect, this.uv, this.sprite, this.scaleMode, base.panel.contextType, false, ref zero);
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							rectParams = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(base.contentRect, this.uv, this.vectorImage, this.scaleMode, base.panel.contextType);
						}
					}
				}
				rectParams.color = this.tintColor;
				mgc.Rectangle(rectParams);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002B1FC File Offset: 0x000293FC
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			Texture2D image = null;
			Sprite sprite = null;
			VectorImage vectorImage = null;
			Color white = Color.white;
			ICustomStyle customStyle = e.customStyle;
			bool flag = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_ImageProperty, out image);
			if (flag)
			{
				this.m_Image = image;
				this.m_Sprite = null;
				this.m_VectorImage = null;
			}
			bool flag2 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_SpriteProperty, out sprite);
			if (flag2)
			{
				this.m_Image = null;
				this.m_Sprite = sprite;
				this.m_VectorImage = null;
			}
			bool flag3 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_VectorImageProperty, out vectorImage);
			if (flag3)
			{
				this.m_Image = null;
				this.m_Sprite = null;
				this.m_VectorImage = vectorImage;
			}
			string value;
			bool flag4 = !this.m_ScaleModeIsInline && customStyle.TryGetValue(Image.s_ScaleModeProperty, out value);
			if (flag4)
			{
				int scaleMode;
				StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.ScaleMode, value, out scaleMode);
				this.SetScaleMode((ScaleMode)scaleMode);
			}
			bool flag5 = !this.m_TintColorIsInline && customStyle.TryGetValue(Image.s_TintColorProperty, out white);
			if (flag5)
			{
				this.m_TintColor = white;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002B320 File Offset: 0x00029520
		private void SetScaleMode(ScaleMode mode)
		{
			bool flag = this.m_ScaleMode != mode;
			if (flag)
			{
				this.m_ScaleMode = mode;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002B354 File Offset: 0x00029554
		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / vector.x;
				this.m_UV.width = srcRect.width / vector.x;
				this.m_UV.height = srcRect.height / vector.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / vector.y;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002B44C File Offset: 0x0002964C
		private Rect GetSourceRect()
		{
			Rect zero = Rect.zero;
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				zero.x = this.uv.x * vector.x;
				zero.width = this.uv.width * vector.x;
				zero.y = (1f - this.uv.y - this.uv.height) * vector.y;
				zero.height = this.uv.height * vector.y;
			}
			return zero;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002B548 File Offset: 0x00029748
		// Note: this type is marked as 'beforefieldinit'.
		static Image()
		{
		}

		// Token: 0x040004A6 RID: 1190
		private ScaleMode m_ScaleMode;

		// Token: 0x040004A7 RID: 1191
		private Texture m_Image;

		// Token: 0x040004A8 RID: 1192
		private Sprite m_Sprite;

		// Token: 0x040004A9 RID: 1193
		private VectorImage m_VectorImage;

		// Token: 0x040004AA RID: 1194
		private Rect m_UV;

		// Token: 0x040004AB RID: 1195
		private Color m_TintColor;

		// Token: 0x040004AC RID: 1196
		private bool m_ImageIsInline;

		// Token: 0x040004AD RID: 1197
		private bool m_ScaleModeIsInline;

		// Token: 0x040004AE RID: 1198
		private bool m_TintColorIsInline;

		// Token: 0x040004AF RID: 1199
		public static readonly string ussClassName = "unity-image";

		// Token: 0x040004B0 RID: 1200
		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		// Token: 0x040004B1 RID: 1201
		private static CustomStyleProperty<Sprite> s_SpriteProperty = new CustomStyleProperty<Sprite>("--unity-image");

		// Token: 0x040004B2 RID: 1202
		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		// Token: 0x040004B3 RID: 1203
		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		// Token: 0x040004B4 RID: 1204
		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		// Token: 0x02000147 RID: 327
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
			// Token: 0x06000AB5 RID: 2741 RVA: 0x0002B5AA File Offset: 0x000297AA
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000148 RID: 328
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002B5B4 File Offset: 0x000297B4
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B5D3 File Offset: 0x000297D3
			public UxmlTraits()
			{
			}

			// Token: 0x02000149 RID: 329
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__1 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000AB8 RID: 2744 RVA: 0x0002B5DC File Offset: 0x000297DC
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__1(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000AB9 RID: 2745 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000ABA RID: 2746 RVA: 0x0002B5FC File Offset: 0x000297FC
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						return false;
					}
					this.<>1__state = -1;
					return false;
				}

				// Token: 0x17000213 RID: 531
				// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002B622 File Offset: 0x00029822
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000ABC RID: 2748 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000214 RID: 532
				// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002B622 File Offset: 0x00029822
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000ABE RID: 2750 RVA: 0x0002B62C File Offset: 0x0002982C
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					Image.UxmlTraits.<get_uxmlChildElementsDescription>d__1 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new Image.UxmlTraits.<get_uxmlChildElementsDescription>d__1(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000ABF RID: 2751 RVA: 0x0002B674 File Offset: 0x00029874
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x040004B5 RID: 1205
				private int <>1__state;

				// Token: 0x040004B6 RID: 1206
				private UxmlChildElementDescription <>2__current;

				// Token: 0x040004B7 RID: 1207
				private int <>l__initialThreadId;

				// Token: 0x040004B8 RID: 1208
				public Image.UxmlTraits <>4__this;
			}
		}
	}
}
