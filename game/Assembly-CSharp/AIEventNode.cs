using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class AIEventNode : AINode
{
	// Token: 0x06001B00 RID: 6912 RVA: 0x000A7A18 File Offset: 0x000A5C18
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.Title = "Event";
		inspectorProps.MinInspectorSize.x = 180f;
		return inspectorProps;
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x000A7A3A File Offset: 0x000A5C3A
	public bool CanTrigger(EventTrigger trigger)
	{
		return trigger == this.Trigger && this.Logic != null;
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x000A7A53 File Offset: 0x000A5C53
	public bool TryTrigger(EventTrigger trigger, AIControl entity)
	{
		return this.CanTrigger(trigger) && base.DoUpdate(entity) != AILogicState.Fail;
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x000A7A6D File Offset: 0x000A5C6D
	internal override AILogicState Run(AIControl entity)
	{
		return (this.Logic as AINode).DoUpdate(entity);
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x000A7A80 File Offset: 0x000A5C80
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
		if (this.Logic != null)
		{
			(this.Logic as AINode).CollectActions(list, wasChecked);
		}
		return list;
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x000A7AC0 File Offset: 0x000A5CC0
	public AIEventNode()
	{
	}

	// Token: 0x04001BA3 RID: 7075
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), false, "", PortLocation.Header)]
	public Node Logic;

	// Token: 0x04001BA4 RID: 7076
	public EventTrigger Trigger;
}
