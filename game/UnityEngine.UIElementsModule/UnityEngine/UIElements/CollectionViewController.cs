using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000107 RID: 263
	internal class CollectionViewController
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000820 RID: 2080 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		// (remove) Token: 0x06000821 RID: 2081 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		public event Action itemsSourceChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.itemsSourceChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.itemsSourceChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000822 RID: 2082 RVA: 0x0001E418 File Offset: 0x0001C618
		// (remove) Token: 0x06000823 RID: 2083 RVA: 0x0001E450 File Offset: 0x0001C650
		public event Action<int, int> itemIndexChanged
		{
			[CompilerGenerated]
			add
			{
				Action<int, int> action = this.itemIndexChanged;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> value2 = (Action<int, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.itemIndexChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<int, int> action = this.itemIndexChanged;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> value2 = (Action<int, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.itemIndexChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001E485 File Offset: 0x0001C685
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001E490 File Offset: 0x0001C690
		public IList itemsSource
		{
			get
			{
				return this.m_ItemsSource;
			}
			set
			{
				bool flag = this.m_ItemsSource == value;
				if (!flag)
				{
					this.m_ItemsSource = value;
					this.RaiseItemsSourceChanged();
				}
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		protected void SetItemsSourceWithoutNotify(IList source)
		{
			this.m_ItemsSource = source;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001E4C5 File Offset: 0x0001C6C5
		protected BaseVerticalCollectionView view
		{
			get
			{
				return this.m_View;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001E4CD File Offset: 0x0001C6CD
		public void SetView(BaseVerticalCollectionView view)
		{
			this.m_View = view;
			Assert.IsNotNull<BaseVerticalCollectionView>(this.m_View, "View must not be null.");
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public virtual int GetItemsCount()
		{
			IList itemsSource = this.m_ItemsSource;
			return (itemsSource != null) ? itemsSource.Count : 0;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001E50C File Offset: 0x0001C70C
		public virtual int GetIndexForId(int id)
		{
			return id;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001E520 File Offset: 0x0001C720
		public virtual int GetIdForIndex(int index)
		{
			Func<int, int> getItemId = this.m_View.getItemId;
			return (getItemId != null) ? getItemId(index) : index;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001E54C File Offset: 0x0001C74C
		public virtual object GetItemForIndex(int index)
		{
			bool flag = index < 0 || index >= this.m_ItemsSource.Count;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_ItemsSource[index];
			}
			return result;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001E58A File Offset: 0x0001C78A
		internal virtual void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			reusableItem.Init(this.MakeItem());
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001E59A File Offset: 0x0001C79A
		internal virtual void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.BindItem(reusableItem.bindableElement, index);
			reusableItem.SetSelected(this.m_View.selectedIndices.Contains(index));
			reusableItem.rootElement.pseudoStates &= ~PseudoStates.Hover;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		internal virtual void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.UnbindItem(reusableItem.bindableElement, index);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001E5E9 File Offset: 0x0001C7E9
		internal virtual void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			this.DestroyItem(reusableItem.bindableElement);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		public virtual VisualElement MakeItem()
		{
			bool flag = this.m_View.makeItem == null;
			VisualElement result;
			if (flag)
			{
				bool flag2 = this.m_View.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				result = new Label();
			}
			else
			{
				result = this.m_View.makeItem();
			}
			return result;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001E658 File Offset: 0x0001C858
		protected virtual void BindItem(VisualElement element, int index)
		{
			bool flag = this.m_View.bindItem == null;
			if (flag)
			{
				bool flag2 = this.m_View.makeItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
				}
				Label label = (Label)element;
				object obj = this.m_ItemsSource[index];
				label.text = (((obj != null) ? obj.ToString() : null) ?? "null");
			}
			else
			{
				this.m_View.bindItem(element, index);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001E6DB File Offset: 0x0001C8DB
		public virtual void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.m_View.unbindItem;
			if (unbindItem != null)
			{
				unbindItem(element, index);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001E6F7 File Offset: 0x0001C8F7
		public virtual void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.m_View.destroyItem;
			if (destroyItem != null)
			{
				destroyItem(element);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001E712 File Offset: 0x0001C912
		protected void RaiseItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001E727 File Offset: 0x0001C927
		protected void RaiseItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000020C2 File Offset: 0x000002C2
		public CollectionViewController()
		{
		}

		// Token: 0x04000364 RID: 868
		private BaseVerticalCollectionView m_View;

		// Token: 0x04000365 RID: 869
		private IList m_ItemsSource;

		// Token: 0x04000366 RID: 870
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action itemsSourceChanged;

		// Token: 0x04000367 RID: 871
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<int, int> itemIndexChanged;
	}
}
