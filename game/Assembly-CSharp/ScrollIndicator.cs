using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class ScrollIndicator : Indicatable
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x0600095B RID: 2395 RVA: 0x0003EF9F File Offset: 0x0003D19F
	public override Transform Root
	{
		get
		{
			return this.Anchor;
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0003EFA7 File Offset: 0x0003D1A7
	private void Start()
	{
		WorldIndicators.Indicate(this);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0003EFB0 File Offset: 0x0003D1B0
	public override bool ShouldIndicate()
	{
		return base.ShouldIndicate() && this.scroll.IsAvailable && Vector3.Distance(PlayerControl.myInstance.display.CenterOfMass.position, base.transform.position) > 15f;
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0003F001 File Offset: 0x0003D201
	private void OnDestroy()
	{
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0003F009 File Offset: 0x0003D209
	public ScrollIndicator()
	{
	}

	// Token: 0x040007BD RID: 1981
	public Transform Anchor;

	// Token: 0x040007BE RID: 1982
	public ScrollPickup scroll;
}
