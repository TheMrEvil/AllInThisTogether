using System;

// Token: 0x02000083 RID: 131
internal static class EntityStatExtensions
{
	// Token: 0x060005A2 RID: 1442 RVA: 0x00028C3E File Offset: 0x00026E3E
	public static bool NeedsAbilityInfo(this EStat trigger)
	{
		return trigger == EStat.OnCooldown;
	}
}
