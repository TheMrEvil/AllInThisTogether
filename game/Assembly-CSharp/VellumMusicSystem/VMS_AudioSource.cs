using System;
using UnityEngine;
using UnityEngine.Audio;

namespace VellumMusicSystem
{
	// Token: 0x020003C3 RID: 963
	public class VMS_AudioSource : MonoBehaviour
	{
		// Token: 0x06001FB2 RID: 8114 RVA: 0x000BC850 File Offset: 0x000BAA50
		private void Awake()
		{
			this.audioSources = base.GetComponents<AudioSource>();
			foreach (AudioSource audioSource in this.audioSources)
			{
				if (this.output != null)
				{
					audioSource.outputAudioMixerGroup = this.output;
				}
				audioSource.ignoreListenerPause = true;
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000BC8A3 File Offset: 0x000BAAA3
		private void Start()
		{
			this.defaultSnapShot = this.output.audioMixer.FindSnapshot("Snapshot");
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000BC8C0 File Offset: 0x000BAAC0
		public void Play(AudioClip clipToPlay, double scheduleTime, out double scheduledEndTime)
		{
			if (clipToPlay == null)
			{
				Debug.LogWarning("Attempted to play a missing clip");
				scheduledEndTime = scheduleTime;
				return;
			}
			if (clipToPlay != this.audioSources[this.toggle].clip)
			{
				AudioClip clip = this.audioSources[this.toggle].clip;
				this.audioSources[this.toggle].clip = null;
				if (clip != null && this.autoUnload && clip != this.audioSources[1 - this.toggle].clip)
				{
					clip.UnloadAudioData();
				}
				this.audioSources[this.toggle].clip = clipToPlay;
			}
			this.audioSources[this.toggle].PlayScheduled(scheduleTime);
			double num = (double)this.audioSources[this.toggle].clip.samples / (double)this.audioSources[this.toggle].clip.frequency;
			scheduledEndTime = scheduleTime + num;
			this.toggle = 1 - this.toggle;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		public void Stop()
		{
			AudioSource[] array = this.audioSources;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Stop();
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000BC9EE File Offset: 0x000BABEE
		public void PlayEnding(AudioClip clipToPlay, double scheduleTime)
		{
			if (clipToPlay != this.audioSources[this.toggle].clip)
			{
				this.audioSources[this.toggle].clip = clipToPlay;
			}
			this.audioSources[2].PlayScheduled(scheduleTime);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000BCA2C File Offset: 0x000BAC2C
		public void SetVolume(float volume)
		{
			AudioSource[] array = this.audioSources;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].volume = volume;
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000BCA58 File Offset: 0x000BAC58
		public int UnloadUnusedClips()
		{
			int num = 0;
			bool flag = true;
			bool flag2 = true;
			if (this.audioSources[this.toggle].isPlaying)
			{
				flag = false;
			}
			if (this.audioSources[1 - this.toggle].isPlaying)
			{
				flag2 = false;
			}
			if (flag && !flag2 && this.audioSources[this.toggle].clip == this.audioSources[1 - this.toggle].clip)
			{
				flag = false;
			}
			if (flag2 && !flag && this.audioSources[this.toggle].clip == this.audioSources[1 - this.toggle].clip)
			{
				flag2 = false;
			}
			if (flag && this.audioSources[this.toggle].clip != null)
			{
				this.audioSources[this.toggle].clip.UnloadAudioData();
				this.audioSources[this.toggle].clip = null;
				num++;
			}
			if (flag2 && this.audioSources[1 - this.toggle].clip != null)
			{
				this.audioSources[1 - this.toggle].clip.UnloadAudioData();
				this.audioSources[1 - this.toggle].clip = null;
				num++;
			}
			return num;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000BCBA2 File Offset: 0x000BADA2
		public VMS_AudioSource()
		{
		}

		// Token: 0x04001FDB RID: 8155
		[HideInInspector]
		public bool autoUnload = true;

		// Token: 0x04001FDC RID: 8156
		[SerializeField]
		private AudioMixerGroup output;

		// Token: 0x04001FDD RID: 8157
		private AudioMixerSnapshot defaultSnapShot;

		// Token: 0x04001FDE RID: 8158
		private AudioSource[] audioSources;

		// Token: 0x04001FDF RID: 8159
		private int toggle;
	}
}
