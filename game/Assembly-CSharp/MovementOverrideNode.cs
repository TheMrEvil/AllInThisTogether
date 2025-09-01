using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class MovementOverrideNode : ModOverrideNode
{
	// Token: 0x06001C56 RID: 7254 RVA: 0x000AD59E File Offset: 0x000AB79E
	public override bool ShouldOverride(EffectProperties props, Node node)
	{
		return node is AbilityMoveNode && props != null && base.ScopeMatches(props.AbilityType);
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x000AD5BE File Offset: 0x000AB7BE
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Movement Override",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x000AD5E5 File Offset: 0x000AB7E5
	public MovementOverrideNode()
	{
	}

	// Token: 0x04001D28 RID: 7464
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Land", PortLocation.Default)]
	public List<Node> OnLand = new List<Node>();
}
