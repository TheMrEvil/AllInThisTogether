using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000019 RID: 25
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class ActionRigidbody : MonoBehaviour, IPunObservable
{
	// Token: 0x06000084 RID: 132 RVA: 0x00006904 File Offset: 0x00004B04
	private void Awake()
	{
		this.startPt = base.transform.position;
		this.rb = base.GetComponent<Rigidbody>();
		this.view = base.GetComponent<PhotonView>();
		this.trigger = base.GetComponent<ProjectileTrigger>();
		if (this.trigger != null)
		{
			ProjectileTrigger projectileTrigger = this.trigger;
			projectileTrigger.OnHit = (Action<Vector3>)Delegate.Combine(projectileTrigger.OnHit, new Action<Vector3>(this.OnProjectileHit));
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000697C File Offset: 0x00004B7C
	private void FixedUpdate()
	{
		if (!this.view.IsMine)
		{
			if (Vector3.Distance(base.transform.position, this.pos) > 5f)
			{
				base.transform.position = this.pos;
			}
			else
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.pos, Time.fixedDeltaTime * 9f);
			}
			this.rb.velocity = this.vel;
			this.rb.angularVelocity = Vector3.Lerp(this.rb.angularVelocity, this.angVel, Time.fixedDeltaTime * 9f);
		}
		else
		{
			this.pos = base.transform.position;
			this.vel = this.rb.velocity;
			this.angVel = this.rb.angularVelocity;
		}
		if (this.impactSoundCD > 0f)
		{
			this.impactSoundCD -= Time.fixedDeltaTime;
		}
		if (this.CanReset && base.transform.position.y < -15f)
		{
			this.Reset();
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00006AAB File Offset: 0x00004CAB
	private void Reset()
	{
		base.transform.position = this.startPt;
		this.rb.velocity = Vector3.zero;
		this.rb.angularVelocity = Vector3.zero;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00006ADE File Offset: 0x00004CDE
	private void OnProjectileHit(Vector3 pos)
	{
		this.rb.AddExplosionForce(50f, pos, 5f, 0f);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00006AFC File Offset: 0x00004CFC
	private void OnCollisionEnter(Collision other)
	{
		PlayerControl componentInParent = other.gameObject.GetComponentInParent<PlayerControl>();
		if (componentInParent != null && componentInParent.view.OwnerActorNr != this.view.OwnerActorNr)
		{
			this.view.RequestOwnership();
		}
		List<ActionRigidbody.PhysSound> sounds = this.EnvImpacts;
		if (componentInParent != null)
		{
			sounds = this.PlayerImpacts;
		}
		this.PlayImpactSound(sounds, other.contacts[0].point, other.relativeVelocity.magnitude);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00006B80 File Offset: 0x00004D80
	private void PlayImpactSound(List<ActionRigidbody.PhysSound> sounds, Vector3 point, float speed)
	{
		if (speed < 3f || this.impactSoundCD > 0f)
		{
			return;
		}
		this.impactSoundCD = 0.33f;
		for (int i = sounds.Count - 1; i >= 0; i--)
		{
			if (sounds[i].ForceRequired <= speed)
			{
				sounds[i].PlaySound(point, Mathf.Clamp01((speed - 3f) / 3f));
				return;
			}
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00006BF0 File Offset: 0x00004DF0
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(this.rb.velocity);
			stream.SendNext(this.rb.angularVelocity);
			return;
		}
		this.pos = (Vector3)stream.ReceiveNext();
		this.vel = (Vector3)stream.ReceiveNext();
		this.angVel = (Vector3)stream.ReceiveNext();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00006C7B File Offset: 0x00004E7B
	public ActionRigidbody()
	{
	}

	// Token: 0x0400007B RID: 123
	private Vector3 startPt;

	// Token: 0x0400007C RID: 124
	private Rigidbody rb;

	// Token: 0x0400007D RID: 125
	private PhotonView view;

	// Token: 0x0400007E RID: 126
	private ProjectileTrigger trigger;

	// Token: 0x0400007F RID: 127
	private Vector3 pos;

	// Token: 0x04000080 RID: 128
	private Vector3 vel;

	// Token: 0x04000081 RID: 129
	private Vector3 angVel;

	// Token: 0x04000082 RID: 130
	public bool CanReset = true;

	// Token: 0x04000083 RID: 131
	public List<ActionRigidbody.PhysSound> EnvImpacts = new List<ActionRigidbody.PhysSound>();

	// Token: 0x04000084 RID: 132
	public List<ActionRigidbody.PhysSound> PlayerImpacts = new List<ActionRigidbody.PhysSound>();

	// Token: 0x04000085 RID: 133
	private float impactSoundCD;

	// Token: 0x020003E8 RID: 1000
	[Serializable]
	public class PhysSound
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x000BFFF8 File Offset: 0x000BE1F8
		public void PlaySound(Vector3 point, float volumeMult = 1f)
		{
			if (this.Clips.Count == 0)
			{
				return;
			}
			AudioManager.PlayClipAtPoint(this.Clips.GetRandomClip(-1), point, UnityEngine.Random.Range(0.9f, 1.1f), volumeMult, 1f, 10f, 250f);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000C0045 File Offset: 0x000BE245
		public PhysSound()
		{
		}

		// Token: 0x040020CC RID: 8396
		public float ForceRequired;

		// Token: 0x040020CD RID: 8397
		public List<AudioClip> Clips = new List<AudioClip>();
	}
}
