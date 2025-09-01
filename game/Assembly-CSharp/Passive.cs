using System;

// Token: 0x02000338 RID: 824
[Serializable]
public class Passive
{
	// Token: 0x06001C14 RID: 7188 RVA: 0x000ABFCE File Offset: 0x000AA1CE
	public virtual bool Matches(Passive p)
	{
		return this.Equals(p);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x000ABFD7 File Offset: 0x000AA1D7
	public Passive()
	{
	}

	// Token: 0x02000667 RID: 1639
	public enum EntityValue
	{
		// Token: 0x04002B52 RID: 11090
		Size,
		// Token: 0x04002B53 RID: 11091
		Shield,
		// Token: 0x04002B54 RID: 11092
		ShieldOverchargeMax,
		// Token: 0x04002B55 RID: 11093
		ShieldDelay,
		// Token: 0x04002B56 RID: 11094
		HealingReceived,
		// Token: 0x04002B57 RID: 11095
		Health = 128,
		// Token: 0x04002B58 RID: 11096
		Mana,
		// Token: 0x04002B59 RID: 11097
		ManaRecharge,
		// Token: 0x04002B5A RID: 11098
		DamageTaken,
		// Token: 0x04002B5B RID: 11099
		ImpairDuration,
		// Token: 0x04002B5C RID: 11100
		ForceTaken,
		// Token: 0x04002B5D RID: 11101
		Speed = 256,
		// Token: 0x04002B5E RID: 11102
		P_JumpHeight = 384,
		// Token: 0x04002B5F RID: 11103
		P_Gravity,
		// Token: 0x04002B60 RID: 11104
		P_Acceleration,
		// Token: 0x04002B61 RID: 11105
		P_AirControl,
		// Token: 0x04002B62 RID: 11106
		P_GroundFriction,
		// Token: 0x04002B63 RID: 11107
		P_AirFriction,
		// Token: 0x04002B64 RID: 11108
		P_AirJumps,
		// Token: 0x04002B65 RID: 11109
		P_GlideForce,
		// Token: 0x04002B66 RID: 11110
		P_PageRerolls,
		// Token: 0x04002B67 RID: 11111
		P_AutoRevives
	}

	// Token: 0x02000668 RID: 1640
	public enum AbilityValue
	{
		// Token: 0x04002B69 RID: 11113
		AllDamage,
		// Token: 0x04002B6A RID: 11114
		ManaCost,
		// Token: 0x04002B6B RID: 11115
		Cooldown,
		// Token: 0x04002B6C RID: 11116
		ForceApplied,
		// Token: 0x04002B6D RID: 11117
		CritChance,
		// Token: 0x04002B6E RID: 11118
		CritDamageMult,
		// Token: 0x04002B6F RID: 11119
		ShieldGained,
		// Token: 0x04002B70 RID: 11120
		ManaGenerated,
		// Token: 0x04002B71 RID: 11121
		HealingDone,
		// Token: 0x04002B72 RID: 11122
		Projectile_Damage = 128,
		// Token: 0x04002B73 RID: 11123
		Projectile_Speed,
		// Token: 0x04002B74 RID: 11124
		Projectile_Range,
		// Token: 0x04002B75 RID: 11125
		Projectile_Size,
		// Token: 0x04002B76 RID: 11126
		Area_Damage = 256,
		// Token: 0x04002B77 RID: 11127
		Area_Lifetime,
		// Token: 0x04002B78 RID: 11128
		Area_Size,
		// Token: 0x04002B79 RID: 11129
		Area_TickRate,
		// Token: 0x04002B7A RID: 11130
		Beam_Damage = 384,
		// Token: 0x04002B7B RID: 11131
		Beam_Range,
		// Token: 0x04002B7C RID: 11132
		Status_Damage = 512,
		// Token: 0x04002B7D RID: 11133
		Status_Duration
	}
}
