using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using QFSW.QC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B0 RID: 432
public class GameHUD : MonoBehaviour
{
	// Token: 0x060011D9 RID: 4569 RVA: 0x0006EC04 File Offset: 0x0006CE04
	private void Awake()
	{
		GameHUD.instance = this;
		PlatformSetup.OnLoggedIn = (Action)Delegate.Combine(PlatformSetup.OnLoggedIn, new Action(this.OnLoggedIn));
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.SceneChanged));
		this.upgradeRect = this.UpgradeGroup.GetComponent<RectTransform>();
		PlayerControl.LocalPlayerSpawned = (Action)Delegate.Combine(PlayerControl.LocalPlayerSpawned, new Action(this.PlayerSpawned));
		UIPanel gameplayInvisible = this.GameplayInvisible;
		gameplayInvisible.OnEnteredPanel = (Action)Delegate.Combine(gameplayInvisible.OnEnteredPanel, new Action(this.HUDEntered));
		UIPanel gameplayInvisible2 = this.GameplayInvisible;
		gameplayInvisible2.OnLeftPanel = (Action)Delegate.Combine(gameplayInvisible2.OnLeftPanel, new Action(this.HUDExit));
		this.raycaster = base.GetComponent<GraphicRaycaster>();
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0006ECE2 File Offset: 0x0006CEE2
	private void HUDEntered()
	{
		UIFluidManager.ResetContainers();
		UISelector.ResetSelected();
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x0006ECEE File Offset: 0x0006CEEE
	private void HUDExit()
	{
		UIFluidManager.ResetContainers();
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x0006ECF5 File Offset: 0x0006CEF5
	private void SceneChanged()
	{
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x0006ECF7 File Offset: 0x0006CEF7
	private void OnLoggedIn()
	{
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0006ECFC File Offset: 0x0006CEFC
	private void PlayerSpawned()
	{
		this.PlayerCoreChanged(PlayerControl.myInstance.actions.core.Root.magicColor);
		PlayerActions actions = PlayerControl.myInstance.actions;
		actions.coreChanged = (Action<MagicColor>)Delegate.Combine(actions.coreChanged, new Action<MagicColor>(this.PlayerCoreChanged));
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x0006ED53 File Offset: 0x0006CF53
	private void PlayerCoreChanged(MagicColor e)
	{
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x0006ED58 File Offset: 0x0006CF58
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		bool isDead = PlayerControl.myInstance.IsDead;
		if (Input.GetKeyDown(KeyCode.Alpha0) && !QuantumConsole.Instance.IsActive)
		{
			PlayerControl myInstance = PlayerControl.myInstance;
			if (myInstance != null && myInstance.IsDeveloper && PanelManager.CurPanel == PanelType.GameInvisible)
			{
				this.CycleHudMode();
			}
		}
		Behaviour behaviour = this.raycaster;
		UIPanel curSelect = PanelManager.curSelect;
		behaviour.enabled = (curSelect != null && curSelect.gameplayInteractable);
		this.DeathInfo.TickUpdate(isDead);
		this.UpdateTeammates();
		this.UpdateUpgradeInfo();
		this.UpdateSigReminder();
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0006EDF0 File Offset: 0x0006CFF0
	private void UpdateUpgradeInfo()
	{
		string text = "";
		bool flag = false;
		float fillAmount = 0f;
		bool flag2 = PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1;
		bool flag3 = PanelManager.CurPanel != PanelType.Augments;
		if (TutorialManager.InTutorial)
		{
			if (PlayerChoicePanel.instance.HasChoices)
			{
				text = "Page available";
				flag = true;
			}
		}
		else if (VoteManager.IsVoting && PhotonNetwork.InRoom && flag2)
		{
			text = "Vote In Progress";
			flag = true;
			if (VoteManager.IsTimed)
			{
				fillAmount = GameplayManager.instance.Timer / 30f;
			}
		}
		else if (AugmentsPanel.UpgradesAvailable > 0)
		{
			int upgradesAvailable = AugmentsPanel.UpgradesAvailable;
			text = upgradesAvailable.ToString() + ((upgradesAvailable == 1) ? " Page" : " Pages") + " available";
			flag = (upgradesAvailable > 0);
		}
		else if (PlayerChoicePanel.instance.HasChoices)
		{
			text = "Page available";
			flag = true;
		}
		else if (GameplayManager.CurState == GameState.Reward_Fountain && flag3)
		{
			if (flag2)
			{
				text = (RewardManager.instance.ReadyPlayers.Contains(PlayerControl.MyViewID) ? "Waiting for Others" : "Ready to Continue?");
			}
			else
			{
				text = "Ready to Continue?";
			}
			flag = true;
		}
		else if (GameplayManager.CurState == GameState.Reward_FontPages && flag3 && flag2)
		{
			text = "Waiting for Others";
			flag = true;
		}
		else if (GameplayManager.CurState == GameState.Vignette_PreWait && flag3 && flag2)
		{
			text = "Waiting for Others";
			flag = true;
		}
		else if (GameplayManager.CurState == GameState.Hub_Bindings && PanelManager.CurPanel != PanelType.Bindings && VoteManager.IsVoting)
		{
			text = "Select Bindings";
			flag = true;
		}
		else if (GameplayManager.CurState == GameState.Reward_Enemy && PanelManager.CurPanel != PanelType.EnemyAugments)
		{
			text = "Torn Pages";
			flag = true;
		}
		if (PanelManager.CurPanel == PanelType.Augments && PlayerChoicePanel.instance.ShouldShow)
		{
			flag = false;
		}
		if (PanelManager.CurPanel == PanelType.EnemyAugments)
		{
			flag = false;
		}
		flag &= (GameplayUI.instance.HUDGroup.alpha > 0f);
		this.UpgradeGroup.UpdateOpacity(flag, 2f, false);
		if (this.UpgradeInfo.text != text && flag)
		{
			this.UpgradeInfo.text = text;
		}
		if (flag)
		{
			this.UpgradeTimer.fillAmount = fillAmount;
		}
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0006F012 File Offset: 0x0006D212
	public void CycleHudMode()
	{
		GameHUD.Mode = ((GameHUD.Mode == GameHUD.HUDMode.All) ? GameHUD.HUDMode.Off : GameHUD.HUDMode.All);
		Action onHUDModeChanged = this.OnHUDModeChanged;
		if (onHUDModeChanged == null)
		{
			return;
		}
		onHUDModeChanged();
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x0006F034 File Offset: 0x0006D234
	private void UpdateTeammates()
	{
		List<PlayerControl> list = new List<PlayerControl>();
		for (int i = this.teamDisplays.Count - 1; i >= 0; i--)
		{
			if (this.teamDisplays[i].Player == null)
			{
				UnityEngine.Object.Destroy(this.teamDisplays[i].gameObject);
				this.teamDisplays.RemoveAt(i);
			}
			else
			{
				list.Add(this.teamDisplays[i].Player);
			}
		}
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!(playerControl == PlayerControl.myInstance) && !list.Contains(playerControl))
			{
				this.AddTeamUI(playerControl);
			}
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x0006F110 File Offset: 0x0006D310
	private void AddTeamUI(PlayerControl player)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TeammateRef, this.TeammateRef.transform.parent);
		gameObject.SetActive(true);
		TeammateUI component = gameObject.GetComponent<TeammateUI>();
		component.Setup(player);
		this.teamDisplays.Add(component);
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x0006F158 File Offset: 0x0006D358
	private void UpdateSigReminder()
	{
		bool flag = false;
		if (PlayerControl.myInstance != null && !PlayerControl.myInstance.IsDead)
		{
			Ability ability = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Utility);
			if (ability.IsOnCooldown || ability.ChargesAvailable < ability.ChargeCount)
			{
				this.sigAvailableFor = 0f;
			}
			else if (GameplayManager.instance.CurrentState == GameState.InWave && AIManager.AliveEnemies > 0)
			{
				this.sigAvailableFor += Time.deltaTime;
				if (this.sigAvailableFor > 25f && AIManager.AliveEnemies > 2)
				{
					flag = true;
				}
			}
		}
		if (PlayerControl.myInstance.actions.core.Root.magicColor == MagicColor.Neutral)
		{
			flag = false;
		}
		this.SignatureReminder.UpdateOpacity(flag, 4f, true);
		if (flag)
		{
			this.sigT += Time.deltaTime / this.ScaleAnimTime;
			if (this.sigT >= 1f)
			{
				this.sigT -= 1f;
			}
			this.SigScaleRect.transform.localScale = Vector3.one * this.ScaleCurve.Evaluate(this.sigT);
		}
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0006F288 File Offset: 0x0006D488
	public static void OnHealed(int amount)
	{
		if (amount <= 0)
		{
			return;
		}
		CombatTextController.ShowLocalHeal(amount);
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0006F295 File Offset: 0x0006D495
	public void TestDamage()
	{
		GameHUD.OnDamageTaken(new DamageInfo(0f, DNumType.Default, 0, 0f, null)
		{
			Amount = (float)this.DebugDmg,
			ShieldAmount = (float)this.DebugShield
		});
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
	public void GotRewardAugment(AugmentTree augment)
	{
		base.StopAllCoroutines();
		this.RewardInfo.SetupAugment(augment, 0, this.RewardInfo.transform.position.y, PlayerControl.myInstance, false);
		base.StartCoroutine("RewardSequence");
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0006F304 File Offset: 0x0006D504
	private IEnumerator RewardSequence()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 4f;
			this.RewardGroup.alpha = t;
			yield return true;
		}
		yield return new WaitForSeconds(3f);
		while (t > 0f)
		{
			t -= Time.deltaTime * 2f;
			this.RewardGroup.alpha = t;
			yield return true;
		}
		yield break;
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x0006F313 File Offset: 0x0006D513
	public void TestHeal()
	{
		GameHUD.OnHealed(UnityEngine.Random.Range(5, 25));
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x0006F322 File Offset: 0x0006D522
	public static void OnDamageTaken(DamageInfo dmg)
	{
		CombatTextController.ShowDamageTaken(dmg);
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0006F32A File Offset: 0x0006D52A
	private void OnDisable()
	{
		base.StopAllCoroutines();
		this.RewardGroup.alpha = 0f;
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0006F342 File Offset: 0x0006D542
	public GameHUD()
	{
	}

	// Token: 0x04001092 RID: 4242
	public static GameHUD instance;

	// Token: 0x04001093 RID: 4243
	public UIPanel GameplayInvisible;

	// Token: 0x04001094 RID: 4244
	[Header("Player Display")]
	public Image PlayerIcon;

	// Token: 0x04001095 RID: 4245
	public DeathRecap DeathInfo;

	// Token: 0x04001096 RID: 4246
	[Header("Signature Reminder")]
	public CanvasGroup SignatureReminder;

	// Token: 0x04001097 RID: 4247
	public RectTransform SigScaleRect;

	// Token: 0x04001098 RID: 4248
	public float ScaleAnimTime = 1.5f;

	// Token: 0x04001099 RID: 4249
	public AnimationCurve ScaleCurve;

	// Token: 0x0400109A RID: 4250
	[Header("Upgrade UI")]
	public CanvasGroup UpgradeGroup;

	// Token: 0x0400109B RID: 4251
	public TextMeshProUGUI UpgradeInfo;

	// Token: 0x0400109C RID: 4252
	public ParticleSystem UngradeParticles;

	// Token: 0x0400109D RID: 4253
	public Image UpgradeTimer;

	// Token: 0x0400109E RID: 4254
	private RectTransform upgradeRect;

	// Token: 0x0400109F RID: 4255
	[Header("Pickup Reward")]
	public CanvasGroup RewardGroup;

	// Token: 0x040010A0 RID: 4256
	public AugmentDetailBox RewardInfo;

	// Token: 0x040010A1 RID: 4257
	public GameObject TeammateRef;

	// Token: 0x040010A2 RID: 4258
	private List<TeammateUI> teamDisplays = new List<TeammateUI>();

	// Token: 0x040010A3 RID: 4259
	public static GameHUD.HUDMode Mode;

	// Token: 0x040010A4 RID: 4260
	public Action OnHUDModeChanged;

	// Token: 0x040010A5 RID: 4261
	private GraphicRaycaster raycaster;

	// Token: 0x040010A6 RID: 4262
	private float sigAvailableFor;

	// Token: 0x040010A7 RID: 4263
	private float sigT;

	// Token: 0x040010A8 RID: 4264
	public int DebugDmg;

	// Token: 0x040010A9 RID: 4265
	public int DebugShield;

	// Token: 0x02000574 RID: 1396
	public enum HUDMode
	{
		// Token: 0x04002736 RID: 10038
		All,
		// Token: 0x04002737 RID: 10039
		Essentials,
		// Token: 0x04002738 RID: 10040
		Off
	}

	// Token: 0x02000575 RID: 1397
	[CompilerGenerated]
	private sealed class <RewardSequence>d__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024FB RID: 9467 RVA: 0x000CFD93 File Offset: 0x000CDF93
		[DebuggerHidden]
		public <RewardSequence>d__40(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000CFDA2 File Offset: 0x000CDFA2
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000CFDA4 File Offset: 0x000CDFA4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GameHUD gameHUD = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_F3;
			case 3:
				this.<>1__state = -1;
				goto IL_F3;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(3f);
				this.<>1__state = 2;
				return true;
			}
			t += Time.deltaTime * 4f;
			gameHUD.RewardGroup.alpha = t;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_F3:
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime * 2f;
			gameHUD.RewardGroup.alpha = t;
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000CFEB2 File Offset: 0x000CE0B2
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000CFEBA File Offset: 0x000CE0BA
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000CFEC1 File Offset: 0x000CE0C1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002739 RID: 10041
		private int <>1__state;

		// Token: 0x0400273A RID: 10042
		private object <>2__current;

		// Token: 0x0400273B RID: 10043
		public GameHUD <>4__this;

		// Token: 0x0400273C RID: 10044
		private float <t>5__2;
	}
}
