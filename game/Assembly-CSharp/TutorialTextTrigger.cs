using System;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class TutorialTextTrigger : MonoBehaviour
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060009FA RID: 2554 RVA: 0x00041E3B File Offset: 0x0004003B
	private bool InValidStep
	{
		get
		{
			return this.MinStep <= TutorialManager.CurrentStep && this.MaxStep >= TutorialManager.CurrentStep && TutorialManager.InTutorial;
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00041E5E File Offset: 0x0004005E
	private void Awake()
	{
		this.cgroup = base.GetComponent<CanvasGroup>();
		this.cgroup.alpha = 0f;
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00041E7C File Offset: 0x0004007C
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		float num = Vector3.Distance(base.transform.position, PlayerControl.myInstance.Movement.GetPosition());
		this.cgroup.UpdateOpacity(num <= this.Range && this.InValidStep, 1f, true);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00041EDA File Offset: 0x000400DA
	private void OnDrawGizmos()
	{
		if (this.DebugRange)
		{
			BetterGizmos.DrawSphere(Color.green, base.transform.position, this.Range);
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00041EFF File Offset: 0x000400FF
	public TutorialTextTrigger()
	{
	}

	// Token: 0x0400088B RID: 2187
	public float Range = 25f;

	// Token: 0x0400088C RID: 2188
	public bool DebugRange;

	// Token: 0x0400088D RID: 2189
	private CanvasGroup cgroup;

	// Token: 0x0400088E RID: 2190
	public TutorialStep MinStep;

	// Token: 0x0400088F RID: 2191
	public TutorialStep MaxStep = TutorialStep.Completed;
}
