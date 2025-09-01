using System;

// Token: 0x02000323 RID: 803
public class AIDangerCheck : AITestNode
{
	// Token: 0x06001BA2 RID: 7074 RVA: 0x000AA138 File Offset: 0x000A8338
	public override bool Evaluate(EntityControl entity)
	{
		bool flag = this.DangerTest == AIDangerCheck.DangerType.Any;
		if (this.DangerTest == AIDangerCheck.DangerType.InAoE || flag)
		{
			foreach (AreaOfEffect areaOfEffect in AreaOfEffect.AllAreas)
			{
				if (areaOfEffect.IsNegative && areaOfEffect.HasEntityInside(entity))
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000AA1B4 File Offset: 0x000A83B4
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "In Danger";
		return inspectorProps;
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000AA1C7 File Offset: 0x000A83C7
	public AIDangerCheck()
	{
	}

	// Token: 0x04001BFC RID: 7164
	public AIDangerCheck.DangerType DangerTest;

	// Token: 0x0200065E RID: 1630
	public enum DangerType
	{
		// Token: 0x04002B2F RID: 11055
		Any,
		// Token: 0x04002B30 RID: 11056
		InAoE
	}
}
