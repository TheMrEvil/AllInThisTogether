using System;
using UnityEngine.Serialization;

// Token: 0x02000092 RID: 146
[Serializable]
public class Mana
{
	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00031CD5 File Offset: 0x0002FED5
	public bool IsAvailable
	{
		get
		{
			return this.T >= 1f;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00031CE7 File Offset: 0x0002FEE7
	public bool IsColorCompleted
	{
		get
		{
			return this.C >= 1f;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00031CF9 File Offset: 0x0002FEF9
	public float RechargeProp
	{
		get
		{
			if (this.T >= 1f)
			{
				return this.C / 1f;
			}
			return this.T / 1f;
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00031D21 File Offset: 0x0002FF21
	public Mana(MagicColor e, PlayerControl owner, bool Temp = false)
	{
		this.magicColor = e;
		this.control = owner;
		this.wantedColor = owner.SignatureColor;
		this.IsTemp = Temp;
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00031D58 File Offset: 0x0002FF58
	public float NeededForRecharge
	{
		get
		{
			if (this.T >= 1f)
			{
				return 1f - this.C;
			}
			return 1f - this.T;
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00031D80 File Offset: 0x0002FF80
	public bool TickRecharge(float deltaTime, bool fromTime = false)
	{
		bool flag = false;
		if (this.T < 1f)
		{
			deltaTime = this.ChargeNeutral(deltaTime);
			flag = (deltaTime >= 0f);
			if (!fromTime)
			{
				Action<Mana> onRechargedPartial = this.OnRechargedPartial;
				if (onRechargedPartial != null)
				{
					onRechargedPartial(this);
				}
			}
		}
		if (this.C < 1f && deltaTime > 0f)
		{
			flag |= this.ChargeColor(deltaTime);
			if (!fromTime)
			{
				Action<Mana> onRechargedPartial2 = this.OnRechargedPartial;
				if (onRechargedPartial2 != null)
				{
					onRechargedPartial2(this);
				}
			}
		}
		return flag;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00031DFC File Offset: 0x0002FFFC
	private float ChargeNeutral(float deltaTime)
	{
		this.T += deltaTime;
		if (this.T >= 1f)
		{
			this.T = 1f;
			this.Recharge(false);
			return this.T - 1f;
		}
		return -1f;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00031E48 File Offset: 0x00030048
	private bool ChargeColor(float deltaTime)
	{
		this.C += deltaTime;
		if (this.C >= 1f)
		{
			this.C = 1f;
			this.magicColor = this.wantedColor;
			this.Transform(this.magicColor, false);
			return true;
		}
		return false;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00031E97 File Offset: 0x00030097
	public void ChargeColorExplicit(MagicColor e)
	{
		this.T = 1f;
		this.C = 1f;
		this.magicColor = e;
		this.Transform(this.magicColor, false);
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00031EC4 File Offset: 0x000300C4
	public void Consume(int manaConsumed)
	{
		if (this.control.IsDead)
		{
			return;
		}
		if (this.magicColor == MagicColor.Neutral)
		{
			this.T = this.C;
			this.C = 0f;
		}
		else
		{
			this.T = 0f;
			this.C = 0f;
		}
		AugmentTree baseAugment = GameDB.GetElement(this.magicColor).BaseAugment;
		if (baseAugment != null)
		{
			EffectProperties effectProperties = new EffectProperties(this.control);
			effectProperties.SetExtra(EProp.Stacks, (float)manaConsumed);
			effectProperties.AbilityType = PlayerAbilityType.None;
			baseAugment.Root.TryTrigger(this.control, EventTrigger.Died, effectProperties, 1f);
		}
		Action<Mana> onUsed = this.OnUsed;
		if (onUsed == null)
		{
			return;
		}
		onUsed(this);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00031F78 File Offset: 0x00030178
	public void Reset(bool invokeEvents, bool ignoreFX = false)
	{
		this.T = 1f;
		this.C = 0f;
		this.wantedColor = this.control.actions.core.Root.magicColor;
		if (invokeEvents)
		{
			this.Recharge(ignoreFX);
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00031FC5 File Offset: 0x000301C5
	public void Drain()
	{
		this.T = 0f;
		this.C = 0f;
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00031FE0 File Offset: 0x000301E0
	private void Recharge(bool ignoreFX = false)
	{
		if (this.control.IsDead)
		{
			return;
		}
		AugmentTree baseAugment = GameDB.GetElement(this.magicColor).BaseAugment;
		if (baseAugment != null)
		{
			baseAugment.Root.TryTrigger(this.control, EventTrigger.Spawned, null, 1f);
		}
		Action<Mana, bool> onRegen = this.OnRegen;
		if (onRegen != null)
		{
			onRegen(this, ignoreFX);
		}
		this.control.Mana.OnManaRecharged(this.magicColor);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00032058 File Offset: 0x00030258
	private void Transform(MagicColor e, bool ignoreFX = false)
	{
		if (this.control.IsDead)
		{
			return;
		}
		AugmentTree baseAugment = GameDB.GetElement(this.magicColor).BaseAugment;
		if (baseAugment != null)
		{
			baseAugment.Root.TryTrigger(this.control, EventTrigger.ManaTransformed, null, 1f);
		}
		Action<Mana, bool> onTransform = this.OnTransform;
		if (onTransform != null)
		{
			onTransform(this, ignoreFX);
		}
		this.control.Mana.OnManaTransformed(this.magicColor);
	}

	// Token: 0x0400059C RID: 1436
	[FormerlySerializedAs("Element")]
	public MagicColor magicColor = MagicColor.Neutral;

	// Token: 0x0400059D RID: 1437
	public MagicColor wantedColor = MagicColor.Neutral;

	// Token: 0x0400059E RID: 1438
	public float T;

	// Token: 0x0400059F RID: 1439
	public float C;

	// Token: 0x040005A0 RID: 1440
	private PlayerControl control;

	// Token: 0x040005A1 RID: 1441
	public bool IsTemp;

	// Token: 0x040005A2 RID: 1442
	public Action<Mana, bool> OnGained;

	// Token: 0x040005A3 RID: 1443
	public Action<Mana> OnUsed;

	// Token: 0x040005A4 RID: 1444
	public Action<Mana, bool> OnRegen;

	// Token: 0x040005A5 RID: 1445
	public Action<Mana> OnRechargedPartial;

	// Token: 0x040005A6 RID: 1446
	public Action<Mana, bool> OnTransform;
}
