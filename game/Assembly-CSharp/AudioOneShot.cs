using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class AudioOneShot : AudioPlayer
{
	// Token: 0x06000157 RID: 343 RVA: 0x0000E108 File Offset: 0x0000C308
	public override void Play()
	{
		AudioSource audioSource = AudioManager.PlayClipAtPoint(this.Options.GetRandomClip(-1), base.transform.position, 1f, UnityEngine.Random.Range(this.PitchRange.x, this.PitchRange.y), 1f, this.MinMaxRange.x, this.MinMaxRange.y);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = AudioManager.instance.SFXGroup;
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000E186 File Offset: 0x0000C386
	public AudioOneShot()
	{
	}

	// Token: 0x0400017E RID: 382
	public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

	// Token: 0x0400017F RID: 383
	public Vector2 MinMaxRange = new Vector2(15f, 250f);
}
