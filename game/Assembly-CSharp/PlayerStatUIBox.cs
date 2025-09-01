using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
public class PlayerStatUIBox : MonoBehaviour
{
	// Token: 0x06000FF6 RID: 4086 RVA: 0x000646D0 File Offset: 0x000628D0
	public void Setup(PlayerControl player, PlayerGameStats stats)
	{
		this.Stats = stats;
		this.playerRef = player;
		this.Username.text = player.GetUsernameText();
		this.TotalDamage.text = PlayerStatUIBox.GetTextOutput("Total Damage", 70, stats.TotalDamage());
		this.PlayerIcon.sprite = GameDB.GetElement(player.actions.core.Root.magicColor).Icon;
		this.AbilityStats(player);
		this.SetupAugments(player);
		this.AddBaseStat(PlayerStat.Status, 1);
		Debug.Log("Top Damage for " + player.Username + ": " + stats.GetMax(PlayerStat.TopDamage).ToString());
		this.AddMaxStat(PlayerStat.TopDamage, 150);
		this.AddBaseStat(PlayerStat.SelfHeal, 10);
		this.AddBaseStat(PlayerStat.OtherHeal, 10);
		if (this.Stats.GetHeal(PlayerStat.OtherHeal) > 0)
		{
			this.AddMaxStat(PlayerStat.TopHeal, 5);
			this.AddStat("Total Healing", this.Stats.TotalHealing());
		}
		this.AddCountStat(PlayerStat.Kills, 0);
		this.AddCountStat(PlayerStat.Revives, 1);
		this.AddCountStat(PlayerStat.Rebirths, 1);
		this.AddCountStat(PlayerStat.Mana_Provided, 10);
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00064804 File Offset: 0x00062A04
	private void AbilityStats(PlayerControl player)
	{
		this.AddAbilityBox(player.actions.primary, player, this.Stats.GetDamage(PlayerStat.Primary));
		this.AddAbilityBox(player.actions.secondary, player, this.Stats.GetDamage(PlayerStat.Secondary));
		this.AddAbilityBox(player.actions.movement, player, this.Stats.GetDamage(PlayerStat.Movement));
		this.AddAbilityBox(player.actions.utility, player, this.Stats.GetDamage(PlayerStat.Utility));
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x00064889 File Offset: 0x00062A89
	private void AddAbilityBox(AbilityTree ability, PlayerControl player, int damage)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AbilityGroupRef, this.AbilityGroupRef.transform.parent);
		gameObject.SetActive(true);
		gameObject.GetComponent<PlayerStatAbilityUIGroup>().Setup(ability, player, damage);
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000648BC File Offset: 0x00062ABC
	private void AddBaseStat(PlayerStat stat, int MinValue)
	{
		int damage = this.Stats.GetDamage(stat);
		if (damage < MinValue)
		{
			return;
		}
		this.AddStat(stat, damage);
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x000648E4 File Offset: 0x00062AE4
	private void AddMaxStat(PlayerStat stat, int MinValue)
	{
		int max = this.Stats.GetMax(stat);
		if (max < MinValue)
		{
			return;
		}
		this.AddStat(stat, max);
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0006490C File Offset: 0x00062B0C
	private void AddCountStat(PlayerStat stat, int MinValue)
	{
		int count = this.Stats.GetCount(stat);
		if (count < MinValue)
		{
			return;
		}
		this.AddStat(stat, count);
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00064933 File Offset: 0x00062B33
	private void AddStat(PlayerStat stat, int value)
	{
		this.AddStat(PostGame_Stats_Display.GetTitle(stat), value);
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00064942 File Offset: 0x00062B42
	private void AddStat(string title, int value)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StatTextRef, this.StatList);
		gameObject.SetActive(true);
		gameObject.GetComponent<TextMeshProUGUI>().text = PlayerStatUIBox.GetTextOutput(title, 70, value);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0006496F File Offset: 0x00062B6F
	public static string GetTextOutput(string infoName, int spacing, int amount)
	{
		return string.Concat(new string[]
		{
			infoName,
			":<pos=",
			spacing.ToString(),
			"%>",
			string.Format("{0:n0}", amount)
		});
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x000649AD File Offset: 0x00062BAD
	public static string GetTextOutput(string infoName, int amount)
	{
		return infoName + ": " + string.Format("{0:n0}", amount);
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000649CC File Offset: 0x00062BCC
	private void SetupAugments(PlayerControl player)
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in player.Augment.trees)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.boxRef, this.boxRef.transform.parent);
			gameObject.SetActive(true);
			AugmentInfoBox component = gameObject.GetComponent<AugmentInfoBox>();
			component.Setup(keyValuePair.Key, keyValuePair.Value, ModType.Player, player, TextAnchor.UpperCenter, component.transform.position);
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x00064A68 File Offset: 0x00062C68
	public PlayerStatUIBox()
	{
	}

	// Token: 0x04000E0F RID: 3599
	public TextMeshProUGUI Username;

	// Token: 0x04000E10 RID: 3600
	public TextMeshProUGUI TotalDamage;

	// Token: 0x04000E11 RID: 3601
	public Image PlayerIcon;

	// Token: 0x04000E12 RID: 3602
	[Header("Ability Boxes")]
	public GameObject AbilityGroupRef;

	// Token: 0x04000E13 RID: 3603
	[Header("Stat Lists")]
	public GameObject StatTextRef;

	// Token: 0x04000E14 RID: 3604
	public Transform StatList;

	// Token: 0x04000E15 RID: 3605
	[Header("Augments")]
	public GameObject boxRef;

	// Token: 0x04000E16 RID: 3606
	private PlayerGameStats Stats;

	// Token: 0x04000E17 RID: 3607
	[NonSerialized]
	public PlayerControl playerRef;
}
