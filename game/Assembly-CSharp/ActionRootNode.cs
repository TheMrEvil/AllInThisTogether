using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class ActionRootNode : RootNode
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x0009F9F3 File Offset: 0x0009DBF3
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Start",
			ShowInspectorView = false,
			ShowInputNode = false,
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x0009FA28 File Offset: 0x0009DC28
	public void TryCancel(EffectProperties props)
	{
		foreach (Node node in this.OnCancel)
		{
			if (node != null)
			{
				EffectNode effectNode = node as EffectNode;
				if (effectNode != null)
				{
					effectNode.Invoke(props);
				}
			}
		}
		foreach (Node node2 in this.OnStart)
		{
			if (node2 != null)
			{
				EffectNode effectNode2 = node2 as EffectNode;
				if (effectNode2 != null)
				{
					effectNode2.TryCancel(props);
				}
			}
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x0009FAE8 File Offset: 0x0009DCE8
	public void Apply(EffectProperties props)
	{
		foreach (Node node in this.OnStart)
		{
			if (node != null)
			{
				EffectNode effectNode = node as EffectNode;
				if (effectNode != null)
				{
					effectNode.Invoke(props);
				}
			}
		}
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x0009FB50 File Offset: 0x0009DD50
	public ActionRootNode()
	{
	}

	// Token: 0x040019D6 RID: 6614
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "", PortLocation.Header)]
	public List<Node> OnStart = new List<Node>();

	// Token: 0x040019D7 RID: 6615
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Cancel", PortLocation.Default)]
	public List<Node> OnCancel = new List<Node>();
}
