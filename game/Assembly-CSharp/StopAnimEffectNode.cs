using System;

// Token: 0x020002E5 RID: 741
public class StopAnimEffectNode : EffectNode
{
	// Token: 0x06001AAE RID: 6830 RVA: 0x000A60A6 File Offset: 0x000A42A6
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stop Animation"
		};
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x000A60B8 File Offset: 0x000A42B8
	internal override void Apply(EffectProperties properties)
	{
		properties.AffectedControl.display.StopCurrentAbilityAnim();
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x000A60CA File Offset: 0x000A42CA
	public StopAnimEffectNode()
	{
	}
}
