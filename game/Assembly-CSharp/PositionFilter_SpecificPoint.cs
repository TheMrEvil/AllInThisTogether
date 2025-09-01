using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class PositionFilter_SpecificPoint : PositionFilterNode
{
	// Token: 0x06001B7E RID: 7038 RVA: 0x000A9874 File Offset: 0x000A7A74
	public override void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
		points.Clear();
		Vector3 point = this.GetPoint(control);
		if (point.IsValid())
		{
			points.Add(new PotentialNavPoint(point, true, false));
		}
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000A98A8 File Offset: 0x000A7AA8
	public override bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		Vector3 point2 = this.GetPoint(asker);
		return point2.IsValid() && Vector3.Distance(point2, point.pos) < 1f;
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x000A98DA File Offset: 0x000A7ADA
	private Vector3 GetPoint(AIControl asker)
	{
		if (this.Point != PositionFilter_SpecificPoint.PointType.Target)
		{
			return Vector3.one.INVALID();
		}
		if (asker.currentTarget == null)
		{
			return Vector3.one.INVALID();
		}
		return asker.currentTarget.movement.GetPosition();
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x000A9918 File Offset: 0x000A7B18
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Specific Point";
		return inspectorProps;
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x000A992B File Offset: 0x000A7B2B
	public PositionFilter_SpecificPoint()
	{
	}

	// Token: 0x04001BEC RID: 7148
	public PositionFilter_SpecificPoint.PointType Point;

	// Token: 0x02000658 RID: 1624
	public enum PointType
	{
		// Token: 0x04002B1A RID: 11034
		Target
	}
}
