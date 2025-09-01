using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class AIAudio : EntityAudio
{
	// Token: 0x060002F7 RID: 759 RVA: 0x000190BA File Offset: 0x000172BA
	public void Jump(Vector3 point)
	{
		this.PlaySFX(this.JumpClips.GetRandomClip(-1), 1f, 1f, 1f);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x000190DD File Offset: 0x000172DD
	public void Land(Vector3 point)
	{
		this.PlaySFX(this.LandClips.GetRandomClip(-1), 1f, 1f, 1f);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00019100 File Offset: 0x00017300
	public void PlayExtraEffect()
	{
		if (this.ExtraEffect.Count == 0)
		{
			return;
		}
		AudioSource audioSource = AudioManager.PlayClipAtPoint(this.ExtraEffect.GetRandomClip(-1), this.control.display.CenterOfMass.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f, 15f * this.DistanceMult, 75f * this.DistanceMult);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = ((this.GroupOverride == null) ? AudioManager.instance.SFXGroup : this.GroupOverride);
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000191A4 File Offset: 0x000173A4
	public void Transform(AIAudio aud)
	{
		this.Spawn = aud.Spawn;
		this.JumpClips = aud.JumpClips;
		this.LandClips = aud.LandClips;
		this.ShieldBreak = aud.ShieldBreak;
		this.Footsteps = aud.Footsteps;
		this.ExtraEffect = aud.ExtraEffect;
		this.Death = aud.Death;
		this.DistanceMult = aud.DistanceMult;
		this.NoRandomLoop = aud.NoRandomLoop;
		this.GroupOverride = aud.GroupOverride;
		this.PassiveLoop = aud.PassiveLoop;
		AudioFader passiveFader = this.passiveFader;
		if (passiveFader != null)
		{
			passiveFader.Stop();
		}
		base.SetupPassive();
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0001924C File Offset: 0x0001744C
	internal override void OnShieldBreak(DamageInfo dmg)
	{
		if (this.ShieldBreak.Count == 0)
		{
			this.PlaySFX(AIManager.instance.ShieldBreakSFX.GetRandomClip(-1), 5f, 1f, 1f);
			return;
		}
		base.OnShieldBreak(dmg);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00019288 File Offset: 0x00017488
	public override void Setup()
	{
		base.Setup();
		if (this.Spawn.Count > 0)
		{
			this.PlaySFX(this.Spawn.GetRandomClip(-1), 1f, 1f, 1f);
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x000192BF File Offset: 0x000174BF
	public AIAudio()
	{
	}

	// Token: 0x040002F4 RID: 756
	public List<AudioClip> Spawn;

	// Token: 0x040002F5 RID: 757
	public List<AudioClip> JumpClips;

	// Token: 0x040002F6 RID: 758
	public List<AudioClip> LandClips;

	// Token: 0x040002F7 RID: 759
	public List<AudioClip> ExtraEffect;
}
