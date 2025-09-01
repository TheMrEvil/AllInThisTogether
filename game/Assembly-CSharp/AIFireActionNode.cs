using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class AIFireActionNode : EffectNode
{
	// Token: 0x060019D5 RID: 6613 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
	internal override void Apply(EffectProperties properties)
	{
		if (this.Action == null)
		{
			return;
		}
		EffectProperties props = properties.Copy(false);
		this.Action.Root.Apply(props);
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x000A0BDD File Offset: 0x0009EDDD
	public void CancelAction(EffectProperties props)
	{
		if (this.Action == null)
		{
			return;
		}
		Debug.Log("Canceling Action Graph");
		this.Action.Root.TryCancel(props);
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x000A0C09 File Offset: 0x0009EE09
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "AI Use Action",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000A0C30 File Offset: 0x0009EE30
	public AIFireActionNode()
	{
	}

	// Token: 0x04001A33 RID: 6707
	public ActionTree Action;

	// Token: 0x02000646 RID: 1606
	public enum TargetOffsetMode
	{
		// Token: 0x04002AC7 RID: 10951
		None,
		// Token: 0x04002AC8 RID: 10952
		Velocity
	}
}
