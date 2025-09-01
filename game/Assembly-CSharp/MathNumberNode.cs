using System;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class MathNumberNode : NumberNode
{
	// Token: 0x06001D7E RID: 7550 RVA: 0x000B36A0 File Offset: 0x000B18A0
	public override float Evaluate(EffectProperties props)
	{
		float num = 0f;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
			}
		}
		float num2 = this.SecondaryValue;
		if (this.Secondary != null)
		{
			NumberNode numberNode2 = this.Secondary as NumberNode;
			if (numberNode2 != null)
			{
				num2 = numberNode2.Evaluate(props);
			}
		}
		float result;
		switch (this.M)
		{
		case MathNumberNode.MathType.Exponent:
			result = Mathf.Pow(num, num2);
			break;
		case MathNumberNode.MathType.Root:
			result = Mathf.Pow(num, 1f / Mathf.Max(num2, 0.01f));
			break;
		case MathNumberNode.MathType.Sin:
			result = Mathf.Sin(num);
			break;
		case MathNumberNode.MathType.Cos:
			result = Mathf.Cos(num);
			break;
		case MathNumberNode.MathType.Tan:
			result = Mathf.Tan(num);
			break;
		case MathNumberNode.MathType.Pi:
			result = 3.1415927f;
			break;
		case MathNumberNode.MathType.Mod:
			result = num % num2;
			break;
		case MathNumberNode.MathType.Abs:
			result = Mathf.Abs(num);
			break;
		case MathNumberNode.MathType.Shift:
			result = (float)((int)num << (int)num2);
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x000B37B0 File Offset: 0x000B19B0
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Math",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x000B37FE File Offset: 0x000B19FE
	public bool NeedSecond()
	{
		return this.M == MathNumberNode.MathType.Exponent || this.M == MathNumberNode.MathType.Root || this.M == MathNumberNode.MathType.Mod || this.M == MathNumberNode.MathType.Shift;
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x000B3825 File Offset: 0x000B1A25
	public MathNumberNode()
	{
	}

	// Token: 0x04001E30 RID: 7728
	public MathNumberNode.MathType M;

	// Token: 0x04001E31 RID: 7729
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "In", PortLocation.Header)]
	public Node Value;

	// Token: 0x04001E32 RID: 7730
	[HideInInspector]
	[SerializeField]
	[ShowPort("NeedSecond")]
	[OutputPort(typeof(NumberNode), false, "Dynamic", PortLocation.Default)]
	public Node Secondary;

	// Token: 0x04001E33 RID: 7731
	public float SecondaryValue;

	// Token: 0x02000684 RID: 1668
	public enum MathType
	{
		// Token: 0x04002BDA RID: 11226
		Exponent,
		// Token: 0x04002BDB RID: 11227
		Root,
		// Token: 0x04002BDC RID: 11228
		Sin,
		// Token: 0x04002BDD RID: 11229
		Cos,
		// Token: 0x04002BDE RID: 11230
		Tan,
		// Token: 0x04002BDF RID: 11231
		Pi,
		// Token: 0x04002BE0 RID: 11232
		Mod,
		// Token: 0x04002BE1 RID: 11233
		Abs,
		// Token: 0x04002BE2 RID: 11234
		Shift
	}
}
