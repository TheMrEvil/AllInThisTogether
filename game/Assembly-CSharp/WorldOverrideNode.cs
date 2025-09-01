using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class WorldOverrideNode : ModOverrideNode
{
	// Token: 0x06001C64 RID: 7268 RVA: 0x000AD815 File Offset: 0x000ABA15
	internal override bool CanScope()
	{
		return false;
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x000AD818 File Offset: 0x000ABA18
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Game Override",
			MinInspectorSize = new Vector2(160f, 0f)
		};
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x000AD83F File Offset: 0x000ABA3F
	public WorldOverrideNode()
	{
	}

	// Token: 0x04001D40 RID: 7488
	public string ID;
}
