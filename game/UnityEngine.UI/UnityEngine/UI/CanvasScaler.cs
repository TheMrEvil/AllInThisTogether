using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(Canvas))]
	[ExecuteAlways]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[DisallowMultipleComponent]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000D101 File Offset: 0x0000B301
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000D109 File Offset: 0x0000B309
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000D112 File Offset: 0x0000B312
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000D11A File Offset: 0x0000B31A
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = Mathf.Max(0.01f, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D12D File Offset: 0x0000B32D
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000D138 File Offset: 0x0000B338
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000D1D6 File Offset: 0x0000B3D6
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D1E7 File Offset: 0x0000B3E7
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000D200 File Offset: 0x0000B400
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000D209 File Offset: 0x0000B409
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000D211 File Offset: 0x0000B411
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000D21A File Offset: 0x0000B41A
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000D222 File Offset: 0x0000B422
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = Mathf.Max(1f, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000D235 File Offset: 0x0000B435
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000D23D File Offset: 0x0000B43D
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D248 File Offset: 0x0000B448
		protected CanvasScaler()
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
			Canvas.preWillRenderCanvases += this.Canvas_preWillRenderCanvases;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		private void Canvas_preWillRenderCanvases()
		{
			this.Handle();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D2F7 File Offset: 0x0000B4F7
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			Canvas.preWillRenderCanvases -= this.Canvas_preWillRenderCanvases;
			base.OnDisable();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D328 File Offset: 0x0000B528
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				return;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				return;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D394 File Offset: 0x0000B594
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D3AE File Offset: 0x0000B5AE
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 renderingDisplaySize = this.m_Canvas.renderingDisplaySize;
			int targetDisplay = this.m_Canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				Display display = Display.displays[targetDisplay];
				renderingDisplaySize = new Vector2((float)display.renderingWidth, (float)display.renderingHeight);
			}
			float scaleFactor = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float a = Mathf.Log(renderingDisplaySize.x / this.m_ReferenceResolution.x, 2f);
				float b = Mathf.Log(renderingDisplaySize.y / this.m_ReferenceResolution.y, 2f);
				float p = Mathf.Lerp(a, b, this.m_MatchWidthOrHeight);
				scaleFactor = Mathf.Pow(2f, p);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				scaleFactor = Mathf.Min(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				scaleFactor = Mathf.Max(renderingDisplaySize.x / this.m_ReferenceResolution.x, renderingDisplaySize.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(scaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D500 File Offset: 0x0000B700
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = (dpi == 0f) ? this.m_FallbackScreenDPI : dpi;
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D592 File Offset: 0x0000B792
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040000C2 RID: 194
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040000C3 RID: 195
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040000C4 RID: 196
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040000C5 RID: 197
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040000C6 RID: 198
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040000C7 RID: 199
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040000C8 RID: 200
		private const float kLogBase = 2f;

		// Token: 0x040000C9 RID: 201
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040000CA RID: 202
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040000CB RID: 203
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040000CC RID: 204
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040000CD RID: 205
		private Canvas m_Canvas;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		protected bool m_PresetInfoIsWorld;

		// Token: 0x02000096 RID: 150
		public enum ScaleMode
		{
			// Token: 0x040002AB RID: 683
			ConstantPixelSize,
			// Token: 0x040002AC RID: 684
			ScaleWithScreenSize,
			// Token: 0x040002AD RID: 685
			ConstantPhysicalSize
		}

		// Token: 0x02000097 RID: 151
		public enum ScreenMatchMode
		{
			// Token: 0x040002AF RID: 687
			MatchWidthOrHeight,
			// Token: 0x040002B0 RID: 688
			Expand,
			// Token: 0x040002B1 RID: 689
			Shrink
		}

		// Token: 0x02000098 RID: 152
		public enum Unit
		{
			// Token: 0x040002B3 RID: 691
			Centimeters,
			// Token: 0x040002B4 RID: 692
			Millimeters,
			// Token: 0x040002B5 RID: 693
			Inches,
			// Token: 0x040002B6 RID: 694
			Points,
			// Token: 0x040002B7 RID: 695
			Picas
		}
	}
}
