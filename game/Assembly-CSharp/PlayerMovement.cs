using System;
using System.Runtime.CompilerServices;
using CMF;
using Fluxy;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class PlayerMovement : EntityMovement
{
	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x000320CF File Offset: 0x000302CF
	public PlayerControl Control
	{
		get
		{
			return this.controller as PlayerControl;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060006CF RID: 1743 RVA: 0x000320DC File Offset: 0x000302DC
	// (set) Token: 0x060006D0 RID: 1744 RVA: 0x000320E4 File Offset: 0x000302E4
	public AdvancedWalkerController walkerController
	{
		[CompilerGenerated]
		get
		{
			return this.<walkerController>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<walkerController>k__BackingField = value;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000320ED File Offset: 0x000302ED
	// (set) Token: 0x060006D2 RID: 1746 RVA: 0x000320F5 File Offset: 0x000302F5
	public Mover mover
	{
		[CompilerGenerated]
		get
		{
			return this.<mover>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<mover>k__BackingField = value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060006D3 RID: 1747 RVA: 0x000320FE File Offset: 0x000302FE
	// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00032106 File Offset: 0x00030306
	public Vector3 extraVel
	{
		[CompilerGenerated]
		get
		{
			return this.<extraVel>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<extraVel>k__BackingField = value;
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00032110 File Offset: 0x00030310
	public override void Awake()
	{
		base.Awake();
		this.wantRot = Quaternion.identity;
		this.walkerController = base.GetComponentInChildren<AdvancedWalkerController>();
		this.mover = base.GetComponentInChildren<Mover>();
		this.rb = this.walkerController.GetComponent<Rigidbody>();
		this.headLook = base.GetComponentInChildren<HeadAim>();
		this.input = base.GetComponent<PlayerInput>();
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00032170 File Offset: 0x00030370
	public override void Setup()
	{
		if (this.controller.IsMine)
		{
			PlayerMovement.myCamera = this.cameraRoot.GetComponentInChildren<Camera>();
			FluxySolver.LODCam = PlayerMovement.myCamera;
			this.headLook.localControl = true;
			AdvancedWalkerController walkerController = this.walkerController;
			walkerController.OnJump = (Controller.VectorEvent)Delegate.Combine(walkerController.OnJump, new Controller.VectorEvent(this.OnJumped));
			AdvancedWalkerController walkerController2 = this.walkerController;
			walkerController2.OnLand = (Action<Vector3, Vector3, Vector3>)Delegate.Combine(walkerController2.OnLand, new Action<Vector3, Vector3, Vector3>(this.OnLand));
		}
		else
		{
			this.headLook.localControl = false;
			this.DisableMover();
			UnityEngine.Object.Destroy(this.cameraRoot);
		}
		EntityHealth health = this.controller.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(delegate(DamageInfo <p0>)
		{
			this.DisableMover();
		}));
		EntityHealth health2 = this.controller.health;
		health2.OnRevive = (Action<int>)Delegate.Combine(health2.OnRevive, new Action<int>(delegate(int x)
		{
			this.EnableMover();
		}));
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00032278 File Offset: 0x00030478
	public override void Update()
	{
		base.Update();
		if (!this.controller.IsMine)
		{
			if (this.controller.IsDead)
			{
				return;
			}
			if (Vector3.Distance(this.rb.transform.position, this.wantPosition) > 15f)
			{
				this.rb.transform.position = this.wantPosition;
			}
			PlayerMovement.SyncMode syncMode = this.syncMode;
			if (syncMode != PlayerMovement.SyncMode.Lerp)
			{
				if (syncMode == PlayerMovement.SyncMode.New)
				{
					float num = Vector3.Distance(this.rb.transform.position, this.wantPosition);
					this.rb.transform.position = Vector3.MoveTowards(this.rb.transform.position, this.wantPosition, this.wantVel.magnitude * 1f * Time.deltaTime);
					this.rb.transform.position = Vector3.Lerp(this.rb.transform.position, this.wantPosition, Time.deltaTime * ((num < 25f) ? 0.5f : 8f));
					this.rb.velocity = Vector3.Lerp(this.rb.velocity, this.wantVel, Time.deltaTime * 9f);
					this.lerpVel = Vector3.Lerp(this.lerpVel, this.wantVel, Time.deltaTime * 9f);
				}
			}
			else
			{
				this.rb.transform.position = Vector3.Lerp(this.rb.transform.position, this.wantPosition, Time.deltaTime * 9f);
				this.rb.velocity = Vector3.Lerp(this.rb.velocity, this.wantVel, Time.deltaTime * 9f);
				this.lerpVel = Vector3.Lerp(this.lerpVel, this.wantVel, Time.deltaTime * 9f);
			}
			this.walkerController.SetMomentum(this.lerpVel);
		}
		else
		{
			this.extraVel = Vector3.Lerp(this.extraVel, Vector3.zero, Time.deltaTime * 2f);
			Shader.SetGlobalVector("_CameraPos", PlayerMovement.myCamera.transform.position);
			Shader.SetGlobalVector("_PlayerPos", this.Control.Display.HeadRoot.position);
			this.walkerController.frictionMultGround = Mathf.Lerp(this.walkerController.frictionMultGround, 1f, Time.deltaTime * 2f);
			this.walkerController.frictionMultAir = Mathf.Lerp(this.walkerController.frictionMultAir, 1f, Time.deltaTime);
		}
		this.RecalculateCollider();
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00032540 File Offset: 0x00030740
	public override void DisableMover()
	{
		this.walkerController.enabled = false;
		this.rb.isKinematic = true;
		this.mover.enabled = false;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00032568 File Offset: 0x00030768
	public override void EnableMover()
	{
		if (!this.controller.IsMine)
		{
			return;
		}
		Vector3 position = this.GetPosition();
		Vector3 forward = this.GetForward();
		this.walkerController.enabled = true;
		this.mover.enabled = true;
		this.rb.isKinematic = false;
		this.SetPositionImmediate(position, forward, true);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x000325BE File Offset: 0x000307BE
	public override bool IsMoverEnabled()
	{
		return this.walkerController.enabled || this.mover.enabled;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x000325DC File Offset: 0x000307DC
	private void RecalculateCollider()
	{
		if (Mathf.Abs(this.Control.display.displayHolder.localScale.y - this.lastScaleRecalculated) < 0.05f)
		{
			return;
		}
		this.lastScaleRecalculated = this.Control.display.displayHolder.localScale.y;
		this.mover.RecalculateColliderDimensions();
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00032644 File Offset: 0x00030844
	public override bool IsMoving()
	{
		return this.IsMoverEnabled() && this.GetVelocity().magnitude > 0.25f;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00032670 File Offset: 0x00030870
	public override Vector3 GetPosition()
	{
		return this.walkerController.transform.position;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00032682 File Offset: 0x00030882
	public override Vector3 GetForward()
	{
		return Vector3.ProjectOnPlane(this.headAim.transform.forward, Vector3.up);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0003269E File Offset: 0x0003089E
	public override Quaternion GetRotation()
	{
		return this.walkerController.transform.rotation;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x000326B0 File Offset: 0x000308B0
	public override Vector3 GetVelocity()
	{
		if (!this.Control.IsMine)
		{
			return this.lerpVel;
		}
		return this.walkerController.GetVelocity();
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x000326D1 File Offset: 0x000308D1
	public Vector3 GetPlanarVelocity()
	{
		return Vector3.ProjectOnPlane(this.GetVelocity(), Vector3.up);
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x000326E3 File Offset: 0x000308E3
	public float GetAirHeight()
	{
		if (!this.walkerController.IsGrounded())
		{
			return this.walkerController.GetAirHeight();
		}
		return 0f;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00032703 File Offset: 0x00030903
	public void ResetVelocity()
	{
		this.walkerController.ResetMomentum();
		this.walkerController.ResetVelocity();
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0003271C File Offset: 0x0003091C
	public override void AddForce(ApplyForceNode node, Vector3 dir, EffectProperties props)
	{
		this.walkerController.frictionMultGround = 0f;
		this.walkerController.frictionMultAir = 20f;
		float num = node.GetForceValue(props);
		if (this.mover.IsGrounded() || node.ForceMode == ForceMode.VelocityChange)
		{
			num *= 7f;
		}
		else
		{
			num *= 5f * node.InAirMultiplier;
		}
		Vector3 b = node.VerticalAdd * Vector3.up * num;
		if (node.ForceMode == ForceMode.VelocityChange)
		{
			this.walkerController.SetMomentum(dir * num);
			return;
		}
		if (node.ForceMode == ForceMode.Acceleration)
		{
			this.extraVel = dir * num;
			return;
		}
		this.walkerController.AddMomentum(dir * num + b);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x000327E2 File Offset: 0x000309E2
	public void MoveForce(Vector3 force)
	{
		this.walkerController.frictionMultGround = 0f;
		this.walkerController.frictionMultAir = 20f;
		this.walkerController.ApplyVerticalMomentum(force);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00032810 File Offset: 0x00030A10
	public void SetMomentum(Vector3 vel)
	{
		this.walkerController.SetMomentum(vel);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00032820 File Offset: 0x00030A20
	private void OnJumped(Vector3 v)
	{
		EffectProperties props = new EffectProperties(this.Control);
		this.Control.TriggerSnippets(EventTrigger.OnJump, props, 1f);
		Action jumped = this.Jumped;
		if (jumped != null)
		{
			jumped();
		}
		this.Control.Net.Jumped();
		this.Control.Audio.PlayEventSound(0);
		this.Control.Display.OnJumped();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00032890 File Offset: 0x00030A90
	private void OnLand(Vector3 momentum, Vector3 point, Vector3 surfaceNormal)
	{
		EffectProperties effectProperties = new EffectProperties(this.Control);
		effectProperties.SetExtra(EProp.DynamicInput, momentum.magnitude);
		this.Control.TriggerSnippets(EventTrigger.Player_OnLand, effectProperties, 1f);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000328CA File Offset: 0x00030ACA
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		this.wantPosition = pos;
		this.wantVel = vel;
		this.wantRot = rot;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x000328E1 File Offset: 0x00030AE1
	public void MapChanged()
	{
		this.mover.RecalculateColliderDimensions();
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x000328F0 File Offset: 0x00030AF0
	public void SetPositionWithCamera(Vector3 point, Vector3 forward, bool clearMomentum = true, bool changeCamera = true)
	{
		this.SetPositionImmediate(point, forward, clearMomentum);
		if (changeCamera && this.Control.IsMine)
		{
			Vector3 vector = Vector3.ProjectOnPlane(forward, Vector3.up);
			if (vector.magnitude > 0f && forward.magnitude > 0f)
			{
				this.Control.Display.CamController.RotateTowardDirection(vector.normalized, 90000f);
			}
		}
		this.headAim.SetForwardExplicit(forward);
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0003296C File Offset: 0x00030B6C
	public void SetPositionWithExplicitCamera(Vector3 point, Vector3 forward, Vector3 cameraForward)
	{
		this.SetPositionImmediate(point, forward, true);
		if (this.Control.IsMine)
		{
			this.Control.Display.CamController.RotateTowardDirection(cameraForward.normalized, 90000f);
		}
		this.headAim.SetForwardExplicit(forward);
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000329BC File Offset: 0x00030BBC
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		this.rb.MovePosition(point);
		this.rb.transform.position = point;
		Vector3 vector = Vector3.ProjectOnPlane(forward, Vector3.up);
		if (vector.magnitude > 0f)
		{
			this.rb.transform.forward = vector.normalized;
		}
		if (clearMomentum)
		{
			this.rb.velocity = Vector3.zero;
			this.rb.angularVelocity = Vector3.zero;
		}
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00032A3A File Offset: 0x00030C3A
	public PlayerMovement()
	{
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00032A4D File Offset: 0x00030C4D
	[CompilerGenerated]
	private void <Setup>b__25_0(DamageInfo <p0>)
	{
		this.DisableMover();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00032A55 File Offset: 0x00030C55
	[CompilerGenerated]
	private void <Setup>b__25_1(int x)
	{
		this.EnableMover();
	}

	// Token: 0x040005A7 RID: 1447
	public static Camera myCamera;

	// Token: 0x040005A8 RID: 1448
	public GameObject cameraRoot;

	// Token: 0x040005A9 RID: 1449
	public PlayerMovement.SyncMode syncMode;

	// Token: 0x040005AA RID: 1450
	[NonSerialized]
	public PlayerInput input;

	// Token: 0x040005AB RID: 1451
	[NonSerialized]
	public HeadAim headLook;

	// Token: 0x040005AC RID: 1452
	[SerializeField]
	private HeadAim headAim;

	// Token: 0x040005AD RID: 1453
	[CompilerGenerated]
	private AdvancedWalkerController <walkerController>k__BackingField;

	// Token: 0x040005AE RID: 1454
	[CompilerGenerated]
	private Mover <mover>k__BackingField;

	// Token: 0x040005AF RID: 1455
	private Rigidbody rb;

	// Token: 0x040005B0 RID: 1456
	private float lastScaleRecalculated = 1f;

	// Token: 0x040005B1 RID: 1457
	private Vector3 lerpVel;

	// Token: 0x040005B2 RID: 1458
	[CompilerGenerated]
	private Vector3 <extraVel>k__BackingField;

	// Token: 0x040005B3 RID: 1459
	public Action Jumped;

	// Token: 0x020004A9 RID: 1193
	public enum SyncMode
	{
		// Token: 0x040023EE RID: 9198
		Lerp,
		// Token: 0x040023EF RID: 9199
		New
	}
}
