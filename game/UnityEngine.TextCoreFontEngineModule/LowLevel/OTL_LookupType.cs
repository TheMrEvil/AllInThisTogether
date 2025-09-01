using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000016 RID: 22
	internal enum OTL_LookupType
	{
		// Token: 0x0400009A RID: 154
		Single_Substitution = 32769,
		// Token: 0x0400009B RID: 155
		Multiple_Substitution,
		// Token: 0x0400009C RID: 156
		Alternate_Substitution,
		// Token: 0x0400009D RID: 157
		Ligature_Substitution,
		// Token: 0x0400009E RID: 158
		Contextual_Substitution,
		// Token: 0x0400009F RID: 159
		Chaining_Contextual_Substitution,
		// Token: 0x040000A0 RID: 160
		Extension_Substitution,
		// Token: 0x040000A1 RID: 161
		Reverse_Chaining_Contextual_Single_Substitution,
		// Token: 0x040000A2 RID: 162
		Single_Adjustment = 16385,
		// Token: 0x040000A3 RID: 163
		Pair_Adjustment,
		// Token: 0x040000A4 RID: 164
		Cursive_Attachment,
		// Token: 0x040000A5 RID: 165
		Mark_to_Base_Attachment,
		// Token: 0x040000A6 RID: 166
		Mark_to_Ligature_Attachment,
		// Token: 0x040000A7 RID: 167
		Mark_to_Mark_Attachment,
		// Token: 0x040000A8 RID: 168
		Contextual_Positioning,
		// Token: 0x040000A9 RID: 169
		Chaining_Contextual_Positioning,
		// Token: 0x040000AA RID: 170
		Extension_Positioning
	}
}
