using System;
using System.Collections.Generic;
using QFSW.QC;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200003A RID: 58
[CommandPrefix("plr.")]
public static class Cmd_Player
{
	// Token: 0x060001C8 RID: 456 RVA: 0x00010800 File Offset: 0x0000EA00
	[Command("reset-position", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Resets your position to a random player spawn point")]
	private static string ResetPosition()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		Transform spawnPoint = GameplayManager.instance.GetSpawnPoint();
		PlayerControl.myInstance.Movement.SetPositionWithCamera(spawnPoint.position, spawnPoint.forward, true, true);
		PlayerControl.myInstance.Movement.ResetVelocity();
		return "Player sent to spawn point";
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0001085C File Offset: 0x0000EA5C
	[Command("revive", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Revives local player with full health")]
	private static string Revive()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (!PlayerControl.myInstance.IsDead)
		{
			return "Can only revive if you're dead!";
		}
		PlayerControl.myInstance.health.Revive(1f);
		return "Player sent to spawn point";
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0001089C File Offset: 0x0000EA9C
	[Command("damage", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Take the specifid amount of damage")]
	private static string Damage(int amount = 25)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (PlayerControl.myInstance.IsDead)
		{
			return "Can't take damage while dead";
		}
		if (amount <= 0)
		{
			return "Need a damage value > 0";
		}
		DamageInfo dmg = new DamageInfo((float)amount, DNumType.Default, PlayerControl.myInstance.ViewID, 1f, null);
		PlayerControl.myInstance.health.ApplyDamageImmediate(dmg);
		return "Player Damaged";
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00010906 File Offset: 0x0000EB06
	[Command("dps", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Take the specifid amount of damage")]
	private static string ToggleDPS()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		DPSMeterUI.Toggle();
		return "DPS Meter Active: " + DPSMeterUI.IsActive.ToString();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00010938 File Offset: 0x0000EB38
	[Command("heal", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Take the specifid amount of damage")]
	private static string Heal(float amount = 25f)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (PlayerControl.myInstance.IsDead)
		{
			return "Can't heal while dead, use plr.revive first";
		}
		if (amount <= 0f)
		{
			return "Need a value > 0";
		}
		PlayerControl.myInstance.health.Heal(new DamageInfo((int)amount));
		return "Player Healed";
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00010994 File Offset: 0x0000EB94
	[Command("loadbuild", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Load a JSON Build from the Parse backend")]
	private static string LoadBuild(string json)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		json = json.Substring(1, json.Length - 2);
		JSONNode jsonnode = JSON.Parse(json);
		if (jsonnode == null || !jsonnode.HasKey("players"))
		{
			return "Invalid JSON Data";
		}
		JSONArray asArray = jsonnode["players"].AsArray;
		if (asArray == null || asArray.Count == 0)
		{
			return "Invalid JSON Data: Missing players";
		}
		JSONNode jsonnode2 = asArray[0];
		JSONObject asObject = jsonnode2["loadout"].AsObject;
		JSONArray asArray2 = jsonnode2["pages"].AsArray;
		if (asObject == null || asArray2 == null)
		{
			return "Invalid JSON Data: Missing loadout or pages";
		}
		GameDB.ElementInfo element = GameDB.GetElement(GameDB.GetColor(asObject.GetValueOrDefault("core", "").ToString().Replace(" Ink", "").Replace("\"", "")));
		PlayerControl.myInstance.actions.SetCore(element.Core);
		foreach (string text in new List<string>
		{
			asObject.GetValueOrDefault("primary", ""),
			asObject.GetValueOrDefault("secondary", ""),
			asObject.GetValueOrDefault("utility", "")
		})
		{
			if (!string.IsNullOrEmpty(text))
			{
				AbilityTree abilityTree = null;
				foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
				{
					if (!(abilityUnlock.Ability.Root.Usage.AbilityMetadata.Name != text))
					{
						abilityTree = abilityUnlock.Ability;
						break;
					}
				}
				if (abilityTree == null)
				{
					return "Could not find ability: " + text;
				}
				PlayerControl.myInstance.actions.LoadAbility(abilityTree.Root.PlrAbilityType, abilityTree.Root.guid, false);
			}
		}
		foreach (KeyValuePair<string, JSONNode> keyValuePair in asArray2)
		{
			string text2 = keyValuePair.Value.ToString().Replace("\"", "").Replace(" ", "_");
			AugmentTree augmentByName = GraphDB.GetAugmentByName(text2);
			if (augmentByName == null)
			{
				return "Couldn't find page with name [" + text2 + "]";
			}
			PlayerControl.myInstance.AddAugment(augmentByName, 1);
		}
		return "Loaded Player Data";
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00010C98 File Offset: 0x0000EE98
	[Command("mana", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Refills your mana")]
	private static string Mana(MagicColor color = MagicColor.Any)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (color == MagicColor.Any)
		{
			PlayerControl.myInstance.Mana.Recharge(99f);
			for (int i = 0; i < 5; i++)
			{
				PlayerControl.myInstance.Mana.AddMana(MagicColor.Neutral, true, true);
			}
		}
		else
		{
			PlayerControl.myInstance.Mana.ResetMana();
			for (int j = 0; j < 10; j++)
			{
				PlayerControl.myInstance.Mana.ChangeManaElement(false, true, color);
			}
			for (int k = 0; k < 5; k++)
			{
				PlayerControl.myInstance.Mana.AddMana(color, true, true);
			}
		}
		return "Player Mana Refilled";
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00010D40 File Offset: 0x0000EF40
	[Command("page-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified page to your player")]
	private static string AddAugment([Cmd_Player.PlayerAugmentAttribute] string augment, int count = 1)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (augment.Length == 0)
		{
			return "Need to specify page";
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find page with name [" + augment + "]";
		}
		PlayerControl.myInstance.AddAugment(augmentByName, count);
		return "Added page [" + augment + "]";
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00010DAC File Offset: 0x0000EFAC
	[Command("page-reqs", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified page to your player")]
	private static string AugmentRequirements([Cmd_Player.PlayerAugmentAttribute] string augment)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (augment.Length == 0)
		{
			return "Need to specify page";
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find page with name [" + augment + "]";
		}
		bool flag = augmentByName.Root.Validate(PlayerControl.myInstance.SelfProps);
		return string.Format("[{0}] Requirements Met: {1}", augment, flag);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00010E24 File Offset: 0x0000F024
	[Command("pages-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified page to your player")]
	private static string AddAugment([Cmd_Player.PlayerAugmentAttribute] List<string> augments)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (augments.Count == 0)
		{
			return "Need to specify pages";
		}
		foreach (string text in augments)
		{
			string text2 = text.Replace("\"", "").Replace(" ", "_");
			AugmentTree augmentByName = GraphDB.GetAugmentByName(text2);
			if (augmentByName == null)
			{
				return "Couldn't find page with name [" + text2 + "]";
			}
			PlayerControl.myInstance.AddAugment(augmentByName, 1);
		}
		return "Added pages";
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	[Command("scroll", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Create an scroll for the specified page")]
	private static string CreateScroll([Cmd_Player.PlayerAugmentAttribute] string augment)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (augment.Length == 0)
		{
			return "Need to specify page";
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find page with name [" + augment + "]";
		}
		GoalManager.instance.CreateScroll(augmentByName, PlayerControl.myInstance.CameraAimPoint);
		return "Scroll created [" + augment + "]";
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00010F58 File Offset: 0x0000F158
	[Command("status-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified Status Effect to yourself")]
	private static string AddStatus([Cmd_AI.StatusAttribute] string status, float duration = 0f, int stacks = 1)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (status.Length == 0)
		{
			return "Need to specify status";
		}
		StatusTree statusByName = GraphDB.GetStatusByName(status);
		if (statusByName == null)
		{
			return "Couldn't find status with name [" + status + "]";
		}
		PlayerControl.myInstance.net.ApplyStatus(statusByName.RootNode.guid.GetHashCode(), PlayerControl.myInstance.ViewID, duration, stacks, true, 0);
		return "Added Status [" + statusByName.Root.EffectName + "] to Self";
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00010FF0 File Offset: 0x0000F1F0
	[Command("status-remove", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Removes the specified Status Effect to yourself")]
	private static string RemoveStatus([Cmd_AI.StatusAttribute] string status)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (status.Length == 0)
		{
			return "Need to specify status";
		}
		StatusTree statusByName = GraphDB.GetStatusByName(status);
		if (statusByName == null)
		{
			return "Couldn't find status with name [" + status + "]";
		}
		PlayerControl.myInstance.net.RemoveStatus(statusByName.RootNode.guid.GetHashCode(), PlayerControl.myInstance.ViewID, 0, true, true);
		return "Removed Status [" + statusByName.Root.EffectName + "] from Self";
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00011088 File Offset: 0x0000F288
	[Command("cosmetic", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Apply the specified cosmetic")]
	public static string Cosmetic([Cmd_Player.PlayerCosmeticAttribute] string cid)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (cid.Length == 0)
		{
			return "Need to specify a cosmetic item";
		}
		Cosmetic cosmetic = CosmeticDB.GetCosmetic(cid);
		if (cosmetic == null)
		{
			return "Could not find Cosmetic: " + cid;
		}
		PlayerControl.myInstance.Display.ChangeCosmetic(cosmetic);
		return "Cosmetic changed";
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x000110E1 File Offset: 0x0000F2E1
	[Command("ink", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add or Subtract specified Ink")]
	private static string ModifyInk(int amount)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (amount == 0)
		{
			return "";
		}
		InkManager.instance.AddInk(amount);
		return amount.ToString() + " Ink Added";
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0001111C File Offset: 0x0000F31C
	[Command("cooldowns", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Resets all cooldowns")]
	private static string ResetCooldowns()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Primary, false);
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Secondary, false);
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Movement, false);
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Utility, false);
		return "Player Cooldowns Reset";
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00011185 File Offset: 0x0000F385
	[Command("page-reset", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Resets player pages")]
	private static string ResetAugments()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		PlayerControl.myInstance.Augment = new Augments();
		return "Player Pages Reset";
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x000111B0 File Offset: 0x0000F3B0
	[Command("ability", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Equip Ability")]
	private static string ApplyAbility([Cmd_Player.PlayerAbilityAttribute] string abilityName)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		AbilityTree abilityTree = null;
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (!(abilityUnlock.Ability.Root.Usage.AbilityMetadata.Name != abilityName))
			{
				abilityTree = abilityUnlock.Ability;
				break;
			}
		}
		if (abilityTree == null)
		{
			return "Could not find Ability: " + abilityName;
		}
		PlayerControl.myInstance.actions.LoadAbility(abilityTree.Root.PlrAbilityType, abilityTree.Root.guid, false);
		return "Ability Selected: " + abilityName;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00011288 File Offset: 0x0000F488
	[Command("core", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set the player core")]
	private static string Core(MagicColor color = MagicColor.Neutral)
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		GameDB.ElementInfo element = GameDB.GetElement(color);
		PlayerControl.myInstance.actions.SetCore(element.Core);
		return "Core changed to " + color.ToString();
	}

	// Token: 0x060001DB RID: 475 RVA: 0x000112DC File Offset: 0x0000F4DC
	[Command("reward", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add Page reward for selection")]
	private static string AddAugmentChoice()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (!GameplayManager.IsInGame)
		{
			return "Must be in a game";
		}
		List<AugmentTree> list = new List<AugmentTree>();
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Player);
		int num = Mathf.Min(3, validMods.Count);
		for (int i = 0; i < num; i++)
		{
			AugmentTree item = GraphDB.ChooseModFromList(ModType.Player, validMods, false, false);
			validMods.Remove(item);
			list.Add(item);
		}
		AugmentsPanel.AwardUpgradeChoice(list);
		return "Page Choice Added";
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00011358 File Offset: 0x0000F558
	[Command("reward-explicit", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add Specific Page rewards for selection")]
	private static string AddAugmentChoiceExplicit([Cmd_Player.PlayerAugmentAttribute] string augment1, [Cmd_Player.PlayerAugmentAttribute] string augment2 = "", [Cmd_Player.PlayerAugmentAttribute] string augment3 = "")
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (!GameplayManager.IsInGame)
		{
			return "Must be in a game";
		}
		List<AugmentTree> list = new List<AugmentTree>();
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment1);
		if (augmentByName == null)
		{
			return "Couldn't find page with name [" + augment1 + "]";
		}
		list.Add(augmentByName);
		if (!string.IsNullOrEmpty(augment2))
		{
			augmentByName = GraphDB.GetAugmentByName(augment2);
			if (augmentByName == null)
			{
				return "Couldn't find page with name [" + augment2 + "]";
			}
			list.Add(augmentByName);
		}
		if (!string.IsNullOrEmpty(augment3))
		{
			augmentByName = GraphDB.GetAugmentByName(augment3);
			if (augmentByName == null)
			{
				return "Couldn't find page with name [" + augment3 + "]";
			}
			list.Add(augmentByName);
		}
		AugmentsPanel.AwardUpgradeChoice(list);
		return "Page Choices Added";
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00011420 File Offset: 0x0000F620
	[Command("targetable", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Toggles local player targetability")]
	private static string ToggleTargetable()
	{
		if (PlayerControl.myInstance == null)
		{
			return Cmd_Player.NoPlayerError;
		}
		if (!GameplayManager.IsInGame)
		{
			return "Must be in a game";
		}
		PlayerControl.myInstance.Net.ToggleTargetable(!PlayerControl.myInstance.Targetable, PlayerControl.myInstance.Affectable);
		return "Player Targetable: " + PlayerControl.myInstance.Targetable.ToString();
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060001DE RID: 478 RVA: 0x0001148C File Offset: 0x0000F68C
	private static string NoPlayerError
	{
		get
		{
			return "Player does not Exist!";
		}
	}

	// Token: 0x02000422 RID: 1058
	public struct PlayerAugmentTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000423 RID: 1059
	public sealed class PlayerAugmentAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020E1 RID: 8417 RVA: 0x000C161C File Offset: 0x000BF81C
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000C1624 File Offset: 0x000BF824
		public PlayerAugmentAttribute()
		{
		}

		// Token: 0x0400213F RID: 8511
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Player.PlayerAugmentTag)
		};
	}

	// Token: 0x02000424 RID: 1060
	public class PlayerAugmentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020E3 RID: 8419 RVA: 0x000C1654 File Offset: 0x000BF854
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Player.PlayerAugmentTag>();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000C165D File Offset: 0x000BF85D
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000C1668 File Offset: 0x000BF868
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AugmentTree augmentTree in GraphDB.instance.PlayerMods)
			{
				list.Add(augmentTree.Root.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000C16E0 File Offset: 0x000BF8E0
		public PlayerAugmentSuggestor()
		{
		}
	}

	// Token: 0x02000425 RID: 1061
	public struct PlayerAbilityTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000426 RID: 1062
	public sealed class PlayerAbilityAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020E7 RID: 8423 RVA: 0x000C16E8 File Offset: 0x000BF8E8
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000C16F0 File Offset: 0x000BF8F0
		public PlayerAbilityAttribute()
		{
		}

		// Token: 0x04002140 RID: 8512
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Player.PlayerAbilityTag)
		};
	}

	// Token: 0x02000427 RID: 1063
	public class PlayerAbilitySuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020E9 RID: 8425 RVA: 0x000C1720 File Offset: 0x000BF920
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Player.PlayerAbilityTag>();
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000C1729 File Offset: 0x000BF929
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000C1734 File Offset: 0x000BF934
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
			{
				list.Add(abilityUnlock.Ability.Root.Usage.AbilityMetadata.Name);
			}
			return list;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000C17AC File Offset: 0x000BF9AC
		public PlayerAbilitySuggestor()
		{
		}
	}

	// Token: 0x02000428 RID: 1064
	public struct PlayerCosmeticTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000429 RID: 1065
	public sealed class PlayerCosmeticAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020ED RID: 8429 RVA: 0x000C17B4 File Offset: 0x000BF9B4
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000C17BC File Offset: 0x000BF9BC
		public PlayerCosmeticAttribute()
		{
		}

		// Token: 0x04002141 RID: 8513
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Player.PlayerCosmeticTag)
		};
	}

	// Token: 0x0200042A RID: 1066
	public class PlayerCosmeticSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x000C17EC File Offset: 0x000BF9EC
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Player.PlayerCosmeticTag>();
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000C17F5 File Offset: 0x000BF9F5
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000C1800 File Offset: 0x000BFA00
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			list.Add(CosmeticDB.DB.DefaultHead.cosmeticid);
			foreach (Cosmetic_Head cosmetic_Head in CosmeticDB.DB.Heads)
			{
				list.Add(cosmetic_Head.cosmeticid);
			}
			list.Add(CosmeticDB.DB.DefaultSkin.cosmeticid);
			foreach (Cosmetic_Skin cosmetic_Skin in CosmeticDB.DB.Skins)
			{
				list.Add(cosmetic_Skin.cosmeticid);
			}
			list.Add(CosmeticDB.DB.DefaultBook.cosmeticid);
			foreach (Cosmetic_Book cosmetic_Book in CosmeticDB.DB.Books)
			{
				list.Add(cosmetic_Book.cosmeticid);
			}
			foreach (Cosmetic_Signature cosmetic_Signature in CosmeticDB.DB.Signatures)
			{
				list.Add(cosmetic_Signature.cosmeticid);
			}
			return list;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000C198C File Offset: 0x000BFB8C
		public PlayerCosmeticSuggestor()
		{
		}
	}
}
