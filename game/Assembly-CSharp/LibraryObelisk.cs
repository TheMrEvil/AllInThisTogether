using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class LibraryObelisk : MonoBehaviour
{
	// Token: 0x06000CFD RID: 3325 RVA: 0x00052E14 File Offset: 0x00051014
	public void GiveReward()
	{
		Vector3 position = PlayerControl.myInstance.Display.GetLocation(ActionLocation.Floor).position;
		this.ApplyFX.transform.position = position;
		this.ApplyFX.Play();
		AudioManager.PlayClipAtPoint(this.ApplySFX, position, 1f, 1f, 1f, 10f, 250f);
		if (PlayerControl.myInstance.HasStatusEffectGUID(this.Small.ID))
		{
			PlayerControl.myInstance.Net.RemoveStatusNetwork(this.Small.HashCode, 0, 0, false, true);
			PlayerControl.myInstance.Net.ApplyStatus(this.Big.HashCode, 0, 0f, 1, false, 0);
			return;
		}
		if (PlayerControl.myInstance.HasStatusEffectGUID(this.Big.ID))
		{
			PlayerControl.myInstance.Net.RemoveStatusNetwork(this.Big.HashCode, 0, 0, false, true);
			return;
		}
		PlayerControl.myInstance.Net.ApplyStatus(this.Small.HashCode, 0, 0f, 1, false, 0);
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00052F2B File Offset: 0x0005112B
	public LibraryObelisk()
	{
	}

	// Token: 0x04000A62 RID: 2658
	public ParticleSystem ApplyFX;

	// Token: 0x04000A63 RID: 2659
	public AudioClip ApplySFX;

	// Token: 0x04000A64 RID: 2660
	public StatusTree Small;

	// Token: 0x04000A65 RID: 2661
	public StatusTree Big;
}
