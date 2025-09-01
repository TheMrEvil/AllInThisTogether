using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class HitMarker : MonoBehaviour
{
	// Token: 0x06001208 RID: 4616 RVA: 0x0006FD4C File Offset: 0x0006DF4C
	public static void Show(DNumType dNum, int amount, Vector3 hitPoint, int depth)
	{
		HitMarker.instance.ShowMarker(dNum == DNumType.Crit);
		if (GameHUD.Mode != GameHUD.HUDMode.Off)
		{
			CombatTextController.ShowDamageNum(dNum, amount, hitPoint, depth);
		}
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0006FD6D File Offset: 0x0006DF6D
	public static void KillMarker()
	{
		HitMarker.instance.killImage.alpha = 1f;
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0006FD83 File Offset: 0x0006DF83
	private void Awake()
	{
		HitMarker.instance = this;
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x0006FD8C File Offset: 0x0006DF8C
	private void ShowMarker(bool isCrit)
	{
		if (this.lastImpact > 0f)
		{
			return;
		}
		this.lastImpact = this.MarkCooldown;
		if (isCrit)
		{
			this.critImage.alpha = 1f;
		}
		else
		{
			this.baseImage.alpha = 1f;
		}
		AudioManager.PlayClipAtPoint(isCrit ? this.critClip : this.hitClips.GetRandomClip(-1), Vector3.zero, isCrit ? 1f : 0.75f, 1f, 0f, 10f, 250f);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0006FE20 File Offset: 0x0006E020
	private void Update()
	{
		this.killImage.UpdateOpacity(false, 2f, true);
		this.baseImage.UpdateOpacity(false, 2f, true);
		this.critImage.UpdateOpacity(false, 2f, true);
		if (this.lastImpact > 0f)
		{
			this.lastImpact -= Time.deltaTime;
		}
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0006FE82 File Offset: 0x0006E082
	public HitMarker()
	{
	}

	// Token: 0x040010D5 RID: 4309
	private static HitMarker instance;

	// Token: 0x040010D6 RID: 4310
	public CanvasGroup killImage;

	// Token: 0x040010D7 RID: 4311
	public CanvasGroup baseImage;

	// Token: 0x040010D8 RID: 4312
	public CanvasGroup critImage;

	// Token: 0x040010D9 RID: 4313
	public List<AudioClip> hitClips;

	// Token: 0x040010DA RID: 4314
	public AudioClip critClip;

	// Token: 0x040010DB RID: 4315
	public float MarkCooldown;

	// Token: 0x040010DC RID: 4316
	private float lastImpact;
}
