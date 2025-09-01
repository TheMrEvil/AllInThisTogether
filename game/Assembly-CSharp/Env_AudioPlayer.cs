using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

// Token: 0x0200002E RID: 46
public class Env_AudioPlayer : MonoBehaviour
{
	// Token: 0x06000160 RID: 352 RVA: 0x0000E2DC File Offset: 0x0000C4DC
	private void Start()
	{
		base.Invoke("PlayAudio", UnityEngine.Random.Range(0f, this.PlayInterval.x));
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000E300 File Offset: 0x0000C500
	private void PlayAudio()
	{
		AudioSource audioSource = AudioManager.PlayClipAtPoint(this.audioClips.GetRandomClip(-1), base.transform.position, this.Volume, UnityEngine.Random.Range(this.Pitch.x, this.Pitch.y), 1f, this.DistRange.x, this.DistRange.y);
		if (this.Group != null)
		{
			audioSource.outputAudioMixerGroup = this.Group;
		}
		UnityEvent onPlay = this.OnPlay;
		if (onPlay != null)
		{
			onPlay.Invoke();
		}
		base.Invoke("PlayAudio", UnityEngine.Random.Range(this.PlayInterval.x, this.PlayInterval.y));
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
	public Env_AudioPlayer()
	{
	}

	// Token: 0x04000186 RID: 390
	public List<AudioClip> audioClips = new List<AudioClip>();

	// Token: 0x04000187 RID: 391
	public Vector2 PlayInterval = new Vector2(1f, 3f);

	// Token: 0x04000188 RID: 392
	public float Volume = 1f;

	// Token: 0x04000189 RID: 393
	public Vector2 Pitch = new Vector2(1f, 1f);

	// Token: 0x0400018A RID: 394
	public Vector2 DistRange = new Vector2(15f, 250f);

	// Token: 0x0400018B RID: 395
	public AudioMixerGroup Group;

	// Token: 0x0400018C RID: 396
	public UnityEvent OnPlay;
}
