using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class GameRecord
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0004F012 File Offset: 0x0004D212
	private static GameRecord Current
	{
		get
		{
			if (GameRecord._current == null)
			{
				GameRecord.Reset();
			}
			return GameRecord._current;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0004F028 File Offset: 0x0004D228
	private GameRecord.Chapter CurrentChapter
	{
		get
		{
			if (this.Chapters.Count == 0)
			{
				GameRecord.NextChapter();
			}
			List<GameRecord.Chapter> chapters = this.Chapters;
			int index = chapters.Count - 1;
			return chapters[index];
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0004F05C File Offset: 0x0004D25C
	public static void Reset()
	{
		GameRecord._current = new GameRecord();
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0004F068 File Offset: 0x0004D268
	public static void NewGame()
	{
		GameRecord.Reset();
		GameRecord.Current.HeaderInfo.Setup();
		GameRecord.NextChapter();
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0004F084 File Offset: 0x0004D284
	public static void NextChapter()
	{
		GameRecord.Chapter chapter = new GameRecord.Chapter();
		chapter.Setup();
		if (GameRecord.Current.Chapters.Count > 0)
		{
			GameRecord.Current.CurrentChapter.Completed();
		}
		GameRecord.Current.Chapters.Add(chapter);
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0004F0CE File Offset: 0x0004D2CE
	public static void Heartbeat()
	{
		GameRecord.Current.AddHeartbeat();
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0004F0DA File Offset: 0x0004D2DA
	public static void FontPurchase(AugmentTree augment)
	{
		GameRecord.Current.CurrentChapter.FontPurchased(augment);
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0004F0EC File Offset: 0x0004D2EC
	public static void EnemyUpgradeChosen(AugmentTree augment, List<AugmentTree> options, int votes)
	{
		GameRecord.Current.CurrentChapter.EnemyUpgraded(augment, options, votes);
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0004F100 File Offset: 0x0004D300
	public static void PlayerUpgradeChosen(int playerID, AugmentTree augment, List<AugmentTree> options)
	{
		GameRecord.Current.CurrentChapter.PlayerUpgraded(playerID, augment, options);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0004F114 File Offset: 0x0004D314
	public static void PlayerUpgradeRerolled(int playerID, List<AugmentTree> options)
	{
		GameRecord.Current.CurrentChapter.PlayerRerolled(playerID, options);
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x0004F127 File Offset: 0x0004D327
	public static void EnemySeen(string enemy)
	{
		GameRecord.Current.CurrentChapter.AddEnemy(enemy);
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0004F13C File Offset: 0x0004D33C
	public static void AIEvent(GameRecord.EventType e, AIControl ai, string extra = "")
	{
		GameRecord.ChapterEvent chapterEvent = GameRecord.RecordEvent(e);
		switch (e)
		{
		case GameRecord.EventType.Enemy_Spawned:
			chapterEvent.id = ai.AIName;
			chapterEvent.location = ai.movement.GetPosition();
			chapterEvent.viewID = ai.ViewID;
			return;
		case GameRecord.EventType.Enemy_Died:
			chapterEvent.location = ai.movement.GetPosition();
			chapterEvent.viewID = ai.ViewID;
			return;
		case GameRecord.EventType.Enemy_AbilityUsed:
			chapterEvent.location = ai.movement.GetPosition();
			chapterEvent.viewID = ai.ViewID;
			chapterEvent.id = extra;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x0004F1D2 File Offset: 0x0004D3D2
	public static void PlayerAbilityEvent(PlayerControl plr, PlayerAbilityType ability)
	{
		GameRecord.ChapterEvent chapterEvent = GameRecord.RecordEvent(GameRecord.EventType.Player_AbilityUsed);
		chapterEvent.location = plr.movement.GetPosition();
		chapterEvent.viewID = plr.ViewID;
		chapterEvent.id = ability.ToString();
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0004F20C File Offset: 0x0004D40C
	public static void PlayerDamageTaken(PlayerControl plr, DamageInfo dmg)
	{
		GameRecord.ChapterEvent chapterEvent = GameRecord.RecordEvent(GameRecord.EventType.Player_DamageTaken);
		chapterEvent.viewID = plr.ViewID;
		chapterEvent.otherViewID = dmg.SourceID;
		chapterEvent.location = plr.movement.GetPosition();
		chapterEvent.dmgRef = dmg;
		chapterEvent.curBar = (int)plr.health.shield;
		chapterEvent.curHP = plr.health.health;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0004F271 File Offset: 0x0004D471
	public static void PlayerBarrierBroken(PlayerControl plr, DamageInfo dmg)
	{
		GameRecord.ChapterEvent chapterEvent = GameRecord.RecordEvent(GameRecord.EventType.Player_BarrierBreak);
		chapterEvent.viewID = plr.ViewID;
		chapterEvent.otherViewID = dmg.SourceID;
		chapterEvent.location = plr.movement.GetPosition();
		chapterEvent.dmgRef = dmg;
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0004F2A8 File Offset: 0x0004D4A8
	private static GameRecord.ChapterEvent RecordEvent(GameRecord.EventType e)
	{
		GameRecord.ChapterEvent chapterEvent = new GameRecord.ChapterEvent(e);
		GameRecord.AddChapterEvent(chapterEvent);
		return chapterEvent;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x0004F2B6 File Offset: 0x0004D4B6
	public static void RecordEvent(GameRecord.EventType e, string id)
	{
		GameRecord.AddChapterEvent(new GameRecord.ChapterEvent(e)
		{
			id = id
		});
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0004F2CA File Offset: 0x0004D4CA
	public static void RecordEvent(GameRecord.EventType e, string id, Vector3 loc)
	{
		GameRecord.AddChapterEvent(new GameRecord.ChapterEvent(e)
		{
			id = id,
			location = loc
		});
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0004F2E8 File Offset: 0x0004D4E8
	public static void RecordEvent(GameRecord.EventType e, PlayerControl playerRef, Vector3 loc, PlayerControl playerRefTwo = null)
	{
		GameRecord.ChapterEvent chapterEvent = new GameRecord.ChapterEvent(e)
		{
			location = loc,
			viewID = playerRef.ViewID
		};
		if (playerRefTwo != null && playerRefTwo != playerRef)
		{
			chapterEvent.otherViewID = playerRefTwo.ViewID;
		}
		GameRecord.AddChapterEvent(chapterEvent);
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0004F333 File Offset: 0x0004D533
	public static void RecordEvent(GameRecord.EventType e, PlayerControl playerRef, string id)
	{
		GameRecord.AddChapterEvent(new GameRecord.ChapterEvent(e)
		{
			viewID = playerRef.ViewID,
			id = id
		});
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0004F354 File Offset: 0x0004D554
	public void AddHeartbeat()
	{
		float gameTime = GameplayManager.instance.GameTime;
		foreach (PlayerControl entity in PlayerControl.AllPlayers)
		{
			this.Heartbeats.Add(new GameRecord.HeartBeat(entity, gameTime));
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			AIControl aicontrol = (AIControl)entityControl;
			if (aicontrol.Level.HasFlag(EnemyLevel.Boss))
			{
				this.Heartbeats.Add(new GameRecord.HeartBeat(aicontrol, gameTime));
			}
		}
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0004F42C File Offset: 0x0004D62C
	public static List<string> GetCurrentBindingNames()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
		{
			list.Add(keyValuePair.Key.Name);
		}
		return list;
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0004F49C File Offset: 0x0004D69C
	public static List<string> GetCurrentTornPageNames()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in AIManager.GlobalEnemyMods.trees)
		{
			list.Add(keyValuePair.Key.Name);
		}
		return list;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0004F508 File Offset: 0x0004D708
	public static List<string> GetCurrentFontPowerNames()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
		{
			list.Add(keyValuePair.Key.Name);
		}
		return list;
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0004F574 File Offset: 0x0004D774
	public static string GetCurrentPlayerData()
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("players", GameRecord.Header.GetPlayerData());
		return jsonobject.ToString();
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0004F590 File Offset: 0x0004D790
	public static string GetLossData()
	{
		if (GameRecord.Current.HeaderInfo.Won)
		{
			return "";
		}
		int count = GameRecord.Current.Chapters.Count;
		GameRecord.Chapter currentChapter = GameRecord.Current.CurrentChapter;
		List<string> list = currentChapter.EnemiesPresent.ToList<string>();
		JSONNode jsonnode = new JSONObject();
		jsonnode.Add("wave", count);
		jsonnode.Add("map", MapManager.CurrentSceneName);
		if (currentChapter.HasEvent(GameRecord.EventType.Bonus_Spawned))
		{
			jsonnode.Add("bonus", currentChapter.GetFirstEvent(GameRecord.EventType.Bonus_Spawned).id);
		}
		if (list.Count > 0)
		{
			JSONArray jsonarray = new JSONArray();
			foreach (string s in list)
			{
				jsonarray.Add(s);
			}
			jsonnode.Add("enemies", jsonarray);
		}
		return jsonnode.ToString();
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0004F69C File Offset: 0x0004D89C
	private static void AddChapterEvent(GameRecord.ChapterEvent e)
	{
		if (GameRecord.Current.Chapters.Count == 0)
		{
			GameRecord.NextChapter();
		}
		GameRecord.Current.CurrentChapter.AddEvent(e);
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
	public static void GameCompleted(bool won, float totalTime)
	{
		GameRecord.Current.HeaderInfo.Won = won;
		GameRecord.Current.HeaderInfo.GameTime = totalTime;
		GameRecord.Current.CurrentChapter.Completed();
		if (!GameplayManager.IsChallengeActive)
		{
			GameRecord.Upload();
		}
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0004F704 File Offset: 0x0004D904
	private string ToJSON()
	{
		JSONNode jsonnode = new JSONObject();
		jsonnode.Add("header", this.HeaderInfo.GetJSON());
		JSONArray jsonarray = new JSONArray();
		foreach (GameRecord.Chapter chapter in this.Chapters)
		{
			jsonarray.Add(chapter.GetJSON());
		}
		jsonnode.Add("chapters", jsonarray);
		JSONArray jsonarray2 = new JSONArray();
		foreach (GameRecord.HeartBeat heartBeat in this.Heartbeats)
		{
			jsonarray2.Add(heartBeat.GetJSON());
		}
		jsonnode.Add("heartbeats", jsonarray2);
		return jsonnode.ToString();
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0004F7F0 File Offset: 0x0004D9F0
	public static void Upload()
	{
		if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode && PlayerControl.AllPlayers.Count > 1)
		{
			return;
		}
		ParseManager.UploadRun(GameRecord.Current.ToJSON().NetworkCompress(), NetworkManager.GetVersionCode(), GameplayManager.instance.GameGraph.Root.Name, PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount : 1, GameplayManager.BindingLevel, GameRecord.Current.HeaderInfo.Won, GameRecord.Current.HeaderInfo.GameTime, GameRecord.GetCurrentBindingNames(), GameRecord.GetCurrentTornPageNames(), GameRecord.GetCurrentFontPowerNames(), GameRecord.GetCurrentPlayerData(), GameRecord.GetLossData());
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0004F898 File Offset: 0x0004DA98
	public static void UploadQuit()
	{
		if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode && PlayerControl.AllPlayers.Count > 1)
		{
			return;
		}
		ParseManager.UploadQuit(GameRecord.Current.ToJSON(), NetworkManager.GetVersionCode(), GameplayManager.instance.GameGraph.Root.Name, GameplayManager.BindingLevel, GameRecord.GetCurrentBindingNames(), GameRecord.GetCurrentTornPageNames(), GameRecord.GetCurrentFontPowerNames(), GameRecord.GetCurrentPlayerData());
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0004F904 File Offset: 0x0004DB04
	public static void UploadAppendix(bool didWin)
	{
		if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode && PlayerControl.AllPlayers.Count > 1)
		{
			return;
		}
		int num = WaveManager.instance.AppendixChapterNumber - (didWin ? 1 : 0);
		float level = (float)WaveManager.instance.AppendixLevel + Mathf.Max(0f, (float)num / 10f);
		ParseManager.UploadAppendix(NetworkManager.GetVersionCode(), GameplayManager.CurTomeRoot.Name, GameplayManager.BindingLevel, didWin, PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount : 1, level, GameRecord.GetCurrentBindingNames(), GameRecord.GetCurrentTornPageNames(), GameRecord.GetCurrentFontPowerNames(), GameRecord.GetCurrentPlayerData());
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0004F9A4 File Offset: 0x0004DBA4
	public static void UploadChallenge(bool didWin, float baseTime, float totalTime, int uniqueStat)
	{
		if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode && PlayerControl.AllPlayers.Count > 1)
		{
			return;
		}
		Debug.Log("Upload Challenge sending...");
		int appendixLevel = WaveManager.instance.AppendixLevel;
		ParseManager.UploadChallenge(NetworkManager.GetVersionCode(), GameplayManager.Challenge.ID, MetaDB.GetCurrentChallengeNumber(), didWin, PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount : 1, appendixLevel, baseTime, totalTime, GameRecord.GetCurrentPlayerData(), uniqueStat);
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004FA19 File Offset: 0x0004DC19
	public GameRecord()
	{
	}

	// Token: 0x040009E3 RID: 2531
	public const float HEARTBEAT_TIMER = 2.5f;

	// Token: 0x040009E4 RID: 2532
	private static GameRecord _current;

	// Token: 0x040009E5 RID: 2533
	private readonly GameRecord.Header HeaderInfo = new GameRecord.Header();

	// Token: 0x040009E6 RID: 2534
	private readonly List<GameRecord.Chapter> Chapters = new List<GameRecord.Chapter>();

	// Token: 0x040009E7 RID: 2535
	private readonly List<GameRecord.HeartBeat> Heartbeats = new List<GameRecord.HeartBeat>();

	// Token: 0x020004FE RID: 1278
	private class Header
	{
		// Token: 0x0600236E RID: 9070 RVA: 0x000C9ECE File Offset: 0x000C80CE
		public void Setup()
		{
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000C9ED0 File Offset: 0x000C80D0
		public JSONNode GetJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("version", NetworkManager.GetVersionCode());
			jsonnode.Add("genre", GameplayManager.instance.GameGraph.Root.Name);
			jsonnode.Add("AI", AIManager.instance.Layout.GetJSON());
			jsonnode.Add("win", this.Won);
			jsonnode.Add("timer", this.GameTime);
			jsonnode.Add("binding_level", GameplayManager.BindingLevel);
			JSONArray jsonarray = new JSONArray();
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
			{
				AugmentRootNode augmentRootNode;
				int num;
				keyValuePair.Deconstruct(out augmentRootNode, out num);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				jsonarray.Add(augmentRootNode2.Name);
			}
			jsonnode.Add("bindings", jsonarray);
			jsonnode.Add("fountain", this.GetFountainData());
			jsonnode.Add("players", GameRecord.Header.GetPlayerData());
			JSONArray jsonarray2 = new JSONArray();
			foreach (string s in MapManager.UsedVignettes)
			{
				jsonarray2.Add(s);
			}
			jsonnode.Add("vignettes", jsonarray2);
			return jsonnode;
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000CA070 File Offset: 0x000C8270
		private JSONArray GetFountainData()
		{
			JSONArray jsonarray = new JSONArray();
			foreach (InkRow inkRow in InkManager.Store)
			{
				JSONArray jsonarray2 = new JSONArray();
				foreach (InkTalent inkTalent in inkRow.Options)
				{
					JSONNode jsonnode = new JSONObject();
					jsonnode.Add("augment", inkTalent.Augment.GetName());
					jsonnode.Add("cost", inkTalent.Cost);
					jsonnode.Add("invested", inkTalent.CurrentValue);
					jsonarray2.Add(jsonnode);
				}
				jsonarray.Add(jsonarray2);
			}
			return jsonarray;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000CA16C File Offset: 0x000C836C
		public static JSONArray GetPlayerData()
		{
			JSONArray jsonarray = new JSONArray();
			foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
			{
				JSONNode jsonnode = new JSONObject();
				jsonnode.Add("GameID", playerControl.ViewID);
				jsonnode.Add("Username", playerControl.Username);
				jsonnode.Add("level", playerControl.InkLevel);
				jsonnode.Add("prestige", playerControl.PrestigeLevel);
				JSONNode jsonnode2 = new JSONObject();
				jsonnode2.Add("core", playerControl.actions.core.Root.Name);
				jsonnode2.Add("primary", playerControl.actions.primary.Root.Usage.AbilityMetadata.Name);
				jsonnode2.Add("secondary", playerControl.actions.secondary.Root.Usage.AbilityMetadata.Name);
				jsonnode2.Add("movement", playerControl.actions.movement.Root.Usage.AbilityMetadata.Name);
				jsonnode.Add("loadout", jsonnode2);
				JSONArray jsonarray2 = new JSONArray();
				foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in playerControl.Augment.trees)
				{
					AugmentRootNode augmentRootNode;
					int num;
					keyValuePair.Deconstruct(out augmentRootNode, out num);
					AugmentRootNode augmentRootNode2 = augmentRootNode;
					jsonarray2.Add(augmentRootNode2.Name);
				}
				jsonnode.Add("pages", jsonarray2);
				jsonarray.Add(jsonnode);
			}
			return jsonarray;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000CA384 File Offset: 0x000C8584
		public Header()
		{
		}

		// Token: 0x0400255B RID: 9563
		private int playerCount;

		// Token: 0x0400255C RID: 9564
		public bool Won;

		// Token: 0x0400255D RID: 9565
		public float GameTime;
	}

	// Token: 0x020004FF RID: 1279
	public class Chapter
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x000CA38C File Offset: 0x000C858C
		public void Setup()
		{
			GameplayManager instance = GameplayManager.instance;
			this.startTime = ((instance != null) ? instance.GameTime : 0f);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000CA3A9 File Offset: 0x000C85A9
		public void AddEvent(GameRecord.ChapterEvent e)
		{
			this.events.Add(e);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000CA3B7 File Offset: 0x000C85B7
		public void FontPurchased(AugmentTree augment)
		{
			this.FountainPurchases.Add(augment.Root.Name);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000CA3CF File Offset: 0x000C85CF
		public void EnemyUpgraded(AugmentTree chosen, List<AugmentTree> options, int votes = 0)
		{
			this.EnemyPick = new GameRecord.Chapter.UpgradeSelection(chosen.Root, options, -1)
			{
				Votes = votes
			};
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000CA3EC File Offset: 0x000C85EC
		public void PlayerRerolled(int playerID, List<AugmentTree> options)
		{
			GameRecord.Chapter.UpgradeSelection item = new GameRecord.Chapter.UpgradeSelection(null, options, playerID);
			this.Rerolls.Add(item);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000CA410 File Offset: 0x000C8610
		public void PlayerUpgraded(int playerID, AugmentTree chosen, List<AugmentTree> options)
		{
			GameRecord.Chapter.UpgradeSelection item = new GameRecord.Chapter.UpgradeSelection(chosen.Root, options, playerID);
			this.PlayerPicks.Add(item);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000CA437 File Offset: 0x000C8637
		public void AddEnemy(string enemyID)
		{
			if (!this.EnemiesPresent.Contains(enemyID))
			{
				this.EnemiesPresent.Add(enemyID);
			}
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000CA454 File Offset: 0x000C8654
		public GameRecord.ChapterEvent GetFirstEvent(GameRecord.EventType ev)
		{
			foreach (GameRecord.ChapterEvent chapterEvent in this.events)
			{
				if (chapterEvent.eventType == ev)
				{
					return chapterEvent;
				}
			}
			return null;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000CA4B0 File Offset: 0x000C86B0
		public bool HasEvent(GameRecord.EventType ev)
		{
			using (List<GameRecord.ChapterEvent>.Enumerator enumerator = this.events.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.eventType == ev)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000CA50C File Offset: 0x000C870C
		public void Completed()
		{
			this.endTime = GameplayManager.instance.GameTime;
			this.map = MapManager.CurrentSceneName;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000CA52C File Offset: 0x000C872C
		public JSONNode GetJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("map", this.map);
			jsonnode.Add("time_start", (int)this.startTime);
			jsonnode.Add("time_end", (int)this.endTime);
			if (this.PlayerPicks.Count > 0)
			{
				JSONArray jsonarray = new JSONArray();
				foreach (GameRecord.Chapter.UpgradeSelection upgradeSelection in this.PlayerPicks)
				{
					jsonarray.Add(upgradeSelection.ToJSON());
				}
				jsonnode.Add("upgrades_player", jsonarray);
			}
			if (this.Rerolls.Count > 0)
			{
				JSONArray jsonarray2 = new JSONArray();
				foreach (GameRecord.Chapter.UpgradeSelection upgradeSelection2 in this.Rerolls)
				{
					jsonarray2.Add(upgradeSelection2.ToJSON());
				}
				jsonnode.Add("rerolls_player", jsonarray2);
			}
			if (this.FountainPurchases.Count > 0)
			{
				JSONArray jsonarray3 = new JSONArray();
				foreach (string s in this.FountainPurchases)
				{
					jsonarray3.Add(s);
				}
				jsonnode.Add("upgrades_font", jsonarray3);
			}
			if (this.EnemyPick != null)
			{
				jsonnode.Add("upgrade_enemy", this.EnemyPick.ToJSON());
			}
			JSONArray jsonarray4 = new JSONArray();
			foreach (string s2 in this.EnemiesPresent)
			{
				jsonarray4.Add(s2);
			}
			jsonnode.Add("enemies", jsonarray4);
			JSONArray jsonarray5 = new JSONArray();
			foreach (GameRecord.ChapterEvent chapterEvent in this.events)
			{
				jsonarray5.Add(chapterEvent.GetJSON());
			}
			jsonnode.Add("events", jsonarray5);
			return jsonnode;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000CA7A4 File Offset: 0x000C89A4
		public static GameRecord.Chapter FromJSON(JSONNode n)
		{
			GameRecord.Chapter chapter = new GameRecord.Chapter();
			chapter.map = n.GetValueOrDefault("map", "-");
			chapter.startTime = n.GetValueOrDefault("time_start", 0);
			chapter.endTime = n.GetValueOrDefault("time_end", 0);
			foreach (KeyValuePair<string, JSONNode> keyValuePair in (n["enemies"] as JSONArray))
			{
				chapter.EnemiesPresent.Add(keyValuePair.Value.ToString().Replace("\"", ""));
			}
			foreach (KeyValuePair<string, JSONNode> keyValuePair2 in (n["events"] as JSONArray))
			{
				chapter.events.Add(GameRecord.ChapterEvent.FromJSON(keyValuePair2.Value));
			}
			return chapter;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000CA8A0 File Offset: 0x000C8AA0
		public Chapter()
		{
		}

		// Token: 0x0400255E RID: 9566
		public string map = "-";

		// Token: 0x0400255F RID: 9567
		public float startTime;

		// Token: 0x04002560 RID: 9568
		public float endTime;

		// Token: 0x04002561 RID: 9569
		public List<string> FountainPurchases = new List<string>();

		// Token: 0x04002562 RID: 9570
		private GameRecord.Chapter.UpgradeSelection EnemyPick;

		// Token: 0x04002563 RID: 9571
		private List<GameRecord.Chapter.UpgradeSelection> Rerolls = new List<GameRecord.Chapter.UpgradeSelection>();

		// Token: 0x04002564 RID: 9572
		private List<GameRecord.Chapter.UpgradeSelection> PlayerPicks = new List<GameRecord.Chapter.UpgradeSelection>();

		// Token: 0x04002565 RID: 9573
		public HashSet<string> EnemiesPresent = new HashSet<string>();

		// Token: 0x04002566 RID: 9574
		public List<GameRecord.ChapterEvent> events = new List<GameRecord.ChapterEvent>();

		// Token: 0x020006C0 RID: 1728
		private class UpgradeSelection
		{
			// Token: 0x06002863 RID: 10339 RVA: 0x000D879C File Offset: 0x000D699C
			public UpgradeSelection(AugmentRootNode augment, List<AugmentTree> options, int pid = -1)
			{
				this.playerID = pid;
				this.Chosen = ((augment == null) ? "-" : augment.Name);
				foreach (AugmentTree augmentTree in options)
				{
					this.Options.Add(((augmentTree != null) ? augmentTree.Root.Name : null) ?? "-");
				}
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x000D8858 File Offset: 0x000D6A58
			public JSONNode ToJSON()
			{
				JSONNode jsonnode = new JSONObject();
				if (this.playerID != -1)
				{
					jsonnode.Add("player", this.playerID);
				}
				if (this.Votes > 0)
				{
					jsonnode.Add("votes", this.Votes);
				}
				jsonnode.Add("chosen", this.Chosen);
				JSONArray jsonarray = new JSONArray();
				foreach (string s in this.Options)
				{
					jsonarray.Add(s);
				}
				jsonnode.Add("options", jsonarray);
				return jsonnode;
			}

			// Token: 0x04002CDB RID: 11483
			private int playerID = -1;

			// Token: 0x04002CDC RID: 11484
			public int Votes = -1;

			// Token: 0x04002CDD RID: 11485
			private string Chosen = "-";

			// Token: 0x04002CDE RID: 11486
			private List<string> Options = new List<string>();
		}
	}

	// Token: 0x02000500 RID: 1280
	public class ChapterEvent
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x000CA8F8 File Offset: 0x000C8AF8
		public ChapterEvent(GameRecord.EventType e)
		{
			this.eventType = e;
			this.location = Vector3.one.INVALID();
			GameplayManager instance = GameplayManager.instance;
			this.eventTime = ((instance != null) ? instance.GameTime : 0f);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000CA958 File Offset: 0x000C8B58
		public JSONNode GetJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("event_id", (int)this.eventType);
			jsonnode.Add("event", this.eventType.ToString());
			jsonnode.Add("time", this.eventTime.ToString("0.00"));
			if (this.location.IsValid())
			{
				jsonnode.Add("location", this.location.ToSimpleString());
			}
			if (this.viewID > 0)
			{
				jsonnode.Add("view_id", this.viewID);
			}
			if (!string.IsNullOrEmpty(this.id) && this.id.Length > 0)
			{
				jsonnode.Add("id", this.id);
			}
			if (this.otherViewID > 0)
			{
				jsonnode.Add("by_view_id", this.otherViewID);
			}
			if (this.dmgRef != null)
			{
				jsonnode.Add("hp", this.curHP);
				jsonnode.Add("bar", this.curBar);
				jsonnode.Add("amt", (int)this.dmgRef.TotalAmount);
				jsonnode.Add("src", this.dmgRef.CauseName);
			}
			return jsonnode;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000CAAC8 File Offset: 0x000C8CC8
		public static GameRecord.ChapterEvent FromJSON(JSONNode jsn)
		{
			GameRecord.ChapterEvent chapterEvent = new GameRecord.ChapterEvent((GameRecord.EventType)jsn.GetValueOrDefault("event_id", 0));
			chapterEvent.eventTime = jsn.GetValueOrDefault("time", 0);
			if (jsn.HasKey("location"))
			{
				chapterEvent.location = jsn.GetValueOrDefault("location", "").ToString().ToVector3();
			}
			if (jsn.HasKey("view_id"))
			{
				chapterEvent.viewID = jsn.GetValueOrDefault("view_id", -1);
			}
			if (jsn.HasKey("id"))
			{
				chapterEvent.id = jsn.GetValueOrDefault("id", "");
			}
			if (jsn.HasKey("by_view_id"))
			{
				chapterEvent.otherViewID = jsn.GetValueOrDefault("by_view_id", -1);
			}
			if (jsn.HasKey("hp"))
			{
				chapterEvent.curHP = jsn.GetValueOrDefault("hp", 0);
				chapterEvent.curBar = jsn.GetValueOrDefault("bar", 0);
				DamageInfo damageInfo = new DamageInfo(jsn.GetValueOrDefault("amt", 0));
				if (jsn.HasKey("src"))
				{
					damageInfo.CauseName = jsn.GetValueOrDefault("src", "");
				}
			}
			return chapterEvent;
		}

		// Token: 0x04002567 RID: 9575
		public GameRecord.EventType eventType;

		// Token: 0x04002568 RID: 9576
		public int viewID = -1;

		// Token: 0x04002569 RID: 9577
		public string id = "";

		// Token: 0x0400256A RID: 9578
		public Vector3 location;

		// Token: 0x0400256B RID: 9579
		public int otherViewID = -1;

		// Token: 0x0400256C RID: 9580
		public DamageInfo dmgRef;

		// Token: 0x0400256D RID: 9581
		public int curHP;

		// Token: 0x0400256E RID: 9582
		public int curBar;

		// Token: 0x0400256F RID: 9583
		public float eventTime;
	}

	// Token: 0x02000501 RID: 1281
	public enum EventType
	{
		// Token: 0x04002571 RID: 9585
		_,
		// Token: 0x04002572 RID: 9586
		Player_Connected,
		// Token: 0x04002573 RID: 9587
		Player_Disconnected,
		// Token: 0x04002574 RID: 9588
		Player_Died,
		// Token: 0x04002575 RID: 9589
		Player_Revived,
		// Token: 0x04002576 RID: 9590
		Player_AugmentAdd,
		// Token: 0x04002577 RID: 9591
		Player_DamageTaken,
		// Token: 0x04002578 RID: 9592
		Player_AbilityUsed,
		// Token: 0x04002579 RID: 9593
		Player_BarrierBreak,
		// Token: 0x0400257A RID: 9594
		Bonus_Spawned = 64,
		// Token: 0x0400257B RID: 9595
		Bonus_Completed,
		// Token: 0x0400257C RID: 9596
		Bonus_Failed,
		// Token: 0x0400257D RID: 9597
		Enemy_Spawned = 90,
		// Token: 0x0400257E RID: 9598
		Enemy_Died,
		// Token: 0x0400257F RID: 9599
		Enemy_AbilityUsed
	}

	// Token: 0x02000502 RID: 1282
	private class HeartBeat
	{
		// Token: 0x06002383 RID: 9091 RVA: 0x000CAC50 File Offset: 0x000C8E50
		public HeartBeat(EntityControl entity, float t)
		{
			this.viewID = entity.ViewID;
			this.Location = entity.movement.GetPosition();
			this.Health = entity.health.health;
			this.Barrier = (int)entity.health.shield;
			this.Time = (int)t;
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000CACAC File Offset: 0x000C8EAC
		public JSONNode GetJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("t", this.Time);
			jsonnode.Add("id", this.viewID);
			jsonnode.Add("hp", this.Health);
			jsonnode.Add("bar", this.Barrier);
			if (this.Location.IsValid())
			{
				jsonnode.Add("loc", this.Location.ToSimpleString());
			}
			return jsonnode;
		}

		// Token: 0x04002580 RID: 9600
		private int viewID;

		// Token: 0x04002581 RID: 9601
		private int Time;

		// Token: 0x04002582 RID: 9602
		private Vector3 Location;

		// Token: 0x04002583 RID: 9603
		private int Health;

		// Token: 0x04002584 RID: 9604
		private int Barrier;
	}
}
