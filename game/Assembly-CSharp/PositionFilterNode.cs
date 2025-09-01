using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class PositionFilterNode : Node
{
	// Token: 0x06001B74 RID: 7028 RVA: 0x000A9745 File Offset: 0x000A7945
	public void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin, bool IsRetest)
	{
		if (!this.AlwaysRequired && IsRetest)
		{
			return;
		}
		this.FilterPoints(ref points, control, origin);
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x000A975F File Offset: 0x000A795F
	public virtual void FilterPoints(ref List<PotentialNavPoint> points, AIControl control, Vector3 origin)
	{
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x000A9761 File Offset: 0x000A7961
	public bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin, bool IsRetest)
	{
		return (!this.AlwaysRequired && IsRetest) || this.PointIsValid(asker, point, origin);
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x000A977C File Offset: 0x000A797C
	public virtual bool PointIsValid(AIControl asker, PotentialNavPoint point, Vector3 origin)
	{
		return false;
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x000A977F File Offset: 0x000A797F
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			ShowInputNode = false
		};
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x000A978D File Offset: 0x000A798D
	public PositionFilterNode()
	{
	}

	// Token: 0x04001BE8 RID: 7144
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(Node), true, "", PortLocation.Vertical)]
	public Node Outputs;

	// Token: 0x04001BE9 RID: 7145
	public bool AlwaysRequired = true;
}
