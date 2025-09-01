using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Photon.Pun
{
	// Token: 0x02000010 RID: 16
	public class PhotonHandler : ConnectionHandler, IInRoomCallbacks, IMatchmakingCallbacks
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002168 File Offset: 0x00000368
		internal static PhotonHandler Instance
		{
			get
			{
				if (PhotonHandler.instance == null)
				{
					PhotonHandler.instance = UnityEngine.Object.FindObjectOfType<PhotonHandler>();
					if (PhotonHandler.instance == null)
					{
						PhotonHandler.instance = new GameObject
						{
							name = "PhotonMono"
						}.AddComponent<PhotonHandler>();
					}
				}
				return PhotonHandler.instance;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		protected override void Awake()
		{
			this.swSendOutgoing.Start();
			this.swViewUpdate.Start();
			if (PhotonHandler.instance == null || this == PhotonHandler.instance)
			{
				PhotonHandler.instance = this;
				base.Awake();
				return;
			}
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F8 File Offset: 0x000003F8
		protected virtual void OnEnable()
		{
			if (PhotonHandler.Instance != this)
			{
				UnityEngine.Debug.LogError("PhotonHandler is a singleton but there are multiple instances. this != Instance.");
				return;
			}
			base.Client = PhotonNetwork.NetworkingClient;
			if (PhotonNetwork.PhotonServerSettings.EnableSupportLogger)
			{
				SupportLogger supportLogger = base.gameObject.GetComponent<SupportLogger>();
				if (supportLogger == null)
				{
					supportLogger = base.gameObject.AddComponent<SupportLogger>();
				}
				if (this.supportLoggerComponent != null && supportLogger.GetInstanceID() != this.supportLoggerComponent.GetInstanceID())
				{
					UnityEngine.Debug.LogWarningFormat("Cached SupportLogger component is different from the one attached to PhotonMono GameObject", Array.Empty<object>());
				}
				this.supportLoggerComponent = supportLogger;
				this.supportLoggerComponent.Client = PhotonNetwork.NetworkingClient;
			}
			this.UpdateInterval = 1000 / PhotonNetwork.SendRate;
			this.UpdateIntervalOnSerialize = 1000 / PhotonNetwork.SerializationRate;
			PhotonNetwork.AddCallbackTarget(this);
			base.StartFallbackSendAckThread();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022CA File Offset: 0x000004CA
		protected void Start()
		{
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode loadingMode)
			{
				PhotonNetwork.NewSceneLoaded();
			};
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022F0 File Offset: 0x000004F0
		protected override void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
			base.OnDisable();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022FE File Offset: 0x000004FE
		protected void FixedUpdate()
		{
			if (Time.timeScale > PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate)
			{
				this.Dispatch();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		protected void LateUpdate()
		{
			if (Time.timeScale <= PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate)
			{
				this.Dispatch();
			}
			if (PhotonNetwork.IsMessageQueueRunning && this.swViewUpdate.ElapsedMilliseconds >= (long)(this.UpdateIntervalOnSerialize - 8))
			{
				PhotonNetwork.RunViewUpdate();
				this.swViewUpdate.Restart();
				PhotonHandler.SendAsap = true;
			}
			if (PhotonHandler.SendAsap || this.swSendOutgoing.ElapsedMilliseconds >= (long)this.UpdateInterval)
			{
				PhotonHandler.SendAsap = false;
				bool flag = true;
				int num = 0;
				while (PhotonNetwork.IsMessageQueueRunning && flag && num < PhotonHandler.MaxDatagrams)
				{
					flag = PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
					num++;
				}
				if (num >= PhotonHandler.MaxDatagrams)
				{
					PhotonHandler.SendAsap = true;
				}
				this.swSendOutgoing.Restart();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023CC File Offset: 0x000005CC
		protected void Dispatch()
		{
			if (PhotonNetwork.NetworkingClient == null)
			{
				UnityEngine.Debug.LogError("NetworkPeer broke!");
				return;
			}
			bool flag = true;
			Exception ex = null;
			int num = 0;
			while (PhotonNetwork.IsMessageQueueRunning && flag)
			{
				try
				{
					flag = PhotonNetwork.NetworkingClient.LoadBalancingPeer.DispatchIncomingCommands();
				}
				catch (Exception ex2)
				{
					num++;
					if (ex == null)
					{
						ex = ex2;
					}
				}
			}
			if (ex != null)
			{
				throw new AggregateException("Caught " + num.ToString() + " exception(s) in methods called by DispatchIncomingCommands(). Rethrowing first only (see above).", ex);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000244C File Offset: 0x0000064C
		public void OnCreatedRoom()
		{
			PhotonNetwork.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002458 File Offset: 0x00000658
		public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
			PhotonNetwork.LoadLevelIfSynced();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000245F File Offset: 0x0000065F
		public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002464 File Offset: 0x00000664
		public void OnMasterClientSwitched(Player newMasterClient)
		{
			foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
			{
				if (photonView.IsRoomView)
				{
					photonView.OwnerActorNr = newMasterClient.ActorNumber;
					photonView.ControllerActorNr = newMasterClient.ActorNumber;
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D4 File Offset: 0x000006D4
		public void OnFriendListUpdate(List<FriendInfo> friendList)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024D6 File Offset: 0x000006D6
		public void OnCreateRoomFailed(short returnCode, string message)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024D8 File Offset: 0x000006D8
		public void OnJoinRoomFailed(short returnCode, string message)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024DA File Offset: 0x000006DA
		public void OnJoinRandomFailed(short returnCode, string message)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024DC File Offset: 0x000006DC
		public void OnJoinedRoom()
		{
			if (PhotonNetwork.ViewCount == 0)
			{
				return;
			}
			NonAllocDictionary<int, PhotonView>.ValueIterator photonViewCollection = PhotonNetwork.PhotonViewCollection;
			bool flag = PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1;
			if (flag)
			{
				this.reusableIntList.Clear();
			}
			foreach (PhotonView photonView in photonViewCollection)
			{
				int ownerActorNr = photonView.OwnerActorNr;
				int creatorActorNr = photonView.CreatorActorNr;
				photonView.RebuildControllerCache(false);
				if (flag && ownerActorNr != creatorActorNr)
				{
					this.reusableIntList.Add(photonView.ViewID);
					this.reusableIntList.Add(ownerActorNr);
				}
			}
			if (flag && this.reusableIntList.Count > 0)
			{
				PhotonNetwork.OwnershipUpdate(this.reusableIntList.ToArray(), -1);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025B8 File Offset: 0x000007B8
		public void OnLeftRoom()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025BC File Offset: 0x000007BC
		public void OnPlayerEnteredRoom(Player newPlayer)
		{
			bool isMasterClient = PhotonNetwork.IsMasterClient;
			NonAllocDictionary<int, PhotonView>.ValueIterator photonViewCollection = PhotonNetwork.PhotonViewCollection;
			if (isMasterClient)
			{
				this.reusableIntList.Clear();
			}
			foreach (PhotonView photonView in photonViewCollection)
			{
				photonView.RebuildControllerCache(false);
				if (isMasterClient)
				{
					int ownerActorNr = photonView.OwnerActorNr;
					if (ownerActorNr != photonView.CreatorActorNr)
					{
						this.reusableIntList.Add(photonView.ViewID);
						this.reusableIntList.Add(ownerActorNr);
					}
				}
			}
			if (isMasterClient && this.reusableIntList.Count > 0)
			{
				PhotonNetwork.OwnershipUpdate(this.reusableIntList.ToArray(), newPlayer.ActorNumber);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002680 File Offset: 0x00000880
		public void OnPlayerLeftRoom(Player otherPlayer)
		{
			NonAllocDictionary<int, PhotonView>.ValueIterator photonViewCollection = PhotonNetwork.PhotonViewCollection;
			int actorNumber = otherPlayer.ActorNumber;
			if (otherPlayer.IsInactive)
			{
				using (NonAllocDictionary<int, PhotonView>.ValueIterator enumerator = photonViewCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PhotonView photonView = enumerator.Current;
						if (photonView.ControllerActorNr == actorNumber)
						{
							photonView.ControllerActorNr = PhotonNetwork.MasterClient.ActorNumber;
						}
					}
					return;
				}
			}
			bool autoCleanUp = PhotonNetwork.CurrentRoom.AutoCleanUp;
			foreach (PhotonView photonView2 in photonViewCollection)
			{
				if ((!autoCleanUp || photonView2.CreatorActorNr != actorNumber) && (photonView2.OwnerActorNr == actorNumber || photonView2.ControllerActorNr == actorNumber))
				{
					photonView2.OwnerActorNr = 0;
					photonView2.ControllerActorNr = PhotonNetwork.MasterClient.ActorNumber;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002778 File Offset: 0x00000978
		public PhotonHandler()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027A1 File Offset: 0x000009A1
		// Note: this type is marked as 'beforefieldinit'.
		static PhotonHandler()
		{
		}

		// Token: 0x0400001C RID: 28
		private static PhotonHandler instance;

		// Token: 0x0400001D RID: 29
		public static int MaxDatagrams = 10;

		// Token: 0x0400001E RID: 30
		public static bool SendAsap;

		// Token: 0x0400001F RID: 31
		private const int SerializeRateFrameCorrection = 8;

		// Token: 0x04000020 RID: 32
		protected internal int UpdateInterval;

		// Token: 0x04000021 RID: 33
		protected internal int UpdateIntervalOnSerialize;

		// Token: 0x04000022 RID: 34
		private readonly Stopwatch swSendOutgoing = new Stopwatch();

		// Token: 0x04000023 RID: 35
		private readonly Stopwatch swViewUpdate = new Stopwatch();

		// Token: 0x04000024 RID: 36
		private SupportLogger supportLoggerComponent;

		// Token: 0x04000025 RID: 37
		protected List<int> reusableIntList = new List<int>();

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060001B9 RID: 441 RVA: 0x0000B356 File Offset: 0x00009556
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060001BA RID: 442 RVA: 0x0000B362 File Offset: 0x00009562
			public <>c()
			{
			}

			// Token: 0x060001BB RID: 443 RVA: 0x0000B36A File Offset: 0x0000956A
			internal void <Start>b__13_0(Scene scene, LoadSceneMode loadingMode)
			{
				PhotonNetwork.NewSceneLoaded();
			}

			// Token: 0x04000113 RID: 275
			public static readonly PhotonHandler.<>c <>9 = new PhotonHandler.<>c();

			// Token: 0x04000114 RID: 276
			public static UnityAction<Scene, LoadSceneMode> <>9__13_0;
		}
	}
}
