using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000194 RID: 404
	internal readonly struct TreeItem
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000373B8 File Offset: 0x000355B8
		public int id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000373C0 File Offset: 0x000355C0
		public int parentId
		{
			[CompilerGenerated]
			get
			{
				return this.<parentId>k__BackingField;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000373C8 File Offset: 0x000355C8
		public IEnumerable<int> childrenIds
		{
			[CompilerGenerated]
			get
			{
				return this.<childrenIds>k__BackingField;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000373D0 File Offset: 0x000355D0
		public bool hasChildren
		{
			get
			{
				return this.childrenIds != null && this.childrenIds.Any<int>();
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000373E8 File Offset: 0x000355E8
		public TreeItem(int id, int parentId = -1, IEnumerable<int> childrenIds = null)
		{
			this.id = id;
			this.parentId = parentId;
			this.childrenIds = childrenIds;
		}

		// Token: 0x04000624 RID: 1572
		public const int invalidId = -1;

		// Token: 0x04000625 RID: 1573
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <id>k__BackingField;

		// Token: 0x04000626 RID: 1574
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <parentId>k__BackingField;

		// Token: 0x04000627 RID: 1575
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly IEnumerable<int> <childrenIds>k__BackingField;
	}
}
