using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class AudioPlayer : MonoBehaviour
{
	// Token: 0x06000159 RID: 345 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
	private void OnEnable()
	{
		if (this.PlayOnStart)
		{
			this.Play();
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
	public virtual void Play()
	{
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000E1CA File Offset: 0x0000C3CA
	public virtual void Stop()
	{
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000E1CC File Offset: 0x0000C3CC
	internal void AddSource()
	{
		if (this.source != null)
		{
			return;
		}
		this.source = base.gameObject.AddComponent<AudioSource>();
		this.source.dopplerLevel = 0f;
		this.source.outputAudioMixerGroup = AudioManager.instance.SFXGroup;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000E21E File Offset: 0x0000C41E
	public AudioPlayer()
	{
	}

	// Token: 0x04000180 RID: 384
	public List<AudioClip> Options;

	// Token: 0x04000181 RID: 385
	public bool PlayOnStart = true;

	// Token: 0x04000182 RID: 386
	internal AudioSource source;
}
