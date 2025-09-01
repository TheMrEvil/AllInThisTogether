using System;

// Token: 0x02000315 RID: 789
public class CancelMovementNode : AIActionNode
{
	// Token: 0x06001B64 RID: 7012 RVA: 0x000A921A File Offset: 0x000A741A
	internal override AILogicState Run(AIControl entity)
	{
		if (entity.Movement.IsMoving())
		{
			entity.Movement.CancelMovement();
		}
		return AILogicState.Success;
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x000A9235 File Offset: 0x000A7435
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stop Moving",
			ShowInspectorView = false
		};
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000A924E File Offset: 0x000A744E
	public CancelMovementNode()
	{
	}
}
