using System;

// Token: 0x0200031F RID: 799
public class AIDropTargetNode : AIActionNode
{
	// Token: 0x06001B8F RID: 7055 RVA: 0x000A9A62 File Offset: 0x000A7C62
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 100f;
		inspectorProps.Title = "Drop Target";
		return inspectorProps;
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x000A9A84 File Offset: 0x000A7C84
	internal override AILogicState Run(AIControl entity)
	{
		if (entity.currentTarget == null)
		{
			return AILogicState.Fail;
		}
		if (base.AIRoot.CurSequence != null)
		{
			return AILogicState.Fail;
		}
		if (entity.IsUsingActiveAbility())
		{
			return AILogicState.Fail;
		}
		entity.SetTarget(null);
		return AILogicState.Success;
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x000A9ABD File Offset: 0x000A7CBD
	public AIDropTargetNode()
	{
	}
}
