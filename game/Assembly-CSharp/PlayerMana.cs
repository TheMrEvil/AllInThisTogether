using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class PlayerMana : MonoBehaviour
{
	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x00030BB6 File Offset: 0x0002EDB6
	// (set) Token: 0x06000696 RID: 1686 RVA: 0x00030BBE File Offset: 0x0002EDBE
	public PlayerControl Control
	{
		[CompilerGenerated]
		get
		{
			return this.<Control>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<Control>k__BackingField = value;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x00030BC7 File Offset: 0x0002EDC7
	private bool isMine
	{
		get
		{
			return this.Control.IsMine;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000698 RID: 1688 RVA: 0x00030BD4 File Offset: 0x0002EDD4
	private bool isDead
	{
		get
		{
			return this.Control.IsDead;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x00030BE1 File Offset: 0x0002EDE1
	public float ManaRechargeRate
	{
		get
		{
			if (this.BaseManaRecharge == 0f)
			{
				return 0f;
			}
			return this.Control.GetPassiveMod(Passive.EntityValue.ManaRecharge, this.BaseManaRecharge);
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x0600069A RID: 1690 RVA: 0x00030C0C File Offset: 0x0002EE0C
	public int MaxMana
	{
		get
		{
			return this.CoreMana.Count;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x00030C19 File Offset: 0x0002EE19
	public int TotalMana
	{
		get
		{
			return this.MaxMana + this.TempMana.Count;
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600069C RID: 1692 RVA: 0x00030C2D File Offset: 0x0002EE2D
	public float CurrentManaProportion
	{
		get
		{
			return (float)this.CurrentMana / (float)this.TotalMana;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x0600069D RID: 1693 RVA: 0x00030C40 File Offset: 0x0002EE40
	public bool HasNeutralMana
	{
		get
		{
			using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.magicColor == MagicColor.Neutral)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600069E RID: 1694 RVA: 0x00030C9C File Offset: 0x0002EE9C
	public int CurrentMana
	{
		get
		{
			int num = 0;
			using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsAvailable)
					{
						num++;
					}
				}
			}
			return num + this.TempMana.Count;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x00030D04 File Offset: 0x0002EF04
	public float CurrentColorManaProportion
	{
		get
		{
			float num = (float)Mathf.Max(this.CoreMana.Count, 1);
			int num2 = 0;
			using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.magicColor != MagicColor.Neutral)
					{
						num2++;
					}
				}
			}
			return (float)num2 / num;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00030D74 File Offset: 0x0002EF74
	public int ColorManaAvailable
	{
		get
		{
			int num = this.CoreColorManaAvailable;
			using (List<Mana>.Enumerator enumerator = this.TempMana.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.magicColor != MagicColor.Neutral)
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00030DD4 File Offset: 0x0002EFD4
	public int CoreColorManaAvailable
	{
		get
		{
			int num = 0;
			using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.magicColor != MagicColor.Neutral)
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00030E30 File Offset: 0x0002F030
	public Augments ManaBaseAugments
	{
		get
		{
			if (this.manaCache == null)
			{
				this.manaCache = new Augments();
				Dictionary<MagicColor, int> dictionary = new Dictionary<MagicColor, int>();
				foreach (Mana mana in this.CoreMana)
				{
					if (mana.IsAvailable)
					{
						if (!dictionary.ContainsKey(mana.magicColor))
						{
							dictionary.Add(mana.magicColor, 0);
						}
						Dictionary<MagicColor, int> dictionary2 = dictionary;
						MagicColor magicColor = mana.magicColor;
						int num = dictionary2[magicColor];
						dictionary2[magicColor] = num + 1;
					}
				}
				foreach (Mana mana2 in this.TempMana)
				{
					if (mana2.IsAvailable)
					{
						if (!dictionary.ContainsKey(mana2.magicColor))
						{
							dictionary.Add(mana2.magicColor, 0);
						}
						Dictionary<MagicColor, int> dictionary3 = dictionary;
						MagicColor magicColor = mana2.magicColor;
						int num = dictionary3[magicColor];
						dictionary3[magicColor] = num + 1;
					}
				}
				foreach (KeyValuePair<MagicColor, int> keyValuePair in dictionary)
				{
					AugmentTree baseAugment = GameDB.GetElement(keyValuePair.Key).BaseAugment;
					if (!(baseAugment == null))
					{
						this.manaCache.Add(baseAugment, keyValuePair.Value);
					}
				}
			}
			return this.manaCache;
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00030FC8 File Offset: 0x0002F1C8
	private void Awake()
	{
		this.Control = base.GetComponent<PlayerControl>();
		PlayerActions component = base.GetComponent<PlayerActions>();
		component.abilityCastSucceeded = (Action<PlayerAbilityType, Dictionary<MagicColor, int>>)Delegate.Combine(component.abilityCastSucceeded, new Action<PlayerAbilityType, Dictionary<MagicColor, int>>(this.AbilityUsed));
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00030FFD File Offset: 0x0002F1FD
	public void Setup()
	{
		this.ResetMana();
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00031008 File Offset: 0x0002F208
	public void ResetMana()
	{
		this.CoreMana.Clear();
		this.TempMana.Clear();
		if (this.Control.actions.core != null)
		{
			this.AddMana(this.Control.actions.core.Root.magicColor, false, true);
		}
		for (int i = 0; i < this.BaseMana - 1; i++)
		{
			this.AddMana(MagicColor.Neutral, false, true);
		}
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00031081 File Offset: 0x0002F281
	private void AbilityUsed(PlayerAbilityType aType, Dictionary<MagicColor, int> manaUsed)
	{
		if (this.Control == PlayerControl.myInstance && aType == PlayerAbilityType.Utility)
		{
			GameStats.IncrementStat(PlayerControl.myInstance.SignatureColor, GameStats.SignatureStat.UltsCast, 1U, false);
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x000310AB File Offset: 0x0002F2AB
	public bool IsCooldownHalted()
	{
		return this.Control.IsUsingActiveAbility();
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x000310B8 File Offset: 0x0002F2B8
	private void Update()
	{
		if (this.isMine)
		{
			float num = this.ManaRechargeRate;
			if (this.IsCooldownHalted())
			{
				num *= 10f;
			}
			Mana rechargingMana = this.GetRechargingMana(num);
			if (rechargingMana != null)
			{
				rechargingMana.TickRecharge(Time.deltaTime / num, true);
			}
		}
		this.manaCache = null;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00031108 File Offset: 0x0002F308
	public Mana GetRechargingMana(float chargeRate)
	{
		for (int i = this.CoreMana.Count - 1; i >= 0; i--)
		{
			Mana mana = this.CoreMana[i];
			if (!mana.IsAvailable && chargeRate != 0f)
			{
				return mana;
			}
		}
		return null;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00031150 File Offset: 0x0002F350
	public void AddMana(MagicColor magicColor, bool temporary, bool ignoreFX = false)
	{
		if (temporary && this.TempMana.Count >= 5)
		{
			return;
		}
		Mana mana = new Mana(magicColor, this.Control, temporary);
		if (temporary)
		{
			this.TempMana.Add(mana);
		}
		else
		{
			this.CoreMana.Add(mana);
		}
		mana.Reset(true, ignoreFX);
		if (this.isMine)
		{
			ManaDisplay.instance.ManaChanged();
			if (!ignoreFX)
			{
				this.Control.Audio.PlayManaGained(magicColor);
			}
		}
		Action<Mana, bool> onGained = mana.OnGained;
		if (onGained != null)
		{
			onGained(mana, ignoreFX);
		}
		this.OnMaianGained(magicColor, temporary);
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x000311E4 File Offset: 0x0002F3E4
	public void RemoveMana(MagicColor e, bool fromFront)
	{
		if (fromFront)
		{
			for (int i = 0; i < this.CoreMana.Count; i++)
			{
				if (this.CoreMana[i].magicColor == e || e == MagicColor.Any)
				{
					this.CoreMana.RemoveAt(i);
					if (this.isMine)
					{
						ManaDisplay.instance.ManaChanged();
					}
					return;
				}
			}
			return;
		}
		for (int j = this.CoreMana.Count - 1; j >= 0; j--)
		{
			if (this.CoreMana[j].magicColor == e || e == MagicColor.Any)
			{
				this.CoreMana.RemoveAt(j);
				if (this.isMine)
				{
					ManaDisplay.instance.ManaChanged();
				}
				return;
			}
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00031290 File Offset: 0x0002F490
	public void RemoveTempMana(MagicColor e)
	{
		for (int i = 0; i < this.TempMana.Count; i++)
		{
			if (this.TempMana[i].magicColor == e)
			{
				this.TempMana.RemoveAt(i);
				if (this.isMine)
				{
					ManaDisplay.instance.ManaChanged();
				}
				return;
			}
		}
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x000312E8 File Offset: 0x0002F4E8
	public void ColorizeNeutral(int amount, bool coreOnly, MagicColor color)
	{
		if (color == MagicColor.Neutral)
		{
			Debug.Log("Can't colorize to neutral");
			return;
		}
		int num = 0;
		foreach (Mana mana in this.CoreMana)
		{
			if (num >= amount)
			{
				break;
			}
			if (mana.magicColor == MagicColor.Neutral && mana.IsAvailable)
			{
				mana.ChargeColorExplicit(color);
				num++;
			}
		}
		if (!coreOnly)
		{
			foreach (Mana mana2 in this.TempMana)
			{
				if (num >= amount)
				{
					break;
				}
				if (mana2.magicColor == MagicColor.Neutral && mana2.IsAvailable)
				{
					mana2.ChargeColorExplicit(color);
					num++;
				}
			}
		}
		if (this.isMine)
		{
			ManaDisplay.instance.ManaChanged();
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000313DC File Offset: 0x0002F5DC
	public void ChangeManaElementTemp(bool requireNeutral, MagicColor e, int count = 1)
	{
		for (int i = this.TempMana.Count - 1; i >= 0; i--)
		{
			if (!requireNeutral || this.TempMana[i].magicColor == MagicColor.Neutral)
			{
				this.TempMana[i].magicColor = e;
				this.TempMana[i].Reset(true, false);
				count--;
				if (count <= 0)
				{
					return;
				}
			}
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00031448 File Offset: 0x0002F648
	public void ChangeManaElement(bool requireNeutral, bool fromFront, MagicColor e)
	{
		if (fromFront)
		{
			for (int i = 0; i < this.CoreMana.Count; i++)
			{
				if ((!requireNeutral || this.CoreMana[i].magicColor == MagicColor.Neutral) && this.CoreMana[i].magicColor != e)
				{
					this.ChangeManaElement(i, e);
					return;
				}
			}
			return;
		}
		for (int j = this.CoreMana.Count - 1; j >= 0; j--)
		{
			if ((!requireNeutral || this.CoreMana[j].magicColor == MagicColor.Neutral) && this.CoreMana[j].magicColor != e)
			{
				this.ChangeManaElement(j, e);
				return;
			}
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x000314EF File Offset: 0x0002F6EF
	private void ChangeManaElement(int index, MagicColor e)
	{
		this.CoreMana[index].magicColor = e;
		this.CoreMana[index].Reset(true, false);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00031516 File Offset: 0x0002F716
	public void ManaGenerated(EffectProperties props)
	{
		this.Control.TriggerSnippets(EventTrigger.ManaGenerated, props, 1f);
		this.Control.PStats.IncreaseCounts(PlayerStat.Mana_Provided, (int)props.GetExtra(EProp.Snip_Input, 1f));
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00031550 File Offset: 0x0002F750
	public void Recharge(float amount)
	{
		if (this.isDead || amount <= 0f)
		{
			return;
		}
		int num = this.CoreMana.Count - 1;
		while (num >= 0 && amount > 0f)
		{
			if (!this.CoreMana[num].IsAvailable)
			{
				float num2 = Mathf.Min(amount, this.CoreMana[num].NeededForRecharge);
				this.CoreMana[num].TickRecharge(num2, false);
				amount -= num2;
			}
			num--;
		}
		int num3 = 0;
		while (num3 < this.TempMana.Count && amount > 0f)
		{
			if (!this.TempMana[num3].IsAvailable || this.TempMana[num3].magicColor == MagicColor.Neutral)
			{
				float num4 = Mathf.Min(amount, this.TempMana[num3].NeededForRecharge);
				this.TempMana[num3].TickRecharge(num4, false);
				amount -= num4;
			}
			num3++;
		}
		int num5 = 0;
		while (num5 < this.CoreMana.Count && amount > 0f)
		{
			if (!this.CoreMana[num5].IsAvailable || this.CoreMana[num5].magicColor == MagicColor.Neutral)
			{
				float num6 = Mathf.Min(amount, this.CoreMana[num5].NeededForRecharge);
				this.CoreMana[num5].TickRecharge(num6, false);
				amount -= num6;
			}
			num5++;
		}
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x000316CC File Offset: 0x0002F8CC
	public void Drain()
	{
		this.TempMana.Clear();
		foreach (Mana mana in this.CoreMana)
		{
			mana.Drain();
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00031728 File Offset: 0x0002F928
	public Dictionary<MagicColor, int> ConsumeMana(float amount, bool local)
	{
		Dictionary<MagicColor, int> dictionary = new Dictionary<MagicColor, int>();
		if (this.isDead || amount == 0f)
		{
			return dictionary;
		}
		if (amount < 0f)
		{
			this.Recharge(-amount);
			return dictionary;
		}
		new List<Mana>();
		foreach (Mana mana in this.PrepareUsage((int)amount))
		{
			mana.Consume((int)amount);
			if (this.isMine && mana.IsTemp)
			{
				ManaDisplay.instance.ManaChanged();
			}
			if (!dictionary.ContainsKey(mana.magicColor))
			{
				dictionary.Add(mana.magicColor, 0);
			}
			Dictionary<MagicColor, int> dictionary2 = dictionary;
			MagicColor magicColor = mana.magicColor;
			int num = dictionary2[magicColor];
			dictionary2[magicColor] = num + 1;
			if (local)
			{
				this.OnManaUsed(mana.magicColor, mana.IsTemp);
			}
			mana.magicColor = MagicColor.Neutral;
		}
		this.manaCache = null;
		return dictionary;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00031828 File Offset: 0x0002FA28
	public List<MagicColor> GetNextMana(int count)
	{
		List<MagicColor> list = new List<MagicColor>();
		for (int i = 0; i < this.TempMana.Count; i++)
		{
			if (count <= 0)
			{
				return list;
			}
			list.Add(this.TempMana[i].magicColor);
			count--;
		}
		for (int j = 0; j < this.CoreMana.Count; j++)
		{
			if (count <= 0)
			{
				return list;
			}
			if (this.CoreMana[j].IsAvailable)
			{
				list.Add(this.CoreMana[j].magicColor);
				count--;
			}
		}
		for (int k = 0; k < count; k++)
		{
			list.Add(MagicColor.Any);
		}
		return list;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x000318D4 File Offset: 0x0002FAD4
	private List<Mana> PrepareUsage(int amount)
	{
		List<Mana> list = new List<Mana>();
		int num = 0;
		while (num < this.TempMana.Count && amount > 0)
		{
			list.Add(this.TempMana[num]);
			amount--;
			num++;
		}
		foreach (Mana item in list)
		{
			this.TempMana.Remove(item);
		}
		int num2 = 0;
		while (num2 < this.CoreMana.Count && amount > 0)
		{
			if (this.CoreMana[num2].IsAvailable)
			{
				list.Add(this.CoreMana[num2]);
				amount--;
			}
			num2++;
		}
		return list;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x000319AC File Offset: 0x0002FBAC
	public bool CanUseMana(float amount)
	{
		if (amount <= 0f)
		{
			return true;
		}
		if (this.isDead)
		{
			return false;
		}
		foreach (Mana mana in this.TempMana)
		{
			amount -= 1f;
			if (amount <= 0f)
			{
				return true;
			}
		}
		using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsAvailable)
				{
					amount -= 1f;
					if (amount <= 0f)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00031A7C File Offset: 0x0002FC7C
	public bool HasManaElement(MagicColor e)
	{
		using (List<Mana>.Enumerator enumerator = this.CoreMana.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.magicColor == e)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00031AD8 File Offset: 0x0002FCD8
	private int IndexOf(Mana m)
	{
		if (!m.IsTemp)
		{
			return this.CoreMana.IndexOf(m);
		}
		return this.TempMana.IndexOf(m);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00031AFB File Offset: 0x0002FCFB
	private void AddTemp()
	{
		this.AddMana(MagicColor.Blue, true, false);
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00031B08 File Offset: 0x0002FD08
	private void OnManaUsed(MagicColor e, bool temp)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.StartLoc = (effectProperties.OutLoc = this.Control.Display.CenterLocation);
		effectProperties.SourceControl = this.Control;
		effectProperties.Affected = this.Control.gameObject;
		effectProperties.AddMana(e, 1);
		this.Control.TriggerSnippets(EventTrigger.ManaUsed, effectProperties, 1f);
		if (this.Control == PlayerControl.myInstance && e != MagicColor.Neutral && e != MagicColor.Any)
		{
			GameStats.IncrementStat(e, GameStats.SignatureStat.ManaSpent, 1U, false);
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00031B94 File Offset: 0x0002FD94
	public void OnManaRecharged(MagicColor e)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.StartLoc = (effectProperties.OutLoc = this.Control.Display.CenterLocation);
		effectProperties.SourceControl = this.Control;
		effectProperties.AddMana(e, 1);
		this.Control.TriggerSnippets(EventTrigger.ManaRecharged, effectProperties, 1f);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00031BF0 File Offset: 0x0002FDF0
	public void OnManaTransformed(MagicColor e)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.StartLoc = (effectProperties.OutLoc = this.Control.Display.CenterLocation);
		effectProperties.SourceControl = this.Control;
		effectProperties.AddMana(e, 1);
		this.Control.TriggerSnippets(EventTrigger.ManaTransformed, effectProperties, 1f);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00031C4C File Offset: 0x0002FE4C
	private void OnMaianGained(MagicColor e, bool temp)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.StartLoc = (effectProperties.OutLoc = this.Control.Display.CenterLocation);
		effectProperties.SourceControl = this.Control;
		effectProperties.AddMana(e, 1);
		this.Control.TriggerSnippets(EventTrigger.ManaGained, effectProperties, 1f);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00031CA5 File Offset: 0x0002FEA5
	public PlayerMana()
	{
	}

	// Token: 0x04000596 RID: 1430
	[CompilerGenerated]
	private PlayerControl <Control>k__BackingField;

	// Token: 0x04000597 RID: 1431
	public int BaseMana = 5;

	// Token: 0x04000598 RID: 1432
	[Tooltip("Time in seconds required to fully recharge 1 mana pip")]
	public float BaseManaRecharge = 5f;

	// Token: 0x04000599 RID: 1433
	private Augments manaCache;

	// Token: 0x0400059A RID: 1434
	public List<Mana> CoreMana = new List<Mana>();

	// Token: 0x0400059B RID: 1435
	public List<Mana> TempMana = new List<Mana>();
}
