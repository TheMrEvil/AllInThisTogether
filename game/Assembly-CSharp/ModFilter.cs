using System;

// Token: 0x02000345 RID: 837
public enum ModFilter
{
	// Token: 0x04001CEF RID: 7407
	General_Offense,
	// Token: 0x04001CF0 RID: 7408
	General_Defense,
	// Token: 0x04001CF1 RID: 7409
	General_Movement,
	// Token: 0x04001CF2 RID: 7410
	General_Ranged,
	// Token: 0x04001CF3 RID: 7411
	General_Melee,
	// Token: 0x04001CF4 RID: 7412
	General_Support,
	// Token: 0x04001CF5 RID: 7413
	General_Simple,
	// Token: 0x04001CF6 RID: 7414
	General_Conditional,
	// Token: 0x04001CF7 RID: 7415
	Anti_Movement = 100,
	// Token: 0x04001CF8 RID: 7416
	Anti_Ranged,
	// Token: 0x04001CF9 RID: 7417
	Anti_Melee,
	// Token: 0x04001CFA RID: 7418
	Anti_Target,
	// Token: 0x04001CFB RID: 7419
	Anti_AoE,
	// Token: 0x04001CFC RID: 7420
	Anti_HeavyDmg,
	// Token: 0x04001CFD RID: 7421
	Anti_LightDmg,
	// Token: 0x04001CFE RID: 7422
	Mech_Projectile = 200,
	// Token: 0x04001CFF RID: 7423
	Mech_AoE,
	// Token: 0x04001D00 RID: 7424
	Mech_Beam,
	// Token: 0x04001D01 RID: 7425
	Mech_Buff,
	// Token: 0x04001D02 RID: 7426
	Mech_Keyword,
	// Token: 0x04001D03 RID: 7427
	Mech_Health,
	// Token: 0x04001D04 RID: 7428
	Mech_Barrier,
	// Token: 0x04001D05 RID: 7429
	Mech_Summon,
	// Token: 0x04001D06 RID: 7430
	Mech_Mana,
	// Token: 0x04001D07 RID: 7431
	Mech_Drop,
	// Token: 0x04001D08 RID: 7432
	Mech_Cooldown,
	// Token: 0x04001D09 RID: 7433
	Mech_Teleport,
	// Token: 0x04001D0A RID: 7434
	Mech_Debuff,
	// Token: 0x04001D0B RID: 7435
	Mech_AddKeyword,
	// Token: 0x04001D0C RID: 7436
	Mech_Statmod,
	// Token: 0x04001D0D RID: 7437
	Mech_Tradeoff,
	// Token: 0x04001D0E RID: 7438
	Mech_OnDeath,
	// Token: 0x04001D0F RID: 7439
	Mech_OnSpawn,
	// Token: 0x04001D10 RID: 7440
	Mech_PhysForce,
	// Token: 0x04001D11 RID: 7441
	Mech_ForceMove,
	// Token: 0x04001D12 RID: 7442
	GameChanger = 2000,
	// Token: 0x04001D13 RID: 7443
	AugmentChoice,
	// Token: 0x04001D14 RID: 7444
	Fountain,
	// Token: 0x04001D15 RID: 7445
	AbilityChanger,
	// Token: 0x04001D16 RID: 7446
	Fountain_GameLoop,
	// Token: 0x04001D17 RID: 7447
	Fountain_MapInteract,
	// Token: 0x04001D18 RID: 7448
	Fountain_AntiEnemy,
	// Token: 0x04001D19 RID: 7449
	Fountain_Tome,
	// Token: 0x04001D1A RID: 7450
	Fountain_Bonus,
	// Token: 0x04001D1B RID: 7451
	Fountain_Longterm,
	// Token: 0x04001D1C RID: 7452
	Genre_Breadth = 2150,
	// Token: 0x04001D1D RID: 7453
	Genre_Depth,
	// Token: 0x04001D1E RID: 7454
	Genre_Special,
	// Token: 0x04001D1F RID: 7455
	Enemy_Boss = 2250,
	// Token: 0x04001D20 RID: 7456
	Player_Core = 2500,
	// Token: 0x04001D21 RID: 7457
	Player_Primary,
	// Token: 0x04001D22 RID: 7458
	Player_Secondary,
	// Token: 0x04001D23 RID: 7459
	Player_Movement,
	// Token: 0x04001D24 RID: 7460
	Player_CoreAbility,
	// Token: 0x04001D25 RID: 7461
	Player_CoreKeyword
}
