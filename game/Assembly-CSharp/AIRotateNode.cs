using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class AIRotateNode : AIActionNode
{
	// Token: 0x06001B52 RID: 6994 RVA: 0x000A8DDE File Offset: 0x000A6FDE
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Rotate",
			MinInspectorSize = new Vector2(220f, 0f)
		};
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x000A8E08 File Offset: 0x000A7008
	internal override AILogicState Run(AIControl entity)
	{
		EntityControl currentTarget = entity.currentTarget;
		if (currentTarget == null)
		{
			return AILogicState.Fail;
		}
		Vector3 vector = Vector3.ProjectOnPlane((currentTarget.movement.GetPosition() - entity.movement.GetPosition()).normalized, Vector3.up);
		Vector3 vector2 = entity.movement.GetForward();
		vector2 = Vector3.RotateTowards(vector2, vector, this.Speed * 0.017453292f * AIRootNode.AITickRate, 1f).normalized;
		entity.movement.SetForward(vector2, true);
		if (Vector3.Angle(vector2, vector) < this.AngleLimit)
		{
			return AILogicState.Success;
		}
		return AILogicState.Running;
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000A8EA7 File Offset: 0x000A70A7
	public AIRotateNode()
	{
	}

	// Token: 0x04001BC8 RID: 7112
	public AIRotateNode.Target RotateTarget;

	// Token: 0x04001BC9 RID: 7113
	public float Speed;

	// Token: 0x04001BCA RID: 7114
	[Range(0f, 90f)]
	public float AngleLimit = 5f;

	// Token: 0x02000655 RID: 1621
	public enum Target
	{
		// Token: 0x04002B12 RID: 11026
		Target
	}
}
