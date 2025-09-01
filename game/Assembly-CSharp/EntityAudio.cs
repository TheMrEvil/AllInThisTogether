using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000077 RID: 119
public class EntityAudio : MonoBehaviour
{
	// Token: 0x06000485 RID: 1157 RVA: 0x000223FF File Offset: 0x000205FF
	private void Awake()
	{
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00022404 File Offset: 0x00020604
	public virtual void Setup()
	{
		this.control = base.GetComponent<EntityControl>();
		this.net = base.GetComponent<EntityNetworked>();
		EntityHealth health = this.control.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
		{
			this.PlayDeathSFX();
		}));
		EntityHealth health2 = this.control.health;
		health2.OnShieldsDepleted = (Action<DamageInfo>)Delegate.Combine(health2.OnShieldsDepleted, new Action<DamageInfo>(this.OnShieldBreak));
		this.SetupPassive();
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00022488 File Offset: 0x00020688
	internal void SetupPassive()
	{
		if (this.PassiveLoop.Count == 0)
		{
			return;
		}
		this.passiveFader = this.control.display.CenterOfMass.gameObject.GetOrAddComponent<AudioFader>();
		if (this.NoRandomLoop)
		{
			this.passiveFader.RandomStart = false;
		}
		this.passiveFader.Options = this.PassiveLoop;
		this.passiveFader.GroupOverride = this.GroupOverride;
		this.passiveFader.FadeDuration = 1.5f;
		this.passiveFader.MinMaxRange = new Vector2(8f, 25f) * this.DistanceMult;
		this.passiveFader.Play();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0002253C File Offset: 0x0002073C
	internal virtual void PlayDeathSFX()
	{
		AudioFader audioFader = this.passiveFader;
		if (audioFader != null)
		{
			audioFader.Stop();
		}
		if (this.Death.Count > 0)
		{
			this.PlaySFX(this.Death.GetRandomClip(-1), 1f, 1f, 1f);
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00022589 File Offset: 0x00020789
	internal virtual void OnShieldBreak(DamageInfo dmg)
	{
		if (this.ShieldBreak.Count > 0)
		{
			this.PlaySFX(this.ShieldBreak.GetRandomClip(-1), 5f, 1f, 1f);
		}
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x000225BC File Offset: 0x000207BC
	public void PlayFootstep()
	{
		if (this.Footsteps.Count == 0 || this.control.movement.GetVelocity().magnitude < 0.2f)
		{
			return;
		}
		AudioSource audioSource = AudioManager.PlayClipAtPoint(this.Footsteps.GetRandomClip(-1), this.control.display.GetLocation(ActionLocation.Floor).position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f, 10f * this.DistanceMult, 50f * this.DistanceMult);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = ((this.GroupOverride == null) ? AudioManager.instance.SFXGroup : this.GroupOverride);
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00022680 File Offset: 0x00020880
	internal virtual void PlaySFX(AudioClip clip, float distMultExtra = 1f, float volume = 1f, float pitch = 1f)
	{
		if (clip == null)
		{
			return;
		}
		AudioSource audioSource = AudioManager.PlayClipAtPoint(clip, this.control.display.CenterOfMass.position, volume, pitch * UnityEngine.Random.Range(0.95f, 1.05f), 1f, 3f * this.DistanceMult * distMultExtra, 15f * this.DistanceMult * distMultExtra);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = ((this.GroupOverride == null) ? AudioManager.instance.SFXGroup : this.GroupOverride);
		}
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00022716 File Offset: 0x00020916
	public EntityAudio()
	{
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00022729 File Offset: 0x00020929
	[CompilerGenerated]
	private void <Setup>b__11_0(DamageInfo <p0>)
	{
		this.PlayDeathSFX();
	}

	// Token: 0x040003C0 RID: 960
	internal EntityControl control;

	// Token: 0x040003C1 RID: 961
	internal EntityNetworked net;

	// Token: 0x040003C2 RID: 962
	public List<AudioClip> ShieldBreak;

	// Token: 0x040003C3 RID: 963
	public List<AudioClip> Footsteps;

	// Token: 0x040003C4 RID: 964
	public List<AudioClip> Death;

	// Token: 0x040003C5 RID: 965
	public List<AudioClip> PassiveLoop;

	// Token: 0x040003C6 RID: 966
	public float DistanceMult = 1f;

	// Token: 0x040003C7 RID: 967
	public bool NoRandomLoop;

	// Token: 0x040003C8 RID: 968
	public AudioMixerGroup GroupOverride;

	// Token: 0x040003C9 RID: 969
	internal AudioFader passiveFader;

	// Token: 0x02000495 RID: 1173
	public enum NetAudioEvent
	{
		// Token: 0x04002341 RID: 9025
		Jump
	}
}
