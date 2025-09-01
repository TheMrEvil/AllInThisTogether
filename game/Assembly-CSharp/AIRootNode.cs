using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class AIRootNode : AINode
{
	// Token: 0x06001B1B RID: 6939 RVA: 0x000A7FF4 File Offset: 0x000A61F4
	internal override AILogicState Run(AIControl entity)
	{
		foreach (Node node in this.Events)
		{
			AINode ainode = (AINode)node;
			if (ainode.CurrentState == AILogicState.Running)
			{
				return ainode.DoUpdate(entity);
			}
		}
		this.CurSequence = null;
		if (this.Entry != null)
		{
			AINode ainode2 = this.Entry as AINode;
			if (ainode2 != null)
			{
				return ainode2.DoUpdate(entity);
			}
		}
		return AILogicState.Success;
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x000A8088 File Offset: 0x000A6288
	public bool CanTrigger(EventTrigger trigger)
	{
		using (List<Node>.Enumerator enumerator = this.Events.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((AIEventNode)enumerator.Current).CanTrigger(trigger))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000A80E8 File Offset: 0x000A62E8
	public bool TriggerEvents(EventTrigger trigger, AIControl entity)
	{
		bool flag = false;
		foreach (Node node in this.Events)
		{
			AIEventNode aieventNode = (AIEventNode)node;
			flag |= aieventNode.TryTrigger(trigger, entity);
		}
		return flag;
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x000A8148 File Offset: 0x000A6348
	public override void ClearState(List<Node> wasChecked = null)
	{
		this.CurSequence = null;
		base.ClearState(wasChecked);
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x000A8158 File Offset: 0x000A6358
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Entry",
			ShowInspectorView = false,
			ShowInputNode = false,
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x000A8190 File Offset: 0x000A6390
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
		this.Abilities = new List<AbilityTree>();
		if (this.Entry != null)
		{
			AINode ainode = this.Entry as AINode;
			if (ainode != null)
			{
				ainode.CollectActions(this.Abilities, wasChecked);
			}
		}
		foreach (Node node in this.Events)
		{
			((AIEventNode)node).CollectActions(this.Abilities, wasChecked);
		}
		return this.Abilities;
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x000A8248 File Offset: 0x000A6448
	public AIRootNode()
	{
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x000A8266 File Offset: 0x000A6466
	// Note: this type is marked as 'beforefieldinit'.
	static AIRootNode()
	{
	}

	// Token: 0x04001BB2 RID: 7090
	public static float AITickRate = 0.1f;

	// Token: 0x04001BB3 RID: 7091
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), false, "", PortLocation.Header)]
	public Node Entry;

	// Token: 0x04001BB4 RID: 7092
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AIEventNode), true, "Events", PortLocation.Default)]
	public List<Node> Events = new List<Node>();

	// Token: 0x04001BB5 RID: 7093
	[NonSerialized]
	public List<AbilityTree> Abilities = new List<AbilityTree>();

	// Token: 0x04001BB6 RID: 7094
	[NonSerialized]
	public AISequenceNode CurSequence;
}
