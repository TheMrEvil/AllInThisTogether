using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class AIStatusCheck : AITestNode
{
	// Token: 0x06001BBD RID: 7101 RVA: 0x000AA7C4 File Offset: 0x000A89C4
	public override bool Evaluate(EntityControl entity)
	{
		EntityControl entity2 = AITestNode.GetEntity(entity, this.Entity);
		if (this.StatusEffect == null || entity2 == null)
		{
			return false;
		}
		EntityControl.AppliedStatus statusInfoByID = entity2.GetStatusInfoByID(this.StatusEffect.ID, -1);
		return statusInfoByID != null && ((this.Min == 0 && this.Max == 0) || (statusInfoByID.Stacks >= this.Min && (statusInfoByID.Stacks < this.Max || this.Max <= this.Min)));
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000AA852 File Offset: 0x000A8A52
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Has Status Effect",
			ShowInputNode = false,
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000AA880 File Offset: 0x000A8A80
	public AIStatusCheck()
	{
	}

	// Token: 0x04001C20 RID: 7200
	public AITestNode.TestTarget Entity;

	// Token: 0x04001C21 RID: 7201
	public StatusTree StatusEffect;

	// Token: 0x04001C22 RID: 7202
	public int Min;

	// Token: 0x04001C23 RID: 7203
	public int Max;
}
