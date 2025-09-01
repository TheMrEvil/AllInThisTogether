using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x02000204 RID: 516
	public struct ServerInfo : IEquatable<ServerInfo>
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0001AAFF File Offset: 0x00018CFF
		public string Name
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0001AB08 File Offset: 0x00018D08
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0001AB10 File Offset: 0x00018D10
		public int Ping
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Ping>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ping>k__BackingField = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0001AB19 File Offset: 0x00018D19
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0001AB21 File Offset: 0x00018D21
		public string GameDir
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<GameDir>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GameDir>k__BackingField = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0001AB2A File Offset: 0x00018D2A
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0001AB32 File Offset: 0x00018D32
		public string Map
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Map>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Map>k__BackingField = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0001AB3B File Offset: 0x00018D3B
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0001AB43 File Offset: 0x00018D43
		public string Description
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0001AB4C File Offset: 0x00018D4C
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0001AB54 File Offset: 0x00018D54
		public uint AppId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<AppId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AppId>k__BackingField = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0001AB5D File Offset: 0x00018D5D
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0001AB65 File Offset: 0x00018D65
		public int Players
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Players>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Players>k__BackingField = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0001AB6E File Offset: 0x00018D6E
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0001AB76 File Offset: 0x00018D76
		public int MaxPlayers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<MaxPlayers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaxPlayers>k__BackingField = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0001AB7F File Offset: 0x00018D7F
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x0001AB87 File Offset: 0x00018D87
		public int BotPlayers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<BotPlayers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BotPlayers>k__BackingField = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0001AB90 File Offset: 0x00018D90
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x0001AB98 File Offset: 0x00018D98
		public bool Passworded
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Passworded>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Passworded>k__BackingField = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0001ABA1 File Offset: 0x00018DA1
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x0001ABA9 File Offset: 0x00018DA9
		public bool Secure
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Secure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Secure>k__BackingField = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0001ABB2 File Offset: 0x00018DB2
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x0001ABBA File Offset: 0x00018DBA
		public uint LastTimePlayed
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<LastTimePlayed>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastTimePlayed>k__BackingField = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0001ABC3 File Offset: 0x00018DC3
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0001ABCB File Offset: 0x00018DCB
		public int Version
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public string TagString
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<TagString>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TagString>k__BackingField = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0001ABED File Offset: 0x00018DED
		public ulong SteamId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<SteamId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SteamId>k__BackingField = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0001ABF6 File Offset: 0x00018DF6
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0001ABFE File Offset: 0x00018DFE
		public uint AddressRaw
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<AddressRaw>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AddressRaw>k__BackingField = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0001AC07 File Offset: 0x00018E07
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0001AC0F File Offset: 0x00018E0F
		public IPAddress Address
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Address>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Address>k__BackingField = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0001AC18 File Offset: 0x00018E18
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0001AC20 File Offset: 0x00018E20
		public int ConnectionPort
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<ConnectionPort>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConnectionPort>k__BackingField = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0001AC29 File Offset: 0x00018E29
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0001AC31 File Offset: 0x00018E31
		public int QueryPort
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<QueryPort>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QueryPort>k__BackingField = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public string[] Tags
		{
			get
			{
				bool flag = this._tags == null;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(this.TagString);
					if (flag2)
					{
						this._tags = this.TagString.Split(new char[]
						{
							','
						});
					}
				}
				return this._tags;
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0001AC94 File Offset: 0x00018E94
		internal static ServerInfo From(gameserveritem_t item)
		{
			return new ServerInfo
			{
				AddressRaw = item.NetAdr.IP,
				Address = Utility.Int32ToIp(item.NetAdr.IP),
				ConnectionPort = (int)item.NetAdr.ConnectionPort,
				QueryPort = (int)item.NetAdr.QueryPort,
				Name = item.ServerNameUTF8(),
				Ping = item.Ping,
				GameDir = item.GameDirUTF8(),
				Map = item.MapUTF8(),
				Description = item.GameDescriptionUTF8(),
				AppId = item.AppID,
				Players = item.Players,
				MaxPlayers = item.MaxPlayers,
				BotPlayers = item.BotPlayers,
				Passworded = item.Password,
				Secure = item.Secure,
				LastTimePlayed = item.TimeLastPlayed,
				Version = item.ServerVersion,
				TagString = item.GameTagsUTF8(),
				SteamId = item.SteamID
			};
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0001ADD7 File Offset: 0x00018FD7
		public ServerInfo(uint ip, ushort cport, ushort qport, uint timeplayed)
		{
			this = default(ServerInfo);
			this.AddressRaw = ip;
			this.Address = Utility.Int32ToIp(ip);
			this.ConnectionPort = (int)cport;
			this.QueryPort = (int)qport;
			this.LastTimePlayed = timeplayed;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0001AE0F File Offset: 0x0001900F
		public void AddToHistory()
		{
			SteamMatchmaking.Internal.AddFavoriteGame(SteamClient.AppId, this.AddressRaw, (ushort)this.ConnectionPort, (ushort)this.QueryPort, 2U, (uint)Epoch.Current);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0001AE3C File Offset: 0x0001903C
		public async Task<Dictionary<string, string>> QueryRulesAsync()
		{
			return await SourceServerQuery.GetRules(ref this);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0001AE88 File Offset: 0x00019088
		public void RemoveFromHistory()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame(SteamClient.AppId, this.AddressRaw, (ushort)this.ConnectionPort, (ushort)this.QueryPort, 2U);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0001AEB0 File Offset: 0x000190B0
		public void AddToFavourites()
		{
			SteamMatchmaking.Internal.AddFavoriteGame(SteamClient.AppId, this.AddressRaw, (ushort)this.ConnectionPort, (ushort)this.QueryPort, 1U, (uint)Epoch.Current);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0001AEDD File Offset: 0x000190DD
		public void RemoveFromFavourites()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame(SteamClient.AppId, this.AddressRaw, (ushort)this.ConnectionPort, (ushort)this.QueryPort, 1U);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0001AF08 File Offset: 0x00019108
		public bool Equals(ServerInfo other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0001AF38 File Offset: 0x00019138
		public override int GetHashCode()
		{
			return this.Address.GetHashCode() + this.SteamId.GetHashCode() + this.ConnectionPort.GetHashCode() + this.QueryPort.GetHashCode();
		}

		// Token: 0x04000C1F RID: 3103
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Name>k__BackingField;

		// Token: 0x04000C20 RID: 3104
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Ping>k__BackingField;

		// Token: 0x04000C21 RID: 3105
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <GameDir>k__BackingField;

		// Token: 0x04000C22 RID: 3106
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Map>k__BackingField;

		// Token: 0x04000C23 RID: 3107
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Description>k__BackingField;

		// Token: 0x04000C24 RID: 3108
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <AppId>k__BackingField;

		// Token: 0x04000C25 RID: 3109
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Players>k__BackingField;

		// Token: 0x04000C26 RID: 3110
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <MaxPlayers>k__BackingField;

		// Token: 0x04000C27 RID: 3111
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <BotPlayers>k__BackingField;

		// Token: 0x04000C28 RID: 3112
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <Passworded>k__BackingField;

		// Token: 0x04000C29 RID: 3113
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <Secure>k__BackingField;

		// Token: 0x04000C2A RID: 3114
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <LastTimePlayed>k__BackingField;

		// Token: 0x04000C2B RID: 3115
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Version>k__BackingField;

		// Token: 0x04000C2C RID: 3116
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <TagString>k__BackingField;

		// Token: 0x04000C2D RID: 3117
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <SteamId>k__BackingField;

		// Token: 0x04000C2E RID: 3118
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <AddressRaw>k__BackingField;

		// Token: 0x04000C2F RID: 3119
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IPAddress <Address>k__BackingField;

		// Token: 0x04000C30 RID: 3120
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ConnectionPort>k__BackingField;

		// Token: 0x04000C31 RID: 3121
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <QueryPort>k__BackingField;

		// Token: 0x04000C32 RID: 3122
		private string[] _tags;

		// Token: 0x04000C33 RID: 3123
		internal const uint k_unFavoriteFlagNone = 0U;

		// Token: 0x04000C34 RID: 3124
		internal const uint k_unFavoriteFlagFavorite = 1U;

		// Token: 0x04000C35 RID: 3125
		internal const uint k_unFavoriteFlagHistory = 2U;

		// Token: 0x02000293 RID: 659
		[CompilerGenerated]
		private sealed class <QueryRulesAsync>d__85 : IAsyncStateMachine
		{
			// Token: 0x0600126C RID: 4716 RVA: 0x00025266 File Offset: 0x00023466
			public <QueryRulesAsync>d__85()
			{
			}

			// Token: 0x0600126D RID: 4717 RVA: 0x00025270 File Offset: 0x00023470
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Dictionary<string, string> result2;
				try
				{
					TaskAwaiter<Dictionary<string, string>> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SourceServerQuery.GetRules(ref this).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<Dictionary<string, string>> taskAwaiter2 = taskAwaiter;
							ServerInfo.<QueryRulesAsync>d__85 <QueryRulesAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Dictionary<string, string>>, ServerInfo.<QueryRulesAsync>d__85>(ref taskAwaiter, ref <QueryRulesAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<Dictionary<string, string>> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<Dictionary<string, string>>);
						num2 = -1;
					}
					Dictionary<string, string> result = taskAwaiter.GetResult();
					result2 = result;
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

			// Token: 0x0600126E RID: 4718 RVA: 0x0002533C File Offset: 0x0002353C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F75 RID: 3957
			public int <>1__state;

			// Token: 0x04000F76 RID: 3958
			public AsyncTaskMethodBuilder<Dictionary<string, string>> <>t__builder;

			// Token: 0x04000F77 RID: 3959
			public ServerInfo <>4__this;

			// Token: 0x04000F78 RID: 3960
			private Dictionary<string, string> <>s__1;

			// Token: 0x04000F79 RID: 3961
			private TaskAwaiter<Dictionary<string, string>> <>u__1;
		}
	}
}
