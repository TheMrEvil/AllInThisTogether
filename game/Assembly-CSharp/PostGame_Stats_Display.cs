using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class PostGame_Stats_Display : MonoBehaviour
{
	// Token: 0x06001018 RID: 4120 RVA: 0x00065030 File Offset: 0x00063230
	public void Clear()
	{
		foreach (PostGame_StatEntry postGame_StatEntry in this.StatList)
		{
			UnityEngine.Object.Destroy(postGame_StatEntry.gameObject);
		}
		this.StatList.Clear();
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x00065090 File Offset: 0x00063290
	private void AddPlayerBox(string label, int value, bool wasTop = false)
	{
		this.AddStatBox(label, value, wasTop, this.PlayerList);
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000650A1 File Offset: 0x000632A1
	private void AddGlobalBox(string label, int value, bool wasTop = false)
	{
		this.AddStatBox(label, value, wasTop, this.GlobalList);
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x000650B4 File Offset: 0x000632B4
	private void AddStatBox(string label, int value, bool wasTop, Transform holder)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StatEntryRef, holder);
		gameObject.SetActive(true);
		PostGame_StatEntry component = gameObject.GetComponent<PostGame_StatEntry>();
		component.SetupBasic(label, value, wasTop);
		this.StatList.Add(component);
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000650F0 File Offset: 0x000632F0
	public void LoadPlayerStats(PlayerGameStats stats, List<PlayerGameStats> allStats)
	{
		this.Clear();
		PostGame_Stats_Display.allPlrStats = allStats;
		this.curStats = stats;
		this.TryAddStat(PlayerStat.TotalDamage, 0);
		this.TryAddStat(PlayerStat.BossDamage, 100);
		this.TryAddStat(PlayerStat.Status, 10);
		this.TryAddStat(PlayerStat.TopDamage, 150);
		this.TryAddStat(PlayerStat.Inkling, 50);
		this.TryAddStat(PlayerStat.AtlasDmg, 50);
		this.AddUniques();
		this.TryAddStat(PlayerStat.SelfHeal, 10);
		this.TryAddStat(PlayerStat.OtherHeal, 10);
		if (stats.GetHeal(PlayerStat.OtherHeal) > 0)
		{
			this.TryAddStat(PlayerStat.TopHeal, 5);
			this.TryAddStat(PlayerStat.TotalHealing, 5);
		}
		this.TryAddStat(PlayerStat.Kills, 0);
		this.TryAddStat(PlayerStat.Revives, 1);
		this.TryAddStat(PlayerStat.Rebirths, 1);
		this.TryAddStat(PlayerStat.Mana_Provided, 5);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000651A8 File Offset: 0x000633A8
	private void AddUniques()
	{
		if (this.curStats.GetStat(PlayerStat.FlourishCount) > 10)
		{
			float num = (float)this.curStats.GetStat(PlayerStat.FlourishCount);
			int stat = this.curStats.GetStat(PlayerStat.BaseDamageCount);
			float num2 = num / (num + (float)stat) * 100f;
			this.AddPlayerStat("Flourish %", (int)num2, false);
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x000651FC File Offset: 0x000633FC
	private void TryAddStat(PlayerStat stat, int MinValue = 0)
	{
		int stat2 = this.curStats.GetStat(stat);
		if (stat2 < MinValue)
		{
			return;
		}
		this.AddStat(stat, stat2);
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x00065224 File Offset: 0x00063424
	private void AddStat(PlayerStat stat, int value)
	{
		bool wasTop = this.IsTopStat(stat, value);
		this.AddPlayerStat(PostGame_Stats_Display.GetTitle(stat), value, wasTop);
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x00065248 File Offset: 0x00063448
	private void AddPlayerStat(string title, int value, bool wasTop)
	{
		this.AddPlayerBox(title, value, wasTop);
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x00065254 File Offset: 0x00063454
	public static string GetTitle(PlayerStat stat)
	{
		switch (stat)
		{
		case PlayerStat.Primary:
			return "Generator Damage";
		case PlayerStat.Secondary:
			return "Spender Damage";
		case PlayerStat.Utility:
			return "Signature Damage";
		case PlayerStat.Movement:
			return "Move Spell Damage";
		case PlayerStat.Ghost:
			return "Ghost Damage";
		case PlayerStat.Status:
			return "Status Damage";
		case PlayerStat.Flourish:
			return "Flourish Damage";
		case PlayerStat.TopDamage:
			return "Largest Hit";
		case PlayerStat.TopHeal:
			return "Top Heal";
		case PlayerStat.SelfHeal:
			return "Healing (Self)";
		case PlayerStat.OtherHeal:
			return "Healing (Others)";
		case PlayerStat.Mana_Provided:
			return "Temp Mana Gen";
		case PlayerStat.TotalDamage:
			return "Total Damage";
		case PlayerStat.TotalHealing:
			return "Total Healing";
		case PlayerStat.Inkling:
			return "Inkling Damage";
		case PlayerStat.BossDamage:
			return "Boss Damage";
		case PlayerStat.AtlasDmg:
			return "Atlas Damage";
		}
		return stat.ToString();
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x00065340 File Offset: 0x00063540
	private bool IsTopStat(PlayerStat stat, int value)
	{
		if (PostGame_Stats_Display.allPlrStats.Count <= 1)
		{
			return false;
		}
		using (List<PlayerGameStats>.Enumerator enumerator = PostGame_Stats_Display.allPlrStats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetStat(stat) > value)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x000653AC File Offset: 0x000635AC
	public PostGame_Stats_Display()
	{
	}

	// Token: 0x04000E34 RID: 3636
	public Transform PlayerList;

	// Token: 0x04000E35 RID: 3637
	public Transform GlobalList;

	// Token: 0x04000E36 RID: 3638
	public GameObject StatEntryRef;

	// Token: 0x04000E37 RID: 3639
	private List<PostGame_StatEntry> StatList = new List<PostGame_StatEntry>();

	// Token: 0x04000E38 RID: 3640
	private static List<PlayerGameStats> allPlrStats;

	// Token: 0x04000E39 RID: 3641
	private PlayerGameStats curStats;
}
