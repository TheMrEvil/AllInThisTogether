using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036F RID: 879
[CreateAssetMenu(order = -5)]
public class StatusTree : GraphTree
{
	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06001D27 RID: 7463 RVA: 0x000B13C3 File Offset: 0x000AF5C3
	public StatusRootNode Root
	{
		get
		{
			if (this.RootNode == null)
			{
				return null;
			}
			return this.RootNode as StatusRootNode;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000B13E0 File Offset: 0x000AF5E0
	public int HashCode
	{
		get
		{
			return base.ID.GetHashCode();
		}
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x000B13ED File Offset: 0x000AF5ED
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(StatusRootNode)) as StatusRootNode);
		base.CreateRootNode();
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x000B1410 File Offset: 0x000AF610
	public override Dictionary<Type, string> GetContextOptions()
	{
		return new Dictionary<Type, string>
		{
			{
				typeof(StatusModNode),
				"Augments/Augment"
			},
			{
				typeof(ModPassiveNode),
				"Augments/Passive"
			},
			{
				typeof(ModSnippetNode),
				"Augments/Snippet"
			},
			{
				typeof(AbilityMoveMod),
				"Modify Move Speed"
			},
			{
				typeof(ModifyPropsNode),
				"Effects/Meta/Modify Props"
			},
			{
				typeof(ApplyOnNode),
				"Effects/Meta/Apply On"
			},
			{
				typeof(ConditionalEffectNode),
				"Effects/Meta/Conditional Effect"
			},
			{
				typeof(CacheNumberNode),
				"Effects/Meta/Cache Number"
			},
			{
				typeof(DelayEffectNode),
				"Effects/Meta/Delay"
			},
			{
				typeof(SubActionNode),
				"Effects/Sub-Action"
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
				"Effects/Modify Mana"
			},
			{
				typeof(ActivateKeywordNode),
				"Effects/Keyword"
			},
			{
				typeof(ApplyStatusNode),
				"Effects/Apply Status"
			},
			{
				typeof(RemoveStatusNode),
				"Effects/Remove Status"
			},
			{
				typeof(ModifyCooldownNode),
				"Effects/Cooldown"
			},
			{
				typeof(PostProcessNode),
				"Effects/Post Processing"
			},
			{
				typeof(AbilityCancelActionNode),
				"Effects/Cancel Action"
			},
			{
				typeof(CancelExplicitNode),
				"Effects/Cancel Graph"
			},
			{
				typeof(AudioEffectNode),
				"Effects/Play Audio"
			},
			{
				typeof(KillEntityNode),
				"Effects/Kill Entity"
			},
			{
				typeof(EntityEventNode),
				"Effects/Entity Event"
			},
			{
				typeof(SpawnAINode),
				"Effects/Spawn AI"
			},
			{
				typeof(ApplyForceNode),
				"Effects/Physics Force"
			},
			{
				typeof(ApplyMoveEffect),
				"Effects/Force Move"
			},
			{
				typeof(ModifyInkNode),
				"Effects/Add Ink"
			},
			{
				typeof(AddAugmentEffectNode),
				"Effects/Add Augment"
			},
			{
				typeof(ApplyTagNode),
				"Effects/Add Tag"
			},
			{
				typeof(RemoveTagNode),
				"Effects/Remove Tag"
			},
			{
				typeof(UpdateStatNode),
				"Effects/Increment Stat"
			},
			{
				typeof(CompleteObjectiveNode),
				"Effects/Events/Complete Bonus Objective"
			},
			{
				typeof(UpdateObjectiveTextNode),
				"Effects/Events/Update Objective Text"
			},
			{
				typeof(UpdateRaidInfoNode),
				"Effects/Events/Update Raid Text"
			},
			{
				typeof(VignetteActionNode),
				"Effects/Events/Vignette Action"
			},
			{
				typeof(CompleteAchievementNode),
				"Effects/Events/Complete Achievement"
			},
			{
				typeof(WorldPingNode),
				"Effects/Events/World Ping"
			},
			{
				typeof(CameraFOVNode),
				"Player/Camera FOV"
			},
			{
				typeof(FogMaterialNode),
				"Player/Fog Override"
			},
			{
				typeof(ReleaseFogNode),
				"Player/Release Fog"
			},
			{
				typeof(PlayAnimEffectNode),
				"Player/Play Anim"
			},
			{
				typeof(StopAnimEffectNode),
				"Player/Stop Anim"
			},
			{
				typeof(RewardScrollNode),
				"Player/Augment Scroll"
			},
			{
				typeof(SpawnVFXNode),
				"Visuals/Spawn VFX"
			},
			{
				typeof(ApplyMaterialNode),
				"Visuals/Apply Material"
			},
			{
				typeof(ModifyMaterialNode),
				"Visuals/Modify Material"
			},
			{
				typeof(RemoveMaterialNode),
				"Visuals/Remove Material"
			},
			{
				typeof(AddMeshFXNode),
				"Visuals/Apply Mesh VFX"
			},
			{
				typeof(RemoveMeshFXNode),
				"Visuals/Remove Mesh VFX"
			}
		};
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x000B1827 File Offset: 0x000AFA27
	public new static GraphTree CreateAndOpenTree(string title)
	{
		return null;
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x000B182A File Offset: 0x000AFA2A
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x000B1831 File Offset: 0x000AFA31
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x000B1838 File Offset: 0x000AFA38
	internal override string GetName()
	{
		if (!(this.RootNode == null))
		{
			return this.Root.EffectName + " [" + (this.Root.ShowInUI ? "T" : "F") + "]";
		}
		return "-";
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x000B188C File Offset: 0x000AFA8C
	internal override string GetDetail()
	{
		if (!(this.RootNode == null))
		{
			return TextParser.EditorParse(this.Root.Description);
		}
		return "-";
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x000B18B2 File Offset: 0x000AFAB2
	public StatusTree()
	{
	}
}
