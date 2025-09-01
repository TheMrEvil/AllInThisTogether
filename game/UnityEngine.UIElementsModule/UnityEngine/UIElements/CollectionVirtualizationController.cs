using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010E RID: 270
	internal abstract class CollectionVirtualizationController
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000898 RID: 2200
		// (set) Token: 0x06000899 RID: 2201
		public abstract int firstVisibleIndex { get; protected set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600089A RID: 2202
		public abstract int visibleItemCount { get; }

		// Token: 0x0600089B RID: 2203 RVA: 0x00020488 File Offset: 0x0001E688
		protected CollectionVirtualizationController(ScrollView scrollView)
		{
			this.m_ScrollView = scrollView;
		}

		// Token: 0x0600089C RID: 2204
		public abstract void Refresh(bool rebuild);

		// Token: 0x0600089D RID: 2205
		public abstract void ScrollToItem(int id);

		// Token: 0x0600089E RID: 2206
		public abstract void Resize(Vector2 size);

		// Token: 0x0600089F RID: 2207
		public abstract void OnScroll(Vector2 offset);

		// Token: 0x060008A0 RID: 2208
		public abstract int GetIndexFromPosition(Vector2 position);

		// Token: 0x060008A1 RID: 2209
		public abstract float GetExpectedItemHeight(int index);

		// Token: 0x060008A2 RID: 2210
		public abstract float GetExpectedContentHeight();

		// Token: 0x060008A3 RID: 2211
		public abstract void OnFocus(VisualElement leafTarget);

		// Token: 0x060008A4 RID: 2212
		public abstract void OnBlur(VisualElement willFocus);

		// Token: 0x060008A5 RID: 2213
		public abstract void UpdateBackground();

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008A6 RID: 2214
		public abstract IEnumerable<ReusableCollectionItem> activeItems { get; }

		// Token: 0x060008A7 RID: 2215
		internal abstract void StartDragItem(ReusableCollectionItem item);

		// Token: 0x060008A8 RID: 2216
		internal abstract void EndDrag(int dropIndex);

		// Token: 0x04000386 RID: 902
		protected readonly ScrollView m_ScrollView;
	}
}
