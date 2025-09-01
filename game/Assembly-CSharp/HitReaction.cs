using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200022C RID: 556
public class HitReaction : MonoBehaviour
{
	// Token: 0x0600172A RID: 5930 RVA: 0x000929E0 File Offset: 0x00090BE0
	public void ImpactAction()
	{
		this.OnImpact.Invoke();
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000929ED File Offset: 0x00090BED
	public void DebugImpact()
	{
		this.ImpactAction();
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x000929F5 File Offset: 0x00090BF5
	public HitReaction()
	{
	}

	// Token: 0x040016F2 RID: 5874
	public UnityEvent OnImpact;
}
