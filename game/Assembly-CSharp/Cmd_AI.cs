using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using QFSW.QC;
using UnityEngine;

// Token: 0x02000037 RID: 55
[CommandPrefix("ai.")]
public static class Cmd_AI
{
	// Token: 0x0600019B RID: 411 RVA: 0x0000F790 File Offset: 0x0000D990
	[Command("augment-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified Augment to the enemies")]
	private static string AddAugment([Cmd_AI.AIAugmentAttribute] string augment, int count = 1)
	{
		if (!Cmd_AI.IsMasterClient)
		{
			return Cmd_AI.NotMaster;
		}
		if (augment.Length == 0)
		{
			return "Need to specify augment";
		}
		AugmentTree augmentByName = GraphDB.GetAugmentByName(augment);
		if (augmentByName == null)
		{
			return "Couldn't find augment with name [" + augment + "]";
		}
		if (augmentByName.Root.ApplyPlayerTeam)
		{
			GameplayManager.instance.PlayerTeamMods.Add(augmentByName, 1);
		}
		AIManager.GlobalEnemyMods.Add(augmentByName, count);
		GameplayManager.instance.SyncMods();
		return "Added AI Augment [" + augment + "]";
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000F828 File Offset: 0x0000DA28
	[Command("augments-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified Augment to the enemies")]
	private static string AddAugment(List<string> augments, int count = 1)
	{
		if (!Cmd_AI.IsMasterClient)
		{
			return Cmd_AI.NotMaster;
		}
		if (augments.Count == 0)
		{
			return "Need to specify torn pages";
		}
		foreach (string text in augments)
		{
			AugmentTree augmentByName = GraphDB.GetAugmentByName(text.Replace("\"", ""));
			if (!(augmentByName == null))
			{
				if (augmentByName.Root.ApplyPlayerTeam)
				{
					GameplayManager.instance.PlayerTeamMods.Add(augmentByName, 1);
				}
				AIManager.GlobalEnemyMods.Add(augmentByName, count);
			}
		}
		GameplayManager.instance.SyncMods();
		return "Added Torn Pages";
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000F8EC File Offset: 0x0000DAEC
	[Command("augment-reset", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Reset all AI Augments")]
	private static string ResetAugments()
	{
		if (!Cmd_AI.IsMasterClient)
		{
			return Cmd_AI.NotMaster;
		}
		AIManager.instance.ResetAugments();
		return "AI Augments Reset";
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000F90C File Offset: 0x0000DB0C
	[Command("status-add", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Adds the specified Status Effect to your current AI Target")]
	private static string AddStatus([Cmd_AI.StatusAttribute] string status, float duration = 0f, int stacks = 1)
	{
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.currentTarget == null)
		{
			return "Need to target something";
		}
		if (!(PlayerControl.myInstance.currentTarget is AIControl))
		{
			return "Need to target an AI";
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
		PlayerControl.myInstance.currentTarget.net.ApplyStatus(statusByName.RootNode.guid.GetHashCode(), statusByName.Root.IsNegative ? PlayerControl.myInstance.ViewID : PlayerControl.myInstance.currentTarget.ViewID, duration, stacks, true, 0);
		return "Added Status [" + statusByName.Root.EffectName + "] to " + PlayerControl.myInstance.currentTarget.name;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000FA00 File Offset: 0x0000DC00
	[Command("killall", Platform.AllPlatforms, MonoTargetType.Single)]
	public static string KillAll()
	{
		if (!Cmd_AI.IsMasterClient)
		{
			return Cmd_AI.NotMaster;
		}
		int num = 0;
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.health.isDead && entityControl.Targetable)
			{
				num++;
				entityControl.health.DebugKill();
			}
		}
		return "Killed " + num.ToString() + " alive AI";
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000FA94 File Offset: 0x0000DC94
	[Command("killtarget", Platform.AllPlatforms, MonoTargetType.Single)]
	public static string KillTarget()
	{
		if (!Cmd_AI.IsMasterClient)
		{
			return Cmd_AI.NotMaster;
		}
		if (PlayerControl.myInstance == null)
		{
			return "No player";
		}
		if (!(PlayerControl.myInstance.currentTarget == null))
		{
			AIControl aicontrol = PlayerControl.myInstance.currentTarget as AIControl;
			if (aicontrol != null)
			{
				aicontrol.health.DebugKill();
				return "Killed targeted AI";
			}
		}
		return "No target";
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000FAFC File Offset: 0x0000DCFC
	[Command("spawn", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Spawns the specified enemy entity")]
	private static string Spawn([Cmd_AI.AISpawnAttribute] string spawn, int count = 1, bool timeSlice = false)
	{
		if (spawn.Length == 0)
		{
			return "Need to specify an enemy";
		}
		AIData.AIDetails enemy = AIManager.instance.DB.GetEnemy(spawn);
		if (enemy == null)
		{
			return "Couldn't find enemy with name [" + spawn + "]";
		}
		if (count > 1)
		{
			if (timeSlice)
			{
				float num = Mathf.Min(0.1f, 5f / (float)count);
				Action <>9__0;
				for (int i = 0; i < count; i++)
				{
					MonoBehaviour mb = UnityMainThreadDispatcher.Instance();
					Action f;
					if ((f = <>9__0) == null)
					{
						f = (<>9__0 = delegate()
						{
							AIManager.SpawnEnemy(enemy, false);
						});
					}
					mb.Invoke(f, (float)i * num);
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					AIManager.SpawnEnemy(enemy, false);
				}
			}
		}
		else
		{
			Vector3 normalized = (PlayerControl.myInstance.movement.GetPosition() - PlayerControl.myInstance.CameraAimPoint).normalized;
			AIManager.SpawnAIExplicit(enemy.ResourcePath, PlayerControl.myInstance.CameraAimPoint, normalized);
		}
		return "Spawned Enemy [" + spawn + "]";
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000FC18 File Offset: 0x0000DE18
	[Command("spawn-group", Platform.AllPlatforms, MonoTargetType.Single)]
	[CommandDescription("Spawns the specified enemy entities")]
	private static string SpawnGroup([Cmd_AI.AISpawnAttribute] List<string> enemies, List<int> counts, bool atTargetPoint = true)
	{
		if (enemies.Count == 0)
		{
			return "Need to specify enemies";
		}
		for (int i = 0; i < enemies.Count; i++)
		{
			int num = 1;
			if (counts.Count > i)
			{
				num = counts[i];
			}
			AIData.AIDetails enemy = AIManager.instance.DB.GetEnemy(enemies[i]);
			if (enemy == null)
			{
				return "Couldn't find enemy with name [" + enemies[i] + "]";
			}
			for (int j = 0; j < num; j++)
			{
				if (atTargetPoint)
				{
					Vector3 normalized = (PlayerControl.myInstance.movement.GetPosition() - PlayerControl.myInstance.CameraAimPoint).normalized;
					AIManager.SpawnAIExplicit(enemy.ResourcePath, PlayerControl.myInstance.CameraAimPoint, normalized);
				}
				else
				{
					AIManager.SpawnEnemy(enemy, false);
				}
			}
		}
		return "Spawned Enemies";
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000FCED File Offset: 0x0000DEED
	private static bool IsMasterClient
	{
		get
		{
			return !PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000FCFD File Offset: 0x0000DEFD
	private static string NotMaster
	{
		get
		{
			return "Only Host can use this command";
		}
	}

	// Token: 0x02000400 RID: 1024
	public struct AIAugmentTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000401 RID: 1025
	public sealed class AIAugmentAttribute : SuggestorTagAttribute
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x000C0CEA File Offset: 0x000BEEEA
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000C0CF4 File Offset: 0x000BEEF4
		public AIAugmentAttribute()
		{
		}

		// Token: 0x04002132 RID: 8498
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_AI.AIAugmentTag)
		};
	}

	// Token: 0x02000402 RID: 1026
	public class AIAugmentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x0600209F RID: 8351 RVA: 0x000C0D24 File Offset: 0x000BEF24
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_AI.AIAugmentTag>();
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000C0D2D File Offset: 0x000BEF2D
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000C0D38 File Offset: 0x000BEF38
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AugmentTree augmentTree in GraphDB.instance.EnemyMods)
			{
				list.Add(augmentTree.Root.Name.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000C0DB0 File Offset: 0x000BEFB0
		public AIAugmentSuggestor()
		{
		}
	}

	// Token: 0x02000403 RID: 1027
	public struct StatusTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000404 RID: 1028
	public sealed class StatusAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x000C0DB8 File Offset: 0x000BEFB8
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000C0DC0 File Offset: 0x000BEFC0
		public StatusAttribute()
		{
		}

		// Token: 0x04002133 RID: 8499
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_AI.StatusTag)
		};
	}

	// Token: 0x02000405 RID: 1029
	public class StatusSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020A5 RID: 8357 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_AI.StatusTag>();
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000C0DF9 File Offset: 0x000BEFF9
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000C0E04 File Offset: 0x000BF004
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (StatusTree statusTree in GraphDB.instance.AllStatuses)
			{
				list.Add(statusTree.Root.EffectName.Replace(" ", "_"));
			}
			return list;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000C0E7C File Offset: 0x000BF07C
		public StatusSuggestor()
		{
		}
	}

	// Token: 0x02000406 RID: 1030
	public struct AISpawnTag : IQcSuggestorTag
	{
	}

	// Token: 0x02000407 RID: 1031
	public sealed class AISpawnAttribute : SuggestorTagAttribute
	{
		// Token: 0x060020A9 RID: 8361 RVA: 0x000C0E84 File Offset: 0x000BF084
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000C0E8C File Offset: 0x000BF08C
		public AISpawnAttribute()
		{
		}

		// Token: 0x04002134 RID: 8500
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(Cmd_AI.AISpawnTag)
		};
	}

	// Token: 0x02000408 RID: 1032
	public class AISpawnSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x060020AB RID: 8363 RVA: 0x000C0EBC File Offset: 0x000BF0BC
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<Cmd_AI.AISpawnTag>();
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000C0EC5 File Offset: 0x000BF0C5
		protected override IQcSuggestion ItemToSuggestion(string abilityName)
		{
			return new RawSuggestion(abilityName, true);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000C0ED0 File Offset: 0x000BF0D0
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			List<string> list = new List<string>();
			foreach (AIData.AIDetails aidetails in AIManager.instance.DB.Enemies)
			{
				if (aidetails != null && !(aidetails.Reference == null))
				{
					list.Add(aidetails.Reference.name.Replace(" ", "_"));
				}
			}
			return list;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000C0F60 File Offset: 0x000BF160
		public AISpawnSuggestor()
		{
		}
	}

	// Token: 0x02000409 RID: 1033
	[CompilerGenerated]
	private sealed class <>c__DisplayClass6_0
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x000C0F68 File Offset: 0x000BF168
		public <>c__DisplayClass6_0()
		{
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000C0F70 File Offset: 0x000BF170
		internal void <Spawn>b__0()
		{
			AIManager.SpawnEnemy(this.enemy, false);
		}

		// Token: 0x04002135 RID: 8501
		public AIData.AIDetails enemy;

		// Token: 0x04002136 RID: 8502
		public Action <>9__0;
	}
}
