using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class ModDifficultyNode : Node
{
	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000AB8E1 File Offset: 0x000A9AE1
	private AugmentRootNode MyAugment
	{
		get
		{
			return this.CalledFrom as AugmentRootNode;
		}
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000AB8EE File Offset: 0x000A9AEE
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Difficulty Mod",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000AB918 File Offset: 0x000A9B18
	public static int ModifyDifficulty(AugmentRootNode augment, ModDifficultyNode dMod, int baseVal, GenreTree genre, int wave, List<AugmentRootNode> enemyAugments, List<ModDifficultyNode.PlayerInfo> players)
	{
		float num = ModDifficultyNode.ModifyFromGenre(augment, (float)baseVal, genre, wave);
		if (num < -10f)
		{
			return (int)num;
		}
		if (dMod == null)
		{
			return (int)Mathf.Clamp(num, -100f, 100f);
		}
		num = ModDifficultyNode.ModifyFromPlayers(augment, dMod, (float)baseVal, players);
		if (num < -10f)
		{
			return (int)num;
		}
		num = ModDifficultyNode.ModifyFromEnemy(augment, dMod, (float)baseVal, enemyAugments);
		if (num < -10f)
		{
			return (int)num;
		}
		return (int)Mathf.Clamp(num, -100f, 100f);
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x000AB998 File Offset: 0x000A9B98
	public static float ModifyFromGenre(AugmentRootNode augment, float baseVal, GenreTree genre, int curWave)
	{
		if (augment.ApplyType == EnemyType.Any || genre == null)
		{
			return baseVal;
		}
		float num = (float)(genre.Root.Waves.Count - curWave);
		int num2 = 0;
		for (int i = curWave; i < genre.Root.Waves.Count; i++)
		{
			GenreWaveNode genreWaveNode = genre.Root.Waves[i] as GenreWaveNode;
			if (genreWaveNode.Spawn != null && genreWaveNode.Spawn.HasEnemyType(augment.ApplyType))
			{
				num2++;
			}
		}
		if (num2 == 0)
		{
			return -100f;
		}
		float num3 = 1f - (float)num2 / num;
		baseVal -= num3 * 25f;
		return baseVal;
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x000ABA48 File Offset: 0x000A9C48
	public static float ModifyFromPlayers(AugmentRootNode augment, ModDifficultyNode dMod, float baseVal, List<ModDifficultyNode.PlayerInfo> players)
	{
		if (players == null || players.Count == 0)
		{
			return baseVal;
		}
		foreach (ModDifficultyNode.PlayerInfo playerInfo in players)
		{
		}
		return baseVal;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x000ABAA0 File Offset: 0x000A9CA0
	public static float ModifyFromEnemy(AugmentRootNode augment, ModDifficultyNode dMod, float baseVal, List<AugmentRootNode> augments)
	{
		if (augments == null || augments.Count == 0)
		{
			return baseVal;
		}
		foreach (AugmentRootNode augmentRootNode in augments)
		{
		}
		return baseVal;
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x000ABAF8 File Offset: 0x000A9CF8
	public static void FilterDifficultyMods(ref List<AugmentTree> options, int desiredDiff)
	{
		float num = 15f;
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		int currentWave = WaveManager.CurrentWave;
		List<AugmentRootNode> enemyAugments = new List<AugmentRootNode>(AIManager.GlobalEnemyMods.trees.Keys);
		List<ModDifficultyNode.PlayerInfo> list = new List<ModDifficultyNode.PlayerInfo>();
		foreach (PlayerControl player in PlayerControl.AllPlayers)
		{
			list.Add(new ModDifficultyNode.PlayerInfo(player));
		}
		int count = options.Count;
		for (int i = options.Count - 1; i >= 0; i--)
		{
			AugmentTree augmentTree = options[i];
			int num2 = (augmentTree != null) ? augmentTree.Root.GetDifficulty(gameGraph, currentWave, enemyAugments, list) : -100;
			if (num2 < 0 || (float)Mathf.Abs(num2 - desiredDiff) > num)
			{
				options.RemoveAt(i);
			}
		}
		Debug.Log(string.Format("Filtered {0} enemy upgrades down to {1} for Difficulty Request: {2}/100", count, options.Count, desiredDiff));
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x000ABC0C File Offset: 0x000A9E0C
	public ModDifficultyNode()
	{
	}

	// Token: 0x02000664 RID: 1636
	[Serializable]
	public class PlayerInfo
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x000D6ED8 File Offset: 0x000D50D8
		public PlayerInfo()
		{
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000D6EF8 File Offset: 0x000D50F8
		public PlayerInfo(PlayerControl player)
		{
			if (player == null)
			{
				return;
			}
			this.Core = player.actions.core;
			this.Primary = player.actions.primary;
			this.Secondary = player.actions.secondary;
			this.Movement = player.actions.movement;
			this.Augments = new List<AugmentRootNode>(player.AllAugments(false, null).trees.Keys);
		}

		// Token: 0x04002B44 RID: 11076
		public AugmentTree Core;

		// Token: 0x04002B45 RID: 11077
		public AbilityTree Primary;

		// Token: 0x04002B46 RID: 11078
		public AbilityTree Secondary;

		// Token: 0x04002B47 RID: 11079
		public AbilityTree Movement;

		// Token: 0x04002B48 RID: 11080
		public List<AugmentTree> DebugAugmentInfo = new List<AugmentTree>();

		// Token: 0x04002B49 RID: 11081
		[NonSerialized]
		public List<AugmentRootNode> Augments = new List<AugmentRootNode>();
	}
}
