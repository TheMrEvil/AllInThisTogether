using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E2 RID: 482
public class InkTalentLevelupUI : MonoBehaviour
{
	// Token: 0x06001436 RID: 5174 RVA: 0x0007E0F8 File Offset: 0x0007C2F8
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.SignatureInk)
		{
			return;
		}
		int nextInkLevelRequirement = PlayerDB.GetNextInkLevelRequirement(Progression.InkLevel);
		int levelProgress = Progression.LevelProgress;
		string text = levelProgress.ToString() + " / " + nextInkLevelRequirement.ToString();
		if (this.FillInfoText.text != text)
		{
			this.FillInfoText.text = text;
		}
		float num = (float)levelProgress / Mathf.Max((float)nextInkLevelRequirement, 1f);
		if (Mathf.Abs(this.FillBar.fillAmount - num) < 0.33f)
		{
			this.FillBar.fillAmount = Mathf.Lerp(this.FillBar.fillAmount, num, Time.deltaTime * 8f);
		}
		else
		{
			this.FillBar.fillAmount = num;
		}
		if (InputManager.IsUsingController && InputManager.UIAct.UISecondary.WasReleased)
		{
			this.DepositUp();
		}
		this.UpdateDeposit();
		if (this.SaveDelay > 0f)
		{
			this.SaveDelay -= Time.deltaTime;
			if (this.SaveDelay < 0f)
			{
				Progression.SaveState();
				Currency.Save();
				if (PlayerControl.myInstance != null)
				{
					PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaEvent, null, 1f);
				}
			}
		}
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x0007E230 File Offset: 0x0007C430
	public void DepositDown()
	{
		if (Currency.LoadoutCoin <= 0 || this.isDepositing || Progression.InkLevel >= Progression.MaxInkLevel)
		{
			return;
		}
		AudioManager.PlayInterfaceSFX(SignatureInkUIControl.instance.LevelStartSFX, 1f, 0f);
		SignatureInkUIControl.instance.ProgressLoop.Play();
		SignatureInkUIControl.instance.LevelCharge.Play();
		this.isDepositing = true;
		this.depositT = 0f;
		this.holdTime = 0f;
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x0007E2AE File Offset: 0x0007C4AE
	public void CancelImmediate()
	{
		SignatureInkUIControl.instance.ProgressLoop.Stop();
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x0007E2BF File Offset: 0x0007C4BF
	public void DepositUp()
	{
		if (!this.isDepositing)
		{
			return;
		}
		SignatureInkUIControl.instance.LevelCharge.Stop();
		this.isDepositing = false;
		this.SaveDelay = 0.5f;
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0007E2EC File Offset: 0x0007C4EC
	private void UpdateDeposit()
	{
		if (!this.isDepositing)
		{
			this.EndDepositing();
			return;
		}
		if (Currency.LoadoutCoin <= 0)
		{
			this.DepositUp();
		}
		if (SignatureInkUIControl.instance.ProgressLoop.volume < 1f)
		{
			SignatureInkUIControl.instance.ProgressLoop.volume += Time.deltaTime * 4f;
		}
		this.depositT -= Time.deltaTime;
		this.holdTime += Time.deltaTime;
		int nextInkLevelRequirement = PlayerDB.GetNextInkLevelRequirement(Progression.InkLevel);
		if (this.depositT <= 0f)
		{
			float num = Mathf.Max(1f, this.holdTime);
			this.depositT = 0.1f / num;
			num *= Mathf.Max(1f, Mathf.Sqrt(Mathf.Pow((float)nextInkLevelRequirement, 0.8f)) - 3f);
			int num2 = (int)Mathf.Max(1f, 0.5f * num);
			num2 = Mathf.Min(num2, Currency.LoadoutCoin);
			num2 = Mathf.Min(num2, Progression.RemainingInkForLevel());
			Currency.SpendLoadoutCoin(num2, false);
			this.numDepositted += num2;
			LibraryInfoWidget.QuilmarkBurst(num2, this.FillBar.transform);
			List<AudioClip> clips = this.CoinFew;
			if (num2 > 3)
			{
				clips = this.CoinMany;
			}
			else if (num2 > 1)
			{
				clips = this.CoinFew;
			}
			float num3 = (float)Progression.LevelProgress / Mathf.Max((float)nextInkLevelRequirement, 1f);
			AudioManager.PlayInterfaceSFX(clips.GetRandomClip(-1), 1f, 0.8f + 0.4f * num3);
			if (Progression.AddLevelProgress(num2, false))
			{
				this.DepositUp();
				this.DidLevelUp();
			}
		}
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0007E488 File Offset: 0x0007C688
	private void EndDepositing()
	{
		if (SignatureInkUIControl.instance == null)
		{
			return;
		}
		if (SignatureInkUIControl.instance.ProgressLoop.volume > 0f)
		{
			SignatureInkUIControl.instance.ProgressLoop.volume -= Time.deltaTime * 2f;
			if (SignatureInkUIControl.instance.ProgressLoop.volume <= 0f)
			{
				SignatureInkUIControl.instance.ProgressLoop.Stop();
			}
		}
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x0007E4FF File Offset: 0x0007C6FF
	private void DidLevelUp()
	{
		SignatureInkUIControl.instance.LevelUp();
		ProgressionPanel.instance.LevelChanged();
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x0007E515 File Offset: 0x0007C715
	public InkTalentLevelupUI()
	{
	}

	// Token: 0x04001370 RID: 4976
	public TextMeshProUGUI FillInfoText;

	// Token: 0x04001371 RID: 4977
	public Image FillBar;

	// Token: 0x04001372 RID: 4978
	public CanvasGroup ButtonGroup;

	// Token: 0x04001373 RID: 4979
	public List<AudioClip> CoinSingle;

	// Token: 0x04001374 RID: 4980
	public List<AudioClip> CoinFew;

	// Token: 0x04001375 RID: 4981
	public List<AudioClip> CoinMany;

	// Token: 0x04001376 RID: 4982
	private bool isDepositing;

	// Token: 0x04001377 RID: 4983
	private float depositT;

	// Token: 0x04001378 RID: 4984
	private float holdTime;

	// Token: 0x04001379 RID: 4985
	private int numDepositted;

	// Token: 0x0400137A RID: 4986
	private float SaveDelay;
}
