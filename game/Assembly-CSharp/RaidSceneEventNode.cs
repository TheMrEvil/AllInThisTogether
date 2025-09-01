using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class RaidSceneEventNode : EffectNode
{
	// Token: 0x06001A6F RID: 6767 RVA: 0x000A4543 File Offset: 0x000A2743
	internal override void Apply(EffectProperties properties)
	{
		if (!RaidScene.CanTriggerEvent(this.ID))
		{
			return;
		}
		RaidManager.instance.TryTriggerSceneEvent(this.ID, this.LocalOnly);
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x000A4569 File Offset: 0x000A2769
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Raid Event",
			MinInspectorSize = new Vector2(100f, 0f)
		};
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000A4590 File Offset: 0x000A2790
	public RaidSceneEventNode()
	{
	}

	// Token: 0x04001AE5 RID: 6885
	public string ID;

	// Token: 0x04001AE6 RID: 6886
	public bool LocalOnly;
}
