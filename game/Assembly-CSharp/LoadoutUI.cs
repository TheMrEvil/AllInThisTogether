using System;
using System.Collections.Generic;
using Fluxy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B8 RID: 440
public class LoadoutUI : MonoBehaviour
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x0600121E RID: 4638 RVA: 0x0007031B File Offset: 0x0006E51B
	private bool ShouldShow
	{
		get
		{
			return !UITransitionHelper.InWavePrelim() && PanelManager.CurPanel != PanelType.Augments && (!PlayerNook.IsInEditMode || !MapManager.InLobbyScene);
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x00070342 File Offset: 0x0006E542
	private void Awake()
	{
		LoadoutUI.instance = this;
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x0007034C File Offset: 0x0006E54C
	private void Start()
	{
		if (Settings.LowRez)
		{
			foreach (Transform transform in this.BindingIcons)
			{
				transform.transform.localScale = Vector3.one * 1.33f;
			}
		}
		FluxyCanvasContainer container = UIFluidManager.GetContainer(UIFluidType.Front);
		if (container == null)
		{
			return;
		}
		foreach (LoadoutUI.AbilityDisplay abilityDisplay in this.AbilityDisplays)
		{
			if (abilityDisplay.FluidFX != null)
			{
				container.AddTarget(abilityDisplay.FluidFX);
			}
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x00070420 File Offset: 0x0006E620
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		bool isDead = PlayerControl.myInstance.IsDead;
		this.AliveAbilityGroup.UpdateOpacity(!isDead && this.ShouldShow, 4f, false);
		this.GhostAbilityGroup.UpdateOpacity(isDead && GameplayManager.instance.CurrentState != GameState.Ended && !PlayerControl.myInstance.Health.isAutoReviving && PlayerControl.myInstance.Health.CanSelfRevive, 4f, false);
		PlayerActions actions = PlayerControl.myInstance.actions;
		if (isDead)
		{
			this.GhostAbilityDisplay.UpdateDisplay(actions.GetAbility(PlayerAbilityType.Ghost));
		}
		foreach (LoadoutUI.AbilityDisplay abilityDisplay in this.AbilityDisplays)
		{
			abilityDisplay.UpdateDisplay(actions.GetAbility(abilityDisplay.PlayerAbilityType));
		}
		this.UpdateUltimate();
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x00070520 File Offset: 0x0006E720
	private void UpdateUltimate()
	{
		Ability ability = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Utility);
		if (this.cachedUlt != ability.AbilityTree)
		{
			this.cachedUlt = ability.AbilityTree;
			PlayerDB.CoreDisplay core = PlayerDB.GetCore(this.cachedUlt);
			if (core != null)
			{
				this.UltimateReadyRender.ChangeMaterial(core.AbilityGlow);
			}
		}
		bool shouldShow = ability.currentCD <= 0f;
		this.UltimateReadyGroup.UpdateOpacity(shouldShow, 2f, true);
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000705A0 File Offset: 0x0006E7A0
	public LoadoutUI()
	{
	}

	// Token: 0x04001103 RID: 4355
	public List<LoadoutUI.AbilityDisplay> AbilityDisplays;

	// Token: 0x04001104 RID: 4356
	public CanvasGroup AliveAbilityGroup;

	// Token: 0x04001105 RID: 4357
	public CanvasGroup GhostAbilityGroup;

	// Token: 0x04001106 RID: 4358
	public LoadoutUI.AbilityDisplay GhostAbilityDisplay;

	// Token: 0x04001107 RID: 4359
	public Sprite ManaNeeded;

	// Token: 0x04001108 RID: 4360
	public static LoadoutUI instance;

	// Token: 0x04001109 RID: 4361
	public List<Transform> BindingIcons;

	// Token: 0x0400110A RID: 4362
	[Header("Ultimate Display")]
	public CanvasGroup UltimateReadyGroup;

	// Token: 0x0400110B RID: 4363
	public UIMeshRenderer UltimateReadyRender;

	// Token: 0x0400110C RID: 4364
	private AbilityTree cachedUlt;

	// Token: 0x0400110D RID: 4365
	private bool wasUltReady;

	// Token: 0x0200057B RID: 1403
	[Serializable]
	public class AbilityDisplay
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000D043A File Offset: 0x000CE63A
		private bool ShouldShow
		{
			get
			{
				if (TutorialManager.InTutorial)
				{
					return TutorialManager.ShouldShowAbilityUI(this.PlayerAbilityType);
				}
				return this.cachedTree != null;
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000D045C File Offset: 0x000CE65C
		public void UpdateDisplay(Ability ability)
		{
			if (this.CanvasGroup != null)
			{
				this.CanvasGroup.UpdateOpacity(this.ShouldShow, 2f, false);
			}
			if (ability == null || ability.AbilityTree == null)
			{
				this.CooldownFill.fillAmount = 1f;
				return;
			}
			if (this.cachedTree != ability.AbilityTree)
			{
				this.cachedTree = ability.AbilityTree;
				foreach (Image image in this.Icons)
				{
					image.sprite = ability.AbilityTree.Root.Usage.AbilityMetadata.Icon;
				}
				this.CooldownFill.fillAmount = 1f;
			}
			if (this.ChargeCounter != null)
			{
				if (ability.props.Charges > 1)
				{
					this.ChargeCounter.text = ability.ChargesAvailable.ToString();
				}
				else if (this.ChargeCounter.text != "")
				{
					this.ChargeCounter.text = "";
				}
			}
			float num = ability.currentCD / ability.AbilityCooldown.AtLeast(0.1f);
			if (this.invertFill)
			{
				num = 1f - num;
			}
			this.UpdateCDTimer(ability);
			if (this.CooldownFill.fillAmount < 1f && ability.currentCD <= 0f && this.OnCooldownFX != null)
			{
				AudioManager.PlayInterfaceSFX(this.CooldownSFX, 0.75f, 0f);
				this.OnCooldownFX.Play();
				PlayerControl myInstance = PlayerControl.myInstance;
				if (myInstance != null)
				{
					myInstance.Display.AbilityCDComplete(this.PlayerAbilityType);
				}
			}
			if (this.AvailableDisplay != null)
			{
				this.AvailableDisplay.alpha = (float)((ability.currentCD <= 0f) ? 1 : 0);
			}
			this.CooldownFill.fillAmount = num;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000D066C File Offset: 0x000CE86C
		private void UpdateCDTimer(Ability ability)
		{
			if (this.CDTimerGroup == null)
			{
				return;
			}
			bool flag = ability.AbilityCooldown >= 2f && ability.currentCD > 0f;
			this.CDTimerGroup.UpdateOpacity(flag, 4f, true);
			if (flag)
			{
				this.CDTimer.text = Mathf.CeilToInt(ability.currentCD).ToString();
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000D06DC File Offset: 0x000CE8DC
		private void UpdateCost(int cost, PlayerAbilityType pType)
		{
			EffectProperties effectProperties = new EffectProperties(PlayerControl.myInstance);
			effectProperties.AbilityType = pType;
			int num = Mathf.CeilToInt(PlayerControl.myInstance.ModifyManaCost(effectProperties, (float)cost));
			for (int i = 0; i < this.ManaCosts.Length; i++)
			{
				if (i != num - 1)
				{
					this.ManaCosts[i].Root.SetActive(false);
				}
				else
				{
					this.ManaCosts[i].Root.SetActive(true);
					List<MagicColor> nextMana = PlayerControl.myInstance.Mana.GetNextMana(num);
					this.ManaCosts[i].ShowMana(nextMana);
				}
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000D0770 File Offset: 0x000CE970
		private void UpdateFluid(int manaCost, bool canCast, PlayerAbilityType pType)
		{
			if (this.FluidFX == null)
			{
				return;
			}
			if (!canCast || manaCost <= 0)
			{
				this.FluidFX.densityWeight = Mathf.Lerp(this.FluidFX.densityWeight, 0f, Time.deltaTime * 8f);
				return;
			}
			EffectProperties effectProperties = new EffectProperties(PlayerControl.myInstance);
			effectProperties.AbilityType = pType;
			int count = Mathf.CeilToInt(PlayerControl.myInstance.ModifyManaCost(effectProperties, (float)manaCost));
			List<MagicColor> nextMana = PlayerControl.myInstance.Mana.GetNextMana(count);
			MagicColor magicColor = MagicColor.Neutral;
			foreach (MagicColor magicColor2 in nextMana)
			{
				if (magicColor2 != MagicColor.Neutral && magicColor2 != MagicColor.Any)
				{
					magicColor = magicColor2;
					break;
				}
			}
			if (magicColor == MagicColor.Neutral)
			{
				this.FluidFX.densityWeight = Mathf.Lerp(this.FluidFX.densityWeight, 0f, Time.deltaTime * 8f);
				return;
			}
			Color bodyGlowColor = PlayerDB.GetCore(magicColor).BodyGlowColor;
			this.FluidFX.color = bodyGlowColor;
			this.FluidFX.densityWeight = Mathf.Lerp(this.FluidFX.densityWeight, 1f, Time.deltaTime * 8f);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000D08B4 File Offset: 0x000CEAB4
		public AbilityDisplay()
		{
		}

		// Token: 0x04002755 RID: 10069
		public PlayerAbilityType PlayerAbilityType;

		// Token: 0x04002756 RID: 10070
		public CanvasGroup CanvasGroup;

		// Token: 0x04002757 RID: 10071
		public CanvasGroup AvailableDisplay;

		// Token: 0x04002758 RID: 10072
		public ParticleSystem OnCooldownFX;

		// Token: 0x04002759 RID: 10073
		public AudioClip CooldownSFX;

		// Token: 0x0400275A RID: 10074
		public List<Image> Icons;

		// Token: 0x0400275B RID: 10075
		public Image CooldownFill;

		// Token: 0x0400275C RID: 10076
		public bool invertFill;

		// Token: 0x0400275D RID: 10077
		public CanvasGroup CDTimerGroup;

		// Token: 0x0400275E RID: 10078
		public TextMeshProUGUI CDTimer;

		// Token: 0x0400275F RID: 10079
		public TextMeshProUGUI ChargeCounter;

		// Token: 0x04002760 RID: 10080
		public FluxyTarget FluidFX;

		// Token: 0x04002761 RID: 10081
		private AbilityTree cachedTree;

		// Token: 0x04002762 RID: 10082
		public LoadoutUI.ManaCost[] ManaCosts;
	}

	// Token: 0x0200057C RID: 1404
	[Serializable]
	public class ManaCost
	{
		// Token: 0x0600251C RID: 9500 RVA: 0x000D08BC File Offset: 0x000CEABC
		public void ShowMana(List<MagicColor> elements)
		{
			for (int i = 0; i < Mathf.Min(elements.Count, this.Sprites.Length); i++)
			{
				Sprite sprite = (elements[i] == MagicColor.Any) ? LoadoutUI.instance.ManaNeeded : GameDB.GetElement(elements[i]).ManaIcon;
				this.Sprites[i].sprite = sprite;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000D091C File Offset: 0x000CEB1C
		public ManaCost()
		{
		}

		// Token: 0x04002763 RID: 10083
		public GameObject Root;

		// Token: 0x04002764 RID: 10084
		public Image[] Sprites;
	}

	// Token: 0x0200057D RID: 1405
	[Serializable]
	public class UltMaterial
	{
		// Token: 0x0600251E RID: 9502 RVA: 0x000D0924 File Offset: 0x000CEB24
		public UltMaterial()
		{
		}

		// Token: 0x04002765 RID: 10085
		public MagicColor magicColor;

		// Token: 0x04002766 RID: 10086
		public Material Material;
	}
}
