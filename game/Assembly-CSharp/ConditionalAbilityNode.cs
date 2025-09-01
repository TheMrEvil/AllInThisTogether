using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class ConditionalAbilityNode : AbilityNode
{
	// Token: 0x06001993 RID: 6547 RVA: 0x0009F644 File Offset: 0x0009D844
	internal override AbilityState Run(EffectProperties props)
	{
		bool flag = this.cachedVal;
		if (!this.didEvaluate || !this.EvaluateOnce)
		{
			flag = (this.Filter as LogicNode).Evaluate(props);
			this.didEvaluate = true;
		}
		this.cachedVal = flag;
		List<Node> list = this.Effects;
		if (!flag)
		{
			list = this.ElseEffects;
		}
		AbilityState result = AbilityState.Finished;
		using (List<Node>.Enumerator enumerator = list.GetEnumerator())
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

	// Token: 0x06001994 RID: 6548 RVA: 0x0009F6E4 File Offset: 0x0009D8E4
	internal override void OnCancel(EffectProperties props)
	{
		bool flag = (this.Filter as LogicNode).Evaluate(props);
		List<Node> list = this.Effects;
		if (!flag)
		{
			list = this.ElseEffects;
		}
		foreach (Node node in list)
		{
			((AbilityNode)node).Cancel(props);
		}
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x0009F758 File Offset: 0x0009D958
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Run If",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x0009F77F File Offset: 0x0009D97F
	public ConditionalAbilityNode()
	{
	}

	// Token: 0x040019BF RID: 6591
	public ApplyOn Check;

	// Token: 0x040019C0 RID: 6592
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "If", PortLocation.Vertical)]
	public Node Filter;

	// Token: 0x040019C1 RID: 6593
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "Then", PortLocation.Default)]
	public List<Node> Effects = new List<Node>();

	// Token: 0x040019C2 RID: 6594
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "Else", PortLocation.Default)]
	public List<Node> ElseEffects = new List<Node>();

	// Token: 0x040019C3 RID: 6595
	public bool EvaluateOnce;

	// Token: 0x040019C4 RID: 6596
	private bool didEvaluate;

	// Token: 0x040019C5 RID: 6597
	private bool cachedVal;
}
