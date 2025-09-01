using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class AbilityRepeatNode : AbilityNode
{
	// Token: 0x0600196C RID: 6508 RVA: 0x0009E954 File Offset: 0x0009CB54
	internal override AbilityState Run(EffectProperties props)
	{
		bool flag = false;
		if (!this.startedTimer)
		{
			this.startedTimer = true;
			this.timer = props.Lifetime;
			this.repeatCount = 0;
			flag = true;
		}
		float num = this.RepeatTime;
		if (num >= 0f)
		{
			num *= this.RepeatTimeMult.Evaluate(props.Lifetime);
		}
		if (this.RepeatTime <= 0f || props.Lifetime - this.timer >= num || flag)
		{
			this.repeatCount++;
			props.SetExtra(EProp.Stacks, (float)this.repeatCount);
			foreach (Node node in this.AfterDelay)
			{
				AbilityNode abilityNode = (AbilityNode)node;
				if (this.CancelBeforeApply)
				{
					abilityNode.Cancel(props);
				}
				abilityNode.DoUpdate(props);
			}
			this.timer = props.Lifetime;
		}
		else if (this.ContinuousUpdate && this.CancelBeforeApply)
		{
			foreach (Node node2 in this.AfterDelay)
			{
				((AbilityNode)node2).DoUpdate(props);
			}
		}
		return AbilityState.Running;
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x0009EAB0 File Offset: 0x0009CCB0
	internal override void OnCancel(EffectProperties props)
	{
		this.startedTimer = false;
		this.repeatCount = 0;
		foreach (Node node in this.AfterDelay)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x0009EB14 File Offset: 0x0009CD14
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Repeat",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x0009EB3C File Offset: 0x0009CD3C
	public AbilityRepeatNode()
	{
	}

	// Token: 0x04001998 RID: 6552
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> AfterDelay = new List<Node>();

	// Token: 0x04001999 RID: 6553
	[Tooltip("Repeats Outputs every X Seconds")]
	public float RepeatTime = 0.1f;

	// Token: 0x0400199A RID: 6554
	[Tooltip("Most ability nodes will only fire once per ability cast unless canceled")]
	public bool CancelBeforeApply = true;

	// Token: 0x0400199B RID: 6555
	[Tooltip("Continues to call node Update every frame regardless of repeat time")]
	public bool ContinuousUpdate;

	// Token: 0x0400199C RID: 6556
	[Tooltip("Multiplier based on duration ability has been channeled")]
	public AnimationCurve RepeatTimeMult = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400199D RID: 6557
	private bool startedTimer;

	// Token: 0x0400199E RID: 6558
	private float timer;

	// Token: 0x0400199F RID: 6559
	private int repeatCount;
}
