using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class WaveManager : MonoBehaviour, IPunObservable
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00043942 File Offset: 0x00041B42
	// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00043949 File Offset: 0x00041B49
	public static GenreWaveNode WaveConfig
	{
		[CompilerGenerated]
		get
		{
			return WaveManager.<WaveConfig>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			WaveManager.<WaveConfig>k__BackingField = value;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00043951 File Offset: 0x00041B51
	private GameState CurrentState
	{
		get
		{
			return GameplayManager.CurState;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00043958 File Offset: 0x00041B58
	// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0004396A File Offset: 0x00041B6A
	public static int CurrentWave
	{
		get
		{
			WaveManager waveManager = WaveManager.instance;
			if (waveManager == null)
			{
				return 0;
			}
			return waveManager._currentWave;
		}
		set
		{
			if (WaveManager.instance != null)
			{
				WaveManager.instance._currentWave = value;
			}
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00043984 File Offset: 0x00041B84
	public static GenreWaveNode NextWaveInfo
	{
		get
		{
			if (GameplayManager.CurrentTome == null)
			{
				return null;
			}
			return GameplayManager.CurrentTome.Root.GetWave(WaveManager.CurrentWave + 1, WaveManager.instance.AppendixLevel);
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000439B5 File Offset: 0x00041BB5
	public static bool GoalComplete
	{
		get
		{
			return !(WaveManager.WaveConfig == null) && !(WaveManager.instance == null) && WaveManager.WaveConfig.IsFinished();
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000439E0 File Offset: 0x00041BE0
	public static float GoalProportion
	{
		get
		{
			if (WaveManager.WaveConfig == null || WaveManager.instance == null)
			{
				return 0f;
			}
			if (WaveManager.WaveConfig.ProgressRequired() > 0 && WaveManager.WaveConfig.chapterType == GenreWaveNode.ChapterType.PointTotal)
			{
				return Mathf.Clamp(WaveManager.instance.GoalProgress / (float)WaveManager.WaveConfig.ProgressRequired(), 0f, 1f);
			}
			if (WaveManager.WaveConfig.chapterType == GenreWaveNode.ChapterType.Boss)
			{
				return 1f;
			}
			return 0f;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00043A64 File Offset: 0x00041C64
	public static float GoalCompletionProportion
	{
		get
		{
			if (WaveManager.WaveConfig == null || WaveManager.instance == null)
			{
				return 0f;
			}
			if (WaveManager.WaveConfig.ProgressRequired() > 0 && WaveManager.WaveConfig.chapterType == GenreWaveNode.ChapterType.PointTotal)
			{
				return Mathf.Clamp(WaveManager.instance.GoalCompletion / (float)WaveManager.WaveConfig.ProgressRequired(), 0f, 1f);
			}
			GenreWaveNode.ChapterType chapterType = WaveManager.WaveConfig.chapterType;
			return 0f;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00043AE4 File Offset: 0x00041CE4
	public int AppendixChapterNumber
	{
		get
		{
			if (this.AppendixLevel == 0)
			{
				return 0;
			}
			if (GameplayManager.CurTomeRoot == null)
			{
				return 0;
			}
			int num = WaveManager.CurrentWave + 1;
			num -= GameplayManager.CurTomeRoot.Waves.Count;
			num -= this.AppendixLevel;
			for (int i = 1; i < this.AppendixLevel; i++)
			{
				num -= GameplayManager.CurTomeRoot.Appendix.Count;
			}
			return num;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00043B51 File Offset: 0x00041D51
	public static float WaveElapsedTime
	{
		get
		{
			return Mathf.Max(0f, GameplayManager.instance.GameTime - WaveManager.WaveStartTime);
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x00043B6D File Offset: 0x00041D6D
	private void Awake()
	{
		WaveManager.instance = this;
		WaveManager.WaveConfig = null;
		this.view = base.GetComponent<PhotonView>();
		this.Reset();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00043BB0 File Offset: 0x00041DB0
	private void OnGameStateChanged(GameState from, GameState to)
	{
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		if (to == GameState.Reward_Start && WaveManager.CurrentWave >= 0 && !TutorialManager.InTutorial && !WaveManager.NewStart)
		{
			Debug.Log(string.Format("WaveManager Chapter {0} Completed | Appendix ", WaveManager.CurrentWave) + this.AppendixLevel.ToString());
			if (curTomeRoot.FinishedGame(WaveManager.CurrentWave, this.AppendixLevel) || curTomeRoot.FinishedGame(WaveManager.CurrentWave + 1, this.AppendixLevel))
			{
				UnityMainThreadDispatcher.Instance().Invoke(delegate
				{
					if (this.AppendixLevel <= 0)
					{
						EndGameAnim.Display(true);
						UnityMainThreadDispatcher.Instance().Invoke(new Action(PostFXManager.instance.DoPulse), 0.65f);
						return;
					}
					InfoDisplay.SetText("Appendix " + this.AppendixLevel.ToString() + " Restored!", 3f, InfoArea.Title);
					PostFXManager.instance.DoPulse();
				}, 1.5f);
				return;
			}
			InfoDisplay.SetText("Chapter Complete", 2f, InfoArea.Title);
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00043C63 File Offset: 0x00041E63
	public void Reset()
	{
		WaveManager.CurrentWave = -1;
		this.GoalProgress = 0f;
		this.GoalCompletion = 0f;
		this.WavesCompleted = 0;
		this.AppendixLevel = 0;
		this.ClearWaveConfig();
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x00043C95 File Offset: 0x00041E95
	private void Update()
	{
		if (GameplayManager.instance == null)
		{
			return;
		}
		if (RaidManager.IsInRaid)
		{
			return;
		}
		this.UpdateAll();
		if (PhotonNetwork.IsMasterClient)
		{
			this.UpdateMaster();
		}
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00043CC0 File Offset: 0x00041EC0
	private void UpdateAll()
	{
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00043CC4 File Offset: 0x00041EC4
	private void UpdateMaster()
	{
		if (this.CurrentState == GameState.PostRewards && GameplayManager.instance.Timer <= 0f)
		{
			this.NextWave();
		}
		if (this.CurrentState != GameState.InWave)
		{
			return;
		}
		if (WaveManager.WaveConfig.IsFinished() && WaveManager.WaveConfig.ShoudlEndWave())
		{
			AIManager.KillAllEnemies();
			this.EndWave();
		}
		if (PlayerControl.PlayerCount > 1 && PlayerControl.DeadPlayersCount >= PlayerControl.PlayerCount)
		{
			bool flag = false;
			using (List<PlayerControl>.Enumerator enumerator = PlayerControl.AllPlayers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Health.GroupRezTimer > 0f)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				GameplayManager.instance.EndGame(false);
			}
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00043D94 File Offset: 0x00041F94
	[PunRPC]
	private void WaveStartedNetwork(int wave)
	{
		WaveManager.WaveStartTime = GameplayManager.instance.GameTime;
		bool isMasterClient = PhotonNetwork.IsMasterClient;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00043DAC File Offset: 0x00041FAC
	public void EndWave()
	{
		Debug.Log("Wave Completed");
		AIManager.instance.EndWave();
		this.view.RPC("WaveEndedNetwork", RpcTarget.All, new object[]
		{
			WaveManager.CurrentWave
		});
		GameplayManager.instance.UpdateGameState(GameState.Reward_Start);
		this.GoalProgress = 0f;
		this.GoalCompletion = 0f;
		RewardManager.instance.ReadyPlayers.Clear();
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.WaveEnded);
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		if (((curTomeRoot != null) ? curTomeRoot.GetReward(WaveManager.CurrentWave, this.AppendixLevel) : null) == null)
		{
			this.NextWave();
			return;
		}
		UnityMainThreadDispatcher.Instance().Invoke(new Action(RewardManager.instance.NextReward), 1f);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x00043E78 File Offset: 0x00042078
	[PunRPC]
	private void WaveEndedNetwork(int wave)
	{
		WaveManager.NewStart = false;
		GameRecord.NextChapter();
		GoalManager.instance.CancelBonusObjective();
		GoalManager.Reset(false);
		InkManager.instance.AddWaveInk(false);
		GameplayManager.instance.ClearWorldEffects();
		GameStats.IncrementStat(GameplayManager.CurrentTome, PlayerControl.myInstance.SignatureColor, GameStats.Stat.ChaptersComplete, 1, true);
		GameStats.IncrementStat(PlayerControl.myInstance.SignatureColor, GameStats.SignatureStat.TimePlayed, (uint)WaveManager.WaveElapsedTime, false);
		GameStats.SaveIfNeeded();
		if (!GameplayManager.CurTomeRoot.FinishedGame(WaveManager.CurrentWave, this.AppendixLevel) && !GameplayManager.CurTomeRoot.FinishedGame(WaveManager.CurrentWave + 1, this.AppendixLevel))
		{
			Fountain.instance.ChapterCompletePulse();
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00043F24 File Offset: 0x00042124
	public void NextWave()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		WaveManager.CurrentWave++;
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		if (curTomeRoot != null && curTomeRoot.FinishedGame(WaveManager.CurrentWave, this.AppendixLevel))
		{
			PlayerControl.myInstance.TryRespawn();
			GameplayManager.instance.GameWon();
			return;
		}
		this.WavesCompleted++;
		this.SetWaveConfig(WaveManager.CurrentWave);
		GoalManager.instance.NextBonusObjective();
		AIManager.instance.ActivateWave(WaveManager.WaveConfig);
		GenreWaveNode waveConfig = WaveManager.WaveConfig;
		if (waveConfig != null)
		{
			waveConfig.Setup();
		}
		GameplayManager.instance.UpdateGameState(GameState.InWave);
		this.view.RPC("WaveStartedNetwork", RpcTarget.All, new object[]
		{
			WaveManager.CurrentWave
		});
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.WaveStarted);
		RewardManager.instance.ReadyPlayers.Clear();
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00044004 File Offset: 0x00042204
	public void NextAppendix()
	{
		this.AppendixLevel++;
		this.view.RPC("NextAppendixNetwork", RpcTarget.All, new object[]
		{
			this.AppendixLevel
		});
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0004403C File Offset: 0x0004223C
	[PunRPC]
	private void NextAppendixNetwork(int appendixLevel)
	{
		this.AppendixLevel = appendixLevel;
		MapManager.Reset(false);
		WaveManager.NewStart = true;
		MapManager.SelectBiome(MapManager.GetRandom(0, 9999999));
		GameplayManager.instance.UpdateGameState(GameState.Reward_Start);
		UnityMainThreadDispatcher.Instance().Invoke(new Action(RewardManager.instance.NextReward), 1f);
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00044098 File Offset: 0x00042298
	public void SetWaveConfig(int Wave)
	{
		if (Wave < 0 || GameplayManager.CurrentTome == null)
		{
			if (WaveManager.WaveConfig != null)
			{
				this.ClearWaveConfig();
			}
			return;
		}
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		GenreWaveNode genreWaveNode = (curTomeRoot != null) ? curTomeRoot.GetWave(Wave, this.AppendixLevel) : null;
		if (genreWaveNode == WaveManager.WaveConfig)
		{
			return;
		}
		WaveManager.WaveConfig = genreWaveNode;
		this.GoalProgress = 0f;
		this.GoalCompletion = 0f;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0004410D File Offset: 0x0004230D
	public void ClearWaveConfig()
	{
		WaveManager.WaveConfig = null;
		this.GoalProgress = 0f;
		this.GoalCompletion = 0f;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0004412C File Offset: 0x0004232C
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(WaveManager.CurrentWave);
			stream.SendNext(this.WavesCompleted);
			stream.SendNext(this.GoalProgress);
			stream.SendNext(this.GoalCompletion);
			stream.SendNext(this.AppendixLevel);
			return;
		}
		WaveManager.CurrentWave = (int)stream.ReceiveNext();
		this.WavesCompleted = (int)stream.ReceiveNext();
		this.GoalProgress = (float)stream.ReceiveNext();
		this.GoalCompletion = (float)stream.ReceiveNext();
		this.SetWaveConfig(WaveManager.CurrentWave);
		this.AppendixLevel = (int)stream.ReceiveNext();
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x000441F5 File Offset: 0x000423F5
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00044217 File Offset: 0x00042417
	public WaveManager()
	{
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00044220 File Offset: 0x00042420
	[CompilerGenerated]
	private void <OnGameStateChanged>b__31_0()
	{
		if (this.AppendixLevel <= 0)
		{
			EndGameAnim.Display(true);
			UnityMainThreadDispatcher.Instance().Invoke(new Action(PostFXManager.instance.DoPulse), 0.65f);
			return;
		}
		InfoDisplay.SetText("Appendix " + this.AppendixLevel.ToString() + " Restored!", 3f, InfoArea.Title);
		PostFXManager.instance.DoPulse();
	}

	// Token: 0x040008D2 RID: 2258
	public static WaveManager instance;

	// Token: 0x040008D3 RID: 2259
	private PhotonView view;

	// Token: 0x040008D4 RID: 2260
	public int WavesCompleted;

	// Token: 0x040008D5 RID: 2261
	public float GoalProgress;

	// Token: 0x040008D6 RID: 2262
	public float GoalCompletion;

	// Token: 0x040008D7 RID: 2263
	public int AppendixLevel;

	// Token: 0x040008D8 RID: 2264
	[CompilerGenerated]
	private static GenreWaveNode <WaveConfig>k__BackingField;

	// Token: 0x040008D9 RID: 2265
	private static float WaveStartTime;

	// Token: 0x040008DA RID: 2266
	public static bool NewStart;

	// Token: 0x040008DB RID: 2267
	private int _currentWave;
}
