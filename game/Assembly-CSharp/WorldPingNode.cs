using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class WorldPingNode : EffectNode
{
	// Token: 0x06001AC9 RID: 6857 RVA: 0x000A6974 File Offset: 0x000A4B74
	internal override void Apply(EffectProperties properties)
	{
		string text = this.PingText;
		for (int i = 0; i < this.Numbers.Count; i++)
		{
			if (!(this.Numbers[i] == null))
			{
				NumberNode numberNode = this.Numbers[i] as NumberNode;
				if (numberNode != null)
				{
					text = text.Replace(string.Format("$Value_{0}$", i + 1), ((int)numberNode.Evaluate(properties)).ToString());
				}
			}
		}
		UIPing.WorldEventPing(text, this.ShowTime);
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x000A69FC File Offset: 0x000A4BFC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "World Ping",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x000A6A23 File Offset: 0x000A4C23
	public WorldPingNode()
	{
	}

	// Token: 0x04001B5E RID: 7006
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Inputs", PortLocation.Default)]
	public List<Node> Numbers = new List<Node>();

	// Token: 0x04001B5F RID: 7007
	public string PingText;

	// Token: 0x04001B60 RID: 7008
	public float ShowTime = 2f;
}
