using System;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class CompleteAchievementNode : EffectNode
{
	// Token: 0x06001A1F RID: 6687 RVA: 0x000A2A91 File Offset: 0x000A0C91
	internal override void Apply(EffectProperties properties)
	{
		if (!this.GlobalCompletion && properties.SourceControl != PlayerControl.myInstance && properties.AffectedControl != PlayerControl.myInstance)
		{
			return;
		}
		AchievementManager.Unlock(this.ID);
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x000A2ACB File Offset: 0x000A0CCB
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Done Achievement",
			MinInspectorSize = new Vector2(200f, 0f),
			MaxInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x000A2B07 File Offset: 0x000A0D07
	public CompleteAchievementNode()
	{
	}

	// Token: 0x04001A95 RID: 6805
	public string ID;

	// Token: 0x04001A96 RID: 6806
	[Tooltip("Used for Multiplayer World Events that should trigger completion for all players")]
	public bool GlobalCompletion;
}
