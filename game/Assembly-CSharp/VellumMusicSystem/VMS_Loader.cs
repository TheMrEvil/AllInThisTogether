using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003C9 RID: 969
	public static class VMS_Loader
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001FD1 RID: 8145 RVA: 0x000BD1D8 File Offset: 0x000BB3D8
		// (remove) Token: 0x06001FD2 RID: 8146 RVA: 0x000BD20C File Offset: 0x000BB40C
		public static event Action<AudioClip> OnClipLoadStarted
		{
			[CompilerGenerated]
			add
			{
				Action<AudioClip> action = VMS_Loader.OnClipLoadStarted;
				Action<AudioClip> action2;
				do
				{
					action2 = action;
					Action<AudioClip> value2 = (Action<AudioClip>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<AudioClip>>(ref VMS_Loader.OnClipLoadStarted, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<AudioClip> action = VMS_Loader.OnClipLoadStarted;
				Action<AudioClip> action2;
				do
				{
					action2 = action;
					Action<AudioClip> value2 = (Action<AudioClip>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<AudioClip>>(ref VMS_Loader.OnClipLoadStarted, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001FD3 RID: 8147 RVA: 0x000BD240 File Offset: 0x000BB440
		// (remove) Token: 0x06001FD4 RID: 8148 RVA: 0x000BD274 File Offset: 0x000BB474
		public static event Action OnClipsLoaded
		{
			[CompilerGenerated]
			add
			{
				Action action = VMS_Loader.OnClipsLoaded;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Loader.OnClipsLoaded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = VMS_Loader.OnClipsLoaded;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Loader.OnClipsLoaded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000BD2A8 File Offset: 0x000BB4A8
		public static void LoadClipSetData(VMS_ClipSet clipSetToLoad)
		{
			if (clipSetToLoad.loaded)
			{
				return;
			}
			List<AudioClip> allClips = clipSetToLoad.GetAllClips();
			for (int i = 0; i < allClips.Count; i++)
			{
				if (allClips[i].loadState == AudioDataLoadState.Unloaded)
				{
					allClips[i].LoadAudioData();
					Action<AudioClip> onClipLoadStarted = VMS_Loader.OnClipLoadStarted;
					if (onClipLoadStarted != null)
					{
						onClipLoadStarted(allClips[i]);
					}
				}
			}
			clipSetToLoad.loaded = true;
			if (clipSetToLoad.fallbackClipSet != null)
			{
				VMS_Loader.LoadClipSetData(clipSetToLoad.fallbackClipSet);
			}
			Action onClipsLoaded = VMS_Loader.OnClipsLoaded;
			if (onClipsLoaded == null)
			{
				return;
			}
			onClipsLoaded();
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000BD338 File Offset: 0x000BB538
		public static void UnloadClipSetData(VMS_ClipSet clipSetToUnload)
		{
			if (!clipSetToUnload.loaded)
			{
				return;
			}
			List<AudioClip> allClips = clipSetToUnload.GetAllClips();
			for (int i = 0; i < allClips.Count; i++)
			{
				if (allClips[i].loadState != AudioDataLoadState.Unloaded)
				{
					allClips[i].UnloadAudioData();
				}
			}
			clipSetToUnload.loaded = false;
		}

		// Token: 0x04002006 RID: 8198
		[CompilerGenerated]
		private static Action<AudioClip> OnClipLoadStarted;

		// Token: 0x04002007 RID: 8199
		[CompilerGenerated]
		private static Action OnClipsLoaded;
	}
}
