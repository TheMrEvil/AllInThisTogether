using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200023E RID: 574
public static class Settings
{
	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06001768 RID: 5992 RVA: 0x00093991 File Offset: 0x00091B91
	public static bool LowRez
	{
		get
		{
			return Screen.height <= 804;
		}
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000939A2 File Offset: 0x00091BA2
	public static void Reset()
	{
		Settings.Initialized = false;
		Settings.OnGameplaySettingsChanged = null;
		Settings.OnSystemSettingsChanged = null;
		Settings.SystemSettings = new Dictionary<SystemSetting, object>();
		Settings.GameplaySettings = new Dictionary<GameSetting, object>();
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000939CA File Offset: 0x00091BCA
	public static void ResetForPrestige(bool resetTutorials)
	{
		Settings.SavedTalents = Progression.TalentBuild.ToString();
		Settings.PrevBindings = "";
		if (resetTutorials)
		{
			Settings.UITutorialsCompleted.Clear();
			Settings.LibraryTutorialsCompleted.Clear();
		}
		Settings.SaveSettings();
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x00093A04 File Offset: 0x00091C04
	public static void LoadSettings()
	{
		if (Settings.Initialized)
		{
			return;
		}
		Settings.SystemSettings.Clear();
		try
		{
			ES3.CacheFile("settings.vel");
			ES3Settings settings = new ES3Settings("settings.vel", new Enum[]
			{
				ES3.Location.Cache
			});
			Settings.SystemSettings = ES3.Load<Dictionary<SystemSetting, object>>("System", new Dictionary<SystemSetting, object>(), settings);
			Settings.GameplaySettings = ES3.Load<Dictionary<GameSetting, object>>("Gameplay", new Dictionary<GameSetting, object>(), settings);
			Settings.LibraryTutorialsCompleted = ES3.Load<List<LibraryTutorial>>("LibraryTuts", new List<LibraryTutorial>(), settings);
			Settings.UITutorialsCompleted = ES3.Load<List<UITutorial.Tutorial>>("UITuts", new List<UITutorial.Tutorial>(), settings);
			Settings.SavedLoadout = ES3.Load<string>("Loadout", "", settings);
			Settings.SavedOutfit = ES3.Load<string>("Outfit", "", settings);
			Settings.SavedEmotes = ES3.Load<string>("Emotes", "", settings);
			Settings.SavedTalents = ES3.Load<string>("Talents", "", settings);
			Settings.NookLayout = ES3.Load<string>("NookLayout", "", settings);
			Settings.SavedLibTalents = ES3.Load<string>("LibTalents", "", settings);
			Settings.CurFullLoadout = ES3.Load<int>("CurFull", -1, settings);
			Settings.FullLoadouts = ES3.Load<List<string>>("FullLoadouts", new List<string>(), settings);
			Settings.PrevBindings = ES3.Load<string>("PrevBinds", "", settings);
			Settings.DoneFTUX = ES3.Load<bool>("DoneFTUX", false, settings);
			Vector2 vector = ES3.Load<Vector2>("Resolution", Vector2.zero, settings);
			ResolutionSelector.SetupFromSave((int)vector.x, (int)vector.y);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		Settings.Initialized = true;
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x00093BB4 File Offset: 0x00091DB4
	private static void SaveSettings()
	{
		ES3Settings settings = new ES3Settings("settings.vel", null);
		bool flag = false;
		try
		{
			ES3.CacheFile("settings.vel");
			settings = new ES3Settings("settings.vel", new Enum[]
			{
				ES3.Location.Cache
			});
		}
		catch (Exception)
		{
			ES3.DeleteFile("settings.vel");
			flag = true;
		}
		ES3.Save<Dictionary<SystemSetting, object>>("System", Settings.SystemSettings, settings);
		ES3.Save<Dictionary<GameSetting, object>>("Gameplay", Settings.GameplaySettings, settings);
		ES3.Save<string>("Loadout", Settings.SavedLoadout, settings);
		ES3.Save<string>("Outfit", Settings.SavedOutfit, settings);
		ES3.Save<string>("Emotes", Settings.SavedEmotes, settings);
		ES3.Save<string>("Talents", Settings.SavedTalents, settings);
		ES3.Save<string>("NookLayout", Settings.NookLayout, settings);
		ES3.Save<string>("LibTalents", Settings.SavedLibTalents, settings);
		ES3.Save<int>("CurFull", Settings.CurFullLoadout, settings);
		ES3.Save<List<string>>("FullLoadouts", Settings.FullLoadouts, settings);
		ES3.Save<string>("PrevBinds", Settings.PrevBindings, settings);
		ES3.Save<bool>("DoneFTUX", Settings.DoneFTUX, settings);
		ES3.Save<List<LibraryTutorial>>("LibraryTuts", Settings.LibraryTutorialsCompleted, settings);
		ES3.Save<List<UITutorial.Tutorial>>("UITuts", Settings.UITutorialsCompleted, settings);
		if (!flag)
		{
			ES3.StoreCachedFile("settings.vel");
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x00093D04 File Offset: 0x00091F04
	public static void SaveResolutionExplicit(Vector2 res)
	{
		res.x = (float)((int)res.x);
		res.y = (float)((int)res.y);
		ES3.Save<Vector2>("Resolution", res, "settings.vel");
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x00093D34 File Offset: 0x00091F34
	public static void SetInt(SystemSetting setting, int value)
	{
		if (Settings.SystemSettings.ContainsKey(setting))
		{
			Settings.SystemSettings[setting] = value;
		}
		else
		{
			Settings.SystemSettings.Add(setting, value);
		}
		Action<SystemSetting> onSystemSettingsChanged = Settings.OnSystemSettingsChanged;
		if (onSystemSettingsChanged != null)
		{
			onSystemSettingsChanged(setting);
		}
		Settings.SaveSettings();
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x00093D88 File Offset: 0x00091F88
	public static void SetFloat(SystemSetting setting, float value)
	{
		if (Settings.SystemSettings.ContainsKey(setting))
		{
			Settings.SystemSettings[setting] = value;
		}
		else
		{
			Settings.SystemSettings.Add(setting, value);
		}
		Action<SystemSetting> onSystemSettingsChanged = Settings.OnSystemSettingsChanged;
		if (onSystemSettingsChanged != null)
		{
			onSystemSettingsChanged(setting);
		}
		Settings.SaveSettings();
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x00093DDC File Offset: 0x00091FDC
	public static void SetBool(SystemSetting setting, bool value)
	{
		if (Settings.SystemSettings.ContainsKey(setting))
		{
			Settings.SystemSettings[setting] = value;
		}
		else
		{
			Settings.SystemSettings.Add(setting, value);
		}
		Action<SystemSetting> onSystemSettingsChanged = Settings.OnSystemSettingsChanged;
		if (onSystemSettingsChanged != null)
		{
			onSystemSettingsChanged(setting);
		}
		Settings.SaveSettings();
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x00093E30 File Offset: 0x00092030
	public static float GetFloat(SystemSetting setting, float defaultValue = 0f)
	{
		if (Settings.SystemSettings.ContainsKey(setting))
		{
			object obj = Settings.SystemSettings[setting];
			if (obj is int)
			{
				return (float)((int)Settings.SystemSettings[setting]);
			}
			if (obj is float)
			{
				return (float)Settings.SystemSettings[setting];
			}
		}
		return defaultValue;
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x00093E8C File Offset: 0x0009208C
	public static int GetInt(SystemSetting setting, int defaultValue = 0)
	{
		if (Settings.SystemSettings.ContainsKey(setting) && Settings.SystemSettings[setting] is int)
		{
			return (int)Settings.SystemSettings[setting];
		}
		return defaultValue;
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x00093EBF File Offset: 0x000920BF
	public static bool GetBool(SystemSetting setting, bool defaultValue = false)
	{
		if (Settings.SystemSettings.ContainsKey(setting) && Settings.SystemSettings[setting] is bool)
		{
			return (bool)Settings.SystemSettings[setting];
		}
		return defaultValue;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x00093EF2 File Offset: 0x000920F2
	public static void SetInt(GameSetting setting, int value)
	{
		if (Settings.GameplaySettings.ContainsKey(setting))
		{
			Settings.GameplaySettings[setting] = value;
		}
		else
		{
			Settings.GameplaySettings.Add(setting, value);
		}
		Settings.SaveSettings();
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x00093F2A File Offset: 0x0009212A
	public static void SetFloat(GameSetting setting, float value)
	{
		if (Settings.GameplaySettings.ContainsKey(setting))
		{
			Settings.GameplaySettings[setting] = value;
		}
		else
		{
			Settings.GameplaySettings.Add(setting, value);
		}
		Settings.SaveSettings();
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x00093F62 File Offset: 0x00092162
	public static void SetBool(GameSetting setting, bool value)
	{
		if (Settings.GameplaySettings.ContainsKey(setting))
		{
			Settings.GameplaySettings[setting] = value;
		}
		else
		{
			Settings.GameplaySettings.Add(setting, value);
		}
		Settings.SaveSettings();
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x00093F9C File Offset: 0x0009219C
	public static float GetFloat(GameSetting setting, float defaultValue = 0f)
	{
		if (Settings.GameplaySettings.ContainsKey(setting))
		{
			object obj = Settings.GameplaySettings[setting];
			if (obj is int)
			{
				return (float)((int)Settings.GameplaySettings[setting]);
			}
			if (obj is float)
			{
				return (float)Settings.GameplaySettings[setting];
			}
		}
		return defaultValue;
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x00093FF8 File Offset: 0x000921F8
	public static int GetInt(GameSetting setting, int defaultValue = 0)
	{
		if (Settings.GameplaySettings.ContainsKey(setting) && Settings.GameplaySettings[setting] is int)
		{
			return (int)Settings.GameplaySettings[setting];
		}
		return defaultValue;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x0009402B File Offset: 0x0009222B
	public static bool GetBool(GameSetting setting, bool defaultValue = false)
	{
		if (Settings.GameplaySettings.ContainsKey(setting) && Settings.GameplaySettings[setting] is bool)
		{
			return (bool)Settings.GameplaySettings[setting];
		}
		return defaultValue;
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x0009405E File Offset: 0x0009225E
	public static void DoneTutorial()
	{
		if (Settings.DoneFTUX)
		{
			return;
		}
		Settings.DoneFTUX = true;
		Settings.SaveSettings();
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x00094073 File Offset: 0x00092273
	public static void ResetTutorial()
	{
		Settings.DoneFTUX = false;
		Settings.LibraryTutorialsCompleted = new List<LibraryTutorial>();
		Settings.UITutorialsCompleted = new List<UITutorial.Tutorial>();
		Settings.SaveSettings();
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x00094094 File Offset: 0x00092294
	public static void SaveLoadout()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		Settings.SavedLoadout = new Progression.Loadout("")
		{
			Core = PlayerControl.myInstance.actions.core,
			Generator = PlayerControl.myInstance.actions.primary,
			Spender = PlayerControl.myInstance.actions.secondary,
			Movement = PlayerControl.myInstance.actions.movement
		}.ToString();
		Settings.SaveSettings();
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x0009411C File Offset: 0x0009231C
	public static Progression.Loadout GetLoadout()
	{
		return new Progression.Loadout(Settings.SavedLoadout);
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x00094128 File Offset: 0x00092328
	public static void SaveOutfit()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		Settings.SavedOutfit = PlayerControl.myInstance.Display.CurSet.ToString();
		Settings.SaveSettings();
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x00094156 File Offset: 0x00092356
	public static CosmeticSet GetOutfit()
	{
		return new CosmeticSet(Settings.SavedOutfit);
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x00094162 File Offset: 0x00092362
	public static void SaveEmotes(string top, string bottom, string left, string right)
	{
		Settings.SavedEmotes = string.Concat(new string[]
		{
			top,
			"|",
			bottom,
			"|",
			left,
			"|",
			right
		});
		Settings.SaveSettings();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000941A4 File Offset: 0x000923A4
	public static List<string> GetEmotes()
	{
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(Settings.SavedEmotes))
		{
			foreach (string item in Settings.SavedEmotes.Split('|', StringSplitOptions.None))
			{
				list.Add(item);
			}
		}
		for (int j = list.Count; j < 4; j++)
		{
			list.Add("");
		}
		return list;
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x0009420B File Offset: 0x0009240B
	public static void SaveTalentBuild()
	{
		Settings.SavedTalents = Progression.TalentBuild.ToString();
		Settings.SaveSettings();
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x00094221 File Offset: 0x00092421
	public static void SaveLibraryBuild()
	{
		Settings.SavedLibTalents = Progression.LibraryBuild.ToString();
		Settings.SaveSettings();
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x00094237 File Offset: 0x00092437
	public static Progression.EquippedTalents GetTalentBuild()
	{
		return new Progression.EquippedTalents(Settings.SavedTalents);
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x00094243 File Offset: 0x00092443
	public static Progression.LibraryLoadout GetLibTalentBuild()
	{
		return new Progression.LibraryLoadout(Settings.SavedLibTalents);
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x0009424F File Offset: 0x0009244F
	public static void SaveNookLayout(string layout)
	{
		Settings.NookLayout = layout;
		Settings.SaveSettings();
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x0009425C File Offset: 0x0009245C
	public static string GetNookLayout()
	{
		return Settings.NookLayout;
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x00094264 File Offset: 0x00092464
	public static List<Progression.FullLoadout> GetFullLoadouts()
	{
		if (Settings.FullLoadouts == null)
		{
			Settings.FullLoadouts = new List<string>();
		}
		List<Progression.FullLoadout> list = new List<Progression.FullLoadout>();
		foreach (string input in Settings.FullLoadouts)
		{
			list.Add(new Progression.FullLoadout(input));
		}
		return list;
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000942D4 File Offset: 0x000924D4
	public static void SaveNewFullLoadout(Progression.FullLoadout loadout)
	{
		Settings.FullLoadouts.Add(loadout.ToString());
		Settings.CurFullLoadout = Settings.FullLoadouts.Count - 1;
		Settings.SaveSettings();
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000942FC File Offset: 0x000924FC
	public static void DeleteLoadout(int index)
	{
		if (index == Settings.CurFullLoadout)
		{
			Settings.CurFullLoadout = -1;
		}
		else if (index < Settings.CurFullLoadout)
		{
			Settings.CurFullLoadout--;
		}
		Settings.FullLoadouts.RemoveAt(index);
		Settings.SaveSettings();
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x00094332 File Offset: 0x00092532
	public static void UpdateEquippedFullLoadout(int index)
	{
		if (index >= Settings.FullLoadouts.Count)
		{
			return;
		}
		if (index >= 0)
		{
			new Progression.FullLoadout(Settings.FullLoadouts[index]).SetEquipped();
		}
		Settings.CurFullLoadout = index;
		Settings.SaveLoadout();
		Settings.SaveTalentBuild();
		Settings.SaveOutfit();
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x00094370 File Offset: 0x00092570
	public static Progression.FullLoadout ModifyFullLoadout(Progression.FullLoadout loadout, int index, bool includeName = false)
	{
		if (index < 0 || index >= Settings.FullLoadouts.Count)
		{
			return null;
		}
		Progression.FullLoadout fullLoadout = new Progression.FullLoadout(Settings.FullLoadouts[index]);
		fullLoadout.UpdateData(loadout, includeName);
		Settings.FullLoadouts[index] = fullLoadout.ToString();
		Settings.SaveSettings();
		return fullLoadout;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000943C0 File Offset: 0x000925C0
	public static void SaveBindingLoadout(string str)
	{
		Settings.PrevBindings = str;
		Settings.SaveSettings();
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000943CD File Offset: 0x000925CD
	public static bool HasCompletedLibraryTutorial(LibraryTutorial step)
	{
		return Settings.LibraryTutorialsCompleted.Contains(step);
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000943DA File Offset: 0x000925DA
	public static void LibraryTutorialCompleted(LibraryTutorial step)
	{
		if (Settings.LibraryTutorialsCompleted.Contains(step))
		{
			return;
		}
		Settings.LibraryTutorialsCompleted.Add(step);
		Settings.SaveSettings();
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000943FA File Offset: 0x000925FA
	public static bool HasCompletedUITutorial(UITutorial.Tutorial step)
	{
		return Settings.UITutorialsCompleted.Contains(step);
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x00094407 File Offset: 0x00092607
	public static void UITutorialCompleted(UITutorial.Tutorial step)
	{
		if (Settings.UITutorialsCompleted.Contains(step))
		{
			return;
		}
		Settings.UITutorialsCompleted.Add(step);
		Settings.SaveSettings();
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x00094428 File Offset: 0x00092628
	public static bool HasBackupAvailable()
	{
		string path = Path.Combine(Application.persistentDataPath, Settings.BACKUP_FOLDER);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
			return false;
		}
		List<string> list = Directory.GetDirectories(path).ToList<string>();
		if (list.Count <= 0)
		{
			return false;
		}
		if (list.Count > 20)
		{
			Settings.DeleteOldest();
		}
		return true;
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x00094480 File Offset: 0x00092680
	public static DateTime GetLatestBackupTime()
	{
		if (!Settings.HasBackupAvailable())
		{
			return DateTime.Now;
		}
		List<string> list = Directory.GetDirectories(Path.Combine(Application.persistentDataPath, Settings.BACKUP_FOLDER)).ToList<string>();
		list.Sort((string a, string b) => DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a)));
		string str = list[0];
		Debug.Log("Latest backup folder: " + str);
		return DateTime.Now;
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000944F4 File Offset: 0x000926F4
	public static bool LoadLatestBackup(string fileName)
	{
		if (!Settings.HasBackupAvailable())
		{
			return false;
		}
		List<string> list = Directory.GetDirectories(Path.Combine(Application.persistentDataPath, Settings.BACKUP_FOLDER)).ToList<string>();
		list.Sort((string a, string b) => DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a)));
		string text = list[0];
		Debug.Log("Loading backup from folder:" + text);
		string text2 = Path.Combine(text, fileName);
		if (!File.Exists(text2))
		{
			Debug.LogError("Backup file not found: " + text2);
			return false;
		}
		string destFileName = Path.Combine(Application.persistentDataPath, fileName);
		File.Copy(text2, destFileName, true);
		return true;
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x00094598 File Offset: 0x00092798
	public static void SaveInBackup(string fileName)
	{
		string text = Path.Combine(Application.persistentDataPath, Settings.BACKUP_FOLDER);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		try
		{
			string text2 = Path.Combine(Application.persistentDataPath, fileName);
			if (File.Exists(text2))
			{
				string path = DateTime.Now.ToString("yyyyMMddHHmm");
				string text3 = Path.Combine(text, path);
				if (!Directory.Exists(text3))
				{
					Directory.CreateDirectory(text3);
				}
				File.Copy(text2, Path.Combine(text3, fileName), true);
				Debug.Log("Saved backup to " + text3);
			}
			else
			{
				Debug.LogError("Source file not found for backup: " + text2);
			}
		}
		catch (Exception ex)
		{
			string str = "Failed to save backup folder [ ";
			string str2 = " ]: ";
			Exception ex2 = ex;
			Debug.LogError(str + fileName + str2 + ((ex2 != null) ? ex2.ToString() : null));
			throw;
		}
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x00094670 File Offset: 0x00092870
	private static void DeleteOldest()
	{
		List<string> list = Directory.GetDirectories(Path.Combine(Application.persistentDataPath, Settings.BACKUP_FOLDER)).ToList<string>();
		list.Sort((string b, string a) => DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a)));
		Directory.Delete(list[0], true);
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000946C7 File Offset: 0x000928C7
	// Note: this type is marked as 'beforefieldinit'.
	static Settings()
	{
	}

	// Token: 0x0400171D RID: 5917
	public static string BACKUP_FOLDER = "Save_Snapshots";

	// Token: 0x0400171E RID: 5918
	private const string SAVE_FILE = "settings.vel";

	// Token: 0x0400171F RID: 5919
	private static bool Initialized = false;

	// Token: 0x04001720 RID: 5920
	private static Dictionary<SystemSetting, object> SystemSettings = new Dictionary<SystemSetting, object>();

	// Token: 0x04001721 RID: 5921
	public static Action<SystemSetting> OnSystemSettingsChanged;

	// Token: 0x04001722 RID: 5922
	private static Dictionary<GameSetting, object> GameplaySettings = new Dictionary<GameSetting, object>();

	// Token: 0x04001723 RID: 5923
	public static Action<GameSetting> OnGameplaySettingsChanged;

	// Token: 0x04001724 RID: 5924
	public static List<LibraryTutorial> LibraryTutorialsCompleted = new List<LibraryTutorial>();

	// Token: 0x04001725 RID: 5925
	public static List<UITutorial.Tutorial> UITutorialsCompleted = new List<UITutorial.Tutorial>();

	// Token: 0x04001726 RID: 5926
	private static string SavedLoadout;

	// Token: 0x04001727 RID: 5927
	private static string SavedOutfit;

	// Token: 0x04001728 RID: 5928
	private static string SavedTalents;

	// Token: 0x04001729 RID: 5929
	private static string SavedLibTalents;

	// Token: 0x0400172A RID: 5930
	private static string SavedEmotes;

	// Token: 0x0400172B RID: 5931
	private static string NookLayout;

	// Token: 0x0400172C RID: 5932
	public static int CurFullLoadout = -1;

	// Token: 0x0400172D RID: 5933
	public static List<string> FullLoadouts;

	// Token: 0x0400172E RID: 5934
	public static string PrevBindings;

	// Token: 0x0400172F RID: 5935
	public static bool DoneFTUX;

	// Token: 0x02000602 RID: 1538
	public enum SettingTypes
	{
		// Token: 0x0400297C RID: 10620
		System,
		// Token: 0x0400297D RID: 10621
		Gameplay
	}

	// Token: 0x02000603 RID: 1539
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x000D436F File Offset: 0x000D256F
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000D437B File Offset: 0x000D257B
		public <>c()
		{
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000D4383 File Offset: 0x000D2583
		internal int <GetLatestBackupTime>b__64_0(string a, string b)
		{
			return DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a));
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000D4396 File Offset: 0x000D2596
		internal int <LoadLatestBackup>b__65_0(string a, string b)
		{
			return DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a));
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000D43A9 File Offset: 0x000D25A9
		internal int <DeleteOldest>b__67_0(string b, string a)
		{
			return DateTime.Compare(Directory.GetCreationTime(b), Directory.GetCreationTime(a));
		}

		// Token: 0x0400297E RID: 10622
		public static readonly Settings.<>c <>9 = new Settings.<>c();

		// Token: 0x0400297F RID: 10623
		public static Comparison<string> <>9__64_0;

		// Token: 0x04002980 RID: 10624
		public static Comparison<string> <>9__65_0;

		// Token: 0x04002981 RID: 10625
		public static Comparison<string> <>9__67_0;
	}
}
