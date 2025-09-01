using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000B3 RID: 179
	public struct PartyBeacon
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00011E46 File Offset: 0x00010046
		private static ISteamParties Internal
		{
			get
			{
				return SteamParties.Internal;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00011E50 File Offset: 0x00010050
		public SteamId Owner
		{
			get
			{
				SteamId result = default(SteamId);
				SteamPartyBeaconLocation_t steamPartyBeaconLocation_t = default(SteamPartyBeaconLocation_t);
				string text;
				PartyBeacon.Internal.GetBeaconDetails(this.Id, ref result, ref steamPartyBeaconLocation_t, out text);
				return result;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00011E8C File Offset: 0x0001008C
		public string MetaData
		{
			get
			{
				SteamId steamId = default(SteamId);
				SteamPartyBeaconLocation_t steamPartyBeaconLocation_t = default(SteamPartyBeaconLocation_t);
				string result;
				PartyBeacon.Internal.GetBeaconDetails(this.Id, ref steamId, ref steamPartyBeaconLocation_t, out result);
				return result;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00011EC8 File Offset: 0x000100C8
		public async Task<string> JoinAsync()
		{
			JoinPartyCallback_t? joinPartyCallback_t = await PartyBeacon.Internal.JoinParty(this.Id);
			JoinPartyCallback_t? result = joinPartyCallback_t;
			joinPartyCallback_t = null;
			string result2;
			if (result == null || result.Value.Result != Result.OK)
			{
				result2 = null;
			}
			else
			{
				result2 = result.Value.ConnectStringUTF8();
			}
			return result2;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00011F14 File Offset: 0x00010114
		public void OnReservationCompleted(SteamId steamid)
		{
			PartyBeacon.Internal.OnReservationCompleted(this.Id, steamid);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00011F29 File Offset: 0x00010129
		public void CancelReservation(SteamId steamid)
		{
			PartyBeacon.Internal.CancelReservation(this.Id, steamid);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00011F40 File Offset: 0x00010140
		public bool Destroy()
		{
			return PartyBeacon.Internal.DestroyBeacon(this.Id);
		}

		// Token: 0x04000762 RID: 1890
		internal PartyBeaconID_t Id;

		// Token: 0x0200026B RID: 619
		[CompilerGenerated]
		private sealed class <JoinAsync>d__7 : IAsyncStateMachine
		{
			// Token: 0x060011F5 RID: 4597 RVA: 0x00021442 File Offset: 0x0001F642
			public <JoinAsync>d__7()
			{
			}

			// Token: 0x060011F6 RID: 4598 RVA: 0x0002144C File Offset: 0x0001F64C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				string result2;
				try
				{
					CallResult<JoinPartyCallback_t> callResult;
					if (num != 0)
					{
						callResult = PartyBeacon.Internal.JoinParty(this.Id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<JoinPartyCallback_t> callResult2 = callResult;
							PartyBeacon.<JoinAsync>d__7 <JoinAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<JoinPartyCallback_t>, PartyBeacon.<JoinAsync>d__7>(ref callResult, ref <JoinAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<JoinPartyCallback_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<JoinPartyCallback_t>);
						num2 = -1;
					}
					joinPartyCallback_t = callResult.GetResult();
					result = joinPartyCallback_t;
					joinPartyCallback_t = null;
					bool flag = result == null || result.Value.Result != Result.OK;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						result2 = result.Value.ConnectStringUTF8();
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

			// Token: 0x060011F7 RID: 4599 RVA: 0x00021580 File Offset: 0x0001F780
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E4D RID: 3661
			public int <>1__state;

			// Token: 0x04000E4E RID: 3662
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000E4F RID: 3663
			public PartyBeacon <>4__this;

			// Token: 0x04000E50 RID: 3664
			private JoinPartyCallback_t? <result>5__1;

			// Token: 0x04000E51 RID: 3665
			private JoinPartyCallback_t? <>s__2;

			// Token: 0x04000E52 RID: 3666
			private CallResult<JoinPartyCallback_t> <>u__1;
		}
	}
}
