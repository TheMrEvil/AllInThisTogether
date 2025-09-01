using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class AbilityChanceNode : AbilityNode
{
	// Token: 0x06001944 RID: 6468 RVA: 0x0009D85C File Offset: 0x0009BA5C
	internal override AbilityState Run(EffectProperties props)
	{
		if (!this.rolled)
		{
			this.DoRoll(props);
		}
		AbilityState result = AbilityState.Finished;
		if (this.rollResult)
		{
			using (List<Node>.Enumerator enumerator = this.SuccessEvents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
					{
						result = AbilityState.Running;
					}
				}
				return result;
			}
		}
		using (List<Node>.Enumerator enumerator = this.FailEvents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
				{
					result = AbilityState.Running;
				}
			}
		}
		return result;
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x0009D91C File Offset: 0x0009BB1C
	internal override void OnCancel(EffectProperties props)
	{
		if (!this.rolled)
		{
			return;
		}
		this.rolled = false;
		if (this.rollResult)
		{
			using (List<Node>.Enumerator enumerator = this.SuccessEvents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Node node = enumerator.Current;
					((AbilityNode)node).Cancel(props);
				}
				return;
			}
		}
		foreach (Node node2 in this.FailEvents)
		{
			((AbilityNode)node2).Cancel(props);
		}
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
	private void DoRoll(EffectProperties props)
	{
		this.rolled = true;
		float num = this.Chance;
		if (this.InputValue != null)
		{
			NumberNode numberNode = this.InputValue as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
			}
		}
		this.rollResult = ((float)UnityEngine.Random.Range(0, 100) < num);
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x0009DA22 File Offset: 0x0009BC22
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Chance",
			MinInspectorSize = new Vector2(120f, 0f)
		};
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x0009DA49 File Offset: 0x0009BC49
	public AbilityChanceNode()
	{
	}

	// Token: 0x04001965 RID: 6501
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic", PortLocation.Header)]
	public Node InputValue;

	// Token: 0x04001966 RID: 6502
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "Success", PortLocation.Default)]
	public List<Node> SuccessEvents = new List<Node>();

	// Token: 0x04001967 RID: 6503
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "Fail", PortLocation.Default)]
	public List<Node> FailEvents = new List<Node>();

	// Token: 0x04001968 RID: 6504
	[Range(0f, 100f)]
	public float Chance;

	// Token: 0x04001969 RID: 6505
	private bool rolled;

	// Token: 0x0400196A RID: 6506
	private bool rollResult;
}
