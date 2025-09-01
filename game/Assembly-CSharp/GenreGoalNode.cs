using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class GenreGoalNode : Node
{
	// Token: 0x06001C8D RID: 7309 RVA: 0x000AE0C1 File Offset: 0x000AC2C1
	public virtual void Setup()
	{
		this.Progress = 0f;
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000AE0CE File Offset: 0x000AC2CE
	public virtual void TickUpdate()
	{
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
	public virtual bool IsFinished()
	{
		return false;
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x000AE0D3 File Offset: 0x000AC2D3
	public virtual string GetGoalInfo()
	{
		return "";
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x000AE0DA File Offset: 0x000AC2DA
	public GenreGoalNode()
	{
	}

	// Token: 0x04001D56 RID: 7510
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreSpawnNode), true, "Spawns", PortLocation.Header)]
	public Node Spawns;

	// Token: 0x04001D57 RID: 7511
	public string StartText;

	// Token: 0x04001D58 RID: 7512
	[NonSerialized]
	public float Progress;
}
