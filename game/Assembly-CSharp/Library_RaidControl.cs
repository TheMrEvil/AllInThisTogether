using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using VolumetricLights;

// Token: 0x0200011D RID: 285
public class Library_RaidControl : MonoBehaviourPunCallbacks
{
	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000559F2 File Offset: 0x00053BF2
	public static bool IsPreparing
	{
		get
		{
			Library_RaidControl library_RaidControl = Library_RaidControl.instance;
			return library_RaidControl != null && library_RaidControl.isPreparing;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00055A04 File Offset: 0x00053C04
	public static bool IsInAntechamber
	{
		get
		{
			return Library_RaidControl.instance != null && Library_RaidControl.instance.isPlayerInAntechamber;
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x00055A20 File Offset: 0x00053C20
	private void Awake()
	{
		Library_RaidControl.instance = this;
		this.isPlayerInAntechamber = false;
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
		this.LibVolumeLight.customRange += 0.01f;
		this.AntechamberHolder.SetActive(false);
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00055A80 File Offset: 0x00053C80
	private void Update()
	{
		if (!this.isPlayerInAntechamber)
		{
			bool flag = this.isPreparing;
			using (List<EntityIndicator>.Enumerator enumerator = this.InsideIndicators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (WorldIndicators.IsIndicating(enumerator.Current))
					{
						flag = true;
					}
				}
			}
			foreach (LorePage lorePage in this.InsideLorePages)
			{
				if (lorePage.IsActive && lorePage.Indicator.gameObject.activeSelf)
				{
					flag = true;
				}
			}
			if (!flag && this.AntechamberIndicator.activeSelf)
			{
				this.AntechamberIndicator.SetActive(false);
				return;
			}
			if (flag && !this.AntechamberIndicator.activeSelf)
			{
				this.AntechamberIndicator.SetActive(true);
				return;
			}
		}
		else
		{
			if (this.AntechamberIndicator.activeSelf)
			{
				this.AntechamberIndicator.SetActive(false);
			}
			foreach (Library_RaidControl.AntechamberRaidInfo antechamberRaidInfo in this.RaidInfo)
			{
				bool flag2 = this.IsRaidUnlocked(antechamberRaidInfo.RaidType);
				bool flag3 = !flag2;
				if (!flag2)
				{
					float num = Vector3.Distance(PlayerControl.myInstance.movement.GetPosition(), antechamberRaidInfo.StartDiagetic.transform.position);
					flag3 &= (num < 20f);
				}
				antechamberRaidInfo.UnlockTextGroup.UpdateOpacity(flag3, 2f, true);
			}
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00055C34 File Offset: 0x00053E34
	public void GoToAntechamber()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null)
		{
			return;
		}
		PageFlip.instance.DoFlipInstant();
		this.AntechamberHolder.SetActive(true);
		this.LibraryHolder.SetActive(false);
		myInstance.Movement.SetPositionWithCamera(this.AntechamberSpawn.position, this.AntechamberSpawn.forward, true, true);
		this.isPlayerInAntechamber = true;
		this.AnteVolumeLight.customRange += UnityEngine.Random.Range(-0.05f, 0.05f);
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && !aicontrol.IsDead && aicontrol.PetOwnerID == myInstance.ViewID)
			{
				Vector3 point = GoalManager.FixPointOnNav(this.AntechamberSpawn.position + UnityEngine.Random.insideUnitSphere * 6f);
				aicontrol.Movement.SetPositionImmediate(point, aicontrol.Movement.GetForward(), true);
			}
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x00055D58 File Offset: 0x00053F58
	public void ReturnToLibrary()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null)
		{
			return;
		}
		PageFlip.instance.DoFlipInstant();
		this.AntechamberHolder.SetActive(false);
		this.LibraryHolder.SetActive(true);
		myInstance.Movement.SetPositionWithCamera(this.ReturnSpawn.position, this.ReturnSpawn.forward, true, true);
		this.isPlayerInAntechamber = false;
		this.LibVolumeLight.customRange += UnityEngine.Random.Range(-0.05f, 0.05f);
		if (this.isPreparing && PlayerControl.PlayerCount <= 1)
		{
			this.CancelPreparation();
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && !aicontrol.IsDead && aicontrol.PetOwnerID == myInstance.ViewID)
			{
				Vector3 point = GoalManager.FixPointOnNav(this.ReturnSpawn.position + UnityEngine.Random.insideUnitSphere * 6f);
				aicontrol.Movement.SetPositionImmediate(point, aicontrol.Movement.GetForward(), true);
			}
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00055E94 File Offset: 0x00054094
	public void SpawnInAntechamber()
	{
		this.AntechamberHolder.SetActive(true);
		this.LibraryHolder.SetActive(false);
		int b = PlayerControl.AllPlayers.IndexOf(PlayerControl.myInstance);
		Transform transform = this.AnteSpawns[Mathf.Min(this.AnteSpawns.Count - 1, b)];
		PlayerControl.myInstance.Movement.SetPositionWithCamera(transform.position, transform.forward, true, true);
		this.isPlayerInAntechamber = true;
		this.AnteVolumeLight.customRange += UnityEngine.Random.Range(-0.05f, 0.05f);
		base.Invoke("AntechamberSpawneDelayed", 0.5f);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00055F3D File Offset: 0x0005413D
	private void AntechamberSpawneDelayed()
	{
		Action onMapChangeFinished = MapManager.OnMapChangeFinished;
		if (onMapChangeFinished == null)
		{
			return;
		}
		onMapChangeFinished();
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00055F4E File Offset: 0x0005414E
	public void Reset()
	{
		this.AntechamberHolder.SetActive(false);
		this.LibraryHolder.SetActive(true);
		this.isPlayerInAntechamber = false;
		if (this.isPreparing && PlayerControl.PlayerCount <= 1)
		{
			this.CancelPreparation();
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00055F85 File Offset: 0x00054185
	public void StartMyriadRaid()
	{
		RaidManager.instance.TryPrepareRaid(RaidDB.RaidType.Myriad);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00055F92 File Offset: 0x00054192
	public void StartVerseRaid()
	{
		RaidManager.instance.TryPrepareRaid(RaidDB.RaidType.Verse);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00055F9F File Offset: 0x0005419F
	public void StartHorizonRaid()
	{
		RaidManager.instance.TryPrepareRaid(RaidDB.RaidType.Horizon);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00055FAC File Offset: 0x000541AC
	public bool AllZonesReady()
	{
		if (this.CurZones.Count <= 0)
		{
			return false;
		}
		foreach (LibraryMPStartZone libraryMPStartZone in this.CurZones)
		{
			if (!libraryMPStartZone.IsActive || !libraryMPStartZone.IsPlayerInside)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00056020 File Offset: 0x00054220
	public bool CanDoHardMode(RaidDB.RaidType raid)
	{
		bool result;
		switch (raid)
		{
		case RaidDB.RaidType.Myriad:
			result = this.my_hardAllowed;
			break;
		case RaidDB.RaidType.Verse:
			result = this.ve_hardAllowed;
			break;
		case RaidDB.RaidType.Horizon:
			result = this.ho_hardAllowed;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00056060 File Offset: 0x00054260
	public void RaidZonePrepare()
	{
		if (!this.isPreparing)
		{
			Room currentRoom = PhotonNetwork.CurrentRoom;
			if (((currentRoom != null) ? currentRoom.PlayerCount : 1) > 1)
			{
				AudioManager.PlaySFX2D(this.VoteStartSFX, 1f, 0.1f);
				LibraryInfoWidget.Notify("Ready?", RaidDB.GetRaid(RaidManager.instance.CurrentRaid).RaidName, "Go to Antechamber to begin!");
				InfoDisplay.SetText("Raid Prepared", 2f, InfoArea.DetailBottom);
			}
		}
		this.isPreparing = true;
		this.ClearZones();
		Room currentRoom2 = PhotonNetwork.CurrentRoom;
		int num = (currentRoom2 != null) ? currentRoom2.PlayerCount : 1;
		if (num <= 1)
		{
			return;
		}
		Library_RaidControl.AntechamberRaidInfo raidInfo = this.GetRaidInfo(RaidManager.RaidType);
		foreach (Transform transform in raidInfo.Spawns[num % raidInfo.Spawns.Count].Spawns)
		{
			this.CreateZone(transform.position);
		}
		foreach (Library_RaidControl.AntechamberRaidInfo antechamberRaidInfo in this.RaidInfo)
		{
			antechamberRaidInfo.StartDiagetic.gameObject.SetActive(antechamberRaidInfo != raidInfo && this.IsRaidUnlocked(antechamberRaidInfo.RaidType));
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x000561C8 File Offset: 0x000543C8
	private void CreateZone(Vector3 pt)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ZoneRef);
		gameObject.transform.position = pt;
		gameObject.SetActive(true);
		LibraryMPStartZone component = gameObject.GetComponent<LibraryMPStartZone>();
		this.CurZones.Add(component);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00056208 File Offset: 0x00054408
	public void CancelPreparation()
	{
		this.isPreparing = false;
		this.ClearZones();
		foreach (Library_RaidControl.AntechamberRaidInfo antechamberRaidInfo in this.RaidInfo)
		{
			antechamberRaidInfo.StartDiagetic.gameObject.SetActive(this.IsRaidUnlocked(antechamberRaidInfo.RaidType) && !VoteManager.IsVoting);
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0005628C File Offset: 0x0005448C
	private void ClearZones()
	{
		foreach (LibraryMPStartZone libraryMPStartZone in this.CurZones)
		{
			libraryMPStartZone.Deactivate();
		}
		this.CurZones.Clear();
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x000562E8 File Offset: 0x000544E8
	private void OnGameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Hub_Preparing)
		{
			this.CancelPreparation();
		}
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x000562F4 File Offset: 0x000544F4
	public void UpdateRaidUnlocked(bool myriad, bool verse, bool horizon, bool mHM, bool vHM, bool hHM)
	{
		this.myriadUnlocked = (this.myriadUnlocked || myriad);
		this.horizonUnlocked = (this.horizonUnlocked || horizon);
		this.verseUnlocked = (this.verseUnlocked || verse);
		this.my_hardAllowed = (this.my_hardAllowed || mHM);
		this.ve_hardAllowed = (this.ve_hardAllowed || vHM);
		this.ho_hardAllowed = (this.ho_hardAllowed || hHM);
		foreach (Library_RaidControl.AntechamberRaidInfo antechamberRaidInfo in this.RaidInfo)
		{
			bool flag = this.IsRaidUnlocked(antechamberRaidInfo.RaidType);
			antechamberRaidInfo.Door_LockedDisplay.SetActive(!flag);
			antechamberRaidInfo.Door_UnlockedDisplay.SetActive(flag);
			antechamberRaidInfo.StartDiagetic.gameObject.SetActive(flag && !this.isPreparing);
			antechamberRaidInfo.UnlockText.SetText(RaidDB.GetRaidUnlockText(antechamberRaidInfo.RaidType), true);
			if (flag && antechamberRaidInfo.LorePage != null && !UnlockManager.HasSeenLorePage(antechamberRaidInfo.LorePage.UID) && Library_TornCracks.IsAntechamberAvailable())
			{
				antechamberRaidInfo.LorePage.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00056438 File Offset: 0x00054638
	public override void OnJoinedRoom()
	{
		this.CheckRaidAccess();
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00056440 File Offset: 0x00054640
	public void CheckRaidAccess()
	{
		this.myriadUnlocked = RaidDB.GetRaid(RaidDB.RaidType.Myriad).IsUnlocked();
		this.verseUnlocked = RaidDB.GetRaid(RaidDB.RaidType.Verse).IsUnlocked();
		this.horizonUnlocked = RaidDB.GetRaid(RaidDB.RaidType.Horizon).IsUnlocked();
		this.my_hardAllowed = (GameStats.GetRaidStat("myriad_boss", GameStats.RaidStat.Completed, 0) + GameStats.GetRaidStat("myriad_boss", GameStats.RaidStat.HardMode_Completed, 0) > 0);
		this.ve_hardAllowed = (GameStats.GetRaidStat("verse_boss", GameStats.RaidStat.Completed, 0) + GameStats.GetRaidStat("verse_boss", GameStats.RaidStat.HardMode_Completed, 0) > 0);
		this.ho_hardAllowed = (GameStats.GetRaidStat("horizon_boss", GameStats.RaidStat.Completed, 0) + GameStats.GetRaidStat("horizon_boss", GameStats.RaidStat.HardMode_Completed, 0) > 0);
		base.Invoke("OpenDoorsDelayed", 0.5f);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x000564F6 File Offset: 0x000546F6
	private void OpenDoorsDelayed()
	{
		MapManager.instance.TryOpenRaidDoors(this.myriadUnlocked, this.verseUnlocked, this.horizonUnlocked, this.my_hardAllowed, this.ve_hardAllowed, this.ho_hardAllowed);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00056528 File Offset: 0x00054728
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.TryOpenRaidDoors(this.myriadUnlocked, this.verseUnlocked, this.horizonUnlocked, this.my_hardAllowed, this.ve_hardAllowed, this.ho_hardAllowed);
		}
		if (!this.isPreparing)
		{
			return;
		}
		this.RaidZonePrepare();
		if (PhotonNetwork.IsMasterClient)
		{
			RaidManager.instance.PrepareReadyUpForPlayer(newPlayer);
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0005658B File Offset: 0x0005478B
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		if (!this.isPreparing)
		{
			return;
		}
		this.RaidZonePrepare();
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0005659C File Offset: 0x0005479C
	public override void OnLeftRoom()
	{
		this.isPreparing = false;
		this.isPlayerInAntechamber = false;
		this.AntechamberHolder.SetActive(false);
		this.LibraryHolder.SetActive(true);
		this.ClearZones();
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x000565CA File Offset: 0x000547CA
	private void DebugPrepTravel()
	{
		this.PrepareTraveling(RaidDB.RaidType.Myriad);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x000565D4 File Offset: 0x000547D4
	public void PrepareTraveling(RaidDB.RaidType raidType)
	{
		Library_RaidControl.AntechamberRaidInfo raidInfo = this.GetRaidInfo(raidType);
		if (raidInfo == null)
		{
			return;
		}
		float num;
		switch (raidType)
		{
		case RaidDB.RaidType.Myriad:
			num = 0.15f;
			break;
		case RaidDB.RaidType.Verse:
			num = 0.02f;
			break;
		case RaidDB.RaidType.Horizon:
			num = 1f;
			break;
		default:
			throw new SwitchExpressionException(raidType);
		}
		float bwsketch = num;
		PostFXManager.instance.SetBWSketch(bwsketch);
		Fountain.instance.UpdateFountainLoc(raidInfo.Origin);
		AudioManager.PlayLoudSFX2D(this.RaidStartSFX, 1f, 0.1f);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00056658 File Offset: 0x00054858
	private Library_RaidControl.AntechamberRaidInfo GetRaidInfo(RaidDB.RaidType raidType)
	{
		foreach (Library_RaidControl.AntechamberRaidInfo antechamberRaidInfo in this.RaidInfo)
		{
			if (antechamberRaidInfo.RaidType == raidType)
			{
				return antechamberRaidInfo;
			}
		}
		return null;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000566B4 File Offset: 0x000548B4
	private bool IsRaidUnlocked(RaidDB.RaidType raidType)
	{
		bool result;
		switch (raidType)
		{
		case RaidDB.RaidType.Myriad:
			result = this.myriadUnlocked;
			break;
		case RaidDB.RaidType.Verse:
			result = this.verseUnlocked;
			break;
		case RaidDB.RaidType.Horizon:
			result = this.horizonUnlocked;
			break;
		default:
			throw new SwitchExpressionException(raidType);
		}
		return result;
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x000566FD File Offset: 0x000548FD
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0005671F File Offset: 0x0005491F
	public Library_RaidControl()
	{
	}

	// Token: 0x04000AFC RID: 2812
	public static Library_RaidControl instance;

	// Token: 0x04000AFD RID: 2813
	public GameObject AntechamberHolder;

	// Token: 0x04000AFE RID: 2814
	public Transform AntechamberSpawn;

	// Token: 0x04000AFF RID: 2815
	public GameObject ZoneRef;

	// Token: 0x04000B00 RID: 2816
	public AudioClip VoteStartSFX;

	// Token: 0x04000B01 RID: 2817
	public GameObject AntechamberIndicator;

	// Token: 0x04000B02 RID: 2818
	public List<EntityIndicator> InsideIndicators;

	// Token: 0x04000B03 RID: 2819
	public VolumetricLight AnteVolumeLight;

	// Token: 0x04000B04 RID: 2820
	public AudioClip RaidStartSFX;

	// Token: 0x04000B05 RID: 2821
	public List<Transform> AnteSpawns;

	// Token: 0x04000B06 RID: 2822
	public GameObject LibraryHolder;

	// Token: 0x04000B07 RID: 2823
	public Transform ReturnSpawn;

	// Token: 0x04000B08 RID: 2824
	public GameObject ReturnIndicator;

	// Token: 0x04000B09 RID: 2825
	public VolumetricLight LibVolumeLight;

	// Token: 0x04000B0A RID: 2826
	public List<LorePage> InsideLorePages;

	// Token: 0x04000B0B RID: 2827
	public List<Library_RaidControl.AntechamberRaidInfo> RaidInfo;

	// Token: 0x04000B0C RID: 2828
	private bool isPreparing;

	// Token: 0x04000B0D RID: 2829
	private bool isPlayerInAntechamber;

	// Token: 0x04000B0E RID: 2830
	private List<LibraryMPStartZone> CurZones = new List<LibraryMPStartZone>();

	// Token: 0x04000B0F RID: 2831
	private bool myriadUnlocked;

	// Token: 0x04000B10 RID: 2832
	private bool verseUnlocked;

	// Token: 0x04000B11 RID: 2833
	private bool horizonUnlocked;

	// Token: 0x04000B12 RID: 2834
	private bool my_hardAllowed;

	// Token: 0x04000B13 RID: 2835
	private bool ve_hardAllowed;

	// Token: 0x04000B14 RID: 2836
	private bool ho_hardAllowed;

	// Token: 0x02000529 RID: 1321
	[Serializable]
	public class AntechamberRaidInfo
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x000CC8D5 File Offset: 0x000CAAD5
		public AntechamberRaidInfo()
		{
		}

		// Token: 0x0400261A RID: 9754
		public RaidDB.RaidType RaidType;

		// Token: 0x0400261B RID: 9755
		public Transform Origin;

		// Token: 0x0400261C RID: 9756
		public SimpleDiagetic StartDiagetic;

		// Token: 0x0400261D RID: 9757
		public GameObject Door_LockedDisplay;

		// Token: 0x0400261E RID: 9758
		public GameObject Door_UnlockedDisplay;

		// Token: 0x0400261F RID: 9759
		public CanvasGroup UnlockTextGroup;

		// Token: 0x04002620 RID: 9760
		public TextMeshProUGUI UnlockText;

		// Token: 0x04002621 RID: 9761
		public LorePage LorePage;

		// Token: 0x04002622 RID: 9762
		public List<LibraryMPStarter.ZoneGroup> Spawns;
	}
}
