using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using SimpleJSON;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class LocalRunRecord
{
	// Token: 0x06000B18 RID: 2840 RVA: 0x00047F75 File Offset: 0x00046175
	private LocalRunRecord()
	{
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00047FB4 File Offset: 0x000461B4
	public LocalRunRecord(bool won, List<PlayerGameStats> stats)
	{
		foreach (PlayerGameStats playerGameStats in stats)
		{
			if (!(playerGameStats.PlayerRef == null))
			{
				if (playerGameStats.PlayerRef.ViewID == PlayerControl.myInstance.ViewID)
				{
					this.MyInfo = new LocalRunRecord.PlayerInfo(playerGameStats);
				}
				else
				{
					this.OtherPlayerInfo.Add(new LocalRunRecord.PlayerInfo(playerGameStats));
				}
			}
		}
		this.Won = won;
		this.RunDate = DateTime.Now;
		this.TomeID = GameplayManager.instance.GameGraph.ID;
		this.IsChallenge = GameplayManager.IsChallengeActive;
		this.ChallengeID = (GameplayManager.IsChallengeActive ? GameplayManager.Challenge.ID : "");
		this.ChallengeBindingBoost = (GameplayManager.IsChallengeActive ? GameplayManager.Challenge.BindingBoost : 0);
		this.Timer = GameplayManager.instance.GameTime;
		this.Appendix = WaveManager.instance.AppendixLevel;
		this.Chapter = ((WaveManager.instance.AppendixLevel > 0) ? WaveManager.instance.AppendixChapterNumber : WaveManager.CurrentWave);
		this.BindingLevel = GameplayManager.BindingLevel;
		foreach (AugmentRootNode augmentRootNode in AIManager.GetEnemyAugments())
		{
			this.enemyMods.Add(augmentRootNode.guid);
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
		{
			this.fontPowers.Add(keyValuePair.Key.guid);
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair2 in GameplayManager.instance.GenreBindings.trees)
		{
			this.bindings.Add(keyValuePair2.Key.guid);
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00048238 File Offset: 0x00046438
	public LocalRunRecord(bool won, RaidDB.RaidType raid, List<PlayerGameStats> stats)
	{
		foreach (PlayerGameStats playerGameStats in stats)
		{
			if (!(playerGameStats.PlayerRef == null))
			{
				if (playerGameStats.PlayerRef.ViewID == PlayerControl.myInstance.ViewID)
				{
					this.MyInfo = new LocalRunRecord.PlayerInfo(playerGameStats);
				}
				else
				{
					this.OtherPlayerInfo.Add(new LocalRunRecord.PlayerInfo(playerGameStats));
				}
			}
		}
		this.IsRaid = true;
		this.Raid = raid;
		this.Won = won;
		this.RunDate = DateTime.Now;
		this.Timer = GameplayManager.instance.GameTime;
		this.AttemptCounts = RaidManager.instance.AttemptCounts;
		this.HardMode = (RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard);
		foreach (AugmentRootNode augmentRootNode in AIManager.GetEnemyAugments())
		{
			this.enemyMods.Add(augmentRootNode.guid);
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x000483A0 File Offset: 0x000465A0
	private LocalRunRecord(string json)
	{
		JSONNode jsonnode = JSON.Parse(json);
		string s = jsonnode.GetValueOrDefault("Date", "");
		this.RunDate = DateTime.ParseExact(s, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
		this.Timer = jsonnode.GetValueOrDefault("Timer", 0f);
		this.Won = jsonnode.GetValueOrDefault("Won", false);
		this.TomeID = jsonnode.GetValueOrDefault("TomeID", "");
		this.IsChallenge = jsonnode.GetValueOrDefault("IsChallenge", false);
		this.ChallengeID = jsonnode.GetValueOrDefault("ChallengeID", "");
		this.Chapter = jsonnode.GetValueOrDefault("Chapter", 0);
		this.Appendix = jsonnode.GetValueOrDefault("Appendix", 0);
		this.BindingLevel = jsonnode.GetValueOrDefault("BindingLevel", 0);
		this.IsRaid = jsonnode.GetValueOrDefault("IsRaid", false);
		this.Raid = (RaidDB.RaidType)jsonnode.GetValueOrDefault("Raid", 0).AsInt;
		this.HardMode = jsonnode.GetValueOrDefault("HardMode", false);
		foreach (KeyValuePair<string, JSONNode> keyValuePair in jsonnode.GetValueOrDefault("Attempts", new JSONArray()))
		{
			this.AttemptCounts.Add(keyValuePair.Value.AsInt);
		}
		foreach (KeyValuePair<string, JSONNode> keyValuePair2 in jsonnode.GetValueOrDefault("FontPowers", new JSONArray()))
		{
			this.fontPowers.Add(keyValuePair2.Value);
		}
		foreach (KeyValuePair<string, JSONNode> keyValuePair3 in jsonnode.GetValueOrDefault("EnemyMods", new JSONArray()))
		{
			this.enemyMods.Add(keyValuePair3.Value);
		}
		foreach (KeyValuePair<string, JSONNode> keyValuePair4 in jsonnode.GetValueOrDefault("Bindings", new JSONArray()))
		{
			this.bindings.Add(keyValuePair4.Value);
		}
		this.MyInfo = new LocalRunRecord.PlayerInfo(jsonnode["MyStats"]);
		foreach (KeyValuePair<string, JSONNode> aKeyValue in jsonnode.GetValueOrDefault("OtherPlayerStats", new JSONArray()))
		{
			this.OtherPlayerInfo.Add(new LocalRunRecord.PlayerInfo(aKeyValue));
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000486BC File Offset: 0x000468BC
	public List<AugmentRootNode> GetFontPowers()
	{
		List<AugmentRootNode> list = new List<AugmentRootNode>();
		foreach (string guid in this.fontPowers)
		{
			list.Add(GraphDB.GetAugment(guid));
		}
		return list;
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00048720 File Offset: 0x00046920
	public List<AugmentRootNode> GetEnemyMods()
	{
		List<AugmentRootNode> list = new List<AugmentRootNode>();
		foreach (string guid in this.enemyMods)
		{
			list.Add(GraphDB.GetAugment(guid));
		}
		return list;
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00048784 File Offset: 0x00046984
	public List<AugmentRootNode> GetBindings()
	{
		List<AugmentRootNode> list = new List<AugmentRootNode>();
		foreach (string guid in this.bindings)
		{
			list.Add(GraphDB.GetAugment(guid));
		}
		return list;
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x000487E8 File Offset: 0x000469E8
	public PlayerDB.CoreDisplay GetCoreInfo(PlayerGameStats stats)
	{
		LocalRunRecord.PlayerInfo playerInfo = this.GetPlayerInfo(stats);
		if (playerInfo == null)
		{
			return null;
		}
		return PlayerDB.GetCore(playerInfo.Color);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0004880D File Offset: 0x00046A0D
	public AbilityTree GetCoreAbility(PlayerGameStats stats)
	{
		PlayerDB.CoreDisplay coreInfo = this.GetCoreInfo(stats);
		if (coreInfo == null)
		{
			return null;
		}
		return coreInfo.Ability;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00048824 File Offset: 0x00046A24
	public AbilityTree GetPrimaryAbility(PlayerGameStats stats)
	{
		LocalRunRecord.PlayerInfo playerInfo = this.GetPlayerInfo(stats);
		if (playerInfo == null)
		{
			return null;
		}
		return GraphDB.GetAbility(playerInfo.PrimaryAbility);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0004884C File Offset: 0x00046A4C
	public AbilityTree GetSecondaryAbility(PlayerGameStats stats)
	{
		LocalRunRecord.PlayerInfo playerInfo = this.GetPlayerInfo(stats);
		if (playerInfo == null)
		{
			return null;
		}
		return GraphDB.GetAbility(playerInfo.SecondaryAbility);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00048874 File Offset: 0x00046A74
	public AbilityTree GetMovementAbility(PlayerGameStats stats)
	{
		LocalRunRecord.PlayerInfo playerInfo = this.GetPlayerInfo(stats);
		if (playerInfo == null)
		{
			return null;
		}
		return GraphDB.GetAbility(playerInfo.MovementAbility);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0004889C File Offset: 0x00046A9C
	public LocalRunRecord.PlayerInfo GetPlayerInfo(PlayerGameStats stats)
	{
		if (this.MyInfo.Stats == stats)
		{
			return this.MyInfo;
		}
		foreach (LocalRunRecord.PlayerInfo playerInfo in this.OtherPlayerInfo)
		{
			if (playerInfo.Stats == stats)
			{
				return playerInfo;
			}
		}
		return null;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x00048910 File Offset: 0x00046B10
	private string ToJSON()
	{
		JSONNode jsonnode = new JSONObject();
		jsonnode.Add("Date", this.RunDate.ToString("yyyyMMddHHmmss"));
		jsonnode.Add("Timer", (int)this.Timer);
		jsonnode.Add("Won", this.Won);
		if (this.IsRaid)
		{
			jsonnode.Add("IsRaid", this.IsRaid);
			jsonnode.Add("Raid", (int)this.Raid);
			jsonnode.Add("HardMode", this.HardMode);
			JSONArray jsonarray = new JSONArray();
			foreach (int n in this.AttemptCounts)
			{
				jsonarray.Add(n);
			}
			jsonnode.Add("Attempts", jsonarray);
		}
		else
		{
			jsonnode.Add("TomeID", this.TomeID);
			jsonnode.Add("IsChallenge", this.IsChallenge);
			jsonnode.Add("ChallengeID", this.ChallengeID);
			jsonnode.Add("Chapter", this.Chapter);
			jsonnode.Add("Appendix", this.Appendix);
			jsonnode.Add("BindingLevel", this.BindingLevel);
			JSONArray jsonarray2 = new JSONArray();
			foreach (string s in this.fontPowers)
			{
				jsonarray2.Add(s);
			}
			jsonnode.Add("FontPowers", jsonarray2);
		}
		JSONArray jsonarray3 = new JSONArray();
		foreach (string s2 in this.enemyMods)
		{
			jsonarray3.Add(s2);
		}
		jsonnode.Add("EnemyMods", jsonarray3);
		JSONArray jsonarray4 = new JSONArray();
		foreach (string s3 in this.bindings)
		{
			jsonarray4.Add(s3);
		}
		jsonnode.Add("Bindings", jsonarray4);
		jsonnode.Add("MyStats", this.MyInfo.ToJSON());
		JSONArray jsonarray5 = new JSONArray();
		foreach (LocalRunRecord.PlayerInfo playerInfo in this.OtherPlayerInfo)
		{
			jsonarray5.Add(playerInfo.ToJSON());
		}
		jsonnode.Add("OtherPlayerStats", jsonarray5);
		return jsonnode.ToString();
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00048C38 File Offset: 0x00046E38
	private void SaveToFileLocation(string ID, string json)
	{
		string str = this.RunDate.ToString("yyyyMMddHHmmss");
		string text = ID + "_" + str + ".json";
		string text2 = Path.Combine(Application.persistentDataPath, "Runs");
		try
		{
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			File.WriteAllText(Path.Combine(text2, text), json);
			if (Directory.GetFiles(text2).Length > 40)
			{
				LocalRunRecord.DeleteOldest();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to create run record [ " + text + " ]: " + ex.ToString());
		}
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00048CD8 File Offset: 0x00046ED8
	private static void DeleteOldest()
	{
		try
		{
			List<string> list = Directory.GetFiles(Application.persistentDataPath + "/Runs/").ToList<string>();
			list.Sort((string b, string a) => DateTime.Compare(File.GetCreationTime(b), File.GetCreationTime(a)));
			File.Delete(list[0]);
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to delete oldest run record: " + ex.ToString());
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00048D58 File Offset: 0x00046F58
	public static void SaveRecord(List<PlayerGameStats> stats, bool won)
	{
		if (stats == null || stats.Count <= 0)
		{
			return;
		}
		LocalRunRecord localRunRecord = new LocalRunRecord(won, stats);
		localRunRecord.SaveToFileLocation(localRunRecord.TomeID, localRunRecord.ToJSON());
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00048D8C File Offset: 0x00046F8C
	public static void SaveRaidRecord(List<PlayerGameStats> stats, bool won, RaidDB.RaidType raid)
	{
		if (stats == null || stats.Count <= 0)
		{
			return;
		}
		LocalRunRecord localRunRecord = new LocalRunRecord(won, raid, stats);
		localRunRecord.SaveToFileLocation("Raid", localRunRecord.ToJSON());
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00048DC0 File Offset: 0x00046FC0
	private static LocalRunRecord LoadRecord(string filePath)
	{
		string json = "";
		try
		{
			json = File.ReadAllText(filePath);
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to load run record from file [" + filePath + "]: " + ex.ToString());
			return null;
		}
		return new LocalRunRecord(json);
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00048E14 File Offset: 0x00047014
	public static List<LocalRunRecord> LoadRecentRuns(int count = 10)
	{
		string path = Application.persistentDataPath + "/Runs/";
		List<LocalRunRecord> list = new List<LocalRunRecord>();
		if (!Directory.Exists(path))
		{
			return list;
		}
		List<string> list2 = Directory.GetFiles(path).ToList<string>();
		list2.Sort((string a, string b) => DateTime.Compare(File.GetCreationTime(b), File.GetCreationTime(a)));
		for (int i = 0; i < Mathf.Min(list2.Count, count); i++)
		{
			try
			{
				LocalRunRecord localRunRecord = LocalRunRecord.LoadRecord(list2[i]);
				if (localRunRecord != null)
				{
					list.Add(localRunRecord);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to load run record from file [" + list2[i] + "]: " + ex.ToString());
			}
		}
		return list;
	}

	// Token: 0x0400092F RID: 2351
	public DateTime RunDate;

	// Token: 0x04000930 RID: 2352
	public readonly string TomeID;

	// Token: 0x04000931 RID: 2353
	public readonly bool Won;

	// Token: 0x04000932 RID: 2354
	public readonly float Timer;

	// Token: 0x04000933 RID: 2355
	public readonly int Chapter;

	// Token: 0x04000934 RID: 2356
	public readonly int Appendix;

	// Token: 0x04000935 RID: 2357
	public readonly int BindingLevel;

	// Token: 0x04000936 RID: 2358
	public readonly bool IsChallenge;

	// Token: 0x04000937 RID: 2359
	public readonly string ChallengeID;

	// Token: 0x04000938 RID: 2360
	public readonly int ChallengeBindingBoost;

	// Token: 0x04000939 RID: 2361
	public readonly bool IsRaid;

	// Token: 0x0400093A RID: 2362
	public readonly bool HardMode;

	// Token: 0x0400093B RID: 2363
	public readonly RaidDB.RaidType Raid;

	// Token: 0x0400093C RID: 2364
	public readonly List<int> AttemptCounts = new List<int>();

	// Token: 0x0400093D RID: 2365
	private readonly List<string> fontPowers = new List<string>();

	// Token: 0x0400093E RID: 2366
	private readonly List<string> enemyMods = new List<string>();

	// Token: 0x0400093F RID: 2367
	private readonly List<string> bindings = new List<string>();

	// Token: 0x04000940 RID: 2368
	public readonly LocalRunRecord.PlayerInfo MyInfo;

	// Token: 0x04000941 RID: 2369
	public readonly List<LocalRunRecord.PlayerInfo> OtherPlayerInfo = new List<LocalRunRecord.PlayerInfo>();

	// Token: 0x020004E9 RID: 1257
	[Serializable]
	public class PlayerInfo
	{
		// Token: 0x0600232A RID: 9002 RVA: 0x000C87F4 File Offset: 0x000C69F4
		public PlayerInfo(PlayerGameStats stats)
		{
			PlayerControl playerRef = stats.PlayerRef;
			if (playerRef == null)
			{
				Debug.LogError("PlayerInfo Creation from GameStats: Player is null");
				return;
			}
			this.Color = playerRef.actions.core.Root.magicColor;
			this.PrimaryAbility = playerRef.actions.primary.ID;
			this.SecondaryAbility = playerRef.actions.secondary.ID;
			this.MovementAbility = playerRef.actions.movement.ID;
			this.Username = playerRef.GetUsernameText();
			this.Augments = playerRef.Augment.TreeIDs.ToList<string>();
			this.Stats = stats;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000C88B4 File Offset: 0x000C6AB4
		public PlayerInfo(JSONNode n)
		{
			this.Username = n.GetValueOrDefault("Username", "");
			this.Color = (MagicColor)n.GetValueOrDefault("Color", 0);
			this.PrimaryAbility = n.GetValueOrDefault("PrimaryAbility", "");
			this.SecondaryAbility = n.GetValueOrDefault("SecondaryAbility", "");
			this.MovementAbility = n.GetValueOrDefault("MovementAbility", "");
			foreach (KeyValuePair<string, JSONNode> keyValuePair in n.GetValueOrDefault("Augments", new JSONArray()))
			{
				this.Augments.Add(keyValuePair.Value);
			}
			if (n.HasKey("Stats"))
			{
				this.Stats = new PlayerGameStats(n["Stats"]);
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000C89D4 File Offset: 0x000C6BD4
		public List<AugmentRootNode> GetAugments()
		{
			List<AugmentRootNode> list = new List<AugmentRootNode>();
			foreach (string guid in this.Augments)
			{
				list.Add(GraphDB.GetAugment(guid));
			}
			return list;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000C8A38 File Offset: 0x000C6C38
		public JSONNode ToJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("Username", this.Username);
			jsonnode.Add("Color", (int)this.Color);
			jsonnode.Add("PrimaryAbility", this.PrimaryAbility);
			jsonnode.Add("SecondaryAbility", this.SecondaryAbility);
			jsonnode.Add("MovementAbility", this.MovementAbility);
			JSONArray jsonarray = new JSONArray();
			foreach (string s in this.Augments)
			{
				jsonarray.Add(s);
			}
			jsonnode.Add("Augments", jsonarray);
			jsonnode.Add("Stats", this.Stats.ToJSON());
			return jsonnode;
		}

		// Token: 0x040024F9 RID: 9465
		public string Username;

		// Token: 0x040024FA RID: 9466
		public MagicColor Color;

		// Token: 0x040024FB RID: 9467
		public string PrimaryAbility;

		// Token: 0x040024FC RID: 9468
		public string SecondaryAbility;

		// Token: 0x040024FD RID: 9469
		public string MovementAbility;

		// Token: 0x040024FE RID: 9470
		public List<string> Augments = new List<string>();

		// Token: 0x040024FF RID: 9471
		public PlayerGameStats Stats;
	}

	// Token: 0x020004EA RID: 1258
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600232E RID: 9006 RVA: 0x000C8B30 File Offset: 0x000C6D30
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000C8B3C File Offset: 0x000C6D3C
		public <>c()
		{
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000C8B44 File Offset: 0x000C6D44
		internal int <DeleteOldest>b__34_0(string b, string a)
		{
			return DateTime.Compare(File.GetCreationTime(b), File.GetCreationTime(a));
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000C8B57 File Offset: 0x000C6D57
		internal int <LoadRecentRuns>b__38_0(string a, string b)
		{
			return DateTime.Compare(File.GetCreationTime(b), File.GetCreationTime(a));
		}

		// Token: 0x04002500 RID: 9472
		public static readonly LocalRunRecord.<>c <>9 = new LocalRunRecord.<>c();

		// Token: 0x04002501 RID: 9473
		public static Comparison<string> <>9__34_0;

		// Token: 0x04002502 RID: 9474
		public static Comparison<string> <>9__38_0;
	}
}
