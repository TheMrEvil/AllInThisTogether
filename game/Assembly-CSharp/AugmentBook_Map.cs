using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F4 RID: 500
public class AugmentBook_Map : MonoBehaviour
{
	// Token: 0x06001546 RID: 5446 RVA: 0x0008573C File Offset: 0x0008393C
	public void Setup()
	{
		if (RaidManager.IsInRaid)
		{
			RaidDB.Raid raid = RaidDB.GetRaid(RaidManager.instance.CurrentRaid);
			if (raid == null)
			{
				return;
			}
			this.TomeTitle.text = raid.RaidName;
			int currentEncounterIndex = RaidManager.instance.CurrentEncounterIndex;
			this.SetupRaid(raid, currentEncounterIndex);
			return;
		}
		else
		{
			GameplayManager instance = GameplayManager.instance;
			GenreRootNode genreRootNode;
			if (instance == null)
			{
				genreRootNode = null;
			}
			else
			{
				GenreTree gameGraph = instance.GameGraph;
				genreRootNode = ((gameGraph != null) ? gameGraph.Root : null);
			}
			GenreRootNode genreRootNode2 = genreRootNode;
			if (genreRootNode2 == null)
			{
				return;
			}
			this.TomeTitle.text = (GameplayManager.IsChallengeActive ? GameplayManager.Challenge.Name : genreRootNode2.ShortName);
			bool flag = WaveManager.instance.AppendixLevel > 0 | (GameplayManager.IsChallengeActive && GameplayManager.Challenge.TornPages.Count > 0);
			this.FirstBookDisplay.SetActive(true);
			this.RaidTypeDisplay.gameObject.SetActive(false);
			if (!flag)
			{
				this.SetupGameMap(genreRootNode2);
				return;
			}
			this.SetupAppendix(genreRootNode2);
			return;
		}
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x00085830 File Offset: 0x00083A30
	private void SetupGameMap(GenreRootNode root)
	{
		this.AppendixGroup.SetActive(false);
		this.MapGroup.SetActive(true);
		int num = Mathf.Min(root.Waves.Count - 1, this.MapItems.Count);
		for (int i = 0; i < num; i++)
		{
			this.MapItems[i].gameObject.SetActive(true);
			this.MapItems[i].Setup(root.GetWave(i + 1, 0), AIManager.GetEnemyAugment(i), i);
		}
		for (int j = num; j < this.MapItems.Count; j++)
		{
			this.MapItems[j].gameObject.SetActive(false);
		}
		int wavesCompleted = WaveManager.instance.WavesCompleted;
		this.ChapterLabel.text = "Chapter " + (wavesCompleted + 1).ToString();
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x00085914 File Offset: 0x00083B14
	private void SetupAppendix(GenreRootNode root)
	{
		this.AppendixGroup.SetActive(true);
		this.MapGroup.SetActive(false);
		this.AppendHeader.SetActive(WaveManager.instance.AppendixLevel > 0);
		this.AppendTitle.text = "Appendix " + WaveManager.instance.AppendixLevel.ToString();
		this.ClearList();
		foreach (string guid in AIManager.AugmentIDs)
		{
			AugmentTree augment = GraphDB.GetAugment(guid);
			if (!(augment == null))
			{
				AugmentBookBarItem component = UnityEngine.Object.Instantiate<GameObject>(this.UpgradeListRef, this.AppendList).GetComponent<AugmentBookBarItem>();
				component.Setup(augment, null);
				this.upgradeList.Add(component);
			}
		}
		UISelector.SetupVerticalListNav<AugmentBookBarItem>(this.upgradeList, null, null, false);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x00085A08 File Offset: 0x00083C08
	private void SetupRaid(RaidDB.Raid raid, int curIndex)
	{
		this.AppendixGroup.SetActive(false);
		this.MapGroup.SetActive(true);
		this.FirstBookDisplay.SetActive(false);
		this.RaidTypeDisplay.gameObject.SetActive(true);
		this.RaidTypeDisplay.sprite = (RaidManager.IsHardMode ? raid.StampHard : raid.StampNormal);
		RaidDB.Encounter encounter = raid.Encounters[Mathf.Min(curIndex, raid.Encounters.Count - 1)];
		List<RaidDB.Encounter> list = new List<RaidDB.Encounter>();
		for (int i = 0; i < raid.Encounters.Count; i++)
		{
			RaidDB.Encounter encounter2 = raid.Encounters[i];
			if (encounter2.Type != RaidDB.EncounterType.Vignette)
			{
				list.Add(encounter2);
			}
			if (encounter == encounter2)
			{
				curIndex = i;
				if (encounter2.Type == RaidDB.EncounterType.Vignette)
				{
					curIndex--;
					encounter = raid.Encounters[Mathf.Clamp(curIndex, 0, raid.Encounters.Count - 1)];
				}
			}
		}
		this.ChapterLabel.text = encounter.Name;
		int num = Mathf.Min(list.Count, this.MapItems.Count);
		for (int j = 0; j < num; j++)
		{
			this.MapItems[j].gameObject.SetActive(true);
			this.MapItems[j].SetupAsRaid(list[j], j, j < curIndex + 1, null);
		}
		for (int k = num; k < this.MapItems.Count; k++)
		{
			this.MapItems[k].gameObject.SetActive(false);
		}
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00085BA4 File Offset: 0x00083DA4
	private void ClearList()
	{
		foreach (AugmentBookBarItem augmentBookBarItem in this.upgradeList)
		{
			UnityEngine.Object.Destroy(augmentBookBarItem.gameObject);
		}
		this.upgradeList.Clear();
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x00085C04 File Offset: 0x00083E04
	public void SelectDefaultUI()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (WaveManager.instance.AppendixLevel <= 0)
		{
			UISelector.SelectSelectable(this.DefaultButton);
			return;
		}
		if (this.upgradeList.Count > 0)
		{
			UISelector.SelectSelectable(this.upgradeList[0].GetComponent<Selectable>());
		}
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x00085C56 File Offset: 0x00083E56
	public void TickUpdate()
	{
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x00085C58 File Offset: 0x00083E58
	public AugmentBook_Map()
	{
	}

	// Token: 0x040014C3 RID: 5315
	public CanvasGroup Group;

	// Token: 0x040014C4 RID: 5316
	public GameObject MapGroup;

	// Token: 0x040014C5 RID: 5317
	public TextMeshProUGUI TomeTitle;

	// Token: 0x040014C6 RID: 5318
	public TextMeshProUGUI ChapterLabel;

	// Token: 0x040014C7 RID: 5319
	public List<AugmentBook_Map_Item> MapItems;

	// Token: 0x040014C8 RID: 5320
	public Button DefaultButton;

	// Token: 0x040014C9 RID: 5321
	public GameObject AppendixGroup;

	// Token: 0x040014CA RID: 5322
	public GameObject AppendHeader;

	// Token: 0x040014CB RID: 5323
	public TextMeshProUGUI AppendTitle;

	// Token: 0x040014CC RID: 5324
	public Transform AppendList;

	// Token: 0x040014CD RID: 5325
	public GameObject UpgradeListRef;

	// Token: 0x040014CE RID: 5326
	public AutoScrollRect Scroller;

	// Token: 0x040014CF RID: 5327
	private List<AugmentBookBarItem> upgradeList = new List<AugmentBookBarItem>();

	// Token: 0x040014D0 RID: 5328
	public GameObject FirstBookDisplay;

	// Token: 0x040014D1 RID: 5329
	public Image RaidTypeDisplay;
}
