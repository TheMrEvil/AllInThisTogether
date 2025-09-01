using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class AIMovementCheck : AITestNode
{
	// Token: 0x06001BB3 RID: 7091 RVA: 0x000AA5C0 File Offset: 0x000A87C0
	public override bool Evaluate(EntityControl entity)
	{
		AIControl aicontrol = entity as AIControl;
		AIMovementCheck.MovementTestType movementTestType = this.movementTest;
		if (movementTestType != AIMovementCheck.MovementTestType.IsMoving)
		{
			return movementTestType == AIMovementCheck.MovementTestType.NotMoving && !aicontrol.Movement.IsMoving();
		}
		return aicontrol.Movement.IsMoving();
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000AA600 File Offset: 0x000A8800
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Movement Check",
			MinInspectorSize = new Vector2(270f, 0f),
			ShowInputNode = false
		};
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000AA62E File Offset: 0x000A882E
	public AIMovementCheck()
	{
	}

	// Token: 0x04001C0F RID: 7183
	public AIMovementCheck.MovementTestType movementTest;

	// Token: 0x02000661 RID: 1633
	public enum MovementTestType
	{
		// Token: 0x04002B39 RID: 11065
		IsMoving,
		// Token: 0x04002B3A RID: 11066
		NotMoving
	}
}
