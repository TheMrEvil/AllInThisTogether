using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class LibraryRaces : MonoBehaviour
{
	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000D06 RID: 3334 RVA: 0x000530C6 File Offset: 0x000512C6
	public static bool IsPlayerRacing
	{
		get
		{
			return LibraryRaces.instance != null && LibraryRaces.instance.LocalPlayerRacing;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000D07 RID: 3335 RVA: 0x000530E1 File Offset: 0x000512E1
	private bool IsMultiplayer
	{
		get
		{
			return PlayerControl.PlayerCount > 1;
		}
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x000530EC File Offset: 0x000512EC
	private void Awake()
	{
		LibraryRaces.instance = this;
		NetworkManager.LeftRoom = (Action)Delegate.Combine(NetworkManager.LeftRoom, new Action(this.FullReset));
		LibraryManager libraryManager = UnityEngine.Object.FindObjectOfType<LibraryManager>();
		libraryManager.OnPlayerLoadoutChanged = (Action)Delegate.Combine(libraryManager.OnPlayerLoadoutChanged, new Action(this.OnPlayerLoadoutChanged));
		this.MPEntryRef.SetActive(false);
		this.LocalBestTimes = new Dictionary<string, float>();
		foreach (LibraryRaces.Race race in this.Races)
		{
			float bestRace = GameStats.GetBestRace(race.RaceID);
			if (bestRace > 0f)
			{
				this.LocalBestTimes[race.RaceID] = bestRace;
			}
		}
		this.OnPlayerLoadoutChanged();
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x000531C8 File Offset: 0x000513C8
	private void Update()
	{
		if (this.CurState == LibraryRaces.RaceState.None)
		{
			this.RaceStartGroup.alpha = 0f;
			this.PrepareCanvasGroup.alpha = 0f;
			this.RaceTimerGroup.UpdateOpacity(false, 4f, true);
			this.LBGroup.UpdateOpacity(true, 4f, true);
			if (this.IsMultiplayer)
			{
				this.mpDisplayT += Time.deltaTime;
				if (this.mpDisplayT > this.MPSwapTime)
				{
					this.mpShowLastRace = !this.mpShowLastRace;
					this.mpDisplayT = 0f;
				}
				this.MPBestGroup.UpdateOpacity(!this.mpShowLastRace, 6f, true);
				this.MPLastGroup.UpdateOpacity(this.mpShowLastRace, 6f, true);
			}
			return;
		}
		if (this.CurState == LibraryRaces.RaceState.Prepared)
		{
			this.UpdatePrepared();
			return;
		}
		if (this.CurState == LibraryRaces.RaceState.Racing)
		{
			this.UpdateRacing();
		}
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x000532B8 File Offset: 0x000514B8
	private void UpdatePrepared()
	{
		float num = Vector3.Distance(PlayerControl.myInstance.Display.CenterOfMass.position, this.ReadyZone.position);
		this.LocalPlayerInside = (num < this.ReadyDistance);
		int num2 = 0;
		using (List<PlayerControl>.Enumerator enumerator = PlayerControl.AllPlayers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Vector3.Distance(enumerator.Current.Display.CenterOfMass.position, this.ReadyZone.position) < this.ReadyDistance)
				{
					num2++;
				}
			}
		}
		if (this.LocalPlayerInside && !this.InsideFX.isPlaying)
		{
			this.InsideFX.Play();
		}
		else if (!this.LocalPlayerInside && this.InsideFX.isPlaying)
		{
			this.InsideFX.Stop();
		}
		this.PrepareTimer -= Time.deltaTime;
		this.PrepareCanvasGroup.UpdateOpacity(true, 4f, true);
		this.PrepTimerText.text = Mathf.CeilToInt(this.PrepareTimer).ToString();
		this.PrepReadyText.text = num2.ToString() + "/" + PlayerControl.AllPlayers.Count.ToString();
		if (PhotonNetwork.IsMasterClient && this.PrepareTimer < this.PrepTime - 0.5f && (this.PrepareTimer <= 0f || num2 >= PlayerControl.AllPlayers.Count))
		{
			if (num2 == 0)
			{
				this.NetMessage("CancelRace", 0f, "");
				return;
			}
			this.NetMessage("StartRace", 0f, "");
		}
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x00053478 File Offset: 0x00051678
	public void TryPrepareRace()
	{
		if (this.CurState != LibraryRaces.RaceState.None)
		{
			return;
		}
		this.NetMessage("TryPrepare", 0f, this.curAbility.ID);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0005349E File Offset: 0x0005169E
	private void TryPrepareMaster(string abilityID)
	{
		if (!PhotonNetwork.IsMasterClient || this.CurState != LibraryRaces.RaceState.None)
		{
			return;
		}
		this.NetMessage("Prepare", 0f, abilityID);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x000534C4 File Offset: 0x000516C4
	private void PrepareRace(string ID)
	{
		this.RacePrompt.Deactivate();
		this.LBGroup.alpha = 0f;
		this.curAbility = GraphDB.GetAbility(ID);
		foreach (LibraryRaces.Race race in this.Races)
		{
			race.WallIndicator.SetActive(race.Ability == this.curAbility);
		}
		this.CurrentRace = this.GetRace(this.curAbility);
		this.LoadResultsInfo(this.CurrentRace);
		if (this.CurrentRace == null)
		{
			return;
		}
		Debug.Log("Race Prepared - Clearing");
		this.PlayersRacing.Clear();
		if (!this.IsMultiplayer)
		{
			this.StartRace(true);
			return;
		}
		AudioManager.PlaySFX2D(this.PrepareSFX, 1f, 0.1f);
		LibraryInfoWidget.Notify("Get Ready!", "Race Prepared!", this.curAbility.Root.Name);
		this.PrepareTimer = this.PrepTime;
		this.ReadyZone.gameObject.SetActive(true);
		this.CurState = LibraryRaces.RaceState.Prepared;
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x000535F8 File Offset: 0x000517F8
	private void StartRace(bool isLocalRacing)
	{
		this.RaceTimerGroup.alpha = 0f;
		this.RaceStartGroup.alpha = 0f;
		this.PrepareCanvasGroup.alpha = 0f;
		this.WaitingGroup.alpha = 0f;
		this.LocalFinished = false;
		this.mpDisplayT = 0f;
		this.mpShowLastRace = true;
		float num = 4f;
		this.RaceTimer = -num;
		this.ReadyZone.gameObject.SetActive(false);
		this.LocalPlayerRacing = isLocalRacing;
		if (this.LocalPlayerRacing)
		{
			AudioManager.PlaySFX2D(this.RaceReadySFX, 1f, 0.1f);
			PlayerControl.ToggleMultiplayerCollision(false);
			PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Movement, this.curAbility.ID, false);
			int playerIndex = PlayerControl.GetPlayerIndex(PlayerControl.myInstance);
			Transform transform = this.StartPts[playerIndex];
			this.TimeSinceLastCheckpoint = -num;
			this.CurrentCheckpoint = 0;
			this.NetMessage("PlayerStarted", (float)PlayerControl.myInstance.view.OwnerActorNr, "");
			PlayerControl.myInstance.Movement.SetPositionWithCamera(transform.position, transform.forward, true, true);
			PlayerControl.myInstance.net.ApplyStatus(this.StartRaceStatus.HashCode, -1, num, 1, false, 0);
			this.NextCheckpoint();
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
			}, 1f);
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
			}, 2f);
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
			}, 3f);
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				AudioManager.PlaySFX2D(this.RaceStartSFX, 1f, 0.1f);
				this.RaceStartVFX.Play();
			}, 4f);
		}
		this.CurState = LibraryRaces.RaceState.Racing;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x000537C4 File Offset: 0x000519C4
	private void UpdateRacing()
	{
		if (this.LocalFinished)
		{
			this.WaitingGroup.UpdateOpacity(this.PlayersRacing.Count > 0, 3f, false);
		}
		this.RaceTimer += Time.deltaTime;
		if (!this.LocalPlayerRacing)
		{
			this.CheckAllEnded();
			return;
		}
		if (this.TimeSinceLastCheckpoint > 12f)
		{
			this.CancelParticipation();
		}
		this.TimeSinceLastCheckpoint += Time.deltaTime;
		this.PrepareCanvasGroup.alpha = 0f;
		this.RaceStartGroup.UpdateOpacity(this.RaceTimer < 0f, 2.5f, true);
		this.RaceTimerGroup.UpdateOpacity(true, 2.5f, true);
		if (this.RaceTimer < 0f)
		{
			this.RaceStartTimerText.text = Mathf.CeilToInt(-this.RaceTimer).ToString();
		}
		else
		{
			this.RaceStartTimerText.text = "GO!";
		}
		this.RaceTimerText.text = ((this.RaceTimer > 0f) ? LibraryRaces.GetTimerText(this.RaceTimer) : "00:00:00");
		float num = Vector3.Distance(PlayerControl.myInstance.Display.GetLocation(ActionLocation.CenterOfMass).position, this.CurCheckpointDisplay.transform.position);
		num = Mathf.Min(num, Vector3.Distance(PlayerControl.myInstance.Display.GetLocation(ActionLocation.Floor).position, this.CurCheckpointDisplay.transform.position));
		num = Mathf.Min(num, Vector3.Distance(PlayerControl.myInstance.Display.GetLocation(ActionLocation.Head).position, this.CurCheckpointDisplay.transform.position));
		this.DistFromCheckpoint = num;
		if (num < this.CheckpointDistance)
		{
			this.NextCheckpoint();
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0005398C File Offset: 0x00051B8C
	private void NextCheckpoint()
	{
		if (this.CurCheckpointDisplay != null)
		{
			this.CheckpointFX.transform.SetPositionAndRotation(this.CurCheckpointDisplay.transform.position, this.CurCheckpointDisplay.transform.rotation);
			this.FinishVFX.transform.SetPositionAndRotation(this.CurCheckpointDisplay.transform.position, this.CurCheckpointDisplay.transform.rotation);
			UnityEngine.Object.Destroy(this.CurCheckpointDisplay);
		}
		if (this.NextCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.NextCheckpointDisplay);
		}
		this.TimeSinceLastCheckpoint = 0f;
		if (this.CurrentCheckpoint >= this.CurrentRace.CheckPts.Count)
		{
			this.FinishRace();
			return;
		}
		if (this.CurrentCheckpoint > 0)
		{
			this.CheckpointFX.Play();
			AudioManager.PlaySFX2D(this.CheckpointSFX, 1f, 0.1f);
		}
		if (this.ShouldResetMoveCD)
		{
			PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Movement, false);
		}
		LibraryRaces.Checkpoint checkpoint = this.CurrentRace.CheckPts[this.CurrentCheckpoint];
		this.ShouldResetMoveCD = checkpoint.ResetMoveCD;
		bool flag = this.CurrentCheckpoint + 1 >= this.CurrentRace.CheckPts.Count;
		GameObject gameObject = checkpoint.ResetMoveCD ? this.CheckPointCDDisplayRef : this.CheckPointDisplayRef;
		this.CurCheckpointDisplay = UnityEngine.Object.Instantiate<GameObject>(flag ? this.FinishlineDisplayRef : gameObject, checkpoint.position.position, checkpoint.position.rotation);
		this.CurCheckpointDisplay.SetActive(true);
		if (!flag)
		{
			checkpoint = this.CurrentRace.CheckPts[this.CurrentCheckpoint + 1];
			gameObject = (checkpoint.ResetMoveCD ? this.NextCheckpointCDDisplayRef : this.NextCheckpointDisplayRef);
			this.NextCheckpointDisplay = UnityEngine.Object.Instantiate<GameObject>(gameObject, checkpoint.position.position, checkpoint.position.rotation);
			this.NextCheckpointDisplay.SetActive(true);
		}
		this.CurrentCheckpoint++;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x00053B98 File Offset: 0x00051D98
	private void FinishRace()
	{
		Debug.Log("Finished Race");
		this.FinishVFX.Play();
		AudioManager.PlayLoudSFX2D(this.FinishSFX, 1f, 0.1f);
		this.LocalPlayerRacing = false;
		this.LocalFinished = true;
		this.NetMessage("PlayerFinished", this.RaceTimer, PlayerControl.myInstance.view.OwnerActorNr.ToString());
		this.LocalLastTimes[this.CurrentRace.RaceID] = this.RaceTimer;
		PlayerControl.ToggleMultiplayerCollision(true);
		float num = 10000f;
		float num2;
		if (this.LocalBestTimes.TryGetValue(this.CurrentRace.RaceID, out num2))
		{
			num = num2;
		}
		if (this.RaceTimer < num)
		{
			this.LocalBestTimes[this.CurrentRace.RaceID] = this.RaceTimer;
			GameStats.SaveBestRace(this.CurrentRace.RaceID, this.RaceTimer);
		}
		if (this.CurCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.CurCheckpointDisplay);
		}
		if (this.NextCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.NextCheckpointDisplay);
		}
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x00053CB8 File Offset: 0x00051EB8
	private void RecordResult(int playerID, float timer)
	{
		if (this.IsMultiplayer)
		{
			PlayerControl player = PlayerControl.GetPlayer(playerID);
			if (!this.MPLastRaceTimes.ContainsKey(this.CurrentRace.RaceID))
			{
				this.MPLastRaceTimes[this.CurrentRace.RaceID] = new Dictionary<string, float>();
			}
			this.MPLastRaceTimes[this.CurrentRace.RaceID][player.GetUsernameText()] = timer;
			if (!this.MPSessionTimes.ContainsKey(this.CurrentRace.RaceID))
			{
				this.MPSessionTimes[this.CurrentRace.RaceID] = new Dictionary<string, float>();
			}
			float num = 10000f;
			if (this.MPSessionTimes[this.CurrentRace.RaceID].ContainsKey(player.GetUsernameText()))
			{
				num = this.MPSessionTimes[this.CurrentRace.RaceID][player.GetUsernameText()];
			}
			if (timer < num)
			{
				this.MPSessionTimes[this.CurrentRace.RaceID][player.GetUsernameText()] = timer;
			}
		}
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00053DD0 File Offset: 0x00051FD0
	private void CancelParticipation()
	{
		Debug.Log("Quit Race");
		this.LocalPlayerRacing = false;
		this.NetMessage("PlayerEnded", (float)PlayerControl.myInstance.view.OwnerActorNr, "");
		PlayerControl.ToggleMultiplayerCollision(true);
		if (this.CurCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.CurCheckpointDisplay);
		}
		if (this.NextCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.NextCheckpointDisplay);
		}
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x00053E48 File Offset: 0x00052048
	public void CheckAllEnded()
	{
		for (int i = this.PlayersRacing.Count - 1; i >= 0; i--)
		{
			if (PlayerControl.GetPlayer(this.PlayersRacing[i]) == null)
			{
				this.PlayersRacing.RemoveAt(i);
			}
		}
		if (this.PlayersRacing.Count > 0 || this.RaceTimer < 0f)
		{
			return;
		}
		if (PhotonNetwork.IsMasterClient)
		{
			this.NetMessage("RaceEnded", 0f, "");
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x00053ECA File Offset: 0x000520CA
	private void EndOfRace()
	{
		this.CurState = LibraryRaces.RaceState.Ended;
		base.Invoke("ReturnToBaseline", 3f);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x00053EE3 File Offset: 0x000520E3
	private void ReturnToBaseline()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			this.NetMessage("ReturnToBase", 0f, "");
		}
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x00053F01 File Offset: 0x00052101
	public void DisplayWaiting()
	{
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x00053F04 File Offset: 0x00052104
	private void LoadResultsInfo(LibraryRaces.Race race)
	{
		if (race == null || race.Ability == null)
		{
			return;
		}
		this.AbilityNameText.text = race.Ability.Root.Name;
		this.AuthorTimeText.text = "<sprite name=\"ribbon\">" + LibraryRaces.GetTimerText(race.AuthorTime);
		this.SoloResults.SetActive(!this.IsMultiplayer);
		this.MPResults.SetActive(this.IsMultiplayer);
		if (!this.IsMultiplayer)
		{
			if (this.LocalBestTimes.ContainsKey(race.RaceID))
			{
				float num = this.LocalBestTimes[race.RaceID];
				if (num > race.AuthorTime)
				{
					this.SoloBestAuthor.gameObject.SetActive(false);
					this.SoloBestText.text = LibraryRaces.GetTimerText(num);
				}
				else
				{
					this.SoloBestAuthor.gameObject.SetActive(true);
					this.SoloBestAuthor.text = LibraryRaces.GetTimerText(num);
					this.SoloBestText.text = "";
				}
			}
			else
			{
				this.SoloBestAuthor.gameObject.SetActive(false);
				this.SoloBestText.text = "--:--:--";
			}
			if (!this.LocalLastTimes.ContainsKey(race.RaceID))
			{
				this.SoloLastAuthor.gameObject.SetActive(false);
				this.SoloLastText.text = "--:--:--";
				return;
			}
			float num2 = this.LocalLastTimes[race.RaceID];
			if (num2 > race.AuthorTime)
			{
				this.SoloLastAuthor.gameObject.SetActive(false);
				this.SoloLastText.text = LibraryRaces.GetTimerText(num2);
				return;
			}
			this.SoloLastAuthor.gameObject.SetActive(true);
			this.SoloLastAuthor.text = LibraryRaces.GetTimerText(num2);
			this.SoloLastText.text = "";
			return;
		}
		else
		{
			foreach (GameObject obj in this.MPEntries)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.MPEntries.Clear();
			if (this.MPSessionTimes.ContainsKey(race.RaceID))
			{
				this.AddEntries(this.MPSessionTimes[race.RaceID], this.MPBestList);
			}
			else
			{
				this.AddEntries(new Dictionary<string, float>(), this.MPBestList);
			}
			if (this.MPLastRaceTimes.ContainsKey(race.RaceID))
			{
				this.AddEntries(this.MPLastRaceTimes[race.RaceID], this.MPLastList);
				return;
			}
			this.AddEntries(new Dictionary<string, float>(), this.MPLastList);
			return;
		}
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x000541B0 File Offset: 0x000523B0
	private void AddEntries(Dictionary<string, float> times, Transform list)
	{
		List<ValueTuple<string, float>> list2 = new List<ValueTuple<string, float>>();
		foreach (KeyValuePair<string, float> keyValuePair in times)
		{
			list2.Add(new ValueTuple<string, float>(keyValuePair.Key, keyValuePair.Value));
		}
		list2.Sort(([TupleElementNames(new string[]
		{
			"username",
			"time"
		})] ValueTuple<string, float> x, [TupleElementNames(new string[]
		{
			"username",
			"time"
		})] ValueTuple<string, float> y) => x.Item2.CompareTo(y.Item2));
		LibraryRaces.Race currentRace = this.CurrentRace;
		float authorTime = (currentRace != null) ? currentRace.AuthorTime : 0f;
		for (int i = 0; i < 4; i++)
		{
			string username = "";
			float time = 0f;
			if (list2.Count > i)
			{
				username = list2[i].Item1;
				time = list2[i].Item2;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.MPEntryRef, list);
			gameObject.SetActive(true);
			gameObject.GetComponent<LibraryRaceEntry>().Setup(username, time, authorTime);
			this.MPEntries.Add(gameObject);
		}
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x000542CC File Offset: 0x000524CC
	private void NetMessage(string type, float value = 0f, string data = "")
	{
		GoalManager.instance.LibraryRaceEvent(type, value, data);
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x000542DC File Offset: 0x000524DC
	public void GetNetMessage(string type, float value, string data)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
		if (num <= 1421533292U)
		{
			if (num <= 885315261U)
			{
				if (num != 732025783U)
				{
					if (num != 885315261U)
					{
						return;
					}
					if (!(type == "TryPrepare"))
					{
						return;
					}
					this.TryPrepareMaster(data);
					return;
				}
				else
				{
					if (!(type == "ReturnToBase"))
					{
						return;
					}
					this.ResetState(false);
					return;
				}
			}
			else if (num != 1203133958U)
			{
				if (num != 1421533292U)
				{
					return;
				}
				if (!(type == "PlayerEnded"))
				{
					return;
				}
				this.PlayersRacing.Remove((int)value);
				this.CheckAllEnded();
				return;
			}
			else
			{
				if (!(type == "CancelRace"))
				{
					return;
				}
				this.ResetState(false);
				return;
			}
		}
		else if (num <= 2501950084U)
		{
			if (num != 2146235598U)
			{
				if (num != 2501950084U)
				{
					return;
				}
				if (!(type == "RaceEnded"))
				{
					return;
				}
				this.EndOfRace();
				return;
			}
			else
			{
				if (!(type == "PlayerFinished"))
				{
					return;
				}
				int num2;
				int.TryParse(data, out num2);
				this.RecordResult(num2, value);
				this.PlayersRacing.Remove(num2);
				this.CheckAllEnded();
				return;
			}
		}
		else if (num != 2576692834U)
		{
			if (num != 3271354157U)
			{
				if (num != 4062363516U)
				{
					return;
				}
				if (!(type == "Prepare"))
				{
					return;
				}
				this.PrepareRace(data);
				return;
			}
			else
			{
				if (!(type == "PlayerStarted"))
				{
					return;
				}
				this.PlayersRacing.Add((int)value);
				return;
			}
		}
		else
		{
			if (!(type == "StartRace"))
			{
				return;
			}
			this.StartRace(this.LocalPlayerInside);
			return;
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x00054454 File Offset: 0x00052654
	private void OnPlayerLoadoutChanged()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.curAbility = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Movement).AbilityTree;
		foreach (LibraryRaces.Race race in this.Races)
		{
			race.WallIndicator.SetActive(race.Ability == this.curAbility);
		}
		this.LoadResultsInfo(this.GetRace(this.curAbility));
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x000544F8 File Offset: 0x000526F8
	private LibraryRaces.Race GetRace(AbilityTree ability)
	{
		foreach (LibraryRaces.Race race in this.Races)
		{
			if (race.Ability == ability)
			{
				return race;
			}
		}
		return null;
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0005455C File Offset: 0x0005275C
	public static string GetTimerText(float time)
	{
		int num = (int)(time * 1000f);
		int num2 = num / 60000;
		int num3 = num % 60000;
		int num4 = num3 / 1000;
		int num5 = (int)Mathf.Round((float)(num3 % 1000) / 10f);
		return string.Format("{0:00}:{1:00}:{2:00}", num2, num4, num5);
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x000545B7 File Offset: 0x000527B7
	private void FullReset()
	{
		this.ResetState(true);
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x000545C0 File Offset: 0x000527C0
	private void ResetState(bool includeLeaderboard)
	{
		this.LocalPlayerInside = false;
		this.LocalPlayerRacing = false;
		this.mpDisplayT = 0f;
		this.mpShowLastRace = true;
		this.ReadyZone.gameObject.SetActive(false);
		this.CurState = LibraryRaces.RaceState.None;
		this.RacePrompt.Activate();
		PlayerControl.ToggleMultiplayerCollision(true);
		this.LoadResultsInfo(this.CurrentRace);
		if (this.CurCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.CurCheckpointDisplay);
		}
		if (this.NextCheckpointDisplay != null)
		{
			UnityEngine.Object.Destroy(this.NextCheckpointDisplay);
		}
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x00054654 File Offset: 0x00052854
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		foreach (Transform transform in this.StartPts)
		{
			Gizmos.DrawSphere(transform.position, 0.5f);
		}
		Gizmos.color = Color.green;
		if (this.curAbility != null)
		{
			this.GetRace(this.curAbility);
		}
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x000546E0 File Offset: 0x000528E0
	private void OnDestroy()
	{
		NetworkManager.LeftRoom = (Action)Delegate.Remove(NetworkManager.LeftRoom, new Action(this.FullReset));
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00054704 File Offset: 0x00052904
	public LibraryRaces()
	{
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00054785 File Offset: 0x00052985
	[CompilerGenerated]
	private void <StartRace>b__80_0()
	{
		AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0005479C File Offset: 0x0005299C
	[CompilerGenerated]
	private void <StartRace>b__80_1()
	{
		AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x000547B3 File Offset: 0x000529B3
	[CompilerGenerated]
	private void <StartRace>b__80_2()
	{
		AudioManager.PlaySFX2D(this.RaceStartTickSFX, 1f, 0.1f);
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x000547CA File Offset: 0x000529CA
	[CompilerGenerated]
	private void <StartRace>b__80_3()
	{
		AudioManager.PlaySFX2D(this.RaceStartSFX, 1f, 0.1f);
		this.RaceStartVFX.Play();
	}

	// Token: 0x04000A68 RID: 2664
	public static LibraryRaces instance;

	// Token: 0x04000A69 RID: 2665
	public List<LibraryRaces.Race> Races;

	// Token: 0x04000A6A RID: 2666
	public SimpleDiagetic RacePrompt;

	// Token: 0x04000A6B RID: 2667
	public TextMeshProUGUI AbilityNameText;

	// Token: 0x04000A6C RID: 2668
	public TextMeshProUGUI AuthorTimeText;

	// Token: 0x04000A6D RID: 2669
	public CanvasGroup PrepareCanvasGroup;

	// Token: 0x04000A6E RID: 2670
	public TextMeshProUGUI PrepTimerText;

	// Token: 0x04000A6F RID: 2671
	public TextMeshProUGUI PrepReadyText;

	// Token: 0x04000A70 RID: 2672
	public AudioClip PrepareSFX;

	// Token: 0x04000A71 RID: 2673
	public float ReadyDistance = 5f;

	// Token: 0x04000A72 RID: 2674
	public float PrepTime = 15f;

	// Token: 0x04000A73 RID: 2675
	public Transform ReadyZone;

	// Token: 0x04000A74 RID: 2676
	public ParticleSystem InsideFX;

	// Token: 0x04000A75 RID: 2677
	public List<Transform> StartPts;

	// Token: 0x04000A76 RID: 2678
	public StatusTree StartRaceStatus;

	// Token: 0x04000A77 RID: 2679
	public AudioClip RaceReadySFX;

	// Token: 0x04000A78 RID: 2680
	public AudioClip RaceStartTickSFX;

	// Token: 0x04000A79 RID: 2681
	public AudioClip RaceStartSFX;

	// Token: 0x04000A7A RID: 2682
	public CanvasGroup RaceStartGroup;

	// Token: 0x04000A7B RID: 2683
	public CanvasGroup RaceTimerGroup;

	// Token: 0x04000A7C RID: 2684
	public CanvasGroup WaitingGroup;

	// Token: 0x04000A7D RID: 2685
	public TextMeshProUGUI RaceStartTimerText;

	// Token: 0x04000A7E RID: 2686
	public TextMeshProUGUI RaceTimerText;

	// Token: 0x04000A7F RID: 2687
	public ParticleSystem RaceStartVFX;

	// Token: 0x04000A80 RID: 2688
	public StatusTree CheckpointStatus;

	// Token: 0x04000A81 RID: 2689
	public float CheckpointDistance = 4.5f;

	// Token: 0x04000A82 RID: 2690
	public GameObject CheckPointDisplayRef;

	// Token: 0x04000A83 RID: 2691
	public GameObject CheckPointCDDisplayRef;

	// Token: 0x04000A84 RID: 2692
	public GameObject NextCheckpointDisplayRef;

	// Token: 0x04000A85 RID: 2693
	public GameObject NextCheckpointCDDisplayRef;

	// Token: 0x04000A86 RID: 2694
	public GameObject FinishlineDisplayRef;

	// Token: 0x04000A87 RID: 2695
	public ParticleSystem CheckpointFX;

	// Token: 0x04000A88 RID: 2696
	public AudioClip CheckpointSFX;

	// Token: 0x04000A89 RID: 2697
	public AudioClip FinishSFX;

	// Token: 0x04000A8A RID: 2698
	public ParticleSystem FinishVFX;

	// Token: 0x04000A8B RID: 2699
	public CanvasGroup LBGroup;

	// Token: 0x04000A8C RID: 2700
	public GameObject SoloResults;

	// Token: 0x04000A8D RID: 2701
	public TextMeshProUGUI SoloLastText;

	// Token: 0x04000A8E RID: 2702
	public TextMeshProUGUI SoloLastAuthor;

	// Token: 0x04000A8F RID: 2703
	public TextMeshProUGUI SoloBestText;

	// Token: 0x04000A90 RID: 2704
	public TextMeshProUGUI SoloBestAuthor;

	// Token: 0x04000A91 RID: 2705
	public float MPSwapTime = 5f;

	// Token: 0x04000A92 RID: 2706
	public GameObject MPResults;

	// Token: 0x04000A93 RID: 2707
	public CanvasGroup MPBestGroup;

	// Token: 0x04000A94 RID: 2708
	public Transform MPBestList;

	// Token: 0x04000A95 RID: 2709
	public CanvasGroup MPLastGroup;

	// Token: 0x04000A96 RID: 2710
	public Transform MPLastList;

	// Token: 0x04000A97 RID: 2711
	public GameObject MPEntryRef;

	// Token: 0x04000A98 RID: 2712
	private List<GameObject> MPEntries = new List<GameObject>();

	// Token: 0x04000A99 RID: 2713
	private LibraryRaces.Race CurrentRace;

	// Token: 0x04000A9A RID: 2714
	public AbilityTree curAbility;

	// Token: 0x04000A9B RID: 2715
	public LibraryRaces.RaceState CurState;

	// Token: 0x04000A9C RID: 2716
	public bool LocalPlayerInside;

	// Token: 0x04000A9D RID: 2717
	public bool LocalPlayerRacing;

	// Token: 0x04000A9E RID: 2718
	public float PrepareTimer;

	// Token: 0x04000A9F RID: 2719
	public float RaceTimer;

	// Token: 0x04000AA0 RID: 2720
	public List<int> PlayersRacing = new List<int>();

	// Token: 0x04000AA1 RID: 2721
	public bool LocalFinished;

	// Token: 0x04000AA2 RID: 2722
	public float TimeSinceLastCheckpoint;

	// Token: 0x04000AA3 RID: 2723
	public int CurrentCheckpoint;

	// Token: 0x04000AA4 RID: 2724
	public bool ShouldResetMoveCD;

	// Token: 0x04000AA5 RID: 2725
	public float DistFromCheckpoint;

	// Token: 0x04000AA6 RID: 2726
	private bool mpShowLastRace;

	// Token: 0x04000AA7 RID: 2727
	private float mpDisplayT;

	// Token: 0x04000AA8 RID: 2728
	private Dictionary<string, float> LocalBestTimes = new Dictionary<string, float>();

	// Token: 0x04000AA9 RID: 2729
	private Dictionary<string, Dictionary<string, float>> MPSessionTimes = new Dictionary<string, Dictionary<string, float>>();

	// Token: 0x04000AAA RID: 2730
	private Dictionary<string, float> LocalLastTimes = new Dictionary<string, float>();

	// Token: 0x04000AAB RID: 2731
	private Dictionary<string, Dictionary<string, float>> MPLastRaceTimes = new Dictionary<string, Dictionary<string, float>>();

	// Token: 0x04000AAC RID: 2732
	private GameObject CurCheckpointDisplay;

	// Token: 0x04000AAD RID: 2733
	private GameObject NextCheckpointDisplay;

	// Token: 0x0200051B RID: 1307
	[Serializable]
	public class Race
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x000CC130 File Offset: 0x000CA330
		private void AddPoint()
		{
			if (this.CheckPts.Count > 0)
			{
				List<LibraryRaces.Checkpoint> checkPts = this.CheckPts;
				int index = checkPts.Count - 1;
				Transform position = checkPts[index].position;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(position.gameObject, position.parent);
				gameObject.name = "P_" + (this.CheckPts.Count + 1).ToString();
				gameObject.transform.position += position.forward * 15f;
				LibraryRaces.Checkpoint item = new LibraryRaces.Checkpoint(gameObject.transform);
				this.CheckPts.Add(item);
				return;
			}
			Debug.LogError("No Checkpoints to add to - add one manually first");
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000CC1E7 File Offset: 0x000CA3E7
		public Race()
		{
		}

		// Token: 0x040025DD RID: 9693
		public string RaceID;

		// Token: 0x040025DE RID: 9694
		public AbilityTree Ability;

		// Token: 0x040025DF RID: 9695
		public float AuthorTime = 25f;

		// Token: 0x040025E0 RID: 9696
		public GameObject WallIndicator;

		// Token: 0x040025E1 RID: 9697
		public List<LibraryRaces.Checkpoint> CheckPts;
	}

	// Token: 0x0200051C RID: 1308
	[Serializable]
	public class Checkpoint
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x000CC1FA File Offset: 0x000CA3FA
		public Checkpoint(Transform p)
		{
			this.position = p;
		}

		// Token: 0x040025E2 RID: 9698
		public Transform position;

		// Token: 0x040025E3 RID: 9699
		public bool ResetMoveCD;
	}

	// Token: 0x0200051D RID: 1309
	public enum RaceState
	{
		// Token: 0x040025E5 RID: 9701
		None,
		// Token: 0x040025E6 RID: 9702
		Prepared,
		// Token: 0x040025E7 RID: 9703
		Racing,
		// Token: 0x040025E8 RID: 9704
		Ended
	}

	// Token: 0x0200051E RID: 1310
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060023D7 RID: 9175 RVA: 0x000CC209 File Offset: 0x000CA409
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000CC215 File Offset: 0x000CA415
		public <>c()
		{
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000CC21D File Offset: 0x000CA41D
		internal int <AddEntries>b__91_0([TupleElementNames(new string[]
		{
			"username",
			"time"
		})] ValueTuple<string, float> x, [TupleElementNames(new string[]
		{
			"username",
			"time"
		})] ValueTuple<string, float> y)
		{
			return x.Item2.CompareTo(y.Item2);
		}

		// Token: 0x040025E9 RID: 9705
		public static readonly LibraryRaces.<>c <>9 = new LibraryRaces.<>c();

		// Token: 0x040025EA RID: 9706
		[TupleElementNames(new string[]
		{
			"username",
			"time"
		})]
		public static Comparison<ValueTuple<string, float>> <>9__91_0;
	}
}
