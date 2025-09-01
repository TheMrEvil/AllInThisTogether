using System;
using QFSW.QC;
using UnityEngine;

// Token: 0x0200003C RID: 60
[CommandPrefix("unlock.")]
public class CMD_Unlock
{
	// Token: 0x060001E8 RID: 488 RVA: 0x00011788 File Offset: 0x0000F988
	[Command("loadoutToken", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Modify your lodout tokens by the specified amount")]
	private static string LoadoutToken(int amount = 1)
	{
		if (amount == 0)
		{
			return "Need a value other than 0";
		}
		if (amount < 0)
		{
			Currency.SpendLoadoutCoin(amount, true);
		}
		else
		{
			Currency.AddLoadoutCoin(amount, true);
		}
		return "You now have " + Currency.LoadoutCoin.ToString() + " loadout tokens.";
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x000117D0 File Offset: 0x0000F9D0
	[Command("gildings", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Modify your cosmetic currency by the specified amount")]
	private static string Gildings(int amount = 100)
	{
		if (amount == 0)
		{
			return "Need a value other than 0";
		}
		Currency.ModifyInternal(amount);
		return "You now have " + Currency.Gildings.ToString() + " gildings.";
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00011808 File Offset: 0x0000FA08
	[Command("binding", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Unlock a Binding for use")]
	private static string Bindings([Cmd_Game.BindingAugmentAttribute] string augment)
	{
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find Binding: " + augment;
		}
		string str = "Adding Binding - ";
		AugmentTree augmentTree = augmentByName;
		Debug.Log(str + ((augmentTree != null) ? augmentTree.ToString() : null));
		UnlockManager.UnlockBinding(augmentByName);
		return "Binding [" + augmentByName.Root.Name + "] Unlocked";
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0001186D File Offset: 0x0000FA6D
	[Command("reset-all", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all saved game data (does not include settings)")]
	private static string ResetAll()
	{
		UnlockManager.Reset();
		Currency.Reset();
		Progression.ResetSave();
		GameStats.ResetSaved();
		Settings.ResetTutorial();
		return "Game Save Data Reset";
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0001188D File Offset: 0x0000FA8D
	[Command("reset-currency", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset saved currency (loadout tokens and cosmetic currency)")]
	private static string ResetCurrency()
	{
		Currency.Reset();
		return "Currency Values Reset";
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00011899 File Offset: 0x0000FA99
	[Command("reset-cosmetics", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset unlocked cosmetics")]
	private static string ResetCosmetics()
	{
		UnlockManager.ResetCosmetics();
		return "Cosmetic Unlocks Reset";
	}

	// Token: 0x060001EE RID: 494 RVA: 0x000118A5 File Offset: 0x0000FAA5
	[Command("reset-tutorial", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset tutorial completion state")]
	private static string ResetTutorial()
	{
		Settings.ResetTutorial();
		return "Tutorial Progress Reset";
	}

	// Token: 0x060001EF RID: 495 RVA: 0x000118B1 File Offset: 0x0000FAB1
	[Command("reset-stats", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset saved game stats")]
	private static string ResetStats()
	{
		GameStats.ResetSaved();
		return "Game Stats Reset";
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000118BD File Offset: 0x0000FABD
	[Command("reset-abilities", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all unlocked abilities")]
	private static string ResetAbilities()
	{
		UnlockManager.ResetAbilities();
		return "Ability Unlocks Reset";
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x000118C9 File Offset: 0x0000FAC9
	[Command("reset-pages", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all unlocked Pages")]
	private static string ResetAugments()
	{
		UnlockManager.ResetAugments();
		return "Augment Unlocks Reset";
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x000118D5 File Offset: 0x0000FAD5
	[Command("reset-genres", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all unlocked abilities")]
	private static string ResetGenres()
	{
		UnlockManager.ResetGenres();
		return "Genre Unlocks Reset";
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000118E1 File Offset: 0x0000FAE1
	[Command("reset-bindings", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all unlocked abilities")]
	private static string ResetBindings()
	{
		UnlockManager.ResetBindings();
		return "Binding Unlocks Reset";
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000118ED File Offset: 0x0000FAED
	[Command("reset-talents", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset unlocked cosmetics")]
	private static string ResetTalents()
	{
		Progression.ResetProgression();
		UnlockManager.ResetTalents();
		return "Talent Unlocks Reset";
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000118FE File Offset: 0x0000FAFE
	public CMD_Unlock()
	{
	}
}
