using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000196 RID: 406
	public readonly struct TreeViewItemData<T>
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00037445 File Offset: 0x00035645
		public int id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003744D File Offset: 0x0003564D
		public T data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00037455 File Offset: 0x00035655
		public IEnumerable<TreeViewItemData<T>> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003745D File Offset: 0x0003565D
		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00037478 File Offset: 0x00035678
		public TreeViewItemData(int id, T data, List<TreeViewItemData<T>> children = null)
		{
			this.id = id;
			this.m_Data = data;
			this.m_Children = (children ?? new List<TreeViewItemData<T>>());
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00037499 File Offset: 0x00035699
		internal void AddChild(TreeViewItemData<T> child)
		{
			this.m_Children.Add(child);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000374AC File Offset: 0x000356AC
		internal void AddChildren(IList<TreeViewItemData<T>> children)
		{
			foreach (TreeViewItemData<T> child in children)
			{
				this.AddChild(child);
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000374F8 File Offset: 0x000356F8
		internal void InsertChild(TreeViewItemData<T> child, int index)
		{
			bool flag = index == -1 || index >= this.m_Children.Count;
			if (flag)
			{
				this.m_Children.Add(child);
			}
			else
			{
				this.m_Children.Insert(index, child);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00037540 File Offset: 0x00035740
		internal void RemoveChild(int childId)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					bool flag2 = childId == this.m_Children[i].id;
					if (flag2)
					{
						this.m_Children.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000375A8 File Offset: 0x000357A8
		internal int GetChildIndex(int itemId)
		{
			int num = 0;
			foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
			{
				bool flag = treeViewItemData.id == itemId;
				if (flag)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00037614 File Offset: 0x00035814
		internal bool HasChildRecursive(int childId)
		{
			bool flag = !this.hasChildren;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
				{
					bool flag2 = treeViewItemData.id == childId;
					if (flag2)
					{
						return true;
					}
					bool flag3 = treeViewItemData.HasChildRecursive(childId);
					if (flag3)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003769C File Offset: 0x0003589C
		internal void ReplaceChild(TreeViewItemData<T> newChild)
		{
			bool flag = !this.hasChildren;
			if (!flag)
			{
				int num = 0;
				foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
				{
					bool flag2 = treeViewItemData.id == newChild.id;
					if (flag2)
					{
						this.m_Children.RemoveAt(num);
						this.m_Children.Insert(num, newChild);
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x0400062A RID: 1578
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <id>k__BackingField;

		// Token: 0x0400062B RID: 1579
		private readonly T m_Data;

		// Token: 0x0400062C RID: 1580
		private readonly IList<TreeViewItemData<T>> m_Children;
	}
}
