using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class TutorialCorePickup : DiageticOption
{
	// Token: 0x060009D9 RID: 2521 RVA: 0x000413CA File Offset: 0x0003F5CA
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x000413D2 File Offset: 0x0003F5D2
	public override void Activate()
	{
		base.Activate();
		this.LoopSystem.Play();
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x000413E5 File Offset: 0x0003F5E5
	public override void Deactivate()
	{
		base.Deactivate();
		this.LoopSystem.Stop();
		this.PickupBurst.Play();
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00041403 File Offset: 0x0003F603
	public override void Select()
	{
		PlayerControl.myInstance.actions.SetCore(this.Core);
		this.Deactivate();
		PlayerControl.myInstance.AugmentsChanged();
		Action<TutorialCorePickup> onSelected = this.OnSelected;
		if (onSelected == null)
		{
			return;
		}
		onSelected(this);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0004143B File Offset: 0x0003F63B
	public TutorialCorePickup()
	{
	}

	// Token: 0x04000838 RID: 2104
	public ParticleSystem LoopSystem;

	// Token: 0x04000839 RID: 2105
	public ParticleSystem PickupBurst;

	// Token: 0x0400083A RID: 2106
	public AugmentTree CoreInfo;

	// Token: 0x0400083B RID: 2107
	public AugmentTree Core;

	// Token: 0x0400083C RID: 2108
	public Action<TutorialCorePickup> OnSelected;
}
