using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class SubtractNumberNode : NumberNode
{
	// Token: 0x06001D9B RID: 7579 RVA: 0x000B3DD8 File Offset: 0x000B1FD8
	public override float Evaluate(EffectProperties props)
	{
		float num = 0f;
		float num2 = 0f;
		if (this.A != null)
		{
			num = (this.A as NumberNode).Evaluate(props);
		}
		if (this.B != null)
		{
			num2 = (this.B as NumberNode).Evaluate(props);
		}
		return num - num2;
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x000B3E34 File Offset: 0x000B2034
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "A - B",
			MinInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = false
		};
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x000B3E69 File Offset: 0x000B2069
	public SubtractNumberNode()
	{
	}

	// Token: 0x04001E4F RID: 7759
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "A", PortLocation.Default)]
	public Node A;

	// Token: 0x04001E50 RID: 7760
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "B", PortLocation.Default)]
	public Node B;
}
