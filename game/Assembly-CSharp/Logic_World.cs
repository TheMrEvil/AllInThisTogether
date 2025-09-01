using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200028A RID: 650
public class Logic_World : LogicNode
{
	// Token: 0x0600191E RID: 6430 RVA: 0x0009C9C4 File Offset: 0x0009ABC4
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		bool flag;
		switch (this.Test)
		{
		case Logic_World.WorldTest.CurrentState:
			flag = this.GameStateMatches();
			break;
		case Logic_World.WorldTest.InLastWave:
			flag = WaveManager.WaveConfig.IsFinished();
			break;
		case Logic_World.WorldTest.NextBossIs:
			flag = (this.BossRef == Logic_World.GetNextBoss());
			break;
		case Logic_World.WorldTest.InBonusObjective:
			flag = GoalManager.InBonusObjective;
			break;
		case Logic_World.WorldTest.QuestActive:
			flag = Progression.CanCompleteQuest(this.QuestID);
			break;
		case Logic_World.WorldTest.DoneVignetteAct:
		{
			VignetteControl instance = VignetteControl.instance;
			flag = (instance != null && instance.HasDoneAction(this.QuestID));
			break;
		}
		case Logic_World.WorldTest.DoneVignette:
			flag = MapManager.UsedVignettes.Contains(this.SceneRef.name);
			break;
		case Logic_World.WorldTest.IsFightingBoss:
			flag = (RaidManager.IsInRaid ? RaidManager.IsEncounterStarted : (AIManager.GetBoss() != null));
			break;
		case Logic_World.WorldTest.InLibrary:
			flag = MapManager.InLobbyScene;
			break;
		case Logic_World.WorldTest.ChallengeActive:
			flag = GameplayManager.IsChallengeActive;
			break;
		case Logic_World.WorldTest.IsLibraryRacing:
			flag = LibraryRaces.IsPlayerRacing;
			break;
		case Logic_World.WorldTest.IsRaidHardMode:
			flag = (RaidManager.IsInRaid && RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard);
			break;
		case Logic_World.WorldTest.RaidEncounterActive:
			flag = (RaidManager.IsInRaid && RaidManager.IsEncounterStarted);
			break;
		case Logic_World.WorldTest.IsInRaid:
			flag = RaidManager.IsInRaid;
			break;
		default:
			flag = false;
			break;
		}
		bool flag2 = flag;
		this.EditorStateDisplay = (flag2 ? NodeState.Success : NodeState.Fail);
		return flag2;
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x0009CB28 File Offset: 0x0009AD28
	private bool GameStateMatches()
	{
		if (RaidManager.IsInRaid)
		{
			GameState state = this.State;
			bool result;
			if (state != GameState.InWave)
			{
				if (state != GameState.Vignette_Inside)
				{
					result = (GameplayManager.instance.CurrentState == this.State);
				}
				else
				{
					result = (VignetteControl.instance != null);
				}
			}
			else
			{
				result = (RaidManager.IsEncounterStarted || GameplayManager.instance.CurrentState == this.State);
			}
			return result;
		}
		return GameplayManager.instance.CurrentState == this.State;
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x0009CBA4 File Offset: 0x0009ADA4
	public static GameObject GetNextBoss()
	{
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		GenreRootNode genreRootNode = (gameGraph != null) ? gameGraph.Root : null;
		if (genreRootNode == null)
		{
			return null;
		}
		int num = Mathf.Max(0, WaveManager.CurrentWave);
		if (WaveManager.instance.AppendixLevel > 0)
		{
			num = Mathf.Max(WaveManager.instance.AppendixChapterNumber - 1, 0);
			int i = num;
			while (i < genreRootNode.Appendix.Count)
			{
				GenreWaveNode genreWaveNode = genreRootNode.Appendix[i] as GenreWaveNode;
				if (genreWaveNode != null && genreWaveNode.chapterType == GenreWaveNode.ChapterType.Boss)
				{
					AIData.AIDetails boss = genreWaveNode.GetBoss();
					if (boss == null)
					{
						return null;
					}
					return boss.Reference;
				}
				else
				{
					i++;
				}
			}
			return null;
		}
		int j = num;
		while (j < genreRootNode.Waves.Count)
		{
			GenreWaveNode genreWaveNode2 = genreRootNode.Waves[j] as GenreWaveNode;
			if (genreWaveNode2 != null && genreWaveNode2.chapterType == GenreWaveNode.ChapterType.Boss)
			{
				AIData.AIDetails boss2 = genreWaveNode2.GetBoss();
				if (boss2 == null)
				{
					return null;
				}
				return boss2.Reference;
			}
			else
			{
				j++;
			}
		}
		return null;
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x0009CC98 File Offset: 0x0009AE98
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Game State";
		inspectorProps.SortX = true;
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x0009CCE7 File Offset: 0x0009AEE7
	public Logic_World()
	{
	}

	// Token: 0x04001938 RID: 6456
	public Logic_World.WorldTest Test;

	// Token: 0x04001939 RID: 6457
	public GameState State;

	// Token: 0x0400193A RID: 6458
	public GameObject BossRef;

	// Token: 0x0400193B RID: 6459
	public Scene SceneRef;

	// Token: 0x0400193C RID: 6460
	public string QuestID;

	// Token: 0x0200063A RID: 1594
	public enum WorldTest
	{
		// Token: 0x04002A7F RID: 10879
		MapSize,
		// Token: 0x04002A80 RID: 10880
		CurrentState,
		// Token: 0x04002A81 RID: 10881
		InLastWave,
		// Token: 0x04002A82 RID: 10882
		NextBossIs,
		// Token: 0x04002A83 RID: 10883
		InBonusObjective,
		// Token: 0x04002A84 RID: 10884
		QuestActive,
		// Token: 0x04002A85 RID: 10885
		DoneVignetteAct,
		// Token: 0x04002A86 RID: 10886
		DoneVignette,
		// Token: 0x04002A87 RID: 10887
		IsFightingBoss,
		// Token: 0x04002A88 RID: 10888
		InLibrary,
		// Token: 0x04002A89 RID: 10889
		ChallengeActive,
		// Token: 0x04002A8A RID: 10890
		IsLibraryRacing,
		// Token: 0x04002A8B RID: 10891
		IsRaidHardMode,
		// Token: 0x04002A8C RID: 10892
		RaidEncounterActive,
		// Token: 0x04002A8D RID: 10893
		IsInRaid
	}
}
