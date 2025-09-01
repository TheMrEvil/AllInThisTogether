using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class LibraryMPStarter : MonoBehaviourPunCallbacks
{
	// Token: 0x06000804 RID: 2052 RVA: 0x0003885C File Offset: 0x00036A5C
	private void Awake()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
		LibraryMPStarter.CurZones = new List<LibraryMPStartZone>();
		LibraryMPStarter.instance = this;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00038890 File Offset: 0x00036A90
	public bool AllZonesReady()
	{
		if (LibraryMPStarter.CurZones.Count <= 0)
		{
			return false;
		}
		foreach (LibraryMPStartZone libraryMPStartZone in LibraryMPStarter.CurZones)
		{
			if (!libraryMPStartZone.IsActive || !libraryMPStartZone.IsPlayerInside)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00038904 File Offset: 0x00036B04
	public void PrepareToGo()
	{
		this.isPreparing = true;
		this.ClearZones();
		Room currentRoom = PhotonNetwork.CurrentRoom;
		int num = (currentRoom != null) ? currentRoom.PlayerCount : 1;
		if (num <= 1)
		{
			return;
		}
		foreach (Transform transform in this.Spawns[num % this.Spawns.Count].Spawns)
		{
			this.CreateZone(transform.position);
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00038998 File Offset: 0x00036B98
	private void CreateZone(Vector3 pt)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ZoneRef);
		gameObject.transform.position = pt;
		gameObject.SetActive(true);
		LibraryMPStartZone component = gameObject.GetComponent<LibraryMPStartZone>();
		LibraryMPStarter.CurZones.Add(component);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x000389D4 File Offset: 0x00036BD4
	public void CancelPreparation()
	{
		this.isPreparing = false;
		this.ClearZones();
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x000389E4 File Offset: 0x00036BE4
	private void ClearZones()
	{
		foreach (LibraryMPStartZone libraryMPStartZone in LibraryMPStarter.CurZones)
		{
			libraryMPStartZone.Deactivate();
		}
		LibraryMPStarter.CurZones.Clear();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00038A40 File Offset: 0x00036C40
	private void OnGameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Hub_ReadyUp)
		{
			Room currentRoom = PhotonNetwork.CurrentRoom;
			if (((currentRoom != null) ? currentRoom.PlayerCount : 1) > 1)
			{
				AudioManager.PlaySFX2D(this.VoteStartSFX, 1f, 0.1f);
				LibraryInfoWidget.Notify("Ready?", GameplayManager.instance.GameGraph.Root.ShortName, "Go to The Font to begin!");
				InfoDisplay.SetText("Tome Activated", 2f, InfoArea.DetailBottom);
			}
			this.PrepareToGo();
			RaidManager.instance.CancelRaidPrep();
			return;
		}
		if (from == GameState.Hub_ReadyUp)
		{
			this.CancelPreparation();
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00038ACA File Offset: 0x00036CCA
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if (!this.isPreparing)
		{
			return;
		}
		this.PrepareToGo();
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00038ADB File Offset: 0x00036CDB
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		if (!this.isPreparing)
		{
			return;
		}
		this.PrepareToGo();
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00038AEC File Offset: 0x00036CEC
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00038B0E File Offset: 0x00036D0E
	public LibraryMPStarter()
	{
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00038B16 File Offset: 0x00036D16
	// Note: this type is marked as 'beforefieldinit'.
	static LibraryMPStarter()
	{
	}

	// Token: 0x040006C1 RID: 1729
	public static LibraryMPStarter instance;

	// Token: 0x040006C2 RID: 1730
	public GameObject ZoneRef;

	// Token: 0x040006C3 RID: 1731
	public List<LibraryMPStarter.ZoneGroup> Spawns;

	// Token: 0x040006C4 RID: 1732
	public AudioClip VoteStartSFX;

	// Token: 0x040006C5 RID: 1733
	private bool isPreparing;

	// Token: 0x040006C6 RID: 1734
	public static List<LibraryMPStartZone> CurZones = new List<LibraryMPStartZone>();

	// Token: 0x020004B5 RID: 1205
	[Serializable]
	public class ZoneGroup
	{
		// Token: 0x06002274 RID: 8820 RVA: 0x000C6DF3 File Offset: 0x000C4FF3
		public ZoneGroup()
		{
		}

		// Token: 0x04002418 RID: 9240
		public int PlayerCount;

		// Token: 0x04002419 RID: 9241
		public List<Transform> Spawns;
	}
}
