using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000095 RID: 149
public class PlayerPing : MonoBehaviour
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000729 RID: 1833 RVA: 0x00033D01 File Offset: 0x00031F01
	// (set) Token: 0x0600072A RID: 1834 RVA: 0x00033D09 File Offset: 0x00031F09
	public PlayerControl control
	{
		[CompilerGenerated]
		get
		{
			return this.<control>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<control>k__BackingField = value;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x0600072B RID: 1835 RVA: 0x00033D12 File Offset: 0x00031F12
	public bool IsMine
	{
		get
		{
			return this.control != null && this.control.IsMine;
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00033D2F File Offset: 0x00031F2F
	private void Start()
	{
		this.control = base.GetComponentInParent<PlayerControl>();
		WorldIndicators.Indicate(this);
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.Clear));
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00033D64 File Offset: 0x00031F64
	public bool TryPing(PlayerDB.PingType ping)
	{
		if (!this.IsMine || PlayerControl.MyCamera == null)
		{
			return false;
		}
		if (this.icd > 0f)
		{
			return false;
		}
		Vector3 position = PlayerControl.MyCamera.transform.position;
		Vector3 forward = PlayerControl.MyCamera.transform.forward;
		if (PlayerControl.myInstance.currentTarget != null)
		{
			Vector3 vector = PlayerControl.myInstance.currentTarget.display.CenterOfMass.position - PlayerControl.MyCamera.transform.position;
			if (Vector3.Dot(vector.normalized, forward) > this.DotCancelDist.Evaluate(vector.magnitude))
			{
				if (this.IsVisible && this.FollowEntity == PlayerControl.myInstance.currentTarget)
				{
					return this.CancelPing(true);
				}
				if (ping == PlayerDB.PingType.Generic)
				{
					ping = PlayerDB.PingType.Attack;
				}
				return this.ConfirmPing(Vector3.zero, ping, PlayerControl.myInstance.currentTarget, true);
			}
		}
		RaycastHit raycastHit;
		if (!Physics.Raycast(new Ray(position, forward), out raycastHit, 500f, this.HitMask))
		{
			return this.CancelPing(true);
		}
		EntityControl componentInParent = raycastHit.collider.GetComponentInParent<EntityControl>();
		if (componentInParent != null && !(componentInParent is PlayerControl))
		{
			if (this.IsVisible && this.FollowEntity == componentInParent)
			{
				return this.CancelPing(true);
			}
			return this.ConfirmPing(Vector3.zero, ping, componentInParent, true);
		}
		else
		{
			Vector3 vector2 = this.NearestNavPt(raycastHit.point);
			bool needsOffset = true;
			if (!vector2.IsValid())
			{
				needsOffset = false;
				vector2 = raycastHit.point;
			}
			if (Vector3.Distance(this.Position, vector2) < 3f && (ping == this.CurrentPing || ping == PlayerDB.PingType.Generic))
			{
				return this.CancelPing(true);
			}
			return this.ConfirmPing(vector2, ping, null, needsOffset);
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00033F2F File Offset: 0x0003212F
	private bool ConfirmPing(Vector3 pos, PlayerDB.PingType pingType, EntityControl entity = null, bool needsOffset = true)
	{
		this.icd = 0.3f;
		this.CurrentPing = pingType;
		this.control.Net.Ping(pingType, pos, entity, needsOffset);
		return true;
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00033F5C File Offset: 0x0003215C
	public void SetPing(PlayerDB.PingType ping, Vector3 pos, int EntityID, bool needOffset)
	{
		this.Position = pos;
		this.NeedsOffset = needOffset;
		if (EntityID > 0)
		{
			this.FollowEntity = EntityControl.GetEntity(EntityID);
		}
		else
		{
			this.FollowEntity = null;
		}
		this.IsVisible = true;
		this.offsetT = 0f;
		this.releaseTimer = 15f;
		if (this.NeedsOffset && this.FollowEntity == null)
		{
			PlayerDB.PingDisplay ping2 = PlayerDB.GetPing(ping);
			if (ping2 != null)
			{
				ParticleSystem.MainModule main = this.particleBeam.main;
				Color color = ping2.Color;
				main.startColor = new ParticleSystem.MinMaxGradient(color);
			}
			this.particleBeam.Play();
		}
		else
		{
			this.particleBeam.Stop();
			this.particleBeam.Clear();
		}
		AudioManager.PlaySFX2D(this.Clips_Pinged.GetRandomClip(-1), 1f, 0.1f);
		Action<PlayerDB.PingType> onPinged = this.OnPinged;
		if (onPinged == null)
		{
			return;
		}
		onPinged(ping);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0003403D File Offset: 0x0003223D
	private void Clear()
	{
		this.Clear(false);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00034046 File Offset: 0x00032246
	public void Clear(bool active = true)
	{
		if (!this.IsVisible)
		{
			return;
		}
		this.CancelPing(active);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0003405C File Offset: 0x0003225C
	private bool CancelPing(bool active)
	{
		if (!this.IsVisible)
		{
			return false;
		}
		this.Position = Vector3.one.INVALID();
		this.FollowEntity = null;
		this.IsVisible = false;
		this.particleBeam.Stop();
		this.control.Net.ClearPing();
		if (active)
		{
			AudioManager.PlaySFX2D(this.Clips_Canceled.GetRandomClip(-1), 1f, 0.1f);
		}
		return true;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000340CC File Offset: 0x000322CC
	private void LateUpdate()
	{
		if (this.icd > 0f)
		{
			this.icd -= Time.deltaTime;
		}
		if (!this.IsVisible)
		{
			return;
		}
		this.releaseTimer -= Time.deltaTime;
		if (this.releaseTimer <= 0f)
		{
			this.CancelPing(false);
			return;
		}
		if (this.FollowEntity != null)
		{
			if (this.FollowEntity.IsDead)
			{
				this.CancelPing(false);
				return;
			}
			base.transform.position = this.FollowEntity.display.CenterOfMass.position;
			return;
		}
		else
		{
			if (this.NeedsOffset)
			{
				if (this.offsetT < 1f)
				{
					this.offsetT += Time.deltaTime / this.OffsetDuration;
				}
				base.transform.position = Vector3.Lerp(this.Position, this.Position + this.WantOffset, this.OffsetCurve.Evaluate(this.offsetT));
				return;
			}
			base.transform.position = this.Position;
			return;
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x000341E8 File Offset: 0x000323E8
	private Vector3 NearestNavPt(Vector3 p)
	{
		NavMeshHit navMeshHit;
		if (!NavMesh.SamplePosition(p, out navMeshHit, 2f, -1))
		{
			return p.INVALID();
		}
		return navMeshHit.position;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00034213 File Offset: 0x00032413
	private void OnDestroy()
	{
		WorldIndicators.ReleaseIndicator(this);
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.Clear));
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0003423B File Offset: 0x0003243B
	public PlayerPing()
	{
	}

	// Token: 0x040005B8 RID: 1464
	[CompilerGenerated]
	private PlayerControl <control>k__BackingField;

	// Token: 0x040005B9 RID: 1465
	public AnimationCurve DotCancelDist;

	// Token: 0x040005BA RID: 1466
	public LayerMask HitMask;

	// Token: 0x040005BB RID: 1467
	public ParticleSystem particleBeam;

	// Token: 0x040005BC RID: 1468
	private float icd;

	// Token: 0x040005BD RID: 1469
	private float releaseTimer;

	// Token: 0x040005BE RID: 1470
	private float offsetT;

	// Token: 0x040005BF RID: 1471
	public float OffsetDuration = 0.5f;

	// Token: 0x040005C0 RID: 1472
	public AnimationCurve OffsetCurve;

	// Token: 0x040005C1 RID: 1473
	public Vector3 WantOffset = new Vector3(0f, 3f, 0f);

	// Token: 0x040005C2 RID: 1474
	public List<AudioClip> Clips_Pinged;

	// Token: 0x040005C3 RID: 1475
	public List<AudioClip> Clips_Canceled;

	// Token: 0x040005C4 RID: 1476
	public bool IsVisible;

	// Token: 0x040005C5 RID: 1477
	public Vector3 Position;

	// Token: 0x040005C6 RID: 1478
	public bool NeedsOffset;

	// Token: 0x040005C7 RID: 1479
	public EntityControl FollowEntity;

	// Token: 0x040005C8 RID: 1480
	private PlayerDB.PingType CurrentPing;

	// Token: 0x040005C9 RID: 1481
	public Action<PlayerDB.PingType> OnPinged;
}
