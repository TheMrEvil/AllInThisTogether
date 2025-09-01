using System;

// Token: 0x02000044 RID: 68
public static class ExtensionsAILayout
{
	// Token: 0x06000218 RID: 536 RVA: 0x00013064 File Offset: 0x00011264
	public static EnemyType GetEnemyType(this AILayout.GenreEnemy e)
	{
		if (e <= AILayout.GenreEnemy.Fodder_Ranger_2)
		{
			if (e <= AILayout.GenreEnemy.Fodder_Striker_2)
			{
				if (e == AILayout.GenreEnemy.Fodder_Striker_1)
				{
					return EnemyType.Splice;
				}
				if (e == AILayout.GenreEnemy.Fodder_Striker_2)
				{
					return EnemyType.Splice;
				}
			}
			else
			{
				if (e == AILayout.GenreEnemy.Fodder_Ranger_1)
				{
					return EnemyType.Tangent;
				}
				if (e == AILayout.GenreEnemy.Fodder_Ranger_2)
				{
					return EnemyType.Tangent;
				}
			}
		}
		else if (e <= AILayout.GenreEnemy.Base_Striker_3)
		{
			if (e == AILayout.GenreEnemy.Fodder_Controller)
			{
				return EnemyType.Raving;
			}
			switch (e)
			{
			case AILayout.GenreEnemy.Base_Striker_1:
				return EnemyType.Splice;
			case AILayout.GenreEnemy.Base_Striker_2:
				return EnemyType.Splice;
			case AILayout.GenreEnemy.Base_Striker_3:
				return EnemyType.Splice;
			}
		}
		else
		{
			switch (e)
			{
			case AILayout.GenreEnemy.Base_Ranger_1:
				return EnemyType.Tangent;
			case AILayout.GenreEnemy.Base_Ranger_2:
				return EnemyType.Tangent;
			case AILayout.GenreEnemy.Base_Ranger_3:
				return EnemyType.Tangent;
			default:
				if (e == AILayout.GenreEnemy.Base_Controller_1)
				{
					return EnemyType.Raving;
				}
				if (e == AILayout.GenreEnemy.Base_Controller_2)
				{
					return EnemyType.Raving;
				}
				break;
			}
		}
		return EnemyType.Unique;
	}
}
