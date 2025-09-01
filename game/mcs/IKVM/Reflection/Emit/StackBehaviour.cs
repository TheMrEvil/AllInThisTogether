using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E1 RID: 225
	public enum StackBehaviour
	{
		// Token: 0x04000466 RID: 1126
		Pop0,
		// Token: 0x04000467 RID: 1127
		Pop1,
		// Token: 0x04000468 RID: 1128
		Pop1_pop1,
		// Token: 0x04000469 RID: 1129
		Popi,
		// Token: 0x0400046A RID: 1130
		Popi_pop1,
		// Token: 0x0400046B RID: 1131
		Popi_popi,
		// Token: 0x0400046C RID: 1132
		Popi_popi8,
		// Token: 0x0400046D RID: 1133
		Popi_popi_popi,
		// Token: 0x0400046E RID: 1134
		Popi_popr4,
		// Token: 0x0400046F RID: 1135
		Popi_popr8,
		// Token: 0x04000470 RID: 1136
		Popref,
		// Token: 0x04000471 RID: 1137
		Popref_pop1,
		// Token: 0x04000472 RID: 1138
		Popref_popi,
		// Token: 0x04000473 RID: 1139
		Popref_popi_popi,
		// Token: 0x04000474 RID: 1140
		Popref_popi_popi8,
		// Token: 0x04000475 RID: 1141
		Popref_popi_popr4,
		// Token: 0x04000476 RID: 1142
		Popref_popi_popr8,
		// Token: 0x04000477 RID: 1143
		Popref_popi_popref,
		// Token: 0x04000478 RID: 1144
		Push0,
		// Token: 0x04000479 RID: 1145
		Push1,
		// Token: 0x0400047A RID: 1146
		Push1_push1,
		// Token: 0x0400047B RID: 1147
		Pushi,
		// Token: 0x0400047C RID: 1148
		Pushi8,
		// Token: 0x0400047D RID: 1149
		Pushr4,
		// Token: 0x0400047E RID: 1150
		Pushr8,
		// Token: 0x0400047F RID: 1151
		Pushref,
		// Token: 0x04000480 RID: 1152
		Varpop,
		// Token: 0x04000481 RID: 1153
		Varpush,
		// Token: 0x04000482 RID: 1154
		Popref_popi_pop1
	}
}
