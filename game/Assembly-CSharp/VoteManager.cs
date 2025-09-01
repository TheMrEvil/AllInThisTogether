using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class VoteManager : MonoBehaviourPunCallbacks
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00042E26 File Offset: 0x00041026
	public static bool IsTimed
	{
		get
		{
			return VoteManager.CurrentVote == ChoiceType.EnemyScroll;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00042E30 File Offset: 0x00041030
	public static int MyCurrentVote
	{
		get
		{
			if (VoteManager.PlayerVotes == null || !VoteManager.PlayerVotes.ContainsKey(PhotonNetwork.LocalPlayer.ActorNumber))
			{
				return -1;
			}
			return VoteManager.PlayerVotes[PhotonNetwork.LocalPlayer.ActorNumber];
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00042E65 File Offset: 0x00041065
	public static bool IsVoting
	{
		get
		{
			return VoteManager.CurrentState == VoteState.Voting;
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00042E70 File Offset: 0x00041070
	private void Awake()
	{
		VoteManager.instance = this;
		VoteManager.PlayerVotes = new Dictionary<int, int>();
		VoteManager.NeededVotes = new List<int>();
		VoteManager.CurrentState = VoteState._;
		VoteManager.CurrentVote = ChoiceType._;
		this.view = base.GetComponent<PhotonView>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.SceneChanged));
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00042ECF File Offset: 0x000410CF
	private void SceneChanged()
	{
		this.HardReset();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00042ED7 File Offset: 0x000410D7
	public static void PrepareVote(ModType voteType)
	{
		VoteManager.instance.view.RPC("PrepareVoteNetwork", RpcTarget.All, new object[]
		{
			(int)voteType
		});
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00042EFD File Offset: 0x000410FD
	[PunRPC]
	private void PrepareVoteNetwork(int mType)
	{
		VoteManager.CurrentState = VoteState.VotePrepared;
		this.preparedType = (ModType)mType;
		if (PhotonNetwork.IsMasterClient)
		{
			VoteManager.RequestPrepared();
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00042F18 File Offset: 0x00041118
	public static void RequestPrepared()
	{
		VoteManager.instance.view.RPC("RequestPreparedNetwork", RpcTarget.MasterClient, Array.Empty<object>());
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x00042F34 File Offset: 0x00041134
	[PunRPC]
	private void RequestPreparedNetwork()
	{
		if (!PhotonNetwork.IsMasterClient || VoteManager.CurrentState != VoteState.VotePrepared)
		{
			return;
		}
		VoteManager.StartVote(this.preparedType);
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x00042F51 File Offset: 0x00041151
	public static void RequestVote(ChoiceType vt)
	{
		VoteManager.instance.view.RPC("RequestVoteNetwork", PhotonNetwork.MasterClient, new object[]
		{
			(int)vt
		});
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00042F7B File Offset: 0x0004117B
	[PunRPC]
	private void RequestVoteNetwork(int voteIndex)
	{
		if (VoteManager.IsVoting || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		VoteManager.StartVote((ChoiceType)voteIndex);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00042F92 File Offset: 0x00041192
	public static void StartVote(ChoiceType vt)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		VoteManager.instance.view.RPC("StartVoteNetwork", RpcTarget.All, new object[]
		{
			(int)vt,
			-1
		});
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00042FCC File Offset: 0x000411CC
	public static void StartVote(ModType MType)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		ChoiceType choiceType = ChoiceType.EnemyScroll;
		VoteManager.instance.view.RPC("StartVoteNetwork", RpcTarget.All, new object[]
		{
			(int)choiceType,
			-1
		});
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00043010 File Offset: 0x00041210
	[PunRPC]
	private void StartVoteNetwork(int vt, int rewardIndex)
	{
		VoteManager.CurrentVote = (ChoiceType)vt;
		VoteManager.CurrentState = VoteState.Voting;
		VoteManager.NeededVotes.Clear();
		VoteManager.PlayerVotes.Clear();
		foreach (Player player in PhotonNetwork.PlayerList)
		{
			VoteManager.NeededVotes.Add(player.ActorNumber);
		}
		this.InitializeVote(rewardIndex);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0004306C File Offset: 0x0004126C
	private void InitializeVote(int rewardIndex = -1)
	{
		if (VoteManager.IsTimed)
		{
			GameplayManager.instance.Timer = 30f;
		}
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		switch (VoteManager.CurrentVote)
		{
		case ChoiceType.EnemyScroll:
		{
			List<AugmentTree> mods = RaidManager.IsInRaid ? RaidManager.GetRaidEnemyOptions() : Fountain.ScrollManager.GetEnemyOptions();
			string text = this.EncodeModifiers(mods);
			this.view.RPC("LoadOptionsNetwork", RpcTarget.All, new object[]
			{
				(int)VoteManager.CurrentVote,
				text
			});
			return;
		}
		case ChoiceType._:
		case ChoiceType.__:
			break;
		case ChoiceType.Bindings:
			this.view.RPC("LoadOptionsNetwork", RpcTarget.All, new object[]
			{
				(int)VoteManager.CurrentVote,
				""
			});
			return;
		case ChoiceType.ReturnLibrary:
			this.view.RPC("LoadOptionsNetwork", RpcTarget.All, new object[]
			{
				(int)VoteManager.CurrentVote,
				""
			});
			return;
		case ChoiceType.Endless:
			this.view.RPC("LoadOptionsNetwork", RpcTarget.All, new object[]
			{
				(int)VoteManager.CurrentVote,
				""
			});
			return;
		case ChoiceType.RaidDifficulty:
			this.view.RPC("LoadOptionsNetwork", RpcTarget.All, new object[]
			{
				(int)VoteManager.CurrentVote,
				""
			});
			break;
		default:
			return;
		}
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x000431C0 File Offset: 0x000413C0
	[PunRPC]
	private void LoadOptionsNetwork(int vtype, string EncodedOptions)
	{
		switch (vtype)
		{
		case 1:
			Fountain.ScrollManager.LoadEnemyScrolls(this.DecodeModifiers(EncodedOptions));
			Fountain.ScrollManager.LoadChoiceUI();
			return;
		case 2:
		case 3:
		case 5:
			break;
		case 4:
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				PanelManager.instance.GoToPanel(PanelType.GameInvisible);
			}
			BindingsPanel.instance.Setup(GameplayManager.instance.GameGraph);
			return;
		case 6:
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				PanelManager.instance.GoToPanel(PanelType.GameInvisible);
			}
			EndlessVotePanel.instance.VoteStarted();
			return;
		case 7:
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				PanelManager.instance.GoToPanel(PanelType.GameInvisible);
			}
			RaidDifficultyUI.instance.VoteStarted();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00043273 File Offset: 0x00041473
	public static void Vote(int ChoiceID)
	{
		if (!VoteManager.IsVoting)
		{
			return;
		}
		VoteManager.instance.view.RPC("VoteNetwork", RpcTarget.All, new object[]
		{
			PhotonNetwork.LocalPlayer.ActorNumber,
			ChoiceID
		});
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x000432B4 File Offset: 0x000414B4
	[PunRPC]
	private void VoteNetwork(int VoterID, int ChoiceID)
	{
		if (ChoiceID == -1)
		{
			if (VoteManager.PlayerVotes.ContainsKey(VoterID))
			{
				VoteManager.PlayerVotes.Remove(VoterID);
			}
		}
		else
		{
			if (!VoteManager.PlayerVotes.ContainsKey(VoterID))
			{
				VoteManager.PlayerVotes.Add(VoterID, ChoiceID);
			}
			VoteManager.PlayerVotes[VoterID] = ChoiceID;
			this.CheckVoteCompleted();
		}
		Action<ChoiceType> onVotesChanged = VoteManager.OnVotesChanged;
		if (onVotesChanged == null)
		{
			return;
		}
		onVotesChanged(VoteManager.CurrentVote);
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00043320 File Offset: 0x00041520
	private void Update()
	{
		if (VoteManager.CurrentState == VoteState.VotePrepared)
		{
			InfoDisplay.SetText("Vote Available", 1f, InfoArea.DetailTop);
		}
		if (GameplayManager.IsInGame && PlayerControl.PlayerCount > 1 && VoteManager.EndRunVotes.Count == PlayerControl.PlayerCount)
		{
			this.CancelRunAndReturnToLibrary();
		}
		if (VoteManager.CurrentState != VoteState.Voting)
		{
			return;
		}
		if (VoteManager.IsTimed && VoteManager.PlayerVotes.Count == 0)
		{
			GameplayManager.instance.Timer = 30f;
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00043398 File Offset: 0x00041598
	private void CheckVoteCompleted()
	{
		if (VoteManager.NeededVotes.Count == 0)
		{
			return;
		}
		bool flag = true;
		foreach (int key in VoteManager.NeededVotes)
		{
			if (!VoteManager.PlayerVotes.ContainsKey(key))
			{
				flag = false;
			}
		}
		if (flag)
		{
			this.VoteCompleted();
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0004340C File Offset: 0x0004160C
	public static void ForceEndVote()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		VoteManager.instance.VoteCompleted();
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00043420 File Offset: 0x00041620
	private void VoteCompleted()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (int key in VoteManager.PlayerVotes.Keys)
		{
			int num = VoteManager.PlayerVotes[key];
			if (!dictionary.ContainsKey(num))
			{
				dictionary.Add(num, 0);
			}
			Dictionary<int, int> dictionary2 = dictionary;
			int key2 = num;
			dictionary2[key2]++;
		}
		List<int> list = new List<int>();
		int num2 = 0;
		foreach (int num3 in dictionary.Keys)
		{
			if (dictionary[num3] == num2)
			{
				list.Add(num3);
			}
			else if (dictionary[num3] > num2)
			{
				num2 = dictionary[num3];
				list.Clear();
				list.Add(num3);
			}
		}
		if (list.Count == 0)
		{
			list.Add(0);
		}
		int num4 = list[UnityEngine.Random.Range(0, list.Count)];
		if (VoteManager.CurrentVote == ChoiceType.Endless && list.Count > 1)
		{
			num4 = 1;
		}
		this.view.RPC("VoteCompletedNetwork", RpcTarget.All, new object[]
		{
			(int)VoteManager.CurrentVote,
			num4,
			num2
		});
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x000435A4 File Offset: 0x000417A4
	[PunRPC]
	private void VoteCompletedNetwork(int voteType, int winner, int votes)
	{
		if (voteType == 1)
		{
			Fountain.ScrollManager.ApplyEnemyScroll(winner, votes);
		}
		else if (voteType == 4)
		{
			BindingsPanel.instance.ApplyBindings();
		}
		else if (voteType == 5)
		{
			Debug.Log("Returning to Library");
			MapManager.ReturnToLibrary();
		}
		else if (voteType == 6)
		{
			GoalManager.Reset(false);
			GameplayManager.instance.EndlessDecisionMade(winner == 1);
			EndlessVotePanel.instance.VoteCompleted(winner);
		}
		else if (voteType == 7)
		{
			RaidManager.instance.Difficulty = (RaidDB.Difficulty)winner;
			RaidDifficultyUI.instance.VoteCompleted(winner);
			RaidManager.instance.RaidDifficultySelected();
		}
		VoteManager.CurrentState = VoteState.Completed;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0004363C File Offset: 0x0004183C
	public static int GetVotes(int OptionID)
	{
		if (OptionID == -1)
		{
			return 0;
		}
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			if (keyValuePair.Value == OptionID)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x000436A0 File Offset: 0x000418A0
	private string EncodeModifiers(List<AugmentTree> mods)
	{
		string text = "";
		if (mods.Count == 0)
		{
			return text;
		}
		foreach (AugmentTree augmentTree in mods)
		{
			text = text + augmentTree.ID + "|";
		}
		return text.Substring(0, text.Length - 1);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00043718 File Offset: 0x00041918
	private List<AugmentTree> DecodeModifiers(string opts)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		string[] array = opts.Split('|', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			AugmentTree augment = GraphDB.GetAugment(array[i]);
			if (augment != null)
			{
				list.Add(augment);
			}
		}
		return list;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0004375D File Offset: 0x0004195D
	public void ToggleWantToLeaveVote()
	{
		this.view.RPC("UpdateLeaveVoteNetwork", RpcTarget.All, new object[]
		{
			PhotonNetwork.LocalPlayer.ActorNumber
		});
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00043788 File Offset: 0x00041988
	[PunRPC]
	private void UpdateLeaveVoteNetwork(int playerID)
	{
		bool flag = VoteManager.EndRunVotes.Contains(playerID);
		if (flag)
		{
			VoteManager.EndRunVotes.Remove(playerID);
		}
		else
		{
			VoteManager.EndRunVotes.Add(playerID);
		}
		Player player = PhotonNetwork.CurrentRoom.GetPlayer(playerID, false);
		if (player != null)
		{
			if (flag)
			{
				UIPing.WorldEventPing("<b>" + player.NickName + "</b> reconsidered.", 2f);
				return;
			}
			UIPing.WorldEventPing("<b>" + player.NickName + "</b> votes to leave.", 2f);
		}
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00043810 File Offset: 0x00041A10
	public void CancelRunAndReturnToLibrary()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		VoteManager.EndRunVotes.Clear();
		if (RaidManager.IsInRaid)
		{
			if (RaidManager.IsEncounterStarted)
			{
				RaidManager.instance.EncounterFailed();
			}
			RaidRecord.UploadResult(RaidRecord.Result.Quit, RaidManager.instance.currentEncounterTime);
		}
		else
		{
			GameRecord.UploadQuit();
		}
		MapManager.ReturnToLibrary();
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00043864 File Offset: 0x00041A64
	public override void OnPlayerLeftRoom(Player Player)
	{
		VoteManager.EndRunVotes.Remove(Player.ActorNumber);
		VoteManager.NeededVotes.Remove(Player.ActorNumber);
		if (VoteManager.PlayerVotes.ContainsKey(Player.ActorNumber))
		{
			VoteManager.PlayerVotes.Remove(Player.ActorNumber);
			Action<ChoiceType> onVotesChanged = VoteManager.OnVotesChanged;
			if (onVotesChanged == null)
			{
				return;
			}
			onVotesChanged(VoteManager.CurrentVote);
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x000438CA File Offset: 0x00041ACA
	public override void OnLeftRoom()
	{
		this.HardReset();
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x000438D2 File Offset: 0x00041AD2
	public void HardReset()
	{
		VoteManager.CurrentState = VoteState._;
		VoteManager.EndRunVotes.Clear();
		VoteManager.NeededVotes.Clear();
		VoteManager.PlayerVotes.Clear();
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x000438F8 File Offset: 0x00041AF8
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.SceneChanged));
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0004391A File Offset: 0x00041B1A
	public VoteManager()
	{
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00043922 File Offset: 0x00041B22
	// Note: this type is marked as 'beforefieldinit'.
	static VoteManager()
	{
	}

	// Token: 0x040008BA RID: 2234
	public static Dictionary<int, int> PlayerVotes = new Dictionary<int, int>();

	// Token: 0x040008BB RID: 2235
	private static List<int> NeededVotes = new List<int>();

	// Token: 0x040008BC RID: 2236
	public static VoteState CurrentState;

	// Token: 0x040008BD RID: 2237
	public static ChoiceType CurrentVote;

	// Token: 0x040008BE RID: 2238
	public const float VOTE_TIME = 30f;

	// Token: 0x040008BF RID: 2239
	public static VoteManager instance;

	// Token: 0x040008C0 RID: 2240
	private PhotonView view;

	// Token: 0x040008C1 RID: 2241
	public static HashSet<int> EndRunVotes = new HashSet<int>();

	// Token: 0x040008C2 RID: 2242
	public static Action<ChoiceType> OnVotesChanged;

	// Token: 0x040008C3 RID: 2243
	private ModType preparedType;
}
