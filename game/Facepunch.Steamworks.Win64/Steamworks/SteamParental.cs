using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009D RID: 157
	public class SteamParental : SteamSharedClass<SteamParental>
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		internal static ISteamParentalSettings Internal
		{
			get
			{
				return SteamSharedClass<SteamParental>.Interface as ISteamParentalSettings;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0000DB48 File Offset: 0x0000BD48
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamParentalSettings(server));
			SteamParental.InstallEvents(server);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0000DB60 File Offset: 0x0000BD60
		internal static void InstallEvents(bool server)
		{
			Dispatch.Install<SteamParentalSettingsChanged_t>(delegate(SteamParentalSettingsChanged_t x)
			{
				Action onSettingsChanged = SteamParental.OnSettingsChanged;
				if (onSettingsChanged != null)
				{
					onSettingsChanged();
				}
			}, server);
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000840 RID: 2112 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		// (remove) Token: 0x06000841 RID: 2113 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public static event Action OnSettingsChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamParental.OnSettingsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParental.OnSettingsChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamParental.OnSettingsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamParental.OnSettingsChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public static bool IsParentalLockEnabled
		{
			get
			{
				return SteamParental.Internal.BIsParentalLockEnabled();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		public static bool IsParentalLockLocked
		{
			get
			{
				return SteamParental.Internal.BIsParentalLockLocked();
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		public static bool IsAppBlocked(AppId app)
		{
			return SteamParental.Internal.BIsAppBlocked(app.Value);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0000DC22 File Offset: 0x0000BE22
		public static bool BIsAppInBlockList(AppId app)
		{
			return SteamParental.Internal.BIsAppInBlockList(app.Value);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0000DC39 File Offset: 0x0000BE39
		public static bool IsFeatureBlocked(ParentalFeature feature)
		{
			return SteamParental.Internal.BIsFeatureBlocked(feature);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0000DC46 File Offset: 0x0000BE46
		public static bool BIsFeatureInBlockList(ParentalFeature feature)
		{
			return SteamParental.Internal.BIsFeatureInBlockList(feature);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0000DC53 File Offset: 0x0000BE53
		public SteamParental()
		{
		}

		// Token: 0x0400070E RID: 1806
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSettingsChanged;

		// Token: 0x02000239 RID: 569
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001133 RID: 4403 RVA: 0x0001E43A File Offset: 0x0001C63A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001134 RID: 4404 RVA: 0x0001E446 File Offset: 0x0001C646
			public <>c()
			{
			}

			// Token: 0x06001135 RID: 4405 RVA: 0x0001E44F File Offset: 0x0001C64F
			internal void <InstallEvents>b__3_0(SteamParentalSettingsChanged_t x)
			{
				Action onSettingsChanged = SteamParental.OnSettingsChanged;
				if (onSettingsChanged != null)
				{
					onSettingsChanged();
				}
			}

			// Token: 0x04000D45 RID: 3397
			public static readonly SteamParental.<>c <>9 = new SteamParental.<>c();

			// Token: 0x04000D46 RID: 3398
			public static Action<SteamParentalSettingsChanged_t> <>9__3_0;
		}
	}
}
