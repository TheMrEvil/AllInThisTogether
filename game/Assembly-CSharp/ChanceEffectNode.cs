using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class ChanceEffectNode : EffectNode
{
	// Token: 0x06001A1C RID: 6684 RVA: 0x000A29AC File Offset: 0x000A0BAC
	internal override void Apply(EffectProperties properties)
	{
		float num = this.Chance;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(properties);
			}
		}
		if ((float)properties.RandomInt(0, 10000) >= num * 10000f)
		{
			return;
		}
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).Invoke(properties.Copy(false));
		}
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x000A2A4C File Offset: 0x000A0C4C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Chance Effect",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x000A2A73 File Offset: 0x000A0C73
	public ChanceEffectNode()
	{
	}

	// Token: 0x04001A92 RID: 6802
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic %", PortLocation.Header)]
	public Node Value;

	// Token: 0x04001A93 RID: 6803
	[Range(0f, 1f)]
	public float Chance = 1f;

	// Token: 0x04001A94 RID: 6804
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Effects", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();
}
