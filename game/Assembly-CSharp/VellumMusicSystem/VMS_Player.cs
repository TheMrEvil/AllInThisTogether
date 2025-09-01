using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003CA RID: 970
	public class VMS_Player : MonoBehaviour
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001FD7 RID: 8151 RVA: 0x000BD388 File Offset: 0x000BB588
		// (remove) Token: 0x06001FD8 RID: 8152 RVA: 0x000BD3BC File Offset: 0x000BB5BC
		public static event Action OnMusicPlayerStart
		{
			[CompilerGenerated]
			add
			{
				Action action = VMS_Player.OnMusicPlayerStart;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerStart, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = VMS_Player.OnMusicPlayerStart;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerStart, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001FD9 RID: 8153 RVA: 0x000BD3F0 File Offset: 0x000BB5F0
		// (remove) Token: 0x06001FDA RID: 8154 RVA: 0x000BD424 File Offset: 0x000BB624
		public static event Action OnMusicPlayerStopping
		{
			[CompilerGenerated]
			add
			{
				Action action = VMS_Player.OnMusicPlayerStopping;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerStopping, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = VMS_Player.OnMusicPlayerStopping;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerStopping, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001FDB RID: 8155 RVA: 0x000BD458 File Offset: 0x000BB658
		// (remove) Token: 0x06001FDC RID: 8156 RVA: 0x000BD48C File Offset: 0x000BB68C
		public static event Action OnMusicPlayerReset
		{
			[CompilerGenerated]
			add
			{
				Action action = VMS_Player.OnMusicPlayerReset;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerReset, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = VMS_Player.OnMusicPlayerReset;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnMusicPlayerReset, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001FDD RID: 8157 RVA: 0x000BD4C0 File Offset: 0x000BB6C0
		// (remove) Token: 0x06001FDE RID: 8158 RVA: 0x000BD4F4 File Offset: 0x000BB6F4
		public static event Action OnScheduleNewClips
		{
			[CompilerGenerated]
			add
			{
				Action action = VMS_Player.OnScheduleNewClips;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnScheduleNewClips, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = VMS_Player.OnScheduleNewClips;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref VMS_Player.OnScheduleNewClips, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001FDF RID: 8159 RVA: 0x000BD528 File Offset: 0x000BB728
		// (remove) Token: 0x06001FE0 RID: 8160 RVA: 0x000BD55C File Offset: 0x000BB75C
		public static event Action<VMS_TimeCalculationEvent> OnTimeCalculated
		{
			[CompilerGenerated]
			add
			{
				Action<VMS_TimeCalculationEvent> action = VMS_Player.OnTimeCalculated;
				Action<VMS_TimeCalculationEvent> action2;
				do
				{
					action2 = action;
					Action<VMS_TimeCalculationEvent> value2 = (Action<VMS_TimeCalculationEvent>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_TimeCalculationEvent>>(ref VMS_Player.OnTimeCalculated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<VMS_TimeCalculationEvent> action = VMS_Player.OnTimeCalculated;
				Action<VMS_TimeCalculationEvent> action2;
				do
				{
					action2 = action;
					Action<VMS_TimeCalculationEvent> value2 = (Action<VMS_TimeCalculationEvent>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_TimeCalculationEvent>>(ref VMS_Player.OnTimeCalculated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001FE1 RID: 8161 RVA: 0x000BD590 File Offset: 0x000BB790
		// (remove) Token: 0x06001FE2 RID: 8162 RVA: 0x000BD5C4 File Offset: 0x000BB7C4
		public static event Action<bool> OnMenuStateChange
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = VMS_Player.OnMenuStateChange;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref VMS_Player.OnMenuStateChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = VMS_Player.OnMenuStateChange;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref VMS_Player.OnMenuStateChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001FE3 RID: 8163 RVA: 0x000BD5F8 File Offset: 0x000BB7F8
		// (remove) Token: 0x06001FE4 RID: 8164 RVA: 0x000BD62C File Offset: 0x000BB82C
		public static event Action<VMS_ClipSet> OnClipSetChanged
		{
			[CompilerGenerated]
			add
			{
				Action<VMS_ClipSet> action = VMS_Player.OnClipSetChanged;
				Action<VMS_ClipSet> action2;
				do
				{
					action2 = action;
					Action<VMS_ClipSet> value2 = (Action<VMS_ClipSet>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_ClipSet>>(ref VMS_Player.OnClipSetChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<VMS_ClipSet> action = VMS_Player.OnClipSetChanged;
				Action<VMS_ClipSet> action2;
				do
				{
					action2 = action;
					Action<VMS_ClipSet> value2 = (Action<VMS_ClipSet>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_ClipSet>>(ref VMS_Player.OnClipSetChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001FE5 RID: 8165 RVA: 0x000BD660 File Offset: 0x000BB860
		// (remove) Token: 0x06001FE6 RID: 8166 RVA: 0x000BD694 File Offset: 0x000BB894
		public static event Action<VMS_ClipSet> OnNewClipSetChosen
		{
			[CompilerGenerated]
			add
			{
				Action<VMS_ClipSet> action = VMS_Player.OnNewClipSetChosen;
				Action<VMS_ClipSet> action2;
				do
				{
					action2 = action;
					Action<VMS_ClipSet> value2 = (Action<VMS_ClipSet>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_ClipSet>>(ref VMS_Player.OnNewClipSetChosen, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<VMS_ClipSet> action = VMS_Player.OnNewClipSetChosen;
				Action<VMS_ClipSet> action2;
				do
				{
					action2 = action;
					Action<VMS_ClipSet> value2 = (Action<VMS_ClipSet>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<VMS_ClipSet>>(ref VMS_Player.OnNewClipSetChosen, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000BD6C7 File Offset: 0x000BB8C7
		public bool IsPlaying
		{
			get
			{
				return this.running;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000BD6CF File Offset: 0x000BB8CF
		public static double CurrentMusicTime
		{
			get
			{
				return AudioSettings.dspTime - VMS_Player.lastMusicTime;
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000BD6DC File Offset: 0x000BB8DC
		private void Awake()
		{
			this.defaultStoppingTime = this.stoppingTime;
			if (VMS_Player.instance != null && VMS_Player.instance != this)
			{
				UnityEngine.Debug.LogWarning("Only one music player can exist in the scene. " + base.gameObject.name + " has been destroyed.");
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			VMS_Player.instance = this;
			this.audioSourceController = base.GetComponentInChildren<VMS_AudioSourceController>();
			if (this.audioSourceController == null)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("No Audio Source Controller has been set. " + base.gameObject.name + " has been destroyed.");
				}
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.audioSourceController.SetLoadType(this.autoUnloadClips);
			if (this.neverAutoUnloadAmbience)
			{
				this.audioSourceController.ambienceSource.autoUnload = false;
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000BD7B8 File Offset: 0x000BB9B8
		private void Start()
		{
			if (this.callSceneEventsOnStart)
			{
				Action<bool> onMenuStateChange = VMS_Player.OnMenuStateChange;
				if (onMenuStateChange != null)
				{
					onMenuStateChange(this.sceneData.inMenu);
				}
				Action<VMS_ClipSet> onNewClipSetChosen = VMS_Player.OnNewClipSetChosen;
				if (onNewClipSetChosen != null)
				{
					onNewClipSetChosen(this.sceneData.clipSet);
				}
			}
			if (this.playOnAwake)
			{
				this.StartMusicPlayer();
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000BD811 File Offset: 0x000BBA11
		private void OnDisable()
		{
			VMS_Player.OnMusicPlayerReset -= this.RestartOnStop;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000BD824 File Offset: 0x000BBA24
		public void StartMusicPlayer(VMS_ClipSet set, bool startInMenu)
		{
			if (!base.isActiveAndEnabled)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("Cannot start player, the game object is disabled.");
				}
				return;
			}
			this.ToggleMenu(startInMenu);
			this.StartMusicPlayer(set);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000BD850 File Offset: 0x000BBA50
		public void StartMusicPlayer(VMS_ClipSet set)
		{
			if (!base.isActiveAndEnabled)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("Cannot start player, the game object is disabled.");
				}
				return;
			}
			this.SetClipSet(set);
			if (this.running)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("Cannot start player, it's already running.");
				}
				return;
			}
			base.StartCoroutine(this.RunMusicPlayer());
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000BD8A8 File Offset: 0x000BBAA8
		public void StartMusicPlayer()
		{
			if (!base.isActiveAndEnabled)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("Cannot start player, the game object is disabled.");
				}
				return;
			}
			if (this.sceneData.clipSet == null)
			{
				if (this.logWarnings)
				{
					UnityEngine.Debug.LogWarning("Cannot start player, no Clip Set has been specified. Select a Clip Set or use the Clip Set overload method.");
				}
				return;
			}
			this.StartMusicPlayer(this.sceneData.clipSet);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000BD908 File Offset: 0x000BBB08
		public void StopMusicPlayer()
		{
			bool flag;
			this.StopMusicPlayer(out flag, -1f);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000BD922 File Offset: 0x000BBB22
		public void StopMusicPlayer(float stoppingTime)
		{
			this.stoppingTime = stoppingTime;
			this.StopMusicPlayer();
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000BD934 File Offset: 0x000BBB34
		private void StopMusicPlayer(out bool successfull, float overrideStopTime = -1f)
		{
			this.stoppingTime = ((overrideStopTime >= 0f) ? overrideStopTime : this.defaultStoppingTime);
			if (!this.running && this.logWarnings)
			{
				UnityEngine.Debug.LogWarning("Cannot stop player, it isn't running.");
				successfull = false;
				return;
			}
			if (this.stopping)
			{
				UnityEngine.Debug.LogWarning("Cannot stop player, it is already stopping.");
				successfull = false;
				return;
			}
			if (this.running)
			{
				this.stopping = true;
				successfull = true;
				return;
			}
			successfull = false;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000BD9A2 File Offset: 0x000BBBA2
		public void ToggleMenu()
		{
			this.ToggleMenu(!this.sceneData.inMenu);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000BD9B8 File Offset: 0x000BBBB8
		public void ToggleMenu(bool inGame)
		{
			this.sceneData.inMenu = inGame;
			Action<bool> onMenuStateChange = VMS_Player.OnMenuStateChange;
			if (onMenuStateChange != null)
			{
				onMenuStateChange(this.sceneData.inMenu);
			}
			this.audioSourceController.ToggleMenuMix(inGame, this.menuFadeTime);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000BD9F4 File Offset: 0x000BBBF4
		public void SetClipSet(VMS_ClipSet clipSet)
		{
			if (this.sceneData.clipSet == clipSet)
			{
				return;
			}
			if (clipSet.restartOnly || (this.sceneData.clipSet != null && this.sceneData.clipSet.restartOnly))
			{
				this.RestartMusicPlayer(clipSet, -1f);
				return;
			}
			this.sceneData.clipSet = clipSet;
			Action<VMS_ClipSet> onNewClipSetChosen = VMS_Player.OnNewClipSetChosen;
			if (onNewClipSetChosen == null)
			{
				return;
			}
			onNewClipSetChosen(this.sceneData.clipSet);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000BDA78 File Offset: 0x000BBC78
		public void RestartMusicPlayer(VMS_ClipSet clipSet, float overrideStopTime = -1f)
		{
			VMS_Player.OnMusicPlayerReset += this.RestartOnStop;
			bool flag;
			this.StopMusicPlayer(out flag, overrideStopTime);
			if (!flag)
			{
				VMS_Player.OnMusicPlayerReset -= this.RestartOnStop;
			}
			if (this.sceneData.clipSet != clipSet)
			{
				this.sceneData.clipSet = clipSet;
				Action<VMS_ClipSet> onNewClipSetChosen = VMS_Player.OnNewClipSetChosen;
				if (onNewClipSetChosen == null)
				{
					return;
				}
				onNewClipSetChosen(this.sceneData.clipSet);
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000BDAEC File Offset: 0x000BBCEC
		private void RestartOnStop()
		{
			this.StartMusicPlayer();
			VMS_Player.OnMusicPlayerReset -= this.RestartOnStop;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000BDB08 File Offset: 0x000BBD08
		public void UnloadUnusedClips()
		{
			int num = this.audioSourceController.UnloadUnusedClips();
			if (this.logWarnings)
			{
				UnityEngine.Debug.Log(num.ToString() + " clips were unloaded.");
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000BDB3F File Offset: 0x000BBD3F
		public double GetCurrentBarTime()
		{
			if (this.sceneData.clipSet == null)
			{
				return 10.0;
			}
			return this.sceneData.clipSet.setInfo.BarTime(2);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000BDB74 File Offset: 0x000BBD74
		public double GetCurrentBeatLength()
		{
			if (this.sceneData.clipSet == null)
			{
				return 10.0;
			}
			return this.sceneData.clipSet.setInfo.BeatTime();
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000BDBA8 File Offset: 0x000BBDA8
		private IEnumerator RunMusicPlayer()
		{
			VMS_Sequencer sequencer = new VMS_Sequencer();
			double dspTime = AudioSettings.dspTime;
			double tail = this.sceneData.clipSet.setInfo.TailTime();
			double intro = this.sceneData.clipSet.setInfo.IntroTime();
			bool halfPointReached = false;
			this.sceneData.lastClipSet = this.sceneData.clipSet;
			this.audioSourceController.SetInitialVolume(this.sceneData.clipSet.volumeProfile, this.sceneData.inMenu);
			this.running = true;
			Action onMusicPlayerStart = VMS_Player.OnMusicPlayerStart;
			if (onMusicPlayerStart != null)
			{
				onMusicPlayerStart();
			}
			double endTime;
			double lastStartTime = this.ScheduleClips(dspTime + (double)this.startDelay, out endTime, sequencer.GetSequencerEvents(this.sceneData, this.sceneData.clipSet.setInfo.sequencerSettingsProfile));
			VMS_Player.lastMusicTime = lastStartTime;
			Action<VMS_TimeCalculationEvent> onTimeCalculated = VMS_Player.OnTimeCalculated;
			if (onTimeCalculated != null)
			{
				onTimeCalculated(new VMS_TimeCalculationEvent
				{
					rawStartTime = lastStartTime,
					startTime = lastStartTime + intro,
					rawEndTime = endTime,
					endTime = endTime - tail
				});
			}
			if (this.logWarnings && this.startDelay < 1f)
			{
				UnityEngine.Debug.LogWarning("Start Delay is set to less than one second. Using a very low start delay value may cause timing issues on systems with slow load times.");
			}
			while (!this.stopping)
			{
				dspTime = AudioSettings.dspTime;
				if (dspTime > lastStartTime + intro + (endTime - tail - (lastStartTime + intro)) / 2.0)
				{
					halfPointReached = true;
					yield return null;
				}
				if (halfPointReached)
				{
					if (this.sceneData.clipSet != this.sceneData.lastClipSet)
					{
						this.sceneData.clipSetHasChanged = true;
						Action<VMS_ClipSet> onClipSetChanged = VMS_Player.OnClipSetChanged;
						if (onClipSetChanged != null)
						{
							onClipSetChanged(this.sceneData.clipSet);
						}
					}
					lastStartTime = this.ScheduleClips(endTime - (intro + tail), out endTime, sequencer.GetSequencerEvents(this.sceneData, this.sceneData.clipSet.setInfo.sequencerSettingsProfile));
					Action<VMS_TimeCalculationEvent> onTimeCalculated2 = VMS_Player.OnTimeCalculated;
					if (onTimeCalculated2 != null)
					{
						onTimeCalculated2(new VMS_TimeCalculationEvent
						{
							rawStartTime = lastStartTime,
							startTime = lastStartTime + intro,
							rawEndTime = endTime,
							endTime = endTime - tail
						});
					}
					this.sceneData.clipSetHasChanged = false;
					this.sceneData.lastClipSet = this.sceneData.clipSet;
					halfPointReached = false;
				}
				yield return null;
			}
			Action onMusicPlayerStopping = VMS_Player.OnMusicPlayerStopping;
			if (onMusicPlayerStopping != null)
			{
				onMusicPlayerStopping();
			}
			this.audioSourceController.DisableAllSources(new Action(this.ResetPlayer), this.stoppingTime);
			yield break;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000BDBB7 File Offset: 0x000BBDB7
		private void ResetPlayer()
		{
			this.stopping = false;
			this.running = false;
			if (this.unloadAllOnStop)
			{
				this.UnloadUnusedClips();
			}
			Action onMusicPlayerReset = VMS_Player.OnMusicPlayerReset;
			if (onMusicPlayerReset == null)
			{
				return;
			}
			onMusicPlayerReset();
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000BDBE4 File Offset: 0x000BBDE4
		private double ScheduleClips(double startTime, out double clipEndTime, VMS_SequencerEvent sequencerEvent)
		{
			VMS_Player.<>c__DisplayClass63_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sequencerEvent = sequencerEvent;
			CS$<>8__locals1.startTime = startTime;
			Action onScheduleNewClips = VMS_Player.OnScheduleNewClips;
			if (onScheduleNewClips != null)
			{
				onScheduleNewClips();
			}
			float fadeDuration = (float)this.sceneData.clipSet.setInfo.BarTime(5);
			this.audioSourceController.FadeToNewVolumeProfile(this.sceneData.clipSet.volumeProfile, fadeDuration);
			double num;
			this.<ScheduleClips>g__ScheduleClip|63_0(VMS_StemType.Melody, CS$<>8__locals1.sequencerEvent.melody, this.audioSourceController.melodySource, out num, ref CS$<>8__locals1);
			this.<ScheduleClips>g__ScheduleClip|63_0(VMS_StemType.Rhythm, CS$<>8__locals1.sequencerEvent.rhythm, this.audioSourceController.rhythmSource, out num, ref CS$<>8__locals1);
			this.<ScheduleClips>g__ScheduleClip|63_0(VMS_StemType.Bass, CS$<>8__locals1.sequencerEvent.bass, this.audioSourceController.bassSource, out num, ref CS$<>8__locals1);
			this.<ScheduleClips>g__ScheduleClip|63_0(VMS_StemType.Drums, CS$<>8__locals1.sequencerEvent.drums, this.audioSourceController.drumsSource, out num, ref CS$<>8__locals1);
			AudioClip clip = this.sceneData.clipSet.GetClip(VMS_StemType.Ambience, CS$<>8__locals1.sequencerEvent.ambience.section, CS$<>8__locals1.sequencerEvent.subSection);
			if (clip != null)
			{
				double num2;
				this.audioSourceController.ambienceSource.Play(clip, CS$<>8__locals1.startTime, out num2);
				clipEndTime = num2;
				return CS$<>8__locals1.startTime;
			}
			if (clip == null && this.logWarnings)
			{
				UnityEngine.Debug.LogWarning("The requested Ambience clip was missing. Ambience clips are required for the player to run. The player will be stopped.");
			}
			this.StopMusicPlayer();
			clipEndTime = CS$<>8__locals1.startTime;
			return CS$<>8__locals1.startTime;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000BDD5C File Offset: 0x000BBF5C
		public VMS_Player()
		{
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000BDDAC File Offset: 0x000BBFAC
		[CompilerGenerated]
		private void <ScheduleClips>g__ScheduleClip|63_0(VMS_StemType stemType, VMS_StemEvent stemEvent, VMS_AudioSource audioSource, out double endTime, ref VMS_Player.<>c__DisplayClass63_0 A_5)
		{
			if (stemEvent.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				AudioClip clip = this.sceneData.clipSet.GetClip(stemType, stemEvent.section, A_5.sequencerEvent.subSection);
				if (clip != null)
				{
					audioSource.Play(clip, A_5.startTime, out endTime);
					return;
				}
			}
			if (stemEvent.section == -1 && this.sceneData.lastClipSet != null)
			{
				AudioClip clip2 = this.sceneData.lastClipSet.GetClip(stemType, stemEvent.section, A_5.sequencerEvent.subSection);
				if (clip2 != null)
				{
					audioSource.Play(clip2, A_5.startTime, out endTime);
					return;
				}
			}
			endTime = A_5.startTime;
		}

		// Token: 0x04002008 RID: 8200
		public static VMS_Player instance;

		// Token: 0x04002009 RID: 8201
		private VMS_AudioSourceController audioSourceController;

		// Token: 0x0400200A RID: 8202
		[CompilerGenerated]
		private static Action OnMusicPlayerStart;

		// Token: 0x0400200B RID: 8203
		[CompilerGenerated]
		private static Action OnMusicPlayerStopping;

		// Token: 0x0400200C RID: 8204
		[CompilerGenerated]
		private static Action OnMusicPlayerReset;

		// Token: 0x0400200D RID: 8205
		[CompilerGenerated]
		private static Action OnScheduleNewClips;

		// Token: 0x0400200E RID: 8206
		[CompilerGenerated]
		private static Action<VMS_TimeCalculationEvent> OnTimeCalculated;

		// Token: 0x0400200F RID: 8207
		[CompilerGenerated]
		private static Action<bool> OnMenuStateChange;

		// Token: 0x04002010 RID: 8208
		[CompilerGenerated]
		private static Action<VMS_ClipSet> OnClipSetChanged;

		// Token: 0x04002011 RID: 8209
		[CompilerGenerated]
		private static Action<VMS_ClipSet> OnNewClipSetChosen;

		// Token: 0x04002012 RID: 8210
		private bool running;

		// Token: 0x04002013 RID: 8211
		private bool stopping;

		// Token: 0x04002014 RID: 8212
		[Header("Player Settings")]
		[Space(10f)]
		[Tooltip("Enables status warnings when trying to use the player. (for general debugging, add a VMS_Debugger)")]
		public bool logWarnings;

		// Token: 0x04002015 RID: 8213
		[SerializeField]
		private bool playOnAwake = true;

		// Token: 0x04002016 RID: 8214
		[Tooltip("The delay between starting the player and playback. Allows time for the initial clips to be loaded.")]
		[SerializeField]
		private float startDelay = 2f;

		// Token: 0x04002017 RID: 8215
		[Space(10f)]
		[Header("Fade Settings")]
		[Space(10f)]
		[Tooltip("The time duration when fading between menu states")]
		[SerializeField]
		private float menuFadeTime = 1.5f;

		// Token: 0x04002018 RID: 8216
		[Tooltip("The time duration when fading out the player")]
		[SerializeField]
		private float stoppingTime = 3f;

		// Token: 0x04002019 RID: 8217
		private float defaultStoppingTime;

		// Token: 0x0400201A RID: 8218
		[Space(10f)]
		[Tooltip("Automatically unloads clips when new clips are scheduled. Warning: Setting this to false will cause high memory use.")]
		[SerializeField]
		private bool autoUnloadClips = true;

		// Token: 0x0400201B RID: 8219
		[Tooltip("Prevents ambience clips from being unloaded. Avoids uneccessary unloading / reloading of ambience clips, increases memory use.")]
		[SerializeField]
		private bool neverAutoUnloadAmbience;

		// Token: 0x0400201C RID: 8220
		[Tooltip("Automatically unloads referenced clips after stopping the player.")]
		[SerializeField]
		private bool unloadAllOnStop = true;

		// Token: 0x0400201D RID: 8221
		[Space(10f)]
		[Tooltip("Calls scene data events in start. Used to set debug UI buttons etc.")]
		[SerializeField]
		private bool callSceneEventsOnStart = true;

		// Token: 0x0400201E RID: 8222
		[SerializeField]
		private VMS_SceneData sceneData;

		// Token: 0x0400201F RID: 8223
		private static double lastMusicTime;

		// Token: 0x020006A1 RID: 1697
		[CompilerGenerated]
		private sealed class <RunMusicPlayer>d__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002833 RID: 10291 RVA: 0x000D8073 File Offset: 0x000D6273
			[DebuggerHidden]
			public <RunMusicPlayer>d__61(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002834 RID: 10292 RVA: 0x000D8082 File Offset: 0x000D6282
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002835 RID: 10293 RVA: 0x000D8084 File Offset: 0x000D6284
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				VMS_Player vms_Player = this;
				double dspTime;
				switch (num)
				{
				case 0:
				{
					this.<>1__state = -1;
					sequencer = new VMS_Sequencer();
					dspTime = AudioSettings.dspTime;
					tail = vms_Player.sceneData.clipSet.setInfo.TailTime();
					intro = vms_Player.sceneData.clipSet.setInfo.IntroTime();
					halfPointReached = false;
					vms_Player.sceneData.lastClipSet = vms_Player.sceneData.clipSet;
					vms_Player.audioSourceController.SetInitialVolume(vms_Player.sceneData.clipSet.volumeProfile, vms_Player.sceneData.inMenu);
					vms_Player.running = true;
					Action onMusicPlayerStart = VMS_Player.OnMusicPlayerStart;
					if (onMusicPlayerStart != null)
					{
						onMusicPlayerStart();
					}
					lastStartTime = vms_Player.ScheduleClips(dspTime + (double)vms_Player.startDelay, out endTime, sequencer.GetSequencerEvents(vms_Player.sceneData, vms_Player.sceneData.clipSet.setInfo.sequencerSettingsProfile));
					VMS_Player.lastMusicTime = lastStartTime;
					Action<VMS_TimeCalculationEvent> onTimeCalculated = VMS_Player.OnTimeCalculated;
					if (onTimeCalculated != null)
					{
						onTimeCalculated(new VMS_TimeCalculationEvent
						{
							rawStartTime = lastStartTime,
							startTime = lastStartTime + intro,
							rawEndTime = endTime,
							endTime = endTime - tail
						});
					}
					if (vms_Player.logWarnings && vms_Player.startDelay < 1f)
					{
						UnityEngine.Debug.LogWarning("Start Delay is set to less than one second. Using a very low start delay value may cause timing issues on systems with slow load times.");
						goto IL_32B;
					}
					goto IL_32B;
				}
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_32B;
				default:
					return false;
				}
				IL_1F5:
				if (halfPointReached)
				{
					if (vms_Player.sceneData.clipSet != vms_Player.sceneData.lastClipSet)
					{
						vms_Player.sceneData.clipSetHasChanged = true;
						Action<VMS_ClipSet> onClipSetChanged = VMS_Player.OnClipSetChanged;
						if (onClipSetChanged != null)
						{
							onClipSetChanged(vms_Player.sceneData.clipSet);
						}
					}
					lastStartTime = vms_Player.ScheduleClips(endTime - (intro + tail), out endTime, sequencer.GetSequencerEvents(vms_Player.sceneData, vms_Player.sceneData.clipSet.setInfo.sequencerSettingsProfile));
					Action<VMS_TimeCalculationEvent> onTimeCalculated2 = VMS_Player.OnTimeCalculated;
					if (onTimeCalculated2 != null)
					{
						onTimeCalculated2(new VMS_TimeCalculationEvent
						{
							rawStartTime = lastStartTime,
							startTime = lastStartTime + intro,
							rawEndTime = endTime,
							endTime = endTime - tail
						});
					}
					vms_Player.sceneData.clipSetHasChanged = false;
					vms_Player.sceneData.lastClipSet = vms_Player.sceneData.clipSet;
					halfPointReached = false;
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
				IL_32B:
				if (vms_Player.stopping)
				{
					Action onMusicPlayerStopping = VMS_Player.OnMusicPlayerStopping;
					if (onMusicPlayerStopping != null)
					{
						onMusicPlayerStopping();
					}
					vms_Player.audioSourceController.DisableAllSources(new Action(vms_Player.ResetPlayer), vms_Player.stoppingTime);
					return false;
				}
				dspTime = AudioSettings.dspTime;
				if (dspTime > lastStartTime + intro + (endTime - tail - (lastStartTime + intro)) / 2.0)
				{
					halfPointReached = true;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				goto IL_1F5;
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06002836 RID: 10294 RVA: 0x000D83F5 File Offset: 0x000D65F5
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002837 RID: 10295 RVA: 0x000D83FD File Offset: 0x000D65FD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06002838 RID: 10296 RVA: 0x000D8404 File Offset: 0x000D6604
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C68 RID: 11368
			private int <>1__state;

			// Token: 0x04002C69 RID: 11369
			private object <>2__current;

			// Token: 0x04002C6A RID: 11370
			public VMS_Player <>4__this;

			// Token: 0x04002C6B RID: 11371
			private VMS_Sequencer <sequencer>5__2;

			// Token: 0x04002C6C RID: 11372
			private double <lastStartTime>5__3;

			// Token: 0x04002C6D RID: 11373
			private double <tail>5__4;

			// Token: 0x04002C6E RID: 11374
			private double <intro>5__5;

			// Token: 0x04002C6F RID: 11375
			private bool <halfPointReached>5__6;

			// Token: 0x04002C70 RID: 11376
			private double <endTime>5__7;
		}

		// Token: 0x020006A2 RID: 1698
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass63_0
		{
			// Token: 0x04002C71 RID: 11377
			public VMS_Player <>4__this;

			// Token: 0x04002C72 RID: 11378
			public VMS_SequencerEvent sequencerEvent;

			// Token: 0x04002C73 RID: 11379
			public double startTime;
		}
	}
}
