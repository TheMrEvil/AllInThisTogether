using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class CompleteObjectiveNode : EffectNode
{
	// Token: 0x06001A22 RID: 6690 RVA: 0x000A2B10 File Offset: 0x000A0D10
	internal override void Apply(EffectProperties properties)
	{
		Vector3 point = properties.GetOrigin();
		if (this.Loc != null)
		{
			LocationNode locationNode = this.Loc as LocationNode;
			if (locationNode != null)
			{
				point = locationNode.GetLocation(properties).GetPosition(properties);
			}
		}
		if (this.Event == CompleteObjectiveNode.BonusEvent.Complete)
		{
			GoalManager.TryCompleteBonusObjective(point);
			return;
		}
		GoalManager.TryCancelBonusObjective();
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x000A2B63 File Offset: 0x000A0D63
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Complete Bonus",
			MinInspectorSize = new Vector2(220f, 0f)
		};
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000A2B8A File Offset: 0x000A0D8A
	public CompleteObjectiveNode()
	{
	}

	// Token: 0x04001A97 RID: 6807
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Loc", PortLocation.Header)]
	public Node Loc;

	// Token: 0x04001A98 RID: 6808
	public CompleteObjectiveNode.BonusEvent Event;

	// Token: 0x02000648 RID: 1608
	public enum BonusEvent
	{
		// Token: 0x04002ACE RID: 10958
		Complete,
		// Token: 0x04002ACF RID: 10959
		Cancel
	}
}
