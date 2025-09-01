using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class LibraryMotePickup : PlayerTrigger
{
	// Token: 0x06000CFA RID: 3322 RVA: 0x00052D34 File Offset: 0x00050F34
	internal override void OnEntered(PlayerControl plr)
	{
		base.OnEntered(plr);
		AudioManager.PlayClipAtPoint(this.PickupSFX, base.transform.position, 1f, 1f, 1f, 10f, 250f);
		if (this.Type == LibraryMotePickup.MoteType.Quillmark)
		{
			Currency.AddLoadoutCoin(this.Amount, true);
		}
		else if (this.Type == LibraryMotePickup.MoteType.Gilding)
		{
			Currency.Add(this.Amount, true);
		}
		LibraryMoteManager.MoteCollectedLocal(this);
		if (this.Status != null && !LibraryRaces.IsPlayerRacing)
		{
			plr.Net.ApplyStatus(this.Status.HashCode, -1, 0f, 0, false, 0);
		}
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x00052DDD File Offset: 0x00050FDD
	public override void Trigger()
	{
		if (this.didTrigger)
		{
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject, this.DestroyDelay);
		base.Trigger();
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x00052DFF File Offset: 0x00050FFF
	public LibraryMotePickup()
	{
	}

	// Token: 0x04000A5D RID: 2653
	public LibraryMotePickup.MoteType Type;

	// Token: 0x04000A5E RID: 2654
	public int Amount;

	// Token: 0x04000A5F RID: 2655
	public AudioClip PickupSFX;

	// Token: 0x04000A60 RID: 2656
	public float DestroyDelay = 1.5f;

	// Token: 0x04000A61 RID: 2657
	public StatusTree Status;

	// Token: 0x02000518 RID: 1304
	public enum MoteType
	{
		// Token: 0x040025CC RID: 9676
		Quillmark,
		// Token: 0x040025CD RID: 9677
		Gilding
	}
}
