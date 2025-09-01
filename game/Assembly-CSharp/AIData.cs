using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class AIData : ScriptableObject
{
	// Token: 0x060001FA RID: 506 RVA: 0x00011998 File Offset: 0x0000FB98
	private void TestButton()
	{
		string text = "";
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			AIControl component = aidetails.ControlRef.GetComponent<AIControl>();
			if (component.EnemyType == EnemyType.Tangent)
			{
				text = text + component.DisplayName + "\n";
			}
		}
		Debug.Log(text);
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00011A18 File Offset: 0x0000FC18
	public AILayout GetLayout(string jsonData)
	{
		if (jsonData.Contains("LayoutName_"))
		{
			string b = jsonData.Replace("LayoutName_", "");
			foreach (AILayoutRef ailayoutRef in this.Layouts)
			{
				if (ailayoutRef.name == b)
				{
					return ailayoutRef.Layout;
				}
			}
		}
		return new AILayout(jsonData, this);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00011AA4 File Offset: 0x0000FCA4
	public string GetRandomLayout(int bindingLevel, List<AILayout> exclude = null, int seed = -1)
	{
		if (seed == -1)
		{
			seed = UnityEngine.Random.Range(0, int.MaxValue);
		}
		Debug.Log("Getting AI Layout for heat level " + bindingLevel.ToString() + " - Seed: " + seed.ToString());
		System.Random rnd = new System.Random(seed);
		return this.GenerateLayout(bindingLevel, rnd).ToString();
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00011AF8 File Offset: 0x0000FCF8
	public AIData.AIDetails GetEnemy(string aiName)
	{
		if (string.IsNullOrEmpty(aiName))
		{
			return null;
		}
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			GameObject reference = aidetails.Reference;
			if (((reference != null) ? reference.name.ToLower().Replace(" ", "_") : null) == aiName.ToLower().Replace(" ", "_"))
			{
				return aidetails;
			}
		}
		return null;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00011B98 File Offset: 0x0000FD98
	public AIData.AIDetails GetEnemyByID(string StatID)
	{
		if (string.IsNullOrEmpty(StatID))
		{
			return null;
		}
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			if (aidetails.ControlRef.StatID == StatID)
			{
				return aidetails;
			}
		}
		return null;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00011C08 File Offset: 0x0000FE08
	public AIData.AIDetails GetEnemy(GameObject refObj)
	{
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			if (aidetails.Reference == refObj)
			{
				return aidetails;
			}
		}
		return null;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00011C6C File Offset: 0x0000FE6C
	public AIData.AIDetails ChooseEnemy(List<AIData.AIDetails> options, float Budget, bool nudgeBudget = true)
	{
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		if (Budget > 0f && nudgeBudget)
		{
			float num = 500f;
			foreach (AIData.AIDetails aidetails in options)
			{
				if (aidetails.PointValue < num)
				{
					num = aidetails.PointValue;
				}
			}
			Budget = Mathf.Max(Budget, num);
		}
		foreach (AIData.AIDetails aidetails2 in options)
		{
			if (aidetails2.PointValue <= Budget)
			{
				list.Add(aidetails2);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00011D4C File Offset: 0x0000FF4C
	public List<AIData.AIDetails> GetDetails(List<AILayout.GenreEnemy> opts, AILayout layout)
	{
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		foreach (AILayout.GenreEnemy etype in opts)
		{
			AIData.AIDetails details = this.GetDetails(etype, layout);
			if (details != null)
			{
				list.Add(details);
			}
		}
		return list;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00011DB0 File Offset: 0x0000FFB0
	public List<AIData.AIDetails> GetNormalEnemies(AILayout layout)
	{
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		foreach (AILayout.GenreEnemy etype in new List<AILayout.GenreEnemy>
		{
			AILayout.GenreEnemy.Base_Ranger_1,
			AILayout.GenreEnemy.Base_Ranger_2,
			AILayout.GenreEnemy.Base_Ranger_3,
			AILayout.GenreEnemy.Base_Striker_1,
			AILayout.GenreEnemy.Base_Striker_2,
			AILayout.GenreEnemy.Base_Striker_3,
			AILayout.GenreEnemy.Base_Controller_1,
			AILayout.GenreEnemy.Base_Controller_2
		})
		{
			AIData.AIDetails details = this.GetDetails(etype, layout);
			if (details != null)
			{
				list.Add(details);
			}
		}
		return list;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00011E58 File Offset: 0x00010058
	public List<AIData.AIDetails> GetFodderEnemies(AILayout layout)
	{
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		foreach (AILayout.GenreEnemy etype in new List<AILayout.GenreEnemy>
		{
			AILayout.GenreEnemy.Fodder_Striker_1,
			AILayout.GenreEnemy.Fodder_Striker_2,
			AILayout.GenreEnemy.Fodder_Ranger_1,
			AILayout.GenreEnemy.Fodder_Ranger_2,
			AILayout.GenreEnemy.Fodder_Controller
		})
		{
			AIData.AIDetails details = this.GetDetails(etype, layout);
			if (details != null)
			{
				list.Add(details);
			}
		}
		return list;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00011EE4 File Offset: 0x000100E4
	public float GetValue(List<AIData.AIDetails> enemies)
	{
		float num = 0f;
		foreach (AIData.AIDetails aidetails in enemies)
		{
			num += aidetails.PointValue;
		}
		return num;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00011F3C File Offset: 0x0001013C
	public AIData.AIDetails GetDetails(AILayout.GenreEnemy etype, AILayout layout)
	{
		GameObject enemy = layout.GetEnemy(etype);
		return this.GetDetails(enemy);
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00011F58 File Offset: 0x00010158
	public AIData.AIDetails GetDetails(GameObject o)
	{
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			if (aidetails.Reference == o)
			{
				return aidetails;
			}
		}
		return null;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00011FBC File Offset: 0x000101BC
	public List<AIData.EnemyCodexEntry> GetEnemies(EnemyLevel level)
	{
		switch (level)
		{
		case EnemyLevel.Fodder:
			return this.EnemyCodex_Fodder;
		case EnemyLevel.Default:
			return this.EnemyCodex_Normal;
		case EnemyLevel.Fodder | EnemyLevel.Default:
			break;
		case EnemyLevel.Minion:
			return this.EnemyCodex_Raid;
		default:
			if (level == EnemyLevel.Elite)
			{
				return this.EnemyCodex_Elites;
			}
			if (level == EnemyLevel.Boss)
			{
				return this.EnemyCodex_Bosses;
			}
			break;
		}
		return this.EnemyCodex_Normal;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00012024 File Offset: 0x00010224
	public AIData.TornFamilyInfo GetFamilyData(EnemyType eType)
	{
		foreach (AIData.TornFamilyInfo tornFamilyInfo in this.FamilyInfo)
		{
			if (eType.HasFlag(tornFamilyInfo.EType))
			{
				return tornFamilyInfo;
			}
		}
		return this.FamilyInfo[0];
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0001209C File Offset: 0x0001029C
	private AILayout GenerateLayout(int bindingLevel, System.Random rnd)
	{
		AILayout ailayout = new AILayout();
		List<AIData.AIDetails> ignore = new List<AIData.AIDetails>();
		AILayout ailayout2 = ailayout;
		AIData.AIDetails randomValidDetail = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Fodder, bindingLevel, rnd, ignore);
		ailayout2.Fodder_Striker_1 = ((randomValidDetail != null) ? randomValidDetail.Reference : null);
		AILayout ailayout3 = ailayout;
		AIData.AIDetails randomValidDetail2 = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Fodder, bindingLevel, rnd, ignore);
		ailayout3.Fodder_Striker_2 = ((randomValidDetail2 != null) ? randomValidDetail2.Reference : null);
		AILayout ailayout4 = ailayout;
		AIData.AIDetails randomValidDetail3 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Fodder, bindingLevel, rnd, ignore);
		ailayout4.Fodder_Ranger_1 = ((randomValidDetail3 != null) ? randomValidDetail3.Reference : null);
		AILayout ailayout5 = ailayout;
		AIData.AIDetails randomValidDetail4 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Fodder, bindingLevel, rnd, ignore);
		ailayout5.Fodder_Ranger_2 = ((randomValidDetail4 != null) ? randomValidDetail4.Reference : null);
		AILayout ailayout6 = ailayout;
		AIData.AIDetails randomValidDetail5 = this.GetRandomValidDetail(EnemyType.Raving, EnemyLevel.Fodder, bindingLevel, rnd, ignore);
		ailayout6.Fodder_Controller = ((randomValidDetail5 != null) ? randomValidDetail5.Reference : null);
		AILayout ailayout7 = ailayout;
		AIData.AIDetails randomValidDetail6 = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout7.Striker_Base = ((randomValidDetail6 != null) ? randomValidDetail6.Reference : null);
		AILayout ailayout8 = ailayout;
		AIData.AIDetails randomValidDetail7 = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout8.Striker_Support = ((randomValidDetail7 != null) ? randomValidDetail7.Reference : null);
		AILayout ailayout9 = ailayout;
		AIData.AIDetails randomValidDetail8 = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout9.Striker_Backup = ((randomValidDetail8 != null) ? randomValidDetail8.Reference : null);
		AILayout ailayout10 = ailayout;
		AIData.AIDetails randomValidDetail9 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout10.Ranger_Base = ((randomValidDetail9 != null) ? randomValidDetail9.Reference : null);
		AILayout ailayout11 = ailayout;
		AIData.AIDetails randomValidDetail10 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout11.Ranger_Support = ((randomValidDetail10 != null) ? randomValidDetail10.Reference : null);
		AILayout ailayout12 = ailayout;
		AIData.AIDetails randomValidDetail11 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout12.Ranger_Backup = ((randomValidDetail11 != null) ? randomValidDetail11.Reference : null);
		AILayout ailayout13 = ailayout;
		AIData.AIDetails randomValidDetail12 = this.GetRandomValidDetail(EnemyType.Raving, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout13.Controller_Base = ((randomValidDetail12 != null) ? randomValidDetail12.Reference : null);
		AILayout ailayout14 = ailayout;
		AIData.AIDetails randomValidDetail13 = this.GetRandomValidDetail(EnemyType.Raving, EnemyLevel.Default, bindingLevel, rnd, ignore);
		ailayout14.Controller_Support = ((randomValidDetail13 != null) ? randomValidDetail13.Reference : null);
		ailayout.Elites = new List<GameObject>();
		for (int i = 0; i < 10; i++)
		{
			AIData.AIDetails randomValidDetail14 = this.GetRandomValidDetail(EnemyType.Any, EnemyLevel.Elite, bindingLevel, rnd, ignore);
			GameObject gameObject = (randomValidDetail14 != null) ? randomValidDetail14.Reference : null;
			if (gameObject != null)
			{
				ailayout.Elites.Add(gameObject);
			}
		}
		ailayout.Bosses = new List<GameObject>();
		for (int j = 0; j < 8; j++)
		{
			AIData.AIDetails randomValidDetail15 = this.GetRandomValidDetail(EnemyType.Any, EnemyLevel.Boss, bindingLevel, rnd, ignore);
			GameObject gameObject2 = (randomValidDetail15 != null) ? randomValidDetail15.Reference : null;
			if (gameObject2 != null)
			{
				ailayout.Bosses.Add(gameObject2);
			}
		}
		AIData.AIDetails randomValidDetail16 = this.GetRandomValidDetail(EnemyType.Splice, EnemyLevel.Boss, bindingLevel, rnd, ignore);
		GameObject gameObject3 = (randomValidDetail16 != null) ? randomValidDetail16.Reference : null;
		if (gameObject3 != null)
		{
			ailayout.Bosses.Add(gameObject3);
		}
		AIData.AIDetails randomValidDetail17 = this.GetRandomValidDetail(EnemyType.Tangent, EnemyLevel.Boss, bindingLevel, rnd, ignore);
		GameObject gameObject4 = (randomValidDetail17 != null) ? randomValidDetail17.Reference : null;
		if (gameObject4 != null)
		{
			ailayout.Bosses.Add(gameObject4);
		}
		AIData.AIDetails randomValidDetail18 = this.GetRandomValidDetail(EnemyType.Raving, EnemyLevel.Boss, bindingLevel, rnd, ignore);
		GameObject gameObject5 = (randomValidDetail18 != null) ? randomValidDetail18.Reference : null;
		if (gameObject5 != null)
		{
			ailayout.Bosses.Add(gameObject5);
		}
		return ailayout;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00012354 File Offset: 0x00010554
	private AIData.AIDetails GetRandomValidDetail(EnemyType eType, EnemyLevel eLevel, int heatLevel, System.Random rnd, List<AIData.AIDetails> ignore = null)
	{
		if (ignore == null)
		{
			ignore = new List<AIData.AIDetails>();
		}
		List<AIData.AIDetails> list = new List<AIData.AIDetails>();
		foreach (AIData.AIDetails aidetails in this.Enemies)
		{
			if (!ignore.Contains(aidetails) && aidetails.CanSpawnGenre && aidetails.MinHeatLevel <= heatLevel)
			{
				AIControl controlRef = aidetails.ControlRef;
				if (!(controlRef == null) && controlRef.EnemyType.HasFlag(eType) && controlRef.Level.HasFlag(eLevel))
				{
					int num = 0;
					while ((float)num < aidetails.LayoutRarity)
					{
						list.Add(aidetails);
						num++;
					}
				}
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		if (!GameplayManager.IsChallengeActive && eLevel == EnemyLevel.Boss && list.Count > 1 && !string.IsNullOrEmpty(Progression.LastBoss))
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (!(list[i].ControlRef.DisplayName != Progression.LastBoss) && list.Count > 1)
				{
					list.RemoveAt(i);
				}
			}
		}
		AIData.AIDetails aidetails2 = list[rnd.Next(0, list.Count)];
		ignore.Add(aidetails2);
		return aidetails2;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x000124C4 File Offset: 0x000106C4
	public AIData()
	{
	}

	// Token: 0x040001E7 RID: 487
	public bool UpdateRuntimeTrees;

	// Token: 0x040001E8 RID: 488
	public List<AILayoutRef> Layouts;

	// Token: 0x040001E9 RID: 489
	public List<AIData.AIDetails> Enemies;

	// Token: 0x040001EA RID: 490
	[Range(0f, 100f)]
	public float GeneratedChance = 75f;

	// Token: 0x040001EB RID: 491
	public List<AIData.EnemyCodexEntry> EnemyCodex_Fodder;

	// Token: 0x040001EC RID: 492
	public List<AIData.EnemyCodexEntry> EnemyCodex_Normal;

	// Token: 0x040001ED RID: 493
	public List<AIData.EnemyCodexEntry> EnemyCodex_Elites;

	// Token: 0x040001EE RID: 494
	public List<AIData.EnemyCodexEntry> EnemyCodex_Bosses;

	// Token: 0x040001EF RID: 495
	public List<AIData.EnemyCodexEntry> EnemyCodex_Raid;

	// Token: 0x040001F0 RID: 496
	[Header("Visuals")]
	public Color ThematicColor;

	// Token: 0x040001F1 RID: 497
	public Color TraditionalColor;

	// Token: 0x040001F2 RID: 498
	public List<AIData.TornFamilyInfo> FamilyInfo;

	// Token: 0x040001F3 RID: 499
	public GameObject SpawnRingDisplay;

	// Token: 0x0200042B RID: 1067
	[Serializable]
	public class AIDetails
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000C1994 File Offset: 0x000BFB94
		public float PointValue
		{
			get
			{
				return this.ControlRef.PointValue;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x000C19A1 File Offset: 0x000BFBA1
		public string ResourcePath
		{
			get
			{
				return AIData.AIDetails.GetResourcePath(this.Reference);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000C19AE File Offset: 0x000BFBAE
		public AIControl ControlRef
		{
			get
			{
				return this.Reference.GetComponent<AIControl>();
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000C19BB File Offset: 0x000BFBBB
		private string Name
		{
			get
			{
				if (this.Reference == null)
				{
					return "Undefined";
				}
				return this.Reference.name;
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000C19DC File Offset: 0x000BFBDC
		public static string GetResourcePath(GameObject aiPrefab)
		{
			AIControl component = aiPrefab.GetComponent<AIControl>();
			if (component == null)
			{
				return aiPrefab.name;
			}
			string str;
			if (component.Level.HasFlag(EnemyLevel.Boss))
			{
				str = "Boss/";
			}
			else if (component.Level.HasFlag(EnemyLevel.Elite))
			{
				str = "Elite/";
			}
			else
			{
				str = component.EnemyType.ToString() + "/";
			}
			return str + aiPrefab.name;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000C1A6F File Offset: 0x000BFC6F
		public AIDetails()
		{
		}

		// Token: 0x04002142 RID: 8514
		public GameObject Reference;

		// Token: 0x04002143 RID: 8515
		public SpawnType AIType = SpawnType.AI_Ground;

		// Token: 0x04002144 RID: 8516
		public bool CanSpawnGenre = true;

		// Token: 0x04002145 RID: 8517
		public int MinHeatLevel;

		// Token: 0x04002146 RID: 8518
		[Range(0f, 100f)]
		public float LayoutRarity = 50f;
	}

	// Token: 0x0200042C RID: 1068
	[Serializable]
	public class EnemyCodexEntry
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x000C1A90 File Offset: 0x000BFC90
		public bool IDMatches(string id)
		{
			if (!this.ExplicitIDs && this.PrefabRef != null)
			{
				return this.PrefabRef.GetComponent<AIControl>().StatID == id;
			}
			using (List<string>.Enumerator enumerator = this.StatIDs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Equals(id))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000C1B18 File Offset: 0x000BFD18
		[return: TupleElementNames(new string[]
		{
			"kills",
			"killedBy"
		})]
		public ValueTuple<int, int> GetEnemyStat(bool isRaid)
		{
			int num = 0;
			int num2 = 0;
			if (isRaid && this.ExplicitIDs)
			{
				using (List<string>.Enumerator enumerator = this.StatIDs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string encounterID = enumerator.Current;
						int num3 = GameStats.GetRaidStat(encounterID, GameStats.RaidStat.Completed, 0);
						num3 += GameStats.GetRaidStat(encounterID, GameStats.RaidStat.HardMode_Completed, 0);
						int num4 = GameStats.GetRaidStat(encounterID, GameStats.RaidStat.Attempts, 0);
						num4 += GameStats.GetRaidStat(encounterID, GameStats.RaidStat.HardMode_Attempts, 0);
						num4 -= num3;
						num += num3;
						num2 += num4;
					}
					goto IL_F5;
				}
			}
			if (!this.ExplicitIDs && this.PrefabRef != null)
			{
				return GameStats.GetEnemyStat(this.PrefabRef.GetComponent<AIControl>().StatID);
			}
			foreach (string enemyID in this.StatIDs)
			{
				ValueTuple<int, int> enemyStat = GameStats.GetEnemyStat(enemyID);
				num += enemyStat.Item1;
				num2 += enemyStat.Item2;
			}
			IL_F5:
			num = Mathf.Max(0, num);
			num2 = Mathf.Max(0, num2);
			return new ValueTuple<int, int>(num, num2);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000C1C50 File Offset: 0x000BFE50
		public EnemyCodexEntry()
		{
		}

		// Token: 0x04002147 RID: 8519
		public string Name;

		// Token: 0x04002148 RID: 8520
		public EnemyType Type;

		// Token: 0x04002149 RID: 8521
		public bool ExplicitIDs;

		// Token: 0x0400214A RID: 8522
		public GameObject PrefabRef;

		// Token: 0x0400214B RID: 8523
		public List<string> StatIDs;

		// Token: 0x0400214C RID: 8524
		public Sprite Portrait;

		// Token: 0x0400214D RID: 8525
		public bool AlwaysVisible;

		// Token: 0x0400214E RID: 8526
		[TextArea(5, 8)]
		public string TopDetail;

		// Token: 0x0400214F RID: 8527
		[TextArea(4, 6)]
		public string TypeDetail;

		// Token: 0x04002150 RID: 8528
		public List<string> Tips;

		// Token: 0x04002151 RID: 8529
		public List<AIData.EnemyCodexAbility> Abilities;

		// Token: 0x04002152 RID: 8530
		public List<AIData.EnemyCodexPageInfo> Pages = new List<AIData.EnemyCodexPageInfo>();
	}

	// Token: 0x0200042D RID: 1069
	[Serializable]
	public class EnemyCodexAbility
	{
		// Token: 0x060020FC RID: 8444 RVA: 0x000C1C64 File Offset: 0x000BFE64
		public string GetDetailText(bool isRaid)
		{
			string text = TextParser.AugmentDetail(this.Info, null, null, false);
			if (this.Bindings.Count > 0)
			{
				if (!string.IsNullOrEmpty(this.Info))
				{
					text += "<size=14>\n\n</size>";
				}
				for (int i = 0; i < this.Bindings.Count; i++)
				{
					AIData.EnemyCodexAbility.BindingInfo bindingInfo = this.Bindings[i];
					if (isRaid)
					{
						text = text + "<size=32><sprite name=\"raid_hard\"></size><b>Hard Mode:</b> " + TextParser.AugmentDetail(bindingInfo.Text, null, null, false);
						if (i < this.Bindings.Count - 1)
						{
							text += "<size=10>\n\n</size>";
						}
					}
					else
					{
						text = text + string.Format("<sprite name=\"binding\"><b>{0}+:</b> ", bindingInfo.StartBinding) + TextParser.AugmentDetail(bindingInfo.Text, null, null, false);
						if (i < this.Bindings.Count - 1)
						{
							text += "<size=10>\n\n</size>";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000C1D55 File Offset: 0x000BFF55
		public EnemyCodexAbility()
		{
		}

		// Token: 0x04002153 RID: 8531
		public string AbilityName;

		// Token: 0x04002154 RID: 8532
		public AIData.EnemyCodexAbility.Category AbilityType;

		// Token: 0x04002155 RID: 8533
		[TextArea(6, 6)]
		public string Info;

		// Token: 0x04002156 RID: 8534
		public List<AIData.EnemyCodexAbility.BindingInfo> Bindings = new List<AIData.EnemyCodexAbility.BindingInfo>();

		// Token: 0x020006AE RID: 1710
		[Serializable]
		public class BindingInfo
		{
			// Token: 0x06002857 RID: 10327 RVA: 0x000D86FA File Offset: 0x000D68FA
			public BindingInfo()
			{
			}

			// Token: 0x04002C8A RID: 11402
			public int StartBinding;

			// Token: 0x04002C8B RID: 11403
			[TextArea]
			public string Text;
		}

		// Token: 0x020006AF RID: 1711
		public enum Category
		{
			// Token: 0x04002C8D RID: 11405
			Generic,
			// Token: 0x04002C8E RID: 11406
			AoE,
			// Token: 0x04002C8F RID: 11407
			Projectile,
			// Token: 0x04002C90 RID: 11408
			Buff,
			// Token: 0x04002C91 RID: 11409
			Debuff,
			// Token: 0x04002C92 RID: 11410
			Deadly
		}
	}

	// Token: 0x0200042E RID: 1070
	[Serializable]
	public class TornFamilyInfo
	{
		// Token: 0x060020FE RID: 8446 RVA: 0x000C1D68 File Offset: 0x000BFF68
		public TornFamilyInfo()
		{
		}

		// Token: 0x04002157 RID: 8535
		public EnemyType EType;

		// Token: 0x04002158 RID: 8536
		public Sprite FamilySprite;

		// Token: 0x04002159 RID: 8537
		public Sprite BossSprite;
	}

	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class EnemyCodexPageInfo
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x000C1D70 File Offset: 0x000BFF70
		public EnemyCodexPageInfo()
		{
		}

		// Token: 0x0400215A RID: 8538
		public AIData.EnemyCodexPageInfo.PageFeature Start;

		// Token: 0x0400215B RID: 8539
		public int StartAbility;

		// Token: 0x0400215C RID: 8540
		[Space]
		public AIData.EnemyCodexPageInfo.PageFeature End = AIData.EnemyCodexPageInfo.PageFeature.Ability;

		// Token: 0x0400215D RID: 8541
		public int EndAbility = 99;

		// Token: 0x020006B0 RID: 1712
		public enum PageFeature
		{
			// Token: 0x04002C94 RID: 11412
			Description,
			// Token: 0x04002C95 RID: 11413
			Tips,
			// Token: 0x04002C96 RID: 11414
			FamilyInfo,
			// Token: 0x04002C97 RID: 11415
			Ability
		}
	}
}
