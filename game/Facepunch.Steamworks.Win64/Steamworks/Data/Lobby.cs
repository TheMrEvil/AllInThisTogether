using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x020001FD RID: 509
	public struct Lobby
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0001A011 File Offset: 0x00018211
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0001A019 File Offset: 0x00018219
		public SteamId Id
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0001A022 File Offset: 0x00018222
		public Lobby(SteamId id)
		{
			this.Id = id;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0001A030 File Offset: 0x00018230
		public async Task<RoomEnter> Join()
		{
			LobbyEnter_t? lobbyEnter_t = await SteamMatchmaking.Internal.JoinLobby(this.Id);
			LobbyEnter_t? result = lobbyEnter_t;
			lobbyEnter_t = null;
			RoomEnter result2;
			if (result == null)
			{
				result2 = RoomEnter.Error;
			}
			else
			{
				result2 = (RoomEnter)result.Value.EChatRoomEnterResponse;
			}
			return result2;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0001A07C File Offset: 0x0001827C
		public void Leave()
		{
			SteamMatchmaking.Internal.LeaveLobby(this.Id);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0001A090 File Offset: 0x00018290
		public bool InviteFriend(SteamId steamid)
		{
			return SteamMatchmaking.Internal.InviteUserToLobby(this.Id, steamid);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0001A0B3 File Offset: 0x000182B3
		public int MemberCount
		{
			get
			{
				return SteamMatchmaking.Internal.GetNumLobbyMembers(this.Id);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0001A0C8 File Offset: 0x000182C8
		public IEnumerable<Friend> Members
		{
			get
			{
				int num;
				for (int i = 0; i < this.MemberCount; i = num + 1)
				{
					yield return new Friend(SteamMatchmaking.Internal.GetLobbyMemberByIndex(this.Id, i));
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0001A0EC File Offset: 0x000182EC
		public string GetData(string key)
		{
			return SteamMatchmaking.Internal.GetLobbyData(this.Id, key);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0001A110 File Offset: 0x00018310
		public bool SetData(string key, string value)
		{
			bool flag = key.Length > 255;
			if (flag)
			{
				throw new ArgumentException("Key should be < 255 chars", "key");
			}
			bool flag2 = value.Length > 8192;
			if (flag2)
			{
				throw new ArgumentException("Value should be < 8192 chars", "key");
			}
			return SteamMatchmaking.Internal.SetLobbyData(this.Id, key, value);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0001A178 File Offset: 0x00018378
		public bool DeleteData(string key)
		{
			return SteamMatchmaking.Internal.DeleteLobbyData(this.Id, key);
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0001A19C File Offset: 0x0001839C
		public IEnumerable<KeyValuePair<string, string>> Data
		{
			get
			{
				int cnt = SteamMatchmaking.Internal.GetLobbyDataCount(this.Id);
				int num;
				for (int i = 0; i < cnt; i = num + 1)
				{
					string a;
					string b;
					bool lobbyDataByIndex = SteamMatchmaking.Internal.GetLobbyDataByIndex(this.Id, i, out a, out b);
					if (lobbyDataByIndex)
					{
						yield return new KeyValuePair<string, string>(a, b);
					}
					a = null;
					b = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public string GetMemberData(Friend member, string key)
		{
			return SteamMatchmaking.Internal.GetLobbyMemberData(this.Id, member.Id, key);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0001A1E9 File Offset: 0x000183E9
		public void SetMemberData(string key, string value)
		{
			SteamMatchmaking.Internal.SetLobbyMemberData(this.Id, key, value);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0001A200 File Offset: 0x00018400
		public bool SendChatString(string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			return this.SendChatBytes(bytes);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0001A228 File Offset: 0x00018428
		internal unsafe bool SendChatBytes(byte[] data)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return SteamMatchmaking.Internal.SendLobbyChatMsg(this.Id, (IntPtr)((void*)value), data.Length);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0001A26C File Offset: 0x0001846C
		public bool Refresh()
		{
			return SteamMatchmaking.Internal.RequestLobbyData(this.Id);
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0001A28E File Offset: 0x0001848E
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public int MaxMembers
		{
			get
			{
				return SteamMatchmaking.Internal.GetLobbyMemberLimit(this.Id);
			}
			set
			{
				SteamMatchmaking.Internal.SetLobbyMemberLimit(this.Id, value);
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public bool SetPublic()
		{
			return SteamMatchmaking.Internal.SetLobbyType(this.Id, LobbyType.Public);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public bool SetPrivate()
		{
			return SteamMatchmaking.Internal.SetLobbyType(this.Id, LobbyType.Private);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0001A2FC File Offset: 0x000184FC
		public bool SetInvisible()
		{
			return SteamMatchmaking.Internal.SetLobbyType(this.Id, LobbyType.Invisible);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0001A320 File Offset: 0x00018520
		public bool SetFriendsOnly()
		{
			return SteamMatchmaking.Internal.SetLobbyType(this.Id, LobbyType.FriendsOnly);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0001A344 File Offset: 0x00018544
		public bool SetJoinable(bool b)
		{
			return SteamMatchmaking.Internal.SetLobbyJoinable(this.Id, b);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0001A368 File Offset: 0x00018568
		public void SetGameServer(SteamId steamServer)
		{
			bool flag = !steamServer.IsValid;
			if (flag)
			{
				throw new ArgumentException("SteamId for server is invalid");
			}
			SteamMatchmaking.Internal.SetLobbyGameServer(this.Id, 0U, 0, steamServer);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0001A3A4 File Offset: 0x000185A4
		public void SetGameServer(string ip, ushort port)
		{
			IPAddress ipAddress;
			bool flag = !IPAddress.TryParse(ip, out ipAddress);
			if (flag)
			{
				throw new ArgumentException("IP address for server is invalid");
			}
			SteamMatchmaking.Internal.SetLobbyGameServer(this.Id, ipAddress.IpToInt32(), port, default(SteamId));
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public bool GetGameServer(ref uint ip, ref ushort port, ref SteamId serverId)
		{
			return SteamMatchmaking.Internal.GetLobbyGameServer(this.Id, ref ip, ref port, ref serverId);
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0001A415 File Offset: 0x00018615
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x0001A42C File Offset: 0x0001862C
		public Friend Owner
		{
			get
			{
				return new Friend(SteamMatchmaking.Internal.GetLobbyOwner(this.Id));
			}
			set
			{
				SteamMatchmaking.Internal.SetLobbyOwner(this.Id, value.Id);
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0001A445 File Offset: 0x00018645
		public bool IsOwnedBy(SteamId k)
		{
			return this.Owner.Id == k;
		}

		// Token: 0x04000C0D RID: 3085
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SteamId <Id>k__BackingField;

		// Token: 0x0200028F RID: 655
		[CompilerGenerated]
		private sealed class <Join>d__5 : IAsyncStateMachine
		{
			// Token: 0x06001256 RID: 4694 RVA: 0x00024CFA File Offset: 0x00022EFA
			public <Join>d__5()
			{
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x00024D04 File Offset: 0x00022F04
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				RoomEnter result2;
				try
				{
					CallResult<LobbyEnter_t> callResult;
					if (num != 0)
					{
						callResult = SteamMatchmaking.Internal.JoinLobby(base.Id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LobbyEnter_t> callResult2 = callResult;
							Lobby.<Join>d__5 <Join>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LobbyEnter_t>, Lobby.<Join>d__5>(ref callResult, ref <Join>d__);
							return;
						}
					}
					else
					{
						CallResult<LobbyEnter_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LobbyEnter_t>);
						num2 = -1;
					}
					lobbyEnter_t = callResult.GetResult();
					result = lobbyEnter_t;
					lobbyEnter_t = null;
					bool flag = result == null;
					if (flag)
					{
						result2 = RoomEnter.Error;
					}
					else
					{
						result2 = (RoomEnter)result.Value.EChatRoomEnterResponse;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x00024E1C File Offset: 0x0002301C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F58 RID: 3928
			public int <>1__state;

			// Token: 0x04000F59 RID: 3929
			public AsyncTaskMethodBuilder<RoomEnter> <>t__builder;

			// Token: 0x04000F5A RID: 3930
			public Lobby <>4__this;

			// Token: 0x04000F5B RID: 3931
			private LobbyEnter_t? <result>5__1;

			// Token: 0x04000F5C RID: 3932
			private LobbyEnter_t? <>s__2;

			// Token: 0x04000F5D RID: 3933
			private CallResult<LobbyEnter_t> <>u__1;
		}

		// Token: 0x02000290 RID: 656
		[CompilerGenerated]
		private sealed class <get_Members>d__11 : IEnumerable<Friend>, IEnumerable, IEnumerator<Friend>, IDisposable, IEnumerator
		{
			// Token: 0x06001259 RID: 4697 RVA: 0x00024E1E File Offset: 0x0002301E
			[DebuggerHidden]
			public <get_Members>d__11(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00024E39 File Offset: 0x00023039
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600125B RID: 4699 RVA: 0x00024E3C File Offset: 0x0002303C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= base.MemberCount)
				{
					return false;
				}
				this.<>2__current = new Friend(SteamMatchmaking.Internal.GetLobbyMemberByIndex(base.Id, i));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000309 RID: 777
			// (get) Token: 0x0600125C RID: 4700 RVA: 0x00024ED2 File Offset: 0x000230D2
			Friend IEnumerator<Friend>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600125D RID: 4701 RVA: 0x00024EDA File Offset: 0x000230DA
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700030A RID: 778
			// (get) Token: 0x0600125E RID: 4702 RVA: 0x00024EE1 File Offset: 0x000230E1
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x00024EF0 File Offset: 0x000230F0
			[DebuggerHidden]
			IEnumerator<Friend> IEnumerable<Friend>.GetEnumerator()
			{
				Lobby.<get_Members>d__11 <get_Members>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Members>d__ = this;
				}
				else
				{
					<get_Members>d__ = new Lobby.<get_Members>d__11(0);
				}
				<get_Members>d__.<>4__this = ref this;
				return <get_Members>d__;
			}

			// Token: 0x06001260 RID: 4704 RVA: 0x00024F33 File Offset: 0x00023133
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Friend>.GetEnumerator();
			}

			// Token: 0x04000F5E RID: 3934
			private int <>1__state;

			// Token: 0x04000F5F RID: 3935
			private Friend <>2__current;

			// Token: 0x04000F60 RID: 3936
			private int <>l__initialThreadId;

			// Token: 0x04000F61 RID: 3937
			public Lobby <>4__this;

			// Token: 0x04000F62 RID: 3938
			public Lobby <>3__<>4__this;

			// Token: 0x04000F63 RID: 3939
			private int <i>5__1;
		}

		// Token: 0x02000291 RID: 657
		[CompilerGenerated]
		private sealed class <get_Data>d__16 : IEnumerable<KeyValuePair<string, string>>, IEnumerable, IEnumerator<KeyValuePair<string, string>>, IDisposable, IEnumerator
		{
			// Token: 0x06001261 RID: 4705 RVA: 0x00024F3B File Offset: 0x0002313B
			[DebuggerHidden]
			public <get_Data>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001262 RID: 4706 RVA: 0x00024F56 File Offset: 0x00023156
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001263 RID: 4707 RVA: 0x00024F58 File Offset: 0x00023158
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					cnt = SteamMatchmaking.Internal.GetLobbyDataCount(base.Id);
					i = 0;
					goto IL_B8;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_99:
				a = null;
				b = null;
				int num2 = i;
				i = num2 + 1;
				IL_B8:
				if (i >= cnt)
				{
					return false;
				}
				bool lobbyDataByIndex = SteamMatchmaking.Internal.GetLobbyDataByIndex(base.Id, i, out a, out b);
				if (lobbyDataByIndex)
				{
					this.<>2__current = new KeyValuePair<string, string>(a, b);
					this.<>1__state = 1;
					return true;
				}
				goto IL_99;
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06001264 RID: 4708 RVA: 0x00025033 File Offset: 0x00023233
			KeyValuePair<string, string> IEnumerator<KeyValuePair<string, string>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001265 RID: 4709 RVA: 0x0002503B File Offset: 0x0002323B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06001266 RID: 4710 RVA: 0x00025042 File Offset: 0x00023242
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001267 RID: 4711 RVA: 0x00025050 File Offset: 0x00023250
			[DebuggerHidden]
			IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
			{
				Lobby.<get_Data>d__16 <get_Data>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Data>d__ = this;
				}
				else
				{
					<get_Data>d__ = new Lobby.<get_Data>d__16(0);
				}
				<get_Data>d__.<>4__this = ref this;
				return <get_Data>d__;
			}

			// Token: 0x06001268 RID: 4712 RVA: 0x00025093 File Offset: 0x00023293
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.String>>.GetEnumerator();
			}

			// Token: 0x04000F64 RID: 3940
			private int <>1__state;

			// Token: 0x04000F65 RID: 3941
			private KeyValuePair<string, string> <>2__current;

			// Token: 0x04000F66 RID: 3942
			private int <>l__initialThreadId;

			// Token: 0x04000F67 RID: 3943
			public Lobby <>4__this;

			// Token: 0x04000F68 RID: 3944
			public Lobby <>3__<>4__this;

			// Token: 0x04000F69 RID: 3945
			private int <cnt>5__1;

			// Token: 0x04000F6A RID: 3946
			private int <i>5__2;

			// Token: 0x04000F6B RID: 3947
			private string <a>5__3;

			// Token: 0x04000F6C RID: 3948
			private string <b>5__4;
		}
	}
}
