using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001B RID: 27
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000D5EE File Offset: 0x0000B7EE
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000D5F6 File Offset: 0x0000B7F6
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000D60C File Offset: 0x0000B80C
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D62E File Offset: 0x0000B82E
		protected ContentSizeFitter()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D636 File Offset: 0x0000B836
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D644 File Offset: 0x0000B844
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D662 File Offset: 0x0000B862
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D66C File Offset: 0x0000B86C
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitMode = (axis == 0) ? this.horizontalFit : this.verticalFit;
			if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.None);
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
			if (fitMode == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
				return;
			}
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D70C File Offset: 0x0000B90C
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D715 File Offset: 0x0000B915
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000D4 RID: 212
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000099 RID: 153
		public enum FitMode
		{
			// Token: 0x040002B9 RID: 697
			Unconstrained,
			// Token: 0x040002BA RID: 698
			MinSize,
			// Token: 0x040002BB RID: 699
			PreferredSize
		}
	}
}
