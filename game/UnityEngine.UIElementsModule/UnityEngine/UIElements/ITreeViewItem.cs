using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A3 RID: 419
	internal interface ITreeViewItem
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DD4 RID: 3540
		int id { get; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DD5 RID: 3541
		ITreeViewItem parent { get; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DD6 RID: 3542
		IEnumerable<ITreeViewItem> children { get; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000DD7 RID: 3543
		bool hasChildren { get; }

		// Token: 0x06000DD8 RID: 3544
		void AddChild(ITreeViewItem child);

		// Token: 0x06000DD9 RID: 3545
		void AddChildren(IList<ITreeViewItem> children);

		// Token: 0x06000DDA RID: 3546
		void RemoveChild(ITreeViewItem child);
	}
}
