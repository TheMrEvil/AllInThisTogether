using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000024 RID: 36
	[AddComponentMenu("Layout/Layout Element", 140)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000E400 File Offset: 0x0000C600
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E416 File Offset: 0x0000C616
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000E41E File Offset: 0x0000C61E
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E434 File Offset: 0x0000C634
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000E43C File Offset: 0x0000C63C
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000E452 File Offset: 0x0000C652
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000E45A File Offset: 0x0000C65A
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000E470 File Offset: 0x0000C670
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000E478 File Offset: 0x0000C678
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E48E File Offset: 0x0000C68E
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000E496 File Offset: 0x0000C696
		public virtual int layoutPriority
		{
			get
			{
				return this.m_LayoutPriority;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_LayoutPriority, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		protected LayoutElement()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E508 File Offset: 0x0000C708
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E516 File Offset: 0x0000C716
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E51E File Offset: 0x0000C71E
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E52C File Offset: 0x0000C72C
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E534 File Offset: 0x0000C734
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E53C File Offset: 0x0000C73C
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private float m_FlexibleHeight = -1f;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		private int m_LayoutPriority = 1;
	}
}
