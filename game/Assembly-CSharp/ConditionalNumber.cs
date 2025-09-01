using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class ConditionalNumber : NumberNode
{
	// Token: 0x06001D67 RID: 7527 RVA: 0x000B2C5C File Offset: 0x000B0E5C
	public override float Evaluate(EffectProperties props)
	{
		if (!(this.Filter == null))
		{
			LogicNode logicNode = this.Filter as LogicNode;
			if (logicNode != null)
			{
				if (logicNode.Evaluate(props))
				{
					if (this.TrueValue != null)
					{
						NumberNode numberNode = this.TrueValue as NumberNode;
						if (numberNode != null)
						{
							return numberNode.Evaluate(props);
						}
					}
					return this.TrueNum;
				}
				if (this.FalseValue != null)
				{
					NumberNode numberNode2 = this.FalseValue as NumberNode;
					if (numberNode2 != null)
					{
						return numberNode2.Evaluate(props);
					}
				}
				return this.FalseNum;
			}
		}
		return float.NaN;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x000B2CEC File Offset: 0x000B0EEC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Conditional",
			MinInspectorSize = new Vector2(180f, 0f),
			MaxInspectorSize = new Vector2(180f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x000B2D3A File Offset: 0x000B0F3A
	public ConditionalNumber()
	{
	}

	// Token: 0x04001E11 RID: 7697
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "If", PortLocation.Vertical)]
	public Node Filter;

	// Token: 0x04001E12 RID: 7698
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "True Val", PortLocation.Default)]
	public Node TrueValue;

	// Token: 0x04001E13 RID: 7699
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "False Val", PortLocation.Default)]
	public Node FalseValue;

	// Token: 0x04001E14 RID: 7700
	public float TrueNum;

	// Token: 0x04001E15 RID: 7701
	public float FalseNum;
}
