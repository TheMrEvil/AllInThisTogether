using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A4 RID: 676
[CreateAssetMenu(order = -5)]
public class AbilityTree : GraphTree
{
	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06001984 RID: 6532 RVA: 0x0009F129 File Offset: 0x0009D329
	public AbilityRootNode Root
	{
		get
		{
			return this.RootNode as AbilityRootNode;
		}
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x0009F136 File Offset: 0x0009D336
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(AbilityRootNode)) as AbilityRootNode);
		base.CreateRootNode();
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x0009F15C File Offset: 0x0009D35C
	public override Dictionary<Type, string> GetContextOptions()
	{
		Dictionary<Type, string> dictionary = new Dictionary<Type, string>();
		dictionary.Add(typeof(AbilityDelayNode), "Timers/Delay");
		dictionary.Add(typeof(AbilityRepeatNode), "Timers/Repeat");
		dictionary.Add(typeof(AbilityDurationNode), "Timers/Over Time");
		dictionary.Add(typeof(AbilityOnceNode), "Timers/Run Once");
		dictionary.Add(typeof(CancelAbilityNode), "Timers/End Ability");
		dictionary.Add(typeof(ConditionalAbilityNode), "Meta/Conditional Run");
		dictionary.Add(typeof(ConditionalEffectNode), "Meta/Conditional Effect");
		dictionary.Add(typeof(AbilitySelectorNode), "Meta/Threshold Selector");
		dictionary.Add(typeof(AbilityChanceNode), "Meta/Chance Event");
		dictionary.Add(typeof(ModifyPropsNode), "Meta/Modify Props");
		dictionary.Add(typeof(CacheNumberNode), "Meta/Cache Number");
		dictionary.Add(typeof(SubActionNode), "Effects/Run Action");
		dictionary.Add(typeof(AbilityCancelActionNode), "Effects/Cancel Action");
		dictionary.Add(typeof(AbilityCancelTimedNode), "Effects/Cancel Ability Node");
		dictionary.Add(typeof(CancelExplicitNode), "Effects/Cancel Action Graph");
		dictionary.Add(typeof(SpawnVFXNode), "Effects/Spawn VFX");
		dictionary.Add(typeof(AddMeshFXNode), "Effects/Spawn MeshFX");
		dictionary.Add(typeof(RemoveMeshFXNode), "Effects/Remove MeshFX");
		dictionary.Add(typeof(AbilityMoveMod), "Effects/Movement Modifier");
		dictionary.Add(typeof(ActivateKeywordNode), "Effects/Keyword");
		dictionary.Add(typeof(RotateNode), "Effects/Rotate Caster");
		dictionary.Add(typeof(AbilityAnimNode), "Effects/Play Animation");
		dictionary.Add(typeof(AbilityStopAnimNode), "Effects/Stop Animation");
		dictionary.Add(typeof(DelayEffectNode), "Effects/Delay Effect");
		dictionary.Add(typeof(AbilityMoveNode), "Effects/Move Caster");
		dictionary.Add(typeof(ApplyForceNode), "Effects/Apply Force");
		dictionary.Add(typeof(ModifyCooldownNode), "Effects/Modify Cooldown");
		dictionary.Add(typeof(ModifyManaNode), "Effects/Modify Mana");
		dictionary.Add(typeof(ApplyStatusNode), "Effects/Apply Status");
		dictionary.Add(typeof(RemoveStatusNode), "Effects/Remove Status");
		dictionary.Add(typeof(ApplyDamageNode), "Effects/Damage");
		dictionary.Add(typeof(ApplyTagNode), "Effects/Add Tag");
		dictionary.Add(typeof(RemoveTagNode), "Effects/Remove Tag");
		dictionary.Add(typeof(AudioEffectNode), "Effects/Play Audio Clip");
		dictionary.Add(typeof(RaidSceneEventNode), "Effects/Raid Scene Event");
		dictionary.Add(typeof(WorldPingNode), "Effects/World Ping");
		if (this.RootNode != null && (this.RootNode as AbilityRootNode).Usage.IsPlayerAbility)
		{
			dictionary.Add(typeof(CameraEffectNode), "Player/Camera Shake");
			dictionary.Add(typeof(CameraFOVNode), "Player/Camera FOV");
			dictionary.Add(typeof(PlayAnimEffectNode), "Player/Anim Trigger");
			dictionary.Add(typeof(PlayerChangeAbilityNode), "Player/Change Ability");
		}
		return dictionary;
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x0009F4DD File Offset: 0x0009D6DD
	public new static GraphTree CreateAndOpenTree(string title)
	{
		return null;
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x0009F4E0 File Offset: 0x0009D6E0
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x0009F4E7 File Offset: 0x0009D6E7
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x0009F4EE File Offset: 0x0009D6EE
	internal override string GetName()
	{
		if (!(this.RootNode == null))
		{
			return this.Root.Name + string.Format(" ({0})", this.Root.AbilityType);
		}
		return "-";
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x0009F52E File Offset: 0x0009D72E
	internal override string GetDetail()
	{
		if (!(this.RootNode == null))
		{
			return TextParser.EditorParse(this.Root.Usage.AbilityMetadata.Detail);
		}
		return "-";
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0009F55E File Offset: 0x0009D75E
	public static implicit operator AbilityRootNode(AbilityTree t)
	{
		return t.RootNode as AbilityRootNode;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x0009F56B File Offset: 0x0009D76B
	public AbilityTree()
	{
	}
}
