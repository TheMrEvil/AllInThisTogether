using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class AIChangeBrainNode : AIActionNode
{
	// Token: 0x06001B4C RID: 6988 RVA: 0x000A8D62 File Offset: 0x000A6F62
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Brain == null)
		{
			return AILogicState.Fail;
		}
		entity.Net.ChangeBrain(this.Brain);
		return AILogicState.Success;
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000A8D86 File Offset: 0x000A6F86
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Change Brain",
			MinInspectorSize = new Vector2(100f, 0f)
		};
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x000A8DAD File Offset: 0x000A6FAD
	public AIChangeBrainNode()
	{
	}

	// Token: 0x04001BC6 RID: 7110
	public AITree Brain;
}
