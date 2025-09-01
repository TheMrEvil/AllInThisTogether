using System;
using System.Collections.Generic;

// Token: 0x02000314 RID: 788
public class AIWaitNode : AIActionNode
{
	// Token: 0x06001B60 RID: 7008 RVA: 0x000A917C File Offset: 0x000A737C
	internal override AILogicState Run(AIControl entity)
	{
		if (!this.isWaiting)
		{
			this.isWaiting = true;
			this.startedWaitTime = GameplayManager.instance.GameTime;
			return AILogicState.Running;
		}
		if (this.isWaiting && GameplayManager.instance.GameTime - this.startedWaitTime < this.Duration)
		{
			return AILogicState.Running;
		}
		this.isWaiting = false;
		return AILogicState.Success;
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x000A91D5 File Offset: 0x000A73D5
	public override void ClearState(List<Node> wasChecked = null)
	{
		base.ClearState(wasChecked);
		this.isWaiting = false;
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x000A91E5 File Offset: 0x000A73E5
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 220f;
		inspectorProps.Title = "Wait";
		return inspectorProps;
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000A9207 File Offset: 0x000A7407
	public AIWaitNode()
	{
	}

	// Token: 0x04001BD5 RID: 7125
	public float Duration = 1.5f;

	// Token: 0x04001BD6 RID: 7126
	private bool isWaiting;

	// Token: 0x04001BD7 RID: 7127
	private float startedWaitTime;
}
