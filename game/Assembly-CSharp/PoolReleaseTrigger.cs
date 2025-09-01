using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class PoolReleaseTrigger : MonoBehaviour
{
	// Token: 0x06001814 RID: 6164 RVA: 0x00096F75 File Offset: 0x00095175
	private void Awake()
	{
		if (this.Trigger == PoolReleaseTrigger.TriggerType.MapChange)
		{
			MapManager.OnMapChangeFinished = (Action)Delegate.Combine(MapManager.OnMapChangeFinished, new Action(this.Release));
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x00096F9F File Offset: 0x0009519F
	private void Release()
	{
		ActionPool.ReleaseObject(base.gameObject);
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x00096FAC File Offset: 0x000951AC
	public PoolReleaseTrigger()
	{
	}

	// Token: 0x040017E7 RID: 6119
	public PoolReleaseTrigger.TriggerType Trigger;

	// Token: 0x0200061B RID: 1563
	public enum TriggerType
	{
		// Token: 0x040029D4 RID: 10708
		MapChange
	}
}
