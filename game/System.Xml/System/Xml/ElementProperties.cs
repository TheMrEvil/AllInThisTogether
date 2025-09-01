using System;

namespace System.Xml
{
	// Token: 0x02000056 RID: 86
	internal enum ElementProperties : uint
	{
		// Token: 0x04000681 RID: 1665
		DEFAULT,
		// Token: 0x04000682 RID: 1666
		URI_PARENT,
		// Token: 0x04000683 RID: 1667
		BOOL_PARENT,
		// Token: 0x04000684 RID: 1668
		NAME_PARENT = 4U,
		// Token: 0x04000685 RID: 1669
		EMPTY = 8U,
		// Token: 0x04000686 RID: 1670
		NO_ENTITIES = 16U,
		// Token: 0x04000687 RID: 1671
		HEAD = 32U,
		// Token: 0x04000688 RID: 1672
		BLOCK_WS = 64U,
		// Token: 0x04000689 RID: 1673
		HAS_NS = 128U
	}
}
