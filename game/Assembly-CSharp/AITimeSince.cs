using System;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class AITimeSince : AITestNode
{
	// Token: 0x06001BC6 RID: 7110 RVA: 0x000AAA70 File Offset: 0x000A8C70
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.Entity);
		if (!entity2)
		{
			return false;
		}
		float num = entity2.TimeSinceLast(this.TimeSince);
		if (num >= 0f)
		{
			return AICheckNode.RunNumericTest(num, this.Seconds, this.Is);
		}
		return this.ResponseIfNever;
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000AAAC2 File Offset: 0x000A8CC2
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Time Since",
			MinInspectorSize = new Vector2(280f, 0f),
			ShowInputNode = false
		};
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x000AAAF0 File Offset: 0x000A8CF0
	public AITimeSince()
	{
	}

	// Token: 0x04001C2A RID: 7210
	public AITestNode.TestTarget Entity;

	// Token: 0x04001C2B RID: 7211
	public TimeSince TimeSince;

	// Token: 0x04001C2C RID: 7212
	public NumericTest Is;

	// Token: 0x04001C2D RID: 7213
	public bool ResponseIfNever;

	// Token: 0x04001C2E RID: 7214
	public float Seconds = 5f;
}
