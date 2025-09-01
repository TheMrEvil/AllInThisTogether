using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class AISequenceNode : AINode
{
	// Token: 0x06001B27 RID: 6951 RVA: 0x000A83AB File Offset: 0x000A65AB
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Sequence"
		};
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x000A83C0 File Offset: 0x000A65C0
	internal override AILogicState Run(AIControl entity)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup - this.LastActivationTime > 1.5f * (realtimeSinceStartup - entity.LastBrainTime))
		{
			this.current = 0;
		}
		this.LastActivationTime = Time.realtimeSinceStartup;
		switch ((this.Sequence[this.current] as AINode).DoUpdate(entity))
		{
		case AILogicState.Success:
			this.current++;
			base.AIRoot.CurSequence = this;
			break;
		case AILogicState.Fail:
			this.current = 0;
			if (base.AIRoot.CurSequence == this)
			{
				base.AIRoot.CurSequence = null;
			}
			return AILogicState.Fail;
		case AILogicState.Running:
			base.AIRoot.CurSequence = this;
			return AILogicState.Running;
		}
		int num = (this.current == this.Sequence.Count) ? 0 : 2;
		if (num == 0)
		{
			this.current = 0;
			if (base.AIRoot.CurSequence == this)
			{
				base.AIRoot.CurSequence = null;
			}
		}
		return (AILogicState)num;
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x000A84C0 File Offset: 0x000A66C0
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
		foreach (Node node in this.Sequence)
		{
			(node as AINode).CollectActions(list, null);
		}
		return list;
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x000A8538 File Offset: 0x000A6738
	public override void ClearState(List<Node> wasChecked = null)
	{
		base.ClearState(wasChecked);
		this.current = 0;
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x000A8548 File Offset: 0x000A6748
	public AISequenceNode()
	{
	}

	// Token: 0x04001BB9 RID: 7097
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), true, "", PortLocation.Header)]
	public List<Node> Sequence = new List<Node>();

	// Token: 0x04001BBA RID: 7098
	private int current;

	// Token: 0x04001BBB RID: 7099
	private float LastActivationTime;
}
