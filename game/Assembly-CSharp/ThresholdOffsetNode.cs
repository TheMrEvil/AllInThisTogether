using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class ThresholdOffsetNode : LocationOffsetNode
{
	// Token: 0x06001DB1 RID: 7601 RVA: 0x000B4274 File Offset: 0x000B2474
	public override LocOffset GetOffset(EffectProperties props)
	{
		if (this.Offsets.Count == 0)
		{
			return this.Offset;
		}
		float f = 0f;
		if (this.InputVal != null)
		{
			NumberNode numberNode = this.InputVal as NumberNode;
			if (numberNode != null)
			{
				f = numberNode.Evaluate(props);
			}
		}
		int num = Mathf.RoundToInt(f);
		num = Mathf.Clamp(num, 0, this.Offsets.Count - 1);
		return (this.Offsets[num] as LocationOffsetNode).GetOffset(props);
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x000B42F3 File Offset: 0x000B24F3
	public override bool SholdShowLocation()
	{
		return false;
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x000B42F6 File Offset: 0x000B24F6
	public ThresholdOffsetNode()
	{
	}

	// Token: 0x04001E57 RID: 7767
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Input", PortLocation.Header)]
	public Node InputVal;

	// Token: 0x04001E58 RID: 7768
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationOffsetNode), true, "Offsets", PortLocation.Default)]
	public List<Node> Offsets = new List<Node>();
}
