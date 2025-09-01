using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
[RequireComponent(typeof(BoxCollider))]
public class TutorialStepTrigger : MonoBehaviour
{
	// Token: 0x060009F7 RID: 2551 RVA: 0x00041DDC File Offset: 0x0003FFDC
	private void Awake()
	{
		this.col = base.GetComponent<BoxCollider>();
		this.col.isTrigger = true;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00041DF6 File Offset: 0x0003FFF6
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<PlayerControl>() == null)
		{
			return;
		}
		if (TutorialManager.CurrentStep >= this.Step)
		{
			return;
		}
		if (TutorialManager.CurrentStep < this.MinReq)
		{
			return;
		}
		TutorialManager.instance.ChangeStep(this.Step);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00041E33 File Offset: 0x00040033
	public TutorialStepTrigger()
	{
	}

	// Token: 0x04000888 RID: 2184
	private BoxCollider col;

	// Token: 0x04000889 RID: 2185
	public TutorialStep MinReq;

	// Token: 0x0400088A RID: 2186
	public TutorialStep Step;
}
