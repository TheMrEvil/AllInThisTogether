using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A8 RID: 168
	public class SteamVideo : SteamClientClass<SteamVideo>
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00010A12 File Offset: 0x0000EC12
		internal static ISteamVideo Internal
		{
			get
			{
				return SteamClientClass<SteamVideo>.Interface as ISteamVideo;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00010A1E File Offset: 0x0000EC1E
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamVideo(server));
			SteamVideo.InstallEvents();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00010A38 File Offset: 0x0000EC38
		internal static void InstallEvents()
		{
			Dispatch.Install<BroadcastUploadStart_t>(delegate(BroadcastUploadStart_t x)
			{
				Action onBroadcastStarted = SteamVideo.OnBroadcastStarted;
				if (onBroadcastStarted != null)
				{
					onBroadcastStarted();
				}
			}, false);
			Dispatch.Install<BroadcastUploadStop_t>(delegate(BroadcastUploadStop_t x)
			{
				Action<BroadcastUploadResult> onBroadcastStopped = SteamVideo.OnBroadcastStopped;
				if (onBroadcastStopped != null)
				{
					onBroadcastStopped(x.Result);
				}
			}, false);
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600094B RID: 2379 RVA: 0x00010A94 File Offset: 0x0000EC94
		// (remove) Token: 0x0600094C RID: 2380 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public static event Action OnBroadcastStarted
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamVideo.OnBroadcastStarted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamVideo.OnBroadcastStarted, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamVideo.OnBroadcastStarted;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamVideo.OnBroadcastStarted, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600094D RID: 2381 RVA: 0x00010AFC File Offset: 0x0000ECFC
		// (remove) Token: 0x0600094E RID: 2382 RVA: 0x00010B30 File Offset: 0x0000ED30
		public static event Action<BroadcastUploadResult> OnBroadcastStopped
		{
			[CompilerGenerated]
			add
			{
				Action<BroadcastUploadResult> action = SteamVideo.OnBroadcastStopped;
				Action<BroadcastUploadResult> action2;
				do
				{
					action2 = action;
					Action<BroadcastUploadResult> value2 = (Action<BroadcastUploadResult>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<BroadcastUploadResult>>(ref SteamVideo.OnBroadcastStopped, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<BroadcastUploadResult> action = SteamVideo.OnBroadcastStopped;
				Action<BroadcastUploadResult> action2;
				do
				{
					action2 = action;
					Action<BroadcastUploadResult> value2 = (Action<BroadcastUploadResult>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<BroadcastUploadResult>>(ref SteamVideo.OnBroadcastStopped, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00010B64 File Offset: 0x0000ED64
		public static bool IsBroadcasting
		{
			get
			{
				int num = 0;
				return SteamVideo.Internal.IsBroadcasting(ref num);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00010B84 File Offset: 0x0000ED84
		public static int NumViewers
		{
			get
			{
				int num = 0;
				bool flag = !SteamVideo.Internal.IsBroadcasting(ref num);
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public SteamVideo()
		{
		}

		// Token: 0x04000740 RID: 1856
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnBroadcastStarted;

		// Token: 0x04000741 RID: 1857
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<BroadcastUploadResult> OnBroadcastStopped;

		// Token: 0x02000259 RID: 601
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060011B6 RID: 4534 RVA: 0x00020666 File Offset: 0x0001E866
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x00020672 File Offset: 0x0001E872
			public <>c()
			{
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x0002067B File Offset: 0x0001E87B
			internal void <InstallEvents>b__3_0(BroadcastUploadStart_t x)
			{
				Action onBroadcastStarted = SteamVideo.OnBroadcastStarted;
				if (onBroadcastStarted != null)
				{
					onBroadcastStarted();
				}
			}

			// Token: 0x060011B9 RID: 4537 RVA: 0x0002068E File Offset: 0x0001E88E
			internal void <InstallEvents>b__3_1(BroadcastUploadStop_t x)
			{
				Action<BroadcastUploadResult> onBroadcastStopped = SteamVideo.OnBroadcastStopped;
				if (onBroadcastStopped != null)
				{
					onBroadcastStopped(x.Result);
				}
			}

			// Token: 0x04000DF4 RID: 3572
			public static readonly SteamVideo.<>c <>9 = new SteamVideo.<>c();

			// Token: 0x04000DF5 RID: 3573
			public static Action<BroadcastUploadStart_t> <>9__3_0;

			// Token: 0x04000DF6 RID: 3574
			public static Action<BroadcastUploadStop_t> <>9__3_1;
		}
	}
}
