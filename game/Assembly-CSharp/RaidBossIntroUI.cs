using System;
using TMPro;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class RaidBossIntroUI : MonoBehaviour
{
	// Token: 0x0600128C RID: 4748 RVA: 0x00072747 File Offset: 0x00070947
	private void Awake()
	{
		RaidBossIntroUI.instance = this;
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x00072750 File Offset: 0x00070950
	public void Setup(string bossName, string subName = "")
	{
		RaidScene raidScene = RaidScene.instance;
		if (raidScene == null)
		{
			return;
		}
		AudioManager.PlayLoudSFX2D(this.IntroSFX, 1f, 0.1f);
		this.Fader.alpha = 1f;
		base.transform.SetPositionAndRotation(raidScene.BossNamePoint.position, raidScene.BossNamePoint.rotation);
		this.NameText.text = bossName;
		this.SubnameText.text = subName;
		this.Anim.Play("Intro");
		base.Invoke("HideDelayed", 3.5f);
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x000727EB File Offset: 0x000709EB
	private void HideDelayed()
	{
		this.Fader.alpha = 0f;
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000727FD File Offset: 0x000709FD
	public RaidBossIntroUI()
	{
	}

	// Token: 0x0400118C RID: 4492
	public static RaidBossIntroUI instance;

	// Token: 0x0400118D RID: 4493
	public Animator Anim;

	// Token: 0x0400118E RID: 4494
	public TextMeshProUGUI NameText;

	// Token: 0x0400118F RID: 4495
	public TextMeshProUGUI SubnameText;

	// Token: 0x04001190 RID: 4496
	public CanvasGroup Fader;

	// Token: 0x04001191 RID: 4497
	public AudioClip IntroSFX;
}
