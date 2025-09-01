using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class CompareNumberNode : NumberNode
{
	// Token: 0x06001D64 RID: 7524 RVA: 0x000B2B20 File Offset: 0x000B0D20
	public override float Evaluate(EffectProperties props)
	{
		float num = 0f;
		CompareNumberNode.Comparison ctype = this.CType;
		if (ctype == CompareNumberNode.Comparison.Max)
		{
			num = float.MinValue;
			foreach (Node node in this.Numbers)
			{
				num = Mathf.Max(((NumberNode)node).Evaluate(props), num);
			}
			return num;
		}
		if (ctype != CompareNumberNode.Comparison.Min)
		{
			return num;
		}
		num = float.MaxValue;
		foreach (Node node2 in this.Numbers)
		{
			num = Mathf.Min(((NumberNode)node2).Evaluate(props), num);
		}
		return num;
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x000B2BF4 File Offset: 0x000B0DF4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Compare",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = true
		};
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x000B2C49 File Offset: 0x000B0E49
	public CompareNumberNode()
	{
	}

	// Token: 0x04001E0F RID: 7695
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "", PortLocation.Header)]
	public List<Node> Numbers = new List<Node>();

	// Token: 0x04001E10 RID: 7696
	public CompareNumberNode.Comparison CType;

	// Token: 0x02000680 RID: 1664
	public enum Comparison
	{
		// Token: 0x04002BCA RID: 11210
		Max,
		// Token: 0x04002BCB RID: 11211
		Min
	}
}
