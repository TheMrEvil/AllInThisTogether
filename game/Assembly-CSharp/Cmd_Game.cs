using System;
using System.Collections.Generic;
using Photon.Pun;
using QFSW.QC;
using UnityEngine;

// Token: 0x02000038 RID: 56
[CommandPrefix("game.")]
public class Cmd_Game
{
	// Token: 0x060001A5 RID: 421 RVA: 0x0000FD04 File Offset: 0x0000DF04
	[Command("genre", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Select and Start the specified genre")]
	private static string PickGenre([Cmd_Game.GenreAttribute] string genre)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		GenreTree genreByName = GraphDB.GetGenreByName(genre);
		if (genreByName == null)
		{
			return "Couldn't find Genre: " + genre;
		}
		GenrePanel.ApplyGenre(genreByName);
		return "Loaded Genre: " + (genreByName.RootNode as GenreRootNode).Name;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000FD68 File Offset: 0x0000DF68
	[Command("challenge", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Select and Prepare the specified Book Club Challenge")]
	private static string Challenge([Cmd_Game.ChallengeAttribute] string challenge)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		MetaDB.BookClubChallenge bookClubChallenge = MetaDB.GetBookClubChallenge(challenge);
		if (bookClubChallenge == null)
		{
			string str = "Couldn't find Challenge: ";
			MetaDB.BookClubChallenge bookClubChallenge2 = bookClubChallenge;
			return str + ((bookClubChallenge2 != null) ? bookClubChallenge2.ToString() : null);
		}
		BookClubPanel.editorOverrideChallenge = bookClubChallenge;
		GameplayManager.instance.LoadChallenge(bookClubChallenge);
		return "Loaded Challenge: " + bookClubChallenge.Name;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
	[Command("map", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Change the current map")]
	private static string ChangeMap([Cmd_Game.MapAttribute] string mapName)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (GameplayManager.instance.CurrentState == GameState.InWave)
		{
			return "Can't change map during a wave";
		}
		GameMap mapByName = AIManager.instance.Waves.GetMapByName(mapName);
		if (mapByName == null)
		{
			return "Couldn't find Map: " + mapName;
		}
		if (MapManager.CurrentSceneName == mapByName.Scene.SceneName)
		{
			return "Already on this map!";
		}
		if (GameplayManager.CurrentTome == null)
		{
			GameplayManager.instance.TrySetGenre(UnlockDB.DB.Genres[0].Genre);
		}
		MapManager.instance.ChangeMap(mapByName.Scene.SceneName);
		GameplayManager.instance.UpdateGameState(GameState.EXPLICIT_CMD);
		return "Loading Map: " + mapByName.Name;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
	[Command("map-vignette", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Change the current map")]
	private static string ChangeVignette([Cmd_Game.VignetteAttribute] string mapName)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (GameplayManager.instance.CurrentState == GameState.InWave)
		{
			return "Can't change scene during a wave";
		}
		Vignette vignetteByName = AIManager.instance.Waves.GetVignetteByName(mapName);
		if (vignetteByName == null)
		{
			return "Couldn't find Vignette: " + mapName;
		}
		if (MapManager.CurrentSceneName == vignetteByName.Scene.SceneName)
		{
			return "Already in this Vignette!";
		}
		GameplayManager.instance.UpdateGameState(GameState.Vignette_PreWait);
		MapManager.instance.ChangeMap(vignetteByName.Scene.SceneName);
		GameplayManager.instance.UpdateGameState(GameState.Vignette_Traveling);
		return "Loading Vignette: " + vignetteByName.Name;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000FF5C File Offset: 0x0000E15C
	[Command("augment-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Fountan Augment")]
	private static string AddAugment([Cmd_Game.FountainAugmentAttribute] string augment, int count = 1)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find Augment: " + augment;
		}
		InkManager.instance.AwardGameAugment(augmentByName, count);
		return "Added Ink Augment: " + (augmentByName.RootNode as AugmentRootNode).Name;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000FFC5 File Offset: 0x0000E1C5
	[Command("library2", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Fountan Augment")]
	private static string NewLibrary()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!MapManager.InLobbyScene)
		{
			return "Have to be in lobby";
		}
		MapManager.instance.ChangeMap("LibraryV2");
		return "Changing Library Scene";
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00010004 File Offset: 0x0000E204
	[Command("binding-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Bindng Augment")]
	private static string AddBinding([Cmd_Game.BindingAugmentAttribute] string augment, int count = 1)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		Debug.Log("Checking GraphDB");
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find Binding: " + augment;
		}
		string str = "Adding Binding - ";
		AugmentTree augmentTree = augmentByName;
		Debug.Log(str + ((augmentTree != null) ? augmentTree.ToString() : null));
		GameplayManager.instance.AddBinding(augmentByName);
		return "Added Binding: " + (augmentByName.RootNode as AugmentRootNode).Name;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00010094 File Offset: 0x0000E294
	[Command("bindings-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Bindng Augment")]
	private static string AddBinding(List<string> augments)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		Debug.Log("Checking GraphDB");
		if (augments.Count == 0)
		{
			return "Need to specify Bindings";
		}
		foreach (string text in augments)
		{
			string text2 = text.Replace("\"", "");
			AugmentTree augmentByName = GraphDB.GetAugmentByName(text2);
			if (augmentByName == null)
			{
				return "Couldn't find Binding: " + text2;
			}
			string str = "Adding Binding - ";
			AugmentTree augmentTree = augmentByName;
			Debug.Log(str + ((augmentTree != null) ? augmentTree.ToString() : null));
			GameplayManager.instance.AddBinding(augmentByName);
		}
		return "Added Bindings";
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00010168 File Offset: 0x0000E368
	[Command("binding-reset", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Bindng Augment")]
	private static string ResetBindings()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		GameplayManager.instance.GenreBindings = new Augments();
		GameplayManager.instance.SyncMods();
		return "Game Bindings Reset";
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000101A4 File Offset: 0x0000E3A4
	[Command("wave-complete", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Add a Fountan Augment")]
	private static string CompleteWave()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!GameplayManager.IsInGame || GameplayManager.instance.CurrentState != GameState.InWave)
		{
			return "Not in a wave";
		}
		AIManager.KillAllEnemies();
		WaveManager.instance.EndWave();
		return "Completed Wave";
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000101F9 File Offset: 0x0000E3F9
	[Command("vignette-end", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Complete the current Vignette")]
	private static string CompleteVignette()
	{
		return "Not available outside Editor";
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00010200 File Offset: 0x0000E400
	[Command("timescale", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Change the game speed")]
	private static string Timescale(float scale = 1f)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		scale = Mathf.Clamp(scale, 0.05f, 4f);
		GameplayManager.instance.SetTimescale(scale);
		return "Timescale set to " + scale.ToString();
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00010258 File Offset: 0x0000E458
	[Command("bonus", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Start a Bonus Objective")]
	private static string BonusObjective([Cmd_Game.ObjectiveAugmentAttribute] string augment)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find Objective: " + augment;
		}
		GoalManager.instance.SetBonusObjective(augmentByName, true);
		GoalManager.instance.BeginBonusObjective();
		return "NA";
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000102B6 File Offset: 0x0000E4B6
	[Command("bonus-complete", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Complete the current Bonus Objective")]
	private static string CompleteBonusObjective()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!GoalManager.InBonusObjective)
		{
			return "Not in Bonus Objective";
		}
		GoalManager.TryCompleteBonusObjective(Vector3.zero);
		return "BO Completed";
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x000102EE File Offset: 0x0000E4EE
	[Command("bonus-fail", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Fail the current Bonus Objective")]
	private static string CancelBonusObjective()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!GoalManager.InBonusObjective)
		{
			return "Not in Bonus Objective";
		}
		GoalManager.TryCancelBonusObjective();
		return "BO Canceled";
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00010324 File Offset: 0x0000E524
	[Command("ai-layout", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Start the selected AI Layout")]
	private static string AILayout([Cmd_Game.AILayoutOptionAttribute] string layoutName)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		AILayout layout = AIManager.instance.DB.GetLayout("LayoutName_" + layoutName);
		if (layout == null)
		{
			return "Couldn't find AI Layout: " + layoutName;
		}
		AIManager.instance.OverrideLayout(layout);
		return "NA";
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00010388 File Offset: 0x0000E588
	[Command("raid-hardmode", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set Raid Difficulty Mode")]
	private static string RaidHardMode()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!RaidManager.IsInRaid)
		{
			return "Not in Raid";
		}
		RaidManager.instance.Difficulty = ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard) ? RaidDB.Difficulty.Normal : RaidDB.Difficulty.Hard);
		return "Raid Difficulty Set to " + RaidManager.instance.Difficulty.ToString();
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000103F6 File Offset: 0x0000E5F6
	[Command("end", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("End the current game")]
	private static string EndGame()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		GameplayManager.instance.EndGame(true);
		return "Game Ended";
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00010422 File Offset: 0x0000E622
	[Command("complete", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Complete the current game")]
	private static string CompleteGame()
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		GameplayManager.instance.GameWon();
		return "Game Completed";
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00010450 File Offset: 0x0000E650
	[Command("tutorial", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("End the current game")]
	private static string SetTutorialStep(TutorialStep step)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		if (!TutorialManager.InTutorial || UnityEngine.Object.FindObjectOfType<TutorialMapControl>() == null)
		{
			return "Not in Tutorial";
		}
		TutorialManager.instance.ChangeStep(step);
		return "Tutorial Step Changed: " + step.ToString();
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000104B3 File Offset: 0x0000E6B3
	[Command("set-wave", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set the current wave number")]
	private static string SetWave(int wave)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		WaveManager.CurrentWave = wave;
		WaveManager.instance.WavesCompleted = wave - 1;
		return "Current Wave Set to " + wave.ToString();
	}

	// Token: 0x060001BA RID: 442 RVA: 0x000104F4 File Offset: 0x0000E6F4
	[Command("set-appendix", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Set the current Appendix value")]
	private static string SetAppendix(int appendix, int appendixWave = 0)
	{
		if (PlayerControl.myInstance == null || !PhotonNetwork.IsMasterClient)
		{
			return Cmd_Game.NotMaster;
		}
		WaveManager.instance.AppendixLevel = appendix;
		int num = 0;
		if (appendix > 0 && GameplayManager.CurTomeRoot != null)
		{
			num += GameplayManager.CurTomeRoot.Waves.Count;
			num += appendix;
			for (int i = 1; i < appendix; i++)
			{
				num += GameplayManager.CurTomeRoot.Appendix.Count;
			}
			num += Mathf.Max(appendixWave, GameplayManager.CurTomeRoot.Appendix.Count - 1);
		}
		WaveManager.CurrentWave = num;
		WaveManager.instance.WavesCompleted = num - 1;
		return string.Format("Current Appendix Set to {0}.{1} (Chapter {2})", appendix, appendixWave, num);
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060001BB RID: 443 RVA: 0x000105B6 File Offset: 0x0000E7B6
	private static bool IsMasterClient
	{
		get
		{
			return !PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060001BC RID: 444 RVA: 0x000105C6 File Offset: 0x0000E7C6
	private static string NotMaster
	{
		get
		{
			return "<color=red>Only Host can use this command</color>";
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060001BD RID: 445 RVA: 0x000105CD File Offset: 0x0000E7CD
	private static string NotInGame
	{
		get
		{
			return "Can't execute while not in game";
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060001BE RID: 446 RVA: 0x000105D4 File Offset: 0x0000E7D4
	private static string NoPlayerError
	{
		get
		{
			return "Player does not Exist!";
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000105DB File Offset: 0x0000E7DB
	public Cmd_Game()
	{
	}

	// Token: 0x0200040A RID: 1034
	public struct GenreTag : IQcSuggestorTag
	{
	}

	// Token: 0x0200040B RID: 1035
	public sealed class GenreAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020B1 RID: 8369 RVA: 0x000C0F7F File Offset: 0x000BF17F
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000C0F88 File Offset: 0x000BF188
		public GenreAttribute()
		{
		}

		// Token: 0x04002137 RID: 8503
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.GenreTag)
		};
	}

	// Token: 0x0200040C RID: 1036
	public class GenreSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000C0FB8 File Offset: 0x000BF1B8
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.GenreTag>();
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000C0FC1 File Offset: 0x000BF1C1
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (GenreTree genreTree in GraphDB.instance.AllGenres)
			{
				list.Add((genreTree.RootNode as GenreRootNode).Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000C1048 File Offset: 0x000BF248
		public GenreSuggestor()
		{
		}
	}

	// Token: 0x0200040D RID: 1037
	public struct FountainAugmentTag : IQcSuggestorTag
	{
	}

	// Token: 0x0200040E RID: 1038
	public sealed class FountainAugmentAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020B7 RID: 8375 RVA: 0x000C1050 File Offset: 0x000BF250
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000C1058 File Offset: 0x000BF258
		public FountainAugmentAttribute()
		{
		}

		// Token: 0x04002138 RID: 8504
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.FountainAugmentTag)
		};
	}

	// Token: 0x0200040F RID: 1039
	public class FountainAugmentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020B9 RID: 8377 RVA: 0x000C1088 File Offset: 0x000BF288
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.FountainAugmentTag>();
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000C1091 File Offset: 0x000BF291
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000C109C File Offset: 0x000BF29C
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AugmentTree augmentTree in GraphDB.instance.FountainMods)
			{
				list.Add(augmentTree.Root.Name.Replace(" ", "_"));
			}
			foreach (AugmentTree augmentTree2 in GraphDB.instance.WorldMods)
			{
				list.Add(augmentTree2.Root.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000C1174 File Offset: 0x000BF374
		public FountainAugmentSuggestor()
		{
		}
	}

	// Token: 0x02000410 RID: 1040
	public struct BindingAugmentTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000411 RID: 1041
	public sealed class BindingAugmentAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x000C117C File Offset: 0x000BF37C
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000C1184 File Offset: 0x000BF384
		public BindingAugmentAttribute()
		{
		}

		// Token: 0x04002139 RID: 8505
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.BindingAugmentTag)
		};
	}

	// Token: 0x02000412 RID: 1042
	public class BindingAugmentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020BF RID: 8383 RVA: 0x000C11B4 File Offset: 0x000BF3B4
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.BindingAugmentTag>();
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000C11BD File Offset: 0x000BF3BD
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000C11C8 File Offset: 0x000BF3C8
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AugmentTree augmentTree in GraphDB.instance.WorldMods)
			{
				list.Add(augmentTree.Root.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000C1240 File Offset: 0x000BF440
		public BindingAugmentSuggestor()
		{
		}
	}

	// Token: 0x02000413 RID: 1043
	public struct ObjectiveAugmentTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000414 RID: 1044
	public sealed class ObjectiveAugmentAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020C3 RID: 8387 RVA: 0x000C1248 File Offset: 0x000BF448
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000C1250 File Offset: 0x000BF450
		public ObjectiveAugmentAttribute()
		{
		}

		// Token: 0x0400213A RID: 8506
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.ObjectiveAugmentTag)
		};
	}

	// Token: 0x02000415 RID: 1045
	public class ObjectiveAugmentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x000C1280 File Offset: 0x000BF480
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.ObjectiveAugmentTag>();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000C1289 File Offset: 0x000BF489
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000C1294 File Offset: 0x000BF494
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AugmentTree augmentTree in GraphDB.instance.BonusObjectives)
			{
				list.Add(augmentTree.Root.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000C130C File Offset: 0x000BF50C
		public ObjectiveAugmentSuggestor()
		{
		}
	}

	// Token: 0x02000416 RID: 1046
	public struct AILayoutOptionTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000417 RID: 1047
	public sealed class AILayoutOptionAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020C9 RID: 8393 RVA: 0x000C1314 File Offset: 0x000BF514
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000C131C File Offset: 0x000BF51C
		public AILayoutOptionAttribute()
		{
		}

		// Token: 0x0400213B RID: 8507
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.AILayoutOptionTag)
		};
	}

	// Token: 0x02000418 RID: 1048
	public class AILayoutOptionSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020CB RID: 8395 RVA: 0x000C134C File Offset: 0x000BF54C
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.AILayoutOptionTag>();
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000C1355 File Offset: 0x000BF555
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000C1360 File Offset: 0x000BF560
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AILayoutRef ailayoutRef in AIManager.instance.DB.Layouts)
			{
				list.Add(ailayoutRef.name);
			}
			return list;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000C13C8 File Offset: 0x000BF5C8
		public AILayoutOptionSuggestor()
		{
		}
	}

	// Token: 0x02000419 RID: 1049
	public struct MapTag : IQcSuggestorTag
	{
	}

	// Token: 0x0200041A RID: 1050
	public sealed class MapAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x000C13D0 File Offset: 0x000BF5D0
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000C13D8 File Offset: 0x000BF5D8
		public MapAttribute()
		{
		}

		// Token: 0x0400213C RID: 8508
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.MapTag)
		};
	}

	// Token: 0x0200041B RID: 1051
	public class MapSuggester : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x000C1408 File Offset: 0x000BF608
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.MapTag>();
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000C1411 File Offset: 0x000BF611
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000C141C File Offset: 0x000BF61C
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (GameMap gameMap in AIManager.instance.Waves.Maps)
			{
				list.Add(gameMap.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000C1494 File Offset: 0x000BF694
		public MapSuggester()
		{
		}
	}

	// Token: 0x0200041C RID: 1052
	public struct VignetteTag : IQcSuggestorTag
	{
	}

	// Token: 0x0200041D RID: 1053
	public sealed class VignetteAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x000C149C File Offset: 0x000BF69C
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000C14A4 File Offset: 0x000BF6A4
		public VignetteAttribute()
		{
		}

		// Token: 0x0400213D RID: 8509
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.VignetteTag)
		};
	}

	// Token: 0x0200041E RID: 1054
	public class VignetteSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020D7 RID: 8407 RVA: 0x000C14D4 File Offset: 0x000BF6D4
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.VignetteTag>();
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000C14DD File Offset: 0x000BF6DD
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000C14E8 File Offset: 0x000BF6E8
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (Vignette vignette in AIManager.instance.Waves.Vignettes)
			{
				list.Add(vignette.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000C1560 File Offset: 0x000BF760
		public VignetteSuggestor()
		{
		}
	}

	// Token: 0x0200041F RID: 1055
	public struct ChallengeTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000420 RID: 1056
	public sealed class ChallengeAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020DB RID: 8411 RVA: 0x000C1568 File Offset: 0x000BF768
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000C1570 File Offset: 0x000BF770
		public ChallengeAttribute()
		{
		}

		// Token: 0x0400213E RID: 8510
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_Game.ChallengeTag)
		};
	}

	// Token: 0x02000421 RID: 1057
	public class ChallengeSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020DD RID: 8413 RVA: 0x000C15A0 File Offset: 0x000BF7A0
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_Game.ChallengeTag>();
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000C15A9 File Offset: 0x000BF7A9
		protected override IQcSuggestion ItemToSuggestion(string challengeName)
		{
			return new RawSuggestion(challengeName, true);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000C15B4 File Offset: 0x000BF7B4
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (MetaDB.BookClubChallenge bookClubChallenge in MetaDB.AllChallenges)
			{
				list.Add(bookClubChallenge.ID);
			}
			return list;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000C1614 File Offset: 0x000BF814
		public ChallengeSuggestor()
		{
		}
	}
}
