using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A1 RID: 161
	public class SteamScreenshots : SteamClientClass<SteamScreenshots>
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0000E165 File Offset: 0x0000C365
		internal static ISteamScreenshots Internal
		{
			get
			{
				return SteamClientClass<SteamScreenshots>.Interface as ISteamScreenshots;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000E171 File Offset: 0x0000C371
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamScreenshots(server));
			SteamScreenshots.InstallEvents();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0000E188 File Offset: 0x0000C388
		internal static void InstallEvents()
		{
			Dispatch.Install<ScreenshotRequested_t>(delegate(ScreenshotRequested_t x)
			{
				Action onScreenshotRequested = SteamScreenshots.OnScreenshotRequested;
				if (onScreenshotRequested != null)
				{
					onScreenshotRequested();
				}
			}, false);
			Dispatch.Install<ScreenshotReady_t>(delegate(ScreenshotReady_t x)
			{
				bool flag = x.Result != Result.OK;
				if (flag)
				{
					Action<Result> onScreenshotFailed = SteamScreenshots.OnScreenshotFailed;
					if (onScreenshotFailed != null)
					{
						onScreenshotFailed(x.Result);
					}
				}
				else
				{
					Action<Screenshot> onScreenshotReady = SteamScreenshots.OnScreenshotReady;
					if (onScreenshotReady != null)
					{
						onScreenshotReady(new Screenshot
						{
							Value = x.Local
						});
					}
				}
			}, false);
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x0000E218 File Offset: 0x0000C418
		public static event Action OnScreenshotRequested
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamScreenshots.OnScreenshotRequested;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamScreenshots.OnScreenshotRequested, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamScreenshots.OnScreenshotRequested;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamScreenshots.OnScreenshotRequested, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000877 RID: 2167 RVA: 0x0000E24C File Offset: 0x0000C44C
		// (remove) Token: 0x06000878 RID: 2168 RVA: 0x0000E280 File Offset: 0x0000C480
		public static event Action<Screenshot> OnScreenshotReady
		{
			[CompilerGenerated]
			add
			{
				Action<Screenshot> action = SteamScreenshots.OnScreenshotReady;
				Action<Screenshot> action2;
				do
				{
					action2 = action;
					Action<Screenshot> value2 = (Action<Screenshot>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Screenshot>>(ref SteamScreenshots.OnScreenshotReady, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Screenshot> action = SteamScreenshots.OnScreenshotReady;
				Action<Screenshot> action2;
				do
				{
					action2 = action;
					Action<Screenshot> value2 = (Action<Screenshot>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Screenshot>>(ref SteamScreenshots.OnScreenshotReady, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000879 RID: 2169 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		// (remove) Token: 0x0600087A RID: 2170 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public static event Action<Result> OnScreenshotFailed
		{
			[CompilerGenerated]
			add
			{
				Action<Result> action = SteamScreenshots.OnScreenshotFailed;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamScreenshots.OnScreenshotFailed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result> action = SteamScreenshots.OnScreenshotFailed;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamScreenshots.OnScreenshotFailed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000E31C File Offset: 0x0000C51C
		public unsafe static Screenshot? WriteScreenshot(byte[] data, int width, int height)
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
			ScreenshotHandle screenshotHandle = SteamScreenshots.Internal.WriteScreenshot((IntPtr)((void*)value), (uint)data.Length, width, height);
			bool flag = screenshotHandle.Value == 0U;
			Screenshot? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new Screenshot?(new Screenshot
				{
					Value = screenshotHandle
				});
			}
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000E394 File Offset: 0x0000C594
		public static Screenshot? AddScreenshot(string filename, string thumbnail, int width, int height)
		{
			ScreenshotHandle screenshotHandle = SteamScreenshots.Internal.AddScreenshotToLibrary(filename, thumbnail, width, height);
			bool flag = screenshotHandle.Value == 0U;
			Screenshot? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new Screenshot?(new Screenshot
				{
					Value = screenshotHandle
				});
			}
			return result;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		public static void TriggerScreenshot()
		{
			SteamScreenshots.Internal.TriggerScreenshot();
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0000E3FE File Offset: 0x0000C5FE
		public static bool Hooked
		{
			get
			{
				return SteamScreenshots.Internal.IsScreenshotsHooked();
			}
			set
			{
				SteamScreenshots.Internal.HookScreenshots(value);
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000E40C File Offset: 0x0000C60C
		public SteamScreenshots()
		{
		}

		// Token: 0x04000713 RID: 1811
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnScreenshotRequested;

		// Token: 0x04000714 RID: 1812
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Screenshot> OnScreenshotReady;

		// Token: 0x04000715 RID: 1813
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result> OnScreenshotFailed;

		// Token: 0x0200023E RID: 574
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600114E RID: 4430 RVA: 0x0001E713 File Offset: 0x0001C913
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600114F RID: 4431 RVA: 0x0001E71F File Offset: 0x0001C91F
			public <>c()
			{
			}

			// Token: 0x06001150 RID: 4432 RVA: 0x0001E728 File Offset: 0x0001C928
			internal void <InstallEvents>b__3_0(ScreenshotRequested_t x)
			{
				Action onScreenshotRequested = SteamScreenshots.OnScreenshotRequested;
				if (onScreenshotRequested != null)
				{
					onScreenshotRequested();
				}
			}

			// Token: 0x06001151 RID: 4433 RVA: 0x0001E73C File Offset: 0x0001C93C
			internal void <InstallEvents>b__3_1(ScreenshotReady_t x)
			{
				bool flag = x.Result != Result.OK;
				if (flag)
				{
					Action<Result> onScreenshotFailed = SteamScreenshots.OnScreenshotFailed;
					if (onScreenshotFailed != null)
					{
						onScreenshotFailed(x.Result);
					}
				}
				else
				{
					Action<Screenshot> onScreenshotReady = SteamScreenshots.OnScreenshotReady;
					if (onScreenshotReady != null)
					{
						onScreenshotReady(new Screenshot
						{
							Value = x.Local
						});
					}
				}
			}

			// Token: 0x04000D57 RID: 3415
			public static readonly SteamScreenshots.<>c <>9 = new SteamScreenshots.<>c();

			// Token: 0x04000D58 RID: 3416
			public static Action<ScreenshotRequested_t> <>9__3_0;

			// Token: 0x04000D59 RID: 3417
			public static Action<ScreenshotReady_t> <>9__3_1;
		}
	}
}
