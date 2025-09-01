using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003C4 RID: 964
	public class VMS_AudioSourceController : MonoBehaviour
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x000BCBB1 File Offset: 0x000BADB1
		private void Update()
		{
			if (this.flagForUpdate)
			{
				this.UpdateVolume();
				this.flagForUpdate = false;
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000BCBC8 File Offset: 0x000BADC8
		private void UpdateVolume()
		{
			this.melodySource.SetVolume(Mathf.Lerp(this.gameVol.melodyVolume, this.menuVol.melodyVolume, this.menuMix) * this.masterVolume);
			this.rhythmSource.SetVolume(Mathf.Lerp(this.gameVol.rhythmVolume, this.menuVol.rhythmVolume, this.menuMix) * this.masterVolume);
			this.bassSource.SetVolume(Mathf.Lerp(this.gameVol.bassVolume, this.menuVol.bassVolume, this.menuMix) * this.masterVolume);
			this.drumsSource.SetVolume(Mathf.Lerp(this.gameVol.drumsVolume, this.menuVol.drumsVolume, this.menuMix) * this.masterVolume);
			this.ambienceSource.SetVolume(Mathf.Lerp(this.gameVol.ambienceVolume, this.menuVol.ambienceVolume, this.menuMix) * this.masterVolume);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000BCCD4 File Offset: 0x000BAED4
		public void SetInitialVolume(VMS_VolumeProfileSet volumeProfileSet, bool inGame)
		{
			this.gameVol = volumeProfileSet.gameplayVolume;
			this.menuVol = volumeProfileSet.menuVolume;
			VolumeProfile volumeProfile = inGame ? this.gameVol : this.menuVol;
			this.melodySource.SetVolume(volumeProfile.melodyVolume);
			this.rhythmSource.SetVolume(volumeProfile.rhythmVolume);
			this.bassSource.SetVolume(volumeProfile.bassVolume);
			this.drumsSource.SetVolume(volumeProfile.drumsVolume);
			this.ambienceSource.SetVolume(volumeProfile.ambienceVolume);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000BCD60 File Offset: 0x000BAF60
		public void SetLoadType(bool autoUnload)
		{
			this.melodySource.autoUnload = autoUnload;
			this.rhythmSource.autoUnload = autoUnload;
			this.bassSource.autoUnload = autoUnload;
			this.drumsSource.autoUnload = autoUnload;
			this.ambienceSource.autoUnload = autoUnload;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000BCDA0 File Offset: 0x000BAFA0
		public void ToggleMenuMix(bool inMenu, float fadeTime)
		{
			if (this.blendCoroutine != null)
			{
				base.StopCoroutine(this.blendCoroutine);
			}
			float target = (float)(inMenu ? 1 : 0);
			this.blendCoroutine = base.StartCoroutine(this.FadeMenuMix(target, fadeTime, this.transitionCurve));
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000BCDE4 File Offset: 0x000BAFE4
		public void DisableAllSources(Action OnFinished, float stoppingTime)
		{
			if (this.stopCoroutine != null)
			{
				UnityEngine.Debug.LogWarning("Cannot disable audio sources, they are already stopping");
				return;
			}
			base.StartCoroutine(this.FadeOutAndStop(stoppingTime, OnFinished, this.stopCurve));
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000BCE0E File Offset: 0x000BB00E
		private IEnumerator FadeOutAndStop(float fadeDuration, Action OnStop, AnimationCurve fadeCurve)
		{
			float time = 0f;
			float start = this.masterVolume;
			while (time < fadeDuration)
			{
				float num = time / fadeDuration;
				num = fadeCurve.Evaluate(num);
				this.masterVolume = Mathf.Lerp(start, 0f, num);
				this.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				yield return null;
			}
			this.masterVolume = 0f;
			this.UpdateVolume();
			this.melodySource.Stop();
			this.rhythmSource.Stop();
			this.bassSource.Stop();
			this.drumsSource.Stop();
			this.ambienceSource.Stop();
			if (OnStop != null)
			{
				OnStop();
			}
			this.masterVolume = 1f;
			yield break;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000BCE32 File Offset: 0x000BB032
		private IEnumerator FadeMenuMix(float target, float fadeTime, AnimationCurve curve)
		{
			float time = 0f;
			float start = this.menuMix;
			while (time < fadeTime)
			{
				float num = time / fadeTime;
				num = curve.Evaluate(num);
				this.menuMix = Mathf.Lerp(start, target, num);
				this.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				yield return null;
			}
			this.menuMix = target;
			this.flagForUpdate = true;
			yield break;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000BCE56 File Offset: 0x000BB056
		public void FadeToNewVolumeProfile(VMS_VolumeProfileSet volumeProfileSet, float fadeDuration)
		{
			if (this.fadeCoroutine != null)
			{
				base.StopCoroutine(this.fadeCoroutine);
			}
			this.fadeCoroutine = base.StartCoroutine(this.FadeVolumeProfiles(volumeProfileSet, fadeDuration));
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000BCE80 File Offset: 0x000BB080
		private IEnumerator FadeVolumeProfiles(VMS_VolumeProfileSet targetProfileSet, float fadeDuration)
		{
			float time = 0f;
			VolumeProfile gameStart = this.gameVol;
			VolumeProfile menuStart = this.menuVol;
			while (time < fadeDuration)
			{
				float num = time / fadeDuration;
				num = this.transitionCurve.Evaluate(num);
				this.gameVol.melodyVolume = Mathf.Lerp(gameStart.melodyVolume, targetProfileSet.gameplayVolume.melodyVolume, num);
				this.gameVol.rhythmVolume = Mathf.Lerp(gameStart.rhythmVolume, targetProfileSet.gameplayVolume.rhythmVolume, num);
				this.gameVol.bassVolume = Mathf.Lerp(gameStart.bassVolume, targetProfileSet.gameplayVolume.bassVolume, num);
				this.gameVol.drumsVolume = Mathf.Lerp(gameStart.drumsVolume, targetProfileSet.gameplayVolume.drumsVolume, num);
				this.gameVol.ambienceVolume = Mathf.Lerp(gameStart.ambienceVolume, targetProfileSet.gameplayVolume.ambienceVolume, num);
				this.menuVol.melodyVolume = Mathf.Lerp(menuStart.melodyVolume, targetProfileSet.menuVolume.melodyVolume, num);
				this.menuVol.rhythmVolume = Mathf.Lerp(menuStart.rhythmVolume, targetProfileSet.menuVolume.rhythmVolume, num);
				this.menuVol.bassVolume = Mathf.Lerp(menuStart.bassVolume, targetProfileSet.menuVolume.bassVolume, num);
				this.menuVol.drumsVolume = Mathf.Lerp(menuStart.drumsVolume, targetProfileSet.menuVolume.drumsVolume, num);
				this.menuVol.ambienceVolume = Mathf.Lerp(menuStart.ambienceVolume, targetProfileSet.menuVolume.ambienceVolume, num);
				this.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				yield return null;
			}
			this.gameVol = targetProfileSet.gameplayVolume;
			this.menuVol = targetProfileSet.menuVolume;
			yield break;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000BCE9D File Offset: 0x000BB09D
		public int UnloadUnusedClips()
		{
			return 0 + this.melodySource.UnloadUnusedClips() + this.rhythmSource.UnloadUnusedClips() + this.bassSource.UnloadUnusedClips() + this.drumsSource.UnloadUnusedClips() + this.ambienceSource.UnloadUnusedClips();
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000BCEDC File Offset: 0x000BB0DC
		public VMS_AudioSourceController()
		{
		}

		// Token: 0x04001FE0 RID: 8160
		[Header("Audio Sources")]
		public VMS_AudioSource melodySource;

		// Token: 0x04001FE1 RID: 8161
		public VMS_AudioSource rhythmSource;

		// Token: 0x04001FE2 RID: 8162
		public VMS_AudioSource bassSource;

		// Token: 0x04001FE3 RID: 8163
		public VMS_AudioSource drumsSource;

		// Token: 0x04001FE4 RID: 8164
		public VMS_AudioSource ambienceSource;

		// Token: 0x04001FE5 RID: 8165
		[Header("Settings")]
		[SerializeField]
		private AnimationCurve transitionCurve;

		// Token: 0x04001FE6 RID: 8166
		[SerializeField]
		private AnimationCurve stopCurve;

		// Token: 0x04001FE7 RID: 8167
		private float masterVolume = 1f;

		// Token: 0x04001FE8 RID: 8168
		private float menuMix;

		// Token: 0x04001FE9 RID: 8169
		private Coroutine fadeCoroutine;

		// Token: 0x04001FEA RID: 8170
		private Coroutine blendCoroutine;

		// Token: 0x04001FEB RID: 8171
		private Coroutine stopCoroutine;

		// Token: 0x04001FEC RID: 8172
		private VolumeProfile gameVol;

		// Token: 0x04001FED RID: 8173
		private VolumeProfile menuVol;

		// Token: 0x04001FEE RID: 8174
		private bool flagForUpdate;

		// Token: 0x0200069C RID: 1692
		[CompilerGenerated]
		private sealed class <FadeOutAndStop>d__21 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600281E RID: 10270 RVA: 0x000D7B09 File Offset: 0x000D5D09
			[DebuggerHidden]
			public <FadeOutAndStop>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600281F RID: 10271 RVA: 0x000D7B18 File Offset: 0x000D5D18
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002820 RID: 10272 RVA: 0x000D7B1C File Offset: 0x000D5D1C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				VMS_AudioSourceController vms_AudioSourceController = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					time = 0f;
					start = vms_AudioSourceController.masterVolume;
				}
				if (time >= fadeDuration)
				{
					vms_AudioSourceController.masterVolume = 0f;
					vms_AudioSourceController.UpdateVolume();
					vms_AudioSourceController.melodySource.Stop();
					vms_AudioSourceController.rhythmSource.Stop();
					vms_AudioSourceController.bassSource.Stop();
					vms_AudioSourceController.drumsSource.Stop();
					vms_AudioSourceController.ambienceSource.Stop();
					Action action = OnStop;
					if (action != null)
					{
						action();
					}
					vms_AudioSourceController.masterVolume = 1f;
					return false;
				}
				float num2 = time / fadeDuration;
				num2 = fadeCurve.Evaluate(num2);
				vms_AudioSourceController.masterVolume = Mathf.Lerp(start, 0f, num2);
				vms_AudioSourceController.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06002821 RID: 10273 RVA: 0x000D7C35 File Offset: 0x000D5E35
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002822 RID: 10274 RVA: 0x000D7C3D File Offset: 0x000D5E3D
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06002823 RID: 10275 RVA: 0x000D7C44 File Offset: 0x000D5E44
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C4A RID: 11338
			private int <>1__state;

			// Token: 0x04002C4B RID: 11339
			private object <>2__current;

			// Token: 0x04002C4C RID: 11340
			public VMS_AudioSourceController <>4__this;

			// Token: 0x04002C4D RID: 11341
			public float fadeDuration;

			// Token: 0x04002C4E RID: 11342
			public AnimationCurve fadeCurve;

			// Token: 0x04002C4F RID: 11343
			public Action OnStop;

			// Token: 0x04002C50 RID: 11344
			private float <time>5__2;

			// Token: 0x04002C51 RID: 11345
			private float <start>5__3;
		}

		// Token: 0x0200069D RID: 1693
		[CompilerGenerated]
		private sealed class <FadeMenuMix>d__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002824 RID: 10276 RVA: 0x000D7C4C File Offset: 0x000D5E4C
			[DebuggerHidden]
			public <FadeMenuMix>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002825 RID: 10277 RVA: 0x000D7C5B File Offset: 0x000D5E5B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002826 RID: 10278 RVA: 0x000D7C60 File Offset: 0x000D5E60
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				VMS_AudioSourceController vms_AudioSourceController = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					time = 0f;
					start = vms_AudioSourceController.menuMix;
				}
				if (time >= fadeTime)
				{
					vms_AudioSourceController.menuMix = target;
					vms_AudioSourceController.flagForUpdate = true;
					return false;
				}
				float num2 = time / fadeTime;
				num2 = curve.Evaluate(num2);
				vms_AudioSourceController.menuMix = Mathf.Lerp(start, target, num2);
				vms_AudioSourceController.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06002827 RID: 10279 RVA: 0x000D7D29 File Offset: 0x000D5F29
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002828 RID: 10280 RVA: 0x000D7D31 File Offset: 0x000D5F31
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x06002829 RID: 10281 RVA: 0x000D7D38 File Offset: 0x000D5F38
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C52 RID: 11346
			private int <>1__state;

			// Token: 0x04002C53 RID: 11347
			private object <>2__current;

			// Token: 0x04002C54 RID: 11348
			public VMS_AudioSourceController <>4__this;

			// Token: 0x04002C55 RID: 11349
			public float fadeTime;

			// Token: 0x04002C56 RID: 11350
			public AnimationCurve curve;

			// Token: 0x04002C57 RID: 11351
			public float target;

			// Token: 0x04002C58 RID: 11352
			private float <time>5__2;

			// Token: 0x04002C59 RID: 11353
			private float <start>5__3;
		}

		// Token: 0x0200069E RID: 1694
		[CompilerGenerated]
		private sealed class <FadeVolumeProfiles>d__24 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600282A RID: 10282 RVA: 0x000D7D40 File Offset: 0x000D5F40
			[DebuggerHidden]
			public <FadeVolumeProfiles>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600282B RID: 10283 RVA: 0x000D7D4F File Offset: 0x000D5F4F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600282C RID: 10284 RVA: 0x000D7D54 File Offset: 0x000D5F54
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				VMS_AudioSourceController vms_AudioSourceController = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					time = 0f;
					gameStart = vms_AudioSourceController.gameVol;
					menuStart = vms_AudioSourceController.menuVol;
				}
				if (time >= fadeDuration)
				{
					vms_AudioSourceController.gameVol = targetProfileSet.gameplayVolume;
					vms_AudioSourceController.menuVol = targetProfileSet.menuVolume;
					return false;
				}
				float num2 = time / fadeDuration;
				num2 = vms_AudioSourceController.transitionCurve.Evaluate(num2);
				vms_AudioSourceController.gameVol.melodyVolume = Mathf.Lerp(gameStart.melodyVolume, targetProfileSet.gameplayVolume.melodyVolume, num2);
				vms_AudioSourceController.gameVol.rhythmVolume = Mathf.Lerp(gameStart.rhythmVolume, targetProfileSet.gameplayVolume.rhythmVolume, num2);
				vms_AudioSourceController.gameVol.bassVolume = Mathf.Lerp(gameStart.bassVolume, targetProfileSet.gameplayVolume.bassVolume, num2);
				vms_AudioSourceController.gameVol.drumsVolume = Mathf.Lerp(gameStart.drumsVolume, targetProfileSet.gameplayVolume.drumsVolume, num2);
				vms_AudioSourceController.gameVol.ambienceVolume = Mathf.Lerp(gameStart.ambienceVolume, targetProfileSet.gameplayVolume.ambienceVolume, num2);
				vms_AudioSourceController.menuVol.melodyVolume = Mathf.Lerp(menuStart.melodyVolume, targetProfileSet.menuVolume.melodyVolume, num2);
				vms_AudioSourceController.menuVol.rhythmVolume = Mathf.Lerp(menuStart.rhythmVolume, targetProfileSet.menuVolume.rhythmVolume, num2);
				vms_AudioSourceController.menuVol.bassVolume = Mathf.Lerp(menuStart.bassVolume, targetProfileSet.menuVolume.bassVolume, num2);
				vms_AudioSourceController.menuVol.drumsVolume = Mathf.Lerp(menuStart.drumsVolume, targetProfileSet.menuVolume.drumsVolume, num2);
				vms_AudioSourceController.menuVol.ambienceVolume = Mathf.Lerp(menuStart.ambienceVolume, targetProfileSet.menuVolume.ambienceVolume, num2);
				vms_AudioSourceController.flagForUpdate = true;
				time += Time.unscaledDeltaTime;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x0600282D RID: 10285 RVA: 0x000D7FE1 File Offset: 0x000D61E1
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600282E RID: 10286 RVA: 0x000D7FE9 File Offset: 0x000D61E9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x0600282F RID: 10287 RVA: 0x000D7FF0 File Offset: 0x000D61F0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C5A RID: 11354
			private int <>1__state;

			// Token: 0x04002C5B RID: 11355
			private object <>2__current;

			// Token: 0x04002C5C RID: 11356
			public VMS_AudioSourceController <>4__this;

			// Token: 0x04002C5D RID: 11357
			public float fadeDuration;

			// Token: 0x04002C5E RID: 11358
			public VMS_VolumeProfileSet targetProfileSet;

			// Token: 0x04002C5F RID: 11359
			private float <time>5__2;

			// Token: 0x04002C60 RID: 11360
			private VolumeProfile <gameStart>5__3;

			// Token: 0x04002C61 RID: 11361
			private VolumeProfile <menuStart>5__4;
		}
	}
}
