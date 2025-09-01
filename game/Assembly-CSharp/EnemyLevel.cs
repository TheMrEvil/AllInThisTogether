using System;

// Token: 0x0200003F RID: 63
[Flags]
public enum EnemyLevel
{
	// Token: 0x040001F5 RID: 501
	None = 0,
	// Token: 0x040001F6 RID: 502
	Fodder = 1,
	// Token: 0x040001F7 RID: 503
	Default = 2,
	// Token: 0x040001F8 RID: 504
	Minion = 4,
	// Token: 0x040001F9 RID: 505
	Elite = 8,
	// Token: 0x040001FA RID: 506
	Boss = 16,
	// Token: 0x040001FB RID: 507
	All = 31
}
