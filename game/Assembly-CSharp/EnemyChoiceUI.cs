using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001DE RID: 478
public class EnemyChoiceUI : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0007C050 File Offset: 0x0007A250
	public bool NeedsAnyBossWarning
	{
		get
		{
			return this.needsBossWarn && !RaidManager.IsInRaid;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0007C064 File Offset: 0x0007A264
	private bool IsBinding
	{
		get
		{
			return this.Augment.Root.modType == ModType.Binding;
		}
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x0007C079 File Offset: 0x0007A279
	private void Awake()
	{
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x0007C07B File Offset: 0x0007A27B
	private void Update()
	{
		this.UpdateVotePips();
		this.BWOverlay.UpdateOpacity(!this.isHovered, 4f, false);
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
	public void Setup(AugmentTree tree, int voteIndex, AugmentQuality dangerDisplay)
	{
		this.ChoiceID = voteIndex;
		this.CheckIsHidden();
		this.Augment = tree;
		this.Button.interactable = false;
		this.CoreIcon.sprite = tree.Root.Icon;
		this.CoreIcon_BW.sprite = this.CoreIcon.sprite;
		this.TitleText.text = tree.Root.Name;
		this.TitleText.color = GameDB.Quality(dangerDisplay).EnemyColor;
		this.LabelText.text = TextParser.AugmentDetail(tree.Root.Detail, tree, PlayerControl.myInstance, false);
		this.HideAllVotes();
		this.needsBossWarn = false;
		if (this.isHidden)
		{
			this.CoreIcon.sprite = this.HiddenIcon;
			this.CoreIcon_BW.sprite = this.HiddenIcon;
			this.TitleText.text = "<size=48>???</size>";
			this.LabelText.text = " ";
			UIPingable component = base.GetComponent<UIPingable>();
			if (component != null)
			{
				component.SetupAsHiddenEnemyPage();
			}
		}
		else
		{
			this.SetupBossInfo();
			UIPingable component2 = base.GetComponent<UIPingable>();
			if (component2 != null)
			{
				component2.PingType = UIPing.UIPingType.Augment_Enemy;
				component2.Setup(tree);
			}
		}
		this.BindingDisplay.SetActive(this.IsBinding);
		if (this.IsBinding)
		{
			this.BindingLevelText.text = "+" + this.Augment.Root.HeatLevel.ToString();
		}
		this.anim.Play("EnemyBox_Open");
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x0007C22C File Offset: 0x0007A42C
	private void SetupBossInfo()
	{
		if (TutorialManager.InTutorial || this.IsBinding)
		{
			return;
		}
		if (!this.Augment.Root.ApplyTo.AnyFlagsMatch(EnemyLevel.Boss))
		{
			return;
		}
		if (this.Augment.Root.ApplyType <= EnemyType.Any)
		{
			this.BossIcon.sprite = this.GenericBossSprite;
			this.needsBossWarn = true;
			return;
		}
		GameObject nextBoss = Logic_World.GetNextBoss();
		if (nextBoss != null)
		{
			EnemyType enemyType = nextBoss.GetComponent<AIControl>().EnemyType;
			if (this.Augment.Root.ApplyType.AnyFlagsMatch(enemyType) || GameplayManager.HasGameOverride("Boss_WildType"))
			{
				AIManager instance = AIManager.instance;
				AIData.TornFamilyInfo tornFamilyInfo = (instance != null) ? instance.DB.GetFamilyData(enemyType) : null;
				if (tornFamilyInfo != null)
				{
					this.BossIcon.sprite = tornFamilyInfo.BossSprite;
					this.needsBossWarn = true;
				}
			}
		}
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x0007C303 File Offset: 0x0007A503
	private void CheckIsHidden()
	{
		if (this.ChoiceID == 0)
		{
			this.isHidden = GameplayManager.HasGameOverride("Enemy_Hide_First");
		}
		if (this.ChoiceID == 1)
		{
			this.isHidden = GameplayManager.HasGameOverride("Enemy_Hide_Second");
		}
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x0007C338 File Offset: 0x0007A538
	public void RevealIfHidden()
	{
		if (!this.isHidden)
		{
			return;
		}
		this.isHidden = false;
		this.CoreIcon.sprite = this.Augment.Root.Icon;
		this.CoreIcon_BW.sprite = this.CoreIcon.sprite;
		this.TitleText.text = this.Augment.Root.Name;
		this.LabelText.text = TextParser.AugmentDetail(this.Augment.Root.Detail, this.Augment, PlayerControl.myInstance, false);
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x0007C3CD File Offset: 0x0007A5CD
	public void Choose()
	{
		EnemySelectionPanel.instance.VoteEnemyScroll(this.ChoiceID);
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x0007C3DF File Offset: 0x0007A5DF
	public void OnSelect(BaseEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			this.OnHoverEnter();
		}
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x0007C3EE File Offset: 0x0007A5EE
	public void OnDeselect(BaseEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			this.OnHoverExit();
		}
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x0007C400 File Offset: 0x0007A600
	public void OnHoverEnter()
	{
		this.isHovered = true;
		if (this.isHidden)
		{
			return;
		}
		this.SetupKeywords();
		PlayerControl.myInstance.Display.ShowRange(this.Augment.Root.DisplayRadius);
		EnemySelectionPanel.instance.EnemySelectionHovered(this);
		this.BossWarnDisplay.SetActive(this.needsBossWarn);
		if (this.NeedsAnyBossWarning)
		{
			this.BossWarnFX.Play();
		}
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0007C471 File Offset: 0x0007A671
	public void OnHoverExit()
	{
		this.isHovered = false;
		this.ClearKeywords();
		PlayerControl.myInstance.Display.ReleaseRange();
		EnemySelectionPanel.instance.EnemySelectionUnhovered(this);
		this.BossWarnDisplay.SetActive(false);
		this.BossWarnFX.Stop();
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
	private void SetupKeywords()
	{
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.Augment.Root.Detail, null))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordList, ref this.keywords, null);
		}
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x0007C524 File Offset: 0x0007A724
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

	// Token: 0x060013FF RID: 5119 RVA: 0x0007C590 File Offset: 0x0007A790
	private void HideAllVotes()
	{
		foreach (EnemyChoiceUI.VoteGroup voteGroup in this.VoteGroups)
		{
			voteGroup.holder.SetActive(false);
		}
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x0007C5E8 File Offset: 0x0007A7E8
	private void UpdateVotePips()
	{
		if (this.Augment == null || this.ChoiceID < 0 || !PhotonNetwork.InRoom || PlayerControl.myInstance == null || PlayerControl.PlayerCount < 2)
		{
			return;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			if (keyValuePair.Value == this.ChoiceID && keyValuePair.Key != PlayerControl.myInstance.view.OwnerActorNr)
			{
				list.Add(keyValuePair.Key);
			}
		}
		bool flag = VoteManager.MyCurrentVote == this.ChoiceID;
		this.MyVoteGroup.UpdateOpacity(flag, 2f, true);
		this.MyVote.gameObject.SetActive(flag);
		if (flag)
		{
			this.MyVote.Setup(PlayerControl.myInstance);
		}
		int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
		foreach (EnemyChoiceUI.VoteGroup voteGroup in this.VoteGroups)
		{
			if (playerCount != voteGroup.playerCount)
			{
				if (voteGroup.holder.activeSelf)
				{
					voteGroup.holder.SetActive(false);
				}
			}
			else
			{
				if (!voteGroup.holder.activeSelf)
				{
					voteGroup.holder.SetActive(true);
				}
				for (int i = 0; i < voteGroup.VotePips.Count; i++)
				{
					if (i >= list.Count)
					{
						voteGroup.VotePips[i].gameObject.SetActive(false);
					}
					else
					{
						voteGroup.VotePips[i].gameObject.SetActive(true);
						voteGroup.VotePips[i].Setup(list[i]);
					}
				}
			}
		}
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x0007C7F0 File Offset: 0x0007A9F0
	public EnemyChoiceUI()
	{
	}

	// Token: 0x0400130C RID: 4876
	public Animator anim;

	// Token: 0x0400130D RID: 4877
	public Image CoreIcon;

	// Token: 0x0400130E RID: 4878
	public CanvasGroup BWOverlay;

	// Token: 0x0400130F RID: 4879
	public Image CoreIcon_BW;

	// Token: 0x04001310 RID: 4880
	public TextMeshProUGUI TitleText;

	// Token: 0x04001311 RID: 4881
	public TextMeshProUGUI LabelText;

	// Token: 0x04001312 RID: 4882
	public RectTransform KeywordList;

	// Token: 0x04001313 RID: 4883
	public Button Button;

	// Token: 0x04001314 RID: 4884
	public Sprite HiddenIcon;

	// Token: 0x04001315 RID: 4885
	public GameObject BossWarnDisplay;

	// Token: 0x04001316 RID: 4886
	public Sprite GenericBossSprite;

	// Token: 0x04001317 RID: 4887
	public Image BossIcon;

	// Token: 0x04001318 RID: 4888
	public ParticleSystem BossWarnFX;

	// Token: 0x04001319 RID: 4889
	private bool needsBossWarn;

	// Token: 0x0400131A RID: 4890
	public GameObject BindingDisplay;

	// Token: 0x0400131B RID: 4891
	public TextMeshProUGUI BindingLevelText;

	// Token: 0x0400131C RID: 4892
	[Header("Voting")]
	public int ChoiceID;

	// Token: 0x0400131D RID: 4893
	public UIPlayerVoteDisplay MyVote;

	// Token: 0x0400131E RID: 4894
	public CanvasGroup MyVoteGroup;

	// Token: 0x0400131F RID: 4895
	public List<EnemyChoiceUI.VoteGroup> VoteGroups;

	// Token: 0x04001320 RID: 4896
	private bool isHovered;

	// Token: 0x04001321 RID: 4897
	[NonSerialized]
	public bool isHidden;

	// Token: 0x04001322 RID: 4898
	[NonSerialized]
	public AugmentTree Augment;

	// Token: 0x04001323 RID: 4899
	private List<KeywordBoxUI> keywords = new List<KeywordBoxUI>();

	// Token: 0x020005A8 RID: 1448
	[Serializable]
	public class VoteGroup
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x000D2031 File Offset: 0x000D0231
		public VoteGroup()
		{
		}

		// Token: 0x04002816 RID: 10262
		public GameObject holder;

		// Token: 0x04002817 RID: 10263
		public int playerCount;

		// Token: 0x04002818 RID: 10264
		public List<UIPlayerVoteDisplay> VotePips;
	}
}
