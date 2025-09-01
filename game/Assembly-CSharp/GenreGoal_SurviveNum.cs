using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class GenreGoal_SurviveNum : GenreGoalNode
{
	// Token: 0x06001C9B RID: 7323 RVA: 0x000AE52D File Offset: 0x000AC72D
	public override bool IsFinished()
	{
		return AIManager.instance.InGoalKilled >= this.KillsRequired;
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x000AE544 File Offset: 0x000AC744
	public override void TickUpdate()
	{
		this.Progress = (float)AIManager.instance.InGoalKilled / (float)this.KillsRequired;
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x000AE55F File Offset: 0x000AC75F
	public override string GetGoalInfo()
	{
		return "- Survive -";
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x000AE566 File Offset: 0x000AC766
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Survive (Count)",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x000AE58D File Offset: 0x000AC78D
	public GenreGoal_SurviveNum()
	{
	}

	// Token: 0x04001D5F RID: 7519
	public int KillsRequired = 20;
}
