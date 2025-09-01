using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class AbilityDelayNode : AbilityNode
{
	// Token: 0x06001949 RID: 6473 RVA: 0x0009DA68 File Offset: 0x0009BC68
	internal override AbilityState Run(EffectProperties props)
	{
		if (!this.startedTimer)
		{
			this.startedTimer = true;
			this.startTime = props.Lifetime;
		}
		if (this.completed)
		{
			return AbilityState.Finished;
		}
		float num = this.Delay;
		if (this.DynamicDelay != null)
		{
			NumberNode numberNode = this.DynamicDelay as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
			}
		}
		if (props.Lifetime - this.startTime >= num)
		{
			AbilityState abilityState = AbilityState.Finished;
			using (List<Node>.Enumerator enumerator = this.AfterDelay.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
					{
						abilityState = AbilityState.Running;
					}
				}
			}
			if (abilityState == AbilityState.Finished)
			{
				this.completed = true;
			}
			return abilityState;
		}
		return AbilityState.Running;
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x0009DB34 File Offset: 0x0009BD34
	internal override void OnCancel(EffectProperties props)
	{
		this.startedTimer = false;
		this.completed = false;
		foreach (Node node in this.AfterDelay)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x0009DB98 File Offset: 0x0009BD98
	public override void OnCloned()
	{
		this.completed = false;
		this.startedTimer = false;
		base.OnCloned();
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x0009DBAE File Offset: 0x0009BDAE
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Delay",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x0009DBD5 File Offset: 0x0009BDD5
	public AbilityDelayNode()
	{
	}

	// Token: 0x0400196B RID: 6507
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "", PortLocation.Header)]
	public List<Node> AfterDelay = new List<Node>();

	// Token: 0x0400196C RID: 6508
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Delay", PortLocation.Default)]
	public Node DynamicDelay;

	// Token: 0x0400196D RID: 6509
	[Tooltip("Executes Outputs after X Seconds")]
	public float Delay;

	// Token: 0x0400196E RID: 6510
	private bool startedTimer;

	// Token: 0x0400196F RID: 6511
	private float startTime;

	// Token: 0x04001970 RID: 6512
	private bool completed;
}
