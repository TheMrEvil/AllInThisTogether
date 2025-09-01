using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B0 RID: 688
public class EffectGroupNode : EffectNode
{
	// Token: 0x060019C4 RID: 6596 RVA: 0x000A06C6 File Offset: 0x0009E8C6
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Effect Group",
			MinInspectorSize = new Vector2(110f, 0f)
		};
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x000A06F0 File Offset: 0x0009E8F0
	internal override void Apply(EffectProperties properties)
	{
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).Invoke(properties);
		}
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x000A0748 File Offset: 0x0009E948
	public override void TryCancel(EffectProperties props)
	{
		foreach (Node node in this.Effects)
		{
			((EffectNode)node).TryCancel(props);
		}
		base.TryCancel(props);
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x000A07A8 File Offset: 0x0009E9A8
	public EffectGroupNode()
	{
	}

	// Token: 0x04001A0A RID: 6666
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "Effects", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();
}
