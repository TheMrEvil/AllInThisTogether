using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A7 RID: 167
	public class SteamUtils : SteamSharedClass<SteamUtils>
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001048A File Offset: 0x0000E68A
		internal static ISteamUtils Internal
		{
			get
			{
				return SteamSharedClass<SteamUtils>.Interface as ISteamUtils;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00010496 File Offset: 0x0000E696
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamUtils(server));
			SteamUtils.InstallEvents(server);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000104B0 File Offset: 0x0000E6B0
		internal static void InstallEvents(bool server)
		{
			Dispatch.Install<IPCountry_t>(delegate(IPCountry_t x)
			{
				Action onIpCountryChanged = SteamUtils.OnIpCountryChanged;
				if (onIpCountryChanged != null)
				{
					onIpCountryChanged();
				}
			}, server);
			Dispatch.Install<LowBatteryPower_t>(delegate(LowBatteryPower_t x)
			{
				Action<int> onLowBatteryPower = SteamUtils.OnLowBatteryPower;
				if (onLowBatteryPower != null)
				{
					onLowBatteryPower((int)x.MinutesBatteryLeft);
				}
			}, server);
			Dispatch.Install<SteamShutdown_t>(delegate(SteamShutdown_t x)
			{
				SteamUtils.SteamClosed();
			}, server);
			Dispatch.Install<GamepadTextInputDismissed_t>(delegate(GamepadTextInputDismissed_t x)
			{
				Action<bool> onGamepadTextInputDismissed = SteamUtils.OnGamepadTextInputDismissed;
				if (onGamepadTextInputDismissed != null)
				{
					onGamepadTextInputDismissed(x.Submitted);
				}
			}, server);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00010556 File Offset: 0x0000E756
		private static void SteamClosed()
		{
			SteamClient.Cleanup();
			Action onSteamShutdown = SteamUtils.OnSteamShutdown;
			if (onSteamShutdown != null)
			{
				onSteamShutdown();
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000925 RID: 2341 RVA: 0x00010570 File Offset: 0x0000E770
		// (remove) Token: 0x06000926 RID: 2342 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public static event Action OnIpCountryChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUtils.OnIpCountryChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUtils.OnIpCountryChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUtils.OnIpCountryChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUtils.OnIpCountryChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000927 RID: 2343 RVA: 0x000105D8 File Offset: 0x0000E7D8
		// (remove) Token: 0x06000928 RID: 2344 RVA: 0x0001060C File Offset: 0x0000E80C
		public static event Action<int> OnLowBatteryPower
		{
			[CompilerGenerated]
			add
			{
				Action<int> action = SteamUtils.OnLowBatteryPower;
				Action<int> action2;
				do
				{
					action2 = action;
					Action<int> value2 = (Action<int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<int>>(ref SteamUtils.OnLowBatteryPower, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<int> action = SteamUtils.OnLowBatteryPower;
				Action<int> action2;
				do
				{
					action2 = action;
					Action<int> value2 = (Action<int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<int>>(ref SteamUtils.OnLowBatteryPower, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000929 RID: 2345 RVA: 0x00010640 File Offset: 0x0000E840
		// (remove) Token: 0x0600092A RID: 2346 RVA: 0x00010674 File Offset: 0x0000E874
		public static event Action OnSteamShutdown
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUtils.OnSteamShutdown;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUtils.OnSteamShutdown, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUtils.OnSteamShutdown;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUtils.OnSteamShutdown, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600092B RID: 2347 RVA: 0x000106A8 File Offset: 0x0000E8A8
		// (remove) Token: 0x0600092C RID: 2348 RVA: 0x000106DC File Offset: 0x0000E8DC
		public static event Action<bool> OnGamepadTextInputDismissed
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = SteamUtils.OnGamepadTextInputDismissed;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref SteamUtils.OnGamepadTextInputDismissed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = SteamUtils.OnGamepadTextInputDismissed;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref SteamUtils.OnGamepadTextInputDismissed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001070F File Offset: 0x0000E90F
		public static uint SecondsSinceAppActive
		{
			get
			{
				return SteamUtils.Internal.GetSecondsSinceAppActive();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001071B File Offset: 0x0000E91B
		public static uint SecondsSinceComputerActive
		{
			get
			{
				return SteamUtils.Internal.GetSecondsSinceComputerActive();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00010727 File Offset: 0x0000E927
		public static Universe ConnectedUniverse
		{
			get
			{
				return SteamUtils.Internal.GetConnectedUniverse();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00010733 File Offset: 0x0000E933
		public static DateTime SteamServerTime
		{
			get
			{
				return Epoch.ToDateTime(SteamUtils.Internal.GetServerRealTime());
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00010749 File Offset: 0x0000E949
		public static string IpCountry
		{
			get
			{
				return SteamUtils.Internal.GetIPCountry();
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00010758 File Offset: 0x0000E958
		public static bool GetImageSize(int image, out uint width, out uint height)
		{
			width = 0U;
			height = 0U;
			return SteamUtils.Internal.GetImageSize(image, ref width, ref height);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00010780 File Offset: 0x0000E980
		public static Image? GetImage(int image)
		{
			bool flag = image == -1;
			Image? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = image == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Image image2 = default(Image);
					bool flag3 = !SteamUtils.GetImageSize(image, out image2.Width, out image2.Height);
					if (flag3)
					{
						result = null;
					}
					else
					{
						uint num = image2.Width * image2.Height * 4U;
						byte[] array = Helpers.TakeBuffer((int)num);
						bool flag4 = !SteamUtils.Internal.GetImageRGBA(image, array, (int)num);
						if (flag4)
						{
							result = null;
						}
						else
						{
							image2.Data = new byte[num];
							Array.Copy(array, 0L, image2.Data, 0L, (long)((ulong)num));
							result = new Image?(image2);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001085C File Offset: 0x0000EA5C
		public static bool UsingBatteryPower
		{
			get
			{
				return SteamUtils.Internal.GetCurrentBatteryPower() != byte.MaxValue;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00010872 File Offset: 0x0000EA72
		public static float CurrentBatteryPower
		{
			get
			{
				return Math.Min((float)(SteamUtils.Internal.GetCurrentBatteryPower() / 100), 1f);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001088C File Offset: 0x0000EA8C
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00010893 File Offset: 0x0000EA93
		public static NotificationPosition OverlayNotificationPosition
		{
			get
			{
				return SteamUtils.overlayNotificationPosition;
			}
			set
			{
				SteamUtils.overlayNotificationPosition = value;
				SteamUtils.Internal.SetOverlayNotificationPosition(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000108A8 File Offset: 0x0000EAA8
		public static bool IsOverlayEnabled
		{
			get
			{
				return SteamUtils.Internal.IsOverlayEnabled();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public static bool DoesOverlayNeedPresent
		{
			get
			{
				return SteamUtils.Internal.BOverlayNeedsPresent();
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public static async Task<CheckFileSignature> CheckFileSignatureAsync(string filename)
		{
			CheckFileSignature_t? checkFileSignature_t = await SteamUtils.Internal.CheckFileSignature(filename);
			CheckFileSignature_t? r = checkFileSignature_t;
			checkFileSignature_t = null;
			if (r == null)
			{
				throw new Exception("Something went wrong");
			}
			return r.Value.CheckFileSignature;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00010908 File Offset: 0x0000EB08
		public static bool ShowGamepadTextInput(GamepadTextInputMode inputMode, GamepadTextInputLineMode lineInputMode, string description, int maxChars, string existingText = "")
		{
			return SteamUtils.Internal.ShowGamepadTextInput(inputMode, lineInputMode, description, (uint)maxChars, existingText);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static string GetEnteredGamepadText()
		{
			uint enteredGamepadTextLength = SteamUtils.Internal.GetEnteredGamepadTextLength();
			bool flag = enteredGamepadTextLength == 0U;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text;
				bool flag2 = !SteamUtils.Internal.GetEnteredGamepadTextInput(out text);
				if (flag2)
				{
					result = string.Empty;
				}
				else
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00010977 File Offset: 0x0000EB77
		public static string SteamUILanguage
		{
			get
			{
				return SteamUtils.Internal.GetSteamUILanguage();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00010983 File Offset: 0x0000EB83
		public static bool IsSteamRunningInVR
		{
			get
			{
				return SteamUtils.Internal.IsSteamRunningInVR();
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001098F File Offset: 0x0000EB8F
		public static void SetOverlayNotificationInset(int x, int y)
		{
			SteamUtils.Internal.SetOverlayNotificationInset(x, y);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001099F File Offset: 0x0000EB9F
		public static bool IsSteamInBigPictureMode
		{
			get
			{
				return SteamUtils.Internal.IsSteamInBigPictureMode();
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000109AB File Offset: 0x0000EBAB
		public static void StartVRDashboard()
		{
			SteamUtils.Internal.StartVRDashboard();
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000109B8 File Offset: 0x0000EBB8
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public static bool VrHeadsetStreaming
		{
			get
			{
				return SteamUtils.Internal.IsVRHeadsetStreamingEnabled();
			}
			set
			{
				SteamUtils.Internal.SetVRHeadsetStreamingEnabled(value);
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000109D4 File Offset: 0x0000EBD4
		internal static bool IsCallComplete(SteamAPICall_t call, out bool failed)
		{
			failed = false;
			return SteamUtils.Internal.IsAPICallCompleted(call, ref failed);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000109F5 File Offset: 0x0000EBF5
		public static bool IsSteamChinaLauncher
		{
			get
			{
				return SteamUtils.Internal.IsSteamChinaLauncher();
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00010A01 File Offset: 0x0000EC01
		public SteamUtils()
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00010A0A File Offset: 0x0000EC0A
		// Note: this type is marked as 'beforefieldinit'.
		static SteamUtils()
		{
		}

		// Token: 0x0400073B RID: 1851
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnIpCountryChanged;

		// Token: 0x0400073C RID: 1852
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<int> OnLowBatteryPower;

		// Token: 0x0400073D RID: 1853
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSteamShutdown;

		// Token: 0x0400073E RID: 1854
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<bool> OnGamepadTextInputDismissed;

		// Token: 0x0400073F RID: 1855
		private static NotificationPosition overlayNotificationPosition = NotificationPosition.BottomRight;

		// Token: 0x02000257 RID: 599
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060011AD RID: 4525 RVA: 0x000204DA File Offset: 0x0001E6DA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x000204E6 File Offset: 0x0001E6E6
			public <>c()
			{
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x000204EF File Offset: 0x0001E6EF
			internal void <InstallEvents>b__3_0(IPCountry_t x)
			{
				Action onIpCountryChanged = SteamUtils.OnIpCountryChanged;
				if (onIpCountryChanged != null)
				{
					onIpCountryChanged();
				}
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x00020502 File Offset: 0x0001E702
			internal void <InstallEvents>b__3_1(LowBatteryPower_t x)
			{
				Action<int> onLowBatteryPower = SteamUtils.OnLowBatteryPower;
				if (onLowBatteryPower != null)
				{
					onLowBatteryPower((int)x.MinutesBatteryLeft);
				}
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x0002051B File Offset: 0x0001E71B
			internal void <InstallEvents>b__3_2(SteamShutdown_t x)
			{
				SteamUtils.SteamClosed();
			}

			// Token: 0x060011B2 RID: 4530 RVA: 0x00020523 File Offset: 0x0001E723
			internal void <InstallEvents>b__3_3(GamepadTextInputDismissed_t x)
			{
				Action<bool> onGamepadTextInputDismissed = SteamUtils.OnGamepadTextInputDismissed;
				if (onGamepadTextInputDismissed != null)
				{
					onGamepadTextInputDismissed(x.Submitted);
				}
			}

			// Token: 0x04000DE9 RID: 3561
			public static readonly SteamUtils.<>c <>9 = new SteamUtils.<>c();

			// Token: 0x04000DEA RID: 3562
			public static Action<IPCountry_t> <>9__3_0;

			// Token: 0x04000DEB RID: 3563
			public static Action<LowBatteryPower_t> <>9__3_1;

			// Token: 0x04000DEC RID: 3564
			public static Action<SteamShutdown_t> <>9__3_2;

			// Token: 0x04000DED RID: 3565
			public static Action<GamepadTextInputDismissed_t> <>9__3_3;
		}

		// Token: 0x02000258 RID: 600
		[CompilerGenerated]
		private sealed class <CheckFileSignatureAsync>d__41 : IAsyncStateMachine
		{
			// Token: 0x060011B3 RID: 4531 RVA: 0x0002053C File Offset: 0x0001E73C
			public <CheckFileSignatureAsync>d__41()
			{
			}

			// Token: 0x060011B4 RID: 4532 RVA: 0x00020548 File Offset: 0x0001E748
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CheckFileSignature checkFileSignature;
				try
				{
					CallResult<CheckFileSignature_t> callResult;
					if (num != 0)
					{
						callResult = SteamUtils.Internal.CheckFileSignature(filename).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<CheckFileSignature_t> callResult2 = callResult;
							SteamUtils.<CheckFileSignatureAsync>d__41 <CheckFileSignatureAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<CheckFileSignature_t>, SteamUtils.<CheckFileSignatureAsync>d__41>(ref callResult, ref <CheckFileSignatureAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<CheckFileSignature_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<CheckFileSignature_t>);
						num2 = -1;
					}
					checkFileSignature_t = callResult.GetResult();
					r = checkFileSignature_t;
					checkFileSignature_t = null;
					bool flag = r == null;
					if (flag)
					{
						throw new Exception("Something went wrong");
					}
					checkFileSignature = r.Value.CheckFileSignature;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(checkFileSignature);
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x00020664 File Offset: 0x0001E864
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DEE RID: 3566
			public int <>1__state;

			// Token: 0x04000DEF RID: 3567
			public AsyncTaskMethodBuilder<CheckFileSignature> <>t__builder;

			// Token: 0x04000DF0 RID: 3568
			public string filename;

			// Token: 0x04000DF1 RID: 3569
			private CheckFileSignature_t? <r>5__1;

			// Token: 0x04000DF2 RID: 3570
			private CheckFileSignature_t? <>s__2;

			// Token: 0x04000DF3 RID: 3571
			private CallResult<CheckFileSignature_t> <>u__1;
		}
	}
}
