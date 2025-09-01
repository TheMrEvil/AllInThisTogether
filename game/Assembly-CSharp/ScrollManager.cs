using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class ScrollManager : MonoBehaviour
{
	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x0003F011 File Offset: 0x0003D211
	private InputActions actions
	{
		get
		{
			return InputManager.Actions;
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0003F018 File Offset: 0x0003D218
	private void Awake()
	{
		this.CurOptions = new List<AugmentTree>();
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0003F028 File Offset: 0x0003D228
	public List<AugmentTree> GetEnemyOptions()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		if (WaveManager.instance.AppendixLevel > 0)
		{
			Debug.Log(string.Format("Generating Enemy Rewards for Appendix {0} - Chapter {1}", WaveManager.instance.AppendixLevel, WaveManager.instance.AppendixChapterNumber));
		}
		if (WaveManager.instance.AppendixLevel > 0 && WaveManager.instance.AppendixChapterNumber == 0)
		{
			list = this.GenerateBindingsForAppendix(WaveManager.instance.AppendixLevel);
		}
		else
		{
			GenreRewardNode genreRewardNode = RewardManager.instance.RewardConfig();
			if (genreRewardNode != null)
			{
				list = genreRewardNode.GetEnemyMods();
			}
		}
		if (list.Count < 1)
		{
			list.Add(this.FallbackEnemyPick);
		}
		return list;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003F0D4 File Offset: 0x0003D2D4
	public void LoadEnemyScrolls(List<AugmentTree> mods)
	{
		this.CurOptions = mods;
		this.ScrollDisplay.Activate();
		AudioManager.PlaySFX2D(this.EnemyScrollSpawned.GetRandomClip(-1), 1f, 0.1f);
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003F103 File Offset: 0x0003D303
	public void LoadChoiceUI()
	{
		EnemySelectionPanel.LoadEnemyScrolls(this.CurOptions);
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0003F110 File Offset: 0x0003D310
	private List<AugmentTree> GenerateBindingsForAppendix(int appendixLevel)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		list.Add(WaveDB.GetAppendixBinding(appendixLevel));
		Augments genreBindings = GameplayManager.instance.GenreBindings;
		List<AugmentTree> list2 = new List<AugmentTree>();
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if ((!(bindingUnlock.Parent == null) || bindingUnlock.UnlockedBy == Unlockable.UnlockType.Default) && (!(bindingUnlock.Parent != null) || genreBindings.TreeIDs.Contains(bindingUnlock.Parent.ID)) && !GameplayManager.instance.GenreBindings.TreeIDs.Contains(bindingUnlock.Binding.ID))
			{
				list2.Add(bindingUnlock.Binding);
			}
		}
		int num = Mathf.Min(2, list2.Count);
		for (int i = 0; i < num; i++)
		{
			int index = GameplayManager.IsChallengeActive ? MapManager.GetRandom(0, list2.Count) : UnityEngine.Random.Range(0, list2.Count);
			Debug.Log("Selected " + list2[index].Root.Name);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0003F270 File Offset: 0x0003D470
	public void ApplyEnemyScroll(int ScrollID, int votes)
	{
		if (ScrollID == -1)
		{
			return;
		}
		AugmentTree augmentTree = null;
		if (ScrollID >= 0 && ScrollID < this.CurOptions.Count)
		{
			augmentTree = this.CurOptions[ScrollID];
		}
		this.ScrollDisplay.ConfirmAndDeactivate();
		EnemySelectionPanel.instance.AugmentChosen(ScrollID);
		GameRecord.EnemyUpgradeChosen(augmentTree, this.CurOptions, votes);
		if (RaidManager.IsInRaid)
		{
			RaidManager.OnEnemyPageSelected(augmentTree);
		}
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (augmentTree != null)
		{
			if (augmentTree.Root.modType == ModType.Binding)
			{
				GameplayManager.instance.AddBinding(augmentTree);
				return;
			}
			if (augmentTree.Root.ApplyPlayerTeam)
			{
				GameplayManager.instance.PlayerTeamMods.Add(augmentTree, 1);
			}
			AIManager.AugmentIDs.Add(augmentTree.ID);
			AIManager.instance.AddAugment(augmentTree);
		}
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0003F33C File Offset: 0x0003D53C
	public ScrollManager()
	{
	}

	// Token: 0x040007BF RID: 1983
	public ScrollTrigger ScrollDisplay;

	// Token: 0x040007C0 RID: 1984
	public AugmentTree FallbackEnemyPick;

	// Token: 0x040007C1 RID: 1985
	public List<AudioClip> EnemyScrollSpawned;

	// Token: 0x040007C2 RID: 1986
	public List<AudioClip> EnemyScrollChosen;

	// Token: 0x040007C3 RID: 1987
	private List<AugmentTree> CurOptions;
}
