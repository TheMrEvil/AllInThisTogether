using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000007 RID: 7
	[Flags]
	[UsedByNativeCode]
	public enum GlyphLoadFlags
	{
		// Token: 0x0400002D RID: 45
		LOAD_DEFAULT = 0,
		// Token: 0x0400002E RID: 46
		LOAD_NO_SCALE = 1,
		// Token: 0x0400002F RID: 47
		LOAD_NO_HINTING = 2,
		// Token: 0x04000030 RID: 48
		LOAD_RENDER = 4,
		// Token: 0x04000031 RID: 49
		LOAD_NO_BITMAP = 8,
		// Token: 0x04000032 RID: 50
		LOAD_FORCE_AUTOHINT = 32,
		// Token: 0x04000033 RID: 51
		LOAD_MONOCHROME = 4096,
		// Token: 0x04000034 RID: 52
		LOAD_NO_AUTOHINT = 32768,
		// Token: 0x04000035 RID: 53
		LOAD_COLOR = 1048576,
		// Token: 0x04000036 RID: 54
		LOAD_COMPUTE_METRICS = 2097152,
		// Token: 0x04000037 RID: 55
		LOAD_BITMAP_METRICS_ONLY = 4194304
	}
}
