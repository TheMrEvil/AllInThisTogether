using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000195 RID: 405
	internal readonly struct TreeViewItemWrapper
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00037400 File Offset: 0x00035600
		public int id
		{
			get
			{
				return this.item.id;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0003740D File Offset: 0x0003560D
		public int parentId
		{
			get
			{
				return this.item.parentId;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0003741A File Offset: 0x0003561A
		public IEnumerable<int> childrenIds
		{
			get
			{
				return this.item.childrenIds;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00037427 File Offset: 0x00035627
		public bool hasChildren
		{
			get
			{
				return this.item.hasChildren;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00037434 File Offset: 0x00035634
		public TreeViewItemWrapper(TreeItem item, int depth)
		{
			this.item = item;
			this.depth = depth;
		}

		// Token: 0x04000628 RID: 1576
		public readonly TreeItem item;

		// Token: 0x04000629 RID: 1577
		public readonly int depth;
	}
}
