using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class RewardManager : MonoBehaviour, IPunObservable
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000946 RID: 2374 RVA: 0x0003E8F5 File Offset: 0x0003CAF5
	private static GameState CurrentState
	{
		get
		{
			return GameplayManager.CurState;
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000947 RID: 2375 RVA: 0x0003E8FC File Offset: 0x0003CAFC
	public static bool InRewards
	{
		get
		{
			return RewardManager.CurrentState == GameState.Reward_Start || RewardManager.CurrentState == GameState.Reward_Player || RewardManager.CurrentState == GameState.Reward_Fountain || RewardManager.CurrentState == GameState.Reward_Map || RewardManager.CurrentState == GameState.Reward_Traveling || RewardManager.CurrentState == GameState.Reward_PreEnemy || RewardManager.CurrentState == GameState.Reward_Enemy;
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000948 RID: 2376 RVA: 0x0003E93C File Offset: 0x0003CB3C
	public static bool ShouldSkipFontSelection
	{
		get
		{
			return InkManager.Store.Count == 0 || (WaveManager.instance.AppendixLevel > 0 && InkManager.TotalTeamPoints <= 0);
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0003E964 File Offset: 0x0003CB64
	private void Awake()
	{
		RewardManager.instance = this;
		this.view = base.GetComponent<PhotonView>();
		this.Reset();
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0003E97E File Offset: 0x0003CB7E
	private void Update()
	{
		this.CheckFontPowerAutoSkip();
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0003E986 File Offset: 0x0003CB86
	public void Reset()
	{
		this.ReadyPlayers.Clear();
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0003E993 File Offset: 0x0003CB93
	public GenreRewardNode RewardConfig()
	{
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		if (curTomeRoot == null)
		{
			return null;
		}
		int currentWave = WaveManager.CurrentWave;
		WaveManager waveManager = WaveManager.instance;
		return curTomeRoot.GetReward(currentWave, (waveManager != null) ? waveManager.AppendixLevel : 0);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0003E9BC File Offset: 0x0003CBBC
	public void StartGameReward()
	{
		GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
		GenreFountainNode genreFountainNode = ((curTomeRoot != null) ? curTomeRoot.FountainNode : null) as GenreFountainNode;
		if (genreFountainNode != null && genreFountainNode.RewardAtStart)
		{
			InkManager.instance.FirstRoundHostInk();
		}
		RewardManager.instance.NextReward();
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0003EA05 File Offset: 0x0003CC05
	public void NextRewardInteraction()
	{
		this.view.RPC("NextRewardInteractNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.MyViewID
		});
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0003EA2C File Offset: 0x0003CC2C
	[PunRPC]
	private void NextRewardInteractNetwork(int PlayerID)
	{
		if (!PhotonNetwork.IsMasterClient || RewardManager.CurrentState != GameState.Reward_Fountain)
		{
			return;
		}
		if (this.ReadyPlayers.Contains(PlayerID))
		{
			this.ReadyPlayers.Remove(PlayerID);
		}
		else
		{
			this.ReadyPlayers.Add(PlayerID);
		}
		if (this.ReadyPlayers.Count == PhotonNetwork.CurrentRoom.PlayerCount)
		{
			this.NextReward();
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0003EA8F File Offset: 0x0003CC8F
	private void CheckFontPowerAutoSkip()
	{
		if (PlayerControl.PlayerCount != 1 || RewardManager.CurrentState != GameState.Reward_Fountain)
		{
			return;
		}
		if (RewardManager.ShouldSkipFontSelection && !PlayerChoicePanel.IsSelecting && !PlayerChoicePanel.InApplySequence)
		{
			this.NextReward();
		}
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0003EABD File Offset: 0x0003CCBD
	public void NextReward()
	{
		this.NextReward(0);
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0003EAC8 File Offset: 0x0003CCC8
	private void NextReward(int depth)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (depth > 10)
		{
			Debug.LogError("Infinite Recursion in NextReward Method!");
			return;
		}
		if (GameplayManager.CurTomeRoot == null)
		{
			return;
		}
		GameplayManager.instance.Timer = 10f;
		this.GoToNextRewardState(RewardManager.CurrentState);
		if (RewardManager.CurrentState == GameState.PostRewards || TutorialManager.InTutorial || !RewardManager.InRewards)
		{
			return;
		}
		GenreRewardNode genreRewardNode = this.RewardConfig();
		if (genreRewardNode == null || !genreRewardNode.HasReward(RewardManager.CurrentState))
		{
			if (RewardManager.CurrentState != GameState.Reward_Fountain)
			{
				this.NextReward(depth + 1);
				return;
			}
		}
		else
		{
			this.ApplyReward(this.RewardConfig());
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003EB68 File Offset: 0x0003CD68
	private void GoToNextRewardState(GameState curState)
	{
		switch (curState)
		{
		case GameState.InWave:
			GameplayManager.instance.UpdateGameState(GameState.Reward_Start);
			return;
		case GameState.Reward_Start:
			GameplayManager.instance.UpdateGameState(GameState.Reward_Player);
			return;
		case GameState.Reward_Player:
			if (this.ChapterHasVignette())
			{
				GameplayManager.instance.UpdateGameState(GameState.Vignette_PreWait);
				return;
			}
			GameplayManager.instance.UpdateGameState(GameState.Reward_Fountain);
			return;
		case GameState.Reward_Fountain:
			GameplayManager.instance.UpdateGameState(GameState.Reward_Map);
			return;
		case GameState.Reward_FontPages:
			return;
		case GameState.Reward_Map:
			GameplayManager.instance.UpdateGameState(GameState.Reward_Traveling);
			return;
		case GameState.Reward_Traveling:
			GameplayManager.instance.UpdateGameState(GameState.Reward_PreEnemy);
			return;
		case GameState.Reward_PreEnemy:
			GameplayManager.instance.UpdateGameState(GameState.Reward_Enemy);
			return;
		case GameState.Reward_Enemy:
			GameplayManager.instance.UpdateGameState(GameState.PostRewards);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0003EC1C File Offset: 0x0003CE1C
	private void ApplyReward(GenreRewardNode rewardNode)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		int curWave = WaveManager.CurrentWave;
		if (rewardNode == null)
		{
			Debug.LogError("Failed to Load Reward Config data for wave " + curWave.ToString());
		}
		if (RewardManager.CurrentState == GameState.Reward_Player)
		{
			if (curWave >= 0 && !WaveManager.NewStart)
			{
				UnityMainThreadDispatcher.Instance().Invoke(delegate
				{
					this.view.RPC("PlayerScrollNetwork", RpcTarget.All, Array.Empty<object>());
				}, this.StartRewardDelay);
				UnityMainThreadDispatcher.Instance().Invoke(new Action(this.NextReward), this.StartRewardDelay + 1f);
			}
			else
			{
				this.view.RPC("PlayerScrollNetwork", RpcTarget.All, Array.Empty<object>());
				UnityMainThreadDispatcher.Instance().Invoke(new Action(this.NextReward), 1f);
			}
			GenreRootNode curTomeRoot = GameplayManager.CurTomeRoot;
			GenreFountainNode genreFountainNode = ((curTomeRoot != null) ? curTomeRoot.FountainNode : null) as GenreFountainNode;
			if (genreFountainNode != null && ((curWave == 0 && !genreFountainNode.RewardAtStart) | (curWave == -1 && genreFountainNode.RewardAtStart)))
			{
				InkManager.instance.SetupFountainLayers(genreFountainNode);
				return;
			}
		}
		else if (RewardManager.CurrentState != GameState.Reward_Fountain)
		{
			if (RewardManager.CurrentState == GameState.Reward_FontPages)
			{
				if (InkManager.FontPagesOwed > 0)
				{
					this.view.RPC("FontPageRewards", RpcTarget.All, new object[]
					{
						InkManager.FontPagesOwed
					});
					return;
				}
				this.NextReward();
				return;
			}
			else if (RewardManager.CurrentState == GameState.Reward_Map)
			{
				if (!rewardNode.HasReward(GameState.Reward_Map) || curWave < 0)
				{
					this.NextReward();
					return;
				}
				GenreWaveNode waveConfig = WaveManager.WaveConfig;
				GameMap gameMap = (waveConfig != null) ? waveConfig.GetMap(true) : null;
				if (waveConfig == null && WaveManager.instance.AppendixLevel > 0)
				{
					GenreRootNode curTomeRoot2 = GameplayManager.CurTomeRoot;
					gameMap = ((curTomeRoot2 != null) ? curTomeRoot2.GetFirstAppendixMap() : null);
				}
				if (gameMap != null)
				{
					if (gameMap.IsMultiBiome)
					{
						MapManager.ChangeBiome(gameMap.GetOtherBiome(MapManager.TomeBiome));
					}
					MapManager.instance.ChangeMap(gameMap.Scene.SceneName);
					return;
				}
				MapManager.instance.StayMap();
				return;
			}
			else
			{
				if (RewardManager.CurrentState == GameState.Reward_PreEnemy)
				{
					UnityMainThreadDispatcher.Instance().Invoke(delegate
					{
						GenreRewardNode genreRewardNode = this.RewardConfig();
						if (genreRewardNode != null && genreRewardNode.EnemyType != GenreRewardNode.RewardType.None)
						{
							EnemySelectionPanel.instance.GoToUI();
							return;
						}
						GameplayManager.instance.InfoMessage("Chapter  " + (curWave + 2).ToString(), 1.25f, InfoArea.Title);
					}, 1f);
					UnityMainThreadDispatcher.Instance().Invoke(new Action(this.NextReward), 1.5f);
					return;
				}
				if (RewardManager.CurrentState == GameState.Reward_Enemy)
				{
					VoteManager.PrepareVote(ModType.Enemy);
				}
			}
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0003EE87 File Offset: 0x0003D087
	private bool ChapterHasVignette()
	{
		return GameplayManager.CurTomeRoot.GetVignette(WaveManager.CurrentWave, WaveManager.instance.AppendixLevel) != null;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
	public void PostVignetteReward()
	{
		if (WaveManager.instance.AppendixLevel > 0 && InkManager.TotalTeamPoints <= 0)
		{
			GameplayManager.instance.UpdateGameState(GameState.Reward_Map);
			this.ApplyReward(this.RewardConfig());
			return;
		}
		GameplayManager.instance.UpdateGameState(GameState.Reward_Fountain);
		this.ApplyReward(this.RewardConfig());
		AugmentsPanel.TryOpen();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0003EF00 File Offset: 0x0003D100
	[PunRPC]
	private void PlayerScrollNetwork()
	{
		AudioManager.PlaySFX2D(this.ScrollRewardSFX, 1f, 0.1f);
		PlayerChoicePanel.ChoicesChosen = 0;
		PlayerChoicePanel.ChoiceTotal = 0;
		AugmentsPanel.AwardUpgradeChoice();
		if (this.ChapterHasVignette() && WaveManager.instance.AppendixLevel <= 0)
		{
			AugmentsPanel.AwardUpgradeChoice();
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0003EF4D File Offset: 0x0003D14D
	[PunRPC]
	private void FontPageRewards(int rewardCount)
	{
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0003EF4F File Offset: 0x0003D14F
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(this.ReadyPlayers.ToArray());
			return;
		}
		this.ReadyPlayers = new List<int>((int[])stream.ReceiveNext());
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0003EF81 File Offset: 0x0003D181
	public RewardManager()
	{
	}

	// Token: 0x040007B7 RID: 1975
	public static RewardManager instance;

	// Token: 0x040007B8 RID: 1976
	public float StartRewardDelay = 1f;

	// Token: 0x040007B9 RID: 1977
	public AudioClip ScrollRewardSFX;

	// Token: 0x040007BA RID: 1978
	private PhotonView view;

	// Token: 0x040007BB RID: 1979
	public List<int> ReadyPlayers = new List<int>();

	// Token: 0x040007BC RID: 1980
	[NonSerialized]
	public bool NewStart;

	// Token: 0x020004C2 RID: 1218
	[CompilerGenerated]
	private sealed class <>c__DisplayClass23_0
	{
		// Token: 0x060022A9 RID: 8873 RVA: 0x000C7691 File Offset: 0x000C5891
		public <>c__DisplayClass23_0()
		{
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000C7699 File Offset: 0x000C5899
		internal void <ApplyReward>b__1()
		{
			this.<>4__this.view.RPC("PlayerScrollNetwork", RpcTarget.All, Array.Empty<object>());
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000C76B8 File Offset: 0x000C58B8
		internal void <ApplyReward>b__0()
		{
			GenreRewardNode genreRewardNode = this.<>4__this.RewardConfig();
			if (genreRewardNode != null && genreRewardNode.EnemyType != GenreRewardNode.RewardType.None)
			{
				EnemySelectionPanel.instance.GoToUI();
				return;
			}
			GameplayManager.instance.InfoMessage("Chapter  " + (this.curWave + 2).ToString(), 1.25f, InfoArea.Title);
		}

		// Token: 0x04002444 RID: 9284
		public RewardManager <>4__this;

		// Token: 0x04002445 RID: 9285
		public int curWave;
	}
}
