using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class AbilityCancelTimedNode : EffectNode
{
	// Token: 0x06001940 RID: 6464 RVA: 0x0009D770 File Offset: 0x0009B970
	internal override void Apply(EffectProperties properties)
	{
		foreach (Node node in this.actions)
		{
			((AbilityNode)node).Cancel(properties);
		}
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x0009D7C8 File Offset: 0x0009B9C8
	internal override AbilityState Run(EffectProperties props)
	{
		foreach (Node node in this.actions)
		{
			((AbilityNode)node).Cancel(props);
		}
		return AbilityState.Finished;
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x0009D820 File Offset: 0x0009BA20
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cancel Ability Node",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x0009D847 File Offset: 0x0009BA47
	public AbilityCancelTimedNode()
	{
	}

	// Token: 0x04001964 RID: 6500
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> actions = new List<Node>();
}
