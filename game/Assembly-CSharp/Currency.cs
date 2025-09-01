using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public static class Currency
{
	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0004B2FB File Offset: 0x000494FB
	// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0004B302 File Offset: 0x00049502
	public static int Gildings
	{
		[CompilerGenerated]
		get
		{
			return Currency.<Gildings>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Currency.<Gildings>k__BackingField = value;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0004B30A File Offset: 0x0004950A
	// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0004B311 File Offset: 0x00049511
	public static int LoadoutCoin
	{
		[CompilerGenerated]
		get
		{
			return Currency.<LoadoutCoin>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Currency.<LoadoutCoin>k__BackingField = value;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0004B319 File Offset: 0x00049519
	// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0004B320 File Offset: 0x00049520
	public static int GildingsSpent
	{
		[CompilerGenerated]
		get
		{
			return Currency.<GildingsSpent>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Currency.<GildingsSpent>k__BackingField = value;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0004B328 File Offset: 0x00049528
	// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0004B32F File Offset: 0x0004952F
	public static int LCoinSpent
	{
		[CompilerGenerated]
		get
		{
			return Currency.<LCoinSpent>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Currency.<LCoinSpent>k__BackingField = value;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0004B337 File Offset: 0x00049537
	// (set) Token: 0x06000B81 RID: 2945 RVA: 0x0004B348 File Offset: 0x00049548
	public static bool Initialized
	{
		get
		{
			if (Currency._initialized)
			{
				return true;
			}
			Currency.Initialize();
			return true;
		}
		set
		{
			Currency._initialized = value;
		}
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0004B350 File Offset: 0x00049550
	private static void Initialize()
	{
		if (!UnlockManager.Initialized)
		{
			return;
		}
		try
		{
			ES3.CacheFile("unlocks.vel");
			ES3Settings settings = new ES3Settings("unlocks.vel", new Enum[]
			{
				ES3.Location.Cache
			});
			Currency.Gildings = ES3.Load<int>("Currency_Gilding", 0, settings);
			Currency.LoadoutCoin = ES3.Load<int>("LCoins", 0, settings);
			Currency.GildingsSpent = ES3.Load<int>("Gilding_Spent", 0, settings);
			Currency.LCoinSpent = ES3.Load<int>("LCoins_Spent", 0, settings);
		}
		catch (Exception)
		{
			Debug.Log("Failed to load Unlocks File for currency");
		}
		Currency._initialized = true;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0004B3F4 File Offset: 0x000495F4
	public static void Add(int amount, bool shouldSaveImmediate = true)
	{
		if (!Currency.Initialized)
		{
			return;
		}
		if (amount == 0)
		{
			return;
		}
		Currency.Gildings += amount;
		if (shouldSaveImmediate)
		{
			Currency.Save();
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0004B416 File Offset: 0x00049616
	public static void ModifyInternal(int amount)
	{
		if (!Currency.Initialized)
		{
			return;
		}
		Currency.Gildings += amount;
		Currency.Gildings = Mathf.Max(0, Currency.Gildings);
		Currency.Save();
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0004B441 File Offset: 0x00049641
	private static bool CanSpend(int amount)
	{
		return Currency.Initialized && Currency.Gildings >= amount;
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0004B457 File Offset: 0x00049657
	public static bool TrySpend(int amount)
	{
		if (!Currency.CanSpend(amount))
		{
			return false;
		}
		Currency.Gildings -= amount;
		Currency.GildingsSpent += amount;
		if (Currency.Gildings < 0)
		{
			Currency.Gildings = 0;
		}
		Currency.Save();
		return true;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0004B48F File Offset: 0x0004968F
	public static void AddLoadoutCoin(int amount = 1, bool saveImmediate = true)
	{
		if (!Currency.Initialized || amount == 0)
		{
			return;
		}
		Currency.LoadoutCoin += Mathf.Abs(amount);
		if (saveImmediate)
		{
			Currency.Save();
		}
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0004B4B8 File Offset: 0x000496B8
	public static void SpendLoadoutCoin(int amount = 1, bool saveImmediate = true)
	{
		if (!Currency.Initialized)
		{
			return;
		}
		Currency.LoadoutCoin -= Mathf.Abs(amount);
		Currency.LoadoutCoin = Mathf.Max(0, Currency.LoadoutCoin);
		Currency.LCoinSpent += amount;
		if (saveImmediate)
		{
			Currency.Save();
			if (PlayerControl.myInstance != null)
			{
				PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaEvent, null, 1f);
			}
		}
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0004B521 File Offset: 0x00049721
	public static void ResetForPrestige()
	{
		Currency.LCoinSpent += Currency.LoadoutCoin;
		Currency.LoadoutCoin = 500;
		Currency.Save();
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0004B542 File Offset: 0x00049742
	public static void Reset()
	{
		Currency.LoadoutCoin = 0;
		Currency.Gildings = 0;
		Currency.Save();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0004B558 File Offset: 0x00049758
	public static void Save()
	{
		if (!Currency.Initialized)
		{
			return;
		}
		ES3Settings settings = new ES3Settings("unlocks.vel", null);
		bool flag = false;
		try
		{
			ES3.CacheFile("unlocks.vel");
			settings = new ES3Settings("unlocks.vel", new Enum[]
			{
				ES3.Location.Cache
			});
		}
		catch (Exception)
		{
			flag = true;
		}
		ES3.Save<int>("Currency_Gilding", Mathf.Max(Currency.Gildings, 0), settings);
		ES3.Save<int>("LCoins", Mathf.Max(Currency.LoadoutCoin, 0), settings);
		ES3.Save<int>("Gilding_Spent", Mathf.Max(Currency.GildingsSpent, 0), settings);
		ES3.Save<int>("LCoins_Spent", Mathf.Max(Currency.LCoinSpent, 0), settings);
		if (!flag)
		{
			ES3.StoreCachedFile("unlocks.vel");
		}
	}

	// Token: 0x0400096A RID: 2410
	[CompilerGenerated]
	private static int <Gildings>k__BackingField;

	// Token: 0x0400096B RID: 2411
	[CompilerGenerated]
	private static int <LoadoutCoin>k__BackingField;

	// Token: 0x0400096C RID: 2412
	[CompilerGenerated]
	private static int <GildingsSpent>k__BackingField;

	// Token: 0x0400096D RID: 2413
	[CompilerGenerated]
	private static int <LCoinSpent>k__BackingField;

	// Token: 0x0400096E RID: 2414
	private static bool _initialized;

	// Token: 0x0400096F RID: 2415
	private const string GILDING_KEY = "Currency_Gilding";

	// Token: 0x04000970 RID: 2416
	private const string LC_KEY = "LCoins";

	// Token: 0x04000971 RID: 2417
	private const string GILDING_SPENT = "Gilding_Spent";

	// Token: 0x04000972 RID: 2418
	private const string LC_SPENT = "LCoins_Spent";
}
