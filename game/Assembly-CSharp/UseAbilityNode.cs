using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class UseAbilityNode : AIActionNode
{
	// Token: 0x06001B9A RID: 7066 RVA: 0x000A9EEC File Offset: 0x000A80EC
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Ability == null)
		{
			return AILogicState.Fail;
		}
		AbilityType abilityType = (this.Ability.RootNode as AbilityRootNode).AbilityType;
		if (!entity.CanUseAbilities())
		{
			return AILogicState.Fail;
		}
		if (entity.IsUsingAbility(this.Ability, false))
		{
			if (abilityType == AbilityType.Instant)
			{
				return AILogicState.Fail;
			}
			return AILogicState.Running;
		}
		else
		{
			if (this.didUse)
			{
				this.didUse = false;
				return AILogicState.Success;
			}
			if (!entity.CanUseAbility(this.Ability.RootNode.guid))
			{
				return AILogicState.Fail;
			}
			this.didUse = (abilityType == AbilityType.Channel_Active);
			Vector3 targetPt = (entity.currentTarget == null) ? entity.movement.GetPosition() : entity.currentTarget.movement.GetPosition();
			int seed = UnityEngine.Random.Range(0, int.MaxValue);
			if (entity.net != null)
			{
				AINetworked ainetworked = entity.net as AINetworked;
				if (ainetworked != null)
				{
					ainetworked.AbilityActivated(this.Ability, targetPt, seed);
				}
			}
			if (this.StopMoving && entity.Movement.IsMoving())
			{
				entity.Movement.CancelMovement();
			}
			if (abilityType == AbilityType.Instant || abilityType == AbilityType.Channel_Passive)
			{
				return AILogicState.Success;
			}
			return AILogicState.Running;
		}
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x000AA001 File Offset: 0x000A8201
	public override List<AbilityTree> CollectActions(List<AbilityTree> list, List<Node> wasChecked = null)
	{
		if (wasChecked == null)
		{
			wasChecked = new List<Node>();
		}
		if (wasChecked.Contains(this))
		{
			return list;
		}
		wasChecked.Add(this);
		list.Add(this.Ability);
		return list;
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x000AA02C File Offset: 0x000A822C
	private void NewAbilityGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Ability = (AbilityTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as AbilityTree);
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x000AA059 File Offset: 0x000A8259
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 220f;
		inspectorProps.Title = "Use Ability";
		return inspectorProps;
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x000AA07B File Offset: 0x000A827B
	public UseAbilityNode()
	{
	}

	// Token: 0x04001BF5 RID: 7157
	public AbilityTree Ability;

	// Token: 0x04001BF6 RID: 7158
	[NonSerialized]
	private float cdValue;

	// Token: 0x04001BF7 RID: 7159
	public bool StopMoving;

	// Token: 0x04001BF8 RID: 7160
	private bool didUse;
}
