using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x0200038B RID: 907
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x00032104 File Offset: 0x00030304
		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection result;
				if (flag)
				{
					result = PlayerConnection.CreateInstance();
				}
				else
				{
					result = PlayerConnection.s_Instance;
				}
				return result;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x00032134 File Offset: 0x00030334
		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00032154 File Offset: 0x00030354
		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00032184 File Offset: 0x00030384
		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000321B4 File Offset: 0x000303B4
		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000321D4 File Offset: 0x000303D4
		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00032264 File Offset: 0x00030464
		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000322CC File Offset: 0x000304CC
		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int arg in this.m_connectedPlayers)
			{
				callback(arg);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00032338 File Offset: 0x00030538
		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0003234D File Offset: 0x0003054D
		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00032362 File Offset: 0x00030562
		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00032378 File Offset: 0x00030578
		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000323B8 File Offset: 0x000305B8
		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000323F8 File Offset: 0x000305F8
		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> callback = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime now = DateTime.Now;
			this.Register(messageId, callback);
			while ((DateTime.Now - now).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, callback);
			return msgReceived;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0003247A File Offset: 0x0003067A
		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0003248C File Offset: 0x0003068C
		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] array = null;
			bool flag = size > 0UL;
			if (flag)
			{
				array = new byte[size];
				Marshal.Copy(data, array, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), array, (int)guid);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000324D3 File Offset: 0x000306D3
		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000324FD File Offset: 0x000306FD
		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00032527 File Offset: 0x00030727
		public PlayerConnection()
		{
		}

		// Token: 0x04000A23 RID: 2595
		internal static IPlayerEditorConnectionNative connectionNative;

		// Token: 0x04000A24 RID: 2596
		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		// Token: 0x04000A25 RID: 2597
		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		// Token: 0x04000A26 RID: 2598
		private bool m_IsInitilized;

		// Token: 0x04000A27 RID: 2599
		private static PlayerConnection s_Instance;

		// Token: 0x0200038C RID: 908
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06001EE8 RID: 7912 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x06001EE9 RID: 7913 RVA: 0x00032546 File Offset: 0x00030746
			internal bool <Register>b__0(PlayerEditorConnectionEvents.MessageTypeSubscribers x)
			{
				return x.MessageTypeId == this.messageId;
			}

			// Token: 0x04000A28 RID: 2600
			public Guid messageId;
		}

		// Token: 0x0200038D RID: 909
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06001EEA RID: 7914 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06001EEB RID: 7915 RVA: 0x00032559 File Offset: 0x00030759
			internal bool <Unregister>b__0(PlayerEditorConnectionEvents.MessageTypeSubscribers x)
			{
				return x.MessageTypeId == this.messageId;
			}

			// Token: 0x04000A29 RID: 2601
			public Guid messageId;
		}

		// Token: 0x0200038E RID: 910
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x06001EEC RID: 7916 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06001EED RID: 7917 RVA: 0x0003256C File Offset: 0x0003076C
			internal void <BlockUntilRecvMsg>b__0(MessageEventArgs args)
			{
				this.msgReceived = true;
			}

			// Token: 0x04000A2A RID: 2602
			public bool msgReceived;
		}
	}
}
