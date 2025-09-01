using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class ProjectileLifetimeProps : Node
{
	// Token: 0x06001AD1 RID: 6865 RVA: 0x000A6AC0 File Offset: 0x000A4CC0
	public bool HasDistEvents()
	{
		return this.OnDistance.Count > 0;
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x000A6AD0 File Offset: 0x000A4CD0
	public bool HasLifeEvents()
	{
		return this.OnLived.Count > 0;
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x000A6AE0 File Offset: 0x000A4CE0
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.SizeOverTime.GetString());
		this.SizeOverTime = animationCurve;
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x000A6B0B File Offset: 0x000A4D0B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Lifetime",
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x000A6B24 File Offset: 0x000A4D24
	public ProjectileLifetimeProps()
	{
	}

	// Token: 0x04001B6C RID: 7020
	public float MaxLifetime = 4f;

	// Token: 0x04001B6D RID: 7021
	public AnimationCurve SizeOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001B6E RID: 7022
	public float DistanceEventRate = 1f;

	// Token: 0x04001B6F RID: 7023
	public float LifeEventRate = 1f;

	// Token: 0x04001B70 RID: 7024
	public bool LifeRepeat = true;

	// Token: 0x04001B71 RID: 7025
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On End", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();

	// Token: 0x04001B72 RID: 7026
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpireNatural = new List<Node>();

	// Token: 0x04001B73 RID: 7027
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Distance", PortLocation.Default)]
	public List<Node> OnDistance = new List<Node>();

	// Token: 0x04001B74 RID: 7028
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Lived", PortLocation.Default)]
	public List<Node> OnLived = new List<Node>();
}
