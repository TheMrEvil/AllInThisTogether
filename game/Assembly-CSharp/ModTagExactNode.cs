using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class ModTagExactNode : ModTagNode
{
	// Token: 0x06001C73 RID: 7283 RVA: 0x000ADA20 File Offset: 0x000ABC20
	public override bool Validate(EntityControl control)
	{
		ModTagExactNode.MatchTest test = this.Test;
		if (test == ModTagExactNode.MatchTest.Augment)
		{
			AugmentTree augmentTest = this.AugmentTest;
			return control.HasAugment((augmentTest != null) ? augmentTest.ID : null, true);
		}
		if (test != ModTagExactNode.MatchTest.Ability)
		{
			return false;
		}
		AbilityTree abilityTree = this.AbilityTree;
		return control.HasAbility((abilityTree != null) ? abilityTree.ID : null);
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x000ADA71 File Offset: 0x000ABC71
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Exact Match";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x000ADA99 File Offset: 0x000ABC99
	public ModTagExactNode()
	{
	}

	// Token: 0x04001D45 RID: 7493
	public ModTagExactNode.MatchTest Test;

	// Token: 0x04001D46 RID: 7494
	public AugmentTree AugmentTest;

	// Token: 0x04001D47 RID: 7495
	public AbilityTree AbilityTree;

	// Token: 0x0200066C RID: 1644
	public enum MatchTest
	{
		// Token: 0x04002B83 RID: 11139
		Augment,
		// Token: 0x04002B84 RID: 11140
		Ability
	}
}
