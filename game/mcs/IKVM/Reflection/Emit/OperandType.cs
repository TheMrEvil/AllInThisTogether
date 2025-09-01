using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000DD RID: 221
	public enum OperandType
	{
		// Token: 0x0400043C RID: 1084
		InlineBrTarget,
		// Token: 0x0400043D RID: 1085
		InlineField,
		// Token: 0x0400043E RID: 1086
		InlineI,
		// Token: 0x0400043F RID: 1087
		InlineI8,
		// Token: 0x04000440 RID: 1088
		InlineMethod,
		// Token: 0x04000441 RID: 1089
		InlineNone,
		// Token: 0x04000442 RID: 1090
		InlinePhi,
		// Token: 0x04000443 RID: 1091
		InlineR,
		// Token: 0x04000444 RID: 1092
		InlineSig = 9,
		// Token: 0x04000445 RID: 1093
		InlineString,
		// Token: 0x04000446 RID: 1094
		InlineSwitch,
		// Token: 0x04000447 RID: 1095
		InlineTok,
		// Token: 0x04000448 RID: 1096
		InlineType,
		// Token: 0x04000449 RID: 1097
		InlineVar,
		// Token: 0x0400044A RID: 1098
		ShortInlineBrTarget,
		// Token: 0x0400044B RID: 1099
		ShortInlineI,
		// Token: 0x0400044C RID: 1100
		ShortInlineR,
		// Token: 0x0400044D RID: 1101
		ShortInlineVar
	}
}
