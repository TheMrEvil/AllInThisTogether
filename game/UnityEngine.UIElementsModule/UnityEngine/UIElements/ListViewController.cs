using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000108 RID: 264
	internal class ListViewController : CollectionViewController
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000838 RID: 2104 RVA: 0x0001E740 File Offset: 0x0001C940
		// (remove) Token: 0x06000839 RID: 2105 RVA: 0x0001E778 File Offset: 0x0001C978
		public event Action itemsSourceSizeChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.itemsSourceSizeChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceSizeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.itemsSourceSizeChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceSizeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600083A RID: 2106 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		// (remove) Token: 0x0600083B RID: 2107 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		public event Action<IEnumerable<int>> itemsAdded
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<int>> action = this.itemsAdded;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsAdded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<int>> action = this.itemsAdded;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsAdded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600083C RID: 2108 RVA: 0x0001E820 File Offset: 0x0001CA20
		// (remove) Token: 0x0600083D RID: 2109 RVA: 0x0001E858 File Offset: 0x0001CA58
		public event Action<IEnumerable<int>> itemsRemoved
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<int>> action = this.itemsRemoved;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsRemoved, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<int>> action = this.itemsRemoved;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsRemoved, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001E88D File Offset: 0x0001CA8D
		private ListView listView
		{
			get
			{
				return base.view as ListView;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E89C File Offset: 0x0001CA9C
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableListViewItem reusableListViewItem = reusableItem as ReusableListViewItem;
			bool flag = reusableListViewItem != null;
			if (flag)
			{
				reusableListViewItem.Init(this.MakeItem(), this.listView.reorderable && this.listView.reorderMode == ListViewReorderMode.Animated);
				reusableListViewItem.bindableElement.style.position = Position.Relative;
				reusableListViewItem.bindableElement.style.flexBasis = StyleKeyword.Initial;
				reusableListViewItem.bindableElement.style.marginTop = 0f;
				reusableListViewItem.bindableElement.style.marginBottom = 0f;
				reusableListViewItem.bindableElement.style.paddingTop = 0f;
				reusableListViewItem.bindableElement.style.flexGrow = 0f;
				reusableListViewItem.bindableElement.style.flexShrink = 0f;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableListViewItem reusableListViewItem = reusableItem as ReusableListViewItem;
			bool flag = reusableListViewItem != null;
			if (flag)
			{
				bool flag2 = this.listView.reorderable && this.listView.reorderMode == ListViewReorderMode.Animated;
				reusableListViewItem.UpdateDragHandle(flag2 && this.NeedsDragHandle(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E9FC File Offset: 0x0001CBFC
		public virtual bool NeedsDragHandle(int index)
		{
			return true;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001EA10 File Offset: 0x0001CC10
		public virtual void AddItems(int itemCount)
		{
			this.EnsureItemSourceCanBeResized();
			int count = base.itemsSource.Count;
			List<int> list = CollectionPool<List<int>, int>.Get();
			try
			{
				bool isFixedSize = base.itemsSource.IsFixedSize;
				if (isFixedSize)
				{
					base.itemsSource = ListViewController.AddToArray((Array)base.itemsSource, itemCount);
					for (int i = 0; i < itemCount; i++)
					{
						list.Add(count + i);
					}
				}
				else
				{
					Type type = base.itemsSource.GetType();
					Type type2 = null;
					foreach (Type type3 in type.GetInterfaces())
					{
						bool flag = ListViewController.<AddItems>g__IsGenericList|14_0(type3);
						if (flag)
						{
							type2 = type3;
							break;
						}
					}
					bool flag2 = type2 != null && type2.GetGenericArguments()[0].IsValueType;
					if (flag2)
					{
						Type type4 = type2.GetGenericArguments()[0];
						for (int k = 0; k < itemCount; k++)
						{
							list.Add(count + k);
							base.itemsSource.Add(Activator.CreateInstance(type4));
						}
					}
					else
					{
						for (int l = 0; l < itemCount; l++)
						{
							list.Add(count + l);
							base.itemsSource.Add(null);
						}
					}
				}
				this.RaiseItemsAdded(list);
			}
			finally
			{
				CollectionPool<List<int>, int>.Release(list);
			}
			this.RaiseOnSizeChanged();
			bool isFixedSize2 = base.itemsSource.IsFixedSize;
			if (isFixedSize2)
			{
				this.listView.Rebuild();
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001EBB8 File Offset: 0x0001CDB8
		public virtual void Move(int index, int newIndex)
		{
			bool flag = base.itemsSource == null;
			if (!flag)
			{
				bool flag2 = index == newIndex;
				if (!flag2)
				{
					int num = Mathf.Min(index, newIndex);
					int num2 = Mathf.Max(index, newIndex);
					bool flag3 = num < 0 || num2 >= base.itemsSource.Count;
					if (!flag3)
					{
						int dstIndex = newIndex;
						int num3 = (newIndex < index) ? 1 : -1;
						while (Mathf.Min(index, newIndex) < Mathf.Max(index, newIndex))
						{
							this.Swap(index, newIndex);
							newIndex += num3;
						}
						base.RaiseItemIndexChanged(index, dstIndex);
					}
				}
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001EC50 File Offset: 0x0001CE50
		public virtual void RemoveItem(int index)
		{
			List<int> list = CollectionPool<List<int>, int>.Get();
			try
			{
				list.Add(index);
				this.RemoveItems(list);
			}
			finally
			{
				CollectionPool<List<int>, int>.Release(list);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001EC94 File Offset: 0x0001CE94
		public virtual void RemoveItems(List<int> indices)
		{
			this.EnsureItemSourceCanBeResized();
			indices.Sort();
			bool isFixedSize = base.itemsSource.IsFixedSize;
			if (isFixedSize)
			{
				base.itemsSource = ListViewController.RemoveFromArray((Array)base.itemsSource, indices);
			}
			else
			{
				for (int i = indices.Count - 1; i >= 0; i--)
				{
					base.itemsSource.RemoveAt(indices[i]);
				}
			}
			this.RaiseItemsRemoved(indices);
			this.RaiseOnSizeChanged();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		internal virtual void RemoveItems(int itemCount)
		{
			bool flag = itemCount <= 0;
			if (!flag)
			{
				int itemsCount = this.GetItemsCount();
				List<int> list = CollectionPool<List<int>, int>.Get();
				try
				{
					int num = itemsCount - itemCount;
					for (int i = num; i < itemsCount; i++)
					{
						list.Add(i);
					}
					this.RemoveItems(list);
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(list);
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001ED90 File Offset: 0x0001CF90
		protected void RaiseOnSizeChanged()
		{
			Action action = this.itemsSourceSizeChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001EDA5 File Offset: 0x0001CFA5
		protected void RaiseItemsAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001EDBB File Offset: 0x0001CFBB
		protected void RaiseItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		private static Array AddToArray(Array source, int itemCount)
		{
			Type elementType = source.GetType().GetElementType();
			bool flag = elementType == null;
			if (flag)
			{
				throw new InvalidOperationException("Cannot resize source, because its size is fixed.");
			}
			Array array = Array.CreateInstance(elementType, source.Length + itemCount);
			Array.Copy(source, array, source.Length);
			return array;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001EE24 File Offset: 0x0001D024
		private static Array RemoveFromArray(Array source, List<int> indicesToRemove)
		{
			int length = source.Length;
			int num = length - indicesToRemove.Count;
			bool flag = num < 0;
			if (flag)
			{
				throw new InvalidOperationException("Cannot remove more items than the current count from source.");
			}
			Type elementType = source.GetType().GetElementType();
			bool flag2 = num == 0;
			Array result;
			if (flag2)
			{
				result = Array.CreateInstance(elementType, 0);
			}
			else
			{
				Array array = Array.CreateInstance(elementType, num);
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < source.Length; i++)
				{
					bool flag3 = num3 < indicesToRemove.Count && indicesToRemove[num3] == i;
					if (flag3)
					{
						num3++;
					}
					else
					{
						array.SetValue(source.GetValue(i), num2);
						num2++;
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		private void Swap(int lhs, int rhs)
		{
			object value = base.itemsSource[lhs];
			base.itemsSource[lhs] = base.itemsSource[rhs];
			base.itemsSource[rhs] = value;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001EF30 File Offset: 0x0001D130
		private void EnsureItemSourceCanBeResized()
		{
			bool flag = base.itemsSource.IsFixedSize && !base.itemsSource.GetType().IsArray;
			if (flag)
			{
				throw new InvalidOperationException("Cannot add or remove items from source, because its size is fixed.");
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001EF70 File Offset: 0x0001D170
		public ListViewController()
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001EF79 File Offset: 0x0001D179
		[CompilerGenerated]
		internal static bool <AddItems>g__IsGenericList|14_0(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>);
		}

		// Token: 0x04000368 RID: 872
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action itemsSourceSizeChanged;

		// Token: 0x04000369 RID: 873
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> itemsAdded;

		// Token: 0x0400036A RID: 874
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> itemsRemoved;
	}
}
