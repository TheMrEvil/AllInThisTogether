using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010F RID: 271
	internal class DynamicHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00020499 File Offset: 0x0001E699
		internal IDictionary<int, float> itemHeightCache
		{
			get
			{
				return this.m_ItemHeightCache;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000204A4 File Offset: 0x0001E6A4
		private float defaultExpectedHeight
		{
			get
			{
				bool flag = this.m_MinimumItemHeight > 0f;
				float result;
				if (flag)
				{
					result = this.m_MinimumItemHeight;
				}
				else
				{
					bool flag2 = this.m_CollectionView.m_ItemHeightIsInline && this.m_CollectionView.fixedItemHeight > 0f;
					if (flag2)
					{
						result = this.m_CollectionView.fixedItemHeight;
					}
					else
					{
						result = (float)BaseVerticalCollectionView.s_DefaultItemHeight;
					}
				}
				return result;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002050A File Offset: 0x0001E70A
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00020517 File Offset: 0x0001E717
		private float contentPadding
		{
			get
			{
				return base.serializedData.contentPadding;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.paddingTop = value;
				base.serializedData.contentPadding = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00020553 File Offset: 0x0001E753
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00020560 File Offset: 0x0001E760
		private float contentHeight
		{
			get
			{
				return base.serializedData.contentHeight;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.height = value;
				base.serializedData.contentHeight = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0002059C File Offset: 0x0001E79C
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x000205A9 File Offset: 0x0001E7A9
		private int anchoredIndex
		{
			get
			{
				return base.serializedData.anchoredItemIndex;
			}
			set
			{
				base.serializedData.anchoredItemIndex = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000205C4 File Offset: 0x0001E7C4
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x000205D1 File Offset: 0x0001E7D1
		private float anchorOffset
		{
			get
			{
				return base.serializedData.anchorOffset;
			}
			set
			{
				base.serializedData.anchorOffset = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000205EC File Offset: 0x0001E7EC
		private float viewportMaxOffset
		{
			get
			{
				return base.serializedData.scrollOffset.y + this.m_ScrollView.contentViewport.layout.height;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00004E8A File Offset: 0x0000308A
		protected override bool alwaysRebindOnRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00020624 File Offset: 0x0001E824
		public DynamicHeightVirtualizationController(BaseVerticalCollectionView collectionView) : base(collectionView)
		{
			this.m_FillCallback = new Action(this.Fill);
			this.m_ScrollCallback = new Action(this.OnScrollUpdate);
			this.m_GeometryChangedCallback = new Action<ReusableCollectionItem>(this.OnRecycledItemGeometryChanged);
			this.m_IndexOutOfBoundsPredicate = new Predicate<int>(this.IsIndexOutOfBounds);
			this.m_ScrollResetCallback = new Action(this.ResetScroll);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public override void Refresh(bool rebuild)
		{
			this.CleanItemHeightCache();
			int count = this.m_ActiveItems.Count;
			bool flag = false;
			if (rebuild)
			{
				this.m_WaitingCache.Clear();
			}
			else
			{
				flag |= (this.m_WaitingCache.RemoveWhere(this.m_IndexOutOfBoundsPredicate) > 0);
			}
			base.Refresh(rebuild);
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			bool flag2 = this.m_CollectionView.HasValidDataAndBindings();
			if (flag2)
			{
				bool flag3 = flag || count != this.m_ActiveItems.Count;
				if (flag3)
				{
					this.contentHeight = this.GetExpectedContentHeight();
					float highValueWithoutNotify = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
					this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(highValueWithoutNotify);
					this.m_ScrollView.verticalScroller.value = base.serializedData.scrollOffset.y;
					base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002081C File Offset: 0x0001EA1C
		public override void ScrollToItem(int index)
		{
			bool flag = index < -1;
			if (!flag)
			{
				float height = this.m_ScrollView.contentContainer.layout.height;
				float height2 = this.m_ScrollView.contentViewport.layout.height;
				bool flag2 = index == -1;
				if (flag2)
				{
					this.m_ForcedLastVisibleItem = base.itemsCount - 1;
					this.m_ForcedFirstVisibleItem = -1;
					this.m_StickToBottom = true;
					this.m_ScrollView.scrollOffset = new Vector2(0f, (height2 >= height) ? 0f : height);
				}
				else
				{
					bool flag3 = this.firstVisibleIndex >= index;
					if (flag3)
					{
						this.m_ForcedFirstVisibleItem = index;
						this.m_ForcedLastVisibleItem = -1;
						this.m_ScrollView.scrollOffset = new Vector2(0f, this.GetContentHeightForIndex(index - 1));
					}
					else
					{
						float contentHeightForIndex = this.GetContentHeightForIndex(index);
						bool flag4 = contentHeightForIndex < this.contentPadding + height2;
						if (!flag4)
						{
							float y = contentHeightForIndex - height2 + (float)BaseVerticalCollectionView.s_DefaultItemHeight;
							this.m_ForcedLastVisibleItem = index;
							this.m_ForcedFirstVisibleItem = -1;
							this.m_ScrollView.scrollOffset = new Vector2(0f, y);
						}
					}
				}
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00020950 File Offset: 0x0001EB50
		public override void Resize(Vector2 size)
		{
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
			float height = this.m_ScrollView.contentViewport.layout.height;
			float num = Mathf.Max(0f, this.contentHeight - height);
			float valueWithoutNotify = Mathf.Min(base.serializedData.scrollOffset.y, num);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(valueWithoutNotify);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
			float num2 = this.m_CollectionView.ResolveItemHeight(size.y);
			int num3 = Mathf.CeilToInt(num2 / this.defaultExpectedHeight);
			int num4 = num3;
			bool flag = num4 <= 0;
			if (!flag)
			{
				num4 += 2;
				int num5 = Mathf.Min(num4, base.itemsCount);
				bool flag2 = this.m_ActiveItems.Count != num5;
				if (flag2)
				{
					int count = this.m_ActiveItems.Count;
					bool flag3 = count > num5;
					if (flag3)
					{
						int num6 = count - num5;
						for (int i = 0; i < num6; i++)
						{
							int activeItemsIndex = this.m_ActiveItems.Count - 1;
							this.ReleaseItem(activeItemsIndex);
						}
					}
					else
					{
						int num7 = num5 - this.m_ActiveItems.Count;
						for (int j = 0; j < num7; j++)
						{
							int num8 = j + this.firstVisibleIndex + count;
							T orMakeItemAtIndex = this.GetOrMakeItemAtIndex(-1, -1);
							bool flag4 = this.IsIndexOutOfBounds(num8);
							if (flag4)
							{
								this.HideItem(this.m_ActiveItems.Count - 1);
							}
							else
							{
								base.Setup(orMakeItemAtIndex, num8);
								this.MarkWaitingForLayout(orMakeItemAtIndex);
							}
						}
					}
				}
				this.ScheduleFill();
				this.ScheduleScrollDirectionReset();
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00020B5C File Offset: 0x0001ED5C
		public override void OnScroll(Vector2 scrollOffset)
		{
			bool flag = this.m_DelayedScrollOffset == scrollOffset;
			if (!flag)
			{
				this.m_DelayedScrollOffset = scrollOffset;
				bool flag2 = this.m_ForcedFirstVisibleItem != -1 || this.m_ForcedLastVisibleItem != -1;
				if (flag2)
				{
					this.OnScrollUpdate();
					this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
				}
				else
				{
					DynamicHeightVirtualizationController<T>.VirtualizationChange lastChange = this.m_LastChange;
					bool flag3 = lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize || lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
					if (flag3)
					{
						float height = this.m_ScrollView.contentViewport.layout.height;
						float num = Mathf.Max(0f, this.contentHeight - height);
						float valueWithoutNotify = Mathf.Min(base.serializedData.scrollOffset.y, num);
						this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
						this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(valueWithoutNotify);
						base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
					}
					else
					{
						this.ScheduleScroll();
					}
				}
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00020C78 File Offset: 0x0001EE78
		private void OnScrollUpdate()
		{
			Vector2 vector = float.IsNegativeInfinity(this.m_DelayedScrollOffset.y) ? base.serializedData.scrollOffset : this.m_DelayedScrollOffset;
			bool flag = float.IsNaN(this.m_ScrollView.contentViewport.layout.height) || float.IsNaN(vector.y);
			if (!flag)
			{
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Scroll;
				float expectedContentHeight = this.GetExpectedContentHeight();
				this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
				this.m_ScrollDirection = ((vector.y < base.serializedData.scrollOffset.y) ? DynamicHeightVirtualizationController<T>.ScrollDirection.Up : DynamicHeightVirtualizationController<T>.ScrollDirection.Down);
				float num = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
				bool flag2 = vector.y <= 0f;
				if (flag2)
				{
					this.m_ForcedFirstVisibleItem = 0;
				}
				this.m_StickToBottom = (num > 0f && Math.Abs(vector.y - this.m_ScrollView.verticalScroller.highValue) < float.Epsilon);
				base.serializedData.scrollOffset = vector;
				this.m_CollectionView.SaveViewData();
				int num2 = (this.m_ForcedFirstVisibleItem != -1) ? this.m_ForcedFirstVisibleItem : this.GetFirstVisibleItem(base.serializedData.scrollOffset.y);
				float contentHeightForIndex = this.GetContentHeightForIndex(num2 - 1);
				this.contentPadding = contentHeightForIndex;
				this.m_ForcedFirstVisibleItem = -1;
				bool flag3 = num2 != this.firstVisibleIndex;
				if (flag3)
				{
					this.CycleItems(num2);
				}
				else
				{
					this.Fill();
				}
				this.ScheduleScrollDirectionReset();
				this.m_DelayedScrollOffset = Vector2.negativeInfinity;
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00020E3C File Offset: 0x0001F03C
		private void CycleItems(int firstIndex)
		{
			bool flag = firstIndex == this.firstVisibleIndex;
			if (!flag)
			{
				T firstVisibleItem = base.firstVisibleItem;
				this.contentPadding = this.GetContentHeightForIndex(firstIndex - 1);
				this.firstVisibleIndex = firstIndex;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = firstVisibleItem == null || this.m_ActiveItems.Count <= Mathf.Abs(this.firstVisibleIndex - firstVisibleItem.index);
					if (!flag3)
					{
						bool flag4 = this.firstVisibleIndex < firstVisibleItem.index;
						if (flag4)
						{
							int num = firstVisibleItem.index - this.firstVisibleIndex;
							List<T> scrollInsertionList = this.m_ScrollInsertionList;
							for (int i = 0; i < num; i++)
							{
								T t = this.m_ActiveItems[this.m_ActiveItems.Count - 1];
								scrollInsertionList.Insert(0, t);
								this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
								t.rootElement.SendToBack();
							}
							this.m_ActiveItems.InsertRange(0, scrollInsertionList);
							this.m_ScrollInsertionList.Clear();
						}
						else
						{
							List<T> scrollInsertionList2 = this.m_ScrollInsertionList;
							int num2 = 0;
							while (this.firstVisibleIndex > this.m_ActiveItems[num2].index)
							{
								T t2 = this.m_ActiveItems[num2];
								scrollInsertionList2.Add(t2);
								num2++;
								t2.rootElement.BringToFront();
							}
							this.m_ActiveItems.RemoveRange(0, num2);
							this.m_ActiveItems.AddRange(scrollInsertionList2);
							this.m_ScrollInsertionList.Clear();
						}
					}
					float num3 = this.contentPadding;
					for (int j = 0; j < this.m_ActiveItems.Count; j++)
					{
						T t3 = this.m_ActiveItems[j];
						int num4 = this.firstVisibleIndex + j;
						int index = t3.index;
						bool flag5 = t3.rootElement.style.display == DisplayStyle.Flex;
						this.m_WaitingCache.Remove(index);
						bool flag6 = this.IsIndexOutOfBounds(num4);
						if (flag6)
						{
							this.HideItem(j);
						}
						else
						{
							base.Setup(t3, num4);
							bool flag7 = num3 > this.viewportMaxOffset;
							bool flag8 = flag7;
							if (flag8)
							{
								this.HideItem(j);
							}
							else
							{
								bool flag9 = num4 != index || !flag5;
								if (flag9)
								{
									this.MarkWaitingForLayout(t3);
								}
							}
							num3 += this.GetExpectedItemHeight(num4);
						}
					}
				}
				bool flag10 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
				if (flag10)
				{
					this.UpdateAnchor();
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00021140 File Offset: 0x0001F340
		private bool NeedsFill()
		{
			bool flag = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.None || this.anchoredIndex < 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				T t = base.lastVisibleItem;
				int num = (t != null) ? t.index : -1;
				float num2 = this.contentPadding;
				bool flag2 = num2 > base.serializedData.scrollOffset.y;
				if (flag2)
				{
					result = true;
				}
				else
				{
					for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
					{
						bool flag3 = num2 >= this.viewportMaxOffset;
						if (flag3)
						{
							break;
						}
						num2 += this.GetExpectedItemHeight(i);
						bool flag4 = i > num;
						if (flag4)
						{
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00021200 File Offset: 0x0001F400
		private void Fill()
		{
			bool flag = !this.m_CollectionView.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.m_ActiveItems.Count == 0;
				if (flag2)
				{
					this.contentHeight = 0f;
					this.contentPadding = 0f;
				}
				else
				{
					bool flag3 = this.anchoredIndex < 0;
					if (!flag3)
					{
						bool flag4 = this.contentPadding > this.contentHeight;
						if (flag4)
						{
							this.OnScrollUpdate();
						}
						else
						{
							float num = this.contentPadding;
							float num2 = this.contentPadding;
							int num3 = 0;
							for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
							{
								bool flag5 = num2 >= this.viewportMaxOffset;
								if (flag5)
								{
									break;
								}
								num2 += this.GetExpectedItemHeight(i);
								T t = this.m_ActiveItems[num3++];
								bool flag6 = t.index != i || t.rootElement.style.display == DisplayStyle.None;
								if (flag6)
								{
									base.Setup(t, i);
									this.MarkWaitingForLayout(t);
								}
								bool flag7 = num3 >= this.m_ActiveItems.Count;
								if (flag7)
								{
									break;
								}
							}
							bool flag8 = this.firstVisibleIndex > 0 && this.contentPadding > base.serializedData.scrollOffset.y;
							if (flag8)
							{
								List<T> scrollInsertionList = this.m_ScrollInsertionList;
								for (int j = this.m_ActiveItems.Count - 1; j >= num3; j--)
								{
									bool flag9 = this.firstVisibleIndex == 0;
									if (flag9)
									{
										break;
									}
									T t2 = this.m_ActiveItems[j];
									scrollInsertionList.Insert(0, t2);
									this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
									t2.rootElement.SendToBack();
									int num4 = this.firstVisibleIndex - 1;
									this.firstVisibleIndex = num4;
									int num5 = num4;
									base.Setup(t2, num5);
									this.MarkWaitingForLayout(t2);
									num -= this.GetExpectedItemHeight(num5);
									bool flag10 = num < base.serializedData.scrollOffset.y;
									if (flag10)
									{
										break;
									}
								}
								this.m_ActiveItems.InsertRange(0, scrollInsertionList);
								this.m_ScrollInsertionList.Clear();
							}
							this.contentPadding = num;
							this.contentHeight = this.GetExpectedContentHeight();
							bool flag11 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
							if (flag11)
							{
								this.UpdateAnchor();
							}
							bool flag12 = this.m_WaitingCache.Count == 0;
							if (flag12)
							{
								this.ResetScroll();
								this.ApplyScrollViewUpdate(true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000214E0 File Offset: 0x0001F6E0
		private void UpdateScrollViewContainer(float previousHeight, float newHeight)
		{
			bool stickToBottom = this.m_StickToBottom;
			if (!stickToBottom)
			{
				bool flag = this.m_ForcedLastVisibleItem >= 0;
				if (flag)
				{
					float contentHeightForIndex = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					base.serializedData.scrollOffset.y = contentHeightForIndex + (float)BaseVerticalCollectionView.s_DefaultItemHeight - this.m_ScrollView.contentViewport.layout.height;
				}
				else
				{
					bool flag2 = this.m_ScrollDirection == DynamicHeightVirtualizationController<T>.ScrollDirection.Up;
					if (flag2)
					{
						SerializedVirtualizationData serializedData = base.serializedData;
						serializedData.scrollOffset.y = serializedData.scrollOffset.y + (newHeight - previousHeight);
					}
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021578 File Offset: 0x0001F778
		private void ApplyScrollViewUpdate(bool dimensionsOnly = false)
		{
			float contentPadding = this.contentPadding;
			float y = base.serializedData.scrollOffset.y;
			float num = y - contentPadding;
			bool flag = this.anchoredIndex >= 0;
			if (flag)
			{
				bool flag2 = this.firstVisibleIndex != this.anchoredIndex;
				if (flag2)
				{
					this.CycleItems(this.anchoredIndex);
					this.ScheduleFill();
				}
				this.firstVisibleIndex = this.anchoredIndex;
				num = this.anchorOffset;
			}
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = expectedContentHeight;
			this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
			float num2 = Mathf.Max(0f, expectedContentHeight - this.m_ScrollView.contentViewport.layout.height);
			float valueWithoutNotify = Mathf.Min(this.contentPadding + num, num2);
			bool flag3 = this.m_StickToBottom && num2 > 0f;
			if (flag3)
			{
				valueWithoutNotify = num2;
			}
			else
			{
				bool flag4 = this.m_ForcedLastVisibleItem != -1;
				if (flag4)
				{
					float contentHeightForIndex = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					float num3 = contentHeightForIndex + (float)BaseVerticalCollectionView.s_DefaultItemHeight - this.m_ScrollView.contentViewport.layout.height;
					valueWithoutNotify = num3;
				}
			}
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num2);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(valueWithoutNotify);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.slider.value;
			bool flag5 = dimensionsOnly || this.m_LastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			if (flag5)
			{
				this.ScheduleScrollDirectionReset();
			}
			else
			{
				bool flag6 = this.NeedsFill();
				if (flag6)
				{
					this.Fill();
				}
				else
				{
					float num4 = this.contentPadding;
					int firstVisibleIndex = this.firstVisibleIndex;
					List<T> scrollInsertionList = this.m_ScrollInsertionList;
					int num5 = 0;
					for (int i = 0; i < this.m_ActiveItems.Count; i++)
					{
						T t = this.m_ActiveItems[i];
						int index = t.index;
						bool flag7 = index < 0;
						if (flag7)
						{
							break;
						}
						float expectedItemHeight = this.GetExpectedItemHeight(index);
						bool flag8 = this.m_ActiveItems[i].rootElement.style.display == DisplayStyle.Flex;
						if (flag8)
						{
							bool flag9 = num4 + expectedItemHeight <= base.serializedData.scrollOffset.y;
							if (flag9)
							{
								t.rootElement.BringToFront();
								this.HideItem(i);
								scrollInsertionList.Add(t);
								num5++;
								int firstVisibleIndex2 = this.firstVisibleIndex;
								this.firstVisibleIndex = firstVisibleIndex2 + 1;
							}
							else
							{
								bool flag10 = num4 > this.viewportMaxOffset;
								if (flag10)
								{
									this.HideItem(i);
								}
							}
						}
						num4 += this.GetExpectedItemHeight(index);
					}
					this.m_ActiveItems.RemoveRange(0, num5);
					this.m_ActiveItems.AddRange(scrollInsertionList);
					this.m_ScrollInsertionList.Clear();
					bool flag11 = this.firstVisibleIndex != firstVisibleIndex;
					if (flag11)
					{
						this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
						this.UpdateAnchor();
					}
					this.ScheduleScrollDirectionReset();
					this.m_ForcedLastVisibleItem = -1;
					this.m_CollectionView.SaveViewData();
				}
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000218FF File Offset: 0x0001FAFF
		private void UpdateAnchor()
		{
			this.anchoredIndex = this.firstVisibleIndex;
			this.anchorOffset = base.serializedData.scrollOffset.y - this.contentPadding;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00021930 File Offset: 0x0001FB30
		private void ScheduleFill()
		{
			bool flag = this.m_ScheduledItem == null;
			if (flag)
			{
				this.m_ScheduledItem = this.m_CollectionView.schedule.Execute(this.m_FillCallback);
			}
			else
			{
				this.m_ScheduledItem.Pause();
				this.m_ScheduledItem.Resume();
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00021984 File Offset: 0x0001FB84
		private void ScheduleScroll()
		{
			bool flag = this.m_ScrollScheduledItem == null;
			if (flag)
			{
				this.m_ScrollScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollCallback);
			}
			else
			{
				this.m_ScrollScheduledItem.Pause();
				this.m_ScrollScheduledItem.Resume();
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000219D8 File Offset: 0x0001FBD8
		private void ScheduleScrollDirectionReset()
		{
			bool flag = this.m_ScrollResetScheduledItem == null;
			if (flag)
			{
				this.m_ScrollResetScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollResetCallback);
			}
			else
			{
				this.m_ScrollResetScheduledItem.Pause();
				this.m_ScrollResetScheduledItem.Resume();
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00021A2A File Offset: 0x0001FC2A
		private void ResetScroll()
		{
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			this.m_ScrollView.UpdateContentViewTransform();
			this.UpdateAnchor();
			this.m_CollectionView.SaveViewData();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00021A5C File Offset: 0x0001FC5C
		public override int GetIndexFromPosition(Vector2 position)
		{
			int num = 0;
			for (float num2 = 0f; num2 < position.y; num2 += this.GetExpectedItemHeight(num++))
			{
			}
			return num - 1;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00021A98 File Offset: 0x0001FC98
		public override float GetExpectedItemHeight(int index)
		{
			int draggedIndex = base.GetDraggedIndex();
			bool flag = draggedIndex >= 0 && index == draggedIndex;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float num;
				result = (this.m_ItemHeightCache.TryGetValue(index, out num) ? num : this.defaultExpectedHeight);
			}
			return result;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private int GetFirstVisibleItem(float offset)
		{
			bool flag = offset <= 0f;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = -1;
				while (offset > 0f)
				{
					num++;
					float expectedItemHeight = this.GetExpectedItemHeight(num);
					offset -= expectedItemHeight;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00021B30 File Offset: 0x0001FD30
		public override float GetExpectedContentHeight()
		{
			return this.m_AccumulatedHeight + (float)(base.itemsCount - this.m_ItemHeightCache.Count) * this.defaultExpectedHeight;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00021B64 File Offset: 0x0001FD64
		private float GetContentHeightForIndex(int lastIndex)
		{
			bool flag = lastIndex < 0;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo;
				bool flag2 = this.m_ContentHeightCache.TryGetValue(lastIndex, out contentHeightCacheInfo);
				if (flag2)
				{
					int draggedIndex = base.GetDraggedIndex();
					bool flag3 = draggedIndex >= 0 && lastIndex >= draggedIndex;
					if (flag3)
					{
						result = contentHeightCacheInfo.sum + (float)(lastIndex - contentHeightCacheInfo.count + 1) * this.defaultExpectedHeight - this.m_DraggedItem.rootElement.layout.height;
					}
					else
					{
						result = contentHeightCacheInfo.sum + (float)(lastIndex - contentHeightCacheInfo.count + 1) * this.defaultExpectedHeight;
					}
				}
				else
				{
					result = this.GetContentHeightForIndex(lastIndex - 1) + this.GetExpectedItemHeight(lastIndex);
				}
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00021C24 File Offset: 0x0001FE24
		private DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo GetCachedContentHeight(int index)
		{
			while (index >= 0)
			{
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo result;
				bool flag = this.m_ContentHeightCache.TryGetValue(index, out result);
				if (flag)
				{
					return result;
				}
				index--;
			}
			return default(DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00021C6C File Offset: 0x0001FE6C
		private void RegisterItemHeight(int index, float height)
		{
			bool flag = height <= 0f;
			if (!flag)
			{
				float num = this.m_CollectionView.ResolveItemHeight(height);
				float num2;
				bool flag2 = this.m_ItemHeightCache.TryGetValue(index, out num2);
				if (flag2)
				{
					this.m_AccumulatedHeight -= num2;
				}
				this.m_AccumulatedHeight += num;
				this.m_ItemHeightCache[index] = num;
				bool flag3 = index > this.m_HighestCachedIndex;
				if (flag3)
				{
					this.m_HighestCachedIndex = index;
				}
				bool flag4 = num2 == 0f;
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo cachedContentHeight = this.GetCachedContentHeight(index - 1);
				this.m_ContentHeightCache[index] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(cachedContentHeight.sum + num, cachedContentHeight.count + 1);
				foreach (KeyValuePair<int, float> keyValuePair in this.m_ItemHeightCache)
				{
					bool flag5 = keyValuePair.Key > index;
					if (flag5)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo = this.m_ContentHeightCache[keyValuePair.Key];
						this.m_ContentHeightCache[keyValuePair.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(contentHeightCacheInfo.sum - num2 + num, flag4 ? (contentHeightCacheInfo.count + 1) : contentHeightCacheInfo.count);
					}
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00021DCC File Offset: 0x0001FFCC
		private void UnregisterItemHeight(int index)
		{
			float num;
			bool flag = !this.m_ItemHeightCache.TryGetValue(index, out num);
			if (!flag)
			{
				this.m_AccumulatedHeight -= num;
				this.m_ItemHeightCache.Remove(index);
				this.m_ContentHeightCache.Remove(index);
				int num2 = -1;
				foreach (KeyValuePair<int, float> keyValuePair in this.m_ItemHeightCache)
				{
					bool flag2 = keyValuePair.Key > index;
					if (flag2)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo contentHeightCacheInfo = this.m_ContentHeightCache[keyValuePair.Key];
						this.m_ContentHeightCache[keyValuePair.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(contentHeightCacheInfo.sum - num, contentHeightCacheInfo.count - 1);
					}
					bool flag3 = keyValuePair.Key > num2;
					if (flag3)
					{
						num2 = keyValuePair.Key;
					}
				}
				this.m_HighestCachedIndex = num2;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00021ED4 File Offset: 0x000200D4
		private void CleanItemHeightCache()
		{
			bool flag = !this.IsIndexOutOfBounds(this.m_HighestCachedIndex);
			if (!flag)
			{
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					foreach (int num in this.m_ItemHeightCache.Keys)
					{
						bool flag2 = this.IsIndexOutOfBounds(num);
						if (flag2)
						{
							list.Add(num);
						}
					}
					foreach (int index in list)
					{
						this.UnregisterItemHeight(index);
					}
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
				this.m_MinimumItemHeight = -1f;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00021FCC File Offset: 0x000201CC
		private void OnRecycledItemGeometryChanged(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			if (!flag)
			{
				bool flag2 = this.UpdateRegisteredHeight(item);
				if (flag2)
				{
					this.ApplyScrollViewUpdate(false);
				}
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00022040 File Offset: 0x00020240
		private bool UpdateRegisteredHeight(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = item.rootElement.layout.height < this.defaultExpectedHeight;
				if (flag2)
				{
					this.m_MinimumItemHeight = item.rootElement.layout.height;
					this.Resize(this.m_ScrollView.layout.size);
				}
				float num = item.rootElement.layout.height - item.rootElement.resolvedStyle.paddingTop;
				float b;
				bool flag3 = this.m_ItemHeightCache.TryGetValue(item.index, out b);
				float num2 = flag3 ? this.GetExpectedItemHeight(item.index) : this.defaultExpectedHeight;
				bool flag4 = this.m_WaitingCache.Count == 0;
				if (flag4)
				{
					bool flag5 = num > num2;
					if (flag5)
					{
						this.m_StickToBottom = false;
					}
					else
					{
						float num3 = num - num2;
						float num4 = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
						this.m_StickToBottom = (num4 > 0f && base.serializedData.scrollOffset.y >= this.m_ScrollView.verticalScroller.highValue + num3);
					}
				}
				bool flag6 = !flag3 || !Mathf.Approximately(num, b);
				if (flag6)
				{
					this.RegisterItemHeight(item.index, num);
					this.UpdateScrollViewContainer(num2, num);
					bool flag7 = this.m_WaitingCache.Count == 0;
					if (flag7)
					{
						return true;
					}
				}
				result = (this.m_WaitingCache.Remove(item.index) && this.m_WaitingCache.Count == 0);
			}
			return result;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00022260 File Offset: 0x00020460
		internal override T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T orMakeItemAtIndex = base.GetOrMakeItemAtIndex(activeItemIndex, scrollViewIndex);
			orMakeItemAtIndex.onGeometryChanged += this.m_GeometryChangedCallback;
			return orMakeItemAtIndex;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00022290 File Offset: 0x00020490
		internal override void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			t.onGeometryChanged -= this.m_GeometryChangedCallback;
			int index = t.index;
			this.UnregisterItemHeight(index);
			base.ReleaseItem(activeItemsIndex);
			this.m_WaitingCache.Remove(index);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000222E6 File Offset: 0x000204E6
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_WaitingCache.Remove(item.index);
			base.StartDragItem(item);
			this.m_DraggedItem.onGeometryChanged -= this.m_GeometryChangedCallback;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002231C File Offset: 0x0002051C
		internal override void EndDrag(int dropIndex)
		{
			bool flag = this.m_DraggedItem.index < dropIndex;
			int index = this.m_DraggedItem.index;
			int num = flag ? 1 : -1;
			float expectedItemHeight = this.GetExpectedItemHeight(index);
			for (int num2 = index; num2 != dropIndex; num2 += num)
			{
				float expectedItemHeight2 = this.GetExpectedItemHeight(num2);
				float expectedItemHeight3 = this.GetExpectedItemHeight(num2 + num);
				bool flag2 = Mathf.Approximately(expectedItemHeight2, expectedItemHeight3);
				if (!flag2)
				{
					this.RegisterItemHeight(num2, expectedItemHeight3);
				}
			}
			this.RegisterItemHeight(flag ? (dropIndex - 1) : dropIndex, expectedItemHeight);
			bool flag3 = this.firstVisibleIndex > this.m_DraggedItem.index;
			if (flag3)
			{
				this.firstVisibleIndex = this.GetFirstVisibleItem(base.serializedData.scrollOffset.y);
				this.UpdateAnchor();
			}
			this.m_DraggedItem.onGeometryChanged += this.m_GeometryChangedCallback;
			base.EndDrag(dropIndex);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00022420 File Offset: 0x00020620
		private void HideItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			t.rootElement.style.display = DisplayStyle.None;
			this.m_WaitingCache.Remove(t.index);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00022470 File Offset: 0x00020670
		private void MarkWaitingForLayout(T item)
		{
			bool isDragGhost = item.isDragGhost;
			if (!isDragGhost)
			{
				this.m_WaitingCache.Add(item.index);
				item.rootElement.lastLayout = Rect.zero;
				item.rootElement.MarkDirtyRepaint();
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000224CC File Offset: 0x000206CC
		private bool IsIndexOutOfBounds(int i)
		{
			return this.m_CollectionView.itemsSource == null || i >= base.itemsCount;
		}

		// Token: 0x04000387 RID: 903
		private int m_HighestCachedIndex = -1;

		// Token: 0x04000388 RID: 904
		private readonly Dictionary<int, float> m_ItemHeightCache = new Dictionary<int, float>(32);

		// Token: 0x04000389 RID: 905
		private readonly Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo> m_ContentHeightCache = new Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo>(32);

		// Token: 0x0400038A RID: 906
		private readonly HashSet<int> m_WaitingCache = new HashSet<int>();

		// Token: 0x0400038B RID: 907
		private int m_ForcedFirstVisibleItem = -1;

		// Token: 0x0400038C RID: 908
		private int m_ForcedLastVisibleItem = -1;

		// Token: 0x0400038D RID: 909
		private bool m_StickToBottom;

		// Token: 0x0400038E RID: 910
		private DynamicHeightVirtualizationController<T>.VirtualizationChange m_LastChange;

		// Token: 0x0400038F RID: 911
		private DynamicHeightVirtualizationController<T>.ScrollDirection m_ScrollDirection;

		// Token: 0x04000390 RID: 912
		private Vector2 m_DelayedScrollOffset = Vector2.negativeInfinity;

		// Token: 0x04000391 RID: 913
		private float m_AccumulatedHeight;

		// Token: 0x04000392 RID: 914
		private float m_MinimumItemHeight = -1f;

		// Token: 0x04000393 RID: 915
		private Action m_FillCallback;

		// Token: 0x04000394 RID: 916
		private Action m_ScrollCallback;

		// Token: 0x04000395 RID: 917
		private Action m_ScrollResetCallback;

		// Token: 0x04000396 RID: 918
		private Action<ReusableCollectionItem> m_GeometryChangedCallback;

		// Token: 0x04000397 RID: 919
		private IVisualElementScheduledItem m_ScheduledItem;

		// Token: 0x04000398 RID: 920
		private IVisualElementScheduledItem m_ScrollScheduledItem;

		// Token: 0x04000399 RID: 921
		private IVisualElementScheduledItem m_ScrollResetScheduledItem;

		// Token: 0x0400039A RID: 922
		private Predicate<int> m_IndexOutOfBoundsPredicate;

		// Token: 0x02000110 RID: 272
		private readonly struct ContentHeightCacheInfo
		{
			// Token: 0x060008D7 RID: 2263 RVA: 0x000224FA File Offset: 0x000206FA
			public ContentHeightCacheInfo(float sum, int count)
			{
				this.sum = sum;
				this.count = count;
			}

			// Token: 0x0400039B RID: 923
			public readonly float sum;

			// Token: 0x0400039C RID: 924
			public readonly int count;
		}

		// Token: 0x02000111 RID: 273
		private enum VirtualizationChange
		{
			// Token: 0x0400039E RID: 926
			None,
			// Token: 0x0400039F RID: 927
			Resize,
			// Token: 0x040003A0 RID: 928
			Scroll,
			// Token: 0x040003A1 RID: 929
			ForcedScroll
		}

		// Token: 0x02000112 RID: 274
		private enum ScrollDirection
		{
			// Token: 0x040003A3 RID: 931
			Idle,
			// Token: 0x040003A4 RID: 932
			Up,
			// Token: 0x040003A5 RID: 933
			Down
		}
	}
}
