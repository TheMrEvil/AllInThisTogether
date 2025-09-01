using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x0200038F RID: 911
	[Serializable]
	internal class PlayerEditorConnectionEvents
	{
		// Token: 0x06001EEE RID: 7918 RVA: 0x00032578 File Offset: 0x00030778
		public void InvokeMessageIdSubscribers(Guid messageId, byte[] data, int playerId)
		{
			IEnumerable<PlayerEditorConnectionEvents.MessageTypeSubscribers> enumerable = from x in this.messageTypeSubscribers
			where x.MessageTypeId == messageId
			select x;
			bool flag = !enumerable.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>();
			if (flag)
			{
				string str = "No actions found for messageId: ";
				Guid messageId2 = messageId;
				Debug.LogError(str + messageId2.ToString());
			}
			else
			{
				MessageEventArgs arg = new MessageEventArgs
				{
					playerId = playerId,
					data = data
				};
				foreach (PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers in enumerable)
				{
					messageTypeSubscribers.messageCallback.Invoke(arg);
				}
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00032640 File Offset: 0x00030840
		public UnityEvent<MessageEventArgs> AddAndCreate(Guid messageId)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = this.messageTypeSubscribers.SingleOrDefault((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (flag)
			{
				messageTypeSubscribers = new PlayerEditorConnectionEvents.MessageTypeSubscribers
				{
					MessageTypeId = messageId,
					messageCallback = new PlayerEditorConnectionEvents.MessageEvent()
				};
				this.messageTypeSubscribers.Add(messageTypeSubscribers);
			}
			messageTypeSubscribers.subscriberCount++;
			return messageTypeSubscribers.messageCallback;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000326C0 File Offset: 0x000308C0
		public void UnregisterManagedCallback(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers = this.messageTypeSubscribers.SingleOrDefault((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			bool flag = messageTypeSubscribers == null;
			if (!flag)
			{
				messageTypeSubscribers.subscriberCount--;
				messageTypeSubscribers.messageCallback.RemoveListener(callback);
				bool flag2 = messageTypeSubscribers.subscriberCount <= 0;
				if (flag2)
				{
					this.messageTypeSubscribers.Remove(messageTypeSubscribers);
				}
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00032738 File Offset: 0x00030938
		public PlayerEditorConnectionEvents()
		{
		}

		// Token: 0x04000A2B RID: 2603
		[SerializeField]
		public List<PlayerEditorConnectionEvents.MessageTypeSubscribers> messageTypeSubscribers = new List<PlayerEditorConnectionEvents.MessageTypeSubscribers>();

		// Token: 0x04000A2C RID: 2604
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent connectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x04000A2D RID: 2605
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent disconnectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x02000390 RID: 912
		[Serializable]
		public class MessageEvent : UnityEvent<MessageEventArgs>
		{
			// Token: 0x06001EF2 RID: 7922 RVA: 0x00032762 File Offset: 0x00030962
			public MessageEvent()
			{
			}
		}

		// Token: 0x02000391 RID: 913
		[Serializable]
		public class ConnectionChangeEvent : UnityEvent<int>
		{
			// Token: 0x06001EF3 RID: 7923 RVA: 0x0003276B File Offset: 0x0003096B
			public ConnectionChangeEvent()
			{
			}
		}

		// Token: 0x02000392 RID: 914
		[Serializable]
		public class MessageTypeSubscribers
		{
			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00032774 File Offset: 0x00030974
			// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x00032791 File Offset: 0x00030991
			public Guid MessageTypeId
			{
				get
				{
					return new Guid(this.m_messageTypeId);
				}
				set
				{
					this.m_messageTypeId = value.ToString();
				}
			}

			// Token: 0x06001EF6 RID: 7926 RVA: 0x000327A7 File Offset: 0x000309A7
			public MessageTypeSubscribers()
			{
			}

			// Token: 0x04000A2E RID: 2606
			[SerializeField]
			private string m_messageTypeId;

			// Token: 0x04000A2F RID: 2607
			public int subscriberCount = 0;

			// Token: 0x04000A30 RID: 2608
			public PlayerEditorConnectionEvents.MessageEvent messageCallback = new PlayerEditorConnectionEvents.MessageEvent();
		}

		// Token: 0x02000393 RID: 915
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06001EF7 RID: 7927 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06001EF8 RID: 7928 RVA: 0x000327C2 File Offset: 0x000309C2
			internal bool <InvokeMessageIdSubscribers>b__0(PlayerEditorConnectionEvents.MessageTypeSubscribers x)
			{
				return x.MessageTypeId == this.messageId;
			}

			// Token: 0x04000A31 RID: 2609
			public Guid messageId;
		}

		// Token: 0x02000394 RID: 916
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06001EF9 RID: 7929 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06001EFA RID: 7930 RVA: 0x000327D5 File Offset: 0x000309D5
			internal bool <AddAndCreate>b__0(PlayerEditorConnectionEvents.MessageTypeSubscribers x)
			{
				return x.MessageTypeId == this.messageId;
			}

			// Token: 0x04000A32 RID: 2610
			public Guid messageId;
		}

		// Token: 0x02000395 RID: 917
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06001EFB RID: 7931 RVA: 0x00002072 File Offset: 0x00000272
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06001EFC RID: 7932 RVA: 0x000327E8 File Offset: 0x000309E8
			internal bool <UnregisterManagedCallback>b__0(PlayerEditorConnectionEvents.MessageTypeSubscribers x)
			{
				return x.MessageTypeId == this.messageId;
			}

			// Token: 0x04000A33 RID: 2611
			public Guid messageId;
		}
	}
}
