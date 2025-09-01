using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AF RID: 687
public class BeamNode : Node
{
	// Token: 0x060019C1 RID: 6593 RVA: 0x000A0590 File Offset: 0x0009E790
	public float GetLength(EffectProperties props)
	{
		if (!(this.DynamicLength == null))
		{
			NumberNode numberNode = this.DynamicLength as NumberNode;
			if (numberNode != null)
			{
				return numberNode.Evaluate(props);
			}
		}
		return this.Length;
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x000A05C8 File Offset: 0x0009E7C8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Beam Properties",
			MinInspectorSize = new Vector2(250f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x000A05F8 File Offset: 0x0009E7F8
	public BeamNode()
	{
	}

	// Token: 0x040019FD RID: 6653
	public float Length;

	// Token: 0x040019FE RID: 6654
	public AnimationCurve LengthOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x040019FF RID: 6655
	public AnimationCurve WidthOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001A00 RID: 6656
	public float TickRate = 0.5f;

	// Token: 0x04001A01 RID: 6657
	public float Duration;

	// Token: 0x04001A02 RID: 6658
	public bool EndWithCaster = true;

	// Token: 0x04001A03 RID: 6659
	public bool StopsOnCollision = true;

	// Token: 0x04001A04 RID: 6660
	public EffectInteractsWith InteractsWith;

	// Token: 0x04001A05 RID: 6661
	public bool IgnoreCaster = true;

	// Token: 0x04001A06 RID: 6662
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Length", PortLocation.Default)]
	public Node DynamicLength;

	// Token: 0x04001A07 RID: 6663
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Enter", PortLocation.Default)]
	public List<Node> OnEnter = new List<Node>();

	// Token: 0x04001A08 RID: 6664
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Exit", PortLocation.Default)]
	public List<Node> OnExit = new List<Node>();

	// Token: 0x04001A09 RID: 6665
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Tick", PortLocation.Default)]
	public List<Node> OnTick = new List<Node>();
}
