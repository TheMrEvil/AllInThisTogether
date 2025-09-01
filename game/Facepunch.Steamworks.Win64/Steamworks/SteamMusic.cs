using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000099 RID: 153
	public class SteamMusic : SteamClientClass<SteamMusic>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0000CE10 File Offset: 0x0000B010
		internal static ISteamMusic Internal
		{
			get
			{
				return SteamClientClass<SteamMusic>.Interface as ISteamMusic;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0000CE1C File Offset: 0x0000B01C
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamMusic(server));
			SteamMusic.InstallEvents();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0000CE34 File Offset: 0x0000B034
		internal static void InstallEvents()
		{
			Dispatch.Install<PlaybackStatusHasChanged_t>(delegate(PlaybackStatusHasChanged_t x)
			{
				Action onPlaybackChanged = SteamMusic.OnPlaybackChanged;
				if (onPlaybackChanged != null)
				{
					onPlaybackChanged();
				}
			}, false);
			Dispatch.Install<VolumeHasChanged_t>(delegate(VolumeHasChanged_t x)
			{
				Action<float> onVolumeChanged = SteamMusic.OnVolumeChanged;
				if (onVolumeChanged != null)
				{
					onVolumeChanged(x.NewVolume);
				}
			}, false);
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060007EB RID: 2027 RVA: 0x0000CE90 File Offset: 0x0000B090
		// (remove) Token: 0x060007EC RID: 2028 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public static event Action OnPlaybackChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamMusic.OnPlaybackChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamMusic.OnPlaybackChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamMusic.OnPlaybackChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamMusic.OnPlaybackChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060007ED RID: 2029 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		// (remove) Token: 0x060007EE RID: 2030 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public static event Action<float> OnVolumeChanged
		{
			[CompilerGenerated]
			add
			{
				Action<float> action = SteamMusic.OnVolumeChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref SteamMusic.OnVolumeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<float> action = SteamMusic.OnVolumeChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref SteamMusic.OnVolumeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0000CF5F File Offset: 0x0000B15F
		public static bool IsEnabled
		{
			get
			{
				return SteamMusic.Internal.BIsEnabled();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0000CF6B File Offset: 0x0000B16B
		public static bool IsPlaying
		{
			get
			{
				return SteamMusic.Internal.BIsPlaying();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0000CF77 File Offset: 0x0000B177
		public static MusicStatus Status
		{
			get
			{
				return SteamMusic.Internal.GetPlaybackStatus();
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000CF83 File Offset: 0x0000B183
		public static void Play()
		{
			SteamMusic.Internal.Play();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000CF90 File Offset: 0x0000B190
		public static void Pause()
		{
			SteamMusic.Internal.Pause();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public static void PlayPrevious()
		{
			SteamMusic.Internal.PlayPrevious();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		public static void PlayNext()
		{
			SteamMusic.Internal.PlayNext();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0000CFC3 File Offset: 0x0000B1C3
		public static float Volume
		{
			get
			{
				return SteamMusic.Internal.GetVolume();
			}
			set
			{
				SteamMusic.Internal.SetVolume(value);
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		public SteamMusic()
		{
		}

		// Token: 0x04000702 RID: 1794
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnPlaybackChanged;

		// Token: 0x04000703 RID: 1795
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<float> OnVolumeChanged;

		// Token: 0x02000234 RID: 564
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001125 RID: 4389 RVA: 0x0001E273 File Offset: 0x0001C473
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001126 RID: 4390 RVA: 0x0001E27F File Offset: 0x0001C47F
			public <>c()
			{
			}

			// Token: 0x06001127 RID: 4391 RVA: 0x0001E288 File Offset: 0x0001C488
			internal void <InstallEvents>b__3_0(PlaybackStatusHasChanged_t x)
			{
				Action onPlaybackChanged = SteamMusic.OnPlaybackChanged;
				if (onPlaybackChanged != null)
				{
					onPlaybackChanged();
				}
			}

			// Token: 0x06001128 RID: 4392 RVA: 0x0001E29B File Offset: 0x0001C49B
			internal void <InstallEvents>b__3_1(VolumeHasChanged_t x)
			{
				Action<float> onVolumeChanged = SteamMusic.OnVolumeChanged;
				if (onVolumeChanged != null)
				{
					onVolumeChanged(x.NewVolume);
				}
			}

			// Token: 0x04000D36 RID: 3382
			public static readonly SteamMusic.<>c <>9 = new SteamMusic.<>c();

			// Token: 0x04000D37 RID: 3383
			public static Action<PlaybackStatusHasChanged_t> <>9__3_0;

			// Token: 0x04000D38 RID: 3384
			public static Action<VolumeHasChanged_t> <>9__3_1;
		}
	}
}
