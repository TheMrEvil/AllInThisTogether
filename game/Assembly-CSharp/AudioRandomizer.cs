using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002D RID: 45
[RequireComponent(typeof(AudioSource))]
public class AudioRandomizer : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x0000E230 File Offset: 0x0000C430
	private void Awake()
	{
		this.src = base.GetComponent<AudioSource>();
		this.src.pitch = UnityEngine.Random.Range(this.Pitch.x, this.Pitch.y);
		if (this.src.clip == null && this.Clips.Count > 0)
		{
			this.src.clip = this.Clips.GetRandomClip(-1);
		}
		if (this.src.playOnAwake)
		{
			this.src.Play();
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000E2BF File Offset: 0x0000C4BF
	public AudioRandomizer()
	{
	}

	// Token: 0x04000183 RID: 387
	private AudioSource src;

	// Token: 0x04000184 RID: 388
	public List<AudioClip> Clips;

	// Token: 0x04000185 RID: 389
	public Vector2 Pitch = new Vector2(0.95f, 1.05f);
}
