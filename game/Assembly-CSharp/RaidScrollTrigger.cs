using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class RaidScrollTrigger : DiageticOption
{
	// Token: 0x0600087C RID: 2172 RVA: 0x0003A955 File Offset: 0x00038B55
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0003A95D File Offset: 0x00038B5D
	public override void Activate()
	{
		base.Activate();
		this.LoopSFX.Play();
		this.BaseFX.Play();
		if (this.BaseObject != null)
		{
			this.BaseObject.SetActive(true);
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0003A998 File Offset: 0x00038B98
	public override void Deactivate()
	{
		if (!this.IsAvailable)
		{
			return;
		}
		this.LoopSFX.Stop();
		if (this.BaseObject != null)
		{
			this.BaseObject.SetActive(false);
		}
		base.Deactivate();
		EntityIndicator entityIndicator = this.indicator;
		if (entityIndicator != null)
		{
			entityIndicator.Deactivate();
		}
		if (this.BaseFX != null)
		{
			this.BaseFX.Stop();
		}
		if (this.InteractFX != null)
		{
			this.InteractFX.Play();
		}
		AudioManager.PlayClipAtPoint(this.InteractSFX.GetRandomClip(-1), base.transform.position, 1f, 1f, 1f, 10f, 250f);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0003AA52 File Offset: 0x00038C52
	public override void Select()
	{
		this.Deactivate();
		RaidManager.instance.AwardPlayerPages(this.HasFilter ? this.Filter : null);
		base.Select();
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0003AA7B File Offset: 0x00038C7B
	public RaidScrollTrigger()
	{
	}

	// Token: 0x04000725 RID: 1829
	public string Label;

	// Token: 0x04000726 RID: 1830
	public ParticleSystem BaseFX;

	// Token: 0x04000727 RID: 1831
	public GameObject BaseObject;

	// Token: 0x04000728 RID: 1832
	public AudioFader LoopSFX;

	// Token: 0x04000729 RID: 1833
	public ParticleSystem InteractFX;

	// Token: 0x0400072A RID: 1834
	public List<AudioClip> InteractSFX;

	// Token: 0x0400072B RID: 1835
	public EntityIndicator indicator;

	// Token: 0x0400072C RID: 1836
	public bool HasFilter;

	// Token: 0x0400072D RID: 1837
	public AugmentFilter Filter;
}
