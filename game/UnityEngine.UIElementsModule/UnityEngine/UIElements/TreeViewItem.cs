using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A4 RID: 420
	internal class TreeViewItem<T> : ITreeViewItem
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003A1A8 File Offset: 0x000383A8
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0003A1B0 File Offset: 0x000383B0
		public int id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003A1B9 File Offset: 0x000383B9
		public ITreeViewItem parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003A1C4 File Offset: 0x000383C4
		public IEnumerable<ITreeViewItem> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003A1DC File Offset: 0x000383DC
		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0003A207 File Offset: 0x00038407
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0003A20F File Offset: 0x0003840F
		public T data
		{
			[CompilerGenerated]
			get
			{
				return this.<data>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<data>k__BackingField = value;
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003A218 File Offset: 0x00038418
		public TreeViewItem(int id, T data, List<TreeViewItem<T>> children = null)
		{
			this.id = id;
			this.data = data;
			bool flag = children != null;
			if (flag)
			{
				foreach (TreeViewItem<T> child in children)
				{
					this.AddChild(child);
				}
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003A28C File Offset: 0x0003848C
		public void AddChild(ITreeViewItem child)
		{
			TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
			bool flag = treeViewItem == null;
			if (!flag)
			{
				bool flag2 = this.m_Children == null;
				if (flag2)
				{
					this.m_Children = new List<ITreeViewItem>();
				}
				this.m_Children.Add(treeViewItem);
				treeViewItem.m_Parent = this;
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003A2D8 File Offset: 0x000384D8
		public void AddChildren(IList<ITreeViewItem> children)
		{
			foreach (ITreeViewItem child in children)
			{
				this.AddChild(child);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003A324 File Offset: 0x00038524
		public void RemoveChild(ITreeViewItem child)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
				bool flag2 = treeViewItem == null;
				if (!flag2)
				{
					this.m_Children.Remove(treeViewItem);
				}
			}
		}

		// Token: 0x04000677 RID: 1655
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <id>k__BackingField;

		// Token: 0x04000678 RID: 1656
		internal TreeViewItem<T> m_Parent;

		// Token: 0x04000679 RID: 1657
		private List<ITreeViewItem> m_Children;

		// Token: 0x0400067A RID: 1658
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T <data>k__BackingField;
	}
}
