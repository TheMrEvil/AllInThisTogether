using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class PositionFilter_TargetVisible : PositionFilterNode
{
	// Token: 0x06001B83 RID: 7043 RVA: 0x000A9934 File Offset: 0x000A7B34
	public override void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
		if (control.currentTarget == null)
		{
			points.Clear();
			return;
		}
		for (int i = points.Count - 1; i >= 0; i--)
		{
			if (!this.PointIsValid(control, points[i], origin))
			{
				points.RemoveAt(i);
			}
		}
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x000A9985 File Offset: 0x000A7B85
	public override bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		return !(asker.currentTarget == null) && point.IsEntityVisible(asker.currentTarget);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x000A99A3 File Offset: 0x000A7BA3
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Target Visible";
		return inspectorProps;
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000A99B6 File Offset: 0x000A7BB6
	public PositionFilter_TargetVisible()
	{
	}
}
