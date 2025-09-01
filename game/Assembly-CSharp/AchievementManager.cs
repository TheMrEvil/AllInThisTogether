using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x020000EE RID: 238
public static class AchievementManager
{
	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00046570 File Offset: 0x00044770
	// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00046583 File Offset: 0x00044783
	public static bool Initialized
	{
		get
		{
			if (!AchievementManager._initialized)
			{
				AchievementManager.Initialize();
			}
			return AchievementManager._initialized;
		}
		set
		{
			AchievementManager._initialized = value;
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0004658C File Offset: 0x0004478C
	private static void Initialize()
	{
		if (!UnlockManager.Initialized)
		{
			return;
		}
		AchievementManager.AchievementUnlocked = null;
		try
		{
			AchievementManager.UnlockedAchievements = new HashSet<string>();
			AchievementManager.UnlockedAchievements = ES3.Load<HashSet<string>>("Achievements", "unlocks.vel", new HashSet<string>());
			AchievementManager.ClaimedAchievements = ES3.Load<HashSet<string>>("ClaimedAchs", "unlocks.vel", new HashSet<string>());
		}
		catch (Exception)
		{
		}
		AchievementManager._initialized = true;
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00046600 File Offset: 0x00044800
	public static AchievementRootNode GetAchievement(string ID)
	{
		return GraphDB.GetAchievement(ID);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00046608 File Offset: 0x00044808
	public static void CheckInitialAchievements()
	{
		foreach (string achievementID in AchievementManager.UnlockedAchievements)
		{
			AchievementManager.UnlockPlatform(achievementID);
		}
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00046658 File Offset: 0x00044858
	public static bool HasAchievementWithTrigger(EventTrigger trigger)
	{
		return GraphDB.HasAchievementWithTrigger(trigger);
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00046660 File Offset: 0x00044860
	public static void UnlockEvent(EventTrigger trigger, EffectProperties props)
	{
		if (!UnlockManager.Initialized || !AchievementManager.Initialized)
		{
			return;
		}
		foreach (AchievementRootNode achievementRootNode in GraphDB.GetAchievements(trigger))
		{
			try
			{
				if (!string.IsNullOrEmpty(achievementRootNode.ID))
				{
					if (achievementRootNode.TryUnlock(trigger, props))
					{
						AchievementManager.Unlock(achievementRootNode.ID);
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x000466F8 File Offset: 0x000448F8
	public static void Unlock(string achievementID)
	{
		if (AchievementManager.IsUnlocked(achievementID))
		{
			return;
		}
		Debug.Log("Achievement Completed - " + achievementID);
		AchievementManager.UnlockPlatform(achievementID);
		AchievementRootNode achievement = AchievementManager.GetAchievement(achievementID);
		if (achievement == null || !achievement.RequiresClaim)
		{
			UnlockManager.AchievementUnlocked(achievementID);
			if (achievement.RewardsCurrency)
			{
				Currency.AddLoadoutCoin(achievement.Quillmarks, true);
				Currency.Add(achievement.Gildings, true);
			}
		}
		AchievementManager.UnlockedAchievements.Add(achievementID);
		ES3.Save<HashSet<string>>("Achievements", AchievementManager.UnlockedAchievements, "unlocks.vel");
		Action<string> achievementUnlocked = AchievementManager.AchievementUnlocked;
		if (achievementUnlocked == null)
		{
			return;
		}
		achievementUnlocked(achievementID);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00046794 File Offset: 0x00044994
	public static bool ClaimAchievement(string achievementID)
	{
		if (!AchievementManager.IsUnlocked(achievementID))
		{
			return false;
		}
		if (AchievementManager.IsClaimed(achievementID))
		{
			return false;
		}
		AchievementRootNode achievement = AchievementManager.GetAchievement(achievementID);
		if (achievement == null || !achievement.RequiresClaim)
		{
			return false;
		}
		AchievementManager.ClaimedAchievements.Add(achievementID);
		ES3.Save<HashSet<string>>("ClaimedAchs", AchievementManager.ClaimedAchievements, "unlocks.vel");
		UnlockManager.AchievementUnlocked(achievementID);
		if (achievement.RewardsCurrency)
		{
			Currency.AddLoadoutCoin(achievement.Quillmarks, true);
			if (achievement.Quillmarks > 0)
			{
				LibraryInfoWidget.QuillmarksGained(achievement.Quillmarks);
			}
			Currency.Add(achievement.Gildings, true);
			if (achievement.Gildings > 0)
			{
				LibraryInfoWidget.GildingsGained(achievement.Gildings);
			}
		}
		return true;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00046840 File Offset: 0x00044A40
	public static void ResetForPrestige()
	{
		List<string> list = new List<string>();
		foreach (string text in AchievementManager.UnlockedAchievements)
		{
			AchievementRootNode achievement = GraphDB.GetAchievement(text);
			if (!(achievement != null) || achievement.ResetWithPrestige)
			{
				list.Add(text);
			}
		}
		foreach (string item in list)
		{
			AchievementManager.UnlockedAchievements.Remove(item);
			AchievementManager.ClaimedAchievements.Remove(item);
		}
		ES3.Save<HashSet<string>>("Achievements", AchievementManager.UnlockedAchievements, "unlocks.vel");
		ES3.Save<HashSet<string>>("ClaimedAchs", AchievementManager.ClaimedAchievements, "unlocks.vel");
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0004692C File Offset: 0x00044B2C
	private static void UnlockPlatform(string achievementID)
	{
		AchievementManager.UnlockSteamAchievement(achievementID);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00046934 File Offset: 0x00044B34
	public static bool IsUnlocked(string achievementID)
	{
		return AchievementManager.UnlockedAchievements.Contains(achievementID);
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00046941 File Offset: 0x00044B41
	public static bool IsClaimed(string achievementID)
	{
		return AchievementManager.ClaimedAchievements.Contains(achievementID);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00046950 File Offset: 0x00044B50
	private static void UnlockSteamAchievement(string id)
	{
		try
		{
			if (PlatformSetup.IsSteamInitialized)
			{
				bool flag;
				if (SteamUserStats.GetAchievement(id, out flag))
				{
					if (!flag)
					{
						SteamUserStats.SetAchievement(id);
						SteamUserStats.StoreStats();
					}
				}
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x0400091A RID: 2330
	public static Action<string> AchievementUnlocked;

	// Token: 0x0400091B RID: 2331
	public static HashSet<string> UnlockedAchievements;

	// Token: 0x0400091C RID: 2332
	public static HashSet<string> ClaimedAchievements;

	// Token: 0x0400091D RID: 2333
	private static bool _initialized;
}
