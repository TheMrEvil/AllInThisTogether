using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class AoEApplicationNode : Node
{
	// Token: 0x060019BB RID: 6587 RVA: 0x000A03FD File Offset: 0x0009E5FD
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "AoE Application",
			SortX = true,
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x000A042C File Offset: 0x0009E62C
	public AoEApplicationNode()
	{
	}

	// Token: 0x040019DE RID: 6622
	public float TickRate = 0.5f;

	// Token: 0x040019DF RID: 6623
	public EffectInteractsWith Affects;

	// Token: 0x040019E0 RID: 6624
	public bool ExcludeSelf;

	// Token: 0x040019E1 RID: 6625
	public bool ExcludeAffected;

	// Token: 0x040019E2 RID: 6626
	public bool SingleEntrance;

	// Token: 0x040019E3 RID: 6627
	public int MaxInsideAtOnce;

	// Token: 0x040019E4 RID: 6628
	[Tooltip("0 is infinite")]
	public int EndIfEntered;

	// Token: 0x040019E5 RID: 6629
	[Tooltip("End once X entities have entered the AoE")]
	public bool CancelIfEnd = true;

	// Token: 0x040019E6 RID: 6630
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicFilterNode), true, "Filters", PortLocation.Vertical)]
	public List<Node> Filters = new List<Node>();

	// Token: 0x040019E7 RID: 6631
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Enter", PortLocation.Default)]
	public List<Node> OnEnter = new List<Node>();

	// Token: 0x040019E8 RID: 6632
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Exit", PortLocation.Default)]
	public List<Node> OnExit = new List<Node>();

	// Token: 0x040019E9 RID: 6633
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Tick", PortLocation.Default)]
	public List<Node> OnTick = new List<Node>();

	// Token: 0x040019EA RID: 6634
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On End Inside", PortLocation.Default)]
	public List<Node> OnEndInside = new List<Node>();
}
