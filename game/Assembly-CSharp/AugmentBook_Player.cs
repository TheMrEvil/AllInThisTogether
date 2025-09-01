using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F6 RID: 502
public class AugmentBook_Player : MonoBehaviour
{
	// Token: 0x06001554 RID: 5460 RVA: 0x00085F7C File Offset: 0x0008417C
	public void Refresh()
	{
		if (this.selectedPlayer == null)
		{
			return;
		}
		this.UpdateNav();
		this.PlayerName.text = this.selectedPlayer.GetUsernameText();
		this.AliveGroup.SetActive(!this.selectedPlayer.IsDead);
		this.DeadGroup.SetActive(this.selectedPlayer.IsDead);
		if (!this.selectedPlayer.IsDead)
		{
			this.HealthText.text = this.selectedPlayer.health.health.ToString() + "/" + this.selectedPlayer.health.MaxHealth.ToString();
			this.BarrierText.text = ((int)this.selectedPlayer.health.shield).ToString() + "/" + this.selectedPlayer.health.MaxShield.ToString();
		}
		int numberValue = this.selectedPlayer.Health.AutoReviveCount - this.selectedPlayer.Health.AutoRevivesUsed;
		this.ReviveCounter.text = numberValue.ToString();
		this.RevivePing.SetNumberValue(numberValue);
		AugmentTree core = this.selectedPlayer.actions.core;
		this.Signature.Setup(core, this.selectedPlayer, 0);
		PlayerDB.CoreDisplay core2 = PlayerDB.GetCore(core);
		if (core2 != null)
		{
			this.Signature.Icon.sprite = core2.MajorIcon;
			this.SignatureBorder.sprite = core2.BorderGlowIcon;
		}
		this.SigAbility.Setup(this.selectedPlayer.actions.utility, this.selectedPlayer, 0);
		this.Primary.Setup(this.selectedPlayer.actions.primary, this.selectedPlayer, 0);
		this.Secondary.Setup(this.selectedPlayer.actions.secondary, this.selectedPlayer, 0);
		this.Movement.Setup(this.selectedPlayer.actions.movement, this.selectedPlayer, 0);
		float d = 1f;
		if (Settings.LowRez)
		{
			d = 1.5f;
		}
		foreach (Transform transform in this.AbilityBindings)
		{
			transform.transform.localScale = Vector3.one * d;
		}
		this.ClearBars();
		this.NoAugments.SetActive(this.selectedPlayer.Augment.trees.Count == 0);
		bool flag = this.selectedPlayer == PlayerControl.myInstance && !TutorialManager.InTutorial;
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.selectedPlayer.Augment.trees)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BarRef, this.BarHolder);
			AugmentBookBarItem item = gameObject.GetComponent<AugmentBookBarItem>();
			item.Setup(keyValuePair.Key, this.selectedPlayer);
			AugmentBookBarItem item2 = item;
			item2.OnSelected = (Action)Delegate.Combine(item2.OnSelected, new Action(delegate()
			{
				this.AugmentBarSelected(item);
			}));
			if (flag && !keyValuePair.Key.NoOmit)
			{
				item.AllowShredding();
			}
			this.AugmentBars.Add(item);
		}
		if (this.AugmentBars.Count > 0)
		{
			Button component = this.AugmentBars[0].GetComponent<Button>();
			Button component2 = this.Primary.GetComponent<Button>();
			UISelector.SetupVerticalListNav<AugmentBookBarItem>(this.AugmentBars, component2, component2, false);
			component2.SetNavigation(component, UIDirection.Down, false);
			component2.SetNavigation(this.Signature.GetComponent<Button>(), UIDirection.Up, false);
			this.SigAbility.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
			this.Secondary.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
			this.Movement.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
		}
		if (AugmentsPanel.instance.IsOnBookSelection && AugmentsPanel.instance.CurTab == AugmentsPanel.BookTab.Players)
		{
			this.SelectDefaultUI();
		}
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000863F4 File Offset: 0x000845F4
	public void SelectDefaultUI()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.AugmentBars.Count > 0)
		{
			UISelector.SelectSelectable(this.AugmentBars[0].GetComponent<Selectable>());
			return;
		}
		UISelector.SelectSelectable(this.Signature.GetComponent<Button>());
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x00086434 File Offset: 0x00084634
	public void TickUpdate()
	{
		if (InputManager.UIAct.Page_Next.WasPressed)
		{
			this.NextPlayer();
		}
		else if (InputManager.UIAct.Page_Prev.WasPressed)
		{
			this.PrevPlayer();
		}
		if (PlayerControl.myInstance == this.selectedPlayer)
		{
			this.UpdateShredding();
		}
		if (InputManager.IsUsingController && AugmentsPanel.instance.IsOnBookSelection)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000864A8 File Offset: 0x000846A8
	private void UpdateShredding()
	{
		if (this.curSelected == null || !this.curSelected.CanShred || this.curSelected.Mod == null || this.curSelected.Mod.NoOmit)
		{
			this.shredT = 0f;
			return;
		}
		if (InputManager.UIAct.UISecondary.IsPressed)
		{
			if (this.didShred)
			{
				return;
			}
			this.shredT += Time.deltaTime;
			this.curSelected.ShredFill.fillAmount = this.shredT / 1f;
			if (this.shredT >= 1f)
			{
				AugmentTree augment = GraphDB.GetAugment(this.curSelected.Mod.tree.ID);
				this.curSelected.OnDeselect(null);
				augment.Root.TryTrigger(PlayerControl.myInstance, EventTrigger.This_Omitted, new EffectProperties(PlayerControl.myInstance), 1f);
				PlayerControl.myInstance.RemoveAugment(augment, 1);
				AudioManager.PlaySFX2D(AudioManager.instance.PageShredded, 1f, 0.1f);
				this.Refresh();
				this.didShred = true;
				return;
			}
		}
		else
		{
			this.didShred = false;
			this.shredT = 0f;
			if (this.curSelected != null)
			{
				this.curSelected.ShredFill.fillAmount = 0f;
			}
		}
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0008660C File Offset: 0x0008480C
	public void AugmentBarSelected(AugmentBookBarItem item)
	{
		this.shredT = 0f;
		this.curSelected = item;
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x00086620 File Offset: 0x00084820
	private void UpdateNav()
	{
		bool flag = PlayerControl.AllPlayers.Count > 1;
		foreach (GameObject gameObject in this.KBMNavButtons)
		{
			gameObject.SetActive(flag && !InputManager.IsUsingController);
		}
		foreach (GameObject gameObject2 in this.ControllerNavButtons)
		{
			gameObject2.SetActive(flag && InputManager.IsUsingController);
		}
		this.PipHolder.SetActive(flag);
		if (flag)
		{
			foreach (GameObject obj in this.pips)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.pips.Clear();
			for (int i = 0; i < PlayerControl.AllPlayers.Count; i++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.PipRef, this.PipRef.transform.parent);
				gameObject3.SetActive(true);
				int x = i;
				gameObject3.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.SetPlayer(this.GetPlayer(x));
				});
				this.pips.Add(gameObject3);
				gameObject3.GetComponent<Image>().sprite = ((this.CurPlayerIndex() == i) ? this.Pip_Filled : this.Pip_Empty);
			}
		}
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000867D4 File Offset: 0x000849D4
	public void NextPlayer()
	{
		if (PlayerControl.AllPlayers.Count <= 1)
		{
			return;
		}
		this.selectedPlayer = this.GetPlayer(this.CurPlayerIndex() + 1);
		this.Refresh();
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x00086800 File Offset: 0x00084A00
	public void PrevPlayer()
	{
		if (PlayerControl.AllPlayers.Count <= 1)
		{
			return;
		}
		int num = this.CurPlayerIndex() - 1;
		if (num < 0)
		{
			num = PlayerControl.AllPlayers.Count - 1;
		}
		this.selectedPlayer = this.GetPlayer(num);
		this.Refresh();
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x00086848 File Offset: 0x00084A48
	public void SetPlayer(PlayerControl player)
	{
		this.selectedPlayer = player;
		this.Refresh();
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x00086857 File Offset: 0x00084A57
	private int CurPlayerIndex()
	{
		if (this.selectedPlayer == null)
		{
			return 0;
		}
		return PlayerControl.AllPlayers.IndexOf(this.selectedPlayer);
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x00086879 File Offset: 0x00084A79
	private PlayerControl GetPlayer(int index)
	{
		index %= PlayerControl.AllPlayers.Count;
		return PlayerControl.AllPlayers[index];
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x00086894 File Offset: 0x00084A94
	private void ClearBars()
	{
		foreach (AugmentBookBarItem augmentBookBarItem in this.AugmentBars)
		{
			UnityEngine.Object.Destroy(augmentBookBarItem.gameObject);
		}
		this.AugmentBars.Clear();
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000868F4 File Offset: 0x00084AF4
	public AugmentBook_Player()
	{
	}

	// Token: 0x040014E0 RID: 5344
	public CanvasGroup Group;

	// Token: 0x040014E1 RID: 5345
	public TextMeshProUGUI PlayerName;

	// Token: 0x040014E2 RID: 5346
	public GameObject AliveGroup;

	// Token: 0x040014E3 RID: 5347
	public TextMeshProUGUI HealthText;

	// Token: 0x040014E4 RID: 5348
	public TextMeshProUGUI BarrierText;

	// Token: 0x040014E5 RID: 5349
	public TextMeshProUGUI ReviveCounter;

	// Token: 0x040014E6 RID: 5350
	public GameObject DeadGroup;

	// Token: 0x040014E7 RID: 5351
	public UIPingable RevivePing;

	// Token: 0x040014E8 RID: 5352
	public PlayerStatAbilityUIGroup Signature;

	// Token: 0x040014E9 RID: 5353
	public Image SignatureBorder;

	// Token: 0x040014EA RID: 5354
	public PlayerStatAbilityUIGroup SigAbility;

	// Token: 0x040014EB RID: 5355
	public PlayerStatAbilityUIGroup Primary;

	// Token: 0x040014EC RID: 5356
	public PlayerStatAbilityUIGroup Secondary;

	// Token: 0x040014ED RID: 5357
	public PlayerStatAbilityUIGroup Movement;

	// Token: 0x040014EE RID: 5358
	public List<Transform> AbilityBindings;

	// Token: 0x040014EF RID: 5359
	public GameObject BarRef;

	// Token: 0x040014F0 RID: 5360
	public GameObject NoAugments;

	// Token: 0x040014F1 RID: 5361
	public Transform BarHolder;

	// Token: 0x040014F2 RID: 5362
	public AutoScrollRect Scroller;

	// Token: 0x040014F3 RID: 5363
	private List<AugmentBookBarItem> AugmentBars = new List<AugmentBookBarItem>();

	// Token: 0x040014F4 RID: 5364
	public List<GameObject> ControllerNavButtons;

	// Token: 0x040014F5 RID: 5365
	public List<GameObject> KBMNavButtons;

	// Token: 0x040014F6 RID: 5366
	public GameObject PipHolder;

	// Token: 0x040014F7 RID: 5367
	public GameObject PipRef;

	// Token: 0x040014F8 RID: 5368
	public Sprite Pip_Filled;

	// Token: 0x040014F9 RID: 5369
	public Sprite Pip_Empty;

	// Token: 0x040014FA RID: 5370
	private List<GameObject> pips = new List<GameObject>();

	// Token: 0x040014FB RID: 5371
	private PlayerControl selectedPlayer;

	// Token: 0x040014FC RID: 5372
	private float shredT;

	// Token: 0x040014FD RID: 5373
	private AugmentBookBarItem curSelected;

	// Token: 0x040014FE RID: 5374
	private bool didShred;

	// Token: 0x020005C7 RID: 1479
	[CompilerGenerated]
	private sealed class <>c__DisplayClass28_0
	{
		// Token: 0x0600261D RID: 9757 RVA: 0x000D316B File Offset: 0x000D136B
		public <>c__DisplayClass28_0()
		{
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000D3173 File Offset: 0x000D1373
		internal void <Refresh>b__0()
		{
			this.<>4__this.AugmentBarSelected(this.item);
		}

		// Token: 0x040028A4 RID: 10404
		public AugmentBookBarItem item;

		// Token: 0x040028A5 RID: 10405
		public AugmentBook_Player <>4__this;
	}

	// Token: 0x020005C8 RID: 1480
	[CompilerGenerated]
	private sealed class <>c__DisplayClass36_0
	{
		// Token: 0x0600261F RID: 9759 RVA: 0x000D3186 File Offset: 0x000D1386
		public <>c__DisplayClass36_0()
		{
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000D318E File Offset: 0x000D138E
		internal void <UpdateNav>b__0()
		{
			this.<>4__this.SetPlayer(this.<>4__this.GetPlayer(this.x));
		}

		// Token: 0x040028A6 RID: 10406
		public int x;

		// Token: 0x040028A7 RID: 10407
		public AugmentBook_Player <>4__this;
	}
}
