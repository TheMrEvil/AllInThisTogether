using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class AIStatCheck : AITestNode
{
	// Token: 0x06001BB9 RID: 7097 RVA: 0x000AA688 File Offset: 0x000A8888
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.Entity);
		if (!entity2)
		{
			return false;
		}
		float statValue = entity2.GetStatValue(this.Stat);
		return this.NumericTest(statValue);
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x000AA6C0 File Offset: 0x000A88C0
	private bool NumericTest(float val)
	{
		bool result;
		switch (this.Test)
		{
		case NumberTest.Equals:
			result = ((int)val == (int)this.Value);
			break;
		case NumberTest.NotEquals:
			result = ((int)val != (int)this.Value);
			break;
		case NumberTest.LessThan:
			result = (val < this.Value);
			break;
		case NumberTest.GreaterThan:
			result = (val > this.Value);
			break;
		case NumberTest.Between:
			result = (val >= this.Between.x && val <= this.Between.y);
			break;
		case NumberTest.LTOrE:
			result = (val <= this.Value);
			break;
		case NumberTest.GTOrE:
			result = (val >= this.Value);
			break;
		case NumberTest.HasFlag:
			result = (((int)val & (int)this.Value) != 0);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x000AA78C File Offset: 0x000A898C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stat Check",
			MinInspectorSize = new Vector2(270f, 0f),
			ShowInputNode = false
		};
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000AA7BA File Offset: 0x000A89BA
	public AIStatCheck()
	{
	}

	// Token: 0x04001C12 RID: 7186
	public AITestNode.TestTarget Entity;

	// Token: 0x04001C13 RID: 7187
	public EStat Stat;

	// Token: 0x04001C14 RID: 7188
	public NumberTest Test;

	// Token: 0x04001C15 RID: 7189
	public float Value;

	// Token: 0x04001C16 RID: 7190
	public Vector2 Between;
}
