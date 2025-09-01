using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class GenreWaveNode : Node
{
	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x000B0219 File Offset: 0x000AE419
	public GenreSpawnNode Spawn
	{
		get
		{
			if (this.Spawns == null)
			{
				return null;
			}
			return this.Spawns as GenreSpawnNode;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x000B0238 File Offset: 0x000AE438
	public int CalculatedBossIndex
	{
		get
		{
			int bossIndex = this.BossIndex;
			WaveManager instance = WaveManager.instance;
			return (bossIndex + ((instance != null) ? new int?(instance.AppendixLevel) : null)).GetValueOrDefault();
		}
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x000B0298 File Offset: 0x000AE498
	public int ProgressRequired()
	{
		if (this.chapterType == GenreWaveNode.ChapterType.PointTotal)
		{
			GenreSpawnNode genreSpawnNode = this.Spawns as GenreSpawnNode;
			return Mathf.FloorToInt((float)((genreSpawnNode != null) ? genreSpawnNode.PointTotal : 1) * AIManager.instance.Waves.GetPlayerValues(PlayerControl.PlayerCount).TotalBudgetMult);
		}
		return 1;
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x000B02E8 File Offset: 0x000AE4E8
	public void Setup()
	{
		GenreWaveNode.ChapterType chapterType = this.chapterType;
		if (chapterType != GenreWaveNode.ChapterType.PointTotal && chapterType == GenreWaveNode.ChapterType.Boss)
		{
			this.SpawnBossEnemies();
		}
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x000B030C File Offset: 0x000AE50C
	public AugmentTree GetBonusObjective(List<string> ignores, int heatLevel)
	{
		List<AugmentTree> modifiers = this.BonusObjective.GetModifiers(ModType.BonusObjective, ignores);
		if (!this.BonusObjective.Explicit)
		{
			modifiers.RemoveAll((AugmentTree x) => x.Root.MinHeat > (float)heatLevel);
		}
		return GraphDB.ChooseModFromList(ModType.BonusObjective, modifiers, false, true);
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x000B0360 File Offset: 0x000AE560
	private void SpawnBossEnemies()
	{
		AIData.AIDetails boss = this.GetBoss();
		if (boss == null)
		{
			return;
		}
		EntityControl entityControl = AIManager.SpawnEnemy(boss, false);
		foreach (string augmentID in this.AddedMods)
		{
			entityControl.net.AddAugment(augmentID);
		}
		entityControl.net.AddAugment(AIManager.instance.BossMod.ID);
		AIManager.instance.PointsSpawned += Mathf.Max(boss.PointValue, 1f);
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x000B0408 File Offset: 0x000AE608
	public AIData.AIDetails GetBoss()
	{
		AILayout layout = AIManager.instance.Layout;
		if (this.BossType == EnemyType.Unique)
		{
			return AIManager.instance.DB.GetDetails(this.ExplicitBoss);
		}
		return AIManager.instance.DB.GetDetails(layout.GetBoss(this.CalculatedBossIndex, this.BossType));
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x000B0464 File Offset: 0x000AE664
	public bool IsFinished()
	{
		GenreWaveNode.ChapterType chapterType = this.chapterType;
		if (chapterType == GenreWaveNode.ChapterType.PointTotal)
		{
			return WaveManager.GoalProportion >= 1f;
		}
		if (chapterType != GenreWaveNode.ChapterType.Boss)
		{
			return false;
		}
		if (AIManager.instance.InGoalKilled == 0 || AIManager.instance.PointsSpawned == 0f)
		{
			return false;
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.IsDead && entityControl.HasAugment(AIManager.instance.BossMod.ID, true))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x000B051C File Offset: 0x000AE71C
	public bool ShoudlEndWave()
	{
		GenreWaveNode.ChapterType chapterType = this.chapterType;
		if (chapterType != GenreWaveNode.ChapterType.PointTotal)
		{
			return chapterType == GenreWaveNode.ChapterType.Boss && this.IsFinished();
		}
		return this.IsFinished() && AIManager.RemainingForGenre == 0;
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x000B0554 File Offset: 0x000AE754
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Wave",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x000B057C File Offset: 0x000AE77C
	private List<GameObject> GetAllAI()
	{
		AIData aidata = Resources.Load<AIData>("AI Database");
		List<GameObject> list = new List<GameObject>();
		if (aidata == null)
		{
			return list;
		}
		foreach (AIData.AIDetails aidetails in aidata.Enemies)
		{
			list.Add(aidetails.Reference);
		}
		return list;
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x000B05F4 File Offset: 0x000AE7F4
	private List<string> GetModifierIDs()
	{
		GraphDB graphDB = Resources.Load<GraphDB>("GraphDB");
		List<string> list = new List<string>();
		if (graphDB == null)
		{
			return list;
		}
		foreach (AugmentTree augmentTree in graphDB.EnemyMods)
		{
			list.Add(augmentTree.ID);
		}
		return list;
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x000B066C File Offset: 0x000AE86C
	public GameMap GetMap(bool allowMultiBiome)
	{
		return GenreMapNode.GetMap(this.NextMap, allowMultiBiome, this.NextMapNode);
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x000B0680 File Offset: 0x000AE880
	public Vignette GetVignette()
	{
		if (!(this.NextVignette == null))
		{
			GenreVignetteNode genreVignetteNode = this.NextVignette as GenreVignetteNode;
			if (genreVignetteNode != null)
			{
				return genreVignetteNode.GetVignette();
			}
		}
		return null;
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x000B06B4 File Offset: 0x000AE8B4
	public string GetBonusObjectiveName()
	{
		if (this.BonusObjective.Explicit && this.BonusObjective.OptionOverrides.Count > 0 && this.BonusObjective.OptionOverrides[0] != null)
		{
			return "Bonus Objective: " + this.BonusObjective.OptionOverrides[0].name.Replace("BO_", "");
		}
		return "Bonus Objective";
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x000B072F File Offset: 0x000AE92F
	public bool NextMapExplicit()
	{
		return this.NextMap == GenreWaveNode.NextMapType.Explicit;
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x000B073A File Offset: 0x000AE93A
	public GenreWaveNode()
	{
	}

	// Token: 0x04001D9B RID: 7579
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreSpawnNode), false, "Spawns", PortLocation.Header)]
	public Node Spawns;

	// Token: 0x04001D9C RID: 7580
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreWorldNode), false, "Wave Options", PortLocation.Default)]
	public Node WaveOptions;

	// Token: 0x04001D9D RID: 7581
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreRewardNode), false, "Rewards", PortLocation.Default)]
	public Node Reward;

	// Token: 0x04001D9E RID: 7582
	[ShowPort("NextMapExplicit")]
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreMapNode), false, "Next Map", PortLocation.Default)]
	public Node NextMapNode;

	// Token: 0x04001D9F RID: 7583
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreVignetteNode), false, "Vignette", PortLocation.Default)]
	public Node NextVignette;

	// Token: 0x04001DA0 RID: 7584
	public GenreWaveNode.ChapterType chapterType;

	// Token: 0x04001DA1 RID: 7585
	[Range(1f, 10f)]
	public int BossIndex = 1;

	// Token: 0x04001DA2 RID: 7586
	public EnemyType BossType;

	// Token: 0x04001DA3 RID: 7587
	public GameObject ExplicitBoss;

	// Token: 0x04001DA4 RID: 7588
	public List<string> AddedMods = new List<string>();

	// Token: 0x04001DA5 RID: 7589
	public GenreWaveNode.EventType Event;

	// Token: 0x04001DA6 RID: 7590
	public AugmentFilter BonusObjective = new AugmentFilter();

	// Token: 0x04001DA7 RID: 7591
	public List<GenreSpawnNode.EliteSpawn> Elites;

	// Token: 0x04001DA8 RID: 7592
	[Header("Reward")]
	public AugmentFilter EliteReward = new AugmentFilter
	{
		CanUsePlayer = true
	};

	// Token: 0x04001DA9 RID: 7593
	public GenreWaveNode.NextMapType NextMap = GenreWaveNode.NextMapType.Random;

	// Token: 0x02000677 RID: 1655
	public enum NextMapType
	{
		// Token: 0x04002BAA RID: 11178
		_,
		// Token: 0x04002BAB RID: 11179
		Random,
		// Token: 0x04002BAC RID: 11180
		Explicit,
		// Token: 0x04002BAD RID: 11181
		None
	}

	// Token: 0x02000678 RID: 1656
	public enum ChapterType
	{
		// Token: 0x04002BAF RID: 11183
		PointTotal,
		// Token: 0x04002BB0 RID: 11184
		Boss
	}

	// Token: 0x02000679 RID: 1657
	public enum EventType
	{
		// Token: 0x04002BB2 RID: 11186
		None,
		// Token: 0x04002BB3 RID: 11187
		BonusObjective,
		// Token: 0x04002BB4 RID: 11188
		Elite
	}

	// Token: 0x0200067A RID: 1658
	[CompilerGenerated]
	private sealed class <>c__DisplayClass22_0
	{
		// Token: 0x060027CC RID: 10188 RVA: 0x000D71DE File Offset: 0x000D53DE
		public <>c__DisplayClass22_0()
		{
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000D71E6 File Offset: 0x000D53E6
		internal bool <GetBonusObjective>b__0(AugmentTree x)
		{
			return x.Root.MinHeat > (float)this.heatLevel;
		}

		// Token: 0x04002BB5 RID: 11189
		public int heatLevel;
	}
}
