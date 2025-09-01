using System;
using System.Collections.Generic;
using Fluxy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EB RID: 491
public class ProgressionPanel : MonoBehaviour
{
	// Token: 0x060014E5 RID: 5349 RVA: 0x0008345C File Offset: 0x0008165C
	private void Awake()
	{
		ProgressionPanel.instance = this;
		this.LevelItemRef.SetActive(false);
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnterPanel));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnLeftPanel));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.GoToCores));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.GoToInkTalents));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.OnSecondaryAction));
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0008352C File Offset: 0x0008172C
	private void Start()
	{
		Progression.TalentBuild = Settings.GetTalentBuild();
		Progression.LibraryBuild = Settings.GetLibTalentBuild();
		if (this.levelItems.Count == 0)
		{
			for (int i = 0; i < PlayerDB.InkLevels.Count; i++)
			{
				this.CreateLevelItem(PlayerDB.InkLevels[i], i);
			}
			UISelector.SetupHorizontalListNav<InkLevel_Item>(this.levelItems, null, null, true);
		}
		UISelector.SetupHorizontalListNav<InkLevel_Item>(this.levelItems, null, null, false);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000835A0 File Offset: 0x000817A0
	private void OnEnterPanel()
	{
		this.UpdateLevelText();
		this.UpdateInkIcon();
		this.Scrolling.normalizedPosition = new Vector2((float)Progression.InkLevel / (float)Progression.MaxInkLevel, 0f);
		this.LevelChanged();
		if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Meta) && LibraryManager.InLibraryTutorial)
		{
			if (Currency.LoadoutCoin < 60)
			{
				Currency.AddLoadoutCoin(60 - Currency.LoadoutCoin, true);
			}
			LibraryManager.FinishedTutorial(LibraryTutorial.Meta);
			UITutorial.TryTutorial(UITutorial.Tutorial.Meta);
		}
		UISelector.SelectSelectable(this.levelItems[Progression.InkLevel - 1].GetComponent<Button>());
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00083630 File Offset: 0x00081830
	private void OnLeftPanel()
	{
		this.LevelupUI.DepositUp();
		this.LevelupUI.CancelImmediate();
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00083648 File Offset: 0x00081848
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.SignatureInk)
		{
			return;
		}
		if (this.AutoScroll != null && InputManager.IsUsingController)
		{
			this.AutoScroll.TickUpdate();
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00083674 File Offset: 0x00081874
	private void CreateLevelItem(PlayerDB.TalentRow row, int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LevelItemRef, this.LevelItemRef.transform.parent);
		gameObject.SetActive(true);
		InkLevel_Item component = gameObject.GetComponent<InkLevel_Item>();
		component.Setup(row, index);
		this.levelItems.Add(component);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000836C0 File Offset: 0x000818C0
	public void LevelChanged()
	{
		this.TalentsUpdated();
		bool flag = Progression.InkLevel >= Progression.MaxInkLevel;
		this.MaxLevelDisplay.SetActive(flag);
		foreach (GameObject gameObject in this.LevelupObjects)
		{
			gameObject.SetActive(!flag);
		}
		InkLevel_Item inkLevel_Item = null;
		foreach (InkLevel_Item inkLevel_Item2 in this.levelItems)
		{
			if (inkLevel_Item2.Level == Progression.InkLevel)
			{
				inkLevel_Item = inkLevel_Item2;
			}
		}
		if (inkLevel_Item != null)
		{
			this.UnlockTalentFX.transform.position = inkLevel_Item.transform.position;
			this.UnlockTalentFX.Play();
		}
		this.PrestigeRewardDisplay.gameObject.SetActive(flag && !Progression.IsMaxPrestige);
		if (flag)
		{
			this.PrestigeRewardDisplay.Setup();
			this.PrestigeButton.gameObject.SetActive(!Progression.IsMaxPrestige);
			this.MaxPrestigeInfo.SetActive(Progression.IsMaxPrestige);
			this.CurPrestige.gameObject.SetActive(Progression.PrestigeCount > 0);
			this.CurPrestige.sprite = MetaDB.GetPrestigeIcon(Progression.PrestigeCount);
		}
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x00083838 File Offset: 0x00081A38
	private void TalentsUpdated()
	{
		Settings.SaveTalentBuild();
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance != null)
		{
			myInstance.UpdateTalents();
		}
		this.UpdateLevelText();
		foreach (InkLevel_Item inkLevel_Item in this.levelItems)
		{
			inkLevel_Item.UpdateUnlockDisplay();
		}
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000838A4 File Offset: 0x00081AA4
	private void UpdateLevelText()
	{
		this.LevelText.text = Progression.InkLevel.ToString();
		this.NextLevelText.text = (Progression.InkLevel + 1).ToString();
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000838E4 File Offset: 0x00081AE4
	private void UpdateInkIcon()
	{
		InkCoresPanel inkCoresPanel = InkCoresPanel.instance;
		PlayerControl myInstance = PlayerControl.myInstance;
		InkCoresPanel.CoreInfo coreInfo = inkCoresPanel.GetCoreInfo((myInstance != null) ? myInstance.actions.core : null);
		if (coreInfo == null)
		{
			return;
		}
		this.InkIconFront.sprite = coreInfo.BaseIcon;
		this.InkIconBack.sprite = coreInfo.LockIcon;
		this.InkIconGlow.sprite = coreInfo.BorderIcon;
		VertexGradient colorGradient = this.InkColorText.colorGradient;
		colorGradient.bottomLeft = coreInfo.GradientColor;
		colorGradient.bottomRight = coreInfo.GradientColor;
		this.InkColorText.colorGradient = colorGradient;
		this.InkFluxy.color = coreInfo.FluidColor;
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0008398C File Offset: 0x00081B8C
	public void RequestPrestige()
	{
		if (Progression.InkLevel < Progression.MaxInkLevel || ConfirmationPrompt.IsInPrompt)
		{
			return;
		}
		ConfirmationPrompt.OpenPrompt(this.PromptInfo, "Confirm", "Cancel", new Action<bool>(this.ConfirmPrestige), 2f);
		ConfirmationPrompt.SetTitle("ASCEND");
		ConfirmationPrompt.instance.ShowPrestige(Progression.PrestigeCount + 1);
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000839EE File Offset: 0x00081BEE
	public void ConfirmPrestige(bool wasConfirmed)
	{
		if (!wasConfirmed)
		{
			return;
		}
		Progression.Prestige();
		UnlockManager.PrestigeLevelUpdated(Progression.PrestigeCount);
		SignatureInkUIControl.instance.BeginPrestigeSequence(true);
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x00083A0E File Offset: 0x00081C0E
	private void OnSecondaryAction()
	{
		if (Progression.InkLevel >= Progression.MaxInkLevel)
		{
			if (!Progression.IsMaxPrestige)
			{
				this.RequestPrestige();
			}
			return;
		}
		this.LevelupUI.DepositDown();
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x00083A3A File Offset: 0x00081C3A
	public void ExitPanel()
	{
		if (PanelManager.CurPanel != PanelType.SignatureInk)
		{
			return;
		}
		PanelManager.instance.PopPanel();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x00083A50 File Offset: 0x00081C50
	public void GoToInkTalents()
	{
		InkTalentsPanel.instance.OpenUI(MagicColor.Neutral);
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00083A5D File Offset: 0x00081C5D
	public void GoToCores()
	{
		InkCoresPanel.instance.OpenUI(PlayerControl.myInstance.actions.core.Root.magicColor);
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x00083A82 File Offset: 0x00081C82
	public ProgressionPanel()
	{
	}

	// Token: 0x04001449 RID: 5193
	public static ProgressionPanel instance;

	// Token: 0x0400144A RID: 5194
	public TextMeshProUGUI LevelText;

	// Token: 0x0400144B RID: 5195
	public TextMeshProUGUI NextLevelText;

	// Token: 0x0400144C RID: 5196
	public List<GameObject> LevelupObjects;

	// Token: 0x0400144D RID: 5197
	public GameObject MaxLevelDisplay;

	// Token: 0x0400144E RID: 5198
	public InkTalentLevelupUI LevelupUI;

	// Token: 0x0400144F RID: 5199
	public Image InkIconBack;

	// Token: 0x04001450 RID: 5200
	public Image InkIconFront;

	// Token: 0x04001451 RID: 5201
	public Image InkIconGlow;

	// Token: 0x04001452 RID: 5202
	public TextMeshProUGUI InkColorText;

	// Token: 0x04001453 RID: 5203
	public FluxyTarget InkFluxy;

	// Token: 0x04001454 RID: 5204
	public AutoScrollRect AutoScroll;

	// Token: 0x04001455 RID: 5205
	public GameObject LevelItemRef;

	// Token: 0x04001456 RID: 5206
	public RectTransform HorizontalRow;

	// Token: 0x04001457 RID: 5207
	public ScrollRect Scrolling;

	// Token: 0x04001458 RID: 5208
	private List<InkLevel_Item> levelItems = new List<InkLevel_Item>();

	// Token: 0x04001459 RID: 5209
	public ParticleSystem UnlockTalentFX;

	// Token: 0x0400145A RID: 5210
	public Scriptorium_PrestigeArea PrestigeRewardDisplay;

	// Token: 0x0400145B RID: 5211
	[TextArea(6, 8)]
	public string PromptInfo;

	// Token: 0x0400145C RID: 5212
	public GameObject MaxPrestigeInfo;

	// Token: 0x0400145D RID: 5213
	public Button PrestigeButton;

	// Token: 0x0400145E RID: 5214
	public Image CurPrestige;
}
