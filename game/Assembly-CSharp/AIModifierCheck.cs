using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class AIModifierCheck : AITestNode
{
	// Token: 0x06001BB0 RID: 7088 RVA: 0x000AA500 File Offset: 0x000A8700
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.TestingEntity);
		if (entity2 == null)
		{
			return false;
		}
		switch (this.Test)
		{
		case AIModifierCheck.TestType.HasAugment:
		{
			EntityControl entityControl = entity2;
			AugmentTree augment = this.Augment;
			return entityControl.HasAugment((augment != null) ? augment.ID : null, true);
		}
		case AIModifierCheck.TestType.HasEntityPassive:
			return entity2.Augment.HasPassive(this.EntityPassive);
		case AIModifierCheck.TestType.HasAbilityPassive:
			return entity2.Augment.HasPassive(new ValueTuple<PlayerAbilityType, Passive.AbilityValue>(PlayerAbilityType.Any, this.AbilityPassive));
		default:
			return false;
		}
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000AA58F File Offset: 0x000A878F
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Augment Test";
		inspectorProps.MinInspectorSize = new Vector2(270f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000AA5B7 File Offset: 0x000A87B7
	public AIModifierCheck()
	{
	}

	// Token: 0x04001C0A RID: 7178
	public AIModifierCheck.TestType Test;

	// Token: 0x04001C0B RID: 7179
	public AITestNode.TestTarget TestingEntity;

	// Token: 0x04001C0C RID: 7180
	public AugmentTree Augment;

	// Token: 0x04001C0D RID: 7181
	public Passive.EntityValue EntityPassive;

	// Token: 0x04001C0E RID: 7182
	public Passive.AbilityValue AbilityPassive;

	// Token: 0x02000660 RID: 1632
	public enum TestType
	{
		// Token: 0x04002B35 RID: 11061
		HasAugment,
		// Token: 0x04002B36 RID: 11062
		HasEntityPassive,
		// Token: 0x04002B37 RID: 11063
		HasAbilityPassive
	}
}
