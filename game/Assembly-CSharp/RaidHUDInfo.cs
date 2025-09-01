using System;
using TMPro;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class RaidHUDInfo : MonoBehaviour
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06001290 RID: 4752 RVA: 0x00072805 File Offset: 0x00070A05
	private bool ShouldShow
	{
		get
		{
			return RaidManager.IsInRaid && RaidManager.IsEncounterStarted && !string.IsNullOrEmpty(RaidHUDInfo.Text);
		}
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x00072828 File Offset: 0x00070A28
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		RaidManager.OnEncounterPrepared = (Action)Delegate.Combine(RaidManager.OnEncounterPrepared, new Action(this.ClearInfo));
		RaidManager.OnEncounterEnded = (Action)Delegate.Combine(RaidManager.OnEncounterEnded, new Action(this.ClearInfo));
		RaidManager.OnEncounterReset = (Action)Delegate.Combine(RaidManager.OnEncounterReset, new Action(this.ClearInfo));
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x000728B1 File Offset: 0x00070AB1
	private void ClearInfo()
	{
		RaidHUDInfo.Text = "";
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x000728BD File Offset: 0x00070ABD
	public static void UpdateInfo(string info)
	{
		RaidHUDInfo.Text = TextParser.AugmentDetail(info, null, null, false);
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000728D0 File Offset: 0x00070AD0
	private void Update()
	{
		this.canvasGroup.UpdateOpacity(this.ShouldShow, 2f, true);
		if (!this.ShouldShow)
		{
			return;
		}
		if (!this.Label.text.Equals(RaidHUDInfo.Text))
		{
			this.Label.text = RaidHUDInfo.Text;
			if (!string.IsNullOrEmpty(RaidHUDInfo.Text))
			{
				this.TextChangeVFX.Play();
				AudioManager.PlayInterfaceSFX(this.TextChangeSFX, 1f, 0f);
			}
		}
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x00072950 File Offset: 0x00070B50
	public RaidHUDInfo()
	{
	}

	// Token: 0x04001192 RID: 4498
	private CanvasGroup canvasGroup;

	// Token: 0x04001193 RID: 4499
	public TextMeshProUGUI Label;

	// Token: 0x04001194 RID: 4500
	public ParticleSystem TextChangeVFX;

	// Token: 0x04001195 RID: 4501
	public AudioClip TextChangeSFX;

	// Token: 0x04001196 RID: 4502
	public static string Text;
}
