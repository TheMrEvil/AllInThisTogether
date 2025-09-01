using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class InkManager : MonoBehaviourPunCallbacks, IPunObservable
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x00020F74 File Offset: 0x0001F174
	public static int MyShards
	{
		get
		{
			int result = 0;
			if (InkManager.Bank == null || PlayerControl.myInstance == null || !InkManager.Bank.ContainsKey(PlayerControl.myInstance.ViewID))
			{
				return result;
			}
			return InkManager.Bank[PlayerControl.myInstance.ViewID].Amount;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x00020FC8 File Offset: 0x0001F1C8
	public static int InkMultiplier
	{
		get
		{
			int result = 4;
			if (PhotonNetwork.InRoom)
			{
				int num;
				switch (PhotonNetwork.CurrentRoom.PlayerCount)
				{
				case 1:
					num = 4;
					break;
				case 2:
					num = 2;
					break;
				case 3:
					num = 1;
					break;
				case 4:
					num = 1;
					break;
				default:
					num = 0;
					break;
				}
				result = num;
			}
			return result;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600043B RID: 1083 RVA: 0x00021018 File Offset: 0x0001F218
	public static int TotalTeamPoints
	{
		get
		{
			if (InkManager.Bank == null)
			{
				return 0;
			}
			int num = 0;
			foreach (KeyValuePair<int, InkManager.PlayerInk> keyValuePair in InkManager.Bank)
			{
				num += keyValuePair.Value.Amount;
			}
			return num;
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00021080 File Offset: 0x0001F280
	private void Awake()
	{
		InkManager.instance = this;
		InkManager.Bank = new Dictionary<int, InkManager.PlayerInk>();
		InkManager.PurchasedMods = new Augments();
		InkManager.NewRowAvailable = false;
		this.curLayersAvailable = 0;
		InkManager.FontPagesOwed = 0;
		InkManager.FontPageChosen = new Dictionary<int, int>();
		InkManager.Store = new List<InkRow>();
		this.view = base.GetComponent<PhotonView>();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x000210FC File Offset: 0x0001F2FC
	public void Reset()
	{
		InkManager.Bank.Clear();
		InkManager.PurchasedMods = new Augments();
		InkManager.Store.Clear();
		InkManager.FontPageChosen = new Dictionary<int, int>();
		this.curLayersAvailable = 0;
		InkManager.NewRowAvailable = false;
		this.PauseQuoteID = "";
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00021149 File Offset: 0x0001F349
	private void Update()
	{
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0002114C File Offset: 0x0001F34C
	public void AddWaveInk(bool force = false)
	{
		if ((RewardManager.instance.RewardConfig() == null || RewardManager.instance.RewardConfig().NoFountainPoints) && !force)
		{
			return;
		}
		int inkMultiplier = InkManager.InkMultiplier;
		this.AddInk(inkMultiplier);
		if (PhotonNetwork.IsMasterClient)
		{
			this.HostSpecialInkRewards();
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0002119C File Offset: 0x0001F39C
	public void FirstRoundHostInk()
	{
		UnityEngine.Debug.Log("Adding First-Round Ink Reward");
		this.AddWaveInk(true);
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!(playerControl == PlayerControl.myInstance))
			{
				UnityEngine.Debug.Log("Adding FirstRound Ink for player " + playerControl.ViewID.ToString());
				this.AddInkNetwork(InkManager.InkMultiplier, playerControl.ViewID);
			}
		}
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00021234 File Offset: 0x0001F434
	private void HostSpecialInkRewards()
	{
		if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
		{
			int index = Mathf.Abs(WaveManager.CurrentWave) % PlayerControl.AllPlayers.Count;
			int viewID = PlayerControl.AllPlayers[index].ViewID;
			this.AddInkNetwork(1, viewID);
		}
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0002127D File Offset: 0x0001F47D
	public void AddInk(int Amount)
	{
		if (Amount == 0)
		{
			return;
		}
		this.view.RPC("AddInkNetwork", RpcTarget.MasterClient, new object[]
		{
			Amount,
			PlayerControl.MyViewID
		});
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x000212B0 File Offset: 0x0001F4B0
	[PunRPC]
	public void AddInkNetwork(int Amount, int PlayerID)
	{
		UnityEngine.Debug.Log("Adding ink for Player " + PlayerID.ToString());
		this.GetOrCreateInk(PlayerID).Amount += Amount;
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x000212DC File Offset: 0x0001F4DC
	public void SetupFountainLayers(GenreFountainNode fountain)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < fountain.Layers; i++)
		{
			if (i > 0)
			{
				num += InkManager.Store[i - 1].RowCost;
			}
			List<AugmentTree> modifiers = fountain.GetModifiers(i, num);
			int cost = fountain.GetCost(i);
			this.AddStoreLayer(modifiers, cost, i);
		}
		this.SyncStore();
		this.view.RPC("StoreLayerAddedNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00021354 File Offset: 0x0001F554
	private void AddStoreLayer(List<AugmentTree> options, int unlockCost, int layer)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		InkRow inkRow = new InkRow();
		inkRow.Layer = layer;
		inkRow.UnlockCost = unlockCost;
		foreach (AugmentTree option in options)
		{
			InkTalent inkTalent = this.GenerateInkOption(option);
			inkTalent.Row = inkRow;
			inkRow.Options.Add(inkTalent);
		}
		InkManager.Store.Add(inkRow);
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x000213E0 File Offset: 0x0001F5E0
	[PunRPC]
	private void StoreLayerAddedNetwork()
	{
		Fountain.instance.FountainLayerAdded();
		InkManager.NewRowAvailable = true;
		FountainStoreUI.instance.Invalidate();
		AugmentsPanel.instance.Refresh();
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00021408 File Offset: 0x0001F608
	public void TryInvest(InkTalent p, int Amount)
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.view.RPC("TryInvestNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.myInstance.ViewID,
			p.ID,
			Amount
		});
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00021460 File Offset: 0x0001F660
	[PunRPC]
	private void TryInvestNetwork(int PlayerID, string ItemID, int Amount)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		InkTalent storeItem = this.GetStoreItem(ItemID);
		if (storeItem == null || storeItem.State != InkTalent.InkPurchaseState.Available || !storeItem.CanPurchase)
		{
			return;
		}
		int num = Mathf.Min(storeItem.Cost - storeItem.CurrentValue, Amount);
		if (!InkManager.Bank.ContainsKey(PlayerID) || InkManager.Bank[PlayerID].Amount < num)
		{
			return;
		}
		storeItem.CurrentValue += num;
		InkManager.Bank[PlayerID].Amount -= num;
		this.CheckNewLayer();
		if (storeItem.CurrentValue < storeItem.Cost)
		{
			this.SyncStore();
			return;
		}
		this.CompletePurchase(storeItem);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0002150E File Offset: 0x0001F70E
	private void CompletePurchase(InkTalent p)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		p.State = InkTalent.InkPurchaseState.Purchased;
		this.AwardGameAugment(p.Augment, 1);
		this.SyncStore();
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00021532 File Offset: 0x0001F732
	public void AwardGameAugment(AugmentTree tree, int count)
	{
		this.view.RPC("ApplyPurchaseAugmentNetwork", RpcTarget.All, new object[]
		{
			tree.Root.guid,
			count
		});
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00021564 File Offset: 0x0001F764
	[PunRPC]
	public void ApplyPurchaseAugmentNetwork(string GUID, int count)
	{
		AugmentTree augment = GraphDB.GetAugment(GUID);
		if (augment == null)
		{
			UnityEngine.Debug.LogError("Store Augment [" + GUID + "] couldn't be found");
			return;
		}
		Fountain.instance.FountainPowerAdded(augment);
		GameRecord.FontPurchase(augment);
		InkManager.PurchasedMods.Add(augment, count);
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			augment.Root.TryTrigger(playerControl, EventTrigger.ThisChosen, new EffectProperties(playerControl), 1f);
		}
		AugmentsPanel.instance.Refresh();
		for (int i = 0; i < count; i++)
		{
			augment.Root.TryTrigger(PlayerControl.myInstance, EventTrigger.ModAdded, null, 1f);
			foreach (Node node in augment.Root.Overrides)
			{
				AugmentAwardOverrideNode augmentAwardOverrideNode = node as AugmentAwardOverrideNode;
				if (augmentAwardOverrideNode != null)
				{
					this.AwardPlayerPage(augmentAwardOverrideNode, null);
				}
			}
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00021694 File Offset: 0x0001F894
	public void AwardPlayerPage(AugmentAwardOverrideNode n = null, List<AugmentTree> Ignore = null)
	{
		this.AwardPlayerPageChoice((n != null) ? n.Filter : null, Ignore);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x000216AC File Offset: 0x0001F8AC
	public void AwardPlayerPageChoice(AugmentFilter filter, List<AugmentTree> Ignore = null)
	{
		List<AugmentTree> list;
		if (filter != null)
		{
			list = GraphDB.GetValidMods(ModType.Player);
			filter.FilterList(list, PlayerControl.myInstance);
		}
		else
		{
			list = GenreRewardNode.PlayerAlgorithmReward(QualitySelector.Any, Ignore);
		}
		List<AugmentTree> list2 = new List<AugmentTree>();
		int num = 0;
		while (num < 3 && list.Count > 0)
		{
			AugmentTree item = GraphDB.ChooseModFromList(ModType.Player, list, false, GameplayManager.IsChallengeActive);
			list.Remove(item);
			list2.Add(item);
			num++;
		}
		AugmentsPanel.AwardUpgradeChoice(list2);
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00021718 File Offset: 0x0001F918
	private void StoreWaveStarted()
	{
		bool isMasterClient = PhotonNetwork.IsMasterClient;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00021720 File Offset: 0x0001F920
	private void SyncStore()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		for (int i = 0; i < InkManager.Store.Count; i++)
		{
			if (InkManager.Store[i] != null)
			{
				this.view.RPC("SyncStoreLayer", RpcTarget.All, new object[]
				{
					InkManager.Store.Count,
					i,
					InkManager.Store[i].UnlockCost
				});
				List<InkTalent> options = InkManager.Store[i].Options;
				for (int j = 0; j < options.Count; j++)
				{
					PhotonView photonView = this.view;
					string methodName = "SyncItemNetwork";
					RpcTarget target = RpcTarget.All;
					object[] array = new object[7];
					array[0] = options.Count;
					array[1] = j;
					int num = 2;
					AugmentTree augment = options[j].Augment;
					array[num] = ((augment != null) ? augment.Root.guid : null);
					array[3] = options[j].ID;
					array[4] = options[j].CurrentValue;
					array[5] = (int)options[j].State;
					array[6] = i;
					photonView.RPC(methodName, target, array);
				}
			}
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00021860 File Offset: 0x0001FA60
	[PunRPC]
	private void SyncStoreLayer(int total, int layer, int cost)
	{
		if (InkManager.Store.Count != total)
		{
			if (InkManager.Store.Count > total)
			{
				InkManager.Store = InkManager.Store.GetRange(0, total);
			}
			else
			{
				for (int i = InkManager.Store.Count; i < total; i++)
				{
					InkManager.Store.Add(new InkRow());
				}
			}
		}
		InkManager.Store[layer].UnlockCost = cost;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x000218D0 File Offset: 0x0001FAD0
	[PunRPC]
	private void SyncItemNetwork(int total, int index, string augmentID, string itemID, int currentVal, int state, int layer)
	{
		if (InkManager.Store.Count <= layer)
		{
			UnityEngine.Debug.LogError(string.Format("Invalid Layer ID! - Got Layer {0} but only have {1} rows", layer, InkManager.Store.Count));
			return;
		}
		InkRow inkRow = InkManager.Store[layer];
		if (augmentID == null || augmentID.Length < 5)
		{
			inkRow.Options[index] = null;
			UnityEngine.Debug.LogError("Provided GUID [" + augmentID + "] was invalid, setting store option to null");
			return;
		}
		if (inkRow.Options.Count < total)
		{
			for (int i = inkRow.Options.Count; i < total; i++)
			{
				inkRow.Options.Add(null);
			}
		}
		List<InkTalent> options = inkRow.Options;
		InkTalent inkTalent;
		if ((inkTalent = options[index]) == null)
		{
			inkTalent = (options[index] = new InkTalent());
		}
		InkTalent inkTalent2 = inkTalent;
		inkTalent2.CurrentValue = currentVal;
		inkTalent2.State = (InkTalent.InkPurchaseState)state;
		inkTalent2.Row = inkRow;
		inkTalent2.ID = itemID;
		if (inkTalent2.Augment == null || inkTalent2.Augment.Root.guid != augmentID)
		{
			inkTalent2.Augment = GraphDB.GetAugment(augmentID);
		}
		FountainStoreUI.instance.UpdateInkInfo(inkTalent2);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00021A02 File Offset: 0x0001FC02
	[PunRPC]
	private void SyncPagesOwed(int numPages)
	{
		InkManager.FontPagesOwed = numPages;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00021A0A File Offset: 0x0001FC0A
	private InkTalent GenerateInkOption(AugmentTree option)
	{
		return new InkTalent
		{
			Augment = option
		};
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00021A18 File Offset: 0x0001FC18
	private void CheckNewLayer()
	{
		int num = 0;
		for (int i = 0; i < InkManager.Store.Count; i++)
		{
			if (InkManager.Store[i].IsUnlocked)
			{
				num = i;
			}
		}
		if (num <= this.curLayersAvailable)
		{
			return;
		}
		this.curLayersAvailable = num;
		this.view.RPC("NewLayerUnlocked", RpcTarget.All, new object[]
		{
			num,
			InkManager.FontPagesOwed
		});
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00021A8E File Offset: 0x0001FC8E
	[PunRPC]
	private void NewLayerUnlocked(int layerID, int numPages)
	{
		InkManager.FontPagesOwed = numPages;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00021A98 File Offset: 0x0001FC98
	public InkManager.PlayerInk GetOrCreateInk(int ActorID)
	{
		if (InkManager.Bank.ContainsKey(ActorID))
		{
			return InkManager.Bank[ActorID];
		}
		InkManager.PlayerInk playerInk = new InkManager.PlayerInk();
		playerInk.ActorID = ActorID;
		playerInk.Amount = 0;
		InkManager.Bank.Add(ActorID, playerInk);
		return playerInk;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00021ADF File Offset: 0x0001FCDF
	public static bool HasAugment(AugmentTree tree)
	{
		return !(InkManager.instance == null) && InkManager.PurchasedMods != null && InkManager.PurchasedMods.trees.ContainsKey(tree.Root);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00021B0C File Offset: 0x0001FD0C
	public InkTalent GetStoreItem(string ItemID)
	{
		foreach (InkRow inkRow in InkManager.Store)
		{
			foreach (InkTalent inkTalent in inkRow.Options)
			{
				if (inkTalent.ID == ItemID)
				{
					return inkTalent;
				}
			}
		}
		return null;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00021BA8 File Offset: 0x0001FDA8
	public static int GetInvested(int layer = -1)
	{
		if (layer >= 0 && layer < InkManager.Store.Count)
		{
			return InkManager.Store[layer].InvestedInRow;
		}
		int num = 0;
		foreach (InkRow inkRow in InkManager.Store)
		{
			num += inkRow.InvestedInRow;
		}
		return num;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00021C24 File Offset: 0x0001FE24
	public void TriggerAugments(EventTrigger Trigger)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, 9999999);
		this.view.RPC("TriggerInkAugmentsNetwork", RpcTarget.All, new object[]
		{
			(int)Trigger,
			num
		});
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00021C70 File Offset: 0x0001FE70
	[PunRPC]
	private void TriggerInkAugmentsNetwork(int trigger, int randomSeed)
	{
		base.StartCoroutine(this.AugmentTriggerOverTime((EventTrigger)trigger, randomSeed));
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00021C8E File Offset: 0x0001FE8E
	private IEnumerator AugmentTriggerOverTime(EventTrigger trigger, int seed)
	{
		Dictionary<SpawnType, List<SpawnPoint>> MapPoints = new Dictionary<SpawnType, List<SpawnPoint>>();
		System.Random rng = new System.Random(seed);
		foreach (SpawnType spawnType in SpawnType._.MapTypes())
		{
			List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(spawnType, EnemyLevel.None);
			if (allSpawns.Count > 0)
			{
				allSpawns.Sort((SpawnPoint x, SpawnPoint y) => x.GetHashCode().CompareTo(y.GetHashCode()));
				allSpawns.Shuffle(rng);
				MapPoints.Add(spawnType, allSpawns);
			}
		}
		List<ModSnippetNode> snippets = new List<ModSnippetNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			AugmentRootNode augment = augmentRootNode;
			int count = num;
			snippets.Clear();
			augment.CollectSnippets(ref snippets, trigger);
			if (snippets.Count != 0)
			{
				for (int i = 0; i < count; i = num + 1)
				{
					yield return new WaitForSeconds(0.5f);
					Vector3 position = Fountain.instance.transform.position;
					if (augment.SpawnType != SpawnType.None && MapPoints.ContainsKey(augment.SpawnType) && MapPoints[augment.SpawnType].Count > 0)
					{
						List<SpawnPoint> list = MapPoints[augment.SpawnType];
						num = list.Count - 1;
						position = list[num].transform.position;
						list.RemoveAt(list.Count - 1);
					}
					EffectProperties effectProperties = augment.RunLocally ? new EffectProperties(PlayerControl.myInstance) : new EffectProperties();
					effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(position, Vector3.up));
					effectProperties.SaveLocation("map_point", position);
					effectProperties.SourceType = ActionSource.Fountain;
					effectProperties.AbilityType = PlayerAbilityType.None;
					effectProperties.IsWorld = !augment.RunLocally;
					foreach (ModSnippetNode modSnippetNode in snippets)
					{
						modSnippetNode.TryTriggerFromProps(effectProperties, augment);
					}
					num = i;
				}
				augment = null;
			}
		}
		Dictionary<AugmentRootNode, int>.Enumerator enumerator2 = default(Dictionary<AugmentRootNode, int>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00021CA4 File Offset: 0x0001FEA4
	private void RunWorldAction()
	{
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00021CA6 File Offset: 0x0001FEA6
	public void TryAddBinding(string bindingID)
	{
		this.view.RPC("TryAddBindingNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.myInstance.ViewID,
			bindingID
		});
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00021CD5 File Offset: 0x0001FED5
	[PunRPC]
	public void TryAddBindingNetwork(int playerID, string bindingID)
	{
		if (BindingsPanel.instance.TryAddBindingMaster(playerID, bindingID))
		{
			this.SyncBindings();
		}
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00021CEB File Offset: 0x0001FEEB
	public void TryRemoveBinding(string bindingID)
	{
		this.view.RPC("TryRemoveBindingNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.myInstance.ViewID,
			bindingID
		});
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00021D1A File Offset: 0x0001FF1A
	[PunRPC]
	public void TryRemoveBindingNetwork(int playerID, string bindingID)
	{
		if (BindingsPanel.instance.TryRemoveBindingMaster(playerID, bindingID))
		{
			this.SyncBindings();
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00021D30 File Offset: 0x0001FF30
	public void TryRandomBindings(int level)
	{
		this.view.RPC("TryRandomBindingsNetwork", RpcTarget.MasterClient, new object[]
		{
			level
		});
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00021D52 File Offset: 0x0001FF52
	[PunRPC]
	public void TryRandomBindingsNetwork(int level)
	{
		BindingsPanel.instance.SetRandomBindings(level);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00021D5F File Offset: 0x0001FF5F
	public void TryLoadBindings(string bindingIDs)
	{
		this.view.RPC("TryLoadBindingsNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.myInstance.ViewID,
			bindingIDs
		});
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00021D8E File Offset: 0x0001FF8E
	[PunRPC]
	public void TryLoadBindingsNetwork(int playerID, string bindingIDs)
	{
		if (BindingsPanel.instance.TryLoadBindings(playerID, bindingIDs))
		{
			this.SyncBindings();
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00021DA4 File Offset: 0x0001FFA4
	public void SyncBindings()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		string bindingValues = BindingsPanel.instance.GetBindingValues();
		this.view.RPC("SyncBindingsNetwork", RpcTarget.All, new object[]
		{
			bindingValues
		});
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00021DDF File Offset: 0x0001FFDF
	[PunRPC]
	public void SyncBindingsNetwork(string encoded)
	{
		BindingsPanel.instance.SetBindingValues(encoded);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00021DEC File Offset: 0x0001FFEC
	public void RewardFontPagesOwed(int rewardCount)
	{
		for (int i = 0; i < rewardCount; i++)
		{
			this.AwardPlayerPage(null, null);
		}
		InkManager.FontPagesOwed = 0;
		InkManager.FontPageChosen.Clear();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			InkManager.FontPageChosen.Add(playerControl.view.OwnerActorNr, rewardCount);
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00021E74 File Offset: 0x00020074
	public void RewardPagePicked()
	{
		this.view.RPC("RewardPagePickedNetwork", RpcTarget.All, new object[]
		{
			PlayerControl.myInstance.view.OwnerActorNr
		});
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00021EA4 File Offset: 0x000200A4
	[PunRPC]
	private void RewardPagePickedNetwork(int playerID)
	{
		if (InkManager.FontPageChosen.ContainsKey(playerID))
		{
			Dictionary<int, int> fontPageChosen = InkManager.FontPageChosen;
			int num = playerID;
			int num2 = fontPageChosen[num];
			fontPageChosen[num] = num2 - 1;
		}
		bool flag = false;
		foreach (KeyValuePair<int, int> keyValuePair in InkManager.FontPageChosen)
		{
			int num;
			int num2;
			keyValuePair.Deconstruct(out num2, out num);
			if (num > 0)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			AugmentsPanel.TryClose();
			if (PhotonNetwork.IsMasterClient)
			{
				RewardManager.instance.NextReward();
			}
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00021F44 File Offset: 0x00020144
	public void SelectPauseQuote(GenreTree tome, int bindingLevel)
	{
		string text = LoreDB.SelectPauseQuoteID(tome, bindingLevel);
		this.view.RPC("SendPauseQuoteNetwork", RpcTarget.All, new object[]
		{
			text
		});
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00021F74 File Offset: 0x00020174
	[PunRPC]
	private void SendPauseQuoteNetwork(string ID)
	{
		this.PauseQuoteID = ID;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00021F7D File Offset: 0x0002017D
	private void OnGameStateChanged(GameState from, GameState to)
	{
		if (to != GameState.Hub)
		{
			if (to != GameState.InWave)
			{
				return;
			}
			this.StoreWaveStarted();
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00021F94 File Offset: 0x00020194
	public override void OnPlayerEnteredRoom(Player Player)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("SyncPagesOwed", Player, new object[]
		{
			InkManager.FontPagesOwed
		});
		this.view.RPC("SendPauseQuoteNetwork", Player, new object[]
		{
			this.PauseQuoteID
		});
		this.SyncStore();
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00021FF3 File Offset: 0x000201F3
	public override void OnPlayerLeftRoom(Player Player)
	{
		if (InkManager.FontPageChosen.ContainsKey(Player.ActorNumber))
		{
			InkManager.FontPageChosen.Remove(Player.ActorNumber);
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00022018 File Offset: 0x00020218
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(InkManager.Bank.Count);
			using (Dictionary<int, InkManager.PlayerInk>.Enumerator enumerator = InkManager.Bank.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, InkManager.PlayerInk> keyValuePair = enumerator.Current;
					int num;
					InkManager.PlayerInk playerInk;
					keyValuePair.Deconstruct(out num, out playerInk);
					InkManager.PlayerInk playerInk2 = playerInk;
					stream.SendNext(playerInk2.ActorID);
					stream.SendNext(playerInk2.Amount);
				}
				return;
			}
		}
		InkManager.Bank.Clear();
		float num2 = (float)((int)stream.ReceiveNext());
		int num3 = 0;
		while ((float)num3 < num2)
		{
			InkManager.PlayerInk playerInk3 = new InkManager.PlayerInk();
			playerInk3.ActorID = (int)stream.ReceiveNext();
			playerInk3.Amount = (int)stream.ReceiveNext();
			InkManager.Bank.Add(playerInk3.ActorID, playerInk3);
			num3++;
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00022118 File Offset: 0x00020318
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0002213A File Offset: 0x0002033A
	public InkManager()
	{
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00022142 File Offset: 0x00020342
	// Note: this type is marked as 'beforefieldinit'.
	static InkManager()
	{
	}

	// Token: 0x040003A7 RID: 935
	public static InkManager instance;

	// Token: 0x040003A8 RID: 936
	private static Dictionary<int, InkManager.PlayerInk> Bank = new Dictionary<int, InkManager.PlayerInk>();

	// Token: 0x040003A9 RID: 937
	private PhotonView view;

	// Token: 0x040003AA RID: 938
	private const int NUM_OPTIONS = 3;

	// Token: 0x040003AB RID: 939
	public static List<InkRow> Store = new List<InkRow>();

	// Token: 0x040003AC RID: 940
	public static Augments PurchasedMods = new Augments();

	// Token: 0x040003AD RID: 941
	public static int FontPagesOwed = 0;

	// Token: 0x040003AE RID: 942
	public static Dictionary<int, int> FontPageChosen = new Dictionary<int, int>();

	// Token: 0x040003AF RID: 943
	public string PauseQuoteID;

	// Token: 0x040003B0 RID: 944
	public static bool NewRowAvailable;

	// Token: 0x040003B1 RID: 945
	private int curLayersAvailable;

	// Token: 0x02000490 RID: 1168
	public class PlayerInk
	{
		// Token: 0x060021EE RID: 8686 RVA: 0x000C4679 File Offset: 0x000C2879
		public PlayerInk()
		{
		}

		// Token: 0x04002329 RID: 9001
		public int ActorID;

		// Token: 0x0400232A RID: 9002
		public int Amount;

		// Token: 0x0400232B RID: 9003
		public bool InUse;

		// Token: 0x0400232C RID: 9004
		public string UserID;
	}

	// Token: 0x02000491 RID: 1169
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x000C4681 File Offset: 0x000C2881
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000C468D File Offset: 0x000C288D
		public <>c()
		{
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000C4698 File Offset: 0x000C2898
		internal int <AugmentTriggerOverTime>b__49_0(SpawnPoint x, SpawnPoint y)
		{
			return x.GetHashCode().CompareTo(y.GetHashCode());
		}

		// Token: 0x0400232D RID: 9005
		public static readonly InkManager.<>c <>9 = new InkManager.<>c();

		// Token: 0x0400232E RID: 9006
		public static Comparison<SpawnPoint> <>9__49_0;
	}

	// Token: 0x02000492 RID: 1170
	[CompilerGenerated]
	private sealed class <AugmentTriggerOverTime>d__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021F2 RID: 8690 RVA: 0x000C46B9 File Offset: 0x000C28B9
		[DebuggerHidden]
		public <AugmentTriggerOverTime>d__49(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000C46C8 File Offset: 0x000C28C8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000C4700 File Offset: 0x000C2900
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					Vector3 position = Fountain.instance.transform.position;
					int num2;
					if (augment.SpawnType != SpawnType.None && MapPoints.ContainsKey(augment.SpawnType) && MapPoints[augment.SpawnType].Count > 0)
					{
						List<SpawnPoint> list = MapPoints[augment.SpawnType];
						num2 = list.Count - 1;
						position = list[num2].transform.position;
						list.RemoveAt(list.Count - 1);
					}
					EffectProperties effectProperties = augment.RunLocally ? new EffectProperties(PlayerControl.myInstance) : new EffectProperties();
					effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(position, Vector3.up));
					effectProperties.SaveLocation("map_point", position);
					effectProperties.SourceType = ActionSource.Fountain;
					effectProperties.AbilityType = PlayerAbilityType.None;
					effectProperties.IsWorld = !augment.RunLocally;
					foreach (ModSnippetNode modSnippetNode in snippets)
					{
						modSnippetNode.TryTriggerFromProps(effectProperties, augment);
					}
					num2 = i;
					i = num2 + 1;
					goto IL_2C8;
				}
				else
				{
					this.<>1__state = -1;
					MapPoints = new Dictionary<SpawnType, List<SpawnPoint>>();
					System.Random rng = new System.Random(seed);
					foreach (SpawnType spawnType in SpawnType._.MapTypes())
					{
						List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(spawnType, EnemyLevel.None);
						if (allSpawns.Count > 0)
						{
							allSpawns.Sort(new Comparison<SpawnPoint>(InkManager.<>c.<>9.<AugmentTriggerOverTime>b__49_0));
							allSpawns.Shuffle(rng);
							MapPoints.Add(spawnType, allSpawns);
						}
					}
					snippets = new List<ModSnippetNode>();
					enumerator2 = InkManager.PurchasedMods.trees.GetEnumerator();
					this.<>1__state = -3;
				}
				IL_2E0:
				while (enumerator2.MoveNext())
				{
					KeyValuePair<AugmentRootNode, int> keyValuePair = enumerator2.Current;
					int num2;
					AugmentRootNode augmentRootNode;
					keyValuePair.Deconstruct(out augmentRootNode, out num2);
					augment = augmentRootNode;
					count = num2;
					snippets.Clear();
					augment.CollectSnippets(ref snippets, trigger);
					if (snippets.Count != 0)
					{
						i = 0;
						goto IL_2C8;
					}
				}
				this.<>m__Finally1();
				enumerator2 = default(Dictionary<AugmentRootNode, int>.Enumerator);
				return false;
				IL_2C8:
				if (i >= count)
				{
					augment = null;
					goto IL_2E0;
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				result = true;
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000C4A68 File Offset: 0x000C2C68
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator2).Dispose();
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000C4A82 File Offset: 0x000C2C82
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000C4A8A File Offset: 0x000C2C8A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000C4A91 File Offset: 0x000C2C91
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400232F RID: 9007
		private int <>1__state;

		// Token: 0x04002330 RID: 9008
		private object <>2__current;

		// Token: 0x04002331 RID: 9009
		public int seed;

		// Token: 0x04002332 RID: 9010
		public EventTrigger trigger;

		// Token: 0x04002333 RID: 9011
		private Dictionary<SpawnType, List<SpawnPoint>> <MapPoints>5__2;

		// Token: 0x04002334 RID: 9012
		private List<ModSnippetNode> <snippets>5__3;

		// Token: 0x04002335 RID: 9013
		private Dictionary<AugmentRootNode, int>.Enumerator <>7__wrap3;

		// Token: 0x04002336 RID: 9014
		private AugmentRootNode <augment>5__5;

		// Token: 0x04002337 RID: 9015
		private int <count>5__6;

		// Token: 0x04002338 RID: 9016
		private int <i>5__7;
	}
}
