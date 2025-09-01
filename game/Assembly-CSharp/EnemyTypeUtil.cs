using System;

// Token: 0x02000041 RID: 65
public static class EnemyTypeUtil
{
	// Token: 0x0600020C RID: 524 RVA: 0x000124D8 File Offset: 0x000106D8
	public static bool Matches(this EnemyType eType, EnemyType test)
	{
		if (eType == EnemyType.Any || test == EnemyType.Any)
		{
			return true;
		}
		foreach (EnemyType enemyType in (EnemyType[])Enum.GetValues(typeof(EnemyType)))
		{
			if (enemyType != EnemyType.Any && eType.HasFlag(enemyType) && test.HasFlag(enemyType))
			{
				return true;
			}
		}
		return false;
	}
}
