using System;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class ClampNumberNode : NumberNode
{
	// Token: 0x06001D60 RID: 7520 RVA: 0x000B2A10 File Offset: 0x000B0C10
	public override float Evaluate(EffectProperties props)
	{
		if (this.Value == null)
		{
			return 0f;
		}
		float input = (this.Value as NumberNode).Evaluate(props);
		return this.Modify(input);
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x000B2A4C File Offset: 0x000B0C4C
	private float Modify(float input)
	{
		float result;
		switch (this.Modification)
		{
		case ClampNumberNode.ModifyType.Floor:
			result = (float)Mathf.FloorToInt(input);
			break;
		case ClampNumberNode.ModifyType.Cieling:
			result = (float)Mathf.CeilToInt(input);
			break;
		case ClampNumberNode.ModifyType.Clamp:
			result = ((this.Max < this.Min) ? Mathf.Max(input, this.Min) : Mathf.Clamp(input, this.Min, this.Max));
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x000B2AC0 File Offset: 0x000B0CC0
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Clamp",
			MinInspectorSize = new Vector2(165f, 0f),
			MaxInspectorSize = new Vector2(165f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = true
		};
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x000B2B15 File Offset: 0x000B0D15
	public ClampNumberNode()
	{
	}

	// Token: 0x04001E0B RID: 7691
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Input", PortLocation.Header)]
	public Node Value;

	// Token: 0x04001E0C RID: 7692
	public ClampNumberNode.ModifyType Modification;

	// Token: 0x04001E0D RID: 7693
	public float Min;

	// Token: 0x04001E0E RID: 7694
	public float Max;

	// Token: 0x0200067F RID: 1663
	public enum ModifyType
	{
		// Token: 0x04002BC6 RID: 11206
		Floor,
		// Token: 0x04002BC7 RID: 11207
		Cieling,
		// Token: 0x04002BC8 RID: 11208
		Clamp
	}
}
