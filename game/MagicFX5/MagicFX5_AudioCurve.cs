using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000005 RID: 5
	public class MagicFX5_AudioCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000255E File Offset: 0x0000075E
		private void Awake()
		{
			this._audioSource = base.GetComponent<AudioSource>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000256C File Offset: 0x0000076C
		internal override void OnEnableExtended()
		{
			this._leftTime = -this.StartDelay;
			this._startVolume = this._audioSource.volume;
			this._startPitch = this._audioSource.pitch;
			if (this.RandomClips.Length != 0)
			{
				this._audioSource.clip = this.RandomClips[UnityEngine.Random.Range(0, this.RandomClips.Length - 1)];
				this._audioSource.PlayDelayed(this.StartDelay);
			}
			this._audioSource.time = this.AudioClipStartTimeOffset;
			if (this.StartDelay > 0f)
			{
				this._audioSource.PlayDelayed(this.StartDelay);
			}
			if (this.UseVolumeCurve)
			{
				this._audioSource.enabled = true;
				this._audioSource.volume = this.VolumeOverTime.Evaluate(0f);
				this._canUpdate = true;
			}
			if (this.UsePitchCurve)
			{
				this._audioSource.enabled = true;
				this._audioSource.pitch = this.PitchOverTime.Evaluate(0f);
				this._canUpdate = true;
			}
			if (this.UseRandomPitch && !this.UsePitchCurve)
			{
				this._audioSource.pitch = this._startPitch + UnityEngine.Random.Range(-this.PitchOffset, this.PitchOffset);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026B3 File Offset: 0x000008B3
		internal override void OnDisableExtended()
		{
			this._audioSource.volume = this._startVolume;
			this._audioSource.pitch = this._startPitch;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026D8 File Offset: 0x000008D8
		internal override void ManualUpdate()
		{
			if (!this._canUpdate)
			{
				return;
			}
			this._leftTime += Time.deltaTime;
			if (this.Loop)
			{
				this._leftTime %= this.Duration;
			}
			if (this.UseVolumeCurve)
			{
				this._audioSource.volume = this.VolumeOverTime.Evaluate(this._leftTime / this.Duration) * this._startVolume;
			}
			if (this.UsePitchCurve)
			{
				this._audioSource.pitch = this.PitchOverTime.Evaluate(this._leftTime / this.Duration) * this._startPitch;
			}
			if (!this.Loop && this._leftTime > this.Duration)
			{
				this._canUpdate = true;
				if (this.AutoDeactivation)
				{
					this._audioSource.enabled = false;
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027B0 File Offset: 0x000009B0
		public MagicFX5_AudioCurve()
		{
		}

		// Token: 0x0400001B RID: 27
		public bool UseVolumeCurve = true;

		// Token: 0x0400001C RID: 28
		public bool UsePitchCurve;

		// Token: 0x0400001D RID: 29
		public AnimationCurve VolumeOverTime = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

		// Token: 0x0400001E RID: 30
		public AnimationCurve PitchOverTime = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

		// Token: 0x0400001F RID: 31
		public float StartDelay;

		// Token: 0x04000020 RID: 32
		public float Duration = 2f;

		// Token: 0x04000021 RID: 33
		public bool Loop;

		// Token: 0x04000022 RID: 34
		public bool AutoDeactivation = true;

		// Token: 0x04000023 RID: 35
		[Space]
		public bool UseRandomPitch;

		// Token: 0x04000024 RID: 36
		public float PitchOffset = 0.15f;

		// Token: 0x04000025 RID: 37
		[Space]
		public float AudioClipStartTimeOffset;

		// Token: 0x04000026 RID: 38
		[Space]
		public AudioClip[] RandomClips;

		// Token: 0x04000027 RID: 39
		private AudioSource _audioSource;

		// Token: 0x04000028 RID: 40
		private float _leftTime;

		// Token: 0x04000029 RID: 41
		private float _startVolume;

		// Token: 0x0400002A RID: 42
		private float _startPitch;

		// Token: 0x0400002B RID: 43
		private bool _canUpdate;
	}
}
