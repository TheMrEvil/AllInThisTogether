using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class OneOverNumberNode : NumberNode
{
	// Token: 0x06001D88 RID: 7560 RVA: 0x000B3910 File Offset: 0x000B1B10
	public override float Evaluate(EffectProperties props)
	{
		if (this.Value == null)
		{
			return 0f;
		}
		float num = (this.Value as NumberNode).Evaluate(props);
		if (num == 0f)
		{
			return 0f;
		}
		return 1f / num;
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x000B3958 File Offset: 0x000B1B58
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "1/X",
			MinInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = false
		};
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x000B398D File Offset: 0x000B1B8D
	public OneOverNumberNode()
	{
	}

	// Token: 0x04001E35 RID: 7733
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "X", PortLocation.Header)]
	public Node Value;
}
