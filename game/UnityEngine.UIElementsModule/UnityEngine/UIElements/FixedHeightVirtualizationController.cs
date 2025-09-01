using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000113 RID: 275
	internal class FixedHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0002250B File Offset: 0x0002070B
		private float resolvedItemHeight
		{
			get
			{
				return this.m_CollectionView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00022520 File Offset: 0x00020720
		protected override bool VisibleItemPredicate(T i)
		{
			return true;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00022533 File Offset: 0x00020733
		public FixedHeightVirtualizationController(BaseVerticalCollectionView collectionView) : base(collectionView)
		{
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00022540 File Offset: 0x00020740
		public override int GetIndexFromPosition(Vector2 position)
		{
			return (int)(position.y / this.resolvedItemHeight);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00022560 File Offset: 0x00020760
		public override float GetExpectedItemHeight(int index)
		{
			return this.resolvedItemHeight;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00022578 File Offset: 0x00020778
		public override float GetExpectedContentHeight()
		{
			return (float)base.itemsCount * this.resolvedItemHeight;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00022598 File Offset: 0x00020798
		public override void ScrollToItem(int index)
		{
			bool flag = this.visibleItemCount == 0 || index < -1;
			if (!flag)
			{
				float resolvedItemHeight = this.resolvedItemHeight;
				bool flag2 = index == -1;
				if (flag2)
				{
					int num = (int)(base.lastHeight / resolvedItemHeight);
					bool flag3 = base.itemsCount < num;
					if (flag3)
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, 0f);
					}
					else
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, (float)(base.itemsCount + 1) * resolvedItemHeight);
					}
				}
				else
				{
					bool flag4 = this.firstVisibleIndex >= index;
					if (flag4)
					{
						this.m_ScrollView.scrollOffset = Vector2.up * (resolvedItemHeight * (float)index);
					}
					else
					{
						int num2 = (int)(base.lastHeight / resolvedItemHeight);
						bool flag5 = index < this.firstVisibleIndex + num2;
						if (!flag5)
						{
							int num3 = index - num2 + 1;
							float num4 = resolvedItemHeight - (base.lastHeight - (float)num2 * resolvedItemHeight);
							float y = resolvedItemHeight * (float)num3 + num4;
							this.m_ScrollView.scrollOffset = new Vector2(this.m_ScrollView.scrollOffset.x, y);
						}
					}
				}
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000226C4 File Offset: 0x000208C4
		public override void Resize(Vector2 size)
		{
			float resolvedItemHeight = this.resolvedItemHeight;
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.m_ScrollView.contentContainer.style.height = expectedContentHeight;
			float num = Mathf.Max(0f, expectedContentHeight - this.m_ScrollView.contentViewport.layout.height);
			float num2 = Mathf.Min(base.serializedData.scrollOffset.y, num);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(num2);
			int num3 = (int)(this.m_CollectionView.ResolveItemHeight(size.y) / resolvedItemHeight);
			bool flag = num3 > 0;
			if (flag)
			{
				num3 += 2;
			}
			int num4 = Mathf.Min(num3, base.itemsCount);
			bool flag2 = this.visibleItemCount != num4;
			if (flag2)
			{
				int visibleItemCount = this.visibleItemCount;
				bool flag3 = this.visibleItemCount > num4;
				if (flag3)
				{
					int num5 = visibleItemCount - num4;
					for (int i = 0; i < num5; i++)
					{
						int activeItemsIndex = this.m_ActiveItems.Count - 1;
						this.ReleaseItem(activeItemsIndex);
					}
				}
				else
				{
					int num6 = num4 - this.visibleItemCount;
					for (int j = 0; j < num6; j++)
					{
						int newIndex = j + this.firstVisibleIndex + visibleItemCount;
						T orMakeItemAtIndex = this.GetOrMakeItemAtIndex(-1, -1);
						base.Setup(orMakeItemAtIndex, newIndex);
					}
				}
			}
			this.OnScroll(new Vector2(0f, num2));
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00022864 File Offset: 0x00020A64
		public override void OnScroll(Vector2 scrollOffset)
		{
			float num = Mathf.Max(0f, scrollOffset.y);
			float resolvedItemHeight = this.resolvedItemHeight;
			int num2 = (int)(num / resolvedItemHeight);
			this.m_ScrollView.contentContainer.style.paddingTop = (float)num2 * resolvedItemHeight;
			this.m_ScrollView.contentContainer.style.height = (float)base.itemsCount * resolvedItemHeight;
			base.serializedData.scrollOffset.y = scrollOffset.y;
			bool flag = num2 != this.firstVisibleIndex;
			if (flag)
			{
				this.firstVisibleIndex = num2;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = this.firstVisibleIndex < this.m_ActiveItems[0].index;
					if (flag3)
					{
						int num3 = this.m_ActiveItems[0].index - this.firstVisibleIndex;
						List<T> scrollInsertionList = this.m_ScrollInsertionList;
						int num4 = 0;
						while (num4 < num3 && this.m_ActiveItems.Count > 0)
						{
							T t = this.m_ActiveItems[this.m_ActiveItems.Count - 1];
							scrollInsertionList.Add(t);
							this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
							t.rootElement.SendToBack();
							num4++;
						}
						this.m_ActiveItems.InsertRange(0, scrollInsertionList);
						this.m_ScrollInsertionList.Clear();
					}
					else
					{
						bool flag4 = this.firstVisibleIndex < this.m_ActiveItems[this.m_ActiveItems.Count - 1].index;
						if (flag4)
						{
							List<T> scrollInsertionList2 = this.m_ScrollInsertionList;
							int num5 = 0;
							while (this.firstVisibleIndex > this.m_ActiveItems[num5].index)
							{
								T t2 = this.m_ActiveItems[num5];
								scrollInsertionList2.Add(t2);
								num5++;
								t2.rootElement.BringToFront();
							}
							this.m_ActiveItems.RemoveRange(0, num5);
							this.m_ActiveItems.AddRange(scrollInsertionList2);
							scrollInsertionList2.Clear();
						}
					}
					for (int i = 0; i < this.m_ActiveItems.Count; i++)
					{
						int newIndex = i + this.firstVisibleIndex;
						base.Setup(this.m_ActiveItems[i], newIndex);
					}
				}
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00022B0C File Offset: 0x00020D0C
		internal override T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T orMakeItemAtIndex = base.GetOrMakeItemAtIndex(activeItemIndex, scrollViewIndex);
			orMakeItemAtIndex.rootElement.style.height = this.resolvedItemHeight;
			return orMakeItemAtIndex;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00022B4C File Offset: 0x00020D4C
		internal override void EndDrag(int dropIndex)
		{
			this.m_DraggedItem.rootElement.style.height = this.resolvedItemHeight;
			bool flag = this.firstVisibleIndex > this.m_DraggedItem.index;
			if (flag)
			{
				this.m_ScrollView.verticalScroller.value = base.serializedData.scrollOffset.y - this.resolvedItemHeight;
			}
			base.EndDrag(dropIndex);
		}
	}
}
