using System;
using System.Collections.Generic;
using SimpleJSON;

// Token: 0x0200008E RID: 142
[Serializable]
public class PlayerGameStats
{
	// Token: 0x0600066F RID: 1647 RVA: 0x0002F92C File Offset: 0x0002DB2C
	public PlayerGameStats()
	{
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0002F96B File Offset: 0x0002DB6B
	public PlayerGameStats(string json) : this(JSON.Parse(json))
	{
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0002F97C File Offset: 0x0002DB7C
	public PlayerGameStats(JSONNode n)
	{
		JSONNode jsonnode = n["Damages"];
		foreach (string text in jsonnode.Keys)
		{
			PlayerStat key = (PlayerStat)int.Parse(text);
			this.Damages.Add(key, jsonnode.GetValueOrDefault(text, 0));
		}
		JSONNode jsonnode2 = n["Heals"];
		foreach (string text2 in jsonnode2.Keys)
		{
			PlayerStat key2 = (PlayerStat)int.Parse(text2);
			this.Heals.Add(key2, jsonnode2.GetValueOrDefault(text2, 0));
		}
		JSONNode jsonnode3 = n["Counts"];
		foreach (string text3 in jsonnode3.Keys)
		{
			PlayerStat key3 = (PlayerStat)int.Parse(text3);
			this.Counts.Add(key3, jsonnode3.GetValueOrDefault(text3, 0));
		}
		JSONNode jsonnode4 = n["Maxes"];
		foreach (string text4 in jsonnode4.Keys)
		{
			PlayerStat key4 = (PlayerStat)int.Parse(text4);
			this.Maxes.Add(key4, jsonnode4.GetValueOrDefault(text4, 0));
		}
		JSONNode jsonnode5 = n["Dmgs"];
		foreach (string text5 in jsonnode5.Keys)
		{
			this.AugmentDmg.Add(text5, jsonnode5.GetValueOrDefault(text5, 0));
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0002FB84 File Offset: 0x0002DD84
	public JSONNode ToJSON()
	{
		JSONNode jsonnode = new JSONObject();
		JSONNode jsonnode2 = new JSONObject();
		foreach (KeyValuePair<PlayerStat, int> keyValuePair in this.Damages)
		{
			jsonnode2.Add(((int)keyValuePair.Key).ToString(), keyValuePair.Value);
		}
		jsonnode.Add("Damages", jsonnode2);
		JSONNode jsonnode3 = new JSONObject();
		foreach (KeyValuePair<PlayerStat, int> keyValuePair2 in this.Heals)
		{
			jsonnode3.Add(((int)keyValuePair2.Key).ToString(), keyValuePair2.Value);
		}
		jsonnode.Add("Heals", jsonnode3);
		JSONNode jsonnode4 = new JSONObject();
		foreach (KeyValuePair<PlayerStat, int> keyValuePair3 in this.Counts)
		{
			jsonnode4.Add(((int)keyValuePair3.Key).ToString(), keyValuePair3.Value);
		}
		jsonnode.Add("Counts", jsonnode4);
		JSONNode jsonnode5 = new JSONObject();
		foreach (KeyValuePair<PlayerStat, int> keyValuePair4 in this.Maxes)
		{
			jsonnode5.Add(((int)keyValuePair4.Key).ToString(), keyValuePair4.Value);
		}
		jsonnode.Add("Maxes", jsonnode5);
		JSONNode jsonnode6 = new JSONObject();
		foreach (KeyValuePair<string, int> keyValuePair5 in this.AugmentDmg)
		{
			jsonnode6.Add(keyValuePair5.Key, keyValuePair5.Value);
		}
		jsonnode.Add("Dmgs", jsonnode6);
		return jsonnode;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0002FDD0 File Offset: 0x0002DFD0
	public override string ToString()
	{
		return this.ToJSON().ToString();
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0002FDE0 File Offset: 0x0002DFE0
	public void DamageDone(DamageInfo info)
	{
		if (!GameplayManager.IsInGame)
		{
			return;
		}
		bool flag = false;
		switch (info.AbilityType)
		{
		case PlayerAbilityType.Primary:
			flag = true;
			this.IncreaseDamage(PlayerStat.Primary, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Secondary:
			flag = true;
			this.IncreaseDamage(PlayerStat.Secondary, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Utility:
			flag = true;
			this.IncreaseDamage(PlayerStat.Utility, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Movement:
			flag = true;
			this.IncreaseDamage(PlayerStat.Movement, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Ghost:
			flag = true;
			this.IncreaseDamage(PlayerStat.Ghost, (int)info.TotalAmount);
			break;
		}
		if (!flag && info.DamageType == DNumType.Greenling)
		{
			flag = true;
			this.IncreaseDamage(PlayerStat.Inkling, (int)info.TotalAmount);
		}
		else if (!flag && info.DamageType == DNumType.Orange)
		{
			flag = true;
			this.IncreaseDamage(PlayerStat.AtlasDmg, (int)info.TotalAmount);
		}
		else if (!flag && info.EffectSource == EffectSource.StatusEffect)
		{
			flag = true;
			this.IncreaseDamage(PlayerStat.Status, (int)info.TotalAmount);
		}
		if (!flag)
		{
			this.IncreaseDamage(PlayerStat.MiscDamage, (int)info.TotalAmount);
		}
		if (info.DamageType == DNumType.Crit)
		{
			this.IncreaseDamage(PlayerStat.Flourish, (int)info.TotalAmount);
			this.IncreaseCounts(PlayerStat.FlourishCount, 1);
		}
		else if (info.DamageType == DNumType.Default)
		{
			this.IncreaseCounts(PlayerStat.BaseDamageCount, 1);
		}
		EntityControl entity = EntityControl.GetEntity(info.AffectedID);
		if (entity != null)
		{
			AIControl aicontrol = entity as AIControl;
			if (aicontrol != null && aicontrol.Level.AnyFlagsMatch(EnemyLevel.Boss))
			{
				this.IncreaseDamage(PlayerStat.BossDamage, (int)info.TotalAmount);
			}
		}
		this.AugmentDamage(info.CauseID, (int)info.TotalAmount);
		this.TrySetMax(PlayerStat.TopDamage, (int)info.TotalAmount);
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0002FF70 File Offset: 0x0002E170
	public void AugmentDamage(string id, int value)
	{
		if (value <= 0)
		{
			return;
		}
		if (string.IsNullOrEmpty(id))
		{
			return;
		}
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		if (!this.AugmentDmg.ContainsKey(id))
		{
			this.AugmentDmg.Add(id, value);
			return;
		}
		Dictionary<string, int> augmentDmg = this.AugmentDmg;
		augmentDmg[id] += value;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0002FFC8 File Offset: 0x0002E1C8
	public int GetAugmentDamage(AugmentRootNode augment)
	{
		if (this.AugmentDmg.ContainsKey(augment.guid))
		{
			return this.AugmentDmg[augment.guid];
		}
		return 0;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
	public void HealingDone(DamageInfo info)
	{
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		switch (info.AbilityType)
		{
		case PlayerAbilityType.Primary:
			this.IncreaseHeal(PlayerStat.Primary, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Secondary:
			this.IncreaseHeal(PlayerStat.Secondary, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Utility:
			this.IncreaseHeal(PlayerStat.Utility, (int)info.TotalAmount);
			break;
		case PlayerAbilityType.Movement:
			this.IncreaseHeal(PlayerStat.Movement, (int)info.TotalAmount);
			break;
		}
		if (info.EffectSource == EffectSource.StatusEffect)
		{
			this.IncreaseHeal(PlayerStat.Status, (int)info.TotalAmount);
		}
		if (info.EffectSource == EffectSource.Pickup)
		{
			this.IncreaseHeal(PlayerStat.Pickup, (int)info.TotalAmount);
		}
		else if (info.AffectedID == info.SourceID)
		{
			this.IncreaseHeal(PlayerStat.SelfHeal, (int)info.TotalAmount);
		}
		else
		{
			this.IncreaseHeal(PlayerStat.OtherHeal, (int)info.TotalAmount);
		}
		this.TrySetMax(PlayerStat.TopHeal, (int)info.TotalAmount);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x000300D0 File Offset: 0x0002E2D0
	private void IncreaseDamage(PlayerStat stat, int amount)
	{
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		if (amount <= 0)
		{
			return;
		}
		if (!this.Damages.ContainsKey(stat))
		{
			this.Damages.Add(stat, 0);
		}
		Dictionary<PlayerStat, int> damages = this.Damages;
		damages[stat] += amount;
		GameStats.IncrementStat(stat, (uint)amount);
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x00030128 File Offset: 0x0002E328
	private void IncreaseHeal(PlayerStat stat, int amount)
	{
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		if (amount <= 0)
		{
			return;
		}
		if (!this.Heals.ContainsKey(stat))
		{
			this.Heals.Add(stat, 0);
		}
		Dictionary<PlayerStat, int> heals = this.Heals;
		heals[stat] += amount;
		GameStats.IncrementStat(stat, (uint)amount);
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00030180 File Offset: 0x0002E380
	public void IncreaseCounts(PlayerStat stat, int amount = 1)
	{
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		if (!this.Counts.ContainsKey(stat))
		{
			this.Counts.Add(stat, 0);
		}
		Dictionary<PlayerStat, int> counts = this.Counts;
		counts[stat] += amount;
		GameStats.IncrementStat(stat, (uint)amount);
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x000301D0 File Offset: 0x0002E3D0
	public void TrySetMax(PlayerStat stat, int val)
	{
		if (!GameplayManager.IsInGame)
		{
			return;
		}
		if (!this.Maxes.ContainsKey(stat))
		{
			this.Maxes.Add(stat, 0);
		}
		if (this.Maxes[stat] >= val)
		{
			return;
		}
		this.Maxes[stat] = val;
		GameStats.TryUpdateMax(stat, (uint)val);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00030224 File Offset: 0x0002E424
	public int TotalDamage()
	{
		int num = 0;
		foreach (KeyValuePair<PlayerStat, int> keyValuePair in this.Damages)
		{
			PlayerStat key = keyValuePair.Key;
			if (key != PlayerStat.BossDamage && key != PlayerStat.TopDamage)
			{
				num += keyValuePair.Value;
			}
		}
		return num;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x00030290 File Offset: 0x0002E490
	public int TotalHealing()
	{
		return 0 + this.GetHeal(PlayerStat.SelfHeal) + this.GetHeal(PlayerStat.OtherHeal) + this.GetHeal(PlayerStat.Pickup);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x000302AD File Offset: 0x0002E4AD
	public int GetDamage(PlayerStat stat)
	{
		if (stat == PlayerStat.TotalDamage)
		{
			return this.TotalDamage();
		}
		if (!this.Damages.ContainsKey(stat))
		{
			return 0;
		}
		return this.Damages[stat];
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000302D7 File Offset: 0x0002E4D7
	public int GetHeal(PlayerStat stat)
	{
		if (stat == PlayerStat.TotalDamage)
		{
			return this.TotalHealing();
		}
		if (!this.Heals.ContainsKey(stat))
		{
			return 0;
		}
		return this.Heals[stat];
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x00030301 File Offset: 0x0002E501
	public int GetMax(PlayerStat stat)
	{
		if (!this.Maxes.ContainsKey(stat))
		{
			return 0;
		}
		return this.Maxes[stat];
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0003031F File Offset: 0x0002E51F
	public int GetCount(PlayerStat stat)
	{
		if (!this.Counts.ContainsKey(stat))
		{
			return 0;
		}
		return this.Counts[stat];
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00030340 File Offset: 0x0002E540
	public int GetStat(PlayerStat stat)
	{
		switch (stat)
		{
		case PlayerStat.Status:
			return this.GetDamage(stat);
		case PlayerStat.Flourish:
			return this.GetDamage(stat);
		case PlayerStat.TopDamage:
			return this.GetMax(stat);
		case PlayerStat.TopHeal:
			return this.GetMax(stat);
		case PlayerStat.Kills:
			return this.GetCount(stat);
		case PlayerStat.Rebirths:
			return this.GetCount(stat);
		case PlayerStat.Revives:
			return this.GetCount(stat);
		case PlayerStat.SelfHeal:
			return this.GetHeal(stat);
		case PlayerStat.OtherHeal:
			return this.GetHeal(stat);
		case PlayerStat.Seconds_Invuln:
			return this.GetCount(stat);
		case PlayerStat.Mana_Provided:
			return this.GetCount(stat);
		case PlayerStat.TotalDamage:
			return this.TotalDamage();
		case PlayerStat.TotalHealing:
			return this.TotalHealing();
		case PlayerStat.Inkling:
			return this.GetDamage(stat);
		case PlayerStat.BossDamage:
			return this.GetDamage(stat);
		case PlayerStat.BaseDamageCount:
			return this.GetCount(stat);
		case PlayerStat.FlourishCount:
			return this.GetCount(stat);
		case PlayerStat.AtlasDmg:
			return this.GetDamage(stat);
		}
		return 0;
	}

	// Token: 0x04000550 RID: 1360
	public PlayerControl PlayerRef;

	// Token: 0x04000551 RID: 1361
	private Dictionary<PlayerStat, int> Damages = new Dictionary<PlayerStat, int>();

	// Token: 0x04000552 RID: 1362
	private Dictionary<PlayerStat, int> Heals = new Dictionary<PlayerStat, int>();

	// Token: 0x04000553 RID: 1363
	private Dictionary<PlayerStat, int> Counts = new Dictionary<PlayerStat, int>();

	// Token: 0x04000554 RID: 1364
	private Dictionary<PlayerStat, int> Maxes = new Dictionary<PlayerStat, int>();

	// Token: 0x04000555 RID: 1365
	private Dictionary<string, int> AugmentDmg = new Dictionary<string, int>();
}
