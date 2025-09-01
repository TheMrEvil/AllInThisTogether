using System;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class AbilityStopAnimNode : AbilityNode
{
	// Token: 0x06001981 RID: 6529 RVA: 0x0009F0D9 File Offset: 0x0009D2D9
	internal override AbilityState Run(EffectProperties props)
	{
		props.SourceControl.display.StopCurrentAbilityAnim();
		return AbilityState.Finished;
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x0009F0EC File Offset: 0x0009D2EC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stop Animation",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(250f, 0f),
			ShowInspectorView = false
		};
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x0009F121 File Offset: 0x0009D321
	public AbilityStopAnimNode()
	{
	}
}
