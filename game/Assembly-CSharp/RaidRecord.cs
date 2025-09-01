using System;
using System.Collections.Generic;
using Photon.Pun;
using SimpleJSON;

// Token: 0x02000107 RID: 263
public class RaidRecord
{
	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0004FA42 File Offset: 0x0004DC42
	private static RaidRecord Current
	{
		get
		{
			if (RaidRecord._current == null)
			{
				RaidRecord.Reset();
			}
			return RaidRecord._current;
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0004FA55 File Offset: 0x0004DC55
	public static void NewRecord(string encounter, bool hardMode)
	{
		RaidRecord.Reset();
		RaidRecord.Current.Encounter = encounter;
		RaidRecord.Current.HardMode = hardMode;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0004FA72 File Offset: 0x0004DC72
	public static void NextAttempt(float lastAttemptDuration)
	{
		RaidRecord.Current.totalDuration += (int)lastAttemptDuration;
		RaidRecord.Current.Attempts++;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0004FA99 File Offset: 0x0004DC99
	public static void AddDeathReason(string reason)
	{
		RaidRecord.Current.DeathsTo.Add(reason);
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0004FAAB File Offset: 0x0004DCAB
	public static void PageSelected(string page)
	{
		RaidRecord.Current.EncounterPage = page;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0004FAB8 File Offset: 0x0004DCB8
	public static void UploadResult(RaidRecord.Result result, float attemptDuration)
	{
		if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode && PlayerControl.AllPlayers.Count > 1 && result != RaidRecord.Result.Quit)
		{
			return;
		}
		RaidRecord.Current.EndResult = result;
		RaidRecord.Current.totalDuration += (int)attemptDuration;
		ParseManager.UploadRaidEncounter(NetworkManager.GetVersionCode(), RaidRecord.Current, PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount : 1, RaidRecord.GetCurrentPlayerData(), RaidRecord.GetEncounterData((int)attemptDuration));
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0004FB34 File Offset: 0x0004DD34
	private static string GetEncounterData(int lastAttemptDuration)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("enemyPage", RaidRecord.Current.EncounterPage);
		jsonobject.Add("attempts", RaidRecord.Current.Attempts);
		jsonobject.Add("totalTime", RaidRecord.Current.totalDuration);
		jsonobject.Add("finalTime", lastAttemptDuration);
		return jsonobject.ToString();
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0004FBAA File Offset: 0x0004DDAA
	private static string GetCurrentPlayerData()
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("players", RaidRecord.GetPlayerData());
		return jsonobject.ToString();
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
	private static JSONArray GetPlayerData()
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

	// Token: 0x06000C5A RID: 3162 RVA: 0x0004FDE0 File Offset: 0x0004DFE0
	private static void Reset()
	{
		RaidRecord._current = new RaidRecord();
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0004FDEC File Offset: 0x0004DFEC
	public RaidRecord()
	{
	}

	// Token: 0x040009E8 RID: 2536
	private static RaidRecord _current;

	// Token: 0x040009E9 RID: 2537
	public string Encounter;

	// Token: 0x040009EA RID: 2538
	public RaidRecord.Result EndResult;

	// Token: 0x040009EB RID: 2539
	public bool HardMode;

	// Token: 0x040009EC RID: 2540
	public List<string> DeathsTo = new List<string>();

	// Token: 0x040009ED RID: 2541
	private string EncounterPage = "";

	// Token: 0x040009EE RID: 2542
	private int totalDuration;

	// Token: 0x040009EF RID: 2543
	private int Attempts;

	// Token: 0x02000503 RID: 1283
	public enum Result
	{
		// Token: 0x04002586 RID: 9606
		Won,
		// Token: 0x04002587 RID: 9607
		Lost,
		// Token: 0x04002588 RID: 9608
		Quit
	}
}
