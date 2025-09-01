using System;
using System.Collections.Generic;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class JumpPad : MonoBehaviour
{
	// Token: 0x06000BFE RID: 3070 RVA: 0x0004DCDC File Offset: 0x0004BEDC
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		if (this.icd > 0f)
		{
			this.icd -= Time.deltaTime;
			return;
		}
		Vector3 position = PlayerControl.myInstance.Movement.GetPosition();
		if (Vector3.Distance(base.transform.position, position) < this.Radius)
		{
			this.ApplyForce();
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0004DD48 File Offset: 0x0004BF48
	private void ApplyForce()
	{
		if (this.JumpSFX != null && this.JumpSFX.Count > 0)
		{
			AudioManager.PlayClipAtPoint(this.JumpSFX.GetRandomClip(-1), base.transform.position, 1f, 1f, 0.75f, 35f, 250f);
		}
		if (this.JumpVFX != null)
		{
			this.JumpVFX.Play();
		}
		this.icd = 1f;
		PlayerControl.myInstance.Movement.SetMomentum(this.ForceDir.forward * this.Force);
		int stacks = 1;
		if (this.AddedEffect != null && this.AddedEffect.Root.CanStack)
		{
			stacks = this.Stacks;
		}
		if (this.AddedEffect != null)
		{
			PlayerControl.myInstance.Net.ApplyStatus(this.AddedEffect.HashCode, -1, this.EffectDuration, stacks, false, 0);
		}
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0004DE48 File Offset: 0x0004C048
	private void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<ActionRigidbody>() != null)
		{
			col.GetComponent<Rigidbody>().AddForce(this.ForceDir.forward * this.Force * 0.66f, ForceMode.VelocityChange);
			if (this.JumpSFX != null && this.JumpSFX.Count > 0)
			{
				AudioManager.PlayClipAtPoint(this.JumpSFX.GetRandomClip(-1), base.transform.position, 1f, 1f, 0.75f, 35f, 250f);
			}
			if (this.JumpVFX != null)
			{
				this.JumpVFX.Play();
			}
		}
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0004DEF8 File Offset: 0x0004C0F8
	private void OnDrawGizmos()
	{
		BetterGizmos.DrawCircle2D(new Color(1f, 0.3f, 0.3f, 1f), base.transform.position, Vector3.up, this.Radius);
		Gizmos.color = new Color(0.75f, 0.1f, 0.1f, 0.3f);
		Gizmos.DrawSphere(base.transform.position, this.Radius);
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0004DF6D File Offset: 0x0004C16D
	public JumpPad()
	{
	}

	// Token: 0x040009B7 RID: 2487
	public float Radius = 3f;

	// Token: 0x040009B8 RID: 2488
	public float Force = 25f;

	// Token: 0x040009B9 RID: 2489
	public Transform ForceDir;

	// Token: 0x040009BA RID: 2490
	public List<AudioClip> JumpSFX;

	// Token: 0x040009BB RID: 2491
	public ParticleSystem JumpVFX;

	// Token: 0x040009BC RID: 2492
	public StatusTree AddedEffect;

	// Token: 0x040009BD RID: 2493
	public float EffectDuration = 1f;

	// Token: 0x040009BE RID: 2494
	public int Stacks = 1;

	// Token: 0x040009BF RID: 2495
	private float icd;
}
