using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x020001E1 RID: 481
public class InkCoresPanel : MonoBehaviour
{
	// Token: 0x06001421 RID: 5153 RVA: 0x0007D5B8 File Offset: 0x0007B7B8
	private void Awake()
	{
		InkCoresPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredUI));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(CanvasController.StopVideo));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.GoToInkTalents));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.GoToLevels));
		component.OnPageNext = (Action)Delegate.Combine(component.OnPageNext, new Action(this.NextInfo));
		component.OnPagePrev = (Action)Delegate.Combine(component.OnPagePrev, new Action(this.PrevInfo));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.ActionButtonClick));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
		for (int i = 0; i < this.CoreInfos.Count; i++)
		{
			this.InkDisplays[i].Setup(this.CoreInfos[i].Core, this.CoreInfos[i].BaseIcon, this.CoreInfos[i].LockIcon, this.CoreInfos[i].BorderIcon);
		}
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0007D74C File Offset: 0x0007B94C
	public void OpenUI(MagicColor color)
	{
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(color);
		if (core == null)
		{
			return;
		}
		PanelManager.instance.PushOrGoTo(PanelType.InkCores);
		this.SelectCore(core.core, false);
		if (InputManager.IsUsingController)
		{
			AbilityBoxUI coreButton = this.GetCoreButton(core.core);
			if (coreButton != null)
			{
				UISelector.SelectSelectable(coreButton.GetComponent<Button>());
			}
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
	private void OnEnteredUI()
	{
		this.WantShowPanel = true;
		this.PanelFader.alpha = 1f;
		this.PanelFader.interactable = true;
		this.PanelFader.blocksRaycasts = true;
		this.UpdateSelectorInfo();
		UITutorial.TryTutorial(UITutorial.Tutorial.Signatures);
		foreach (GameObject gameObject in this.TopTabs)
		{
			gameObject.SetActive(!Library_RaidControl.IsInAntechamber);
		}
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0007D83C File Offset: 0x0007BA3C
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.InkCores)
		{
			return;
		}
		if (this.WantShowPanel)
		{
			this.PanelFader.UpdateOpacity(true, 4f, false);
			return;
		}
		this.PanelFader.UpdateOpacity(false, 0.5f, false);
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x0007D878 File Offset: 0x0007BA78
	public void SelectCore(AugmentTree core, bool canEquip = false)
	{
		if (this.SelectedCore == core && canEquip)
		{
			this.TryEquip();
			return;
		}
		foreach (AbilityBoxUI abilityBoxUI in this.InkDisplays)
		{
			abilityBoxUI.SelectedDisplay.SetActive(core == abilityBoxUI.coreTree);
			abilityBoxUI.EquippedDisplay.SetActive(abilityBoxUI.coreTree == PlayerControl.myInstance.actions.core);
		}
		this.SelectedCore = core;
		this.SelectInfo(InkCoresPanel.CoreInfoType.Passive);
		this.UpdateButtonInfo();
		bool flag = RaidDB.IsAnyRaidUnlocked();
		this.StickerDisplay.gameObject.SetActive(flag);
		if (flag)
		{
			this.StickerDisplay.ShowStickers(this.SelectedCore.ID);
		}
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x0007D95C File Offset: 0x0007BB5C
	public void NextInfo()
	{
		int num = (int)this.SelectedTab;
		num++;
		int count = this.GetCoreInfo(this.SelectedCore).Details.Count;
		if (num >= count)
		{
			num = 0;
		}
		this.SelectInfo((InkCoresPanel.CoreInfoType)num);
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x0007D998 File Offset: 0x0007BB98
	public void PrevInfo()
	{
		int num = (int)this.SelectedTab;
		num--;
		if (num < 0)
		{
			num = this.GetCoreInfo(this.SelectedCore).Details.Count - 1;
		}
		this.SelectInfo((InkCoresPanel.CoreInfoType)num);
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x0007D9D4 File Offset: 0x0007BBD4
	private void SelectInfo(InkCoresPanel.CoreInfoType tab)
	{
		this.SelectedTab = tab;
		this.InfoNumberText.text = ((int)(tab + 1)).ToString() + " / " + this.GetCoreInfo(this.SelectedCore).Details.Count.ToString();
		if (this.GetInfoBase(this.SelectedTab) == null)
		{
			return;
		}
		this.UpdateTabBoxInfo();
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x0007DA3C File Offset: 0x0007BC3C
	private void OnInputChanged()
	{
		foreach (GameObject gameObject in this.KBMSwaps)
		{
			gameObject.SetActive(!InputManager.IsUsingController);
		}
		foreach (GameObject gameObject2 in this.ControllerSwaps)
		{
			gameObject2.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x0007DAD8 File Offset: 0x0007BCD8
	private void UpdateTabBoxInfo()
	{
		InkCoresPanel.CoreInfo coreInfo = this.GetCoreInfo(this.SelectedCore);
		if (coreInfo == null)
		{
			return;
		}
		InkCoresPanel.CoreVideo info = coreInfo.GetInfo(this.SelectedTab);
		if (info == null)
		{
			return;
		}
		this.InfoTitle.text = info.Title;
		this.InfoDetail.text = TextParser.AugmentDetail(info.Details, null, null, false);
		this.InfoIcon.sprite = info.Icon;
		CanvasController.ChangeVideo(info.Video);
		this.CoreMechTitle.text = coreInfo.CoreMechanicTitle;
		this.CoreMechDetail.text = TextParser.AugmentDetail(coreInfo.CoreMechanicDetail, null, null, false);
		this.ColoreSparknotes.text = coreInfo.Sparknotes;
		this.InkColorText.text = this.SelectedCore.Root.Name;
		VertexGradient colorGradient = this.InkColorText.colorGradient;
		colorGradient.bottomLeft = coreInfo.GradientColor;
		colorGradient.bottomRight = coreInfo.GradientColor;
		this.InkColorText.colorGradient = colorGradient;
		this.InfoTitle.colorGradient = colorGradient;
		this.CoreSmallIcon.sprite = coreInfo.SymbolIcon;
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x0007DBF4 File Offset: 0x0007BDF4
	private void UpdateSelectorInfo()
	{
		foreach (AbilityBoxUI abilityBoxUI in this.InkDisplays)
		{
			abilityBoxUI.UpdateLockedState();
		}
		foreach (AbilityBoxUI abilityBoxUI2 in this.InkDisplays)
		{
			abilityBoxUI2.SelectedDisplay.SetActive(this.SelectedCore == abilityBoxUI2.coreTree);
			abilityBoxUI2.EquippedDisplay.SetActive(abilityBoxUI2.coreTree == PlayerControl.myInstance.actions.core);
		}
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
	private void UpdateButtonInfo()
	{
		this.EquippedInfoDisplay.SetActive(false);
		if (UnlockManager.IsCoreUnlocked(this.SelectedCore))
		{
			bool active = PlayerControl.myInstance.actions.core == this.SelectedCore;
			this.ActionText.text = "Equip " + this.SelectedCore.Root.Name;
			this.EquippedInfoDisplay.SetActive(active);
			this.ActionButton.gameObject.SetActive(false);
			this.RaidUnlockText.SetActive(false);
			return;
		}
		this.ActionText.text = "Unlock " + this.SelectedCore.Root.Name;
		this.ActionButton.interactable = (Currency.LoadoutCoin >= MetaDB.SignatureUnlockCost);
		this.ActionButton.gameObject.SetActive(true);
		if (Library_RaidControl.IsInAntechamber)
		{
			this.ActionButton.gameObject.SetActive(false);
			this.RaidUnlockText.SetActive(true);
			return;
		}
		this.RaidUnlockText.SetActive(false);
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x0007DDD4 File Offset: 0x0007BFD4
	public void ActionButtonClick()
	{
		if (UnlockManager.IsCoreUnlocked(this.SelectedCore))
		{
			this.TryEquip();
			return;
		}
		this.TryUnlock();
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x0007DDF0 File Offset: 0x0007BFF0
	private void TryUnlock()
	{
		int signatureUnlockCost = MetaDB.SignatureUnlockCost;
		if (Currency.LoadoutCoin < signatureUnlockCost)
		{
			return;
		}
		if (this.SelectedCore == null || UnlockManager.IsCoreUnlocked(this.SelectedCore))
		{
			return;
		}
		if (Library_RaidControl.IsInAntechamber)
		{
			return;
		}
		UnlockManager.UnlockCore(this.SelectedCore);
		Currency.SpendLoadoutCoin(signatureUnlockCost, true);
		LibraryInfoWidget.QuillmarksSpent(signatureUnlockCost, this.ActionButton.transform);
		this.TryEquip();
		SignatureInkUIControl.instance.NewCoreUnlocked(this.SelectedCore.Root.magicColor);
		this.UpdateSelectorInfo();
		foreach (AbilityBoxUI abilityBoxUI in this.InkDisplays)
		{
			if (abilityBoxUI.coreTree == this.SelectedCore && abilityBoxUI.UnlockFX != null)
			{
				abilityBoxUI.UnlockFX.Play();
			}
		}
		AudioManager.PlayInterfaceSFX(this.CoreUnlockedSFX, 1f, 0f);
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x0007DEFC File Offset: 0x0007C0FC
	private void TryEquip()
	{
		if (this.SelectedCore == null || !UnlockManager.IsCoreUnlocked(this.SelectedCore) || PlayerControl.myInstance.actions.core == this.SelectedCore)
		{
			return;
		}
		PlayerControl.myInstance.actions.SetCore(this.SelectedCore);
		Settings.SaveLoadout();
		this.UpdateButtonInfo();
		this.UpdateSelectorInfo();
		AudioManager.PlayInterfaceSFX(this.CoreChangedSFX, 1f, 0f);
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x0007DF7C File Offset: 0x0007C17C
	public void GoToInkTalents()
	{
		if (Library_RaidControl.IsInAntechamber)
		{
			return;
		}
		if (Progression.InkLevel < 4)
		{
			InkTalentsPanel.instance.OpenUI(MagicColor.Neutral);
			return;
		}
		InkTalentsPanel.instance.OpenUI(this.SelectedCore.Root.magicColor);
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x0007DFB4 File Offset: 0x0007C1B4
	public void GoToLevels()
	{
		if (Library_RaidControl.IsInAntechamber)
		{
			return;
		}
		PanelManager.instance.PushOrGoTo(PanelType.SignatureInk);
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x0007DFCC File Offset: 0x0007C1CC
	private AbilityBoxUI GetCoreButton(AugmentTree core)
	{
		foreach (AbilityBoxUI abilityBoxUI in this.InkDisplays)
		{
			if (abilityBoxUI.coreTree == core)
			{
				return abilityBoxUI;
			}
		}
		return null;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0007E030 File Offset: 0x0007C230
	public InkCoresPanel.CoreInfo GetCoreInfo(AugmentTree core)
	{
		foreach (InkCoresPanel.CoreInfo coreInfo in this.CoreInfos)
		{
			if (coreInfo.Core == core)
			{
				return coreInfo;
			}
		}
		return null;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0007E094 File Offset: 0x0007C294
	private InkCoresPanel.InfoTypeDisplay GetInfoBase(InkCoresPanel.CoreInfoType iType)
	{
		foreach (InkCoresPanel.InfoTypeDisplay infoTypeDisplay in this.InfoTypes)
		{
			if (infoTypeDisplay.InfoType == iType)
			{
				return infoTypeDisplay;
			}
		}
		return null;
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
	public InkCoresPanel()
	{
	}

	// Token: 0x04001354 RID: 4948
	public static InkCoresPanel instance;

	// Token: 0x04001355 RID: 4949
	[NonSerialized]
	public bool WantShowPanel;

	// Token: 0x04001356 RID: 4950
	public CanvasGroup PanelFader;

	// Token: 0x04001357 RID: 4951
	public List<AbilityBoxUI> InkDisplays;

	// Token: 0x04001358 RID: 4952
	public GameObject SparknotesGroup;

	// Token: 0x04001359 RID: 4953
	public TextMeshProUGUI ColoreSparknotes;

	// Token: 0x0400135A RID: 4954
	public Button ActionButton;

	// Token: 0x0400135B RID: 4955
	public TextMeshProUGUI ActionText;

	// Token: 0x0400135C RID: 4956
	public GameObject EquippedInfoDisplay;

	// Token: 0x0400135D RID: 4957
	public AudioClip CoreUnlockedSFX;

	// Token: 0x0400135E RID: 4958
	public GameObject RaidUnlockText;

	// Token: 0x0400135F RID: 4959
	public TextMeshProUGUI InfoTitle;

	// Token: 0x04001360 RID: 4960
	public TextMeshProUGUI InfoDetail;

	// Token: 0x04001361 RID: 4961
	public Image InfoIcon;

	// Token: 0x04001362 RID: 4962
	public Image CoreSmallIcon;

	// Token: 0x04001363 RID: 4963
	public TextMeshProUGUI InkColorText;

	// Token: 0x04001364 RID: 4964
	public List<InkCoresPanel.CoreInfo> CoreInfos;

	// Token: 0x04001365 RID: 4965
	public TextMeshProUGUI CoreMechTitle;

	// Token: 0x04001366 RID: 4966
	public TextMeshProUGUI CoreMechDetail;

	// Token: 0x04001367 RID: 4967
	public RaidStickers StickerDisplay;

	// Token: 0x04001368 RID: 4968
	[Header("Swapping")]
	public List<GameObject> TopTabs;

	// Token: 0x04001369 RID: 4969
	public TextMeshProUGUI InfoNumberText;

	// Token: 0x0400136A RID: 4970
	public List<GameObject> KBMSwaps;

	// Token: 0x0400136B RID: 4971
	public List<GameObject> ControllerSwaps;

	// Token: 0x0400136C RID: 4972
	public List<InkCoresPanel.InfoTypeDisplay> InfoTypes;

	// Token: 0x0400136D RID: 4973
	public AudioClip CoreChangedSFX;

	// Token: 0x0400136E RID: 4974
	private AugmentTree SelectedCore;

	// Token: 0x0400136F RID: 4975
	private InkCoresPanel.CoreInfoType SelectedTab;

	// Token: 0x020005AC RID: 1452
	[Serializable]
	public class CoreInfo
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x000D21F0 File Offset: 0x000D03F0
		public InkCoresPanel.CoreVideo GetInfo(InkCoresPanel.CoreInfoType infoType)
		{
			foreach (InkCoresPanel.CoreVideo coreVideo in this.Details)
			{
				if (coreVideo.InfoType == infoType)
				{
					return coreVideo;
				}
			}
			return null;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000D224C File Offset: 0x000D044C
		public CoreInfo()
		{
		}

		// Token: 0x04002822 RID: 10274
		public AugmentTree Core;

		// Token: 0x04002823 RID: 10275
		public Sprite BaseIcon;

		// Token: 0x04002824 RID: 10276
		public Sprite LockIcon;

		// Token: 0x04002825 RID: 10277
		public Sprite BorderIcon;

		// Token: 0x04002826 RID: 10278
		public Sprite SymbolIcon;

		// Token: 0x04002827 RID: 10279
		public Color GradientColor;

		// Token: 0x04002828 RID: 10280
		public Color FluidColor;

		// Token: 0x04002829 RID: 10281
		[TextArea(5, 6)]
		public string Sparknotes;

		// Token: 0x0400282A RID: 10282
		public string CoreMechanicTitle;

		// Token: 0x0400282B RID: 10283
		[TextArea(5, 6)]
		public string CoreMechanicDetail;

		// Token: 0x0400282C RID: 10284
		public List<InkCoresPanel.CoreVideo> Details;
	}

	// Token: 0x020005AD RID: 1453
	[Serializable]
	public class CoreVideo
	{
		// Token: 0x060025BF RID: 9663 RVA: 0x000D2254 File Offset: 0x000D0454
		public CoreVideo()
		{
		}

		// Token: 0x0400282D RID: 10285
		public InkCoresPanel.CoreInfoType InfoType;

		// Token: 0x0400282E RID: 10286
		public string Title;

		// Token: 0x0400282F RID: 10287
		[TextArea(4, 5)]
		public string Details;

		// Token: 0x04002830 RID: 10288
		public VideoClip Video;

		// Token: 0x04002831 RID: 10289
		public Sprite Icon;
	}

	// Token: 0x020005AE RID: 1454
	[Serializable]
	public class InfoTypeDisplay
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x000D225C File Offset: 0x000D045C
		public InfoTypeDisplay()
		{
		}

		// Token: 0x04002832 RID: 10290
		public InkCoresPanel.CoreInfoType InfoType;

		// Token: 0x04002833 RID: 10291
		public InputActions.InputAction Binding;

		// Token: 0x04002834 RID: 10292
		[TextArea(4, 5)]
		public string InfoDetail;
	}

	// Token: 0x020005AF RID: 1455
	public enum CoreInfoType
	{
		// Token: 0x04002836 RID: 10294
		Passive,
		// Token: 0x04002837 RID: 10295
		Mana,
		// Token: 0x04002838 RID: 10296
		Ultimate,
		// Token: 0x04002839 RID: 10297
		Ultimate_2
	}
}
