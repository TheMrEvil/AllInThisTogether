using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class AbilityCancelActionNode : EffectNode
{
	// Token: 0x0600193C RID: 6460 RVA: 0x0009D6AA File Offset: 0x0009B8AA
	internal override AbilityState Run(EffectProperties props)
	{
		this.Apply(props);
		return AbilityState.Finished;
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x0009D6B4 File Offset: 0x0009B8B4
	internal override void Apply(EffectProperties properties)
	{
		foreach (Node node in this.actions)
		{
			EffectNode effectNode = (EffectNode)node;
			AIFireActionNode aifireActionNode = effectNode as AIFireActionNode;
			if (aifireActionNode != null)
			{
				aifireActionNode.CancelAction(properties);
			}
			else
			{
				SubActionNode subActionNode = effectNode as SubActionNode;
				if (subActionNode != null)
				{
					subActionNode.CancelAction(properties);
				}
				else
				{
					effectNode.TryCancel(properties);
				}
			}
		}
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0009D734 File Offset: 0x0009B934
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cancel Action",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x0009D75B File Offset: 0x0009B95B
	public AbilityCancelActionNode()
	{
	}

	// Token: 0x04001963 RID: 6499
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "", PortLocation.Header)]
	public List<Node> actions = new List<Node>();
}
