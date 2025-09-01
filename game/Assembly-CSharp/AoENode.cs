using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AD RID: 685
public class AoENode : Node
{
	// Token: 0x060019BD RID: 6589 RVA: 0x000A0488 File Offset: 0x0009E688
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "AoE Info",
			AllowMultipleInputs = true
		};
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000A04A4 File Offset: 0x0009E6A4
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.SizeOverTime.GetString());
		this.SizeOverTime = animationCurve;
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x000A04CF File Offset: 0x0009E6CF
	public bool HasSpawnDelay()
	{
		return this.ActiveAfter > 0f;
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x000A04E0 File Offset: 0x0009E6E0
	public AoENode()
	{
	}

	// Token: 0x040019EB RID: 6635
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Scale", PortLocation.Header)]
	public Node DynamicScale;

	// Token: 0x040019EC RID: 6636
	public float Scale = 1f;

	// Token: 0x040019ED RID: 6637
	public AnimationCurve SizeOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x040019EE RID: 6638
	public float Duration = 10f;

	// Token: 0x040019EF RID: 6639
	public float ActiveAfter;

	// Token: 0x040019F0 RID: 6640
	[Tooltip("True of only one instance can exist at a time per entity")]
	public float TimerCycle = 1f;

	// Token: 0x040019F1 RID: 6641
	public bool EndWithCaster;

	// Token: 0x040019F2 RID: 6642
	public bool CanOverride = true;

	// Token: 0x040019F3 RID: 6643
	[Tooltip("True of only one instance can exist at a time per entity")]
	public bool Singleton;

	// Token: 0x040019F4 RID: 6644
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AoEApplicationNode), true, "Application", PortLocation.Default)]
	public List<Node> ApplyProps = new List<Node>();

	// Token: 0x040019F5 RID: 6645
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Spawn", PortLocation.Default)]
	[ShowPort("HasSpawnDelay")]
	public List<Node> OnSpawn = new List<Node>();

	// Token: 0x040019F6 RID: 6646
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Start", PortLocation.Default)]
	public List<Node> OnStart = new List<Node>();

	// Token: 0x040019F7 RID: 6647
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();

	// Token: 0x040019F8 RID: 6648
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Timer", PortLocation.Default)]
	public List<Node> OnTimer = new List<Node>();
}
