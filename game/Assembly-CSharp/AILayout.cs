using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Sirenix.OdinInspector;
using UnityEngine;

// Token: 0x02000043 RID: 67
[Serializable]
public class AILayout
{
	// Token: 0x06000210 RID: 528 RVA: 0x00012570 File Offset: 0x00010770
	public EnemyType GetBossType(int index = 0, EnemyType bossType = EnemyType.Any)
	{
		if (bossType == EnemyType.Unique || this.Bosses.Count == 0)
		{
			return EnemyType.Unique;
		}
		if (bossType == EnemyType.Any)
		{
			return this.Bosses[index % this.Bosses.Count].GetComponent<AIControl>().EnemyType;
		}
		foreach (GameObject gameObject in this.Bosses)
		{
			AIControl aicontrol = gameObject.GetComponent<EntityControl>() as AIControl;
			if (!(aicontrol == null) && bossType.AnyFlagsMatch(aicontrol.EnemyType))
			{
				return bossType;
			}
		}
		return this.Bosses[index % this.Bosses.Count].GetComponent<AIControl>().EnemyType;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00012640 File Offset: 0x00010840
	public AILayout()
	{
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00012660 File Offset: 0x00010860
	public AILayout(string jsonData, AIData data)
	{
		JSONNode jsonnode = JSON.Parse(jsonData);
		AIData.AIDetails enemy = data.GetEnemy(jsonnode.GetValueOrDefault("F_S_1", ""));
		this.Fodder_Striker_1 = ((enemy != null) ? enemy.Reference : null);
		AIData.AIDetails enemy2 = data.GetEnemy(jsonnode.GetValueOrDefault("F_S_2", ""));
		this.Fodder_Striker_2 = ((enemy2 != null) ? enemy2.Reference : null);
		AIData.AIDetails enemy3 = data.GetEnemy(jsonnode.GetValueOrDefault("F_R_1", ""));
		this.Fodder_Ranger_1 = ((enemy3 != null) ? enemy3.Reference : null);
		AIData.AIDetails enemy4 = data.GetEnemy(jsonnode.GetValueOrDefault("F_R_2", ""));
		this.Fodder_Ranger_2 = ((enemy4 != null) ? enemy4.Reference : null);
		AIData.AIDetails enemy5 = data.GetEnemy(jsonnode.GetValueOrDefault("F_C_1", ""));
		this.Fodder_Controller = ((enemy5 != null) ? enemy5.Reference : null);
		AIData.AIDetails enemy6 = data.GetEnemy(jsonnode.GetValueOrDefault("S_1", ""));
		this.Striker_Base = ((enemy6 != null) ? enemy6.Reference : null);
		AIData.AIDetails enemy7 = data.GetEnemy(jsonnode.GetValueOrDefault("S_2", ""));
		this.Striker_Support = ((enemy7 != null) ? enemy7.Reference : null);
		AIData.AIDetails enemy8 = data.GetEnemy(jsonnode.GetValueOrDefault("S_3", ""));
		this.Striker_Backup = ((enemy8 != null) ? enemy8.Reference : null);
		AIData.AIDetails enemy9 = data.GetEnemy(jsonnode.GetValueOrDefault("R_1", ""));
		this.Ranger_Base = ((enemy9 != null) ? enemy9.Reference : null);
		AIData.AIDetails enemy10 = data.GetEnemy(jsonnode.GetValueOrDefault("R_2", ""));
		this.Ranger_Support = ((enemy10 != null) ? enemy10.Reference : null);
		AIData.AIDetails enemy11 = data.GetEnemy(jsonnode.GetValueOrDefault("R_3", ""));
		this.Ranger_Backup = ((enemy11 != null) ? enemy11.Reference : null);
		AIData.AIDetails enemy12 = data.GetEnemy(jsonnode.GetValueOrDefault("C_1", ""));
		this.Controller_Base = ((enemy12 != null) ? enemy12.Reference : null);
		AIData.AIDetails enemy13 = data.GetEnemy(jsonnode.GetValueOrDefault("C_2", ""));
		this.Controller_Support = ((enemy13 != null) ? enemy13.Reference : null);
		JSONNode jsonnode2 = jsonnode["Elite"] as JSONArray;
		this.Elites = new List<GameObject>();
		foreach (KeyValuePair<string, JSONNode> keyValuePair in jsonnode2)
		{
			List<GameObject> elites = this.Elites;
			AIData.AIDetails enemy14 = data.GetEnemy(keyValuePair.Value);
			elites.Add((enemy14 != null) ? enemy14.Reference : null);
		}
		JSONNode jsonnode3 = jsonnode["Boss"] as JSONArray;
		this.Bosses = new List<GameObject>();
		foreach (KeyValuePair<string, JSONNode> keyValuePair2 in jsonnode3)
		{
			List<GameObject> bosses = this.Bosses;
			AIData.AIDetails enemy15 = data.GetEnemy(keyValuePair2.Value);
			bosses.Add((enemy15 != null) ? enemy15.Reference : null);
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x000129D4 File Offset: 0x00010BD4
	public JSONNode GetJSON()
	{
		JSONNode jsonnode = new JSONObject();
		jsonnode.Add("F_S_1", (this.Fodder_Striker_1 == null) ? "" : this.Fodder_Striker_1.name);
		jsonnode.Add("F_S_2", (this.Fodder_Striker_2 == null) ? "" : this.Fodder_Striker_2.name);
		jsonnode.Add("F_R_1", (this.Fodder_Ranger_1 == null) ? "" : this.Fodder_Ranger_1.name);
		jsonnode.Add("F_R_2", (this.Fodder_Ranger_2 == null) ? "" : this.Fodder_Ranger_2.name);
		jsonnode.Add("F_C_1", (this.Fodder_Controller == null) ? "" : this.Fodder_Controller.name);
		jsonnode.Add("S_1", (this.Striker_Base == null) ? "" : this.Striker_Base.name);
		jsonnode.Add("S_2", (this.Striker_Support == null) ? "" : this.Striker_Support.name);
		jsonnode.Add("S_3", (this.Striker_Backup == null) ? "" : this.Striker_Backup.name);
		jsonnode.Add("R_1", (this.Ranger_Base == null) ? "" : this.Ranger_Base.name);
		jsonnode.Add("R_2", (this.Ranger_Support == null) ? "" : this.Ranger_Support.name);
		jsonnode.Add("R_3", (this.Ranger_Backup == null) ? "" : this.Ranger_Backup.name);
		jsonnode.Add("C_1", (this.Controller_Base == null) ? "" : this.Controller_Base.name);
		jsonnode.Add("C_2", (this.Controller_Support == null) ? "" : this.Controller_Support.name);
		JSONArray jsonarray = new JSONArray();
		foreach (GameObject gameObject in this.Elites)
		{
			jsonarray.Add(gameObject.name);
		}
		jsonnode.Add("Elite", jsonarray);
		JSONArray jsonarray2 = new JSONArray();
		foreach (GameObject gameObject2 in this.Bosses)
		{
			if (gameObject2 != null)
			{
				jsonarray2.Add(gameObject2.name);
			}
		}
		jsonnode.Add("Boss", jsonarray2);
		return jsonnode;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00012D28 File Offset: 0x00010F28
	public override string ToString()
	{
		return this.GetJSON().ToString();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00012D38 File Offset: 0x00010F38
	public GameObject GetElite(int Number)
	{
		int num = Number - 1;
		if (this.Elites == null || this.Elites.Count == 0)
		{
			return null;
		}
		return this.Elites[num % this.Elites.Count];
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00012D78 File Offset: 0x00010F78
	public GameObject GetBoss(int Number, EnemyType BossType = EnemyType.Any)
	{
		int num = Number - 1;
		if (this.Bosses == null || this.Bosses.Count == 0)
		{
			return null;
		}
		if (BossType == EnemyType.Any)
		{
			return this.Bosses[num % this.Bosses.Count];
		}
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject gameObject in this.Bosses)
		{
			AIControl aicontrol = gameObject.GetComponent<EntityControl>() as AIControl;
			if (!(aicontrol == null) && BossType.AnyFlagsMatch(aicontrol.EnemyType))
			{
				list.Add(gameObject);
			}
		}
		if (list.Count == 0)
		{
			Debug.LogError("Had no valid bosses of type " + BossType.ToString() + ", falling back to any boss");
			return this.Bosses[num % this.Bosses.Count];
		}
		return list[num % list.Count];
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00012E80 File Offset: 0x00011080
	public GameObject GetEnemy(AILayout.GenreEnemy enemy)
	{
		if (enemy == AILayout.GenreEnemy.Elite_Random && this.Elites.Count > 0)
		{
			return this.Elites[UnityEngine.Random.Range(0, this.Elites.Count)];
		}
		if (enemy <= AILayout.GenreEnemy.Fodder_Ranger_2)
		{
			if (enemy <= AILayout.GenreEnemy.Fodder_Striker_2)
			{
				if (enemy == AILayout.GenreEnemy.Fodder_Striker_1)
				{
					return this.Fodder_Striker_1;
				}
				if (enemy == AILayout.GenreEnemy.Fodder_Striker_2)
				{
					return (this.Fodder_Striker_2 != null) ? this.Fodder_Striker_2 : this.Fodder_Striker_1;
				}
			}
			else
			{
				if (enemy == AILayout.GenreEnemy.Fodder_Ranger_1)
				{
					return this.Fodder_Ranger_1;
				}
				if (enemy == AILayout.GenreEnemy.Fodder_Ranger_2)
				{
					return (this.Fodder_Ranger_2 != null) ? this.Fodder_Ranger_2 : this.Fodder_Ranger_1;
				}
			}
		}
		else if (enemy <= AILayout.GenreEnemy.Base_Striker_3)
		{
			if (enemy == AILayout.GenreEnemy.Fodder_Controller)
			{
				return this.Fodder_Controller;
			}
			switch (enemy)
			{
			case AILayout.GenreEnemy.Base_Striker_1:
				return this.Striker_Base;
			case AILayout.GenreEnemy.Base_Striker_2:
				return (this.Striker_Support != null) ? this.Striker_Support : this.Striker_Base;
			case AILayout.GenreEnemy.Base_Striker_3:
				return (this.Striker_Backup != null) ? this.Striker_Backup : this.Striker_Base;
			}
		}
		else
		{
			switch (enemy)
			{
			case AILayout.GenreEnemy.Base_Ranger_1:
				return this.Ranger_Base;
			case AILayout.GenreEnemy.Base_Ranger_2:
				return (this.Ranger_Support != null) ? this.Ranger_Support : this.Ranger_Base;
			case AILayout.GenreEnemy.Base_Ranger_3:
				return (this.Ranger_Backup != null) ? this.Ranger_Backup : this.Ranger_Base;
			default:
				if (enemy == AILayout.GenreEnemy.Base_Controller_1)
				{
					return this.Controller_Base;
				}
				if (enemy == AILayout.GenreEnemy.Base_Controller_2)
				{
					return (this.Controller_Support != null) ? this.Controller_Support : this.Controller_Base;
				}
				break;
			}
		}
		return null;
	}

	// Token: 0x04000208 RID: 520
	[Header("Striker")]
	public GameObject Striker_Base;

	// Token: 0x04000209 RID: 521
	public GameObject Striker_Support;

	// Token: 0x0400020A RID: 522
	public GameObject Striker_Backup;

	// Token: 0x0400020B RID: 523
	[Header("Ranger")]
	public GameObject Ranger_Base;

	// Token: 0x0400020C RID: 524
	public GameObject Ranger_Support;

	// Token: 0x0400020D RID: 525
	public GameObject Ranger_Backup;

	// Token: 0x0400020E RID: 526
	[Header("Controller")]
	public GameObject Controller_Base;

	// Token: 0x0400020F RID: 527
	public GameObject Controller_Support;

	// Token: 0x04000210 RID: 528
	public GameObject Fodder_Striker_1;

	// Token: 0x04000211 RID: 529
	public GameObject Fodder_Striker_2;

	// Token: 0x04000212 RID: 530
	[Space(5f)]
	public GameObject Fodder_Ranger_1;

	// Token: 0x04000213 RID: 531
	public GameObject Fodder_Ranger_2;

	// Token: 0x04000214 RID: 532
	[Space(5f)]
	public GameObject Fodder_Controller;

	// Token: 0x04000215 RID: 533
	public List<GameObject> Elites = new List<GameObject>();

	// Token: 0x04000216 RID: 534
	public List<GameObject> Bosses = new List<GameObject>();

	// Token: 0x02000430 RID: 1072
	[Serializable]
	public class EnemyTypeList
	{
		// Token: 0x06002100 RID: 8448 RVA: 0x000C1D88 File Offset: 0x000BFF88
		public AILayout.EnemyTypeList Copy()
		{
			AILayout.EnemyTypeList enemyTypeList = base.MemberwiseClone() as AILayout.EnemyTypeList;
			enemyTypeList.Enemies = new List<AILayout.GenreEnemy>();
			foreach (AILayout.GenreEnemy item in this.Enemies)
			{
				enemyTypeList.Enemies.Add(item);
			}
			return enemyTypeList;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000C1DF8 File Offset: 0x000BFFF8
		public bool HasEnemyType(EnemyType eType)
		{
			foreach (AILayout.GenreEnemy e in this.Enemies)
			{
				if (eType.AnyFlagsMatch(e.GetEnemyType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000C1E5C File Offset: 0x000C005C
		public EnemyTypeList()
		{
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000C1E70 File Offset: 0x000C0070
		// Note: this type is marked as 'beforefieldinit'.
		static EnemyTypeList()
		{
		}

		// Token: 0x0400215E RID: 8542
		public List<AILayout.GenreEnemy> Enemies = new List<AILayout.GenreEnemy>();

		// Token: 0x0400215F RID: 8543
		private static IEnumerable EnemyTypeFormat = new ValueDropdownList<AILayout.GenreEnemy>
		{
			{
				"Striker/Fodder",
				AILayout.GenreEnemy.Fodder_Striker_1
			},
			{
				"Striker/Base",
				AILayout.GenreEnemy.Base_Striker_1
			},
			{
				"Striker/Support",
				AILayout.GenreEnemy.Base_Striker_2
			},
			{
				"Ranger/Fodder",
				AILayout.GenreEnemy.Fodder_Ranger_1
			},
			{
				"Ranger/Base",
				AILayout.GenreEnemy.Base_Ranger_1
			},
			{
				"Ranger/Support",
				AILayout.GenreEnemy.Base_Ranger_2
			},
			{
				"Controller/Fodder",
				AILayout.GenreEnemy.Fodder_Controller
			},
			{
				"Controller/Base",
				AILayout.GenreEnemy.Base_Controller_1
			},
			{
				"Controller/Support",
				AILayout.GenreEnemy.Base_Controller_2
			},
			{
				"Extra/Striker/Fodder",
				AILayout.GenreEnemy.Fodder_Striker_2
			},
			{
				"Extra/Striker/Backup",
				AILayout.GenreEnemy.Base_Striker_3
			},
			{
				"Extra/Ranger/Fodder",
				AILayout.GenreEnemy.Fodder_Ranger_2
			},
			{
				"Extra/Ranger/Backup",
				AILayout.GenreEnemy.Base_Ranger_3
			}
		};
	}

	// Token: 0x02000431 RID: 1073
	[Serializable]
	public class EnemyBaseTypeList
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000C1F30 File Offset: 0x000C0130
		public AILayout.EnemyBaseTypeList Copy()
		{
			AILayout.EnemyBaseTypeList enemyBaseTypeList = base.MemberwiseClone() as AILayout.EnemyBaseTypeList;
			enemyBaseTypeList.Enemies = new List<AILayout.GenreEnemy>();
			foreach (AILayout.GenreEnemy item in this.Enemies)
			{
				enemyBaseTypeList.Enemies.Add(item);
			}
			return enemyBaseTypeList;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000C1FA0 File Offset: 0x000C01A0
		public bool HasEnemyType(EnemyType eType)
		{
			foreach (AILayout.GenreEnemy e in this.Enemies)
			{
				if (eType.AnyFlagsMatch(e.GetEnemyType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000C2004 File Offset: 0x000C0204
		public EnemyBaseTypeList()
		{
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000C2018 File Offset: 0x000C0218
		// Note: this type is marked as 'beforefieldinit'.
		static EnemyBaseTypeList()
		{
		}

		// Token: 0x04002160 RID: 8544
		public List<AILayout.GenreEnemy> Enemies = new List<AILayout.GenreEnemy>();

		// Token: 0x04002161 RID: 8545
		private static IEnumerable EnemyTypeFormat = new ValueDropdownList<AILayout.GenreEnemy>
		{
			{
				"Striker/Base",
				AILayout.GenreEnemy.Base_Striker_1
			},
			{
				"Striker/Support",
				AILayout.GenreEnemy.Base_Striker_2
			},
			{
				"Ranger/Base",
				AILayout.GenreEnemy.Base_Ranger_1
			},
			{
				"Ranger/Support",
				AILayout.GenreEnemy.Base_Ranger_2
			},
			{
				"Controller/Base",
				AILayout.GenreEnemy.Base_Controller_1
			},
			{
				"Controller/Support",
				AILayout.GenreEnemy.Base_Controller_2
			},
			{
				"Extra/Striker/Backup",
				AILayout.GenreEnemy.Base_Striker_3
			},
			{
				"Extra/Ranger/Backup",
				AILayout.GenreEnemy.Base_Ranger_3
			}
		};
	}

	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class FodderTypeList
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x000C2098 File Offset: 0x000C0298
		public AILayout.FodderTypeList Copy()
		{
			AILayout.FodderTypeList fodderTypeList = base.MemberwiseClone() as AILayout.FodderTypeList;
			fodderTypeList.Enemies = new List<AILayout.GenreEnemy>();
			foreach (AILayout.GenreEnemy item in this.Enemies)
			{
				fodderTypeList.Enemies.Add(item);
			}
			return fodderTypeList;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000C2108 File Offset: 0x000C0308
		public bool HasEnemyType(EnemyType eType)
		{
			foreach (AILayout.GenreEnemy e in this.Enemies)
			{
				if (eType.AnyFlagsMatch(e.GetEnemyType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000C216C File Offset: 0x000C036C
		public FodderTypeList()
		{
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000C2180 File Offset: 0x000C0380
		// Note: this type is marked as 'beforefieldinit'.
		static FodderTypeList()
		{
		}

		// Token: 0x04002162 RID: 8546
		public List<AILayout.GenreEnemy> Enemies = new List<AILayout.GenreEnemy>();

		// Token: 0x04002163 RID: 8547
		private static IEnumerable EnemyTypeFormat = new ValueDropdownList<AILayout.GenreEnemy>
		{
			{
				"Striker",
				AILayout.GenreEnemy.Fodder_Striker_1
			},
			{
				"Ranger",
				AILayout.GenreEnemy.Fodder_Ranger_1
			},
			{
				"Controller",
				AILayout.GenreEnemy.Fodder_Controller
			},
			{
				"Extra/Striker",
				AILayout.GenreEnemy.Fodder_Striker_2
			},
			{
				"Extra/Ranger",
				AILayout.GenreEnemy.Fodder_Ranger_2
			}
		};
	}

	// Token: 0x02000433 RID: 1075
	public enum GenreEnemy
	{
		// Token: 0x04002165 RID: 8549
		Fodder_Striker_1,
		// Token: 0x04002166 RID: 8550
		Fodder_Striker_2,
		// Token: 0x04002167 RID: 8551
		Fodder_Ranger_1 = 8,
		// Token: 0x04002168 RID: 8552
		Fodder_Ranger_2,
		// Token: 0x04002169 RID: 8553
		Fodder_Controller = 16,
		// Token: 0x0400216A RID: 8554
		Base_Striker_1 = 32,
		// Token: 0x0400216B RID: 8555
		Base_Striker_2,
		// Token: 0x0400216C RID: 8556
		Base_Striker_3,
		// Token: 0x0400216D RID: 8557
		Base_Ranger_1 = 52,
		// Token: 0x0400216E RID: 8558
		Base_Ranger_2,
		// Token: 0x0400216F RID: 8559
		Base_Ranger_3,
		// Token: 0x04002170 RID: 8560
		Base_Controller_1 = 72,
		// Token: 0x04002171 RID: 8561
		Base_Controller_2,
		// Token: 0x04002172 RID: 8562
		Elite_Random = 128
	}
}
