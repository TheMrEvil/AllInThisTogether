using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AE RID: 942
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendOp
	{
		// Token: 0x04000A97 RID: 2711
		Add,
		// Token: 0x04000A98 RID: 2712
		Subtract,
		// Token: 0x04000A99 RID: 2713
		ReverseSubtract,
		// Token: 0x04000A9A RID: 2714
		Min,
		// Token: 0x04000A9B RID: 2715
		Max,
		// Token: 0x04000A9C RID: 2716
		LogicalClear,
		// Token: 0x04000A9D RID: 2717
		LogicalSet,
		// Token: 0x04000A9E RID: 2718
		LogicalCopy,
		// Token: 0x04000A9F RID: 2719
		LogicalCopyInverted,
		// Token: 0x04000AA0 RID: 2720
		LogicalNoop,
		// Token: 0x04000AA1 RID: 2721
		LogicalInvert,
		// Token: 0x04000AA2 RID: 2722
		LogicalAnd,
		// Token: 0x04000AA3 RID: 2723
		LogicalNand,
		// Token: 0x04000AA4 RID: 2724
		LogicalOr,
		// Token: 0x04000AA5 RID: 2725
		LogicalNor,
		// Token: 0x04000AA6 RID: 2726
		LogicalXor,
		// Token: 0x04000AA7 RID: 2727
		LogicalEquivalence,
		// Token: 0x04000AA8 RID: 2728
		LogicalAndReverse,
		// Token: 0x04000AA9 RID: 2729
		LogicalAndInverted,
		// Token: 0x04000AAA RID: 2730
		LogicalOrReverse,
		// Token: 0x04000AAB RID: 2731
		LogicalOrInverted,
		// Token: 0x04000AAC RID: 2732
		Multiply,
		// Token: 0x04000AAD RID: 2733
		Screen,
		// Token: 0x04000AAE RID: 2734
		Overlay,
		// Token: 0x04000AAF RID: 2735
		Darken,
		// Token: 0x04000AB0 RID: 2736
		Lighten,
		// Token: 0x04000AB1 RID: 2737
		ColorDodge,
		// Token: 0x04000AB2 RID: 2738
		ColorBurn,
		// Token: 0x04000AB3 RID: 2739
		HardLight,
		// Token: 0x04000AB4 RID: 2740
		SoftLight,
		// Token: 0x04000AB5 RID: 2741
		Difference,
		// Token: 0x04000AB6 RID: 2742
		Exclusion,
		// Token: 0x04000AB7 RID: 2743
		HSLHue,
		// Token: 0x04000AB8 RID: 2744
		HSLSaturation,
		// Token: 0x04000AB9 RID: 2745
		HSLColor,
		// Token: 0x04000ABA RID: 2746
		HSLLuminosity
	}
}
