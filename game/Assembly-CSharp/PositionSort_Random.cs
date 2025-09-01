using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class PositionSort_Random : PositionFilterNode
{
	// Token: 0x06001B8B RID: 7051 RVA: 0x000A9A3A File Offset: 0x000A7C3A
	public override void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
		points.Shuffle(null);
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x000A9A44 File Offset: 0x000A7C44
	public override bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		return true;
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x000A9A47 File Offset: 0x000A7C47
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Random Point";
		return inspectorProps;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x000A9A5A File Offset: 0x000A7C5A
	public PositionSort_Random()
	{
	}
}
