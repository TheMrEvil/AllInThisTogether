using System;

// Token: 0x0200033F RID: 831
internal static class EventTriggerExtension
{
	// Token: 0x06001C3B RID: 7227 RVA: 0x000ACB19 File Offset: 0x000AAD19
	public static bool IsLocalTrigger(this EventTrigger trigger)
	{
		return trigger == EventTrigger.ProjectileFired;
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x000ACB24 File Offset: 0x000AAD24
	public static bool CanScope(this EventTrigger trigger)
	{
		if (trigger <= EventTrigger.AbilityHit)
		{
			switch (trigger)
			{
			case EventTrigger.ProjectileFired:
				return true;
			case EventTrigger.ProjectileImpact:
				return true;
			case EventTrigger.DamageTaken:
			case EventTrigger.CriticalTaken:
			case EventTrigger.KilledEntity:
			case EventTrigger.ThisChosen:
			case EventTrigger.ModAdded:
				break;
			case EventTrigger.DamageDone:
				return true;
			case EventTrigger.CriticalDone:
				return true;
			case EventTrigger.AoESpawned:
				return true;
			case EventTrigger.AbilityUsed:
				return true;
			default:
				if (trigger == EventTrigger.AbilityHit)
				{
					return true;
				}
				break;
			}
		}
		else
		{
			if (trigger == EventTrigger.PlayerAbilityFirstHit)
			{
				return true;
			}
			if (trigger == EventTrigger.AbilityReleased)
			{
				return true;
			}
			if (trigger == EventTrigger.HealProvided)
			{
				return true;
			}
		}
		return false;
	}
}
