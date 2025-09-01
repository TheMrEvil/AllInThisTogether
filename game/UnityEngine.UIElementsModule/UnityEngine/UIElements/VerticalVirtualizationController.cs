using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000117 RID: 279
	internal abstract class VerticalVirtualizationController<T> : CollectionVirtualizationController where T : ReusableCollectionItem, new()
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00023610 File Offset: 0x00021810
		public override IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				return this.m_ActiveItems as IEnumerable<ReusableCollectionItem>;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0002361D File Offset: 0x0002181D
		internal int itemsCount
		{
			get
			{
				return this.m_CollectionView.itemsSource.Count;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002362F File Offset: 0x0002182F
		protected virtual bool VisibleItemPredicate(T i)
		{
			return i.rootElement.style.display == DisplayStyle.Flex;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00023651 File Offset: 0x00021851
		internal T firstVisibleItem
		{
			get
			{
				return this.m_ActiveItems.FirstOrDefault(this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00023664 File Offset: 0x00021864
		internal T lastVisibleItem
		{
			get
			{
				return this.m_ActiveItems.LastOrDefault(this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00023677 File Offset: 0x00021877
		public override int visibleItemCount
		{
			get
			{
				return this.m_ActiveItems.Count(this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002368A File Offset: 0x0002188A
		protected SerializedVirtualizationData serializedData
		{
			get
			{
				return this.m_CollectionView.serializedVirtualizationData;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00023697 File Offset: 0x00021897
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x000236D5 File Offset: 0x000218D5
		public override int firstVisibleIndex
		{
			get
			{
				return Mathf.Min(this.serializedData.firstVisibleIndex, (this.m_CollectionView.viewController != null) ? (this.m_CollectionView.viewController.GetItemsCount() - 1) : this.serializedData.firstVisibleIndex);
			}
			protected set
			{
				this.serializedData.firstVisibleIndex = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000236E3 File Offset: 0x000218E3
		protected float lastHeight
		{
			get
			{
				return this.m_CollectionView.lastHeight;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0000AD4B File Offset: 0x00008F4B
		protected virtual bool alwaysRebindOnRefresh
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000236F0 File Offset: 0x000218F0
		protected VerticalVirtualizationController(BaseVerticalCollectionView collectionView) : base(collectionView.scrollView)
		{
			this.m_CollectionView = collectionView;
			this.m_ActiveItems = new List<T>();
			this.m_VisibleItemPredicateDelegate = new Func<T, bool>(this.VisibleItemPredicate);
			this.m_ScrollView.contentContainer.disableClipping = false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000237B4 File Offset: 0x000219B4
		public override void Refresh(bool rebuild)
		{
			bool flag = this.m_CollectionView.HasValidDataAndBindings();
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				int num = this.firstVisibleIndex + i;
				T t = this.m_ActiveItems[i];
				bool flag2 = t.rootElement.style.display == DisplayStyle.Flex;
				if (rebuild)
				{
					bool flag3 = flag && t.index != -1;
					if (flag3)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
					}
					this.m_CollectionView.viewController.InvokeDestroyItem(t);
					this.m_Pool.Release(t);
				}
				else
				{
					bool flag4 = this.m_CollectionView.itemsSource != null && num >= 0 && num < this.itemsCount;
					if (flag4)
					{
						bool flag5 = !flag;
						if (!flag5)
						{
							bool flag6 = flag2 || this.alwaysRebindOnRefresh;
							if (flag6)
							{
								bool flag7 = t.index != -1;
								if (flag7)
								{
									this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
								}
								t.index = -1;
								this.Setup(t, num);
							}
						}
					}
					else
					{
						bool flag8 = flag2;
						if (flag8)
						{
							this.ReleaseItem(i--);
						}
					}
				}
			}
			if (rebuild)
			{
				this.m_Pool.Clear();
				this.m_ActiveItems.Clear();
				this.m_ScrollView.Clear();
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002397C File Offset: 0x00021B7C
		protected void Setup(T recycledItem, int newIndex)
		{
			bool isDragGhost = recycledItem.isDragGhost;
			bool flag = this.GetDraggedIndex() == newIndex;
			if (flag)
			{
				bool flag2 = recycledItem.index != -1;
				if (flag2)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
				}
				recycledItem.SetDragGhost(true);
				recycledItem.index = this.m_DraggedItem.index;
				recycledItem.rootElement.style.display = DisplayStyle.Flex;
			}
			else
			{
				bool flag3 = isDragGhost;
				if (flag3)
				{
					recycledItem.SetDragGhost(false);
				}
				bool flag4 = newIndex >= this.itemsCount;
				if (flag4)
				{
					recycledItem.rootElement.style.display = DisplayStyle.None;
					bool flag5 = recycledItem.index >= 0 && recycledItem.index < this.itemsCount;
					if (flag5)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						recycledItem.index = -1;
					}
				}
				else
				{
					recycledItem.rootElement.style.display = DisplayStyle.Flex;
					int idForIndex = this.m_CollectionView.viewController.GetIdForIndex(newIndex);
					bool flag6 = recycledItem.index == newIndex && recycledItem.id == idForIndex;
					if (!flag6)
					{
						bool enable = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.None && newIndex % 2 == 1;
						recycledItem.rootElement.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, enable);
						int index = recycledItem.index;
						bool flag7 = recycledItem.index != -1;
						if (flag7)
						{
							this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						}
						recycledItem.index = newIndex;
						recycledItem.id = idForIndex;
						int num = newIndex - this.firstVisibleIndex;
						bool flag8 = num >= this.m_ScrollView.contentContainer.childCount;
						if (flag8)
						{
							recycledItem.rootElement.BringToFront();
						}
						else
						{
							bool flag9 = num >= 0;
							if (flag9)
							{
								recycledItem.rootElement.PlaceBehind(this.m_ScrollView.contentContainer[num]);
							}
							else
							{
								recycledItem.rootElement.SendToBack();
							}
						}
						this.m_CollectionView.viewController.InvokeBindItem(recycledItem, newIndex);
						this.HandleFocus(recycledItem, index);
					}
				}
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00023C60 File Offset: 0x00021E60
		public override void OnFocus(VisualElement leafTarget)
		{
			bool flag = leafTarget == this.m_ScrollView.contentContainer;
			if (!flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				bool flag2 = this.m_ScrollView.contentContainer.FindElementInTree(leafTarget, this.m_LastFocusedElementTreeChildIndexes);
				if (flag2)
				{
					VisualElement visualElement = this.m_ScrollView.contentContainer[this.m_LastFocusedElementTreeChildIndexes[0]];
					foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
					{
						bool flag3 = reusableCollectionItem.rootElement == visualElement;
						if (flag3)
						{
							this.m_LastFocusedElementIndex = reusableCollectionItem.index;
							break;
						}
					}
					this.m_LastFocusedElementTreeChildIndexes.RemoveAt(0);
				}
				else
				{
					this.m_LastFocusedElementIndex = -1;
				}
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00023D44 File Offset: 0x00021F44
		public override void OnBlur(VisualElement willFocus)
		{
			bool flag = willFocus == null || willFocus != this.m_ScrollView.contentContainer;
			if (flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				this.m_LastFocusedElementIndex = -1;
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00023D84 File Offset: 0x00021F84
		private void HandleFocus(ReusableCollectionItem recycledItem, int previousIndex)
		{
			bool flag = this.m_LastFocusedElementIndex == -1;
			if (!flag)
			{
				bool flag2 = this.m_LastFocusedElementIndex == recycledItem.index;
				if (flag2)
				{
					VisualElement visualElement = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
					if (visualElement != null)
					{
						visualElement.Focus();
					}
				}
				else
				{
					bool flag3 = this.m_LastFocusedElementIndex != previousIndex;
					if (flag3)
					{
						VisualElement visualElement2 = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
						if (visualElement2 != null)
						{
							visualElement2.Blur();
						}
					}
					else
					{
						this.m_ScrollView.contentContainer.Focus();
					}
				}
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00023E14 File Offset: 0x00022014
		public override void UpdateBackground()
		{
			float num;
			bool flag = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.All || (num = this.m_ScrollView.contentViewport.resolvedStyle.height - this.GetExpectedContentHeight()) <= 0f;
			if (flag)
			{
				VisualElement emptyRows = this.m_EmptyRows;
				if (emptyRows != null)
				{
					emptyRows.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.lastVisibleItem == null;
				if (!flag2)
				{
					bool flag3 = this.m_EmptyRows == null;
					if (flag3)
					{
						this.m_EmptyRows = new VisualElement
						{
							classList = 
							{
								BaseVerticalCollectionView.backgroundFillUssClassName
							}
						};
					}
					bool flag4 = this.m_EmptyRows.parent == null;
					if (flag4)
					{
						this.m_ScrollView.contentViewport.Add(this.m_EmptyRows);
					}
					float expectedItemHeight = this.GetExpectedItemHeight(-1);
					int num2 = Mathf.FloorToInt(num / expectedItemHeight) + 1;
					bool flag5 = num2 > this.m_EmptyRows.childCount;
					if (flag5)
					{
						int num3 = num2 - this.m_EmptyRows.childCount;
						for (int i = 0; i < num3; i++)
						{
							VisualElement visualElement = new VisualElement();
							visualElement.style.flexShrink = 0f;
							this.m_EmptyRows.Add(visualElement);
						}
					}
					T t = this.lastVisibleItem;
					int num4 = (t != null) ? t.index : -1;
					int childCount = this.m_EmptyRows.hierarchy.childCount;
					for (int j = 0; j < childCount; j++)
					{
						VisualElement visualElement2 = this.m_EmptyRows.hierarchy[j];
						num4++;
						visualElement2.style.height = expectedItemHeight;
						visualElement2.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, num4 % 2 == 1);
					}
				}
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00023FF0 File Offset: 0x000221F0
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_DraggedItem = (item as T);
			int num = this.m_ActiveItems.IndexOf(this.m_DraggedItem);
			this.m_ActiveItems.RemoveAt(num);
			T orMakeItemAtIndex = this.GetOrMakeItemAtIndex(num, num);
			this.Setup(orMakeItemAtIndex, this.m_DraggedItem.index);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00024050 File Offset: 0x00022250
		internal override void EndDrag(int dropIndex)
		{
			ReusableCollectionItem recycledItemFromIndex = this.m_CollectionView.GetRecycledItemFromIndex(dropIndex);
			int index = (recycledItemFromIndex != null) ? this.m_ScrollView.IndexOf(recycledItemFromIndex.rootElement) : this.m_ActiveItems.Count;
			this.m_ScrollView.Insert(index, this.m_DraggedItem.rootElement);
			this.m_ActiveItems.Insert(index, this.m_DraggedItem);
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				T t = this.m_ActiveItems[i];
				bool isDragGhost = t.isDragGhost;
				if (isDragGhost)
				{
					t.index = -1;
					this.ReleaseItem(i);
					i--;
				}
			}
			bool flag = Math.Min(dropIndex, this.itemsCount - 1) != this.m_DraggedItem.index;
			if (flag)
			{
				bool flag2 = this.lastVisibleItem != null;
				if (flag2)
				{
					this.lastVisibleItem.rootElement.style.display = DisplayStyle.None;
				}
				bool flag3 = this.m_DraggedItem.index < dropIndex;
				if (flag3)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(this.m_DraggedItem, this.m_DraggedItem.index);
					this.m_DraggedItem.index = -1;
				}
				else
				{
					bool flag4 = recycledItemFromIndex != null;
					if (flag4)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItemFromIndex, recycledItemFromIndex.index);
						recycledItemFromIndex.index = -1;
					}
				}
			}
			this.m_DraggedItem = default(T);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002420C File Offset: 0x0002240C
		internal virtual T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T t = this.m_Pool.Get();
			bool flag = t.rootElement == null;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeMakeItem(t);
			}
			t.PreAttachElement();
			bool flag2 = activeItemIndex == -1;
			if (flag2)
			{
				this.m_ActiveItems.Add(t);
			}
			else
			{
				this.m_ActiveItems.Insert(activeItemIndex, t);
			}
			bool flag3 = scrollViewIndex == -1;
			if (flag3)
			{
				this.m_ScrollView.Add(t.rootElement);
			}
			else
			{
				this.m_ScrollView.Insert(scrollViewIndex, t.rootElement);
			}
			return t;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000242CC File Offset: 0x000224CC
		internal virtual void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			bool flag = t.index != -1;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeUnbindItem(t, t.index);
			}
			this.m_Pool.Release(t);
			this.m_ActiveItems.Remove(t);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002433C File Offset: 0x0002253C
		protected int GetDraggedIndex()
		{
			ListViewDraggerAnimated listViewDraggerAnimated = this.m_CollectionView.dragger as ListViewDraggerAnimated;
			bool flag = listViewDraggerAnimated != null && listViewDraggerAnimated.isDragging;
			int result;
			if (flag)
			{
				result = listViewDraggerAnimated.draggedItem.index;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x040003BD RID: 957
		private readonly ObjectPool<T> m_Pool = new ObjectPool<T>(() => Activator.CreateInstance<T>(), null, delegate(T i)
		{
			i.DetachElement();
		}, null, true, 10, 10000);

		// Token: 0x040003BE RID: 958
		protected BaseVerticalCollectionView m_CollectionView;

		// Token: 0x040003BF RID: 959
		protected const int k_ExtraVisibleItems = 2;

		// Token: 0x040003C0 RID: 960
		protected List<T> m_ActiveItems;

		// Token: 0x040003C1 RID: 961
		protected T m_DraggedItem;

		// Token: 0x040003C2 RID: 962
		private int m_LastFocusedElementIndex = -1;

		// Token: 0x040003C3 RID: 963
		private List<int> m_LastFocusedElementTreeChildIndexes = new List<int>();

		// Token: 0x040003C4 RID: 964
		protected readonly Func<T, bool> m_VisibleItemPredicateDelegate;

		// Token: 0x040003C5 RID: 965
		protected List<T> m_ScrollInsertionList = new List<T>();

		// Token: 0x040003C6 RID: 966
		private VisualElement m_EmptyRows;

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000928 RID: 2344 RVA: 0x0002437E File Offset: 0x0002257E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x0002438A File Offset: 0x0002258A
			internal T <.ctor>b__30_0()
			{
				return Activator.CreateInstance<T>();
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x00024391 File Offset: 0x00022591
			internal void <.ctor>b__30_1(T i)
			{
				i.DetachElement();
			}

			// Token: 0x040003C7 RID: 967
			public static readonly VerticalVirtualizationController<T>.<>c <>9 = new VerticalVirtualizationController<T>.<>c();

			// Token: 0x040003C8 RID: 968
			public static Func<T> <>9__30_0;

			// Token: 0x040003C9 RID: 969
			public static Action<T> <>9__30_1;
		}
	}
}
