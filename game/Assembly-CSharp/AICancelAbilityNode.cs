using System;
using System.Collections.Generic;

// Token: 0x0200030D RID: 781
public class AICancelAbilityNode : AIActionNode
{
	// Token: 0x06001B47 RID: 6983 RVA: 0x000A8C24 File Offset: 0x000A6E24
	internal override AILogicState Run(AIControl entity)
	{
		if (this.CancelAll)
		{
			AILogicState ailogicState = AILogicState.Fail;
			foreach (Ability ability in entity.Abilities)
			{
				AILogicState ailogicState2 = this.ReleaseAbility(entity, ability.AbilityTree);
				if (ailogicState2 == AILogicState.Running)
				{
					ailogicState = AILogicState.Running;
				}
				if (ailogicState != AILogicState.Running && ailogicState2 == AILogicState.Success)
				{
					ailogicState = AILogicState.Success;
				}
			}
			if (ailogicState != AILogicState.Fail)
			{
				return ailogicState;
			}
		}
		else
		{
			AILogicState ailogicState3 = this.ReleaseAbility(entity, this.Ability);
			if (ailogicState3 != AILogicState.Running)
			{
				return ailogicState3;
			}
		}
		if (!this.SucceedIfNotCasting)
		{
			return AILogicState.Fail;
		}
		return AILogicState.Success;
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000A8CC0 File Offset: 0x000A6EC0
	private AILogicState ReleaseAbility(AIControl entity, AbilityTree ability)
	{
		if (this.Ability == null)
		{
			return AILogicState.Fail;
		}
		if (!entity.IsUsingAbility(this.Ability, false))
		{
			return AILogicState.Fail;
		}
		if (!entity.IsReleasingAbility(this.Ability))
		{
			(entity.net as AINetworked).AbilityReleased(this.Ability);
		}
		if (!entity.IsReleasingAbility(this.Ability))
		{
			return AILogicState.Success;
		}
		return AILogicState.Running;
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x000A8D28 File Offset: 0x000A6F28
	public override void ClearState(List<Node> wasChecked = null)
	{
		base.ClearState(wasChecked);
		this.isReleasing = false;
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x000A8D38 File Offset: 0x000A6F38
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 280f;
		inspectorProps.Title = "Cancel Ability";
		return inspectorProps;
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x000A8D5A File Offset: 0x000A6F5A
	public AICancelAbilityNode()
	{
	}

	// Token: 0x04001BC2 RID: 7106
	public AbilityTree Ability;

	// Token: 0x04001BC3 RID: 7107
	public bool SucceedIfNotCasting;

	// Token: 0x04001BC4 RID: 7108
	public bool CancelAll;

	// Token: 0x04001BC5 RID: 7109
	private bool isReleasing;
}
