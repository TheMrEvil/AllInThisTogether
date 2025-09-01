using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class PositionFilter_Distance : PositionFilterNode
{
	// Token: 0x06001B7A RID: 7034 RVA: 0x000A979C File Offset: 0x000A799C
	public override void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
		for (int i = points.Count - 1; i >= 0; i--)
		{
			if (!this.PointIsValid(control, points[i], origin))
			{
				points.RemoveAt(i);
			}
		}
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x000A97D8 File Offset: 0x000A79D8
	public override bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		this.dist = (point.pos - origin).sqrMagnitude;
		return this.dist >= this.Range.x * this.Range.x && this.dist <= this.Range.y * this.Range.y;
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000A9842 File Offset: 0x000A7A42
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Distance";
		return inspectorProps;
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000A9855 File Offset: 0x000A7A55
	public PositionFilter_Distance()
	{
	}

	// Token: 0x04001BEA RID: 7146
	public Vector2 Range = new Vector2(0f, 50f);

	// Token: 0x04001BEB RID: 7147
	private float dist;
}
