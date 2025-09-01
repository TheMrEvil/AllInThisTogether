using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AB RID: 683
[CreateAssetMenu(order = -5)]
public class ActionTree : GraphTree
{
	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0009FE37 File Offset: 0x0009E037
	public ActionRootNode Root
	{
		get
		{
			return this.RootNode as ActionRootNode;
		}
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x0009FE44 File Offset: 0x0009E044
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(ActionRootNode)) as ActionRootNode);
		base.CreateRootNode();
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x0009FE68 File Offset: 0x0009E068
	public override Dictionary<Type, string> GetContextOptions()
	{
		return new Dictionary<Type, string>
		{
			{
				typeof(SubActionNode),
				"Meta/Fire Sub-Action"
			},
			{
				typeof(ActionThresholdNode),
				"Meta/Threshold Selector"
			},
			{
				typeof(EffectGroupNode),
				"Meta/Effect Group"
			},
			{
				typeof(DelayEffectNode),
				"Meta/Delay"
			},
			{
				typeof(MultiEffectNode),
				"Meta/Multi-Invoke"
			},
			{
				typeof(ChanceEffectNode),
				"Meta/Random Chance"
			},
			{
				typeof(ApplyOnNode),
				"Meta/Apply On"
			},
			{
				typeof(ConditionalEffectNode),
				"Meta/Conditional Effect"
			},
			{
				typeof(ModifyPropsNode),
				"Meta/Modify Props"
			},
			{
				typeof(CacheNumberNode),
				"Meta/Cache Number"
			},
			{
				typeof(SpawnVFXNode),
				"Effects/Spawn VFX"
			},
			{
				typeof(AddMeshFXNode),
				"Effects/Spawn MeshFX"
			},
			{
				typeof(SpawnGameObjectNode),
				"Effects/Spawn Prefab"
			},
			{
				typeof(ApplyForceNode),
				"Effects/Physics Force"
			},
			{
				typeof(ApplyDamageNode),
				"Effects/Damage"
			},
			{
				typeof(HealNode),
				"Effects/Heal"
			},
			{
				typeof(ModifyManaNode),
				"Effects/Mana"
			},
			{
				typeof(ModifyShieldNode),
				"Effects/Shield"
			},
			{
				typeof(ActivateKeywordNode),
				"Effects/Keyword"
			},
			{
				typeof(ReviveEntityNode),
				"Effects/Revive"
			},
			{
				typeof(TeleportEffectNode),
				"Effects/Teleport"
			},
			{
				typeof(ApplyMoveEffect),
				"Effects/Force Move"
			},
			{
				typeof(ApplyStatusNode),
				"Effects/Add Status Effect"
			},
			{
				typeof(RemoveStatusNode),
				"Effects/Remove Status Effect"
			},
			{
				typeof(ModifyCooldownNode),
				"Effects/Cooldown"
			},
			{
				typeof(AudioEffectNode),
				"Effects/Play Audio Clip"
			},
			{
				typeof(AbilityCancelActionNode),
				"Effects/Cancel Effect"
			},
			{
				typeof(CancelExplicitNode),
				"Effects/Cancel Graph"
			},
			{
				typeof(StopAbilityNode),
				"Effects/Cancel Ability"
			},
			{
				typeof(CameraEffectNode),
				"Effects/Player/Camera Shake"
			},
			{
				typeof(CameraFOVNode),
				"Effects/Player/Camera FOV"
			},
			{
				typeof(PostProcessNode),
				"Effects/Player/Post Process Effect"
			},
			{
				typeof(TriggerAbilityActionNode),
				"Effects/Player/Trigger Ability Action"
			},
			{
				typeof(PlayerChangeAbilityNode),
				"Effects/Player/Change Ability"
			},
			{
				typeof(ModifyInkNode),
				"Effects/Player/Add Ink"
			},
			{
				typeof(AddAugmentEffectNode),
				"Effects/Player/Add Augment"
			},
			{
				typeof(RewardScrollNode),
				"Effects/Player/Augment Scroll"
			},
			{
				typeof(SpawnPickupNode),
				"Effects/Player/Spawn Pickup"
			},
			{
				typeof(UpdateStatNode),
				"Effects/Player/Increment Stat"
			},
			{
				typeof(CompleteQuestNode),
				"Effects/Player/Complete Quest"
			},
			{
				typeof(CompleteAchievementNode),
				"Effects/Player/Complete Achievement"
			},
			{
				typeof(VignetteActionNode),
				"Effects/Game Loop/Vignette Action"
			},
			{
				typeof(CompleteObjectiveNode),
				"Effects/Game Loop/Complete Bonus Objective"
			},
			{
				typeof(UpdateObjectiveTextNode),
				"Effects/Game Loop/Update Objective Text"
			},
			{
				typeof(UpdateRaidInfoNode),
				"Effects/Game Loop/Update Raid Text"
			},
			{
				typeof(RaidSceneEventNode),
				"Effects/Game Loop/Raid Scene Event"
			},
			{
				typeof(WorldPingNode),
				"Effects/Game Loop/World Ping"
			},
			{
				typeof(SpawnProjectileEffectNode),
				"Projectile/Spawn Projectile"
			},
			{
				typeof(ProjectileNode),
				"Projectile/Projectile Core"
			},
			{
				typeof(ProjectileMoveProps),
				"Projectile/Movement"
			},
			{
				typeof(ProjectileMoveProps_Gravity),
				"Projectile/Modules/Gravity"
			},
			{
				typeof(ProjectileMoveProps_Arc),
				"Projectile/Modules/Arc"
			},
			{
				typeof(ProjectileMoveProps_Homing),
				"Projectile/Modules/Homing"
			},
			{
				typeof(ProjectileMoveProps_Random),
				"Projectile/Modules/Random"
			},
			{
				typeof(ProjectileLifetimeProps),
				"Projectile/Lifetime"
			},
			{
				typeof(ProjectileInteractionNode),
				"Projectile/Interaction"
			},
			{
				typeof(SpawnAoENode),
				"AoE/Spawn AoE"
			},
			{
				typeof(AoENode),
				"AoE/AoE Core"
			},
			{
				typeof(AoEApplicationNode),
				"AoE/Application"
			},
			{
				typeof(SpawnBeamNode),
				"Beam/Spawn Beam"
			},
			{
				typeof(BeamNode),
				"Beam/Beam Core"
			},
			{
				typeof(SpawnAINode),
				"Entities/Spawn AI"
			},
			{
				typeof(EntityEventNode),
				"Entities/Entity Event"
			},
			{
				typeof(ApplyTagNode),
				"Entities/Add Tag"
			},
			{
				typeof(RemoveTagNode),
				"Entities/Remove Tag"
			},
			{
				typeof(KillEntityNode),
				"Entities/Kill Entity"
			}
		};
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000A03E4 File Offset: 0x0009E5E4
	public new static GraphTree CreateAndOpenTree(string title)
	{
		return null;
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x000A03E7 File Offset: 0x0009E5E7
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x000A03EE File Offset: 0x0009E5EE
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x000A03F5 File Offset: 0x0009E5F5
	public ActionTree()
	{
	}
}
