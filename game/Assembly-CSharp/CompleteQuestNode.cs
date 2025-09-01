using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class CompleteQuestNode : EffectNode
{
	// Token: 0x06001A25 RID: 6693 RVA: 0x000A2B94 File Offset: 0x000A0D94
	internal override void Apply(EffectProperties properties)
	{
		if (properties.SourceControl != PlayerControl.myInstance)
		{
			return;
		}
		string id = this.ID;
		if (this.UseSnippetInput)
		{
			id = properties.InputID;
		}
		if (!Progression.CanCompleteQuest(id))
		{
			return;
		}
		MetaDB.DailyQuest quest = MetaDB.GetQuest(id);
		AchievementToast instance = AchievementToast.instance;
		if (instance != null)
		{
			instance.Show(quest);
		}
		Progression.CompleteQuest(id);
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000A2BF1 File Offset: 0x000A0DF1
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Complete Quest",
			MinInspectorSize = new Vector2(200f, 0f),
			MaxInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000A2C2D File Offset: 0x000A0E2D
	public CompleteQuestNode()
	{
	}

	// Token: 0x04001A99 RID: 6809
	public bool UseSnippetInput;

	// Token: 0x04001A9A RID: 6810
	public string ID;
}
