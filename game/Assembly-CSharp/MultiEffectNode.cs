using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class MultiEffectNode : EffectNode
{
	// Token: 0x06001A5D RID: 6749 RVA: 0x000A40B4 File Offset: 0x000A22B4
	internal override void Apply(EffectProperties properties)
	{
		int num = this.ApplyCount;
		if (this.Value != null)
		{
			num = (int)(this.Value as NumberNode).Evaluate(properties);
		}
		for (int i = 0; i < num; i++)
		{
			properties.SetExtra(EProp.Node_Output, (float)i);
			if (this.Effects.Count > 0 && this.SeparateCache)
			{
				properties = properties.Copy(true);
				if (this.IncrementRandom)
				{
					properties.OverrideSeed(properties.RandSeed + i, 0);
				}
			}
			foreach (Node node in this.Effects)
			{
				((EffectNode)node).Invoke(properties.Copy(false));
			}
		}
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000A4190 File Offset: 0x000A2390
	public override void TryCancel(EffectProperties props)
	{
		EffectProperties props2 = props.Copy(this.SeparateCache);
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).TryCancel(props2);
		}
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x000A41F4 File Offset: 0x000A23F4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Invoke Multiple",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000A421B File Offset: 0x000A241B
	public MultiEffectNode()
	{
	}

	// Token: 0x04001AD0 RID: 6864
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Scaling Count", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001AD1 RID: 6865
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Effects", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x04001AD2 RID: 6866
	public int ApplyCount;

	// Token: 0x04001AD3 RID: 6867
	public bool SeparateCache;

	// Token: 0x04001AD4 RID: 6868
	public bool IncrementRandom;
}
