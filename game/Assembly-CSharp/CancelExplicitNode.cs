using System;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class CancelExplicitNode : EffectNode
{
	// Token: 0x06001A19 RID: 6681 RVA: 0x000A2947 File Offset: 0x000A0B47
	internal override void Apply(EffectProperties properties)
	{
		if (this.Action == null)
		{
			return;
		}
		(this.Action.RootNode.Clone(null, false) as ActionRootNode).TryCancel(properties.Copy(false));
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x000A297B File Offset: 0x000A0B7B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cancel Graph",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x000A29A2 File Offset: 0x000A0BA2
	public CancelExplicitNode()
	{
	}

	// Token: 0x04001A91 RID: 6801
	public ActionTree Action;
}
