using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009E RID: 158
	public class SteamParties : SteamClientClass<SteamParties>
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		internal static ISteamParties Internal
		{
			get
			{
				return SteamClientClass<SteamParties>.Interface as ISteamParties;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000DC68 File Offset: 0x0000BE68
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamParties(server));
			this.InstallEvents(server);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0000DC84 File Offset: 0x0000BE84
		internal void InstallEvents(bool server)
		{
			Dispatch.Install<AvailableBeaconLocationsUpdated_t>(delegate(AvailableBeaconLocationsUpdated_t x)
			{
				Action onBeaconLocationsUpdated = SteamParties.OnBeaconLocationsUpdated;
				if (onBeaconLocationsUpdated != null)
				{
					onBeaconLocationsUpdated();
				}
			}, server);
			Dispatch.Install<ActiveBeaconsUpdated_t>(delegate(ActiveBeaconsUpdated_t x)
			{
				Action onActiveBeaconsUpdated = SteamParties.OnActiveBeaconsUpdated;
				if (onActiveBeaconsUpdated != null)
				{
					onActiveBeaconsUpdated();
				}
			}, server);
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600084C RID: 2124 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		// (remove) Token: 0x0600084D RID: 2125 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public static event Action OnBeaconLocationsUpdated
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamParties.OnBeaconLocationsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParties.OnBeaconLocationsUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamParties.OnBeaconLocationsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParties.OnBeaconLocationsUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600084E RID: 2126 RVA: 0x0000DD48 File Offset: 0x0000BF48
		// (remove) Token: 0x0600084F RID: 2127 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static event Action OnActiveBeaconsUpdated
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamParties.OnActiveBeaconsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParties.OnActiveBeaconsUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamParties.OnActiveBeaconsUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParties.OnActiveBeaconsUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		public static int ActiveBeaconCount
		{
			get
			{
				return (int)SteamParties.Internal.GetNumActiveBeacons();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public static IEnumerable<PartyBeacon> ActiveBeacons
		{
			get
			{
				uint i = 0U;
				while ((ulong)i < (ulong)((long)SteamParties.ActiveBeaconCount))
				{
					yield return new PartyBeacon
					{
						Id = SteamParties.Internal.GetBeaconByIndex(i)
					};
					uint num = i;
					i = num + 1U;
				}
				yield break;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public SteamParties()
		{
		}

		// Token: 0x0400070F RID: 1807
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnBeaconLocationsUpdated;

		// Token: 0x04000710 RID: 1808
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnActiveBeaconsUpdated;

		// Token: 0x0200023A RID: 570
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001136 RID: 4406 RVA: 0x0001E462 File Offset: 0x0001C662
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x0001E46E File Offset: 0x0001C66E
			public <>c()
			{
			}

			// Token: 0x06001138 RID: 4408 RVA: 0x0001E477 File Offset: 0x0001C677
			internal void <InstallEvents>b__3_0(AvailableBeaconLocationsUpdated_t x)
			{
				Action onBeaconLocationsUpdated = SteamParties.OnBeaconLocationsUpdated;
				if (onBeaconLocationsUpdated != null)
				{
					onBeaconLocationsUpdated();
				}
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x0001E48A File Offset: 0x0001C68A
			internal void <InstallEvents>b__3_1(ActiveBeaconsUpdated_t x)
			{
				Action onActiveBeaconsUpdated = SteamParties.OnActiveBeaconsUpdated;
				if (onActiveBeaconsUpdated != null)
				{
					onActiveBeaconsUpdated();
				}
			}

			// Token: 0x04000D47 RID: 3399
			public static readonly SteamParties.<>c <>9 = new SteamParties.<>c();

			// Token: 0x04000D48 RID: 3400
			public static Action<AvailableBeaconLocationsUpdated_t> <>9__3_0;

			// Token: 0x04000D49 RID: 3401
			public static Action<ActiveBeaconsUpdated_t> <>9__3_1;
		}

		// Token: 0x0200023B RID: 571
		[CompilerGenerated]
		private sealed class <get_ActiveBeacons>d__13 : IEnumerable<PartyBeacon>, IEnumerable, IEnumerator<PartyBeacon>, IDisposable, IEnumerator
		{
			// Token: 0x0600113A RID: 4410 RVA: 0x0001E49D File Offset: 0x0001C69D
			[DebuggerHidden]
			public <get_ActiveBeacons>d__13(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600113B RID: 4411 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600113C RID: 4412 RVA: 0x0001E4BC File Offset: 0x0001C6BC
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
					uint num2 = i;
					i = num2 + 1U;
				}
				else
				{
					this.<>1__state = -1;
					i = 0U;
				}
				if ((ulong)i >= (ulong)((long)SteamParties.ActiveBeaconCount))
				{
					return false;
				}
				this.<>2__current = new PartyBeacon
				{
					Id = SteamParties.Internal.GetBeaconByIndex(i)
				};
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x0600113D RID: 4413 RVA: 0x0001E54E File Offset: 0x0001C74E
			PartyBeacon IEnumerator<PartyBeacon>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600113E RID: 4414 RVA: 0x0001E556 File Offset: 0x0001C756
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x0600113F RID: 4415 RVA: 0x0001E55D File Offset: 0x0001C75D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001140 RID: 4416 RVA: 0x0001E56C File Offset: 0x0001C76C
			[DebuggerHidden]
			IEnumerator<PartyBeacon> IEnumerable<PartyBeacon>.GetEnumerator()
			{
				SteamParties.<get_ActiveBeacons>d__13 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamParties.<get_ActiveBeacons>d__13(0);
				}
				return result;
			}

			// Token: 0x06001141 RID: 4417 RVA: 0x0001E5A3 File Offset: 0x0001C7A3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.PartyBeacon>.GetEnumerator();
			}

			// Token: 0x04000D4A RID: 3402
			private int <>1__state;

			// Token: 0x04000D4B RID: 3403
			private PartyBeacon <>2__current;

			// Token: 0x04000D4C RID: 3404
			private int <>l__initialThreadId;

			// Token: 0x04000D4D RID: 3405
			private uint <i>5__1;
		}
	}
}
