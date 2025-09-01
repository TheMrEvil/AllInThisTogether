using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019C RID: 412
public class BonusUI : MonoBehaviour
{
	// Token: 0x0600113A RID: 4410 RVA: 0x0006ACDE File Offset: 0x00068EDE
	private void Awake()
	{
		BonusUI.instance = this;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0006ACE8 File Offset: 0x00068EE8
	private void Update()
	{
		bool flag = GoalManager.HasObjective && GoalManager.StartedObjective;
		this.cGroup.UpdateOpacity(flag, 2f, true);
		if (flag)
		{
			bool inLastChance = GoalManager.InLastChance;
			this.ExclamationGroup.UpdateOpacity(!inLastChance, 4f, true);
			if (inLastChance)
			{
				this.LCBar.fillAmount = GoalManager.LastChanceT / 15f;
				if (this.nextTimerCD - 1f >= (float)Mathf.CeilToInt(GoalManager.LastChanceT))
				{
					this.nextTimerCD = (float)Mathf.CeilToInt(GoalManager.LastChanceT);
					this.anim.SetTrigger("NextWarn");
					this.TimerText.text = this.nextTimerCD.ToString();
				}
			}
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0006ADA1 File Offset: 0x00068FA1
	private void UpdateLastChance()
	{
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0006ADA4 File Offset: 0x00068FA4
	public void Setup()
	{
		if (!GoalManager.HasObjective)
		{
			return;
		}
		AugmentTree bonusObjective = GoalManager.BonusObjective;
		AugmentRootNode augmentRootNode = (bonusObjective != null) ? bonusObjective.Root : null;
		if (augmentRootNode == null)
		{
			return;
		}
		this.Title.text = augmentRootNode.Name;
		this.Detail.text = augmentRootNode.Detail;
		this.anim.Play("Reveal");
		this.cGroup.alpha = 1f;
		this.nextTimerCD = 16f;
		AudioManager.PlayInterfaceSFX(this.BonusStarted, 1f, 0f);
		ParticleSystem startedFX = this.StartedFX;
		if (startedFX != null)
		{
			startedFX.Play();
		}
		base.Invoke("ToSide", 3.5f);
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0006AE58 File Offset: 0x00069058
	public void UpdateDetail(string newText)
	{
		this.Detail.text = newText;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0006AE66 File Offset: 0x00069066
	private void ToSide()
	{
		AudioManager.PlayInterfaceSFX(this.BonusMoveOver, 1f, 0f);
		this.anim.Play("ToSide");
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0006AE8D File Offset: 0x0006908D
	public void Failed()
	{
		this.anim.ResetTrigger("NextWarn");
		this.anim.Play("Failed");
		AudioManager.PlayInterfaceSFX(this.BonusFailed, 1f, 0f);
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0006AEC4 File Offset: 0x000690C4
	public void Completed()
	{
		this.anim.Play("Completed");
		AudioManager.PlayInterfaceSFX(this.BonusCompleted, 1f, 0f);
		ParticleSystem completedFX = this.CompletedFX;
		if (completedFX != null)
		{
			completedFX.Play();
		}
		WaveProgressBar.instance.PulseBonusObjective();
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0006AF11 File Offset: 0x00069111
	public BonusUI()
	{
	}

	// Token: 0x04000FA3 RID: 4003
	public static BonusUI instance;

	// Token: 0x04000FA4 RID: 4004
	public TextMeshProUGUI Title;

	// Token: 0x04000FA5 RID: 4005
	public TextMeshProUGUI Detail;

	// Token: 0x04000FA6 RID: 4006
	public CanvasGroup cGroup;

	// Token: 0x04000FA7 RID: 4007
	[Header("Last Chance")]
	public CanvasGroup ExclamationGroup;

	// Token: 0x04000FA8 RID: 4008
	public TextMeshProUGUI TimerText;

	// Token: 0x04000FA9 RID: 4009
	public Image LCBar;

	// Token: 0x04000FAA RID: 4010
	private float nextTimerCD;

	// Token: 0x04000FAB RID: 4011
	[Header("Effects")]
	public Animator anim;

	// Token: 0x04000FAC RID: 4012
	public AudioClip BonusStarted;

	// Token: 0x04000FAD RID: 4013
	public AudioClip BonusMoveOver;

	// Token: 0x04000FAE RID: 4014
	public AudioClip BonusCompleted;

	// Token: 0x04000FAF RID: 4015
	public AudioClip BonusFailed;

	// Token: 0x04000FB0 RID: 4016
	public ParticleSystem StartedFX;

	// Token: 0x04000FB1 RID: 4017
	public ParticleSystem CompletedFX;
}
