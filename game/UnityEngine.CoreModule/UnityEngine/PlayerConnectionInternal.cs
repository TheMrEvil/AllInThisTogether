using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001D0 RID: 464
	[NativeHeader("Runtime/Export/PlayerConnection/PlayerConnectionInternal.bindings.h")]
	internal class PlayerConnectionInternal : IPlayerEditorConnectionNative
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x00022F90 File Offset: 0x00021190
		void IPlayerEditorConnectionNative.SendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			PlayerConnectionInternal.SendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00022FD0 File Offset: 0x000211D0
		bool IPlayerEditorConnectionNative.TrySendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			return PlayerConnectionInternal.TrySendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00023010 File Offset: 0x00021210
		void IPlayerEditorConnectionNative.Poll()
		{
			PlayerConnectionInternal.PollInternal();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00023019 File Offset: 0x00021219
		void IPlayerEditorConnectionNative.RegisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.RegisterInternal(messageId.ToString("N"));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0002302E File Offset: 0x0002122E
		void IPlayerEditorConnectionNative.UnregisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.UnregisterInternal(messageId.ToString("N"));
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00023043 File Offset: 0x00021243
		void IPlayerEditorConnectionNative.Initialize()
		{
			PlayerConnectionInternal.Initialize();
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0002304C File Offset: 0x0002124C
		bool IPlayerEditorConnectionNative.IsConnected()
		{
			return PlayerConnectionInternal.IsConnected();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00023063 File Offset: 0x00021263
		void IPlayerEditorConnectionNative.DisconnectAll()
		{
			PlayerConnectionInternal.DisconnectAll();
		}

		// Token: 0x060015B5 RID: 5557
		[FreeFunction("PlayerConnection_Bindings::IsConnected")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConnected();

		// Token: 0x060015B6 RID: 5558
		[FreeFunction("PlayerConnection_Bindings::Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Initialize();

		// Token: 0x060015B7 RID: 5559
		[FreeFunction("PlayerConnection_Bindings::RegisterInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterInternal(string messageId);

		// Token: 0x060015B8 RID: 5560
		[FreeFunction("PlayerConnection_Bindings::UnregisterInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterInternal(string messageId);

		// Token: 0x060015B9 RID: 5561
		[FreeFunction("PlayerConnection_Bindings::SendMessage")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage(string messageId, [Unmarshalled] byte[] data, int playerId);

		// Token: 0x060015BA RID: 5562
		[FreeFunction("PlayerConnection_Bindings::TrySendMessage")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySendMessage(string messageId, [Unmarshalled] byte[] data, int playerId);

		// Token: 0x060015BB RID: 5563
		[FreeFunction("PlayerConnection_Bindings::PollInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PollInternal();

		// Token: 0x060015BC RID: 5564
		[FreeFunction("PlayerConnection_Bindings::DisconnectAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisconnectAll();

		// Token: 0x060015BD RID: 5565 RVA: 0x00002072 File Offset: 0x00000272
		public PlayerConnectionInternal()
		{
		}

		// Token: 0x020001D1 RID: 465
		[Flags]
		public enum MulticastFlags
		{
			// Token: 0x040007AA RID: 1962
			kRequestImmediateConnect = 1,
			// Token: 0x040007AB RID: 1963
			kSupportsProfile = 2,
			// Token: 0x040007AC RID: 1964
			kCustomMessage = 4,
			// Token: 0x040007AD RID: 1965
			kUseAlternateIP = 8
		}
	}
}
