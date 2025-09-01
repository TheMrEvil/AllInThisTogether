using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Photon.Pun
{
	// Token: 0x02000014 RID: 20
	[AddComponentMenu("Photon Networking/Photon View")]
	public class PhotonView : MonoBehaviour
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007D84 File Offset: 0x00005F84
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00007DA7 File Offset: 0x00005FA7
		public int Prefix
		{
			get
			{
				if (this.prefixField == -1 && PhotonNetwork.NetworkingClient != null)
				{
					this.prefixField = (int)PhotonNetwork.currentLevelPrefix;
				}
				return this.prefixField;
			}
			set
			{
				this.prefixField = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00007DB0 File Offset: 0x00005FB0
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public object[] InstantiationData
		{
			get
			{
				return this.instantiationDataField;
			}
			protected internal set
			{
				this.instantiationDataField = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007DC1 File Offset: 0x00005FC1
		[Obsolete("Renamed. Use IsRoomView instead")]
		public bool IsSceneView
		{
			get
			{
				return this.IsRoomView;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007DC9 File Offset: 0x00005FC9
		public bool IsRoomView
		{
			get
			{
				return this.CreatorActorNr == 0;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public bool IsOwnerActive
		{
			get
			{
				return this.Owner != null && !this.Owner.IsInactive;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007DEE File Offset: 0x00005FEE
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007DF6 File Offset: 0x00005FF6
		public bool IsMine
		{
			[CompilerGenerated]
			get
			{
				return this.<IsMine>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsMine>k__BackingField = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007DFF File Offset: 0x00005FFF
		public bool AmController
		{
			get
			{
				return this.IsMine;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007E07 File Offset: 0x00006007
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007E0F File Offset: 0x0000600F
		public Player Controller
		{
			[CompilerGenerated]
			get
			{
				return this.<Controller>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Controller>k__BackingField = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007E18 File Offset: 0x00006018
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00007E20 File Offset: 0x00006020
		public int CreatorActorNr
		{
			[CompilerGenerated]
			get
			{
				return this.<CreatorActorNr>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CreatorActorNr>k__BackingField = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007E29 File Offset: 0x00006029
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00007E31 File Offset: 0x00006031
		public bool AmOwner
		{
			[CompilerGenerated]
			get
			{
				return this.<AmOwner>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AmOwner>k__BackingField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007E3A File Offset: 0x0000603A
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00007E42 File Offset: 0x00006042
		public Player Owner
		{
			[CompilerGenerated]
			get
			{
				return this.<Owner>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Owner>k__BackingField = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007E4B File Offset: 0x0000604B
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007E54 File Offset: 0x00006054
		public int OwnerActorNr
		{
			get
			{
				return this.ownerActorNr;
			}
			set
			{
				if (value != 0 && this.ownerActorNr == value)
				{
					return;
				}
				Player owner = this.Owner;
				this.Owner = ((PhotonNetwork.CurrentRoom == null) ? null : PhotonNetwork.CurrentRoom.GetPlayer(value, true));
				this.ownerActorNr = ((this.Owner != null) ? this.Owner.ActorNumber : value);
				this.AmOwner = (PhotonNetwork.LocalPlayer != null && this.ownerActorNr == PhotonNetwork.LocalPlayer.ActorNumber);
				this.UpdateCallbackLists();
				if (this.OnOwnerChangeCallbacks != null)
				{
					int i = 0;
					int count = this.OnOwnerChangeCallbacks.Count;
					while (i < count)
					{
						this.OnOwnerChangeCallbacks[i].OnOwnerChange(this.Owner, owner);
						i++;
					}
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007F0D File Offset: 0x0000610D
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00007F18 File Offset: 0x00006118
		public int ControllerActorNr
		{
			get
			{
				return this.controllerActorNr;
			}
			set
			{
				Player controller = this.Controller;
				this.Controller = ((PhotonNetwork.CurrentRoom == null) ? null : PhotonNetwork.CurrentRoom.GetPlayer(value, true));
				if (this.Controller != null && this.Controller.IsInactive)
				{
					this.Controller = PhotonNetwork.MasterClient;
				}
				this.controllerActorNr = ((this.Controller != null) ? this.Controller.ActorNumber : value);
				this.IsMine = (PhotonNetwork.LocalPlayer != null && this.controllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber);
				if (this.Controller != controller)
				{
					this.UpdateCallbackLists();
					if (this.OnControllerChangeCallbacks != null)
					{
						int i = 0;
						int count = this.OnControllerChangeCallbacks.Count;
						while (i < count)
						{
							this.OnControllerChangeCallbacks[i].OnControllerChange(this.Controller, controller);
							i++;
						}
					}
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007FED File Offset: 0x000061ED
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00007FF8 File Offset: 0x000061F8
		public int ViewID
		{
			get
			{
				return this.viewIdField;
			}
			set
			{
				if (value != 0 && this.viewIdField != 0)
				{
					Debug.LogWarning("Changing a ViewID while it's in use is not possible (except setting it to 0 (not being used). Current ViewID: " + this.viewIdField.ToString());
					return;
				}
				if (value == 0 && this.viewIdField != 0)
				{
					PhotonNetwork.LocalCleanPhotonView(this);
				}
				this.viewIdField = value;
				this.CreatorActorNr = value / PhotonNetwork.MAX_VIEW_IDS;
				this.OwnerActorNr = this.CreatorActorNr;
				this.ControllerActorNr = this.CreatorActorNr;
				this.RebuildControllerCache(false);
				if (value != 0)
				{
					PhotonNetwork.RegisterPhotonView(this);
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008079 File Offset: 0x00006279
		protected internal void Awake()
		{
			if (this.ViewID != 0)
			{
				return;
			}
			if (this.sceneViewId != 0)
			{
				this.ViewID = this.sceneViewId;
			}
			this.FindObservables(false);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000809F File Offset: 0x0000629F
		internal void ResetPhotonView(bool resetOwner)
		{
			this.lastOnSerializeDataSent = null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000080A8 File Offset: 0x000062A8
		internal void RebuildControllerCache(bool ownerHasChanged = false)
		{
			if (this.controllerActorNr == 0 || this.OwnerActorNr == 0 || this.Owner == null || this.Owner.IsInactive)
			{
				Player masterClient = PhotonNetwork.MasterClient;
				this.ControllerActorNr = ((masterClient == null) ? -1 : masterClient.ActorNumber);
				return;
			}
			this.ControllerActorNr = this.OwnerActorNr;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008100 File Offset: 0x00006300
		public void OnPreNetDestroy(PhotonView rootView)
		{
			this.UpdateCallbackLists();
			if (this.OnPreNetDestroyCallbacks != null)
			{
				int i = 0;
				int count = this.OnPreNetDestroyCallbacks.Count;
				while (i < count)
				{
					this.OnPreNetDestroyCallbacks[i].OnPreNetDestroy(rootView);
					i++;
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008148 File Offset: 0x00006348
		protected internal void OnDestroy()
		{
			if (!this.removedFromLocalViewList && PhotonNetwork.LocalCleanPhotonView(this) && this.InstantiationId > 0 && !ConnectionHandler.AppQuits && PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				Debug.Log("PUN-instantiated '" + base.gameObject.name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy().");
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000819C File Offset: 0x0000639C
		public void RequestOwnership()
		{
			if (this.OwnershipTransfer != OwnershipOption.Fixed)
			{
				PhotonNetwork.RequestOwnership(this.ViewID, this.ownerActorNr);
				return;
			}
			if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Attempting to RequestOwnership of GameObject '",
					base.name,
					"' viewId: ",
					this.ViewID.ToString(),
					", but PhotonView.OwnershipTransfer is set to Fixed."
				}));
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008210 File Offset: 0x00006410
		public void TransferOwnership(Player newOwner)
		{
			if (newOwner != null)
			{
				this.TransferOwnership(newOwner.ActorNumber);
				return;
			}
			if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Attempting to TransferOwnership of GameObject '",
					base.name,
					"' viewId: ",
					this.ViewID.ToString(),
					", but provided Player newOwner is null."
				}));
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008278 File Offset: 0x00006478
		public void TransferOwnership(int newOwnerId)
		{
			if (this.OwnershipTransfer == OwnershipOption.Takeover || (this.OwnershipTransfer == OwnershipOption.Request && this.AmController))
			{
				PhotonNetwork.TransferOwnership(this.ViewID, newOwnerId);
				return;
			}
			if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				if (this.OwnershipTransfer == OwnershipOption.Fixed)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Attempting to TransferOwnership of GameObject '",
						base.name,
						"' viewId: ",
						this.ViewID.ToString(),
						" without the authority to do so. TransferOwnership is not allowed if PhotonView.OwnershipTransfer is set to Fixed."
					}));
					return;
				}
				if (this.OwnershipTransfer == OwnershipOption.Request)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Attempting to TransferOwnership of GameObject '",
						base.name,
						"' viewId: ",
						this.ViewID.ToString(),
						" without the authority to do so. PhotonView.OwnershipTransfer is set to Request, so only the controller of this object can TransferOwnership."
					}));
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008350 File Offset: 0x00006550
		public void FindObservables(bool force = false)
		{
			if (!force && this.observableSearch == PhotonView.ObservableSearch.Manual)
			{
				return;
			}
			if (this.ObservedComponents == null)
			{
				this.ObservedComponents = new List<Component>();
			}
			else
			{
				this.ObservedComponents.Clear();
			}
			base.transform.GetNestedComponentsInChildren(force || this.observableSearch == PhotonView.ObservableSearch.AutoFindAll, this.ObservedComponents);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000083AC File Offset: 0x000065AC
		public void SerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
			{
				for (int i = 0; i < this.ObservedComponents.Count; i++)
				{
					if (this.ObservedComponents[i] != null)
					{
						this.SerializeComponent(this.ObservedComponents[i], stream, info);
					}
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008410 File Offset: 0x00006610
		public void DeserializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
			{
				for (int i = 0; i < this.ObservedComponents.Count; i++)
				{
					Component component = this.ObservedComponents[i];
					if (component != null)
					{
						this.DeserializeComponent(component, stream, info);
					}
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008468 File Offset: 0x00006668
		protected internal void DeserializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
		{
			IPunObservable punObservable = component as IPunObservable;
			if (punObservable != null)
			{
				punObservable.OnPhotonSerializeView(stream, info);
				return;
			}
			string str = "Observed scripts have to implement IPunObservable. ";
			string str2 = (component != null) ? component.ToString() : null;
			string str3 = " does not. It is Type: ";
			Type type = component.GetType();
			Debug.LogError(str + str2 + str3 + ((type != null) ? type.ToString() : null), component.gameObject);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000084C4 File Offset: 0x000066C4
		protected internal void SerializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
		{
			IPunObservable punObservable = component as IPunObservable;
			if (punObservable != null)
			{
				punObservable.OnPhotonSerializeView(stream, info);
				return;
			}
			string str = "Observed scripts have to implement IPunObservable. ";
			string str2 = (component != null) ? component.ToString() : null;
			string str3 = " does not. It is Type: ";
			Type type = component.GetType();
			Debug.LogError(str + str2 + str3 + ((type != null) ? type.ToString() : null), component.gameObject);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000851D File Offset: 0x0000671D
		public void RefreshRpcMonoBehaviourCache()
		{
			this.RpcMonoBehaviours = base.GetComponents<MonoBehaviour>();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000852B File Offset: 0x0000672B
		public void RPC(string methodName, RpcTarget target, params object[] parameters)
		{
			PhotonNetwork.RPC(this, methodName, target, false, parameters);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008537 File Offset: 0x00006737
		public void RpcSecure(string methodName, RpcTarget target, bool encrypt, params object[] parameters)
		{
			PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008544 File Offset: 0x00006744
		public void RPC(string methodName, Player targetPlayer, params object[] parameters)
		{
			PhotonNetwork.RPC(this, methodName, targetPlayer, false, parameters);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008550 File Offset: 0x00006750
		public void RpcSecure(string methodName, Player targetPlayer, bool encrypt, params object[] parameters)
		{
			PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000855D File Offset: 0x0000675D
		public static PhotonView Get(Component component)
		{
			return component.transform.GetParentComponent<PhotonView>();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000856A File Offset: 0x0000676A
		public static PhotonView Get(GameObject gameObj)
		{
			return gameObj.transform.GetParentComponent<PhotonView>();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008577 File Offset: 0x00006777
		public static PhotonView Find(int viewID)
		{
			return PhotonNetwork.GetPhotonView(viewID);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000857F File Offset: 0x0000677F
		public void AddCallbackTarget(IPhotonViewCallback obj)
		{
			this.CallbackChangeQueue.Enqueue(new PhotonView.CallbackTargetChange(obj, null, true));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008594 File Offset: 0x00006794
		public void RemoveCallbackTarget(IPhotonViewCallback obj)
		{
			this.CallbackChangeQueue.Enqueue(new PhotonView.CallbackTargetChange(obj, null, false));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000085A9 File Offset: 0x000067A9
		public void AddCallback<T>(IPhotonViewCallback obj) where T : class, IPhotonViewCallback
		{
			this.CallbackChangeQueue.Enqueue(new PhotonView.CallbackTargetChange(obj, typeof(T), true));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000085C7 File Offset: 0x000067C7
		public void RemoveCallback<T>(IPhotonViewCallback obj) where T : class, IPhotonViewCallback
		{
			this.CallbackChangeQueue.Enqueue(new PhotonView.CallbackTargetChange(obj, typeof(T), false));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000085E8 File Offset: 0x000067E8
		private void UpdateCallbackLists()
		{
			while (this.CallbackChangeQueue.Count > 0)
			{
				PhotonView.CallbackTargetChange callbackTargetChange = this.CallbackChangeQueue.Dequeue();
				IPhotonViewCallback obj = callbackTargetChange.obj;
				Type type = callbackTargetChange.type;
				bool add = callbackTargetChange.add;
				if (type == null)
				{
					this.TryRegisterCallback<IOnPhotonViewPreNetDestroy>(obj, ref this.OnPreNetDestroyCallbacks, add);
					this.TryRegisterCallback<IOnPhotonViewOwnerChange>(obj, ref this.OnOwnerChangeCallbacks, add);
					this.TryRegisterCallback<IOnPhotonViewControllerChange>(obj, ref this.OnControllerChangeCallbacks, add);
				}
				else if (type == typeof(IOnPhotonViewPreNetDestroy))
				{
					this.RegisterCallback<IOnPhotonViewPreNetDestroy>(obj as IOnPhotonViewPreNetDestroy, ref this.OnPreNetDestroyCallbacks, add);
				}
				else if (type == typeof(IOnPhotonViewOwnerChange))
				{
					this.RegisterCallback<IOnPhotonViewOwnerChange>(obj as IOnPhotonViewOwnerChange, ref this.OnOwnerChangeCallbacks, add);
				}
				else if (type == typeof(IOnPhotonViewControllerChange))
				{
					this.RegisterCallback<IOnPhotonViewControllerChange>(obj as IOnPhotonViewControllerChange, ref this.OnControllerChangeCallbacks, add);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000086D4 File Offset: 0x000068D4
		private void TryRegisterCallback<T>(IPhotonViewCallback obj, ref List<T> list, bool add) where T : class, IPhotonViewCallback
		{
			T t = obj as T;
			if (t != null)
			{
				this.RegisterCallback<T>(t, ref list, add);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000086FE File Offset: 0x000068FE
		private void RegisterCallback<T>(T obj, ref List<T> list, bool add) where T : class, IPhotonViewCallback
		{
			if (list == null)
			{
				list = new List<T>();
			}
			if (add)
			{
				if (!list.Contains(obj))
				{
					list.Add(obj);
					return;
				}
			}
			else if (list.Contains(obj))
			{
				list.Remove(obj);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008734 File Offset: 0x00006934
		public override string ToString()
		{
			return string.Format("View {0}{3} on {1} {2}", new object[]
			{
				this.ViewID,
				(base.gameObject != null) ? base.gameObject.name : "GO==null",
				this.IsRoomView ? "(scene)" : string.Empty,
				(this.Prefix > 0) ? ("lvl" + this.Prefix.ToString()) : ""
			});
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000087C4 File Offset: 0x000069C4
		public PhotonView()
		{
		}

		// Token: 0x04000088 RID: 136
		[FormerlySerializedAs("group")]
		public byte Group;

		// Token: 0x04000089 RID: 137
		[FormerlySerializedAs("prefixBackup")]
		public int prefixField = -1;

		// Token: 0x0400008A RID: 138
		internal object[] instantiationDataField;

		// Token: 0x0400008B RID: 139
		protected internal List<object> lastOnSerializeDataSent;

		// Token: 0x0400008C RID: 140
		protected internal List<object> syncValues;

		// Token: 0x0400008D RID: 141
		protected internal object[] lastOnSerializeDataReceived;

		// Token: 0x0400008E RID: 142
		[FormerlySerializedAs("synchronization")]
		public ViewSynchronization Synchronization = ViewSynchronization.UnreliableOnChange;

		// Token: 0x0400008F RID: 143
		protected internal bool mixedModeIsReliable;

		// Token: 0x04000090 RID: 144
		[FormerlySerializedAs("ownershipTransfer")]
		public OwnershipOption OwnershipTransfer;

		// Token: 0x04000091 RID: 145
		public PhotonView.ObservableSearch observableSearch;

		// Token: 0x04000092 RID: 146
		public List<Component> ObservedComponents;

		// Token: 0x04000093 RID: 147
		internal MonoBehaviour[] RpcMonoBehaviours;

		// Token: 0x04000094 RID: 148
		[CompilerGenerated]
		private bool <IsMine>k__BackingField;

		// Token: 0x04000095 RID: 149
		[CompilerGenerated]
		private Player <Controller>k__BackingField;

		// Token: 0x04000096 RID: 150
		[CompilerGenerated]
		private int <CreatorActorNr>k__BackingField;

		// Token: 0x04000097 RID: 151
		[CompilerGenerated]
		private bool <AmOwner>k__BackingField;

		// Token: 0x04000098 RID: 152
		[CompilerGenerated]
		private Player <Owner>k__BackingField;

		// Token: 0x04000099 RID: 153
		[NonSerialized]
		private int ownerActorNr;

		// Token: 0x0400009A RID: 154
		[NonSerialized]
		private int controllerActorNr;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		[FormerlySerializedAs("viewIdField")]
		[HideInInspector]
		public int sceneViewId;

		// Token: 0x0400009C RID: 156
		[NonSerialized]
		private int viewIdField;

		// Token: 0x0400009D RID: 157
		[FormerlySerializedAs("instantiationId")]
		public int InstantiationId;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		[HideInInspector]
		public bool isRuntimeInstantiated;

		// Token: 0x0400009F RID: 159
		protected internal bool removedFromLocalViewList;

		// Token: 0x040000A0 RID: 160
		private Queue<PhotonView.CallbackTargetChange> CallbackChangeQueue = new Queue<PhotonView.CallbackTargetChange>();

		// Token: 0x040000A1 RID: 161
		private List<IOnPhotonViewPreNetDestroy> OnPreNetDestroyCallbacks;

		// Token: 0x040000A2 RID: 162
		private List<IOnPhotonViewOwnerChange> OnOwnerChangeCallbacks;

		// Token: 0x040000A3 RID: 163
		private List<IOnPhotonViewControllerChange> OnControllerChangeCallbacks;

		// Token: 0x0200002F RID: 47
		public enum ObservableSearch
		{
			// Token: 0x04000121 RID: 289
			Manual,
			// Token: 0x04000122 RID: 290
			AutoFindActive,
			// Token: 0x04000123 RID: 291
			AutoFindAll
		}

		// Token: 0x02000030 RID: 48
		private struct CallbackTargetChange
		{
			// Token: 0x060001CB RID: 459 RVA: 0x0000B525 File Offset: 0x00009725
			public CallbackTargetChange(IPhotonViewCallback obj, Type type, bool add)
			{
				this.obj = obj;
				this.type = type;
				this.add = add;
			}

			// Token: 0x04000124 RID: 292
			public IPhotonViewCallback obj;

			// Token: 0x04000125 RID: 293
			public Type type;

			// Token: 0x04000126 RID: 294
			public bool add;
		}
	}
}
