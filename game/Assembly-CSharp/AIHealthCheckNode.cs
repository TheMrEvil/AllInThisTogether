using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class AIHealthCheckNode : AITestNode
{
	// Token: 0x06001BAA RID: 7082 RVA: 0x000AA358 File Offset: 0x000A8558
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.TestingEntity);
		return entity2 != null && AICheckNode.RunNumericTest(entity2.health.CurrentHealthProportion, this.Value / 100f, this.Test);
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000AA39F File Offset: 0x000A859F
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Health Test";
		return inspectorProps;
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x000AA3B2 File Offset: 0x000A85B2
	public AIHealthCheckNode()
	{
	}

	// Token: 0x04001C02 RID: 7170
	public AITestNode.TestTarget TestingEntity;

	// Token: 0x04001C03 RID: 7171
	public NumericTest Test;

	// Token: 0x04001C04 RID: 7172
	[Range(0f, 100f)]
	public float Value;
}
