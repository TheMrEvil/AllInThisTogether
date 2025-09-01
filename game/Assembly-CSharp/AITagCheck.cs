using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class AITagCheck : AITestNode
{
	// Token: 0x06001BC0 RID: 7104 RVA: 0x000AA888 File Offset: 0x000A8A88
	public override bool Evaluate(EntityControl entity)
	{
		return (entity as AIControl).HasTag(this.Tag);
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000AA89B File Offset: 0x000A8A9B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Has Tag",
			MinInspectorSize = new Vector2(200f, 0f),
			ShowInputNode = false
		};
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000AA8C9 File Offset: 0x000A8AC9
	public AITagCheck()
	{
	}

	// Token: 0x04001C24 RID: 7204
	public string Tag;
}
