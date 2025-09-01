using System;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class GenreGoal_SurviveTime : GenreGoalNode
{
	// Token: 0x06001CA0 RID: 7328 RVA: 0x000AE59D File Offset: 0x000AC79D
	public override void TickUpdate()
	{
		this.Progress += Time.deltaTime / this.TimeLimit;
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x000AE5B8 File Offset: 0x000AC7B8
	public override bool IsFinished()
	{
		return AIManager.instance.PointsSpawned > 0f && this.Progress >= 1f;
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x000AE5DD File Offset: 0x000AC7DD
	public override string GetGoalInfo()
	{
		return "- Survive -";
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x000AE5E4 File Offset: 0x000AC7E4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Survive (Timer)",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x000AE60B File Offset: 0x000AC80B
	public GenreGoal_SurviveTime()
	{
	}

	// Token: 0x04001D60 RID: 7520
	public float TimeLimit;
}
