using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class AoEOverrideNode : ModOverrideNode
{
	// Token: 0x06001C40 RID: 7232 RVA: 0x000ACBA3 File Offset: 0x000AADA3
	public override void OverrideNodeProperties(EffectProperties props, Node node, object[] values)
	{
		if (!(node is SpawnAoENode))
		{
			return;
		}
		base.ScopeMatches(props.AbilityType);
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x000ACBBB File Offset: 0x000AADBB
	public override void OverrideNodeEffects(EffectProperties props, Node node, ref List<ModOverrideNode> overrides)
	{
		if (!(node is AoENode))
		{
			return;
		}
		if (props == null || !base.ScopeMatches(props.AbilityType))
		{
			return;
		}
		if (props.Keyword != Keyword.None && this.OverrideScope != PlayerAbilityType.Any)
		{
			return;
		}
		overrides.Add(this);
	}

	// Token: 0x06001C42 RID: 7234 RVA: 0x000ACBF7 File Offset: 0x000AADF7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "AoE Override",
			MinInspectorSize = new Vector2(300f, 0f)
		};
	}

	// Token: 0x06001C43 RID: 7235 RVA: 0x000ACC20 File Offset: 0x000AAE20
	public AoEOverrideNode()
	{
	}

	// Token: 0x04001CC9 RID: 7369
	public bool OverrideSize;

	// Token: 0x04001CCA RID: 7370
	public float Scale = 1f;

	// Token: 0x04001CCB RID: 7371
	[Range(0f, 1f)]
	public float ScaleWeight = 1f;

	// Token: 0x04001CCC RID: 7372
	public AnimationCurve SizeOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001CCD RID: 7373
	public bool OverrideDuration;

	// Token: 0x04001CCE RID: 7374
	public float Duration = 10f;

	// Token: 0x04001CCF RID: 7375
	public float ActiveAfter;

	// Token: 0x04001CD0 RID: 7376
	[Range(0f, 1f)]
	public float ActiveWeight;

	// Token: 0x04001CD1 RID: 7377
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AoEApplicationNode), true, "Application Effects", PortLocation.Default)]
	public List<Node> ApplyProps = new List<Node>();

	// Token: 0x04001CD2 RID: 7378
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Start", PortLocation.Default)]
	public List<Node> OnStart = new List<Node>();

	// Token: 0x04001CD3 RID: 7379
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();
}
