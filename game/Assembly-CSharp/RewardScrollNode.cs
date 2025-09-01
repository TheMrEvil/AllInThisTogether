using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class RewardScrollNode : EffectNode
{
	// Token: 0x06001A81 RID: 6785 RVA: 0x000A4918 File Offset: 0x000A2B18
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (!this.ShouldApply(properties, applicationEntity) && !this.AlwaysReward)
		{
			return;
		}
		Vector3 pos = Vector3.zero;
		if (this.Loc != null)
		{
			pos = (this.Loc as LocationNode).GetLocation(properties).GetPosition(properties);
		}
		else if (properties.OutLoc != null)
		{
			pos = properties.GetOutputPoint();
		}
		else
		{
			pos = properties.GetOrigin();
		}
		if (this.PickThree)
		{
			GoalManager.instance.CreatePageSelect(this.Filter, pos);
			return;
		}
		if (this.Augment == null)
		{
			GoalManager.instance.GiveAugmentScroll(this.Filter, pos);
			return;
		}
		GoalManager.instance.CreateScroll(this.Augment, pos);
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x000A49D9 File Offset: 0x000A2BD9
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Give Scroll",
			MinInspectorSize = new Vector2(160f, 0f)
		};
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x000A4A00 File Offset: 0x000A2C00
	public RewardScrollNode()
	{
	}

	// Token: 0x04001AF3 RID: 6899
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Loc", PortLocation.Header)]
	public Node Loc;

	// Token: 0x04001AF4 RID: 6900
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001AF5 RID: 6901
	public bool AlwaysReward;

	// Token: 0x04001AF6 RID: 6902
	public bool PickThree;

	// Token: 0x04001AF7 RID: 6903
	public AugmentTree Augment;

	// Token: 0x04001AF8 RID: 6904
	public AugmentFilter Filter;
}
