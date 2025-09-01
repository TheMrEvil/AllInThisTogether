using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class AICooldownNode : AINode
{
	// Token: 0x06001AF9 RID: 6905 RVA: 0x000A78F8 File Offset: 0x000A5AF8
	internal override AILogicState Run(AIControl entity)
	{
		if ((Time.realtimeSinceStartup - this.cdValue < this.Cooldown && !this.isRunning) || this.Output == null)
		{
			return AILogicState.Fail;
		}
		AILogicState ailogicState = (this.Output as AINode).Run(entity);
		if (ailogicState == AILogicState.Success)
		{
			this.cdValue = Time.realtimeSinceStartup;
		}
		this.isRunning = (ailogicState == AILogicState.Running);
		return ailogicState;
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x000A795C File Offset: 0x000A5B5C
	public override List<AbilityTree> CollectActions(List<AbilityTree> list, List<Node> wasChecked = null)
	{
		if (wasChecked == null)
		{
			wasChecked = new List<Node>();
		}
		if (wasChecked.Contains(this))
		{
			return list;
		}
		wasChecked.Add(this);
		return (this.Output as AINode).CollectActions(list, wasChecked);
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x000A798C File Offset: 0x000A5B8C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cooldown",
			ShowInspectorView = true
		};
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x000A79A5 File Offset: 0x000A5BA5
	public AICooldownNode()
	{
	}

	// Token: 0x04001B9D RID: 7069
	public float Cooldown = 5f;

	// Token: 0x04001B9E RID: 7070
	private float cdValue;

	// Token: 0x04001B9F RID: 7071
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), false, "", PortLocation.Header)]
	public Node Output;

	// Token: 0x04001BA0 RID: 7072
	private bool isRunning;
}
