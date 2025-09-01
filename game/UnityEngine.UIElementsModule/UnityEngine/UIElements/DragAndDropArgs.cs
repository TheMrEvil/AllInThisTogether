using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B2 RID: 434
	internal struct DragAndDropArgs : IListDragAndDropArgs
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0003AE61 File Offset: 0x00039061
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0003AE69 File Offset: 0x00039069
		public object target
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<target>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<target>k__BackingField = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0003AE72 File Offset: 0x00039072
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0003AE7A File Offset: 0x0003907A
		public int insertAtIndex
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<insertAtIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<insertAtIndex>k__BackingField = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0003AE83 File Offset: 0x00039083
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0003AE8B File Offset: 0x0003908B
		public int parentId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<parentId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<parentId>k__BackingField = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003AE94 File Offset: 0x00039094
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0003AE9C File Offset: 0x0003909C
		public int childIndex
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<childIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<childIndex>k__BackingField = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003AEA5 File Offset: 0x000390A5
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0003AEAD File Offset: 0x000390AD
		public DragAndDropPosition dragAndDropPosition
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<dragAndDropPosition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dragAndDropPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003AEB6 File Offset: 0x000390B6
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0003AEBE File Offset: 0x000390BE
		public IDragAndDropData dragAndDropData
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<dragAndDropData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dragAndDropData>k__BackingField = value;
			}
		}

		// Token: 0x04000697 RID: 1687
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private object <target>k__BackingField;

		// Token: 0x04000698 RID: 1688
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <insertAtIndex>k__BackingField;

		// Token: 0x04000699 RID: 1689
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <parentId>k__BackingField;

		// Token: 0x0400069A RID: 1690
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <childIndex>k__BackingField;

		// Token: 0x0400069B RID: 1691
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DragAndDropPosition <dragAndDropPosition>k__BackingField;

		// Token: 0x0400069C RID: 1692
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IDragAndDropData <dragAndDropData>k__BackingField;
	}
}
