using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class ModSnippetNode : Node
{
	// Token: 0x06001C31 RID: 7217 RVA: 0x000AC434 File Offset: 0x000AA634
	public void TryTrigger(EntityControl control, EffectProperties props, float chanceMult, AugmentRootNode root)
	{
		if (this.Snippet == null)
		{
			return;
		}
		if (this.CanScope() && !this.ScopeMatches(props.AbilityType))
		{
			return;
		}
		if (!this.SpecificMatches(props))
		{
			return;
		}
		if (!this.RequirementsMet(props))
		{
			return;
		}
		if (!this.Trigger.IsLocalTrigger() && this.HasProcChance)
		{
			float num = 1000f;
			if (UnityEngine.Random.Range(0f, num) > this.ProcChance * chanceMult * num)
			{
				return;
			}
		}
		if (props.Depth >= this.MaxDepth)
		{
			return;
		}
		EffectProperties effectProperties = props;
		if (!this.NoProps)
		{
			effectProperties = props.Copy(false);
			effectProperties.SourceType = ActionSource.Snippet;
			if (this.Loc != null)
			{
				effectProperties.StartLoc = (effectProperties.OutLoc = (this.Loc as PoseNode).GetPose(props));
			}
			float val = this.NumberVal;
			if (this.Num != null)
			{
				NumberNode numberNode = this.Num as NumberNode;
				if (numberNode != null)
				{
					val = numberNode.Evaluate(props);
				}
			}
			effectProperties.SetExtra(EProp.Snip_Input, val);
			effectProperties.Depth++;
			if (!string.IsNullOrEmpty(this.UniqueID))
			{
				effectProperties.InputID = this.UniqueID;
			}
			effectProperties.CauseName = root.GetCauseName();
			effectProperties.CauseID = root.GetCauseID();
		}
		if (this.Trigger.IsLocalTrigger() || this.LocalOnly)
		{
			control.RunSnippetLocal(this.Snippet.RootNode.guid, effectProperties);
			return;
		}
		control.net.ExecuteActionTree(this.Snippet.RootNode.guid, effectProperties);
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x000AC5CC File Offset: 0x000AA7CC
	public void TryTriggerFromProps(EffectProperties props, AugmentRootNode root)
	{
		if (this.Snippet == null)
		{
			return;
		}
		if (props.Depth >= this.MaxDepth)
		{
			return;
		}
		EffectProperties effectProperties = props.Copy(false);
		if (this.Loc != null)
		{
			effectProperties.StartLoc = (effectProperties.OutLoc = (this.Loc as PoseNode).GetPose(props));
		}
		effectProperties.CauseName = root.GetCauseName();
		effectProperties.CauseID = root.GetCauseID();
		effectProperties.SetExtra(EProp.Snip_Input, this.NumberVal);
		effectProperties.Depth++;
		this.Snippet.Root.Apply(effectProperties);
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x000AC678 File Offset: 0x000AA878
	public bool RequirementsMet(EffectProperties props)
	{
		if (!(this.Reqs == null))
		{
			LogicNode logicNode = this.Reqs as LogicNode;
			if (logicNode != null)
			{
				return logicNode.Evaluate(props);
			}
		}
		return true;
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x000AC6AB File Offset: 0x000AA8AB
	public bool ScopeMatches(PlayerAbilityType aType)
	{
		return this.EventScope == PlayerAbilityType.Any || aType == PlayerAbilityType.Any || this.EventScope == aType;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x000AC6C5 File Offset: 0x000AA8C5
	private bool CanScope()
	{
		return this.Trigger.CanScope();
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x000AC6D4 File Offset: 0x000AA8D4
	private bool SpecificMatches(EffectProperties props)
	{
		if (this.Trigger == EventTrigger.RecievedStatus || this.Trigger == EventTrigger.LostStatus)
		{
			if (this.StatusRef.ID == props.GraphIDRef)
			{
				Debug.Log("Checking StatusRef (" + this.StatusRef.Root.EffectName + "): Match Found -> Depth: " + props.Depth.ToString());
			}
			return this.StatusRef != null && this.StatusRef.ID == props.GraphIDRef;
		}
		return this.Trigger != EventTrigger.KeywordTriggered || (props.Keyword != Keyword.None && props.Keyword == this.Keyword);
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x000AC78C File Offset: 0x000AA98C
	private void NewActionGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Snippet = (ActionTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as ActionTree);
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x000AC7B9 File Offset: 0x000AA9B9
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Snippet",
			MinInspectorSize = new Vector2(440f, 0f)
		};
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x000AC7E0 File Offset: 0x000AA9E0
	public ModSnippetNode()
	{
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x000AC80C File Offset: 0x000AAA0C
	// Note: this type is marked as 'beforefieldinit'.
	static ModSnippetNode()
	{
	}

	// Token: 0x04001C7D RID: 7293
	public EventTrigger Trigger;

	// Token: 0x04001C7E RID: 7294
	public PlayerAbilityType EventScope;

	// Token: 0x04001C7F RID: 7295
	public StatusTree StatusRef;

	// Token: 0x04001C80 RID: 7296
	public Keyword Keyword = Keyword.None;

	// Token: 0x04001C81 RID: 7297
	public ActionTree Snippet;

	// Token: 0x04001C82 RID: 7298
	public float NumberVal;

	// Token: 0x04001C83 RID: 7299
	public bool LocalOnly;

	// Token: 0x04001C84 RID: 7300
	public bool NoProps;

	// Token: 0x04001C85 RID: 7301
	public bool HasProcChance = true;

	// Token: 0x04001C86 RID: 7302
	[Range(0f, 1f)]
	public float ProcChance = 1f;

	// Token: 0x04001C87 RID: 7303
	[Range(1f, 10f)]
	public int MaxDepth = 2;

	// Token: 0x04001C88 RID: 7304
	public string UniqueID;

	// Token: 0x04001C89 RID: 7305
	[InputPort(typeof(LogicNode), false, "Requirements", PortLocation.Vertical)]
	[HideInInspector]
	[SerializeField]
	public Node Reqs;

	// Token: 0x04001C8A RID: 7306
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Input", PortLocation.Default)]
	public Node Num;

	// Token: 0x04001C8B RID: 7307
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001C8C RID: 7308
	public static IEnumerable Format = new ValueDropdownList<EventTrigger>
	{
		{
			"None",
			EventTrigger._
		},
		{
			"Entity/Spawn",
			EventTrigger.Spawned
		},
		{
			"Entity/Died",
			EventTrigger.Died
		},
		{
			"Entity/Damage Taken",
			EventTrigger.DamageTaken
		},
		{
			"Entity/Critical Taken",
			EventTrigger.CriticalTaken
		},
		{
			"Entity/Shield Broken",
			EventTrigger.ShieldBroken
		},
		{
			"Entity/Recieved Status",
			EventTrigger.RecievedStatus
		},
		{
			"Entity/Lost Status",
			EventTrigger.LostStatus
		},
		{
			"Entity/Killed Entity",
			EventTrigger.KilledEntity
		},
		{
			"Entity/Heal Recieved",
			EventTrigger.HealRecieved
		},
		{
			"Entity/Revived",
			EventTrigger.Revived
		},
		{
			"Entity/Target Changed",
			EventTrigger.Target_Changed
		},
		{
			"Ability/Ability Used",
			EventTrigger.AbilityUsed
		},
		{
			"Ability/Ability Released",
			EventTrigger.AbilityReleased
		},
		{
			"Ability/Ability Hit",
			EventTrigger.AbilityHit
		},
		{
			"Ability/Player First Hit",
			EventTrigger.PlayerAbilityFirstHit
		},
		{
			"Ability/Damage Done",
			EventTrigger.DamageDone
		},
		{
			"Ability/Critical Done",
			EventTrigger.CriticalDone
		},
		{
			"Ability/Heal Done",
			EventTrigger.HealProvided
		},
		{
			"Ability/AoE Spawned",
			EventTrigger.AoESpawned
		},
		{
			"Ability/Projectile Fired",
			EventTrigger.ProjectileFired
		},
		{
			"Ability/Projectile Impact",
			EventTrigger.ProjectileImpact
		},
		{
			"Ability/Keyword Triggered",
			EventTrigger.KeywordTriggered
		},
		{
			"Time/Every 0.25s",
			EventTrigger.TimePassed_0_25
		},
		{
			"Time/Every 0.5s",
			EventTrigger.TimePassed_0_5
		},
		{
			"Time/Every 1s",
			EventTrigger.TimePassed_1
		},
		{
			"Time/Every 5s",
			EventTrigger.TimePassed_5
		},
		{
			"Time/Every 10s",
			EventTrigger.TimePassed_10
		},
		{
			"Time/Wave Start",
			EventTrigger.WaveStarted
		},
		{
			"Time/Spawn Group End",
			EventTrigger.Wave_SpawnGroupCompleted
		},
		{
			"Time/Wave End",
			EventTrigger.WaveEnded
		},
		{
			"Time/Game Started",
			EventTrigger.Game_Started
		},
		{
			"Time/Game Won",
			EventTrigger.Game_Won
		},
		{
			"Time/Game Lost",
			EventTrigger.Game_Lost
		},
		{
			"Time/Appendix Won",
			EventTrigger.Appendix_Won
		},
		{
			"Time/Appedix Lost",
			EventTrigger.Appendix_Lost
		},
		{
			"World/Game State Changed",
			EventTrigger.GameStateChanged
		},
		{
			"World/Elite Spawned",
			EventTrigger.EliteSpawn
		},
		{
			"World/Elite Died",
			EventTrigger.EliteDied
		},
		{
			"World/Bonus Start",
			EventTrigger.WaveBonusStarted
		},
		{
			"World/Bonus Complete",
			EventTrigger.WaveBonusCompleted
		},
		{
			"World/Bonus Cancel",
			EventTrigger.WaveBonusCanceled
		},
		{
			"World/Map Changed",
			EventTrigger.MapChanged
		},
		{
			"World/Raid Encounter Started",
			EventTrigger.RaidEncounterStarted
		},
		{
			"World/Raid Encounter Completed",
			EventTrigger.RaidEncounterCompleted
		},
		{
			"World/Raid Encounter Reset",
			EventTrigger.RaidEncounterReset
		},
		{
			"Player/Mana Used",
			EventTrigger.ManaUsed
		},
		{
			"Player/Mana Recharged",
			EventTrigger.ManaRecharged
		},
		{
			"Player/Mana Transformed",
			EventTrigger.ManaTransformed
		},
		{
			"Player/Mana Gained",
			EventTrigger.ManaGained
		},
		{
			"Player/Temp Mana Generated",
			EventTrigger.ManaGenerated
		},
		{
			"Player/Jumped",
			EventTrigger.OnJump
		},
		{
			"Player/Landed",
			EventTrigger.Player_OnLand
		},
		{
			"Player/Meta Event",
			EventTrigger.Player_MetaEvent
		},
		{
			"Player/Meta Event (Special)",
			EventTrigger.Player_MetaSpecialEvent
		},
		{
			"Augment/This Chosen",
			EventTrigger.ThisChosen
		},
		{
			"Augment/This Removed",
			EventTrigger.ThisRemoved
		},
		{
			"Augment/This Omitted",
			EventTrigger.This_Omitted
		},
		{
			"Augment/Mod Added",
			EventTrigger.ModAdded
		}
	};
}
