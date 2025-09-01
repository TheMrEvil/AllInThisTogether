using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class ProjectileInteractionNode : Node
{
	// Token: 0x06001ACC RID: 6860 RVA: 0x000A6A41 File Offset: 0x000A4C41
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		return base.Clone(alreadyCloned, fullClone) as ProjectileInteractionNode;
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x000A6A50 File Offset: 0x000A4C50
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Proj Interaction",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x000A6A7E File Offset: 0x000A4C7E
	public bool HasMultipleInteractions()
	{
		return this.PierceCount > 0;
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x000A6A89 File Offset: 0x000A4C89
	public bool CanFilterInteractions()
	{
		return this.InteractsWith != EffectInteractsWith.Environment;
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x000A6A97 File Offset: 0x000A4C97
	public ProjectileInteractionNode()
	{
	}

	// Token: 0x04001B61 RID: 7009
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Impact", PortLocation.Default)]
	public List<Node> OnImpact = new List<Node>();

	// Token: 0x04001B62 RID: 7010
	[HideInInspector]
	[SerializeField]
	[ShowPort("HasMultipleInteractions")]
	[OutputPort(typeof(EffectNode), true, "On Final Impact", PortLocation.Default)]
	public List<Node> OnFinalImpact = new List<Node>();

	// Token: 0x04001B63 RID: 7011
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "?", PortLocation.Vertical)]
	[ShowPort("CanFilterInteractions")]
	public Node Filter;

	// Token: 0x04001B64 RID: 7012
	public EffectInteractsWith InteractsWith;

	// Token: 0x04001B65 RID: 7013
	public bool IgnoreSelf;

	// Token: 0x04001B66 RID: 7014
	public int PierceCount;

	// Token: 0x04001B67 RID: 7015
	public float InteractionDelay;

	// Token: 0x04001B68 RID: 7016
	[Header("Extra Impacts")]
	public ProjectileInteractionNode.VelBehaviour Result;

	// Token: 0x04001B69 RID: 7017
	public float VelocityDampen = 1f;

	// Token: 0x04001B6A RID: 7018
	public float OutwardForceAdd;

	// Token: 0x04001B6B RID: 7019
	public bool AngleAffectDamp;

	// Token: 0x02000652 RID: 1618
	public enum VelBehaviour
	{
		// Token: 0x04002B06 RID: 11014
		Pierce,
		// Token: 0x04002B07 RID: 11015
		Reflect
	}
}
