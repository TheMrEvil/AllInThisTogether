using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019A RID: 410
public class GhostLoadout : MonoBehaviour
{
	// Token: 0x06001131 RID: 4401 RVA: 0x0006A7BC File Offset: 0x000689BC
	private void Awake()
	{
		this.cgp = base.GetComponent<CanvasGroup>();
		this.allParticles.Add(this.WillRezFX);
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0006A7DC File Offset: 0x000689DC
	private void Update()
	{
		if (this.cgp.alpha <= 0f)
		{
			foreach (ParticleSystem particleSystem in this.allParticles)
			{
				if (particleSystem.isPlaying)
				{
					particleSystem.Stop();
				}
			}
			return;
		}
		if (PlayerControl.myInstance == null || !PlayerControl.myInstance.IsDead || !PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom.PlayerCount <= 1)
		{
			return;
		}
		bool hasAutoRevivesAvailable = PlayerControl.myInstance.Health.HasAutoRevivesAvailable;
		this.AutoRevivePrompt.UpdateOpacity(hasAutoRevivesAvailable, 3f, true);
		float reviveProgress = PlayerControl.myInstance.Health.ReviveProgress;
		if (reviveProgress >= 1f && !this.WillRezFX.isPlaying)
		{
			this.WillRezFX.Play();
		}
		else if (reviveProgress < 1f && this.WillRezFX.isPlaying)
		{
			this.WillRezFX.Stop();
		}
		this.WillRezFill.fillAmount = Mathf.Lerp(this.WillRezFill.fillAmount, reviveProgress, Time.deltaTime * 8f);
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0006A914 File Offset: 0x00068B14
	public GhostLoadout()
	{
	}

	// Token: 0x04000F8E RID: 3982
	[Header("Rez Capability")]
	public ParticleSystem WillRezFX;

	// Token: 0x04000F8F RID: 3983
	public Image WillRezFill;

	// Token: 0x04000F90 RID: 3984
	public Image AbilityIcon;

	// Token: 0x04000F91 RID: 3985
	public CanvasGroup AutoRevivePrompt;

	// Token: 0x04000F92 RID: 3986
	private CanvasGroup cgp;

	// Token: 0x04000F93 RID: 3987
	private List<ParticleSystem> allParticles = new List<ParticleSystem>();
}
