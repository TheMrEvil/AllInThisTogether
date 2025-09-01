using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class Library_TornCracks : MonoBehaviour
{
	// Token: 0x06000D8F RID: 3471 RVA: 0x00056B27 File Offset: 0x00054D27
	private void Awake()
	{
		Library_TornCracks.instance = this;
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Combine(NetworkManager.OnRoomPlayersChanged, new Action(this.SyncLevel));
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x00056B4F File Offset: 0x00054D4F
	private void Start()
	{
		LibraryManager libraryManager = LibraryManager.instance;
		libraryManager.OnLibraryEntered = (Action)Delegate.Combine(libraryManager.OnLibraryEntered, new Action(this.SyncLevel));
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x00056B78 File Offset: 0x00054D78
	private void Update()
	{
		if (this.currentCrackLevel == 0 && PlayerControl.myInstance != null)
		{
			bool shouldShow = Vector3.Distance(this.RivalsFadeGroup.transform.position, PlayerControl.myInstance.Movement.GetPosition()) < 15f;
			this.RivalsFadeGroup.UpdateOpacity(shouldShow, 2f, true);
		}
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00056BD8 File Offset: 0x00054DD8
	public static bool IsAntechamberAvailable()
	{
		return !(Library_TornCracks.instance == null) && Library_TornCracks.instance.currentCrackLevel > 0;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x00056BF6 File Offset: 0x00054DF6
	private void SyncLevel()
	{
		if (!PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.LibraryCrackLevel(Library_TornCracks.GetCompletedIndex());
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x00056C18 File Offset: 0x00054E18
	public void SetLevel(int level)
	{
		for (int i = 0; i < this.CrackLevels.Count; i++)
		{
			this.CrackLevels[i].SetActive(i == level);
		}
		this.EntryTrigger.SetActive(level > 0);
		this.currentCrackLevel = level;
		if (this.currentCrackLevel > 0 && this.AntechamberLorePage != null && !UnlockManager.HasSeenLorePage(this.AntechamberLorePage.UID))
		{
			this.AntechamberLorePage.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x00056CA0 File Offset: 0x00054EA0
	public static int GetCompletedIndex()
	{
		int result = 0;
		if (Library_TornCracks.instance == null)
		{
			return result;
		}
		for (int i = 1; i < Library_TornCracks.instance.CrackLevels.Count; i++)
		{
			if (GameStats.GetTomeStat(Library_TornCracks.instance.CrackLevels[i].Tome, GameStats.Stat.TomesWon, 0) > 0)
			{
				result = i;
			}
		}
		return result;
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00056CFA File Offset: 0x00054EFA
	private void OnDestroy()
	{
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Remove(NetworkManager.OnRoomPlayersChanged, new Action(this.SyncLevel));
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00056D1C File Offset: 0x00054F1C
	public Library_TornCracks()
	{
	}

	// Token: 0x04000B25 RID: 2853
	public static Library_TornCracks instance;

	// Token: 0x04000B26 RID: 2854
	public List<Library_TornCracks.CrackLevel> CrackLevels = new List<Library_TornCracks.CrackLevel>();

	// Token: 0x04000B27 RID: 2855
	public GameObject EntryTrigger;

	// Token: 0x04000B28 RID: 2856
	public LorePage AntechamberLorePage;

	// Token: 0x04000B29 RID: 2857
	public CanvasGroup RivalsFadeGroup;

	// Token: 0x04000B2A RID: 2858
	private int currentCrackLevel;

	// Token: 0x0200052C RID: 1324
	[Serializable]
	public class CrackLevel
	{
		// Token: 0x06002400 RID: 9216 RVA: 0x000CCA30 File Offset: 0x000CAC30
		public void SetActive(bool isActive)
		{
			foreach (GameObject gameObject in this.CrackObjects)
			{
				gameObject.SetActive(isActive);
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000CCA84 File Offset: 0x000CAC84
		public CrackLevel()
		{
		}

		// Token: 0x0400262E RID: 9774
		public List<GameObject> CrackObjects;

		// Token: 0x0400262F RID: 9775
		public GenreTree Tome;
	}
}
