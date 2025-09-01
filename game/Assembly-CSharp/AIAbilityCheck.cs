using System;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class AIAbilityCheck : AITestNode
{
	// Token: 0x06001B9F RID: 7071 RVA: 0x000AA084 File Offset: 0x000A8284
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.Entity);
		if (!entity2)
		{
			return false;
		}
		Ability ability = entity2.GetAbility(this.Ability.RootNode.guid);
		if (ability == null)
		{
			return false;
		}
		AIAbilityCheck.TestType test = this.Test;
		if (test != AIAbilityCheck.TestType.IsCasting)
		{
			return test == AIAbilityCheck.TestType.CanUse && !entity2.IsUsingAbility(this.Ability, false) && ability.currentCD <= 0f;
		}
		return entity2.IsUsingAbility(this.Ability, false);
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000AA105 File Offset: 0x000A8305
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Ability Test";
		inspectorProps.MinInspectorSize = new Vector2(270f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000AA12D File Offset: 0x000A832D
	public AIAbilityCheck()
	{
	}

	// Token: 0x04001BF9 RID: 7161
	public AITestNode.TestTarget Entity;

	// Token: 0x04001BFA RID: 7162
	public AbilityTree Ability;

	// Token: 0x04001BFB RID: 7163
	public AIAbilityCheck.TestType Test;

	// Token: 0x0200065D RID: 1629
	public enum TestType
	{
		// Token: 0x04002B2C RID: 11052
		IsCasting,
		// Token: 0x04002B2D RID: 11053
		CanUse
	}
}
