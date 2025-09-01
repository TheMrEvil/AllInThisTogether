using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AC RID: 428
public class FountainStoreUI : MonoBehaviour
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060011BC RID: 4540 RVA: 0x0006E0E8 File Offset: 0x0006C2E8
	public static bool wantVisible
	{
		get
		{
			return GameplayManager.IsInGame && GameplayManager.CurState != GameState.InWave && GameplayManager.CurState == GameState.Reward_Fountain && PanelManager.CurPanel == PanelType.Augments && !PlayerChoicePanel.InApplySequence && !PlayerChoicePanel.IsSelecting && InkManager.Store != null && InkManager.Store.Count != 0 && !VoteManager.IsVoting && VoteManager.CurrentState != VoteState.VotePrepared && AugmentsPanel.UpgradesAvailable <= 0 && !PlayerChoicePanel.instance.HasChoices && (WaveManager.instance.AppendixLevel <= 0 || InkManager.TotalTeamPoints > 0);
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x0006E17F File Offset: 0x0006C37F
	private void Awake()
	{
		FountainStoreUI.instance = this;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0006E193 File Offset: 0x0006C393
	public void Setup()
	{
		this.Clear();
		this.UpdatePointDisplay();
		this.PopulateStoreLayers();
		InkManager.NewRowAvailable = false;
		this.hasSetup = true;
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
	public void Invalidate()
	{
		this.hasSetup = false;
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
	public void Refresh()
	{
		if (!this.hasSetup)
		{
			this.Setup();
			return;
		}
		this.UpdatePointDisplay();
		foreach (FountainStoreLayerUI fountainStoreLayerUI in this.layers)
		{
			fountainStoreLayerUI.UpdateDisplay(false);
			fountainStoreLayerUI.UpdateAllItems();
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0006E22C File Offset: 0x0006C42C
	private void Update()
	{
		bool wantVisible = FountainStoreUI.wantVisible;
		this.canvasGroup.UpdateOpacity(wantVisible, 2f, false);
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		this.ForceFX.SetActive(wantVisible);
		if (wantVisible && !this.didWantVisible)
		{
			this.TrySelectController();
		}
		this.didWantVisible = wantVisible;
		if (!FountainStoreUI.wantVisible)
		{
			return;
		}
		this.UpdatePointDisplay();
		if (this.needLayerUpdate)
		{
			foreach (FountainStoreLayerUI fountainStoreLayerUI in this.layers)
			{
				fountainStoreLayerUI.UpdateDisplay(false);
			}
			this.ShowDetail(this.selectedItem);
			this.needLayerUpdate = false;
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x0006E2EC File Offset: 0x0006C4EC
	private void TrySelectController()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (AugmentsPanel.instance.IsOnBookSelection)
		{
			AugmentsPanel.instance.TryToggleFocus();
			return;
		}
		this.SelectDefault();
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0006E313 File Offset: 0x0006C513
	public void SelectDefault()
	{
		UISelector.SelectSelectable(this.layers[0].bars[0].ButtonRef);
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0006E338 File Offset: 0x0006C538
	private void Clear()
	{
		foreach (FountainStoreLayerUI fountainStoreLayerUI in this.layers)
		{
			UnityEngine.Object.Destroy(fountainStoreLayerUI.gameObject);
		}
		this.layers.Clear();
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0006E398 File Offset: 0x0006C598
	public void UpdatePointDisplay()
	{
		this.LocalPlayerPts.text = InkManager.MyShards.ToString();
		this.LocalPtsPing.SetNumberValue(InkManager.MyShards);
		if (this.cachedPts != InkManager.MyShards)
		{
			this.cachedPts = InkManager.MyShards;
			this.needLayerUpdate = true;
		}
		this.OwedPageGroup.UpdateOpacity(InkManager.FontPagesOwed > 0, 2f, true);
		this.OwedPageCount.text = "x " + InkManager.FontPagesOwed.ToString();
		this.ptTimer -= Time.deltaTime;
		if (this.ptTimer > 0f)
		{
			return;
		}
		this.ptTimer = 0.5f;
		this.ClearPlayerPoints();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!(playerControl == PlayerControl.myInstance))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.OtherPlrPointRef, this.OtherPlrPointRef.transform.parent);
				gameObject.gameObject.SetActive(true);
				gameObject.GetComponent<FountainUIAllyPoints>().Setup(playerControl);
				this.otherPlayerObjs.Add(gameObject);
			}
		}
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0006E4E0 File Offset: 0x0006C6E0
	private void ClearPlayerPoints()
	{
		foreach (GameObject obj in this.otherPlayerObjs)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.otherPlayerObjs.Clear();
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x0006E53C File Offset: 0x0006C73C
	public void InkInvested(Transform atPoint)
	{
		if (!FountainStoreUI.wantVisible)
		{
			return;
		}
		if (Time.realtimeSinceStartup - this.lastInvestTime < 0.25f)
		{
			return;
		}
		this.lastInvestTime = Time.realtimeSinceStartup;
		AudioManager.PlayInterfaceSFX(this.InvestClip, 1f, 0f);
		this.InvestFX.transform.position = atPoint.position;
		this.InvestFX.Play();
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
	public void UpdateInkInfo(InkTalent option)
	{
		if (!FountainStoreUI.wantVisible)
		{
			return;
		}
		foreach (FountainStoreLayerUI fountainStoreLayerUI in this.layers)
		{
			fountainStoreLayerUI.UpdateItem(option);
		}
		this.needLayerUpdate = true;
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0006E608 File Offset: 0x0006C808
	private void PopulateStoreLayers()
	{
		for (int i = 0; i < InkManager.Store.Count; i++)
		{
			this.CreateNewLayer(InkManager.Store[i]).CreateItems();
		}
		this.UpdateLayerNavigation();
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
		List<string> list = new List<string>();
		foreach (InkRow inkRow in InkManager.Store)
		{
			foreach (InkTalent inkTalent in inkRow.Options)
			{
				list.Add(inkTalent.Augment.ID);
			}
		}
		Progression.SawAugment(list);
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0006E6E8 File Offset: 0x0006C8E8
	private FountainStoreLayerUI CreateNewLayer(InkRow row)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LayerRef, this.LayerHolder);
		gameObject.SetActive(true);
		FountainStoreLayerUI component = gameObject.GetComponent<FountainStoreLayerUI>();
		component.Setup(row);
		this.layers.Add(component);
		return component;
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0006E728 File Offset: 0x0006C928
	private void UpdateLayerNavigation()
	{
		for (int i = 1; i < this.layers.Count; i++)
		{
			FountainStoreLayerUI fountainStoreLayerUI = this.layers[i - 1];
			FountainStoreLayerUI fountainStoreLayerUI2 = this.layers[i];
			for (int j = 0; j < fountainStoreLayerUI.bars.Count; j++)
			{
				Selectable buttonRef = fountainStoreLayerUI.bars[j].ButtonRef;
				Button buttonRef2 = fountainStoreLayerUI2.bars[Mathf.Clamp(j, 0, fountainStoreLayerUI2.bars.Count - 1)].ButtonRef;
				buttonRef.SetNavigation(buttonRef2, UIDirection.Down, false);
			}
			for (int k = 0; k < fountainStoreLayerUI2.bars.Count; k++)
			{
				Selectable buttonRef3 = fountainStoreLayerUI2.bars[k].ButtonRef;
				Button buttonRef4 = fountainStoreLayerUI.bars[Mathf.Clamp(k, 0, fountainStoreLayerUI.bars.Count - 1)].ButtonRef;
				buttonRef3.SetNavigation(buttonRef4, UIDirection.Up, false);
			}
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x0006E820 File Offset: 0x0006CA20
	public void ShowDetail(FountainStoreItemUI item)
	{
		if (item == null)
		{
			return;
		}
		this.selectedItem = item;
		this.ItemTitle.text = item.Option.Augment.Root.Name;
		this.ItemTitle.color = GameDB.Quality(item.Option.Augment.Root.DisplayQuality).PlayerColor;
		this.ItemDetail.text = TextParser.AugmentDetail(item.Option.Augment.Root.Detail, item.Option.Augment, null, false);
		this.ItemProgressText.text = item.Option.CurrentValue.ToString() + "/" + item.Option.Cost.ToString();
		this.ClearKeywords();
		this.SetupKeywords(item.Option.Augment);
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x0006E90C File Offset: 0x0006CB0C
	private void SetupKeywords(AugmentTree augment)
	{
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(augment.Root.Detail, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywords, PlayerControl.myInstance);
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0006E97C File Offset: 0x0006CB7C
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywords)
		{
			if (keywordBoxUI != null)
			{
				UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
			}
		}
		this.keywords.Clear();
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0006E9E8 File Offset: 0x0006CBE8
	public FountainStoreUI()
	{
	}

	// Token: 0x0400106D RID: 4205
	private CanvasGroup canvasGroup;

	// Token: 0x0400106E RID: 4206
	public GameObject LayerRef;

	// Token: 0x0400106F RID: 4207
	public Transform LayerHolder;

	// Token: 0x04001070 RID: 4208
	private List<FountainStoreLayerUI> layers = new List<FountainStoreLayerUI>();

	// Token: 0x04001071 RID: 4209
	public TextMeshProUGUI ItemTitle;

	// Token: 0x04001072 RID: 4210
	public TextMeshProUGUI ItemDetail;

	// Token: 0x04001073 RID: 4211
	public TextMeshProUGUI ItemProgressText;

	// Token: 0x04001074 RID: 4212
	public RectTransform KeywordHolder;

	// Token: 0x04001075 RID: 4213
	private List<KeywordBoxUI> keywords = new List<KeywordBoxUI>();

	// Token: 0x04001076 RID: 4214
	private FountainStoreItemUI selectedItem;

	// Token: 0x04001077 RID: 4215
	public TextMeshProUGUI LocalPlayerPts;

	// Token: 0x04001078 RID: 4216
	public UIPingable LocalPtsPing;

	// Token: 0x04001079 RID: 4217
	public GameObject OtherPlrPointRef;

	// Token: 0x0400107A RID: 4218
	private List<GameObject> otherPlayerObjs = new List<GameObject>();

	// Token: 0x0400107B RID: 4219
	private int cachedPts;

	// Token: 0x0400107C RID: 4220
	private float ptTimer;

	// Token: 0x0400107D RID: 4221
	public CanvasGroup OwedPageGroup;

	// Token: 0x0400107E RID: 4222
	public TextMeshProUGUI OwedPageCount;

	// Token: 0x0400107F RID: 4223
	public GameObject ForceFX;

	// Token: 0x04001080 RID: 4224
	public AudioClip InvestClip;

	// Token: 0x04001081 RID: 4225
	public ParticleSystem InvestFX;

	// Token: 0x04001082 RID: 4226
	public static FountainStoreUI instance;

	// Token: 0x04001083 RID: 4227
	private bool needLayerUpdate;

	// Token: 0x04001084 RID: 4228
	private bool hasSetup;

	// Token: 0x04001085 RID: 4229
	private bool didWantVisible;

	// Token: 0x04001086 RID: 4230
	private float lastInvestTime;
}
