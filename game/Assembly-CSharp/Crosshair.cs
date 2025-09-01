using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A2 RID: 418
public class Crosshair : MonoBehaviour
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x0600116F RID: 4463 RVA: 0x0006C09D File Offset: 0x0006A29D
	private bool ShouldShow
	{
		get
		{
			return !UITransitionHelper.InWavePrelim() && PanelManager.CurPanel != PanelType.Augments && PanelManager.CurPanel != PanelType.Pause;
		}
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0006C0BC File Offset: 0x0006A2BC
	private void Awake()
	{
		Crosshair.instance = this;
		Crosshair.rect = base.GetComponent<RectTransform>();
		Crosshair.wantAnchor = Vector2.one * 0.5f;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		base.InvokeRepeating("CheckCrosshair", 1f, 2.872351f);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0006C10F File Offset: 0x0006A30F
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.canvasGroup.UpdateOpacity(this.ShouldShow, 4f, true);
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x0006C138 File Offset: 0x0006A338
	private void CheckCrosshair()
	{
		int @int = Settings.GetInt(SystemSetting.Crosshair, 0);
		for (int i = 0; i < this.Crosshairs.Count; i++)
		{
			this.Crosshairs[i].SetActive(i == @int);
		}
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x0006C179 File Offset: 0x0006A379
	public void CastFailed(PlayerAbilityType abilityType, CastFailedReason reason, int cost)
	{
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0006C17B File Offset: 0x0006A37B
	public void AbilityManaUsed(PlayerAbilityType abilityType, Dictionary<MagicColor, int> manaUsed)
	{
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0006C17D File Offset: 0x0006A37D
	public static void SetTargetPoint(Vector3 worldPoint)
	{
		if (PlayerMovement.myCamera == null || Crosshair.instance == null)
		{
			return;
		}
		Crosshair.instance.worldPoint = worldPoint;
		Crosshair.wantAnchor = PlayerMovement.myCamera.WorldToViewportPoint(worldPoint);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0006C1BA File Offset: 0x0006A3BA
	public Crosshair()
	{
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0006C1C2 File Offset: 0x0006A3C2
	// Note: this type is marked as 'beforefieldinit'.
	static Crosshair()
	{
	}

	// Token: 0x04001005 RID: 4101
	public static Crosshair instance;

	// Token: 0x04001006 RID: 4102
	public Vector3 worldPoint;

	// Token: 0x04001007 RID: 4103
	private static RectTransform rect;

	// Token: 0x04001008 RID: 4104
	private static Vector2 wantAnchor = Vector2.one * 0.5f;

	// Token: 0x04001009 RID: 4105
	private CanvasGroup canvasGroup;

	// Token: 0x0400100A RID: 4106
	public List<Crosshair.AbilityArc> Arcs;

	// Token: 0x0400100B RID: 4107
	public List<GameObject> Crosshairs;

	// Token: 0x0400100C RID: 4108
	public AnimationCurve ManaPulseCurve;

	// Token: 0x0400100D RID: 4109
	public Sprite InvalidMana;

	// Token: 0x0400100E RID: 4110
	public Sprite FreeMana;

	// Token: 0x0400100F RID: 4111
	public AnimationCurve OutCurve;

	// Token: 0x04001010 RID: 4112
	public AnimationCurve InCurve;

	// Token: 0x0200056C RID: 1388
	[Serializable]
	public class AbilityArc
	{
		// Token: 0x060024DC RID: 9436 RVA: 0x000CF7A6 File Offset: 0x000CD9A6
		public void Failed(CastFailedReason reason, int cost)
		{
			if (reason == CastFailedReason.Cooldown)
			{
				this.FlashCD();
				return;
			}
			if (reason == CastFailedReason.Mana)
			{
				this.ShowCost(cost);
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000CF7BE File Offset: 0x000CD9BE
		private void FlashCD()
		{
			this.CDFlash.alpha = 1f;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000CF7D0 File Offset: 0x000CD9D0
		private void ShowCost(int cost)
		{
			if (this.ManaPulses.Count == 0)
			{
				return;
			}
			int index = Mathf.Clamp(cost - 1, 0, this.ManaPulses.Count);
			this.ManaPulses[index].PulseEmpty();
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000CF814 File Offset: 0x000CDA14
		public void ManaUsed(Dictionary<MagicColor, int> manaUsed)
		{
			if (this.ManaPulses.Count == 0)
			{
				return;
			}
			if (manaUsed.Count == 0)
			{
				this.ManaPulses[0].PulseFree();
				return;
			}
			List<MagicColor> list = new List<MagicColor>();
			foreach (KeyValuePair<MagicColor, int> keyValuePair in manaUsed)
			{
				MagicColor magicColor;
				int num;
				keyValuePair.Deconstruct(out magicColor, out num);
				MagicColor item = magicColor;
				int num2 = num;
				for (int i = 0; i < num2; i++)
				{
					list.Add(item);
				}
			}
			int index = Mathf.Clamp(list.Count - 1, 0, this.ManaPulses.Count);
			this.ManaPulses[index].ShowUsed(list);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000CF8E0 File Offset: 0x000CDAE0
		public void UpdateDisplay(Crosshair crosshair)
		{
			if (PlayerControl.myInstance == null)
			{
				return;
			}
			Ability ability = PlayerControl.myInstance.actions.GetAbility(this.PlayerAbilityType);
			if (this.CDFlash.alpha > 0f)
			{
				this.CDFlash.alpha -= Time.deltaTime * 3f;
			}
			foreach (Crosshair.AbilityArc.ManaPulse manaPulse in this.ManaPulses)
			{
				manaPulse.UpdateIfOpen();
				if (manaPulse.extraShowTime > 0f)
				{
					manaPulse.extraShowTime -= Time.deltaTime;
				}
				else
				{
					manaPulse.Group.alpha -= Time.deltaTime * 2f;
				}
			}
			if (ability.IsOnCooldown)
			{
				if (this.T < 1f)
				{
					this.T += Time.deltaTime * 3f;
				}
				float num = ability.currentCD / ability.AbilityCooldown;
				this.CooldownFill.fillAmount = (this.invertFill ? (1f - num) : num);
				this.ArcRect.localScale = Vector3.one * crosshair.OutCurve.Evaluate(this.T);
				this.Group.alpha = crosshair.OutCurve.Evaluate(this.T);
				return;
			}
			if (this.T > 0f)
			{
				this.T -= Time.deltaTime * 3f;
			}
			this.CooldownFill.fillAmount = (float)(this.invertFill ? 0 : 1);
			this.ArcRect.localScale = Vector3.one * crosshair.InCurve.Evaluate(this.T);
			this.Group.alpha = crosshair.OutCurve.Evaluate(this.T);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000CFAE0 File Offset: 0x000CDCE0
		public AbilityArc()
		{
		}

		// Token: 0x04002716 RID: 10006
		public PlayerAbilityType PlayerAbilityType;

		// Token: 0x04002717 RID: 10007
		public List<Crosshair.AbilityArc.ManaPulse> ManaPulses;

		// Token: 0x04002718 RID: 10008
		public CanvasGroup Group;

		// Token: 0x04002719 RID: 10009
		public RectTransform ArcRect;

		// Token: 0x0400271A RID: 10010
		public Image CooldownFill;

		// Token: 0x0400271B RID: 10011
		public CanvasGroup CDFlash;

		// Token: 0x0400271C RID: 10012
		public bool invertFill;

		// Token: 0x0400271D RID: 10013
		private float T;

		// Token: 0x020006C2 RID: 1730
		[Serializable]
		public class ManaPulse
		{
			// Token: 0x06002866 RID: 10342 RVA: 0x000D8928 File Offset: 0x000D6B28
			public void UpdateIfOpen()
			{
				if (this.Group.alpha <= 0f)
				{
					return;
				}
				Image[] sprites = this.Sprites;
				for (int i = 0; i < sprites.Length; i++)
				{
					sprites[i].transform.localScale = Vector3.one * Crosshair.instance.ManaPulseCurve.Evaluate(this.pulseT);
				}
				if (this.pulseT < 1f)
				{
					this.pulseT += Time.deltaTime;
				}
			}

			// Token: 0x06002867 RID: 10343 RVA: 0x000D89A8 File Offset: 0x000D6BA8
			public void PulseEmpty()
			{
				this.extraShowTime = 0f;
				Image[] sprites = this.Sprites;
				for (int i = 0; i < sprites.Length; i++)
				{
					sprites[i].sprite = Crosshair.instance.InvalidMana;
				}
				this.Group.alpha = 1f;
				this.pulseT = 0f;
			}

			// Token: 0x06002868 RID: 10344 RVA: 0x000D8A04 File Offset: 0x000D6C04
			public void PulseFree()
			{
				this.extraShowTime = 0.5f;
				for (int i = 0; i < this.Sprites.Length; i++)
				{
					this.Sprites[i].sprite = Crosshair.instance.FreeMana;
				}
				this.Group.alpha = 1f;
				this.pulseT = 0f;
			}

			// Token: 0x06002869 RID: 10345 RVA: 0x000D8A64 File Offset: 0x000D6C64
			public void ShowUsed(List<MagicColor> elements)
			{
				this.extraShowTime = 0.5f;
				for (int i = 0; i < elements.Count; i++)
				{
					this.Sprites[i].sprite = GameDB.GetElement(elements[i]).ManaIcon;
				}
				this.Group.alpha = 1f;
				this.pulseT = 0f;
			}

			// Token: 0x0600286A RID: 10346 RVA: 0x000D8AC6 File Offset: 0x000D6CC6
			public ManaPulse()
			{
			}

			// Token: 0x04002CE1 RID: 11489
			public CanvasGroup Group;

			// Token: 0x04002CE2 RID: 11490
			public Image[] Sprites;

			// Token: 0x04002CE3 RID: 11491
			[NonSerialized]
			public float extraShowTime;

			// Token: 0x04002CE4 RID: 11492
			private float pulseT;
		}
	}
}
