using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D8 RID: 472
public class ConfirmationPrompt : MonoBehaviour
{
	// Token: 0x0600139D RID: 5021 RVA: 0x00079C87 File Offset: 0x00077E87
	private void Awake()
	{
		ConfirmationPrompt.instance = this;
		ConfirmationPrompt.IsInPrompt = false;
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.CheckInputMethod));
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00079CB8 File Offset: 0x00077EB8
	public static void OpenPrompt(string description, string confirmBtn, string cancelBtn, Action<bool> responseAction, float holdTime = 0f)
	{
		ConfirmationPrompt.instance.OnActivate();
		ConfirmationPrompt.instance.OnResponse = responseAction;
		ConfirmationPrompt.instance.BodyText.text = description;
		ConfirmationPrompt.instance.ConfirmText.text = confirmBtn;
		ConfirmationPrompt.instance.CancelText.text = cancelBtn;
		ConfirmationPrompt.instance.TitleText.text = "";
		ConfirmationPrompt.instance.ShowDefault();
		ConfirmationPrompt.SetHoldRequirement(holdTime);
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x00079D30 File Offset: 0x00077F30
	public static void UseTertiary(string prompt, Action responseAction, bool hideCancel)
	{
		ConfirmationPrompt.instance.TertiaryButton.SetActive(true);
		ConfirmationPrompt.instance.usingTertiary = true;
		ConfirmationPrompt.instance.TertiaryText.text = prompt;
		ConfirmationPrompt.instance.onTertiary = responseAction;
		if (hideCancel)
		{
			ConfirmationPrompt.instance.CancelButton.SetActive(false);
		}
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00079D86 File Offset: 0x00077F86
	public static void SetHoldRequirement(float value)
	{
		ConfirmationPrompt.instance.RequireHold = (value > 0f);
		ConfirmationPrompt.instance.HoldDuration = value;
		ConfirmationPrompt.instance.HoldPrompt.SetActive(ConfirmationPrompt.instance.RequireHold);
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x00079DBE File Offset: 0x00077FBE
	public static void SetTitle(string title)
	{
		ConfirmationPrompt.instance.TitleText.text = title;
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00079DD0 File Offset: 0x00077FD0
	private void ShowDefault()
	{
		this.PrestigeIcon.gameObject.SetActive(false);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00079DE4 File Offset: 0x00077FE4
	public void ShowPrestige(int level)
	{
		this.PrestigeIcon.gameObject.SetActive(true);
		this.PrestigeIcon.sprite = MetaDB.GetPrestigeIcon(level);
		Unlockable prestigeRewardDisplay = UnlockDB.GetPrestigeRewardDisplay(level);
		this.PrestigeRewardDisplay.gameObject.SetActive(prestigeRewardDisplay != null);
		if (prestigeRewardDisplay != null)
		{
			this.PrestigeRewardDisplay.Setup(0, prestigeRewardDisplay);
		}
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00079E40 File Offset: 0x00078040
	private void OnActivate()
	{
		this.BlurBack.SetActive(true);
		this.TertiaryButton.SetActive(false);
		this.CancelButton.SetActive(true);
		ConfirmationPrompt.IsInPrompt = true;
		this.isConfirmPressed = false;
		this.usingTertiary = false;
		this.Fader.UpdateOpacity(true, 1f, false);
		ConfirmationPrompt.instance.HoldT = 0f;
		this.CheckInputMethod();
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00079EAC File Offset: 0x000780AC
	private void CheckInputMethod()
	{
		foreach (GameObject gameObject in this.ControllerPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x00079F04 File Offset: 0x00078104
	private void Update()
	{
		this.Fader.UpdateOpacity(ConfirmationPrompt.IsInPrompt, 4f, false);
		bool flag = ConfirmationPrompt.IsInPrompt || this.Fader.alpha > 0f;
		if (this.BlurBack.activeSelf != flag)
		{
			this.BlurBack.gameObject.SetActive(flag);
		}
		if (ConfirmationPrompt.IsInPrompt)
		{
			if (InputManager.IsUsingController)
			{
				if (InputManager.UIAct.UISecondary.WasPressed && !this.isConfirmPressed)
				{
					this.ConfirmDown();
				}
				else if (this.isConfirmPressed && !InputManager.UIAct.UISecondary.IsPressed)
				{
					this.ConfirmUp();
				}
				else if (InputManager.UIAct.UITertiary.WasPressed)
				{
					this.Tertiary();
				}
			}
			if (InputManager.UIAct.UIBack.WasPressed)
			{
				this.Cancel();
			}
		}
		if (this.RequireHold && this.isConfirmPressed)
		{
			if (this.isConfirmPressed)
			{
				if (this.HoldT > this.HoldDuration)
				{
					this.ConfirmFinalize();
				}
				this.HoldT += Time.deltaTime;
				this.HoldFill.fillAmount = this.HoldT / Mathf.Max(this.HoldDuration, 0.1f);
				return;
			}
			if (this.HoldFill.fillAmount > 0f)
			{
				this.HoldFill.fillAmount = Mathf.Lerp(this.HoldFill.fillAmount, 0f, Time.deltaTime * 6f);
				return;
			}
		}
		else
		{
			this.HoldFill.fillAmount = 0f;
		}
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x0007A096 File Offset: 0x00078296
	public void ConfirmDown()
	{
		if (!ConfirmationPrompt.IsInPrompt)
		{
			return;
		}
		if (!this.RequireHold)
		{
			this.ConfirmFinalize();
			return;
		}
		this.HoldT = 0f;
		this.isConfirmPressed = true;
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x0007A0C1 File Offset: 0x000782C1
	public void ConfirmUp()
	{
		this.isConfirmPressed = false;
		this.HoldT = 0f;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x0007A0D5 File Offset: 0x000782D5
	private void ConfirmFinalize()
	{
		if (!ConfirmationPrompt.IsInPrompt)
		{
			return;
		}
		this.ExitPrompt();
		Action<bool> onResponse = this.OnResponse;
		if (onResponse != null)
		{
			onResponse(true);
		}
		this.HoldT = 0f;
		this.isConfirmPressed = false;
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x0007A109 File Offset: 0x00078309
	public void Cancel()
	{
		this.ExitPrompt();
		Action<bool> onResponse = this.OnResponse;
		if (onResponse == null)
		{
			return;
		}
		onResponse(false);
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x0007A122 File Offset: 0x00078322
	public void Tertiary()
	{
		this.ExitPrompt();
		Action action = this.onTertiary;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x0007A13A File Offset: 0x0007833A
	private void ExitPrompt()
	{
		ConfirmationPrompt.IsInPrompt = false;
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x0007A142 File Offset: 0x00078342
	public ConfirmationPrompt()
	{
	}

	// Token: 0x040012B8 RID: 4792
	public static ConfirmationPrompt instance;

	// Token: 0x040012B9 RID: 4793
	public static bool IsInPrompt;

	// Token: 0x040012BA RID: 4794
	public CanvasGroup Fader;

	// Token: 0x040012BB RID: 4795
	public TextMeshProUGUI TitleText;

	// Token: 0x040012BC RID: 4796
	public TextMeshProUGUI BodyText;

	// Token: 0x040012BD RID: 4797
	public TextMeshProUGUI ConfirmText;

	// Token: 0x040012BE RID: 4798
	public TextMeshProUGUI CancelText;

	// Token: 0x040012BF RID: 4799
	public GameObject CancelButton;

	// Token: 0x040012C0 RID: 4800
	[Header("Tertiary")]
	public GameObject TertiaryButton;

	// Token: 0x040012C1 RID: 4801
	public TextMeshProUGUI TertiaryText;

	// Token: 0x040012C2 RID: 4802
	private Action onTertiary;

	// Token: 0x040012C3 RID: 4803
	private bool usingTertiary;

	// Token: 0x040012C4 RID: 4804
	public List<GameObject> ControllerPrompts;

	// Token: 0x040012C5 RID: 4805
	public GameObject BlurBack;

	// Token: 0x040012C6 RID: 4806
	public GameObject HoldPrompt;

	// Token: 0x040012C7 RID: 4807
	public Image HoldFill;

	// Token: 0x040012C8 RID: 4808
	[Header("Prestige")]
	public Image PrestigeIcon;

	// Token: 0x040012C9 RID: 4809
	public Scriptorium_PrestigeRewardItem PrestigeRewardDisplay;

	// Token: 0x040012CA RID: 4810
	private Action<bool> OnResponse;

	// Token: 0x040012CB RID: 4811
	private bool RequireHold;

	// Token: 0x040012CC RID: 4812
	private float HoldDuration;

	// Token: 0x040012CD RID: 4813
	private float HoldT;

	// Token: 0x040012CE RID: 4814
	private bool isConfirmPressed;
}
