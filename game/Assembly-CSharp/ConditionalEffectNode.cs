using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class ConditionalEffectNode : EffectNode
{
	// Token: 0x06001A28 RID: 6696 RVA: 0x000A2C38 File Offset: 0x000A0E38
	internal override void Apply(EffectProperties properties)
	{
		if (!(this.Filter == null) && !(this.Filter as LogicNode).Evaluate(properties))
		{
			bool flag = this.ElseEffects.Count > 1;
			foreach (Node node in this.ElseEffects)
			{
				if (node != null)
				{
					EffectNode effectNode = node as EffectNode;
					if (effectNode != null)
					{
						effectNode.Invoke(flag ? properties.Copy(false) : properties);
					}
				}
			}
			return;
		}
		bool flag2 = this.Effects.Count > 1;
		foreach (Node node2 in this.Effects)
		{
			if (node2 != null)
			{
				EffectNode effectNode2 = node2 as EffectNode;
				if (effectNode2 != null)
				{
					effectNode2.Invoke(flag2 ? properties.Copy(false) : properties);
				}
			}
		}
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x000A2D58 File Offset: 0x000A0F58
	public override void TryCancel(EffectProperties props)
	{
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).TryCancel((this.Effects.Count > 1) ? props.Copy(false) : props);
		}
		foreach (Node node2 in this.ElseEffects)
		{
			((EffectNode)node2).TryCancel((this.ElseEffects.Count > 1) ? props.Copy(false) : props);
		}
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x000A2E24 File Offset: 0x000A1024
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Invoke If",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x000A2E4B File Offset: 0x000A104B
	public ConditionalEffectNode()
	{
	}

	// Token: 0x04001A9B RID: 6811
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "If", PortLocation.Vertical)]
	public Node Filter;

	// Token: 0x04001A9C RID: 6812
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Then", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001A9D RID: 6813
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Else", PortLocation.Default)]
	public List<Node> ElseEffects = new List<Node>();
}
