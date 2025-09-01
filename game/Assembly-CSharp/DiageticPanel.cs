using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class DiageticPanel : MonoBehaviour
{
	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00068046 File Offset: 0x00066246
	private DiageticOption selected
	{
		get
		{
			return DiageticSelector.CurrentSelected;
		}
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x00068050 File Offset: 0x00066250
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.rect = base.GetComponent<RectTransform>();
		this.allOptions.Add(this.Portal);
		this.allOptions.Add(this.Augment);
		this.allOptions.Add(this.Reward);
		this.allOptions.Add(this.Vignette);
		this.allOptions.Add(this.Status);
		foreach (Component component in this.allOptions)
		{
			component.gameObject.SetActive(false);
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00068124 File Offset: 0x00066324
	private void LateUpdate()
	{
		this.TryUpdateSelection();
		this.UpdateInteractDisplay();
		this.canvasGroup.UpdateOpacity(this.selected != null, 8f, true);
		if (this.selected == null)
		{
			return;
		}
		this.rect.FollowWorldObject(this.selected.UIRoot, this.canvas, 0, 0f);
		this.UpdateVotes(this.selected.NeedsVote, this.selected.GetVoteInfo());
		this.UpdateHold();
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000681B0 File Offset: 0x000663B0
	private void UpdateSelectionDisplay()
	{
		PortalTrigger portalTrigger = this.selected as PortalTrigger;
		if (portalTrigger != null)
		{
			this.SetComponentActive(this.Portal);
			this.Portal.Setup(portalTrigger.portalType);
		}
		if (this.selected is BossRewardTrigger)
		{
			this.SetComponentActive(this.Reward);
			return;
		}
		VignetteTrigger vignetteTrigger = this.selected as VignetteTrigger;
		if (vignetteTrigger != null)
		{
			this.SetComponentActive(this.Vignette);
			this.Vignette.Setup(vignetteTrigger);
			return;
		}
		AIDiageticInteraction aidiageticInteraction = this.selected as AIDiageticInteraction;
		if (aidiageticInteraction != null)
		{
			this.SetComponentActive(this.Vignette);
			this.Vignette.Setup(aidiageticInteraction);
			return;
		}
		SimpleDiagetic simpleDiagetic = this.selected as SimpleDiagetic;
		if (simpleDiagetic != null)
		{
			this.SetComponentActive(this.Vignette);
			this.Vignette.Setup(simpleDiagetic);
			return;
		}
		BookMeshInteraction bookMeshInteraction = this.selected as BookMeshInteraction;
		if (bookMeshInteraction != null)
		{
			this.SetComponentActive(this.Vignette);
			this.Vignette.Setup(bookMeshInteraction);
			return;
		}
		RaidScrollTrigger raidScrollTrigger = this.selected as RaidScrollTrigger;
		if (raidScrollTrigger != null)
		{
			this.SetComponentActive(this.Vignette);
			this.Vignette.Setup(raidScrollTrigger);
			return;
		}
		ScrollPickup scrollPickup = this.selected as ScrollPickup;
		if (scrollPickup != null)
		{
			this.SetComponentActive(this.Augment);
			this.Augment.Setup(scrollPickup.Augment);
			return;
		}
		TutorialCorePickup tutorialCorePickup = this.selected as TutorialCorePickup;
		if (tutorialCorePickup != null)
		{
			this.SetComponentActive(this.Augment);
			this.Augment.Setup(tutorialCorePickup.CoreInfo);
			return;
		}
		StatusPickup statusPickup = this.selected as StatusPickup;
		if (statusPickup != null)
		{
			this.SetComponentActive(this.Status);
			this.Status.Setup(statusPickup.Status, statusPickup.Stacks);
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00068368 File Offset: 0x00066568
	private void UpdateInteractDisplay()
	{
		bool flag = true;
		ScrollPickup scrollPickup = this.selected as ScrollPickup;
		if (scrollPickup != null)
		{
			flag = scrollPickup.CanCollect;
		}
		else
		{
			StatusPickup statusPickup = this.selected as StatusPickup;
			if (statusPickup != null)
			{
				flag = statusPickup.CanCollect;
			}
		}
		if (this.InteractDisplay.activeSelf != flag)
		{
			this.InteractDisplay.SetActive(flag);
		}
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000683C0 File Offset: 0x000665C0
	private void SetComponentActive(Component c)
	{
		foreach (Component component in this.allOptions)
		{
			component.gameObject.SetActive(component == c);
		}
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00068420 File Offset: 0x00066620
	private void TryUpdateSelection()
	{
		if (this.lastSelected == this.selected || this.selected == null)
		{
			return;
		}
		this.lastSelected = this.selected;
		this.UpdateSelectionDisplay();
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x00068458 File Offset: 0x00066658
	private void UpdateVotes(bool needsVote, List<int> playerIDs)
	{
		if (!needsVote)
		{
			this.VoteGroup.alpha = 0f;
			return;
		}
		this.VoteGroup.alpha = 1f;
		if (playerIDs.Contains(PlayerControl.MyViewID))
		{
			this.MyVotePip.Setup(true);
		}
		else
		{
			this.MyVotePip.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.VotePips.Count; i++)
		{
			if (i >= playerIDs.Count)
			{
				this.VotePips[i].gameObject.SetActive(false);
			}
			else
			{
				this.VotePips[i].gameObject.SetActive(true);
				this.VotePips[i].Setup(playerIDs[i]);
			}
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0006851C File Offset: 0x0006671C
	private void UpdateHold()
	{
		this.HoldGroup.UpdateOpacity(this.selected.NeedsHold, 4f, true);
		float holdTimer = DiageticSelector.HoldTimer;
		this.InteractFill.fillAmount = holdTimer;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00068557 File Offset: 0x00066757
	public DiageticPanel()
	{
	}

	// Token: 0x04000EF9 RID: 3833
	private CanvasGroup canvasGroup;

	// Token: 0x04000EFA RID: 3834
	private RectTransform rect;

	// Token: 0x04000EFB RID: 3835
	[Header("Core UI")]
	public RectTransform canvas;

	// Token: 0x04000EFC RID: 3836
	public CanvasGroup HoldGroup;

	// Token: 0x04000EFD RID: 3837
	public GameObject InteractDisplay;

	// Token: 0x04000EFE RID: 3838
	public Image InteractFill;

	// Token: 0x04000EFF RID: 3839
	private DiageticOption lastSelected;

	// Token: 0x04000F00 RID: 3840
	[Header("Voting")]
	public CanvasGroup VoteGroup;

	// Token: 0x04000F01 RID: 3841
	public VoteIcon MyVotePip;

	// Token: 0x04000F02 RID: 3842
	public List<VoteIcon> VotePips;

	// Token: 0x04000F03 RID: 3843
	[Header("Diagetic Options")]
	public Diagetic_PortalUI Portal;

	// Token: 0x04000F04 RID: 3844
	public Diagetic_AugmentUI Augment;

	// Token: 0x04000F05 RID: 3845
	public Diagetic_PortalUI Reward;

	// Token: 0x04000F06 RID: 3846
	public Diagetic_VignetteUI Vignette;

	// Token: 0x04000F07 RID: 3847
	public Diagetic_StatusUI Status;

	// Token: 0x04000F08 RID: 3848
	private List<Component> allOptions = new List<Component>();
}
