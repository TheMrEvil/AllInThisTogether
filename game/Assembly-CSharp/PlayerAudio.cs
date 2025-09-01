using System;
using System.Collections.Generic;
using CMF;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class PlayerAudio : EntityAudio
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060005CE RID: 1486 RVA: 0x0002B138 File Offset: 0x00029338
	private PlayerControl Control
	{
		get
		{
			return this.control as PlayerControl;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x0002B145 File Offset: 0x00029345
	private PlayerNetwork Net
	{
		get
		{
			return this.net as PlayerNetwork;
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0002B154 File Offset: 0x00029354
	public override void Setup()
	{
		base.Setup();
		if (this.Control.IsMine)
		{
			this.MovementLoop.outputAudioMixerGroup = AudioManager.instance.SFXGroup;
			this.MovementLoop.spatialBlend = 0f;
			this.SetupEvents();
		}
		EntityHealth health = this.Control.health;
		health.OnShieldChargeStart = (Action)Delegate.Combine(health.OnShieldChargeStart, new Action(this.OnShieldChargeSarted));
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0002B1CC File Offset: 0x000293CC
	private void SetupEvents()
	{
		AdvancedWalkerController walkerController = this.WalkerController;
		walkerController.OnJump = (Controller.VectorEvent)Delegate.Combine(walkerController.OnJump, new Controller.VectorEvent(this.OnJumped));
		AdvancedWalkerController walkerController2 = this.WalkerController;
		walkerController2.OnLand = (Action<Vector3, Vector3, Vector3>)Delegate.Combine(walkerController2.OnLand, new Action<Vector3, Vector3, Vector3>(this.OnLanded));
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002B228 File Offset: 0x00029428
	private void Update()
	{
		Vector3 vel = Vector3.ProjectOnPlane(this.WalkerController.GetVelocity(), Vector3.up);
		this.TryPlayDirChangeClip(vel);
		float num = vel.magnitude / 6f;
		float num2 = Mathf.Clamp(num * 0.15f, 0f, 0.5f);
		num2 = (this.Control.Display.DisplayGrounded ? num2 : 0f);
		this.MovementLoop.volume = Mathf.Lerp(this.MovementLoop.volume, num2, Time.deltaTime * 6f);
		float b = Mathf.Clamp(1f + num * 0.2f - 0.25f, 1f, 1.5f);
		this.MovementLoop.pitch = Mathf.Lerp(this.MovementLoop.pitch, b, Time.deltaTime * 4f);
		this.prevVel = vel;
		if (this.Control.IsMine && this.Listener != null)
		{
			this.Listener.transform.rotation = PlayerControl.MyCamera.transform.rotation;
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0002B344 File Offset: 0x00029544
	private void TryPlayDirChangeClip(Vector3 vel)
	{
		float num = 0.25f;
		float num2 = 20f;
		float num3 = 0.5f;
		if (this.prevVel.magnitude > num && vel.magnitude > num)
		{
			if (Vector3.Angle(this.prevVel, vel) > num2 && !this.directionChangedRecently)
			{
				this.directionChangedRecently = true;
				return;
			}
			if (this.directionChangedRecently && vel.magnitude > this.prevVel.magnitude * num3)
			{
				this.PlaySFX(this.MoveChangeClips.GetRandomClip(-1), 0.15f, UnityEngine.Random.Range(0.8f, 1.2f), 1f);
				this.directionChangedRecently = false;
				return;
			}
			if (this.directionChangedRecently && vel.magnitude <= this.prevVel.magnitude * num3)
			{
				this.directionChangedRecently = false;
				return;
			}
		}
		else
		{
			this.directionChangedRecently = false;
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0002B41E File Offset: 0x0002961E
	public void PlayManaRegen(MagicColor e)
	{
		this.PlaySFX(this.ManaRegen[UnityEngine.Random.Range(0, this.ManaRegen.Count)], 1f, 1f, 1f);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0002B451 File Offset: 0x00029651
	public void PlayManaGained(MagicColor e)
	{
		this.PlaySFX(this.ManaTempGain[UnityEngine.Random.Range(0, this.ManaTempGain.Count)], 1f, 0.3f, 1f);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0002B484 File Offset: 0x00029684
	internal override void OnShieldBreak(DamageInfo dmg)
	{
		if (this.Control.IsMine)
		{
			AudioManager.PlaySFX2D(this.SelfShieldBreak, 1f, 0.1f);
			return;
		}
		base.OnShieldBreak(dmg);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0002B4B0 File Offset: 0x000296B0
	private void OnShieldChargeSarted()
	{
		if (this.Control.IsMine)
		{
			AudioManager.PlaySFX2D(this.ShieldRecharge, 1f, 0.1f);
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0002B4D4 File Offset: 0x000296D4
	internal override void PlayDeathSFX()
	{
		AudioFader passiveFader = this.passiveFader;
		if (passiveFader != null)
		{
			passiveFader.Stop();
		}
		if (this.control.IsMine)
		{
			AudioManager.PlaySFX2D(this.SelfDeath.GetRandomClip(-1), 1f, 0.1f);
			return;
		}
		AudioManager.PlaySFX2D(this.Death.GetRandomClip(-1), 1f, 0.1f);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0002B536 File Offset: 0x00029736
	private void OnJumped(Vector3 momentum)
	{
		if ((double)(Time.realtimeSinceStartup - this.lastJumpTime) < 0.25)
		{
			return;
		}
		this.lastJumpTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002B55C File Offset: 0x0002975C
	public void OnLanded(Vector3 momentum, Vector3 point, Vector3 surfaceNormal)
	{
		if (this.Net == null || this.Control == null)
		{
			return;
		}
		this.Control.Display.LandAnim();
		if ((double)(Time.realtimeSinceStartup - this.lastLandTime) < 0.5)
		{
			return;
		}
		this.lastLandTime = Time.realtimeSinceStartup;
		if (momentum.y > -2f)
		{
			return;
		}
		if (momentum.y > -9f)
		{
			this.PlaySFX(this.LandSoftClips.GetRandomClip(-1), 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
		else
		{
			this.Control.Display.LandFX(point, surfaceNormal);
			this.PlaySFX(this.LandClips.GetRandomClip(-1), 0.6f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
		this.Net.Landed(momentum);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0002B64B File Offset: 0x0002984B
	public void PlayEventSound(int EventID)
	{
		if (EventID == 0)
		{
			this.PlayJumpSFX();
			return;
		}
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0002B657 File Offset: 0x00029857
	public void AugmentAdded()
	{
		this.PlaySFX(this.UpgradeAdded, 1f, 1f, 1f);
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002B674 File Offset: 0x00029874
	private void PlayJumpSFX()
	{
		this.PlaySFX(this.JumpClips.GetRandomClip(-1), 0.45f, 1f, 1f);
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0002B698 File Offset: 0x00029898
	internal override void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f, float distMultExtra = 1f)
	{
		if (clip == null || this.Control == null)
		{
			return;
		}
		float threed = this.Control.IsMine ? 0.25f : 1f;
		AudioSource audioSource = AudioManager.PlayClipAtPoint(clip, this.Control.display.CenterOfMass.position, volume, pitch * UnityEngine.Random.Range(0.95f, 1.05f), threed, 10f * distMultExtra, 25f * distMultExtra);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = AudioManager.instance.SFXGroup;
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0002B72F File Offset: 0x0002992F
	public PlayerAudio()
	{
	}

	// Token: 0x040004DA RID: 1242
	public AudioListener Listener;

	// Token: 0x040004DB RID: 1243
	public AdvancedWalkerController WalkerController;

	// Token: 0x040004DC RID: 1244
	public AudioSource MovementLoop;

	// Token: 0x040004DD RID: 1245
	public List<AudioClip> MoveChangeClips;

	// Token: 0x040004DE RID: 1246
	public List<AudioClip> JumpClips;

	// Token: 0x040004DF RID: 1247
	public List<AudioClip> LandClips;

	// Token: 0x040004E0 RID: 1248
	public List<AudioClip> LandSoftClips;

	// Token: 0x040004E1 RID: 1249
	public List<AudioClip> SelfDeath;

	// Token: 0x040004E2 RID: 1250
	public AudioClip UpgradeAdded;

	// Token: 0x040004E3 RID: 1251
	[Header("Mana")]
	public List<AudioClip> ManaRegen;

	// Token: 0x040004E4 RID: 1252
	public List<AudioClip> ManaTempGain;

	// Token: 0x040004E5 RID: 1253
	[Header("Shield")]
	public AudioClip ShieldRecharge;

	// Token: 0x040004E6 RID: 1254
	public AudioClip SelfShieldBreak;

	// Token: 0x040004E7 RID: 1255
	private Vector3 prevVel;

	// Token: 0x040004E8 RID: 1256
	private bool directionChangedRecently;

	// Token: 0x040004E9 RID: 1257
	private float lastJumpTime;

	// Token: 0x040004EA RID: 1258
	private float lastLandTime;
}
