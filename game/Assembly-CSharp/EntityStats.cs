using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
public static class EntityStats
{
	// Token: 0x06000595 RID: 1429 RVA: 0x00028514 File Offset: 0x00026714
	public static float GetStatValue(this EntityControl control, EntityStats.NumberStat stat, object meta = null)
	{
		switch (stat)
		{
		case EntityStats.NumberStat.MinEnemyDist:
			return EntityStats.MinEntityDist(control, false);
		case EntityStats.NumberStat.MinAllyDist:
			return EntityStats.MinEntityDist(control, true);
		case EntityStats.NumberStat.ManaPercent:
		{
			PlayerControl playerControl = control as PlayerControl;
			return (playerControl != null) ? (playerControl.Mana.CurrentManaProportion * 100f) : float.NaN;
		}
		case EntityStats.NumberStat.HealthPercent:
			return control.health.CurrentHealthProportion * 100f;
		case EntityStats.NumberStat.Speed:
			return control.movement.GetVelocity().magnitude;
		case EntityStats.NumberStat.ShieldPercent:
			return control.health.CurrentShieldProportion * 100f;
		case EntityStats.NumberStat.MaxHealth:
			return (float)control.health.MaxHealth;
		case EntityStats.NumberStat.MaxBarrier:
			return (float)control.health.MaxShield;
		case EntityStats.NumberStat.TargetedBy:
			return (float)control.NumTargetedBy();
		case EntityStats.NumberStat.TimeSince:
			return (meta != null) ? control.TimeSinceLast((TimeSince)meta) : -1f;
		case EntityStats.NumberStat.DistanceFrom:
			return (meta != null) ? Vector3.Distance(control.movement.GetPosition(), (meta as EntityControl).movement.GetPosition()) : float.NaN;
		case EntityStats.NumberStat.FOVAngle:
			return (meta != null) ? control.movement.GetAngleFrom((meta as EntityControl).movement.GetPosition()) : float.NaN;
		case EntityStats.NumberStat.AirHeight:
		{
			PlayerControl playerControl2 = control as PlayerControl;
			return (playerControl2 != null) ? playerControl2.Movement.GetAirHeight() : float.NaN;
		}
		case EntityStats.NumberStat.CurrentHealth:
			return (float)control.health.health;
		case EntityStats.NumberStat.CurrentBarrier:
			return control.health.shield;
		case EntityStats.NumberStat.NumPages:
		{
			Augments augment = control.Augment;
			return (float)((augment != null) ? augment.TreeIDs.Count : 0);
		}
		case EntityStats.NumberStat.Mana_Total:
		{
			PlayerControl playerControl3 = control as PlayerControl;
			return (playerControl3 != null) ? ((float)playerControl3.Mana.TotalMana) : float.NaN;
		}
		case EntityStats.NumberStat.Mana_Available:
		{
			PlayerControl playerControl4 = control as PlayerControl;
			return (playerControl4 != null) ? ((float)playerControl4.Mana.CurrentMana) : float.NaN;
		}
		case EntityStats.NumberStat.AIFXScale:
		{
			AIControl aicontrol = control as AIControl;
			return (aicontrol != null) ? aicontrol.Display.VFXScaleFactor : 1f;
		}
		case EntityStats.NumberStat.EntityID:
			return (float)control.ViewID;
		case EntityStats.NumberStat.EntityLifetime:
			return control.TimeSinceLast(TimeSince.Spawned);
		case EntityStats.NumberStat.ColorManaAvailable:
		{
			PlayerControl playerControl5 = control as PlayerControl;
			return (playerControl5 != null) ? ((float)playerControl5.Mana.ColorManaAvailable) : float.NaN;
		}
		case EntityStats.NumberStat.BaseManaCount:
		{
			PlayerControl playerControl6 = control as PlayerControl;
			return (playerControl6 != null) ? ((float)playerControl6.Mana.MaxMana) : float.NaN;
		}
		case EntityStats.NumberStat.BaseColorManaAvailable:
		{
			PlayerControl playerControl7 = control as PlayerControl;
			return (playerControl7 != null) ? ((float)playerControl7.Mana.CoreColorManaAvailable) : float.NaN;
		}
		case EntityStats.NumberStat.AIValue:
		{
			AIControl aicontrol2 = control as AIControl;
			return (aicontrol2 != null) ? aicontrol2.PointValue : 0f;
		}
		}
		return -1f;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0002887C File Offset: 0x00026A7C
	public static float GetStatValue(this EntityControl control, EStat stat)
	{
		if (stat == EStat._)
		{
			return -1f;
		}
		switch (stat)
		{
		case EStat.MinEnemyDist:
			return EntityStats.MinEntityDist(control, false);
		case EStat.MinAllyDist:
			return EntityStats.MinEntityDist(control, true);
		case EStat.ManaPercent:
			if (!(control is PlayerControl))
			{
				return float.NaN;
			}
			return (control as PlayerControl).Mana.CurrentManaProportion * 100f;
		case EStat.HealthPercent:
			return control.health.CurrentHealthProportion * 100f;
		case EStat.Speed:
			return control.movement.GetVelocity().magnitude;
		case EStat.ShieldPercent:
			return control.health.CurrentShieldProportion * 100f;
		case EStat.ShieldOvercharged:
			return Mathf.Max(control.health.shield - (float)control.health.MaxShield, 0f);
		case EStat.MaxHealth:
			return (float)control.health.MaxHealth;
		case EStat.MaxBarrier:
			return (float)control.health.MaxShield;
		}
		return -1f;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0002898A File Offset: 0x00026B8A
	public static float GetStatValue(this EntityControl control, EStat stat, PlayerAbilityType ability)
	{
		if (stat == EStat._)
		{
			return -1f;
		}
		if (stat != EStat.OnCooldown)
		{
			return -1f;
		}
		if (!(control is PlayerControl))
		{
			return -1f;
		}
		return (float)((control as PlayerControl).actions.IsOnCooldown(ability) ? 1 : -1);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000289C5 File Offset: 0x00026BC5
	public static float GetManaStat(this EntityControl control, EStat stat, MagicColor e)
	{
		if (stat == EStat._ || !(control is PlayerControl))
		{
			return -1f;
		}
		if (stat == EStat.OnCooldown)
		{
			return 0f;
		}
		return -1f;
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000289E7 File Offset: 0x00026BE7
	public static bool GetStatTrigger(this EntityControl control, EStat stat)
	{
		if (stat == EStat._)
		{
			return false;
		}
		if (stat != EStat.ShieldCharging)
		{
			return stat == EStat.ShieldOvercharged && control.health.HasOvershield;
		}
		return control.health.ShieldCharging;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00028A13 File Offset: 0x00026C13
	public static bool CheckEntity(this EntityControl control, EStat stat, EntityControl test)
	{
		if (stat == EStat._ || test == null)
		{
			return false;
		}
		if (stat != EStat.IsAlly)
		{
			return stat == EStat.IsEnemy && control.TeamID != test.TeamID;
		}
		return control.TeamID == test.TeamID;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00028A54 File Offset: 0x00026C54
	public static int NumTargetedBy(this EntityControl control)
	{
		int num = 0;
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (entityControl.currentTarget == control && entityControl != control)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00028AC0 File Offset: 0x00026CC0
	public static bool HasStatID(this EntityControl control, EStat stat, string ID)
	{
		if (stat == EStat._)
		{
			return false;
		}
		if (stat != EStat.HasMod)
		{
			return stat == EStat.HasStatus && control.HasStatusEffectGUID(ID);
		}
		return control.HasAugment(ID, true);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00028AE4 File Offset: 0x00026CE4
	public static float GetStatIDValue(this EntityControl control, EStat stat, string ID)
	{
		if (stat == EStat._)
		{
			return -1f;
		}
		if (stat == EStat.HasStatus)
		{
			EntityControl.AppliedStatus statusInfoByID = control.GetStatusInfoByID(ID, -1);
			return (float)((statusInfoByID != null) ? statusInfoByID.Stacks : 0);
		}
		if (stat != EStat.HasAppliedStatus)
		{
			return -1f;
		}
		return (float)((control != null) ? control.NumStatusesApplied(ID) : 0);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00028B30 File Offset: 0x00026D30
	private static float MinEntityDist(EntityControl control, bool allied)
	{
		float num = 5000f;
		Vector3 position = control.movement.GetPosition();
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!(entityControl == control) && !entityControl.IsDead && entityControl.Targetable && (!allied || entityControl.TeamID == control.TeamID) && (allied || entityControl.TeamID != control.TeamID))
			{
				float num2 = Vector3.Distance(entityControl.movement.GetPosition(), position);
				if (num2 < num)
				{
					num = num2;
				}
			}
		}
		return num;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00028BE4 File Offset: 0x00026DE4
	public static bool UsesID(EStat stat)
	{
		return stat == EStat.HasMod || stat == EStat.HasStatus || stat == EStat.HasAppliedStatus;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00028BFD File Offset: 0x00026DFD
	public static bool UsesNumber(EStat stat)
	{
		if (stat <= EStat.ShieldCharging)
		{
			if (stat == EStat.HasMod)
			{
				return false;
			}
			if (stat == EStat.ShieldCharging)
			{
				return false;
			}
		}
		else
		{
			if (stat == EStat.ShieldOvercharged)
			{
				return false;
			}
			if (stat == EStat.IsAlly)
			{
				return false;
			}
			if (stat == EStat.IsEnemy)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00028C2B File Offset: 0x00026E2B
	public static bool UsesEntity(EStat stat)
	{
		return stat == EStat.IsAlly || stat == EStat.IsEnemy;
	}

	// Token: 0x0200049C RID: 1180
	public enum NumberStat
	{
		// Token: 0x0400236C RID: 9068
		MinEnemyDist,
		// Token: 0x0400236D RID: 9069
		MinAllyDist,
		// Token: 0x0400236E RID: 9070
		ManaPercent,
		// Token: 0x0400236F RID: 9071
		HealthPercent,
		// Token: 0x04002370 RID: 9072
		Speed,
		// Token: 0x04002371 RID: 9073
		ShieldPercent,
		// Token: 0x04002372 RID: 9074
		MaxHealth,
		// Token: 0x04002373 RID: 9075
		MaxBarrier,
		// Token: 0x04002374 RID: 9076
		TargetedBy,
		// Token: 0x04002375 RID: 9077
		TimeSince,
		// Token: 0x04002376 RID: 9078
		DistanceFrom,
		// Token: 0x04002377 RID: 9079
		AugmentScalar,
		// Token: 0x04002378 RID: 9080
		StatusStacks,
		// Token: 0x04002379 RID: 9081
		AugmentStacks,
		// Token: 0x0400237A RID: 9082
		FOVAngle,
		// Token: 0x0400237B RID: 9083
		StatusNumApplied,
		// Token: 0x0400237C RID: 9084
		StatusTimeLeft,
		// Token: 0x0400237D RID: 9085
		AirHeight,
		// Token: 0x0400237E RID: 9086
		CurrentHealth,
		// Token: 0x0400237F RID: 9087
		CurrentBarrier,
		// Token: 0x04002380 RID: 9088
		NumPages,
		// Token: 0x04002381 RID: 9089
		Cooldown,
		// Token: 0x04002382 RID: 9090
		Mana_Total,
		// Token: 0x04002383 RID: 9091
		Mana_Available,
		// Token: 0x04002384 RID: 9092
		AIFXScale,
		// Token: 0x04002385 RID: 9093
		StatusLifetime,
		// Token: 0x04002386 RID: 9094
		EntityID,
		// Token: 0x04002387 RID: 9095
		AbilityValue,
		// Token: 0x04002388 RID: 9096
		EntityLifetime,
		// Token: 0x04002389 RID: 9097
		StatusStacksFrom,
		// Token: 0x0400238A RID: 9098
		ColorManaAvailable,
		// Token: 0x0400238B RID: 9099
		ColorManaPercent,
		// Token: 0x0400238C RID: 9100
		BaseManaCount,
		// Token: 0x0400238D RID: 9101
		BaseColorManaAvailable,
		// Token: 0x0400238E RID: 9102
		AIValue,
		// Token: 0x0400238F RID: 9103
		CooldownCurrent
	}
}
